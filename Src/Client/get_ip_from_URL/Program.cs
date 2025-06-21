using System.Diagnostics;
using System.Net;

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
				string filename = Path.GetFileName(Process.GetCurrentProcess().MainModule!.FileName);

				Console.Error.WriteLine($"{filename} <url> [\"{V4_FLAG}\"]");
				return 1;
			}

			string	url			= args[0];
			bool	use_ipv4	= (args.Length >= 2) && (string.Compare(args[1], V4_FLAG, true) == 0);

			var task = use_ipv4 ? new c_HttpClient_ipv4().GetStringAsync(url) : new HttpClient().GetStringAsync(url);

			try
			{
				Console.WriteLine(task.GetAwaiter().GetResult());
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
