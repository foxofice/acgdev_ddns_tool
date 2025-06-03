using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace get_ip_from_URL
{
	internal class Program
	{
		const string V4_FLAG	= "v4";

		/*==============================================================
		 * 显示错误
		 *==============================================================*/
		static void show_error(string txt)
		{
			ConsoleColor c = Console.ForegroundColor;

			Console.ForegroundColor = ConsoleColor.Red;
			Console.Error.WriteLine(txt);

			Console.ForegroundColor = c;
		}

		/*==============================================================
		 * 主函数
		 *==============================================================*/
		static int Main(string[] args)
		{
			if(args.Length == 0)
			{
				string filename = Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName);

				Console.Error.WriteLine($"{filename} <url> [\"{V4_FLAG}\"]");
				return 1;
			}

			string	url			= args[0];
			bool	use_ipv4	= (args.Length >= 2) && (string.Compare(args[1], V4_FLAG, true) == 0);

			WebClient wc = use_ipv4 ? new c_WebClient_ipv4() : new WebClient();

			try
			{
				Console.WriteLine(wc.DownloadString(url));
			}
			catch(Exception ex)
			{
				show_error(ex.Message);
				return 1;
			}

			return 0;
		}
	};
}	// namespace get_ip_from_URL
