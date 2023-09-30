//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 包含文件
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _DDNS_SERVER___DDNS_SERVER_INC_H_
#define _DDNS_SERVER___DDNS_SERVER_INC_H_

#include "../nnn/Src/nnnSocket/nnnSocket.h"
#include "../common/Common-inc.h"

namespace DDNS_Server
{

// 服务器运行状态
enum struct es_State : BYTE
{
	Stopped,	// 停止
	Running,	// 正在运行
	Exiting,	// 正在关闭程序
};

}	// namespace DDNS_Server

#endif	// _DDNS_SERVER___DDNS_SERVER_INC_H_
