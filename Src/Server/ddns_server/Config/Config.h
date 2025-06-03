//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 配置文件
//--------------------------------------------------------------------------------------

#pragma once

#include "s_Config.h"

namespace DDNS_Server
{
namespace Config
{

// 配置文件实例
extern struct s_Config	*g_config;

// 初始化/清理
HRESULT	DoInit();
HRESULT	DoFinal();

}	// namespace Config
}	// namespace DDNS_Server
