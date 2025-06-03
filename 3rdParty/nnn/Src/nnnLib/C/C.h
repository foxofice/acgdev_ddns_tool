//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : ͳһ char*/WCHAR* �� C ����
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
 @param	src	Ҫ������ null ��ֹ���ַ���
 @param	c	Ҫ���ҵ��ַ�
 @return	����ָ�� str �е� c ĩ�γ���λ�õ�ָ�룻����Ҳ��� c���򷵻� NULL
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
 @param	dest		Ŀ���ַ�����������λ��
 @param	dest_size	���ֽ�խ���� char ��Ԫ�Լ����� wchar_t ��Ԫ�е�Ŀ���ַ����������Ĵ�С����ֵ��������㣬�Ҳ��ܴ��� RSIZE_MAX�� ȷ���˴�С�������ַ������ NULL ��ֹ��
 @param	src			�� null ��β��Դ�ַ���������
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
 @param	dest		Null ��ֹ��Ŀ���ַ���������
 @param	dest_size	Ŀ���ַ����������Ĵ�С
 @param	src			�� null ��β��Դ�ַ���������
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
 @param		str	�� Null ��β���ַ���
 @return	���� str �е��ַ����������� Null ��ֹ����
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
