//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : CLR 公共的东西
//--------------------------------------------------------------------------------------

#pragma once

#include "../nnnLib/nnnLib.h"

namespace NNN
{
namespace CLR
{

// https://docs.microsoft.com/en-us/cpp/dotnet/how-to-use-events-in-cpp-cli?view=msvc-170
#define ADD_CLR_EVENT(event_name, params, ...)	\
	public:	\
		delegate void event_name##_Handler(params);	\
		\
		event event_name##_Handler ^Event_##event_name	\
		{	\
			void add(event_name##_Handler ^p)		{ e_##event_name = (event_name##_Handler^)System::Delegate::Combine(e_##event_name, p); }	\
			void remove(event_name##_Handler ^p)	{ e_##event_name = (event_name##_Handler^)System::Delegate::Remove(e_##event_name, p); }	\
			void raise(params)						{ if(e_##event_name != nullptr) e_##event_name->Invoke(__VA_ARGS__); }	\
		}	\
	private:	\
		event_name##_Handler ^e_##event_name;

#define ADD_CLR_EVENT_STATIC(event_name, params, ...)	\
	public:	\
		delegate void event_name##_Handler(params);	\
		\
		static event event_name##_Handler ^Event_##event_name	\
		{	\
			void add(event_name##_Handler ^p)		{ e_##event_name = (event_name##_Handler^)System::Delegate::Combine(e_##event_name, p); }	\
			void remove(event_name##_Handler ^p)	{ e_##event_name = (event_name##_Handler^)System::Delegate::Remove(e_##event_name, p); }	\
			void raise(params)						{ if(e_##event_name != nullptr) e_##event_name->Invoke(__VA_ARGS__); }	\
		}	\
	private:	\
		static event_name##_Handler ^e_##event_name;

/*==============================================================
 * String_to_char()		- String^ -> char*
 * String_to_wchar()	- String^ -> WCHAR*
 *==============================================================*/
#pragma warning(disable : 4996)
inline char* String_to_char(System::String ^txt, __out char *buffer)
{
	buffer[0] = '\0';

	if(txt == nullptr)
		return buffer;

	System::IntPtr ip = System::Runtime::InteropServices::Marshal::StringToHGlobalAnsi(txt);
	strcpy(buffer, (const char*)ip.ToPointer());
	System::Runtime::InteropServices::Marshal::FreeHGlobal(ip);

	return buffer;
}
//--------------------------------------------------
inline WCHAR* String_to_wchar(System::String ^txt, __out WCHAR *buffer)
{
	buffer[0] = L'\0';

	if(txt == nullptr)
		return buffer;

	System::IntPtr ip = System::Runtime::InteropServices::Marshal::StringToHGlobalUni(txt);
	wcscpy(buffer, (const WCHAR*)ip.ToPointer());
	System::Runtime::InteropServices::Marshal::FreeHGlobal(ip);

	return buffer;
}
#pragma warning(default : 4996)


/*==============================================================
 * array<BYTE> <--> BYTE[]
 * copy_from_array()
 * copy_to_array()
 *==============================================================*/
inline void copy_from_array(__in array<BYTE> ^input_array, __out BYTE *output_array)
{
	System::IntPtr ptr(output_array);

	System::Runtime::InteropServices::Marshal::Copy(input_array, 0, ptr, input_array->Length);
}
//--------------------------------------------------
inline void copy_to_array(__in BYTE *input_array, __out array<BYTE> ^output_array, int count)
{
	System::IntPtr ptr(input_array);

	System::Runtime::InteropServices::Marshal::Copy(ptr, output_array, 0, count);
}


/*==============================================================
 * DateTime <--> time_t
 * DataTime_to_timet()
 * timet_to_DataTime()
 *==============================================================*/
inline time_t DataTime_to_timet(System::DateTime datetime)
{
	tm tm_;
	tm_.tm_year	= datetime.Year - 1900;
	tm_.tm_mon	= datetime.Month - 1;
	tm_.tm_mday	= datetime.Day;
	tm_.tm_hour	= datetime.Hour;
	tm_.tm_min	= datetime.Minute;
	tm_.tm_sec	= datetime.Second;

	return NNN::Time::tm_to_timet(tm_);
}
//--------------------------------------------------
inline System::DateTime timet_to_DataTime(time_t time)
{
	tm tm_ = NNN::Time::timet_to_tm(time);

	System::DateTime datetime(	tm_.tm_year + 1900,
								tm_.tm_mon + 1,
								tm_.tm_mday,
								tm_.tm_hour,
								tm_.tm_min,
								tm_.tm_sec );
	return datetime;
}

}	// namespace CLR
}	// namespace NNN
