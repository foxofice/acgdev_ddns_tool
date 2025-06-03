//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 压缩/解压
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNLIB___COMPRESS___COMPRESS_H_
#define _NNNLIB___COMPRESS___COMPRESS_H_

#include "Compress-inc.h"

#include "../../../3rdParty/7zip/Src/C/LzmaDec.h"

namespace NNN
{
namespace Compress
{

// 压缩/解压（LZMA）
NNN_API es_Result	compress_LZMA(	const BYTE							*src,
									size_t								src_len,
									__out BYTE							*compress_buffer,
									size_t								compress_buffer_len,
									__out size_t						&compress_len,
									__out BYTE							outProps[LZMA_PROPS_SIZE],
									__in_opt struct s_CompressParams	*pParams = nullptr );

NNN_API es_Result	uncompress_LZMA(const BYTE	*src,
									size_t		src_len,
									__out BYTE	*uncompress_buffer,
									size_t		uncompress_len,
									const BYTE	Props[LZMA_PROPS_SIZE]);

// 压缩/解压（zlib）
NNN_API es_Result	compress_zlib(	const BYTE							*src,
									size_t								src_len,
									__out BYTE							*compress_buffer,
									size_t								compress_buffer_len,
									__out size_t						&compress_len,
									__in_opt struct s_CompressParams	*pParams = nullptr );

NNN_API es_Result	uncompress_zlib(const BYTE	*src,
									size_t		src_len,
									__out BYTE	*uncompress_buffer,
									size_t		uncompress_len);

// 压缩/解压（LZMA2）
NNN_API es_Result	compress_LZMA2(	const BYTE							*src,
									size_t								src_len,
									__out BYTE							*compress_buffer,
									size_t								compress_buffer_len,
									__out size_t						&compress_len,
									__out BYTE							&outProps,
									__in_opt struct s_CompressParams	*pParams = nullptr );

NNN_API es_Result	uncompress_LZMA2(	const BYTE	*src,
										size_t		src_len,
										__out BYTE	*uncompress_buffer,
										size_t		uncompress_len,
										const BYTE	Props );

// 压缩/解压（zstd）
NNN_API es_Result	compress_zstd(	const BYTE							*src,
									size_t								src_len,
									__out BYTE							*compress_buffer,
									size_t								compress_buffer_len,
									__out size_t						&compress_len,
									__in_opt struct s_CompressParams	*pParams = nullptr );

NNN_API es_Result	uncompress_zstd(const BYTE	*src,
									size_t		src_len,
									__out BYTE	*uncompress_buffer,
									size_t		uncompress_len);

// 压缩/解压（lz4）
NNN_API es_Result	compress_lz4(	const BYTE							*src,
									size_t								src_len,
									__out BYTE							*compress_buffer,
									size_t								compress_buffer_len,
									__out size_t						&compress_len );

NNN_API es_Result	uncompress_lz4(	const BYTE	*src,
									size_t		src_len,
									__out BYTE	*uncompress_buffer,
									size_t		uncompress_len );

// 压缩/解压（通用函数）
NNN_API es_Result	compress(	const BYTE							*src,
								size_t								src_len,
								__out BYTE							*compress_buffer,
								size_t								compress_buffer_len,
								__out size_t						&compress_len,
								es_Algorithm						algorithm,
								__out_opt BYTE						outProps_LZMA[LZMA_PROPS_SIZE],
								__out_opt BYTE						&outProps_LZMA2,
								__in_opt struct s_CompressParams	*pParams = nullptr );

NNN_API es_Result	compress(	const BYTE							*src,
								size_t								src_len,
								__out BYTE							**compress_data,
								__out size_t						&compress_len,
								es_Algorithm						algorithm,
								__out_opt BYTE						outProps_LZMA[LZMA_PROPS_SIZE],
								__out_opt BYTE						&outProps_LZMA2,
								__in_opt struct s_CompressParams	*pParams = nullptr );

NNN_API es_Result	uncompress(	const BYTE		*src,
								size_t			src_len,
								__out BYTE		*uncompress_buffer,
								size_t			uncompress_len,
								es_Algorithm	algorithm,
								const BYTE		Props_LZMA[LZMA_PROPS_SIZE],
								const BYTE		Props_LZMA2 );

//NNN_API es_Result	uncompress(	const BYTE		*src,
//								size_t			src_len,
//								__out BYTE		**uncompress_data,
//								__out size_t	&dst_len,
//								es_Algorithm algorithm, const BYTE Props_LZMA[LZMA_PROPS_SIZE] );

// 计算压缩时需要的缓冲区长度（确保实际输出的压缩长度肯定小于此函数计算出来的长度）
// algorithm = Unknown 时，表示所有算法中的最大值
NNN_API size_t		compress_bound(size_t sourceLen, es_Algorithm algorithm);

}	// namespace Compress
}	// namespace NNN

#endif	// _NNNLIB___COMPRESS___COMPRESS_H_
