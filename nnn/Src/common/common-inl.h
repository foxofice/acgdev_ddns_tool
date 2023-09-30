//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 通用包含文件（内联）
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNN___COMMON___COMMON_INL_H_
#define _NNN___COMMON___COMMON_INL_H_

#include <exception>

#include "common-macro.h"
#include "WinStyle.h"

//=====Added by FPE(2011.12.8)=====(修正 Linux/Unix 下 hash_map 不能用 string 做 key 的 bug)(Start)
//#if (NNN_PLATFORM != NNN_PLATFORM_WIN32) && (NNN_PLATFORM != NNN_PLATFORM_WP8)
//#ifdef __cplusplus
//#include <string>
//#include <ext/hash_map>
////#if (NNN_PLATFORM != NNN_PLATFORM_IOS)
////#include <tr1/functional_hash.h>
////#endif	// !NNN_PLATFORM_IOS
//
/* BEGIN FIX */
//namespace __gnu_cxx
//{
//template<> struct hash< std::string >
//{
//	size_t operator()( const std::string& x ) const
//	{
//		size_t _Val = 2166136261U;
//		size_t _First = 0;
//		size_t _Last = x.size();
//		size_t _Stride = 1 + _Last / 10;
//
//		for(; _First < _Last; _First += _Stride)
//			_Val = 16777619U * _Val ^ (size_t)x[_First];
//		return (_Val);
//	}
//};
//
//template<> struct hash< std::wstring >
//{
//	size_t operator()( const std::wstring& x ) const
//	{
//		size_t _Val = 2166136261U;
//		size_t _First = 0;
//		size_t _Last = x.size();
//		size_t _Stride = 1 + _Last / 10;
//
//		for(; _First < _Last; _First += _Stride)
//			_Val = 16777619U * _Val ^ (size_t)x[_First];
//		return (_Val);
//	}
//};
//
//template<> struct hash< wchar_t >
//{
//	size_t operator()( const wchar_t& x ) const
//	{
//		return (size_t)x;
//	}
//};
//
//template<> struct hash< double >
//{
//	size_t operator()( const double& x ) const
//	{
//		return (size_t)x;
//	}
//};
//
//template<typename T> struct hash< T* >
//{
//	size_t operator()( T *x ) const
//	{
//		return (size_t)x;
//	}
//};
//}	// namespace __gnu_cxx
/* END FIX */
//#endif	// __cplusplus
//#endif	// !NNN_PLATFORM_WIN32 && !NNN_PLATFORM_WP8
//=====Added by FPE(2011.12.8)=====(修正 Linux/Unix 下 hash_map 不能用 string 做 key 的 bug)(End)

#define ThrowIfFailed(hr)	if(FAILED(hr))	throw std::exception();

namespace NNN
{

struct s_Range16
{
	USHORT	m_min	= 0;
	USHORT	m_max	= 0;
};

struct s_Range32
{
	UINT	m_min	= 0;
	UINT	m_max	= 0;
};

struct s_Range64
{
	UINT64	m_min	= 0;
	UINT64	m_max	= 0;
};

struct s_Value
{
	union
	{
		bool				m_bool;
		char				m_char;
		unsigned char		m_byte;
		short				m_short;
		unsigned short		m_ushort;
		int					m_int;
		unsigned int		m_uint;
		long				m_long;
		unsigned long		m_ulong;
		__int64				m_int64;
		unsigned __int64	m_uint64	= 0;
		float				m_float;
		double				m_double;
		void				*m_pointer;
	};
};

}	// namespace NNN

#endif	/* _NNN___COMMON___COMMON_INL_H_ */
