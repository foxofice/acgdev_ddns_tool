//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : ddns_server
//--------------------------------------------------------------------------------------

#pragma once

#include "ddns_server-inc.h"

namespace DDNS_Server
{

extern std::atomic<es_State>	g_running_state;	// 服务器运行状态

}	// namespace DDNS_Server
