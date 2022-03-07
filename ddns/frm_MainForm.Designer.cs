namespace ddns
{
	partial class frm_MainForm
	{
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
		protected override void Dispose(bool disposing)
		{
			if(disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows 窗体设计器生成的代码

		/// <summary>
		/// 设计器支持所需的方法 - 不要修改
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.button_Settings_Update = new System.Windows.Forms.Button();
			this.groupBox_Records = new System.Windows.Forms.GroupBox();
			this.listView_Records = new System.Windows.Forms.ListView();
			this.columnHeader_Records_Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader_Records_Domain = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader_Records_TTL = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader_Records_Last_Result = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader_Records_Last_IP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.contextMenuStrip_Records = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.ToolStripMenuItem_Records_Add = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItem_Records_Edit = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItem_Records_Delete = new System.Windows.Forms.ToolStripMenuItem();
			this.groupBox_Settings = new System.Windows.Forms.GroupBox();
			this.checkBox_Settings_AutoUpdate = new System.Windows.Forms.CheckBox();
			this.checkBox_Settings_Show_Secret = new System.Windows.Forms.CheckBox();
			this.checkBox_Settings_Show_Key = new System.Windows.Forms.CheckBox();
			this.comboBox_Settings_Get_IP_URL = new System.Windows.Forms.ComboBox();
			this.checkBox_Settings_Save_Key_and_Secret = new System.Windows.Forms.CheckBox();
			this.numericUpDown_Settings_Interval = new System.Windows.Forms.NumericUpDown();
			this.label_Settings_Secret = new System.Windows.Forms.Label();
			this.label_Settings_Key = new System.Windows.Forms.Label();
			this.label_Settings_Interval = new System.Windows.Forms.Label();
			this.textBox_Settings_Last_IP = new System.Windows.Forms.TextBox();
			this.textBox_Settings_Secret = new System.Windows.Forms.TextBox();
			this.textBox_Settings_Key = new System.Windows.Forms.TextBox();
			this.label_Settings_Last_IP = new System.Windows.Forms.Label();
			this.label_Settings_Get_IP_URL = new System.Windows.Forms.Label();
			this.listView_Logs = new System.Windows.Forms.ListView();
			this.columnHeader_Logs_Time = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader_Logs_Log = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.contextMenuStrip_Logs = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.ToolStripMenuItem_Logs_Copy = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItem_Logs_Delete = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.ToolStripMenuItem_Logs_SelectAll = new System.Windows.Forms.ToolStripMenuItem();
			this.notifyIcon_Main = new System.Windows.Forms.NotifyIcon(this.components);
			this.contextMenuStrip_NotifyIcon = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.ToolStripMenuItem_Open = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItem_Exit = new System.Windows.Forms.ToolStripMenuItem();
			this.timer_Save_Config = new System.Windows.Forms.Timer(this.components);
			this.label_Tip = new System.Windows.Forms.Label();
			this.linkLabel_godaddy = new System.Windows.Forms.LinkLabel();
			this.linkLabel_Website = new System.Windows.Forms.LinkLabel();
			this.timer_Update = new System.Windows.Forms.Timer(this.components);
			this.checkBox_Settings_Specific_IP = new System.Windows.Forms.CheckBox();
			this.checkBox_Settings_Update_Force = new System.Windows.Forms.CheckBox();
			this.groupBox_Records.SuspendLayout();
			this.contextMenuStrip_Records.SuspendLayout();
			this.groupBox_Settings.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Settings_Interval)).BeginInit();
			this.contextMenuStrip_Logs.SuspendLayout();
			this.contextMenuStrip_NotifyIcon.SuspendLayout();
			this.SuspendLayout();
			// 
			// button_Settings_Update
			// 
			this.button_Settings_Update.Location = new System.Drawing.Point(119, 176);
			this.button_Settings_Update.Name = "button_Settings_Update";
			this.button_Settings_Update.Size = new System.Drawing.Size(75, 23);
			this.button_Settings_Update.TabIndex = 17;
			this.button_Settings_Update.Text = "立即更新";
			this.button_Settings_Update.UseVisualStyleBackColor = true;
			this.button_Settings_Update.Click += new System.EventHandler(this.button_Settings_Update_Click);
			// 
			// groupBox_Records
			// 
			this.groupBox_Records.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.groupBox_Records.Controls.Add(this.listView_Records);
			this.groupBox_Records.Location = new System.Drawing.Point(12, 223);
			this.groupBox_Records.Name = "groupBox_Records";
			this.groupBox_Records.Size = new System.Drawing.Size(441, 124);
			this.groupBox_Records.TabIndex = 19;
			this.groupBox_Records.TabStop = false;
			this.groupBox_Records.Text = "要更新的域名列表（右键菜单设置）";
			// 
			// listView_Records
			// 
			this.listView_Records.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listView_Records.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader_Records_Name,
            this.columnHeader_Records_Domain,
            this.columnHeader_Records_TTL,
            this.columnHeader_Records_Last_Result,
            this.columnHeader_Records_Last_IP});
			this.listView_Records.ContextMenuStrip = this.contextMenuStrip_Records;
			this.listView_Records.FullRowSelect = true;
			this.listView_Records.GridLines = true;
			this.listView_Records.HideSelection = false;
			this.listView_Records.Location = new System.Drawing.Point(6, 20);
			this.listView_Records.Name = "listView_Records";
			this.listView_Records.Size = new System.Drawing.Size(429, 98);
			this.listView_Records.TabIndex = 20;
			this.listView_Records.UseCompatibleStateImageBehavior = false;
			this.listView_Records.View = System.Windows.Forms.View.Details;
			this.listView_Records.SelectedIndexChanged += new System.EventHandler(this.listView_Records_SelectedIndexChanged);
			this.listView_Records.DoubleClick += new System.EventHandler(this.listView_Records_DoubleClick);
			// 
			// columnHeader_Records_Name
			// 
			this.columnHeader_Records_Name.Text = "Name";
			// 
			// columnHeader_Records_Domain
			// 
			this.columnHeader_Records_Domain.Text = "根域名";
			this.columnHeader_Records_Domain.Width = 120;
			// 
			// columnHeader_Records_TTL
			// 
			this.columnHeader_Records_TTL.Text = "TTL";
			this.columnHeader_Records_TTL.Width = 36;
			// 
			// columnHeader_Records_Last_Result
			// 
			this.columnHeader_Records_Last_Result.Text = "上次更新结果";
			this.columnHeader_Records_Last_Result.Width = 84;
			// 
			// columnHeader_Records_Last_IP
			// 
			this.columnHeader_Records_Last_IP.Text = "上次成功更新的IP";
			this.columnHeader_Records_Last_IP.Width = 108;
			// 
			// contextMenuStrip_Records
			// 
			this.contextMenuStrip_Records.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_Records_Add,
            this.ToolStripMenuItem_Records_Edit,
            this.ToolStripMenuItem_Records_Delete});
			this.contextMenuStrip_Records.Name = "contextMenuStrip_Records";
			this.contextMenuStrip_Records.Size = new System.Drawing.Size(101, 70);
			// 
			// ToolStripMenuItem_Records_Add
			// 
			this.ToolStripMenuItem_Records_Add.Image = global::ddns.res_Main.Add;
			this.ToolStripMenuItem_Records_Add.Name = "ToolStripMenuItem_Records_Add";
			this.ToolStripMenuItem_Records_Add.Size = new System.Drawing.Size(100, 22);
			this.ToolStripMenuItem_Records_Add.Text = "添加";
			this.ToolStripMenuItem_Records_Add.Click += new System.EventHandler(this.ToolStripMenuItem_Records_Add_Click);
			// 
			// ToolStripMenuItem_Records_Edit
			// 
			this.ToolStripMenuItem_Records_Edit.Enabled = false;
			this.ToolStripMenuItem_Records_Edit.Image = global::ddns.res_Main.Edit;
			this.ToolStripMenuItem_Records_Edit.Name = "ToolStripMenuItem_Records_Edit";
			this.ToolStripMenuItem_Records_Edit.Size = new System.Drawing.Size(100, 22);
			this.ToolStripMenuItem_Records_Edit.Text = "修改";
			this.ToolStripMenuItem_Records_Edit.Click += new System.EventHandler(this.ToolStripMenuItem_Records_Edit_Click);
			// 
			// ToolStripMenuItem_Records_Delete
			// 
			this.ToolStripMenuItem_Records_Delete.Enabled = false;
			this.ToolStripMenuItem_Records_Delete.Image = global::ddns.res_Main.Delete;
			this.ToolStripMenuItem_Records_Delete.Name = "ToolStripMenuItem_Records_Delete";
			this.ToolStripMenuItem_Records_Delete.Size = new System.Drawing.Size(100, 22);
			this.ToolStripMenuItem_Records_Delete.Text = "删除";
			this.ToolStripMenuItem_Records_Delete.Click += new System.EventHandler(this.ToolStripMenuItem_Records_Delete_Click);
			// 
			// groupBox_Settings
			// 
			this.groupBox_Settings.Controls.Add(this.checkBox_Settings_Update_Force);
			this.groupBox_Settings.Controls.Add(this.checkBox_Settings_AutoUpdate);
			this.groupBox_Settings.Controls.Add(this.checkBox_Settings_Show_Secret);
			this.groupBox_Settings.Controls.Add(this.checkBox_Settings_Specific_IP);
			this.groupBox_Settings.Controls.Add(this.checkBox_Settings_Show_Key);
			this.groupBox_Settings.Controls.Add(this.comboBox_Settings_Get_IP_URL);
			this.groupBox_Settings.Controls.Add(this.checkBox_Settings_Save_Key_and_Secret);
			this.groupBox_Settings.Controls.Add(this.numericUpDown_Settings_Interval);
			this.groupBox_Settings.Controls.Add(this.label_Settings_Secret);
			this.groupBox_Settings.Controls.Add(this.button_Settings_Update);
			this.groupBox_Settings.Controls.Add(this.label_Settings_Key);
			this.groupBox_Settings.Controls.Add(this.label_Settings_Interval);
			this.groupBox_Settings.Controls.Add(this.textBox_Settings_Last_IP);
			this.groupBox_Settings.Controls.Add(this.textBox_Settings_Secret);
			this.groupBox_Settings.Controls.Add(this.textBox_Settings_Key);
			this.groupBox_Settings.Controls.Add(this.label_Settings_Last_IP);
			this.groupBox_Settings.Controls.Add(this.label_Settings_Get_IP_URL);
			this.groupBox_Settings.Location = new System.Drawing.Point(12, 12);
			this.groupBox_Settings.Name = "groupBox_Settings";
			this.groupBox_Settings.Size = new System.Drawing.Size(441, 205);
			this.groupBox_Settings.TabIndex = 1;
			this.groupBox_Settings.TabStop = false;
			this.groupBox_Settings.Text = "设置";
			// 
			// checkBox_Settings_AutoUpdate
			// 
			this.checkBox_Settings_AutoUpdate.AutoSize = true;
			this.checkBox_Settings_AutoUpdate.Checked = true;
			this.checkBox_Settings_AutoUpdate.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox_Settings_AutoUpdate.Location = new System.Drawing.Point(191, 74);
			this.checkBox_Settings_AutoUpdate.Name = "checkBox_Settings_AutoUpdate";
			this.checkBox_Settings_AutoUpdate.Size = new System.Drawing.Size(72, 16);
			this.checkBox_Settings_AutoUpdate.TabIndex = 9;
			this.checkBox_Settings_AutoUpdate.Text = "自动更新";
			this.checkBox_Settings_AutoUpdate.UseVisualStyleBackColor = true;
			this.checkBox_Settings_AutoUpdate.CheckedChanged += new System.EventHandler(this.checkBox_Settings_AutoUpdate_CheckedChanged);
			// 
			// checkBox_Settings_Show_Secret
			// 
			this.checkBox_Settings_Show_Secret.AutoSize = true;
			this.checkBox_Settings_Show_Secret.Location = new System.Drawing.Point(351, 129);
			this.checkBox_Settings_Show_Secret.Name = "checkBox_Settings_Show_Secret";
			this.checkBox_Settings_Show_Secret.Size = new System.Drawing.Size(48, 16);
			this.checkBox_Settings_Show_Secret.TabIndex = 15;
			this.checkBox_Settings_Show_Secret.Text = "显示";
			this.checkBox_Settings_Show_Secret.UseVisualStyleBackColor = true;
			this.checkBox_Settings_Show_Secret.CheckedChanged += new System.EventHandler(this.checkBox_Settings_Show_Secret_CheckedChanged);
			// 
			// checkBox_Settings_Show_Key
			// 
			this.checkBox_Settings_Show_Key.AutoSize = true;
			this.checkBox_Settings_Show_Key.Location = new System.Drawing.Point(351, 102);
			this.checkBox_Settings_Show_Key.Name = "checkBox_Settings_Show_Key";
			this.checkBox_Settings_Show_Key.Size = new System.Drawing.Size(48, 16);
			this.checkBox_Settings_Show_Key.TabIndex = 12;
			this.checkBox_Settings_Show_Key.Text = "显示";
			this.checkBox_Settings_Show_Key.UseVisualStyleBackColor = true;
			this.checkBox_Settings_Show_Key.CheckedChanged += new System.EventHandler(this.checkBox_Settings_Show_Key_CheckedChanged);
			// 
			// comboBox_Settings_Get_IP_URL
			// 
			this.comboBox_Settings_Get_IP_URL.FormattingEnabled = true;
			this.comboBox_Settings_Get_IP_URL.Items.AddRange(new object[] {
            "https://icanhazip.com",
            "https://ipinfo.io/ip",
            "https://ip.42.pl/raw",
            "https://api.ip.sb/ip",
            "https://ip.3322.net",
            "https://ip.qaros.com",
            "https://ident.me",
            "https://api.ipify.org/"});
			this.comboBox_Settings_Get_IP_URL.Location = new System.Drawing.Point(119, 20);
			this.comboBox_Settings_Get_IP_URL.Name = "comboBox_Settings_Get_IP_URL";
			this.comboBox_Settings_Get_IP_URL.Size = new System.Drawing.Size(316, 20);
			this.comboBox_Settings_Get_IP_URL.TabIndex = 3;
			this.comboBox_Settings_Get_IP_URL.TextChanged += new System.EventHandler(this.comboBox_Settings_Get_IP_URL_TextChanged);
			// 
			// checkBox_Settings_Save_Key_and_Secret
			// 
			this.checkBox_Settings_Save_Key_and_Secret.AutoSize = true;
			this.checkBox_Settings_Save_Key_and_Secret.Location = new System.Drawing.Point(119, 154);
			this.checkBox_Settings_Save_Key_and_Secret.Name = "checkBox_Settings_Save_Key_and_Secret";
			this.checkBox_Settings_Save_Key_and_Secret.Size = new System.Drawing.Size(192, 16);
			this.checkBox_Settings_Save_Key_and_Secret.TabIndex = 16;
			this.checkBox_Settings_Save_Key_and_Secret.Text = "保存 Key/Secret 到配置文件中";
			this.checkBox_Settings_Save_Key_and_Secret.UseVisualStyleBackColor = true;
			this.checkBox_Settings_Save_Key_and_Secret.CheckedChanged += new System.EventHandler(this.checkBox_Settings_Save_Key_and_Secret_CheckedChanged);
			// 
			// numericUpDown_Settings_Interval
			// 
			this.numericUpDown_Settings_Interval.Location = new System.Drawing.Point(119, 73);
			this.numericUpDown_Settings_Interval.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
			this.numericUpDown_Settings_Interval.Name = "numericUpDown_Settings_Interval";
			this.numericUpDown_Settings_Interval.Size = new System.Drawing.Size(66, 21);
			this.numericUpDown_Settings_Interval.TabIndex = 8;
			this.numericUpDown_Settings_Interval.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.numericUpDown_Settings_Interval.Value = new decimal(new int[] {
            600,
            0,
            0,
            0});
			this.numericUpDown_Settings_Interval.ValueChanged += new System.EventHandler(this.numericUpDown_Settings_Interval_ValueChanged);
			// 
			// label_Settings_Secret
			// 
			this.label_Settings_Secret.AutoSize = true;
			this.label_Settings_Secret.Location = new System.Drawing.Point(60, 131);
			this.label_Settings_Secret.Name = "label_Settings_Secret";
			this.label_Settings_Secret.Size = new System.Drawing.Size(53, 12);
			this.label_Settings_Secret.TabIndex = 13;
			this.label_Settings_Secret.Text = "Secret：";
			// 
			// label_Settings_Key
			// 
			this.label_Settings_Key.AutoSize = true;
			this.label_Settings_Key.Location = new System.Drawing.Point(78, 104);
			this.label_Settings_Key.Name = "label_Settings_Key";
			this.label_Settings_Key.Size = new System.Drawing.Size(35, 12);
			this.label_Settings_Key.TabIndex = 10;
			this.label_Settings_Key.Text = "Key：";
			// 
			// label_Settings_Interval
			// 
			this.label_Settings_Interval.AutoSize = true;
			this.label_Settings_Interval.Location = new System.Drawing.Point(12, 76);
			this.label_Settings_Interval.Name = "label_Settings_Interval";
			this.label_Settings_Interval.Size = new System.Drawing.Size(101, 12);
			this.label_Settings_Interval.TabIndex = 7;
			this.label_Settings_Interval.Text = "时间间隔（秒）：";
			// 
			// textBox_Settings_Last_IP
			// 
			this.textBox_Settings_Last_IP.Location = new System.Drawing.Point(119, 46);
			this.textBox_Settings_Last_IP.Name = "textBox_Settings_Last_IP";
			this.textBox_Settings_Last_IP.ReadOnly = true;
			this.textBox_Settings_Last_IP.Size = new System.Drawing.Size(226, 21);
			this.textBox_Settings_Last_IP.TabIndex = 5;
			// 
			// textBox_Settings_Secret
			// 
			this.textBox_Settings_Secret.Location = new System.Drawing.Point(119, 127);
			this.textBox_Settings_Secret.Name = "textBox_Settings_Secret";
			this.textBox_Settings_Secret.PasswordChar = '*';
			this.textBox_Settings_Secret.Size = new System.Drawing.Size(226, 21);
			this.textBox_Settings_Secret.TabIndex = 14;
			this.textBox_Settings_Secret.TextChanged += new System.EventHandler(this.textBox_Settings_Secret_TextChanged);
			// 
			// textBox_Settings_Key
			// 
			this.textBox_Settings_Key.Location = new System.Drawing.Point(119, 100);
			this.textBox_Settings_Key.Name = "textBox_Settings_Key";
			this.textBox_Settings_Key.PasswordChar = '*';
			this.textBox_Settings_Key.Size = new System.Drawing.Size(226, 21);
			this.textBox_Settings_Key.TabIndex = 11;
			this.textBox_Settings_Key.TextChanged += new System.EventHandler(this.textBox_Settings_Key_TextChanged);
			// 
			// label_Settings_Last_IP
			// 
			this.label_Settings_Last_IP.AutoSize = true;
			this.label_Settings_Last_IP.Location = new System.Drawing.Point(24, 50);
			this.label_Settings_Last_IP.Name = "label_Settings_Last_IP";
			this.label_Settings_Last_IP.Size = new System.Drawing.Size(89, 12);
			this.label_Settings_Last_IP.TabIndex = 4;
			this.label_Settings_Last_IP.Text = "上次获取的IP：";
			// 
			// label_Settings_Get_IP_URL
			// 
			this.label_Settings_Get_IP_URL.AutoSize = true;
			this.label_Settings_Get_IP_URL.Location = new System.Drawing.Point(6, 23);
			this.label_Settings_Get_IP_URL.Name = "label_Settings_Get_IP_URL";
			this.label_Settings_Get_IP_URL.Size = new System.Drawing.Size(107, 12);
			this.label_Settings_Get_IP_URL.TabIndex = 2;
			this.label_Settings_Get_IP_URL.Text = "检查公网IP的URL：";
			// 
			// listView_Logs
			// 
			this.listView_Logs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listView_Logs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader_Logs_Time,
            this.columnHeader_Logs_Log});
			this.listView_Logs.ContextMenuStrip = this.contextMenuStrip_Logs;
			this.listView_Logs.FullRowSelect = true;
			this.listView_Logs.GridLines = true;
			this.listView_Logs.HideSelection = false;
			this.listView_Logs.Location = new System.Drawing.Point(459, 12);
			this.listView_Logs.Name = "listView_Logs";
			this.listView_Logs.Size = new System.Drawing.Size(513, 335);
			this.listView_Logs.TabIndex = 21;
			this.listView_Logs.UseCompatibleStateImageBehavior = false;
			this.listView_Logs.View = System.Windows.Forms.View.Details;
			this.listView_Logs.SelectedIndexChanged += new System.EventHandler(this.listView_Logs_SelectedIndexChanged);
			this.listView_Logs.SizeChanged += new System.EventHandler(this.listView_Logs_SizeChanged);
			// 
			// columnHeader_Logs_Time
			// 
			this.columnHeader_Logs_Time.Text = "时间";
			this.columnHeader_Logs_Time.Width = 122;
			// 
			// columnHeader_Logs_Log
			// 
			this.columnHeader_Logs_Log.Text = "日志";
			this.columnHeader_Logs_Log.Width = 370;
			// 
			// contextMenuStrip_Logs
			// 
			this.contextMenuStrip_Logs.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_Logs_Copy,
            this.ToolStripMenuItem_Logs_Delete,
            this.toolStripMenuItem1,
            this.ToolStripMenuItem_Logs_SelectAll});
			this.contextMenuStrip_Logs.Name = "contextMenuStrip_Logs";
			this.contextMenuStrip_Logs.Size = new System.Drawing.Size(194, 76);
			// 
			// ToolStripMenuItem_Logs_Copy
			// 
			this.ToolStripMenuItem_Logs_Copy.Enabled = false;
			this.ToolStripMenuItem_Logs_Copy.Image = global::ddns.res_Main.Copy;
			this.ToolStripMenuItem_Logs_Copy.Name = "ToolStripMenuItem_Logs_Copy";
			this.ToolStripMenuItem_Logs_Copy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
			this.ToolStripMenuItem_Logs_Copy.Size = new System.Drawing.Size(193, 22);
			this.ToolStripMenuItem_Logs_Copy.Text = "复制文本";
			this.ToolStripMenuItem_Logs_Copy.Click += new System.EventHandler(this.ToolStripMenuItem_Logs_Copy_Click);
			// 
			// ToolStripMenuItem_Logs_Delete
			// 
			this.ToolStripMenuItem_Logs_Delete.Enabled = false;
			this.ToolStripMenuItem_Logs_Delete.Image = global::ddns.res_Main.Delete;
			this.ToolStripMenuItem_Logs_Delete.Name = "ToolStripMenuItem_Logs_Delete";
			this.ToolStripMenuItem_Logs_Delete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
			this.ToolStripMenuItem_Logs_Delete.Size = new System.Drawing.Size(193, 22);
			this.ToolStripMenuItem_Logs_Delete.Text = "删除选定记录";
			this.ToolStripMenuItem_Logs_Delete.Click += new System.EventHandler(this.ToolStripMenuItem_Logs_Delete_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(190, 6);
			// 
			// ToolStripMenuItem_Logs_SelectAll
			// 
			this.ToolStripMenuItem_Logs_SelectAll.Name = "ToolStripMenuItem_Logs_SelectAll";
			this.ToolStripMenuItem_Logs_SelectAll.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
			this.ToolStripMenuItem_Logs_SelectAll.Size = new System.Drawing.Size(193, 22);
			this.ToolStripMenuItem_Logs_SelectAll.Text = "全选";
			this.ToolStripMenuItem_Logs_SelectAll.Click += new System.EventHandler(this.ToolStripMenuItem_Logs_SelectAll_Click);
			// 
			// notifyIcon_Main
			// 
			this.notifyIcon_Main.ContextMenuStrip = this.contextMenuStrip_NotifyIcon;
			this.notifyIcon_Main.Text = "ddns - godaddy";
			this.notifyIcon_Main.Visible = true;
			this.notifyIcon_Main.DoubleClick += new System.EventHandler(this.notifyIcon_Main_DoubleClick);
			// 
			// contextMenuStrip_NotifyIcon
			// 
			this.contextMenuStrip_NotifyIcon.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_Open,
            this.ToolStripMenuItem_Exit});
			this.contextMenuStrip_NotifyIcon.Name = "contextMenuStrip_NotifyIcon";
			this.contextMenuStrip_NotifyIcon.Size = new System.Drawing.Size(101, 48);
			// 
			// ToolStripMenuItem_Open
			// 
			this.ToolStripMenuItem_Open.Name = "ToolStripMenuItem_Open";
			this.ToolStripMenuItem_Open.Size = new System.Drawing.Size(100, 22);
			this.ToolStripMenuItem_Open.Text = "打开";
			this.ToolStripMenuItem_Open.Click += new System.EventHandler(this.ToolStripMenuItem_Open_Click);
			// 
			// ToolStripMenuItem_Exit
			// 
			this.ToolStripMenuItem_Exit.Image = global::ddns.res_Main.Exit;
			this.ToolStripMenuItem_Exit.Name = "ToolStripMenuItem_Exit";
			this.ToolStripMenuItem_Exit.Size = new System.Drawing.Size(100, 22);
			this.ToolStripMenuItem_Exit.Text = "退出";
			this.ToolStripMenuItem_Exit.Click += new System.EventHandler(this.ToolStripMenuItem_Exit_Click);
			// 
			// timer_Save_Config
			// 
			this.timer_Save_Config.Enabled = true;
			this.timer_Save_Config.Interval = 200;
			this.timer_Save_Config.Tick += new System.EventHandler(this.timer_Save_Config_Tick);
			// 
			// label_Tip
			// 
			this.label_Tip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label_Tip.AutoSize = true;
			this.label_Tip.Location = new System.Drawing.Point(12, 350);
			this.label_Tip.Name = "label_Tip";
			this.label_Tip.Size = new System.Drawing.Size(317, 12);
			this.label_Tip.TabIndex = 22;
			this.label_Tip.Text = "必须先去                                申请 API Key";
			// 
			// linkLabel_godaddy
			// 
			this.linkLabel_godaddy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.linkLabel_godaddy.AutoSize = true;
			this.linkLabel_godaddy.Location = new System.Drawing.Point(65, 350);
			this.linkLabel_godaddy.Name = "linkLabel_godaddy";
			this.linkLabel_godaddy.Size = new System.Drawing.Size(185, 12);
			this.linkLabel_godaddy.TabIndex = 23;
			this.linkLabel_godaddy.TabStop = true;
			this.linkLabel_godaddy.Text = "https://developer.godaddy.com/";
			this.linkLabel_godaddy.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_LinkClicked);
			// 
			// linkLabel_Website
			// 
			this.linkLabel_Website.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.linkLabel_Website.AutoSize = true;
			this.linkLabel_Website.Location = new System.Drawing.Point(727, 350);
			this.linkLabel_Website.Name = "linkLabel_Website";
			this.linkLabel_Website.Size = new System.Drawing.Size(245, 12);
			this.linkLabel_Website.TabIndex = 24;
			this.linkLabel_Website.TabStop = true;
			this.linkLabel_Website.Text = "https://github.com/foxofice/ddns_godaddy";
			this.linkLabel_Website.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_LinkClicked);
			// 
			// timer_Update
			// 
			this.timer_Update.Enabled = true;
			this.timer_Update.Interval = 1000;
			this.timer_Update.Tick += new System.EventHandler(this.timer_Update_Tick);
			// 
			// checkBox_Settings_Specific_IP
			// 
			this.checkBox_Settings_Specific_IP.AutoSize = true;
			this.checkBox_Settings_Specific_IP.Location = new System.Drawing.Point(351, 49);
			this.checkBox_Settings_Specific_IP.Name = "checkBox_Settings_Specific_IP";
			this.checkBox_Settings_Specific_IP.Size = new System.Drawing.Size(84, 16);
			this.checkBox_Settings_Specific_IP.TabIndex = 6;
			this.checkBox_Settings_Specific_IP.Text = "指定IP地址";
			this.checkBox_Settings_Specific_IP.UseVisualStyleBackColor = true;
			this.checkBox_Settings_Specific_IP.CheckedChanged += new System.EventHandler(this.checkBox_Settings_Specific_IP_CheckedChanged);
			// 
			// checkBox_Settings_Update_Force
			// 
			this.checkBox_Settings_Update_Force.AutoSize = true;
			this.checkBox_Settings_Update_Force.Location = new System.Drawing.Point(200, 180);
			this.checkBox_Settings_Update_Force.Name = "checkBox_Settings_Update_Force";
			this.checkBox_Settings_Update_Force.Size = new System.Drawing.Size(72, 16);
			this.checkBox_Settings_Update_Force.TabIndex = 18;
			this.checkBox_Settings_Update_Force.Text = "强制更新";
			this.checkBox_Settings_Update_Force.UseVisualStyleBackColor = true;
			this.checkBox_Settings_Update_Force.CheckedChanged += new System.EventHandler(this.checkBox_Settings_Update_Force_CheckedChanged);
			// 
			// frm_MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(984, 371);
			this.Controls.Add(this.linkLabel_Website);
			this.Controls.Add(this.linkLabel_godaddy);
			this.Controls.Add(this.label_Tip);
			this.Controls.Add(this.listView_Logs);
			this.Controls.Add(this.groupBox_Settings);
			this.Controls.Add(this.groupBox_Records);
			this.Name = "frm_MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "ddns - godaddy v0.02";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_MainForm_FormClosing);
			this.Load += new System.EventHandler(this.frm_MainForm_Load);
			this.groupBox_Records.ResumeLayout(false);
			this.contextMenuStrip_Records.ResumeLayout(false);
			this.groupBox_Settings.ResumeLayout(false);
			this.groupBox_Settings.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Settings_Interval)).EndInit();
			this.contextMenuStrip_Logs.ResumeLayout(false);
			this.contextMenuStrip_NotifyIcon.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button button_Settings_Update;
		private System.Windows.Forms.GroupBox groupBox_Records;
		private System.Windows.Forms.ListView listView_Records;
		private System.Windows.Forms.ColumnHeader columnHeader_Records_Name;
		private System.Windows.Forms.ColumnHeader columnHeader_Records_Domain;
		private System.Windows.Forms.ColumnHeader columnHeader_Records_TTL;
		private System.Windows.Forms.GroupBox groupBox_Settings;
		private System.Windows.Forms.Label label_Settings_Get_IP_URL;
		private System.Windows.Forms.NumericUpDown numericUpDown_Settings_Interval;
		private System.Windows.Forms.Label label_Settings_Interval;
		private System.Windows.Forms.TextBox textBox_Settings_Last_IP;
		private System.Windows.Forms.Label label_Settings_Last_IP;
		private System.Windows.Forms.Label label_Settings_Key;
		private System.Windows.Forms.TextBox textBox_Settings_Key;
		private System.Windows.Forms.CheckBox checkBox_Settings_Save_Key_and_Secret;
		private System.Windows.Forms.Label label_Settings_Secret;
		private System.Windows.Forms.TextBox textBox_Settings_Secret;
		private System.Windows.Forms.ColumnHeader columnHeader_Records_Last_Result;
		private System.Windows.Forms.ColumnHeader columnHeader_Records_Last_IP;
		private System.Windows.Forms.ListView listView_Logs;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip_Records;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Records_Add;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Records_Delete;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Records_Edit;
		private System.Windows.Forms.ColumnHeader columnHeader_Logs_Time;
		private System.Windows.Forms.ColumnHeader columnHeader_Logs_Log;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip_Logs;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Logs_Copy;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Logs_Delete;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Logs_SelectAll;
		private System.Windows.Forms.ComboBox comboBox_Settings_Get_IP_URL;
		private System.Windows.Forms.NotifyIcon notifyIcon_Main;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip_NotifyIcon;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Open;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Exit;
		private System.Windows.Forms.CheckBox checkBox_Settings_Show_Key;
		private System.Windows.Forms.CheckBox checkBox_Settings_Show_Secret;
		private System.Windows.Forms.Timer timer_Save_Config;
		private System.Windows.Forms.Label label_Tip;
		private System.Windows.Forms.LinkLabel linkLabel_godaddy;
		private System.Windows.Forms.LinkLabel linkLabel_Website;
		private System.Windows.Forms.Timer timer_Update;
		private System.Windows.Forms.CheckBox checkBox_Settings_AutoUpdate;
		private System.Windows.Forms.CheckBox checkBox_Settings_Specific_IP;
		private System.Windows.Forms.CheckBox checkBox_Settings_Update_Force;
	}
}

