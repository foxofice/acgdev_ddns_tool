//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 数据包（给 ddns_server 的客户端使用）
//--------------------------------------------------------------------------------------

#pragma once

#include "../../../../3rdParty/nnn/Src/nnnSocketClient/nnnSocketClient.h"

#include "../../Common/Common.h"
#include "../../Common/s_Domain.h"
#include "../../ddns_server_CLR/ddns_server_CLR.h"

namespace ddns_server__Client
{
namespace Packet
{

//================================================================================
// Send
//================================================================================

#pragma region Send
// Client 发送 Ping
// 请参见：ddns_server::Packet::recv_Ping()
NNN_API bool	send_Ping(class NNN::Socket::c_Client *client);

// 【登录验证】

// Client 发送「登录数据」
// 请参见：ddns_server::Packet::recv_Login_Data()
NNN_API bool	send_Login_Data(class NNN::Socket::c_Client	*client,
								const WCHAR					*user,
								const WCHAR					*password,
								const BYTE					send_Key[AES_KEY_LEN],
								const BYTE					send_IV[AES_IV_LEN]);

//【登录验证后】

// Client 发送「更新域名的 A/AAAA 记录」
// 请参见：ddns_server::Packet::recv_Update_Domains()
NNN_API bool	send_Update_Domains(class NNN::Socket::c_Client				*client,
									const BYTE								aes_Key[AES_KEY_LEN],
									const BYTE								aes_IV[AES_IV_LEN],

									const std::vector<struct s_Domain>		&domains,
									bool									DNS_Lookup_First,
									__in_opt const std::vector<std::string>	*DNS_Server_List	= nullptr,
									int										timeout				= 15 * 1000);
#pragma endregion

//================================================================================
// Recv
//================================================================================

#pragma region Recv
#define	PARSE_FUNC(name, ...)	NNN_API es_Parse_Result name(struct NNN::Socket::s_SessionData *sd, ##__VA_ARGS__)

// Server 发送心跳包
// 请参见：ddns_server::Packet::send_KeepAlive()
PARSE_FUNC(recv_KeepAlive);

// Server 回应 Ping
// 请参见：ddns_server::Packet::send_Ping()
PARSE_FUNC(recv_Ping, __out double &ping);

// 【登录验证】

// Server 发送 KeyIV
// 请参见：ddns_server::Packet::send_KeyIV()
PARSE_FUNC(	recv_KeyIV,
			const WCHAR	*user,
			const WCHAR	*password,
			__out BYTE	send_Key[AES_KEY_LEN],
			__out BYTE	send_IV[AES_IV_LEN] );

// Server 发送「登录结果」
// 请参见：ddns_server::Packet::send_Login_Result()
PARSE_FUNC(recv_Login_Result, __out es_Result &result);

//【登录验证后】

// Server 发送「更新域名的 A/AAAA 记录的结果」
// 请参见：ddns_server::Packet::send_Update_Domains_Result()
PARSE_FUNC(	recv_Update_Domains_Result,
			const BYTE							Key[AES_KEY_LEN],
			const BYTE							IV[AES_IV_LEN],
			__out std::vector<struct s_Domain>	&domains );

// Server 发送 Log
// 请参见：ddns_server::Packet::send_Log()
PARSE_FUNC(	recv_Log,
			const BYTE							Key[AES_KEY_LEN],
			const BYTE							IV[AES_IV_LEN],
			__out WCHAR							log[4096],
			__out UINT							&rgb );

#undef PARSE_FUNC
#pragma endregion

}	// namespace Packet
}	// namespace ddns_server__Client
