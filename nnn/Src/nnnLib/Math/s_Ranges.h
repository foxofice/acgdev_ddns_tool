//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 范围列表
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNLIB___MATH___S_RANGES_H_
#define _NNNLIB___MATH___S_RANGES_H_

#include <map>

#include "../../common/common.h"

namespace NNN
{
namespace Math
{

struct s_Ranges
{
	// 构造函数/析构函数
	NNN_API					s_Ranges();
	NNN_API virtual			~s_Ranges();

	// 重置数据
	NNN_API virtual void	Reset();

	// 添加一个数据范围
	NNN_API virtual void	add_range(struct s_Range64 range);

	// 移除一个数据范围
	NNN_API virtual void	remove_range(struct s_Range64 range);

	// 查找指定数值是否在范围列表中
	NNN_API virtual bool	find(UINT64 value);

	// 获取所有范围包含多少个数值（比如范围：1~10、101~200，则返回 110）
	NNN_API virtual UINT64	get_count();

	// range.min -> range
	std::map<UINT64, struct s_Range64>	m_ranges;
};

}	// namespace Math
}	// namespace NNN

#endif	// _NNNLIB___MATH___S_RANGES_H_
