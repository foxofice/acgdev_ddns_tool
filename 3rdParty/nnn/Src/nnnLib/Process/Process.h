//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 进程相关
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNLIB___PROCESS___PROCESS_H_
#define _NNNLIB___PROCESS___PROCESS_H_

#include "../../common/common.h"

namespace NNN
{
namespace Process
{

#if (NNN_PLATFORM == NNN_PLATFORM_WIN32)
// 设置进程亲缘性（用 | 叠加每颗核心。0 = 全部，-1 = 最后一颗核心）
NNN_API BOOL	set_process_affinity_mask(int process_affinity_mask = -1);
#endif	// NNN_PLATFORM_WIN32

// 获取进程 ID（返回 -1 时，表示进程不存在）
// （Win 不区分大小写，Linux 区分大小写）
NNN_API int		get_pid_by_name(const WCHAR *proc_name);
NNN_API int		get_pid_by_name(const char *proc_name);

// 结束进程
NNN_API bool	kill_process(UINT pid);

// 获取当前进程的 ID
NNN_API	UINT	get_current_process_id();

// 获取当前线程正在哪个核心运行
NNN_API bool	get_current_processor_number(__out UINT &number);

}	// namespace Process
}	// namespace NNN

#endif	// _NNNLIB___PROCESS___PROCESS_H_
