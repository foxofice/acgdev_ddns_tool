//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : �����ƶ�ȡ��
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNLIB___BUFFER___S_BINARYREADER_H_
#define _NNNLIB___BUFFER___S_BINARYREADER_H_

#include <string>

#include "../../common/common.h"
#include "../Text/Text.h"

namespace NNN
{
namespace Buffer
{

struct s_BinaryReader
{
	// ���캯��
	inline s_BinaryReader(const BYTE *buffer)
	{
		m_buffer = buffer;
	}

	// ��ȡ����
	template<class T>
	inline const T&		read()
	{
		T &ret = *(T*)(m_buffer + m_offset);
		m_offset += sizeof(T);
		return ret;
	}

	// ��ȡ����
	inline const BYTE*	read_array(size_t len)
	{
		const BYTE *ret = m_buffer + m_offset;
		m_offset += len;
		return ret;
	}

	// ��ȡ WCHAR*�����������ݵ� WCHAR Ϊ 2 �ֽڣ�
	inline void			read_wchar2(__out WCHAR *txt_buffer, size_t chars_count)
	{
		if(chars_count > 0)
		{
			const BYTE *src = read_array(chars_count * 2);

#if (MARKUP_SIZEOFWCHAR == 2)
			CopyMemory(txt_buffer, src, chars_count * 2);
#else
			Text::wchar_short2int(src, (int)chars_count, (BYTE*)txt_buffer);
#endif	// MARKUP_SIZEOFWCHAR
		}

		txt_buffer[chars_count] = L'\0';
	}

	// ��ȡ WCHAR*�����������ݵ� WCHAR Ϊ 4 �ֽڣ�
	inline void			read_wchar4(__out WCHAR *txt_buffer, size_t chars_count)
	{
		if(chars_count > 0)
		{
			const BYTE *src = read_array(chars_count);

#if (MARKUP_SIZEOFWCHAR == 2)
			Text::wchar_int2short(src, (int)chars_count, (BYTE*)txt_buffer);
#else
			CopyMemory(txt_buffer, src, chars_count * 4);
#endif	// MARKUP_SIZEOFWCHAR
		}

		txt_buffer[chars_count] = L'\0';
	}

	// ��ȡ�� std::wstring�����������ݵ� WCHAR Ϊ 2 �ֽڣ�
	inline void			read_wstring2(__out std::wstring &txt, size_t chars_count)
	{
		txt.resize(chars_count);

		if(!txt.empty())
			read_wchar2(&txt[0], chars_count);
	}

	// ��ȡ�� std::wstring�����������ݵ� WCHAR Ϊ 4 �ֽڣ�
	inline void			read_wstring4(__out std::wstring &txt, size_t chars_count)
	{
		txt.resize(chars_count);

		if(!txt.empty())
			read_wchar4(&txt[0], chars_count);
	}

public:
	size_t		m_offset	= 0;
protected:
	const BYTE	*m_buffer	= nullptr;
};

}	// namespace Buffer
}	// namespace NNN

#endif	// _NNNLIB___BUFFER___S_BINARYREADER_H_
