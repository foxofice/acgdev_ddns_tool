//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : ���������
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

// ���� A �Ķ���� unique_A��B �Ķ���� unique_B
// comp Ϊ�Զ���ȽϺ�����������[](const s_Test *a, const s_Test *b) -> bool { return a->m_val < b->m_val; }��
template<typename T, typename Compare>
void diff_sorted_unique(Compare	comp,
						T		*A,			size_t			count_A,			// A ����
						T		*B,			size_t			count_B,			// B ����
						__out T	*unique_A,	__out size_t	&unique_A_count,	// A ������
						__out T *unique_B,	__out size_t	&unique_B_count)	// B ������
{
	// ԭ������
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

	// ʣ����
	while(i < count_A)
		unique_A[unique_A_count++] = A[i++];

	while(j < count_B)
		unique_B[unique_B_count++] = B[j++];
}

}	// namespace Algorithm
}	// namespace NNN

#endif	// _NNNLIB___ALGORITHM___UNIQUE_SET_H_
