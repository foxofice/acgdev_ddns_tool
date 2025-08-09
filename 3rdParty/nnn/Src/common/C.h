//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : ͳһ char*|WCHAR* / Win|Linux �� C ����
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
 @param	dest	Ŀ���ַ�����������λ��
 @param	src		�� NULL ��β��Դ�ַ���������
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
 @param	dest	NULL ��ֹ��Ŀ���ַ���������
 @param	src		�� NULL ��β��Դ�ַ���������
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
 @param		str	�� Null ��β���ַ���
 @return	���� str �е��ַ����������� Null ��ֹ����
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
 @_Buffer		����Ĵ洢λ��
 @_BufferCount	�ɴ洢������ַ���
 @_Format		��ʽ�ؼ��ַ���
 @args			Ҫ���ø�ʽ�Ŀ�ѡ����
 @return		д����ַ����� -1�������������
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
 @dest		Ŀ�����
 @src		Դ����
 @count		Ҫ���Ƶ��ֽ���
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
 @buffer		����Ĵ洢λ��
 @sizeOfBuffer	buffer ���ַ�����С
 @count			Ҫд�������ַ����� ���ڲ��� �� wchar_t����������Ҫд��Ŀ��ַ�����
 @format		��ʽ�淶
 @argptr		ָ������б��ָ��
 @return		д����ַ����������� NULL�������������򷵻ظ���
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
 @param	src	Ҫ������ null ��ֹ���ַ���
 @param	c	Ҫ���ҵ��ַ�
 @return	����ָ�� str �е� c ĩ�γ���λ�õ�ָ�룻����Ҳ��� c���򷵻� NULL
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
