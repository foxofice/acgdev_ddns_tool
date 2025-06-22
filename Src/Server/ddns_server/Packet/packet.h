//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 数据包（收发）
//--------------------------------------------------------------------------------------

#pragma once

#include "../../../../3rdParty/nnn/Src/nnnSocket/nnnSocket.h"
#include "../../ddns_server_CLR/ddns_server_CLR.h"

#include "../../common/Common-inc.h"

namespace DDNS_Server
{
namespace Packet
{

//================================================================================
// Send
//================================================================================

#pragma region Send
// Server 发送心跳包
// S xxxx
void	send_KeepAlive(struct NNN::Socket::s_SessionData *sd);

// Server 回应 Ping
/*
	S xxxx <client_time>.double

<client_time>	- 客户端发送的 NNN::Time::get_time()
*/
void	send_Ping(struct NNN::Socket::s_SessionData *sd, double client_time);

// 【登录验证】

// Server 发送 KeyIV
/*
	S xxxx aes({<send_Key>.32B <send_IV>.16B})

<send_Key>/<send_IV>	- 随机生成的 Key/IV
aes 的 Key/IV			- Common::Encrypt::gen_KeyIV(user, pwd, ...)
*/
void	send_KeyIV(struct NNN::Socket::s_SessionData *sd);

// Server 发送「登录结果」
/*
	S xxxx <result>.B

<result>	- OK、Failed
*/
void	send_Login_Result(struct NNN::Socket::s_SessionData *sd, es_Result result);

//【登录验证后】

// Server 发送「更新域名的 A/AAAA 记录的结果」
/*
	S xxxx <packet_len>.USHORT <aes_data>.?B

	<aes_data> =
	{
		<count>.USHORT
		{
			<domain_len>.B <domain>.?B(char*)

			<current_IPv4_len>.B <current_IPv4>.?B(char*)
			<same_ipv4>.bool
			<err_msg_IPv4_len>.USHORT <err_msg_IPv4>.?B(WCHAR*)

			<current_IPv6_len>.B <current_IPv6>.?B(char*)
			<same_ipv6>.bool
			<err_msg_IPv6_len>.USHORT <err_msg_IPv6>.?B(WCHAR*)
		}*
	}
*/
void	send_Update_Domains_Result(	struct NNN::Socket::s_SessionData	*sd,
									const std::vector<struct s_Domain>	&domains );
#pragma endregion

//================================================================================
// Recv
//================================================================================

#pragma region Recv
// 解析 packet
void	parse_packet(struct NNN::Socket::s_SessionData *sd);

#define	PARSE_FUNC(name)	es_Parse_Result name(struct NNN::Socket::s_SessionData *sd)

// Client 发送 Ping
/*
	R xxxx <client_time>.double

<client_time>	- 客户端发送的 NNN::Time::get_time()
*/
PARSE_FUNC(recv_Ping);

// 【登录验证】

// Client 发送「登录数据」
/*
	R xxxx aes({<send_Key>.32B <send_IV>.16B})

<send_Key>/<send_IV>	- Server 之前自动生成的 Key/IV
aes 的 Key/IV			- Common::Encrypt::gen_KeyIV(pwd, user, ...)
*/
PARSE_FUNC(recv_Login_Data);

//【登录验证后】

// Client 发送「更新域名的 A/AAAA 记录」
/*
	R xxxx <packet_len>.USHORT <aes_data>.?B

	<aes_data> =
	{
		<profiles_count>.B
		{
			<Godaddy_Key_len>.B <Godaddy_Key>.?B(char*)
			<Godaddy_Secret_len>.B <Godaddy_Secret>.?B(char*)
			<dynv6_token_len>.B <dynv6_token>.?B(char*)
			<dynu_API_Key_len>.B <dynu_API_Key>.?B(char*)
		}*

		<DNS_Lookup_First>.bool

		<DNS_Server_List_Count>.B
		{
			<DNS_Server_len>.B <DNS_Server>.?B(char*)
		}*

		<timeout>.int

		<domains_count>.USHORT
		{
			<domain_len>.B <domain>.?B(char*)
			<type>.B

			<input_ipv4_len>.B <input_ipv4>.?B(char*)
			<enable_ipv4>.bool

			<input_ipv6_len>.B <input_ipv6>.?B(char*)
			<enable_ipv6>.bool

			<Godaddy__TTL>.int
			<dynv6__Auto_IPv4>.bool
			<dynv6__Auto_IPv6>.bool
			<dynu__ID>.int
			<dynu__TTL>.int

			<Profile_idx>.B
		}*
	}

如果 <input_ipv4_len>/<input_ipv6_len> = 0，则使用 Server 接受连接的 IP
*/
PARSE_FUNC(recv_Update_Domains);

#undef PARSE_FUNC
#pragma endregion

}	// namespace Packet
}	// namespace DDNS_Server
