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

namespace NNN
{
namespace Console
{

// 初始化/清理
HRESULT			DoInit();
HRESULT			DoFinal();

// 返回一个去除了 ANSI 控制码的纯文本字符串
// （默认不启用 8-bit C1 控制码以避免误伤 UTF‑8。若确需支持 8-bit C1 控制（0x80–0x9F），可设置 enable_c1_controls = true）
NNN_API char*	remove_ansi_code(const char *str, __out char *buffer, bool enable_c1_controls = false);

#ifdef _WIN32
// 获取命令行程序的输出
NNN_API bool	get_cmd_output(const WCHAR *cmd, __out std::wstring &output);
#endif	// _WIN32

}	// namespace Console
}	// namespace NNN

#endif	// _NNNLIB___CONSOLE___CONSOLE_H_
