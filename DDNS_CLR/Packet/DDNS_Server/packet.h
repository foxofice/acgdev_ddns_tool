//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 数据包（跟 DDNS_Server 交互）
//--------------------------------------------------------------------------------------

#pragma once
#ifndef __DDNS_CLR___PACKET___DDNS_SERVER___PACKET_H_
#define __DDNS_CLR___PACKET___DDNS_SERVER___PACKET_H_

#include "../../../nnn/Src/nnnSocketClient/nnnSocketClient.h"

#include "../../../ddns_server_CLR/ddns_server_CLR.h"
#include "../../../Common/Common.h"

namespace DDNS_CLR
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
// 请参见：ddns_server::Packet::recv_Ping()
void	send_Ping();

// 【登录验证】

// Client 发送「登录数据」
// 请参见：ddns_server::Packet::recv_Login_Data()
void	send_Login_Data(const BYTE send_Key[AES_KEY_LEN], const BYTE send_IV[AES_IV_LEN]);

//【登录验证后】

// Client 发送「更新域名的 A/AAAA 记录」
// 请参见：ddns_server::Packet::recv_Update_Domains()
void	send_Update_Domains(const char											*Key,
							const char											*Secret,
							__in_opt const char									*ip,
							const std::vector<struct ddns_server_CLR::s_Record>	&records);
#pragma endregion

//================================================================================
// Recv
//================================================================================

#pragma region Recv
// 解析数据包
void	parse_packet();

#define	PARSE_FUNC(name, ...)	NNN_API es_Parse_Result name(struct NNN::Socket::s_SessionData *sd, ##__VA_ARGS__)

// Server 发送心跳包
// 请参见：ddns_server::Packet::send_KeepAlive()
PARSE_FUNC(recv_KeepAlive);

// Server 回应 Ping
// 请参见：ddns_server::Packet::send_Ping()
PARSE_FUNC(recv_Ping);

// 【登录验证】

// Server 发送 KeyIV
// 请参见：ddns_server::Packet::send_KeyIV()
PARSE_FUNC(recv_KeyIV);

// Server 发送「登录结果」
// 请参见：ddns_server::Packet::send_Login_Result()
PARSE_FUNC(recv_Login_Result);

//【登录验证后】

// Server 发送「更新域名的 A/AAAA 记录的结果」
// 请参见：ddns_server::Packet::send_Update_Domains_Result()
PARSE_FUNC(recv_Update_Domains_Result);

#undef PARSE_FUNC
#pragma endregion

}	// namespace DDNS_Server
}	// namespace Packet
}	// namespace DDNS_CLR

#endif	// __DDNS_CLR___PACKET___DDNS_SERVER___PACKET_H_
