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
#include <utility>	// std::forward

#include "../../common/common.h"
#include "../Buffer/s_Obj_Pool.h"
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

private:
	DISALLOW_COPY_AND_ASSIGN(s_array_ptr);
};

// 指针（自动释放）
template <typename T>
struct s_ptr
{
	inline s_ptr(T *p = nullptr)	{ m_p = p; }
	inline ~s_ptr()					{ SAFE_DELETE(m_p); }

	T	*m_p;

private:
	DISALLOW_COPY_AND_ASSIGN(s_ptr);
};

// 文本数组（只分配一次内存）
struct s_TxtArrayA
{
	// 构造函数/析构函数
	inline s_TxtArrayA()
	{
	}
	NNN_API ~s_TxtArrayA()
	{
		SAFE_DELETE_ARRAY(m_txt_list);
		SAFE_DELETE_ARRAY(m_buffer);
	}

	char	**m_txt_list	= nullptr;
	UINT	m_list_count	= 0;

	char	*m_buffer		= nullptr;

private:
	DISALLOW_COPY_AND_ASSIGN(s_TxtArrayA);
};

// 文本数组（只分配一次内存）
struct s_TxtArrayW
{
	// 构造函数/析构函数
	inline s_TxtArrayW()
	{
	}
	NNN_API ~s_TxtArrayW()
	{
		SAFE_DELETE_ARRAY(m_txt_list);
		SAFE_DELETE_ARRAY(m_buffer);
	}

	WCHAR	**m_txt_list	= nullptr;
	UINT	m_list_count	= 0;

	WCHAR	*m_buffer		= nullptr;

private:
	DISALLOW_COPY_AND_ASSIGN(s_TxtArrayW);
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

private:
	DISALLOW_COPY_AND_ASSIGN(s_TxtListA);
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

private:
	DISALLOW_COPY_AND_ASSIGN(s_TxtListW);
};

// 自动执行 CleanUp 清理
template <typename FUNC>
class c_CleanUp
{
public:
	inline explicit	c_CleanUp(FUNC&& func) : m_func(std::forward<FUNC>(func)) {}
	inline			~c_CleanUp() { m_func(); }

private:
	FUNC	m_func;

private:
	DISALLOW_COPY_AND_ASSIGN(c_CleanUp);
};

}	// namespace STL
}	// namespace NNN

#endif	// _NNNLIB___STL___STL_H_
