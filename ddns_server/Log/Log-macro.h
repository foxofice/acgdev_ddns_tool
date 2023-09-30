//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 日志记录（宏）
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _DDNS_SERVER___LOG___LOG_MACRO_H_
#define _DDNS_SERVER___LOG___LOG_MACRO_H_

#include "../../nnn/Src/nnnLib/nnnLib.h"

#define NNN_CL_STATUS		NNN_CL_GREEN	// Status
#define NNN_CL_SQL			NNN_CL_MAGENTA	// SQL
#define NNN_CL_INFO			NNN_CL_WHITE	// Info
#define NNN_CL_NOTICE		NNN_CL_LT_CYAN	// Notice
#define NNN_CL_WARNING		NNN_CL_YELLOW	// Warning
#define NNN_CL_DEBUG		NNN_CL_CYAN		// Debug
#define NNN_CL_ERROR		NNN_CL_RED		// Error
#define NNN_CL_FATAL_ERROR	NNN_CL_LT_RED	// Fatal Error

#define NNN_CL_NAME			NNN_CL_GREEN	// 名字
#define NNN_CL_VARIABLE		NNN_CL_CYAN		// 变量
#define NNN_CL_VALUE		NNN_CL_WHITE	// 数值

#endif	// _DDNS_SERVER___LOG___LOG_MACRO_H_
