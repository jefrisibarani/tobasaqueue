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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.divMain = new System.Windows.Forms.TableLayoutPanel();
            this.divContent = new System.Windows.Forms.TableLayoutPanel();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.pnlHeaderDiv = new System.Windows.Forms.TableLayoutPanel();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.picHeader = new System.Windows.Forms.PictureBox();
            this.divMenu = new System.Windows.Forms.TableLayoutPanel();
            this.divPost0 = new System.Windows.Forms.TableLayoutPanel();
            this.lblPnl0 = new System.Windows.Forms.Label();
            this.picBtnPnl0 = new System.Windows.Forms.PictureBox();
            this.divPost1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblPnl1 = new System.Windows.Forms.Label();
            this.picBtnPnl1 = new System.Windows.Forms.PictureBox();
            this.divPost2 = new System.Windows.Forms.TableLayoutPanel();
            this.lblPnl2 = new System.Windows.Forms.Label();
            this.picBtnPnl2 = new System.Windows.Forms.PictureBox();
            this.divPost3 = new System.Windows.Forms.TableLayoutPanel();
            this.lblPnl3 = new System.Windows.Forms.Label();
            this.picBtnPnl3 = new System.Windows.Forms.PictureBox();
            this.divPost4 = new System.Windows.Forms.TableLayoutPanel();
            this.lblPnl4 = new System.Windows.Forms.Label();
            this.picBtnPnl4 = new System.Windows.Forms.PictureBox();
            this.pnlMarquee = new System.Windows.Forms.Panel();
            this.imgListMarquee = new System.Windows.Forms.ImageList(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.runTextBottom = new Tobasa.RuntextLabel();
            this.divMain.SuspendLayout();
            this.divContent.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.pnlHeaderDiv.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picHeader)).BeginInit();
            this.divMenu.SuspendLayout();
            this.divPost0.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBtnPnl0)).BeginInit();
            this.divPost1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBtnPnl1)).BeginInit();
            this.divPost2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBtnPnl2)).BeginInit();
            this.divPost3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBtnPnl3)).BeginInit();
            this.divPost4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBtnPnl4)).BeginInit();
            this.pnlMarquee.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // divMain
            // 
            this.divMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.divMain.AutoSize = true;
            this.divMain.ColumnCount = 1;
            this.divMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.divMain.Controls.Add(this.divContent, 0, 0);
            this.divMain.Controls.Add(this.pnlMarquee, 0, 1);
            this.divMain.Location = new System.Drawing.Point(0, 0);
            this.divMain.Name = "divMain";
            this.divMain.RowCount = 2;
            this.divMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.divMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.divMain.Size = new System.Drawing.Size(784, 570);
            this.divMain.TabIndex = 0;
            // 
            // divContent
            // 
            this.divContent.ColumnCount = 1;
            this.divContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.divContent.Controls.Add(this.pnlHeader, 0, 0);
            this.divContent.Controls.Add(this.divMenu, 0, 1);
            this.divContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.divContent.Location = new System.Drawing.Point(3, 3);
            this.divContent.Name = "divContent";
            this.divContent.RowCount = 2;
            this.divContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.divContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.divContent.Size = new System.Drawing.Size(778, 507);
            this.divContent.TabIndex = 12;
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.LightSteelBlue;
            this.pnlHeader.BackgroundImage = global::Tobasa.Properties.Resources.DisplayHeaderBg;
            this.pnlHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlHeader.Controls.Add(this.pnlHeaderDiv);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHeader.Location = new System.Drawing.Point(16, 0);
            this.pnlHeader.Margin = new System.Windows.Forms.Padding(16, 0, 16, 0);
            this.pnlHeader.MinimumSize = new System.Drawing.Size(0, 100);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(746, 126);
            this.pnlHeader.TabIndex = 11;
            // 
            // pnlHeaderDiv
            // 
            this.pnlHeaderDiv.BackColor = System.Drawing.Color.Transparent;
            this.pnlHeaderDiv.ColumnCount = 2;
            this.pnlHeaderDiv.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.pnlHeaderDiv.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 82F));
            this.pnlHeaderDiv.Controls.Add(this.picLogo, 0, 0);
            this.pnlHeaderDiv.Controls.Add(this.picHeader, 1, 0);
            this.pnlHeaderDiv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHeaderDiv.Location = new System.Drawing.Point(0, 0);
            this.pnlHeaderDiv.Margin = new System.Windows.Forms.Padding(0);
            this.pnlHeaderDiv.MinimumSize = new System.Drawing.Size(0, 100);
            this.pnlHeaderDiv.Name = "pnlHeaderDiv";
            this.pnlHeaderDiv.RowCount = 1;
            this.pnlHeaderDiv.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlHeaderDiv.Size = new System.Drawing.Size(746, 126);
            this.pnlHeaderDiv.TabIndex = 2;
            // 
            // picLogo
            // 
            this.picLogo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picLogo.BackColor = System.Drawing.Color.Transparent;
            this.picLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.picLogo.Image = global::Tobasa.Properties.Resources.QueueLogo150;
            this.picLogo.Location = new System.Drawing.Point(5, 5);
            this.picLogo.Margin = new System.Windows.Forms.Padding(5);
            this.picLogo.MinimumSize = new System.Drawing.Size(0, 100);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(124, 116);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picLogo.TabIndex = 0;
            this.picLogo.TabStop = false;
            // 
            // picHeader
            // 
            this.picHeader.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picHeader.Image = global::Tobasa.Properties.Resources.DisplayHeaderImg;
            this.picHeader.Location = new System.Drawing.Point(136, 2);
            this.picHeader.Margin = new System.Windows.Forms.Padding(2);
            this.picHeader.MinimumSize = new System.Drawing.Size(0, 100);
            this.picHeader.Name = "picHeader";
            this.picHeader.Size = new System.Drawing.Size(608, 122);
            this.picHeader.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picHeader.TabIndex = 1;
            this.picHeader.TabStop = false;
            // 
            // divMenu
            // 
            this.divMenu.AutoSize = true;
            this.divMenu.ColumnCount = 1;
            this.divMenu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.divMenu.Controls.Add(this.divPost0, 0, 0);
            this.divMenu.Controls.Add(this.divPost1, 0, 1);
            this.divMenu.Controls.Add(this.divPost2, 0, 2);
            this.divMenu.Controls.Add(this.divPost3, 0, 3);
            this.divMenu.Controls.Add(this.divPost4, 0, 4);
            this.divMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.divMenu.Location = new System.Drawing.Point(16, 151);
            this.divMenu.Margin = new System.Windows.Forms.Padding(16, 25, 16, 3);
            this.divMenu.Name = "divMenu";
            this.divMenu.RowCount = 5;
            this.divMenu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.divMenu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.divMenu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.divMenu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.divMenu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.divMenu.Size = new System.Drawing.Size(746, 353);
            this.divMenu.TabIndex = 10;
            // 
            // divPost0
            // 
            this.divPost0.ColumnCount = 2;
            this.divPost0.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.divPost0.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.divPost0.Controls.Add(this.lblPnl0, 0, 0);
            this.divPost0.Controls.Add(this.picBtnPnl0, 1, 0);
            this.divPost0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.divPost0.Location = new System.Drawing.Point(3, 3);
            this.divPost0.Name = "divPost0";
            this.divPost0.RowCount = 1;
            this.divPost0.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.divPost0.Size = new System.Drawing.Size(740, 64);
            this.divPost0.TabIndex = 1;
            // 
            // lblPnl0
            // 
            this.lblPnl0.AutoSize = true;
            this.lblPnl0.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.lblPnl0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPnl0.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPnl0.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblPnl0.Image = global::Tobasa.Properties.Resources.MenuLabelBg;
            this.lblPnl0.Location = new System.Drawing.Point(0, 10);
            this.lblPnl0.Margin = new System.Windows.Forms.Padding(0, 10, 20, 10);
            this.lblPnl0.Name = "lblPnl0";
            this.lblPnl0.Size = new System.Drawing.Size(498, 44);
            this.lblPnl0.TabIndex = 9;
            this.lblPnl0.Text = "Label Menu 0";
            this.lblPnl0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblPnl0.Resize += new System.EventHandler(this.OnLabelResize);
            // 
            // picBtnPnl0
            // 
            this.picBtnPnl0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picBtnPnl0.Image = global::Tobasa.Properties.Resources.ButtonGreenOff;
            this.picBtnPnl0.Location = new System.Drawing.Point(528, 10);
            this.picBtnPnl0.Margin = new System.Windows.Forms.Padding(10);
            this.picBtnPnl0.Name = "picBtnPnl0";
            this.picBtnPnl0.Size = new System.Drawing.Size(202, 44);
            this.picBtnPnl0.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBtnPnl0.TabIndex = 8;
            this.picBtnPnl0.TabStop = false;
            this.picBtnPnl0.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnMouseDown);
            this.picBtnPnl0.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnMouseUp);
            // 
            // divPost1
            // 
            this.divPost1.ColumnCount = 2;
            this.divPost1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.divPost1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.divPost1.Controls.Add(this.lblPnl1, 0, 0);
            this.divPost1.Controls.Add(this.picBtnPnl1, 1, 0);
            this.divPost1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.divPost1.Location = new System.Drawing.Point(3, 73);
            this.divPost1.Name = "divPost1";
            this.divPost1.RowCount = 1;
            this.divPost1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.divPost1.Size = new System.Drawing.Size(740, 64);
            this.divPost1.TabIndex = 14;
            // 
            // lblPnl1
            // 
            this.lblPnl1.AutoSize = true;
            this.lblPnl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.lblPnl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPnl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPnl1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblPnl1.Image = global::Tobasa.Properties.Resources.MenuLabelBg;
            this.lblPnl1.Location = new System.Drawing.Point(0, 10);
            this.lblPnl1.Margin = new System.Windows.Forms.Padding(0, 10, 20, 10);
            this.lblPnl1.Name = "lblPnl1";
            this.lblPnl1.Size = new System.Drawing.Size(498, 44);
            this.lblPnl1.TabIndex = 6;
            this.lblPnl1.Text = "Label Menu 1";
            this.lblPnl1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblPnl1.Resize += new System.EventHandler(this.OnLabelResize);
            // 
            // picBtnPnl1
            // 
            this.picBtnPnl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picBtnPnl1.Image = global::Tobasa.Properties.Resources.ButtonGreenOff;
            this.picBtnPnl1.Location = new System.Drawing.Point(528, 10);
            this.picBtnPnl1.Margin = new System.Windows.Forms.Padding(10);
            this.picBtnPnl1.Name = "picBtnPnl1";
            this.picBtnPnl1.Size = new System.Drawing.Size(202, 44);
            this.picBtnPnl1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBtnPnl1.TabIndex = 5;
            this.picBtnPnl1.TabStop = false;
            this.picBtnPnl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnMouseDown);
            this.picBtnPnl1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnMouseUp);
            // 
            // divPost2
            // 
            this.divPost2.ColumnCount = 2;
            this.divPost2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.divPost2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.divPost2.Controls.Add(this.lblPnl2, 0, 0);
            this.divPost2.Controls.Add(this.picBtnPnl2, 1, 0);
            this.divPost2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.divPost2.Location = new System.Drawing.Point(3, 143);
            this.divPost2.Name = "divPost2";
            this.divPost2.RowCount = 1;
            this.divPost2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.divPost2.Size = new System.Drawing.Size(740, 64);
            this.divPost2.TabIndex = 15;
            // 
            // lblPnl2
            // 
            this.lblPnl2.AutoSize = true;
            this.lblPnl2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.lblPnl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPnl2.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPnl2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblPnl2.Image = global::Tobasa.Properties.Resources.MenuLabelBg;
            this.lblPnl2.Location = new System.Drawing.Point(0, 10);
            this.lblPnl2.Margin = new System.Windows.Forms.Padding(0, 10, 20, 10);
            this.lblPnl2.Name = "lblPnl2";
            this.lblPnl2.Size = new System.Drawing.Size(498, 44);
            this.lblPnl2.TabIndex = 14;
            this.lblPnl2.Text = "Label Menu 2";
            this.lblPnl2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblPnl2.Resize += new System.EventHandler(this.OnLabelResize);
            // 
            // picBtnPnl2
            // 
            this.picBtnPnl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picBtnPnl2.Image = global::Tobasa.Properties.Resources.ButtonGreenOff;
            this.picBtnPnl2.Location = new System.Drawing.Point(528, 10);
            this.picBtnPnl2.Margin = new System.Windows.Forms.Padding(10);
            this.picBtnPnl2.Name = "picBtnPnl2";
            this.picBtnPnl2.Size = new System.Drawing.Size(202, 44);
            this.picBtnPnl2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBtnPnl2.TabIndex = 13;
            this.picBtnPnl2.TabStop = false;
            this.picBtnPnl2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnMouseDown);
            this.picBtnPnl2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnMouseUp);
            // 
            // divPost3
            // 
            this.divPost3.ColumnCount = 2;
            this.divPost3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.divPost3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.divPost3.Controls.Add(this.lblPnl3, 0, 0);
            this.divPost3.Controls.Add(this.picBtnPnl3, 1, 0);
            this.divPost3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.divPost3.Location = new System.Drawing.Point(3, 213);
            this.divPost3.Name = "divPost3";
            this.divPost3.RowCount = 1;
            this.divPost3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.divPost3.Size = new System.Drawing.Size(740, 64);
            this.divPost3.TabIndex = 16;
            // 
            // lblPnl3
            // 
            this.lblPnl3.AutoSize = true;
            this.lblPnl3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.lblPnl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPnl3.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPnl3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblPnl3.Image = global::Tobasa.Properties.Resources.MenuLabelBg;
            this.lblPnl3.Location = new System.Drawing.Point(0, 10);
            this.lblPnl3.Margin = new System.Windows.Forms.Padding(0, 10, 20, 10);
            this.lblPnl3.Name = "lblPnl3";
            this.lblPnl3.Size = new System.Drawing.Size(498, 44);
            this.lblPnl3.TabIndex = 15;
            this.lblPnl3.Text = "Label Menu 3";
            this.lblPnl3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblPnl3.Resize += new System.EventHandler(this.OnLabelResize);
            // 
            // picBtnPnl3
            // 
            this.picBtnPnl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picBtnPnl3.Image = global::Tobasa.Properties.Resources.ButtonGreenOff;
            this.picBtnPnl3.Location = new System.Drawing.Point(528, 10);
            this.picBtnPnl3.Margin = new System.Windows.Forms.Padding(10);
            this.picBtnPnl3.Name = "picBtnPnl3";
            this.picBtnPnl3.Size = new System.Drawing.Size(202, 44);
            this.picBtnPnl3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBtnPnl3.TabIndex = 14;
            this.picBtnPnl3.TabStop = false;
            this.picBtnPnl3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnMouseDown);
            this.picBtnPnl3.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnMouseUp);
            // 
            // divPost4
            // 
            this.divPost4.ColumnCount = 2;
            this.divPost4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.divPost4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.divPost4.Controls.Add(this.lblPnl4, 0, 0);
            this.divPost4.Controls.Add(this.picBtnPnl4, 1, 0);
            this.divPost4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.divPost4.Location = new System.Drawing.Point(3, 283);
            this.divPost4.Name = "divPost4";
            this.divPost4.RowCount = 1;
            this.divPost4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.divPost4.Size = new System.Drawing.Size(740, 67);
            this.divPost4.TabIndex = 17;
            // 
            // lblPnl4
            // 
            this.lblPnl4.AutoSize = true;
            this.lblPnl4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.lblPnl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPnl4.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPnl4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblPnl4.Image = global::Tobasa.Properties.Resources.MenuLabelBg;
            this.lblPnl4.Location = new System.Drawing.Point(0, 10);
            this.lblPnl4.Margin = new System.Windows.Forms.Padding(0, 10, 20, 10);
            this.lblPnl4.Name = "lblPnl4";
            this.lblPnl4.Size = new System.Drawing.Size(498, 47);
            this.lblPnl4.TabIndex = 15;
            this.lblPnl4.Text = "Label Menu 4";
            this.lblPnl4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblPnl4.Resize += new System.EventHandler(this.OnLabelResize);
            // 
            // picBtnPnl4
            // 
            this.picBtnPnl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picBtnPnl4.Image = global::Tobasa.Properties.Resources.ButtonGreenOff;
            this.picBtnPnl4.Location = new System.Drawing.Point(528, 10);
            this.picBtnPnl4.Margin = new System.Windows.Forms.Padding(10);
            this.picBtnPnl4.Name = "picBtnPnl4";
            this.picBtnPnl4.Size = new System.Drawing.Size(202, 47);
            this.picBtnPnl4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBtnPnl4.TabIndex = 14;
            this.picBtnPnl4.TabStop = false;
            this.picBtnPnl4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnMouseDown);
            this.picBtnPnl4.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnMouseUp);
            // 
            // pnlMarquee
            // 
            this.pnlMarquee.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.pnlMarquee.Controls.Add(this.tableLayoutPanel1);
            this.pnlMarquee.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMarquee.Location = new System.Drawing.Point(0, 513);
            this.pnlMarquee.Margin = new System.Windows.Forms.Padding(0);
            this.pnlMarquee.Name = "pnlMarquee";
            this.pnlMarquee.Size = new System.Drawing.Size(784, 57);
            this.pnlMarquee.TabIndex = 0;
            // 
            // imgListMarquee
            // 
            this.imgListMarquee.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgListMarquee.ImageStream")));
            this.imgListMarquee.TransparentColor = System.Drawing.Color.Transparent;
            this.imgListMarquee.Images.SetKeyName(0, "stock-circle.png");
            this.imgListMarquee.Images.SetKeyName(1, "add.png");
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.runTextBottom, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(784, 57);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // runTextBottom
            // 
            this.runTextBottom.AutoSize = true;
            this.runTextBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.runTextBottom.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold);
            this.runTextBottom.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.runTextBottom.Location = new System.Drawing.Point(3, 0);
            this.runTextBottom.Name = "runTextBottom";
            this.runTextBottom.Size = new System.Drawing.Size(778, 37);
            this.runTextBottom.TabIndex = 0;
            this.runTextBottom.Text = "Selamat Datang";
            this.runTextBottom.UseCompatibleTextRendering = true;
            this.runTextBottom.Resize += new System.EventHandler(this.OnLabelResize);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.divMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Queue Ticket";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            this.divMain.ResumeLayout(false);
            this.divContent.ResumeLayout(false);
            this.divContent.PerformLayout();
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeaderDiv.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picHeader)).EndInit();
            this.divMenu.ResumeLayout(false);
            this.divPost0.ResumeLayout(false);
            this.divPost0.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBtnPnl0)).EndInit();
            this.divPost1.ResumeLayout(false);
            this.divPost1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBtnPnl1)).EndInit();
            this.divPost2.ResumeLayout(false);
            this.divPost2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBtnPnl2)).EndInit();
            this.divPost3.ResumeLayout(false);
            this.divPost3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBtnPnl3)).EndInit();
            this.divPost4.ResumeLayout(false);
            this.divPost4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBtnPnl4)).EndInit();
            this.pnlMarquee.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel divMain;
        private System.Windows.Forms.Panel pnlMarquee;
        private System.Windows.Forms.TableLayoutPanel divContent;
        private System.Windows.Forms.TableLayoutPanel divMenu;
        private System.Windows.Forms.TableLayoutPanel divPost0;
        private System.Windows.Forms.PictureBox picBtnPnl0;
        private System.Windows.Forms.TableLayoutPanel divPost1;
        private System.Windows.Forms.PictureBox picBtnPnl1;
        private System.Windows.Forms.TableLayoutPanel divPost2;
        private System.Windows.Forms.PictureBox picBtnPnl2;
        private System.Windows.Forms.TableLayoutPanel divPost3;
        private System.Windows.Forms.PictureBox picBtnPnl3;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.TableLayoutPanel pnlHeaderDiv;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.PictureBox picHeader;
        private System.Windows.Forms.ImageList imgListMarquee;
        private System.Windows.Forms.Label lblPnl0;
        private System.Windows.Forms.Label lblPnl1;
        private System.Windows.Forms.Label lblPnl2;
        private System.Windows.Forms.Label lblPnl3;
        private System.Windows.Forms.TableLayoutPanel divPost4;
        private System.Windows.Forms.Label lblPnl4;
        private System.Windows.Forms.PictureBox picBtnPnl4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private RuntextLabel runTextBottom;
    }
}

