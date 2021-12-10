#region License
/*
    Sotware Antrian Tobasa
    Copyright (C) 2021  Jefri Sibarani

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/
#endregion

namespace Tobasa
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageGeneral = new System.Windows.Forms.TabPage();
            this.tbReordStatus = new System.Windows.Forms.TextBox();
            this.btnDeleteData = new System.Windows.Forms.Button();
            this.btnLast = new System.Windows.Forms.Button();
            this.btnAddData = new System.Windows.Forms.Button();
            this.btnFirst = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.tabTable = new System.Windows.Forms.TabControl();
            this.tabIpAccessList = new System.Windows.Forms.TabPage();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.gridIpAccess = new System.Windows.Forms.DataGridView();
            this.tabStation = new System.Windows.Forms.TabPage();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.gridStations = new System.Windows.Forms.DataGridView();
            this.tabLogin = new System.Windows.Forms.TabPage();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.gridLogins = new System.Windows.Forms.DataGridView();
            this.tabPost = new System.Windows.Forms.TabPage();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.gridPosts = new System.Windows.Forms.DataGridView();
            this.tabRunText = new System.Windows.Forms.TabPage();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.gridRunText = new System.Windows.Forms.DataGridView();
            this.btnPre = new System.Windows.Forms.Button();
            this.tabPageEncrypt = new System.Windows.Forms.TabPage();
            this.gbEnryptPass = new System.Windows.Forms.GroupBox();
            this.txtECSPass = new System.Windows.Forms.TextBox();
            this.btnEncryptPassword = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCSPass = new System.Windows.Forms.TextBox();
            this.gbPassHash = new System.Windows.Forms.GroupBox();
            this.txtEResult = new System.Windows.Forms.TextBox();
            this.btnCreatePasswordHash = new System.Windows.Forms.Button();
            this.txtEName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtEPass = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gbEnryptTool = new System.Windows.Forms.GroupBox();
            this.tbInput = new System.Windows.Forms.TextBox();
            this.btnEncrypt = new System.Windows.Forms.Button();
            this.tbKey = new System.Windows.Forms.TextBox();
            this.tbOutput = new System.Windows.Forms.RichTextBox();
            this.btnDecrypt = new System.Windows.Forms.Button();
            this.chkSalt = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.tbSalt = new System.Windows.Forms.TextBox();
            this.rbSHA = new System.Windows.Forms.RadioButton();
            this.rbBF = new System.Windows.Forms.RadioButton();
            this.tabPageDiag = new System.Windows.Forms.TabPage();
            this.chkSend4096 = new System.Windows.Forms.RadioButton();
            this.chkSend1536 = new System.Windows.Forms.RadioButton();
            this.chkSend2048 = new System.Windows.Forms.RadioButton();
            this.chkSend1024 = new System.Windows.Forms.RadioButton();
            this.btnSendXXXData = new System.Windows.Forms.Button();
            this.tbLog = new System.Windows.Forms.TextBox();
            this.cbCmd = new System.Windows.Forms.ComboBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tbServerMessages = new System.Windows.Forms.TextBox();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOption = new System.Windows.Forms.ToolStripMenuItem();
            this.menuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPageGeneral.SuspendLayout();
            this.tabTable.SuspendLayout();
            this.tabIpAccessList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridIpAccess)).BeginInit();
            this.tabStation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridStations)).BeginInit();
            this.tabLogin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridLogins)).BeginInit();
            this.tabPost.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridPosts)).BeginInit();
            this.tabRunText.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridRunText)).BeginInit();
            this.tabPageEncrypt.SuspendLayout();
            this.gbEnryptPass.SuspendLayout();
            this.gbPassHash.SuspendLayout();
            this.gbEnryptTool.SuspendLayout();
            this.tabPageDiag.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.34184F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(606, 511);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblStatus);
            this.panel1.Controls.Add(this.tabControl);
            this.panel1.Controls.Add(this.menuStrip);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(600, 505);
            this.panel1.TabIndex = 0;
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblStatus.Location = new System.Drawing.Point(15, 486);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(77, 13);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "Connected to :";
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabPageGeneral);
            this.tabControl.Controls.Add(this.tabPageEncrypt);
            this.tabControl.Controls.Add(this.tabPageDiag);
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Location = new System.Drawing.Point(9, 28);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(582, 453);
            this.tabControl.TabIndex = 0;
            // 
            // tabPageGeneral
            // 
            this.tabPageGeneral.Controls.Add(this.tbReordStatus);
            this.tabPageGeneral.Controls.Add(this.btnDeleteData);
            this.tabPageGeneral.Controls.Add(this.btnLast);
            this.tabPageGeneral.Controls.Add(this.btnAddData);
            this.tabPageGeneral.Controls.Add(this.btnFirst);
            this.tabPageGeneral.Controls.Add(this.btnNext);
            this.tabPageGeneral.Controls.Add(this.tabTable);
            this.tabPageGeneral.Controls.Add(this.btnPre);
            this.tabPageGeneral.Location = new System.Drawing.Point(4, 22);
            this.tabPageGeneral.Name = "tabPageGeneral";
            this.tabPageGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageGeneral.Size = new System.Drawing.Size(574, 427);
            this.tabPageGeneral.TabIndex = 0;
            this.tabPageGeneral.Text = "General";
            this.tabPageGeneral.UseVisualStyleBackColor = true;
            // 
            // tbReordStatus
            // 
            this.tbReordStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbReordStatus.Location = new System.Drawing.Point(82, 397);
            this.tbReordStatus.Margin = new System.Windows.Forms.Padding(0);
            this.tbReordStatus.Name = "tbReordStatus";
            this.tbReordStatus.Size = new System.Drawing.Size(87, 20);
            this.tbReordStatus.TabIndex = 11;
            this.tbReordStatus.Text = "0-0 of 0";
            this.tbReordStatus.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnDeleteData
            // 
            this.btnDeleteData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDeleteData.Enabled = false;
            this.btnDeleteData.Location = new System.Drawing.Point(332, 394);
            this.btnDeleteData.Name = "btnDeleteData";
            this.btnDeleteData.Size = new System.Drawing.Size(75, 24);
            this.btnDeleteData.TabIndex = 4;
            this.btnDeleteData.Text = "Delete Data";
            this.btnDeleteData.UseVisualStyleBackColor = true;
            this.btnDeleteData.Click += new System.EventHandler(this.OnBtnDeleteData);
            // 
            // btnLast
            // 
            this.btnLast.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLast.Location = new System.Drawing.Point(213, 394);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(32, 24);
            this.btnLast.TabIndex = 10;
            this.btnLast.Text = ">|";
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new System.EventHandler(this.OnBtnLastPage);
            // 
            // btnAddData
            // 
            this.btnAddData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddData.Location = new System.Drawing.Point(251, 394);
            this.btnAddData.Name = "btnAddData";
            this.btnAddData.Size = new System.Drawing.Size(75, 24);
            this.btnAddData.TabIndex = 3;
            this.btnAddData.Text = "Add Data";
            this.btnAddData.UseVisualStyleBackColor = true;
            this.btnAddData.Click += new System.EventHandler(this.OnBtnAddData);
            // 
            // btnFirst
            // 
            this.btnFirst.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFirst.Location = new System.Drawing.Point(8, 394);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(32, 24);
            this.btnFirst.TabIndex = 9;
            this.btnFirst.Text = "|<";
            this.btnFirst.UseVisualStyleBackColor = true;
            this.btnFirst.Click += new System.EventHandler(this.OnBtnFirstPage);
            // 
            // btnNext
            // 
            this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNext.Location = new System.Drawing.Point(175, 394);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(32, 24);
            this.btnNext.TabIndex = 8;
            this.btnNext.Text = ">";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.OnBtnNextPage);
            // 
            // tabTable
            // 
            this.tabTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabTable.Controls.Add(this.tabIpAccessList);
            this.tabTable.Controls.Add(this.tabStation);
            this.tabTable.Controls.Add(this.tabLogin);
            this.tabTable.Controls.Add(this.tabPost);
            this.tabTable.Controls.Add(this.tabRunText);
            this.tabTable.Location = new System.Drawing.Point(8, 9);
            this.tabTable.Name = "tabTable";
            this.tabTable.SelectedIndex = 0;
            this.tabTable.Size = new System.Drawing.Size(560, 379);
            this.tabTable.TabIndex = 4;
            this.tabTable.Selected += new System.Windows.Forms.TabControlEventHandler(this.OnTabPageSelected);
            // 
            // tabIpAccessList
            // 
            this.tabIpAccessList.Controls.Add(this.textBox3);
            this.tabIpAccessList.Controls.Add(this.gridIpAccess);
            this.tabIpAccessList.Location = new System.Drawing.Point(4, 22);
            this.tabIpAccessList.Name = "tabIpAccessList";
            this.tabIpAccessList.Padding = new System.Windows.Forms.Padding(3);
            this.tabIpAccessList.Size = new System.Drawing.Size(552, 353);
            this.tabIpAccessList.TabIndex = 0;
            this.tabIpAccessList.Text = "IP access list";
            this.tabIpAccessList.UseVisualStyleBackColor = true;
            // 
            // textBox3
            // 
            this.textBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox3.BackColor = System.Drawing.SystemColors.Control;
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox3.Location = new System.Drawing.Point(9, 280);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(532, 70);
            this.textBox3.TabIndex = 6;
            this.textBox3.Text = "Daftar asal koneksi yang dapat mengaskes server antrian\r\nMasukkan nomor IP Addres" +
    "s (v.4), lalu tentukan hak aksesnya";
            // 
            // gridIpAccess
            // 
            this.gridIpAccess.AllowUserToAddRows = false;
            dataGridViewCellStyle21.BackColor = System.Drawing.Color.MistyRose;
            this.gridIpAccess.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle21;
            this.gridIpAccess.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridIpAccess.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridIpAccess.Location = new System.Drawing.Point(8, 6);
            this.gridIpAccess.MultiSelect = false;
            this.gridIpAccess.Name = "gridIpAccess";
            this.gridIpAccess.ReadOnly = true;
            this.gridIpAccess.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridIpAccess.Size = new System.Drawing.Size(533, 268);
            this.gridIpAccess.TabIndex = 0;
            this.gridIpAccess.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.OnGgridIpAccessListCellDoubleClick);
            this.gridIpAccess.SelectionChanged += new System.EventHandler(this.OnGridIpAccessSelectionChanged);
            // 
            // tabStation
            // 
            this.tabStation.Controls.Add(this.textBox2);
            this.tabStation.Controls.Add(this.gridStations);
            this.tabStation.Location = new System.Drawing.Point(4, 22);
            this.tabStation.Name = "tabStation";
            this.tabStation.Padding = new System.Windows.Forms.Padding(3);
            this.tabStation.Size = new System.Drawing.Size(552, 353);
            this.tabStation.TabIndex = 1;
            this.tabStation.Text = "Station";
            this.tabStation.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.BackColor = System.Drawing.SystemColors.Control;
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Location = new System.Drawing.Point(9, 280);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(532, 70);
            this.textBox2.TabIndex = 6;
            this.textBox2.Text = resources.GetString("textBox2.Text");
            // 
            // gridStations
            // 
            this.gridStations.AllowUserToAddRows = false;
            dataGridViewCellStyle22.BackColor = System.Drawing.Color.MistyRose;
            this.gridStations.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle22;
            this.gridStations.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridStations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridStations.Location = new System.Drawing.Point(8, 6);
            this.gridStations.MultiSelect = false;
            this.gridStations.Name = "gridStations";
            this.gridStations.ReadOnly = true;
            this.gridStations.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridStations.Size = new System.Drawing.Size(533, 268);
            this.gridStations.TabIndex = 2;
            this.gridStations.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.OnGgridStationsCellDoubleClick);
            // 
            // tabLogin
            // 
            this.tabLogin.Controls.Add(this.textBox1);
            this.tabLogin.Controls.Add(this.gridLogins);
            this.tabLogin.Location = new System.Drawing.Point(4, 22);
            this.tabLogin.Name = "tabLogin";
            this.tabLogin.Padding = new System.Windows.Forms.Padding(3);
            this.tabLogin.Size = new System.Drawing.Size(552, 353);
            this.tabLogin.TabIndex = 3;
            this.tabLogin.Text = "Login";
            this.tabLogin.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.textBox1.Location = new System.Drawing.Point(9, 280);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(532, 70);
            this.textBox1.TabIndex = 5;
            this.textBox1.Text = "Selain IP Address, nama Station, User Name/ Password juga diperlukan\r\nuntuk terhu" +
    "bung pada Server Antrian";
            // 
            // gridLogins
            // 
            this.gridLogins.AllowUserToAddRows = false;
            dataGridViewCellStyle23.BackColor = System.Drawing.Color.MistyRose;
            this.gridLogins.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle23;
            this.gridLogins.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridLogins.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridLogins.Location = new System.Drawing.Point(8, 6);
            this.gridLogins.MultiSelect = false;
            this.gridLogins.Name = "gridLogins";
            this.gridLogins.ReadOnly = true;
            this.gridLogins.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridLogins.Size = new System.Drawing.Size(533, 268);
            this.gridLogins.TabIndex = 2;
            this.gridLogins.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.OnGridLoginsCellDoubleClick);
            // 
            // tabPost
            // 
            this.tabPost.Controls.Add(this.textBox4);
            this.tabPost.Controls.Add(this.gridPosts);
            this.tabPost.Location = new System.Drawing.Point(4, 22);
            this.tabPost.Name = "tabPost";
            this.tabPost.Padding = new System.Windows.Forms.Padding(3);
            this.tabPost.Size = new System.Drawing.Size(552, 353);
            this.tabPost.TabIndex = 2;
            this.tabPost.Text = "Post";
            this.tabPost.UseVisualStyleBackColor = true;
            // 
            // textBox4
            // 
            this.textBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.textBox4.Location = new System.Drawing.Point(9, 280);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(532, 70);
            this.textBox4.TabIndex = 6;
            this.textBox4.Text = "Post adalah sebuah grup yang terdiri dari TICKET, CALLER, DISPLAY dan ADMIN.\r\nSet" +
    "iap bagian dari aplikasi antrian ini harus di set POST-nya\r\n";
            // 
            // gridPosts
            // 
            this.gridPosts.AllowUserToAddRows = false;
            dataGridViewCellStyle24.BackColor = System.Drawing.Color.MistyRose;
            this.gridPosts.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle24;
            this.gridPosts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridPosts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridPosts.Location = new System.Drawing.Point(8, 6);
            this.gridPosts.MultiSelect = false;
            this.gridPosts.Name = "gridPosts";
            this.gridPosts.ReadOnly = true;
            this.gridPosts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridPosts.Size = new System.Drawing.Size(533, 268);
            this.gridPosts.TabIndex = 2;
            this.gridPosts.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.OnGridPostsCellDoubleClick);
            // 
            // tabRunText
            // 
            this.tabRunText.Controls.Add(this.textBox5);
            this.tabRunText.Controls.Add(this.gridRunText);
            this.tabRunText.Location = new System.Drawing.Point(4, 22);
            this.tabRunText.Name = "tabRunText";
            this.tabRunText.Padding = new System.Windows.Forms.Padding(3);
            this.tabRunText.Size = new System.Drawing.Size(552, 353);
            this.tabRunText.TabIndex = 4;
            this.tabRunText.Text = "Display running text";
            this.tabRunText.UseVisualStyleBackColor = true;
            // 
            // textBox5
            // 
            this.textBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox5.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.textBox5.Location = new System.Drawing.Point(9, 280);
            this.textBox5.Multiline = true;
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            this.textBox5.Size = new System.Drawing.Size(532, 70);
            this.textBox5.TabIndex = 7;
            this.textBox5.Text = "Daftar text yang akan ditampilkan pada DISPLAY\r\n";
            // 
            // gridRunText
            // 
            this.gridRunText.AllowUserToAddRows = false;
            dataGridViewCellStyle25.BackColor = System.Drawing.Color.MistyRose;
            this.gridRunText.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle25;
            this.gridRunText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridRunText.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridRunText.Location = new System.Drawing.Point(8, 6);
            this.gridRunText.MultiSelect = false;
            this.gridRunText.Name = "gridRunText";
            this.gridRunText.ReadOnly = true;
            this.gridRunText.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridRunText.Size = new System.Drawing.Size(533, 268);
            this.gridRunText.TabIndex = 2;
            this.gridRunText.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.OnGridRunTextCellDoubleClick);
            // 
            // btnPre
            // 
            this.btnPre.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPre.Location = new System.Drawing.Point(46, 394);
            this.btnPre.Name = "btnPre";
            this.btnPre.Size = new System.Drawing.Size(32, 24);
            this.btnPre.TabIndex = 7;
            this.btnPre.Text = "<";
            this.btnPre.UseVisualStyleBackColor = true;
            this.btnPre.Click += new System.EventHandler(this.OnBtnPrevPage);
            // 
            // tabPageEncrypt
            // 
            this.tabPageEncrypt.Controls.Add(this.gbEnryptPass);
            this.tabPageEncrypt.Controls.Add(this.gbPassHash);
            this.tabPageEncrypt.Controls.Add(this.gbEnryptTool);
            this.tabPageEncrypt.Location = new System.Drawing.Point(4, 22);
            this.tabPageEncrypt.Name = "tabPageEncrypt";
            this.tabPageEncrypt.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageEncrypt.Size = new System.Drawing.Size(574, 427);
            this.tabPageEncrypt.TabIndex = 3;
            this.tabPageEncrypt.Text = "Encryption tool";
            this.tabPageEncrypt.UseVisualStyleBackColor = true;
            // 
            // gbEnryptPass
            // 
            this.gbEnryptPass.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbEnryptPass.Controls.Add(this.txtECSPass);
            this.gbEnryptPass.Controls.Add(this.btnEncryptPassword);
            this.gbEnryptPass.Controls.Add(this.label3);
            this.gbEnryptPass.Controls.Add(this.txtCSPass);
            this.gbEnryptPass.Location = new System.Drawing.Point(15, 108);
            this.gbEnryptPass.Name = "gbEnryptPass";
            this.gbEnryptPass.Size = new System.Drawing.Size(543, 81);
            this.gbEnryptPass.TabIndex = 2;
            this.gbEnryptPass.TabStop = false;
            this.gbEnryptPass.Text = "Encrypt password";
            // 
            // txtECSPass
            // 
            this.txtECSPass.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtECSPass.Location = new System.Drawing.Point(72, 47);
            this.txtECSPass.Name = "txtECSPass";
            this.txtECSPass.ReadOnly = true;
            this.txtECSPass.Size = new System.Drawing.Size(456, 20);
            this.txtECSPass.TabIndex = 3;
            // 
            // btnEncryptPassword
            // 
            this.btnEncryptPassword.Location = new System.Drawing.Point(9, 47);
            this.btnEncryptPassword.Name = "btnEncryptPassword";
            this.btnEncryptPassword.Size = new System.Drawing.Size(55, 23);
            this.btnEncryptPassword.TabIndex = 2;
            this.btnEncryptPassword.Text = "Encrypt";
            this.btnEncryptPassword.UseVisualStyleBackColor = true;
            this.btnEncryptPassword.Click += new System.EventHandler(this.OnCreateEncryptedPassword);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Password";
            // 
            // txtCSPass
            // 
            this.txtCSPass.Location = new System.Drawing.Point(72, 24);
            this.txtCSPass.Name = "txtCSPass";
            this.txtCSPass.Size = new System.Drawing.Size(227, 20);
            this.txtCSPass.TabIndex = 1;
            // 
            // gbPassHash
            // 
            this.gbPassHash.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbPassHash.Controls.Add(this.txtEResult);
            this.gbPassHash.Controls.Add(this.btnCreatePasswordHash);
            this.gbPassHash.Controls.Add(this.txtEName);
            this.gbPassHash.Controls.Add(this.label2);
            this.gbPassHash.Controls.Add(this.txtEPass);
            this.gbPassHash.Controls.Add(this.label1);
            this.gbPassHash.Location = new System.Drawing.Point(15, 18);
            this.gbPassHash.Name = "gbPassHash";
            this.gbPassHash.Size = new System.Drawing.Size(543, 78);
            this.gbPassHash.TabIndex = 1;
            this.gbPassHash.TabStop = false;
            this.gbPassHash.Text = "Create password hash";
            // 
            // txtEResult
            // 
            this.txtEResult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEResult.Location = new System.Drawing.Point(72, 47);
            this.txtEResult.Name = "txtEResult";
            this.txtEResult.ReadOnly = true;
            this.txtEResult.Size = new System.Drawing.Size(456, 20);
            this.txtEResult.TabIndex = 4;
            // 
            // btnCreatePasswordHash
            // 
            this.btnCreatePasswordHash.Location = new System.Drawing.Point(9, 47);
            this.btnCreatePasswordHash.Name = "btnCreatePasswordHash";
            this.btnCreatePasswordHash.Size = new System.Drawing.Size(55, 23);
            this.btnCreatePasswordHash.TabIndex = 3;
            this.btnCreatePasswordHash.Text = "Encrypt";
            this.btnCreatePasswordHash.UseVisualStyleBackColor = true;
            this.btnCreatePasswordHash.Click += new System.EventHandler(this.OnCreatePasswordHash);
            // 
            // txtEName
            // 
            this.txtEName.Location = new System.Drawing.Point(72, 24);
            this.txtEName.Name = "txtEName";
            this.txtEName.Size = new System.Drawing.Size(159, 20);
            this.txtEName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(251, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 91;
            this.label2.Text = "Password";
            // 
            // txtEPass
            // 
            this.txtEPass.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEPass.Location = new System.Drawing.Point(312, 24);
            this.txtEPass.Name = "txtEPass";
            this.txtEPass.Size = new System.Drawing.Size(216, 20);
            this.txtEPass.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 90;
            this.label1.Text = "User name";
            // 
            // gbEnryptTool
            // 
            this.gbEnryptTool.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbEnryptTool.Controls.Add(this.tbInput);
            this.gbEnryptTool.Controls.Add(this.btnEncrypt);
            this.gbEnryptTool.Controls.Add(this.tbKey);
            this.gbEnryptTool.Controls.Add(this.tbOutput);
            this.gbEnryptTool.Controls.Add(this.btnDecrypt);
            this.gbEnryptTool.Controls.Add(this.chkSalt);
            this.gbEnryptTool.Controls.Add(this.label10);
            this.gbEnryptTool.Controls.Add(this.label12);
            this.gbEnryptTool.Controls.Add(this.label11);
            this.gbEnryptTool.Controls.Add(this.tbSalt);
            this.gbEnryptTool.Controls.Add(this.rbSHA);
            this.gbEnryptTool.Controls.Add(this.rbBF);
            this.gbEnryptTool.Location = new System.Drawing.Point(15, 204);
            this.gbEnryptTool.Name = "gbEnryptTool";
            this.gbEnryptTool.Size = new System.Drawing.Size(544, 217);
            this.gbEnryptTool.TabIndex = 15;
            this.gbEnryptTool.TabStop = false;
            this.gbEnryptTool.Text = "Encryption";
            // 
            // tbInput
            // 
            this.tbInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbInput.Location = new System.Drawing.Point(72, 70);
            this.tbInput.Name = "tbInput";
            this.tbInput.Size = new System.Drawing.Size(456, 20);
            this.tbInput.TabIndex = 3;
            // 
            // btnEncrypt
            // 
            this.btnEncrypt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEncrypt.Location = new System.Drawing.Point(338, 104);
            this.btnEncrypt.Name = "btnEncrypt";
            this.btnEncrypt.Size = new System.Drawing.Size(93, 23);
            this.btnEncrypt.TabIndex = 7;
            this.btnEncrypt.Text = "Encrypt";
            this.btnEncrypt.UseVisualStyleBackColor = true;
            this.btnEncrypt.Click += new System.EventHandler(this.OnEncrypt);
            // 
            // tbKey
            // 
            this.tbKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbKey.Location = new System.Drawing.Point(72, 25);
            this.tbKey.Name = "tbKey";
            this.tbKey.Size = new System.Drawing.Size(456, 20);
            this.tbKey.TabIndex = 1;
            // 
            // tbOutput
            // 
            this.tbOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbOutput.Location = new System.Drawing.Point(14, 132);
            this.tbOutput.Name = "tbOutput";
            this.tbOutput.Size = new System.Drawing.Size(514, 76);
            this.tbOutput.TabIndex = 4;
            this.tbOutput.Text = "";
            // 
            // btnDecrypt
            // 
            this.btnDecrypt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDecrypt.Location = new System.Drawing.Point(437, 104);
            this.btnDecrypt.Name = "btnDecrypt";
            this.btnDecrypt.Size = new System.Drawing.Size(91, 23);
            this.btnDecrypt.TabIndex = 8;
            this.btnDecrypt.Text = "Decrypt";
            this.btnDecrypt.UseVisualStyleBackColor = true;
            this.btnDecrypt.Click += new System.EventHandler(this.OnDecrypt);
            // 
            // chkSalt
            // 
            this.chkSalt.AutoSize = true;
            this.chkSalt.Location = new System.Drawing.Point(151, 107);
            this.chkSalt.Name = "chkSalt";
            this.chkSalt.Size = new System.Drawing.Size(64, 17);
            this.chkSalt.TabIndex = 6;
            this.chkSalt.Text = "Use salt";
            this.chkSalt.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(16, 28);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(25, 13);
            this.label10.TabIndex = 5;
            this.label10.Text = "Key";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(16, 50);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(25, 13);
            this.label12.TabIndex = 10;
            this.label12.Text = "Salt";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(16, 73);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(30, 13);
            this.label11.TabIndex = 6;
            this.label11.Text = "Data";
            // 
            // tbSalt
            // 
            this.tbSalt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSalt.Location = new System.Drawing.Point(72, 47);
            this.tbSalt.Name = "tbSalt";
            this.tbSalt.Size = new System.Drawing.Size(456, 20);
            this.tbSalt.TabIndex = 2;
            // 
            // rbSHA
            // 
            this.rbSHA.AutoSize = true;
            this.rbSHA.Location = new System.Drawing.Point(14, 107);
            this.rbSHA.Name = "rbSHA";
            this.rbSHA.Size = new System.Drawing.Size(65, 17);
            this.rbSHA.TabIndex = 4;
            this.rbSHA.Text = "SHA256";
            this.rbSHA.UseVisualStyleBackColor = true;
            this.rbSHA.CheckedChanged += new System.EventHandler(this.OnEncryptCheck);
            // 
            // rbBF
            // 
            this.rbBF.AutoSize = true;
            this.rbBF.Checked = true;
            this.rbBF.Location = new System.Drawing.Point(82, 107);
            this.rbBF.Name = "rbBF";
            this.rbBF.Size = new System.Drawing.Size(64, 17);
            this.rbBF.TabIndex = 5;
            this.rbBF.TabStop = true;
            this.rbBF.Text = "Blowfish";
            this.rbBF.UseVisualStyleBackColor = true;
            this.rbBF.CheckedChanged += new System.EventHandler(this.OnEncryptCheck);
            // 
            // tabPageDiag
            // 
            this.tabPageDiag.BackColor = System.Drawing.Color.Transparent;
            this.tabPageDiag.Controls.Add(this.chkSend4096);
            this.tabPageDiag.Controls.Add(this.chkSend1536);
            this.tabPageDiag.Controls.Add(this.chkSend2048);
            this.tabPageDiag.Controls.Add(this.chkSend1024);
            this.tabPageDiag.Controls.Add(this.btnSendXXXData);
            this.tabPageDiag.Controls.Add(this.tbLog);
            this.tabPageDiag.Controls.Add(this.cbCmd);
            this.tabPageDiag.Controls.Add(this.btnSend);
            this.tabPageDiag.Location = new System.Drawing.Point(4, 22);
            this.tabPageDiag.Name = "tabPageDiag";
            this.tabPageDiag.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDiag.Size = new System.Drawing.Size(574, 427);
            this.tabPageDiag.TabIndex = 4;
            this.tabPageDiag.Text = "Diagnostic & Test";
            // 
            // chkSend4096
            // 
            this.chkSend4096.AutoSize = true;
            this.chkSend4096.Location = new System.Drawing.Point(373, 122);
            this.chkSend4096.Name = "chkSend4096";
            this.chkSend4096.Size = new System.Drawing.Size(115, 17);
            this.chkSend4096.TabIndex = 7;
            this.chkSend4096.TabStop = true;
            this.chkSend4096.Text = "Send 4096 bit data";
            this.chkSend4096.UseVisualStyleBackColor = true;
            // 
            // chkSend1536
            // 
            this.chkSend1536.AutoSize = true;
            this.chkSend1536.Location = new System.Drawing.Point(133, 122);
            this.chkSend1536.Name = "chkSend1536";
            this.chkSend1536.Size = new System.Drawing.Size(115, 17);
            this.chkSend1536.TabIndex = 6;
            this.chkSend1536.TabStop = true;
            this.chkSend1536.Text = "Send 1536 bit data";
            this.chkSend1536.UseVisualStyleBackColor = true;
            // 
            // chkSend2048
            // 
            this.chkSend2048.AutoSize = true;
            this.chkSend2048.Location = new System.Drawing.Point(252, 122);
            this.chkSend2048.Name = "chkSend2048";
            this.chkSend2048.Size = new System.Drawing.Size(115, 17);
            this.chkSend2048.TabIndex = 5;
            this.chkSend2048.TabStop = true;
            this.chkSend2048.Text = "Send 2048 bit data";
            this.chkSend2048.UseVisualStyleBackColor = true;
            // 
            // chkSend1024
            // 
            this.chkSend1024.AutoSize = true;
            this.chkSend1024.Location = new System.Drawing.Point(16, 122);
            this.chkSend1024.Name = "chkSend1024";
            this.chkSend1024.Size = new System.Drawing.Size(115, 17);
            this.chkSend1024.TabIndex = 4;
            this.chkSend1024.TabStop = true;
            this.chkSend1024.Text = "Send 1024 bit data";
            this.chkSend1024.UseVisualStyleBackColor = true;
            // 
            // btnSendXXXData
            // 
            this.btnSendXXXData.Location = new System.Drawing.Point(16, 93);
            this.btnSendXXXData.Name = "btnSendXXXData";
            this.btnSendXXXData.Size = new System.Drawing.Size(43, 23);
            this.btnSendXXXData.TabIndex = 3;
            this.btnSendXXXData.Text = "Send";
            this.btnSendXXXData.UseVisualStyleBackColor = true;
            this.btnSendXXXData.Click += new System.EventHandler(this.OnSendXXXData);
            // 
            // tbLog
            // 
            this.tbLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tbLog.ForeColor = System.Drawing.Color.White;
            this.tbLog.Location = new System.Drawing.Point(16, 145);
            this.tbLog.Multiline = true;
            this.tbLog.Name = "tbLog";
            this.tbLog.ReadOnly = true;
            this.tbLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbLog.Size = new System.Drawing.Size(543, 257);
            this.tbLog.TabIndex = 1;
            // 
            // cbCmd
            // 
            this.cbCmd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbCmd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cbCmd.ForeColor = System.Drawing.Color.White;
            this.cbCmd.FormattingEnabled = true;
            this.cbCmd.Items.AddRange(new object[] {
            "Syntax: SYS|LOGIN|REQ|[Module!Post!Station!Username!Password]",
            "Syntax: SYS|LOGIN|RES|[Result!Data]",
            "Sample: SYS|LOGIN|REQ|ADMIN!POST0!ADMIN#1!tobasaqueue!A1410C6E07BDA0D774A76E64402" +
                "4801EB00175B27B85D0289469978603EBB9F4",
            "Sample: SYS|LOGIN|RES|OK!Identifier",
            "---------------------------------------------------------------------------------" +
                "-",
            "Syntax: SYS|NOTIFY|[Type!Message]",
            "Sample: SYS|NOTIFY|ERROR!This is error message",
            "---------------------------------------------------------------------------------" +
                "-",
            "Syntax: SYS|DEL_TABLE|REQ|Identifier|[Table!Param0!Param1]",
            "Syntax: SYS|DEL_TABLE|RES|Identifier|[Table!Result]",
            "Sample: SYS|DEL_TABLE|REQ|Identifier|table!param0!param1",
            "---------------------------------------------------------------------------------" +
                "-",
            "Syntax: SYS|GET_TABLE|REQ|Identifier|[Table!Param0!Param1]",
            "Syntax: SYS|GET_TABLE|RES|Identifier|[Table!Result]",
            "Sample: SYS|GET_TABLE|REQ|Identifier|table!param0!param1",
            "---------------------------------------------------------------------------------" +
                "-",
            "Syntax: SYS|UPDATE_JOB|REQ|Identifier|[Status!JobId!JobNo]",
            "Syntax: SYS|UPDATE_JOB|RES|Identifier|[Status!Result]",
            "Sample: SYS|UPDATE_JOB|REQ|Identifier|PROCESS!2!4",
            "---------------------------------------------------------------------------------" +
                "-",
            "Syntax: SYS|GET_JOB|REQ|Identifier|[Post!Status!Offset!Limit]",
            "Syntax: SYS|GET_JOB|RES|Identifier|[Post!Status!Result]",
            "Sample: SYS|GET_JOB|REQ|Identifier|POST2!FINISHED!0!0",
            "---------------------------------------------------------------------------------" +
                "-",
            "Syntax: CALLER|GET_INFO|REQ|Identifier|[Post]",
            "Syntax: CALLER|GET_INFO|RES|Identifier|[Prefix!Number!Left]",
            "Sample: CALLER|GET_INFO|REQ|Identifier|POST3",
            "---------------------------------------------------------------------------------" +
                "-",
            "Syntax: CALLER|GET_NEXT|REQ|Identifier|[Post!Station]",
            "Syntax: CALLER|GET_NEXT|RES|Identifier|[Prefix!Number!Left]",
            "Sample: CALLER|GET_NEXT|REQ|Identifier|POST3!CALLER#2",
            "---------------------------------------------------------------------------------" +
                "-",
            "Syntax: CALLER|RECALL|REQ|Identifier|[Number!Post!Station]",
            "Syntax: CALLER|RECALL|RES|Identifier|[Result]",
            "Sample: CALLER|RECALL|REQ|Identifier|3!POST1!CALLER#2",
            "---------------------------------------------------------------------------------" +
                "-",
            "Syntax: TICKET|CREATE|REQ|Identifier|[Post!Station]",
            "Syntax: TICKET|CREATE|RES|Identifier|[Prefix!Number!Post!Time]",
            "Sample: TICKET|CREATE|REQ|Identifier|POST1!CALLER#2",
            "---------------------------------------------------------------------------------" +
                "-",
            "Syntax: DISPLAY|CALL|REQ|Identifier|[Prefix!Number!Left!Post!Caller]",
            "Syntax: DISPLAY|CALL|RES|Identifier|[Status]",
            "Sample: DISPLAY|CALL|REQ|Identifier|B!4!6!POST3!CALLER#2",
            "---------------------------------------------------------------------------------" +
                "-",
            "Syntax: DISPLAY|RECALL|REQ|Identifier|[Prefix!Number!Left!Post!Caller]",
            "Syntax: DISPLAY|RECALL|RES|Identifier|[Status]",
            "Sample: DISPLAY|RECALL|REQ|Identifier|BA!6!10!POST3!CALLER#7",
            "---------------------------------------------------------------------------------" +
                "-",
            "Syntax: DISPLAY|SET_FINISHED|REQ|Identifier|[Post!Caller|Data]",
            "Syntax: DISPLAY|SET_FINISHED|RES|Identifier|[Status] ",
            "Sample: DISPLAY|SET_FINISHED|REQ|Identifier|POST5!CALLER#4|A1,A2,A3,A4,A5,A6,A7,A" +
                "9,A10",
            "---------------------------------------------------------------------------------" +
                "-",
            "Syntax: DISPLAY|SHOW_INFO|REQ|Identifier|[Post!Caller|Info]",
            "Syntax: DISPLAY|SHOW_INFO|RES|Identifier|[Status]",
            "Sample: DISPLAY|SHOW_INFO|REQ|Identifier|POST5!CALLER#2|Antrian Telah Selesai",
            "---------------------------------------------------------------------------------" +
                "-",
            "Syntax: DISPLAY|RESET_VALUES|REQ|Identifier|[Post!Station]",
            "Syntax: DISPLAY|RESET_VALUES|RES|Identifier|[Status]",
            "Sample: DISPLAY|RESET_VALUES|REQ|Identifier|POST5!CALLER#2",
            "---------------------------------------------------------------------------------" +
                "-",
            "Syntax: DISPLAY|RESET_RUNTEXT|REQ|Identifier|[Post!Station]",
            "Syntax: DISPLAY|RESET_RUNTEXT|RES|Identifier|[Status]",
            "Sample: DISPLAY|RESET_RUNTEXT|REQ|Identifier|POST5!CALLER#2",
            "---------------------------------------------------------------------------------" +
                "-",
            "Syntax: DISPLAY|DEL_RUNTEXT|REQ|Identifier|[Post!Station!Text]",
            "Syntax: DISPLAY|DEL_RUNTEXT|RES|Identifier|[Status]",
            "Sample: DISPLAY|DEL_RUNTEXT|REQ|Identifier|POST5!CALLER#2|This is running text",
            "Sample: DISPLAY|GET_RUNTEXT|RES|Identifier|Running text Display #1 dari database " +
                "server",
            "---------------------------------------------------------------------------------" +
                "-",
            "Syntax: DISPLAY|GET_RUNTEXT|REQ|Identifier|[Post!Station]",
            "Syntax: DISPLAY|GET_RUNTEXT|RES|Identifier|[Result]",
            "Sample: DISPLAY|GET_RUNTEXT|REQ|Identifier|POST0!DISP#1",
            "---------------------------------------------------------------------------------" +
                "-"});
            this.cbCmd.Location = new System.Drawing.Point(16, 43);
            this.cbCmd.Name = "cbCmd";
            this.cbCmd.Size = new System.Drawing.Size(543, 21);
            this.cbCmd.TabIndex = 0;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(16, 14);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(43, 23);
            this.btnSend.TabIndex = 2;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.OnSendData);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tbServerMessages);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(574, 427);
            this.tabPage1.TabIndex = 5;
            this.tabPage1.Text = "Server messages";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tbServerMessages
            // 
            this.tbServerMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbServerMessages.BackColor = System.Drawing.Color.Black;
            this.tbServerMessages.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.tbServerMessages.Location = new System.Drawing.Point(9, 14);
            this.tbServerMessages.Multiline = true;
            this.tbServerMessages.Name = "tbServerMessages";
            this.tbServerMessages.Size = new System.Drawing.Size(548, 396);
            this.tbServerMessages.TabIndex = 0;
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.menuAbout});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(600, 24);
            this.menuStrip.TabIndex = 2;
            this.menuStrip.Text = "File";
            // 
            // menuFile
            // 
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuOption,
            this.menuExit});
            this.menuFile.Name = "menuFile";
            this.menuFile.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F)));
            this.menuFile.Size = new System.Drawing.Size(37, 20);
            this.menuFile.Text = "File";
            // 
            // menuOption
            // 
            this.menuOption.Name = "menuOption";
            this.menuOption.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.menuOption.Size = new System.Drawing.Size(180, 22);
            this.menuOption.Text = "&Options";
            this.menuOption.Click += new System.EventHandler(this.OnOptions);
            // 
            // menuExit
            // 
            this.menuExit.Name = "menuExit";
            this.menuExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.menuExit.Size = new System.Drawing.Size(180, 22);
            this.menuExit.Text = "E&xit";
            this.menuExit.Click += new System.EventHandler(this.OnExit);
            // 
            // menuAbout
            // 
            this.menuAbout.Name = "menuAbout";
            this.menuAbout.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.A)));
            this.menuAbout.Size = new System.Drawing.Size(52, 20);
            this.menuAbout.Text = "&About";
            this.menuAbout.Click += new System.EventHandler(this.OnAbout);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(606, 511);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1024, 768);
            this.MinimumSize = new System.Drawing.Size(600, 550);
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Admin Form";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
            this.Shown += new System.EventHandler(this.OnFormShown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabPageGeneral.ResumeLayout(false);
            this.tabPageGeneral.PerformLayout();
            this.tabTable.ResumeLayout(false);
            this.tabIpAccessList.ResumeLayout(false);
            this.tabIpAccessList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridIpAccess)).EndInit();
            this.tabStation.ResumeLayout(false);
            this.tabStation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridStations)).EndInit();
            this.tabLogin.ResumeLayout(false);
            this.tabLogin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridLogins)).EndInit();
            this.tabPost.ResumeLayout(false);
            this.tabPost.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridPosts)).EndInit();
            this.tabRunText.ResumeLayout(false);
            this.tabRunText.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridRunText)).EndInit();
            this.tabPageEncrypt.ResumeLayout(false);
            this.gbEnryptPass.ResumeLayout(false);
            this.gbEnryptPass.PerformLayout();
            this.gbPassHash.ResumeLayout(false);
            this.gbPassHash.PerformLayout();
            this.gbEnryptTool.ResumeLayout(false);
            this.gbEnryptTool.PerformLayout();
            this.tabPageDiag.ResumeLayout(false);
            this.tabPageDiag.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageGeneral;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.TabPage tabPageEncrypt;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbSalt;
        private System.Windows.Forms.RadioButton rbBF;
        private System.Windows.Forms.RadioButton rbSHA;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbInput;
        private System.Windows.Forms.Button btnDecrypt;
        private System.Windows.Forms.RichTextBox tbOutput;
        private System.Windows.Forms.TextBox tbKey;
        private System.Windows.Forms.Button btnEncrypt;
        private System.Windows.Forms.CheckBox chkSalt;
        private System.Windows.Forms.TabPage tabPageDiag;
        private System.Windows.Forms.TextBox tbLog;
        private System.Windows.Forms.ComboBox cbCmd;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnSendXXXData;
        private System.Windows.Forms.RadioButton chkSend2048;
        private System.Windows.Forms.RadioButton chkSend1024;
        private System.Windows.Forms.RadioButton chkSend4096;
        private System.Windows.Forms.RadioButton chkSend1536;
        private System.Windows.Forms.DataGridView gridIpAccess;
        private System.Windows.Forms.DataGridView gridStations;
        private System.Windows.Forms.TabControl tabTable;
        private System.Windows.Forms.TabPage tabIpAccessList;
        private System.Windows.Forms.TabPage tabStation;
        private System.Windows.Forms.Button btnDeleteData;
        private System.Windows.Forms.Button btnAddData;
        private System.Windows.Forms.TabPage tabLogin;
        private System.Windows.Forms.DataGridView gridLogins;
        private System.Windows.Forms.TabPage tabPost;
        private System.Windows.Forms.DataGridView gridPosts;
        private System.Windows.Forms.TabPage tabRunText;
        private System.Windows.Forms.DataGridView gridRunText;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.GroupBox gbPassHash;
        private System.Windows.Forms.TextBox txtEName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtEPass;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbEnryptTool;
        private System.Windows.Forms.TextBox txtEResult;
        private System.Windows.Forms.Button btnCreatePasswordHash;
        private System.Windows.Forms.GroupBox gbEnryptPass;
        private System.Windows.Forms.TextBox txtECSPass;
        private System.Windows.Forms.Button btnEncryptPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCSPass;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox tbServerMessages;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPre;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Button btnFirst;
        private System.Windows.Forms.TextBox tbReordStatus;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        private System.Windows.Forms.ToolStripMenuItem menuOption;
        private System.Windows.Forms.ToolStripMenuItem menuExit;
        private System.Windows.Forms.ToolStripMenuItem menuAbout;
    }
}

