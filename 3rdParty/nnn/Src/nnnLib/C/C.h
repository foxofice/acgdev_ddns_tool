//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 统一 char*/WCHAR* 的 C 函数
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNLIB___C___C_H_
#define _NNNLIB___C___C_H_

#include <type_traits>

#include "../../common/common.h"

namespace NNN
{
namespace C
{

// strrchr/wcsrchr
/*
 @param	src	要搜索的 null 终止的字符串
 @param	c	要查找的字符
 @return	返回指向 str 中的 c 末次出现位置的指针；如果找不到 c，则返回 NULL
 */
template <typename T>
inline const T *strrchr(const T *str, T c)
{
	if constexpr (std::is_same_v<T, char>)
		return ::strrchr(str, (int)c);

	else if constexpr (std::is_same_v<T, WCHAR>)
		return wcsrchr(str, c);
}

// strcpy/wcscpy
/*
 @param	dest		目标字符串缓冲区的位置
 @param	dest_size	多字节窄函数 char 单元以及宽函数 wchar_t 单元中的目标字符串缓冲区的大小。此值必须大于零，且不能大于 RSIZE_MAX。 确保此大小考虑了字符串后的 NULL 终止。
 @param	src			以 null 结尾的源字符串缓冲区
 */
template <typename T>
inline void strcpy(T *dest, size_t dest_size, const T *src)
{
	if constexpr (std::is_same_v<T, char>)
	{
#if (NNN_PLATFORM == NNN_PLATFORM_WIN32) || (NNN_PLATFORM == NNN_PLATFORM_WP8)
		strcpy_s(dest, dest_size, src);
#else
		::strcpy(dest, src);
#endif	// NNN_PLATFORM
	}
	else if constexpr (std::is_same_v<T, WCHAR>)
	{
#if (NNN_PLATFORM == NNN_PLATFORM_WIN32) || (NNN_PLATFORM == NNN_PLATFORM_WP8)
		wcscpy_s(dest, dest_size, src);
#else
		wcscpy(dest, src);
#endif	// NNN_PLATFORM
	}
}

// strcat/wcscat
/*
 @param	dest		Null 终止的目标字符串缓冲区
 @param	dest_size	目标字符串缓冲区的大小
 @param	src			以 null 结尾的源字符串缓冲区
 */
template <typename T>
inline void strcat(T *dest, size_t dest_size, const T *src)
{
	if constexpr (std::is_same_v<T, char>)
	{
#if (NNN_PLATFORM == NNN_PLATFORM_WIN32) || (NNN_PLATFORM == NNN_PLATFORM_WP8)
		strcat_s(dest, dest_size, src);
#else
		::strcat(dest, src);
#endif	// NNN_PLATFORM
	}
	else if constexpr (std::is_same_v<T, WCHAR>)
	{
#if (NNN_PLATFORM == NNN_PLATFORM_WIN32) || (NNN_PLATFORM == NNN_PLATFORM_WP8)
		wcscat_s(dest, dest_size, src);
#else
		wcscat(dest, src);
#endif	// NNN_PLATFORM
	}
}

// strlen/wcslen
/*
 @param		str	以 Null 结尾的字符串
 @return	返回 str 中的字符数（不包括 Null 终止符）
 */
template <typename T>
inline size_t strlen(T const *str)
{
	if constexpr (std::is_same_v<T, char>)
		return ::strlen(str);

	else if constexpr (std::is_same_v<T, WCHAR>)
		return wcslen(str);
}

}	// namespace C
}	// namespace NNN

#endif	// _NNNLIB___C___C_H_
