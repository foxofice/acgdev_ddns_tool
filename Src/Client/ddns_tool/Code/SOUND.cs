using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ddns_tool
{
	internal class SOUND
	{
		[DllImport("winmm.dll")]
		private static extern long mciSendString(string command, StringBuilder returnValue, int returnLength, IntPtr winHandle);

		/*==============================================================
		 * 播放
		 *==============================================================*/
		internal static void Play(string filePath)
		{
			string command = $"open \"{filePath}\" type mpegvideo alias SOUND";
			mciSendString(command, null, 0, IntPtr.Zero);

			command = "play SOUND repeat";
			mciSendString(command, null, 0, IntPtr.Zero);
		}

		/*==============================================================
		 * 暂停
		 *==============================================================*/
		internal static void Pause()
		{
			string command = "pause SOUND";
			mciSendString(command, null, 0, IntPtr.Zero);
		}

		/*==============================================================
		 * 停止
		 *==============================================================*/
		internal static void Stop()
		{
			string command = "stop SOUND";
			mciSendString(command, null, 0, IntPtr.Zero);

			command = "close SOUND";
			mciSendString(command, null, 0, IntPtr.Zero);
		}
	};
}	// namespace ddns_tool
