//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 控制台（宏）
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNLIB___CONSOLE___CONSOLE_MACRO_H_
#define _NNNLIB___CONSOLE___CONSOLE_MACRO_H_

// 注意：以下格式在 Linux 下均可全部使用，Windows 下未全部实现

// for help with the console colors look here:
// http://www.edoceo.com/liberum/?doc=printf-with-color

#define NNN_CL_RESET		"\033[0m"		// 清除格式（reset color parameter）
#define NNN_CL_CLS			"\033[2J"		// 清屏（clear screen and go up/left (0, 0 position)）
#define NNN_CL_CLL			"\033[K"		// 清除从光标到行尾的内容（clear line from actual position to end of the line）
#define NNN_CL_HOME			"\033[0G"		// 移动光标到行首（Moves the cursor to indicated column in current row）

// 字体设定
#define NNN_CL_BOLD			"\033[1m"		// 加粗（加亮）（use bold for font）
#define NNN_CL_NORM			NNN_CL_RESET
#define NNN_CL_NORMAL		NNN_CL_RESET
#define NNN_CL_NONE			NNN_CL_RESET

// 前景色
#define NNN_CL_LT_BLACK		"\033[0;30m"	// 黑
#define NNN_CL_LT_RED		"\033[0;31m"	// 红
#define NNN_CL_LT_GREEN		"\033[0;32m"	// 绿
#define NNN_CL_LT_YELLOW	"\033[0;33m"	// 黄
#define NNN_CL_LT_BLUE		"\033[0;34m"	// 蓝
#define NNN_CL_LT_MAGENTA	"\033[0;35m"	// 紫
#define NNN_CL_LT_CYAN		"\033[0;36m"	// 青
#define NNN_CL_LT_WHITE		"\033[0;37m"	// 白

// 前景色加粗（加亮）
#define NNN_CL_GRAY			"\033[1;30m"	// 灰
#define NNN_CL_RED			"\033[1;31m"	// 红
#define NNN_CL_GREEN		"\033[1;32m"	// 绿
#define NNN_CL_YELLOW		"\033[1;33m"	// 黄
#define NNN_CL_BLUE			"\033[1;34m"	// 蓝
#define NNN_CL_MAGENTA		"\033[1;35m"	// 紫
#define NNN_CL_CYAN			"\033[1;36m"	// 青
#define NNN_CL_WHITE		"\033[1;37m"	// 白

#define NNN_CL_BT_BLACK		NNN_CL_GRAY
#define NNN_CL_BT_RED		NNN_CL_RED
#define NNN_CL_BT_GREEN		NNN_CL_GREEN
#define NNN_CL_BT_YELLOW	NNN_CL_YELLOW
#define NNN_CL_BT_BLUE		NNN_CL_BLUE
#define NNN_CL_BT_MAGENTA	NNN_CL_MAGENTA
#define NNN_CL_BT_CYAN		NNN_CL_CYAN
#define NNN_CL_BT_WHITE		NNN_CL_WHITE

// 背景色
#define NNN_CL_BG_BLACK		"\033[40m"		// 黑
#define NNN_CL_BG_RED		"\033[41m"		// 红
#define NNN_CL_BG_GREEN		"\033[42m"		// 绿
#define NNN_CL_BG_YELLOW	"\033[43m"		// 黄
#define NNN_CL_BG_BLUE		"\033[44m"		// 蓝
#define NNN_CL_BG_MAGENTA	"\033[45m"		// 紫
#define NNN_CL_BG_CYAN		"\033[46m"		// 青
#define NNN_CL_BG_WHITE		"\033[47m"		// 白

// 颜色组合
#define NNN_CL_DEFAULT_BLACK	"\033[0;40m"	// 默认黑底
#define NNN_CL_DEFAULT_RED		"\033[0;41m"	// 默认红底
#define NNN_CL_DEFAULT_GREEN	"\033[0;42m"	// 默认绿底
#define NNN_CL_DEFAULT_YELLOW	"\033[0;43m"	// 默认黄底
#define NNN_CL_DEFAULT_BLUE		"\033[0;44m"	// 默认蓝底
#define NNN_CL_DEFAULT_MAGENTA	"\033[0;45m"	// 默认紫底
#define NNN_CL_DEFAULT_CYAN		"\033[0;46m"	// 默认青底
#define NNN_CL_DEFAULT_WHITE	"\033[0;47m"	// 默认白底

#define NNN_CL_BLACK_BLACK		"\033[30;40m"	// 黑字黑底
#define NNN_CL_BLACK_RED		"\033[30;41m"	// 黑字红底
#define NNN_CL_BLACK_GREEN		"\033[30;42m"	// 黑字绿底
#define NNN_CL_BLACK_YELLOW		"\033[30;43m"	// 黑字黄底
#define NNN_CL_BLACK_BLUE		"\033[30;44m"	// 黑字蓝底
#define NNN_CL_BLACK_MAGENTA	"\033[30;45m"	// 黑字紫底
#define NNN_CL_BLACK_CYAN		"\033[30;46m"	// 黑字青底
#define NNN_CL_BLACK_WHITE		"\033[30;47m"	// 黑字白底

