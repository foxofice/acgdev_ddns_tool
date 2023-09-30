//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 分片处理数据
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNLIB___ALGORITHM___S_SLICELOOP_H_
#define _NNNLIB___ALGORITHM___S_SLICELOOP_H_

#include "../../common/common.h"

#include "../Buffer/s_Obj_Pool.h"
#include "../Time/Time_.h"

#include "Algorithm.h"

namespace NNN
{
namespace Algorithm
{

template <typename T_key, typename T_value>
struct s_SliceLoop
{
	// 分片处理数据（std::map 或 NNN_HASH_MAP）
	//	<max_count>	- 要处理的最大数量
	//	<interval>	- 下一次可以处理的时间间隔（ms）
	//	<no_used>	- nullptr 时，表示不移除符合条件的值
	template <typename T_Map>
	void	do_loop_map(T_Map											&map,
						size_t											max_count,
						UINT											interval,
						std::vector<std::pair<T_key, T_value*>>			&results,
						//__out_opt struct Buffer::s_Obj_Pool<T_value>	*pool,
						__out_opt bool									(*cond)(T_value *val))
	{
		if(NNN::Time::tick64() < m_can_loop_tick)
			return;

		results.clear();
		results.reserve(max_count);

		if(max_count == 0)
			return;

		if(map.empty())
			return;

		// 全部循环
		if(map.size() <= max_count)
		{
			for(auto &kvp : map)
			{
				if(cond != nullptr)
				{
					if(!cond(kvp.second))
						continue;
				}

				results.push_back({ kvp.first, kvp.second });

				//if(no_used != nullptr)
				//	no_used->release_object(kvp.second);
			}	// for

			m_have_next_key = false;
		}

		// 部分循环
		else
		{
			auto start = map.begin();

			if(m_have_next_key)
			{
				//start = map.upper_bound(m_next_key);
				start = map.find(m_next_key);
				if(start == map.end())
					start = map.begin();
			}

			size_t count = 0;

			// start -> end()
			for(auto iter = start; iter != map.end(); ++iter)
			{
				if(cond != nullptr)
				{
					if(!cond(iter->second))
						continue;
				}

				results.push_back({ iter->first, iter->second });

				//if(no_used != nullptr)
				//	no_used->release_object(iter->second);

				++count;

				if(count >= max_count)
					break;
			}	// for

			// begin() -> start
			if(count < max_count)
			{
				for(auto iter = map.begin(); iter != start; ++iter)
				{
					if(cond != nullptr)
					{
						if(!cond(iter->second))
							continue;
					}

					results.push_back({ iter->first, iter->second });

					//if(no_used != nullptr)
					//	no_used->release_object(iter->second);

					++count;

					if(count >= max_count)
						break;
				}	// for
			}

			// 计算 m_next_key
			m_have_next_key = false;

			if(!results.empty())
			{
				auto iter = map.find(results.back().first);
				++iter;

				if(iter != map.end())
				{
					m_have_next_key	= true;
					m_next_key		= iter->first;
				}
			}
		}

		// 清除数据
		//if(no_used != nullptr)
		//{
		//	if(results.size() == map.size())
		//	{
		//		map.clear();
		//	}
		//	else
		//	{
		//		if(!results.empty())
		//		{
		//			for(std::pair<T_key, T_value*> &kvp : results)
		//				map.erase(kvp.first);
		//		}
		//	}
		//}

		m_can_loop_tick = NNN::Time::tick64() + interval;
	}

	// 分片处理数据（std::vector）
	//	<count>		- 要处理的数量
	//	<interval>	- 下一次可以处理的时间间隔（ms）
	void	do_loop_vector(	std::vector<T_value*>							&vec,
							size_t											max_count,
							UINT											interval,
							std::vector<size_t>								&results,	// index[]
							//__out_opt struct Buffer::s_Obj_Pool<T_value>	*pool,
							__out_opt bool									(*cond)(T_value *val) )
	{
		if(NNN::Time::tick64() < m_can_loop_tick)
			return;

		results.clear();
		results.reserve(max_count);

		if(max_count == 0)
			return;

		if(vec.empty())
			return;

		size_t vec_size = vec.size();

		// 全部循环
		if(vec_size <= max_count)
		{
			for(size_t i=0; i<vec_size; ++i)
			{
				if(cond != nullptr)
				{
					if(!cond(vec[i]))
						continue;
				}

				results.push_back(i);

				//if(no_used != nullptr)
				//	no_used->release_object(vec[i]);
			}	// for

			m_have_next_index = false;
		}

		// 部分循环
		else
		{
			size_t start_index = 0;

			if(m_have_next_index)
			{
				start_index = m_next_index;

				if(start_index >= vec.size())
					start_index = 0;
			}

			size_t count = 0;

			// start_index -> vec_size
			for(size_t i=start_index; i<vec_size; ++i)
			{
				if(cond != nullptr)
				{
					if(!cond(vec[i]))
						continue;
				}

				results.push_back(i);

				//if(no_used != nullptr)
				//	no_used->release_object(vec[i]);

				++count;

				if(count >= max_count)
					break;
			}	// for

			// 0 -> start_index
			if(count < max_count)
			{
				for(size_t i=0; i<start_index; ++i)
				{
					if(cond != nullptr)
					{
						if(!cond(vec[i]))
							continue;
					}

					results.push_back(i);

					//if(no_used != nullptr)
					//	no_used->release_object(vec[i]);

					++count;

					if(count >= max_count)
						break;
				}	// for
			}

			// 计算 m_have_next_index
			m_have_next_index = false;

			if(!results.empty())
			{
				m_have_next_index	= true;
				m_next_index		= results.back() + 1 - results.size();

				if(m_next_index >= vec_size - results.size())
					m_have_next_index = false;
			}
		}

		// 清除数据
		//if(no_used != nullptr)
		//{
		//	if(results.size() == vec.size())
		//	{
		//		vec.clear();
		//	}
		//	else
		//	{
		//		if(!results.empty())
		//		{
		//			struct Math::s_Ranges ranges;
		//			for(size_t idx : results)
		//				ranges.add_range({ idx, idx });

		//			size_t new_vec_size = 0;

		//			Vector::remove_ranges(&vec[0], vec.size(), new_vec_size, ranges);

		//			vec.resize(new_vec_size);
		//		}
		//	}
		//}

		m_can_loop_tick = NNN::Time::tick64() + interval;
	}

	UINT64	m_can_loop_tick	= 0;			// 可以执行操作的 tick

	bool	m_have_next_key		= false;	// m_next_key 是否存在
	T_key	m_next_key;						// 下一次 loop 操作的 key

	bool	m_have_next_index	= false;	// m_next_index 是否存在
	size_t	m_next_index		= 0;		// 下一次 loop 操作的 index
};

}	// namespace Algorithm
}	// namespace NNN

#endif	// _NNNLIB___ALGORITHM___S_SLICELOOP_H_
