//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 统一 char*|WCHAR* / Win|Linux 的 C 函数
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNN___C___C_H_
#define _NNN___C___C_H_

#include <type_traits>

#include "common.h"

namespace NNN
{
namespace C
{

#pragma region strcpy/wcscpy/STRCPY
inline void strcpy(char *dest, const char *src)
{
#if defined(WIN32) || defined(_WIN32)
	strcpy_s(dest, strlen(src) + 1, src);
#else
	::strcpy(dest, src);
#endif	// WIN32 || _WIN32
}
//--------------------------------------------------
inline void wcscpy(WCHAR *dest, const WCHAR *src)
{
#if defined(WIN32) || defined(_WIN32)
	wcscpy_s(dest, wcslen(src) + 1, src);
#else
	::wcscpy(dest, src);
#endif	// WIN32 || _WIN32
}
//--------------------------------------------------
/*
 @param	dest	目标字符串缓冲区的位置
 @param	src		以 NULL 结尾的源字符串缓冲区
 */
template <typename T>
inline void STRCPY(T *dest, const T *src)
{
	if constexpr (std::is_same_v<T, char>)
	{
		strcpy(dest, src);
	}
	else if constexpr (std::is_same_v<T, WCHAR>)
	{
		wcscpy(dest, src);
	}
}
#pragma endregion

#pragma region strcat/wcscat/STRCAT
inline void strcat(char *dest, const char *src)
{
#if defined(WIN32) || defined(_WIN32)
	strcat_s(dest, strlen(dest) + strlen(src) + 1, src);
#else
	::strcat(dest, src);
#endif	// WIN32 || _WIN32
}
//--------------------------------------------------
inline void wcscat(WCHAR *dest, const WCHAR *src)
{
#if defined(WIN32) || defined(_WIN32)
	wcscat_s(dest, wcslen(dest) + wcslen(src) + 1, src);
#else
	::wcscat(dest, src);
#endif	// WIN32 || _WIN32
}
//--------------------------------------------------
/*
 @param	dest	NULL 终止的目标字符串缓冲区
 @param	src		以 NULL 结尾的源字符串缓冲区
 */
template <typename T>
inline void STRCAT(T *dest, const T *src)
{
	if constexpr (std::is_same_v<T, char>)
	{
		strcat(dest, src);
	}
	else if constexpr (std::is_same_v<T, WCHAR>)
	{
		wcscat(dest, src);
	}
}
#pragma endregion

#pragma region STRLEN
/*
 @param		str	以 Null 结尾的字符串
 @return	返回 str 中的字符数（不包括 Null 终止符）
 */
template <typename T>
inline size_t STRLEN(T const *str)
{
	if constexpr (std::is_same_v<T, char>)
		return ::strlen(str);

	else if constexpr (std::is_same_v<T, WCHAR>)
		return wcslen(str);
}
#pragma endregion

#pragma region sprintf/swprintf/SPRINTF
template <typename... T_Args>
inline int sprintf(char *_Buffer, size_t _BufferCount, const char *_Format, T_Args... args)
{
#if defined(WIN32) || defined(_WIN32)
	return sprintf_s(_Buffer, _BufferCount, _Format, args...);
#else
	return ::sprintf(_Buffer, _Format, args...);
#endif	// WIN32 || _WIN32
}
//--------------------------------------------------
template <typename... T_Args>
inline int swprintf(WCHAR *_Buffer, size_t _BufferCount, const WCHAR *_Format, T_Args... args)
{
#if defined(WIN32) || defined(_WIN32)
	return ::swprintf_s(_Buffer, _BufferCount, _Format, args...);
#else
	return ::swprintf(_Buffer, _Format, args...);
#endif	// WIN32 || _WIN32
}
//--------------------------------------------------
/*
 @_Buffer		输出的存储位置
 @_BufferCount	可存储的最多字符数
 @_Format		格式控件字符串
 @args			要设置格式的可选参数
 @return		写入的字符数或 -1（如果发生错误）
 */
template <typename T, typename... T_Args>
inline int SPRINTF(T *_Buffer, size_t _BufferCount, const T *_Format, T_Args... args)
{
	if constexpr (std::is_same_v<T, char>)
		return sprintf(_Buffer, _BufferCount, _Format, args...);

	if constexpr (std::is_same_v<T, WCHAR>)
		return swprintf(_Buffer, _BufferCount, _Format, args...);
}
#pragma endregion

#pragma region MEMMOVE
/*
 @dest		目标对象
 @src		源对象
 @count		要复制的字节数
*/
inline void MEMMOVE(void *dest, const void *src, size_t count)
{
#if defined(WIN32) || defined(_WIN32)
	memmove_s(dest, count, src, count);
#else
	memmove(dest, src, count);
#endif	// WIN32 || _WIN32
}
#pragma endregion

#pragma region stricmp/wcsicmp/STRICMP
inline int stricmp(const char *_String1, const char *_String2)
{
#if defined(WIN32) || defined(_WIN32)
	return _stricmp(_String1, _String2);
#else
	return strcasecmp(_String1, _String2);
#endif	// WIN32 || _WIN32
}
//--------------------------------------------------
inline int wcsicmp(const WCHAR *_String1, const WCHAR *_String2)
{
#if defined(WIN32) || defined(_WIN32)
	return _wcsicmp(_String1, _String2);
#else
	return wcscasecmp(_String1, _String2);
#endif	// WIN32 || _WIN32
}
//--------------------------------------------------
template <typename T>
inline int STRICMP(const T *_String1, const T *_String2)
{
	if constexpr (std::is_same_v<T, char>)
		return stricmp(_String1, _String2);

	if constexpr (std::is_same_v<T, WCHAR>)
		return wcsicmp(_String1, _String2);
}
#pragma endregion

#pragma region vsnprintf/vsnwprintf/VSNPRINTF
inline int vsnprintf(char *buffer, size_t sizeOfBuffer, size_t count, const char *format, va_list argptr)
{
#if defined(WIN32) || defined(_WIN32)
	return vsnprintf_s(buffer, sizeOfBuffer, count, format, argptr);
#else
	return vsnprintf(buffer, count, format, argptr);
#endif	// WIN32 || _WIN32
}
//--------------------------------------------------
inline int vsnwprintf(WCHAR *buffer, size_t sizeOfBuffer, size_t count, const WCHAR *format, va_list argptr)
{
#if defined(WIN32) || defined(_WIN32)
	return _vsnwprintf_s(buffer, sizeOfBuffer, count, format, argptr);
#else
	return vsnwprintf(buffer, count, format, argptr);
#endif	// WIN32 || _WIN32
}
//--------------------------------------------------
/*
 @buffer		输出的存储位置
 @sizeOfBuffer	buffer 的字符数大小
 @count			要写入的最大字符数。 对于采用 的 wchar_t函数，它是要写入的宽字符数。
 @format		格式规范
 @argptr		指向参数列表的指针
 @return		写入的字符数，不包括 NULL。如果输出错误则返回负数
 */
template <typename T>
inline int VSNPRINTF(T *buffer, size_t sizeOfBuffer, size_t count, const T *format, va_list argptr)
{
	if constexpr (std::is_same_v<T, char>)
		return vsnprintf(buffer, sizeOfBuffer, count, format, argptr);

	if constexpr (std::is_same_v<T, WCHAR>)
		return vsnwprintf(buffer, sizeOfBuffer, count, format, argptr);
}
#pragma endregion

#pragma region STRRCHR
/*
 @param	src	要搜索的 null 终止的字符串
 @param	c	要查找的字符
 @return	返回指向 str 中的 c 末次出现位置的指针；如果找不到 c，则返回 NULL
 */
template <typename T>
inline const T *STRRCHR(const T *str, T c)
{
	if constexpr (std::is_same_v<T, char>)
		return ::strrchr(str, (int)c);

	else if constexpr (std::is_same_v<T, WCHAR>)
		return wcsrchr(str, c);
}
#pragma endregion

}	// namespace C
}	// namespace NNN

#endif	// _NNN___C___C_H_
