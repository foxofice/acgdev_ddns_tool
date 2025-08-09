//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 文件操作
//（注意：使用 filename 的版本，必须是物理存在的文件，包括 Android/iOS/WP8 的 app 包里，但不能 whp 包）
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNLIB___IO___FILE___FILE_H_
#define _NNNLIB___IO___FILE___FILE_H_

#include <string>
#include <vector>
#include <sys/stat.h>

#include "../../../common/common.h"

#include "../../STL/STL.h"
#include "../../Text/Text-inc.h"

namespace NNN
{
namespace IO
{
namespace File
{

// 判断文件是否存在
NNN_API bool	exists(const WCHAR *filename);
NNN_API bool	exists(const char *filename);

// 获取文件大小
NNN_API HRESULT	get_file_size(const WCHAR *filename, __out __int64 *size);
NNN_API HRESULT	get_file_size(const char *filename, __out __int64 *size);

// 获取文件信息
#if defined(WIN32) || defined(_WIN32)
NNN_API HRESULT	get_file_info(const WCHAR *filename, __out struct _stat64 *fi);
NNN_API HRESULT	get_file_info(const char *filename, __out struct _stat64 *fi);
#else
NNN_API HRESULT	get_file_info(const WCHAR *filename, __out struct stat *fi);
NNN_API HRESULT	get_file_info(const char *filename, __out struct stat *fi);
#endif	// WIN32 || _WIN32

//==================== 读取文件 ====================(Start)
// 读取文件内容
NNN_API HRESULT	read_file(const WCHAR *filename, __out BYTE **output, __out_opt __int64 *file_size = nullptr);
NNN_API HRESULT	read_file(const char *filename, __out BYTE **output, __out_opt __int64 *file_size = nullptr);
NNN_API HRESULT	read_file(const WCHAR *filename, __out BYTE *output);
NNN_API HRESULT	read_file(const char *filename, __out BYTE *output);

// 打开一个文本文件，读取文件的所有内容，然后关闭该文件
NNN_API HRESULT	read_all_text(const BYTE *input, size_t size, __out WCHAR *output);
NNN_API HRESULT	read_all_text(const BYTE *input, size_t size, __out WCHAR **output);
NNN_API HRESULT	read_all_text(const BYTE *input, size_t size, __out char *output);
NNN_API HRESULT	read_all_text(const BYTE *input, size_t size, __out char **output);

NNN_API HRESULT	read_all_text(const WCHAR *filename, __out WCHAR *output);
NNN_API HRESULT	read_all_text(const WCHAR *filename, __out WCHAR **output);
NNN_API HRESULT	read_all_text(const char *filename, __out char *output);
NNN_API HRESULT	read_all_text(const char *filename, __out char **output);

// 从内存数据中读取所有行
NNN_API HRESULT	read_all_lines_from_data(const BYTE *input, size_t size, __out struct STL::s_TxtArrayW &output);
NNN_API HRESULT	read_all_lines_from_data(const BYTE *input, size_t size, __out struct STL::s_TxtArrayA &output);
NNN_API HRESULT	read_all_lines_from_data(const WCHAR *txt, __out struct STL::s_TxtArrayW &output);
NNN_API HRESULT	read_all_lines_from_data(const char *txt, __out struct STL::s_TxtArrayA &output);

// 打开一个文本文件，读取文件的所有行，然后关闭该文件
NNN_API HRESULT	read_all_lines(const WCHAR *filename, __out struct STL::s_TxtArrayW &output);
NNN_API HRESULT	read_all_lines(const char *filename, __out struct STL::s_TxtArrayA &output);
//==================== 读取文件 ====================(End)

//==================== 写入文件 ====================(Start)
// 写入内容到文件中
NNN_API HRESULT	write_file(const WCHAR *filename, const BYTE *data, size_t data_len);
NNN_API HRESULT	write_file(const char *filename, const BYTE *data, size_t data_len);

// 创建一个新文件，在其中写入指定的字符串，然后关闭该文件
NNN_API HRESULT	write_all_txt(const char *filename, const char *txt, Text::es_CodepageType type = Text::es_CodepageType::UTF8);
NNN_API HRESULT	write_all_txt(const WCHAR *filename, const WCHAR *txt, Text::es_CodepageType type = Text::es_CodepageType::UTF8);

// 创建一个新文件，在其中写入指定的字符串数组，然后关闭该文件
NNN_API HRESULT	write_all_lines(const char *filename, const char **lines, int lines_count, Text::es_CodepageType type = Text::es_CodepageType::UTF8);
NNN_API HRESULT	write_all_lines(const WCHAR *filename, const WCHAR **lines, int lines_count, Text::es_CodepageType type = Text::es_CodepageType::UTF8);
//==================== 写入文件 ====================(End)

//==================== 追加文件 ====================(Start)
// 追加内容到文件中
NNN_API HRESULT	append_file(const char *filename, const BYTE *data, size_t data_len);
NNN_API HRESULT	append_file(const WCHAR *filename, const BYTE *data, size_t data_len);
NNN_API HRESULT	append_file(const char *filename, const char *txt);
NNN_API HRESULT	append_file(const WCHAR *filename, const WCHAR *txt);

// 在一个文件中追加文本行，然后关闭该文件
NNN_API HRESULT	append_all_lines(const char *filename, const char **lines, int lines_count);
NNN_API HRESULT	append_all_lines(const WCHAR *filename, const WCHAR **lines, int lines_count);
//==================== 追加文件 ====================(End)

// 截断文件
NNN_API HRESULT	truncate_file(const char *filename, __int64 len);
NNN_API HRESULT	truncate_file(const WCHAR *filename, __int64 len);

//==================== 文件操作 ====================(Start)
// 复制一个文件
NNN_API HRESULT	copy_file(const char *src_filename, const char *dst_filename);
NNN_API HRESULT	copy_file(const WCHAR *src_filename, const WCHAR *dst_filename);

// 移动一个文件
NNN_API HRESULT	move_file(const WCHAR *src_filename, const WCHAR *dst_filename);
NNN_API HRESULT	move_file(const char *src_filename, const char *dst_filename);

// 删除一个文件
NNN_API HRESULT	delete_file(const WCHAR *filename);
NNN_API HRESULT	delete_file(const char *filename);

// 更改文件名
NNN_API HRESULT	rename_file(const WCHAR *old_filename, const WCHAR *new_filename);
NNN_API HRESULT	rename_file(const char *old_filename, const char *new_filename);
//==================== 文件操作 ====================(End)

}	// namespace File
}	// namespace IO
}	// namespace NNN

#endif	// _NNNLIB___IO___FILE___FILE_H_
