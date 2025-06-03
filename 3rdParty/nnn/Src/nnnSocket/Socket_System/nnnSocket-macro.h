﻿//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : Socket（宏）
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNSOCKET___NNNSOCKET_MACRO_H_
#define _NNNSOCKET___NNNSOCKET_MACRO_H_

#define NNN_SOCKET_ERROR_LOG	"nnnSocketError.log"	// Socket 出错日志

#include "../../common/common-macro.h"

#if (NNN_PLATFORM == NNN_PLATFORM_WIN32) || (NNN_PLATFORM == NNN_PLATFORM_WP8)
	#ifndef ETHER_ADDR_LEN
	#define ETHER_ADDR_LEN 6
	#endif	// ETHER_ADDR_LEN
#else
	#include <net/ethernet.h>
#endif	// NNN_PLATFORM

// 支持的 I/O 模型
#define NNN_SOCKET_SUPPORT_SELECT	1
#define NNN_SOCKET_SUPPORT_IOCP		0
#define NNN_SOCKET_SUPPORT_EPOLL	0

#if (NNN_PLATFORM == NNN_PLATFORM_WIN32)
#undef NNN_SOCKET_SUPPORT_IOCP
#define NNN_SOCKET_SUPPORT_IOCP		1
#endif	// NNN_PLATFORM_WIN32

#if (NNN_PLATFORM == NNN_PLATFORM_LINUX)	||	\
	(NNN_PLATFORM == NNN_PLATFORM_MAC)		||	\
	(NNN_PLATFORM == NNN_PLATFORM_ANDROID)
#undef NNN_SOCKET_SUPPORT_EPOLL
#define NNN_SOCKET_SUPPORT_EPOLL	1
#endif	// NNN_PLATFORM_LINUX || NNN_PLATFORM_MAC || NNN_PLATFORM_ANDROID

#define NNN_MAX_IP_COUNT				50

#ifndef MAX_ADAPTER_DESCRIPTION_LENGTH
#define MAX_ADAPTER_DESCRIPTION_LENGTH  128 // arb.
#endif

#ifndef MAX_ADAPTER_NAME_LENGTH
#define MAX_ADAPTER_NAME_LENGTH         256 // arb.
#endif

#ifndef NNN_IOCP_QUIT_FLAG
#define NNN_IOCP_QUIT_FLAG				1
#endif

#endif	// _NNNSOCKET___NNNSOCKET_MACRO_H_
