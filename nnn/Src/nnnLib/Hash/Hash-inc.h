//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 计算 Hash（包含文件）
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNLIB___HASH___HASH_INC_H_
#define _NNNLIB___HASH___HASH_INC_H_

namespace NNN
{
namespace Hash
{

#define CRC32_LEN		4
#define CRC64_LEN		8
#define MD5_LEN			16
#define RIPEMD160_LEN	20
#define SHA1_LEN		20
#define SHA256_LEN		32
#define SHA384_LEN		48
#define SHA512_LEN		64

#define MAX_HASH_LEN	64

enum struct es_HashType : unsigned char
{
	CRC32,
	CRC64,
	MD5,
	RIPEMD160,
	SHA1,
	SHA256,
	SHA384,
	SHA512,
};

}	// namespace Hash
}	// namespace NNN

#endif	// _NNNLIB___HASH___HASH_INC_H_
