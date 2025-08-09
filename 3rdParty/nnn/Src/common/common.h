//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 通用包含文件
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNN___COMMON___COMMON_H_
#define _NNN___COMMON___COMMON_H_

#include "common-macro.h"
#include "common-inl.h"

#if defined(WIN32) || defined(_WIN32)
#include <Windows.h>
#endif	// WIN32 || _WIN32

#include "WinStyle.h"
#include "WinStyle/WinError.h"

#include "WinStyle-expand.h"
#include "s_Unknown.h"

#include "wp8_inc.h"
#include "C.h"

#undef min	// use __min instead
#undef max	// use __max instead

#endif	/* _NNN___COMMON___COMMON_H_ */
