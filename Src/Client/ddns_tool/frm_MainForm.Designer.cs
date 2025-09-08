namespace ddns_tool
{
	partial class frm_MainForm
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
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
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
			groupBox_Domains = new GroupBox();
			listView_Domains = new ListView();
			columnHeader_Domains_Domain = new ColumnHeader();
			columnHeader_Domains_Type = new ColumnHeader();
			columnHeader_Domains_Profile = new ColumnHeader();
			columnHeader_Domains_IPv4 = new ColumnHeader();
			columnHeader_Domains_IPv6 = new ColumnHeader();
			columnHeader_Domains_Status = new ColumnHeader();
			contextMenuStrip_Domains = new ContextMenuStrip(components);
			ToolStripMenuItem_Domains_Add = new ToolStripMenuItem();
			ToolStripMenuItem_Domains_Modify = new ToolStripMenuItem();
			ToolStripMenuItem_Domains_Delete = new ToolStripMenuItem();
			ToolStripMenuItem_Domains_IPv4_Enable = new ToolStripMenuItem();
			ToolStripMenuItem_Domains_IPv6_Enable = new ToolStripMenuItem();
			ToolStripMenuItem_Domains_IPv4_Disable = new ToolStripMenuItem();
			ToolStripMenuItem_Domains_IPv6_Disable = new ToolStripMenuItem();
			toolStripMenuItem1 = new ToolStripSeparator();
			ToolStripMenuItem_Domains_CopyText = new ToolStripMenuItem();
			toolStrip_Domains = new ToolStrip();
			toolStripButton_Domains_Add = new ToolStripButton();
			toolStripButton_Domains_Modify = new ToolStripButton();
			toolStripButton_Domains_Delete = new ToolStripButton();
			toolStripButton_Domains_IPv4_Enable = new ToolStripButton();
			toolStripButton_Domains_IPv6_Enable = new ToolStripButton();
			toolStripButton_Domains_IPv4_Disable = new ToolStripButton();
			toolStripButton_Domains_IPv6_Disable = new ToolStripButton();
			toolStripSeparator1 = new ToolStripSeparator();
			toolStripButton_Domains_CopyText = new ToolStripButton();
			splitContainer_Main = new SplitContainer();
			groupBox_Logs = new GroupBox();
			checkBox_Logs__Save_To_File = new CheckBox();
			numericUpDown_Logs_MaxLines = new NumericUpDown();
			label_Logs_MaxLines = new Label();
			listView_Logs = new ListView();
			columnHeader_Logs_Time = new ColumnHeader();
			columnHeader_Logs_Log = new ColumnHeader();
			contextMenuStrip_Logs = new ContextMenuStrip(components);
			ToolStripMenuItem_Logs_Copy = new ToolStripMenuItem();
			ToolStripMenuItem_Logs_Delete = new ToolStripMenuItem();
			toolStripMenuItem2 = new ToolStripSeparator();
			ToolStripMenuItem_Logs_SelectAll = new ToolStripMenuItem();
			notifyIcon_Main = new NotifyIcon(components);
			contextMenuStrip_NotifyIcon = new ContextMenuStrip(components);
			ToolStripMenuItem_Open = new ToolStripMenuItem();
			ToolStripMenuItem_Languages = new ToolStripMenuItem();
			toolStripMenuItem_Languages_CurrentCulture = new ToolStripMenuItem();
			toolStripSeparator2 = new ToolStripSeparator();
			toolStripMenuItem3 = new ToolStripSeparator();
			ToolStripMenuItem_Exit = new ToolStripMenuItem();
			tabControl_Main = new TabControl();
			tabPage_Update_Type = new TabPage();
			groupBox_Settings_RemoteServer = new GroupBox();
			checkBox_Settings_RemoteServer__Ping = new CheckBox();
			checkBox_Settings_RemoteServer__Pwd = new CheckBox();
			textBox_Settings_RemoteServer__Pwd = new TextBox();
			textBox_Settings_RemoteServer__Ping = new TextBox();
			textBox_Settings_RemoteServer__User = new TextBox();
			textBox_Settings_RemoteServer__Addr = new TextBox();
			label_Settings_RemoteServer__Ping = new Label();
			label_Settings_RemoteServer__Pwd = new Label();
			label_Settings_RemoteServer__User = new Label();
			label_Settings_RemoteServer__Addr = new Label();
			radioButton_Settings_Method__Remote = new RadioButton();
			radioButton_Settings_Method__Local = new RadioButton();
			tabPage_Set_IP = new TabPage();
			groupBox_Set_IPv6 = new GroupBox();
			textBox_Settings_IPv6 = new TextBox();
			radioButton_Settings_IPv6__Accept_IP = new RadioButton();
			radioButton_Settings_IPv6__From_URL = new RadioButton();
			radioButton_Settings_IPv6__Manual = new RadioButton();
			comboBox_Settings_IPv6__From_URL = new ComboBox();
			groupBox_Set_IPv4 = new GroupBox();
			textBox_Settings_IPv4 = new TextBox();
			radioButton_Settings_IPv4__Accept_IP = new RadioButton();
			radioButton_Settings_IPv4__Manual = new RadioButton();
			comboBox_Settings_IPv4__From_URL = new ComboBox();
			radioButton_Settings_IPv4__From_URL = new RadioButton();
			tabPage_Security = new TabPage();
			groupBox_Security__Property = new GroupBox();
			checkBox_Security__Save_To_Config = new CheckBox();
			tabControl_Security__Property = new TabControl();
			tabPage_Security__Godaddy = new TabPage();
			checkBox_Security_Godaddy__Secret = new CheckBox();
			checkBox_Security_Godaddy__Key = new CheckBox();
			textBox_Security_Godaddy__Secret = new TextBox();
			textBox_Security_Godaddy__Key = new TextBox();
			label_Security_Godaddy__Secret = new Label();
			label_Security_Godaddy__Key = new Label();
			linkLabel_Security_Godaddy__API = new LinkLabel();
			tabPage_Security__dynv6 = new TabPage();
			checkBox_Security_dynv6__token = new CheckBox();
			textBox_Security_dynv6__token = new TextBox();
			label_Security_dynv6__token = new Label();
			linkLabel_Security_dynv6__API = new LinkLabel();
			tabPage_Security__dynu = new TabPage();
			checkBox_Security_dynu__API_Key = new CheckBox();
			textBox_Security_dynu__API_Key = new TextBox();
			label_Security_dynu__API_Key = new Label();
			linkLabel_Security_dynu__API = new LinkLabel();
			textBox_Security__Property__Name = new TextBox();
			label_Security__Property__Name = new Label();
			button_Security_Del = new Button();
			button_Security_Add = new Button();
			listView_Security = new ListView();
			columnHeader_Security = new ColumnHeader();
			contextMenuStrip_Security = new ContextMenuStrip(components);
			ToolStripMenuItem__Security_Add = new ToolStripMenuItem();
			ToolStripMenuItem__Security_Del = new ToolStripMenuItem();
			tabPage_Update_Action = new TabPage();
			groupBox_Action_IP_Change_PlaySound = new GroupBox();
			button_Action_IP_Change_StopSound = new Button();
			button_Action_IP_Change_PlaySound = new Button();
			textBox_Action_IP_Change_PlaySound = new TextBox();
			checkBox_Action_IP_Change_PlaySound = new CheckBox();
			checkBox_Action_IP_Change_Popup = new CheckBox();
			numericUpDown_Action_Timeout = new NumericUpDown();
			label_Action_Timeout = new Label();
			groupBox_Action_Set_DNS_Server = new GroupBox();
			textBox_Action_Custom_DNS_List = new TextBox();
			checkBox_Action_Use_Custom_DNS = new CheckBox();
			checkBox_Action_DNS_Lookup_First = new CheckBox();
			numericUpDown_Action_AutoAction_Interval = new NumericUpDown();
			checkBox_Action_AutoAction_Interval = new CheckBox();
			checkBox_Action_UpdateIP = new CheckBox();
			tabPage_Fix_hosts = new TabPage();
			textBox_Fix_hosts__Content = new TextBox();
			button_Fix_hosts__Path_Browser = new Button();
			textBox_Fix_hosts__Path = new TextBox();
			label_Fix_hosts__Content = new Label();
			label_Fix_hosts__Path = new Label();
			groupBox_Settings_Preview = new GroupBox();
			label_Settings_Preview__Timeout_Val = new Label();
			label_Settings_Preview__DNS_Server_Val = new Label();
			label_Settings_Preview__DNS_Lookup_First_Val = new Label();
			label_Settings_Preview__Action_AutoUpdate_Val = new Label();
			label_Settings_Preview__Action_UpdateIP_Val = new Label();
			label_Settings_Preview__Security_Val = new Label();
			label_Settings_Preview__Set_IPv6_Val = new Label();
			label_Settings_Preview__Set_IPv4_Val = new Label();
			label_Settings_Preview__Ping_Val = new Label();
			label_Settings_Preview__Update_Type_Val = new Label();
			label_Settings_Preview__Timeout = new Label();
			label_Settings_Preview__DNS_Server = new Label();
			label_Settings_Preview__DNS_Lookup_First = new Label();
			label_Settings_Preview__Action_AutoUpdate = new Label();
			label_Settings_Preview__Action_UpdateIP = new Label();
			label_Settings_Preview__Security = new Label();
			label_Settings_Preview__Set_IPv6 = new Label();
			label_Settings_Preview__Set_IPv4 = new Label();
			label_Settings_Preview__Ping = new Label();
			label_Settings_Preview__Update_Type = new Label();
			linkLabel_WebSite = new LinkLabel();
			linkLabel_Github = new LinkLabel();
			button_Update = new Button();
			timer_Save_Config = new System.Windows.Forms.Timer(components);
			timer_Update = new System.Windows.Forms.Timer(components);
			timer_Ping = new System.Windows.Forms.Timer(components);
			timer_Save_Log = new System.Windows.Forms.Timer(components);
			groupBox_Domains.SuspendLayout();
			contextMenuStrip_Domains.SuspendLayout();
			toolStrip_Domains.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)splitContainer_Main).BeginInit();
			splitContainer_Main.Panel1.SuspendLayout();
			splitContainer_Main.Panel2.SuspendLayout();
			splitContainer_Main.SuspendLayout();
			groupBox_Logs.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)numericUpDown_Logs_MaxLines).BeginInit();
			contextMenuStrip_Logs.SuspendLayout();
			contextMenuStrip_NotifyIcon.SuspendLayout();
			tabControl_Main.SuspendLayout();
			tabPage_Update_Type.SuspendLayout();
			groupBox_Settings_RemoteServer.SuspendLayout();
			tabPage_Set_IP.SuspendLayout();
			groupBox_Set_IPv6.SuspendLayout();
			groupBox_Set_IPv4.SuspendLayout();
			tabPage_Security.SuspendLayout();
			groupBox_Security__Property.SuspendLayout();
			tabControl_Security__Property.SuspendLayout();
			tabPage_Security__Godaddy.SuspendLayout();
			tabPage_Security__dynv6.SuspendLayout();
			tabPage_Security__dynu.SuspendLayout();
			contextMenuStrip_Security.SuspendLayout();
			tabPage_Update_Action.SuspendLayout();
			groupBox_Action_IP_Change_PlaySound.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)numericUpDown_Action_Timeout).BeginInit();
			groupBox_Action_Set_DNS_Server.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)numericUpDown_Action_AutoAction_Interval).BeginInit();
			tabPage_Fix_hosts.SuspendLayout();
			groupBox_Settings_Preview.SuspendLayout();
			SuspendLayout();
			// 
			// groupBox_Domains
			// 
			groupBox_Domains.BackColor = Color.White;
			groupBox_Domains.Controls.Add(listView_Domains);
			groupBox_Domains.Controls.Add(toolStrip_Domains);
			groupBox_Domains.Dock = DockStyle.Fill;
			groupBox_Domains.Location = new Point(0, 0);
			groupBox_Domains.Margin = new Padding(3, 2, 3, 2);
			groupBox_Domains.Name = "groupBox_Domains";
			groupBox_Domains.Padding = new Padding(3, 2, 3, 2);
			groupBox_Domains.Size = new Size(934, 155);
			groupBox_Domains.TabIndex = 2;
			groupBox_Domains.TabStop = false;
			groupBox_Domains.Text = "【域名列表】";
			// 
			// listView_Domains
			// 
			listView_Domains.Columns.AddRange(new ColumnHeader[] { columnHeader_Domains_Domain, columnHeader_Domains_Type, columnHeader_Domains_Profile, columnHeader_Domains_IPv4, columnHeader_Domains_IPv6, columnHeader_Domains_Status });
			listView_Domains.ContextMenuStrip = contextMenuStrip_Domains;
			listView_Domains.Dock = DockStyle.Fill;
			listView_Domains.FullRowSelect = true;
			listView_Domains.GridLines = true;
			listView_Domains.Location = new Point(3, 41);
			listView_Domains.Margin = new Padding(3, 2, 3, 2);
			listView_Domains.Name = "listView_Domains";
			listView_Domains.Size = new Size(928, 112);
			listView_Domains.TabIndex = 4;
			listView_Domains.UseCompatibleStateImageBehavior = false;
			listView_Domains.View = View.Details;
			listView_Domains.SelectedIndexChanged += listView_Domains_SelectedIndexChanged;
			listView_Domains.DoubleClick += listView_Domains_DoubleClick;
			listView_Domains.Resize += listView_Domains_Resize;
			// 
			// columnHeader_Domains_Domain
			// 
			columnHeader_Domains_Domain.Text = "域名";
			columnHeader_Domains_Domain.Width = 184;
			// 
			// columnHeader_Domains_Type
			// 
			columnHeader_Domains_Type.Text = "类型";
			columnHeader_Domains_Type.Width = 198;
			// 
			// columnHeader_Domains_Profile
			// 
			columnHeader_Domains_Profile.Text = "安全配置";
			// 
			// columnHeader_Domains_IPv4
			// 
			columnHeader_Domains_IPv4.Text = "最新IPv4";
			columnHeader_Domains_IPv4.Width = 102;
			// 
			// columnHeader_Domains_IPv6
			// 
			columnHeader_Domains_IPv6.Text = "最新IPv6";
			columnHeader_Domains_IPv6.Width = 273;
			// 
			// columnHeader_Domains_Status
			// 
			columnHeader_Domains_Status.Text = "状态";
			columnHeader_Domains_Status.Width = 90;
			// 
			// contextMenuStrip_Domains
			// 
			contextMenuStrip_Domains.Font = new Font("新宋体", 9F);
			contextMenuStrip_Domains.Items.AddRange(new ToolStripItem[] { ToolStripMenuItem_Domains_Add, ToolStripMenuItem_Domains_Modify, ToolStripMenuItem_Domains_Delete, ToolStripMenuItem_Domains_IPv4_Enable, ToolStripMenuItem_Domains_IPv6_Enable, ToolStripMenuItem_Domains_IPv4_Disable, ToolStripMenuItem_Domains_IPv6_Disable, toolStripMenuItem1, ToolStripMenuItem_Domains_CopyText });
			contextMenuStrip_Domains.Name = "contextMenuStrip_Domains";
			contextMenuStrip_Domains.Size = new Size(160, 186);
			// 
			// ToolStripMenuItem_Domains_Add
			// 
			ToolStripMenuItem_Domains_Add.Image = res_Main.Add;
			ToolStripMenuItem_Domains_Add.Name = "ToolStripMenuItem_Domains_Add";
			ToolStripMenuItem_Domains_Add.Size = new Size(159, 22);
			ToolStripMenuItem_Domains_Add.Text = "添加";
			ToolStripMenuItem_Domains_Add.Click += ToolStripMenuItem_Domains_Add_Click;
			// 
			// ToolStripMenuItem_Domains_Modify
			// 
			ToolStripMenuItem_Domains_Modify.Enabled = false;
			ToolStripMenuItem_Domains_Modify.Image = res_Main.Edit;
			ToolStripMenuItem_Domains_Modify.Name = "ToolStripMenuItem_Domains_Modify";
			ToolStripMenuItem_Domains_Modify.Size = new Size(159, 22);
			ToolStripMenuItem_Domains_Modify.Text = "修改";
			ToolStripMenuItem_Domains_Modify.Click += ToolStripMenuItem_Domains_Modify_Click;
			// 
			// ToolStripMenuItem_Domains_Delete
			// 
			ToolStripMenuItem_Domains_Delete.Enabled = false;
			ToolStripMenuItem_Domains_Delete.Image = res_Main.Delete;
			ToolStripMenuItem_Domains_Delete.Name = "ToolStripMenuItem_Domains_Delete";
			ToolStripMenuItem_Domains_Delete.Size = new Size(159, 22);
			ToolStripMenuItem_Domains_Delete.Text = "删除";
			ToolStripMenuItem_Domains_Delete.Click += ToolStripMenuItem_Domains_Delete_Click;
			// 
			// ToolStripMenuItem_Domains_IPv4_Enable
			// 
			ToolStripMenuItem_Domains_IPv4_Enable.Enabled = false;
			ToolStripMenuItem_Domains_IPv4_Enable.Image = res_Main.On;
			ToolStripMenuItem_Domains_IPv4_Enable.Name = "ToolStripMenuItem_Domains_IPv4_Enable";
			ToolStripMenuItem_Domains_IPv4_Enable.Size = new Size(159, 22);
			ToolStripMenuItem_Domains_IPv4_Enable.Text = "允许更新 IPv4";
			ToolStripMenuItem_Domains_IPv4_Enable.Click += ToolStripMenuItem_Domains_IPv4_Enable_Click;
			// 
			// ToolStripMenuItem_Domains_IPv6_Enable
			// 
			ToolStripMenuItem_Domains_IPv6_Enable.Enabled = false;
			ToolStripMenuItem_Domains_IPv6_Enable.Image = res_Main.On;
			ToolStripMenuItem_Domains_IPv6_Enable.Name = "ToolStripMenuItem_Domains_IPv6_Enable";
			ToolStripMenuItem_Domains_IPv6_Enable.Size = new Size(159, 22);
			ToolStripMenuItem_Domains_IPv6_Enable.Text = "允许更新 IPv6";
			ToolStripMenuItem_Domains_IPv6_Enable.Click += ToolStripMenuItem_Domains_IPv6_Enable_Click;
			// 
			// ToolStripMenuItem_Domains_IPv4_Disable
			// 
			ToolStripMenuItem_Domains_IPv4_Disable.Enabled = false;
			ToolStripMenuItem_Domains_IPv4_Disable.Image = res_Main.Off;
			ToolStripMenuItem_Domains_IPv4_Disable.Name = "ToolStripMenuItem_Domains_IPv4_Disable";
			ToolStripMenuItem_Domains_IPv4_Disable.Size = new Size(159, 22);
			ToolStripMenuItem_Domains_IPv4_Disable.Text = "禁止更新 IPv4";
			ToolStripMenuItem_Domains_IPv4_Disable.Click += ToolStripMenuItem_Domains_IPv4_Disable_Click;
			// 
			// ToolStripMenuItem_Domains_IPv6_Disable
			// 
			ToolStripMenuItem_Domains_IPv6_Disable.Enabled = false;
			ToolStripMenuItem_Domains_IPv6_Disable.Image = res_Main.Off;
			ToolStripMenuItem_Domains_IPv6_Disable.Name = "ToolStripMenuItem_Domains_IPv6_Disable";
			ToolStripMenuItem_Domains_IPv6_Disable.Size = new Size(159, 22);
			ToolStripMenuItem_Domains_IPv6_Disable.Text = "禁止更新 IPv6";
			ToolStripMenuItem_Domains_IPv6_Disable.Click += ToolStripMenuItem_Domains_IPv6_Disable_Click;
			// 
			// toolStripMenuItem1
			// 
			toolStripMenuItem1.Name = "toolStripMenuItem1";
			toolStripMenuItem1.Size = new Size(156, 6);
			// 
			// ToolStripMenuItem_Domains_CopyText
			// 
			ToolStripMenuItem_Domains_CopyText.Enabled = false;
			ToolStripMenuItem_Domains_CopyText.Image = res_Main.Copy;
			ToolStripMenuItem_Domains_CopyText.Name = "ToolStripMenuItem_Domains_CopyText";
			ToolStripMenuItem_Domains_CopyText.ShortcutKeys = Keys.Control | Keys.C;
			ToolStripMenuItem_Domains_CopyText.Size = new Size(159, 22);
			ToolStripMenuItem_Domains_CopyText.Text = "复制文本";
			ToolStripMenuItem_Domains_CopyText.Click += ToolStripMenuItem_Domains_CopyText_Click;
			// 
			// toolStrip_Domains
			// 
			toolStrip_Domains.Font = new Font("新宋体", 9F);
			toolStrip_Domains.Items.AddRange(new ToolStripItem[] { toolStripButton_Domains_Add, toolStripButton_Domains_Modify, toolStripButton_Domains_Delete, toolStripButton_Domains_IPv4_Enable, toolStripButton_Domains_IPv6_Enable, toolStripButton_Domains_IPv4_Disable, toolStripButton_Domains_IPv6_Disable, toolStripSeparator1, toolStripButton_Domains_CopyText });
			toolStrip_Domains.Location = new Point(3, 16);
			toolStrip_Domains.Name = "toolStrip_Domains";
			toolStrip_Domains.Size = new Size(928, 25);
			toolStrip_Domains.TabIndex = 3;
			toolStrip_Domains.Text = "域名列表";
			// 
			// toolStripButton_Domains_Add
			// 
			toolStripButton_Domains_Add.Image = res_Main.Add;
			toolStripButton_Domains_Add.ImageTransparentColor = Color.Magenta;
			toolStripButton_Domains_Add.Name = "toolStripButton_Domains_Add";
			toolStripButton_Domains_Add.Size = new Size(49, 22);
			toolStripButton_Domains_Add.Text = "添加";
			toolStripButton_Domains_Add.Click += toolStripButton_Domains_Add_Click;
			// 
			// toolStripButton_Domains_Modify
			// 
			toolStripButton_Domains_Modify.Enabled = false;
			toolStripButton_Domains_Modify.Image = res_Main.Edit;
			toolStripButton_Domains_Modify.ImageTransparentColor = Color.Magenta;
			toolStripButton_Domains_Modify.Name = "toolStripButton_Domains_Modify";
			toolStripButton_Domains_Modify.Size = new Size(49, 22);
			toolStripButton_Domains_Modify.Text = "修改";
			toolStripButton_Domains_Modify.Click += toolStripButton_Domains_Modify_Click;
			// 
			// toolStripButton_Domains_Delete
			// 
			toolStripButton_Domains_Delete.Enabled = false;
			toolStripButton_Domains_Delete.Image = res_Main.Delete;
			toolStripButton_Domains_Delete.ImageTransparentColor = Color.Magenta;
			toolStripButton_Domains_Delete.Name = "toolStripButton_Domains_Delete";
			toolStripButton_Domains_Delete.Size = new Size(49, 22);
			toolStripButton_Domains_Delete.Text = "删除";
			toolStripButton_Domains_Delete.Click += toolStripButton_Domains_Delete_Click;
			// 
			// toolStripButton_Domains_IPv4_Enable
			// 
			toolStripButton_Domains_IPv4_Enable.Enabled = false;
			toolStripButton_Domains_IPv4_Enable.Image = res_Main.On;
			toolStripButton_Domains_IPv4_Enable.ImageTransparentColor = Color.Magenta;
			toolStripButton_Domains_IPv4_Enable.Name = "toolStripButton_Domains_IPv4_Enable";
			toolStripButton_Domains_IPv4_Enable.Size = new Size(103, 22);
			toolStripButton_Domains_IPv4_Enable.Text = "允许更新 IPv4";
			toolStripButton_Domains_IPv4_Enable.Click += toolStripButton_Domains_IPv4_Enable_Click;
			// 
			// toolStripButton_Domains_IPv6_Enable
			// 
			toolStripButton_Domains_IPv6_Enable.Enabled = false;
			toolStripButton_Domains_IPv6_Enable.Image = res_Main.On;
			toolStripButton_Domains_IPv6_Enable.ImageTransparentColor = Color.Magenta;
			toolStripButton_Domains_IPv6_Enable.Name = "toolStripButton_Domains_IPv6_Enable";
			toolStripButton_Domains_IPv6_Enable.Size = new Size(103, 22);
			toolStripButton_Domains_IPv6_Enable.Text = "允许更新 IPv6";
			toolStripButton_Domains_IPv6_Enable.Click += toolStripButton_Domains_IPv6_Enable_Click;
			// 
			// toolStripButton_Domains_IPv4_Disable
			// 
			toolStripButton_Domains_IPv4_Disable.Enabled = false;
			toolStripButton_Domains_IPv4_Disable.Image = res_Main.Off;
			toolStripButton_Domains_IPv4_Disable.ImageTransparentColor = Color.Magenta;
			toolStripButton_Domains_IPv4_Disable.Name = "toolStripButton_Domains_IPv4_Disable";
			toolStripButton_Domains_IPv4_Disable.Size = new Size(103, 22);
			toolStripButton_Domains_IPv4_Disable.Text = "禁止更新 IPv4";
			toolStripButton_Domains_IPv4_Disable.Click += toolStripButton_Domains_IPv4_Disable_Click;
			// 
			// toolStripButton_Domains_IPv6_Disable
			// 
			toolStripButton_Domains_IPv6_Disable.Enabled = false;
			toolStripButton_Domains_IPv6_Disable.Image = res_Main.Off;
			toolStripButton_Domains_IPv6_Disable.ImageTransparentColor = Color.Magenta;
			toolStripButton_Domains_IPv6_Disable.Name = "toolStripButton_Domains_IPv6_Disable";
			toolStripButton_Domains_IPv6_Disable.Size = new Size(103, 22);
			toolStripButton_Domains_IPv6_Disable.Text = "禁止更新 IPv6";
			toolStripButton_Domains_IPv6_Disable.Click += toolStripButton_Domains_IPv6_Disable_Click;
			// 
			// toolStripSeparator1
			// 
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new Size(6, 25);
			// 
			// toolStripButton_Domains_CopyText
			// 
			toolStripButton_Domains_CopyText.Enabled = false;
			toolStripButton_Domains_CopyText.Image = res_Main.Copy;
			toolStripButton_Domains_CopyText.ImageTransparentColor = Color.Magenta;
			toolStripButton_Domains_CopyText.Name = "toolStripButton_Domains_CopyText";
			toolStripButton_Domains_CopyText.Size = new Size(73, 22);
			toolStripButton_Domains_CopyText.Text = "复制文本";
			toolStripButton_Domains_CopyText.Click += toolStripButton_Domains_CopyText_Click;
			// 
			// splitContainer_Main
			// 
			splitContainer_Main.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			splitContainer_Main.FixedPanel = FixedPanel.Panel1;
			splitContainer_Main.Location = new Point(0, 0);
			splitContainer_Main.Margin = new Padding(3, 2, 3, 2);
			splitContainer_Main.Name = "splitContainer_Main";
			splitContainer_Main.Orientation = Orientation.Horizontal;
			// 
			// splitContainer_Main.Panel1
			// 
			splitContainer_Main.Panel1.Controls.Add(groupBox_Domains);
			// 
			// splitContainer_Main.Panel2
			// 
			splitContainer_Main.Panel2.Controls.Add(groupBox_Logs);
			splitContainer_Main.Size = new Size(934, 470);
			splitContainer_Main.SplitterDistance = 155;
			splitContainer_Main.SplitterWidth = 3;
			splitContainer_Main.TabIndex = 1;
			// 
			// groupBox_Logs
			// 
			groupBox_Logs.BackColor = Color.Gainsboro;
			groupBox_Logs.Controls.Add(checkBox_Logs__Save_To_File);
			groupBox_Logs.Controls.Add(numericUpDown_Logs_MaxLines);
			groupBox_Logs.Controls.Add(label_Logs_MaxLines);
			groupBox_Logs.Controls.Add(listView_Logs);
			groupBox_Logs.Dock = DockStyle.Fill;
			groupBox_Logs.Location = new Point(0, 0);
			groupBox_Logs.Margin = new Padding(3, 2, 3, 2);
			groupBox_Logs.Name = "groupBox_Logs";
			groupBox_Logs.Padding = new Padding(3, 2, 3, 2);
			groupBox_Logs.Size = new Size(934, 312);
			groupBox_Logs.TabIndex = 10;
			groupBox_Logs.TabStop = false;
			groupBox_Logs.Text = "【日志记录】";
			// 
			// checkBox_Logs__Save_To_File
			// 
			checkBox_Logs__Save_To_File.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			checkBox_Logs__Save_To_File.AutoSize = true;
			checkBox_Logs__Save_To_File.Checked = true;
			checkBox_Logs__Save_To_File.CheckState = CheckState.Checked;
			checkBox_Logs__Save_To_File.Location = new Point(820, 292);
			checkBox_Logs__Save_To_File.Margin = new Padding(3, 2, 3, 2);
			checkBox_Logs__Save_To_File.Name = "checkBox_Logs__Save_To_File";
			checkBox_Logs__Save_To_File.Size = new Size(108, 16);
			checkBox_Logs__Save_To_File.TabIndex = 14;
			checkBox_Logs__Save_To_File.Text = "保存到日志文件";
			checkBox_Logs__Save_To_File.UseVisualStyleBackColor = true;
			checkBox_Logs__Save_To_File.CheckedChanged += checkBox_Logs__Save_To_File_CheckedChanged;
			// 
			// numericUpDown_Logs_MaxLines
			// 
			numericUpDown_Logs_MaxLines.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			numericUpDown_Logs_MaxLines.Location = new Point(125, 287);
			numericUpDown_Logs_MaxLines.Margin = new Padding(3, 2, 3, 2);
			numericUpDown_Logs_MaxLines.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
			numericUpDown_Logs_MaxLines.Name = "numericUpDown_Logs_MaxLines";
			numericUpDown_Logs_MaxLines.Size = new Size(60, 21);
			numericUpDown_Logs_MaxLines.TabIndex = 13;
			numericUpDown_Logs_MaxLines.TextAlign = HorizontalAlignment.Center;
			numericUpDown_Logs_MaxLines.Value = new decimal(new int[] { 10000, 0, 0, 0 });
			numericUpDown_Logs_MaxLines.ValueChanged += numericUpDown_Logs_MaxLines_ValueChanged;
			// 
			// label_Logs_MaxLines
			// 
			label_Logs_MaxLines.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			label_Logs_MaxLines.AutoSize = true;
			label_Logs_MaxLines.Location = new Point(6, 289);
			label_Logs_MaxLines.Name = "label_Logs_MaxLines";
			label_Logs_MaxLines.Size = new Size(89, 12);
			label_Logs_MaxLines.TabIndex = 12;
			label_Logs_MaxLines.Text = "最大显示行数：";
			// 
			// listView_Logs
			// 
			listView_Logs.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			listView_Logs.Columns.AddRange(new ColumnHeader[] { columnHeader_Logs_Time, columnHeader_Logs_Log });
			listView_Logs.ContextMenuStrip = contextMenuStrip_Logs;
			listView_Logs.FullRowSelect = true;
			listView_Logs.GridLines = true;
			listView_Logs.Location = new Point(3, 16);
			listView_Logs.Margin = new Padding(3, 2, 3, 2);
			listView_Logs.Name = "listView_Logs";
			listView_Logs.Size = new Size(928, 267);
			listView_Logs.TabIndex = 11;
			listView_Logs.UseCompatibleStateImageBehavior = false;
			listView_Logs.View = View.Details;
			listView_Logs.SelectedIndexChanged += listView_Logs_SelectedIndexChanged;
			listView_Logs.Resize += listView_Logs_Resize;
			// 
			// columnHeader_Logs_Time
			// 
			columnHeader_Logs_Time.Text = "时间";
			columnHeader_Logs_Time.Width = 122;
			// 
			// columnHeader_Logs_Log
			// 
			columnHeader_Logs_Log.Text = "日志";
			columnHeader_Logs_Log.Width = 785;
			// 
			// contextMenuStrip_Logs
			// 
			contextMenuStrip_Logs.Font = new Font("新宋体", 9F);
			contextMenuStrip_Logs.Items.AddRange(new ToolStripItem[] { ToolStripMenuItem_Logs_Copy, ToolStripMenuItem_Logs_Delete, toolStripMenuItem2, ToolStripMenuItem_Logs_SelectAll });
			contextMenuStrip_Logs.Name = "contextMenuStrip_Logs";
			contextMenuStrip_Logs.Size = new Size(160, 76);
			// 
			// ToolStripMenuItem_Logs_Copy
			// 
			ToolStripMenuItem_Logs_Copy.Enabled = false;
			ToolStripMenuItem_Logs_Copy.Image = res_Main.Copy;
			ToolStripMenuItem_Logs_Copy.Name = "ToolStripMenuItem_Logs_Copy";
			ToolStripMenuItem_Logs_Copy.ShortcutKeys = Keys.Control | Keys.C;
			ToolStripMenuItem_Logs_Copy.Size = new Size(159, 22);
			ToolStripMenuItem_Logs_Copy.Text = "复制文本";
			ToolStripMenuItem_Logs_Copy.Click += ToolStripMenuItem_Logs_Copy_Click;
			// 
			// ToolStripMenuItem_Logs_Delete
			// 
			ToolStripMenuItem_Logs_Delete.Enabled = false;
			ToolStripMenuItem_Logs_Delete.Image = res_Main.Delete;
			ToolStripMenuItem_Logs_Delete.Name = "ToolStripMenuItem_Logs_Delete";
			ToolStripMenuItem_Logs_Delete.Size = new Size(159, 22);
			ToolStripMenuItem_Logs_Delete.Text = "删除选定记录";
			ToolStripMenuItem_Logs_Delete.Click += ToolStripMenuItem_Logs_Delete_Click;
			// 
			// toolStripMenuItem2
			// 
			toolStripMenuItem2.Name = "toolStripMenuItem2";
			toolStripMenuItem2.Size = new Size(156, 6);
			// 
			// ToolStripMenuItem_Logs_SelectAll
			// 
			ToolStripMenuItem_Logs_SelectAll.Image = res_Main.SelectAll;
			ToolStripMenuItem_Logs_SelectAll.Name = "ToolStripMenuItem_Logs_SelectAll";
			ToolStripMenuItem_Logs_SelectAll.ShortcutKeys = Keys.Control | Keys.A;
			ToolStripMenuItem_Logs_SelectAll.Size = new Size(159, 22);
			ToolStripMenuItem_Logs_SelectAll.Text = "全选";
			ToolStripMenuItem_Logs_SelectAll.Click += ToolStripMenuItem_Logs_SelectAll_Click;
			// 
			// notifyIcon_Main
			// 
			notifyIcon_Main.BalloonTipIcon = ToolTipIcon.Info;
			notifyIcon_Main.ContextMenuStrip = contextMenuStrip_NotifyIcon;
			notifyIcon_Main.Visible = true;
			notifyIcon_Main.MouseDoubleClick += notifyIcon_Main_MouseDoubleClick;
			// 
			// contextMenuStrip_NotifyIcon
			// 
			contextMenuStrip_NotifyIcon.Font = new Font("新宋体", 9F);
			contextMenuStrip_NotifyIcon.Items.AddRange(new ToolStripItem[] { ToolStripMenuItem_Open, ToolStripMenuItem_Languages, toolStripMenuItem3, ToolStripMenuItem_Exit });
			contextMenuStrip_NotifyIcon.Name = "contextMenuStrip_NotifyIcon";
			contextMenuStrip_NotifyIcon.Size = new Size(161, 76);
			// 
			// ToolStripMenuItem_Open
			// 
			ToolStripMenuItem_Open.Font = new Font("新宋体", 9F, FontStyle.Bold);
			ToolStripMenuItem_Open.Name = "ToolStripMenuItem_Open";
			ToolStripMenuItem_Open.Size = new Size(160, 22);
			ToolStripMenuItem_Open.Text = "打开";
			ToolStripMenuItem_Open.Click += ToolStripMenuItem_Open_Click;
			// 
			// ToolStripMenuItem_Languages
			// 
			ToolStripMenuItem_Languages.DropDownItems.AddRange(new ToolStripItem[] { toolStripMenuItem_Languages_CurrentCulture, toolStripSeparator2 });
			ToolStripMenuItem_Languages.Image = res_Main.Languages;
			ToolStripMenuItem_Languages.Name = "ToolStripMenuItem_Languages";
			ToolStripMenuItem_Languages.Size = new Size(160, 22);
			ToolStripMenuItem_Languages.Text = "语言(Languages)";
			// 
			// toolStripMenuItem_Languages_CurrentCulture
			// 
			toolStripMenuItem_Languages_CurrentCulture.Enabled = false;
			toolStripMenuItem_Languages_CurrentCulture.Name = "toolStripMenuItem_Languages_CurrentCulture";
			toolStripMenuItem_Languages_CurrentCulture.Size = new Size(184, 22);
			toolStripMenuItem_Languages_CurrentCulture.Text = "当前区域设置(xx-xx)";
			// 
			// toolStripSeparator2
			// 
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new Size(181, 6);
			// 
			// toolStripMenuItem3
			// 
			toolStripMenuItem3.Name = "toolStripMenuItem3";
			toolStripMenuItem3.Size = new Size(157, 6);
			// 
			// ToolStripMenuItem_Exit
			// 
			ToolStripMenuItem_Exit.Image = res_Main.Exit;
			ToolStripMenuItem_Exit.Name = "ToolStripMenuItem_Exit";
			ToolStripMenuItem_Exit.Size = new Size(160, 22);
			ToolStripMenuItem_Exit.Text = "退出程序";
			ToolStripMenuItem_Exit.Click += ToolStripMenuItem_Exit_Click;
			// 
			// tabControl_Main
			// 
			tabControl_Main.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			tabControl_Main.Controls.Add(tabPage_Update_Type);
			tabControl_Main.Controls.Add(tabPage_Set_IP);
			tabControl_Main.Controls.Add(tabPage_Security);
			tabControl_Main.Controls.Add(tabPage_Update_Action);
			tabControl_Main.Controls.Add(tabPage_Fix_hosts);
			tabControl_Main.Location = new Point(12, 474);
			tabControl_Main.Margin = new Padding(3, 2, 3, 2);
			tabControl_Main.Name = "tabControl_Main";
			tabControl_Main.SelectedIndex = 0;
			tabControl_Main.Size = new Size(540, 296);
			tabControl_Main.TabIndex = 100;
			// 
			// tabPage_Update_Type
			// 
			tabPage_Update_Type.Controls.Add(groupBox_Settings_RemoteServer);
			tabPage_Update_Type.Controls.Add(radioButton_Settings_Method__Remote);
			tabPage_Update_Type.Controls.Add(radioButton_Settings_Method__Local);
			tabPage_Update_Type.Location = new Point(4, 22);
			tabPage_Update_Type.Margin = new Padding(3, 2, 3, 2);
			tabPage_Update_Type.Name = "tabPage_Update_Type";
			tabPage_Update_Type.Padding = new Padding(3, 2, 3, 2);
			tabPage_Update_Type.Size = new Size(532, 270);
			tabPage_Update_Type.TabIndex = 0;
			tabPage_Update_Type.Text = "更新方式";
			tabPage_Update_Type.UseVisualStyleBackColor = true;
			// 
			// groupBox_Settings_RemoteServer
			// 
			groupBox_Settings_RemoteServer.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			groupBox_Settings_RemoteServer.Controls.Add(checkBox_Settings_RemoteServer__Ping);
			groupBox_Settings_RemoteServer.Controls.Add(checkBox_Settings_RemoteServer__Pwd);
			groupBox_Settings_RemoteServer.Controls.Add(textBox_Settings_RemoteServer__Pwd);
			groupBox_Settings_RemoteServer.Controls.Add(textBox_Settings_RemoteServer__Ping);
			groupBox_Settings_RemoteServer.Controls.Add(textBox_Settings_RemoteServer__User);
			groupBox_Settings_RemoteServer.Controls.Add(textBox_Settings_RemoteServer__Addr);
			groupBox_Settings_RemoteServer.Controls.Add(label_Settings_RemoteServer__Ping);
			groupBox_Settings_RemoteServer.Controls.Add(label_Settings_RemoteServer__Pwd);
			groupBox_Settings_RemoteServer.Controls.Add(label_Settings_RemoteServer__User);
			groupBox_Settings_RemoteServer.Controls.Add(label_Settings_RemoteServer__Addr);
			groupBox_Settings_RemoteServer.Location = new Point(6, 49);
			groupBox_Settings_RemoteServer.Name = "groupBox_Settings_RemoteServer";
			groupBox_Settings_RemoteServer.Size = new Size(520, 128);
			groupBox_Settings_RemoteServer.TabIndex = 103;
			groupBox_Settings_RemoteServer.TabStop = false;
			groupBox_Settings_RemoteServer.Text = "远程 Server 设置";
			// 
			// checkBox_Settings_RemoteServer__Ping
			// 
			checkBox_Settings_RemoteServer__Ping.AutoSize = true;
			checkBox_Settings_RemoteServer__Ping.Checked = true;
			checkBox_Settings_RemoteServer__Ping.CheckState = CheckState.Checked;
			checkBox_Settings_RemoteServer__Ping.Location = new Point(227, 103);
			checkBox_Settings_RemoteServer__Ping.Name = "checkBox_Settings_RemoteServer__Ping";
			checkBox_Settings_RemoteServer__Ping.Size = new Size(120, 16);
			checkBox_Settings_RemoteServer__Ping.TabIndex = 113;
			checkBox_Settings_RemoteServer__Ping.Text = "自动 ping 服务器";
			checkBox_Settings_RemoteServer__Ping.UseVisualStyleBackColor = true;
			checkBox_Settings_RemoteServer__Ping.CheckedChanged += checkBox_Settings_RemoteServer__Ping_CheckedChanged;
			// 
			// checkBox_Settings_RemoteServer__Pwd
			// 
			checkBox_Settings_RemoteServer__Pwd.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			checkBox_Settings_RemoteServer__Pwd.AutoSize = true;
			checkBox_Settings_RemoteServer__Pwd.Location = new Point(466, 76);
			checkBox_Settings_RemoteServer__Pwd.Name = "checkBox_Settings_RemoteServer__Pwd";
			checkBox_Settings_RemoteServer__Pwd.Size = new Size(48, 16);
			checkBox_Settings_RemoteServer__Pwd.TabIndex = 110;
			checkBox_Settings_RemoteServer__Pwd.Text = "显示";
			checkBox_Settings_RemoteServer__Pwd.UseVisualStyleBackColor = true;
			checkBox_Settings_RemoteServer__Pwd.CheckedChanged += checkBox_Settings_RemoteServer__Pwd_CheckedChanged;
			// 
			// textBox_Settings_RemoteServer__Pwd
			// 
			textBox_Settings_RemoteServer__Pwd.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			textBox_Settings_RemoteServer__Pwd.Location = new Point(161, 74);
			textBox_Settings_RemoteServer__Pwd.Name = "textBox_Settings_RemoteServer__Pwd";
			textBox_Settings_RemoteServer__Pwd.PasswordChar = '*';
			textBox_Settings_RemoteServer__Pwd.ReadOnly = true;
			textBox_Settings_RemoteServer__Pwd.Size = new Size(299, 21);
			textBox_Settings_RemoteServer__Pwd.TabIndex = 109;
			textBox_Settings_RemoteServer__Pwd.TextChanged += textBox_Settings_RemoteServer__Pwd_TextChanged;
			// 
			// textBox_Settings_RemoteServer__Ping
			// 
			textBox_Settings_RemoteServer__Ping.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			textBox_Settings_RemoteServer__Ping.Location = new Point(161, 101);
			textBox_Settings_RemoteServer__Ping.Name = "textBox_Settings_RemoteServer__Ping";
			textBox_Settings_RemoteServer__Ping.ReadOnly = true;
			textBox_Settings_RemoteServer__Ping.Size = new Size(63, 21);
			textBox_Settings_RemoteServer__Ping.TabIndex = 112;
			// 
			// textBox_Settings_RemoteServer__User
			// 
			textBox_Settings_RemoteServer__User.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			textBox_Settings_RemoteServer__User.Location = new Point(161, 47);
			textBox_Settings_RemoteServer__User.Name = "textBox_Settings_RemoteServer__User";
			textBox_Settings_RemoteServer__User.ReadOnly = true;
			textBox_Settings_RemoteServer__User.Size = new Size(299, 21);
			textBox_Settings_RemoteServer__User.TabIndex = 107;
			textBox_Settings_RemoteServer__User.TextChanged += textBox_Settings_RemoteServer__User_TextChanged;
			// 
			// textBox_Settings_RemoteServer__Addr
			// 
			textBox_Settings_RemoteServer__Addr.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			textBox_Settings_RemoteServer__Addr.Location = new Point(161, 20);
			textBox_Settings_RemoteServer__Addr.Name = "textBox_Settings_RemoteServer__Addr";
			textBox_Settings_RemoteServer__Addr.ReadOnly = true;
			textBox_Settings_RemoteServer__Addr.Size = new Size(353, 21);
			textBox_Settings_RemoteServer__Addr.TabIndex = 105;
			textBox_Settings_RemoteServer__Addr.Text = "127.0.0.1:3333";
			textBox_Settings_RemoteServer__Addr.TextChanged += textBox_Settings_RemoteServer__Addr_TextChanged;
			// 
			// label_Settings_RemoteServer__Ping
			// 
			label_Settings_RemoteServer__Ping.AutoSize = true;
			label_Settings_RemoteServer__Ping.Location = new Point(6, 104);
			label_Settings_RemoteServer__Ping.Name = "label_Settings_RemoteServer__Ping";
			label_Settings_RemoteServer__Ping.Size = new Size(89, 12);
			label_Settings_RemoteServer__Ping.TabIndex = 111;
			label_Settings_RemoteServer__Ping.Text = "Ping 值 (ms)：";
			// 
			// label_Settings_RemoteServer__Pwd
			// 
			label_Settings_RemoteServer__Pwd.AutoSize = true;
			label_Settings_RemoteServer__Pwd.Location = new Point(6, 77);
			label_Settings_RemoteServer__Pwd.Name = "label_Settings_RemoteServer__Pwd";
			label_Settings_RemoteServer__Pwd.Size = new Size(137, 12);
			label_Settings_RemoteServer__Pwd.TabIndex = 108;
			label_Settings_RemoteServer__Pwd.Text = "登录到 Server 的密码：";
			// 
			// label_Settings_RemoteServer__User
			// 
			label_Settings_RemoteServer__User.AutoSize = true;
			label_Settings_RemoteServer__User.Location = new Point(6, 50);
			label_Settings_RemoteServer__User.Name = "label_Settings_RemoteServer__User";
			label_Settings_RemoteServer__User.Size = new Size(149, 12);
			label_Settings_RemoteServer__User.TabIndex = 106;
			label_Settings_RemoteServer__User.Text = "登录到 Server 的用户名：";
			// 
			// label_Settings_RemoteServer__Addr
			// 
			label_Settings_RemoteServer__Addr.AutoSize = true;
			label_Settings_RemoteServer__Addr.Location = new Point(6, 23);
			label_Settings_RemoteServer__Addr.Name = "label_Settings_RemoteServer__Addr";
			label_Settings_RemoteServer__Addr.Size = new Size(113, 12);
			label_Settings_RemoteServer__Addr.TabIndex = 104;
			label_Settings_RemoteServer__Addr.Text = "Server 地址/端口：";
			// 
			// radioButton_Settings_Method__Remote
			// 
			radioButton_Settings_Method__Remote.AutoSize = true;
			radioButton_Settings_Method__Remote.Location = new Point(6, 27);
			radioButton_Settings_Method__Remote.Name = "radioButton_Settings_Method__Remote";
			radioButton_Settings_Method__Remote.Size = new Size(227, 16);
			radioButton_Settings_Method__Remote.TabIndex = 102;
			radioButton_Settings_Method__Remote.Text = "远程更新（由远程 Server 执行更新）";
			radioButton_Settings_Method__Remote.UseVisualStyleBackColor = true;
			radioButton_Settings_Method__Remote.CheckedChanged += radioButton_Settings_Method__CheckedChanged;
			// 
			// radioButton_Settings_Method__Local
			// 
			radioButton_Settings_Method__Local.AutoSize = true;
			radioButton_Settings_Method__Local.Checked = true;
			radioButton_Settings_Method__Local.Location = new Point(6, 5);
			radioButton_Settings_Method__Local.Name = "radioButton_Settings_Method__Local";
			radioButton_Settings_Method__Local.Size = new Size(119, 16);
			radioButton_Settings_Method__Local.TabIndex = 101;
			radioButton_Settings_Method__Local.TabStop = true;
			radioButton_Settings_Method__Local.Text = "本地更新（直连）";
			radioButton_Settings_Method__Local.UseVisualStyleBackColor = true;
			radioButton_Settings_Method__Local.CheckedChanged += radioButton_Settings_Method__CheckedChanged;
			// 
			// tabPage_Set_IP
			// 
			tabPage_Set_IP.Controls.Add(groupBox_Set_IPv6);
			tabPage_Set_IP.Controls.Add(groupBox_Set_IPv4);
			tabPage_Set_IP.Location = new Point(4, 26);
			tabPage_Set_IP.Margin = new Padding(3, 2, 3, 2);
			tabPage_Set_IP.Name = "tabPage_Set_IP";
			tabPage_Set_IP.Padding = new Padding(3, 2, 3, 2);
			tabPage_Set_IP.Size = new Size(532, 266);
			tabPage_Set_IP.TabIndex = 1;
			tabPage_Set_IP.Text = "设置 IP";
			tabPage_Set_IP.UseVisualStyleBackColor = true;
			// 
			// groupBox_Set_IPv6
			// 
			groupBox_Set_IPv6.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			groupBox_Set_IPv6.Controls.Add(textBox_Settings_IPv6);
			groupBox_Set_IPv6.Controls.Add(radioButton_Settings_IPv6__Accept_IP);
			groupBox_Set_IPv6.Controls.Add(radioButton_Settings_IPv6__From_URL);
			groupBox_Set_IPv6.Controls.Add(radioButton_Settings_IPv6__Manual);
			groupBox_Set_IPv6.Controls.Add(comboBox_Settings_IPv6__From_URL);
			groupBox_Set_IPv6.Location = new Point(6, 106);
			groupBox_Set_IPv6.Name = "groupBox_Set_IPv6";
			groupBox_Set_IPv6.Size = new Size(520, 95);
			groupBox_Set_IPv6.TabIndex = 207;
			groupBox_Set_IPv6.TabStop = false;
			groupBox_Set_IPv6.Text = "IPv6";
			// 
			// textBox_Settings_IPv6
			// 
			textBox_Settings_IPv6.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			textBox_Settings_IPv6.Location = new Point(191, 46);
			textBox_Settings_IPv6.Name = "textBox_Settings_IPv6";
			textBox_Settings_IPv6.ReadOnly = true;
			textBox_Settings_IPv6.Size = new Size(323, 21);
			textBox_Settings_IPv6.TabIndex = 211;
			textBox_Settings_IPv6.TextChanged += textBox_Settings_IPv6_TextChanged;
			// 
			// radioButton_Settings_IPv6__Accept_IP
			// 
			radioButton_Settings_IPv6__Accept_IP.AutoSize = true;
			radioButton_Settings_IPv6__Accept_IP.Location = new Point(6, 73);
			radioButton_Settings_IPv6__Accept_IP.Name = "radioButton_Settings_IPv6__Accept_IP";
			radioButton_Settings_IPv6__Accept_IP.Size = new Size(191, 16);
			radioButton_Settings_IPv6__Accept_IP.TabIndex = 212;
			radioButton_Settings_IPv6__Accept_IP.TabStop = true;
			radioButton_Settings_IPv6__Accept_IP.Text = "Server 接受连接的客户端 IPv6";
			radioButton_Settings_IPv6__Accept_IP.UseVisualStyleBackColor = true;
			radioButton_Settings_IPv6__Accept_IP.CheckedChanged += radioButton_Settings_IPv6__CheckedChanged;
			// 
			// radioButton_Settings_IPv6__From_URL
			// 
			radioButton_Settings_IPv6__From_URL.AutoSize = true;
			radioButton_Settings_IPv6__From_URL.Checked = true;
			radioButton_Settings_IPv6__From_URL.Location = new Point(6, 21);
			radioButton_Settings_IPv6__From_URL.Name = "radioButton_Settings_IPv6__From_URL";
			radioButton_Settings_IPv6__From_URL.Size = new Size(155, 16);
			radioButton_Settings_IPv6__From_URL.TabIndex = 208;
			radioButton_Settings_IPv6__From_URL.TabStop = true;
			radioButton_Settings_IPv6__From_URL.Text = "通过 URL 获取公网 IPv6";
			radioButton_Settings_IPv6__From_URL.UseVisualStyleBackColor = true;
			radioButton_Settings_IPv6__From_URL.CheckedChanged += radioButton_Settings_IPv6__CheckedChanged;
			// 
			// radioButton_Settings_IPv6__Manual
			// 
			radioButton_Settings_IPv6__Manual.AutoSize = true;
			radioButton_Settings_IPv6__Manual.Location = new Point(6, 47);
			radioButton_Settings_IPv6__Manual.Name = "radioButton_Settings_IPv6__Manual";
			radioButton_Settings_IPv6__Manual.Size = new Size(101, 16);
			radioButton_Settings_IPv6__Manual.TabIndex = 210;
			radioButton_Settings_IPv6__Manual.TabStop = true;
			radioButton_Settings_IPv6__Manual.Text = "手动指定 IPv6";
			radioButton_Settings_IPv6__Manual.UseVisualStyleBackColor = true;
			radioButton_Settings_IPv6__Manual.CheckedChanged += radioButton_Settings_IPv6__CheckedChanged;
			// 
			// comboBox_Settings_IPv6__From_URL
			// 
			comboBox_Settings_IPv6__From_URL.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			comboBox_Settings_IPv6__From_URL.FormattingEnabled = true;
			comboBox_Settings_IPv6__From_URL.Location = new Point(191, 20);
			comboBox_Settings_IPv6__From_URL.Name = "comboBox_Settings_IPv6__From_URL";
			comboBox_Settings_IPv6__From_URL.Size = new Size(323, 20);
			comboBox_Settings_IPv6__From_URL.TabIndex = 209;
			comboBox_Settings_IPv6__From_URL.SelectedIndexChanged += comboBox_Settings_IPv6__From_URL_SelectedIndexChanged;
			// 
			// groupBox_Set_IPv4
			// 
			groupBox_Set_IPv4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			groupBox_Set_IPv4.Controls.Add(textBox_Settings_IPv4);
			groupBox_Set_IPv4.Controls.Add(radioButton_Settings_IPv4__Accept_IP);
			groupBox_Set_IPv4.Controls.Add(radioButton_Settings_IPv4__Manual);
			groupBox_Set_IPv4.Controls.Add(comboBox_Settings_IPv4__From_URL);
			groupBox_Set_IPv4.Controls.Add(radioButton_Settings_IPv4__From_URL);
			groupBox_Set_IPv4.Location = new Point(6, 5);
			groupBox_Set_IPv4.Name = "groupBox_Set_IPv4";
			groupBox_Set_IPv4.Size = new Size(520, 95);
			groupBox_Set_IPv4.TabIndex = 201;
			groupBox_Set_IPv4.TabStop = false;
			groupBox_Set_IPv4.Text = "IPv4";
			// 
			// textBox_Settings_IPv4
			// 
			textBox_Settings_IPv4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			textBox_Settings_IPv4.Location = new Point(191, 46);
			textBox_Settings_IPv4.Name = "textBox_Settings_IPv4";
			textBox_Settings_IPv4.ReadOnly = true;
			textBox_Settings_IPv4.Size = new Size(323, 21);
			textBox_Settings_IPv4.TabIndex = 205;
			textBox_Settings_IPv4.TextChanged += textBox_Settings_IPv4_TextChanged;
			// 
			// radioButton_Settings_IPv4__Accept_IP
			// 
			radioButton_Settings_IPv4__Accept_IP.AutoSize = true;
			radioButton_Settings_IPv4__Accept_IP.Location = new Point(6, 73);
			radioButton_Settings_IPv4__Accept_IP.Name = "radioButton_Settings_IPv4__Accept_IP";
			radioButton_Settings_IPv4__Accept_IP.Size = new Size(191, 16);
			radioButton_Settings_IPv4__Accept_IP.TabIndex = 206;
			radioButton_Settings_IPv4__Accept_IP.TabStop = true;
			radioButton_Settings_IPv4__Accept_IP.Text = "Server 接受连接的客户端 IPv4";
			radioButton_Settings_IPv4__Accept_IP.UseVisualStyleBackColor = true;
			radioButton_Settings_IPv4__Accept_IP.CheckedChanged += radioButton_Settings_IPv4__CheckedChanged;
			// 
			// radioButton_Settings_IPv4__Manual
			// 
			radioButton_Settings_IPv4__Manual.AutoSize = true;
			radioButton_Settings_IPv4__Manual.Location = new Point(6, 47);
			radioButton_Settings_IPv4__Manual.Name = "radioButton_Settings_IPv4__Manual";
			radioButton_Settings_IPv4__Manual.Size = new Size(101, 16);
			radioButton_Settings_IPv4__Manual.TabIndex = 204;
			radioButton_Settings_IPv4__Manual.TabStop = true;
			radioButton_Settings_IPv4__Manual.Text = "手动指定 IPv4";
			radioButton_Settings_IPv4__Manual.UseVisualStyleBackColor = true;
			radioButton_Settings_IPv4__Manual.CheckedChanged += radioButton_Settings_IPv4__CheckedChanged;
			// 
			// comboBox_Settings_IPv4__From_URL
			// 
			comboBox_Settings_IPv4__From_URL.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			comboBox_Settings_IPv4__From_URL.FormattingEnabled = true;
			comboBox_Settings_IPv4__From_URL.Location = new Point(191, 20);
			comboBox_Settings_IPv4__From_URL.Name = "comboBox_Settings_IPv4__From_URL";
			comboBox_Settings_IPv4__From_URL.Size = new Size(323, 20);
			comboBox_Settings_IPv4__From_URL.TabIndex = 203;
			comboBox_Settings_IPv4__From_URL.SelectedIndexChanged += comboBox_Settings_IPv4__From_URL_SelectedIndexChanged;
			// 
			// radioButton_Settings_IPv4__From_URL
			// 
			radioButton_Settings_IPv4__From_URL.AutoSize = true;
			radioButton_Settings_IPv4__From_URL.Checked = true;
			radioButton_Settings_IPv4__From_URL.Location = new Point(6, 21);
			radioButton_Settings_IPv4__From_URL.Name = "radioButton_Settings_IPv4__From_URL";
			radioButton_Settings_IPv4__From_URL.Size = new Size(155, 16);
			radioButton_Settings_IPv4__From_URL.TabIndex = 202;
			radioButton_Settings_IPv4__From_URL.TabStop = true;
			radioButton_Settings_IPv4__From_URL.Text = "通过 URL 获取公网 IPv4";
			radioButton_Settings_IPv4__From_URL.UseVisualStyleBackColor = true;
			radioButton_Settings_IPv4__From_URL.CheckedChanged += radioButton_Settings_IPv4__CheckedChanged;
			// 
			// tabPage_Security
			// 
			tabPage_Security.Controls.Add(groupBox_Security__Property);
			tabPage_Security.Controls.Add(button_Security_Del);
			tabPage_Security.Controls.Add(button_Security_Add);
			tabPage_Security.Controls.Add(listView_Security);
			tabPage_Security.Location = new Point(4, 26);
			tabPage_Security.Margin = new Padding(3, 2, 3, 2);
			tabPage_Security.Name = "tabPage_Security";
			tabPage_Security.Padding = new Padding(3, 2, 3, 2);
			tabPage_Security.Size = new Size(532, 266);
			tabPage_Security.TabIndex = 2;
			tabPage_Security.Text = "安全设置";
			tabPage_Security.UseVisualStyleBackColor = true;
			// 
			// groupBox_Security__Property
			// 
			groupBox_Security__Property.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			groupBox_Security__Property.Controls.Add(checkBox_Security__Save_To_Config);
			groupBox_Security__Property.Controls.Add(tabControl_Security__Property);
			groupBox_Security__Property.Controls.Add(textBox_Security__Property__Name);
			groupBox_Security__Property.Controls.Add(label_Security__Property__Name);
			groupBox_Security__Property.Location = new Point(164, 5);
			groupBox_Security__Property.Name = "groupBox_Security__Property";
			groupBox_Security__Property.Size = new Size(362, 172);
			groupBox_Security__Property.TabIndex = 304;
			groupBox_Security__Property.TabStop = false;
			groupBox_Security__Property.Text = "属性";
			// 
			// checkBox_Security__Save_To_Config
			// 
			checkBox_Security__Save_To_Config.AutoSize = true;
			checkBox_Security__Save_To_Config.Checked = true;
			checkBox_Security__Save_To_Config.CheckState = CheckState.Checked;
			checkBox_Security__Save_To_Config.Location = new Point(6, 150);
			checkBox_Security__Save_To_Config.Name = "checkBox_Security__Save_To_Config";
			checkBox_Security__Save_To_Config.Size = new Size(132, 16);
			checkBox_Security__Save_To_Config.TabIndex = 350;
			checkBox_Security__Save_To_Config.Text = "保存到 Config 文件";
			checkBox_Security__Save_To_Config.UseVisualStyleBackColor = true;
			checkBox_Security__Save_To_Config.CheckedChanged += checkBox_Security__Save_To_Config_CheckedChanged;
			// 
			// tabControl_Security__Property
			// 
			tabControl_Security__Property.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			tabControl_Security__Property.Controls.Add(tabPage_Security__Godaddy);
			tabControl_Security__Property.Controls.Add(tabPage_Security__dynv6);
			tabControl_Security__Property.Controls.Add(tabPage_Security__dynu);
			tabControl_Security__Property.Location = new Point(6, 47);
			tabControl_Security__Property.Name = "tabControl_Security__Property";
			tabControl_Security__Property.SelectedIndex = 0;
			tabControl_Security__Property.Size = new Size(350, 97);
			tabControl_Security__Property.TabIndex = 307;
			// 
			// tabPage_Security__Godaddy
			// 
			tabPage_Security__Godaddy.Controls.Add(checkBox_Security_Godaddy__Secret);
			tabPage_Security__Godaddy.Controls.Add(checkBox_Security_Godaddy__Key);
			tabPage_Security__Godaddy.Controls.Add(textBox_Security_Godaddy__Secret);
			tabPage_Security__Godaddy.Controls.Add(textBox_Security_Godaddy__Key);
			tabPage_Security__Godaddy.Controls.Add(label_Security_Godaddy__Secret);
			tabPage_Security__Godaddy.Controls.Add(label_Security_Godaddy__Key);
			tabPage_Security__Godaddy.Controls.Add(linkLabel_Security_Godaddy__API);
			tabPage_Security__Godaddy.Location = new Point(4, 22);
			tabPage_Security__Godaddy.Name = "tabPage_Security__Godaddy";
			tabPage_Security__Godaddy.Padding = new Padding(3);
			tabPage_Security__Godaddy.Size = new Size(342, 71);
			tabPage_Security__Godaddy.TabIndex = 0;
			tabPage_Security__Godaddy.Text = "Godaddy";
			tabPage_Security__Godaddy.UseVisualStyleBackColor = true;
			// 
			// checkBox_Security_Godaddy__Secret
			// 
			checkBox_Security_Godaddy__Secret.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			checkBox_Security_Godaddy__Secret.AutoSize = true;
			checkBox_Security_Godaddy__Secret.Location = new Point(288, 47);
			checkBox_Security_Godaddy__Secret.Name = "checkBox_Security_Godaddy__Secret";
			checkBox_Security_Godaddy__Secret.Size = new Size(48, 16);
			checkBox_Security_Godaddy__Secret.TabIndex = 314;
			checkBox_Security_Godaddy__Secret.Text = "显示";
			checkBox_Security_Godaddy__Secret.UseVisualStyleBackColor = true;
			checkBox_Security_Godaddy__Secret.CheckedChanged += checkBox_Security_Godaddy__Secret_CheckedChanged;
			// 
			// checkBox_Security_Godaddy__Key
			// 
			checkBox_Security_Godaddy__Key.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			checkBox_Security_Godaddy__Key.AutoSize = true;
			checkBox_Security_Godaddy__Key.Location = new Point(288, 20);
			checkBox_Security_Godaddy__Key.Name = "checkBox_Security_Godaddy__Key";
			checkBox_Security_Godaddy__Key.Size = new Size(48, 16);
			checkBox_Security_Godaddy__Key.TabIndex = 311;
			checkBox_Security_Godaddy__Key.Text = "显示";
			checkBox_Security_Godaddy__Key.UseVisualStyleBackColor = true;
			checkBox_Security_Godaddy__Key.CheckedChanged += checkBox_Security_Godaddy__Key_CheckedChanged;
			// 
			// textBox_Security_Godaddy__Secret
			// 
			textBox_Security_Godaddy__Secret.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			textBox_Security_Godaddy__Secret.Location = new Point(71, 45);
			textBox_Security_Godaddy__Secret.Name = "textBox_Security_Godaddy__Secret";
			textBox_Security_Godaddy__Secret.PasswordChar = '*';
			textBox_Security_Godaddy__Secret.Size = new Size(211, 21);
			textBox_Security_Godaddy__Secret.TabIndex = 313;
			textBox_Security_Godaddy__Secret.TextChanged += textBox_Security_Godaddy__Secret_TextChanged;
			// 
			// textBox_Security_Godaddy__Key
			// 
			textBox_Security_Godaddy__Key.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			textBox_Security_Godaddy__Key.Location = new Point(71, 18);
			textBox_Security_Godaddy__Key.Name = "textBox_Security_Godaddy__Key";
			textBox_Security_Godaddy__Key.PasswordChar = '*';
			textBox_Security_Godaddy__Key.Size = new Size(211, 21);
			textBox_Security_Godaddy__Key.TabIndex = 310;
			textBox_Security_Godaddy__Key.TextChanged += textBox_Security_Godaddy__Key_TextChanged;
			// 
			// label_Security_Godaddy__Secret
			// 
			label_Security_Godaddy__Secret.AutoSize = true;
			label_Security_Godaddy__Secret.Location = new Point(6, 48);
			label_Security_Godaddy__Secret.Name = "label_Security_Godaddy__Secret";
			label_Security_Godaddy__Secret.Size = new Size(53, 12);
			label_Security_Godaddy__Secret.TabIndex = 312;
			label_Security_Godaddy__Secret.Text = "Secret：";
			// 
			// label_Security_Godaddy__Key
			// 
			label_Security_Godaddy__Key.AutoSize = true;
			label_Security_Godaddy__Key.Location = new Point(6, 21);
			label_Security_Godaddy__Key.Name = "label_Security_Godaddy__Key";
			label_Security_Godaddy__Key.Size = new Size(35, 12);
			label_Security_Godaddy__Key.TabIndex = 309;
			label_Security_Godaddy__Key.Text = "Key：";
			// 
			// linkLabel_Security_Godaddy__API
			// 
			linkLabel_Security_Godaddy__API.AutoSize = true;
			linkLabel_Security_Godaddy__API.Location = new Point(6, 3);
			linkLabel_Security_Godaddy__API.Name = "linkLabel_Security_Godaddy__API";
			linkLabel_Security_Godaddy__API.Size = new Size(185, 12);
			linkLabel_Security_Godaddy__API.TabIndex = 308;
			linkLabel_Security_Godaddy__API.TabStop = true;
			linkLabel_Security_Godaddy__API.Text = "https://developer.godaddy.com/";
			linkLabel_Security_Godaddy__API.LinkClicked += linkLabel_Security__LinkClicked;
			// 
			// tabPage_Security__dynv6
			// 
			tabPage_Security__dynv6.Controls.Add(checkBox_Security_dynv6__token);
			tabPage_Security__dynv6.Controls.Add(textBox_Security_dynv6__token);
			tabPage_Security__dynv6.Controls.Add(label_Security_dynv6__token);
			tabPage_Security__dynv6.Controls.Add(linkLabel_Security_dynv6__API);
			tabPage_Security__dynv6.Location = new Point(4, 26);
			tabPage_Security__dynv6.Name = "tabPage_Security__dynv6";
			tabPage_Security__dynv6.Padding = new Padding(3);
			tabPage_Security__dynv6.Size = new Size(342, 67);
			tabPage_Security__dynv6.TabIndex = 1;
			tabPage_Security__dynv6.Text = "dynv6";
			tabPage_Security__dynv6.UseVisualStyleBackColor = true;
			// 
			// checkBox_Security_dynv6__token
			// 
			checkBox_Security_dynv6__token.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			checkBox_Security_dynv6__token.AutoSize = true;
			checkBox_Security_dynv6__token.Location = new Point(288, 20);
			checkBox_Security_dynv6__token.Name = "checkBox_Security_dynv6__token";
			checkBox_Security_dynv6__token.Size = new Size(48, 16);
			checkBox_Security_dynv6__token.TabIndex = 318;
			checkBox_Security_dynv6__token.Text = "显示";
			checkBox_Security_dynv6__token.UseVisualStyleBackColor = true;
			checkBox_Security_dynv6__token.CheckedChanged += checkBox_Security_dynv6__token_CheckedChanged;
			// 
			// textBox_Security_dynv6__token
			// 
			textBox_Security_dynv6__token.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			textBox_Security_dynv6__token.Location = new Point(71, 18);
			textBox_Security_dynv6__token.Name = "textBox_Security_dynv6__token";
			textBox_Security_dynv6__token.PasswordChar = '*';
			textBox_Security_dynv6__token.Size = new Size(211, 21);
			textBox_Security_dynv6__token.TabIndex = 317;
			textBox_Security_dynv6__token.TextChanged += textBox_Security_dynv6__token_TextChanged;
			// 
			// label_Security_dynv6__token
			// 
			label_Security_dynv6__token.AutoSize = true;
			label_Security_dynv6__token.Location = new Point(6, 21);
			label_Security_dynv6__token.Name = "label_Security_dynv6__token";
			label_Security_dynv6__token.Size = new Size(47, 12);
			label_Security_dynv6__token.TabIndex = 316;
			label_Security_dynv6__token.Text = "token：";
			// 
			// linkLabel_Security_dynv6__API
			// 
			linkLabel_Security_dynv6__API.AutoSize = true;
			linkLabel_Security_dynv6__API.Location = new Point(6, 3);
			linkLabel_Security_dynv6__API.Name = "linkLabel_Security_dynv6__API";
			linkLabel_Security_dynv6__API.Size = new Size(167, 12);
			linkLabel_Security_dynv6__API.TabIndex = 315;
			linkLabel_Security_dynv6__API.TabStop = true;
			linkLabel_Security_dynv6__API.Text = "https://dynv6.com/docs/apis";
			linkLabel_Security_dynv6__API.LinkClicked += linkLabel_Security__LinkClicked;
			// 
			// tabPage_Security__dynu
			// 
			tabPage_Security__dynu.Controls.Add(checkBox_Security_dynu__API_Key);
			tabPage_Security__dynu.Controls.Add(textBox_Security_dynu__API_Key);
			tabPage_Security__dynu.Controls.Add(label_Security_dynu__API_Key);
			tabPage_Security__dynu.Controls.Add(linkLabel_Security_dynu__API);
			tabPage_Security__dynu.Location = new Point(4, 26);
			tabPage_Security__dynu.Name = "tabPage_Security__dynu";
			tabPage_Security__dynu.Padding = new Padding(3);
			tabPage_Security__dynu.Size = new Size(342, 67);
			tabPage_Security__dynu.TabIndex = 2;
			tabPage_Security__dynu.Text = "dynu";
			tabPage_Security__dynu.UseVisualStyleBackColor = true;
			// 
			// checkBox_Security_dynu__API_Key
			// 
			checkBox_Security_dynu__API_Key.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			checkBox_Security_dynu__API_Key.AutoSize = true;
			checkBox_Security_dynu__API_Key.Location = new Point(288, 20);
			checkBox_Security_dynu__API_Key.Name = "checkBox_Security_dynu__API_Key";
			checkBox_Security_dynu__API_Key.Size = new Size(48, 16);
			checkBox_Security_dynu__API_Key.TabIndex = 322;
			checkBox_Security_dynu__API_Key.Text = "显示";
			checkBox_Security_dynu__API_Key.UseVisualStyleBackColor = true;
			checkBox_Security_dynu__API_Key.CheckedChanged += checkBox_Security_dynu__API_Key_CheckedChanged;
			// 
			// textBox_Security_dynu__API_Key
			// 
			textBox_Security_dynu__API_Key.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			textBox_Security_dynu__API_Key.Location = new Point(71, 18);
			textBox_Security_dynu__API_Key.Name = "textBox_Security_dynu__API_Key";
			textBox_Security_dynu__API_Key.PasswordChar = '*';
			textBox_Security_dynu__API_Key.Size = new Size(211, 21);
			textBox_Security_dynu__API_Key.TabIndex = 321;
			textBox_Security_dynu__API_Key.TextChanged += textBox_Security_dynu__API_Key_TextChanged;
			// 
			// label_Security_dynu__API_Key
			// 
			label_Security_dynu__API_Key.AutoSize = true;
			label_Security_dynu__API_Key.Location = new Point(6, 21);
			label_Security_dynu__API_Key.Name = "label_Security_dynu__API_Key";
			label_Security_dynu__API_Key.Size = new Size(59, 12);
			label_Security_dynu__API_Key.TabIndex = 320;
			label_Security_dynu__API_Key.Text = "API-Key：";
			// 
			// linkLabel_Security_dynu__API
			// 
			linkLabel_Security_dynu__API.AutoSize = true;
			linkLabel_Security_dynu__API.Location = new Point(6, 3);
			linkLabel_Security_dynu__API.Name = "linkLabel_Security_dynu__API";
			linkLabel_Security_dynu__API.Size = new Size(233, 12);
			linkLabel_Security_dynu__API.TabIndex = 319;
			linkLabel_Security_dynu__API.TabStop = true;
			linkLabel_Security_dynu__API.Text = "https://www.dynu.com/en-US/Support/API";
			linkLabel_Security_dynu__API.LinkClicked += linkLabel_Security__LinkClicked;
			// 
			// textBox_Security__Property__Name
			// 
			textBox_Security__Property__Name.Location = new Point(95, 20);
			textBox_Security__Property__Name.Name = "textBox_Security__Property__Name";
			textBox_Security__Property__Name.Size = new Size(144, 21);
			textBox_Security__Property__Name.TabIndex = 306;
			textBox_Security__Property__Name.TextChanged += textBox_Security__Property__Name_TextChanged;
			// 
			// label_Security__Property__Name
			// 
			label_Security__Property__Name.AutoSize = true;
			label_Security__Property__Name.Location = new Point(6, 23);
			label_Security__Property__Name.Name = "label_Security__Property__Name";
			label_Security__Property__Name.Size = new Size(77, 12);
			label_Security__Property__Name.TabIndex = 305;
			label_Security__Property__Name.Text = "配置的名称：";
			// 
			// button_Security_Del
			// 
			button_Security_Del.Enabled = false;
			button_Security_Del.Location = new Point(35, 242);
			button_Security_Del.Name = "button_Security_Del";
			button_Security_Del.Size = new Size(23, 23);
			button_Security_Del.TabIndex = 303;
			button_Security_Del.Text = "-";
			button_Security_Del.UseVisualStyleBackColor = true;
			button_Security_Del.Click += button_Security_Del_Click;
			// 
			// button_Security_Add
			// 
			button_Security_Add.Location = new Point(6, 242);
			button_Security_Add.Name = "button_Security_Add";
			button_Security_Add.Size = new Size(23, 23);
			button_Security_Add.TabIndex = 302;
			button_Security_Add.Text = "+";
			button_Security_Add.UseVisualStyleBackColor = true;
			button_Security_Add.Click += button_Security_Add_Click;
			// 
			// listView_Security
			// 
			listView_Security.Columns.AddRange(new ColumnHeader[] { columnHeader_Security });
			listView_Security.ContextMenuStrip = contextMenuStrip_Security;
			listView_Security.FullRowSelect = true;
			listView_Security.GridLines = true;
			listView_Security.Location = new Point(6, 5);
			listView_Security.MultiSelect = false;
			listView_Security.Name = "listView_Security";
			listView_Security.Size = new Size(152, 231);
			listView_Security.TabIndex = 301;
			listView_Security.UseCompatibleStateImageBehavior = false;
			listView_Security.View = View.Details;
			listView_Security.SelectedIndexChanged += listView_Security_SelectedIndexChanged;
			listView_Security.Resize += listView_Security_Resize;
			// 
			// columnHeader_Security
			// 
			columnHeader_Security.Text = "Profile";
			columnHeader_Security.Width = 131;
			// 
			// contextMenuStrip_Security
			// 
			contextMenuStrip_Security.Font = new Font("新宋体", 9F);
			contextMenuStrip_Security.Items.AddRange(new ToolStripItem[] { ToolStripMenuItem__Security_Add, ToolStripMenuItem__Security_Del });
			contextMenuStrip_Security.Name = "contextMenuStrip_Security";
			contextMenuStrip_Security.Size = new Size(95, 48);
			// 
			// ToolStripMenuItem__Security_Add
			// 
			ToolStripMenuItem__Security_Add.Image = res_Main.Add;
			ToolStripMenuItem__Security_Add.Name = "ToolStripMenuItem__Security_Add";
			ToolStripMenuItem__Security_Add.Size = new Size(94, 22);
			ToolStripMenuItem__Security_Add.Text = "添加";
			ToolStripMenuItem__Security_Add.Click += ToolStripMenuItem__Security_Add_Click;
			// 
			// ToolStripMenuItem__Security_Del
			// 
			ToolStripMenuItem__Security_Del.Enabled = false;
			ToolStripMenuItem__Security_Del.Image = res_Main.Delete;
			ToolStripMenuItem__Security_Del.Name = "ToolStripMenuItem__Security_Del";
			ToolStripMenuItem__Security_Del.Size = new Size(94, 22);
			ToolStripMenuItem__Security_Del.Text = "删除";
			ToolStripMenuItem__Security_Del.Click += ToolStripMenuItem__Security_Del_Click;
			// 
			// tabPage_Update_Action
			// 
			tabPage_Update_Action.Controls.Add(groupBox_Action_IP_Change_PlaySound);
			tabPage_Update_Action.Controls.Add(checkBox_Action_IP_Change_Popup);
			tabPage_Update_Action.Controls.Add(numericUpDown_Action_Timeout);
			tabPage_Update_Action.Controls.Add(label_Action_Timeout);
			tabPage_Update_Action.Controls.Add(groupBox_Action_Set_DNS_Server);
			tabPage_Update_Action.Controls.Add(checkBox_Action_DNS_Lookup_First);
			tabPage_Update_Action.Controls.Add(numericUpDown_Action_AutoAction_Interval);
			tabPage_Update_Action.Controls.Add(checkBox_Action_AutoAction_Interval);
			tabPage_Update_Action.Controls.Add(checkBox_Action_UpdateIP);
			tabPage_Update_Action.Location = new Point(4, 26);
			tabPage_Update_Action.Margin = new Padding(3, 2, 3, 2);
			tabPage_Update_Action.Name = "tabPage_Update_Action";
			tabPage_Update_Action.Padding = new Padding(3, 2, 3, 2);
			tabPage_Update_Action.Size = new Size(532, 266);
			tabPage_Update_Action.TabIndex = 3;
			tabPage_Update_Action.Text = "更新操作";
			tabPage_Update_Action.UseVisualStyleBackColor = true;
			// 
			// groupBox_Action_IP_Change_PlaySound
			// 
			groupBox_Action_IP_Change_PlaySound.Controls.Add(button_Action_IP_Change_StopSound);
			groupBox_Action_IP_Change_PlaySound.Controls.Add(button_Action_IP_Change_PlaySound);
			groupBox_Action_IP_Change_PlaySound.Controls.Add(textBox_Action_IP_Change_PlaySound);
			groupBox_Action_IP_Change_PlaySound.Controls.Add(checkBox_Action_IP_Change_PlaySound);
			groupBox_Action_IP_Change_PlaySound.Location = new Point(6, 216);
			groupBox_Action_IP_Change_PlaySound.Name = "groupBox_Action_IP_Change_PlaySound";
			groupBox_Action_IP_Change_PlaySound.Size = new Size(520, 49);
			groupBox_Action_IP_Change_PlaySound.TabIndex = 412;
			groupBox_Action_IP_Change_PlaySound.TabStop = false;
			// 
			// button_Action_IP_Change_StopSound
			// 
			button_Action_IP_Change_StopSound.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			button_Action_IP_Change_StopSound.Location = new Point(414, 20);
			button_Action_IP_Change_StopSound.Name = "button_Action_IP_Change_StopSound";
			button_Action_IP_Change_StopSound.Size = new Size(100, 23);
			button_Action_IP_Change_StopSound.TabIndex = 415;
			button_Action_IP_Change_StopSound.Text = "停止播放";
			button_Action_IP_Change_StopSound.UseVisualStyleBackColor = true;
			button_Action_IP_Change_StopSound.Click += button_Action_IP_Change_StopSound_Click;
			// 
			// button_Action_IP_Change_PlaySound
			// 
			button_Action_IP_Change_PlaySound.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			button_Action_IP_Change_PlaySound.Location = new Point(376, 20);
			button_Action_IP_Change_PlaySound.Name = "button_Action_IP_Change_PlaySound";
			button_Action_IP_Change_PlaySound.Size = new Size(32, 23);
			button_Action_IP_Change_PlaySound.TabIndex = 414;
			button_Action_IP_Change_PlaySound.Text = "...";
			button_Action_IP_Change_PlaySound.UseVisualStyleBackColor = true;
			button_Action_IP_Change_PlaySound.Click += button_Action_IP_Change_PlaySound_Click;
			// 
			// textBox_Action_IP_Change_PlaySound
			// 
			textBox_Action_IP_Change_PlaySound.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			textBox_Action_IP_Change_PlaySound.Location = new Point(6, 22);
			textBox_Action_IP_Change_PlaySound.Name = "textBox_Action_IP_Change_PlaySound";
			textBox_Action_IP_Change_PlaySound.ReadOnly = true;
			textBox_Action_IP_Change_PlaySound.Size = new Size(364, 21);
			textBox_Action_IP_Change_PlaySound.TabIndex = 413;
			textBox_Action_IP_Change_PlaySound.TextChanged += textBox_Action_IP_Change_PlaySound_TextChanged;
			// 
			// checkBox_Action_IP_Change_PlaySound
			// 
			checkBox_Action_IP_Change_PlaySound.AutoSize = true;
			checkBox_Action_IP_Change_PlaySound.Location = new Point(6, 0);
			checkBox_Action_IP_Change_PlaySound.Name = "checkBox_Action_IP_Change_PlaySound";
			checkBox_Action_IP_Change_PlaySound.Size = new Size(132, 16);
			checkBox_Action_IP_Change_PlaySound.TabIndex = 411;
			checkBox_Action_IP_Change_PlaySound.Text = "IP变动时，播放音乐";
			checkBox_Action_IP_Change_PlaySound.UseVisualStyleBackColor = true;
			checkBox_Action_IP_Change_PlaySound.CheckedChanged += checkBox_Action_IP_Change_PlaySound_CheckedChanged;
			// 
			// checkBox_Action_IP_Change_Popup
			// 
			checkBox_Action_IP_Change_Popup.AutoSize = true;
			checkBox_Action_IP_Change_Popup.Location = new Point(6, 194);
			checkBox_Action_IP_Change_Popup.Name = "checkBox_Action_IP_Change_Popup";
			checkBox_Action_IP_Change_Popup.Size = new Size(156, 16);
			checkBox_Action_IP_Change_Popup.TabIndex = 410;
			checkBox_Action_IP_Change_Popup.Text = "IP变动时，弹出提示窗口";
			checkBox_Action_IP_Change_Popup.UseVisualStyleBackColor = true;
			checkBox_Action_IP_Change_Popup.CheckedChanged += checkBox_Action_IP_Change_Popup_CheckedChanged;
			// 
			// numericUpDown_Action_Timeout
			// 
			numericUpDown_Action_Timeout.Location = new Point(257, 171);
			numericUpDown_Action_Timeout.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
			numericUpDown_Action_Timeout.Name = "numericUpDown_Action_Timeout";
			numericUpDown_Action_Timeout.Size = new Size(60, 21);
			numericUpDown_Action_Timeout.TabIndex = 409;
			numericUpDown_Action_Timeout.TextAlign = HorizontalAlignment.Center;
			numericUpDown_Action_Timeout.Value = new decimal(new int[] { 15, 0, 0, 0 });
			numericUpDown_Action_Timeout.ValueChanged += numericUpDown_Action_Timeout_ValueChanged;
			// 
			// label_Action_Timeout
			// 
			label_Action_Timeout.AutoSize = true;
			label_Action_Timeout.Location = new Point(6, 173);
			label_Action_Timeout.Name = "label_Action_Timeout";
			label_Action_Timeout.Size = new Size(245, 12);
			label_Action_Timeout.TabIndex = 408;
			label_Action_Timeout.Text = "自动更新超时（单位：秒。0 = 无限等待）：";
			// 
			// groupBox_Action_Set_DNS_Server
			// 
			groupBox_Action_Set_DNS_Server.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			groupBox_Action_Set_DNS_Server.Controls.Add(textBox_Action_Custom_DNS_List);
			groupBox_Action_Set_DNS_Server.Controls.Add(checkBox_Action_Use_Custom_DNS);
			groupBox_Action_Set_DNS_Server.Location = new Point(6, 71);
			groupBox_Action_Set_DNS_Server.Name = "groupBox_Action_Set_DNS_Server";
			groupBox_Action_Set_DNS_Server.Size = new Size(520, 94);
			groupBox_Action_Set_DNS_Server.TabIndex = 406;
			groupBox_Action_Set_DNS_Server.TabStop = false;
			// 
			// textBox_Action_Custom_DNS_List
			// 
			textBox_Action_Custom_DNS_List.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			textBox_Action_Custom_DNS_List.Location = new Point(3, 20);
			textBox_Action_Custom_DNS_List.Multiline = true;
			textBox_Action_Custom_DNS_List.Name = "textBox_Action_Custom_DNS_List";
			textBox_Action_Custom_DNS_List.ScrollBars = ScrollBars.Both;
			textBox_Action_Custom_DNS_List.Size = new Size(514, 71);
			textBox_Action_Custom_DNS_List.TabIndex = 407;
			textBox_Action_Custom_DNS_List.Text = "8.8.8.8\r\n8.8.4.4\r\n114.114.114.114\r\n//2001:4860:4860::8888\r\n//2001:4860:4860::8844";
			textBox_Action_Custom_DNS_List.TextChanged += textBox_Action_Custom_DNS_List_TextChanged;
			// 
			// checkBox_Action_Use_Custom_DNS
			// 
			checkBox_Action_Use_Custom_DNS.AutoSize = true;
			checkBox_Action_Use_Custom_DNS.Checked = true;
			checkBox_Action_Use_Custom_DNS.CheckState = CheckState.Checked;
			checkBox_Action_Use_Custom_DNS.Location = new Point(6, 0);
			checkBox_Action_Use_Custom_DNS.Name = "checkBox_Action_Use_Custom_DNS";
			checkBox_Action_Use_Custom_DNS.Size = new Size(402, 16);
			checkBox_Action_Use_Custom_DNS.TabIndex = 405;
			checkBox_Action_Use_Custom_DNS.Text = "设定解析域名的DNS服务器（一行一个。//表示注释。\"\"表示系统默认）";
			checkBox_Action_Use_Custom_DNS.UseVisualStyleBackColor = true;
			checkBox_Action_Use_Custom_DNS.CheckedChanged += checkBox_Action_Use_Custom_DNS_CheckedChanged;
			// 
			// checkBox_Action_DNS_Lookup_First
			// 
			checkBox_Action_DNS_Lookup_First.AutoSize = true;
			checkBox_Action_DNS_Lookup_First.Checked = true;
			checkBox_Action_DNS_Lookup_First.CheckState = CheckState.Checked;
			checkBox_Action_DNS_Lookup_First.Location = new Point(6, 49);
			checkBox_Action_DNS_Lookup_First.Name = "checkBox_Action_DNS_Lookup_First";
			checkBox_Action_DNS_Lookup_First.Size = new Size(228, 16);
			checkBox_Action_DNS_Lookup_First.TabIndex = 404;
			checkBox_Action_DNS_Lookup_First.Text = "IP变动时，才执行更新（先解析域名）";
			checkBox_Action_DNS_Lookup_First.UseVisualStyleBackColor = true;
			checkBox_Action_DNS_Lookup_First.CheckedChanged += checkBox_Action_DNS_Lookup_First_CheckedChanged;
			// 
			// numericUpDown_Action_AutoAction_Interval
			// 
			numericUpDown_Action_AutoAction_Interval.Location = new Point(258, 26);
			numericUpDown_Action_AutoAction_Interval.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
			numericUpDown_Action_AutoAction_Interval.Name = "numericUpDown_Action_AutoAction_Interval";
			numericUpDown_Action_AutoAction_Interval.Size = new Size(60, 21);
			numericUpDown_Action_AutoAction_Interval.TabIndex = 403;
			numericUpDown_Action_AutoAction_Interval.TextAlign = HorizontalAlignment.Center;
			numericUpDown_Action_AutoAction_Interval.Value = new decimal(new int[] { 600, 0, 0, 0 });
			numericUpDown_Action_AutoAction_Interval.ValueChanged += numericUpDown_Action_AutoAction_Interval_ValueChanged;
			// 
			// checkBox_Action_AutoAction_Interval
			// 
			checkBox_Action_AutoAction_Interval.AutoSize = true;
			checkBox_Action_AutoAction_Interval.Checked = true;
			checkBox_Action_AutoAction_Interval.CheckState = CheckState.Checked;
			checkBox_Action_AutoAction_Interval.Location = new Point(6, 27);
			checkBox_Action_AutoAction_Interval.Name = "checkBox_Action_AutoAction_Interval";
			checkBox_Action_AutoAction_Interval.Size = new Size(204, 16);
			checkBox_Action_AutoAction_Interval.TabIndex = 402;
			checkBox_Action_AutoAction_Interval.Text = "自动执行操作的时间间隔（秒）：";
			checkBox_Action_AutoAction_Interval.UseVisualStyleBackColor = true;
			checkBox_Action_AutoAction_Interval.CheckedChanged += checkBox_Action_AutoAction_Interval_CheckedChanged;
			// 
			// checkBox_Action_UpdateIP
			// 
			checkBox_Action_UpdateIP.AutoSize = true;
			checkBox_Action_UpdateIP.Checked = true;
			checkBox_Action_UpdateIP.CheckState = CheckState.Checked;
			checkBox_Action_UpdateIP.Location = new Point(6, 5);
			checkBox_Action_UpdateIP.Name = "checkBox_Action_UpdateIP";
			checkBox_Action_UpdateIP.Size = new Size(96, 16);
			checkBox_Action_UpdateIP.TabIndex = 401;
			checkBox_Action_UpdateIP.Text = "更新域名的IP";
			checkBox_Action_UpdateIP.UseVisualStyleBackColor = true;
			checkBox_Action_UpdateIP.CheckedChanged += checkBox_Action_UpdateIP_CheckedChanged;
			// 
			// tabPage_Fix_hosts
			// 
			tabPage_Fix_hosts.Controls.Add(textBox_Fix_hosts__Content);
			tabPage_Fix_hosts.Controls.Add(button_Fix_hosts__Path_Browser);
			tabPage_Fix_hosts.Controls.Add(textBox_Fix_hosts__Path);
			tabPage_Fix_hosts.Controls.Add(label_Fix_hosts__Content);
			tabPage_Fix_hosts.Controls.Add(label_Fix_hosts__Path);
			tabPage_Fix_hosts.Location = new Point(4, 26);
			tabPage_Fix_hosts.Margin = new Padding(3, 2, 3, 2);
			tabPage_Fix_hosts.Name = "tabPage_Fix_hosts";
			tabPage_Fix_hosts.Padding = new Padding(3, 2, 3, 2);
			tabPage_Fix_hosts.Size = new Size(532, 266);
			tabPage_Fix_hosts.TabIndex = 4;
			tabPage_Fix_hosts.Text = "修正 hosts";
			tabPage_Fix_hosts.UseVisualStyleBackColor = true;
			// 
			// textBox_Fix_hosts__Content
			// 
			textBox_Fix_hosts__Content.HideSelection = false;
			textBox_Fix_hosts__Content.Location = new Point(6, 68);
			textBox_Fix_hosts__Content.Multiline = true;
			textBox_Fix_hosts__Content.Name = "textBox_Fix_hosts__Content";
			textBox_Fix_hosts__Content.ReadOnly = true;
			textBox_Fix_hosts__Content.ScrollBars = ScrollBars.Both;
			textBox_Fix_hosts__Content.Size = new Size(520, 197);
			textBox_Fix_hosts__Content.TabIndex = 505;
			textBox_Fix_hosts__Content.Text = "72.246.164.14\tapi.godaddy.com\r\n162.216.242.29\twww.dynu.com\r\n162.216.242.253\tapi.dynu.com\r\n172.67.74.152\tapi.ipify.org";
			// 
			// button_Fix_hosts__Path_Browser
			// 
			button_Fix_hosts__Path_Browser.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			button_Fix_hosts__Path_Browser.Location = new Point(451, 19);
			button_Fix_hosts__Path_Browser.Name = "button_Fix_hosts__Path_Browser";
			button_Fix_hosts__Path_Browser.Size = new Size(75, 23);
			button_Fix_hosts__Path_Browser.TabIndex = 503;
			button_Fix_hosts__Path_Browser.Text = "打开目录";
			button_Fix_hosts__Path_Browser.UseVisualStyleBackColor = true;
			button_Fix_hosts__Path_Browser.Click += button_Fix_hosts__Path_Browser_Click;
			// 
			// textBox_Fix_hosts__Path
			// 
			textBox_Fix_hosts__Path.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			textBox_Fix_hosts__Path.Location = new Point(6, 21);
			textBox_Fix_hosts__Path.Name = "textBox_Fix_hosts__Path";
			textBox_Fix_hosts__Path.ReadOnly = true;
			textBox_Fix_hosts__Path.Size = new Size(439, 21);
			textBox_Fix_hosts__Path.TabIndex = 502;
			// 
			// label_Fix_hosts__Content
			// 
			label_Fix_hosts__Content.AutoSize = true;
			label_Fix_hosts__Content.Location = new Point(6, 53);
			label_Fix_hosts__Content.Name = "label_Fix_hosts__Content";
			label_Fix_hosts__Content.Size = new Size(101, 12);
			label_Fix_hosts__Content.TabIndex = 504;
			label_Fix_hosts__Content.Text = "并添加以下记录：";
			// 
			// label_Fix_hosts__Path
			// 
			label_Fix_hosts__Path.AutoSize = true;
			label_Fix_hosts__Path.Location = new Point(6, 6);
			label_Fix_hosts__Path.Name = "label_Fix_hosts__Path";
			label_Fix_hosts__Path.Size = new Size(293, 12);
			label_Fix_hosts__Path.TabIndex = 501;
			label_Fix_hosts__Path.Text = "如果出现访问部分域名不正常，可以尝试修改 hosts：";
			// 
			// groupBox_Settings_Preview
			// 
			groupBox_Settings_Preview.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			groupBox_Settings_Preview.Controls.Add(label_Settings_Preview__Timeout_Val);
			groupBox_Settings_Preview.Controls.Add(label_Settings_Preview__DNS_Server_Val);
			groupBox_Settings_Preview.Controls.Add(label_Settings_Preview__DNS_Lookup_First_Val);
			groupBox_Settings_Preview.Controls.Add(label_Settings_Preview__Action_AutoUpdate_Val);
			groupBox_Settings_Preview.Controls.Add(label_Settings_Preview__Action_UpdateIP_Val);
			groupBox_Settings_Preview.Controls.Add(label_Settings_Preview__Security_Val);
			groupBox_Settings_Preview.Controls.Add(label_Settings_Preview__Set_IPv6_Val);
			groupBox_Settings_Preview.Controls.Add(label_Settings_Preview__Set_IPv4_Val);
			groupBox_Settings_Preview.Controls.Add(label_Settings_Preview__Ping_Val);
			groupBox_Settings_Preview.Controls.Add(label_Settings_Preview__Update_Type_Val);
			groupBox_Settings_Preview.Controls.Add(label_Settings_Preview__Timeout);
			groupBox_Settings_Preview.Controls.Add(label_Settings_Preview__DNS_Server);
			groupBox_Settings_Preview.Controls.Add(label_Settings_Preview__DNS_Lookup_First);
			groupBox_Settings_Preview.Controls.Add(label_Settings_Preview__Action_AutoUpdate);
			groupBox_Settings_Preview.Controls.Add(label_Settings_Preview__Action_UpdateIP);
			groupBox_Settings_Preview.Controls.Add(label_Settings_Preview__Security);
			groupBox_Settings_Preview.Controls.Add(label_Settings_Preview__Set_IPv6);
			groupBox_Settings_Preview.Controls.Add(label_Settings_Preview__Set_IPv4);
			groupBox_Settings_Preview.Controls.Add(label_Settings_Preview__Ping);
			groupBox_Settings_Preview.Controls.Add(label_Settings_Preview__Update_Type);
			groupBox_Settings_Preview.Location = new Point(558, 497);
			groupBox_Settings_Preview.Name = "groupBox_Settings_Preview";
			groupBox_Settings_Preview.Size = new Size(364, 221);
			groupBox_Settings_Preview.TabIndex = 600;
			groupBox_Settings_Preview.TabStop = false;
			groupBox_Settings_Preview.Text = "【预览设置】";
			// 
			// label_Settings_Preview__Timeout_Val
			// 
			label_Settings_Preview__Timeout_Val.AutoSize = true;
			label_Settings_Preview__Timeout_Val.Location = new Point(119, 197);
			label_Settings_Preview__Timeout_Val.Name = "label_Settings_Preview__Timeout_Val";
			label_Settings_Preview__Timeout_Val.Size = new Size(17, 12);
			label_Settings_Preview__Timeout_Val.TabIndex = 620;
			label_Settings_Preview__Timeout_Val.Text = "15";
			// 
			// label_Settings_Preview__DNS_Server_Val
			// 
			label_Settings_Preview__DNS_Server_Val.AutoSize = true;
			label_Settings_Preview__DNS_Server_Val.Location = new Point(119, 177);
			label_Settings_Preview__DNS_Server_Val.Name = "label_Settings_Preview__DNS_Server_Val";
			label_Settings_Preview__DNS_Server_Val.Size = new Size(41, 12);
			label_Settings_Preview__DNS_Server_Val.TabIndex = 618;
			label_Settings_Preview__DNS_Server_Val.Text = "自定义";
			// 
			// label_Settings_Preview__DNS_Lookup_First_Val
			// 
			label_Settings_Preview__DNS_Lookup_First_Val.AutoSize = true;
			label_Settings_Preview__DNS_Lookup_First_Val.Location = new Point(119, 157);
			label_Settings_Preview__DNS_Lookup_First_Val.Name = "label_Settings_Preview__DNS_Lookup_First_Val";
			label_Settings_Preview__DNS_Lookup_First_Val.Size = new Size(17, 12);
			label_Settings_Preview__DNS_Lookup_First_Val.TabIndex = 616;
			label_Settings_Preview__DNS_Lookup_First_Val.Text = "√";
			// 
			// label_Settings_Preview__Action_AutoUpdate_Val
			// 
			label_Settings_Preview__Action_AutoUpdate_Val.AutoSize = true;
			label_Settings_Preview__Action_AutoUpdate_Val.Location = new Point(119, 137);
			label_Settings_Preview__Action_AutoUpdate_Val.Name = "label_Settings_Preview__Action_AutoUpdate_Val";
			label_Settings_Preview__Action_AutoUpdate_Val.Size = new Size(47, 12);
			label_Settings_Preview__Action_AutoUpdate_Val.TabIndex = 614;
			label_Settings_Preview__Action_AutoUpdate_Val.Text = "每 600s";
			// 
			// label_Settings_Preview__Action_UpdateIP_Val
			// 
			label_Settings_Preview__Action_UpdateIP_Val.AutoSize = true;
			label_Settings_Preview__Action_UpdateIP_Val.Location = new Point(119, 117);
			label_Settings_Preview__Action_UpdateIP_Val.Name = "label_Settings_Preview__Action_UpdateIP_Val";
			label_Settings_Preview__Action_UpdateIP_Val.Size = new Size(17, 12);
			label_Settings_Preview__Action_UpdateIP_Val.TabIndex = 612;
			label_Settings_Preview__Action_UpdateIP_Val.Text = "√";
			// 
			// label_Settings_Preview__Security_Val
			// 
			label_Settings_Preview__Security_Val.AutoSize = true;
			label_Settings_Preview__Security_Val.Location = new Point(119, 97);
			label_Settings_Preview__Security_Val.Name = "label_Settings_Preview__Security_Val";
			label_Settings_Preview__Security_Val.Size = new Size(83, 12);
			label_Settings_Preview__Security_Val.TabIndex = 610;
			label_Settings_Preview__Security_Val.Text = "xx 个配置文件";
			// 
			// label_Settings_Preview__Set_IPv6_Val
			// 
			label_Settings_Preview__Set_IPv6_Val.AutoSize = true;
			label_Settings_Preview__Set_IPv6_Val.Location = new Point(119, 77);
			label_Settings_Preview__Set_IPv6_Val.Name = "label_Settings_Preview__Set_IPv6_Val";
			label_Settings_Preview__Set_IPv6_Val.Size = new Size(125, 12);
			label_Settings_Preview__Set_IPv6_Val.TabIndex = 608;
			label_Settings_Preview__Set_IPv6_Val.Text = "Server 接受连接的 IP";
			// 
			// label_Settings_Preview__Set_IPv4_Val
			// 
			label_Settings_Preview__Set_IPv4_Val.AutoSize = true;
			label_Settings_Preview__Set_IPv4_Val.Location = new Point(119, 57);
			label_Settings_Preview__Set_IPv4_Val.Name = "label_Settings_Preview__Set_IPv4_Val";
			label_Settings_Preview__Set_IPv4_Val.Size = new Size(125, 12);
			label_Settings_Preview__Set_IPv4_Val.TabIndex = 606;
			label_Settings_Preview__Set_IPv4_Val.Text = "Server 接受连接的 IP";
			// 
			// label_Settings_Preview__Ping_Val
			// 
			label_Settings_Preview__Ping_Val.AutoSize = true;
			label_Settings_Preview__Ping_Val.Location = new Point(119, 37);
			label_Settings_Preview__Ping_Val.Name = "label_Settings_Preview__Ping_Val";
			label_Settings_Preview__Ping_Val.Size = new Size(23, 12);
			label_Settings_Preview__Ping_Val.TabIndex = 604;
			label_Settings_Preview__Ping_Val.Text = "xxx";
			// 
			// label_Settings_Preview__Update_Type_Val
			// 
			label_Settings_Preview__Update_Type_Val.AutoSize = true;
			label_Settings_Preview__Update_Type_Val.Location = new Point(119, 17);
			label_Settings_Preview__Update_Type_Val.Name = "label_Settings_Preview__Update_Type_Val";
			label_Settings_Preview__Update_Type_Val.Size = new Size(137, 12);
			label_Settings_Preview__Update_Type_Val.TabIndex = 602;
			label_Settings_Preview__Update_Type_Val.Text = "xxx.xxxx.xxx.xxx:xxxxx";
			// 
			// label_Settings_Preview__Timeout
			// 
			label_Settings_Preview__Timeout.AutoSize = true;
			label_Settings_Preview__Timeout.Location = new Point(6, 197);
			label_Settings_Preview__Timeout.Name = "label_Settings_Preview__Timeout";
			label_Settings_Preview__Timeout.Size = new Size(83, 12);
			label_Settings_Preview__Timeout.TabIndex = 619;
			label_Settings_Preview__Timeout.Text = "更新超时(s)：";
			// 
			// label_Settings_Preview__DNS_Server
			// 
			label_Settings_Preview__DNS_Server.AutoSize = true;
			label_Settings_Preview__DNS_Server.Location = new Point(6, 177);
			label_Settings_Preview__DNS_Server.Name = "label_Settings_Preview__DNS_Server";
			label_Settings_Preview__DNS_Server.Size = new Size(77, 12);
			label_Settings_Preview__DNS_Server.TabIndex = 617;
			label_Settings_Preview__DNS_Server.Text = "DNS 服务器：";
			// 
			// label_Settings_Preview__DNS_Lookup_First
			// 
			label_Settings_Preview__DNS_Lookup_First.AutoSize = true;
			label_Settings_Preview__DNS_Lookup_First.Location = new Point(6, 157);
			label_Settings_Preview__DNS_Lookup_First.Name = "label_Settings_Preview__DNS_Lookup_First";
			label_Settings_Preview__DNS_Lookup_First.Size = new Size(77, 12);
			label_Settings_Preview__DNS_Lookup_First.TabIndex = 615;
			label_Settings_Preview__DNS_Lookup_First.Text = "先解析域名：";
			// 
			// label_Settings_Preview__Action_AutoUpdate
			// 
			label_Settings_Preview__Action_AutoUpdate.AutoSize = true;
			label_Settings_Preview__Action_AutoUpdate.Location = new Point(6, 137);
			label_Settings_Preview__Action_AutoUpdate.Name = "label_Settings_Preview__Action_AutoUpdate";
			label_Settings_Preview__Action_AutoUpdate.Size = new Size(65, 12);
			label_Settings_Preview__Action_AutoUpdate.TabIndex = 613;
			label_Settings_Preview__Action_AutoUpdate.Text = "自动更新：";
			// 
			// label_Settings_Preview__Action_UpdateIP
			// 
			label_Settings_Preview__Action_UpdateIP.AutoSize = true;
			label_Settings_Preview__Action_UpdateIP.Location = new Point(6, 117);
			label_Settings_Preview__Action_UpdateIP.Name = "label_Settings_Preview__Action_UpdateIP";
			label_Settings_Preview__Action_UpdateIP.Size = new Size(83, 12);
			label_Settings_Preview__Action_UpdateIP.TabIndex = 611;
			label_Settings_Preview__Action_UpdateIP.Text = "更新域名 IP：";
			// 
			// label_Settings_Preview__Security
			// 
			label_Settings_Preview__Security.AutoSize = true;
			label_Settings_Preview__Security.Location = new Point(6, 97);
			label_Settings_Preview__Security.Name = "label_Settings_Preview__Security";
			label_Settings_Preview__Security.Size = new Size(65, 12);
			label_Settings_Preview__Security.TabIndex = 609;
			label_Settings_Preview__Security.Text = "安全设置：";
			// 
			// label_Settings_Preview__Set_IPv6
			// 
			label_Settings_Preview__Set_IPv6.AutoSize = true;
			label_Settings_Preview__Set_IPv6.Location = new Point(6, 77);
			label_Settings_Preview__Set_IPv6.Name = "label_Settings_Preview__Set_IPv6";
			label_Settings_Preview__Set_IPv6.Size = new Size(65, 12);
			label_Settings_Preview__Set_IPv6.TabIndex = 607;
			label_Settings_Preview__Set_IPv6.Text = "设置IPv6：";
			// 
			// label_Settings_Preview__Set_IPv4
			// 
			label_Settings_Preview__Set_IPv4.AutoSize = true;
			label_Settings_Preview__Set_IPv4.Location = new Point(6, 57);
			label_Settings_Preview__Set_IPv4.Name = "label_Settings_Preview__Set_IPv4";
			label_Settings_Preview__Set_IPv4.Size = new Size(65, 12);
			label_Settings_Preview__Set_IPv4.TabIndex = 605;
			label_Settings_Preview__Set_IPv4.Text = "设置IPv4：";
			// 
			// label_Settings_Preview__Ping
			// 
			label_Settings_Preview__Ping.AutoSize = true;
			label_Settings_Preview__Ping.Location = new Point(6, 37);
			label_Settings_Preview__Ping.Name = "label_Settings_Preview__Ping";
			label_Settings_Preview__Ping.Size = new Size(77, 12);
			label_Settings_Preview__Ping.TabIndex = 603;
			label_Settings_Preview__Ping.Text = "Ping（ms）：";
			// 
			// label_Settings_Preview__Update_Type
			// 
			label_Settings_Preview__Update_Type.AutoSize = true;
			label_Settings_Preview__Update_Type.Location = new Point(6, 17);
			label_Settings_Preview__Update_Type.Name = "label_Settings_Preview__Update_Type";
			label_Settings_Preview__Update_Type.Size = new Size(65, 12);
			label_Settings_Preview__Update_Type.TabIndex = 601;
			label_Settings_Preview__Update_Type.Text = "更新方式：";
			// 
			// linkLabel_WebSite
			// 
			linkLabel_WebSite.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			linkLabel_WebSite.AutoSize = true;
			linkLabel_WebSite.Location = new Point(601, 760);
			linkLabel_WebSite.Name = "linkLabel_WebSite";
			linkLabel_WebSite.Size = new Size(29, 12);
			linkLabel_WebSite.TabIndex = 701;
			linkLabel_WebSite.TabStop = true;
			linkLabel_WebSite.Text = "官网";
			linkLabel_WebSite.LinkClicked += linkLabel_WebSite_LinkClicked;
			// 
			// linkLabel_Github
			// 
			linkLabel_Github.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			linkLabel_Github.AutoSize = true;
			linkLabel_Github.Location = new Point(554, 760);
			linkLabel_Github.Name = "linkLabel_Github";
			linkLabel_Github.Size = new Size(41, 12);
			linkLabel_Github.TabIndex = 700;
			linkLabel_Github.TabStop = true;
			linkLabel_Github.Text = "github";
			linkLabel_Github.LinkClicked += linkLabel_Github_LinkClicked;
			// 
			// button_Update
			// 
			button_Update.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			button_Update.Location = new Point(822, 746);
			button_Update.Name = "button_Update";
			button_Update.Size = new Size(100, 23);
			button_Update.TabIndex = 702;
			button_Update.Text = "执行更新操作";
			button_Update.UseVisualStyleBackColor = true;
			button_Update.Click += button_Update_Click;
			// 
			// timer_Save_Config
			// 
			timer_Save_Config.Enabled = true;
			timer_Save_Config.Interval = 5000;
			timer_Save_Config.Tick += timer_Save_Config_Tick;
			// 
			// timer_Update
			// 
			timer_Update.Enabled = true;
			timer_Update.Interval = 1000;
			timer_Update.Tick += timer_Update_Tick;
			// 
			// timer_Ping
			// 
			timer_Ping.Enabled = true;
			timer_Ping.Interval = 1000;
			timer_Ping.Tick += timer_Ping_Tick;
			// 
			// timer_Save_Log
			// 
			timer_Save_Log.Enabled = true;
			timer_Save_Log.Interval = 3000;
			timer_Save_Log.Tick += timer_Save_Log_Tick;
			// 
			// frm_MainForm
			// 
			AutoScaleDimensions = new SizeF(6F, 12F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(934, 781);
			Controls.Add(button_Update);
			Controls.Add(linkLabel_Github);
			Controls.Add(linkLabel_WebSite);
			Controls.Add(groupBox_Settings_Preview);
			Controls.Add(tabControl_Main);
			Controls.Add(splitContainer_Main);
			Font = new Font("新宋体", 9F);
			Margin = new Padding(3, 2, 3, 2);
			Name = "frm_MainForm";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "AcgDev DDNS Tool v0.13";
			FormClosing += frm_MainForm_FormClosing;
			Load += frm_MainForm_Load;
			groupBox_Domains.ResumeLayout(false);
			groupBox_Domains.PerformLayout();
			contextMenuStrip_Domains.ResumeLayout(false);
			toolStrip_Domains.ResumeLayout(false);
			toolStrip_Domains.PerformLayout();
			splitContainer_Main.Panel1.ResumeLayout(false);
			splitContainer_Main.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)splitContainer_Main).EndInit();
			splitContainer_Main.ResumeLayout(false);
			groupBox_Logs.ResumeLayout(false);
			groupBox_Logs.PerformLayout();
			((System.ComponentModel.ISupportInitialize)numericUpDown_Logs_MaxLines).EndInit();
			contextMenuStrip_Logs.ResumeLayout(false);
			contextMenuStrip_NotifyIcon.ResumeLayout(false);
			tabControl_Main.ResumeLayout(false);
			tabPage_Update_Type.ResumeLayout(false);
			tabPage_Update_Type.PerformLayout();
			groupBox_Settings_RemoteServer.ResumeLayout(false);
			groupBox_Settings_RemoteServer.PerformLayout();
			tabPage_Set_IP.ResumeLayout(false);
			groupBox_Set_IPv6.ResumeLayout(false);
			groupBox_Set_IPv6.PerformLayout();
			groupBox_Set_IPv4.ResumeLayout(false);
			groupBox_Set_IPv4.PerformLayout();
			tabPage_Security.ResumeLayout(false);
			groupBox_Security__Property.ResumeLayout(false);
			groupBox_Security__Property.PerformLayout();
			tabControl_Security__Property.ResumeLayout(false);
			tabPage_Security__Godaddy.ResumeLayout(false);
			tabPage_Security__Godaddy.PerformLayout();
			tabPage_Security__dynv6.ResumeLayout(false);
			tabPage_Security__dynv6.PerformLayout();
			tabPage_Security__dynu.ResumeLayout(false);
			tabPage_Security__dynu.PerformLayout();
			contextMenuStrip_Security.ResumeLayout(false);
			tabPage_Update_Action.ResumeLayout(false);
			tabPage_Update_Action.PerformLayout();
			groupBox_Action_IP_Change_PlaySound.ResumeLayout(false);
			groupBox_Action_IP_Change_PlaySound.PerformLayout();
			((System.ComponentModel.ISupportInitialize)numericUpDown_Action_Timeout).EndInit();
			groupBox_Action_Set_DNS_Server.ResumeLayout(false);
			groupBox_Action_Set_DNS_Server.PerformLayout();
			((System.ComponentModel.ISupportInitialize)numericUpDown_Action_AutoAction_Interval).EndInit();
			tabPage_Fix_hosts.ResumeLayout(false);
			tabPage_Fix_hosts.PerformLayout();
			groupBox_Settings_Preview.ResumeLayout(false);
			groupBox_Settings_Preview.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private GroupBox groupBox_Domains;
		private SplitContainer splitContainer_Main;
		private ToolStrip toolStrip_Domains;
		private ToolStripButton toolStripButton_Domains_Add;
		private ToolStripButton toolStripButton_Domains_Modify;
		private ToolStripButton toolStripButton_Domains_Delete;
		private ToolStripButton toolStripButton_Domains_IPv4_Enable;
		private ToolStripButton toolStripButton_Domains_IPv6_Enable;
		private ToolStripButton toolStripButton_Domains_IPv4_Disable;
		private ToolStripButton toolStripButton_Domains_IPv6_Disable;
		private ToolStripSeparator toolStripSeparator1;
		private ToolStripButton toolStripButton_Domains_CopyText;
		private ListView listView_Domains;
		private NotifyIcon notifyIcon_Main;
		private ContextMenuStrip contextMenuStrip_NotifyIcon;
		private ToolStripMenuItem ToolStripMenuItem_Open;
		private ToolStripMenuItem ToolStripMenuItem_Exit;
		private ContextMenuStrip contextMenuStrip_Domains;
		private ToolStripMenuItem ToolStripMenuItem_Domains_Add;
		private ToolStripMenuItem ToolStripMenuItem_Domains_Modify;
		private ToolStripMenuItem ToolStripMenuItem_Domains_Delete;
		private ToolStripMenuItem ToolStripMenuItem_Domains_IPv4_Enable;
		private ToolStripMenuItem ToolStripMenuItem_Domains_IPv6_Enable;
		private ToolStripMenuItem ToolStripMenuItem_Domains_IPv4_Disable;
		private ToolStripMenuItem ToolStripMenuItem_Domains_IPv6_Disable;
		private ToolStripSeparator toolStripMenuItem1;
		private ToolStripMenuItem ToolStripMenuItem_Domains_CopyText;
		private ColumnHeader columnHeader_Domains_Domain;
		private ColumnHeader columnHeader_Domains_Type;
		private ColumnHeader columnHeader_Domains_Profile;
		private ColumnHeader columnHeader_Domains_IPv4;
		private ColumnHeader columnHeader_Domains_IPv6;
		private ColumnHeader columnHeader_Domains_Status;
		private GroupBox groupBox_Logs;
		private ListView listView_Logs;
		private ColumnHeader columnHeader_Logs_Time;
		private ColumnHeader columnHeader_Logs_Log;
		private ContextMenuStrip contextMenuStrip_Logs;
		private ToolStripMenuItem ToolStripMenuItem_Logs_Copy;
		private ToolStripMenuItem ToolStripMenuItem_Logs_Delete;
		private ToolStripSeparator toolStripMenuItem2;
		private ToolStripMenuItem ToolStripMenuItem_Logs_SelectAll;
		private Label label_Logs_MaxLines;
		private CheckBox checkBox_Logs__Save_To_File;
		private NumericUpDown numericUpDown_Logs_MaxLines;
		private TabControl tabControl_Main;
		private TabPage tabPage_Update_Type;
		private TabPage tabPage_Set_IP;
		private TabPage tabPage_Security;
		private TabPage tabPage_Update_Action;
		private TabPage tabPage_Fix_hosts;
		private RadioButton radioButton_Settings_Method__Remote;
		private RadioButton radioButton_Settings_Method__Local;
		private GroupBox groupBox_Settings_RemoteServer;
		private TextBox textBox_Settings_RemoteServer__Addr;
		private Label label_Settings_RemoteServer__Addr;
		private TextBox textBox_Settings_RemoteServer__User;
		private Label label_Settings_RemoteServer__User;
		private Label label_Settings_RemoteServer__Pwd;
		private TextBox textBox_Settings_RemoteServer__Pwd;
		private CheckBox checkBox_Settings_RemoteServer__Pwd;
		private CheckBox checkBox_Settings_RemoteServer__Ping;
		private TextBox textBox_Settings_RemoteServer__Ping;
		private Label label_Settings_RemoteServer__Ping;
		private GroupBox groupBox_Set_IPv4;
		private ComboBox comboBox_Settings_IPv4__From_URL;
		private RadioButton radioButton_Settings_IPv4__From_URL;
		private TextBox textBox_Settings_IPv4;
		private RadioButton radioButton_Settings_IPv4__Accept_IP;
		private RadioButton radioButton_Settings_IPv4__Manual;
		private GroupBox groupBox_Set_IPv6;
		private RadioButton radioButton_Settings_IPv6__From_URL;
		private ComboBox comboBox_Settings_IPv6__From_URL;
		private TextBox textBox_Settings_IPv6;
		private RadioButton radioButton_Settings_IPv6__Accept_IP;
		private RadioButton radioButton_Settings_IPv6__Manual;
		private Button button_Security_Del;
		private Button button_Security_Add;
		private ListView listView_Security;
		private ColumnHeader columnHeader_Security;
		private GroupBox groupBox_Security__Property;
		private TextBox textBox_Security__Property__Name;
		private Label label_Security__Property__Name;
		private TabControl tabControl_Security__Property;
		private TabPage tabPage_Security__Godaddy;
		private LinkLabel linkLabel_Security_Godaddy__API;
		private TabPage tabPage_Security__dynv6;
		private TabPage tabPage_Security__dynu;
		private TextBox textBox_Security_Godaddy__Key;
		private Label label_Security_Godaddy__Key;
		private CheckBox checkBox_Security_Godaddy__Secret;
		private CheckBox checkBox_Security_Godaddy__Key;
		private TextBox textBox_Security_Godaddy__Secret;
		private Label label_Security_Godaddy__Secret;
		private LinkLabel linkLabel_Security_dynv6__API;
		private CheckBox checkBox_Security_dynv6__token;
		private TextBox textBox_Security_dynv6__token;
		private Label label_Security_dynv6__token;
		private LinkLabel linkLabel_Security_dynu__API;
		private CheckBox checkBox_Security_dynu__API_Key;
		private TextBox textBox_Security_dynu__API_Key;
		private Label label_Security_dynu__API_Key;
		private CheckBox checkBox_Security__Save_To_Config;
		private CheckBox checkBox_Action_UpdateIP;
		private CheckBox checkBox_Action_AutoAction_Interval;
		private NumericUpDown numericUpDown_Action_AutoAction_Interval;
		private GroupBox groupBox_Action_Set_DNS_Server;
		private CheckBox checkBox_Action_Use_Custom_DNS;
		private CheckBox checkBox_Action_DNS_Lookup_First;
		private NumericUpDown numericUpDown_Action_Timeout;
		private Label label_Action_Timeout;
		private CheckBox checkBox_Action_IP_Change_Popup;
		private GroupBox groupBox_Action_IP_Change_PlaySound;
		private Button button_Action_IP_Change_StopSound;
		private Button button_Action_IP_Change_PlaySound;
		private TextBox textBox_Action_IP_Change_PlaySound;
		private CheckBox checkBox_Action_IP_Change_PlaySound;
		private GroupBox groupBox_Settings_Preview;
		private Label label_Settings_Preview__Update_Type;
		private Label label_Settings_Preview__Update_Type_Val;
		private Label label_Settings_Preview__Ping_Val;
		private Label label_Settings_Preview__Ping;
		private Label label_Settings_Preview__Set_IPv4;
		private Label label_Settings_Preview__Set_IPv6_Val;
		private Label label_Settings_Preview__Set_IPv4_Val;
		private Label label_Settings_Preview__Set_IPv6;
		private Label label_Settings_Preview__Security;
		private Label label_Settings_Preview__Security_Val;
		private Label label_Settings_Preview__Action_UpdateIP_Val;
		private Label label_Settings_Preview__Action_UpdateIP;
		private Label label_Settings_Preview__Action_AutoUpdate_Val;
		private Label label_Settings_Preview__Action_AutoUpdate;
		private Label label_Settings_Preview__Timeout_Val;
		private Label label_Settings_Preview__DNS_Server_Val;
		private Label label_Settings_Preview__DNS_Lookup_First_Val;
		private Label label_Settings_Preview__Timeout;
		private Label label_Settings_Preview__DNS_Server;
		private Label label_Settings_Preview__DNS_Lookup_First;
		private LinkLabel linkLabel_WebSite;
		private LinkLabel linkLabel_Github;
		private Button button_Update;
		private Button button_Fix_hosts__Path_Browser;
		private TextBox textBox_Fix_hosts__Path;
		private Label label_Fix_hosts__Content;
		private Label label_Fix_hosts__Path;
		private TextBox textBox_Fix_hosts__Content;
		private System.Windows.Forms.Timer timer_Save_Config;
		private System.Windows.Forms.Timer timer_Update;
		private System.Windows.Forms.Timer timer_Ping;
		private ContextMenuStrip contextMenuStrip_Security;
		private ToolStripMenuItem ToolStripMenuItem__Security_Add;
		private ToolStripMenuItem ToolStripMenuItem__Security_Del;
		internal TextBox textBox_Action_Custom_DNS_List;
		private System.Windows.Forms.Timer timer_Save_Log;
		private ToolStripMenuItem ToolStripMenuItem_Languages;
		private ToolStripSeparator toolStripMenuItem3;
		private ToolStripMenuItem toolStripMenuItem_Languages_CurrentCulture;
		private ToolStripSeparator toolStripSeparator2;
	}
}
