//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 处理文本/字符串（包含文件）
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNLIB___TEXT___TEXT_INC_H_
#define _NNNLIB___TEXT___TEXT_INC_H_

#include "../../common/common.h"

namespace NNN
{
namespace Text
{

// 文本编码
enum struct es_CodepageType
{
	Ansi,
	UTF8,
	Unicode,
	Unicode_big_endian,
};

enum struct es_UnicodeType
{
	Word,		// 英文单词（A-Z、a-z、0-9、_）
	Space,		// 空格和 TAB
	Asian,		// 亚洲字符（19968~40869）
	SingleChar,	// 单独的字符
};

#define	WCHAR_SHORT_TO_INT(input_byte_pointer, output_byte_pointer)	\
		{	\
			(output_byte_pointer)[0] = (input_byte_pointer)[0];	\
			(output_byte_pointer)[1] = (input_byte_pointer)[1];	\
			(output_byte_pointer)[2] = 0;						\
			(output_byte_pointer)[3] = 0;						\
		}

#define	WCHAR_INT_TO_SHORT(input_byte_pointer, output_byte_pointer)	\
		{	\
			(output_byte_pointer)[0] = (input_byte_pointer)[0];	\
			(output_byte_pointer)[1] = (input_byte_pointer)[1];	\
		}

//========== 从其他类型转换成字符串 ==========(Start)
#if (NNN_PLATFORM == NNN_PLATFORM_WIN32) || (NNN_PLATFORM == NNN_PLATFORM_WP8)
#pragma warning(disable : 4996)
#endif	// NNN_PLATFORM

// bool <--> char*/WCHAR*
NNN_API inline WCHAR*	value2wchar(bool value, __out WCHAR txt[10])	{ wcscpy(txt, value ? L"true" : L"false");	return txt; }
NNN_API inline char*	value2char(bool value, __out char txt[10])		{ strcpy(txt, value ? "true" : "false");	return txt; }

// float <--> char*/WCHAR*
NNN_API inline WCHAR*	value2wchar(float value, __out WCHAR txt[50])	{ swprintf(txt, 50, L"%f", value);			return txt; }
NNN_API inline char*	value2char(float value, __out char txt[50])		{ sprintf(txt, "%f", value);				return txt; }

// double <--> char*/WCHAR*
NNN_API inline WCHAR*	value2wchar(double value, __out WCHAR txt[50])	{ swprintf(txt, 50, L"%lf", value);			return txt; }
NNN_API inline char*	value2char(double value, __out char txt[50])	{ sprintf(txt, "%lf", value);				return txt; }

// int <--> char*/WCHAR*
NNN_API inline WCHAR*	value2wchar(int value, __out WCHAR txt[20])		{ swprintf(txt, 20, L"%d", value);			return txt; }
NNN_API inline char*	value2char(int value, __out char txt[20])		{ sprintf(txt, "%d", value);				return txt; }

// UINT <--> char*/WCHAR*
NNN_API inline WCHAR*	value2wchar(UINT value, __out WCHAR txt[20])	{ swprintf(txt, 20, L"%u", value);			return txt; }
NNN_API inline char*	value2char(UINT value, __out char txt[20])		{ sprintf(txt, "%u", value);				return txt; }

// INT64 <--> char*/WCHAR*
NNN_API inline WCHAR*	value2wchar(INT64 value, __out WCHAR txt[30])	{ swprintf(txt, 30, L"%lld", value);		return txt; }
NNN_API inline char*	value2char(INT64 value, __out char txt[30])		{ sprintf(txt, "%lld", value);				return txt; }

// UINT64 <--> char*/WCHAR*
NNN_API inline WCHAR*	value2wchar(UINT64 value, __out WCHAR txt[30])	{ swprintf(txt, 30, L"%llu", value);		return txt; }
NNN_API inline char*	value2char(UINT64 value, __out char txt[30])	{ sprintf(txt, "%llu", value);				return txt; }

#if (NNN_PLATFORM == NNN_PLATFORM_WIN32) || (NNN_PLATFORM == NNN_PLATFORM_WP8)
#pragma warning(default : 4996)
#endif	// NNN_PLATFORM
//========== 从其他类型转换成字符串 ==========(End)

}	// namespace Text
}	// namespace NNN

#endif	// _NNNLIB___TEXT___TEXT_INC_H_
