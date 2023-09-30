//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 加密/解密
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNLIB___ENCRYPT___ENCRYPT_H_
#define _NNNLIB___ENCRYPT___ENCRYPT_H_

#include <vector>

#include "../../common/common.h"

namespace NNN
{
namespace Encrypt
{

// Rijndael 加密/解密（CFB 模式）
// 注意：output 长度跟 input 一致
NNN_API HRESULT Rijndael_Encrypt(const BYTE *input, size_t input_len, const BYTE Key[32], const BYTE IV[16], __out BYTE *output);
NNN_API HRESULT Rijndael_Decrypt(const BYTE *input, size_t input_len, const BYTE Key[32], const BYTE IV[16], __out BYTE *output);

}	// namespace Encrypt
}	// namespace NNN

#endif	// _NNNLIB___ENCRYPT___ENCRYPT_H_
