namespace Tobasa
{
    partial class MainForm
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
            this.tabProcess = new System.Windows.Forms.TabPage();
            this.label14 = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.gridJobs = new System.Windows.Forms.DataGridView();
            this.tabFinish = new System.Windows.Forms.TabPage();
            this.label13 = new System.Windows.Forms.Label();
            this.btnRefreshFin = new System.Windows.Forms.Button();
            this.gridJobsFin = new System.Windows.Forms.DataGridView();
            this.tabOptions = new System.Windows.Forms.TabPage();
            this.cbPost = new System.Windows.Forms.ComboBox();
            this.cbCounterNo = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbPort = new System.Windows.Forms.TextBox();
            this.tbServer = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.tbStation = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.mainTab.SuspendLayout();
            this.tabPageMain.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.tabProcess.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridJobs)).BeginInit();
            this.tabFinish.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridJobsFin)).BeginInit();
            this.tabOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAbout
            // 
            this.btnAbout.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnAbout.Location = new System.Drawing.Point(670, 608);
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
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.White;
            this.lblStatus.Location = new System.Drawing.Point(0, 619);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Padding = new System.Windows.Forms.Padding(10, 0, 0, 2);
            this.lblStatus.Size = new System.Drawing.Size(136, 15);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.Text = "Not Connected to Server ";
            // 
            // mainTab
            // 
            this.mainTab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainTab.Controls.Add(this.tabPageMain);
            this.mainTab.Controls.Add(this.tabProcess);
            this.mainTab.Controls.Add(this.tabFinish);
            this.mainTab.Controls.Add(this.tabOptions);
            this.mainTab.Location = new System.Drawing.Point(5, 5);
            this.mainTab.Name = "mainTab";
            this.mainTab.SelectedIndex = 0;
            this.mainTab.Size = new System.Drawing.Size(708, 597);
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
            this.tabPageMain.Size = new System.Drawing.Size(700, 571);
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
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 5;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(694, 565);
            this.tableLayoutPanel3.TabIndex = 15;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.MediumBlue;
            this.label12.Location = new System.Drawing.Point(487, 479);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(153, 86);
            this.label12.TabIndex = 14;
            this.label12.Text = "F12 : Next Number";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // capStation
            // 
            this.capStation.AutoSize = true;
            this.capStation.BackColor = System.Drawing.Color.CadetBlue;
            this.capStation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.capStation.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.capStation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.capStation.Location = new System.Drawing.Point(48, 56);
            this.capStation.Margin = new System.Windows.Forms.Padding(0);
            this.capStation.Name = "capStation";
            this.capStation.Size = new System.Drawing.Size(159, 56);
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
            this.capNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.capNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.capNumber.Location = new System.Drawing.Point(207, 56);
            this.capNumber.Margin = new System.Windows.Forms.Padding(0);
            this.capNumber.Name = "capNumber";
            this.capNumber.Size = new System.Drawing.Size(277, 56);
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
            this.capNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.capNext.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.capNext.Location = new System.Drawing.Point(484, 56);
            this.capNext.Margin = new System.Windows.Forms.Padding(0);
            this.capNext.Name = "capNext";
            this.capNext.Size = new System.Drawing.Size(159, 56);
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
            this.lblStation.Location = new System.Drawing.Point(48, 112);
            this.lblStation.Margin = new System.Windows.Forms.Padding(0);
            this.lblStation.Name = "lblStation";
            this.lblStation.Size = new System.Drawing.Size(159, 113);
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
            this.lblPost.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPost.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblPost.Location = new System.Drawing.Point(207, 112);
            this.lblPost.Margin = new System.Windows.Forms.Padding(0);
            this.lblPost.Name = "lblPost";
            this.lblPost.Size = new System.Drawing.Size(277, 113);
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
            this.lblQueueCount.Location = new System.Drawing.Point(484, 112);
            this.lblQueueCount.Margin = new System.Windows.Forms.Padding(0);
            this.lblQueueCount.Name = "lblQueueCount";
            this.lblQueueCount.Size = new System.Drawing.Size(159, 113);
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
            this.panel2.Location = new System.Drawing.Point(48, 225);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(159, 254);
            this.panel2.TabIndex = 21;
            // 
            // btnCall
            // 
            this.btnCall.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCall.BackColor = System.Drawing.Color.AliceBlue;
            this.btnCall.BackgroundImage = global::Tobasa.Properties.Resources.lblgreen;
            this.btnCall.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCall.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnCall.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnCall.Font = new System.Drawing.Font("Microsoft Sans Serif", 28F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCall.ForeColor = System.Drawing.Color.MintCream;
            this.btnCall.Location = new System.Drawing.Point(15, 50);
            this.btnCall.Margin = new System.Windows.Forms.Padding(0);
            this.btnCall.Name = "btnCall";
            this.btnCall.Size = new System.Drawing.Size(144, 167);
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
            this.panel3.Location = new System.Drawing.Point(207, 225);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(277, 254);
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(277, 254);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // capCurrNumber
            // 
            this.capCurrNumber.AutoSize = true;
            this.capCurrNumber.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.capCurrNumber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.capCurrNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.capCurrNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.capCurrNumber.Location = new System.Drawing.Point(3, 0);
            this.capCurrNumber.Name = "capCurrNumber";
            this.capCurrNumber.Size = new System.Drawing.Size(271, 50);
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
            this.lblNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 68F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumber.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblNumber.Location = new System.Drawing.Point(3, 50);
            this.lblNumber.Name = "lblNumber";
            this.lblNumber.Size = new System.Drawing.Size(271, 204);
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
            this.panel4.Location = new System.Drawing.Point(484, 225);
            this.panel4.Margin = new System.Windows.Forms.Padding(0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(159, 254);
            this.panel4.TabIndex = 23;
            // 
            // btnNext
            // 
            this.btnNext.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnNext.BackColor = System.Drawing.Color.AliceBlue;
            this.btnNext.BackgroundImage = global::Tobasa.Properties.Resources.lblgreen;
            this.btnNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnNext.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnNext.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 28F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.ForeColor = System.Drawing.Color.MintCream;
            this.btnNext.Location = new System.Drawing.Point(0, 50);
            this.btnNext.Margin = new System.Windows.Forms.Padding(0);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(140, 167);
            this.btnNext.TabIndex = 4;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = false;
            this.btnNext.Click += new System.EventHandler(this.OnNextNumber);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.MediumBlue;
            this.label11.Location = new System.Drawing.Point(51, 479);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(153, 86);
            this.label11.TabIndex = 13;
            this.label11.Text = "F9 : Recall";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabProcess
            // 
            this.tabProcess.BackColor = System.Drawing.SystemColors.Window;
            this.tabProcess.Controls.Add(this.label14);
            this.tabProcess.Controls.Add(this.btnRefresh);
            this.tabProcess.Controls.Add(this.gridJobs);
            this.tabProcess.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabProcess.Location = new System.Drawing.Point(4, 22);
            this.tabProcess.Name = "tabProcess";
            this.tabProcess.Padding = new System.Windows.Forms.Padding(3);
            this.tabProcess.Size = new System.Drawing.Size(700, 571);
            this.tabProcess.TabIndex = 1;
            this.tabProcess.Text = "Penyerahan Obat";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.MediumBlue;
            this.label14.Location = new System.Drawing.Point(6, 330);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(360, 16);
            this.label14.TabIndex = 7;
            this.label14.Text = "Double klik pada baris tabel untuk proses penyerahan obat";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(615, 330);
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
            this.gridJobs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
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
            this.gridJobs.Size = new System.Drawing.Size(681, 303);
            this.gridJobs.TabIndex = 3;
            this.gridJobs.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.OnGridJobsCellDoubleClick);
            // 
            // tabFinish
            // 
            this.tabFinish.Controls.Add(this.label13);
            this.tabFinish.Controls.Add(this.btnRefreshFin);
            this.tabFinish.Controls.Add(this.gridJobsFin);
            this.tabFinish.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabFinish.Location = new System.Drawing.Point(4, 22);
            this.tabFinish.Name = "tabFinish";
            this.tabFinish.Padding = new System.Windows.Forms.Padding(3);
            this.tabFinish.Size = new System.Drawing.Size(700, 571);
            this.tabFinish.TabIndex = 2;
            this.tabFinish.Text = "Selesai";
            this.tabFinish.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.MediumBlue;
            this.label13.Location = new System.Drawing.Point(10, 277);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(460, 16);
            this.label13.TabIndex = 6;
            this.label13.Text = "Double klik pada baris tabel untuk proses nomor yang telah diambil obatnya";
            // 
            // btnRefreshFin
            // 
            this.btnRefreshFin.Location = new System.Drawing.Point(615, 277);
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
            this.gridJobsFin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
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
            this.gridJobsFin.Size = new System.Drawing.Size(681, 250);
            this.gridJobsFin.TabIndex = 4;
            this.gridJobsFin.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.OnGridJobsFinCellDoubleClick);
            // 
            // tabOptions
            // 
            this.tabOptions.Controls.Add(this.cbPost);
            this.tabOptions.Controls.Add(this.cbCounterNo);
            this.tabOptions.Controls.Add(this.label9);
            this.tabOptions.Controls.Add(this.tbPort);
            this.tabOptions.Controls.Add(this.tbServer);
            this.tabOptions.Controls.Add(this.label4);
            this.tabOptions.Controls.Add(this.label5);
            this.tabOptions.Controls.Add(this.button1);
            this.tabOptions.Controls.Add(this.tbStation);
            this.tabOptions.Controls.Add(this.label3);
            this.tabOptions.Controls.Add(this.label2);
            this.tabOptions.Location = new System.Drawing.Point(4, 22);
            this.tabOptions.Name = "tabOptions";
            this.tabOptions.Padding = new System.Windows.Forms.Padding(3);
            this.tabOptions.Size = new System.Drawing.Size(700, 571);
            this.tabOptions.TabIndex = 3;
            this.tabOptions.Text = "Options";
            this.tabOptions.UseVisualStyleBackColor = true;
            // 
            // cbPost
            // 
            this.cbPost.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPost.FormattingEnabled = true;
            this.cbPost.Items.AddRange(new object[] {
            "POST#0",
            "POST#1",
            "POST#2",
            "POST#3"});
            this.cbPost.Location = new System.Drawing.Point(86, 55);
            this.cbPost.MaxLength = 10;
            this.cbPost.Name = "cbPost";
            this.cbPost.Size = new System.Drawing.Size(110, 21);
            this.cbPost.TabIndex = 23;
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
            "9"});
            this.cbCounterNo.Location = new System.Drawing.Point(273, 17);
            this.cbCounterNo.MaxLength = 1;
            this.cbCounterNo.Name = "cbCounterNo";
            this.cbCounterNo.Size = new System.Drawing.Size(43, 21);
            this.cbCounterNo.TabIndex = 22;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(203, 22);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(64, 13);
            this.label9.TabIndex = 21;
            this.label9.Text = "Counter No.";
            // 
            // tbPort
            // 
            this.tbPort.Location = new System.Drawing.Point(86, 132);
            this.tbPort.MaxLength = 6;
            this.tbPort.MinimumSize = new System.Drawing.Size(4, 30);
            this.tbPort.Name = "tbPort";
            this.tbPort.Size = new System.Drawing.Size(110, 20);
            this.tbPort.TabIndex = 20;
            this.tbPort.Text = "2345";
            // 
            // tbServer
            // 
            this.tbServer.Location = new System.Drawing.Point(86, 95);
            this.tbServer.MaxLength = 15;
            this.tbServer.Name = "tbServer";
            this.tbServer.Size = new System.Drawing.Size(110, 20);
            this.tbServer.TabIndex = 19;
            this.tbServer.Text = "127.0.0.1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 135);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "Port";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 98);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Server";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 198);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(230, 41);
            this.button1.TabIndex = 16;
            this.button1.Text = "&Save and Reconnect";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.OnSaveSettings);
            // 
            // tbStation
            // 
            this.tbStation.Location = new System.Drawing.Point(86, 17);
            this.tbStation.MaxLength = 32;
            this.tbStation.Name = "tbStation";
            this.tbStation.Size = new System.Drawing.Size(110, 20);
            this.tbStation.TabIndex = 15;
            this.tbStation.Text = "CALL";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Post";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Station Name";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.ClientSize = new System.Drawing.Size(720, 634);
            this.Controls.Add(this.mainTab);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnAbout);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(446, 350);
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Queue Caller";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
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
            this.tabProcess.ResumeLayout(false);
            this.tabProcess.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridJobs)).EndInit();
            this.tabFinish.ResumeLayout(false);
            this.tabFinish.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridJobsFin)).EndInit();
            this.tabOptions.ResumeLayout(false);
            this.tabOptions.PerformLayout();
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
        private System.Windows.Forms.TabPage tabProcess;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DataGridView gridJobs;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.TabPage tabFinish;
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
        private System.Windows.Forms.Button button1;
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
    }
}

