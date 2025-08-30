//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 系统/CPU架构/编译器相关
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNN___COMMON___PLATFORM_H_
#define _NNN___COMMON___PLATFORM_H_

#undef NNN_WINDOWS			// Windows 系统
#undef NNN_LINUX			// Linux 系统
#undef NNN_ANDROID			// Android 系统
#undef NNN_IOS				// iOS 系统
#undef NNN_MAC				// mac 系统

#undef NNN_X86_32			// x86 架构
#undef NNN_X86_64			// x64 架构
#undef NNN_X86				// x86/x64 架构

#undef NNN_ARM_32			// ARM(32bit) 架构
#undef NNN_ARM_64			// ARM(64bit) 架构
#undef NNN_ARM				// ARM/ARM64 架构

#undef NNN_MSVC				// MSVC 编译器
#undef NNN_GCC				// GCC 编译器
#undef NNN_CLANG			// Clang 编译器
#undef NNN_INTEL_CPP		// Intel 编译器

//==================================================

#undef NNN_PLATFORM_PC		// PC 平台
#undef NNN_PLATFORM_MOBILE	// 手机平台

#undef NNN_PLATFORM_NAME	// 平台名称
#undef NNN_ARCH_NAME		// 架构名称
#undef NNN_COMPILER_NAME	// 编译器名称

#undef NNN_PLATFORM_DIR		// 平台目录（x64/Win32/ARM/...）
#undef NNN_CONFIG_DIR		// 配置目录（Debug/Release/Debug.Linux/Release.Linux/...）

//==================================================

#ifdef _WIN32
#define NNN_WINDOWS	1
#endif	// _WIN32

#if defined(__linux__) && !defined(__ANDROID__)
#define NNN_LINUX 1
#endif	// __linux__ && !__ANDROID__

#ifdef __ANDROID__
#define NNN_ANDROID	1
#endif	// ANDROID || __ANDROID__

#ifdef __APPLE__
	#include <TargetConditionals.h>
	#if TARGET_OS_IPHONE
		#define NNN_IOS	1
	#elif TARGET_OS_MAC
		#define NNN_MAC	1
	#endif	// TARGET_OS_IPHONE
#endif	// __APPLE__

#if defined(_M_X64) || defined(__x86_64__)
#define NNN_X86_64	1
#elif defined(_M_IX86) || defined(__i386__)
#define NNN_X86_32	1
#endif

#if defined(NNN_X86_64) || defined(NNN_X86_32)
#define NNN_X86		1
#endif	// NNN_X86_64 || NNN_X86_32

#if defined(_M_ARM64) || defined(__aarch64__)
#define NNN_ARM_64	1
#elif defined(_M_ARM) || defined(__arm__)
#define NNN_ARM_32	1
#endif

#if defined(NNN_ARM_64) || defined(NNN_ARM_32)
#define NNN_ARM		1
#endif	// NNN_ARM_64 || NNN_ARM_32

#ifdef _MSC_VER
#define NNN_MSVC	1
#endif	// _MSC_VER

#ifdef __clang__
	#define NNN_CLANG	1
#elif defined(__GNUC__)
	#define NNN_GCC		1
#endif	// __clang__

#ifdef __INTEL_COMPILER
#define NNN_COMPILER_NAME	1
#endif	// __INTEL_COMPILER

#if defined(NNN_WINDOWS)
	#define NNN_PLATFORM_NAME	"Windows"
	#define NNN_PLATFORM_PC		1
#elif defined(NNN_LINUX)
	#define NNN_PLATFORM_NAME	"Linux"
	#define NNN_PLATFORM_PC		1
#elif defined(NNN_ANDROID)
	#define NNN_PLATFORM_NAME	"Android"
	#define NNN_PLATFORM_MOBILE	1
#elif defined(NNN_IOS)
	#define NNN_PLATFORM_NAME	"iOS"
	#define NNN_PLATFORM_MOBILE	1
#elif defined(NNN_MAC)
	#define NNN_PLATFORM_NAME	"macOS"
	#define NNN_PLATFORM_PC		1
#else
	#define NNN_PLATFORM_NAME	"Unknown_Platform"
#endif

#if defined(NNN_X86_64)
	#define NNN_ARCH_NAME	"x86_64"
#elif defined(NNN_X86_32)
	#define NNN_ARCH_NAME	"x86_32"
#elif defined(NNN_ARM_64)
	#define NNN_ARCH_NAME	"ARM64"
#elif defined(NNN_ARM_32)
	#define NNN_ARCH_NAME	"ARM32"
#else
	#define NNN_ARCH_NAME	"Unknown_Arch"
#endif

#if defined(NNN_MSVC)
	#define NNN_COMPILER_NAME	"MSVC"
#elif defined(NNN_GCC)
	#define NNN_COMPILER_NAME	"GCC"
#elif defined(NNN_CLANG)
	#define NNN_COMPILER_NAME	"Clang"
#elif defined(NNN_COMPILER_NAME)
	#define NNN_COMPILER_NAME	"Intel_Compiler"
#else
	#define NNN_COMPILER_NAME	"Unknown_Compiler"
#endif

#ifdef NNN_WINDOWS
	#ifdef NNN_X86_64
		#define NNN_PLATFORM_DIR	"x64"
	#elif defined(NNN_X86_32)
		#define NNN_PLATFORM_DIR	"Win32"
	#elif defined(NNN_ARM_64)
		#define NNN_PLATFORM_DIR	"ARM64"
	#elif defined(NNN_ARM_32)
		#define NNN_PLATFORM_DIR	"ARM"
	#endif
#elif defined(NNN_LINUX)
	#define NNN_PLATFORM_DIR		"Linux"
#elif defined(NNN_ANDROID)
	#ifdef NNN_X86
		#define NNN_PLATFORM_DIR	"x86"
	#else
		#define NNN_PLATFORM_DIR	"ARM"
	#endif
#elif defined(NNN_IOS)
	#define NNN_PLATFORM_DIR		"iOS"
#elif defined(NNN_MAC)
	#define NNN_PLATFORM_DIR		"Mac"
#endif

#ifdef NNN_WINDOWS
	#ifndef NDEBUG
		#define NNN_CONFIG_DIR	"Debug"
	#else
		#define NNN_CONFIG_DIR	"Release"
	#endif
#elif defined(NNN_LINUX)
	#ifndef NDEBUG
		#define NNN_CONFIG_DIR	"Debug"
	#else
		#define NNN_CONFIG_DIR	"Release"
	#endif
#elif defined(NNN_ANDROID)
	#ifndef NDEBUG
		#define NNN_CONFIG_DIR	"Debug_android"
	#else
		#define NNN_CONFIG_DIR	"Release_android"
	#endif
#elif defined(NNN_IOS)
	#ifndef NDEBUG
		#if TARGET_IPHONE_SIMULATOR
			#define NNN_CONFIG_DIR	"Debug/-iphonesimulator"
		#else
			#define NNN_CONFIG_DIR	"Debug/-iphoneos"
		#endif
	#else
		#if TARGET_IPHONE_SIMULATOR
			#define NNN_CONFIG_DIR	"Release/-iphonesimulator"
		#else
			#define NNN_CONFIG_DIR	"Release/-iphoneos"
		#endif
	#endif
#elif defined(NNN_MAC)
	#ifndef NDEBUG
		#define NNN_CONFIG_DIR	"Debug"
	#else
		#define NNN_CONFIG_DIR	"Release"
	#endif
#endif

#endif	// _NNN___COMMON___PLATFORM_H_
