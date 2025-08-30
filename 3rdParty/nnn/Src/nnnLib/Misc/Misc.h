//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 其他
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNLIB___MISC___MISC_H_
#define _NNNLIB___MISC___MISC_H_

#include "../../common/common.h"

#ifdef _WIN32
#include <Dbghelp.h>
#endif	// _WIN32

#include "s_CPU_Flags.h"

namespace NNN
{
namespace Misc
{

// 初始化/清理
HRESULT			DoInit();
HRESULT			DoFinal();

// 获取 CPU 核心数
NNN_API int		get_processors_count();

// 获取硬件线程数
NNN_API int		get_hardware_concurrency();

#if defined(NNN_WINDOWS) || defined(NNN_LINUX)
// 获取当前的 CPU 占用率
NNN_API float	get_cpu_usage();

// 获取 RAM（物理内存）总大小（字节）
NNN_API UINT64	get_physical_memory();

// 获取当前的剩余 RAM（物理内存）大小（字节）
NNN_API UINT64	get_avail_physical_memory();
#endif	// NNN_WINDOWS || NNN_LINUX

#if	defined(NNN_WINDOWS) ||	defined(NNN_LINUX)
// 获取 cpuid
NNN_API void	cpuid(unsigned int CPUInfo[4], unsigned int InfoType);
#endif	// NNN_WINDOWS || NNN_LINUX

// 获取 CPU 指令周期数
NNN_API UINT64	rdtsc();

}	// namespace Misc
}	// namespace NNN

#endif	// _NNNLIB___MISC___MISC_H_
