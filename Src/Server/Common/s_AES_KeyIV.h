//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : AES �� Key/IV �Ľṹ��
//--------------------------------------------------------------------------------------

#pragma once

#include "../../../3rdParty/nnn/Src/nnnLib/nnnLib.h"

#define AES_KEY_LEN	32
#define AES_IV_LEN	16

struct s_AES_KeyIV
{
	// ������������
	inline void recalc_KeyIV()
	{
		for(int i=0; i<(sizeof(m_Key) / sizeof(UINT64)); ++i)
		{
			UINT64 &val = ((UINT64*)m_Key)[i];
			val = NNN::Math::rand_UINT64();
		}	// for

		for(int i=0; i<(sizeof(m_IV) / sizeof(UINT64)); ++i)
		{
			UINT64 &val = ((UINT64*)m_IV)[i];
			val = NNN::Math::rand_UINT64();
		}	// for
	}

	BYTE	m_Key[AES_KEY_LEN]	= {};
	BYTE	m_IV[AES_IV_LEN]	= {};
};
