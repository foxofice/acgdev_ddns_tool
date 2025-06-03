//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : DNS Lookup
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNSOCKET___SOCKET_SYSTEM___DNS_LOOKUP_H_
#define _NNNSOCKET___SOCKET_SYSTEM___DNS_LOOKUP_H_

#include <string>

#include "../../common/common.h"

namespace NNN
{
namespace Socket
{

struct s_DNS_Lookup_Result
{
	struct
	{
		std::vector<std::string>	m_ip_list;
		char						m_error_msg[1024]	= {};
	} IPV4;

	struct
	{
		std::vector<std::string>	m_ip_list;
		char						m_error_msg[1024]	= {};
	} IPV6;
};

/*
DNS 解析（同时获取 A/AAAA 记录）

	\param[in]	dns_server	DNS服务器（例如：8.8.8.8、2001:4860:4860::8888）
	\param[in]	domain		要解析的域名
	\param[out]	result		解析结果
	\return					成功则返回 true
*/
NNN_API bool	DNS_Lookup(	const char							*dns_server,
							const char							*domain,
							__out struct s_DNS_Lookup_Result	&result );

}	// namespace Socket
}	// namespace NNN

#endif	// _NNNSOCKET___SOCKET_SYSTEM___DNS_LOOKUP_H_
