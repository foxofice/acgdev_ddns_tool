//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : CoreDump ����
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNLIB___MISC___COREDUMP___S_COREDUMP_SETTINGS_H_
#define _NNNLIB___MISC___COREDUMP___S_COREDUMP_SETTINGS_H_

#include "../../../common/common.h"

#ifdef _WIN32
#include <Dbghelp.h>
#endif	// _WIN32

namespace NNN
{
namespace Misc
{
namespace CoreDump
{

struct s_CoreDump_settings
{
	// ���캯��
	NNN_API			s_CoreDump_settings() {}
#ifdef _WIN32
	NNN_API			s_CoreDump_settings(const WCHAR filename[MAX_PATH], bool filenmae_add_datetime = true);

	// ��ȡ minidmp �ļ���
	NNN_API void	get_filename(__out WCHAR filename[MAX_PATH]);

	bool			m_filenmae_add_datetime	= true;						// minidmp �ļ����Ƿ��Զ���������ʱ��
	WCHAR			m_filename[MAX_PATH]	= L"core_dump.dmp";			// core dump ���ļ���
	MINIDUMP_TYPE	m_minidump_type			= MiniDumpWithFullMemory;	// MINIDUMP_TYPE �����
#else
	size_t			m_core_dump_max_size	= SIZE_MAX;			// core dump ������С���ֽڣ�
#endif	// _WIN32
};

}	// namespace CoreDump
}	// namespace Misc
}	// namespace NNN

#endif	// _NNNLIB___MISC___COREDUMP___S_COREDUMP_SETTINGS_H_
