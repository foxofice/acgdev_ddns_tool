//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : nnnLib（宏）
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNLIB___NNNLIB_MACRO_H_
#define _NNNLIB___NNNLIB_MACRO_H_

#include "../common/common-macro.h"

// 打印/写入错误日志
#define NNNLIB_LOG_ERROR(format, ...)	\
		if(NNN::g_log_error_nnnLib)	\
		{	\
			NNN_LOG_ERROR_FUNC("nnnLib_err.log", format, ##__VA_ARGS__);	\
		}

#define NNNLIB_PRINT_ERROR(format, ...)	\
		if(NNN::g_log_error_nnnLib)	\
		{	\
			NNN_PRINT_ERROR_FUNC(format, ##__VA_ARGS__);	\
		}

#endif	// _NNNLIB___NNNLIB_MACRO_H_
