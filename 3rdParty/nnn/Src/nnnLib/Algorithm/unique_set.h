//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 计算独有项
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNLIB___ALGORITHM___UNIQUE_SET_H_
#define _NNNLIB___ALGORITHM___UNIQUE_SET_H_

#include <algorithm>

#include "../../common/common-macro.h"

namespace NNN
{
namespace Algorithm
{

// 生成 A 的独有项到 unique_A、B 的独有项到 unique_B
// comp 为自定义比较函数（范例：[](const s_Test *a, const s_Test *b) -> bool { return a->m_val < b->m_val; }）
template<typename T, typename Compare>
void diff_sorted_unique(Compare	comp,
						T		*A,			size_t			count_A,			// A 集合
						T		*B,			size_t			count_B,			// B 集合
						__out T	*unique_A,	__out size_t	&unique_A_count,	// A 独有项
						__out T *unique_B,	__out size_t	&unique_B_count)	// B 独有项
{
	// 原地排序
	std::sort(A, A + count_A, comp);
	std::sort(B, B + count_B, comp);

	size_t i = 0, j = 0;
	unique_A_count = 0;
	unique_B_count = 0;

	while(i < count_A && j < count_B)
	{
		if(comp(A[i], B[j]))
			unique_A[unique_A_count++] = A[i++];
		else if(comp(B[j], A[i]))
			unique_B[unique_B_count++] = B[j++];
		else
		{
			++i;
			++j;
		}
	}	// while

	// 剩余项
	while(i < count_A)
		unique_A[unique_A_count++] = A[i++];

	while(j < count_B)
		unique_B[unique_B_count++] = B[j++];
}

}	// namespace Algorithm
}	// namespace NNN

#endif	// _NNNLIB___ALGORITHM___UNIQUE_SET_H_
