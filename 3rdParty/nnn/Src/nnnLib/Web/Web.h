//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : Web ���
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNLIB___WEB___WEB_H_
#define _NNNLIB___WEB___WEB_H_

#include "../../common/common.h"

namespace NNN
{
namespace Web
{

// ��ȡָ�� url �� html��check_cert = true ʱ����ʾ��������֤�飩
NNN_API bool	get_html(__out std::wstring &out_html, const char *url, bool check_cert = true);

}	// namespace Web
}	// namespace NNN

#endif	// _NNNLIB___WEB___WEB_H_
