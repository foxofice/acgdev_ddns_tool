//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : WP8 需要包含的内容
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNN___COMMON___WP8_INC_H_
#define _NNN___COMMON___WP8_INC_H_

#include "common-macro.h"

#if (NNN_PLATFORM == NNN_PLATFORM_WP8)

#include <D3D11_1.h>

// OpenGL 部分
typedef unsigned int GLenum;
typedef unsigned int GLbitfield;
typedef unsigned int GLuint;
typedef int GLint;
typedef int GLsizei;
typedef unsigned char GLboolean;
typedef signed char GLbyte;
typedef short GLshort;
typedef unsigned char GLubyte;
typedef unsigned short GLushort;
typedef unsigned long GLulong;
typedef float GLfloat;
typedef float GLclampf;
typedef double GLdouble;
typedef double GLclampd;
typedef void GLvoid;
#if defined(_MSC_VER) && _MSC_VER < 1400
typedef __int64 GLint64EXT;
typedef unsigned __int64 GLuint64EXT;
#elif defined(_MSC_VER) || defined(__BORLANDC__)
typedef signed long long GLint64EXT;
typedef unsigned long long GLuint64EXT;
#else
typedef int64_t GLint64EXT;
typedef uint64_t GLuint64EXT;
#endif
typedef GLint64EXT  GLint64;
typedef GLuint64EXT GLuint64;

typedef char GLchar;

#endif	// NNN_PLATFORM_WP8

#endif	// _NNN___COMMON___WP8_INC_H_
