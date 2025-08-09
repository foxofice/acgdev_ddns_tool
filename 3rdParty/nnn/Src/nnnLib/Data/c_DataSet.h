//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : DataSet（数据集）
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNLIB___DATA___C_DATASET_H_
#define _NNNLIB___DATA___C_DATASET_H_

#include <vector>
#include <string>

#include "../../common/common.h"

#include "c_DataTable.h"

namespace NNN
{
namespace Data
{

class c_DataSet
{
public:
	// 构造函数/析构函数
	NNN_API						c_DataSet();
	NNN_API						~c_DataSet();

	// 重置数据集为初始状态
	NNN_API void				Reset();

	// 获取数据表
	NNN_API class c_DataTable*	Tables(size_t index);
	NNN_API class c_DataTable*	Tables(const WCHAR *table_name);

	// 获取数据表的集合
	NNN_API inline std::vector<class c_DataTable*>*	Tables()	{ return &m_Tables; }

	// 获取数据表的索引（列不存在时，返回 -1）
	NNN_API int					TableIndex(const WCHAR *table_name);

	// 获取表数量
	NNN_API inline size_t		TablesCount()	{ return m_Tables.size(); }

	// 添加一个表
	NNN_API HRESULT				AddTable(class c_DataTable *table);

	// 设置/获取数据集名字
	NNN_API inline void			SetDataSetName(const WCHAR *name)
	{
		m_DataSetName = (name == nullptr) ? L"" : name;
	}
	NNN_API inline const WCHAR*	GetDataSetName()	{ return m_DataSetName.c_str(); }

	// 读取 XML 到 DataSet
	NNN_API HRESULT				ReadXML(const WCHAR *xml_filename);
	NNN_API HRESULT				ReadXML(const BYTE *content, const UINT len);

	// 把 DataSet 写入到 XML
	NNN_API HRESULT				WriteXML(const WCHAR *xml_filename);

protected:
	std::wstring					m_DataSetName;	// 数据集名字
	std::vector<class c_DataTable*>	m_Tables;		// 表
};

}	// namespace Data
}	// namespace NNN

#endif	// _NNNLIB___DATA___C_DATASET_H_
