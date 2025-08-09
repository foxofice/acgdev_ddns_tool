//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 时间相关函数
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNLIB___TIME___TIME_H_
#define _NNNLIB___TIME___TIME_H_

#include "../../common/common-macro.h"

#include <time.h>
#if defined(WIN32) || defined(_WIN32)
	#include <Windows.h>
#else
	#include <unistd.h>
	#include <sys/time.h>
#endif	// WIN32 || _WIN32

#include "../../common/common.h"

namespace NNN
{
namespace Time
{

// 获取当前时刻（tick，单位：ms）
//NNN_API DWORD			tick();
NNN_API UINT64			tick64();

// 获取当前时间（高精度计时器，单位：秒）
NNN_API double			get_time();

// time_t <--> tm
NNN_API inline time_t	tm_to_timet(tm tm_)	{ return mktime(&tm_); }
NNN_API inline tm		timet_to_tm(time_t time_)
{
	tm ret;

#if defined(WIN32) || defined(_WIN32)
	localtime_s(&ret, &time_);
#else
	ret = *localtime(&time_);
#endif	// WIN32 || _WIN32

	return ret;
}

// 获取当前时间
NNN_API inline time_t	get_current_time()	{ return time(nullptr); }
NNN_API inline tm		get_current_tm()	{ return timet_to_tm(get_current_time()); }

#if defined(WIN32) || defined(_WIN32)
// FILETIME <--> __int64
NNN_API __int64			FileTime_to_Int64(FILETIME time);
NNN_API FILETIME		Int64_to_FileTime(__int64 value);

// FILETIME <--> SYSTEMTIME
NNN_API SYSTEMTIME		FileTime_to_SystemTime(FILETIME time);
NNN_API FILETIME		SystemTime_to_FileTime(SYSTEMTIME time);

// FILETIME <--> time_t
NNN_API time_t			FileTime_to_timet(FILETIME time);
NNN_API FILETIME		timet_to_FileTime(time_t time);
#endif	// WIN32 || _WIN32

#if (NNN_PLATFORM == NNN_PLATFORM_WIN32)
// SYSTEMTIME <--> tm
NNN_API struct tm		SystemTime_to_TM(SYSTEMTIME sys_time);
NNN_API SYSTEMTIME		TM_to_SystemTime(struct tm time);
#endif	// NNN_PLATFORM_WIN32

// 检查某一年是否闰年
NNN_API bool			is_leap_year(int year);

// 根据年月日计算 weekday（month = 1~12、day = 1~31、返回值范围 = 0-6）
NNN_API int				calc_weekday(int year, int month, int day);

// 根据年月日计算 yearday（month = 1~12、day = 1~31、返回值范围 = 0~365）
NNN_API int				calc_yearday(int year, int month, int day);

}	// namespace Time
}	// namespace NNN

#endif	// _NNNLIB___TIME___TIME_H_
