//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 重叠对象（仅 IOCP）
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNSOCKET___SOCKET_SYSTEM___S_OVERLAPPED_H_
#define _NNNSOCKET___SOCKET_SYSTEM___S_OVERLAPPED_H_

#include "../../common/common.h"

#ifdef _WIN32

namespace NNN
{
namespace Socket
{

// 前置声明
struct s_SessionData;

// 重叠对象的类型
enum struct es_OverlappedType
{
	None,

	Send,
	Receive,
	AcceptIPv4,
	AcceptIPv6,
};

struct s_OVERLAPPED
{
	OVERLAPPED				m_Overlapped	= {};						// 重叠结构
	es_OverlappedType		m_type			= es_OverlappedType::None;	// 类型
	struct s_SessionData	*m_sd			= nullptr;					// 会话数据
};

}	// namespace Socket
}	// namespace NNN

#endif	// _WIN32

#endif	// _NNNSOCKET___SOCKET_SYSTEM___S_OVERLAPPED_H_
