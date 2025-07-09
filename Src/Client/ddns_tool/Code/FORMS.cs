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

		/*==============================================================
		 * 执行委托
		 *==============================================================*/
		internal static void	invoke(Action func)
		{
			if(frm_MainForm.m_s_Mainform.InvokeRequired)
				frm_MainForm.m_s_Mainform.Invoke(func);
			else
				func();
		}
		//--------------------------------------------------
		internal static TResult	invoke<TResult>(Func<TResult> func)
		{
			if(frm_MainForm.m_s_Mainform.InvokeRequired)
				return (TResult)frm_MainForm.m_s_Mainform.Invoke(func);
			else
				return func();
		}
	};
}	// namespace ddns_tool
