//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 算法
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNLIB___ALGORITHM___ALGORITHM_H_
#define _NNNLIB___ALGORITHM___ALGORITHM_H_

#include <vector>

#include "../../common/common.h"

#include "../Math/s_Ranges.h"

namespace NNN
{
namespace Algorithm
{

// 出处：https://blog.csdn.net/icefireelf/article/details/5796529
template<class T>
size_t BKDRHash(const T *str)
{
//#if (NNN_PLATFORM != NNN_PLATFORM_ANDROID)
//	register
//#endif	// NNN_PLATFORM
		size_t hash = 0;

	while(size_t ch = (size_t)*str++)
	{
		hash = hash * 131 + ch;	// 也可以乘以31、131、1313、13131、131313..
		// 有人说将乘法分解为位运算及加减法可以提高效率，如将上式表达为：hash = hash << 7 + hash << 1 + hash + ch;
		// 但其实在Intel平台上，CPU内部对二者的处理效率都是差不多的，
		// 我分别进行了100亿次的上述两种运算，发现二者时间差距基本为0（如果是Debug版，分解成位运算后的耗时还要高1/3）；
		// 在ARM这类RISC系统上没有测试过，由于ARM内部使用Booth's Algorithm来模拟32位整数乘法运算，它的效率与乘数有关：
		// 当乘数8-31位都为1或0时，需要1个时钟周期
		// 当乘数16-31位都为1或0时，需要2个时钟周期
		// 当乘数24-31位都为1或0时，需要3个时钟周期
		// 否则，需要4个时钟周期
		// 因此，虽然我没有实际测试，但是我依然认为二者效率上差别不大
	}

	return hash;
}

template<class T>
size_t BKDRHash(const T *str, int count)
{
//#if (NNN_PLATFORM != NNN_PLATFORM_ANDROID)
//	register
//#endif	// NNN_PLATFORM
		size_t hash = 0;

	for(int i=0; i<count; ++i)
	{
		size_t ch = str[i];
		hash = hash * 131 + ch;
	}

	return hash;
}

namespace Vector
{

// 上移 Item（count 为移动多少格）
// min_idx/max_idx 为实际需要移动的 idx 的范围
template <typename T>
HRESULT		MoveUpItem(std::vector<T> &v, int index, __inout UINT &count, __out int &min_idx, __out int &max_idx)
{
	if(index < 0 || index >= (int)v.size())
		return E_INVALIDARG;

	int fix_count = std::clamp((int)count, 0, index);
	if(!fix_count)
		return E_FAIL;

	count = (UINT)fix_count;

	max_idx	= index - 1;
	min_idx	= index - fix_count;

	T* item_start = &v[0];
	T item = v[index];

	// 移动
	for(int i = max_idx; i >= min_idx; --i)
		item_start[i + 1] = item_start[i];
	item_start[min_idx] = item;

	return S_OK;
}

// 下移 Item（count 为移动多少格）
// min_idx/max_idx 为实际需要移动的 idx 的范围
template <typename T>
HRESULT		MoveDownItem(std::vector<T> &v, int index, __inout UINT &count, __out int &min_idx, __out int &max_idx)
{
	size_t v_size = v.size();

	if(index < 0 || index >= (int)v_size)
		return E_INVALIDARG;

	int fix_count = std::clamp((int)count, 0, (int)(v_size - 1) - index);
	if(fix_count == 0)
		return E_FAIL;

	count = (UINT)fix_count;

	min_idx = index;
	max_idx = index + fix_count;

	T* item_start = &v[0];
	T item = v[index];

	// 移动
	for(int i = min_idx; i < max_idx; ++i)
		item_start[i] = item_start[i + 1];
	item_start[max_idx] = item;

	return S_OK;
}

// 生成「添加列表」（new_sort_unique_array 存在，但 old_sort_unique_array 不存在）
template <typename T>
void	gen_Add_List(	const T			*new_sort_unique_array,
						size_t			new_sort_unique_array_size,
						const T			*old_sort_unique_array,
						size_t			old_sort_unique_array_size,
						__out T			*add_list,
						__out size_t	&add_list_count )
{
	add_list_count = 0;

	for(size_t i=0, m=0; i<new_sort_unique_array_size; ++i)
	{
		T val = new_sort_unique_array[i];

		if(	(m + 1 > old_sort_unique_array_size)	||
			val != old_sort_unique_array[m] )
		{
			add_list[add_list_count++] = val;
		}
		else
			++m;
	}	// for
}

// 生成「删除列表」（old_sort_unique_array 存在，但 new_sort_unique_array 不存在）
template <typename T>
void	gen_Del_List(	const T			*new_sort_unique_array,
						size_t			new_sort_unique_array_size,
						const T			*old_sort_unique_array,
						size_t			old_sort_unique_array_size,
						__out T			*del_list,
						__out size_t	&del_list_count )
{
	del_list_count = 0;

	for(size_t i=0, m=0; i<old_sort_unique_array_size; ++i)
	{
		T val = old_sort_unique_array[i];

		if(	(m + 1 > new_sort_unique_array_size)	||
			val != new_sort_unique_array[m] )
		{
			del_list[del_list_count++] = val;
		}
		else
			++m;
	}	// for
}

// 从一个数组中移除一些数值
template <typename T>
void	remove_values(	__inout T		*sort_unique_array,
						size_t			sort_unique_array_size,
						__out size_t	&new_size,
						const T			*sort_unique_vals,
						size_t			sort_unique_vals_count )
{
	if(sort_unique_array_size == 0 || sort_unique_vals_count == 0)
		return;

	new_size = sort_unique_array_size;

	size_t m = 0;

	int found_start_idx	= -1;
	int found_end_idx	= -1;

	for(size_t i=0; i<sort_unique_array_size; ++i)
	{
		for(; m<sort_unique_vals_count; ++m)
		{
			const T &val	= sort_unique_vals[m];
			const T &val2	= sort_unique_array[i];

			if(val == val2)
			{
				if(found_start_idx < 0)
				{
					found_start_idx	= i;
					found_end_idx	= i;
				}
				else
				{
					if(found_end_idx + 1 == i)
						found_end_idx = i;
					else
					{
						UINT count = i - found_end_idx - 1;
						memmove(sort_unique_array + found_start_idx,
								sort_unique_array + found_end_idx + 1,
								count * sizeof(T));

						found_start_idx	+= count;
						found_end_idx	= i;
					}
				}

				--new_size;
				//val2 = 0;
			}
			else if(val > val2)
				break;

			//if(val < val2)
			//	continue;
		}	// for

		if(m >= sort_unique_vals_count)
			break;
	}	// for

	if(new_size < sort_unique_array_size)
	{
		size_t count = (size_t)((sort_unique_array_size - 1) - found_end_idx);
		memmove(sort_unique_array + found_start_idx,
				sort_unique_array + found_end_idx + 1,
				count * sizeof(T));
	}
}

// 移除一个数组中的一些元素
template <typename T>
void	remove_ranges(	__inout T					*array_,
						size_t						array_size,
						__out size_t				&new_array_size,
						const struct Math::s_Ranges	&ranges )
{
	if(array_ == nullptr || array_size == 0)
		return;

	if(ranges.m_ranges.empty())
	{
		new_array_size = array_size;
		return;
	}

	// 构造新 ranges
	struct Math::s_Ranges new_ranges;

	struct s_Range64 all_range;
	all_range.m_min = 0;
	all_range.m_max = array_size - 1;

	new_ranges.add_range(all_range);

	for(auto &kvp : ranges.m_ranges)
		new_ranges.remove_range(kvp.second);

	// 处理数组内容
	size_t next_write_idx = 0;

	for(auto &kvp : new_ranges.m_ranges)
	{
		size_t data_len = kvp.second.m_max - kvp.second.m_min + 1;

		if(kvp.first == 0)
		{
			next_write_idx = data_len;
			continue;
		}

		_MEMMOVE(array_ + next_write_idx, array_ + kvp.first, data_len * sizeof(T));
		next_write_idx += data_len;
	}	// for

	new_array_size = next_write_idx;
}

}	// namespace Vector

namespace Map
{

// 生成「添加列表」（new_list 存在，但 old_list 不存在）
template <typename T_new, typename T_old, typename T_key>
void	gen_Add_List(const T_new &new_list, const T_old &old_list, __out std::vector<T_key> &add_list)
{
	add_list.clear();
	add_list.reserve(new_list.size());

	for(auto &kvp: new_list)
	{
		T_key key = (T_key)kvp.first;

		if(old_list.find(key) == old_list.end())
			add_list.push_back(std::move(key));
	}	// for
}

// 生成「删除列表」（old_list 存在，但 new_list 不存在）
template <typename T_new, typename T_old, typename T_key>
void	gen_Del_List(const T_new &new_list, const T_old &old_list, __out std::vector<T_key> &del_list)
{
	del_list.clear();
	del_list.reserve(old_list.size());

	for(auto &kvp: old_list)
	{
		T_key key = (T_key)kvp.first;

		if(new_list.find(key) == new_list.end())
			del_list.push_back(std::move(key));
	}	// for
}

}	// namespace Map

}	// namespace Algorithm
}	// namespace NNN

#endif	// _NNNLIB___ALGORITHM___ALGORITHM_H_
