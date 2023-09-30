//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 环形缓冲区（通用数值类型，固定大小）
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNLIB___BUFFER___S_FIXRINGBUFFER_H_
#define _NNNLIB___BUFFER___S_FIXRINGBUFFER_H_

#include <vector>

#include "../../common/common.h"

namespace NNN
{
namespace Buffer
{

struct s_FixRingBuffer
{
	// 构造函数/析构函数
	NNN_API s_FixRingBuffer(size_t max_count)
	{
		m_buffer.resize(__max(1, max_count));
	}
	NNN_API ~s_FixRingBuffer()
	{
	}

	// 添加新的值（插入到末尾）
	NNN_API void					add_value(struct s_Value value);

	// 获取第一个/最后一个数值
	NNN_API inline struct s_Value	front()	{ return m_buffer[m_start_index]; }
	NNN_API struct s_Value			back();

	// 取得所有数值
	NNN_API void					GetValues(__out std::vector<struct s_Value> &values);

	// 获取最大数量
	NNN_API inline size_t			GetMaxCount() { return m_buffer.size(); }

	// 清空所有数据
	NNN_API void					Clear();

	//======================================================================

protected:
	std::vector<struct s_Value>	m_buffer;
	size_t						m_start_index	= 0;	// 起始元素在 m_buffer 中的 index

public:
	size_t						m_count			= 0;	// 元素数量
};

}	// namespace Buffer
}	// namespace NNN

#endif	// _NNNLIB___BUFFER___S_FIXRINGBUFFER_H_
