//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 简单读写锁
//
//		【特性】
//		读锁递归：			√（单线程or多线程）
//		写锁递归：			×
//		锁升级：			×
//		锁降级：			×
//		写优先or读优先：	不确定
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNLIB___THREAD___C_SIMPLE_RWLOCK_H_
#define _NNNLIB___THREAD___C_SIMPLE_RWLOCK_H_

#include <shared_mutex>

namespace NNN
{
namespace Thread
{

class c_Simple_RWLock
{
public:
	inline void Lock_Read()		{ m_mutex.lock_shared(); }
	inline void Lock_Write()	{ m_mutex.lock(); }
	inline bool TryLock_Read()	{ return m_mutex.try_lock_shared(); }
	inline bool TryLock_Write()	{ return m_mutex.try_lock(); }

	inline void UnLock_Read()	{ m_mutex.unlock_shared(); }
	inline void UnLock_Write()	{ m_mutex.unlock(); }

private:
	std::shared_mutex	m_mutex;
};

}	// namespace Thread
}	// namespace NNN

#endif	// _NNNLIB___THREAD___C_SIMPLE_RWLOCK_H_
