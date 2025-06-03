//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 作为 key 的 s_WStringKey（char*）（WCHAR*）
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNLIB___TEXT___S_WSTRINGKEY_H_
#define _NNNLIB___TEXT___S_WSTRINGKEY_H_

#include "../../common/common-macro.h"

namespace NNN
{
namespace Text
{

struct s_WStringKey
{
	// 构造函数/析构函数
	NNN_API				s_WStringKey();
	NNN_API				s_WStringKey(const WCHAR *str, bool is_path);

	// 重置数据
	NNN_API void		reset();

	// 设置字符串
	NNN_API void		set_string(const WCHAR *str, bool is_path);

	// 重载 < 运算符（用于 set/map）
	NNN_API inline bool	operator < (const struct s_WStringKey &s) const
	{
		return m_hash < s.m_hash;
	}

	// 重载 == 运算符（用于 hash_set/hash_map）
	NNN_API inline bool	operator == (const struct s_WStringKey &s) const
	{
		return m_hash == s.m_hash;
	}

	//NNN_API inline operator size_t() const
	//{
	//	return m_hash;
	//}

	size_t m_hash	= 0;
};

}	// namespace Text
}	// namespace NNN

namespace std
{
	template<> struct hash<struct NNN::Text::s_WStringKey>
	{
		size_t operator()(const struct NNN::Text::s_WStringKey &key) const
		{
			return key.m_hash;
		}
	};
}

#endif	// _NNNLIB___TEXT___S_WSTRINGKEY_H_
