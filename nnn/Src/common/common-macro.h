//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 通用包含文件（宏）
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNN___COMMON___COMMON_MACRO_H_
#define _NNN___COMMON___COMMON_MACRO_H_

// 平台定义
#define NNN_PLATFORM_WIN32		1
#define NNN_PLATFORM_ANDROID	2
#define NNN_PLATFORM_IOS		3
#define NNN_PLATFORM_WP8		4
#define NNN_PLATFORM_LINUX		5
#define NNN_PLATFORM_MAC		6

// 自动推断系统
#ifndef NNN_PLATFORM
	#if defined(WIN32) || defined(_WIN32)
		#include <winapifamily.h>
		#if (WINAPI_FAMILY == WINAPI_FAMILY_DESKTOP_APP)
			#define NNN_PLATFORM	NNN_PLATFORM_WIN32
		#elif (WINAPI_FAMILY == WINAPI_FAMILY_PHONE_APP)
			#define NNN_PLATFORM	NNN_PLATFORM_WP8
		#endif	// WINAPI_FAMILY
	#elif defined(ANDROID) || defined(__ANDROID__)
		#define NNN_PLATFORM	NNN_PLATFORM_ANDROID
	#elif defined(__APPLE__) && (defined(__IPHONE__) || defined(__IPAD__) || defined(__IOS__))
		#define NNN_PLATFORM	NNN_PLATFORM_IOS
	#elif defined(__GNU__) && defined(__unix__) && !defined(__APPLE__)
		#define NNN_PLATFORM	NNN_PLATFORM_LINUX
	#elif defined(__APPLE__)
		#define NNN_PLATFORM	NNN_PLATFORM_MAC
	#endif
#endif	// NNN_PLATFORM

#if (NNN_PLATFORM != NNN_PLATFORM_WIN32) && (NNN_PLATFORM != NNN_PLATFORM_WP8)
	#ifndef __GCC__
	#define __GCC__
	#endif
#endif	// !NNN_PLATFORM_WIN32 && !NNN_PLATFORM_WP8

// 自动加入相关宏定义
#if (NNN_PLATFORM == NNN_PLATFORM_ANDROID)
	#ifndef ANDROID
	#define ANDROID
	#endif

	#ifndef __ANDROID__
	#define __ANDROID__
	#endif
#elif (NNN_PLATFORM == NNN_PLATFORM_IOS)
	#ifndef __IOS__
	#define __IOS__
	#endif
#endif	// NNN_PLATFORM

#if (NNN_PLATFORM == NNN_PLATFORM_IOS)
#include <TargetConditionals.h>
#endif	// NNN_PLATFORM_IOS

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

//========== 导入/导出 ==========(Start)
// NNN_IMPORT
#if (NNN_PLATFORM == NNN_PLATFORM_WIN32) || (NNN_PLATFORM == NNN_PLATFORM_WP8)
	#if (NNN_PLATFORM == NNN_PLATFORM_WIN32)
		#define NNN_IMPORT __declspec(dllimport)
	#else	// NNN_PLATFORM_WP8
		#define NNN_IMPORT
	#endif	// NNN_PLATFORM_WIN32
#else
	// gcc
	#define NNN_IMPORT	__attribute__ ((visibility("default")))
#endif	// NNN_PLATFORM_WIN32 || NNN_PLATFORM_WP8

// NNN_EXPORT
#if (NNN_PLATFORM == NNN_PLATFORM_WIN32) || (NNN_PLATFORM == NNN_PLATFORM_WP8)
	#define NNN_EXPORT __declspec(dllexport)
#else
	// gcc
	#define NNN_EXPORT	__attribute__ ((visibility("default")))
#endif	// NNN_PLATFORM_WIN32 || NNN_PLATFORM_WP8

// NNN_DLLLOCAL
#if (NNN_PLATFORM == NNN_PLATFORM_WIN32) || (NNN_PLATFORM == NNN_PLATFORM_WP8)
	#define NNN_DLLLOCAL
#else
	#define NNN_DLLLOCAL	__attribute__ ((visibility("hidden")))
#endif	// NNN_PLATFORM_WIN32 || NNN_PLATFORM_WP8

#ifndef NNN_API
	#ifdef NNN_EXPORTS
		#define NNN_API NNN_EXPORT
	#else
		#define NNN_API NNN_IMPORT
	#endif	// NNN_EXPORTS
#endif	// !NNN_API
//========== 导入/导出 ==========(End)

//（平台目录 & 配置目录）
#if (NNN_PLATFORM == NNN_PLATFORM_WIN32) || (NNN_PLATFORM == NNN_PLATFORM_WP8)
	#if defined(WIN64) || defined(_WIN64)
		#define PLATFORM_DIR "x64"
	#elif defined(ARM)
		#define PLATFORM_DIR "ARM"
	#else
		#define PLATFORM_DIR "Win32"
	#endif	// WIN64 || _WIN64
