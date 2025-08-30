//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 通用包含文件（宏）
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNN___COMMON___COMMON_MACRO_H_
#define _NNN___COMMON___COMMON_MACRO_H_

#ifndef WIN32_LEAN_AND_MEAN
#define WIN32_LEAN_AND_MEAN
#endif	// WIN32_LEAN_AND_MEAN

#include "NNN_API.h"	// 导入/导出
#include "platform.h"	// 平台相关

/* error reporting helpers */
#ifndef __STR2WSTR
#define __STR2WSTR(str)    L##str
#endif	// __STR2WSTR
#ifndef _STR2WSTR
#define _STR2WSTR(str)     __STR2WSTR(str)
#endif	// _STR2WSTR

#ifndef _STRINGIZEX
#define _STRINGIZEX(x) #x
#endif	// _STRINGIZEX
#ifndef _STRINGIZE
#define _STRINGIZE(x) _STRINGIZEX(x)
#endif	// _STRINGIZE

#if _MSC_VER >= 1700
	#define _ALLOW_KEYWORD_MACROS
#endif	// _MSC_VER

#ifdef _WIN32
	#ifndef __cplusplus
		#define inline __inline
	#endif	// !__cplusplus
#endif	// _WIN32

#ifdef _WIN32
#ifndef _WIN32_WINNT
	#define _WIN32_WINNT 0x0601	// http://msdn.microsoft.com/zh-cn/library/6sehtctf.ASPX
#endif	// _WIN32_WINNT
#endif	// _WIN32

// gcc 所需要的一些宏定义
#ifndef _WIN32
	#define __int64	long long
	#define __int32	int
	#define __int16	short
	#define __int8	char

	#ifndef __declspec
	#define __declspec(x)
	#endif

	#ifndef _In_
	#define _In_
	#endif

	#ifndef _Out_
	#define _Out_
	#endif

	#ifndef _Out_opt_
	#define _Out_opt_
	#endif

	#ifndef _Success_
	#define _Success_(x)
	#endif

	#ifndef __static_assert
	#define __static_assert(constant_expression, string_literal)
	#endif

	#ifndef _In_reads_
	#define _In_reads_(x)
	#endif

	#ifndef _Out_writes_
	#define _Out_writes_(x)
	#endif

	#ifndef _Out_writes_bytes_
	#define _Out_writes_bytes_(x)
	#endif

	#ifndef _In_reads_bytes_
	#define _In_reads_bytes_(x)
	#endif

	#ifndef _Use_decl_annotations_
	#define _Use_decl_annotations_
	#endif

	#ifndef _Analysis_assume_
	#define _Analysis_assume_(x)
	#endif
#endif	// !_WIN32

#define NOMINMAX	//取消 min/max 宏

#ifdef _WIN32
	#include <assert.h>
#endif	// _WIN32

// 普通宏定义
#ifndef SAFE_DELETE
	#define SAFE_DELETE(p)			{ if((p)!=nullptr) { delete (p);		(p)=nullptr; } }
#endif
#ifndef SAFE_DELETE_ARRAY
	#define SAFE_DELETE_ARRAY(p)	{ if((p)!=nullptr) { delete[] (p);		(p)=nullptr; } }
#endif
#ifndef SAFE_RELEASE
	#define SAFE_RELEASE(p)			{ if((p)!=nullptr) { (p)->Release();	(p)=nullptr; } }
#endif
#ifndef SAFE_SYSFREESTRING
	#define SAFE_SYSFREESTRING(p)	{ if((p)!=nullptr) { SysFreeString(p);	(p)=nullptr; } }
#endif
#ifndef SAFE_CLOSE_HANDLE
	#define SAFE_CLOSE_HANDLE(p)	{ if((p)!=nullptr) { CloseHandle(p);	(p)=nullptr; } }
#endif

#ifdef DXUT_AUTOLIB
	#ifndef V
		#define V(x)           { hr = (x); if( FAILED(hr) ) { NNN::DXUTTrace( __FILE__, (DWORD)__LINE__, hr, L#x, true ); } }
	#endif
	#ifndef V_RETURN
		#define V_RETURN(x)    { hr = (x); if( FAILED(hr) ) { return NNN::DXUTTrace( __FILE__, (DWORD)__LINE__, hr, L#x, true ); } }
	#endif
#else
	#ifndef V
		#define V(x)           { hr = (x); assert(SUCCEEDED(hr)); }
	#endif
	#ifndef V_RETURN
		#define V_RETURN(x)    { hr = (x); assert(SUCCEEDED(hr)); if( FAILED(hr) ) { return hr; } }
	#endif
