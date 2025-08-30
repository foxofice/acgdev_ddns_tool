//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : CoreDump 设置
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
	// 构造函数
	NNN_API			s_CoreDump_settings() {}
#ifdef _WIN32
	NNN_API			s_CoreDump_settings(const WCHAR filename[MAX_PATH], bool filenmae_add_datetime = true);

	// 获取 minidmp 文件名
	NNN_API void	get_filename(__out WCHAR filename[MAX_PATH]);

	bool			m_filenmae_add_datetime	= true;						// minidmp 文件名是否自动加上日期时间
	WCHAR			m_filename[MAX_PATH]	= L"core_dump.dmp";			// core dump 的文件名
	MINIDUMP_TYPE	m_minidump_type			= MiniDumpWithFullMemory;	// MINIDUMP_TYPE 的组合
#else
	size_t			m_core_dump_max_size	= SIZE_MAX;			// core dump 的最大大小（字节）
#endif	// _WIN32
};

}	// namespace CoreDump
}	// namespace Misc
}	// namespace NNN

#endif	// _NNNLIB___MISC___COREDUMP___S_COREDUMP_SETTINGS_H_
