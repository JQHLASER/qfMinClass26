namespace qfNet
{
    partial class Form_文件_弹窗2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_文件_弹窗2));
            this.uiListBox1 = new Sunny.UI.UIListBox();
            this.右键 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel2 = new System.Windows.Forms.Panel();
            this.uiTextBox_FileName = new Sunny.UI.UITextBox();
            this.uiLabel_后缀 = new Sunny.UI.UILabel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.uiButton_Yes = new Sunny.UI.UIButton();
            this.uiButton_No = new Sunny.UI.UIButton();
            this.右键.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiListBox1
            // 
            this.uiListBox1.BackColor = System.Drawing.Color.Transparent;
            this.uiListBox1.ContextMenuStrip = this.右键;
            this.uiListBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiListBox1.FillColor = System.Drawing.Color.White;
            this.uiListBox1.Font = new System.Drawing.Font("微软雅黑", 11.89565F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiListBox1.ForeColor = System.Drawing.Color.Gray;
            this.uiListBox1.ForeDisableColor = System.Drawing.Color.Gray;
            this.uiListBox1.HoverColor = System.Drawing.Color.Transparent;
            this.uiListBox1.ItemHeight = 30;
            this.uiListBox1.Items.AddRange(new object[] {
            "2",
            "3",
            "33"});
            this.uiListBox1.Location = new System.Drawing.Point(2, 35);
            this.uiListBox1.Margin = new System.Windows.Forms.Padding(0);
            this.uiListBox1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiListBox1.Name = "uiListBox1";
            this.uiListBox1.Padding = new System.Windows.Forms.Padding(5);
            this.uiListBox1.RectColor = System.Drawing.Color.Transparent;
            this.uiListBox1.Size = new System.Drawing.Size(796, 468);
            this.uiListBox1.Style = Sunny.UI.UIStyle.Custom;
            this.uiListBox1.StyleCustomMode = true;
            this.uiListBox1.TabIndex = 0;
            this.uiListBox1.Text = "uiListBox1";
            this.uiListBox1.文本颜色 = System.Drawing.Color.Gray;
            this.uiListBox1.背景颜色 = System.Drawing.Color.White;
            // 
            // 右键
            // 
            this.右键.Font = new System.Drawing.Font("微软雅黑", 10.01739F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.右键.ImageScalingSize = new System.Drawing.Size(19, 19);
            this.右键.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.删除ToolStripMenuItem});
            this.右键.Name = "右键";
            this.右键.Size = new System.Drawing.Size(113, 30);
            // 
            // 删除ToolStripMenuItem
            // 
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.Size = new System.Drawing.Size(112, 26);
            this.删除ToolStripMenuItem.Text = "删除";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.uiTextBox_FileName);
            this.panel2.Controls.Add(this.uiLabel_后缀);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(2, 503);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(5);
            this.panel2.Size = new System.Drawing.Size(796, 40);
            this.panel2.TabIndex = 2;
            // 
            // uiTextBox_FileName
            // 
            this.uiTextBox_FileName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.uiTextBox_FileName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiTextBox_FileName.FillColor = System.Drawing.Color.White;
            this.uiTextBox_FileName.Font = new System.Drawing.Font("微软雅黑", 10.01739F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiTextBox_FileName.ForeColor = System.Drawing.Color.Gray;
            this.uiTextBox_FileName.ForeDisableColor = System.Drawing.Color.Gray;
            this.uiTextBox_FileName.Location = new System.Drawing.Point(5, 5);
            this.uiTextBox_FileName.Margin = new System.Windows.Forms.Padding(0);
            this.uiTextBox_FileName.Maximum = 2147483647D;
            this.uiTextBox_FileName.Minimum = -2147483648D;
            this.uiTextBox_FileName.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiTextBox_FileName.Name = "uiTextBox_FileName";
            this.uiTextBox_FileName.Padding = new System.Windows.Forms.Padding(5);
            this.uiTextBox_FileName.RectColor = System.Drawing.Color.Silver;
            this.uiTextBox_FileName.Size = new System.Drawing.Size(636, 29);
            this.uiTextBox_FileName.Style = Sunny.UI.UIStyle.Custom;
            this.uiTextBox_FileName.StyleCustomMode = true;
            this.uiTextBox_FileName.TabIndex = 0;
            this.uiTextBox_FileName.Watermark = "";
            this.uiTextBox_FileName.填充颜色 = System.Drawing.Color.White;
            // 
            // uiLabel_后缀
            // 
            this.uiLabel_后缀.BackColor = System.Drawing.Color.Transparent;
            this.uiLabel_后缀.Dock = System.Windows.Forms.DockStyle.Right;
            this.uiLabel_后缀.Font = new System.Drawing.Font("微软雅黑", 10.01739F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel_后缀.ForeColor = System.Drawing.Color.Gray;
            this.uiLabel_后缀.Location = new System.Drawing.Point(641, 5);
            this.uiLabel_后缀.Name = "uiLabel_后缀";
            this.uiLabel_后缀.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.uiLabel_后缀.Size = new System.Drawing.Size(150, 30);
            this.uiLabel_后缀.Style = Sunny.UI.UIStyle.深色;
            this.uiLabel_后缀.TabIndex = 1;
            this.uiLabel_后缀.Text = "ezd(*.ezd)";
            this.uiLabel_后缀.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel1.Controls.Add(this.uiButton_Yes, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.uiButton_No, 3, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(2, 543);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(796, 55);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // uiButton_Yes
            // 
            this.uiButton_Yes.Cursor = System.Windows.Forms.Cursors.Default;
            this.uiButton_Yes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiButton_Yes.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(83)))), ((int)(((byte)(124)))));
            this.uiButton_Yes.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.uiButton_Yes.Location = new System.Drawing.Point(479, 8);
            this.uiButton_Yes.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton_Yes.Name = "uiButton_Yes";
            this.uiButton_Yes.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(83)))), ((int)(((byte)(124)))));
            this.uiButton_Yes.Size = new System.Drawing.Size(144, 44);
            this.uiButton_Yes.Style = Sunny.UI.UIStyle.深色;
            this.uiButton_Yes.StyleCustomMode = true;
            this.uiButton_Yes.TabIndex = 4;
            this.uiButton_Yes.Text = "Yes";
            // 
            // uiButton_No
            // 
            this.uiButton_No.Cursor = System.Windows.Forms.Cursors.Default;
            this.uiButton_No.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiButton_No.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.uiButton_No.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(127)))), ((int)(((byte)(128)))));
            this.uiButton_No.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(87)))), ((int)(((byte)(89)))));
            this.uiButton_No.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(87)))), ((int)(((byte)(89)))));
            this.uiButton_No.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.uiButton_No.Location = new System.Drawing.Point(644, 8);
            this.uiButton_No.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton_No.Name = "uiButton_No";
            this.uiButton_No.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.uiButton_No.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(127)))), ((int)(((byte)(128)))));
            this.uiButton_No.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(87)))), ((int)(((byte)(89)))));
            this.uiButton_No.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(87)))), ((int)(((byte)(89)))));
            this.uiButton_No.Size = new System.Drawing.Size(144, 44);
            this.uiButton_No.Style = Sunny.UI.UIStyle.Red;
            this.uiButton_No.StyleCustomMode = true;
            this.uiButton_No.TabIndex = 5;
            this.uiButton_No.Text = "No";
            // 
            // Form_文件_弹窗2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.uiListBox1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "Form_文件_弹窗2";
            this.Padding = new System.Windows.Forms.Padding(2, 35, 2, 2);
            this.ShowDragStretch = true;
            this.ShowInTaskbar = false;
            this.Text = "";
            this.Load += new System.EventHandler(this.Form_文件_弹窗_Load);
            this.右键.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UIListBox uiListBox1;
        private System.Windows.Forms.Panel panel2;
        private Sunny.UI.UITextBox uiTextBox_FileName;
        private Sunny.UI.UILabel uiLabel_后缀;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Sunny.UI.UIButton uiButton_No;
        private Sunny.UI.UIButton uiButton_Yes;
        private System.Windows.Forms.ContextMenuStrip 右键;
        private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;
    }
}