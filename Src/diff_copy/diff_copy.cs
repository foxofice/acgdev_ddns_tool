using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace diff_copy
{
	internal class diff_copy
	{
		static int Main(string[] args)
		{
			string fileName = Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName);

			if(args.Length != 2)
			{
				Console.WriteLine($"usage: {fileName} <src_file> <dst_file>");
				return 1;
			}

			string src_file = args[0];
			string dst_file = args[1];

			if(!File.Exists(src_file))
			{
				Console.WriteLine($"<{src_file}> not exists");
				return 1;
			}

			bool do_copy = true;

			if(File.Exists(dst_file))
			{
				FileInfo fi_src = new FileInfo(src_file);
				FileInfo fi_dst = new FileInfo(dst_file);

				if(	fi_src.Length			== fi_dst.Length	&&
					fi_src.LastWriteTime	== fi_dst.LastWriteTime )
					do_copy = false;
			}

			if(do_copy)
			{
				string dir = Path.GetDirectoryName(dst_file);
				if(!Directory.Exists(dir))
					Directory.CreateDirectory(dir);

				File.Copy(src_file, dst_file, true);
			}

			Console.WriteLine($"[{(do_copy ? "Copy" : "Skip")}] {src_file} -> {dst_file}");

			return 0;
		}
	};
}	// namespace diff_copy
