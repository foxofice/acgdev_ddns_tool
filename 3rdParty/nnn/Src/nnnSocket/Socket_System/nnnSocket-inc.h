//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : Socket 库（公用文件）
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNSOCKET___NNNSOCKET_INC_H_
#define _NNNSOCKET___NNNSOCKET_INC_H_

#include <string>

#ifdef _WIN32
	#include <WinSock2.h>
	#include <ws2ipdef.h>
	#include <WS2tcpip.h>

	#include <MSWSock.h>
	#include <ioapiset.h>
#else
	#include <errno.h>
	#include <sys/socket.h>
	#include <netinet/in.h>
	#include <netinet/tcp.h>
	#include <net/if.h>
	#include <netdb.h>
	#include <arpa/inet.h>
	#include <sys/ioctl.h>
	#include <sys/syscall.h>
	#include <ifaddrs.h>

	//#ifndef SIOCGIFCONF
	//	#include <sys/sockio.h>	// SIOCGIFCONF on Solaris, maybe others?
	//#endif
	//#include <asm/ioctls.h>
#endif	// _WIN32

#ifdef NNN_ANDROID
#include <sys/select.h>
#endif	// NNN_ANDROID

#include "../../common/common.h"

#include "nnnSocket-macro.h"

namespace NNN
{
namespace Socket
{

// [前置声明] IP 地址 -> 字符串
extern NNN_API char*	ip2str(in_addr addr, __out char buffer[16]);
extern NNN_API char*	ip2str(in6_addr addr, __out char buffer[46]);

struct s_ip_addr
{
	in_addr		m_addr_ipv4;	// IPv4 地址
	in6_addr	m_addr_ipv6;	// IPv6 地址

	int			m_af	= AF_INET;

	NNN_API inline char* to_host_string(__out char buffer[46]) const
	{
		switch(m_af)
		{
		case AF_INET:	return ip2str(m_addr_ipv4, buffer);
		case AF_INET6:	return ip2str(m_addr_ipv6, buffer);
		}

		buffer[0] = '\0';
		return buffer;
	}
};

struct s_ip_endpoint
{
	struct s_ip_addr	m_ip_addr;
	USHORT				m_port	= 0;
};

// 本地 IP
struct s_Local_IP
{
	char	m_AdapterName[MAX_ADAPTER_NAME_LENGTH + 4];			// 网卡名称
	char	m_Description[MAX_ADAPTER_DESCRIPTION_LENGTH + 4];	// 网卡描述

	char	m_IP_Address[46];									// IP 地址
	char	m_IP_Mask[46];										// 子网掩码
};

// 判断两个 s_ip_addr 是否相同
inline bool is_equal(const struct s_ip_addr &ip1, const struct s_ip_addr &ip2)
{
	if(ip1.m_af != ip2.m_af)
		return false;

	switch(ip1.m_af)
	{
	case AF_INET:
		return (memcmp(&ip1.m_addr_ipv4, &ip2.m_addr_ipv4, sizeof(ip1.m_addr_ipv4)) == 0);

	case AF_INET6:
		return (memcmp(&ip1.m_addr_ipv6, &ip2.m_addr_ipv6, sizeof(ip1.m_addr_ipv6)) == 0);
	}

	return false;
}

// 判断两个 s_ip_endpoint 是否相同
inline bool is_equal(const struct s_ip_endpoint &ep1, const struct s_ip_endpoint &ep2)
{
	if(ep1.m_port != ep2.m_port)
		return false;

	return is_equal(ep1.m_ip_addr, ep2.m_ip_addr);
}

}	// namespace Socket
}	// namespace NNN

#endif	// _NNNSOCKET___NNNSOCKET_INC_H_
