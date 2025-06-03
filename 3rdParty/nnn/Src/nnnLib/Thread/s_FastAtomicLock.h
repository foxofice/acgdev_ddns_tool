//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : （不可递归）高速原子锁（lock-free）
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNLIB___THREAD___S_FASTATOMICLOCK_H_
#define _NNNLIB___THREAD___S_FASTATOMICLOCK_H_

#include <atomic>
#include <thread>

namespace NNN
{
namespace Thread
{

struct s_FastAtomicLock
{
public:
	// 加锁/解锁
	inline void	Lock()
	{
		while(m_accessing.test_and_set(std::memory_order_acquire))
			std::this_thread::yield();
	}
	inline void	UnLock()
	{
		m_accessing.clear(std::memory_order_release);
	}

	// 尝试加锁（如果成功获取锁则返回 true，如果其他线程持有锁则返回 false）
	inline bool	TryLock()
	{
		return !m_accessing.test_and_set(std::memory_order_acquire);
	}

protected:
	std::atomic_flag	m_accessing	= ATOMIC_FLAG_INIT;
};

}	// namespace Thread
}	// namespace NNN

#endif	// _NNNLIB___THREAD___S_FASTATOMICLOCK_H_
