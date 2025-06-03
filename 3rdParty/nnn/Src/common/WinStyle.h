//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : Windows 的东西
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNN___COMMON___WINSTYLE_H_
#define _NNN___COMMON___WINSTYLE_H_

#include <stdio.h>

#include "common-macro.h"

#if (NNN_PLATFORM != NNN_PLATFORM_WIN32)
#include <string.h>

#if (NNN_PLATFORM == NNN_PLATFORM_WP8)
#include <windows.h>
#endif	// NNN_PLATFORM_WP8

//=====Added by FPE(2011.8.1)=====(新版 CopyMemory)(Start)
#undef RtlCopyMemory
//#if defined(WIN32) || defined(_WIN32)
//	#define RtlCopyMemory(Destination,Source,Length) memcpy_s((Destination),(Length),(Source),(Length))
//#else
	#define RtlCopyMemory(Destination,Source,Length) memcpy((Destination),(Source),(Length))
//#endif
//=====Added by FPE(2011.8.1)=====(新版 CopyMemory)(End)

#if (NNN_PLATFORM != NNN_PLATFORM_WIN32) && (NNN_PLATFORM != NNN_PLATFORM_WP8)

#if !defined(WIN32) && !defined(_WIN32)
#ifndef _stricmp
#define _stricmp strcasecmp
#endif
#endif

#ifndef _WINDOWS_
#define _WINDOWS_

#ifndef NO_STRICT
#ifndef STRICT
#define STRICT 1
#endif
#endif /* NO_STRICT */


#ifdef __GCC__	// gcc兼容
#define __int64 long long
#endif

#ifndef FLT_DIG
#define FLT_DIG	6
#endif

#ifndef FLT_MAX
#define FLT_MAX 3.40282347E+38F
#endif

#ifndef FLT_MIN
#define FLT_MIN 1.17549435E-38F
#endif

#ifndef DBL_MAX
#define DBL_MAX 1.7976931348623158e+308 // max value
#endif

#ifndef DBL_MIN
#define DBL_MIN 2.2250738585072014e-308 // min positive value
#endif

#ifndef BASETYPES
#define BASETYPES
//=====Modified by FPE(2014.3.20)=====(Linux x64)(Start)
//typedef unsigned long ULONG;
typedef unsigned int ULONG;
//=====Modified by FPE(2014.3.20)=====(Linux x64)(End)
typedef ULONG *PULONG;
typedef unsigned short USHORT;
typedef USHORT *PUSHORT;
typedef unsigned char UCHAR;
typedef UCHAR *PUCHAR;
typedef char *PSZ;
#endif  /* !BASETYPES */

#define MAX_PATH          260

#ifndef NULL
#ifdef __cplusplus
#define NULL    0
#else
#define NULL    ((void *)0)
#endif
#endif

#ifndef FALSE
#define FALSE               0
#endif

#ifndef TRUE
#define TRUE                1
#endif

#ifndef IN
#define IN
#endif

#ifndef OUT
#define OUT
#endif

#ifndef OPTIONAL
#define OPTIONAL
#endif

#undef far
#undef near
#undef pascal

#define far
#define near
#if (!defined(_MAC)) && ((_MSC_VER >= 800) || defined(_STDCALL_SUPPORTED))
#define pascal __stdcall
#else
#define pascal
#endif

#if defined(DOSWIN32) || defined(_MAC)
#define cdecl _cdecl
#ifndef CDECL
#define CDECL _cdecl
#endif
#else
#define cdecl
#ifndef CDECL
#define CDECL
#endif
#endif

#ifdef _MAC
#define CALLBACK    PASCAL
#define WINAPI      CDECL
#define WINAPIV     CDECL
#define APIENTRY    WINAPI
#define APIPRIVATE  CDECL
#ifdef _68K_
#define PASCAL      __pascal
#else
#define PASCAL
#endif
#elif (_MSC_VER >= 800) || defined(_STDCALL_SUPPORTED)
#define CALLBACK    __stdcall
#define WINAPI      __stdcall
#define WINAPIV     __cdecl
#define APIENTRY    WINAPI
#define APIPRIVATE  __stdcall
#define PASCAL      __stdcall
#else
#define CALLBACK
#define WINAPI
#define WINAPIV
#define APIENTRY    WINAPI
#define APIPRIVATE
#define PASCAL      pascal
#endif

#ifdef _M_CEE_PURE
#define WINAPI_INLINE  __clrcall
#else
#define WINAPI_INLINE  WINAPI
#endif

#undef FAR
#undef  NEAR
#define FAR                 far
#define NEAR                near
#ifndef CONST
#define CONST               const
#endif

//=====Modified by FPE(2014.3.20)=====(Linux x64)(Start)
//typedef unsigned long       DWORD;
typedef unsigned int       DWORD;
//=====Modified by FPE(2014.3.20)=====(Linux x64)(End)
#if (NNN_PLATFORM == NNN_PLATFORM_IOS)
	#if !defined(OBJC_HIDE_64) && TARGET_OS_IPHONE && __LP64__
		typedef bool BOOL;
	#else
		typedef signed char BOOL;
	#endif
#else
typedef int                 BOOL;
#endif
typedef unsigned char       BYTE;
typedef unsigned short      WORD;
typedef float               FLOAT;
typedef FLOAT               *PFLOAT;
typedef BOOL near           *PBOOL;
typedef BOOL far            *LPBOOL;
typedef BYTE near           *PBYTE;
typedef BYTE far            *LPBYTE;
typedef int near            *PINT;
typedef int far             *LPINT;
typedef WORD near           *PWORD;
typedef WORD far            *LPWORD;
//=====Modified by FPE(2014.3.20)=====(Linux x64)(Start)
//typedef long far            *LPLONG;
typedef int far            *LPLONG;
//=====Modified by FPE(2014.3.20)=====(Linux x64)(End)
typedef DWORD near          *PDWORD;
typedef DWORD far           *LPDWORD;
typedef void far            *LPVOID;
typedef CONST void far      *LPCVOID;

typedef int                 INT;
typedef unsigned int        UINT;
typedef unsigned int        *PUINT;

typedef BYTE                BOOLEAN;
typedef BOOLEAN             *PBOOLEAN;

//
// Basics
//

#ifndef VOID
#define VOID void
typedef char CHAR;
typedef short SHORT;
//=====Modified by FPE(2014.3.20)=====(Linux x64)(Start)
//typedef long LONG;
typedef int LONG;
//=====Modified by FPE(2014.3.20)=====(Linux x64)(End)
#if !defined(MIDL_PASS)
typedef int INT;
#endif
#endif

//
// UNICODE (Wide Character) types
//

#ifndef _MAC
typedef wchar_t WCHAR;    // wc,   16-bit UNICODE character
#else
// some Macintosh compilers don't define wchar_t in a convenient location, or define it as a char
typedef unsigned short WCHAR;    // wc,   16-bit UNICODE character
#endif

typedef WCHAR *PWCHAR, *LPWCH, *PWCH;
typedef CONST WCHAR *LPCWCH, *PCWCH;

typedef WCHAR *NWPSTR, *LPWSTR, *PWSTR;
typedef PWSTR *PZPWSTR;
typedef CONST PWSTR *PCZPWSTR;
//typedef WCHAR UNALIGNED *LPUWSTR, *PUWSTR;
typedef CONST WCHAR *LPCWSTR, *PCWSTR;
typedef PCWSTR *PZPCWSTR;
//typedef CONST WCHAR UNALIGNED *LPCUWSTR, *PCUWSTR;

typedef WCHAR *PZZWSTR;
typedef CONST WCHAR *PCZZWSTR;
//typedef WCHAR UNALIGNED *PUZZWSTR;
//typedef CONST WCHAR UNALIGNED *PCUZZWSTR;

typedef WCHAR *PNZWCH;
typedef CONST WCHAR *PCNZWCH;
//typedef WCHAR UNALIGNED *PUNZWCH;
//typedef CONST WCHAR UNALIGNED *PCUNZWCH;


//
// Handle to an Object
//

#ifdef STRICT
typedef void *HANDLE;
#if 0 && (_MSC_VER > 1000)
#define DECLARE_HANDLE(name) struct name##__; typedef struct name##__ *name
#else
#define DECLARE_HANDLE(name) struct name##__{int unused;}; typedef struct name##__ *name
#endif
#else
typedef PVOID HANDLE;
#define DECLARE_HANDLE(name) typedef HANDLE name
#endif
typedef HANDLE *PHANDLE;


//
// Flag (bit) fields
//

typedef BYTE   FCHAR;
typedef WORD   FSHORT;
typedef DWORD  FLONG;



