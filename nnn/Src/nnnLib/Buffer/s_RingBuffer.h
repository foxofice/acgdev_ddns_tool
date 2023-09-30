//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 环形缓冲区（BYTE 类型，可自动扩展大小）
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNLIB___BUFFER___S_RINGBUFFER_H_
#define _NNNLIB___BUFFER___S_RINGBUFFER_H_

#include <atomic>

#include "../Thread/s_CriticalSection.h"

namespace NNN
{
namespace Buffer
{

struct s_RingBuffer
{
	// 构造函数/析构函数
	NNN_API			s_RingBuffer()
	{
		Reset();
	}
	NNN_API			~s_RingBuffer()
	{
		SAFE_DELETE_ARRAY(DATA.m_buffer);
	}

	// 清除数据
	NNN_API void	clear();

	// 写入数据（offset 表示从有效数据开始的偏移量，而不是 m_buffer 的偏移量。offset = -1 时，表示追加数据）
	NNN_API void	write_data(const BYTE *input_data, size_t input_data_len, size_t offset = -1);

	// 读取数据（remove = true 时，移除原有数据。buffer = nullptr 时，表示不把数据复制到缓冲区）
	NNN_API void	read_data(__out_opt BYTE *buffer, size_t max_data_len, __out size_t &read_len, bool remove = true);

	// 扩展缓冲区大小（原有数据不变）
	NNN_API void	add_size(size_t add_byte);

	// 重置状态
	NNN_API void	Reset();

	// 获取 m_data_len
	NNN_API size_t	get_data_len();

	// 获取 m_buffer_size
	NNN_API size_t	get_buffer_size();

	// 把数据复制到另一个环形缓冲区
	NNN_API void	CopyTo(struct s_RingBuffer &buffer);

	//======================================================================

protected:
	// 取得数据（可能是一段或两段数据。data2 = nullptr 时，表示只有一段数据）
	NNN_API void	get_data(	__out BYTE* &data1, __out size_t &data1_len,
								__out BYTE* &data2, __out size_t &data2_len );

	struct
	{
		BYTE								*m_buffer		= nullptr;	// 缓冲区
		size_t								m_buffer_size	= 0;		// 缓冲区大小

		size_t								m_data_idx		= 0;		// 有效数据的起始索引（对于 m_buffer）
		size_t								m_data_len		= 0;		// 有效数据的长度

		struct Thread::s_CriticalSection	m_cs;
	} DATA;
};

}	// namespace Buffer
}	// namespace NNN

#endif	// _NNNLIB___BUFFER___S_RINGBUFFER_H_
