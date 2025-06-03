using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ddns_tool
{
	internal class FORMS
	{
		/*==============================================================
		 * 激活窗口
		 *==============================================================*/
		internal static void active_form(Form form)
		{
			form.Show();
			form.BringToFront();
			form.Activate();
			form.WindowState = FormWindowState.Normal;
		}
	};
}	// namespace ddns_tool
