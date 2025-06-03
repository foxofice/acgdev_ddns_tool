//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : DataRow（数据行）类
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNLIB___DATA___C_DATAROW_H_
#define _NNNLIB___DATA___C_DATAROW_H_

#include <vector>
#include <string>

#include "../../common/common.h"
#include "c_DataTable.h"

namespace NNN
{
namespace Data
{

class c_DataRow
{
public:
	// 构造函数/析构函数
	NNN_API c_DataRow()		{}
	NNN_API ~c_DataRow()	{}

	// 获取数据
	NNN_API inline std::vector<std::wstring>*	GetData()	{ return &m_data; }

	// 获取某列的数据
	NNN_API std::wstring						Items(size_t column_index);
	NNN_API std::wstring						Items(const WCHAR *column_name);

	// 设置某列的数据
	NNN_API void								SetItem(size_t column_index, const WCHAR *data);
	NNN_API void								SetItem(const WCHAR *column_name, const WCHAR *data);

	// 设置/获取所属的数据表
	NNN_API	HRESULT								SetDataTable(class c_DataTable *table);
	NNN_API	inline class c_DataTable*			GetDataTable() { return m_DataTable; }

protected:
	class c_DataTable			*m_DataTable	= nullptr;	// 所属的数据表
	std::vector<std::wstring>	m_data;						// 数据
};

}	// namespace Data
}	// namespace NNN

#endif	// _NNNLIB___DATA___C_DATAROW_H_
