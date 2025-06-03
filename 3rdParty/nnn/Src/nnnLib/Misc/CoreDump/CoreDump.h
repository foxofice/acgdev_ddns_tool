//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : CoreDump
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNLIB___MISC___COREDUMP___COREDUMP_H_
#define _NNNLIB___MISC___COREDUMP___COREDUMP_H_

#include "s_CoreDump_settings.h"

namespace NNN
{
namespace Misc
{
namespace CoreDump
{

extern struct s_CoreDump_settings	g_CoreDump_settings;

// 初始化/清理
HRESULT			DoInit();
HRESULT			DoFinal();

// 开启 core dump 生成（默认关闭。<filename>、<filenmae_add_datetime> 仅 Windows 有效）
NNN_API void	enable_core_dump(const struct s_CoreDump_settings &settings);

}	// namespace CoreDump
}	// namespace Misc
}	// namespace NNN

#endif	// _NNNLIB___MISC___COREDUMP___COREDUMP_H_
