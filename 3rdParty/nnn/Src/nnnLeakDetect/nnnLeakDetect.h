//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : �ڴ�й©���
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNN___NNNLEAKDETECT___NNNLEAKDETECT_H_
#define _NNN___NNNLEAKDETECT___NNNLEAKDETECT_H_

/*
ʹ�÷�����
	��������п⡿������ͷ�ļ� nnnLeakDetect.h��Win �»��Զ����� nnnLeakDetect.lib����·����ȷ���ɣ�
	�����ִ�е���������ͷ�ļ� nnnLeakDetect.h + ���� MemoryLeakCheck() �����ڴ�й©���
	������ Win ƽ̨�����Ҫ�ָ��� CRT ���ԣ����ȶ��� NNN_USE_CRT_DEBUG
	������ Release��Ĭ�Ϲرա�Ҫ�����Ļ����ڰ���ͷ�ļ�֮ǰ�ȶ��� NNN_ENABLE_MEMORY_LEAK_DETECT
	������ c++/clr����������޷�ץȡ�ڴ�й©�Ļ��������� imports.cpp ���������� lib ֮ǰ������ nnnLeakDetect.lib

	ע�⣺
		Win �� CRT ����ץ�� new/delete��malloc/free
		NNN_Leak_Detect ������ʵ��ֻץȡ new/delete
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
// �����ڴ�й©��������skip_self_Frames = true ʱ����ʾ���� nnnLeakDetect �ڲ��Է�ֹαй©��
NNN_API	void	Set_Leak_Detect(bool skip_self_Frames);
#else
// ջ�ɼ�
NNN_API void	capture_stacks(bool v);
#endif	// _WIN32

// ����/�ر��ڴ�й©���
NNN_API void	enable();
NNN_API void	disable();

// ��������
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

// �����ڴ�й©���
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
	capture_stacks((param != nullptr) ? param->m_capture_stacks : true);	// ջ�ɼ�
#endif	// _WIN32

#endif	// NNN_ENABLE_MEMORY_LEAK_DETECT
}

}	// namespace Leak_Detect
}	// namespace NNN

#endif	// _NNN___NNNLEAKDETECT___NNNLEAKDETECT_H_
