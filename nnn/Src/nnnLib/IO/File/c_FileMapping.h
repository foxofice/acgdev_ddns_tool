//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 内存映射文件
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNLIB___IO___FILE___C_FILEMAPPING_H_
#define _NNNLIB___IO___FILE___C_FILEMAPPING_H_

#include <string>

#include "../../../common/common.h"

#if (NNN_PLATFORM != NNN_PLATFORM_WP8)

#include "../../Thread/s_CriticalSection.h"

namespace NNN
{
namespace IO
{
namespace File
{

enum struct es_FileMappingType : BYTE
{
	Unknown,
	ReadOnly,	// 读取
	ReadWrite,	// 读写
	CreateNew,	// 创建新文件
};

class c_FileMapping
{
public:
	// 构造函数/析构函数
	NNN_API								c_FileMapping();
	NNN_API								~c_FileMapping();

	// 初始化
	NNN_API HRESULT						Init(const WCHAR *filename, es_FileMappingType type);

	// 重置为初始状态（未初始化的状态）
	NNN_API void						Reset();

	// 映射/取消映射
	/*
		<size_to_write>：	m_type = CreateNew 时，表示新建文件的大小
							m_type = ReadWrite 时，	如果 size_to_write = 0，则映射整个文件内容；
													如果 size_to_write > 文件大小，则自动追加文件内容；
													如果 size_to_write < 文件大小，并不会截断内容。
	*/
	NNN_API void*						Map(UINT64 size_to_write = 0);
	NNN_API void						UnMap();

	// 获取映射的内存地址
	NNN_API inline void*				GetBuffer()		{ return m_buffer; }

	// 获取文件大小
	NNN_API inline UINT64				GetFileSize()	{ return m_file_size; }

	// 获取文件名
	NNN_API inline WCHAR*				GetFilename()	{ return m_filename; }

	// 获取 m_type
	NNN_API inline es_FileMappingType	GetType()		{ return m_type; }

protected:
	struct Thread::s_CriticalSection	m_cs_map;							// 锁定 Map()/UnMap()

	bool								m_is_mapping			= false;
	void								*m_buffer				= nullptr;	// 映射后的指针
	UINT64								m_file_size				= 0;		// 文件大小

	es_FileMappingType					m_type					= es_FileMappingType::Unknown;
	WCHAR								m_filename[MAX_PATH];

#if (NNN_PLATFORM == NNN_PLATFORM_WIN32)
	HANDLE								m_fileHandle			= nullptr;
	HANDLE								m_fileMapping			= nullptr;
#else
	int									m_fd					= -1;
	UINT64								m_map_size				= 0;
#endif	// NNN_PLATFORM_WIN32
};

}	// namespace File
}	// namespace IO
}	// namespace NNN

#endif	// NNN_PLATFORM

#endif	// _NNNLIB___IO___FILE___C_FILEMAPPING_H_
