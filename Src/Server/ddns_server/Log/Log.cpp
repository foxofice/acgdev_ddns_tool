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
	std::string							*m_buffer	= nullptr;
	class NNN::Thread::c_Atomic_Lock	m_lock;
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
	static UINT64 s_can_DoWork_tick = 0;

	if(s_can_DoWork_tick > NNN::Time::tick64())
		return;

	char	log_filename[MAX_PATH];
	tm		tm_	= NNN::Time::get_current_tm();

	sprintf_s(	log_filename,
				Config::g_config->LOG.m_filename,
				tm_.tm_year + 1900, tm_.tm_mon + 1, tm_.tm_mday );

	{
		NNN::Thread::c_Lock l(WRITE_FILE_BUFFER.m_lock);

		if(!WRITE_FILE_BUFFER.m_buffer->empty())
		{
			NNN::IO::File::append_file(log_filename, WRITE_FILE_BUFFER.m_buffer->c_str());
			WRITE_FILE_BUFFER.m_buffer->clear();
		}
	}

	s_can_DoWork_tick = NNN::Time::tick64() + Config::g_config->LOG.m_write_file_interval;
}


/*==============================================================
 * 显示信息（通用函数）
 * _vShowMessage()
 *==============================================================*/
static void _vShowMessage(es_MsgType type, const char *format, va_list ap)
{
	if(format == nullptr || format[0] == '\0')
		return;

	UINT flag = UINT_MAX;

	// 前缀
	char prefix[100];
	prefix[0] = '\0';

	// header 前缀
	switch(type)
	{
	case es_MsgType::None:			// 普通消息
		break;

	case es_MsgType::Status:		// （良好的东西）
		flag = Config::g_config->LOG.m_console_Status;
		strcpy_s(prefix, NNN_CL_STATUS "[Status] " NNN_ANSI_RESET);
		break;

	case es_MsgType::SQL:			// （输出 SQL 相关的东西）
		flag = Config::g_config->LOG.m_console_SQL;
		strcpy_s(prefix, NNN_CL_SQL "[SQL] " NNN_ANSI_RESET);
		break;

	case es_MsgType::Info:			// （变量信息）
		flag = Config::g_config->LOG.m_console_Info;
		strcpy_s(prefix, NNN_CL_INFO "[Info] " NNN_ANSI_RESET);
		break;

	case es_MsgType::Notice:		// （轻于警告）
		flag = Config::g_config->LOG.m_console_Notice;
		strcpy_s(prefix, NNN_CL_NOTICE "[Notice] " NNN_ANSI_RESET);
		break;

	case es_MsgType::Warning:
		flag = Config::g_config->LOG.m_console_Warning;
		strcpy_s(prefix, NNN_CL_WARNING "[Warning] " NNN_ANSI_RESET);
		break;

	case es_MsgType::Debug:
		flag = Config::g_config->LOG.m_console_Debug;
		strcpy_s(prefix, NNN_CL_DEBUG "[Debug] " NNN_ANSI_RESET);
		break;

	case es_MsgType::Error:			// （常规错误）
		flag = Config::g_config->LOG.m_console_Error;
		strcpy_s(prefix, NNN_CL_ERROR "[Error] " NNN_ANSI_RESET);
		break;

	case es_MsgType::FatalError:	// （致命错误，如果可能的话请调用 abort();）
		strcpy_s(prefix, NNN_CL_FATAL_ERROR "[Fatal Error] " NNN_ANSI_RESET);
		break;
	}	// switch

	if(flag == 0)
		return;	// 不打印 + 不写入文件

	bool	flag_show_console	= (flag & (UINT)Config::es_Log_Console::Show);
	bool	flag_write_file		= (flag & (UINT)Config::es_Log_Console::Write);

	const size_t k_STACK_SIZE = 4096;
	struct NNN::Buffer::s_StackBuffer<char, k_STACK_SIZE> log(2048);
	size_t log_len = 0;

	char txt[k_STACK_SIZE];
	strcpy_s(txt, prefix);

	size_t len = strlen(txt);
	vsnprintf_s(txt + len, _countof(txt) - len, _countof(txt) - len - 1, format, ap);

	// 打印日志
	if(flag_show_console)
		printf("%s", txt);

	// 写入文件
	if(flag_write_file)
	{
		char plain_txt[k_STACK_SIZE];
		NNN::Console::remove_ansi_code(txt, plain_txt);

		len = strlen(plain_txt);
		log.reserve(log_len + len);
		CopyMemory(log.m_p + log_len, plain_txt, len);
		log_len += len;

		log.m_p[log_len] = '\0';

		// 时间前缀
		char time_prefix[100];
		time_prefix[0] = '\0';

		if(Config::g_config->LOG.m_timestamp_format[0] != '\0')
		{
			tm tm_ = NNN::Time::get_current_tm();
			strftime(time_prefix, _countof(time_prefix), Config::g_config->LOG.m_timestamp_format, &tm_);
			strcat_s(time_prefix, " ");
		}

		WRITE_FILE_BUFFER.m_lock.Lock();
		*WRITE_FILE_BUFFER.m_buffer += time_prefix;
		*WRITE_FILE_BUFFER.m_buffer += log.m_p;
		WRITE_FILE_BUFFER.m_lock.UnLock();
	}
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
