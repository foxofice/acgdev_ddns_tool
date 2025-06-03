using System;
using System.Collections.Generic;
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
			Uri fromUri	= new Uri(fromPath);
			Uri toUri	= new Uri(toPath);

			if(fromUri.Scheme != toUri.Scheme)
				return toPath;

			Uri relativeUri = fromUri.MakeRelativeUri(toUri);
			string relativePath = Uri.UnescapeDataString(relativeUri.ToString());

			if(toUri.Scheme.ToUpperInvariant() == "FILE")
				relativePath = relativePath.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);

			return relativePath;
		}
	};
}	// namespace ddns_tool
