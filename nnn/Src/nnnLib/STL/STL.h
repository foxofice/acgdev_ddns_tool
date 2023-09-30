//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 用来替代 c++ STL 的东西
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNLIB___STL___STL_H_
#define _NNNLIB___STL___STL_H_

#include <vector>
#include <functional>

#include "../../common/common.h"
#include "../Buffer/s_Obj_Pool.h"
#include "../Text/s_StringKey.h"
#include "../Thread/s_CriticalSection.h"
#include "../Thread/c_Lock.h"

namespace NNN
{
namespace STL
{

// 数组（自动释放）
template <typename T>
struct s_array_ptr
{
	inline s_array_ptr(T *p = nullptr)	{ m_p = p; }
	inline ~s_array_ptr()				{ SAFE_DELETE_ARRAY(m_p); }

	T	*m_p;
};

// 指针（自动释放）
template <typename T>
struct s_ptr
{
	inline s_ptr(T *p = nullptr)	{ m_p = p; }
	inline ~s_ptr()					{ SAFE_DELETE(m_p); }

	T	*m_p;
};

// 文本数组（只分配一次内存）
struct s_TxtArrayA
{
	// 构造函数/析构函数
	NNN_API ~s_TxtArrayA()
	{
		SAFE_DELETE_ARRAY(m_txt_list);
		SAFE_DELETE_ARRAY(m_buffer);
	}

	char	**m_txt_list	= nullptr;
	UINT	m_list_count	= 0;

	char	*m_buffer		= nullptr;
};

// 文本数组（只分配一次内存）
struct s_TxtArrayW
{
	// 构造函数/析构函数
	NNN_API ~s_TxtArrayW()
	{
		SAFE_DELETE_ARRAY(m_txt_list);
		SAFE_DELETE_ARRAY(m_buffer);
	}

	WCHAR	**m_txt_list	= nullptr;
	UINT	m_list_count	= 0;

	WCHAR	*m_buffer		= nullptr;
};

// 文本数组（分配多次内存）
struct s_TxtListA
{
	// 构造函数/析构函数
	NNN_API	s_TxtListA(size_t reserve_size)
	{
		m_txt_list.reserve(reserve_size);
	}
	NNN_API	~s_TxtListA()
	{
		for(char *txt : m_txt_list)
		{
			SAFE_DELETE_ARRAY(txt);
		}
	}

	// 添加新 txt
	NNN_API	void	add_txt(const char *txt)
	{
		if(txt == nullptr)
			return;

		size_t txt_len = strlen(txt);

		char *add_txt = new char[txt_len + 1];
		CopyMemory(add_txt, txt, txt_len + 1);

		if(m_txt_list.size() == m_txt_list.capacity())
			m_txt_list.reserve(m_txt_list.size() * 2);

		m_txt_list.push_back(add_txt);
	}

	std::vector<char*>	m_txt_list;
};

// 文本数组（分配多次内存）
struct s_TxtListW
{
	// 构造函数/析构函数
	NNN_API	s_TxtListW(size_t reserve_size)
	{
		m_txt_list.reserve(reserve_size);
	}
	NNN_API	~s_TxtListW()
	{
		for(WCHAR *txt : m_txt_list)
		{
			SAFE_DELETE_ARRAY(txt);
		}
	}

	// 添加新 txt
	NNN_API	void	add_txt(const WCHAR *txt)
	{
		if(txt == nullptr)
			return;

		size_t txt_len = wcslen(txt);

		WCHAR *add_txt = new WCHAR[txt_len + 1];
		CopyMemory(add_txt, txt, (txt_len + 1) * sizeof(WCHAR));

		if(m_txt_list.size() == m_txt_list.capacity())
			m_txt_list.reserve(m_txt_list.size() * 2);

		m_txt_list.push_back(add_txt);
	}

	std::vector<WCHAR*>	m_txt_list;
};

// 无碰撞的 hash_map
template <typename T_Value>
class c_Safe_HashmapA
{
public:
	struct s_Value
	{
		std::string		m_key;
		T_Value			m_value;
		struct s_Value	*m_next_value	= nullptr;
	};

