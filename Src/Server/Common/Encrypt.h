//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 加密相关
//--------------------------------------------------------------------------------------

#pragma once

#include "Common.h"

namespace Common
{
namespace Encrypt
{

// 根据 2 个文本生成 AES 的 Key/IV
NNN_API void	gen_KeyIV(	const WCHAR	*txt1,
							const WCHAR	*txt2,
							__out BYTE	Key[AES_KEY_LEN],
							__out BYTE	IV[AES_IV_LEN] );

// 编码/解码 header
inline BYTE	encode_header(BYTE header, BYTE xor_val)
{
	BYTE val = header ^ xor_val;
	BYTE ret = (val >> 3) + (val << 5);	// 循环右移

	return ret;
}
inline BYTE	decode_header(BYTE header, BYTE xor_val)
{
	BYTE val = (header << 3) + (header >> 5);	// 循环左移
	BYTE ret = val ^ xor_val;

	return ret;
}

}	// namespace Encrypt
}	// namespace Common
