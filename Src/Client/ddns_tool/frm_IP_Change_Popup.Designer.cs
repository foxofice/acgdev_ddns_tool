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
			this.listView_Main = new System.Windows.Forms.ListView();
			this.columnHeader_Domain = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader_IPv4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader_IPv6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.SuspendLayout();
			// 
			// listView_Main
			// 
			this.listView_Main.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader_Domain,
            this.columnHeader_IPv4,
            this.columnHeader_IPv6});
			this.listView_Main.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listView_Main.FullRowSelect = true;
			this.listView_Main.GridLines = true;
			this.listView_Main.HideSelection = false;
			this.listView_Main.Location = new System.Drawing.Point(0, 0);
			this.listView_Main.Name = "listView_Main";
			this.listView_Main.Size = new System.Drawing.Size(413, 265);
			this.listView_Main.TabIndex = 1;
			this.listView_Main.UseCompatibleStateImageBehavior = false;
			this.listView_Main.View = System.Windows.Forms.View.Details;
			this.listView_Main.Resize += new System.EventHandler(this.listView_Main_Resize);
			// 
			// columnHeader_Domain
			// 
			this.columnHeader_Domain.Text = "域名";
			this.columnHeader_Domain.Width = 260;
			// 
			// columnHeader_IPv4
			// 
			this.columnHeader_IPv4.Text = "IPv4 变化";
			this.columnHeader_IPv4.Width = 66;
			// 
			// columnHeader_IPv6
			// 
			this.columnHeader_IPv6.Text = "IPv6 变化";
			this.columnHeader_IPv6.Width = 66;
			// 
			// frm_IP_Change_Popup
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(413, 265);
			this.Controls.Add(this.listView_Main);
			this.Name = "frm_IP_Change_Popup";
			this.Text = "IP发生变化的域名";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_IP_Change_Popup_FormClosing);
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.ListView listView_Main;
		private System.Windows.Forms.ColumnHeader columnHeader_Domain;
		private System.Windows.Forms.ColumnHeader columnHeader_IPv4;
		private System.Windows.Forms.ColumnHeader columnHeader_IPv6;
	}
}