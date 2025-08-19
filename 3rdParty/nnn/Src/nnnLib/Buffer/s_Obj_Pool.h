//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 对象池（支持超时、线程安全）
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNLIB___BUFFER___S_OBJ_POOL__H_
#define _NNNLIB___BUFFER___S_OBJ_POOL__H_

#include <queue>

#include "../Time/Time_.h"
#include "../Thread/s_FastAtomicLock.h"

namespace NNN
{
namespace Buffer
{

template <typename T, typename... T_Args>
struct s_Obj_Pool
{
	// 构造函数/析构函数
	s_Obj_Pool(UINT64 time_out = 0)
	{
		m_time_out = time_out;
	}
	~s_Obj_Pool()
	{
		delete_all_objs();
	}

	// 创建/清理对象
	T*	create_object(T_Args... args)
	{
		m_lock.Lock();

		if(!m_pool.empty() && m_pool.front().second <= Time::tick64())
		{
			T *ret = m_pool.front().first;
			m_pool.pop();

			m_lock.UnLock();
			return ret;
		}

		m_lock.UnLock();
		return new T(args...);
	}
	void	release_object(T *obj)
	{
		if(obj == nullptr)
			return;

		UINT64 timeout = Time::tick64() + m_time_out;

		m_lock.Lock();

		m_pool.push({ obj, timeout });

		m_lock.UnLock();
	}

	// 删除所有对象
	void	delete_all_objs()
	{
		std::vector<T*> delete_list;
		delete_list.reserve(m_pool.size());

		m_lock.Lock();

		while(!m_pool.empty())
		{
			T *obj = m_pool.front().first;
			m_pool.pop();

			delete_list.push_back(std::move(obj));
		}	// while

		m_lock.UnLock();

		for(T *obj : delete_list)
		{
			SAFE_DELETE(obj);
		}	// for
	}

	// 获取对象数量
	size_t	get_obj_count()
	{
		return m_pool.size();
	}

	// 获取所有对象
	void	get_all_objs(__out std::queue<std::pair<T*, UINT64>> &objs)
	{
		m_lock.Lock();

		objs = m_pool;

		m_lock.UnLock();
	}

protected:
	UINT64								m_time_out	= 0;

	std::queue<std::pair<T*, UINT64>>	m_pool;	// 空闲对象(T*, can_use_tick)
	struct Thread::s_FastAtomicLock		m_lock;
};

}	// namespace Buffer
}	// namespace NNN

#endif	// _NNNLIB___BUFFER___S_OBJ_POOL__H_
