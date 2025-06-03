//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : �������
//--------------------------------------------------------------------------------------

#pragma once

#include "Common.h"

namespace Common
{
namespace Encrypt
{

// ���� 2 ���ı����� AES �� Key/IV
NNN_API void	gen_KeyIV(	const WCHAR	*txt1,
							const WCHAR	*txt2,
							__out BYTE	Key[AES_KEY_LEN],
							__out BYTE	IV[AES_IV_LEN] );

// ����/���� header
inline BYTE	encode_header(BYTE header, BYTE xor_val)
{
	BYTE val = header ^ xor_val;
	BYTE ret = (val >> 3) + (val << 5);	// ѭ������

	return ret;
}
inline BYTE	decode_header(BYTE header, BYTE xor_val)
{
	BYTE val = (header << 3) + (header >> 5);	// ѭ������
	BYTE ret = val ^ xor_val;

	return ret;
}

}	// namespace Encrypt
}	// namespace Common
