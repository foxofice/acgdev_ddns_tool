//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//--------------------------------------------------------------------------------------

#include "../../Common/Encrypt.h"
#include "../../ddns_server_CLR/ddns_server_CLR.h"

#include "../Socket/Socket.h"
#include "../Config/Config.h"
#include "../Session_KeyIV/Session_KeyIV.h"
#include "../Log/Log.h"
#include "packet_header.h"
#include "packet.h"

namespace DDNS_Server
{
namespace Packet
{

/*==============================================================
 * 解析 packet
 * parse_packet()
 *==============================================================*/
void parse_packet(struct NNN::Socket::s_SessionData *sd)
{
	if(sd == nullptr)
		return;

	BYTE total_header[2] = {};
	while(sd->RECV_DATA.m_buffer.get_data_len() >= PACKET_HEADER_LEN * 2)
	{
		size_t read_len = 0;
		sd->RECV_DATA.m_buffer.read_data((BYTE*)total_header, sizeof(total_header), read_len, false);

		// 解码 header
		BYTE header = Common::Encrypt::decode_header(total_header[0], total_header[1]);

		es_Parse_Result res = es_Parse_Result::Unknown;

#define SWITCH_LINE(val, func_name)	case (BYTE)val:	res = func_name(sd);	break;

		// 解析 packet
		switch(header)
		{
		// Client 发送 Ping
		SWITCH_LINE(es_Header::Client_Ping,				recv_Ping);

		//【连接 Server 需要登录密码验证时】

		// Client 发送「登录数据」
		SWITCH_LINE(es_Header::Client_Login_Data,		recv_Login_Data);

		//【登录验证后】

		// Client 发送「更新域名的 A/AAAA 记录」
		SWITCH_LINE(es_Header::Client_Update_Domains,	recv_Update_Domains);
		}	// switch

#undef SWITCH_LINE

		switch(res)
		{
		case es_Parse_Result::Unknown:
			Log::ShowDebug("Unknown packet " NNN_CL_VALUE "%u" NNN_CL_RESET "! ", header);
			break;

		case es_Parse_Result::Attack:
			Log::ShowDebug("Packet " NNN_CL_VALUE "%u" NNN_CL_RESET " attack!! ", header);
			break;

		case es_Parse_Result::Error:
			Log::ShowDebug("Packet " NNN_CL_VALUE "%u" NNN_CL_RESET " parse error!!!! ", header);
			break;
		}	// switch

		CHECK_PACKET_RES_SERVER(Socket::g_server);
	}	// while
}


/*==============================================================
 * Client 发送 Ping
 * recv_Ping()
 *==============================================================*/
es_Parse_Result recv_Ping(struct NNN::Socket::s_SessionData *sd)
{
	BYTE								packet_data[PACKET_HEADER_LEN * 2	+
													sizeof(double)];	// client_time
	struct NNN::Buffer::s_BinaryReader	br(packet_data);

	// 读取 packet_data
	NNN_PACKET_READ_DATA_FIXED(sd->RECV_DATA.m_buffer, sizeof(packet_data));

	// 解析数据
	double client_time = br.read<double>();

	// 发送
	send_Ping(sd, client_time);

	return es_Parse_Result::OK;
}


/*==============================================================
 * Client 发送「登录数据」
 * recv_Login_Data()
 *==============================================================*/
es_Parse_Result recv_Login_Data(struct NNN::Socket::s_SessionData *sd)
{
	BYTE								packet_data[PACKET_HEADER_LEN * 2	+
													AES_KEY_LEN				+	// send_Key
													AES_IV_LEN];				// send_IV
	struct NNN::Buffer::s_BinaryReader	br(packet_data);

	// 读取 packet_data
	NNN_PACKET_READ_DATA_FIXED(sd->RECV_DATA.m_buffer, sizeof(packet_data));

	// 合法性判定
	if(sd->M_SD_DATA__LOGIN_DONE > 0)
	{
		if(Config::g_config->LOG.m_show_login_result)
		{
			char ip_buffer[46];

			Log::ShowMessage(	NNN_CL_VALUE "%s:%u" NNN_CL_RESET " re-login\n",
								sd->GetClientIP(ip_buffer), sd->m_port );
		}

		return es_Parse_Result::Attack;
	}

	// 解析数据
	BYTE Key[AES_KEY_LEN];
	BYTE IV[AES_IV_LEN];
	Common::Encrypt::gen_KeyIV(	Config::g_config->LOGIN.m_password.c_str(),
								Config::g_config->LOGIN.m_user.c_str(),
								Key,
								IV );

	// 解密数据
	BYTE data[AES_KEY_LEN + AES_IV_LEN + ETHER_ADDR_LEN];
	HRESULT hr = NNN::Encrypt::Rijndael_Decrypt(packet_data + br.m_offset,
												sizeof(packet_data) - br.m_offset,
												Key,
												IV,
												data);

	if(FAILED(hr))
	{
		if(Config::g_config->LOG.m_show_login_result)
		{
			char ip_buffer[46];

			Log::ShowMessage(	NNN_CL_VALUE "%s:%u" NNN_CL_RESET " login failed (decrypt failed)\n",
								sd->GetClientIP(ip_buffer), sd->m_port );
		}

		send_Login_Result(sd, es_Result::Failed);
		return es_Parse_Result::Attack;
	}

	struct NNN::Buffer::s_BinaryReader br_data(data);
	const BYTE *send_Key	= br_data.read_array(AES_KEY_LEN);
	const BYTE *send_IV		= br_data.read_array(AES_IV_LEN);

	// 检查 send_Key/send_IV
	struct s_AES_KeyIV *key_iv = (struct s_AES_KeyIV*)sd->M_SD_DATA__KEY_IV.load();

	if(key_iv == nullptr)
	{
		if(Config::g_config->LOG.m_show_login_result)
		{
			char ip_buffer[46];

			Log::ShowMessage(	NNN_CL_VALUE "%s:%u" NNN_CL_RESET " login failed (no KeyIV)\n",
								sd->GetClientIP(ip_buffer), sd->m_port );
		}

		send_Login_Result(sd, es_Result::Failed);
		return es_Parse_Result::Attack;
	}

	if(	memcmp(key_iv->m_Key, send_Key, AES_KEY_LEN)	||
		memcmp(key_iv->m_IV, send_IV, AES_IV_LEN) )
	{
		if(Config::g_config->LOG.m_show_login_result)
		{
			char ip_buffer[46];

			Log::ShowMessage(	NNN_CL_VALUE "%s:%u" NNN_CL_RESET " login failed (KeyIV not match)\n",
								sd->GetClientIP(ip_buffer), sd->m_port );
		}

		send_Login_Result(sd, es_Result::Failed);
		return es_Parse_Result::Attack;
	}

	sd->M_SD_DATA__LOGIN_DONE = 1;

	// 发送
	send_Login_Result(sd, es_Result::OK);

	Socket::remove_logining_sessions(sd->m_session_id);

	if(Config::g_config->LOG.m_show_login_result)
	{
		char ip_buffer[46];

		Log::ShowMessage(	NNN_CL_VALUE "%s:%u" NNN_CL_RESET " login ok\n",
							sd->GetClientIP(ip_buffer), sd->m_port );
	}

	return es_Parse_Result::OK;
}

//【登录验证后】

/*==============================================================
 * Client 发送「更新域名的 A/AAAA 记录」
 * recv_Update_Domains()
 *==============================================================*/
es_Parse_Result recv_Update_Domains(struct NNN::Socket::s_SessionData *sd)
{
	BYTE								packet_data[USHRT_MAX];
	struct NNN::Buffer::s_BinaryReader	br(packet_data);
	USHORT								packet_len	= 0;

	// 读取 packet_data
	NNN_PACKET_READ_DATA(sd->RECV_DATA.m_buffer);

	// 合法性判定
	if(sd->M_SD_DATA__LOGIN_DONE == 0)
		return es_Parse_Result::Attack;

	struct s_AES_KeyIV *KeyIV = (struct s_AES_KeyIV*)sd->M_SD_DATA__KEY_IV.load();
	if(KeyIV == nullptr)
		return es_Parse_Result::Error;

	USHORT		aes_data_len	= (USHORT)(packet_len - br.m_offset);
	const BYTE	*aes_data		= br.read_array(aes_data_len);

	// 解密
	BYTE data[USHRT_MAX];
	HRESULT hr = NNN::Encrypt::Rijndael_Decrypt(aes_data,
												aes_data_len,
												KeyIV->m_Key,
												KeyIV->m_IV,
												data);

	if(FAILED(hr))
		return es_Parse_Result::Attack;

	struct NNN::Buffer::s_BinaryReader br_data(data);

	BYTE		Key_len		= br_data.read<BYTE>();
	const BYTE	*Key_		= br_data.read_array(Key_len);

	BYTE		Secret_len	= br_data.read<BYTE>();
	const BYTE	*Secret_	= br_data.read_array(Secret_len);

	BYTE		ip_len		= br_data.read<BYTE>();
	const BYTE	*ip_		= br_data.read_array(ip_len);

	// 创建 records 列表
	USHORT domains_count = br_data.read<USHORT>();

	std::vector<struct ddns_server_CLR::s_Record> records;
	records.resize(domains_count);

	for(USHORT i=0; i<domains_count; ++i)
	{
		struct ddns_server_CLR::s_Record &record = records[i];

		BYTE		name_len	= br_data.read<BYTE>();
		const BYTE	*name		= br_data.read_array(name_len);

		BYTE		domain_len	= br_data.read<BYTE>();
		const BYTE	*domain		= br_data.read_array(domain_len);

		int			TTL			= br_data.read<int>();
		int			user_idx	= br_data.read<int>();

		CopyMemory(record.m_name,	name,	name_len);
		CopyMemory(record.m_domain,	domain,	domain_len);
		record.m_TTL		= TTL;
		record.m_user_idx	= user_idx;
	}	// for

	char ip[0xff + 1];

	if(ip_len == 0)
		sd->GetClientIP(ip);
	else
	{
		CopyMemory(ip, ip_, ip_len);
		ip[ip_len] = '\0';
	}

	char Key[0xff + 1];
	CopyMemory(Key, Key_, Key_len);
	Key[Key_len] = '\0';

	char Secret[0xff + 1];
	CopyMemory(Secret, Secret_, Secret_len);
	Secret[Secret_len] = '\0';

	char client_ip[46];
	NNN::Console::show_message(	NNN_CL_GREEN "========== %s:%u update domains ==========(Start)" NNN_CL_RESET "\n",
								sd->GetClientIP(client_ip),
								sd->m_port );

	// 执行更新
	ddns_server_CLR::update_records_step3_update_by_server(ip, Key, Secret, records);

	// 回发结果
	send_Update_Domains_Result(sd, ip, records);

	NNN::Console::show_message(	NNN_CL_BLUE "========== %s:%u update domains ==========(End)" NNN_CL_RESET "\n",
								client_ip,
								sd->m_port );

	return es_Parse_Result::OK;
}

}	// namespace Packet
}	// namespace DDNS_Server
