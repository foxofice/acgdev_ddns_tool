//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 控制台
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNLIB___CONSOLE___CONSOLE_H_
#define _NNNLIB___CONSOLE___CONSOLE_H_

#include <string>

#include "../../common/common.h"
#include "Console-macro.h"

// 前置声明
namespace NNN
{
namespace Thread
{
	struct s_LockDetector;
}	// namespace Thread
}	// namespace NNN

namespace NNN
{
namespace Console
{

// 初始化/清理
HRESULT					DoInit();
HRESULT					DoFinal();

// 获取当前线程的 string（纯文本，不带格式）
NNN_API std::string*	get_current_string();

// 锁定/解锁 printf/wprintf
NNN_API void			console_lock();
NNN_API void			console_unlock();

// 用特殊格式输出控制台信息
NNN_API void			show_message(const char *format, ...);
NNN_API void			show_message_stderr(const char *format, ...);

NNN_API void			show_message_func(bool is_stderr, const char *fmt, va_list argptr);

// 设置/获取是否把 show_message()、show_message_stderr() 每次打印的纯文本（不带格式）记录下来（默认 false）
NNN_API void			set_record_last_text(bool record_last_text);
NNN_API bool			get_record_last_text();

#if (NNN_PLATFORM == NNN_PLATFORM_WIN32)
// 获取命令行程序的输出
NNN_API bool			get_cmd_output(const WCHAR *cmd, __out std::wstring &output);
#endif	// NNN_PLATFORM_WIN32

}	// namespace Console
}	// namespace NNN

#endif	// _NNNLIB___CONSOLE___CONSOLE_H_
