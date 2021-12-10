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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
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
            this.label14 = new System.Windows.Forms.Label();
            this.rbSqlserver = new System.Windows.Forms.RadioButton();
            this.rbSqlite = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkSqliteUseDefault = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.chkUseSecuritySaltDefault = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnApplyConfig
            // 
            this.btnApplyConfig.Location = new System.Drawing.Point(354, 424);
            this.btnApplyConfig.Name = "btnApplyConfig";
            this.btnApplyConfig.Size = new System.Drawing.Size(145, 28);
            this.btnApplyConfig.TabIndex = 0;
            this.btnApplyConfig.Text = "Apply Configuration";
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
            this.tbSqlTcp.MaxLength = 6;
            this.tbSqlTcp.Name = "tbSqlTcp";
            this.tbSqlTcp.Size = new System.Drawing.Size(100, 20);
            this.tbSqlTcp.TabIndex = 4;
            // 
            // tbSqlUserName
            // 
            this.tbSqlUserName.Enabled = false;
            this.tbSqlUserName.Location = new System.Drawing.Point(108, 98);
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkSqlUseDefault);
            this.groupBox1.Controls.Add(this.tbSqlPwdConfirm);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.tbSqlPwd);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tbSqlUserName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tbSqlIPAddr);
            this.groupBox1.Controls.Add(this.tbSqlDatabase);
            this.groupBox1.Controls.Add(this.tbSqlTcp);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(12, 213);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(228, 199);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SQL Server Connection Info";
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
            this.groupBox2.Location = new System.Drawing.Point(272, 213);
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
            this.tbQueueIPAddr.Name = "tbQueueIPAddr";
            this.tbQueueIPAddr.Size = new System.Drawing.Size(100, 20);
            this.tbQueueIPAddr.TabIndex = 2;
            // 
            // tbQueueTcp
            // 
            this.tbQueueTcp.Enabled = false;
            this.tbQueueTcp.Location = new System.Drawing.Point(108, 51);
            this.tbQueueTcp.MaxLength = 6;
            this.tbQueueTcp.Name = "tbQueueTcp";
            this.tbQueueTcp.Size = new System.Drawing.Size(100, 20);
            this.tbQueueTcp.TabIndex = 4;
            // 
            // tbSecuritySalt
            // 
            this.tbSecuritySalt.Enabled = false;
            this.tbSecuritySalt.Location = new System.Drawing.Point(18, 21);
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
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(18, 26);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(79, 13);
            this.label14.TabIndex = 16;
            this.label14.Text = "Database type:";
            // 
            // rbSqlserver
            // 
            this.rbSqlserver.AutoSize = true;
            this.rbSqlserver.Location = new System.Drawing.Point(112, 24);
            this.rbSqlserver.Name = "rbSqlserver";
            this.rbSqlserver.Size = new System.Drawing.Size(126, 17);
            this.rbSqlserver.TabIndex = 17;
            this.rbSqlserver.TabStop = true;
            this.rbSqlserver.Text = "Microsoft SQL Server";
            this.rbSqlserver.UseVisualStyleBackColor = true;
            this.rbSqlserver.CheckedChanged += new System.EventHandler(this.OnDBTypeCheckedChanged);
            // 
            // rbSqlite
            // 
            this.rbSqlite.AutoSize = true;
            this.rbSqlite.Location = new System.Drawing.Point(254, 24);
            this.rbSqlite.Name = "rbSqlite";
            this.rbSqlite.Size = new System.Drawing.Size(66, 17);
            this.rbSqlite.TabIndex = 18;
            this.rbSqlite.TabStop = true;
            this.rbSqlite.Text = "SQLite 3";
            this.rbSqlite.UseVisualStyleBackColor = true;
            this.rbSqlite.CheckedChanged += new System.EventHandler(this.OnDBTypeCheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkSqliteUseDefault);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.tbSqliteDbPath);
            this.groupBox3.Controls.Add(this.btnBrowseSqliteDb);
            this.groupBox3.Location = new System.Drawing.Point(12, 130);
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
            this.groupBox4.Location = new System.Drawing.Point(12, 52);
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
            // FormServerConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 464);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.rbSqlite);
            this.Controls.Add(this.rbSqlserver);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnApplyConfig);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormServerConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Set Tobasa Queue Configuration";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.GroupBox groupBox1;
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
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.RadioButton rbSqlserver;
        private System.Windows.Forms.RadioButton rbSqlite;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chkSqliteUseDefault;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox chkUseSecuritySaltDefault;
    }
}

