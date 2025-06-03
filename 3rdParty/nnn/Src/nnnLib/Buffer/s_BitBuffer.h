//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 位缓冲区（类似 vector<bool>）
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNLIB___BUFFER___S_BITBUFFER_H_
#define _NNNLIB___BUFFER___S_BITBUFFER_H_

#include "../../common/common.h"

namespace NNN
{
namespace Buffer
{

struct s_BitBuffer
{
	// 构造函数/析构函数
	NNN_API				s_BitBuffer()
	{
	}
	NNN_API				~s_BitBuffer()
	{
		SAFE_DELETE_ARRAY(m_buffer);
	}

	// 初始化
	NNN_API	HRESULT		Init(size_t bits_count);

	// 重设大小
	NNN_API	HRESULT		resize(size_t bits_count);

	// bit 数 -> 缓冲区大小
	NNN_API size_t		Bits_to_BufferSize(size_t bits_count);

	// 设置/获取数值
	NNN_API void		SetValue(size_t index, bool value);
	NNN_API bool		GetValue(size_t index);

	BYTE	*m_buffer		= nullptr;
	size_t	m_bits_count	= 0;
};

}	// namespace Buffer
}	// namespace NNN

#endif	// _NNNLIB___BUFFER___S_BITBUFFER_H_
