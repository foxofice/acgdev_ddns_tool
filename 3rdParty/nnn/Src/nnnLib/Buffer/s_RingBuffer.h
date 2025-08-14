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

#include "../Thread/s_FastAtomicLock.h"
#include "../Thread/s_CriticalSection.h"

namespace NNN
{
namespace Buffer
{

struct s_RingBuffer
{
	// 构造函数/析构函数
	NNN_API	s_RingBuffer(size_t init_size = m_s_k_INIT_SIZE)
	{
		Reset(init_size);
	}
	NNN_API	~s_RingBuffer();

	// read_data() 移除数据的条件
	/*
		buffer		- 已读取的数据
		read_len	- 已读取的大小
		返回 true 时，则移除数据；否则不移除数据
	*/
	typedef bool	(CALLBACK *LPCALLBACK_READ_DATA_REMOVE_COND)(const BYTE *buffer, size_t read_len);

	// 写入数据（追加数据到末尾）
	NNN_API void	write_data(const BYTE *input_data, size_t input_data_len);

	// 读取数据
	/*
		buffer			-	如果为 nullptr，则表示不把数据复制到缓冲区
		len				-	要读取的字节数
		remove			-	如果为 true，则移除原有数据
		try_read_data	-	如果为 false，则 DATA.m_data_len 必须 >= len 时才读取，否则不读取；
							如果为 true，则尽可能读取（即使 DATA.m_data_len < len）
		remove_cond		-	仅在 remove 为 true 时有效。函数返回 true 时，则移除数据；否则不移除数据
		remove_len		-	实际取出的数据长度（移除长度）

		返回成功读取的字节数
	*/
	NNN_API size_t	read_data(	__out_opt BYTE						*buffer,
								size_t								len,
								bool								remove			= true,
								bool								try_read_data	= false,
								LPCALLBACK_READ_DATA_REMOVE_COND	remove_cond		= nullptr,
								__out_opt size_t					*remove_len		= nullptr );

	// 修改数据
	/*
		input_data_len	- 如果超出有效数据的长度，则只会修改到有效数据的部分
		offset			- 有效数据的偏移量，而不是 m_buffer 的偏移量
		返回成功修改的数据长度
	*/
	NNN_API size_t	edit_data(const BYTE *input_data, size_t input_data_len, size_t offset);

	// 清除数据
	NNN_API void	clear();

	// 重置状态
	NNN_API void	Reset(size_t init_size = m_s_k_INIT_SIZE);

	// 获取 m_data_len
	NNN_API size_t	get_data_len();

	// 获取 m_buffer_size
	NNN_API size_t	get_buffer_size();

	// 保留最小存储长度
	NNN_API void	reserve(size_t len);

	// 把数据复制到另一个环形缓冲区
	NNN_API void	CopyTo(struct s_RingBuffer &buffer);

protected:
	// 取得数据（非线程安全。可能是1段 or 2段数据。data2 = nullptr 时，表示只有1段数据）
	void	get_data(	__out BYTE* &data1, __out size_t &data1_len,
						__out BYTE* &data2, __out size_t &data2_len );

	// 写入数据/修改数据的公共函数
	template <typename UNLOCK_FUNC>
	inline void	write_data_func(const BYTE	*input_data,
								size_t		write_offset,
								size_t		write_len,
								UNLOCK_FUNC	unlock_func);

	constexpr static size_t	m_s_k_INIT_SIZE	= 1024;	// 初始化大小

	struct
	{
		size_t								m_buffer_size	= 0;		// 写入缓冲区大小
		BYTE								*m_buffer		= nullptr;	// 写入缓冲区

		struct Thread::s_FastAtomicLock		m_lock;						// 锁定成员变量访问
		struct Thread::s_CriticalSection	m_lock_reading;				// 锁定现有数据（IO操作）
		struct Thread::s_CriticalSection	m_lock_writing;				// 锁定空白数据（IO操作）

		size_t								m_data_offset	= 0;		// 有效数据的位置
		size_t								m_data_len		= 0;		// 有效数据的长度
	} DATA;
};

}	// namespace Buffer
}	// namespace NNN

#endif	// _NNNLIB___BUFFER___S_RINGBUFFER_H_
