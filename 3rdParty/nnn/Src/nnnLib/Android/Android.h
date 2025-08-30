//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : Android 相关
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNLIB___ANDROID___ANDROID_H_
#define _NNNLIB___ANDROID___ANDROID_H_

#include "../../common/common.h"

#ifdef NNN_ANDROID

#include <jni.h>
#include <android/log.h>
#include <android/asset_manager.h>

namespace NNN
{
namespace Android
{

// 日志记录
#define NNN_ANDROID_LOG_UNKNOWN(...)	__android_log_print(ANDROID_LOG_UNKNOWN,	"nnnEngine", __VA_ARGS__)
#define NNN_ANDROID_LOG_DEFAULT(...)	__android_log_print(ANDROID_LOG_DEFAULT,	"nnnEngine", __VA_ARGS__)
#define NNN_ANDROID_LOG_VERBOSE(...)	__android_log_print(ANDROID_LOG_VERBOSE,	"nnnEngine", __VA_ARGS__)
#define NNN_ANDROID_LOG_DEBUG(...)		__android_log_print(ANDROID_LOG_DEBUG,		"nnnEngine", __VA_ARGS__)
#define NNN_ANDROID_LOG_INFO(...)		__android_log_print(ANDROID_LOG_INFO,		"nnnEngine", __VA_ARGS__)
#define NNN_ANDROID_LOG_WARNING(...)	__android_log_print(ANDROID_LOG_WARN,		"nnnEngine", __VA_ARGS__)
#define NNN_ANDROID_LOG_ERROR(...)		__android_log_print(ANDROID_LOG_ERROR,		"nnnEngine", __VA_ARGS__)
#define NNN_ANDROID_LOG_FATAL(...)		__android_log_print(ANDROID_LOG_FATAL,		"nnnEngine", __VA_ARGS__)
#define NNN_ANDROID_LOG_SILENT(...)		__android_log_print(ANDROID_LOG_SILENT,		"nnnEngine", __VA_ARGS__)

extern AAssetManager *g_mgr;

// 设置/获取 AAssetManager*
inline void				SetAssetManager(AAssetManager *mgr)	{ g_mgr = mgr; }
inline AAssetManager*	GetAssetManager()					{ return g_mgr; }

}	// namespace Android
}	// namespace NNN

#endif	// NNN_ANDROID

#endif	// _NNNLIB___ANDROID___ANDROID_H_
