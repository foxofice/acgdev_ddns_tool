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
 * Client ���� Ping
 * send_Ping()
 *==============================================================*/
void send_Ping()
{
	// ����
	PACKET::send_Ping(g_client);
}

// ����¼��֤��

/*==============================================================
 * Client ���͡���¼���ݡ�
 * send_Login_Data()
 *==============================================================*/
void send_Login_Data(const BYTE send_Key[AES_KEY_LEN], const BYTE send_IV[AES_IV_LEN])
{
	// ����
	PACKET::send_Login_Data(g_client,
							g_login_user->c_str(),
							g_login_pwd->c_str(),
							send_Key,
							send_IV);
}

//����¼��֤��

/*==============================================================
 * Client ���͡����������� A/AAAA ��¼��
 * send_Update_Domains()
 *==============================================================*/
void send_Update_Domains(	const std::vector<struct s_Domain>		&domains,
							bool									DNS_Lookup_First,
							__in_opt const std::vector<std::string>	*DNS_Server_List,
							int										timeout )
{
	// ����
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
