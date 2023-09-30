//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : （可递归）原子锁（无锁、Lock-Free）
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNLIB___THREAD___S_ATOMICLOCK_H_
#define _NNNLIB___THREAD___S_ATOMICLOCK_H_

#include <atomic>

namespace NNN
{
namespace Thread
{

struct s_AtomicLock
{
public:
	// 锁定/解锁
	//（sleep_tick = 0 时，只适合锁定执行时间极短的代码，否则会占用大量 CPU 资源）
	NNN_API void	Lock(UINT sleep_tick = 0);
	NNN_API void	UnLock();

protected:
	std::atomic<UINT64>	m_thread_id		= 0;	// 获取锁的线程
	std::atomic<UINT>	m_lock_count	= 0;
};

}	// namespace Thread
}	// namespace NNN

#endif	// _NNNLIB___THREAD___S_ATOMICLOCK_H_
