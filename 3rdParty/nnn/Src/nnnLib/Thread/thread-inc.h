//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 线程相关（包含文件）
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNLIB___THREAD___THREAD_INC_H_
#define _NNNLIB___THREAD___THREAD_INC_H_

#if defined(_M_X64) || defined(__x86_64__) || defined(_M_IX86) || defined(__i386__)
#include <immintrin.h>	// _mm_pause
#elif defined(_M_ARM64) || defined(__aarch64__) || defined(_M_ARM) || defined(__arm__)
#if defined(_MSC_VER)
#include <intrin.h>		// MSVC 提供的 __yield()
#endif
#else
#include <thread>
#endif

namespace NNN
{
namespace Thread
{

// 在不同架构/编译器下，提供 CPU 友好的 busy-wait 提示
inline void cpu_relax()
{
#if defined(_M_X64) || defined(__x86_64__) || defined(_M_IX86) || defined(__i386__)
	// x86 / x64
	_mm_pause();
#elif defined(_M_ARM64) || defined(__aarch64__) || defined(_M_ARM) || defined(__arm__)
	// ARM / ARM64
	#if defined(_MSC_VER)
		__yield();
	#elif defined(__clang__) || defined(__GNUC__)
		// GCC/Clang 提供 __builtin_arm_yield / __builtin_aarch64_yield
		#if defined(__aarch64__)
			__builtin_aarch64_yield();
		#else
			__builtin_arm_yield();
		#endif
	#else
		// 不识别的编译器，退化到 std::this_thread::yield()
		std::this_thread::yield();
	#endif
#else
	// 其它未知架构，直接交给调度器
	std::this_thread::yield();
#endif
}

}	// namespace Thread
}	// namespace NNN

#endif	// _NNNLIB___THREAD___THREAD_INC_H_