#define NNN_CL_RED_BLACK		"\033[31;40m"	// 红字黑底
#define NNN_CL_RED_RED			"\033[31;41m"	// 红字红底
#define NNN_CL_RED_GREEN		"\033[31;42m"	// 红字绿底
#define NNN_CL_RED_YELLOW		"\033[31;43m"	// 红字黄底
#define NNN_CL_RED_BLUE			"\033[31;44m"	// 红字蓝底
#define NNN_CL_RED_MAGENTA		"\033[31;45m"	// 红字紫底
#define NNN_CL_RED_CYAN			"\033[31;46m"	// 红字青底
#define NNN_CL_RED_WHITE		"\033[31;47m"	// 红字白底

#define NNN_CL_GREEN_BLACK		"\033[32;40m"	// 绿字黑底
#define NNN_CL_GREEN_RED		"\033[32;41m"	// 绿字红底
#define NNN_CL_GREEN_GREEN		"\033[32;42m"	// 绿字绿底
#define NNN_CL_GREEN_YELLOW		"\033[32;43m"	// 绿字黄底
#define NNN_CL_GREEN_BLUE		"\033[32;44m"	// 绿字蓝底
#define NNN_CL_GREEN_MAGENTA	"\033[32;45m"	// 绿字紫底
#define NNN_CL_GREEN_CYAN		"\033[32;46m"	// 绿字青底
#define NNN_CL_GREEN_WHITE		"\033[32;47m"	// 绿字白底

#define NNN_CL_YELLOW_BLACK		"\033[33;40m"	// 黄字黑底
#define NNN_CL_YELLOW_RED		"\033[33;41m"	// 黄字红底
#define NNN_CL_YELLOW_GREEN		"\033[33;42m"	// 黄字绿底
#define NNN_CL_YELLOW_YELLOW	"\033[33;43m"	// 黄字黄底
#define NNN_CL_YELLOW_BLUE		"\033[33;44m"	// 黄字蓝底
#define NNN_CL_YELLOW_MAGENTA	"\033[33;45m"	// 黄字紫底
#define NNN_CL_YELLOW_CYAN		"\033[33;46m"	// 黄字青底
#define NNN_CL_YELLOW_WHITE		"\033[33;47m"	// 黄字白底

#define NNN_CL_BLUE_BLACK		"\033[34;40m"	// 蓝字黑底
#define NNN_CL_BLUE_RED			"\033[34;41m"	// 蓝字红底
#define NNN_CL_BLUE_GREEN		"\033[34;42m"	// 蓝字绿底
#define NNN_CL_BLUE_YELLOW		"\033[34;43m"	// 蓝字黄底
#define NNN_CL_BLUE_BLUE		"\033[34;44m"	// 蓝字蓝底
#define NNN_CL_BLUE_MAGENTA		"\033[34;45m"	// 蓝字紫底
#define NNN_CL_BLUE_CYAN		"\033[34;46m"	// 蓝字青底
#define NNN_CL_BLUE_WHITE		"\033[34;47m"	// 蓝字白底

#define NNN_CL_MAGENTA_BLACK	"\033[35;40m"	// 紫字黑底
#define NNN_CL_MAGENTA_RED		"\033[35;41m"	// 紫字红底
#define NNN_CL_MAGENTA_GREEN	"\033[35;42m"	// 紫字绿底
#define NNN_CL_MAGENTA_YELLOW	"\033[35;43m"	// 紫字黄底
#define NNN_CL_MAGENTA_BLUE		"\033[35;44m"	// 紫字蓝底
#define NNN_CL_MAGENTA_MAGENTA	"\033[35;45m"	// 紫字紫底
#define NNN_CL_MAGENTA_CYAN		"\033[35;46m"	// 紫字青底
#define NNN_CL_MAGENTA_WHITE	"\033[35;47m"	// 紫字白底

#define NNN_CL_CYAN_BLACK		"\033[36;40m"	// 青字黑底
#define NNN_CL_CYAN_RED			"\033[36;41m"	// 青字红底
#define NNN_CL_CYAN_GREEN		"\033[36;42m"	// 青字绿底
#define NNN_CL_CYAN_YELLOW		"\033[36;43m"	// 青字黄底
#define NNN_CL_CYAN_BLUE		"\033[36;44m"	// 青字蓝底
#define NNN_CL_CYAN_MAGENTA		"\033[36;45m"	// 青字紫底
#define NNN_CL_CYAN_CYAN		"\033[36;46m"	// 青字青底
#define NNN_CL_CYAN_WHITE		"\033[36;47m"	// 青字白底

#define NNN_CL_WHITE_BLACK		"\033[37;40m"	// 白字黑底
#define NNN_CL_WHITE_RED		"\033[37;41m"	// 白字红底
#define NNN_CL_WHITE_GREEN		"\033[37;42m"	// 白字绿底
#define NNN_CL_WHITE_YELLOW		"\033[37;43m"	// 白字黄底
#define NNN_CL_WHITE_BLUE		"\033[37;44m"	// 白字蓝底
#define NNN_CL_WHITE_MAGENTA	"\033[37;45m"	// 白字紫底
#define NNN_CL_WHITE_CYAN		"\033[37;46m"	// 白字青底
#define NNN_CL_WHITE_WHITE		"\033[37;47m"	// 白字白底

//#define NNN_CL_WTBL			"\033[37;44m"	// 白字蓝底
//#define NNN_CL_XXBL			"\033[0;44m"	// 蓝底
//#define NNN_CL_PASS			"\033[0;32;42m"	// 绿底绿字

#endif	// _NNNLIB___CONSOLE___CONSOLE_MACRO_H_
