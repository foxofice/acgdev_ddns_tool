//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 客户端数据
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNSOCKET___SOCKET_SYSTEM___C_STATUS_H_
#define _NNNSOCKET___SOCKET_SYSTEM___C_STATUS_H_

#include <atomic>

#include "../../nnnLib/Thread/Thread.h"
#include "../../nnnLib/Thread/c_Simple_RWLock.h"

namespace NNN
{
namespace Socket
{

class c_Status
{
public:
	struct s_Thread_Data
	{
		struct
		{
			UINT	m_last_speed		= 0;	// 上次统计的速度（字节/s）
			UINT	m_speed				= 0;	// 当前速度（字节/s）
			UINT64	m_start_update_tick	= 0;	// 上次开始统计速度的时刻
			UINT64	m_total_len			= 0;	// 数据传输总大小
		} RECV, SEND;
	};

	// 构造函数/析构函数
	NNN_API				c_Status();
	NNN_API				~c_Status();

	// 重置数据
	NNN_API inline void	Reset()
	{
		THREAD_DATA.m_count	= 0;

		m_next_thread_idx	= 0;

		{
			THREAD_IDX_LIST.m_lock.Lock_Write();
			THREAD_IDX_LIST.m_list.clear();
			THREAD_IDX_LIST.m_lock.UnLock_Write();
		}
	}

	// 设置获取是否允许统计速度/数据大小
	NNN_API inline void	SetEnabled(bool enabled)	{ m_enabled = enabled; }
	NNN_API inline bool	GetEnabled()				{ return m_enabled; }

	// 设置最大线程数（可以超出 WorkerThread 数量）
	NNN_API void		set_max_threads_count(size_t max_count);

	// 获取当前线程的 idx（建议在 WorkerThread 保存这个返回值。当无法直接获取保存值的时候，再回退到 HASH_MAP 查询）
	NNN_API size_t		get_thread_idx();

	// 更新速度、总大小
	NNN_API void		UpdateSendStatus(UINT add_size, size_t thread_idx);
	NNN_API void		UpdateRecvStatus(UINT add_size, size_t thread_idx);

	// 获取实时速度、总大小
	NNN_API UINT		GetSendSpeed();
	NNN_API UINT		GetRecvSpeed();
	NNN_API UINT64		GetSendTotalLen();
	NNN_API UINT64		GetRecvTotalLen();

protected:
	bool									m_enabled			= false;	// 是否开启速度统计

	struct
	{
		std::vector<struct s_Thread_Data>	m_data;							// 多线程并发数据
		std::atomic<size_t>					m_count				= 0;		// 当前的数量
	} THREAD_DATA;

	// 下一个要分配的 thread_idx（建议在线程一开始获取这个值）
	std::atomic<size_t>						m_next_thread_idx	= 0;

	struct
	{
		// thread_id -> thread_idx
		NNN_HASH_MAP<UINT64, size_t>		m_list;
		class NNN::Thread::c_Simple_RWLock	m_lock;
	} THREAD_IDX_LIST;
};

}	// namespace Socket
}	// namespace NNN

#endif	// _NNNSOCKET___SOCKET_SYSTEM___C_STATUS_H_
