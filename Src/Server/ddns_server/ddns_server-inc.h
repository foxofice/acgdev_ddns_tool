//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 包含文件
//--------------------------------------------------------------------------------------

#pragma once

#include "../../../3rdParty/nnn/Src/nnnSocket/nnnSocket.h"
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
