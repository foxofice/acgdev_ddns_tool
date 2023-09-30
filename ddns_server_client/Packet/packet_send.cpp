//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//--------------------------------------------------------------------------------------

#include "../../Common/Encrypt.h"

#include "../../ddns_server/Packet/packet_header.h"

#include "packet.h"

namespace DDNS_Server_Client
{
namespace Packet
{

using ES_HEADER	= DDNS_Server::Packet::es_Header;

/*==============================================================
 * Client 发送 Ping
 * send_Ping()
 *==============================================================*/
bool send_Ping(class NNN::Socket::c_Client *client)
{
	BYTE								packet_data[PACKET_HEADER_LEN * 2	+
													sizeof(double)];	// client_time
	struct NNN::Buffer::s_BinaryWriter	bw(packet_data);

	// 填充数据
	BYTE xor_val		= (BYTE)(rand() & 0xff);
	BYTE header_encode	= Common::Encrypt::encode_header((BYTE)ES_HEADER::Client_Ping, xor_val);

	bw.write<BYTE>(header_encode);
	bw.write<BYTE>(xor_val);
	bw.write<double>(NNN::Time::get_time());

	// 发送
	return SUCCEEDED(client->Send(packet_data, sizeof(packet_data)));
}


/*==============================================================
 * Client 发送「登录数据」
 * send_Login_Data()
 *==============================================================*/
bool send_Login_Data(	class NNN::Socket::c_Client	*client,
						const WCHAR					*user,
						const WCHAR					*password,
						const BYTE					send_Key[AES_KEY_LEN],
						const BYTE					send_IV[AES_IV_LEN] )
{
	BYTE								aes_input[AES_KEY_LEN + AES_IV_LEN];
	struct NNN::Buffer::s_BinaryWriter	bw_aes(aes_input);

	BYTE								packet_data[PACKET_HEADER_LEN * 2	+
													sizeof(aes_input)];
	struct NNN::Buffer::s_BinaryWriter	bw(packet_data);

	// 填充数据
	BYTE xor_val		= (BYTE)(rand() & 0xff);
	BYTE header_encode	= Common::Encrypt::encode_header((USHORT)ES_HEADER::Client_Login_Data, xor_val);

	bw.write<BYTE>(header_encode);
	bw.write<BYTE>(xor_val);

	// 加密
	BYTE Key[AES_KEY_LEN];
	BYTE IV[AES_IV_LEN];
	Common::Encrypt::gen_KeyIV(password, user, Key, IV);

	bw_aes.write_array(send_Key, AES_KEY_LEN);
	bw_aes.write_array(send_IV, AES_IV_LEN);

	HRESULT hr = NNN::Encrypt::Rijndael_Encrypt(aes_input,
												sizeof(aes_input),
												Key,
												IV,
												packet_data + bw.m_offset);
	if(FAILED(hr))
		return false;

	// 发送
	return SUCCEEDED(client->Send(packet_data, sizeof(packet_data)));
}

//【登录验证后】

/*==============================================================
 * Client 发送「更新域名的 A/AAAA 记录」
 * send_Update_Domains()
 *==============================================================*/
bool send_Update_Domains(	class NNN::Socket::c_Client							*client,
							const BYTE											aes_Key[AES_KEY_LEN],
							const BYTE											aes_IV[AES_IV_LEN],
							const char											*Key,
							const char											*Secret,
							__in_opt const char									*ip,
							const std::vector<struct ddns_server_CLR::s_Record>	&records )
{
	BYTE								packet_data[USHRT_MAX];
	struct NNN::Buffer::s_BinaryWriter	bw(packet_data);

	// 填充数据
	BYTE xor_val		= (BYTE)(rand() & 0xff);
	BYTE header_encode	= Common::Encrypt::encode_header((USHORT)ES_HEADER::Client_Update_Domains, xor_val);

	bw.write<BYTE>(header_encode);
	bw.write<BYTE>(xor_val);

	USHORT &packet_len = *bw.write<USHORT>(0);

	BYTE								aes_data[USHRT_MAX];
	struct NNN::Buffer::s_BinaryWriter	bw_aes(aes_data);

	// Key_len
	BYTE Key_len = (BYTE)strlen(Key);
	bw_aes.write<BYTE>(Key_len);

	// Key
	bw_aes.write_array(Key, Key_len);

	// Secret_len
	BYTE Secret_len = (BYTE)strlen(Secret);
	bw_aes.write<BYTE>(Secret_len);

	// Secret
	bw_aes.write_array(Secret, Secret_len);

	// ip_len
	BYTE ip_len = (BYTE)strlen(ip);
	bw_aes.write<BYTE>(ip_len);

	// ip
	if(ip_len > 0)
		bw_aes.write_array(ip, ip_len);

	// domains_count
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

		// TTL
		bw_aes.write<int>(record.m_TTL);

		// user_idx
		bw_aes.write<int>(record.m_user_idx);
	}	// for

	HRESULT hr = NNN::Encrypt::Rijndael_Encrypt(aes_data,
												bw_aes.m_offset,
												aes_Key,
												aes_IV,
												packet_data + bw.m_offset);
	if(FAILED(hr))
		return false;

	packet_len = (USHORT)(bw.m_offset + bw_aes.m_offset);

	// 发送
	return SUCCEEDED(client->Send(packet_data, packet_len));
}

}	// namespace Packet
}	// namespace DDNS_Server_Client
