namespace qfCode
{
    partial class Form_工具
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_工具));
            this.panel_控件 = new System.Windows.Forms.Panel();
            this.panel_按钮 = new System.Windows.Forms.Panel();
            this.uiButton_No = new Sunny.UI.UIButton();
            this.uiButton_Yes = new Sunny.UI.UIButton();
            this.uiTitlePanel_工具箱 = new Sunny.UI.UITitlePanel();
            this.panel_按钮.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_控件
            // 
            this.panel_控件.BackColor = System.Drawing.Color.White;
            this.panel_控件.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_控件.Location = new System.Drawing.Point(200, 35);
            this.panel_控件.Name = "panel_控件";
            this.panel_控件.Size = new System.Drawing.Size(600, 500);
            this.panel_控件.TabIndex = 1;
            // 
            // panel_按钮
            // 
            this.panel_按钮.BackColor = System.Drawing.Color.Transparent;
            this.panel_按钮.Controls.Add(this.uiButton_No);
            this.panel_按钮.Controls.Add(this.uiButton_Yes);
            this.panel_按钮.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_按钮.Location = new System.Drawing.Point(200, 535);
            this.panel_按钮.Name = "panel_按钮";
            this.panel_按钮.Size = new System.Drawing.Size(600, 65);
            this.panel_按钮.TabIndex = 2;
            // 
            // uiButton_No
            // 
            this.uiButton_No.Cursor = System.Windows.Forms.Cursors.Default;
            this.uiButton_No.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.uiButton_No.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(127)))), ((int)(((byte)(128)))));
            this.uiButton_No.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(87)))), ((int)(((byte)(89)))));
            this.uiButton_No.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(87)))), ((int)(((byte)(89)))));
            this.uiButton_No.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.uiButton_No.Location = new System.Drawing.Point(358, 16);
            this.uiButton_No.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton_No.Name = "uiButton_No";
            this.uiButton_No.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.uiButton_No.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(127)))), ((int)(((byte)(128)))));
            this.uiButton_No.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(87)))), ((int)(((byte)(89)))));
            this.uiButton_No.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(87)))), ((int)(((byte)(89)))));
            this.uiButton_No.Size = new System.Drawing.Size(150, 35);
            this.uiButton_No.Style = Sunny.UI.UIStyle.Red;
            this.uiButton_No.StyleCustomMode = true;
            this.uiButton_No.TabIndex = 1;
            this.uiButton_No.Text = "取消";
            // 
            // uiButton_Yes
            // 
            this.uiButton_Yes.Cursor = System.Windows.Forms.Cursors.Default;
            this.uiButton_Yes.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(190)))), ((int)(((byte)(40)))));
            this.uiButton_Yes.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(202)))), ((int)(((byte)(81)))));
            this.uiButton_Yes.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(168)))), ((int)(((byte)(35)))));
            this.uiButton_Yes.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(168)))), ((int)(((byte)(35)))));
            this.uiButton_Yes.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.uiButton_Yes.Location = new System.Drawing.Point(86, 16);
            this.uiButton_Yes.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton_Yes.Name = "uiButton_Yes";
            this.uiButton_Yes.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(190)))), ((int)(((byte)(40)))));
            this.uiButton_Yes.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(202)))), ((int)(((byte)(81)))));
            this.uiButton_Yes.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(168)))), ((int)(((byte)(35)))));
            this.uiButton_Yes.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(168)))), ((int)(((byte)(35)))));
            this.uiButton_Yes.Size = new System.Drawing.Size(150, 35);
            this.uiButton_Yes.Style = Sunny.UI.UIStyle.Green;
            this.uiButton_Yes.StyleCustomMode = true;
            this.uiButton_Yes.TabIndex = 0;
            this.uiButton_Yes.Text = "确定";
            // 
            // uiTitlePanel_工具箱
            // 
            this.uiTitlePanel_工具箱.Dock = System.Windows.Forms.DockStyle.Left;
            this.uiTitlePanel_工具箱.FillColor = System.Drawing.Color.Transparent;
            this.uiTitlePanel_工具箱.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.uiTitlePanel_工具箱.ForeColor = System.Drawing.Color.White;
            this.uiTitlePanel_工具箱.Location = new System.Drawing.Point(0, 35);
            this.uiTitlePanel_工具箱.Margin = new System.Windows.Forms.Padding(0);
            this.uiTitlePanel_工具箱.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiTitlePanel_工具箱.Name = "uiTitlePanel_工具箱";
            this.uiTitlePanel_工具箱.Padding = new System.Windows.Forms.Padding(5);
            this.uiTitlePanel_工具箱.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.uiTitlePanel_工具箱.RectColor = System.Drawing.Color.Silver;
            this.uiTitlePanel_工具箱.Size = new System.Drawing.Size(200, 565);
            this.uiTitlePanel_工具箱.Style = Sunny.UI.UIStyle.Custom;
            this.uiTitlePanel_工具箱.StyleCustomMode = true;
            this.uiTitlePanel_工具箱.TabIndex = 3;
            this.uiTitlePanel_工具箱.Text = "uiTitlePanel1";
            this.uiTitlePanel_工具箱.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(83)))), ((int)(((byte)(124)))));
            this.uiTitlePanel_工具箱.TitleHeight = 0;
            this.uiTitlePanel_工具箱.标题栏字体Font = new System.Drawing.Font("微软雅黑", 10F);
            // 
            // Form_工具
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.panel_控件);
            this.Controls.Add(this.panel_按钮);
            this.Controls.Add(this.uiTitlePanel_工具箱);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_工具";
            this.ShowInTaskbar = false;
            this.Text = "";
            this.panel_按钮.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel_控件;
        private System.Windows.Forms.Panel panel_按钮;
        private Sunny.UI.UIButton uiButton_No;
        private Sunny.UI.UIButton uiButton_Yes;
        private Sunny.UI.UITitlePanel uiTitlePanel_工具箱;
    }
}