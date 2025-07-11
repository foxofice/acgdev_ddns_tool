//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : ddns_server 执行的 .Net 代码
//--------------------------------------------------------------------------------------
#pragma once

#include "../../../3rdParty/nnn/Src/common/common-macro.h"
#include "../../../3rdParty/nnn/Src/nnnSocket/nnnSocket.h"
#include "../Common/Common.h"

namespace ddns_server_CLR
{

typedef void	(CALLBACK *LPCALLBACK_SEND_LOG)(struct NNN::Socket::s_SessionData *sd, const WCHAR *log, DWORD color);

// 初始化
NNN_API void	DoInit(LPCALLBACK_SEND_LOG send_log_func, const WCHAR *culture = L"");

// 更新 IP 记录
NNN_API void	update_domains(	__inout std::vector<struct s_Domain>	&domains,
								bool									DNS_Lookup_First,
								__in_opt const std::vector<std::string>	*DNS_Server_List	= nullptr,		// 自定义DNS服务器（列表元素为 "" 时，表示系统默认 DNS）
								int										timeout				= 15 * 1000,
								struct NNN::Socket::s_SessionData		*sd_SendLog			= nullptr );	// 发送 Log 的 sd（Server 用）

}	// namespace ddns_server_CLR
