using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ddns
{
	internal class IMAGE
	{
		/*==============================================================
		 * Image -> Icon
		 *==============================================================*/
		public static Icon image_to_ico(Image image)
		{
			return Icon.FromHandle(((Bitmap)image).GetHicon());
		}
	};
}	// namespace ddns
