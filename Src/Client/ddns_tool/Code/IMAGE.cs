using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ddns_tool
{
	internal class IMAGE
	{
		/*==============================================================
		 * Image -> Icon
		 *==============================================================*/
		internal static Icon image_to_ico(Image image)
		{
			return Icon.FromHandle(((Bitmap)image).GetHicon());
		}

		/*==============================================================
		 * 获取当前 exe 的 Icon
		 *==============================================================*/
		internal static Icon get_exe_icon()
		{
			return Icon.ExtractAssociatedIcon(Process.GetCurrentProcess().MainModule!.FileName)!;
		}
	};
}	// namespace ddns_tool
