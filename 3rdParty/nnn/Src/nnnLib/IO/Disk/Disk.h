//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 磁盘相关
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNLIB___IO___DISK___DISK_H_
#define _NNNLIB___IO___DISK___DISK_H_

#include "../../../common/common.h"

namespace NNN
{
namespace IO
{
namespace Disk
{

// 获取磁盘大小信息
/*
范例：
	【Win32】GetDiskSpace(a, b, c, nullptr)、GetDiskSpace(a, b, c, L"C:")
	【Linux】GetDiskSpace(a, b, c, L"/")
*/
NNN_API HRESULT	GetDiskSpace(	__out UINT64 &FreeBytesAvailable,		// （非超级用户）可用字节数
								__out UINT64 &TotalNumberOfBytes,		// 该分区总大小
								__out UINT64 &TotalNumberOfFreeBytes,	// 该分区空闲字节数
								const WCHAR *DirectoryName = L"/" );	// 磁盘分区路径（可以兼容文件或目录所在磁盘分区）
NNN_API HRESULT	GetDiskSpace(	__out UINT64 &FreeBytesAvailable,		// （非超级用户）可用字节数
								__out UINT64 &TotalNumberOfBytes,		// 该分区总大小
								__out UINT64 &TotalNumberOfFreeBytes,	// 该分区空闲字节数
								const char *DirectoryName = "/" );		// 磁盘分区路径（可以兼容文件或目录所在磁盘分区）

}	// namespace Disk
}	// namespace IO
}	// namespace NNN

#endif	// _NNNLIB___IO___DISK___DISK_H_
