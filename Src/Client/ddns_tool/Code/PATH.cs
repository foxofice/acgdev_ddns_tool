using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ddns_tool
{
	internal class PATH
	{
		/*==============================================================
		 * 获取相对路径
		 *==============================================================*/
		internal static string GetRelativePath(string fromPath, string toPath)
		{
			Uri fromUri	= new(fromPath);
			Uri toUri	= new(toPath);

			if(fromUri.Scheme != toUri.Scheme)
				return toPath;

			Uri relativeUri = fromUri.MakeRelativeUri(toUri);
			string relativePath = Uri.UnescapeDataString(relativeUri.ToString());

			if(toUri.Scheme.ToUpperInvariant() == "FILE")
				relativePath = relativePath.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);

			return relativePath;
		}

		/*==============================================================
		 * 打开所在的文件夹
		 *==============================================================*/
		internal static void	open_dir(string dir)
		{
			Process.Start("explorer.exe", dir);
		}
	};
}	// namespace ddns_tool
