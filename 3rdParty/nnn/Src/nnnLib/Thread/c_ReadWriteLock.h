//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 读写锁
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
#ifndef _NNNLIB___THREAD___C_READWRITELOCK_H_
#define _NNNLIB___THREAD___C_READWRITELOCK_H_

#include <stack>

#include "Thread.h"
#include "c_ReadWriteLock_Base.h"

namespace NNN
{
namespace Thread
{

class c_ReadWriteLock : public c_ReadWriteLock_Base
{
public:
	// 构造函数/析构函数（<max_concurrent_threads_count> 最小为 16）
	NNN_API 				c_ReadWriteLock(UINT max_concurrent_threads_count = 1024);
	NNN_API virtual			~c_ReadWriteLock();

	// 获取/释放读取锁
	NNN_API virtual void	Lock_Read();
	NNN_API virtual void	UnLock_Read();

	// 获取/释放写入锁
	NNN_API virtual void	Lock_Write();

protected:
	// 获取当前线程的 read_count
	UINT*					get_read_count();

	// 检查当前线程的 read_count 是否为 0
	void					check_read_count_zero();

private:
	UINT						*m_read_count_buffer	= nullptr;

	std::stack<UINT>			m_read_count_no_used;
	std::map<UINT64, UINT>		m_read_count;		// thread_id -> m_read_count_buffer 的 idx
	struct s_CriticalSection	m_cs_read_count;	// 锁定 m_read_count_no_used/m_read_count
};

}	// namespace Thread
}	// namespace NNN

#endif	// _NNNLIB___THREAD___C_READWRITELOCK_H_
