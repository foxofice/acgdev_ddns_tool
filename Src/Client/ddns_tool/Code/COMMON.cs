using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ddns_tool
{
	internal class COMMON
	{
		internal const string	STR_TRUE	= "√";
		internal const string	STR_FALSE	= "×";

		/*==============================================================
		 * 打开 URL
		 *==============================================================*/
		internal static void	OpenURL(string url)
		{
			ProcessStartInfo psi = new()
			{
				FileName		= url,
				UseShellExecute	= true	// 确保 URL 由 shell 处理（默认浏览器）
			};

			Process.Start(psi);
		}
	};
}	// namespace ddns_tool