// Component Object Model defines, and macros

#ifdef __cplusplus
    #define EXTERN_C    extern "C"
#else
    #define EXTERN_C    extern
#endif

#ifndef __in
#define __in
#endif

#ifndef __out
#define __out
#endif

#ifndef __inout
#define __inout
#endif

#ifndef __in_opt
#define __in_opt
#endif

#ifndef __out_opt
#define __out_opt
#endif

#ifndef __inout_opt
#define __inout_opt
#endif


//============================================================================
//   _In_\_Out_ Layer:
//============================================================================

// 'in' parameters --------------------------

// input pointer parameter
// e.g. void SetPoint( _In_ const POINT* pPT );
#ifndef _In_
#define _In_
#endif

#ifndef _In_opt_
#define _In_opt_
#endif

// nullterminated 'in' parameters.
// e.g. void CopyStr( _In_z_ const char* szFrom, _Out_z_cap_(cchTo) char* szTo, size_t cchTo );
#define _In_z_
#define _In_opt_z_

// 'input' buffers with given size

// e.g. void SetCharRange( _In_count_(cch) const char* rgch, size_t cch )
// valid buffer extent described by another parameter
#define _In_count_(size)
#define _In_opt_count_(size)
#define _In_bytecount_(size)
#define _In_opt_bytecount_(size)

// valid buffer extent described by a constant extression
#define _In_count_c_(size)
#define _In_opt_count_c_(size)
#define _In_bytecount_c_(size)
#define _In_opt_bytecount_c_(size)

// nullterminated  'input' buffers with given size

// e.g. void SetCharRange( _In_count_(cch) const char* rgch, size_t cch )
// nullterminated valid buffer extent described by another parameter
#define _In_z_count_(size)
#define _In_opt_z_count_(size)
#define _In_z_bytecount_(size)
#define _In_opt_z_bytecount_(size)

// nullterminated valid buffer extent described by a constant extression
#define _In_z_count_c_(size)
#define _In_opt_z_count_c_(size)
#define _In_z_bytecount_c_(size)
#define _In_opt_z_bytecount_c_(size)

// buffer capacity is described by another pointer
// e.g. void Foo( _In_ptrdiff_count_(pchMax) const char* pch, const char* pchMax ) { while pch < pchMax ) pch++; }
#define _In_ptrdiff_count_(size)
#define _In_opt_ptrdiff_count_(size)

// 'x' version for complex expressions that are not supported by the current compiler version
// e.g. void Set3ColMatrix( _In_count_x_(3*cRows) const Elem* matrix, int cRows );
#define _In_count_x_(size)
#define _In_opt_count_x_(size)
#define _In_bytecount_x_(size)
#define _In_opt_bytecount_x_(size)

// 'out' parameters --------------------------

// output pointer parameter
// e.g. void GetPoint( _Out_ POINT* pPT );
#ifndef _Out_
#define _Out_
#endif

#ifndef _Out_opt_
#define _Out_opt_
#endif

// 'out' with buffer size
// e.g. void GetIndeces( _Out_cap_(cIndeces) int* rgIndeces, size_t cIndices );
// buffer capacity is described by another parameter
#define _Out_cap_(size)
#define _Out_opt_cap_(size)
#define _Out_bytecap_(size)
#define _Out_opt_bytecap_(size)

// buffer capacity is described by a constant expression
#define _Out_cap_c_(size)
#define _Out_opt_cap_c_(size)
#define _Out_bytecap_c_(size)
#define _Out_opt_bytecap_c_(size)

// buffer capacity is described by another parameter multiplied by a constant expression
#define _Out_cap_m_(mult,size)
#define _Out_opt_cap_m_(mult,size)
#define _Out_z_cap_m_(mult,size)
#define _Out_opt_z_cap_m_(mult,size)

// buffer capacity is described by another pointer
// e.g. void Foo( _Out_ptrdiff_cap_(pchMax) char* pch, const char* pchMax ) { while pch < pchMax ) pch++; }
#define _Out_ptrdiff_cap_(size)
#define _Out_opt_ptrdiff_cap_(size)

// buffer capacity is described by a complex expression
#define _Out_cap_x_(size)
#define _Out_opt_cap_x_(size)
#define _Out_bytecap_x_(size)
#define _Out_opt_bytecap_x_(size)

// a zero terminated string is filled into a buffer of given capacity
// e.g. void CopyStr( _In_z_ const char* szFrom, _Out_z_cap_(cchTo) char* szTo, size_t cchTo );
// buffer capacity is described by another parameter
#define _Out_z_cap_(size)
#define _Out_opt_z_cap_(size)
#define _Out_z_bytecap_(size)
#define _Out_opt_z_bytecap_(size)

// buffer capacity is described by a constant expression
#define _Out_z_cap_c_(size)
#define _Out_opt_z_cap_c_(size)
#define _Out_z_bytecap_c_(size)
#define _Out_opt_z_bytecap_c_(size)

// buffer capacity is described by a complex expression
#define _Out_z_cap_x_(size)
#define _Out_opt_z_cap_x_(size)
#define _Out_z_bytecap_x_(size)
#define _Out_opt_z_bytecap_x_(size)

// a zero terminated string is filled into a buffer of given capacity
// e.g. size_t CopyCharRange( _In_count_(cchFrom) const char* rgchFrom, size_t cchFrom, _Out_cap_post_count_(cchTo,return)) char* rgchTo, size_t cchTo );
#define _Out_cap_post_count_(cap,count)
#define _Out_opt_cap_post_count_(cap,count)
#define _Out_bytecap_post_bytecount_(cap,count)
#define _Out_opt_bytecap_post_bytecount_(cap,count)

// a zero terminated string is filled into a buffer of given capacity
// e.g. size_t CopyStr( _In_z_ const char* szFrom, _Out_z_cap_post_count_(cchTo,return+1) char* szTo, size_t cchTo );
#define _Out_z_cap_post_count_(cap,count)
#define _Out_opt_z_cap_post_count_(cap,count)
#define _Out_z_bytecap_post_bytecount_(cap,count)
#define _Out_opt_z_bytecap_post_bytecount_(cap,count)

// only use with dereferenced arguments e.g. '*pcch' 
#define _Out_capcount_(capcount)
#define _Out_opt_capcount_(capcount)
#define _Out_bytecapcount_(capcount)
#define _Out_opt_bytecapcount_(capcount)

#define _Out_capcount_x_(capcount)
#define _Out_opt_capcount_x_(capcount)
#define _Out_bytecapcount_x_(capcount)
#define _Out_opt_bytecapcount_x_(capcount)

// e.g. GetString( _Out_z_capcount_(*pLen+1) char* sz, size_t* pLen );
#define _Out_z_capcount_(capcount)
#define _Out_opt_z_capcount_(capcount)
#define _Out_z_bytecapcount_(capcount)
#define _Out_opt_z_bytecapcount_(capcount)

// inout parameters ----------------------------

// inout pointer parameter
// e.g. void ModifyPoint( _Inout_ POINT* pPT );
#define _Inout_
#define _Inout_opt_

// string buffers
// e.g. void toupper( _Inout_z_ char* sz );
#define _Inout_z_
#define _Inout_opt_z_

// 'inout' buffers with initialized elements before and after the call
// e.g. void ModifyIndices( _Inout_count_(cIndices) int* rgIndeces, size_t cIndices );
#define _Inout_count_(size)
#define _Inout_opt_count_(size)
#define _Inout_bytecount_(size)
#define _Inout_opt_bytecount_(size)

#define _Inout_count_c_(size)
#define _Inout_opt_count_c_(size)
#define _Inout_bytecount_c_(size)
#define _Inout_opt_bytecount_c_(size)

// nullterminated 'inout' buffers with initialized elements before and after the call
// e.g. void ModifyIndices( _Inout_count_(cIndices) int* rgIndeces, size_t cIndices );
#define _Inout_z_count_(size)
#define _Inout_opt_z_count_(size)
#define _Inout_z_bytecount_(size)
#define _Inout_opt_z_bytecount_(size)

#define _Inout_z_count_c_(size)
#define _Inout_opt_z_count_c_(size)
#define _Inout_z_bytecount_c_(size)
#define _Inout_opt_z_bytecount_c_(size)

#define _Inout_ptrdiff_count_(size)
#define _Inout_opt_ptrdiff_count_(size)

#define _Inout_count_x_(size)
#define _Inout_opt_count_x_(size)
#define _Inout_bytecount_x_(size)
#define _Inout_opt_bytecount_x_(size)

