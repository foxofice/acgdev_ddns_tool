//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : ���ݰ����� DDNS_Server ������
//--------------------------------------------------------------------------------------

#pragma once

#include "../../../../../3rdParty/nnn/Src/nnnSocketClient/nnnSocketClient.h"

#include "../../../../Server/ddns_server_CLR/ddns_server_CLR.h"
#include "../../../../Server/Common/Common.h"

namespace ddns_tool_CLR
{
namespace Packet
{
namespace DDNS_Server
{

//================================================================================
// Send
//================================================================================

#pragma region Send
// Client ���� Ping
// ��μ���ddns_server::Packet::recv_Ping()
void	send_Ping();

// ����¼��֤��

// Client ���͡���¼���ݡ�
// ��μ���ddns_server::Packet::recv_Login_Data()
void	send_Login_Data(const BYTE send_Key[AES_KEY_LEN], const BYTE send_IV[AES_IV_LEN]);

//����¼��֤��

// Client ���͡����������� A/AAAA ��¼��
// ��μ���ddns_server::Packet::recv_Update_Domains()
void	send_Update_Domains(const std::vector<struct s_Domain>		&domains,
							bool									DNS_Lookup_First,
							__in_opt const std::vector<std::string>	*DNS_Server_List	= nullptr,
							int										timeout				= 15 * 1000);
#pragma endregion

//================================================================================
// Recv
//================================================================================

#pragma region Recv
// �������ݰ�
void	parse_packet();

#define	PARSE_FUNC(name, ...)	NNN_API es_Parse_Result name(struct NNN::Socket::s_SessionData *sd, ##__VA_ARGS__)

// Server ����������
// ��μ���ddns_server::Packet::send_KeepAlive()
PARSE_FUNC(recv_KeepAlive);

// Server ��Ӧ Ping
// ��μ���ddns_server::Packet::send_Ping()
PARSE_FUNC(recv_Ping);

// ����¼��֤��

// Server ���� KeyIV
// ��μ���ddns_server::Packet::send_KeyIV()
PARSE_FUNC(recv_KeyIV);

// Server ���͡���¼�����
// ��μ���ddns_server::Packet::send_Login_Result()
PARSE_FUNC(recv_Login_Result);

//����¼��֤��

// Server ���͡����������� A/AAAA ��¼�Ľ����
// ��μ���ddns_server::Packet::send_Update_Domains_Result()
PARSE_FUNC(recv_Update_Domains_Result);

// Server ���� Log
// ��μ���ddns_server::Packet::send_Log()
PARSE_FUNC(recv_Log);

#undef PARSE_FUNC
#pragma endregion

}	// namespace DDNS_Server
}	// namespace Packet
}	// namespace ddns_tool_CLR
