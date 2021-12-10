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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnAbout = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.mainTab = new System.Windows.Forms.TabControl();
            this.tabPageMain = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label12 = new System.Windows.Forms.Label();
            this.capStation = new System.Windows.Forms.Label();
            this.capNumber = new System.Windows.Forms.Label();
            this.capNext = new System.Windows.Forms.Label();
            this.lblStation = new System.Windows.Forms.Label();
            this.lblPost = new System.Windows.Forms.Label();
            this.lblQueueCount = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCall = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.capCurrNumber = new System.Windows.Forms.Label();
            this.lblNumber = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnNext = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.postsBtnDiv = new System.Windows.Forms.TableLayoutPanel();
            this.btnChangePost0 = new System.Windows.Forms.Button();
            this.btnChangePost1 = new System.Windows.Forms.Button();
            this.btnChangePost2 = new System.Windows.Forms.Button();
            this.btnChangePost3 = new System.Windows.Forms.Button();
            this.btnChangePost4 = new System.Windows.Forms.Button();
            this.btnChangePost5 = new System.Windows.Forms.Button();
            this.btnChangePost6 = new System.Windows.Forms.Button();
            this.btnChangePost7 = new System.Windows.Forms.Button();
            this.btnChangePost8 = new System.Windows.Forms.Button();
            this.btnChangePost9 = new System.Windows.Forms.Button();
            this.tabProcessing = new System.Windows.Forms.TabPage();
            this.label14 = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.gridJobs = new System.Windows.Forms.DataGridView();
            this.tabFinished = new System.Windows.Forms.TabPage();
            this.label13 = new System.Windows.Forms.Label();
            this.btnRefreshFin = new System.Windows.Forms.Button();
            this.gridJobsFin = new System.Windows.Forms.DataGridView();
            this.tabOptions = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSaveSettings = new System.Windows.Forms.Button();
            this.chkManageAdvanceQueue = new System.Windows.Forms.CheckBox();
            this.chkManageAllPostAdvanceQueue = new System.Windows.Forms.CheckBox();
            this.chkShowSwitchPostsButtons = new System.Windows.Forms.CheckBox();
            this.gbConnProps = new System.Windows.Forms.GroupBox();
            this.lblCtrNoChar = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbPost = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbCounterNo = new System.Windows.Forms.ComboBox();
            this.tbStation = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnSaveReconnect = new System.Windows.Forms.Button();
            this.tbPort = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbServer = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.mainTab.SuspendLayout();
            this.tabPageMain.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.postsBtnDiv.SuspendLayout();
            this.tabProcessing.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridJobs)).BeginInit();
            this.tabFinished.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridJobsFin)).BeginInit();
            this.tabOptions.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gbConnProps.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAbout
            // 
            this.btnAbout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAbout.Location = new System.Drawing.Point(641, 460);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(43, 23);
            this.btnAbout.TabIndex = 0;
            this.btnAbout.Text = "About";
            this.btnAbout.UseVisualStyleBackColor = true;
            this.btnAbout.Click += new System.EventHandler(this.OnAbout);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.White;
            this.lblStatus.Location = new System.Drawing.Point(0, 471);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Padding = new System.Windows.Forms.Padding(10, 0, 0, 2);
            this.lblStatus.Size = new System.Drawing.Size(138, 15);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.Text = "Not Connected to Server ";
            // 
            // mainTab
            // 
            this.mainTab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainTab.Controls.Add(this.tabPageMain);
            this.mainTab.Controls.Add(this.tabProcessing);
            this.mainTab.Controls.Add(this.tabFinished);
            this.mainTab.Controls.Add(this.tabOptions);
            this.mainTab.Location = new System.Drawing.Point(5, 5);
            this.mainTab.Name = "mainTab";
            this.mainTab.SelectedIndex = 0;
            this.mainTab.Size = new System.Drawing.Size(679, 449);
            this.mainTab.TabIndex = 17;
            this.mainTab.Selected += new System.Windows.Forms.TabControlEventHandler(this.OnPageSelected);
            // 
            // tabPageMain
            // 
            this.tabPageMain.BackColor = System.Drawing.Color.Lavender;
            this.tabPageMain.Controls.Add(this.tableLayoutPanel3);
            this.tabPageMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPageMain.Location = new System.Drawing.Point(4, 22);
            this.tabPageMain.Name = "tabPageMain";
            this.tabPageMain.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMain.Size = new System.Drawing.Size(671, 423);
            this.tabPageMain.TabIndex = 0;
            this.tabPageMain.Text = "General";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.BackColor = System.Drawing.Color.Lavender;
            this.tableLayoutPanel3.ColumnCount = 5;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tableLayoutPanel3.Controls.Add(this.label12, 3, 4);
            this.tableLayoutPanel3.Controls.Add(this.capStation, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.capNumber, 2, 1);
            this.tableLayoutPanel3.Controls.Add(this.capNext, 3, 1);
            this.tableLayoutPanel3.Controls.Add(this.lblStation, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.lblPost, 2, 2);
            this.tableLayoutPanel3.Controls.Add(this.lblQueueCount, 3, 2);
            this.tableLayoutPanel3.Controls.Add(this.panel2, 1, 3);
            this.tableLayoutPanel3.Controls.Add(this.panel3, 2, 3);
            this.tableLayoutPanel3.Controls.Add(this.panel4, 3, 3);
            this.tableLayoutPanel3.Controls.Add(this.label11, 1, 4);
            this.tableLayoutPanel3.Controls.Add(this.postsBtnDiv, 2, 4);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 5;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(665, 417);
            this.tableLayoutPanel3.TabIndex = 15;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.MediumBlue;
            this.label12.Location = new System.Drawing.Point(467, 331);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(146, 86);
            this.label12.TabIndex = 14;
            this.label12.Text = "F12 : Next Number";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // capStation
            // 
            this.capStation.AutoSize = true;
            this.capStation.BackColor = System.Drawing.Color.CadetBlue;
            this.capStation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.capStation.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.capStation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.capStation.Location = new System.Drawing.Point(46, 20);
            this.capStation.Margin = new System.Windows.Forms.Padding(0);
            this.capStation.Name = "capStation";
            this.capStation.Size = new System.Drawing.Size(152, 41);
            this.capStation.TabIndex = 15;
            this.capStation.Text = "Station";
            this.capStation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.capStation.Resize += new System.EventHandler(this.OnLabelResize);
            // 
            // capNumber
            // 
            this.capNumber.AutoSize = true;
            this.capNumber.BackColor = System.Drawing.Color.CadetBlue;
            this.capNumber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.capNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.capNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.capNumber.Location = new System.Drawing.Point(198, 20);
            this.capNumber.Margin = new System.Windows.Forms.Padding(0);
            this.capNumber.Name = "capNumber";
            this.capNumber.Size = new System.Drawing.Size(266, 41);
            this.capNumber.TabIndex = 16;
            this.capNumber.Text = "Post";
            this.capNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.capNumber.Resize += new System.EventHandler(this.OnLabelResize);
            // 
            // capNext
            // 
            this.capNext.AutoSize = true;
            this.capNext.BackColor = System.Drawing.Color.CadetBlue;
            this.capNext.Dock = System.Windows.Forms.DockStyle.Fill;
            this.capNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.capNext.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.capNext.Location = new System.Drawing.Point(464, 20);
            this.capNext.Margin = new System.Windows.Forms.Padding(0);
            this.capNext.Name = "capNext";
            this.capNext.Size = new System.Drawing.Size(152, 41);
            this.capNext.TabIndex = 17;
            this.capNext.Text = "Total Queue";
            this.capNext.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.capNext.Resize += new System.EventHandler(this.OnLabelResize);
            // 
            // lblStation
            // 
            this.lblStation.AutoSize = true;
            this.lblStation.BackColor = System.Drawing.Color.LightSteelBlue;
            this.lblStation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStation.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblStation.Location = new System.Drawing.Point(46, 61);
            this.lblStation.Margin = new System.Windows.Forms.Padding(0);
            this.lblStation.Name = "lblStation";
            this.lblStation.Size = new System.Drawing.Size(152, 83);
            this.lblStation.TabIndex = 18;
            this.lblStation.Text = "CALL01";
            this.lblStation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblStation.Resize += new System.EventHandler(this.OnLabelResize);
            // 
            // lblPost
            // 
            this.lblPost.AutoSize = true;
            this.lblPost.BackColor = System.Drawing.Color.LightSteelBlue;
            this.lblPost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPost.Font = new System.Drawing.Font("Microsoft Sans Serif", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPost.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblPost.Location = new System.Drawing.Point(198, 61);
            this.lblPost.Margin = new System.Windows.Forms.Padding(0);
            this.lblPost.Name = "lblPost";
            this.lblPost.Size = new System.Drawing.Size(266, 83);
            this.lblPost.TabIndex = 19;
            this.lblPost.Text = "POST1";
            this.lblPost.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblPost.Resize += new System.EventHandler(this.OnLabelResize);
            // 
            // lblQueueCount
            // 
            this.lblQueueCount.AutoSize = true;
            this.lblQueueCount.BackColor = System.Drawing.Color.LightSteelBlue;
            this.lblQueueCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblQueueCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQueueCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblQueueCount.Location = new System.Drawing.Point(464, 61);
            this.lblQueueCount.Margin = new System.Windows.Forms.Padding(0);
            this.lblQueueCount.Name = "lblQueueCount";
            this.lblQueueCount.Size = new System.Drawing.Size(152, 83);
            this.lblQueueCount.TabIndex = 20;
            this.lblQueueCount.Text = "0";
            this.lblQueueCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblQueueCount.Resize += new System.EventHandler(this.OnLabelResize);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panel2.Controls.Add(this.btnCall);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(46, 144);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(152, 187);
            this.panel2.TabIndex = 21;
            // 
            // btnCall
            // 
            this.btnCall.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCall.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnCall.BackColor = System.Drawing.Color.AliceBlue;
            this.btnCall.BackgroundImage = global::Tobasa.Properties.Resources.lblgreen;
            this.btnCall.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCall.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnCall.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnCall.Font = new System.Drawing.Font("Microsoft Sans Serif", 28F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCall.ForeColor = System.Drawing.Color.MintCream;
            this.btnCall.Location = new System.Drawing.Point(5, 9);
            this.btnCall.Name = "btnCall";
            this.btnCall.Size = new System.Drawing.Size(142, 169);
            this.btnCall.TabIndex = 5;
            this.btnCall.Text = "Call";
            this.btnCall.UseVisualStyleBackColor = false;
            this.btnCall.Click += new System.EventHandler(this.OnCallNumber);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panel3.Controls.Add(this.tableLayoutPanel1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(198, 144);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(266, 187);
            this.panel3.TabIndex = 22;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.capCurrNumber, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblNumber, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(266, 187);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // capCurrNumber
            // 
            this.capCurrNumber.AutoSize = true;
            this.capCurrNumber.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.capCurrNumber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.capCurrNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.capCurrNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.capCurrNumber.Location = new System.Drawing.Point(3, 0);
            this.capCurrNumber.Name = "capCurrNumber";
            this.capCurrNumber.Size = new System.Drawing.Size(260, 37);
            this.capCurrNumber.TabIndex = 8;
            this.capCurrNumber.Text = "Current Number";
            this.capCurrNumber.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.capCurrNumber.Resize += new System.EventHandler(this.OnLabelResize);
            // 
            // lblNumber
            // 
            this.lblNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNumber.AutoSize = true;
            this.lblNumber.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lblNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 60F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumber.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblNumber.Location = new System.Drawing.Point(3, 37);
            this.lblNumber.Name = "lblNumber";
            this.lblNumber.Size = new System.Drawing.Size(260, 150);
            this.lblNumber.TabIndex = 7;
            this.lblNumber.Text = "R999";
            this.lblNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblNumber.Resize += new System.EventHandler(this.OnLabelResize);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panel4.Controls.Add(this.btnNext);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(464, 144);
            this.panel4.Margin = new System.Windows.Forms.Padding(0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(152, 187);
            this.panel4.TabIndex = 23;
            // 
            // btnNext
            // 
            this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNext.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnNext.BackColor = System.Drawing.Color.AliceBlue;
            this.btnNext.BackgroundImage = global::Tobasa.Properties.Resources.lblgreen;
            this.btnNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnNext.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnNext.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 28F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.ForeColor = System.Drawing.Color.MintCream;
            this.btnNext.Location = new System.Drawing.Point(5, 9);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(142, 169);
            this.btnNext.TabIndex = 4;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = false;
            this.btnNext.Click += new System.EventHandler(this.OnNextNumber);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.MediumBlue;
            this.label11.Location = new System.Drawing.Point(49, 331);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(146, 86);
            this.label11.TabIndex = 13;
            this.label11.Text = "F9 : Recall";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // postsBtnDiv
            // 
            this.postsBtnDiv.ColumnCount = 5;
            this.postsBtnDiv.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.postsBtnDiv.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.postsBtnDiv.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.postsBtnDiv.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.postsBtnDiv.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.postsBtnDiv.Controls.Add(this.btnChangePost0, 0, 0);
            this.postsBtnDiv.Controls.Add(this.btnChangePost1, 1, 0);
            this.postsBtnDiv.Controls.Add(this.btnChangePost2, 2, 0);
            this.postsBtnDiv.Controls.Add(this.btnChangePost3, 3, 0);
            this.postsBtnDiv.Controls.Add(this.btnChangePost4, 4, 0);
            this.postsBtnDiv.Controls.Add(this.btnChangePost5, 0, 1);
            this.postsBtnDiv.Controls.Add(this.btnChangePost6, 1, 1);
            this.postsBtnDiv.Controls.Add(this.btnChangePost7, 2, 1);
            this.postsBtnDiv.Controls.Add(this.btnChangePost8, 3, 1);
            this.postsBtnDiv.Controls.Add(this.btnChangePost9, 4, 1);
            this.postsBtnDiv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.postsBtnDiv.Location = new System.Drawing.Point(201, 334);
            this.postsBtnDiv.Name = "postsBtnDiv";
            this.postsBtnDiv.RowCount = 2;
            this.postsBtnDiv.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.postsBtnDiv.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.postsBtnDiv.Size = new System.Drawing.Size(260, 80);
            this.postsBtnDiv.TabIndex = 24;
            // 
            // btnChangePost0
            // 
            this.btnChangePost0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnChangePost0.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChangePost0.Location = new System.Drawing.Point(3, 3);
            this.btnChangePost0.Name = "btnChangePost0";
            this.btnChangePost0.Size = new System.Drawing.Size(46, 34);
            this.btnChangePost0.TabIndex = 0;
            this.btnChangePost0.Text = "Post 0";
            this.btnChangePost0.UseVisualStyleBackColor = true;
            this.btnChangePost0.Click += new System.EventHandler(this.OnChangePost);
            // 
            // btnChangePost1
            // 
            this.btnChangePost1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnChangePost1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChangePost1.Location = new System.Drawing.Point(55, 3);
            this.btnChangePost1.Name = "btnChangePost1";
            this.btnChangePost1.Size = new System.Drawing.Size(46, 34);
            this.btnChangePost1.TabIndex = 1;
            this.btnChangePost1.Text = "Post 1";
            this.btnChangePost1.UseVisualStyleBackColor = true;
            this.btnChangePost1.Click += new System.EventHandler(this.OnChangePost);
            // 
            // btnChangePost2
            // 
            this.btnChangePost2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnChangePost2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChangePost2.Location = new System.Drawing.Point(107, 3);
            this.btnChangePost2.Name = "btnChangePost2";
            this.btnChangePost2.Size = new System.Drawing.Size(46, 34);
            this.btnChangePost2.TabIndex = 2;
            this.btnChangePost2.Text = "Post 2";
            this.btnChangePost2.UseVisualStyleBackColor = true;
            this.btnChangePost2.Click += new System.EventHandler(this.OnChangePost);
            // 
            // btnChangePost3
            // 
            this.btnChangePost3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnChangePost3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChangePost3.Location = new System.Drawing.Point(159, 3);
            this.btnChangePost3.Name = "btnChangePost3";
            this.btnChangePost3.Size = new System.Drawing.Size(46, 34);
            this.btnChangePost3.TabIndex = 3;
            this.btnChangePost3.Text = "Post 3";
            this.btnChangePost3.UseVisualStyleBackColor = true;
            this.btnChangePost3.Click += new System.EventHandler(this.OnChangePost);
            // 
            // btnChangePost4
            // 
            this.btnChangePost4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnChangePost4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChangePost4.Location = new System.Drawing.Point(211, 3);
            this.btnChangePost4.Name = "btnChangePost4";
            this.btnChangePost4.Size = new System.Drawing.Size(46, 34);
            this.btnChangePost4.TabIndex = 4;
            this.btnChangePost4.Text = "Post 4";
            this.btnChangePost4.UseVisualStyleBackColor = true;
            this.btnChangePost4.Click += new System.EventHandler(this.OnChangePost);
            // 
            // btnChangePost5
            // 
            this.btnChangePost5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnChangePost5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChangePost5.Location = new System.Drawing.Point(3, 43);
            this.btnChangePost5.Name = "btnChangePost5";
            this.btnChangePost5.Size = new System.Drawing.Size(46, 34);
            this.btnChangePost5.TabIndex = 5;
            this.btnChangePost5.Text = "Post 5";
            this.btnChangePost5.UseVisualStyleBackColor = true;
            this.btnChangePost5.Click += new System.EventHandler(this.OnChangePost);
            // 
            // btnChangePost6
            // 
            this.btnChangePost6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnChangePost6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChangePost6.Location = new System.Drawing.Point(55, 43);
            this.btnChangePost6.Name = "btnChangePost6";
            this.btnChangePost6.Size = new System.Drawing.Size(46, 34);
            this.btnChangePost6.TabIndex = 6;
            this.btnChangePost6.Text = "Post 6";
            this.btnChangePost6.UseVisualStyleBackColor = true;
            this.btnChangePost6.Click += new System.EventHandler(this.OnChangePost);
            // 
            // btnChangePost7
            // 
            this.btnChangePost7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnChangePost7.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChangePost7.Location = new System.Drawing.Point(107, 43);
            this.btnChangePost7.Name = "btnChangePost7";
            this.btnChangePost7.Size = new System.Drawing.Size(46, 34);
            this.btnChangePost7.TabIndex = 7;
            this.btnChangePost7.Text = "Post 7";
            this.btnChangePost7.UseVisualStyleBackColor = true;
            this.btnChangePost7.Click += new System.EventHandler(this.OnChangePost);
            // 
            // btnChangePost8
            // 
            this.btnChangePost8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnChangePost8.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChangePost8.Location = new System.Drawing.Point(159, 43);
            this.btnChangePost8.Name = "btnChangePost8";
            this.btnChangePost8.Size = new System.Drawing.Size(46, 34);
            this.btnChangePost8.TabIndex = 8;
            this.btnChangePost8.Text = "Post 8";
            this.btnChangePost8.UseVisualStyleBackColor = true;
            this.btnChangePost8.Click += new System.EventHandler(this.OnChangePost);
            // 
            // btnChangePost9
            // 
            this.btnChangePost9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnChangePost9.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChangePost9.Location = new System.Drawing.Point(211, 43);
            this.btnChangePost9.Name = "btnChangePost9";
            this.btnChangePost9.Size = new System.Drawing.Size(46, 34);
            this.btnChangePost9.TabIndex = 9;
            this.btnChangePost9.Text = "Post 9";
            this.btnChangePost9.UseVisualStyleBackColor = true;
            this.btnChangePost9.Click += new System.EventHandler(this.OnChangePost);
            // 
            // tabProcessing
            // 
            this.tabProcessing.BackColor = System.Drawing.SystemColors.Window;
            this.tabProcessing.Controls.Add(this.label14);
            this.tabProcessing.Controls.Add(this.btnRefresh);
            this.tabProcessing.Controls.Add(this.gridJobs);
            this.tabProcessing.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabProcessing.Location = new System.Drawing.Point(4, 22);
            this.tabProcessing.Name = "tabProcessing";
            this.tabProcessing.Padding = new System.Windows.Forms.Padding(3);
            this.tabProcessing.Size = new System.Drawing.Size(553, 332);
            this.tabProcessing.TabIndex = 1;
            this.tabProcessing.Text = "Processing";
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.MediumBlue;
            this.label14.Location = new System.Drawing.Point(6, 305);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(208, 16);
            this.label14.TabIndex = 7;
            this.label14.Text = "Double click on a row to set status";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Location = new System.Drawing.Point(468, 301);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 4;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.OnRefresh);
            // 
            // gridJobs
            // 
            this.gridJobs.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Thistle;
            this.gridJobs.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gridJobs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridJobs.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridJobs.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gridJobs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridJobs.DefaultCellStyle = dataGridViewCellStyle3;
            this.gridJobs.Location = new System.Drawing.Point(9, 21);
            this.gridJobs.MinimumSize = new System.Drawing.Size(448, 0);
            this.gridJobs.MultiSelect = false;
            this.gridJobs.Name = "gridJobs";
            this.gridJobs.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridJobs.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.gridJobs.RowHeadersWidth = 20;
            this.gridJobs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridJobs.Size = new System.Drawing.Size(534, 274);
            this.gridJobs.TabIndex = 3;
            this.gridJobs.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.OnGridJobsCellDoubleClick);
            // 
            // tabFinished
            // 
            this.tabFinished.Controls.Add(this.label13);
            this.tabFinished.Controls.Add(this.btnRefreshFin);
            this.tabFinished.Controls.Add(this.gridJobsFin);
            this.tabFinished.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabFinished.Location = new System.Drawing.Point(4, 22);
            this.tabFinished.Name = "tabFinished";
            this.tabFinished.Padding = new System.Windows.Forms.Padding(3);
            this.tabFinished.Size = new System.Drawing.Size(553, 332);
            this.tabFinished.TabIndex = 2;
            this.tabFinished.Text = "Completed";
            this.tabFinished.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.MediumBlue;
            this.label13.Location = new System.Drawing.Point(6, 305);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(249, 16);
            this.label13.TabIndex = 6;
            this.label13.Text = "Double click on a row to close the Queue";
            // 
            // btnRefreshFin
            // 
            this.btnRefreshFin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefreshFin.Location = new System.Drawing.Point(468, 301);
            this.btnRefreshFin.Name = "btnRefreshFin";
            this.btnRefreshFin.Size = new System.Drawing.Size(75, 23);
            this.btnRefreshFin.TabIndex = 5;
            this.btnRefreshFin.Text = "Refresh";
            this.btnRefreshFin.UseVisualStyleBackColor = true;
            this.btnRefreshFin.Click += new System.EventHandler(this.OnRefresh);
            // 
            // gridJobsFin
            // 
            this.gridJobsFin.AllowUserToAddRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.Thistle;
            this.gridJobsFin.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.gridJobsFin.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridJobsFin.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridJobsFin.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.gridJobsFin.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridJobsFin.DefaultCellStyle = dataGridViewCellStyle7;
            this.gridJobsFin.Location = new System.Drawing.Point(9, 21);
            this.gridJobsFin.MinimumSize = new System.Drawing.Size(448, 0);
            this.gridJobsFin.MultiSelect = false;
            this.gridJobsFin.Name = "gridJobsFin";
            this.gridJobsFin.ReadOnly = true;
            this.gridJobsFin.RowHeadersWidth = 20;
            this.gridJobsFin.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridJobsFin.Size = new System.Drawing.Size(534, 274);
            this.gridJobsFin.TabIndex = 4;
            this.gridJobsFin.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.OnGridJobsFinCellDoubleClick);
            // 
            // tabOptions
            // 
            this.tabOptions.Controls.Add(this.groupBox1);
            this.tabOptions.Controls.Add(this.gbConnProps);
            this.tabOptions.Location = new System.Drawing.Point(4, 22);
            this.tabOptions.Name = "tabOptions";
            this.tabOptions.Padding = new System.Windows.Forms.Padding(3);
            this.tabOptions.Size = new System.Drawing.Size(553, 332);
            this.tabOptions.TabIndex = 3;
            this.tabOptions.Text = "Options";
            this.tabOptions.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnSaveSettings);
            this.groupBox1.Controls.Add(this.chkManageAdvanceQueue);
            this.groupBox1.Controls.Add(this.chkManageAllPostAdvanceQueue);
            this.groupBox1.Controls.Add(this.chkShowSwitchPostsButtons);
            this.groupBox1.Location = new System.Drawing.Point(287, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(248, 285);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "General";
            // 
            // btnSaveSettings
            // 
            this.btnSaveSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveSettings.Location = new System.Drawing.Point(145, 233);
            this.btnSaveSettings.Name = "btnSaveSettings";
            this.btnSaveSettings.Size = new System.Drawing.Size(86, 33);
            this.btnSaveSettings.TabIndex = 17;
            this.btnSaveSettings.Text = "Sa&ve Settings";
            this.btnSaveSettings.UseVisualStyleBackColor = true;
            this.btnSaveSettings.Click += new System.EventHandler(this.OnSaveSettings);
            // 
            // chkManageAdvanceQueue
            // 
            this.chkManageAdvanceQueue.AutoSize = true;
            this.chkManageAdvanceQueue.Location = new System.Drawing.Point(22, 106);
            this.chkManageAdvanceQueue.Name = "chkManageAdvanceQueue";
            this.chkManageAdvanceQueue.Size = new System.Drawing.Size(191, 17);
            this.chkManageAdvanceQueue.TabIndex = 2;
            this.chkManageAdvanceQueue.Text = "Manage Advanced queue features";
            this.chkManageAdvanceQueue.UseVisualStyleBackColor = true;
            // 
            // chkManageAllPostAdvanceQueue
            // 
            this.chkManageAllPostAdvanceQueue.AutoSize = true;
            this.chkManageAllPostAdvanceQueue.Location = new System.Drawing.Point(22, 75);
            this.chkManageAllPostAdvanceQueue.Name = "chkManageAllPostAdvanceQueue";
            this.chkManageAllPostAdvanceQueue.Size = new System.Drawing.Size(225, 17);
            this.chkManageAllPostAdvanceQueue.TabIndex = 1;
            this.chkManageAllPostAdvanceQueue.Text = "Manage all posts advance queue features";
            this.chkManageAllPostAdvanceQueue.UseVisualStyleBackColor = true;
            // 
            // chkShowSwitchPostsButtons
            // 
            this.chkShowSwitchPostsButtons.AutoSize = true;
            this.chkShowSwitchPostsButtons.Location = new System.Drawing.Point(22, 46);
            this.chkShowSwitchPostsButtons.Name = "chkShowSwitchPostsButtons";
            this.chkShowSwitchPostsButtons.Size = new System.Drawing.Size(152, 17);
            this.chkShowSwitchPostsButtons.TabIndex = 0;
            this.chkShowSwitchPostsButtons.Text = "Show switch posts buttons";
            this.chkShowSwitchPostsButtons.UseVisualStyleBackColor = true;
            // 
            // gbConnProps
            // 
            this.gbConnProps.Controls.Add(this.lblCtrNoChar);
            this.gbConnProps.Controls.Add(this.label2);
            this.gbConnProps.Controls.Add(this.cbPost);
            this.gbConnProps.Controls.Add(this.label3);
            this.gbConnProps.Controls.Add(this.cbCounterNo);
            this.gbConnProps.Controls.Add(this.tbStation);
            this.gbConnProps.Controls.Add(this.label9);
            this.gbConnProps.Controls.Add(this.btnSaveReconnect);
            this.gbConnProps.Controls.Add(this.tbPort);
            this.gbConnProps.Controls.Add(this.label5);
            this.gbConnProps.Controls.Add(this.tbServer);
            this.gbConnProps.Controls.Add(this.label4);
            this.gbConnProps.Location = new System.Drawing.Point(15, 16);
            this.gbConnProps.Name = "gbConnProps";
            this.gbConnProps.Size = new System.Drawing.Size(254, 285);
            this.gbConnProps.TabIndex = 24;
            this.gbConnProps.TabStop = false;
            this.gbConnProps.Text = "Connection Properties";
            // 
            // lblCtrNoChar
            // 
            this.lblCtrNoChar.AutoSize = true;
            this.lblCtrNoChar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCtrNoChar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCtrNoChar.Location = new System.Drawing.Point(164, 76);
            this.lblCtrNoChar.Margin = new System.Windows.Forms.Padding(5);
            this.lblCtrNoChar.Name = "lblCtrNoChar";
            this.lblCtrNoChar.Padding = new System.Windows.Forms.Padding(3);
            this.lblCtrNoChar.Size = new System.Drawing.Size(23, 21);
            this.lblCtrNoChar.TabIndex = 24;
            this.lblCtrNoChar.Text = "A";
            this.lblCtrNoChar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Station Name";
            // 
            // cbPost
            // 
            this.cbPost.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPost.FormattingEnabled = true;
            this.cbPost.Items.AddRange(new object[] {
            "POST0"});
            this.cbPost.Location = new System.Drawing.Point(102, 108);
            this.cbPost.MaxLength = 10;
            this.cbPost.Name = "cbPost";
            this.cbPost.Size = new System.Drawing.Size(110, 21);
            this.cbPost.TabIndex = 23;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Post";
            // 
            // cbCounterNo
            // 
            this.cbCounterNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCounterNo.FormattingEnabled = true;
            this.cbCounterNo.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.cbCounterNo.Location = new System.Drawing.Point(103, 76);
            this.cbCounterNo.MaxLength = 1;
            this.cbCounterNo.Name = "cbCounterNo";
            this.cbCounterNo.Size = new System.Drawing.Size(43, 21);
            this.cbCounterNo.TabIndex = 22;
            this.cbCounterNo.SelectedIndexChanged += new System.EventHandler(this.OnCbCounterIndexChanged);
            // 
            // tbStation
            // 
            this.tbStation.Location = new System.Drawing.Point(102, 43);
            this.tbStation.MaxLength = 32;
            this.tbStation.Name = "tbStation";
            this.tbStation.Size = new System.Drawing.Size(110, 20);
            this.tbStation.TabIndex = 15;
            this.tbStation.Text = "CALL";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(26, 79);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(64, 13);
            this.label9.TabIndex = 21;
            this.label9.Text = "Counter No.";
            // 
            // btnSaveReconnect
            // 
            this.btnSaveReconnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveReconnect.Location = new System.Drawing.Point(116, 233);
            this.btnSaveReconnect.Name = "btnSaveReconnect";
            this.btnSaveReconnect.Size = new System.Drawing.Size(119, 33);
            this.btnSaveReconnect.TabIndex = 16;
            this.btnSaveReconnect.Text = "&Save and Reconnect";
            this.btnSaveReconnect.UseVisualStyleBackColor = true;
            this.btnSaveReconnect.Click += new System.EventHandler(this.OnSaveSettingsReconnect);
            // 
            // tbPort
            // 
            this.tbPort.Location = new System.Drawing.Point(102, 178);
            this.tbPort.MaxLength = 6;
            this.tbPort.Name = "tbPort";
            this.tbPort.Size = new System.Drawing.Size(44, 20);
            this.tbPort.TabIndex = 20;
            this.tbPort.Text = "2345";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(26, 147);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Server";
            // 
            // tbServer
            // 
            this.tbServer.Location = new System.Drawing.Point(102, 144);
            this.tbServer.MaxLength = 15;
            this.tbServer.Name = "tbServer";
            this.tbServer.Size = new System.Drawing.Size(110, 20);
            this.tbServer.TabIndex = 19;
            this.tbServer.Text = "127.0.0.1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 181);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "Port";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.ClientSize = new System.Drawing.Size(691, 486);
            this.Controls.Add(this.mainTab);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnAbout);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(589, 434);
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Queue Caller";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            this.mainTab.ResumeLayout(false);
            this.tabPageMain.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.postsBtnDiv.ResumeLayout(false);
            this.tabProcessing.ResumeLayout(false);
            this.tabProcessing.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridJobs)).EndInit();
            this.tabFinished.ResumeLayout(false);
            this.tabFinished.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridJobsFin)).EndInit();
            this.tabOptions.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbConnProps.ResumeLayout(false);
            this.gbConnProps.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAbout;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Label lblNumber;
        private System.Windows.Forms.Label capCurrNumber;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.TabControl mainTab;
        private System.Windows.Forms.TabPage tabPageMain;
        private System.Windows.Forms.TabPage tabProcessing;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DataGridView gridJobs;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.TabPage tabFinished;
        private System.Windows.Forms.DataGridView gridJobsFin;
        private System.Windows.Forms.Button btnRefreshFin;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TabPage tabOptions;
        private System.Windows.Forms.ComboBox cbPost;
        private System.Windows.Forms.ComboBox cbCounterNo;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbPort;
        private System.Windows.Forms.TextBox tbServer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSaveReconnect;
        private System.Windows.Forms.TextBox tbStation;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button btnCall;
        private System.Windows.Forms.Label capStation;
        private System.Windows.Forms.Label capNumber;
        private System.Windows.Forms.Label capNext;
        private System.Windows.Forms.Label lblStation;
        private System.Windows.Forms.Label lblPost;
        private System.Windows.Forms.Label lblQueueCount;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.TableLayoutPanel postsBtnDiv;
		private System.Windows.Forms.Button btnChangePost0;
		private System.Windows.Forms.Button btnChangePost1;
		private System.Windows.Forms.Button btnChangePost2;
		private System.Windows.Forms.Button btnChangePost3;
		private System.Windows.Forms.Button btnChangePost4;
		private System.Windows.Forms.Button btnChangePost5;
		private System.Windows.Forms.Button btnChangePost6;
		private System.Windows.Forms.Button btnChangePost7;
		private System.Windows.Forms.Button btnChangePost8;
		private System.Windows.Forms.Button btnChangePost9;
		private System.Windows.Forms.ToolTip toolTip;
		private System.Windows.Forms.GroupBox gbConnProps;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox chkShowSwitchPostsButtons;
		private System.Windows.Forms.Label lblCtrNoChar;
		private System.Windows.Forms.CheckBox chkManageAllPostAdvanceQueue;
		private System.Windows.Forms.CheckBox chkManageAdvanceQueue;
		private System.Windows.Forms.Button btnSaveSettings;
	}
}