// e.g. void AppendToLPSTR( _In_ LPCSTR szFrom, _Inout_cap_(cchTo) LPSTR* szTo, size_t cchTo );
#define _Inout_cap_(size)
#define _Inout_opt_cap_(size)
#define _Inout_bytecap_(size)
#define _Inout_opt_bytecap_(size)

#define _Inout_cap_c_(size)
#define _Inout_opt_cap_c_(size)
#define _Inout_bytecap_c_(size)
#define _Inout_opt_bytecap_c_(size)

#define _Inout_cap_x_(size)
#define _Inout_opt_cap_x_(size)
#define _Inout_bytecap_x_(size)
#define _Inout_opt_bytecap_x_(size)

// inout string buffers with writable size
// e.g. void AppendStr( _In_z_ const char* szFrom, _Inout_z_cap_(cchTo) char* szTo, size_t cchTo );
#define _Inout_z_cap_(size)
#define _Inout_opt_z_cap_(size)
#define _Inout_z_bytecap_(size)
#define _Inout_opt_z_bytecap_(size)

#define _Inout_z_cap_c_(size)
#define _Inout_opt_z_cap_c_(size)
#define _Inout_z_bytecap_c_(size)
#define _Inout_opt_z_bytecap_c_(size)

#define _Inout_z_cap_x_(size)
#define _Inout_opt_z_cap_x_(size)
#define _Inout_z_bytecap_x_(size)
#define _Inout_opt_z_bytecap_x_(size)

// return values -------------------------------

// returning pointers to valid objects
#define _Ret_
#define _Ret_opt_

// More _Ret_ annotations are defined below

// Pointer to pointers -------------------------

// e.g.  HRESULT HrCreatePoint( _Deref_out_opt_ POINT** ppPT );
#define _Deref_out_
#define _Deref_out_opt_
#define _Deref_opt_out_
#define _Deref_opt_out_opt_

// e.g.  void CloneString( _In_z_ const wchar_t* wzFrom, _Deref_out_z_ wchar_t** pWzTo );
#define _Deref_out_z_
#define _Deref_out_opt_z_
#define _Deref_opt_out_z_
#define _Deref_opt_out_opt_z_

// More _Deref_ annotations are defined below

// Other annotations

// Check the return value of a function e.g. _Check_return_ ErrorCode Foo();
#define _Check_return_

// e.g. MyPrintF( _Printf_format_string_ const wchar_t* wzFormat, ... );
#define _Printf_format_string_
#define _Scanf_format_string_
#define _Scanf_s_format_string_
#define _FormatMessage_format_string_

// <expr> indicates whether post conditions apply
#ifndef _Success_
#define _Success_(expr)
#endif

// annotations to express 'boundedness' of integral value parameter
#define _In_bound_
#define _Out_bound_
#define _Ret_bound_
#define _Deref_in_bound_
#define _Deref_out_bound_
#define _Deref_inout_bound_
#define _Deref_ret_bound_

// annotations to express upper and lower bounds of integral value parameter
#define _In_range_(lb,ub)
#define _Out_range_(lb,ub)
#define _Ret_range_(lb,ub)
#define _Deref_in_range_(lb,ub)
#define _Deref_out_range_(lb,ub)
#define _Deref_ret_range_(lb,ub)

//============================================================================
//   _Pre_\_Post_ Layer:
//============================================================================

//
// _Pre_ annotation ---
//
// describing conditions that must be met before the call of the function

// e.g. int strlen( _Pre_z_ const char* sz );
// buffer is a zero terminated string
#define _Pre_z_
#define _Pre_opt_z_

// e.g. void FreeMemory( _Pre_bytecap_(cb) _Post_ptr_invalid_ void* pv, size_t cb );
// buffer capacity described by another parameter
#define _Pre_cap_(size)
#define _Pre_opt_cap_(size)
#define _Pre_bytecap_(size)
#define _Pre_opt_bytecap_(size)

// buffer capacity described by a constant expression
#define _Pre_cap_c_(size)
#define _Pre_opt_cap_c_(size)
#define _Pre_bytecap_c_(size)
#define _Pre_opt_bytecap_c_(size)

// buffer capacity is described by another parameter multiplied by a constant expression
#define _Pre_cap_m_(mult,size)
#define _Pre_opt_cap_m_(mult,size)

// buffer capacity described by size of other buffer, only used by dangerous legacy APIs
// e.g. int strcpy(_Pre_cap_for_(src) char* dst, const char* src);
#define _Pre_cap_for_(param)
#define _Pre_opt_cap_for_(param)

// buffer capacity described by a complex condition
#define _Pre_cap_x_(size)
#define _Pre_opt_cap_x_(size)
#define _Pre_bytecap_x_(size)
#define _Pre_opt_bytecap_x_(size)

// buffer capacity described by the difference to another pointer parameter
#define _Pre_ptrdiff_cap_(ptr)
#define _Pre_opt_ptrdiff_cap_(ptr)

// e.g. void AppendStr( _Pre_z_ const char* szFrom, _Pre_z_cap_(cchTo) _Post_z_ char* szTo, size_t cchTo );
#define _Pre_z_cap_(size)
#define _Pre_opt_z_cap_(size)
#define _Pre_z_bytecap_(size)
#define _Pre_opt_z_bytecap_(size)

#define _Pre_z_cap_c_(size)
#define _Pre_opt_z_cap_c_(size)
#define _Pre_z_bytecap_c_(size)
#define _Pre_opt_z_bytecap_c_(size)

#define _Pre_z_cap_x_(size)
#define _Pre_opt_z_cap_x_(size)
#define _Pre_z_bytecap_x_(size)
#define _Pre_opt_z_bytecap_x_(size)

// known capacity and valid but unknown readable extent
#define _Pre_valid_cap_(size)
#define _Pre_opt_valid_cap_(size)
#define _Pre_valid_bytecap_(size)
#define _Pre_opt_valid_bytecap_(size)

#define _Pre_valid_cap_c_(size)
#define _Pre_opt_valid_cap_c_(size)
#define _Pre_valid_bytecap_c_(size)
#define _Pre_opt_valid_bytecap_c_(size)

#define _Pre_valid_cap_x_(size)
#define _Pre_opt_valid_cap_x_(size)
#define _Pre_valid_bytecap_x_(size)
#define _Pre_opt_valid_bytecap_x_(size)

// e.g. void AppendCharRange( _Pre_count_(cchFrom) const char* rgFrom, size_t cchFrom, _Out_z_cap_(cchTo) char* szTo, size_t cchTo );
// Valid buffer extent described by another parameter
#define _Pre_count_(size)
#define _Pre_opt_count_(size)
#define _Pre_bytecount_(size)
#define _Pre_opt_bytecount_(size)

// Valid buffer extent described by a constant expression
#define _Pre_count_c_(size)
#define _Pre_opt_count_c_(size)
#define _Pre_bytecount_c_(size)
#define _Pre_opt_bytecount_c_(size)

// Valid buffer extent described by a complex expression
#define _Pre_count_x_(size)
#define _Pre_opt_count_x_(size)
#define _Pre_bytecount_x_(size)
#define _Pre_opt_bytecount_x_(size)

// Valid buffer extent described by the difference to another pointer parameter
#define _Pre_ptrdiff_count_(ptr)
#define _Pre_opt_ptrdiff_count_(ptr)

// valid size unknown or indicated by type (e.g.:LPSTR)
#define _Pre_valid_
#define _Pre_opt_valid_

#define _Pre_invalid_

// used with allocated but not yet initialized objects
#define _Pre_notnull_
#define _Pre_maybenull_
#define _Pre_null_

// restrict access rights
#define _Pre_readonly_
#define _Pre_writeonly_
//
// _Post_ annotations ---
//
// describing conditions that hold after the function call

// void CopyStr( _In_z_ const char* szFrom, _Pre_cap_(cch) _Post_z_ char* szFrom, size_t cchFrom );
// buffer will be a zero-terminated string after the call
#define _Post_z_

// char * strncpy(_Out_cap_(_Count) _Post_maybez_ char * _Dest, _In_z_ const char * _Source, _In_ size_t _Count)
// buffer maybe zero-terminated after the call
#define _Post_maybez_

// e.g. SIZE_T HeapSize( _In_ HANDLE hHeap, DWORD dwFlags, _Pre_notnull_ _Post_bytecap_(return) LPCVOID lpMem );
#define _Post_cap_(size)
#define _Post_bytecap_(size)

// e.g. int strlen( _In_z_ _Post_count_(return+1) const char* sz );
#define _Post_count_(size)
#define _Post_bytecount_(size)
#define _Post_count_c_(size)
#define _Post_bytecount_c_(size)
#define _Post_count_x_(size)
#define _Post_bytecount_x_(size)

