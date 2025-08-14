//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : �򵥶�д��
//
//		�����ԡ�
//		�����ݹ飺			�̣����߳�or���̣߳�
//		д���ݹ飺			��
//		��������			��
//		��������			��
//		д����or�����ȣ�	��ȷ��
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
