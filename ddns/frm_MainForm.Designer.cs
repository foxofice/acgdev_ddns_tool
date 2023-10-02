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
			this.checkBox_Settings_Update_Force = new System.Windows.Forms.CheckBox();
			this.checkBox_Settings_AutoUpdate = new System.Windows.Forms.CheckBox();
			this.checkBox_Settings_Show_Secret = new System.Windows.Forms.CheckBox();
			this.checkBox_Settings_Show_Key = new System.Windows.Forms.CheckBox();
			this.comboBox_Settings_Get_IP_URL = new System.Windows.Forms.ComboBox();
			this.checkBox_Settings_Save_Key_and_Secret = new System.Windows.Forms.CheckBox();
			this.numericUpDown_Settings_AutoUpdate_Interval = new System.Windows.Forms.NumericUpDown();
			this.label_Settings_Secret = new System.Windows.Forms.Label();
			this.label_Settings_Key = new System.Windows.Forms.Label();
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
			this.linkLabel_Github = new System.Windows.Forms.LinkLabel();
			this.timer_Update = new System.Windows.Forms.Timer(this.components);
			this.linkLabel_WebSite = new System.Windows.Forms.LinkLabel();
			this.groupBox_Server = new System.Windows.Forms.GroupBox();
			this.checkBox_Server_Ping = new System.Windows.Forms.CheckBox();
			this.checkBox_Server_Show_Pwd = new System.Windows.Forms.CheckBox();
			this.label_Server_User = new System.Windows.Forms.Label();
			this.label_Server_Ping = new System.Windows.Forms.Label();
			this.label_Server_Pwd = new System.Windows.Forms.Label();
			this.label_Server_Addr = new System.Windows.Forms.Label();
			this.textBox_Server_User = new System.Windows.Forms.TextBox();
			this.textBox_Server_Ping = new System.Windows.Forms.TextBox();
			this.textBox_Server_Pwd = new System.Windows.Forms.TextBox();
			this.textBox_Server_Addr = new System.Windows.Forms.TextBox();
			this.timer_Ping = new System.Windows.Forms.Timer(this.components);
			this.groupBox_Type = new System.Windows.Forms.GroupBox();
			this.radioButton_Server = new System.Windows.Forms.RadioButton();
			this.radioButton_Local = new System.Windows.Forms.RadioButton();
			this.groupBox_IP = new System.Windows.Forms.GroupBox();
			this.radioButton_Specific_IP = new System.Windows.Forms.RadioButton();
			this.radioButton_Server_Accept_IP = new System.Windows.Forms.RadioButton();
			this.radioButton_Get_IP_From_URL = new System.Windows.Forms.RadioButton();
			this.groupBox_Security = new System.Windows.Forms.GroupBox();
			this.groupBox_Records.SuspendLayout();
			this.contextMenuStrip_Records.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Settings_AutoUpdate_Interval)).BeginInit();
			this.contextMenuStrip_Logs.SuspendLayout();
			this.contextMenuStrip_NotifyIcon.SuspendLayout();
			this.groupBox_Server.SuspendLayout();
			this.groupBox_Type.SuspendLayout();
			this.groupBox_IP.SuspendLayout();
			this.groupBox_Security.SuspendLayout();
			this.SuspendLayout();
			// 
			// button_Settings_Update
			// 
			this.button_Settings_Update.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.button_Settings_Update.Location = new System.Drawing.Point(378, 572);
			this.button_Settings_Update.Name = "button_Settings_Update";
			this.button_Settings_Update.Size = new System.Drawing.Size(75, 23);
			this.button_Settings_Update.TabIndex = 63;
			this.button_Settings_Update.Text = "立即更新";
			this.button_Settings_Update.UseVisualStyleBackColor = true;
			this.button_Settings_Update.Click += new System.EventHandler(this.button_Settings_Update_Click);
			// 
			// groupBox_Records
			// 
			this.groupBox_Records.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.groupBox_Records.Controls.Add(this.listView_Records);
			this.groupBox_Records.Location = new System.Drawing.Point(12, 397);
			this.groupBox_Records.Name = "groupBox_Records";
			this.groupBox_Records.Size = new System.Drawing.Size(441, 169);
			this.groupBox_Records.TabIndex = 50;
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
			this.listView_Records.Size = new System.Drawing.Size(429, 143);
			this.listView_Records.TabIndex = 51;
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
			// checkBox_Settings_Update_Force
			// 
			this.checkBox_Settings_Update_Force.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.checkBox_Settings_Update_Force.AutoSize = true;
			this.checkBox_Settings_Update_Force.Location = new System.Drawing.Point(300, 576);
			this.checkBox_Settings_Update_Force.Name = "checkBox_Settings_Update_Force";
			this.checkBox_Settings_Update_Force.Size = new System.Drawing.Size(72, 16);
			this.checkBox_Settings_Update_Force.TabIndex = 62;
			this.checkBox_Settings_Update_Force.Text = "强制更新";
			this.checkBox_Settings_Update_Force.UseVisualStyleBackColor = true;
			this.checkBox_Settings_Update_Force.CheckedChanged += new System.EventHandler(this.checkBox_Settings_Update_Force_CheckedChanged);
			// 
			// checkBox_Settings_AutoUpdate
			// 
			this.checkBox_Settings_AutoUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.checkBox_Settings_AutoUpdate.AutoSize = true;
			this.checkBox_Settings_AutoUpdate.Checked = true;
			this.checkBox_Settings_AutoUpdate.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox_Settings_AutoUpdate.Location = new System.Drawing.Point(12, 576);
			this.checkBox_Settings_AutoUpdate.Name = "checkBox_Settings_AutoUpdate";
			this.checkBox_Settings_AutoUpdate.Size = new System.Drawing.Size(168, 16);
			this.checkBox_Settings_AutoUpdate.TabIndex = 60;
			this.checkBox_Settings_AutoUpdate.Text = "自动更新时间间隔（秒）：";
			this.checkBox_Settings_AutoUpdate.UseVisualStyleBackColor = true;
			this.checkBox_Settings_AutoUpdate.CheckedChanged += new System.EventHandler(this.checkBox_Settings_AutoUpdate_CheckedChanged);
			// 
			// checkBox_Settings_Show_Secret
			// 
			this.checkBox_Settings_Show_Secret.AutoSize = true;
			this.checkBox_Settings_Show_Secret.Location = new System.Drawing.Point(387, 49);
			this.checkBox_Settings_Show_Secret.Name = "checkBox_Settings_Show_Secret";
			this.checkBox_Settings_Show_Secret.Size = new System.Drawing.Size(48, 16);
			this.checkBox_Settings_Show_Secret.TabIndex = 46;
			this.checkBox_Settings_Show_Secret.Text = "显示";
			this.checkBox_Settings_Show_Secret.UseVisualStyleBackColor = true;
			this.checkBox_Settings_Show_Secret.CheckedChanged += new System.EventHandler(this.checkBox_Settings_Show_Secret_CheckedChanged);
			// 
			// checkBox_Settings_Show_Key
			// 
			this.checkBox_Settings_Show_Key.AutoSize = true;
			this.checkBox_Settings_Show_Key.Location = new System.Drawing.Point(387, 22);
			this.checkBox_Settings_Show_Key.Name = "checkBox_Settings_Show_Key";
			this.checkBox_Settings_Show_Key.Size = new System.Drawing.Size(48, 16);
			this.checkBox_Settings_Show_Key.TabIndex = 43;
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
			this.comboBox_Settings_Get_IP_URL.Location = new System.Drawing.Point(119, 42);
			this.comboBox_Settings_Get_IP_URL.Name = "comboBox_Settings_Get_IP_URL";
			this.comboBox_Settings_Get_IP_URL.Size = new System.Drawing.Size(316, 20);
			this.comboBox_Settings_Get_IP_URL.TabIndex = 35;
			this.comboBox_Settings_Get_IP_URL.TextChanged += new System.EventHandler(this.comboBox_Settings_Get_IP_URL_TextChanged);
			// 
			// checkBox_Settings_Save_Key_and_Secret
			// 
			this.checkBox_Settings_Save_Key_and_Secret.AutoSize = true;
			this.checkBox_Settings_Save_Key_and_Secret.Location = new System.Drawing.Point(6, 74);
			this.checkBox_Settings_Save_Key_and_Secret.Name = "checkBox_Settings_Save_Key_and_Secret";
			this.checkBox_Settings_Save_Key_and_Secret.Size = new System.Drawing.Size(192, 16);
			this.checkBox_Settings_Save_Key_and_Secret.TabIndex = 47;
			this.checkBox_Settings_Save_Key_and_Secret.Text = "保存 Key/Secret 到配置文件中";
			this.checkBox_Settings_Save_Key_and_Secret.UseVisualStyleBackColor = true;
			this.checkBox_Settings_Save_Key_and_Secret.CheckedChanged += new System.EventHandler(this.checkBox_Settings_Save_Key_and_Secret_CheckedChanged);
			// 
			// numericUpDown_Settings_AutoUpdate_Interval
			// 
			this.numericUpDown_Settings_AutoUpdate_Interval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.numericUpDown_Settings_AutoUpdate_Interval.Location = new System.Drawing.Point(186, 575);
			this.numericUpDown_Settings_AutoUpdate_Interval.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
			this.numericUpDown_Settings_AutoUpdate_Interval.Name = "numericUpDown_Settings_AutoUpdate_Interval";
			this.numericUpDown_Settings_AutoUpdate_Interval.Size = new System.Drawing.Size(66, 21);
			this.numericUpDown_Settings_AutoUpdate_Interval.TabIndex = 61;
			this.numericUpDown_Settings_AutoUpdate_Interval.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.numericUpDown_Settings_AutoUpdate_Interval.Value = new decimal(new int[] {
            600,
            0,
            0,
            0});
			this.numericUpDown_Settings_AutoUpdate_Interval.ValueChanged += new System.EventHandler(this.numericUpDown_Settings_AutoUpdate_Interval_ValueChanged);
			// 
			// label_Settings_Secret
			// 
			this.label_Settings_Secret.AutoSize = true;
			this.label_Settings_Secret.Location = new System.Drawing.Point(6, 50);
			this.label_Settings_Secret.Name = "label_Settings_Secret";
			this.label_Settings_Secret.Size = new System.Drawing.Size(53, 12);
			this.label_Settings_Secret.TabIndex = 44;
			this.label_Settings_Secret.Text = "Secret：";
			// 
			// label_Settings_Key
			// 
			this.label_Settings_Key.AutoSize = true;
			this.label_Settings_Key.Location = new System.Drawing.Point(6, 23);
			this.label_Settings_Key.Name = "label_Settings_Key";
			this.label_Settings_Key.Size = new System.Drawing.Size(35, 12);
			this.label_Settings_Key.TabIndex = 41;
			this.label_Settings_Key.Text = "Key：";
			// 
			// textBox_Settings_Last_IP
			// 
			this.textBox_Settings_Last_IP.Location = new System.Drawing.Point(119, 68);
			this.textBox_Settings_Last_IP.Name = "textBox_Settings_Last_IP";
			this.textBox_Settings_Last_IP.ReadOnly = true;
			this.textBox_Settings_Last_IP.Size = new System.Drawing.Size(316, 21);
			this.textBox_Settings_Last_IP.TabIndex = 37;
			this.textBox_Settings_Last_IP.TextChanged += new System.EventHandler(this.textBox_Settings_Last_IP_TextChanged);
			// 
			// textBox_Settings_Secret
			// 
			this.textBox_Settings_Secret.Location = new System.Drawing.Point(65, 47);
			this.textBox_Settings_Secret.Name = "textBox_Settings_Secret";
			this.textBox_Settings_Secret.PasswordChar = '*';
			this.textBox_Settings_Secret.Size = new System.Drawing.Size(316, 21);
			this.textBox_Settings_Secret.TabIndex = 45;
			this.textBox_Settings_Secret.TextChanged += new System.EventHandler(this.textBox_Settings_Secret_TextChanged);
			// 
			// textBox_Settings_Key
			// 
			this.textBox_Settings_Key.Location = new System.Drawing.Point(65, 20);
			this.textBox_Settings_Key.Name = "textBox_Settings_Key";
			this.textBox_Settings_Key.PasswordChar = '*';
			this.textBox_Settings_Key.Size = new System.Drawing.Size(316, 21);
			this.textBox_Settings_Key.TabIndex = 42;
			this.textBox_Settings_Key.TextChanged += new System.EventHandler(this.textBox_Settings_Key_TextChanged);
			// 
			// label_Settings_Last_IP
			// 
			this.label_Settings_Last_IP.AutoSize = true;
			this.label_Settings_Last_IP.Location = new System.Drawing.Point(6, 71);
			this.label_Settings_Last_IP.Name = "label_Settings_Last_IP";
			this.label_Settings_Last_IP.Size = new System.Drawing.Size(89, 12);
			this.label_Settings_Last_IP.TabIndex = 36;
			this.label_Settings_Last_IP.Text = "上次获取的IP：";
			// 
			// label_Settings_Get_IP_URL
			// 
			this.label_Settings_Get_IP_URL.AutoSize = true;
			this.label_Settings_Get_IP_URL.Location = new System.Drawing.Point(6, 45);
			this.label_Settings_Get_IP_URL.Name = "label_Settings_Get_IP_URL";
			this.label_Settings_Get_IP_URL.Size = new System.Drawing.Size(107, 12);
			this.label_Settings_Get_IP_URL.TabIndex = 34;
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
			this.listView_Logs.Size = new System.Drawing.Size(513, 583);
			this.listView_Logs.TabIndex = 66;
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
			this.timer_Save_Config.Interval = 500;
			this.timer_Save_Config.Tick += new System.EventHandler(this.timer_Save_Config_Tick);
			// 
			// label_Tip
			// 
			this.label_Tip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label_Tip.AutoSize = true;
			this.label_Tip.Location = new System.Drawing.Point(12, 604);
			this.label_Tip.Name = "label_Tip";
			this.label_Tip.Size = new System.Drawing.Size(317, 12);
			this.label_Tip.TabIndex = 64;
			this.label_Tip.Text = "必须先去                                申请 API Key";
			// 
			// linkLabel_godaddy
			// 
			this.linkLabel_godaddy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.linkLabel_godaddy.AutoSize = true;
			this.linkLabel_godaddy.Location = new System.Drawing.Point(65, 604);
			this.linkLabel_godaddy.Name = "linkLabel_godaddy";
			this.linkLabel_godaddy.Size = new System.Drawing.Size(185, 12);
			this.linkLabel_godaddy.TabIndex = 65;
			this.linkLabel_godaddy.TabStop = true;
			this.linkLabel_godaddy.Text = "https://developer.godaddy.com/";
			this.linkLabel_godaddy.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_godaddy_LinkClicked);
			// 
			// linkLabel_Github
			// 
			this.linkLabel_Github.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.linkLabel_Github.AutoSize = true;
			this.linkLabel_Github.Location = new System.Drawing.Point(931, 604);
			this.linkLabel_Github.Name = "linkLabel_Github";
			this.linkLabel_Github.Size = new System.Drawing.Size(41, 12);
			this.linkLabel_Github.TabIndex = 68;
			this.linkLabel_Github.TabStop = true;
			this.linkLabel_Github.Text = "github";
			this.linkLabel_Github.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_Github_LinkClicked);
			// 
			// timer_Update
			// 
			this.timer_Update.Enabled = true;
			this.timer_Update.Interval = 1000;
			this.timer_Update.Tick += new System.EventHandler(this.timer_Update_Tick);
			// 
			// linkLabel_WebSite
			// 
			this.linkLabel_WebSite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.linkLabel_WebSite.AutoSize = true;
			this.linkLabel_WebSite.Location = new System.Drawing.Point(896, 604);
			this.linkLabel_WebSite.Name = "linkLabel_WebSite";
			this.linkLabel_WebSite.Size = new System.Drawing.Size(29, 12);
			this.linkLabel_WebSite.TabIndex = 67;
			this.linkLabel_WebSite.TabStop = true;
			this.linkLabel_WebSite.Text = "官网";
			this.linkLabel_WebSite.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_WebSite_LinkClicked);
			// 
			// groupBox_Server
			// 
			this.groupBox_Server.BackColor = System.Drawing.SystemColors.ControlLight;
			this.groupBox_Server.Controls.Add(this.checkBox_Server_Ping);
			this.groupBox_Server.Controls.Add(this.checkBox_Server_Show_Pwd);
			this.groupBox_Server.Controls.Add(this.label_Server_User);
			this.groupBox_Server.Controls.Add(this.label_Server_Ping);
			this.groupBox_Server.Controls.Add(this.label_Server_Pwd);
			this.groupBox_Server.Controls.Add(this.label_Server_Addr);
			this.groupBox_Server.Controls.Add(this.textBox_Server_User);
			this.groupBox_Server.Controls.Add(this.textBox_Server_Ping);
			this.groupBox_Server.Controls.Add(this.textBox_Server_Pwd);
			this.groupBox_Server.Controls.Add(this.textBox_Server_Addr);
			this.groupBox_Server.Location = new System.Drawing.Point(12, 60);
			this.groupBox_Server.Name = "groupBox_Server";
			this.groupBox_Server.Size = new System.Drawing.Size(441, 128);
			this.groupBox_Server.TabIndex = 10;
			this.groupBox_Server.TabStop = false;
			this.groupBox_Server.Text = "Server 设置";
			// 
			// checkBox_Server_Ping
			// 
			this.checkBox_Server_Ping.AutoSize = true;
			this.checkBox_Server_Ping.Checked = true;
			this.checkBox_Server_Ping.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox_Server_Ping.Location = new System.Drawing.Point(256, 103);
			this.checkBox_Server_Ping.Name = "checkBox_Server_Ping";
			this.checkBox_Server_Ping.Size = new System.Drawing.Size(120, 16);
			this.checkBox_Server_Ping.TabIndex = 20;
			this.checkBox_Server_Ping.Text = "自动 ping 服务器";
			this.checkBox_Server_Ping.UseVisualStyleBackColor = true;
			// 
			// checkBox_Server_Show_Pwd
			// 
			this.checkBox_Server_Show_Pwd.AutoSize = true;
			this.checkBox_Server_Show_Pwd.Location = new System.Drawing.Point(387, 76);
			this.checkBox_Server_Show_Pwd.Name = "checkBox_Server_Show_Pwd";
			this.checkBox_Server_Show_Pwd.Size = new System.Drawing.Size(48, 16);
			this.checkBox_Server_Show_Pwd.TabIndex = 17;
			this.checkBox_Server_Show_Pwd.Text = "显示";
			this.checkBox_Server_Show_Pwd.UseVisualStyleBackColor = true;
			this.checkBox_Server_Show_Pwd.CheckedChanged += new System.EventHandler(this.checkBox_Server_Show_Pwd_CheckedChanged);
			// 
			// label_Server_User
			// 
			this.label_Server_User.AutoSize = true;
			this.label_Server_User.Location = new System.Drawing.Point(6, 50);
			this.label_Server_User.Name = "label_Server_User";
			this.label_Server_User.Size = new System.Drawing.Size(149, 12);
			this.label_Server_User.TabIndex = 13;
			this.label_Server_User.Text = "登录到 Server 的用户名：";
			// 
			// label_Server_Ping
			// 
			this.label_Server_Ping.AutoSize = true;
			this.label_Server_Ping.Location = new System.Drawing.Point(6, 104);
			this.label_Server_Ping.Name = "label_Server_Ping";
			this.label_Server_Ping.Size = new System.Drawing.Size(89, 12);
			this.label_Server_Ping.TabIndex = 18;
			this.label_Server_Ping.Text = "Ping 值 (ms)：";
			// 
			// label_Server_Pwd
			// 
			this.label_Server_Pwd.AutoSize = true;
			this.label_Server_Pwd.Location = new System.Drawing.Point(6, 77);
			this.label_Server_Pwd.Name = "label_Server_Pwd";
			this.label_Server_Pwd.Size = new System.Drawing.Size(137, 12);
			this.label_Server_Pwd.TabIndex = 15;
			this.label_Server_Pwd.Text = "登录到 Server 的密码：";
			// 
			// label_Server_Addr
			// 
			this.label_Server_Addr.AutoSize = true;
			this.label_Server_Addr.Location = new System.Drawing.Point(6, 23);
			this.label_Server_Addr.Name = "label_Server_Addr";
			this.label_Server_Addr.Size = new System.Drawing.Size(113, 12);
			this.label_Server_Addr.TabIndex = 11;
			this.label_Server_Addr.Text = "Server 地址/端口：";
			// 
			// textBox_Server_User
			// 
			this.textBox_Server_User.Location = new System.Drawing.Point(161, 47);
			this.textBox_Server_User.Name = "textBox_Server_User";
			this.textBox_Server_User.ReadOnly = true;
			this.textBox_Server_User.Size = new System.Drawing.Size(220, 21);
			this.textBox_Server_User.TabIndex = 14;
			this.textBox_Server_User.TextChanged += new System.EventHandler(this.textBox_Server_User_TextChanged);
			// 
			// textBox_Server_Ping
			// 
			this.textBox_Server_Ping.Location = new System.Drawing.Point(161, 101);
			this.textBox_Server_Ping.Name = "textBox_Server_Ping";
			this.textBox_Server_Ping.ReadOnly = true;
			this.textBox_Server_Ping.Size = new System.Drawing.Size(89, 21);
			this.textBox_Server_Ping.TabIndex = 19;
			// 
			// textBox_Server_Pwd
			// 
			this.textBox_Server_Pwd.Location = new System.Drawing.Point(161, 74);
			this.textBox_Server_Pwd.Name = "textBox_Server_Pwd";
			this.textBox_Server_Pwd.PasswordChar = '*';
			this.textBox_Server_Pwd.ReadOnly = true;
			this.textBox_Server_Pwd.Size = new System.Drawing.Size(220, 21);
			this.textBox_Server_Pwd.TabIndex = 16;
			this.textBox_Server_Pwd.TextChanged += new System.EventHandler(this.textBox_Server_Pwd_TextChanged);
			// 
			// textBox_Server_Addr
			// 
			this.textBox_Server_Addr.Location = new System.Drawing.Point(161, 20);
			this.textBox_Server_Addr.Name = "textBox_Server_Addr";
			this.textBox_Server_Addr.ReadOnly = true;
			this.textBox_Server_Addr.Size = new System.Drawing.Size(220, 21);
			this.textBox_Server_Addr.TabIndex = 12;
			this.textBox_Server_Addr.Text = "127.0.0.1:3333";
			this.textBox_Server_Addr.TextChanged += new System.EventHandler(this.textBox_Server_Addr_TextChanged);
			// 
			// timer_Ping
			// 
			this.timer_Ping.Enabled = true;
			this.timer_Ping.Interval = 1000;
			this.timer_Ping.Tick += new System.EventHandler(this.timer_Ping_Tick);
			// 
			// groupBox_Type
			// 
			this.groupBox_Type.Controls.Add(this.radioButton_Server);
			this.groupBox_Type.Controls.Add(this.radioButton_Local);
			this.groupBox_Type.Location = new System.Drawing.Point(12, 12);
			this.groupBox_Type.Name = "groupBox_Type";
			this.groupBox_Type.Size = new System.Drawing.Size(441, 42);
			this.groupBox_Type.TabIndex = 0;
			this.groupBox_Type.TabStop = false;
			this.groupBox_Type.Text = "更新方式";
			// 
			// radioButton_Server
			// 
			this.radioButton_Server.AutoSize = true;
			this.radioButton_Server.Location = new System.Drawing.Point(131, 20);
			this.radioButton_Server.Name = "radioButton_Server";
			this.radioButton_Server.Size = new System.Drawing.Size(173, 16);
			this.radioButton_Server.TabIndex = 2;
			this.radioButton_Server.Text = "远程更新（连接到 Server）";
			this.radioButton_Server.UseVisualStyleBackColor = true;
			this.radioButton_Server.CheckedChanged += new System.EventHandler(this.radioButton_Server_CheckedChanged);
			// 
			// radioButton_Local
			// 
			this.radioButton_Local.AutoSize = true;
			this.radioButton_Local.Checked = true;
			this.radioButton_Local.Location = new System.Drawing.Point(6, 20);
			this.radioButton_Local.Name = "radioButton_Local";
			this.radioButton_Local.Size = new System.Drawing.Size(119, 16);
			this.radioButton_Local.TabIndex = 1;
			this.radioButton_Local.TabStop = true;
			this.radioButton_Local.Text = "本地更新（直连）";
			this.radioButton_Local.UseVisualStyleBackColor = true;
			this.radioButton_Local.CheckedChanged += new System.EventHandler(this.radioButton_Local_CheckedChanged);
			// 
			// groupBox_IP
			// 
			this.groupBox_IP.Controls.Add(this.radioButton_Specific_IP);
			this.groupBox_IP.Controls.Add(this.radioButton_Server_Accept_IP);
			this.groupBox_IP.Controls.Add(this.radioButton_Get_IP_From_URL);
			this.groupBox_IP.Controls.Add(this.label_Settings_Get_IP_URL);
			this.groupBox_IP.Controls.Add(this.comboBox_Settings_Get_IP_URL);
			this.groupBox_IP.Controls.Add(this.textBox_Settings_Last_IP);
			this.groupBox_IP.Controls.Add(this.label_Settings_Last_IP);
			this.groupBox_IP.Location = new System.Drawing.Point(12, 194);
			this.groupBox_IP.Name = "groupBox_IP";
			this.groupBox_IP.Size = new System.Drawing.Size(441, 95);
			this.groupBox_IP.TabIndex = 30;
			this.groupBox_IP.TabStop = false;
			this.groupBox_IP.Text = "IP 设置";
			// 
			// radioButton_Specific_IP
			// 
			this.radioButton_Specific_IP.AutoSize = true;
			this.radioButton_Specific_IP.Location = new System.Drawing.Point(161, 20);
			this.radioButton_Specific_IP.Name = "radioButton_Specific_IP";
			this.radioButton_Specific_IP.Size = new System.Drawing.Size(89, 16);
			this.radioButton_Specific_IP.TabIndex = 32;
			this.radioButton_Specific_IP.Text = "手动设置 IP";
			this.radioButton_Specific_IP.UseVisualStyleBackColor = true;
			this.radioButton_Specific_IP.CheckedChanged += new System.EventHandler(this.radioButton_Specific_IP_CheckedChanged);
			// 
			// radioButton_Server_Accept_IP
			// 
			this.radioButton_Server_Accept_IP.AutoSize = true;
			this.radioButton_Server_Accept_IP.Enabled = false;
			this.radioButton_Server_Accept_IP.Location = new System.Drawing.Point(256, 20);
			this.radioButton_Server_Accept_IP.Name = "radioButton_Server_Accept_IP";
			this.radioButton_Server_Accept_IP.Size = new System.Drawing.Size(179, 16);
			this.radioButton_Server_Accept_IP.TabIndex = 33;
			this.radioButton_Server_Accept_IP.Text = "Server 接受连接的客户端 IP";
			this.radioButton_Server_Accept_IP.UseVisualStyleBackColor = true;
			this.radioButton_Server_Accept_IP.CheckedChanged += new System.EventHandler(this.radioButton_Server_Accept_IP_CheckedChanged);
			// 
			// radioButton_Get_IP_From_URL
			// 
			this.radioButton_Get_IP_From_URL.AutoSize = true;
			this.radioButton_Get_IP_From_URL.Checked = true;
			this.radioButton_Get_IP_From_URL.Location = new System.Drawing.Point(6, 20);
			this.radioButton_Get_IP_From_URL.Name = "radioButton_Get_IP_From_URL";
			this.radioButton_Get_IP_From_URL.Size = new System.Drawing.Size(149, 16);
			this.radioButton_Get_IP_From_URL.TabIndex = 31;
			this.radioButton_Get_IP_From_URL.TabStop = true;
			this.radioButton_Get_IP_From_URL.Text = "通过互联网获取公网 IP";
			this.radioButton_Get_IP_From_URL.UseVisualStyleBackColor = true;
			this.radioButton_Get_IP_From_URL.CheckedChanged += new System.EventHandler(this.radioButton_Get_IP_From_URL_CheckedChanged);
			// 
			// groupBox_Security
			// 
			this.groupBox_Security.BackColor = System.Drawing.SystemColors.ControlLight;
			this.groupBox_Security.Controls.Add(this.textBox_Settings_Key);
			this.groupBox_Security.Controls.Add(this.textBox_Settings_Secret);
			this.groupBox_Security.Controls.Add(this.checkBox_Settings_Show_Secret);
			this.groupBox_Security.Controls.Add(this.checkBox_Settings_Save_Key_and_Secret);
			this.groupBox_Security.Controls.Add(this.label_Settings_Key);
			this.groupBox_Security.Controls.Add(this.label_Settings_Secret);
			this.groupBox_Security.Controls.Add(this.checkBox_Settings_Show_Key);
			this.groupBox_Security.Location = new System.Drawing.Point(12, 295);
			this.groupBox_Security.Name = "groupBox_Security";
			this.groupBox_Security.Size = new System.Drawing.Size(441, 96);
			this.groupBox_Security.TabIndex = 40;
			this.groupBox_Security.TabStop = false;
			this.groupBox_Security.Text = "安全设置";
			// 
			// frm_MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(984, 625);
			this.Controls.Add(this.checkBox_Settings_Update_Force);
			this.Controls.Add(this.groupBox_Security);
			this.Controls.Add(this.checkBox_Settings_AutoUpdate);
			this.Controls.Add(this.button_Settings_Update);
			this.Controls.Add(this.groupBox_IP);
			this.Controls.Add(this.numericUpDown_Settings_AutoUpdate_Interval);
			this.Controls.Add(this.groupBox_Type);
			this.Controls.Add(this.groupBox_Server);
			this.Controls.Add(this.linkLabel_WebSite);
			this.Controls.Add(this.linkLabel_Github);
			this.Controls.Add(this.linkLabel_godaddy);
			this.Controls.Add(this.label_Tip);
			this.Controls.Add(this.listView_Logs);
			this.Controls.Add(this.groupBox_Records);
			this.Name = "frm_MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "ddns - godaddy v0.05";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_MainForm_FormClosing);
			this.Load += new System.EventHandler(this.frm_MainForm_Load);
			this.groupBox_Records.ResumeLayout(false);
			this.contextMenuStrip_Records.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Settings_AutoUpdate_Interval)).EndInit();
			this.contextMenuStrip_Logs.ResumeLayout(false);
			this.contextMenuStrip_NotifyIcon.ResumeLayout(false);
			this.groupBox_Server.ResumeLayout(false);
			this.groupBox_Server.PerformLayout();
			this.groupBox_Type.ResumeLayout(false);
			this.groupBox_Type.PerformLayout();
			this.groupBox_IP.ResumeLayout(false);
			this.groupBox_IP.PerformLayout();
			this.groupBox_Security.ResumeLayout(false);
			this.groupBox_Security.PerformLayout();
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
		private System.Windows.Forms.Label label_Settings_Get_IP_URL;
		private System.Windows.Forms.NumericUpDown numericUpDown_Settings_AutoUpdate_Interval;
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
		private System.Windows.Forms.LinkLabel linkLabel_Github;
		private System.Windows.Forms.Timer timer_Update;
		private System.Windows.Forms.CheckBox checkBox_Settings_AutoUpdate;
		private System.Windows.Forms.CheckBox checkBox_Settings_Update_Force;
		private System.Windows.Forms.LinkLabel linkLabel_WebSite;
		private System.Windows.Forms.GroupBox groupBox_Server;
		private System.Windows.Forms.TextBox textBox_Server_Addr;
		private System.Windows.Forms.Label label_Server_Addr;
		private System.Windows.Forms.CheckBox checkBox_Server_Show_Pwd;
		private System.Windows.Forms.Label label_Server_Pwd;
		private System.Windows.Forms.TextBox textBox_Server_Pwd;
		private System.Windows.Forms.Label label_Server_User;
		private System.Windows.Forms.TextBox textBox_Server_User;
		private System.Windows.Forms.Timer timer_Ping;
		private System.Windows.Forms.Label label_Server_Ping;
		private System.Windows.Forms.TextBox textBox_Server_Ping;
		private System.Windows.Forms.GroupBox groupBox_Type;
		private System.Windows.Forms.RadioButton radioButton_Server;
		private System.Windows.Forms.RadioButton radioButton_Local;
		private System.Windows.Forms.GroupBox groupBox_IP;
		private System.Windows.Forms.RadioButton radioButton_Get_IP_From_URL;
		private System.Windows.Forms.RadioButton radioButton_Specific_IP;
		private System.Windows.Forms.RadioButton radioButton_Server_Accept_IP;
		private System.Windows.Forms.GroupBox groupBox_Security;
		private System.Windows.Forms.CheckBox checkBox_Server_Ping;
	}
}

