//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : （可递归）原子锁（性能高于 CRITICAL_SECTION）
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNLIB___THREAD___C_ATOMIC_LOCK_H_
#define _NNNLIB___THREAD___C_ATOMIC_LOCK_H_

#include <atomic>

#include "thread-inc.h"

namespace NNN
{
namespace Thread
{

class c_Atomic_Lock
{
public:
	// 加锁/解锁
	NNN_API void	Lock(int SpinCount = m_s_k_DefaultSpinCount);
	NNN_API void	UnLock();

	// 尝试加锁（如果成功获取锁则返回 true，如果其他线程持有锁则返回 false）
	NNN_API bool	TryLock();

protected:
	std::atomic<bool>	m_lock			= false;	// 是否已加锁
	std::atomic<UINT64>	m_thread_id		= 0;		// 获取锁的线程
	std::atomic<UINT>	m_lock_count	= 0;

#if defined(_M_ARM64) || defined(__aarch64__) || defined(_M_ARM) || defined(__arm__)
	// ARM
	static constexpr int	m_s_k_DefaultSpinCount	= 64;
#else
	// x86/x64
	static constexpr int	m_s_k_DefaultSpinCount	= 128;
#endif
};

}	// namespace Thread
}	// namespace NNN

#endif	// _NNNLIB___THREAD___C_ATOMIC_LOCK_H_
