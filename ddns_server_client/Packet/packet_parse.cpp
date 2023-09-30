//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//--------------------------------------------------------------------------------------

#include "../../Common/Encrypt.h"

#include "packet.h"

namespace DDNS_Server_Client
{
namespace Packet
{

/*==============================================================
 * Server 发送心跳包
 * recv_KeepAlive()
 *==============================================================*/
es_Parse_Result recv_KeepAlive(struct NNN::Socket::s_SessionData *sd)
{
	BYTE								packet_data[PACKET_HEADER_LEN * 2];
	struct NNN::Buffer::s_BinaryReader	br(packet_data);

	// 读取 packet_data
	NNN_PACKET_READ_DATA_FIXED(sd->RECV_DATA.m_buffer, sizeof(packet_data));

	return es_Parse_Result::OK;
}


/*==============================================================
 * Server 回应 Ping
 * recv_Ping()
 *==============================================================*/
es_Parse_Result recv_Ping(	struct NNN::Socket::s_SessionData	*sd,
							__out double						&ping )
{
	BYTE								packet_data[PACKET_HEADER_LEN * 2	+
													sizeof(ping)];
	struct NNN::Buffer::s_BinaryReader	br(packet_data);

	// 读取 packet_data
	NNN_PACKET_READ_DATA_FIXED(sd->RECV_DATA.m_buffer, sizeof(packet_data));

	// 解析数据
	double client_time = br.read<double>();

	ping = NNN::Time::get_time() - client_time;

	return es_Parse_Result::OK;
}

// 【登录验证】

/*==============================================================
 * Server 发送 KeyIV
 * recv_KeyIV()
 *==============================================================*/
es_Parse_Result recv_KeyIV(	struct NNN::Socket::s_SessionData	*sd,
							const WCHAR							*user,
							const WCHAR							*password,
							__out BYTE							send_Key[AES_KEY_LEN],
							__out BYTE							send_IV[AES_IV_LEN] )
{
	BYTE								aes_output[AES_KEY_LEN + AES_IV_LEN];
	BYTE								packet_data[PACKET_HEADER_LEN * 2	+
													sizeof(aes_output)];
	struct NNN::Buffer::s_BinaryReader	br(packet_data);

	// 读取 packet_data
	NNN_PACKET_READ_DATA_FIXED(sd->RECV_DATA.m_buffer, sizeof(packet_data));

	// 解压
	BYTE Key[AES_KEY_LEN];
	BYTE IV[AES_IV_LEN];
	Common::Encrypt::gen_KeyIV(user, password, Key, IV);

	HRESULT hr = NNN::Encrypt::Rijndael_Decrypt(packet_data + br.m_offset,
												sizeof(aes_output),
												Key,
												IV,
												aes_output);
	if(FAILED(hr))
		return es_Parse_Result::Attack;

	CopyMemory(send_Key,	aes_output,					AES_KEY_LEN);
	CopyMemory(send_IV,		aes_output + AES_KEY_LEN,	AES_IV_LEN);

	return es_Parse_Result::OK;
}


/*==============================================================
 * Server 发送「登录结果」
 * recv_Login_Result()
 *==============================================================*/
es_Parse_Result recv_Login_Result(struct NNN::Socket::s_SessionData *sd, __out es_Result &result)
{
	BYTE								packet_data[PACKET_HEADER_LEN * 2	+
													sizeof(BYTE)];	// result
	struct NNN::Buffer::s_BinaryReader	br(packet_data);

	// 读取 packet_data
	NNN_PACKET_READ_DATA_FIXED(sd->RECV_DATA.m_buffer, sizeof(packet_data));

	// 解析数据
	result = (es_Result)br.read<BYTE>();

	return es_Parse_Result::OK;
}

//【登录验证后】

/*==============================================================
 * Server 发送「更新域名的 A/AAAA 记录的结果」
 * recv_Update_Domains_Result()
 *==============================================================*/
es_Parse_Result recv_Update_Domains_Result(	struct NNN::Socket::s_SessionData					*sd,
											const BYTE											Key[AES_KEY_LEN],
											const BYTE											IV[AES_IV_LEN],
											__out std::vector<struct ddns_server_CLR::s_Record>	&records )
{
	BYTE								packet_data[USHRT_MAX];
	struct NNN::Buffer::s_BinaryReader	br(packet_data);
	USHORT								packet_len	= 0;

	// 读取 packet_data
	NNN_PACKET_READ_DATA(sd->RECV_DATA.m_buffer);

	// 解析数据
	USHORT aes_data_len = (USHORT)(packet_len - br.m_offset);

	const BYTE *aes_data = br.read_array(aes_data_len);

	BYTE								data[USHRT_MAX];
	struct NNN::Buffer::s_BinaryReader	br_data(data);

	HRESULT hr = NNN::Encrypt::Rijndael_Decrypt(aes_data, aes_data_len, Key, IV, data);
	if(FAILED(hr))
		return es_Parse_Result::Error;

	// 解析 aes_data 的解密数据
	BYTE ip_len = br_data.read<BYTE>();

	char ip[46];
	const BYTE *ip_ = br_data.read_array(ip_len);
	CopyMemory(ip, ip_, ip_len);
	ip[ip_len] = '\0';

	USHORT count = br_data.read<USHORT>();
	records.resize(count);

	for(USHORT i=0; i<count; ++i)
	{
		struct ddns_server_CLR::s_Record &record = records[i];

		// name_len
		BYTE name_len = br_data.read<BYTE>();

		// name
		const BYTE *name = br_data.read_array(name_len);

		CopyMemory(record.m_name, name, name_len);
		record.m_name[name_len] = '\0';

		// domain_len
		BYTE domain_len = br_data.read<BYTE>();

		// domain
		const BYTE *domain = br_data.read_array(domain_len);

		CopyMemory(record.m_domain, domain, domain_len);
		record.m_domain[domain_len] = '\0';

		// user_idx
		record.m_user_idx = br_data.read<int>();

		// flags
		BYTE flags = br_data.read<BYTE>();

		bool ok		= flags & 0b10000000;
		bool failed	= flags & 0b01000000;

		// err_msg_len
		BYTE err_msg_len = br_data.read<BYTE>();

		// err_msg
		const BYTE *err_msg = br_data.read_array(err_msg_len);

		if(ok)
		{
			_STRCPY(record.m_result_ip, ip);
		}

		if(failed)
		{
			CopyMemory(record.m_err_msg, err_msg, err_msg_len);
			record.m_err_msg[err_msg_len] = '\0';
		}
	}	// for

	return es_Parse_Result::OK;
}

}	// namespace Packet
}	// namespace DDNS_Server_Client
