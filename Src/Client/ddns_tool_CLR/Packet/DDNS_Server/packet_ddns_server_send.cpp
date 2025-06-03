//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//--------------------------------------------------------------------------------------

#include "../../../../Server/ddns_server__Client/Packet/packet.h"

#include "../../ddns_tool_CLR.h"
#include "packet.h"

namespace ddns_tool_CLR
{
namespace Packet
{
namespace DDNS_Server
{

namespace PACKET = ddns_server__Client::Packet;

/*==============================================================
 * Client 发送 Ping
 * send_Ping()
 *==============================================================*/
void send_Ping()
{
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
void send_Update_Domains(	const std::vector<struct s_Domain>		&domains,
							bool									DNS_Lookup_First,
							__in_opt const std::vector<std::string>	*DNS_Server_List,
							int										timeout )
{
	// 发送
	PACKET::send_Update_Domains(g_client,
								g_KeyIV.m_Key,
								g_KeyIV.m_IV,
								domains,
								DNS_Lookup_First,
								DNS_Server_List,
								timeout);
}

}	// namespace DDNS_Server
}	// namespace Packet
}	// namespace ddns_tool_CLR
