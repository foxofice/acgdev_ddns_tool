//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 导入/导出
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNN___COMMON___NNN_API_H_
#define _NNN___COMMON___NNN_API_H_

// NNN_IMPORT
#if defined(WIN32) || defined(_WIN32)
	#define NNN_IMPORT	__declspec(dllimport)
#else
	// gcc
	#define NNN_IMPORT	__attribute__ ((visibility("default")))
#endif	// WIN32 || _WIN32

// NNN_EXPORT
#if defined(WIN32) || defined(_WIN32)
	#define NNN_EXPORT	__declspec(dllexport)
#else
	// gcc
	#define NNN_EXPORT	__attribute__ ((visibility("default")))
#endif	// WIN32 || _WIN32

// NNN_DLLLOCAL
#if defined(WIN32) || defined(_WIN32)
	#define NNN_DLLLOCAL
#else
	#define NNN_DLLLOCAL	__attribute__ ((visibility("hidden")))
#endif	// WIN32 || _WIN32

#ifndef NNN_API
	#ifdef NNN_EXPORTS
		#define NNN_API NNN_EXPORT
	#else
		#define NNN_API NNN_IMPORT
	#endif	// NNN_EXPORTS
#endif	// !NNN_API

#endif	// _NNN___COMMON___NNN_API_H_
