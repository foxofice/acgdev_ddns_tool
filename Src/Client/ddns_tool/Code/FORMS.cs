using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
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

		private static readonly PropertyInfo?	m_s_DoubleBufferedProp = typeof(Control).GetProperty(	"DoubleBuffered",
																										BindingFlags.Instance | BindingFlags.NonPublic );

		/*==============================================================
		 * 设置 DoubleBuffered
		 *==============================================================*/
		internal static void	Set_DoubleBuffered(Control c, bool enabled)
		{
			if(c == null || m_s_DoubleBufferedProp == null)
				return;

			m_s_DoubleBufferedProp.SetValue(c, enabled);
		}

		const int LVM_SETCOLUMNWIDTH		= 0x101E;
		const int LVSCW_AUTOSIZE_USEHEADER	= -2;

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

		static void AutoFillLastColumn(ListView lv, int colIndex)
		{
			SendMessage(lv.Handle, LVM_SETCOLUMNWIDTH, (IntPtr)colIndex, (IntPtr)LVSCW_AUTOSIZE_USEHEADER);
		}

		/*==============================================================
		 * 自动调整 ListView 的列大小（所有列「根据列标题、内容进行调整」）
		 *==============================================================*/
		internal static void	auto_size_ListView(ListView LV)
		{
			//LV.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize | ColumnHeaderAutoResizeStyle.ColumnContent);
			//LV.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

			// 不使用 .Net 自带的 API，改为 WinAPI（.Net 的 API 有 bug）
			for(int i=0; i<LV.Columns.Count; ++i)
				AutoFillLastColumn(LV, i);
		}

		/*==============================================================
		 * 自动调整 ListView 的列大小（把剩余区域「根据列标题、内容进行调整」）
		 *==============================================================*/
		internal static void	auto_size_ListView(ListView LV, int[] widths)
		{
			int idx = -1;

			for(int i = 0; i < widths.Length; ++i)
			{
				if(widths[i] > 0)
					LV.Columns[i].Width = widths[i];
				else
					idx = i;
			}	// for

			if(idx >= 0)
				AutoFillLastColumn(LV, idx);
		}

		/*==============================================================
		 * 自动调整 ListView 的列大小（剩余区域自动填充。固定保留 21 放置垂直滚动条）
		 *==============================================================*/
		internal static void	auto_size_ListView_fixed(ListView LV, int[] widths)
		{
			for(int i = 0; i < widths.Length; ++i)
			{
				if(widths[i] > 0)
					LV.Columns[i].Width = widths[i];
				else
					LV.Columns[i].Width = LV.Width - 21 - widths.Sum();
			}	// for
		}

		/*==============================================================
		 * 自动调整 ListView 的列大小（把剩余区域自动「填满」）
		 *==============================================================*/
		internal static void auto_size_ListView_fill(ListView LV, int[] widths)
		{
			int fillIndex = -1;
			int fixedWidth = 0;

			for(int i = 0; i < widths.Length; ++i)
			{
				if(widths[i] > 0)
				{
					LV.Columns[i].Width = widths[i];
					fixedWidth += widths[i];
				}
				else
				{
					fillIndex = i;
				}
			}	// for

			if(fillIndex >= 0)
			{
				// 可用内容区宽度
				int availableWidth = LV.ClientSize.Width;

				// 如果垂直滚动条可见，减去它的宽度
				if(LV.Items.Count > LV.ClientSize.Height / LV.Font.Height)
					availableWidth -= SystemInformation.VerticalScrollBarWidth;

				// 剩余宽度给填充列
				LV.Columns[fillIndex].Width = Math.Max(0, availableWidth - fixedWidth);
			}
		}
	};
}	// namespace ddns_tool