#elif (NNN_PLATFORM == NNN_PLATFORM_ANDROID)
	#if defined(DEBUG) || defined(_DEBUG)
		#define CONFIGURATION_DIR "Debug_android"
	#else
		#define CONFIGURATION_DIR "Release_android"
	#endif

	#ifdef X86
		#define PLATFORM_DIR "x86"
	#else
		#define PLATFORM_DIR "ARM"
	#endif	// X86
#elif (NNN_PLATFORM == NNN_PLATFORM_IOS)
	#define PLATFORM_DIR "iOS"
#elif (NNN_PLATFORM == NNN_PLATFORM_LINUX)
	#define PLATFORM_DIR "Linux"
#elif (NNN_PLATFORM == NNN_PLATFORM_MAC)
	#define PLATFORM_DIR "Mac"
#endif	// NNN_PLATFORM

#if (NNN_PLATFORM == NNN_PLATFORM_WP8)
	#if defined(DEBUG) || defined(_DEBUG)
		#define CONFIGURATION_DIR "Debug_wp8"
	#else
		#define CONFIGURATION_DIR "Release_wp8"
	#endif
#elif (NNN_PLATFORM == NNN_PLATFORM_IOS)
	#if TARGET_IPHONE_SIMULATOR
		#define CONFIGURATION_DIR "Debug/-iphonesimulator"
	#else
		#define CONFIGURATION_DIR "Debug/-iphoneos"
	#endif	// TARGET_IPHONE_SIMULATOR
#elif (NNN_PLATFORM != NNN_PLATFORM_ANDROID)	// Android 在上面单独设置
	#if defined(DEBUG) || defined(_DEBUG)
		#define CONFIGURATION_DIR "Debug"
	#else
		#define CONFIGURATION_DIR "Release"
	#endif
#endif	// NNN_PLATFORM

#define PLATFORM_DIRW		_STR2WSTR(PLATFORM_DIR)
#define CONFIGURATION_DIRW	_STR2WSTR(CONFIGURATION_DIR)

#if _MSC_VER >= 1700
	#define _ALLOW_KEYWORD_MACROS
#endif	// _MSC_VER

#if (NNN_PLATFORM == NNN_PLATFORM_WIN32) || (NNN_PLATFORM == NNN_PLATFORM_WP8)
#ifndef __cplusplus
#define inline __inline
#endif	// !__cplusplus
#endif	// NNN_PLATFORM_WIN32 || NNN_PLATFORM_WP8

#if (NNN_PLATFORM == NNN_PLATFORM_WIN32)
#ifndef _WIN32_WINNT
	#define _WIN32_WINNT 0x0601	// http://msdn.microsoft.com/zh-cn/library/6sehtctf.ASPX
#endif	// _WIN32_WINNT
#endif	// NNN_PLATFORM_WIN32

// gcc 所需要的一些宏定义
#ifdef __GCC__

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

#endif	// __GCC__

#if defined(__GCC__) || _MSC_VER < 1700

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

#endif	// __GCC__ || _MSC_VER < 1700

#if (NNN_PLATFORM == NNN_PLATFORM_WIN32) || (NNN_PLATFORM == NNN_PLATFORM_WP8)
#include <assert.h>
#endif	// NNN_PLATFORM_WIN32 || NNN_PLATFORM_WP8

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

#if (NNN_PLATFORM == NNN_PLATFORM_WIN32) &&	(defined(DEBUG) || defined(_DEBUG))
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
#else
	#ifndef V
		#define V(x)           { hr = (x); }
	#endif
	#ifndef V_RETURN
		#define V_RETURN(x)    { hr = (x); if( FAILED(hr) ) { return hr; } }
	#endif
#endif

#if (NNN_PLATFORM == NNN_PLATFORM_WIN32) || (NNN_PLATFORM == NNN_PLATFORM_LINUX)
#ifndef NNN_NO_CONSOLE_THREAD_SAFE
	#include <stdio.h>
	#include <wchar.h>

	#undef printf
	#define printf(...)							\
			{									\
				NNN::Console::console_lock();	\
				printf(__VA_ARGS__);			\
				fflush(stdout);					\
				NNN::Console::console_unlock();	\
			}

	#undef wprintf
	#define wprintf(...)						\
			{									\
				NNN::Console::console_lock();	\
				wprintf(__VA_ARGS__);			\
				fflush(stdout);					\
				NNN::Console::console_unlock();	\
			}
