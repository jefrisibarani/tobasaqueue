namespace Tobasa
{
    partial class FormServerConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormServerConfig));
            this.btnApplyConfig = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbSqlIPAddr = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbSqlTcp = new System.Windows.Forms.TextBox();
            this.tbSqlUserName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbSqlDatabase = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBoxSqlServer = new System.Windows.Forms.GroupBox();
            this.chkSqlUseDefault = new System.Windows.Forms.CheckBox();
            this.tbSqlPwdConfirm = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbSqlPwd = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkQueueUseDefault = new System.Windows.Forms.CheckBox();
            this.tbQueuePwdConfirm = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbQueuePwd = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tbQueueUserName = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.tbQueueIPAddr = new System.Windows.Forms.TextBox();
            this.tbQueueTcp = new System.Windows.Forms.TextBox();
            this.tbSecuritySalt = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.tbSqliteDbPath = new System.Windows.Forms.TextBox();
            this.btnBrowseSqliteDb = new System.Windows.Forms.Button();
            this.rbMSSQL = new System.Windows.Forms.RadioButton();
            this.rbSQLITE = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkSqliteUseDefault = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.chkUseSecuritySaltDefault = new System.Windows.Forms.CheckBox();
            this.rbMYSQL = new System.Windows.Forms.RadioButton();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.rbPGSQL = new System.Windows.Forms.RadioButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabServerAndDatabase = new System.Windows.Forms.TabPage();
            this.tabPostSettings = new System.Windows.Forms.TabPage();
            this.callerGroupBox = new System.Windows.Forms.GroupBox();
            this.callerPostCaptionTb = new System.Windows.Forms.TextBox();
            this.callerPostNameTb = new System.Windows.Forms.TextBox();
            this.callerLblName = new System.Windows.Forms.Label();
            this.callerLblCaption = new System.Windows.Forms.Label();
            this.btnPost5 = new System.Windows.Forms.Button();
            this.btnPost6 = new System.Windows.Forms.Button();
            this.ticketGroupBox = new System.Windows.Forms.GroupBox();
            this.ticketLblPrintCopies = new System.Windows.Forms.Label();
            this.ticketPostPrintCopies = new System.Windows.Forms.NumericUpDown();
            this.ticketPostEnabled = new System.Windows.Forms.CheckBox();
            this.ticketPostVisible = new System.Windows.Forms.CheckBox();
            this.ticketPostTicketHeaderTb = new System.Windows.Forms.TextBox();
            this.ticketPostCaptionTb = new System.Windows.Forms.TextBox();
            this.ticketPostNameTb = new System.Windows.Forms.TextBox();
            this.ticketLblTicketHeader = new System.Windows.Forms.Label();
            this.ticketLblName = new System.Windows.Forms.Label();
            this.ticketLblCaption = new System.Windows.Forms.Label();
            this.btnPost7 = new System.Windows.Forms.Button();
            this.btnPost8 = new System.Windows.Forms.Button();
            this.displayGroupBox = new System.Windows.Forms.GroupBox();
            this.displayPostPlayAudioChk = new System.Windows.Forms.CheckBox();
            this.displayPostVisibleChk = new System.Windows.Forms.CheckBox();
            this.displayInfoTextTb = new System.Windows.Forms.TextBox();
            this.displayPostCaptionTb = new System.Windows.Forms.TextBox();
            this.displayPostNameTb = new System.Windows.Forms.TextBox();
            this.displayLblInfoText = new System.Windows.Forms.Label();
            this.displayLblName = new System.Windows.Forms.Label();
            this.displayLblCaption = new System.Windows.Forms.Label();
            this.btnPost9 = new System.Windows.Forms.Button();
            this.btnPost4 = new System.Windows.Forms.Button();
            this.btnPost3 = new System.Windows.Forms.Button();
            this.btnPost2 = new System.Windows.Forms.Button();
            this.btnPost1 = new System.Windows.Forms.Button();
            this.btnPost0 = new System.Windows.Forms.Button();
            this.btnInfo = new System.Windows.Forms.Button();
            this.groupBoxSqlServer.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabServerAndDatabase.SuspendLayout();
            this.tabPostSettings.SuspendLayout();
            this.callerGroupBox.SuspendLayout();
            this.ticketGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ticketPostPrintCopies)).BeginInit();
            this.displayGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnApplyConfig
            // 
            this.btnApplyConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApplyConfig.Location = new System.Drawing.Point(395, 525);
            this.btnApplyConfig.Name = "btnApplyConfig";
            this.btnApplyConfig.Size = new System.Drawing.Size(145, 28);
            this.btnApplyConfig.TabIndex = 0;
            this.btnApplyConfig.Text = "&Apply Configuration";
            this.btnApplyConfig.UseVisualStyleBackColor = true;
            this.btnApplyConfig.Click += new System.EventHandler(this.OnApplyConfig);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "IP Address";
            // 
            // tbSqlIPAddr
            // 
            this.tbSqlIPAddr.Enabled = false;
            this.tbSqlIPAddr.Location = new System.Drawing.Point(108, 29);
            this.tbSqlIPAddr.MaxLength = 15;
            this.tbSqlIPAddr.Name = "tbSqlIPAddr";
            this.tbSqlIPAddr.Size = new System.Drawing.Size(100, 20);
            this.tbSqlIPAddr.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "TCP Port";
            // 
            // tbSqlTcp
            // 
            this.tbSqlTcp.Enabled = false;
            this.tbSqlTcp.Location = new System.Drawing.Point(108, 52);
            this.tbSqlTcp.MaxLength = 5;
            this.tbSqlTcp.Name = "tbSqlTcp";
            this.tbSqlTcp.Size = new System.Drawing.Size(100, 20);
            this.tbSqlTcp.TabIndex = 4;
            // 
            // tbSqlUserName
            // 
            this.tbSqlUserName.Enabled = false;
            this.tbSqlUserName.Location = new System.Drawing.Point(108, 98);
            this.tbSqlUserName.MaxLength = 50;
            this.tbSqlUserName.Name = "tbSqlUserName";
            this.tbSqlUserName.Size = new System.Drawing.Size(100, 20);
            this.tbSqlUserName.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "User Name";
            // 
            // tbSqlDatabase
            // 
            this.tbSqlDatabase.Enabled = false;
            this.tbSqlDatabase.Location = new System.Drawing.Point(108, 75);
            this.tbSqlDatabase.MaxLength = 50;
            this.tbSqlDatabase.Name = "tbSqlDatabase";
            this.tbSqlDatabase.Size = new System.Drawing.Size(100, 20);
            this.tbSqlDatabase.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Database";
            // 
            // groupBoxSqlServer
            // 
            this.groupBoxSqlServer.Controls.Add(this.chkSqlUseDefault);
            this.groupBoxSqlServer.Controls.Add(this.tbSqlPwdConfirm);
            this.groupBoxSqlServer.Controls.Add(this.label6);
            this.groupBoxSqlServer.Controls.Add(this.tbSqlPwd);
            this.groupBoxSqlServer.Controls.Add(this.label5);
            this.groupBoxSqlServer.Controls.Add(this.label2);
            this.groupBoxSqlServer.Controls.Add(this.tbSqlUserName);
            this.groupBoxSqlServer.Controls.Add(this.label1);
            this.groupBoxSqlServer.Controls.Add(this.label3);
            this.groupBoxSqlServer.Controls.Add(this.tbSqlIPAddr);
            this.groupBoxSqlServer.Controls.Add(this.tbSqlDatabase);
            this.groupBoxSqlServer.Controls.Add(this.tbSqlTcp);
            this.groupBoxSqlServer.Controls.Add(this.label4);
            this.groupBoxSqlServer.Location = new System.Drawing.Point(20, 171);
            this.groupBoxSqlServer.Name = "groupBoxSqlServer";
            this.groupBoxSqlServer.Size = new System.Drawing.Size(228, 199);
            this.groupBoxSqlServer.TabIndex = 9;
            this.groupBoxSqlServer.TabStop = false;
            this.groupBoxSqlServer.Text = "Microsoft SQL Server Connnection Info";
            // 
            // chkSqlUseDefault
            // 
            this.chkSqlUseDefault.AutoSize = true;
            this.chkSqlUseDefault.Checked = true;
            this.chkSqlUseDefault.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSqlUseDefault.Location = new System.Drawing.Point(18, 174);
            this.chkSqlUseDefault.Name = "chkSqlUseDefault";
            this.chkSqlUseDefault.Size = new System.Drawing.Size(80, 17);
            this.chkSqlUseDefault.TabIndex = 14;
            this.chkSqlUseDefault.Text = "Use default";
            this.chkSqlUseDefault.UseVisualStyleBackColor = true;
            this.chkSqlUseDefault.CheckedChanged += new System.EventHandler(this.OnSqlUseDefault);
            // 
            // tbSqlPwdConfirm
            // 
            this.tbSqlPwdConfirm.Enabled = false;
            this.tbSqlPwdConfirm.Location = new System.Drawing.Point(108, 145);
            this.tbSqlPwdConfirm.MaxLength = 50;
            this.tbSqlPwdConfirm.Name = "tbSqlPwdConfirm";
            this.tbSqlPwdConfirm.Size = new System.Drawing.Size(100, 20);
            this.tbSqlPwdConfirm.TabIndex = 12;
            this.tbSqlPwdConfirm.UseSystemPasswordChar = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 148);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Password Confirm";
            // 
            // tbSqlPwd
            // 
            this.tbSqlPwd.Enabled = false;
            this.tbSqlPwd.Location = new System.Drawing.Point(108, 121);
            this.tbSqlPwd.MaxLength = 50;
            this.tbSqlPwd.Name = "tbSqlPwd";
            this.tbSqlPwd.Size = new System.Drawing.Size(100, 20);
            this.tbSqlPwd.TabIndex = 10;
            this.tbSqlPwd.UseSystemPasswordChar = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 124);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Password";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkQueueUseDefault);
            this.groupBox2.Controls.Add(this.tbQueuePwdConfirm);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.tbQueuePwd);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.tbQueueUserName);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.tbQueueIPAddr);
            this.groupBox2.Controls.Add(this.tbQueueTcp);
            this.groupBox2.Location = new System.Drawing.Point(280, 171);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(227, 199);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Queue Server Connection Info";
            // 
            // chkQueueUseDefault
            // 
            this.chkQueueUseDefault.AutoSize = true;
            this.chkQueueUseDefault.Checked = true;
            this.chkQueueUseDefault.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkQueueUseDefault.Location = new System.Drawing.Point(18, 174);
            this.chkQueueUseDefault.Name = "chkQueueUseDefault";
            this.chkQueueUseDefault.Size = new System.Drawing.Size(80, 17);
            this.chkQueueUseDefault.TabIndex = 13;
            this.chkQueueUseDefault.Text = "Use default";
            this.chkQueueUseDefault.UseVisualStyleBackColor = true;
            this.chkQueueUseDefault.CheckedChanged += new System.EventHandler(this.OnQueueUseDefault);
            // 
            // tbQueuePwdConfirm
            // 
            this.tbQueuePwdConfirm.Enabled = false;
            this.tbQueuePwdConfirm.Location = new System.Drawing.Point(108, 121);
            this.tbQueuePwdConfirm.MaxLength = 50;
            this.tbQueuePwdConfirm.Name = "tbQueuePwdConfirm";
            this.tbQueuePwdConfirm.Size = new System.Drawing.Size(100, 20);
            this.tbQueuePwdConfirm.TabIndex = 12;
            this.tbQueuePwdConfirm.UseSystemPasswordChar = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 124);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(91, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "Password Confirm";
            // 
            // tbQueuePwd
            // 
            this.tbQueuePwd.Enabled = false;
            this.tbQueuePwd.Location = new System.Drawing.Point(108, 98);
            this.tbQueuePwd.MaxLength = 50;
            this.tbQueuePwd.Name = "tbQueuePwd";
            this.tbQueuePwd.Size = new System.Drawing.Size(100, 20);
            this.tbQueuePwd.TabIndex = 10;
            this.tbQueuePwd.UseSystemPasswordChar = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(15, 101);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "Password";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(15, 54);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(50, 13);
            this.label9.TabIndex = 3;
            this.label9.Text = "TCP Port";
            // 
            // tbQueueUserName
            // 
            this.tbQueueUserName.Enabled = false;
            this.tbQueueUserName.Location = new System.Drawing.Point(108, 75);
            this.tbQueueUserName.MaxLength = 50;
            this.tbQueueUserName.Name = "tbQueueUserName";
            this.tbQueueUserName.Size = new System.Drawing.Size(100, 20);
            this.tbQueueUserName.TabIndex = 8;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(15, 32);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(58, 13);
            this.label10.TabIndex = 1;
            this.label10.Text = "IP Address";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(15, 78);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(60, 13);
            this.label11.TabIndex = 7;
            this.label11.Text = "User Name";
            // 
            // tbQueueIPAddr
            // 
            this.tbQueueIPAddr.Enabled = false;
            this.tbQueueIPAddr.Location = new System.Drawing.Point(108, 29);
            this.tbQueueIPAddr.MaxLength = 15;
            this.tbQueueIPAddr.Name = "tbQueueIPAddr";
            this.tbQueueIPAddr.Size = new System.Drawing.Size(100, 20);
            this.tbQueueIPAddr.TabIndex = 2;
            // 
            // tbQueueTcp
            // 
            this.tbQueueTcp.Enabled = false;
            this.tbQueueTcp.Location = new System.Drawing.Point(108, 51);
            this.tbQueueTcp.MaxLength = 5;
            this.tbQueueTcp.Name = "tbQueueTcp";
            this.tbQueueTcp.Size = new System.Drawing.Size(100, 20);
            this.tbQueueTcp.TabIndex = 4;
            // 
            // tbSecuritySalt
            // 
            this.tbSecuritySalt.Enabled = false;
            this.tbSecuritySalt.Location = new System.Drawing.Point(18, 21);
            this.tbSecuritySalt.MaxLength = 128;
            this.tbSecuritySalt.Name = "tbSecuritySalt";
            this.tbSecuritySalt.Size = new System.Drawing.Size(452, 20);
            this.tbSecuritySalt.TabIndex = 12;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(15, 21);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(47, 13);
            this.label13.TabIndex = 13;
            this.label13.Text = "File path";
            // 
            // tbSqliteDbPath
            // 
            this.tbSqliteDbPath.Enabled = false;
            this.tbSqliteDbPath.Location = new System.Drawing.Point(68, 21);
            this.tbSqliteDbPath.MaxLength = 6;
            this.tbSqliteDbPath.Name = "tbSqliteDbPath";
            this.tbSqliteDbPath.Size = new System.Drawing.Size(372, 20);
            this.tbSqliteDbPath.TabIndex = 14;
            // 
            // btnBrowseSqliteDb
            // 
            this.btnBrowseSqliteDb.Enabled = false;
            this.btnBrowseSqliteDb.Location = new System.Drawing.Point(446, 21);
            this.btnBrowseSqliteDb.Name = "btnBrowseSqliteDb";
            this.btnBrowseSqliteDb.Size = new System.Drawing.Size(24, 23);
            this.btnBrowseSqliteDb.TabIndex = 15;
            this.btnBrowseSqliteDb.Text = "...";
            this.btnBrowseSqliteDb.UseVisualStyleBackColor = true;
            this.btnBrowseSqliteDb.Click += new System.EventHandler(this.OnBrowseSqliteDb);
            // 
            // rbMSSQL
            // 
            this.rbMSSQL.AutoSize = true;
            this.rbMSSQL.Location = new System.Drawing.Point(102, 31);
            this.rbMSSQL.Name = "rbMSSQL";
            this.rbMSSQL.Size = new System.Drawing.Size(126, 17);
            this.rbMSSQL.TabIndex = 17;
            this.rbMSSQL.TabStop = true;
            this.rbMSSQL.Text = "Microsoft SQL Server";
            this.rbMSSQL.UseVisualStyleBackColor = true;
            this.rbMSSQL.CheckedChanged += new System.EventHandler(this.OnDBTypeCheckedChanged);
            // 
            // rbSQLITE
            // 
            this.rbSQLITE.AutoSize = true;
            this.rbSQLITE.Location = new System.Drawing.Point(18, 31);
            this.rbSQLITE.Name = "rbSQLITE";
            this.rbSQLITE.Size = new System.Drawing.Size(66, 17);
            this.rbSQLITE.TabIndex = 18;
            this.rbSQLITE.TabStop = true;
            this.rbSQLITE.Text = "SQLite 3";
            this.rbSQLITE.UseVisualStyleBackColor = true;
            this.rbSQLITE.CheckedChanged += new System.EventHandler(this.OnDBTypeCheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkSqliteUseDefault);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.tbSqliteDbPath);
            this.groupBox3.Controls.Add(this.btnBrowseSqliteDb);
            this.groupBox3.Location = new System.Drawing.Point(20, 88);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(487, 72);
            this.groupBox3.TabIndex = 19;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "SQLite Database Info";
            // 
            // chkSqliteUseDefault
            // 
            this.chkSqliteUseDefault.AutoSize = true;
            this.chkSqliteUseDefault.Checked = true;
            this.chkSqliteUseDefault.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSqliteUseDefault.Location = new System.Drawing.Point(18, 47);
            this.chkSqliteUseDefault.Name = "chkSqliteUseDefault";
            this.chkSqliteUseDefault.Size = new System.Drawing.Size(80, 17);
            this.chkSqliteUseDefault.TabIndex = 16;
            this.chkSqliteUseDefault.Text = "Use default";
            this.chkSqliteUseDefault.UseVisualStyleBackColor = true;
            this.chkSqliteUseDefault.CheckedChanged += new System.EventHandler(this.OnSqliteUseDefault);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.chkUseSecuritySaltDefault);
            this.groupBox4.Controls.Add(this.tbSecuritySalt);
            this.groupBox4.Location = new System.Drawing.Point(20, 382);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(487, 72);
            this.groupBox4.TabIndex = 20;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Security Salt Key";
            // 
            // chkUseSecuritySaltDefault
            // 
            this.chkUseSecuritySaltDefault.AutoSize = true;
            this.chkUseSecuritySaltDefault.Checked = true;
            this.chkUseSecuritySaltDefault.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUseSecuritySaltDefault.Location = new System.Drawing.Point(18, 47);
            this.chkUseSecuritySaltDefault.Name = "chkUseSecuritySaltDefault";
            this.chkUseSecuritySaltDefault.Size = new System.Drawing.Size(80, 17);
            this.chkUseSecuritySaltDefault.TabIndex = 17;
            this.chkUseSecuritySaltDefault.Text = "Use default";
            this.chkUseSecuritySaltDefault.UseVisualStyleBackColor = true;
            this.chkUseSecuritySaltDefault.CheckedChanged += new System.EventHandler(this.OnUseSecuritySaltDefault);
            // 
            // rbMYSQL
            // 
            this.rbMYSQL.AutoSize = true;
            this.rbMYSQL.Location = new System.Drawing.Point(234, 31);
            this.rbMYSQL.Name = "rbMYSQL";
            this.rbMYSQL.Size = new System.Drawing.Size(88, 17);
            this.rbMYSQL.TabIndex = 21;
            this.rbMYSQL.TabStop = true;
            this.rbMYSQL.Text = "MySql Server";
            this.rbMYSQL.UseVisualStyleBackColor = true;
            this.rbMYSQL.CheckedChanged += new System.EventHandler(this.OnDBTypeCheckedChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.rbPGSQL);
            this.groupBox5.Controls.Add(this.rbMSSQL);
            this.groupBox5.Controls.Add(this.rbMYSQL);
            this.groupBox5.Controls.Add(this.rbSQLITE);
            this.groupBox5.Location = new System.Drawing.Point(20, 20);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(487, 54);
            this.groupBox5.TabIndex = 22;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Database type";
            // 
            // rbPGSQL
            // 
            this.rbPGSQL.AutoSize = true;
            this.rbPGSQL.Location = new System.Drawing.Point(328, 31);
            this.rbPGSQL.Name = "rbPGSQL";
            this.rbPGSQL.Size = new System.Drawing.Size(116, 17);
            this.rbPGSQL.TabIndex = 22;
            this.rbPGSQL.TabStop = true;
            this.rbPGSQL.Text = "PostgreSQL Server";
            this.rbPGSQL.UseVisualStyleBackColor = true;
            this.rbPGSQL.CheckedChanged += new System.EventHandler(this.OnDBTypeCheckedChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabServerAndDatabase);
            this.tabControl1.Controls.Add(this.tabPostSettings);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(539, 498);
            this.tabControl1.TabIndex = 23;
            // 
            // tabServerAndDatabase
            // 
            this.tabServerAndDatabase.Controls.Add(this.groupBox5);
            this.tabServerAndDatabase.Controls.Add(this.groupBoxSqlServer);
            this.tabServerAndDatabase.Controls.Add(this.groupBox4);
            this.tabServerAndDatabase.Controls.Add(this.groupBox2);
            this.tabServerAndDatabase.Controls.Add(this.groupBox3);
            this.tabServerAndDatabase.Location = new System.Drawing.Point(4, 22);
            this.tabServerAndDatabase.Name = "tabServerAndDatabase";
            this.tabServerAndDatabase.Padding = new System.Windows.Forms.Padding(3);
            this.tabServerAndDatabase.Size = new System.Drawing.Size(531, 472);
            this.tabServerAndDatabase.TabIndex = 0;
            this.tabServerAndDatabase.Text = "Server and Database";
            this.tabServerAndDatabase.UseVisualStyleBackColor = true;
            // 
            // tabPostSettings
            // 
            this.tabPostSettings.Controls.Add(this.callerGroupBox);
            this.tabPostSettings.Controls.Add(this.btnPost5);
            this.tabPostSettings.Controls.Add(this.btnPost6);
            this.tabPostSettings.Controls.Add(this.ticketGroupBox);
            this.tabPostSettings.Controls.Add(this.btnPost7);
            this.tabPostSettings.Controls.Add(this.btnPost8);
            this.tabPostSettings.Controls.Add(this.displayGroupBox);
            this.tabPostSettings.Controls.Add(this.btnPost9);
            this.tabPostSettings.Controls.Add(this.btnPost4);
            this.tabPostSettings.Controls.Add(this.btnPost3);
            this.tabPostSettings.Controls.Add(this.btnPost2);
            this.tabPostSettings.Controls.Add(this.btnPost1);
            this.tabPostSettings.Controls.Add(this.btnPost0);
            this.tabPostSettings.Location = new System.Drawing.Point(4, 22);
            this.tabPostSettings.Name = "tabPostSettings";
            this.tabPostSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabPostSettings.Size = new System.Drawing.Size(531, 472);
            this.tabPostSettings.TabIndex = 1;
            this.tabPostSettings.Text = "Post Settings";
            this.tabPostSettings.UseVisualStyleBackColor = true;
            // 
            // callerGroupBox
            // 
            this.callerGroupBox.Controls.Add(this.callerPostCaptionTb);
            this.callerGroupBox.Controls.Add(this.callerPostNameTb);
            this.callerGroupBox.Controls.Add(this.callerLblName);
            this.callerGroupBox.Controls.Add(this.callerLblCaption);
            this.callerGroupBox.Location = new System.Drawing.Point(36, 348);
            this.callerGroupBox.Name = "callerGroupBox";
            this.callerGroupBox.Size = new System.Drawing.Size(452, 100);
            this.callerGroupBox.TabIndex = 5;
            this.callerGroupBox.TabStop = false;
            this.callerGroupBox.Text = "Queue Caller";
            // 
            // callerPostCaptionTb
            // 
            this.callerPostCaptionTb.Location = new System.Drawing.Point(98, 50);
            this.callerPostCaptionTb.MaxLength = 50;
            this.callerPostCaptionTb.Name = "callerPostCaptionTb";
            this.callerPostCaptionTb.Size = new System.Drawing.Size(162, 20);
            this.callerPostCaptionTb.TabIndex = 16;
            this.callerPostCaptionTb.TextChanged += new System.EventHandler(this.OnTextChanged);
            this.callerPostCaptionTb.Leave += new System.EventHandler(this.OnFocusLeaveFromTextBox);
            // 
            // callerPostNameTb
            // 
            this.callerPostNameTb.Location = new System.Drawing.Point(98, 24);
            this.callerPostNameTb.MaxLength = 50;
            this.callerPostNameTb.Name = "callerPostNameTb";
            this.callerPostNameTb.Size = new System.Drawing.Size(162, 20);
            this.callerPostNameTb.TabIndex = 15;
            this.callerPostNameTb.TextChanged += new System.EventHandler(this.OnTextChanged);
            this.callerPostNameTb.Leave += new System.EventHandler(this.OnFocusLeaveFromTextBox);
            // 
            // callerLblName
            // 
            this.callerLblName.AutoSize = true;
            this.callerLblName.Location = new System.Drawing.Point(22, 27);
            this.callerLblName.Name = "callerLblName";
            this.callerLblName.Size = new System.Drawing.Size(35, 13);
            this.callerLblName.TabIndex = 13;
            this.callerLblName.Text = "Name";
            // 
            // callerLblCaption
            // 
            this.callerLblCaption.AutoSize = true;
            this.callerLblCaption.Location = new System.Drawing.Point(22, 53);
            this.callerLblCaption.Name = "callerLblCaption";
            this.callerLblCaption.Size = new System.Drawing.Size(43, 13);
            this.callerLblCaption.TabIndex = 12;
            this.callerLblCaption.Text = "Caption";
            // 
            // btnPost5
            // 
            this.btnPost5.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPost5.Location = new System.Drawing.Point(263, 22);
            this.btnPost5.Name = "btnPost5";
            this.btnPost5.Size = new System.Drawing.Size(46, 23);
            this.btnPost5.TabIndex = 5;
            this.btnPost5.Text = "POST 5";
            this.btnPost5.UseVisualStyleBackColor = true;
            this.btnPost5.Click += new System.EventHandler(this.OnBtnPostClick);
            // 
            // btnPost6
            // 
            this.btnPost6.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPost6.Location = new System.Drawing.Point(310, 22);
            this.btnPost6.Name = "btnPost6";
            this.btnPost6.Size = new System.Drawing.Size(46, 23);
            this.btnPost6.TabIndex = 6;
            this.btnPost6.Text = "POST 6";
            this.btnPost6.UseVisualStyleBackColor = true;
            this.btnPost6.Click += new System.EventHandler(this.OnBtnPostClick);
            // 
            // ticketGroupBox
            // 
            this.ticketGroupBox.Controls.Add(this.ticketLblPrintCopies);
            this.ticketGroupBox.Controls.Add(this.ticketPostPrintCopies);
            this.ticketGroupBox.Controls.Add(this.ticketPostEnabled);
            this.ticketGroupBox.Controls.Add(this.ticketPostVisible);
            this.ticketGroupBox.Controls.Add(this.ticketPostTicketHeaderTb);
            this.ticketGroupBox.Controls.Add(this.ticketPostCaptionTb);
            this.ticketGroupBox.Controls.Add(this.ticketPostNameTb);
            this.ticketGroupBox.Controls.Add(this.ticketLblTicketHeader);
            this.ticketGroupBox.Controls.Add(this.ticketLblName);
            this.ticketGroupBox.Controls.Add(this.ticketLblCaption);
            this.ticketGroupBox.Location = new System.Drawing.Point(36, 213);
            this.ticketGroupBox.Name = "ticketGroupBox";
            this.ticketGroupBox.Size = new System.Drawing.Size(452, 129);
            this.ticketGroupBox.TabIndex = 4;
            this.ticketGroupBox.TabStop = false;
            this.ticketGroupBox.Text = "Queue Ticket";
            // 
            // ticketLblPrintCopies
            // 
            this.ticketLblPrintCopies.AutoSize = true;
            this.ticketLblPrintCopies.Location = new System.Drawing.Point(291, 79);
            this.ticketLblPrintCopies.Name = "ticketLblPrintCopies";
            this.ticketLblPrintCopies.Size = new System.Drawing.Size(96, 13);
            this.ticketLblPrintCopies.TabIndex = 15;
            this.ticketLblPrintCopies.Text = "Ticket Print Copies";
            // 
            // ticketPostPrintCopies
            // 
            this.ticketPostPrintCopies.Location = new System.Drawing.Point(396, 77);
            this.ticketPostPrintCopies.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.ticketPostPrintCopies.Name = "ticketPostPrintCopies";
            this.ticketPostPrintCopies.Size = new System.Drawing.Size(41, 20);
            this.ticketPostPrintCopies.TabIndex = 14;
            this.ticketPostPrintCopies.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ticketPostPrintCopies.ValueChanged += new System.EventHandler(this.OnTicketPrintCopiesValueChanged);
            // 
            // ticketPostEnabled
            // 
            this.ticketPostEnabled.AutoSize = true;
            this.ticketPostEnabled.Location = new System.Drawing.Point(294, 54);
            this.ticketPostEnabled.Name = "ticketPostEnabled";
            this.ticketPostEnabled.Size = new System.Drawing.Size(65, 17);
            this.ticketPostEnabled.TabIndex = 13;
            this.ticketPostEnabled.Text = "Enabled";
            this.ticketPostEnabled.UseVisualStyleBackColor = true;
            this.ticketPostEnabled.Click += new System.EventHandler(this.OnPostCheckBoxClick);
            // 
            // ticketPostVisible
            // 
            this.ticketPostVisible.AutoSize = true;
            this.ticketPostVisible.Location = new System.Drawing.Point(294, 28);
            this.ticketPostVisible.Name = "ticketPostVisible";
            this.ticketPostVisible.Size = new System.Drawing.Size(56, 17);
            this.ticketPostVisible.TabIndex = 12;
            this.ticketPostVisible.Text = "Visible";
            this.ticketPostVisible.UseVisualStyleBackColor = true;
            this.ticketPostVisible.Click += new System.EventHandler(this.OnPostCheckBoxClick);
            // 
            // ticketPostTicketHeaderTb
            // 
            this.ticketPostTicketHeaderTb.Location = new System.Drawing.Point(98, 77);
            this.ticketPostTicketHeaderTb.MaxLength = 50;
            this.ticketPostTicketHeaderTb.Name = "ticketPostTicketHeaderTb";
            this.ticketPostTicketHeaderTb.Size = new System.Drawing.Size(162, 20);
            this.ticketPostTicketHeaderTb.TabIndex = 11;
            this.ticketPostTicketHeaderTb.TextChanged += new System.EventHandler(this.OnTextChanged);
            this.ticketPostTicketHeaderTb.Leave += new System.EventHandler(this.OnFocusLeaveFromTextBox);
            // 
            // ticketPostCaptionTb
            // 
            this.ticketPostCaptionTb.Location = new System.Drawing.Point(98, 51);
            this.ticketPostCaptionTb.MaxLength = 50;
            this.ticketPostCaptionTb.Name = "ticketPostCaptionTb";
            this.ticketPostCaptionTb.Size = new System.Drawing.Size(162, 20);
            this.ticketPostCaptionTb.TabIndex = 10;
            this.ticketPostCaptionTb.TextChanged += new System.EventHandler(this.OnTextChanged);
            this.ticketPostCaptionTb.Leave += new System.EventHandler(this.OnFocusLeaveFromTextBox);
            // 
            // ticketPostNameTb
            // 
            this.ticketPostNameTb.Location = new System.Drawing.Point(98, 25);
            this.ticketPostNameTb.MaxLength = 50;
            this.ticketPostNameTb.Name = "ticketPostNameTb";
            this.ticketPostNameTb.Size = new System.Drawing.Size(162, 20);
            this.ticketPostNameTb.TabIndex = 9;
            this.ticketPostNameTb.TextChanged += new System.EventHandler(this.OnTextChanged);
            this.ticketPostNameTb.Leave += new System.EventHandler(this.OnFocusLeaveFromTextBox);
            // 
            // ticketLblTicketHeader
            // 
            this.ticketLblTicketHeader.AutoSize = true;
            this.ticketLblTicketHeader.Location = new System.Drawing.Point(22, 79);
            this.ticketLblTicketHeader.Name = "ticketLblTicketHeader";
            this.ticketLblTicketHeader.Size = new System.Drawing.Size(75, 13);
            this.ticketLblTicketHeader.TabIndex = 8;
            this.ticketLblTicketHeader.Text = "Ticket Header";
            // 
            // ticketLblName
            // 
            this.ticketLblName.AutoSize = true;
            this.ticketLblName.Location = new System.Drawing.Point(22, 28);
            this.ticketLblName.Name = "ticketLblName";
            this.ticketLblName.Size = new System.Drawing.Size(35, 13);
            this.ticketLblName.TabIndex = 7;
            this.ticketLblName.Text = "Name";
            // 
            // ticketLblCaption
            // 
            this.ticketLblCaption.AutoSize = true;
            this.ticketLblCaption.Location = new System.Drawing.Point(22, 54);
            this.ticketLblCaption.Name = "ticketLblCaption";
            this.ticketLblCaption.Size = new System.Drawing.Size(43, 13);
            this.ticketLblCaption.TabIndex = 6;
            this.ticketLblCaption.Text = "Caption";
            // 
            // btnPost7
            // 
            this.btnPost7.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPost7.Location = new System.Drawing.Point(357, 22);
            this.btnPost7.Name = "btnPost7";
            this.btnPost7.Size = new System.Drawing.Size(46, 23);
            this.btnPost7.TabIndex = 7;
            this.btnPost7.Text = "POST 7";
            this.btnPost7.UseVisualStyleBackColor = true;
            this.btnPost7.Click += new System.EventHandler(this.OnBtnPostClick);
            // 
            // btnPost8
            // 
            this.btnPost8.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPost8.Location = new System.Drawing.Point(404, 22);
            this.btnPost8.Name = "btnPost8";
            this.btnPost8.Size = new System.Drawing.Size(46, 23);
            this.btnPost8.TabIndex = 8;
            this.btnPost8.Text = "POST 8";
            this.btnPost8.UseVisualStyleBackColor = true;
            this.btnPost8.Click += new System.EventHandler(this.OnBtnPostClick);
            // 
            // displayGroupBox
            // 
            this.displayGroupBox.Controls.Add(this.displayPostPlayAudioChk);
            this.displayGroupBox.Controls.Add(this.displayPostVisibleChk);
            this.displayGroupBox.Controls.Add(this.displayInfoTextTb);
            this.displayGroupBox.Controls.Add(this.displayPostCaptionTb);
            this.displayGroupBox.Controls.Add(this.displayPostNameTb);
            this.displayGroupBox.Controls.Add(this.displayLblInfoText);
            this.displayGroupBox.Controls.Add(this.displayLblName);
            this.displayGroupBox.Controls.Add(this.displayLblCaption);
            this.displayGroupBox.Location = new System.Drawing.Point(36, 60);
            this.displayGroupBox.Name = "displayGroupBox";
            this.displayGroupBox.Size = new System.Drawing.Size(452, 147);
            this.displayGroupBox.TabIndex = 3;
            this.displayGroupBox.TabStop = false;
            this.displayGroupBox.Text = "Queue Display";
            // 
            // displayPostPlayAudioChk
            // 
            this.displayPostPlayAudioChk.AutoSize = true;
            this.displayPostPlayAudioChk.Location = new System.Drawing.Point(294, 53);
            this.displayPostPlayAudioChk.Name = "displayPostPlayAudioChk";
            this.displayPostPlayAudioChk.Size = new System.Drawing.Size(76, 17);
            this.displayPostPlayAudioChk.TabIndex = 7;
            this.displayPostPlayAudioChk.Text = "Play Audio";
            this.displayPostPlayAudioChk.UseVisualStyleBackColor = true;
            this.displayPostPlayAudioChk.Click += new System.EventHandler(this.OnPostCheckBoxClick);
            // 
            // displayPostVisibleChk
            // 
            this.displayPostVisibleChk.AutoSize = true;
            this.displayPostVisibleChk.Location = new System.Drawing.Point(294, 27);
            this.displayPostVisibleChk.Name = "displayPostVisibleChk";
            this.displayPostVisibleChk.Size = new System.Drawing.Size(56, 17);
            this.displayPostVisibleChk.TabIndex = 6;
            this.displayPostVisibleChk.Text = "Visible";
            this.displayPostVisibleChk.UseVisualStyleBackColor = true;
            this.displayPostVisibleChk.Click += new System.EventHandler(this.OnPostCheckBoxClick);
            // 
            // displayInfoTextTb
            // 
            this.displayInfoTextTb.Location = new System.Drawing.Point(98, 76);
            this.displayInfoTextTb.MaxLength = 50;
            this.displayInfoTextTb.Name = "displayInfoTextTb";
            this.displayInfoTextTb.Size = new System.Drawing.Size(162, 20);
            this.displayInfoTextTb.TabIndex = 5;
            this.displayInfoTextTb.TextChanged += new System.EventHandler(this.OnTextChanged);
            this.displayInfoTextTb.Leave += new System.EventHandler(this.OnFocusLeaveFromTextBox);
            // 
            // displayPostCaptionTb
            // 
            this.displayPostCaptionTb.Location = new System.Drawing.Point(98, 50);
            this.displayPostCaptionTb.MaxLength = 50;
            this.displayPostCaptionTb.Name = "displayPostCaptionTb";
            this.displayPostCaptionTb.Size = new System.Drawing.Size(162, 20);
            this.displayPostCaptionTb.TabIndex = 4;
            this.displayPostCaptionTb.TextChanged += new System.EventHandler(this.OnTextChanged);
            this.displayPostCaptionTb.Leave += new System.EventHandler(this.OnFocusLeaveFromTextBox);
            // 
            // displayPostNameTb
            // 
            this.displayPostNameTb.Location = new System.Drawing.Point(98, 24);
            this.displayPostNameTb.MaxLength = 50;
            this.displayPostNameTb.Name = "displayPostNameTb";
            this.displayPostNameTb.Size = new System.Drawing.Size(162, 20);
            this.displayPostNameTb.TabIndex = 3;
            this.displayPostNameTb.TextChanged += new System.EventHandler(this.OnTextChanged);
            this.displayPostNameTb.Leave += new System.EventHandler(this.OnFocusLeaveFromTextBox);
            // 
            // displayLblInfoText
            // 
            this.displayLblInfoText.AutoSize = true;
            this.displayLblInfoText.Location = new System.Drawing.Point(22, 78);
            this.displayLblInfoText.Name = "displayLblInfoText";
            this.displayLblInfoText.Size = new System.Drawing.Size(49, 13);
            this.displayLblInfoText.TabIndex = 2;
            this.displayLblInfoText.Text = "Info Text";
            // 
            // displayLblName
            // 
            this.displayLblName.AutoSize = true;
            this.displayLblName.Location = new System.Drawing.Point(22, 27);
            this.displayLblName.Name = "displayLblName";
            this.displayLblName.Size = new System.Drawing.Size(35, 13);
            this.displayLblName.TabIndex = 1;
            this.displayLblName.Text = "Name";
            // 
            // displayLblCaption
            // 
            this.displayLblCaption.AutoSize = true;
            this.displayLblCaption.Location = new System.Drawing.Point(22, 53);
            this.displayLblCaption.Name = "displayLblCaption";
            this.displayLblCaption.Size = new System.Drawing.Size(43, 13);
            this.displayLblCaption.TabIndex = 0;
            this.displayLblCaption.Text = "Caption";
            // 
            // btnPost9
            // 
            this.btnPost9.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPost9.Location = new System.Drawing.Point(451, 22);
            this.btnPost9.Name = "btnPost9";
            this.btnPost9.Size = new System.Drawing.Size(46, 23);
            this.btnPost9.TabIndex = 9;
            this.btnPost9.Text = "POST 9";
            this.btnPost9.UseVisualStyleBackColor = true;
            this.btnPost9.Click += new System.EventHandler(this.OnBtnPostClick);
            // 
            // btnPost4
            // 
            this.btnPost4.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPost4.Location = new System.Drawing.Point(216, 22);
            this.btnPost4.Name = "btnPost4";
            this.btnPost4.Size = new System.Drawing.Size(46, 23);
            this.btnPost4.TabIndex = 4;
            this.btnPost4.Text = "POST 4";
            this.btnPost4.UseVisualStyleBackColor = true;
            this.btnPost4.Click += new System.EventHandler(this.OnBtnPostClick);
            // 
            // btnPost3
            // 
            this.btnPost3.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPost3.Location = new System.Drawing.Point(169, 22);
            this.btnPost3.Name = "btnPost3";
            this.btnPost3.Size = new System.Drawing.Size(46, 23);
            this.btnPost3.TabIndex = 3;
            this.btnPost3.Text = "POST 3";
            this.btnPost3.UseVisualStyleBackColor = true;
            this.btnPost3.Click += new System.EventHandler(this.OnBtnPostClick);
            // 
            // btnPost2
            // 
            this.btnPost2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPost2.Location = new System.Drawing.Point(122, 22);
            this.btnPost2.Name = "btnPost2";
            this.btnPost2.Size = new System.Drawing.Size(46, 23);
            this.btnPost2.TabIndex = 2;
            this.btnPost2.Text = "POST 2";
            this.btnPost2.UseVisualStyleBackColor = true;
            this.btnPost2.Click += new System.EventHandler(this.OnBtnPostClick);
            // 
            // btnPost1
            // 
            this.btnPost1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPost1.Location = new System.Drawing.Point(75, 22);
            this.btnPost1.Name = "btnPost1";
            this.btnPost1.Size = new System.Drawing.Size(46, 23);
            this.btnPost1.TabIndex = 1;
            this.btnPost1.Text = "POST 1";
            this.btnPost1.UseVisualStyleBackColor = true;
            this.btnPost1.Click += new System.EventHandler(this.OnBtnPostClick);
            // 
            // btnPost0
            // 
            this.btnPost0.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPost0.Location = new System.Drawing.Point(28, 22);
            this.btnPost0.Name = "btnPost0";
            this.btnPost0.Size = new System.Drawing.Size(46, 23);
            this.btnPost0.TabIndex = 0;
            this.btnPost0.Text = "POST 0";
            this.btnPost0.UseVisualStyleBackColor = true;
            this.btnPost0.Click += new System.EventHandler(this.OnBtnPostClick);
            // 
            // btnInfo
            // 
            this.btnInfo.Location = new System.Drawing.Point(16, 525);
            this.btnInfo.Name = "btnInfo";
            this.btnInfo.Size = new System.Drawing.Size(63, 28);
            this.btnInfo.TabIndex = 24;
            this.btnInfo.Text = "&Info";
            this.btnInfo.UseVisualStyleBackColor = true;
            this.btnInfo.Click += new System.EventHandler(this.OnInfo);
            // 
            // FormServerConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(562, 571);
            this.Controls.Add(this.btnInfo);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnApplyConfig);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormServerConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Set Tobasa Queue Configuration";
            this.groupBoxSqlServer.ResumeLayout(false);
            this.groupBoxSqlServer.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabServerAndDatabase.ResumeLayout(false);
            this.tabPostSettings.ResumeLayout(false);
            this.callerGroupBox.ResumeLayout(false);
            this.callerGroupBox.PerformLayout();
            this.ticketGroupBox.ResumeLayout(false);
            this.ticketGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ticketPostPrintCopies)).EndInit();
            this.displayGroupBox.ResumeLayout(false);
            this.displayGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnApplyConfig;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbSqlIPAddr;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbSqlTcp;
        private System.Windows.Forms.TextBox tbSqlUserName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbSqlDatabase;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBoxSqlServer;
        private System.Windows.Forms.TextBox tbSqlPwdConfirm;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbSqlPwd;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tbQueuePwdConfirm;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbQueuePwd;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbQueueUserName;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbQueueIPAddr;
        private System.Windows.Forms.TextBox tbQueueTcp;
        private System.Windows.Forms.CheckBox chkSqlUseDefault;
        private System.Windows.Forms.CheckBox chkQueueUseDefault;
        private System.Windows.Forms.TextBox tbSecuritySalt;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tbSqliteDbPath;
        private System.Windows.Forms.Button btnBrowseSqliteDb;
        private System.Windows.Forms.RadioButton rbMSSQL;
        private System.Windows.Forms.RadioButton rbSQLITE;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chkSqliteUseDefault;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox chkUseSecuritySaltDefault;
        private System.Windows.Forms.RadioButton rbMYSQL;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabServerAndDatabase;
        private System.Windows.Forms.TabPage tabPostSettings;
        private System.Windows.Forms.GroupBox callerGroupBox;
        private System.Windows.Forms.TextBox callerPostCaptionTb;
        private System.Windows.Forms.TextBox callerPostNameTb;
        private System.Windows.Forms.Label callerLblName;
        private System.Windows.Forms.Label callerLblCaption;
        private System.Windows.Forms.GroupBox ticketGroupBox;
        private System.Windows.Forms.TextBox ticketPostTicketHeaderTb;
        private System.Windows.Forms.TextBox ticketPostCaptionTb;
        private System.Windows.Forms.TextBox ticketPostNameTb;
        private System.Windows.Forms.Label ticketLblTicketHeader;
        private System.Windows.Forms.Label ticketLblName;
        private System.Windows.Forms.Label ticketLblCaption;
        private System.Windows.Forms.GroupBox displayGroupBox;
        private System.Windows.Forms.TextBox displayInfoTextTb;
        private System.Windows.Forms.TextBox displayPostCaptionTb;
        private System.Windows.Forms.TextBox displayPostNameTb;
        private System.Windows.Forms.Label displayLblInfoText;
        private System.Windows.Forms.Label displayLblName;
        private System.Windows.Forms.Label displayLblCaption;
        private System.Windows.Forms.CheckBox displayPostPlayAudioChk;
        private System.Windows.Forms.CheckBox displayPostVisibleChk;
        private System.Windows.Forms.Label ticketLblPrintCopies;
        private System.Windows.Forms.NumericUpDown ticketPostPrintCopies;
        private System.Windows.Forms.CheckBox ticketPostEnabled;
        private System.Windows.Forms.CheckBox ticketPostVisible;
        private System.Windows.Forms.Button btnPost5;
        private System.Windows.Forms.Button btnPost6;
        private System.Windows.Forms.Button btnPost7;
        private System.Windows.Forms.Button btnPost8;
        private System.Windows.Forms.Button btnPost9;
        private System.Windows.Forms.Button btnPost4;
        private System.Windows.Forms.Button btnPost3;
        private System.Windows.Forms.Button btnPost2;
        private System.Windows.Forms.Button btnPost1;
        private System.Windows.Forms.Button btnPost0;
        private System.Windows.Forms.RadioButton rbPGSQL;
        private System.Windows.Forms.Button btnInfo;
    }
}

