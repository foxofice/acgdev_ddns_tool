//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : （可递归）原子锁（Lock-Free）
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNLIB___THREAD___S_ATOMICLOCK_H_
#define _NNNLIB___THREAD___S_ATOMICLOCK_H_

#include <atomic>

#include "../../common/common-inl.h"

namespace NNN
{
namespace Thread
{

struct s_AtomicLock
{
public:
	// 加锁/解锁
	NNN_API void	Lock();
	NNN_API void	UnLock();

	// 尝试加锁（如果成功获取锁则返回 true，如果其他线程持有锁则返回 false）
	NNN_API bool	TryLock();

protected:
	std::atomic<UINT64>	m_thread_id		= 0;	// 获取锁的线程
	UINT				m_lock_count	= 0;
};

}	// namespace Thread
}	// namespace NNN

#endif	// _NNNLIB___THREAD___S_ATOMICLOCK_H_
