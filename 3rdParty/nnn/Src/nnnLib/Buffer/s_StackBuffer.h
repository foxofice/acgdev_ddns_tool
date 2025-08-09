//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 栈内存优化（小缓冲优化，SBO，Small Buffer Optimization）
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNLIB___BUFFER___S_STACKBUFFER_H_
#define _NNNLIB___BUFFER___S_STACKBUFFER_H_

#include <vector>

namespace NNN
{
namespace Buffer
{

template <typename T, size_t stack_size>
struct s_StackBuffer
{
	// 构造函数
	inline s_StackBuffer(size_t init_buffer_len = 0)
	{
		if(init_buffer_len <= _countof(m_short_buffer))
			m_p = m_short_buffer;
		else
		{
			m_long_buffer.resize(init_buffer_len);
			m_p = &m_long_buffer[0];
		}
	}

	// 保留最小存储长度
	inline void	reserve(size_t len)
	{
		if(m_p == m_short_buffer)
		{
			if(len > _countof(m_short_buffer))
			{
				m_long_buffer.resize(len);
				m_p = &m_long_buffer[0];

				CopyMemory(m_p, m_short_buffer, sizeof(m_short_buffer));
			}
		}
		else
		{
			m_long_buffer.resize(len);
			m_p = &m_long_buffer[0];
		}
	}

	T				*m_p;

protected:
	T				m_short_buffer[stack_size];
	std::vector<T>	m_long_buffer;

private:
	DISALLOW_COPY_AND_ASSIGN(s_StackBuffer);
};

}	// namespace Buffer
}	// namespace NNN

#endif	// _NNNLIB___BUFFER___S_STACKBUFFER_H_
