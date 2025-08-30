//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : �����ɵݹ飩��������lock-free��
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNLIB___THREAD___S_ATOMIC_SPINLOCK_H_
#define _NNNLIB___THREAD___S_ATOMIC_SPINLOCK_H_

#include <atomic>

#include "thread-inc.h"

namespace NNN
{
namespace Thread
{

struct s_Atomic_SpinLock
{
public:
	// ����/����
	inline void	Lock()
	{
		while(m_accessing.test_and_set(std::memory_order_acquire))
			cpu_relax();
	}
	inline void	UnLock()
	{
		m_accessing.clear(std::memory_order_release);
	}

	// ���Լ���������ɹ���ȡ���򷵻� true����������̳߳������򷵻� false��
	inline bool	TryLock()
	{
		return !m_accessing.test_and_set(std::memory_order_acquire);
	}

protected:
	std::atomic_flag	m_accessing	= ATOMIC_FLAG_INIT;
};

}	// namespace Thread
}	// namespace NNN

#endif	// _NNNLIB___THREAD___S_ATOMIC_SPINLOCK_H_
