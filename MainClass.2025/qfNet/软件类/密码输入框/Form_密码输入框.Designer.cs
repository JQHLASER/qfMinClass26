namespace qfNet
{
    partial class Form_密码输入框
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_密码输入框));
            this.uiButton_Yes = new Sunny.UI.UIButton();
            this.uiButton_No = new Sunny.UI.UIButton();
            this.uiTextBox_psd = new Sunny.UI.UITextBox();
            this.SuspendLayout();
            // 
            // uiButton_Yes
            // 
            this.uiButton_Yes.Cursor = System.Windows.Forms.Cursors.Default;
            this.uiButton_Yes.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(83)))), ((int)(((byte)(124)))));
            this.uiButton_Yes.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.uiButton_Yes.Location = new System.Drawing.Point(84, 229);
            this.uiButton_Yes.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton_Yes.Name = "uiButton_Yes";
            this.uiButton_Yes.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(83)))), ((int)(((byte)(124)))));
            this.uiButton_Yes.Size = new System.Drawing.Size(162, 47);
            this.uiButton_Yes.Style = Sunny.UI.UIStyle.深色;
            this.uiButton_Yes.StyleCustomMode = true;
            this.uiButton_Yes.TabIndex = 0;
            this.uiButton_Yes.Text = "Yes";
            // 
            // uiButton_No
            // 
            this.uiButton_No.Cursor = System.Windows.Forms.Cursors.Default;
            this.uiButton_No.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.uiButton_No.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(127)))), ((int)(((byte)(128)))));
            this.uiButton_No.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(87)))), ((int)(((byte)(89)))));
            this.uiButton_No.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(87)))), ((int)(((byte)(89)))));
            this.uiButton_No.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.uiButton_No.Location = new System.Drawing.Point(344, 229);
            this.uiButton_No.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton_No.Name = "uiButton_No";
            this.uiButton_No.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.uiButton_No.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(127)))), ((int)(((byte)(128)))));
            this.uiButton_No.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(87)))), ((int)(((byte)(89)))));
            this.uiButton_No.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(87)))), ((int)(((byte)(89)))));
            this.uiButton_No.Size = new System.Drawing.Size(162, 47);
            this.uiButton_No.Style = Sunny.UI.UIStyle.Red;
            this.uiButton_No.StyleCustomMode = true;
            this.uiButton_No.TabIndex = 1;
            this.uiButton_No.Text = "No";
            // 
            // uiTextBox_psd
            // 
            this.uiTextBox_psd.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.uiTextBox_psd.FillColor = System.Drawing.Color.White;
            this.uiTextBox_psd.Font = new System.Drawing.Font("微软雅黑", 11.89565F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiTextBox_psd.Location = new System.Drawing.Point(90, 126);
            this.uiTextBox_psd.Margin = new System.Windows.Forms.Padding(0);
            this.uiTextBox_psd.Maximum = 2147483647D;
            this.uiTextBox_psd.Minimum = -2147483648D;
            this.uiTextBox_psd.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiTextBox_psd.Name = "uiTextBox_psd";
            this.uiTextBox_psd.Padding = new System.Windows.Forms.Padding(5);
            this.uiTextBox_psd.PasswordChar = '*';
            this.uiTextBox_psd.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(83)))), ((int)(((byte)(124)))));
            this.uiTextBox_psd.Size = new System.Drawing.Size(400, 33);
            this.uiTextBox_psd.Style = Sunny.UI.UIStyle.深色;
            this.uiTextBox_psd.StyleCustomMode = true;
            this.uiTextBox_psd.TabIndex = 2;
            this.uiTextBox_psd.Text = "uiTextBox1";
            this.uiTextBox_psd.textAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.uiTextBox_psd.填充颜色 = System.Drawing.Color.White;
            // 
            // Form_密码输入框
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 300);
            this.ControlBox = false;
            this.Controls.Add(this.uiTextBox_psd);
            this.Controls.Add(this.uiButton_No);
            this.Controls.Add(this.uiButton_Yes);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_密码输入框";
            this.Padding = new System.Windows.Forms.Padding(0, 60, 0, 0);
            this.ShowInTaskbar = false;
            this.StyleCustomMode = true;
            this.Text = "PassWord";
            this.TitleHeight = 60;
            this.Load += new System.EventHandler(this.Form_密码输入框_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UIButton uiButton_Yes;
        private Sunny.UI.UIButton uiButton_No;
        private Sunny.UI.UITextBox uiTextBox_psd;
    }
}