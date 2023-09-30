//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 日志记录（包含文件）
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _DDNS_SERVER___LOG___LOG_INC_H_
#define _DDNS_SERVER___LOG___LOG_INC_H_

#include "../../nnn/Src/common/common-macro.h"

namespace DDNS_Server
{
namespace Log
{

// 信息类型
enum struct es_MsgType : USHORT
{
	None		= 0,
	Status		= 1 << 0,	// 状态
	SQL			= 1 << 1,	// SQL
	Info		= 1 << 2,	// 信息（变量信息）
	Notice		= 1 << 3,	// 通知（轻于警告）
	Warning		= 1 << 4,	// 警告
	Debug		= 1 << 5,	// 调试
	Error		= 1 << 6,	// 错误
	FatalError	= 1 << 7,	// 致命错误
};

}	// namespace Log
}	// namespace DDNS_Server

#endif	// _DDNS_SERVER___LOG___LOG_INC_H_
