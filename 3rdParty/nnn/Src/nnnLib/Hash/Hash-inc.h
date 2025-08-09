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

constexpr int CRC32_LEN			= 4;
constexpr int CRC64_LEN			= 8;
constexpr int MD5_LEN			= 16;
constexpr int RIPEMD160_LEN		= 20;
constexpr int SHA1_LEN			= 20;
constexpr int MD5_SHA1_LEN		= MD5_LEN + SHA1_LEN;
constexpr int SHA224_LEN		= 28;
constexpr int SHA256_LEN		= 32;
constexpr int SHA384_LEN		= 48;
constexpr int SHA512_LEN		= 64;
constexpr int SHA512_224_LEN	= 28;
constexpr int SHA512_256_LEN	= 32;

constexpr int SHA3_224_LEN		= 28;
constexpr int SHA3_256_LEN		= 32;
constexpr int SHA3_384_LEN		= 48;
constexpr int SHA3_512_LEN		= 64;

constexpr int SM3_LEN			= 32;
constexpr int BLAKE2S_LEN		= 32;
constexpr int BLAKE2B_LEN		= 64;
constexpr int WHIRLPOOL_LEN		= 64;

constexpr int XXH32_LEN			= 4;
constexpr int XXH64_LEN			= 8;
constexpr int XXH3_LEN			= 8;
constexpr int XXH128_LEN		= 16;

constexpr int MAX_HASH_LEN		= 64;

enum struct es_HashType : unsigned char
{
	CRC32,
	CRC64,
	MD5,
	RIPEMD160,
	SHA1,
	MD5_SHA1,	// 先 MD5 再 SHA1，拼接成 36 字节
	SHA224,
	SHA256,
	SHA384,
	SHA512,
	SHA512_224,	// 初始向量 IV 224 位，压缩函数 SHA-512，输出 224 位
	SHA512_256,	// 初始向量 IV 256 位，压缩函数 SHA-512，输出 256 位

	SHA3_224,
	SHA3_256,
	SHA3_384,
	SHA3_512,

	SM3,
	BLAKE2s_256,
	BLAKE2b_512,
	BLAKE2sp,
	BLAKE2bp,
	Whirlpool,

	XXH32,
	XXH64,
	XXH3,	// XXH3_64bits
	XXH128,	// XXH3_128bits
};

}	// namespace Hash
}	// namespace NNN

#endif	// _NNNLIB___HASH___HASH_INC_H_
