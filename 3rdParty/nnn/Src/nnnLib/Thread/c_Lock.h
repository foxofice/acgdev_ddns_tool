//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 锁（自动 Lock/UnLock）
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNLIB___THREAD___C_LOCK_H_
#define _NNNLIB___THREAD___C_LOCK_H_

namespace NNN
{
namespace Thread
{

template <typename T>
class c_Lock
{
public:
	// 构造函数/析构函数
	inline c_Lock(T &lock)
	{
		m_lock = &lock;
		lock.Lock();
	}
	inline ~c_Lock()
	{
		m_lock->UnLock();
	}

private:
	T	*m_lock	= nullptr;
};

}	// namespace Thread
}	// namespace NNN

#endif	// _NNNLIB___THREAD___C_LOCK_H_
