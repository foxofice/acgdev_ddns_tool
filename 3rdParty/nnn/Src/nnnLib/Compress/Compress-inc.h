//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 压缩/解压（包含文件）
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNLIB___COMPRESS___COMPRESS_INC_H_
#define _NNNLIB___COMPRESS___COMPRESS_INC_H_

#include "../../../3rdParty/zlib/Src/zlib.h"
#include "../../../3rdParty/zstd/Src/lib/zstd.h"

#include "../../common/common.h"

namespace NNN
{
namespace Compress
{

// 压缩算法
enum struct es_Algorithm : BYTE
{
	Unknown,	// （未选择）
	None,		// 仅打包

	LZMA,
	LZMA2,
	zlib,
	zstd,
	lz4,

	//（未实现）
	/*
	PPMd,	// 压缩文档时，压缩比超过 LZMA
	*/

	//（不实现）
	/*
	XZ,		// 主要使用 LZMA2
	LZW,	// LZ78 算法的变种
	LZ77,	// Deflate 就是 LZ77 的一种变种（zlib）
	bzip2,	// 压缩效果比 LZ77/LZ78 更好，但压缩速度较慢
	*/

	MAX,	// （最大值）
};

enum struct es_Result : BYTE
{
	OK,								// 成功
	Invalid_Arg,					// 无效参数
	OutputBuffer_Too_Small,			// 输出的缓冲区太小
	Not_Enough_Memory,				// 内存不足
	Multithreading_Functions_Error,	// 多线程函数错误（LZMA）
	Uncompress_Data_Error,			// 解压数据错误
	Unsupported_Properties,			// 不支持的属性（LZMA）
	Input_EOF,						// 输入缓冲区 src 需要更多数据（LZMA）
	Failed,							// 其他错误
};

// 压缩的详细参数
struct s_CompressParams
{
	struct
	{
		// 0~9（-1 = 默认）
		int	m_level	= Z_DEFAULT_COMPRESSION;
	} zlib;

	struct
	{
		// 1~ZSTD_MAX_CLEVEL（默认 = 3）
		// 0 = 默认（3）
		// < 0 时，表示 fast mode
		int	m_level	= ZSTD_CLEVEL_DEFAULT;
	} zstd;

	struct
	{
		/*
		  level dictSize algo  fb
			0:    16 KB   0    32
			1:    64 KB   0    32
			2:   256 KB   0    32
			3:     1 MB   0    32
			4:     4 MB   0    32
			5:    16 MB   1    32
			6:    32 MB   1    32
			7+:   64 MB   1    64
		*/
		int		m_level			= 5;	// 0~9

		// 默认 16M（对于 32bit，最大值为 1 << 27，即 128 MB；对于 64bit，最大值为 1 << 30，即 1 GB）
		// -1 表示根据 m_LZMA_level 自动计算
		int		m_dictSize		= -1;

		int		m_lc			= 3;	// 0~8（lc = 4 时，有时能给大文件增益效果）
		int		m_lp			= 0;	// 0~4
		int		m_pb			= 2;	// 0~4
		int		m_algo			= -1;	// 0 - fast, 1 - normal, 默认 = 1，-1 = 自动计算
		int		m_fb			= -1;	// 单词大小（默认 32，范围 5~273；-1 表示根据 m_LZMA_level 自动计算）
		int		m_btMode		= 1;	// 0 - hashChain Mode, 1 - binTree mode - normal, default = 1
		int		m_numHashBytes	= 4;	// 2, 3 or 4, default = 4
		UINT32	m_mc			= 32;	// 1 <= mc <= (1 << 30), default = 32

		// 正数表示线程数
		//		0	= CPU 线程
		//		-1	= CPU 线程数 - 1
		//		-2	= CPU 线程数 - 2
		//		……
		int		m_numThreads	= -1;
	} LZMA;
};

#define NNN_DEFLATE_COMPRESSBOUND(sourceLen)	(sourceLen)			+	\
												((sourceLen) >> 12)	+	\
												((sourceLen) >> 14)	+	\
												((sourceLen) >> 25)	+	\
												13

}	// namespace Compress
}	// namespace NNN

#endif	// _NNNLIB___COMPRESS___COMPRESS_INC_H_
