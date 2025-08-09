//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 控制台（宏）
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNLIB___CONSOLE___CONSOLE_MACRO_H_
#define _NNNLIB___CONSOLE___CONSOLE_MACRO_H_

// ANSI 代码定义
#define ANSI_RESET						"0"			// 重置（清除所有样式和颜色）
#define ANSI_BOLD						"1"			// 加粗（高亮）
#define ANSI_BRIGHT						"1"
#define ANSI_DIM						"2"			// 变暗/低亮度（部分终端支持）
#define ANSI_FAINT						"2"
#define ANSI_ITALIC						"3"			// 斜体（部分终端支持）
#define ANSI_UNDERLINE					"4"			// 下划线
#define ANSI_SLOW_BLINK					"5"			// 文本缓慢闪烁（部分终端支持）
#define ANSI_RAPID_BLINK				"6"			// 文本快速闪烁（部分终端支持）
#define ANSI_REVERSE					"7"			// 交换前景色和背景色
#define ANSI_INVERT						"7"
#define ANSI_HIDDEN						"8"			// 文本不可见（部分终端支持）
#define ANSI_STRIKETHROUGH				"9"			// 文本上添加删除线（部分终端支持）

//（前景色）
#define ANSI_BLACK						"30"		// 黑色
#define ANSI_RED						"31"		// 红色
#define ANSI_GREEN						"32"		// 绿色
#define ANSI_YELLOW						"33"		// 黄色
#define ANSI_BLUE						"34"		// 蓝色
#define ANSI_MAGENTA					"35"		// 紫色/洋红
#define ANSI_CYAN						"36"		// 青色
#define ANSI_WHITE						"37"		// 白色

//（背景色）
#define ANSI_BLACK_BG					"40"		// 黑色
#define ANSI_RED_BG						"41"		// 红色
#define ANSI_GREEN_BG					"42"		// 绿色
#define ANSI_YELLOW_BG					"43"		// 黄色
#define ANSI_BLUE_BG					"44"		// 蓝色
#define ANSI_MAGENTA_BG					"45"		// 紫色/洋红
#define ANSI_CYAN_BG					"46"		// 青色
#define ANSI_WHITE_BG					"47"		// 白色

// 256 色（idx = 0~255）
// 0-7 为标准颜色，8-15 为高亮颜色，16-231 为 6×6×6 立方体颜色，232-255 为灰度
#define ANSI_256(idx)					"38;5;"#idx	// 前景色
#define ANSI_256_BG(idx)				"48;5;"#idx	// 背景色

// 24 位 RGB
#define ANSI_RGB(r, g, b)				"38;2;"#r##";"#g##";"#b
#define ANSI_RGB_BG(r, g, b)			"48;2;"#r##";"#g##";" #b

// 设置颜色（格式：\033[<参数1>;<参数2>;...;<参数N>m）
#define NNN_ANSI_COLOR(...)				"\033["##__VA_ARGS__##"m"

#define NNN_ANSI_RESET					NNN_ANSI_COLOR(ANSI_RESET)

#define NNN_ANSI_GRAY					NNN_ANSI_COLOR(ANSI_BOLD ";" ANSI_BLACK)	// 灰
#define NNN_ANSI_RED					NNN_ANSI_COLOR(ANSI_BOLD ";" ANSI_RED)		// 红
#define NNN_ANSI_GREEN					NNN_ANSI_COLOR(ANSI_BOLD ";" ANSI_GREEN)	// 绿
#define NNN_ANSI_YELLOW					NNN_ANSI_COLOR(ANSI_BOLD ";" ANSI_YELLOW)	// 黄
#define NNN_ANSI_BLUE					NNN_ANSI_COLOR(ANSI_BOLD ";" ANSI_BLUE)		// 蓝
#define NNN_ANSI_MAGENTA				NNN_ANSI_COLOR(ANSI_BOLD ";" ANSI_MAGENTA)	// 紫
#define NNN_ANSI_CYAN					NNN_ANSI_COLOR(ANSI_BOLD ";" ANSI_CYAN)		// 青
#define NNN_ANSI_WHITE					NNN_ANSI_COLOR(ANSI_BOLD ";" ANSI_WHITE)	// 白

// 光标控制相关
#define NNN_ANSI_HOME					"\033[0G"						// 移动光标到行首（Moves the cursor to indicated column in current row）
#define NNN_ANSI_POS(row, col)			"\033["#row##";"#col##"H"		// 光标定位到 row,col 位置
#define NNN_ANSI_XY(row, col)			"\033["#row##";"#col##"f"		// 光标定位到 row,col 位置
#define NNN_ANSI_UP(n)					"\033["#n##"A"					// 光标上移 n 行
#define NNN_ANSI_DOWN(n)				"\033["#n##"B"					// 光标下移 n 行
#define NNN_ANSI_RIGHT(n)				"\033["#n##"C"					// 光标右移 n 行
#define NNN_ANSI_LEFT(n)				"\033["#n##"D"					// 光标左移 n 行

#define NNN_ANSI_SHOW_CURSOR			"\033[?25h"						// 使光标可见
#define NNN_ANSI_HIDE_CURSOR			"\033[?25l"						// 隐藏光标

// 清屏
#define NNN_ANSI_CLS_BACK				"\033[0J"						// 清除光标到屏幕末尾，保留光标位置
#define NNN_ANSI_CLS_FRONT				"\033[1J"						// 清除屏幕开头到光标，保留光标位置
#define NNN_ANSI_CLS					"\033[2J"						// 清除整个屏幕，并将光标移到左上角

// 清除行
#define NNN_ANSI_CLL_BACK				"\033[0K"						// 清除从光标当前位置（包含光标所在位置）到行尾的内容
#define NNN_ANSI_CLL_FRONT				"\033[1K"						// 清除从行首到光标当前位置（包含光标所在位置）的内容
#define NNN_ANSI_CLL					"\033[2K"						// 清除当前行的全部内容

// 保存和恢复光标位置
#define NNN_ANSI_SAVE					"\033[s"						// 保存当前光标位置和属性
#define NNN_ANSI_LOAD					"\033[u"						// 恢复之前保存的光标位置和属性

// 屏幕滚动控制
#define NNN_ANSI_SCROLL_UP(n)			"\033["#n##"S"					// 整个屏幕向上滚动 n 行
#define NNN_ANSI_SCROLL_DOWN(n)			"\033["#n##"T"					// 整个屏幕向下滚动 n 行

// 设置显示区域
#define NNN_ANSI_VIEWPORT(top, bottom)	"\033["#top##";"#bottom##"r"	// 设置滚动区域（定义屏幕的滚动区域，从第 <top> 行到第 <bottom> 行）

// 缓冲区
#define NNN_ANSI_BACK_BUFFER			"\033[?1049h"					// 启用替代缓冲区：切换到替代缓冲区（常用于全屏应用）
#define NNN_ANSI_FRONT_BUFFER			"\033[?1049l"					// 恢复主缓冲区：返回主缓冲区

#endif	// _NNNLIB___CONSOLE___CONSOLE_MACRO_H_
