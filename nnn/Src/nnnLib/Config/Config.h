//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 配置文件
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNLIB___CONFIG___CONFIG_H_
#define _NNNLIB___CONFIG___CONFIG_H_

#include "../../common/common.h"

namespace NNN
{
namespace Config
{

struct s_Config_List
{
	// 构造函数/析构函数
	NNN_API ~s_Config_List()
	{
		SAFE_DELETE_ARRAY(m_config_name_list);
		SAFE_DELETE_ARRAY(m_config_value_list);
		SAFE_DELETE_ARRAY(m_buffer);
	}

	WCHAR	**m_config_name_list	= nullptr;
	WCHAR	**m_config_value_list	= nullptr;
	UINT	m_count					= 0;

	WCHAR	*m_buffer				= nullptr;
};

// 读取配置文件
NNN_API HRESULT Read_Config(const WCHAR *filename, __out struct s_Config_List &config_list);
NNN_API HRESULT Read_Config(const BYTE *input, size_t input_len, __out struct s_Config_List &config_list);

}	// namespace Config
}	// namespace NNN

#endif	// _NNNLIB___CONFIG___CONFIG_H_
