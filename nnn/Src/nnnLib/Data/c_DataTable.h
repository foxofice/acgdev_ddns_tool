//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : DataTable（数据表）
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNLIB___DATA___C_DATATABLE_H_
#define _NNNLIB___DATA___C_DATATABLE_H_

#include <vector>
#include <string>

#include "../../common/common.h"

#include "c_DataRow.h"

namespace NNN
{
namespace Data
{

class c_DataTable
{
public:
	// 构造函数/析构函数
	NNN_API						c_DataTable();
	NNN_API explicit			c_DataTable(const WCHAR *table_name);
	NNN_API						~c_DataTable();

	// 重置数据表为初始状态
	NNN_API void				Reset();

	// 设置/获取表名
	NNN_API void				SetTableName(const WCHAR *table_name);
	NNN_API inline std::wstring	GetTableName()	{ return m_TableName; }

	// 获取行/列数
	NNN_API inline size_t		RowsCount()		{ return m_Rows.size(); }
	NNN_API inline size_t		ColumnsCount()	{ return m_Columns.size(); }

	// 获取行
	NNN_API class c_DataRow*	Rows(size_t index);

	// 获取列
	NNN_API std::wstring		Columns(size_t index);
	NNN_API std::wstring		Columns(const WCHAR *column_name);

	// 获取列的集合
	NNN_API class std::vector<std::wstring>* Columns()	{ return &m_Columns; }

	// 获取列的索引（列不存在时，返回 -1）
	NNN_API int					ColumnsIndex(const WCHAR *column_name);

	// 添加列
	NNN_API HRESULT				AddColumn(const WCHAR *column_name);

	// 返回一行新行（如果没有使用 AddRow() 把这行添加到数据表，则需要手动释放此行）
	NNN_API class c_DataRow*	NewRow();

	// 添加一行
	NNN_API HRESULT				AddRow(class c_DataRow *row);

	// 读取 XML 到 DataTable
	NNN_API HRESULT				ReadXML(const WCHAR *xml_filename);
	NNN_API HRESULT				ReadXML(const BYTE *content, const UINT len);

	// 把 DataTable 写入到 XML
	NNN_API HRESULT				WriteXML(const WCHAR *xml_filename, const WCHAR *name, bool is_utf8 = true);

	// 把 DataTable 写入到 XML 的函数（仅填充数据，不实际写入）
			HRESULT				WriteXML_Func(	const WCHAR *xml_filename,
												void *xml,	// tinyxml2::XMLDocument* 类型
												void *root,	// tinyxml2::XMLElement* 类型
												bool is_utf8 = true );

	// 整理所有行（让每行数据的列最少为 columns_count）
			void				FixRows(size_t columns_count);

protected:
	std::wstring					m_TableName;	// 表名

	std::vector<std::wstring>		m_Columns;		// 列
	std::vector<class c_DataRow*>	m_Rows;			// 数据
};

}	// namespace Data
}	// namespace NNN

#endif	// _NNNLIB___DATA___C_DATATABLE_H_
