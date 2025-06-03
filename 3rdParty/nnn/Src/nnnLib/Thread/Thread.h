//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 线程相关
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNLIB___THREAD___THREAD_H_
#define _NNNLIB___THREAD___THREAD_H_

#include "../../common/common-macro.h"

#if (NNN_PLATFORM != NNN_PLATFORM_WIN32) && (NNN_PLATFORM != NNN_PLATFORM_WP8)
#include <pthread.h>
#endif	// NNN_PLATFORM

#include "../../common/common.h"

namespace NNN
{
namespace Thread
{

// 让当前线程睡眠
NNN_API void	Sleep(DWORD dwMilliseconds);

// 获取当前线程的 ID
NNN_API UINT64	GetCurrentThreadID();

// 检查指定线程是否存在
NNN_API HRESULT	is_thread_exists(UINT64 thread_id, bool &is_exists);

// 设置线程亲缘性（用 | 叠加每颗核心。0 = 全部，-1 = 最后一颗核心）
NNN_API BOOL	set_thread_affinity_mask(int process_affinity_mask = -1);

}	// namespace Thread
}	// namespace NNN

#endif	// _NNNLIB___THREAD___THREAD_H_
