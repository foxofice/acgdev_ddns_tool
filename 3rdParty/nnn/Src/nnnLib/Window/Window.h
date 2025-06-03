//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 窗口
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNLIB___WINDOW___WINDOW_H_
#define _NNNLIB___WINDOW___WINDOW_H_

#include "../../common/common.h"

namespace NNN
{
namespace Window
{

#if (NNN_PLATFORM == NNN_PLATFORM_WIN32)
// 闪烁程序的标题栏
NNN_API BOOL	FlashWindow(HWND hWnd, BOOL bInvert = TRUE);
#endif	// NNN_PLATFORM

}	// namespace Window
}	// namespace NNN

#endif	// _NNNLIB___WINDOW___WINDOW_H_
