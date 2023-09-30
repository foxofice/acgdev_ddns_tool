//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : ddns_server
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _DDNS_SERVER___DDNS_SERVER_H_
#define _DDNS_SERVER___DDNS_SERVER_H_

#include "ddns_server-inc.h"

namespace DDNS_Server
{

extern std::atomic<es_State>	g_running_state;	// 服务器运行状态

}	// namespace DDNS_Server

#endif	// _DDNS_SERVER___DDNS_SERVER_H_