// e.g. size_t CopyStr( _In_z_ const char* szFrom, _Pre_cap_(cch) _Post_z_count_(return+1) char* szFrom, size_t cchFrom );
#define _Post_z_count_(size)
#define _Post_z_bytecount_(size)
#define _Post_z_count_c_(size)
#define _Post_z_bytecount_c_(size)
#define _Post_z_count_x_(size)
#define _Post_z_bytecount_x_(size)

// e.g. void free( _Post_ptr_invalid_ void* pv );
#define _Post_ptr_invalid_

// e.g. HRESULT InitStruct( _Post_valid_ Struct* pobj );
#define _Post_valid_
#define _Post_invalid_

// e.g. void ThrowExceptionIfNull( _Post_notnull_ const void* pv );
#define _Post_notnull_

//
// _Ret_ annotations
//
// describing conditions that hold for return values after the call

// e.g. _Ret_z_ CString::operator const wchar_t*() const throw();
#define _Ret_z_
#define _Ret_opt_z_

// e.g. _Ret_opt_bytecap_(cb) void* AllocateMemory( size_t cb );
// Buffer capacity is described by another parameter
#define _Ret_cap_(size)
#define _Ret_opt_cap_(size)
#define _Ret_bytecap_(size)
#define _Ret_opt_bytecap_(size)

// Buffer capacity is described by a constant expression
#define _Ret_cap_c_(size)
#define _Ret_opt_cap_c_(size)
#define _Ret_bytecap_c_(size)
#define _Ret_opt_bytecap_c_(size)

// Buffer capacity is described by a complex condition
#define _Ret_cap_x_(size)
#define _Ret_opt_cap_x_(size)
#define _Ret_bytecap_x_(size)
#define _Ret_opt_bytecap_x_(size)

// return value is nullterminated and capacity is given by another parameter
#define _Ret_z_cap_(size)
#define _Ret_opt_z_cap_(size)
#define _Ret_z_bytecap_(size)
#define _Ret_opt_z_bytecap_(size)

// e.g. _Ret_opt_bytecount_(cb) void* AllocateZeroInitializedMemory( size_t cb );
// Valid Buffer extent is described by another parameter
#define _Ret_count_(size)
#define _Ret_opt_count_(size)
#define _Ret_bytecount_(size)
#define _Ret_opt_bytecount_(size)

// Valid Buffer extent is described by a constant expression
#define _Ret_count_c_(size)
#define _Ret_opt_count_c_(size)
#define _Ret_bytecount_c_(size)
#define _Ret_opt_bytecount_c_(size)

// Valid Buffer extent is described by a complex expression
#define _Ret_count_x_(size)
#define _Ret_opt_count_x_(size)
#define _Ret_bytecount_x_(size)
#define _Ret_opt_bytecount_x_(size)

// return value is nullterminated and length is given by another parameter
#define _Ret_z_count_(size)
#define _Ret_opt_z_count_(size)
#define _Ret_z_bytecount_(size)
#define _Ret_opt_z_bytecount_(size)

// e.g. _Ret_opt_valid_ LPSTR void* CloneSTR( _Pre_valid_ LPSTR src );
#define _Ret_valid_
#define _Ret_opt_valid_

// used with allocated but not yet initialized objects
#define _Ret_notnull_
#define _Ret_maybenull_
#define _Ret_null_

//
// _Deref_pre_ ---
//
// describing conditions for array elements of dereferenced pointer parameters that must be met before the call

// e.g. void SaveStringArray( _In_count_(cStrings) _Deref_pre_z_ const wchar_t* const rgpwch[] );
#define _Deref_pre_z_
#define _Deref_pre_opt_z_

// e.g. void FillInArrayOfStr32( _In_count_(cStrings) _Deref_pre_cap_c_(32) _Deref_post_z_ wchar_t* const rgpwch[] );
// buffer capacity is described by another parameter
#define _Deref_pre_cap_(size)
#define _Deref_pre_opt_cap_(size)
#define _Deref_pre_bytecap_(size)
#define _Deref_pre_opt_bytecap_(size)

// buffer capacity is described by a constant expression
#define _Deref_pre_cap_c_(size)
#define _Deref_pre_opt_cap_c_(size)
#define _Deref_pre_bytecap_c_(size)
#define _Deref_pre_opt_bytecap_c_(size)

// buffer capacity is described by a complex condition
#define _Deref_pre_cap_x_(size)
#define _Deref_pre_opt_cap_x_(size)
#define _Deref_pre_bytecap_x_(size)
#define _Deref_pre_opt_bytecap_x_(size)

// convenience macros for nullterminated buffers with given capacity
#define _Deref_pre_z_cap_(size)
#define _Deref_pre_opt_z_cap_(size)
#define _Deref_pre_z_bytecap_(size)
#define _Deref_pre_opt_z_bytecap_(size)

#define _Deref_pre_z_cap_c_(size)
#define _Deref_pre_opt_z_cap_c_(size)
#define _Deref_pre_z_bytecap_c_(size)
#define _Deref_pre_opt_z_bytecap_c_(size)

#define _Deref_pre_z_cap_x_(size)
#define _Deref_pre_opt_z_cap_x_(size)
#define _Deref_pre_z_bytecap_x_(size)
#define _Deref_pre_opt_z_bytecap_x_(size)

// known capacity and valid but unknown readable extent
#define _Deref_pre_valid_cap_(size)
#define _Deref_pre_opt_valid_cap_(size)
#define _Deref_pre_valid_bytecap_(size)
#define _Deref_pre_opt_valid_bytecap_(size)

#define _Deref_pre_valid_cap_c_(size)
#define _Deref_pre_opt_valid_cap_c_(size)
#define _Deref_pre_valid_bytecap_c_(size)
#define _Deref_pre_opt_valid_bytecap_c_(size)

#define _Deref_pre_valid_cap_x_(size)
#define _Deref_pre_opt_valid_cap_x_(size)
#define _Deref_pre_valid_bytecap_x_(size)
#define _Deref_pre_opt_valid_bytecap_x_(size)

// e.g. void SaveMatrix( _In_count_(n) _Deref_pre_count_(n) const Elem** matrix, size_t n ); 
// valid buffer extent is described by another parameter
#define _Deref_pre_count_(size)
#define _Deref_pre_opt_count_(size)
#define _Deref_pre_bytecount_(size)
#define _Deref_pre_opt_bytecount_(size)

// valid buffer extent is described by a constant expression
#define _Deref_pre_count_c_(size)
#define _Deref_pre_opt_count_c_(size)
#define _Deref_pre_bytecount_c_(size)
#define _Deref_pre_opt_bytecount_c_(size)

// valid buffer extent is described by a complex expression
#define _Deref_pre_count_x_(size)
#define _Deref_pre_opt_count_x_(size)
#define _Deref_pre_bytecount_x_(size)
#define _Deref_pre_opt_bytecount_x_(size)

// e.g. void PrintStringArray( _In_count_(cElems) _Deref_pre_valid_ LPCSTR rgStr[], size_t cElems );
#define _Deref_pre_valid_
#define _Deref_pre_opt_valid_
#define _Deref_pre_invalid_

#define _Deref_pre_notnull_
#define _Deref_pre_maybenull_
#define _Deref_pre_null_

// restrict access rights
#define _Deref_pre_readonly_
#define _Deref_pre_writeonly_

//
// _Deref_post_ ---
//
// describing conditions for array elements or dereferenced pointer parameters that hold after the call

// e.g. void CloneString( _In_z_ const Wchar_t* wzIn _Out_ _Deref_post_z_ wchar_t** pWzOut );
#define _Deref_post_z_
#define _Deref_post_opt_z_

// e.g. HRESULT HrAllocateMemory( size_t cb, _Out_ _Deref_post_bytecap_(cb) void** ppv );
// buffer capacity is described by another parameter
#define _Deref_post_cap_(size)
#define _Deref_post_opt_cap_(size)
#define _Deref_post_bytecap_(size)
#define _Deref_post_opt_bytecap_(size)

// buffer capacity is described by a constant expression
#define _Deref_post_cap_c_(size)
#define _Deref_post_opt_cap_c_(size)
#define _Deref_post_bytecap_c_(size)
#define _Deref_post_opt_bytecap_c_(size)

// buffer capacity is described by a complex expression
#define _Deref_post_cap_x_(size)
#define _Deref_post_opt_cap_x_(size)
#define _Deref_post_bytecap_x_(size)
#define _Deref_post_opt_bytecap_x_(size)

