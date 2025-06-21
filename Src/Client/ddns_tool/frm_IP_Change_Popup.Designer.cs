namespace ddns_tool
{
	partial class frm_IP_Change_Popup
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if(disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			listView_Main = new ListView();
			columnHeader_Domain = new ColumnHeader();
			columnHeader_IPv4 = new ColumnHeader();
			columnHeader_IPv6 = new ColumnHeader();
			SuspendLayout();
			// 
			// listView_Main
			// 
			listView_Main.Columns.AddRange(new ColumnHeader[] { columnHeader_Domain, columnHeader_IPv4, columnHeader_IPv6 });
			listView_Main.Dock = DockStyle.Fill;
			listView_Main.FullRowSelect = true;
			listView_Main.GridLines = true;
			listView_Main.Location = new Point(0, 0);
			listView_Main.Margin = new Padding(3, 2, 3, 2);
			listView_Main.Name = "listView_Main";
			listView_Main.Size = new Size(413, 244);
			listView_Main.TabIndex = 0;
			listView_Main.UseCompatibleStateImageBehavior = false;
			listView_Main.View = View.Details;
			listView_Main.Resize += listView_Main_Resize;
			// 
			// columnHeader_Domain
			// 
			columnHeader_Domain.Text = "域名";
			columnHeader_Domain.Width = 260;
			// 
			// columnHeader_IPv4
			// 
			columnHeader_IPv4.Text = "IPv4 变化";
			columnHeader_IPv4.Width = 66;
			// 
			// columnHeader_IPv6
			// 
			columnHeader_IPv6.Text = "IPv6 变化";
			columnHeader_IPv6.Width = 66;
			// 
			// frm_IP_Change_Popup
			// 
			AutoScaleDimensions = new SizeF(6F, 12F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(413, 244);
			Controls.Add(listView_Main);
			Font = new Font("新宋体", 9F);
			Margin = new Padding(3, 2, 3, 2);
			Name = "frm_IP_Change_Popup";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "IP发生变化的域名";
			FormClosing += frm_IP_Change_Popup_FormClosing;
			Load += frm_IP_Change_Popup_Load;
			ResumeLayout(false);
		}

		#endregion

		private ListView listView_Main;
		private ColumnHeader columnHeader_Domain;
		private ColumnHeader columnHeader_IPv4;
		private ColumnHeader columnHeader_IPv6;
	}
}