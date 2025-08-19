//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 数据包（跟 DDNS_Server 交互）
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
// Client 发送 Ping
// Link: ../../../../Server/ddns_server/Packet/packet.h : recv_Ping)
void	send_Ping();

// 【登录验证】

// Client 发送「登录数据」
// Link: ../../../../Server/ddns_server/Packet/packet.h : recv_Login_Data)
void	send_Login_Data(const BYTE send_Key[AES_KEY_LEN], const BYTE send_IV[AES_IV_LEN]);

//【登录验证后】

// Client 发送「更新域名的 A/AAAA 记录」
// Link: ../../../../Server/ddns_server/Packet/packet.h : recv_Update_Domains)
void	send_Update_Domains(const std::vector<struct s_Domain>		&domains,
							bool									DNS_Lookup_First,
							__in_opt const std::vector<std::string>	*DNS_Server_List	= nullptr,
							int										timeout				= 15 * 1000);
#pragma endregion

//================================================================================
// Recv
//================================================================================

#pragma region Recv
// 解析数据包
void	parse_packet();

#define	PARSE_FUNC(name, ...)	NNN_API es_Parse_Result name(struct NNN::Socket::s_SessionData *sd, ##__VA_ARGS__)

// Server 发送心跳包
// Link: ../../../../Server/ddns_server/Packet/packet.h : send_KeepAlive(
PARSE_FUNC(recv_KeepAlive);

// Server 回应 Ping
// Link: ../../../../Server/ddns_server/Packet/packet.h : send_Ping(
PARSE_FUNC(recv_Ping);

// 【登录验证】

// Server 发送 KeyIV
// Link: ../../../../Server/ddns_server/Packet/packet.h : send_KeyIV(
PARSE_FUNC(recv_KeyIV);

// Server 发送「登录结果」
// Link: ../../../../Server/ddns_server/Packet/packet.h : send_Login_Result(
PARSE_FUNC(recv_Login_Result);

//【登录验证后】

// Server 发送「更新域名的 A/AAAA 记录的结果」
// Link: ../../../../Server/ddns_server/Packet/packet.h : send_Update_Domains_Result(
PARSE_FUNC(recv_Update_Domains_Result);

// Server 发送 Log
// Link: ../../../../Server/ddns_server/Packet/packet.h : send_Log(
PARSE_FUNC(recv_Log);

#undef PARSE_FUNC
#pragma endregion

}	// namespace DDNS_Server
}	// namespace Packet
}	// namespace ddns_tool_CLR
