//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : ���߳�����
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

// �������̵߳ĺ�������
typedef void	(*LP_TASK_FUNC)(int task_idx, class c_Tasks *task);

struct s_CreateTask_Params
{
	LP_TASK_FUNC	m_Func;							// �������߳�
	int				m_tasks_count;					// ��������
	int				m_max_threads_count	= 0;		// ����߳�����0 = CPU �߳�����
	void			*m_user_param		= nullptr;	// �Զ������
};

class c_Tasks
{
public:
	// ���캯��/����������<max_threads_count> = 0 ��ʾ��CPU �߳���������󲻳������߳��� * 2����
	NNN_API						c_Tasks(s_CreateTask_Params create_params);
	NNN_API						~c_Tasks();

	// �ȴ������������
	NNN_API void				wait_for_finish();

	// ��ȡ�߳���
	NNN_API inline size_t		get_threads_count()	{ return m_threads.size(); }

	// ��ȡ�߳�
	NNN_API struct s_Thread*	get_thread(UINT64 thread_id);

	// ��ȡ��ǰ����
	NNN_API inline int			get_progress()		{ return m_progress; }

	// ��ȡ�Զ������
	NNN_API inline void*		get_user_param()	{ return m_create_params.m_user_param; }

protected:
	// ȡ����һ������� idx������ -1 ʱ����ʾû����һ����Ҫ��ɵ�����
	int							fetch_task_idx();

	// �������߳�
	static void					Worker_Thread(void *param);

protected:
	std::set<int>						m_tasks;	// δ��ɵ� task
	class c_Atomic_Lock					m_cs_tasks;

	struct s_CreateTask_Params			m_create_params;

	// thread_id -> s_Thread
	std::map<UINT64, struct s_Thread*>	m_threads;

	// �����Ƿ��ѳ�ʼ�����
	std::atomic<int>					m_init_done		= false;
	// ��ǰ����
	std::atomic<int>					m_progress		= 0;

private:
	DISALLOW_COPY_AND_ASSIGN(c_Tasks);
};

}	// namespace Thread
}	// namespace NNN

#endif	// _NNNLIB___THREAD___C_TASKS_H_
