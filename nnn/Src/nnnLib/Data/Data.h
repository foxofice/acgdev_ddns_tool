//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : Data 接口
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNLIB___DATA___DATA_H_
#define _NNNLIB___DATA___DATA_H_

#include "c_DataRow.h"
#include "c_DataTable.h"
#include "c_DataSet.h"

namespace NNN
{
namespace Data
{

#define NNN_MAX_DATA_NAME_LEN	1024

// 读取 XML 到 DataTable 的函数（table_index 为要读取的表）
HRESULT ReadXML_Func(const BYTE *content, UINT len, bool fill_to_table, class c_DataTable *dt, class c_DataSet *ds);
HRESULT ReadXML_Func(const WCHAR *xml_filename, bool fill_to_table, class c_DataTable *dt, class c_DataSet *ds);

}	// namespace Data
}	// namespace NNN

#endif	// _NNNLIB___DATA___DATA_H_
