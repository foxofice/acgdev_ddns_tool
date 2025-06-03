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

#if (NNN_PLATFORM == NNN_PLATFORM_WIN32) || (NNN_PLATFORM == NNN_PLATFORM_WP8)
#include <Windows.h>
#endif	// NNN_PLATFORM_WIN32 || NNN_PLATFORM_WP8

#include "WinStyle.h"
#include "WinStyle/WinError.h"

#include "WinStyle-expand.h"
#include "s_Unknown.h"

#include "wp8_inc.h"

#undef min	// use __min instead
#undef max	// use __max instead

#endif	/* _NNN___COMMON___COMMON_H_ */
