//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : CoreDump
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNLIB___MISC___COREDUMP___COREDUMP_H_
#define _NNNLIB___MISC___COREDUMP___COREDUMP_H_

#include "s_CoreDump_settings.h"

namespace NNN
{
namespace Misc
{
namespace CoreDump
{

extern struct s_CoreDump_settings	g_CoreDump_settings;

// ��ʼ��/����
HRESULT			DoInit();
HRESULT			DoFinal();

// ���� core dump ���ɣ�Ĭ�Ϲرա�<filename>��<filenmae_add_datetime> �� Windows ��Ч��
NNN_API void	enable_core_dump(const struct s_CoreDump_settings &settings);

}	// namespace CoreDump
}	// namespace Misc
}	// namespace NNN

#endif	// _NNNLIB___MISC___COREDUMP___COREDUMP_H_
