﻿//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 数学库
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNLIB___MATH___MATH_H_
#define _NNNLIB___MATH___MATH_H_

#include <vector>

#include "../../common/common.h"

namespace NNN
{
namespace Math
{

// 返回不小于 val 并且是最接近 val 的 2 的次方的数
NNN_API UINT			next_p2(UINT val);

// 线性插值（rate：src_value 和 dst_value 之间的一个值，0.0 ~ 1.0）
NNN_API inline double	lerp(double src_value, double dst_value, double rate)
{
	rate = cap_value(rate, 0.0, 1.0);
	return src_value * (1.0 - rate) + dst_value * rate;
}

// 64 进制 <--> 10 进制
NNN_API std::string		Encode_64(UINT64 num);
NNN_API bool			Decode_64(const char *txt, __out UINT64 &num);

// 生成随机数（均匀分布）
NNN_API INT8			rand_INT8(INT8 min_val = 0,				INT8 max_val = CHAR_MAX);
NNN_API UINT8			rand_UINT8(UINT8 min_val = 0,			UINT8 max_val = UCHAR_MAX);
NNN_API short			rand_short(short min_val = 0,			short max_val = SHRT_MAX);
NNN_API USHORT			rand_USHORT(USHORT min_val = 0,			USHORT max_val = USHRT_MAX);
NNN_API int				rand_int(int min_val = 0,				int max_val = INT_MAX);
NNN_API UINT			rand_UINT(UINT min_val = 0,				UINT max_val = UINT_MAX);
NNN_API __int64			rand_Int64(__int64 min_val = INT64_MIN,	__int64 max_val = INT64_MAX);
NNN_API UINT64			rand_UINT64(UINT64 min_val = 0,			UINT64 max_val = UINT64_MAX);
NNN_API float			rand_float(float min_val = FLT_MIN,		float max_val = FLT_MAX);
NNN_API double			rand_double(double min_val = DBL_MIN,	double max_val = DBL_MAX);

}	// namespace Math
}	// namespace NNN

#endif	// _NNNLIB___MATH___MATH_H_
