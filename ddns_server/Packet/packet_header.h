//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : Packet 头
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _DDNS_SERVER___PACKET___PACKET_HEADER_H_
#define _DDNS_SERVER___PACKET___PACKET_HEADER_H_

#include "../../nnn/Src/common/common-macro.h"

namespace DDNS_Server
{
namespace Packet
{

enum struct es_Header : BYTE
{
	START = 0,

	Server_KeepAlive,				// Server 发送心跳包

	Client_Ping,					// Client 发送 Ping
	Server_Ping,					// Server 回应 Ping

	//【登录验证】
	Server_KeyIV,					// Server 发送 KeyIV
	Client_Login_Data,				// Client 发送「登录数据」
	Server_Login_Result,			// Server 发送「登录结果」

	//【登录验证后】
	Client_Update_Domains,			// Client 发送「更新域名的 A/AAAA 记录」
	Server_Update_Domains_Result,	// Server 发送「更新域名的 A/AAAA 记录的结果」
};

}	// namespace Packet
}	// namespace DDNS_Server

#endif	// _DDNS_SERVER___PACKET___PACKET_HEADER_H_
