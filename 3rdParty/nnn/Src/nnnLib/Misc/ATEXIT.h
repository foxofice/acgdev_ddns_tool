//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 注册退出函数
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNLIB___MISC___ATEXIT_H_
#define _NNNLIB___MISC___ATEXIT_H_

#include "../../common/common.h"

namespace NNN
{
namespace Misc
{

using CleanupFunc = void(*)();

// 注册退出函数
/*
Win 下已经处理这些关闭控制台窗口的方式：Ctrl + C、Ctrl + Break、点击 X
但是「点击 X」可能无法完整执行注册的 clean_up 函数，Win 机制一到宽限期（大概 5 秒）直接 TerminateProcess() 销毁窗口
*/
NNN_API void atexit(CleanupFunc func);

}	// namespace Misc
}	// namespace NNN

#endif	// _NNNLIB___MISC___ATEXIT_H_
