//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 缓冲区写入
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNLIB___BUFFER___S_BINARYWRITER_H_
#define _NNNLIB___BUFFER___S_BINARYWRITER_H_

#include "../../common/common.h"
#include "../Text/Text.h"

namespace NNN
{
namespace Buffer
{

struct s_BinaryWriter
{
	// 构造函数
	inline s_BinaryWriter(BYTE *buffer)
	{
		m_buffer = buffer;
	}

	// 写入数据（返回写入位置）
	template<class T>
	inline T*		write(const T &value)
	{
		T *ret = (T*)(m_buffer + m_offset);
		*ret = value;
		m_offset += sizeof(T);

		return ret;
	}

	// 写入数组
	inline void		write_array(const void *data, size_t len)
	{
		if(data != nullptr && len > 0)
		{
			CopyMemory(m_buffer + m_offset, data, len);
			m_offset += len;
		}
	}

	// 写入 WCHAR*（二进制数据的 WCHAR 为 2 字节）
	inline void		write_wchar2(const WCHAR *txt, size_t chars_count)
	{
		if(chars_count > 0)
		{
#if (MARKUP_SIZEOFWCHAR == 2)
			write_array(txt, chars_count * 2);
#else
			Text::wchar_int2short((const BYTE*)txt, (int)chars_count, m_buffer + m_offset);
			m_offset += (chars_count * 2);
#endif	// MARKUP_SIZEOFWCHAR
		}
	}

	// 写入 WCHAR*（二进制数据的 WCHAR 为 4 字节）
	inline void		write_wchar4(const WCHAR *txt, size_t chars_count)
	{
		if(chars_count > 0)
		{
#if (MARKUP_SIZEOFWCHAR == 2)
			Text::wchar_short2int((const BYTE*)txt, (int)chars_count, m_buffer + m_offset);
			m_offset += (chars_count * 4);
#else
			write_array(txt, chars_count * 4);
#endif	// MARKUP_SIZEOFWCHAR
		}
	}

public:
	size_t	m_offset	= 0;
protected:
	BYTE	*m_buffer	= nullptr;
};

}	// namespace Buffer
}	// namespace NNN

#endif	// _NNNLIB___BUFFER___S_BINARYWRITER_H_
