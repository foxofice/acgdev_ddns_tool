//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 临界区
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNLIB___THREAD___S_CRITICALSECTION_H_
#define _NNNLIB___THREAD___S_CRITICALSECTION_H_

#include "../../common/common-macro.h"

#ifndef NNN_WINDOWS
#include <pthread.h>
#endif	// !NNN_WINDOWS

#include "../../common/common.h"

namespace NNN
{
namespace Thread
{

struct s_CriticalSection
{
	// 构造函数/析构函数
	NNN_API			s_CriticalSection();
	NNN_API			~s_CriticalSection();

	// 加锁/解锁（递归锁，可多次 Lock，需要对应次数的 UnLock）
	NNN_API	void	Lock();
	NNN_API	void	UnLock();

	// 尝试加锁（如果成功获取锁则返回 true，如果其他线程持有锁则返回 false）
	NNN_API bool	TryLock();

protected:
#ifdef NNN_WINDOWS
	CRITICAL_SECTION	m_cs;
#elif defined(NNN_ANDROID) || defined(NNN_IOS)
	pthread_mutex_t		m_mutex;
#else
	//bool					m_use_mutex				= false;

	//pthread_spinlock_t	m_spinlock;						// spinlock_t 为内核态锁，pthread_spinlock_t 为用户态锁
	//std::atomic<int>		m_spinlock_count		= 0;	// spinlock 锁定次数
	//UINT64				m_spinlock_thread_id	= 0;	// spinlock 锁定线程

	pthread_mutex_t		m_mutex;
#endif	// NNN_WINDOWS
};

}	// namespace Thread
}	// namespace NNN

#endif	// _NNNLIB___THREAD___S_CRITICALSECTION_H_
