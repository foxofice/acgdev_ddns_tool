//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 网络
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _DDNS_SERVER___SOCKET___SOCKET_H_
#define _DDNS_SERVER___SOCKET___SOCKET_H_

#include "../../nnn/Src/nnnSocketServer/nnnSocketServer.h"

#include "Socket-inc.h"

namespace DDNS_Server
{
namespace Socket
{

// Server 实例
extern class NNN::Socket::c_Server	*g_server;

// 初始化/清理
HRESULT	DoInit();
HRESULT	DoFinal();

// 打开/关闭服务器
HRESULT	Start();
HRESULT	Stop();

// 执行周期性工作
void	DoWork();

// 从 LOGINING_SESSIONS.m_sessions 中移除 session_id
void	remove_logining_sessions(UINT64 session_id);

}	// namespace Socket
}	// namespace DDNS_Server

#endif	// _DDNS_SERVER___SOCKET___SOCKET_H_
