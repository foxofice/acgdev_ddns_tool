//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//--------------------------------------------------------------------------------------

#include "../../Common/Encrypt.h"

#include "../../ddns_server/Packet/packet_header.h"

#include "packet.h"

namespace ddns_server__Client
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
	BYTE								send_KeyIV[AES_KEY_LEN + AES_IV_LEN];
	struct NNN::Buffer::s_BinaryWriter	bw_aes(send_KeyIV);

	BYTE								packet_data[PACKET_HEADER_LEN * 2	+
													sizeof(send_KeyIV)];
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

	HRESULT hr = NNN::Encrypt::Rijndael_Encrypt(send_KeyIV,
												sizeof(send_KeyIV),
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
bool send_Update_Domains(	class NNN::Socket::c_Client				*client,
							const BYTE								aes_Key[AES_KEY_LEN],
							const BYTE								aes_IV[AES_IV_LEN],

							const std::vector<struct s_Domain>		&domains,
							bool									DNS_Lookup_First,
							__in_opt const std::vector<std::string>	*DNS_Server_List,
							int										timeout )
{
	BYTE								packet_data[USHRT_MAX];
	struct NNN::Buffer::s_BinaryWriter	bw(packet_data);

	// 填充数据
	BYTE xor_val		= (BYTE)(rand() & 0xff);
	BYTE header_encode	= Common::Encrypt::encode_header((USHORT)ES_HEADER::Client_Update_Domains, xor_val);

	bw.write<BYTE>(header_encode);
	bw.write<BYTE>(xor_val);

	USHORT &packet_len = *bw.write<USHORT>(0);

	//========== 构造 profiles ==========(Start)
	std::map<struct s_Security_Profile*, BYTE>	profiles_to_idx;	// s_Security_Profile* -> Profile_idx
	std::vector<struct s_Security_Profile*>		profiles;

	profiles.reserve(domains.size());

	for(const struct s_Domain &domain : domains)
	{
		if(!domain.IPv4.m_enabled && !domain.IPv6.m_enabled)
			continue;

		if(domain.m_Security_Profile == nullptr)
			continue;

		if(profiles_to_idx.find(domain.m_Security_Profile) == profiles_to_idx.end())
		{
			profiles_to_idx.insert({ domain.m_Security_Profile, (BYTE)profiles.size() });
			profiles.push_back(domain.m_Security_Profile);
		}
	}	// for
	//========== 构造 profiles ==========(End)

	BYTE								aes_data[USHRT_MAX];
	struct NNN::Buffer::s_BinaryWriter	bw_aes(aes_data);

	// profiles_count
	BYTE profiles_count = (BYTE)profiles.size();
	bw_aes.write<BYTE>(profiles_count);

	for(BYTE i=0; i<profiles_count; ++i)
	{
		struct s_Security_Profile *profile = profiles[i];

		// Godaddy_Key_len
		BYTE Godaddy_Key_len = (BYTE)strlen(profile->GODADDY.m_Key);
		bw_aes.write<BYTE>(Godaddy_Key_len);

		// Godaddy_Key
		bw_aes.write_array(profile->GODADDY.m_Key, Godaddy_Key_len);

		// Godaddy_Secret_len
		BYTE Godaddy_Secret_len = (BYTE)strlen(profile->GODADDY.m_Secret);
		bw_aes.write<BYTE>(Godaddy_Secret_len);

		// Secret
		bw_aes.write_array(profile->GODADDY.m_Secret, Godaddy_Secret_len);

		// dynv6_token_len
		BYTE dynv6_token_len = (BYTE)strlen(profile->DYNV6.m_token);
		bw_aes.write<BYTE>(dynv6_token_len);

		// dynv6_token
		bw_aes.write_array(profile->DYNV6.m_token, dynv6_token_len);

		// dynu_API_Key_len
		BYTE dynu_API_Key_len = (BYTE)strlen(profile->DYNU.m_API_KEY);
		bw_aes.write<BYTE>(dynu_API_Key_len);

		// dynu_API_Key
		bw_aes.write_array(profile->DYNU.m_API_KEY, dynu_API_Key_len);
	}	// for

	// DNS_Lookup_First
	bw_aes.write<bool>(DNS_Lookup_First);

	// DNS_Server_List_Count
	BYTE DNS_Server_List_Count = 0;

	if(DNS_Server_List != nullptr)
		DNS_Server_List_Count = (BYTE)DNS_Server_List->size();

	bw_aes.write<BYTE>(DNS_Server_List_Count);

	for(BYTE i=0; i<DNS_Server_List_Count; ++i)
	{
		const std::string &dns_server = (*DNS_Server_List)[i];

		// DNS_Server_len
		BYTE DNS_Server_len = (BYTE)dns_server.size();
		bw_aes.write<BYTE>(DNS_Server_len);

		// DNS_Server
		bw_aes.write_array(dns_server.c_str(), DNS_Server_len);
	}	// for

	// timeout
	bw_aes.write<int>(timeout);

	// domains_count
	USHORT &domains_count = *bw_aes.write<USHORT>(0);

	for(const struct s_Domain &domain : domains)
	{
		if(!domain.IPv4.m_enabled && !domain.IPv6.m_enabled)
			continue;

		if(domain.m_Security_Profile == nullptr)
			continue;

		// domain_len
		BYTE domain_len = (BYTE)strlen(domain.m_domain);
		bw_aes.write<BYTE>(domain_len);

		// domain
		bw_aes.write_array(domain.m_domain, domain_len);

		// type
		bw_aes.write<BYTE>((BYTE)domain.m_type);

		// input_ipv4_len
		BYTE input_ipv4_len = (BYTE)strlen(domain.IPv4.m_input_IP);
		bw_aes.write<BYTE>(input_ipv4_len);

		// input_ipv4
		if(input_ipv4_len > 0)
			bw_aes.write_array(domain.IPv4.m_input_IP, input_ipv4_len);

		// enable_ipv4
		bw_aes.write<bool>((bool)domain.IPv4.m_enabled);

		// input_ipv6_len
		BYTE input_ipv6_len = (BYTE)strlen(domain.IPv6.m_input_IP);
		bw_aes.write<BYTE>(input_ipv6_len);

		// input_ipv6
		if(input_ipv6_len > 0)
			bw_aes.write_array(domain.IPv6.m_input_IP, input_ipv6_len);

		// enable_ipv6
		bw_aes.write<bool>((bool)domain.IPv6.m_enabled);

		// Godaddy_TTL
		bw_aes.write<int>(domain.GODADDY.m_TTL);

		// dynv6_Auto_IPv4
		bw_aes.write<bool>(domain.DYNV6.m_Auto_IPv4);

		// dynv6_Auto_IPv6
		bw_aes.write<bool>(domain.DYNV6.m_Auto_IPv6);

		// dynu__ID
		bw_aes.write<int>(domain.DYNU.m_ID);

		// dynu__TTL
		bw_aes.write<int>(domain.DYNU.m_TTL);

		// Profile_idx
		BYTE Profile_idx = profiles_to_idx[domain.m_Security_Profile];
		bw_aes.write<BYTE>(Profile_idx);

		++domains_count;
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
}	// namespace ddns_server__Client
