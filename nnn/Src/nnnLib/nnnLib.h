//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : nnnLib 接口
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNLIB___NNNLIB_H_
#define _NNNLIB___NNNLIB_H_

#include "nnnLib-macro.h"

#include "Algorithm/Algorithm.h"
#include "Algorithm/s_SliceLoop.h"
#include "Android/Android.h"
#include "Buffer/Buffer.h"
#include "Compress/Compress.h"
#include "Config/Config.h"
#include "Console/Console.h"
#include "Data/Data.h"
#include "Encrypt/Encrypt.h"
#include "Hash/Hash.h"
#include "IO/IO.h"
#include "Log/Log.h"
#include "Math/Math_.h"
#include "Math/s_Ranges.h"
#include "Misc/Misc.h"
#include "Net/Net.h"
#include "Process/Process.h"
#include "STL/STL.h"
#include "Text/Text.h"
#include "Text/s_StringKey.h"
#include "Text/s_WStringKey.h"
#include "Web/Web.h"

#include "Thread/Thread.h"
#include "Thread/s_AtomicLock.h"
#include "Thread/s_CriticalSection.h"
#include "Thread/c_ReadWriteLock.h"
#include "Thread/c_Lock.h"
#include "Thread/s_Thread.h"
#include "Thread/c_Tasks.h"

#include "Time/Time_.h"
#include "Time/c_Timer.h"

#include "Window/Window.h"

namespace NNN
{

// 初始化/清理
NNN_API HRESULT	DoInit();
NNN_API HRESULT	DoFinal();

}	// namespace NNN

#endif	// _NNNLIB___NNNLIB_H_