	// 构造函数/析构函数
	inline			~c_Safe_HashmapA()
	{
		for(auto &kvp : m_hashmap)
		{
			struct s_Value *val = kvp.second;
			SAFE_DELETE(val);
		}
	}

	// 添加
	inline bool		insert(const char *key, const T_Value &value)
	{
		bool ret = false;

		struct Text::s_StringKey str_key(key, false);

		Thread::c_Lock l(m_cs);

		auto iter = m_hashmap.find(str_key);
		if(iter == m_hashmap.end())
		{
			struct s_Value *val = m_values_pool.create_object();
			val->m_key		= key;
			val->m_value	= value;

			m_hashmap.insert({str_key, val});
			ret = true;
		}
		else
		{
			struct s_Value *first_val = iter->second;

			// 是否存在相同的 key
			auto have_same_key = [&]() -> bool
			{
				struct s_Value *tmp = first_val;

				while(true)
				{
					if(tmp->m_key == key)
						return true;

					if(tmp->m_next_value == nullptr)
						return false;

					tmp = tmp->m_next_value;
				}

				return false;
			};

			if(have_same_key())
				ret = false;
			else
			{
				struct s_Value *back_val = first_val;

				while(back_val->m_next_value != nullptr)
					back_val = back_val->m_next_value;

				struct s_Value *new_val = m_values_pool.create_object();
				new_val->m_key		= key;
				new_val->m_value	= value;

				back_val->m_next_value = new_val;
				ret = true;
			}
		}

		return ret;
	}

	// 移除
	inline bool		remove(const char *key, __out_opt T_Value *value)
	{
		struct Text::s_StringKey str_key(key, false);

		Thread::c_Lock l(m_cs);

		auto iter = m_hashmap.find(str_key);

		if(iter == m_hashmap.end())
			return false;

		struct s_Value *first_val	= iter->second;
		struct s_Value *tmp			= first_val;
		struct s_Value *prev_val	= nullptr;

		while(tmp != nullptr)
		{
			if(tmp->m_key == key)
			{
				if(tmp == first_val)
				{
					if(tmp->m_next_value == nullptr)
						m_hashmap.erase(iter);
					else
					{
						iter->second		= tmp->m_next_value;
						tmp->m_next_value	= nullptr;
					}
				}
				else
				{
					prev_val->m_next_value	= tmp->m_next_value;
					tmp->m_next_value		= nullptr;
				}

				if(value != nullptr)
					*value = tmp->m_value;

				m_values_pool.release_object(tmp);
				return true;
			}

			prev_val	= tmp;
			tmp			= tmp->m_next_value;
		}	// while

		return false;
	}

	// 查找
	inline T_Value*	find(const char *key)
	{
		struct Text::s_StringKey str_key(key, false);

		Thread::c_Lock l(m_cs);

		auto iter = m_hashmap.find(str_key);

		if(iter == m_hashmap.end())
			return nullptr;

		struct s_Value *tmp = iter->second;

		while(tmp != nullptr)
		{
			if(tmp->m_key == key)
				return &tmp->m_value;

			tmp = tmp->m_next_value;
		}	// while

		return nullptr;
	}

	// 清空
	inline void		clear()
	{
		Thread::c_Lock l(m_cs);

		for(auto &kvp : m_hashmap)
		{
			struct s_Value *val = kvp.second;
			m_values_pool.release_object(val);
		}

		m_hashmap.clear();
	}

	// 锁定/解锁 m_hashmap
	inline std::unordered_map<struct Text::s_StringKey, struct s_Value*>&	lock_hash_map()
	{
		m_cs.Lock();
		return m_hashmap;
	}
	inline void		unlock_hash_map()
	{
		m_cs.UnLock();
	}

protected:
	std::unordered_map<struct Text::s_StringKey, struct s_Value*>	m_hashmap;
	struct Thread::s_CriticalSection								m_cs;

	struct Buffer::s_Obj_Pool<struct s_Value>						m_values_pool;
};

// 自动执行 CleanUp
class c_CleanUp
{
public:
	inline c_CleanUp(std::function<void()> func) : m_func(func) {}
	inline ~c_CleanUp() { m_func(); }

private:
	std::function<void()> m_func;
};

}	// namespace STL
}	// namespace NNN

#endif	// _NNNLIB___STL___STL_H_
