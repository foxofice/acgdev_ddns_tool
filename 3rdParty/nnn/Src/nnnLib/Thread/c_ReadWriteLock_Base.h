//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : （写优先/相同线程可读写递归）读写锁
//--------------------------------------------------------------------------------------

/*
+------------+------------+------------+------------+
|            | 0 个写线程 | 1 个写线程 | n 个写线程 |
+------------+------------+------------+------------+
| 0 个读线程 |     √     |     √     |     ×     |
+------------+------------+------------+------------+
| 1 个读线程 |     √     |     ×     |     ×     |
+------------+------------+------------+------------+
| n 个读线程 |     √     |     ×     |     ×     |
+------------+------------+------------+------------+
*/

#pragma once
#ifndef _NNNLIB___THREAD___C_READWRITELOCK_BASE_H_
#define _NNNLIB___THREAD___C_READWRITELOCK_BASE_H_

#include <atomic>
#include <map>

#include "../../common/common-macro.h"

#if (NNN_PLATFORM != NNN_PLATFORM_WIN32) && (NNN_PLATFORM != NNN_PLATFORM_WP8)
#include <pthread.h>
#endif	// !NNN_PLATFORM_WIN32 && !NNN_PLATFORM_WP8

#include "../../common/common.h"

#include "s_AtomicLock.h"
#include "s_CriticalSection.h"

namespace NNN
{
namespace Thread
{

class c_ReadWriteLock_Base
{
public:
	// 构造函数/析构函数
	NNN_API					c_ReadWriteLock_Base();
	NNN_API virtual			~c_ReadWriteLock_Base();

	// 获取/释放读取锁
	NNN_API virtual void	Lock_Read()		{}
	NNN_API virtual void	UnLock_Read()	{}

	// 获取/释放写入锁
	NNN_API virtual void	Lock_Write()	{}
	NNN_API virtual void	UnLock_Write()	{ unlock_write_func(); }

protected:
	NNN_API void			lock_read_func(__inout UINT &current_thread_read_count);
	NNN_API void			unlock_read_func(__inout UINT &current_thread_read_count);

	// 获取/释放写入锁
	NNN_API void			lock_write_func(UINT current_thread_read_count);
	NNN_API void			unlock_write_func();

protected:
	struct
	{
		UINT					m_read_threads_count	= 0;	// m_read_threads 的数量

		std::atomic<UINT>		m_ready_to_write_count	= 0;

		UINT64					m_write_thread_id		= 0;	// 获取 write 锁的线程 id
		UINT					m_write_count			= 0;	// 获取 write 锁的次数

#if defined(DEBUG) || defined(_DEBUG)
		std::map<UINT64, UINT>	m_read_count_debug;				// thread_id -> read_count
#endif	// DEBUG || _DEBUG
	} data;

	struct s_AtomicLock		m_lock_data;					// 锁定以上数据

#if (NNN_PLATFORM == NNN_PLATFORM_WIN32) || (NNN_PLATFORM == NNN_PLATFORM_WP8)
	CRITICAL_SECTION	m_cs;
	CONDITION_VARIABLE	m_cv_read;
	CONDITION_VARIABLE	m_cv_write;
#else
	pthread_mutex_t		m_mutex;
	pthread_cond_t		m_cv_read;
	pthread_cond_t		m_cv_write;
#endif	// NNN_PLATFORM

protected:
	// 发送信号通知所有挂起的读取线程
	void	notify_all_read();

	// 发送信号通知一个挂起的写入线程
	void	notify_one_write();

private:
	DISALLOW_COPY_AND_ASSIGN(c_ReadWriteLock_Base);
};

}	// namespace Thread
}	// namespace NNN

#endif	// _NNNLIB___THREAD___C_READWRITELOCK_BASE_H_
