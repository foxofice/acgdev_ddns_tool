//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : Data 接口
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNLIB___DATA___DATA_H_
#define _NNNLIB___DATA___DATA_H_

#include "c_DataTable.h"
#include "c_DataSet.h"

namespace NNN
{
namespace Data
{

#define NNN_MAX_DATA_NAME_LEN	1024

// 读取 XML 到 DataTable/DataSet 的函数
/*
 @content	XML 内容
 @len		<content> 的长度
 @DT		要填充的 DataTable
 @DS		要填充的 DataSet
 */
HRESULT ReadXML_Func(const BYTE *content, size_t len, __out_opt class c_DataSet *DS, __out_opt class c_DataTable *DT);

// 读取 XML 到 DataTable/DataSet 的函数
/*
 @xml_filename	XML 文件
 @DT			要填充的 DataTable
 @DS			要填充的 DataSet
 */
HRESULT ReadXML_Func(const WCHAR *xml_filename, __out_opt class c_DataSet *DS, __out_opt class c_DataTable *DT);

}	// namespace Data
}	// namespace NNN

#endif	// _NNNLIB___DATA___DATA_H_
