//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 处理文本/字符串
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNLIB___TEXT___TEXT_H_
#define _NNNLIB___TEXT___TEXT_H_

#include <string>
#include <vector>

#include "../../common/common-macro.h"

#ifdef NNN_ANDROID
#include <jni.h>
#include <android/log.h>
#endif	// NNN_ANDROID

#include "../STL/STL.h"
#include "Text-inc.h"

namespace NNN
{
namespace Text
{

// 把字符串转换为小写（直接修改）
NNN_API void			StringToLowerInPlace(__inout WCHAR *txt);
NNN_API void			StringToLowerInPlace_ASCII(__inout char *txt);
NNN_API void			StringToLowerInPlace_Unicode(__inout char *txt);

// 把字符串转换为小写（返回 output）
NNN_API const WCHAR*	StringToLower(const WCHAR *txt, __out WCHAR *output);
NNN_API const char*		StringToLower_ASCII(const char *txt, __out char *output);
NNN_API const char*		StringToLower_Unicode(const char *txt, __out char *output);

// 把字符串转换为大写（直接修改）
NNN_API void			StringToUpperInPlace(__inout WCHAR *txt);
NNN_API void			StringToUpperInPlace_ASCII(__inout char *txt);
NNN_API void			StringToUpperInPlace_Unicode(__inout char *txt);

// 把字符串转换为大写（返回 output）
NNN_API const WCHAR*	StringToUpper(const WCHAR *txt, __out WCHAR *output);
NNN_API const char*		StringToUpper_ASCII(const char *txt, __out char *output);
NNN_API const char*		StringToUpper_Unicode(const char *txt, __out char *output);

// wchar* <--> char*（OutputBuffer_CharsCount 是字符数，不是字节数）
NNN_API HRESULT			wchar2char(const WCHAR *input, __out char **output, __out_opt int *output_chars_count);
NNN_API HRESULT			wchar2char(const WCHAR *input, __out char *output, int OutputBuffer_CharsCount, __out_opt int *output_chars_count);
NNN_API HRESULT			char2wchar(const char *input, __out WCHAR **output, __out_opt int *output_chars_count);
NNN_API HRESULT			char2wchar(const char *input, __out WCHAR *output, int OutputBuffer_CharsCount, __out_opt int *output_chars_count);

// wchar* -> std::string
NNN_API HRESULT			wchar2string(const WCHAR *input, __out std::string &output);
// char* -> std::wstring
NNN_API HRESULT			char2wstring(const char *input, __out std::wstring &output);

// BYTE* <--> 16 进制字符串（upper = true 时，表示大写；reverse = true 时，表示反转 input_data）
NNN_API HRESULT			ToHexString(const BYTE *input_data, size_t input_len, __out char *output, bool upper = true, bool reverse = false);
NNN_API HRESULT			ToHexString(const BYTE *input_data, size_t input_len, __out char **output, bool upper = true, bool reverse = false);
NNN_API HRESULT			ToHexString(const BYTE *input_data, size_t input_len, __out WCHAR *output, bool upper = true, bool reverse = false);
NNN_API HRESULT			ToHexString(const BYTE *input_data, size_t input_len, __out WCHAR **output, bool upper = true, bool reverse = false);

NNN_API HRESULT			FromHexString(const char *input, __out BYTE *output);
NNN_API HRESULT			FromHexString(const char *input, __out BYTE **output);
NNN_API HRESULT			FromHexString(const WCHAR *input, __out BYTE *output);
NNN_API HRESULT			FromHexString(const WCHAR *input, __out BYTE **output);

// Base64 编码/解码
NNN_API HRESULT			ToBase64String(const BYTE *input_data, size_t input_len, __out char *output);
NNN_API HRESULT			ToBase64String(const BYTE *input_data, size_t input_len, __out char **output);
NNN_API HRESULT			ToBase64String(const BYTE *input_data, size_t input_len, __out WCHAR *output);
NNN_API HRESULT			ToBase64String(const BYTE *input_data, size_t input_len, __out WCHAR **output);

NNN_API HRESULT			FromBase64String(const char *input, __out BYTE *output, __out size_t &output_len);
NNN_API HRESULT			FromBase64String(const char *input, __out BYTE **output, __out size_t &output_len);
NNN_API HRESULT			FromBase64String(const WCHAR *input, __out BYTE **output, __out size_t &output_len);
NNN_API HRESULT			FromBase64String(const WCHAR *input, __out BYTE *output, __out size_t &output_len);

// 计算 Base64 编码/解码后的数据长度
NNN_API inline size_t	Get_Base64_Encode_Len(size_t data_len)			{ return 4 * ((data_len + 2) / 3); }
NNN_API inline size_t	Get_Base64_Decode_Len(size_t base64_string_len)	{ return base64_string_len / 4 * 3; }

// UTF8 <--> Unicode
NNN_API HRESULT			UTF8_2_Unicode(const char *input, __out WCHAR *output, __out_opt int *output_chars_count);
NNN_API HRESULT			UTF8_2_Unicode(const char *input, __out WCHAR **output, __out_opt int *output_chars_count);
NNN_API HRESULT			Unicode_2_UTF8(const WCHAR *input, __out char *output, __out_opt int *output_chars_count);
NNN_API HRESULT			Unicode_2_UTF8(const WCHAR *input, __out char **output, __out_opt int *output_chars_count);

// UTF8 <--> Ansi
NNN_API HRESULT			UTF8_2_Ansi(const char *input, __out char **output, __out_opt int *output_chars_count);
NNN_API HRESULT			UTF8_2_Ansi(const char *input, __out char output[4096]);
NNN_API HRESULT			Ansi_2_UTF8(const char *input, __out char **output, __out_opt int *output_chars_count);
NNN_API HRESULT			Ansi_2_UTF8(const char *input, __out char output[4096]);

// 判断指定文本是否 UTF8 编码
NNN_API bool			IsTextUTF8(const char *str, size_t length);

// 2 字节 wchar_t <--> 4 字节 wchar_t
NNN_API HRESULT			wchar_short2int(const BYTE *input, int characters_count, __out BYTE **output);
NNN_API HRESULT			wchar_short2int(const BYTE *input, int characters_count, __out BYTE *output);
NNN_API HRESULT			wchar_int2short(const BYTE *input, int characters_count, __out BYTE **output);
NNN_API HRESULT			wchar_int2short(const BYTE *input, int characters_count, __out BYTE *output);

// 获取文本编码
NNN_API es_CodepageType	GetCodepageType(const BYTE *input, int len);

// 获取指定字符的 UnicodeType
NNN_API es_UnicodeType	GetUnicodeType(WCHAR ch);

//==============================

// 分隔字符串（strDelimit：作为分隔符的字符串）
NNN_API void			Split(const char *input, const char *strDelimit, __out struct STL::s_TxtArrayA &output);
NNN_API void			Split(const WCHAR *input, const WCHAR *strDelimit, __out struct STL::s_TxtArrayW &output);

// 分隔字符串（strDelimit_List：分隔字符列表。每个字符为一个分隔符）
NNN_API HRESULT			Split2(const char *input, const char *strDelimit_List, __out struct STL::s_TxtArrayA &output);
NNN_API HRESULT			Split2(const WCHAR *input, const WCHAR *strDelimit_List, __out struct STL::s_TxtArrayW &output);

// 把字符串分割成多行
NNN_API HRESULT			split_to_lines(const char *input, __out struct STL::s_TxtArrayA &lines);
NNN_API HRESULT			split_to_lines(const WCHAR *input, __out struct STL::s_TxtArrayW &lines);

// 去除前后空白
NNN_API char*			trim(const char *input, __out char *output);
NNN_API WCHAR*			trim(const WCHAR *input, __out WCHAR *output);

// 替换字符串
NNN_API bool			ReplaceString(const char *input, const char *old_string, const char *new_string, __out char *output);
NNN_API bool			ReplaceString(const char *input, const char *old_string, const char *new_string, __out char **output);
NNN_API bool			ReplaceString(const WCHAR *input, const WCHAR *old_string, const WCHAR *new_string, __out WCHAR *output);
NNN_API bool			ReplaceString(const WCHAR *input, const WCHAR *old_string, const WCHAR *new_string, __out WCHAR **output);

//==================== 字符串分析 ====================(Start)
// 删除指定位置的字符串（返回 true 表示更改了字符串，false 表示没有修改字符串）
NNN_API bool			RemoveChar(__inout char *txt, int nIndex);
NNN_API bool			RemoveChar(__inout WCHAR *txt, int nIndex);

// 从字符串中移除特定的字符
NNN_API void			RemoveChars(const char *txt, const char *chars_list, __out char *output);
NNN_API void			RemoveChars(const WCHAR *txt, const WCHAR *chars_list, __out WCHAR *output);

//// 获取指定索引位置的上一个单词的索引位置（主要做「Ctrl + 左」的操作时用到）
//NNN_API void			GetPriorItemPos(const std::string &txt, int index, __out int &out_index);
//
//// 获取指定索引位置的下一个单词的索引位置（主要做「Ctrl + 右」的操作时用到）
//NNN_API void			GetNextItemPos(const std::string &txt, int index, __out int &out_index);
//==================== 字符串分析 ====================(End)

// 转换成其他类型
NNN_API bool			ToBoolean(const WCHAR *txt);
NNN_API bool			ToBoolean(const char *txt);

NNN_API float			ToFloat(const WCHAR *txt);
NNN_API float			ToFloat(const char *txt);

NNN_API double			ToDouble(const WCHAR *txt);
NNN_API double			ToDouble(const char *txt);

NNN_API short			ToShort(const WCHAR *txt);
NNN_API short			ToShort(const char *txt);

NNN_API USHORT			ToUSHORT(const WCHAR *txt);
NNN_API USHORT			ToUSHORT(const char *txt);

NNN_API int				ToInt(const WCHAR *txt);
NNN_API int				ToInt(const char *txt);

NNN_API UINT			ToUINT(const WCHAR *txt);
NNN_API UINT			ToUINT(const char *txt);

NNN_API INT64			ToInt64(const WCHAR *txt);
NNN_API INT64			ToInt64(const char *txt);

NNN_API UINT64			ToUINT64(const WCHAR *txt);
NNN_API UINT64			ToUINT64(const char *txt);

// 格式：AARRGGBB 或 0xAARRGGBB
NNN_API DWORD			ToHEX(const WCHAR *txt);
NNN_API DWORD			ToHEX(const char *txt);

// 格式：width, height 或 width * height
NNN_API SIZE			ToSize(const WCHAR *txt);
NNN_API SIZE			ToSize(const char *txt);

// 格式：x, y
NNN_API POINT			ToPoint(const WCHAR *txt);
NNN_API POINT			ToPoint(const char *txt);

// 格式：left, top, right, bottom
NNN_API RECT			ToRect(const WCHAR *txt);
NNN_API RECT			ToRect(const char *txt);

// 读取&修改模板文本（模板变量格式为：${xxx}）
/*
	范例：
	std::wstring output_txt;
	read_template_txt(L"${name} = ${value}", { L"name", L"user", L"value", L"root" }, output_txt);

	output_txt 将变成：「user = root」
*/
NNN_API HRESULT			read_template_txt(	const WCHAR						*template_txt,
											const std::vector<std::wstring>	&replace_words_list,
											__out std::wstring				&output_txt );
NNN_API HRESULT			read_template_file(	const WCHAR						*template_filename,
											const std::vector<std::wstring>	&replace_words_list,
											__out std::wstring				&output_txt );

}	// namespace Text
}	// namespace NNN

#endif	// _NNNLIB___TEXT___TEXT_H_
