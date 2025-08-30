//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 多线程任务
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNLIB___THREAD___C_TASKS_H_
#define _NNNLIB___THREAD___C_TASKS_H_

#include <set>
#include <map>

#include "../../common/common.h"
#include "c_Atomic_Lock.h"
#include "s_Thread.h"

namespace NNN
{
namespace Thread
{

// 工作者线程的函数声明
typedef void	(*LP_TASK_FUNC)(int task_idx, class c_Tasks *task);

struct s_CreateTask_Params
{
	LP_TASK_FUNC	m_Func;							// 工作者线程
	int				m_tasks_count;					// 任务总数
	int				m_max_threads_count	= 0;		// 最大线程数（0 = CPU 线程数）
	void			*m_user_param		= nullptr;	// 自定义参数
};

class c_Tasks
{
public:
	// 构造函数/析构函数（<max_threads_count> = 0 表示「CPU 线程数」，最大不超过「线程数 * 2」）
	NNN_API						c_Tasks(s_CreateTask_Params create_params);
	NNN_API						~c_Tasks();

	// 等待所有任务完成
	NNN_API void				wait_for_finish();

	// 获取线程数
	NNN_API inline size_t		get_threads_count()	{ return m_threads.size(); }

	// 获取线程
	NNN_API struct s_Thread*	get_thread(UINT64 thread_id);

	// 获取当前进度
	NNN_API inline int			get_progress()		{ return m_progress; }

	// 获取自定义参数
	NNN_API inline void*		get_user_param()	{ return m_create_params.m_user_param; }

protected:
	// 取得下一个任务的 idx（返回 -1 时，表示没有下一个需要完成的任务）
	int							fetch_task_idx();

	// 工作者线程
	static void					Worker_Thread(void *param);

protected:
	std::set<int>						m_tasks;	// 未完成的 task
	class c_Atomic_Lock					m_cs_tasks;

	struct s_CreateTask_Params			m_create_params;

	// thread_id -> s_Thread
	std::map<UINT64, struct s_Thread*>	m_threads;

	// 任务是否已初始化完成
	std::atomic<int>					m_init_done		= false;
	// 当前进度
	std::atomic<int>					m_progress		= 0;

private:
	DISALLOW_COPY_AND_ASSIGN(c_Tasks);
};

}	// namespace Thread
}	// namespace NNN

#endif	// _NNNLIB___THREAD___C_TASKS_H_