#endif	// DXUT_AUTOLIB

#undef min	// use __min instead
#undef max	// use __max instead

#if !defined(MARKUP_SIZEOFWCHAR)
	#ifdef _WIN32
		#define MARKUP_SIZEOFWCHAR 2
	#else
		#if __SIZEOF_WCHAR_T__ == 4 || __WCHAR_MAX__ > 0x10000
			#define MARKUP_SIZEOFWCHAR 4
		#else
			#define MARKUP_SIZEOFWCHAR 2
		#endif
	#endif	// _WIN32
#endif

#include <algorithm>

//#undef __max
//#undef __min
//#define __max	std::max
//#define __min	std::min

// Caps values to min/max（请使用 std::clamp 代替）
//#define cap_value(a, min, max) ((a > max) ? max : (a < min) ? min : a)

//#ifndef __swap
//	//#define __swap(a,b) (((a) == (b)) || (((a) ^= (b)), ((b) ^= (a)), ((a) ^= (b))))
//	#define __swap(a, b) { decltype(a) c = a; a = b; b = c; }
//#endif

// Make sure va_copy exists
#ifndef NNN_ANDROID
#include <stdarg.h> // va_list, va_copy(?)
#endif	// !NNN_ANDROID

#ifndef va_copy
	#ifdef __va_copy
		#define va_copy __va_copy
	#else
		#define va_copy(dst, src) ((void) memcpy(&(dst), &(src), sizeof(va_list)))
	#endif	// __va_copy
#endif	// !va_copy

// 计算 RECT 的宽/高
#define RECT_WIDTH(rc)	((rc).right - (rc).left)
#define RECT_HEIGHT(rc)	((rc).bottom - (rc).top)

// hash_map/hash_set
#ifdef __cplusplus
	#include <unordered_map>
	#include <unordered_set>

	using namespace std::string_view_literals;	// 引入 sv 后缀

#if _HAS_CXX20
	struct s_StringHashA
	{
		using is_transparent = void;	// 启用透明哈希

		size_t operator()(std::string_view sv) const
		{
			return std::hash<std::string_view>{}(sv);
		}

		size_t operator()(const std::string& s) const
		{
			return std::hash<std::string>{}(s);
		}

		size_t operator()(const char *s) const
		{
			return std::hash<std::string_view>{}(s);
		}
	};

	struct s_StringHashW
	{
		using is_transparent = void;	// 启用透明哈希

		size_t operator()(std::wstring_view sv) const
		{
			return std::hash<std::wstring_view>{}(sv);
		}

		size_t operator()(const std::wstring& s) const
		{
			return std::hash<std::wstring>{}(s);
		}

		size_t operator()(const wchar_t *s) const
		{
			return std::hash<std::wstring_view>{}(s);
		}
	};

	//========== HashMapSelector ==========(Start)
	template <typename Key, typename Value>
	struct HashMapSelector
	{
		using Type = std::unordered_map<Key, Value>;
	};

	// 特化 Key 是 std::string 的版本
	template <typename Value>
	struct HashMapSelector<std::string, Value>
	{
		using Type = std::unordered_map<std::string, Value, s_StringHashA, std::equal_to<>>;
	};

	// 特化 Key 是 std::wstring 的版本
	template <typename Value>
	struct HashMapSelector<std::wstring, Value>
	{
		using Type = std::unordered_map<std::wstring, Value, s_StringHashW, std::equal_to<>>;
	};
	//========== HashMapSelector ==========(End)
	//========== HashSetSelector ==========(Start)
	template <typename Key>
	struct HashSetSelector
	{
		using Type = std::unordered_set<Key>;
	};

	// 特化 std::string
	template <>
	struct HashSetSelector<std::string>
	{
		using Type = std::unordered_set<std::string, s_StringHashA, std::equal_to<>>;
	};

	// 特化 std::wstring
	template <>
	struct HashSetSelector<std::wstring>
	{
		using Type = std::unordered_set<std::wstring, s_StringHashW, std::equal_to<>>;
	};
	//========== HashSetSelector ==========(End)

	// 定义类型别名
	template <typename Key, typename Value>
	using NNN_HASH_MAP = typename HashMapSelector<Key, Value>::Type;

	template <typename Key>
	using NNN_HASH_SET = typename HashSetSelector<Key>::Type;
