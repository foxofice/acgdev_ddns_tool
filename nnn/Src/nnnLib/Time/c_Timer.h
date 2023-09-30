//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 高精度计时器
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNLIB___TIME___C_TIMER_H_
#define _NNNLIB___TIME___C_TIMER_H_

#include "../../common/common-macro.h"

//#include <time.h>
//#if (NNN_PLATFORM == NNN_PLATFORM_WIN32) || (NNN_PLATFORM == NNN_PLATFORM_WP8)
//	#include <Windows.h>
//#else
//	#include <unistd.h>
//	#include <sys/time.h>
//#endif	// NNN_PLATFORM_WIN32 || NNN_PLATFORM_WP8
//
//#include "../../common/common.h"

namespace NNN
{
namespace Time
{

class c_Timer
{
public:
	// 构造函数
	NNN_API			c_Timer();

	NNN_API void	Reset();			// 重置计时器
	NNN_API void	Start();			// 开始计时器
	NNN_API void	Stop();				// 停止（或暂停）计时器
#if (NNN_PLATFORM == NNN_PLATFORM_WIN32) || (NNN_PLATFORM == NNN_PLATFORM_WP8)
	NNN_API void	Advance();			// 推进计时器 0.1 秒
#endif	// NNN_PLATFORM_WIN32 || NNN_PLATFORM_WP8
	NNN_API double	GetAbsoluteTime();	// 获取绝对系统时间
	NNN_API double	GetTime();			// 获取当前时间
	NNN_API float	GetElapsedTime();	// 获取自上次调用 GetElapsedTime() 的逝去时间

	// 一次性获取所有时间值
	NNN_API void	GetTimeValues(	__out double	*pfTime,
									__out double	*pfAbsoluteTime,
									__out float		*pfElapsedTime );

	NNN_API bool	IsStopped();		// 获取计时器是否已停止

#if (NNN_PLATFORM == NNN_PLATFORM_WIN32)
	// 限制当前线程在一个处理器中（当前的处理器）
	// 这将确保计时代码运行在唯一的处理器上，并将不会受到电源管理（power management）的任何不良影响
	void			LimitThreadAffinityToCurrentProc();
#endif	// NNN_PLATFORM_WIN32 || NNN_PLATFORM_WP8

	// 重置 fElapsedTime（通常在 OnCreate() 完成后执行）
	// 在意外阻塞计时后（例如：长时间的 IO 读写操作），
	// 为了避免 FrameMove/FrameRender 由于 fElapsedTime 逻辑而导致的渲染问题，请调用此函数
	void			ResetElapsedTime();

protected:
	// 如果已停止，则返回停止时的时间；否则返回当前时间
	LARGE_INTEGER	GetAdjustedCurrentTime();

#if (NNN_PLATFORM == NNN_PLATFORM_WIN32) || (NNN_PLATFORM == NNN_PLATFORM_WP8)
	bool		m_bUsingQPF;
#endif	// NNN_PLATFORM_WIN32 || NNN_PLATFORM_WP8
	bool		m_bTimerStopped		= true;
#if (NNN_PLATFORM == NNN_PLATFORM_WIN32) || (NNN_PLATFORM == NNN_PLATFORM_WP8)
	LONGLONG	m_llQPFTicksPerSec	= 0;
#endif	// NNN_PLATFORM_WIN32 || NNN_PLATFORM_WP8

	LONGLONG	m_llStopTime		= 0;
	LONGLONG	m_llLastElapsedTime	= 0;
	LONGLONG	m_llBaseTime		= 0;
};

}	// namespace Time
}	// namespace NNN

#endif	// _NNNLIB___TIME___C_TIMER_H_
