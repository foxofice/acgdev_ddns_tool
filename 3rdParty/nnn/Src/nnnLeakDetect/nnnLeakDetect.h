//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 内存泄漏检测
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNN___NNNLEAKDETECT___NNNLEAKDETECT_H_
#define _NNN___NNNLEAKDETECT___NNNLEAKDETECT_H_

/*
使用方法：
	【针对运行库】包含此头文件 nnnLeakDetect.h（Win 下会自动包含 nnnLeakDetect.lib，库路径正确即可）
	【针对执行档】包含此头文件 nnnLeakDetect.h + 调用 MemoryLeakCheck() 开启内存泄漏检测
	【对于 Win 平台】如果要恢复到 CRT 调试，则先定义 NNN_USE_CRT_DEBUG
	【关于 Release】默认关闭。要开启的话，在包含头文件之前先定义 NNN_ENABLE_MEMORY_LEAK_DETECT
	【关于 c++/clr】如果发现无法抓取内存泄漏的话，可以在 imports.cpp 里引入其他 lib 之前先引入 nnnLeakDetect.lib

	注意：
		Win 的 CRT 可以抓到 new/delete、malloc/free
		NNN_Leak_Detect 的自研实现只抓取 new/delete
*/

#ifndef NDEBUG
#define NNN_ENABLE_MEMORY_LEAK_DETECT
#endif	// !NDEBUG

#include "../common/NNN_API.h"

#ifdef NNN_ENABLE_MEMORY_LEAK_DETECT

#ifdef _WIN32
#ifdef NNN_USE_CRT_DEBUG
	#define new new(_CLIENT_BLOCK, __FILE__, __LINE__)

	#define _CRTDBG_MAP_ALLOC
	#include <crtdbg.h>
#else
	#ifndef NNN_NOT_AUTO_IMPORT_LEAK_DETECT_LIB
		#pragma comment(lib, "nnnLeakDetect.lib")
	#endif	// NNN_LEAK_DETECT_LIB_SELF
#endif	// NNN_USE_CRT_DEBUG
#endif	// _WIN32

#endif	// NNN_ENABLE_MEMORY_LEAK_DETECT

namespace NNN
{
namespace Leak_Detect
{

#ifdef _WIN32
// 设置内存泄漏检测参数（skip_self_Frames = true 时，表示跳过 nnnLeakDetect 内部以防止伪泄漏）
NNN_API	void	Set_Leak_Detect(bool skip_self_Frames);
#else
// 栈采集
NNN_API void	capture_stacks(bool v);
#endif	// _WIN32

// 开启/关闭内存泄漏检测
NNN_API void	enable();
NNN_API void	disable();

// 立即报告
NNN_API void	Report_Now();

struct s_MemoryLeakCheck_Param
{
	bool	m_skip_self_Frames	= true;
	int		m_CRT_Debug_flag	=
#ifdef NNN_USE_CRT_DEBUG
		_CRTDBG_ALLOC_MEM_DF | _CRTDBG_LEAK_CHECK_DF;
#else
		0;
#endif	// NNN_USE_CRT_DEBUG
	bool	m_capture_stacks	= true;
};

// 开启内存泄漏检测
inline void	MemoryLeakCheck(const struct s_MemoryLeakCheck_Param *param = nullptr)
{
#ifdef _WIN32
	(param);
#endif	// _WIN32

#ifdef NNN_ENABLE_MEMORY_LEAK_DETECT

#ifdef _WIN32
	#ifdef NNN_USE_CRT_DEBUG
		int flags = _CrtSetDbgFlag(_CRTDBG_REPORT_FLAG);

		if(param != nullptr)
			flags |= param->m_CRT_Debug_flag;
		else
			flags |= _CRTDBG_ALLOC_MEM_DF | _CRTDBG_LEAK_CHECK_DF;

		_CrtSetDbgFlag(flags);
	#else
		enable();
		Set_Leak_Detect((param != nullptr) ? param->m_skip_self_Frames : true);
	#endif	// NNN_USE_CRT_DEBUG
#else
	enable();
	capture_stacks((param != nullptr) ? param->m_capture_stacks : true);	// 栈采集
#endif	// _WIN32

#endif	// NNN_ENABLE_MEMORY_LEAK_DETECT
}

}	// namespace Leak_Detect
}	// namespace NNN

#endif	// _NNN___NNNLEAKDETECT___NNNLEAKDETECT_H_