// convenience macros for nullterminated buffers with given capacity
#define _Deref_post_z_cap_(size)
#define _Deref_post_opt_z_cap_(size)
#define _Deref_post_z_bytecap_(size)
#define _Deref_post_opt_z_bytecap_(size)

#define _Deref_post_z_cap_c_(size)
#define _Deref_post_opt_z_cap_c_(size)
#define _Deref_post_z_bytecap_c_(size)
#define _Deref_post_opt_z_bytecap_c_(size)

#define _Deref_post_z_cap_x_(size)
#define _Deref_post_opt_z_cap_x_(size)
#define _Deref_post_z_bytecap_x_(size)
#define _Deref_post_opt_z_bytecap_x_(size)

// known capacity and valid but unknown readable extent
#define _Deref_post_valid_cap_(size)
#define _Deref_post_opt_valid_cap_(size)
#define _Deref_post_valid_bytecap_(size)
#define _Deref_post_opt_valid_bytecap_(size)

#define _Deref_post_valid_cap_c_(size)
#define _Deref_post_opt_valid_cap_c_(size)
#define _Deref_post_valid_bytecap_c_(size)
#define _Deref_post_opt_valid_bytecap_c_(size)

#define _Deref_post_valid_cap_x_(size)
#define _Deref_post_opt_valid_cap_x_(size)
#define _Deref_post_valid_bytecap_x_(size)
#define _Deref_post_opt_valid_bytecap_x_(size)

// e.g. HRESULT HrAllocateZeroInitializedMemory( size_t cb, _Out_ _Deref_post_bytecount_(cb) void** ppv );
// valid buffer extent is described by another parameter
#define _Deref_post_count_(size)
#define _Deref_post_opt_count_(size)
#define _Deref_post_bytecount_(size)
#define _Deref_post_opt_bytecount_(size)

// buffer capacity is described by a constant expression
#define _Deref_post_count_c_(size)
#define _Deref_post_opt_count_c_(size)
#define _Deref_post_bytecount_c_(size)
#define _Deref_post_opt_bytecount_c_(size)

// buffer capacity is described by a complex expression
#define _Deref_post_count_x_(size)
#define _Deref_post_opt_count_x_(size)
#define _Deref_post_bytecount_x_(size)
#define _Deref_post_opt_bytecount_x_(size)

// e.g. void GetStrings( _Out_count_(cElems) _Deref_post_valid_ LPSTR const rgStr[], size_t cElems );
#define _Deref_post_valid_
#define _Deref_post_opt_valid_

#define _Deref_post_notnull_
#define _Deref_post_maybenull_
#define _Deref_post_null_

//
// _Deref_ret_ ---
//

#define _Deref_ret_z_
#define _Deref_ret_opt_z_

//
// special _Deref_ ---
//
#define _Deref2_pre_readonly_

// Convenience macros for more concise annotations

//
// _Pre_post ---
//
// describing conditions that hold before and after the function call

#define _Prepost_z_
#define _Prepost_opt_z_

#define _Prepost_count_(size)
#define _Prepost_opt_count_(size)
#define _Prepost_bytecount_(size)
#define _Prepost_opt_bytecount_(size)
#define _Prepost_count_c_(size)
#define _Prepost_opt_count_c_(size)
#define _Prepost_bytecount_c_(size)
#define _Prepost_opt_bytecount_c_(size)
#define _Prepost_count_x_(size)
#define _Prepost_opt_count_x_(size)
#define _Prepost_bytecount_x_(size)
#define _Prepost_opt_bytecount_x_(size)

#define _Prepost_valid_
#define _Prepost_opt_valid_

//
// _Deref_<both> ---
//
// short version for _Deref_pre_<ann> _Deref_post_<ann>
// describing conditions for array elements or dereferenced pointer parameters that hold before and after the call

#define _Deref_prepost_z_
#define _Deref_prepost_opt_z_

#define _Deref_prepost_cap_(size)
#define _Deref_prepost_opt_cap_(size)
#define _Deref_prepost_bytecap_(size)
#define _Deref_prepost_opt_bytecap_(size)

#define _Deref_prepost_cap_x_(size)
#define _Deref_prepost_opt_cap_x_(size)
#define _Deref_prepost_bytecap_x_(size)
#define _Deref_prepost_opt_bytecap_x_(size)

#define _Deref_prepost_z_cap_(size)
#define _Deref_prepost_opt_z_cap_(size)
#define _Deref_prepost_z_bytecap_(size)
#define _Deref_prepost_opt_z_bytecap_(size)

#define _Deref_prepost_valid_cap_(size)
#define _Deref_prepost_opt_valid_cap_(size)
#define _Deref_prepost_valid_bytecap_(size)
#define _Deref_prepost_opt_valid_bytecap_(size)

#define _Deref_prepost_valid_cap_x_(size)
#define _Deref_prepost_opt_valid_cap_x_(size)
#define _Deref_prepost_valid_bytecap_x_(size)
#define _Deref_prepost_opt_valid_bytecap_x_(size)

#define _Deref_prepost_count_(size)
#define _Deref_prepost_opt_count_(size)
#define _Deref_prepost_bytecount_(size)
#define _Deref_prepost_opt_bytecount_(size)

#define _Deref_prepost_count_x_(size)
#define _Deref_prepost_opt_count_x_(size)
#define _Deref_prepost_bytecount_x_(size)
#define _Deref_prepost_opt_bytecount_x_(size)

#define _Deref_prepost_valid_
#define _Deref_prepost_opt_valid_

//
// _Deref_<miscellaneous>
//
// used with references to arrays

#define _Deref_out_z_cap_c_(size)
#define _Deref_inout_z_cap_c_(size)
#define _Deref_out_z_bytecap_c_(size)
#define _Deref_inout_z_bytecap_c_(size)
#define _Deref_inout_z_


//
// _M_IX86 included so that EM CONTEXT structure compiles with
// x86 programs. *** TBD should this be for all architectures?
//

//
// 16 byte aligned type for 128 bit floats
//

//
// For we define a 128 bit structure and use __declspec(align(16)) pragma to
// align to 128 bits.
//

#if defined(_M_IA64) && !defined(MIDL_PASS)
__declspec(align(16))
#endif
typedef struct _FLOAT128 {
    __int64 LowPart;
    __int64 HighPart;
} FLOAT128;

typedef FLOAT128 *PFLOAT128;


//
// __int64 is only supported by 2.0 and later midl.
// __midl is set by the 2.0 midl and not by 1.0 midl.
//

#define _ULONGLONG_
#if (!defined (_MAC) && (!defined(MIDL_PASS) || defined(__midl)) && (!defined(_M_IX86) || (defined(_INTEGRAL_MAX_BITS) && _INTEGRAL_MAX_BITS >= 64)))
typedef __int64 LONGLONG;
typedef unsigned __int64 ULONGLONG;

#define MAXLONGLONG                         (0x7fffffffffffffff)


#else

#if defined(_MAC) && defined(_MAC_INT_64)
typedef __int64 LONGLONG;
typedef unsigned __int64 ULONGLONG;

#define MAXLONGLONG                      (0x7fffffffffffffff)


#else
typedef double LONGLONG;
typedef double ULONGLONG;
#endif //_MAC and int64

#endif

typedef LONGLONG *PLONGLONG;
typedef ULONGLONG *PULONGLONG;

// Update Sequence Number

typedef LONGLONG USN;

