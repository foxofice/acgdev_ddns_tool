//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//--------------------------------------------------------------------------------------

#include "../Config/Config.h"
#include "Log-macro.h"
#include "Log-inc.h"
#include "Log.h"

namespace DDNS_Server
{
namespace Log
{

// 写入文件的缓存
struct
{
	std::string								*m_buffer	= nullptr;
	struct NNN::Thread::s_CriticalSection	m_cs;
} WRITE_FILE_BUFFER;

/*==============================================================
 * 初始化/清理
 * DoInit()
 * DoFinal()
 *==============================================================*/
HRESULT DoInit()
{
	if(WRITE_FILE_BUFFER.m_buffer == nullptr)
	{
		WRITE_FILE_BUFFER.m_buffer = new std::string();
		WRITE_FILE_BUFFER.m_buffer->reserve(512 * 1024);	// 初始 512K
	}

	return S_OK;
}
//--------------------------------------------------
HRESULT DoFinal()
{
	SAFE_DELETE(WRITE_FILE_BUFFER.m_buffer);

	return S_OK;
}


/*==============================================================
 * 执行周期性工作
 * DoWork()
 *==============================================================*/
void DoWork()
{
	// 追加日志到文件
	static UINT64 s_can_append_log_tick = 0;

	if(s_can_append_log_tick <= NNN::Time::tick64())
	{
		NNN::Thread::c_Lock l(WRITE_FILE_BUFFER.m_cs);

		if(!WRITE_FILE_BUFFER.m_buffer->empty())
		{
			char	log_filename[MAX_PATH];
			tm		tm_	= NNN::Time::get_current_tm();

			NNN::C::sprintf(log_filename,
							_countof(log_filename),
							Config::g_config->LOG.m_filename,
							tm_.tm_year + 1900, tm_.tm_mon + 1, tm_.tm_mday);

			NNN::IO::File::append_file(log_filename, WRITE_FILE_BUFFER.m_buffer->c_str());
			WRITE_FILE_BUFFER.m_buffer->clear();

			s_can_append_log_tick = NNN::Time::tick64() + Config::g_config->LOG.m_write_file_interval;
		}
	}
}


/*==============================================================
 * 显示信息（通用函数）
 * _vShowMessage()
 *==============================================================*/
static void _vShowMessage(es_MsgType flag, const char *format, va_list ap)
{
	if(format == nullptr || format[0] == '\0')
		return;

	if(Config::g_config->LOG.m_console_silent_flag & (UINT)flag)
		return;	// 不打印

	// 前缀
	char prefix[100];
	prefix[0] = '\0';

	// header 前缀
	switch(flag)
	{
	case es_MsgType::None:	// 直接 printf 更换
		break;

	case es_MsgType::Status:	// Bright Green（良好的东西）
		NNN::C::strcpy(prefix, NNN_CL_STATUS "[Status] " NNN_ANSI_RESET);
		break;

	case es_MsgType::SQL:	// Bright Violet（输出 SQL 相关的东西）
		NNN::C::strcpy(prefix, NNN_CL_SQL "[SQL] " NNN_ANSI_RESET);
		break;

	case es_MsgType::Info:	// Bright White（变量信息）
		NNN::C::strcpy(prefix, NNN_CL_INFO "[Info] " NNN_ANSI_RESET);
		break;

	case es_MsgType::Notice:	// Bright White（轻于警告）
		NNN::C::strcpy(prefix, NNN_CL_NOTICE "[Notice] " NNN_ANSI_RESET);
		break;

	case es_MsgType::Warning:	// Bright Yellow
		NNN::C::strcpy(prefix, NNN_CL_WARNING "[Warning] " NNN_ANSI_RESET);
		break;

	case es_MsgType::Debug:	// Bright Cyan（重要的东西！）
		NNN::C::strcpy(prefix, NNN_CL_DEBUG "[Debug] " NNN_ANSI_RESET);
		break;

	case es_MsgType::Error:	// Bright Red（常规错误）
		NNN::C::strcpy(prefix, NNN_CL_ERROR "[Error] " NNN_ANSI_RESET);
		break;

	case es_MsgType::FatalError:	// Bright Red（致命错误，如果可能的话请调用 abort();）
		NNN::C::strcpy(prefix, NNN_CL_FATAL_ERROR "[Fatal Error] " NNN_ANSI_RESET);
		break;
	}	// switch

	//========== 打印日志 ==========(Start)
	std::string log;
	log.reserve(1024);

	char txt[4096];
	NNN::C::strcpy(txt, prefix);

	size_t len = strlen(txt);
	NNN::C::vsnprintf(txt + len, _countof(txt) - len, _countof(txt) - len - 1, format, ap);

	printf("%s", txt);

	char plain_txt[4096];
	NNN::Console::remove_ansi_code(txt, plain_txt);
	log += plain_txt;
	//========== 打印日志 ==========(End)

	//========== 写入文件 ==========(Start)
	// 时间前缀
	char time_prefix[100] = {};

	if((Config::g_config->LOG.m_write_file_silent_flag & (UINT)flag) == 0)
	{
		if(Config::g_config->LOG.m_timestamp_format[0] != '\0')
		{
			tm tm_ = NNN::Time::get_current_tm();
			strftime(	time_prefix,
						_countof(time_prefix),
						Config::g_config->LOG.m_timestamp_format,
						&tm_ );
			NNN::C::strcat(time_prefix, " ");
		}

		NNN::Thread::c_Lock l(WRITE_FILE_BUFFER.m_cs);

		*WRITE_FILE_BUFFER.m_buffer += time_prefix;
		*WRITE_FILE_BUFFER.m_buffer += log;
	}
	//========== 写入文件 ==========(End)
}


/*==============================================================
 * 显示信息
 *
 * ShowMessage()	- 一般信息
 * ShowStatus()		- 良好的东西
 * ShowSQL()		- 输出 SQL 相关的东西
 * ShowInfo()		- 变量信息
 * ShowNotice()		- 轻于警告
 * ShowWarning()	- 警告
 * ShowDebug()		- 调试
 * ShowError()		- 常规错误
 * ShowFatalError()	- 致命错误
 *==============================================================*/
void ShowMessage(const char *format, ...)
{
	va_list ap;

	va_start(ap, format);
	_vShowMessage(es_MsgType::None, format, ap);
	va_end(ap);
}
//--------------------------------------------------
void ShowStatus(const char *format, ...)
{
	va_list ap;

	va_start(ap, format);
	_vShowMessage(es_MsgType::Status, format, ap);
	va_end(ap);
}
//--------------------------------------------------
void ShowSQL(const char *format, ...)
{
	va_list ap;

	va_start(ap, format);
	_vShowMessage(es_MsgType::SQL, format, ap);
	va_end(ap);
}
//--------------------------------------------------
void ShowInfo(const char *format, ...)
{
	va_list ap;

	va_start(ap, format);
	_vShowMessage(es_MsgType::Info, format, ap);
	va_end(ap);
}
//--------------------------------------------------
void ShowNotice(const char *format, ...)
{
	va_list ap;

	va_start(ap, format);
	_vShowMessage(es_MsgType::Notice, format, ap);
	va_end(ap);
}
//--------------------------------------------------
void ShowWarning(const char *format, ...)
{
	va_list ap;

	va_start(ap, format);
	_vShowMessage(es_MsgType::Warning, format, ap);
	va_end(ap);
}
//--------------------------------------------------
void ShowDebug(const char *format, ...)
{
	va_list ap;

	va_start(ap, format);
	_vShowMessage(es_MsgType::Debug, format, ap);
	va_end(ap);
}
//--------------------------------------------------
void ShowError(const char *format, ...)
{
	va_list ap;

	va_start(ap, format);
	_vShowMessage(es_MsgType::Error, format, ap);
	va_end(ap);
}
//--------------------------------------------------
void ShowFatalError(const char *format, ...)
{
	va_list ap;

	va_start(ap, format);
	_vShowMessage(es_MsgType::FatalError, format, ap);
	va_end(ap);
}

}	// namespace Log
}	// namespace DDNS_Server
