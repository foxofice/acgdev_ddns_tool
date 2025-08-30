//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : �߳���أ������ļ���
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNLIB___THREAD___THREAD_INC_H_
#define _NNNLIB___THREAD___THREAD_INC_H_

#if defined(_M_X64) || defined(__x86_64__) || defined(_M_IX86) || defined(__i386__)
#include <immintrin.h>	// _mm_pause
#elif defined(_M_ARM64) || defined(__aarch64__) || defined(_M_ARM) || defined(__arm__)
#if defined(_MSC_VER)
#include <intrin.h>		// MSVC �ṩ�� __yield()
#endif
#else
#include <thread>
#endif

namespace NNN
{
namespace Thread
{

// �ڲ�ͬ�ܹ�/�������£��ṩ CPU �Ѻõ� busy-wait ��ʾ
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
		// GCC/Clang �ṩ __builtin_arm_yield / __builtin_aarch64_yield
		#if defined(__aarch64__)
			__builtin_aarch64_yield();
		#else
			__builtin_arm_yield();
		#endif
	#else
		// ��ʶ��ı��������˻��� std::this_thread::yield()
		std::this_thread::yield();
	#endif
#else
	// ����δ֪�ܹ���ֱ�ӽ���������
	std::this_thread::yield();
#endif
}

}	// namespace Thread
}	// namespace NNN

#endif	// _NNNLIB___THREAD___THREAD_INC_H_