#endif	// !NNN_NO_CONSOLE_THREAD_SAFE
#endif	// NNN_PLATFORM

#undef min	// use __min instead
#undef max	// use __max instead

#if !defined(MARKUP_SIZEOFWCHAR)
	#if __SIZEOF_WCHAR_T__ == 4 || __WCHAR_MAX__ > 0x10000
		#define MARKUP_SIZEOFWCHAR 4
	#else
		#define MARKUP_SIZEOFWCHAR 2
	#endif
#endif

#if (NNN_PLATFORM == NNN_PLATFORM_WIN32) || (NNN_PLATFORM == NNN_PLATFORM_WP8)
	#include <stdlib.h>
#else
	// Minimum and maximum macros
	#ifndef __max
		#define __max(a,b) (((a) > (b)) ? (a) : (b))
	#endif
	#ifndef __min
		#define __min(a,b) (((a) < (b)) ? (a) : (b))
	#endif
#endif	// NNN_PLATFORM

// Caps values to min/max
#define cap_value(a, min, max) ((a > max) ? max : (a < min) ? min : a)

#ifdef __cplusplus
template<class T>
inline T ts_max(T a, T b)
{
	T tmp_a = (a);
	T tmp_b = (b);

	return __max(tmp_a, tmp_b);
}

template<class T>
inline T ts_min(T a, T b)
{
	T tmp_a = (a);
	T tmp_b = (b);

	return __min(tmp_a, tmp_b);
}

template<class T>
inline T ts_cap_value(T a, T min, T max)
{
	T tmp_a		= (a);
	T tmp_min	= (min);
	T tmp_max	= (max);

	return cap_value(tmp_a, tmp_min, tmp_max);
}
#endif	// __cplusplus

//#ifndef __swap
//	//#define __swap(a,b) (((a) == (b)) || (((a) ^= (b)), ((b) ^= (a)), ((a) ^= (b))))
//	#define __swap(a, b) { decltype(a) c = a; a = b; b = c; }
//#endif

// Make sure va_copy exists
#if (NNN_PLATFORM != NNN_PLATFORM_ANDROID)
#include <stdarg.h> // va_list, va_copy(?)
#endif	// !NNN_PLATFORM_ANDROID
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
#if (NNN_PLATFORM == NNN_PLATFORM_WIN32) || (NNN_PLATFORM == NNN_PLATFORM_WP8)
	//#include <hash_map>
	//#include <hash_set>
	#include <unordered_map>
	#include <unordered_set>
	#define NNN_HASH_MAP	std::unordered_map	// 在 vs2015 中，unordered_map 比 hash_map 快 7~14%
	#define NNN_HASH_SET	std::unordered_set
#else
	//#include <ext/hash_map>
	//#include <ext/hash_set>
	//#define NNN_HASH_MAP	__gnu_cxx::hash_map	// 在 gcc 中，hash_map 比 unordered_map 快 183%~213%（200% 左右）
	//#define NNN_HASH_SET	__gnu_cxx::hash_set

	#include <unordered_map>
	#include <unordered_set>
	#define NNN_HASH_MAP std::unordered_map
	#define NNN_HASH_SET std::unordered_set
#endif	// NNN_PLATFORM
#endif	// __cplusplus

#if (NNN_PLATFORM == NNN_PLATFORM_ANDROID)
#ifndef S_IREAD
#define S_IREAD		S_IRUSR
#endif	// S_IREAD

#ifndef S_IWRITE
#define S_IWRITE	S_IWUSR
#endif	// S_IWRITE

#ifndef S_IEXEC
#define S_IEXEC		S_IXUSR
#endif	// S_IEXEC
#endif	// NNN_PLATFORM_ANDROID

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
#if (NNN_PLATFORM == NNN_PLATFORM_WIN32) || (NNN_PLATFORM == NNN_PLATFORM_WP8)
	#define NNN_ALIGN(n)	__declspec(align(n))
#else
	#define NNN_ALIGN(n)	__attribute__((aligned(n)))
#endif	// NNN_PLATFORM_WIN32 || NNN_PLATFORM_WP8

//========== 打印/写入错误日志 ==========(Start)
#if (NNN_PLATFORM == NNN_PLATFORM_WIN32) || (NNN_PLATFORM == NNN_PLATFORM_WP8)
	#define	NNN_PRINT_LOG(txt)				OutputDebugStringA(txt)
	#define	NNN_WRITE_LOG(filename, txt)	NNN::Log::AppendLog(filename, txt)
#elif (NNN_PLATFORM == NNN_PLATFORM_ANDROID)
	#define	NNN_PRINT_LOG(txt)				NNN_ANDROID_LOG_DEBUG(txt)
	#define	NNN_WRITE_LOG(filename, txt)
