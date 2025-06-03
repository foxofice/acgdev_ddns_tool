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

#if (NNN_PLATFORM != NNN_PLATFORM_WIN32) && (NNN_PLATFORM != NNN_PLATFORM_WP8)
#include <pthread.h>
#endif	// !NNN_PLATFORM_WIN32 && !NNN_PLATFORM_WP8

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
#if (NNN_PLATFORM == NNN_PLATFORM_WIN32) || (NNN_PLATFORM == NNN_PLATFORM_WP8)
	CRITICAL_SECTION	m_cs;
#elif(NNN_PLATFORM == NNN_PLATFORM_ANDROID) || (NNN_PLATFORM == NNN_PLATFORM_IOS)
	pthread_mutex_t		m_mutex;
#else
	//bool					m_use_mutex				= false;

	//pthread_spinlock_t	m_spinlock;						// spinlock_t 为内核态锁，pthread_spinlock_t 为用户态锁
	//std::atomic<int>		m_spinlock_count		= 0;	// spinlock 锁定次数
	//UINT64				m_spinlock_thread_id	= 0;	// spinlock 锁定线程

	pthread_mutex_t		m_mutex;
#endif	// NNN_PLATFORM
};

}	// namespace Thread
}	// namespace NNN

#endif	// _NNNLIB___THREAD___S_CRITICALSECTION_H_
