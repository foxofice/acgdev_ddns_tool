//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : �������
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNLIB___PROCESS___PROCESS_H_
#define _NNNLIB___PROCESS___PROCESS_H_

#include "../../common/common.h"

namespace NNN
{
namespace Process
{

#if (NNN_PLATFORM == NNN_PLATFORM_WIN32)
// ���ý�����Ե�ԣ��� | ����ÿ�ź��ġ�0 = ȫ����-1 = ���һ�ź��ģ�
NNN_API BOOL	set_process_affinity_mask(int process_affinity_mask = -1);
#endif	// NNN_PLATFORM_WIN32

// ��ȡ���� ID������ -1 ʱ����ʾ���̲����ڣ�
// ��Win �����ִ�Сд��Linux ���ִ�Сд��
NNN_API int		get_pid_by_name(const WCHAR *proc_name);
NNN_API int		get_pid_by_name(const char *proc_name);

// ��������
NNN_API bool	kill_process(UINT pid);

// ��ȡ��ǰ���̵� ID
NNN_API	UINT	get_current_process_id();

// ��ȡ��ǰ�߳������ĸ���������
NNN_API bool	get_current_processor_number(__out UINT &number);

}	// namespace Process
}	// namespace NNN

#endif	// _NNNLIB___PROCESS___PROCESS_H_
