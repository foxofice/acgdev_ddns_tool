//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 日志记录（宏）
//--------------------------------------------------------------------------------------

#pragma once

#include "../../../../3rdParty/nnn/Src/nnnLib/nnnLib.h"

#define NNN_CL_STATUS		NNN_ANSI_GREEN				// Status
#define NNN_CL_SQL			NNN_ANSI_MAGENTA			// SQL
#define NNN_CL_INFO			NNN_ANSI_WHITE				// Info
#define NNN_CL_NOTICE		NNN_ANSI_COLOR(ANSI_CYAN)	// Notice
#define NNN_CL_WARNING		NNN_ANSI_COLOR(ANSI_YELLOW)	// Warning
#define NNN_CL_DEBUG		NNN_ANSI_CYAN				// Debug
#define NNN_CL_ERROR		NNN_ANSI_RED				// Error
#define NNN_CL_FATAL_ERROR	NNN_ANSI_COLOR(ANSI_RED)	// Fatal Error

#define NNN_CL_NAME			NNN_ANSI_GREEN				// 名字
#define NNN_CL_VARIABLE		NNN_ANSI_CYAN				// 变量
#define NNN_CL_VALUE		NNN_ANSI_WHITE				// 数值
