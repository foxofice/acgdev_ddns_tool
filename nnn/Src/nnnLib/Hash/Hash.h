//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 计算 Hash
//（Hash 效率：md5 > crc32 > crc64 > sha1 > ripemd160 > sha256 > sha512 > sha384）
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNLIB___HASH___HASH_H_
#define _NNNLIB___HASH___HASH_H_

#include "../../common/common.h"

#include "Hash-inc.h"

namespace NNN
{
namespace Hash
{

// 计算 Hash 值
NNN_API HRESULT	ComputeHash(es_HashType type, const BYTE *input_data, size_t input_len, __out BYTE *output);
NNN_API HRESULT	ComputeHash(es_HashType type, const BYTE *input_data, size_t input_len, __out char *output);
NNN_API HRESULT	ComputeHash(es_HashType type, const BYTE *input_data, size_t input_len, __out WCHAR *output);

// 获取 Hash 的长度
NNN_API inline size_t	GetHashLen(es_HashType type)
{
	switch(type)
	{
	case es_HashType::CRC32:		return CRC32_LEN;
	case es_HashType::CRC64:		return CRC64_LEN;
	case es_HashType::MD5:			return MD5_LEN;
	case es_HashType::RIPEMD160:	return RIPEMD160_LEN;
	case es_HashType::SHA1:			return SHA1_LEN;
	case es_HashType::SHA256:		return SHA256_LEN;
	case es_HashType::SHA384:		return SHA384_LEN;
	case es_HashType::SHA512:		return SHA512_LEN;
	}

	return 0;
}

}	// namespace Hash
}	// namespace NNN

#endif	// _NNNLIB___HASH___HASH_H_
