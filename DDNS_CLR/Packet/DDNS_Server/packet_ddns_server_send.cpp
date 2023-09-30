//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//--------------------------------------------------------------------------------------

#include "../../../ddns_server_client/Packet/packet.h"

#include "../../DDNS_CLR.h"
#include "packet.h"

namespace DDNS_CLR
{
namespace Packet
{
namespace DDNS_Server
{

/*==============================================================
 * Client 发送 Ping
 * send_Ping()
 *==============================================================*/
void send_Ping()
{
	namespace PACKET = DDNS_Server_Client::Packet;

	// 发送
	PACKET::send_Ping(g_client);
}

// 【登录验证】

/*==============================================================
 * Client 发送「登录数据」
 * send_Login_Data()
 *==============================================================*/
void send_Login_Data(const BYTE send_Key[AES_KEY_LEN], const BYTE send_IV[AES_IV_LEN])
{
	namespace PACKET = DDNS_Server_Client::Packet;

	// 发送
	PACKET::send_Login_Data(g_client,
							g_login_user->c_str(),
							g_login_pwd->c_str(),
							send_Key,
							send_IV);
}

//【登录验证后】

/*==============================================================
 * Client 发送「更新域名的 A/AAAA 记录」
 * send_Update_Domains()
 *==============================================================*/
void send_Update_Domains(	const char											*Key,
							const char											*Secret,
							__in_opt const char									*ip,
							const std::vector<struct ddns_server_CLR::s_Record>	&records )
{
	namespace PACKET = DDNS_Server_Client::Packet;

	// 发送
	PACKET::send_Update_Domains(g_client,
								g_KeyIV.m_Key,
								g_KeyIV.m_IV,
								Key,
								Secret,
								ip,
								records);
}

}	// namespace DDNS_Server
}	// namespace Packet
}	// namespace DDNS_CLR
