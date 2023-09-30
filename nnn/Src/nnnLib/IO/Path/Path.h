//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 目录操作
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNLIB___IO___PATH___PATH_H_
#define _NNNLIB___IO___PATH___PATH_H_

#include <string>
#include <list>
#include <sys/stat.h>

#include "../../../common/common.h"

#include "../../STL/STL.h"

namespace NNN
{
namespace IO
{
namespace Path
{

// 判断目录是否存在
NNN_API bool	exists(const WCHAR *dir);
NNN_API bool	exists(const char *dir);

// 判断文件（或目录）是否存在
NNN_API bool	is_file_or_dir_exists(const WCHAR *path_or_filename);
NNN_API bool	is_file_or_dir_exists(const char *path_or_filename);

// 获取修正后的路径（比如：E:/cpp/nnn/trunk/nnnEngine/Src/Tools/nnnEditor/../../../bin/Win32/Debug/）
NNN_API void	get_fixed_path(const WCHAR *path, __out WCHAR fixed_path[MAX_PATH]);
NNN_API void	get_fixed_path(const char *path, __out char fixed_path[MAX_PATH]);

// 把路径转换为相对路径
NNN_API void	get_relative_path(const WCHAR *full_path, const WCHAR *dir_path, __out WCHAR output_path[MAX_PATH]);
NNN_API void	get_relative_path(const char *full_path, const char *dir_path, __out char output_path[MAX_PATH]);
#if (NNN_PLATFORM == NNN_PLATFORM_WIN32)
NNN_API void	get_relative_path(const WCHAR *path, __out WCHAR output_path[MAX_PATH]);
#endif	// NNN_PLATFORM_WIN32

#if (NNN_PLATFORM == NNN_PLATFORM_WIN32) || (NNN_PLATFORM == NNN_PLATFORM_WP8)
// 设置/获取文件（或文件夹）的属性
NNN_API HRESULT	set_path_attrib(const WCHAR *path, DWORD attrib);
NNN_API HRESULT	get_path_attrib(const WCHAR *path, __out DWORD *attrib);

// 设置/获取文件（或文件夹）时间
NNN_API HRESULT	set_path_time(	const WCHAR	*path,
								FILETIME	create_time,
								FILETIME	access_time,
								FILETIME	write_time );
NNN_API HRESULT	get_path_time(	const WCHAR		*path,
								__out FILETIME	*create_time,
								__out FILETIME	*access_time,
								__out FILETIME	*write_time );
#endif	// NNN_PLATFORM

// 创建目录
#if (NNN_PLATFORM == NNN_PLATFORM_WIN32) || (NNN_PLATFORM == NNN_PLATFORM_WP8)
	#define MODE_LINUX_DEFAULT	0
#else
	#define MODE_LINUX_DEFAULT	S_IRWXU | S_IRWXG | S_IRWXO
#endif	// NNN_PLATFORM_WIN32 || NNN_PLATFORM_WP8
NNN_API bool	create_directory(const WCHAR *dir, USHORT mode_linux = MODE_LINUX_DEFAULT);
NNN_API bool	create_directory(const char *dir, USHORT mode_linux = MODE_LINUX_DEFAULT);
#undef MODE_LINUX_DEFAULT

// 将两个字符串组合成一个路径
NNN_API bool	Combine(const WCHAR *string1, const WCHAR *string2, __out WCHAR path[MAX_PATH]);

// 获取指定文件所在的目录
NNN_API HRESULT	get_dir(const char *path_name, __out char dir[MAX_PATH]);
NNN_API HRESULT	get_dir(const WCHAR *path_name, __out WCHAR dir[MAX_PATH]);

// 把路径转换为绝对路径（TODO: Linux 部分未完成）
//NNN_API std::wstring	GetFullPath(const WCHAR *path);
//NNN_API std::string	GetFullPath(const char *path);

// TODO: 在指定的目录中查找指定的文件名
//NNN_API std::wstring	FindFileInPath(const WCHAR *filename, const WCHAR *path = nullptr);

// 获取指定目录的所有文件
NNN_API HRESULT			GetFiles(	const WCHAR						*path,
									__out struct STL::s_TxtListW	&files,
									bool							top_dir_only = true );

// 获取指定目录的所有文件夹
NNN_API HRESULT			GetDirectories(	const WCHAR						*path,
										__out struct STL::s_TxtListW	&dirs,
										bool							top_dir_only = true );

}	// namespace Path
}	// namespace IO
}	// namespace NNN

#endif	// _NNNLIB___IO___PATH___PATH_H_
