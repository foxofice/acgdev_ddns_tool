//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : s_Unknown 接口（模拟 COM）
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNN___COMMON___S_UNKNOWN_H_
#define _NNN___COMMON___S_UNKNOWN_H_

#include <string>

#include "common-macro.h"

namespace NNN
{

struct s_Unknown
{
	// 构造函数/析构函数
				s_Unknown()		{}
	virtual		~s_Unknown()	{}

	// 增加引用数
	virtual inline int	AddRef()
	{
		return ++m_ref_count;
	}

	// 减少引用数
	virtual inline int	Release()
	{
		if(--m_ref_count == 0)
		{
			delete this;
			return 0;
		}

		return m_ref_count;
	}

	// 获取引用数
	virtual inline int	GetRefCount()
	{
		return m_ref_count;
	}

protected:
	int	m_ref_count	= 1;	// 引用计数
};

}	// namespace NNN

#endif	// _NNN___COMMON___S_UNKNOWN_H_