#if defined(MIDL_PASS)
typedef struct _LARGE_INTEGER {
#else // MIDL_PASS
typedef union _LARGE_INTEGER {
    struct {
        DWORD LowPart;
        LONG HighPart;
    } DUMMYSTRUCTNAME;
    struct {
        DWORD LowPart;
        LONG HighPart;
    } u;
#endif //MIDL_PASS
    LONGLONG QuadPart;
} LARGE_INTEGER;

typedef LARGE_INTEGER *PLARGE_INTEGER;

#if defined(MIDL_PASS)
typedef struct _ULARGE_INTEGER {
#else // MIDL_PASS
typedef union _ULARGE_INTEGER {
    struct {
        DWORD LowPart;
        DWORD HighPart;
    } DUMMYSTRUCTNAME;
    struct {
        DWORD LowPart;
        DWORD HighPart;
    } u;
#endif //MIDL_PASS
    ULONGLONG QuadPart;
} ULARGE_INTEGER;

typedef ULARGE_INTEGER *PULARGE_INTEGER;

// end_ntminiport end_ntndis end_ntminitape


#ifdef __cplusplus
extern "C" {
#endif

typedef signed char         INT8, *PINT8;
typedef signed short        INT16, *PINT16;
typedef signed int          INT32, *PINT32;
typedef signed __int64      INT64, *PINT64;
typedef unsigned char       UINT8, *PUINT8;
typedef unsigned short      UINT16, *PUINT16;
typedef unsigned int        UINT32, *PUINT32;
typedef unsigned __int64    UINT64, *PUINT64;

//
// The following types are guaranteed to be signed and 32 bits wide.
//

typedef signed int LONG32, *PLONG32;

//
// The following types are guaranteed to be unsigned and 32 bits wide.
//

typedef unsigned int ULONG32, *PULONG32;
typedef unsigned int DWORD32, *PDWORD32;

#if !defined(_W64)
#if !defined(__midl) && (defined(_X86_) || defined(_M_IX86)) && _MSC_VER >= 1300
#define _W64 __w64
#else
#define _W64
#endif
#endif

//
// The INT_PTR is guaranteed to be the same size as a pointer.  Its
// size with change with pointer size (32/64).  It should be used
// anywhere that a pointer is cast to an integer type. UINT_PTR is
// the unsigned variation.
//
// __int3264 is intrinsic to 64b MIDL but not to old MIDL or to C compiler.
//
#if ( 501 < __midl )

    typedef [public] __int3264 INT_PTR, *PINT_PTR;
    typedef [public] unsigned __int3264 UINT_PTR, *PUINT_PTR;

    typedef [public] __int3264 LONG_PTR, *PLONG_PTR;
    typedef [public] unsigned __int3264 ULONG_PTR, *PULONG_PTR;

#else  // midl64
// old midl and C++ compiler

#if defined(_WIN64)
    typedef __int64 INT_PTR, *PINT_PTR;
    typedef unsigned __int64 UINT_PTR, *PUINT_PTR;

    typedef __int64 LONG_PTR, *PLONG_PTR;
    typedef unsigned __int64 ULONG_PTR, *PULONG_PTR;

    #define __int3264   __int64

#else
    typedef _W64 int INT_PTR, *PINT_PTR;
    typedef _W64 unsigned int UINT_PTR, *PUINT_PTR;

	//=====Modified by FPE(2014.3.20)=====(Linux x64)(Start)
	/*
    typedef _W64 long LONG_PTR, *PLONG_PTR;
    typedef _W64 unsigned long ULONG_PTR, *PULONG_PTR;
	*/

    typedef _W64 int LONG_PTR, *PLONG_PTR;
    typedef _W64 unsigned int ULONG_PTR, *PULONG_PTR;
	//=====Modified by FPE(2014.3.20)=====(Linux x64)(End)

    #define __int3264   __int32

#endif
#endif // midl64

#ifdef __cplusplus
}	/* extern "C" { */
#endif

/* Types use for passing & returning polymorphic values */
typedef UINT_PTR            WPARAM;
typedef LONG_PTR            LPARAM;
typedef LONG_PTR            LRESULT;

#ifndef NOMINMAX

#ifndef max
#define max(a,b)            (((a) > (b)) ? (a) : (b))
#endif

#ifndef min
#define min(a,b)            (((a) < (b)) ? (a) : (b))
#endif

#endif  /* NOMINMAX */

#define MAKEWORD(a, b)      ((WORD)(((BYTE)(((DWORD_PTR)(a)) & 0xff)) | ((WORD)((BYTE)(((DWORD_PTR)(b)) & 0xff))) << 8))
#define MAKELONG(a, b)      ((LONG)(((WORD)(((DWORD_PTR)(a)) & 0xffff)) | ((DWORD)((WORD)(((DWORD_PTR)(b)) & 0xffff))) << 16))
#define LOWORD(l)           ((WORD)(((DWORD_PTR)(l)) & 0xffff))
#define HIWORD(l)           ((WORD)((((DWORD_PTR)(l)) >> 16) & 0xffff))
#define LOBYTE(w)           ((BYTE)(((DWORD_PTR)(w)) & 0xff))
#define HIBYTE(w)           ((BYTE)((((DWORD_PTR)(w)) >> 8) & 0xff))


#ifndef WIN_INTERNAL
DECLARE_HANDLE            (HWND);
DECLARE_HANDLE            (HHOOK);
#ifdef WINABLE
DECLARE_HANDLE            (HEVENT);
#endif
#endif

typedef WORD                ATOM;

typedef HANDLE NEAR         *SPHANDLE;
typedef HANDLE FAR          *LPHANDLE;
typedef HANDLE              HGLOBAL;
typedef HANDLE              HLOCAL;
typedef HANDLE              GLOBALHANDLE;
typedef HANDLE              LOCALHANDLE;
#ifndef _MANAGED
#ifndef _MAC
#ifdef _WIN64
typedef INT_PTR (FAR WINAPI *FARPROC)();
typedef INT_PTR (NEAR WINAPI *NEARPROC)();
typedef INT_PTR (WINAPI *PROC)();
#else
typedef int (FAR WINAPI *FARPROC)();
typedef int (NEAR WINAPI *NEARPROC)();
typedef int (WINAPI *PROC)();
#endif  // _WIN64
#else
typedef int (CALLBACK *FARPROC)();
typedef int (CALLBACK *NEARPROC)();
typedef int (CALLBACK *PROC)();
#endif
#else
typedef INT_PTR (WINAPI *FARPROC)(void);
typedef INT_PTR (WINAPI *NEARPROC)(void);
typedef INT_PTR (WINAPI *PROC)(void);
#endif

#if !defined(_MAC) || !defined(GDI_INTERNAL)
#ifdef STRICT
typedef void NEAR* HGDIOBJ;
#else
DECLARE_HANDLE(HGDIOBJ);
#endif
#endif


#if !defined(UNALIGNED)
#if defined(_M_IA64) || defined(_M_AMD64)
#define UNALIGNED __unaligned
#else
#define UNALIGNED
#endif
#endif

#ifndef PURE
#define PURE                    = 0
#endif
#ifndef THIS_
#define THIS_
#endif
#ifndef THIS
#define THIS                    void
#endif


//
// SIZE_T used for counts or ranges which need to span the range of
// of a pointer.  SSIZE_T is the signed variation.
//

typedef ULONG_PTR SIZE_T, *PSIZE_T;
typedef LONG_PTR SSIZE_T, *PSSIZE_T;


//
// ANSI (Multi-byte Character) types
//
typedef CHAR *PCHAR, *LPCH, *PCH;
typedef CONST CHAR *LPCCH, *PCCH;

typedef CHAR *NPSTR, *LPSTR, *PSTR;
typedef PSTR *PZPSTR;
typedef CONST PSTR *PCZPSTR;
typedef CONST CHAR *LPCSTR, *PCSTR;
typedef PCSTR *PZPCSTR;

typedef CHAR *PZZSTR;
typedef CONST CHAR *PCZZSTR;

typedef CHAR *PNZCH;
typedef CONST CHAR *PCNZCH;


//
// Neutral ANSI/UNICODE types and macros
//
#ifdef  UNICODE                     // r_winnt

#ifndef _TCHAR_DEFINED
	typedef WCHAR TCHAR, *PTCHAR;
	typedef WCHAR TBYTE , *PTBYTE ;
	#define _TCHAR_DEFINED
#endif /* !_TCHAR_DEFINED */

typedef LPWCH LPTCH, PTCH;
typedef LPCWCH LPCTCH, PCTCH;
typedef LPWSTR PTSTR, LPTSTR;
typedef LPCWSTR PCTSTR, LPCTSTR;
typedef LPUWSTR PUTSTR, LPUTSTR;
typedef LPCUWSTR PCUTSTR, LPCUTSTR;
typedef LPWSTR LP;
typedef PZZWSTR PZZTSTR;
typedef PCZZWSTR PCZZTSTR;
typedef PUZZWSTR PUZZTSTR;
typedef PCUZZWSTR PCUZZTSTR;
typedef PNZWCH PNZTCH;
typedef PCNZWCH PCNZTCH;
typedef PUNZWCH PUNZTCH;
typedef PCUNZWCH PCUNZTCH;
#define __TEXT(quote) L##quote      // r_winnt

#else   /* UNICODE */               // r_winnt

#ifndef _TCHAR_DEFINED
	typedef char TCHAR, *PTCHAR;
	typedef unsigned char TBYTE , *PTBYTE ;
	#define _TCHAR_DEFINED
#endif /* !_TCHAR_DEFINED */

typedef LPCH LPTCH, PTCH;
typedef LPCCH LPCTCH, PCTCH;
typedef LPSTR PTSTR, LPTSTR, PUTSTR, LPUTSTR;
typedef LPCSTR PCTSTR, LPCTSTR, PCUTSTR, LPCUTSTR;
typedef PZZSTR PZZTSTR, PUZZTSTR;
typedef PCZZSTR PCZZTSTR, PCUZZTSTR;
typedef PNZCH PNZTCH, PUNZTCH;
typedef PCNZCH PCNZTCH, PCUNZTCH;
#define __TEXT(quote) quote         // r_winnt

#endif /* UNICODE */                // r_winnt
#define TEXT(quote) __TEXT(quote)   // r_winnt

#ifndef _T
#define _T(x)   __TEXT(x)
#endif


typedef SHORT *PSHORT;
typedef LONG *PLONG;


#define RtlEqualMemory(Destination,Source,Length) (!memcmp((Destination),(Source),(Length)))
#define RtlMoveMemory(Destination,Source,Length) memmove((Destination),(Source),(Length))

#undef RtlCopyMemory
#if defined(WIN32) || defined(_WIN32)
	#define RtlCopyMemory(Destination,Source,Length) memcpy_s((Destination),(Length),(Source),(Length))
#else
	#define RtlCopyMemory(Destination,Source,Length) memcpy((Destination),(Source),(Length))
#endif

#define RtlFillMemory(Destination,Length,Fill) memset((Destination),(Fill),(Length))
#define RtlZeroMemory(Destination,Length) memset((Destination),0,(Length))

#define MoveMemory RtlMoveMemory
#define CopyMemory RtlCopyMemory
#define FillMemory RtlFillMemory
#define ZeroMemory RtlZeroMemory


// 虚拟按键
#ifndef NOVIRTUALKEYCODES


/*
 * Virtual Keys, Standard Set
 */
#define VK_LBUTTON        0x01
#define VK_RBUTTON        0x02
#define VK_CANCEL         0x03
#define VK_MBUTTON        0x04    /* NOT contiguous with L & RBUTTON */

#if(_WIN32_WINNT >= 0x0500)
#define VK_XBUTTON1       0x05    /* NOT contiguous with L & RBUTTON */
#define VK_XBUTTON2       0x06    /* NOT contiguous with L & RBUTTON */
#endif /* _WIN32_WINNT >= 0x0500 */

/*
 * 0x07 : unassigned
 */

#define VK_BACK           0x08
#define VK_TAB            0x09

/*
 * 0x0A - 0x0B : reserved
 */

#define VK_CLEAR          0x0C
#define VK_RETURN         0x0D

#define VK_SHIFT          0x10
#define VK_CONTROL        0x11
#define VK_MENU           0x12
#define VK_PAUSE          0x13
#define VK_CAPITAL        0x14

#define VK_KANA           0x15
#define VK_HANGEUL        0x15  /* old name - should be here for compatibility */
#define VK_HANGUL         0x15
#define VK_JUNJA          0x17
#define VK_FINAL          0x18
#define VK_HANJA          0x19
#define VK_KANJI          0x19

#define VK_ESCAPE         0x1B

#define VK_CONVERT        0x1C
#define VK_NONCONVERT     0x1D
#define VK_ACCEPT         0x1E
#define VK_MODECHANGE     0x1F

#define VK_SPACE          0x20
#define VK_PRIOR          0x21
#define VK_NEXT           0x22
#define VK_END            0x23
#define VK_HOME           0x24
#define VK_LEFT           0x25
#define VK_UP             0x26
#define VK_RIGHT          0x27
#define VK_DOWN           0x28
#define VK_SELECT         0x29
#define VK_PRINT          0x2A
#define VK_EXECUTE        0x2B
#define VK_SNAPSHOT       0x2C
#define VK_INSERT         0x2D
#define VK_DELETE         0x2E
#define VK_HELP           0x2F

/*
 * VK_0 - VK_9 are the same as ASCII '0' - '9' (0x30 - 0x39)
 * 0x40 : unassigned
 * VK_A - VK_Z are the same as ASCII 'A' - 'Z' (0x41 - 0x5A)
 */

#define VK_LWIN           0x5B
#define VK_RWIN           0x5C
#define VK_APPS           0x5D

/*
 * 0x5E : reserved
 */

#define VK_SLEEP          0x5F

#define VK_NUMPAD0        0x60
#define VK_NUMPAD1        0x61
#define VK_NUMPAD2        0x62
#define VK_NUMPAD3        0x63
#define VK_NUMPAD4        0x64
#define VK_NUMPAD5        0x65
#define VK_NUMPAD6        0x66
#define VK_NUMPAD7        0x67
#define VK_NUMPAD8        0x68
#define VK_NUMPAD9        0x69
#define VK_MULTIPLY       0x6A
#define VK_ADD            0x6B
#define VK_SEPARATOR      0x6C
#define VK_SUBTRACT       0x6D
#define VK_DECIMAL        0x6E
#define VK_DIVIDE         0x6F
#define VK_F1             0x70
#define VK_F2             0x71
#define VK_F3             0x72
#define VK_F4             0x73
#define VK_F5             0x74
#define VK_F6             0x75
#define VK_F7             0x76
#define VK_F8             0x77
#define VK_F9             0x78
#define VK_F10            0x79
#define VK_F11            0x7A
#define VK_F12            0x7B
#define VK_F13            0x7C
#define VK_F14            0x7D
#define VK_F15            0x7E
#define VK_F16            0x7F
#define VK_F17            0x80
#define VK_F18            0x81
#define VK_F19            0x82
#define VK_F20            0x83
#define VK_F21            0x84
#define VK_F22            0x85
#define VK_F23            0x86
#define VK_F24            0x87

/*
 * 0x88 - 0x8F : unassigned
 */

#define VK_NUMLOCK        0x90
#define VK_SCROLL         0x91

/*
 * NEC PC-9800 kbd definitions
 */
#define VK_OEM_NEC_EQUAL  0x92   // '=' key on numpad

/*
 * Fujitsu/OASYS kbd definitions
 */
#define VK_OEM_FJ_JISHO   0x92   // 'Dictionary' key
#define VK_OEM_FJ_MASSHOU 0x93   // 'Unregister word' key
#define VK_OEM_FJ_TOUROKU 0x94   // 'Register word' key
#define VK_OEM_FJ_LOYA    0x95   // 'Left OYAYUBI' key
#define VK_OEM_FJ_ROYA    0x96   // 'Right OYAYUBI' key

/*
 * 0x97 - 0x9F : unassigned
 */

/*
 * VK_L* & VK_R* - left and right Alt, Ctrl and Shift virtual keys.
 * Used only as parameters to GetAsyncKeyState() and GetKeyState().
 * No other API or message will distinguish left and right keys in this way.
 */
#define VK_LSHIFT         0xA0
#define VK_RSHIFT         0xA1
#define VK_LCONTROL       0xA2
#define VK_RCONTROL       0xA3
#define VK_LMENU          0xA4
#define VK_RMENU          0xA5

//#if(_WIN32_WINNT >= 0x0500)
#define VK_BROWSER_BACK        0xA6
#define VK_BROWSER_FORWARD     0xA7
#define VK_BROWSER_REFRESH     0xA8
#define VK_BROWSER_STOP        0xA9
#define VK_BROWSER_SEARCH      0xAA
#define VK_BROWSER_FAVORITES   0xAB
#define VK_BROWSER_HOME        0xAC

#define VK_VOLUME_MUTE         0xAD
#define VK_VOLUME_DOWN         0xAE
#define VK_VOLUME_UP           0xAF
#define VK_MEDIA_NEXT_TRACK    0xB0
#define VK_MEDIA_PREV_TRACK    0xB1
#define VK_MEDIA_STOP          0xB2
#define VK_MEDIA_PLAY_PAUSE    0xB3
#define VK_LAUNCH_MAIL         0xB4
#define VK_LAUNCH_MEDIA_SELECT 0xB5
#define VK_LAUNCH_APP1         0xB6
#define VK_LAUNCH_APP2         0xB7

//#endif /* _WIN32_WINNT >= 0x0500 */

/*
 * 0xB8 - 0xB9 : reserved
 */

#define VK_OEM_1          0xBA   // ';:' for US
#define VK_OEM_PLUS       0xBB   // '+' any country
#define VK_OEM_COMMA      0xBC   // ',' any country
#define VK_OEM_MINUS      0xBD   // '-' any country
#define VK_OEM_PERIOD     0xBE   // '.' any country
#define VK_OEM_2          0xBF   // '/?' for US
#define VK_OEM_3          0xC0   // '`~' for US

/*
 * 0xC1 - 0xD7 : reserved
 */

/*
 * 0xD8 - 0xDA : unassigned
 */

#define VK_OEM_4          0xDB  //  '[{' for US
#define VK_OEM_5          0xDC  //  '\|' for US
#define VK_OEM_6          0xDD  //  ']}' for US
#define VK_OEM_7          0xDE  //  ''"' for US
#define VK_OEM_8          0xDF

/*
 * 0xE0 : reserved
 */

/*
 * Various extended or enhanced keyboards
 */
#define VK_OEM_AX         0xE1  //  'AX' key on Japanese AX kbd
#define VK_OEM_102        0xE2  //  "<>" or "\|" on RT 102-key kbd.
#define VK_ICO_HELP       0xE3  //  Help key on ICO
#define VK_ICO_00         0xE4  //  00 key on ICO

#if(WINVER >= 0x0400)
#define VK_PROCESSKEY     0xE5
#endif /* WINVER >= 0x0400 */

#define VK_ICO_CLEAR      0xE6


//#if(_WIN32_WINNT >= 0x0500)
#define VK_PACKET         0xE7
//#endif /* _WIN32_WINNT >= 0x0500 */

/*
 * 0xE8 : unassigned
 */

/*
 * Nokia/Ericsson definitions
 */
#define VK_OEM_RESET      0xE9
#define VK_OEM_JUMP       0xEA
#define VK_OEM_PA1        0xEB
#define VK_OEM_PA2        0xEC
#define VK_OEM_PA3        0xED
#define VK_OEM_WSCTRL     0xEE
#define VK_OEM_CUSEL      0xEF
#define VK_OEM_ATTN       0xF0
#define VK_OEM_FINISH     0xF1
#define VK_OEM_COPY       0xF2
#define VK_OEM_AUTO       0xF3
#define VK_OEM_ENLW       0xF4
#define VK_OEM_BACKTAB    0xF5

#define VK_ATTN           0xF6
#define VK_CRSEL          0xF7
#define VK_EXSEL          0xF8
#define VK_EREOF          0xF9
#define VK_PLAY           0xFA
#define VK_ZOOM           0xFB
#define VK_NONAME         0xFC
#define VK_PA1            0xFD
#define VK_OEM_CLEAR      0xFE

/*
 * 0xFF : reserved
 */


#endif /* !NOVIRTUALKEYCODES */


/* _countof helper */
#if !defined(_countof)
	#if !defined(__cplusplus)
		#define _countof(_Array) (sizeof(_Array) / sizeof(_Array[0]))
	#else
		extern "C++"
		{
		template <typename _CountofType, size_t _SizeOfArray>
		char (*__countof_helper(UNALIGNED _CountofType (&_Array)[_SizeOfArray]))[_SizeOfArray];
		#define _countof(_Array) (sizeof(*__countof_helper(_Array)) + 0)
		}
	#endif
#endif

#ifndef __max
#define __max(a,b)  (((a) > (b)) ? (a) : (b))
#endif
#ifndef __min
#define __min(a,b)  (((a) < (b)) ? (a) : (b))
#endif

#ifndef SIZE_MAX
#ifdef _WIN64
#define SIZE_MAX _UI64_MAX
#else
#define SIZE_MAX UINT_MAX
#endif
#endif


typedef struct tagRECT
{
    LONG    left;
    LONG    top;
    LONG    right;
    LONG    bottom;
} RECT, *PRECT, NEAR *NPRECT, FAR *LPRECT;

typedef const RECT FAR* LPCRECT;

typedef struct _RECTL       /* rcl */
{
    LONG    left;
    LONG    top;
    LONG    right;
    LONG    bottom;
} RECTL, *PRECTL, *LPRECTL;

typedef const RECTL FAR* LPCRECTL;

typedef struct tagPOINT
{
    LONG  x;
    LONG  y;
} POINT, *PPOINT, NEAR *NPPOINT, FAR *LPPOINT;

typedef struct _POINTL      /* ptl  */
{
    LONG  x;
    LONG  y;
} POINTL, *PPOINTL;

typedef struct tagSIZE
{
    LONG        cx;
    LONG        cy;
} SIZE, *PSIZE, *LPSIZE;

typedef SIZE               SIZEL;
typedef SIZE               *PSIZEL, *LPSIZEL;

typedef struct tagPOINTS
{
#ifndef _MAC
    SHORT   x;
    SHORT   y;
#else
    SHORT   y;
    SHORT   x;
#endif
} POINTS, *PPOINTS, *LPPOINTS;

/*
 * DrawText() Format Flags
 */
#define DT_TOP                      0x00000000
#define DT_LEFT                     0x00000000
#define DT_CENTER                   0x00000001
#define DT_RIGHT                    0x00000002
#define DT_VCENTER                  0x00000004
#define DT_BOTTOM                   0x00000008
#define DT_WORDBREAK                0x00000010
#define DT_SINGLELINE               0x00000020
#define DT_EXPANDTABS               0x00000040
#define DT_TABSTOP                  0x00000080
#define DT_NOCLIP                   0x00000100
#define DT_EXTERNALLEADING          0x00000200
#define DT_CALCRECT                 0x00000400
#define DT_NOPREFIX                 0x00000800
#define DT_INTERNAL                 0x00001000

//#if(WINVER >= 0x0400)
//#define DT_EDITCONTROL              0x00002000
//#define DT_PATH_ELLIPSIS            0x00004000
//#define DT_END_ELLIPSIS             0x00008000
//#define DT_MODIFYSTRING             0x00010000
#define DT_RTLREADING               0x00020000
//#define DT_WORD_ELLIPSIS            0x00040000
//#if(WINVER >= 0x0500)
//#define DT_NOFULLWIDTHCHARBREAK     0x00080000
//#if(_WIN32_WINNT >= 0x0500)
//#define DT_HIDEPREFIX               0x00100000
//#define DT_PREFIXONLY               0x00200000
//#endif /* _WIN32_WINNT >= 0x0500 */
//#endif /* WINVER >= 0x0500 */

#endif	/* _WINDOWS_ */

#define UNREFERENCED_PARAMETER(P)          (P)
#define DBG_UNREFERENCED_PARAMETER(P)      (P)
#define DBG_UNREFERENCED_LOCAL_VARIABLE(V) (V)

#endif	// !NNN_PLATFORM_WIN32 && !NNN_PLATFORM_WP8

#ifndef _WAVEFORMATEX_
#define _WAVEFORMATEX_

/*
 *  extended waveform format structure used for all non-PCM formats. this
 *  structure is common to all non-PCM formats.
 */
typedef struct tWAVEFORMATEX
{
    WORD        wFormatTag;         /* format type */
    WORD        nChannels;          /* number of channels (i.e. mono, stereo...) */
    DWORD       nSamplesPerSec;     /* sample rate */
    DWORD       nAvgBytesPerSec;    /* for buffer estimation */
    WORD        nBlockAlign;        /* block size of data */
    WORD        wBitsPerSample;     /* number of bits per sample of mono data */
    WORD        cbSize;             /* the count in bytes of the size of */
                                    /* extra information (after cbSize) */
} WAVEFORMATEX, *PWAVEFORMATEX, NEAR *NPWAVEFORMATEX, FAR *LPWAVEFORMATEX;

#ifndef WAVE_FORMAT_PCM
/* flags for wFormatTag field of WAVEFORMAT */
#define WAVE_FORMAT_PCM     1
#endif	// WAVE_FORMAT_PCM

#endif /* _WAVEFORMATEX_ */
typedef const WAVEFORMATEX FAR *LPCWAVEFORMATEX;

//======================================== （函数） ========================================(Start)
// 判断点是否在矩形中
NNN_API inline BOOL PtInRect(__in CONST RECT *lprc, __in POINT pt)
{
	return ((pt.x >= lprc->left) && (pt.x < lprc->right) &&
			(pt.y >= lprc->top) && (pt.y < lprc->bottom));
}

NNN_API BOOL IsRectEmpty(__in CONST RECT *lprc);														// 判断一个矩形是否为空
NNN_API BOOL SetRectEmpty(__out LPRECT lprc);															// 使矩形为空
NNN_API BOOL IntersectRect(__out LPRECT lprcDst, __in CONST RECT *lprcSrc1, __in CONST RECT *lprcSrc2);	// 计算两个源矩形的交集
NNN_API BOOL UnionRect(__out LPRECT lprcDst, __in CONST RECT *lprcSrc1, __in CONST RECT *lprcSrc2);		// 计算两个源矩形的并集
NNN_API BOOL SetRect(__out LPRECT lprc, int xLeft, int yTop, int xRight, int yBottom);					// 设置矩形的坐标
//======================================== （函数） ========================================(End)

#endif	// !NNN_PLATFORM_WIN32

#endif	/* _NNN___COMMON___WINSTYLE_H_ */
