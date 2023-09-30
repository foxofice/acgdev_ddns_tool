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
#include "../Thread/s_AtomicLock.h"
#include "../Thread/c_Lock.h"

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
		T *ret = fetch_idle_object();

		if(ret != nullptr)
			return ret;

		return new T(args...);
	}
	void	release_object(T *obj)
	{
		if(obj == nullptr)
			return;

		Thread::c_Lock l(m_lock);

		m_objs_no_used.push({ obj, Time::tick64() + m_time_out });
	}

	// 删除所有对象
	void	delete_all_objs()
	{
		std::vector<T*> delete_list;
		delete_list.reserve(m_objs_no_used.size());

		{
			Thread::c_Lock l(m_lock);

			while(!m_objs_no_used.empty())
			{
				T *obj = m_objs_no_used.front().first;
				m_objs_no_used.pop();

				delete_list.push_back(obj);
			}	// while
		}

		for(T *obj : delete_list)
		{
			SAFE_DELETE(obj);
		}	// for
	}

	// 取得一个空闲对象（如果没有空闲对象，则返回 nullptr）
	T*	fetch_idle_object()
	{
		T *ret = nullptr;

		Thread::c_Lock l(m_lock);

		if(m_objs_no_used.empty() || m_objs_no_used.front().second > Time::tick64())
		{
			// do nothing
		}
		else
		{
			ret = m_objs_no_used.front().first;
			m_objs_no_used.pop();
		}

		return ret;
	}

	// 获取对象数量
	size_t	get_obj_count()
	{
		return m_objs_no_used.size();
	}

	// 获取所有对象
	void	get_all_objs(__out std::queue<std::pair<T*, UINT64>> &objs)
	{
		Thread::c_Lock l(m_lock);

		objs = m_objs_no_used;
	}

protected:
	std::queue<std::pair<T*, UINT64>>	m_objs_no_used;	// 不使用的对象(T*, can_use_tick)
	UINT64								m_time_out	= 0;

	struct Thread::s_AtomicLock			m_lock;
};

}	// namespace Buffer
}	// namespace NNN

#endif	// _NNNLIB___BUFFER___S_OBJ_POOL__H_
