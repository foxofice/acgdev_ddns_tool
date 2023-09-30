//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//--------------------------------------------------------------------------------------

#include "../../Common/Encrypt.h"

#include "../Socket/Socket.h"
#include "../Config/Config.h"
#include "../Session_KeyIV/Session_KeyIV.h"
#include "packet_header.h"
#include "packet.h"

namespace DDNS_Server
{
namespace Packet
{

/*==============================================================
 * Server 发送心跳包
 * send_KeepAlive()
 *==============================================================*/
void send_KeepAlive(struct NNN::Socket::s_SessionData *sd)
{
	BYTE								packet_data[PACKET_HEADER_LEN * 2];
	struct NNN::Buffer::s_BinaryWriter	bw(packet_data);

	// 填充数据
	BYTE xor_val		= (BYTE)(rand() & 0xff);
	BYTE header_encode	= Common::Encrypt::encode_header((BYTE)es_Header::Server_KeepAlive, xor_val);

	bw.write<BYTE>(header_encode);
	bw.write<BYTE>(xor_val);

	Socket::g_server->Send(packet_data, sizeof(packet_data), sd);
}


/*==============================================================
 * Server 回应 Ping
 * send_Ping()
 *==============================================================*/
void send_Ping(struct NNN::Socket::s_SessionData *sd, double client_time)
{
	BYTE								packet_data[PACKET_HEADER_LEN * 2	+
													sizeof(client_time)];
	struct NNN::Buffer::s_BinaryWriter	bw(packet_data);

	// 填充数据
	BYTE xor_val		= (BYTE)(rand() & 0xff);
	BYTE header_encode	= Common::Encrypt::encode_header((BYTE)es_Header::Server_Ping, xor_val);

	bw.write<BYTE>(header_encode);
	bw.write<BYTE>(xor_val);
	bw.write<double>(client_time);

	// 发送
	Socket::g_server->Send(packet_data, sizeof(packet_data), sd);
}

//【连接 Server 需要登录密码验证时】

/*==============================================================
 * Server 发送 KeyIV
 * send_KeyIV()
 *==============================================================*/
void send_KeyIV(struct NNN::Socket::s_SessionData *sd)
{
	BYTE								aes_input[AES_KEY_LEN + AES_IV_LEN];
	BYTE								packet_data[PACKET_HEADER_LEN * 2	+
													sizeof(aes_input)];
	struct NNN::Buffer::s_BinaryWriter	bw(packet_data);

	// 填充数据
	BYTE xor_val		= (BYTE)(rand() & 0xff);
	BYTE header_encode	= Common::Encrypt::encode_header((BYTE)es_Header::Server_KeyIV, xor_val);

	bw.write<BYTE>(header_encode);
	bw.write<BYTE>(xor_val);

	BYTE Key[AES_KEY_LEN];
	BYTE IV[AES_IV_LEN];
	Common::Encrypt::gen_KeyIV(	Config::g_config->LOGIN.m_user.c_str(),
								Config::g_config->LOGIN.m_password.c_str(),
								Key,
								IV );

	struct s_AES_KeyIV *key_iv = (struct s_AES_KeyIV*)sd->M_SD_DATA__KEY_IV.load();

	if(key_iv == nullptr)
		key_iv = Session_KeyIV::add_KeyIV(sd->m_session_id);

	sd->M_SD_DATA__KEY_IV = (UINT64)key_iv;

	CopyMemory(aes_input,							key_iv->m_Key,	sizeof(key_iv->m_Key));
	CopyMemory(aes_input + sizeof(key_iv->m_Key),	key_iv->m_IV,	sizeof(key_iv->m_IV));

	// 加密
	NNN::Encrypt::Rijndael_Encrypt(aes_input, sizeof(aes_input), Key, IV, packet_data + bw.m_offset);

	// 发送
	Socket::g_server->Send(packet_data, sizeof(packet_data), sd);
}


/*==============================================================
 * Server 发送「登录结果」
 * send_Login_Result()
 *==============================================================*/
void send_Login_Result(struct NNN::Socket::s_SessionData *sd, es_Result result)
{
	BYTE								packet_data[PACKET_HEADER_LEN * 2	+
													sizeof(BYTE)];	// result
	struct NNN::Buffer::s_BinaryWriter	bw(packet_data);

	// 填充数据
	BYTE xor_val		= (BYTE)(rand() & 0xff);
	BYTE header_encode	= Common::Encrypt::encode_header((BYTE)es_Header::Server_Login_Result, xor_val);

	bw.write<BYTE>(header_encode);
	bw.write<BYTE>(xor_val);
	bw.write<BYTE>((BYTE)result);

	// 发送
	Socket::g_server->Send(packet_data, sizeof(packet_data), sd);
}

//【登录验证后】

/*==============================================================
 * Server 发送「更新域名的 A/AAAA 记录的结果」
 * send_Update_Domains_Result()
 *==============================================================*/
void send_Update_Domains_Result(struct NNN::Socket::s_SessionData					*sd,
								const char											ip[46],
								const std::vector<struct ddns_server_CLR::s_Record>	&records)
{
	BYTE								packet_data[USHRT_MAX];
	struct NNN::Buffer::s_BinaryWriter	bw(packet_data);

	// 合法性判定
	struct s_AES_KeyIV *KeyIV = (struct s_AES_KeyIV*)sd->M_SD_DATA__KEY_IV.load();
	if(KeyIV == nullptr)
		return;

	// 填充数据
	BYTE xor_val		= (BYTE)(rand() & 0xff);
	BYTE header_encode	= Common::Encrypt::encode_header((BYTE)es_Header::Server_Update_Domains_Result, xor_val);

	bw.write<BYTE>(header_encode);
	bw.write<BYTE>(xor_val);

	USHORT &packet_len = *bw.write<USHORT>(0);

	BYTE								aes_data[USHRT_MAX];
	struct NNN::Buffer::s_BinaryWriter	bw_aes(aes_data);

	// 填充 aes_data
	BYTE ip_len = (BYTE)strlen(ip);

	bw_aes.write<BYTE>(ip_len);
	bw_aes.write_array(ip, ip_len);

	bw_aes.write<USHORT>((USHORT)records.size());

	for(const struct ddns_server_CLR::s_Record &record : records)
	{
		// name_len
		BYTE name_len = (BYTE)strlen(record.m_name);

		bw_aes.write<BYTE>(name_len);

		// name
		bw_aes.write_array(record.m_name, name_len);

		// domain_len
		BYTE domain_len = (BYTE)strlen(record.m_domain);

		bw_aes.write<BYTE>(domain_len);

		// domain
		bw_aes.write_array(record.m_domain, domain_len);

		// user_idx
		bw_aes.write<int>(record.m_user_idx);

		// flags
		BYTE flags = 0;

		if(record.m_result_ip[0] != '\0')	flags |= 0b10000000;
		if(record.m_err_msg[0] != '\0')		flags |= 0b01000000;

		bw_aes.write<BYTE>(flags);

		// err_msg_len
		BYTE err_msg_len = (BYTE)strlen(record.m_err_msg);

		bw_aes.write<BYTE>(err_msg_len);

		// err_msg
		bw_aes.write_array(record.m_err_msg, err_msg_len);
	}	// for

	HRESULT hr = NNN::Encrypt::Rijndael_Encrypt(aes_data,
												bw_aes.m_offset,
												KeyIV->m_Key,
												KeyIV->m_IV,
												packet_data + bw.m_offset);
	if(FAILED(hr))
		return;

	packet_len = (USHORT)(bw.m_offset + bw_aes.m_offset);

	// 发送
	Socket::g_server->Send(packet_data, packet_len, sd);
}

}	// namespace Packet
}	// namespace DDNS_Server
