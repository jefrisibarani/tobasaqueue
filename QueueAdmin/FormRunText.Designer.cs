#region License
/*
    Sotware Antrian Tobasa
    Copyright (C) 2015-2025  Jefri Sibarani

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
    partial class FormRunText
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRunText));
            this.label1 = new System.Windows.Forms.Label();
            this.txtStation = new System.Windows.Forms.TextBox();
            this.btnAction = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblRunText = new System.Windows.Forms.Label();
            this.txtRunText = new System.Windows.Forms.TextBox();
            this.chkSticky = new System.Windows.Forms.CheckBox();
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Station Name";
            // 
            // txtStation
            // 
            this.txtStation.Location = new System.Drawing.Point(120, 29);
            this.txtStation.Name = "txtStation";
            this.txtStation.Size = new System.Drawing.Size(178, 20);
            this.txtStation.TabIndex = 1;
            // 
            // btnAction
            // 
            this.btnAction.Location = new System.Drawing.Point(275, 8);
            this.btnAction.Name = "btnAction";
            this.btnAction.Size = new System.Drawing.Size(75, 23);
            this.btnAction.TabIndex = 5;
            this.btnAction.Text = "&Update";
            this.btnAction.UseVisualStyleBackColor = true;
            this.btnAction.Click += new System.EventHandler(this.OnAction);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(356, 8);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.OnClose);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.panel1.Controls.Add(this.btnAction);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Location = new System.Drawing.Point(0, 227);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(450, 40);
            this.panel1.TabIndex = 9;
            // 
            // lblRunText
            // 
            this.lblRunText.AutoSize = true;
            this.lblRunText.Location = new System.Drawing.Point(22, 118);
            this.lblRunText.Name = "lblRunText";
            this.lblRunText.Size = new System.Drawing.Size(71, 13);
            this.lblRunText.TabIndex = 10;
            this.lblRunText.Text = "Running Text";
            // 
            // txtRunText
            // 
            this.txtRunText.Location = new System.Drawing.Point(120, 118);
            this.txtRunText.Multiline = true;
            this.txtRunText.Name = "txtRunText";
            this.txtRunText.Size = new System.Drawing.Size(311, 70);
            this.txtRunText.TabIndex = 4;
            // 
            // chkSticky
            // 
            this.chkSticky.AutoSize = true;
            this.chkSticky.Location = new System.Drawing.Point(120, 61);
            this.chkSticky.Name = "chkSticky";
            this.chkSticky.Size = new System.Drawing.Size(55, 17);
            this.chkSticky.TabIndex = 2;
            this.chkSticky.Text = "Sticky";
            this.chkSticky.UseVisualStyleBackColor = true;
            // 
            // chkActive
            // 
            this.chkActive.AutoSize = true;
            this.chkActive.Location = new System.Drawing.Point(120, 86);
            this.chkActive.Name = "chkActive";
            this.chkActive.Size = new System.Drawing.Size(59, 17);
            this.chkActive.TabIndex = 3;
            this.chkActive.Text = "Active ";
            this.chkActive.UseVisualStyleBackColor = true;
            // 
            // FormRunText
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(448, 264);
            this.Controls.Add(this.chkActive);
            this.Controls.Add(this.chkSticky);
            this.Controls.Add(this.txtRunText);
            this.Controls.Add(this.lblRunText);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtStation);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormRunText";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Form Running Text";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtStation;
        private System.Windows.Forms.Button btnAction;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblRunText;
        private System.Windows.Forms.TextBox txtRunText;
        private System.Windows.Forms.CheckBox chkSticky;
        private System.Windows.Forms.CheckBox chkActive;
    }
}