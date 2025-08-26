//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : ע���˳�����
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNLIB___MISC___ATEXIT_H_
#define _NNNLIB___MISC___ATEXIT_H_

#include "../../common/common.h"

namespace NNN
{
namespace Misc
{

using CleanupFunc = void(*)();

// ע���˳�����
/*
Win ���Ѿ�������Щ�رտ���̨���ڵķ�ʽ��Ctrl + C��Ctrl + Break����� X
���ǡ���� X�������޷�����ִ��ע��� clean_up ������Win ����һ�������ڣ���� 5 �룩ֱ�� TerminateProcess() ���ٴ���
*/
NNN_API void atexit(CleanupFunc func);

}	// namespace Misc
}	// namespace NNN

#endif	// _NNNLIB___MISC___ATEXIT_H_
