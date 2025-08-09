//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//--------------------------------------------------------------------------------------

#include "Encrypt.h"

namespace Common
{
namespace Encrypt
{

/*==============================================================
 * 根据 2 个文本生成 AES 的 Key/IV
 * gen_KeyIV()
 *==============================================================*/
void gen_KeyIV(	const WCHAR	*txt1,
				const WCHAR	*txt2,
				__out BYTE	Key[AES_KEY_LEN],
				__out BYTE	IV[AES_IV_LEN] )
{
	if(	(txt1 == nullptr || txt1[0] == L'\0')	&&
		(txt2 == nullptr || txt2[0] == L'\0') )
	{
		ZeroMemory(Key, AES_KEY_LEN);
		ZeroMemory(IV, AES_IV_LEN);
		return;
	}

	BYTE								data[4096];
	struct NNN::Buffer::s_BinaryWriter	bw(data);

	bw.write_wchar2(txt1, wcslen(txt1));
	bw.write_wchar2(txt2, wcslen(txt2));

	BYTE sha384[NNN::Hash::SHA384_LEN];
	NNN::Hash::ComputeHash(NNN::Hash::es_HashType::SHA384, data, bw.m_offset, sha384);

	CopyMemory(Key,	sha384,					AES_KEY_LEN);
	CopyMemory(IV,	sha384 + AES_KEY_LEN,	AES_IV_LEN);
}

}	// namespace Encrypt
}	// namespace Common