#elif (NNN_PLATFORM == NNN_PLATFORM_IOS)
	#define	NNN_PRINT_LOG(txt)				printf("%s", txt)
	#define	NNN_WRITE_LOG(filename, txt)
#elif (NNN_PLATFORM == NNN_PLATFORM_LINUX) || (NNN_PLATFORM == NNN_PLATFORM_MAC)
	#define	NNN_PRINT_LOG(txt)				printf("%s", txt)
	#define	NNN_WRITE_LOG(filename, txt)	NNN::Log::AppendLog(filename, txt)
#else
	#define	NNN_PRINT_LOG(txt)
	#define	NNN_WRITE_LOG(filename, txt)
#endif	// NNN_PLATFORM

#if (NNN_PLATFORM == NNN_PLATFORM_WIN32) || (NNN_PLATFORM == NNN_PLATFORM_WP8)
	#define _STRCPY		strcpy_s
	#define _WCSCPY		wcscpy_s
	#define _STRCAT		strcat_s
	#define _WCSCAT		wcscat_s
	#define _SPRINTF	sprintf_s
	#define _SWPRINTF	swprintf_s
	#define _SSCANF		sscanf_s
	#define _SWSCANF	swscanf_s
	#define	_VSNPRINTF	vsnprintf_s
	#define	_VSNWPRINTF	_vsnwprintf_s

	#define	_MEMMOVE(dst, src, size)	memmove_s(dst, size, src, size)
#else
	#define _STRCPY		strcpy
	#define _WCSCPY		wcscpy
	#define _STRCAT		strcat
	#define _WCSCAT		wcscat
	#define _SPRINTF	sprintf
	#define _SWPRINTF	swprintf
	#define _SSCANF		sscanf
	#define _SWSCANF	swscanf
	#define	_VSNPRINTF	vsnprintf
	#define	_VSNWPRINTF	vsnwprintf

	#define	_MEMMOVE(dst, src, size)	memmove(dst, src, size)

	#ifndef _wcsicmp
	#define _wcsicmp wcscasecmp
	#endif

	#ifndef stricmp
	#define stricmp strcasecmp
	#endif
#endif	// NNN_PLATFORM_WIN32 || NNN_PLATFORM_WP8

#define	NNN_LOG_ERROR_FUNC(filename, format, ...)	\
		{	\
			char err_log_[4096];	\
			_SPRINTF(err_log_, "[Warning] %s() line:%d - " format, __FUNCTION__, __LINE__, ##__VA_ARGS__);	\
			\
			NNN_WRITE_LOG(filename, err_log_);	\
			NNN_PRINT_LOG(err_log_);			\
		}

#define NNN_PRINT_ERROR_FUNC(format, ...)	\
		{	\
			char err_log_[4096];	\
			_SPRINTF(err_log_, "[Warning] %s() line:%d - " format, __FUNCTION__, __LINE__, ##__VA_ARGS__);	\
			\
			NNN_PRINT_LOG(err_log_);	\
		}
//========== 打印/写入错误日志 ==========(End)

// 重定义 new
#ifdef __cplusplus
	#if defined(DEBUG) || defined(_DEBUG)
		#if (NNN_PLATFORM == NNN_PLATFORM_WIN32) || (NNN_PLATFORM == NNN_PLATFORM_WP8)
			#define new new(_CLIENT_BLOCK, __FILE__, __LINE__)

			#define _CRTDBG_MAP_ALLOC
			#include <crtdbg.h>
		#endif	// NNN_PLATFORM_WIN32 || NNN_PLATFORM_WP8
	#else
		// 修正 Linux 的 STL 的 new 重载版本跟这里的 new 重载冲突
		#if (NNN_PLATFORM == NNN_PLATFORM_LINUX)
			#include <bits/stl_construct.h>
			#include <bits/stl_tree.h>
			#include <shared_mutex>
		#endif	// NNN_PLATFORM_LINUX

		#include <new>
		#define new new(std::nothrow)
	#endif	// DEBUG || _DEBUG
#endif	// __cplusplus

#ifdef __cplusplus
#if (NNN_PLATFORM == NNN_PLATFORM_ANDROID)
	#include <thread>
#endif	// NNN_PLATFORM_ANDROID

#if (NNN_PLATFORM == NNN_PLATFORM_WIN32) || (NNN_PLATFORM == NNN_PLATFORM_WP8)
#include <WinSock2.h>
#include <Windows.h>
#endif	// NNN_PLATFORM_WIN32
#endif	// __cplusplus

#define NNN_PARAMS(...)	__VA_ARGS__

#endif	/* _NNN___COMMON___COMMON_MACRO_H_ */
