//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 线程 结构体
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNLIB___THREAD___S_THREAD_H_
#define _NNNLIB___THREAD___S_THREAD_H_

#include <thread>
#include <atomic>

#include "../../common/common.h"

#include "s_CriticalSection.h"

namespace NNN
{
namespace Thread
{

// 工作者线程的函数声明
typedef void	(*LP_THREAD_FUNC)(void *param);

// 线程结束回调
typedef void	(CALLBACK *LP_THREAD_EXIT_FUNC)();

struct s_CreateThread_Params
{
	LP_THREAD_FUNC		m_Func				= nullptr;
	//LP_THREAD_EXIT_FUNC	m_Exit_Func			= nullptr;
	void				*m_param			= nullptr;
	bool				m_set_param_auto	= false;	// true = 设置 s_Thread 自己的指针到 m_param
};

struct s_Thread
{
public:
	// 构造函数/析构函数
	NNN_API				s_Thread(struct s_CreateThread_Params create_params);
	NNN_API				~s_Thread();

	// （以阻塞的方式）等待线程结束
	NNN_API void		join();

	// 把线程设置为分离状态
	NNN_API void		detach();

	// 线程是否为关联状态
	NNN_API bool		joinable();

	// 获取线程 id
	NNN_API UINT64		get_id();

	// 设置线程为停止状态（需要在工作者线程进行判断&处理）
	NNN_API inline void	set_stop()	{ m_stop_requested = true; }
	NNN_API inline bool	get_stop()	{ return m_stop_requested; }

protected:
	// 工作者线程
	void				WorkerThread();

protected:
	// 目前 c++cli 只支持 c++17（所以 std::jthread* 改成 void*）
	void							*m_thread			= nullptr;	// std::jthread 类型

	// 是否标记为停止状态（需要在工作者线程进行判断&处理）
	std::atomic<bool>				m_stop_requested	= false;

	// 用户执行 CreateThread() 的参数
	struct s_CreateThread_Params	m_create_params;

private:
	DISALLOW_COPY_AND_ASSIGN(s_Thread);
};

}	// namespace Thread
}	// namespace NNN

#endif	// _NNNLIB___THREAD___S_THREAD_H_
