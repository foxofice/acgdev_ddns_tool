//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 日志记录（宏）
//--------------------------------------------------------------------------------------

#pragma once

#include "../../../../3rdParty/nnn/Src/nnnLib/nnnLib.h"

#define NNN_CL_STATUS		NNN_ANSI_GREEN							// Status（绿色）
#define NNN_CL_SQL			NNN_ANSI_MAGENTA						// SQL（紫色）
#define NNN_CL_INFO			NNN_ANSI_WHITE							// Info
#define NNN_CL_NOTICE		NNN_ANSI_COLOR(ANSI_CYAN)				// Notice（青色）
#define NNN_CL_WARNING		NNN_ANSI_COLOR(ANSI_RGB(255, 242, 0))	// Warning（黄色）
#define NNN_CL_DEBUG		NNN_ANSI_CYAN							// Debug（青色）
#define NNN_CL_ERROR		NNN_ANSI_COLOR(ANSI_RGB(255, 128, 128))	// Error（粉红色）
#define NNN_CL_FATAL_ERROR	NNN_ANSI_COLOR(ANSI_RGB(255, 0, 0))		// Fatal Error（红色）

#define NNN_CL_NAME			NNN_ANSI_COLOR(ANSI_RGB(0, 128, 0))		// 名字（绿色）
#define NNN_CL_VARIABLE		NNN_ANSI_COLOR(ANSI_RGB(255, 128, 0))	// 变量（橙色）
#define NNN_CL_VALUE		NNN_ANSI_COLOR(ANSI_RGB(70, 163, 255))	// 数值（蓝色）