#else
	#define NNN_HASH_MAP	std::unordered_map
	#define NNN_HASH_SET	std::unordered_set
#endif	// _HAS_CXX20
#endif	// __cplusplus

#ifdef NNN_ANDROID
	#ifndef S_IREAD
	#define S_IREAD		S_IRUSR
	#endif	// S_IREAD

	#ifndef S_IWRITE
	#define S_IWRITE	S_IWUSR
	#endif	// S_IWRITE

	#ifndef S_IEXEC
	#define S_IEXEC		S_IXUSR
	#endif	// S_IEXEC
#endif	// NNN_ANDROID

// 禁止使用拷贝构造函数和 operator= 赋值操作的宏
#define DISALLOW_COPY_AND_ASSIGN(TypeName) \
			TypeName(const TypeName&)		= delete;	\
			void operator=(const TypeName&)	= delete

// 成员访问宏
#define SET_MEMBER(type, member)		inline void	Set##member(type t)	{ m_##member = t; }
#define GET_MEMBER(type, member)		inline type	Get##member()		{ return m_##member; }
#define SET_GET_MEMBER(type, member)	SET_MEMBER(type, member)		GET_MEMBER(type, member)

#define SETP_MEMBER(type, member)		inline void		Set##member(type* t){ m_##member = *t; }
#define GETP_MEMBER(type, member)		inline type*	Get##member()		{ return &m_##member; }
#define SETP_GETP_MEMBER(type, member)	SETP_MEMBER(type, member)			GETP_MEMBER(type, member)

// 字节对齐
#ifdef _WIN32
	#define NNN_ALIGN(n)	__declspec(align(n))
#else
	#define NNN_ALIGN(n)	__attribute__((aligned(n)))
#endif	// _WIN32

//========== 打印/写入错误日志 ==========(Start)
#ifdef NNN_WINDOWS
	#define	NNN_PRINT_LOG(txt)				OutputDebugStringA(txt)
	#define	NNN_WRITE_LOG(filename, txt)	NNN::Log::AppendLog(filename, txt)
#elif defined(NNN_ANDROID)
	#define	NNN_PRINT_LOG(txt)				NNN_ANDROID_LOG_DEBUG(txt)
	#define	NNN_WRITE_LOG(filename, txt)
#elif defined(NNN_IOS)
	#define	NNN_PRINT_LOG(txt)				printf("%s", txt)
	#define	NNN_WRITE_LOG(filename, txt)
#elif defined(NNN_LINUX) || defined(NNN_MAC)
	#define	NNN_PRINT_LOG(txt)				printf("%s", txt)
	#define	NNN_WRITE_LOG(filename, txt)	NNN::Log::AppendLog(filename, txt)
#else
	#define	NNN_PRINT_LOG(txt)
	#define	NNN_WRITE_LOG(filename, txt)
#endif

#ifdef _WIN32
	#define _SSCANF		sscanf_s
	#define _SWSCANF	swscanf_s

	//#ifndef snprintf
	//#define snprintf	_snprintf
	//#endif
#else
	#define _SSCANF		sscanf
	#define _SWSCANF	swscanf
#endif	// _WIN32

#define	NNN_LOG_ERROR_FUNC(filename, format, ...)	\
		{	\
			char err_log_[4096];	\
			C::sprintf(err_log_, _countof(err_log_), "[Warning] %s() line:%d - " format, __FUNCTION__, __LINE__, ##__VA_ARGS__);	\
			\
			NNN_WRITE_LOG(filename, err_log_);	\
			NNN_PRINT_LOG(err_log_);			\
		}

#define NNN_PRINT_ERROR_FUNC(format, ...)	\
		{	\
			char err_log_[4096];	\
			C::sprintf(err_log_, countof(err_log_), "[Warning] %s() line:%d - " format, __FUNCTION__, __LINE__, ##__VA_ARGS__);	\
			\
			NNN_PRINT_LOG(err_log_);	\
		}
//========== 打印/写入错误日志 ==========(End)

// 内存泄漏检测
#include "../nnnLeakDetect/nnnLeakDetect.h"

#ifdef __cplusplus

#ifdef NNN_ANDROID
#include <thread>
#endif	// NNN_ANDROID

#ifdef _WIN32
#include <Windows.h>
#endif	// _WIN32

#endif	// __cplusplus

#define NNN_PARAMS(...)	__VA_ARGS__

#endif	/* _NNN___COMMON___COMMON_MACRO_H_ */
