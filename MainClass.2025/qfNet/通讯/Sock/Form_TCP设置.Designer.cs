namespace qfNet
{
    partial class Form_TCP设置
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_TCP设置));
            this.uiLabel_IP = new Sunny.UI.UILabel();
            this.uiTextBox_IP = new Sunny.UI.UITextBox();
            this.uiTextBox_Port = new Sunny.UI.UITextBox();
            this.uiLabel2 = new Sunny.UI.UILabel();
            this.uiButton_Yes = new Sunny.UI.UIButton();
            this.uiButton_No = new Sunny.UI.UIButton();
            this.SuspendLayout();
            // 
            // uiLabel_IP
            // 
            this.uiLabel_IP.Font = new System.Drawing.Font("微软雅黑", 11.26957F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel_IP.ForeColor = System.Drawing.Color.Gray;
            this.uiLabel_IP.Location = new System.Drawing.Point(96, 101);
            this.uiLabel_IP.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.uiLabel_IP.Name = "uiLabel_IP";
            this.uiLabel_IP.Size = new System.Drawing.Size(107, 33);
            this.uiLabel_IP.Style = Sunny.UI.UIStyle.Custom;
            this.uiLabel_IP.TabIndex = 0;
            this.uiLabel_IP.Text = "IP";
            this.uiLabel_IP.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // uiTextBox_IP
            // 
            this.uiTextBox_IP.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.uiTextBox_IP.FillColor = System.Drawing.Color.White;
            this.uiTextBox_IP.Font = new System.Drawing.Font("微软雅黑", 11.26957F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiTextBox_IP.Location = new System.Drawing.Point(209, 101);
            this.uiTextBox_IP.Margin = new System.Windows.Forms.Padding(0);
            this.uiTextBox_IP.Maximum = 2147483647D;
            this.uiTextBox_IP.Minimum = -2147483648D;
            this.uiTextBox_IP.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiTextBox_IP.Name = "uiTextBox_IP";
            this.uiTextBox_IP.Padding = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.uiTextBox_IP.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(83)))), ((int)(((byte)(124)))));
            this.uiTextBox_IP.Size = new System.Drawing.Size(250, 31);
            this.uiTextBox_IP.Style = Sunny.UI.UIStyle.Custom;
            this.uiTextBox_IP.TabIndex = 1;
            this.uiTextBox_IP.Text = "127.0.0.1";
            this.uiTextBox_IP.textAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.uiTextBox_IP.填充颜色 = System.Drawing.Color.White;
            // 
            // uiTextBox_Port
            // 
            this.uiTextBox_Port.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.uiTextBox_Port.FillColor = System.Drawing.Color.White;
            this.uiTextBox_Port.Font = new System.Drawing.Font("微软雅黑", 11.26957F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiTextBox_Port.HasMinimum = true;
            this.uiTextBox_Port.Location = new System.Drawing.Point(209, 149);
            this.uiTextBox_Port.Margin = new System.Windows.Forms.Padding(0);
            this.uiTextBox_Port.Maximum = 2147483647D;
            this.uiTextBox_Port.Minimum = 0D;
            this.uiTextBox_Port.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiTextBox_Port.Name = "uiTextBox_Port";
            this.uiTextBox_Port.Padding = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.uiTextBox_Port.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(83)))), ((int)(((byte)(124)))));
            this.uiTextBox_Port.Size = new System.Drawing.Size(250, 31);
            this.uiTextBox_Port.Style = Sunny.UI.UIStyle.Custom;
            this.uiTextBox_Port.TabIndex = 3;
            this.uiTextBox_Port.Text = "0";
            this.uiTextBox_Port.textAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.uiTextBox_Port.Type = Sunny.UI.UITextBox.UIEditType.Integer;
            this.uiTextBox_Port.填充颜色 = System.Drawing.Color.White;
            // 
            // uiLabel2
            // 
            this.uiLabel2.Font = new System.Drawing.Font("微软雅黑", 11.26957F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel2.ForeColor = System.Drawing.Color.Gray;
            this.uiLabel2.Location = new System.Drawing.Point(96, 149);
            this.uiLabel2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.uiLabel2.Name = "uiLabel2";
            this.uiLabel2.Size = new System.Drawing.Size(107, 33);
            this.uiLabel2.Style = Sunny.UI.UIStyle.Custom;
            this.uiLabel2.TabIndex = 2;
            this.uiLabel2.Text = "Port";
            this.uiLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // uiButton_Yes
            // 
            this.uiButton_Yes.Cursor = System.Windows.Forms.Cursors.Default;
            this.uiButton_Yes.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(83)))), ((int)(((byte)(124)))));
            this.uiButton_Yes.Font = new System.Drawing.Font("微软雅黑", 11.26957F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiButton_Yes.Location = new System.Drawing.Point(104, 243);
            this.uiButton_Yes.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.uiButton_Yes.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton_Yes.Name = "uiButton_Yes";
            this.uiButton_Yes.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(83)))), ((int)(((byte)(124)))));
            this.uiButton_Yes.Size = new System.Drawing.Size(150, 45);
            this.uiButton_Yes.Style = Sunny.UI.UIStyle.深色;
            this.uiButton_Yes.StyleCustomMode = true;
            this.uiButton_Yes.TabIndex = 4;
            this.uiButton_Yes.Text = "Yes";
            // 
            // uiButton_No
            // 
            this.uiButton_No.Cursor = System.Windows.Forms.Cursors.Default;
            this.uiButton_No.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.uiButton_No.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(127)))), ((int)(((byte)(128)))));
            this.uiButton_No.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(87)))), ((int)(((byte)(89)))));
            this.uiButton_No.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(87)))), ((int)(((byte)(89)))));
            this.uiButton_No.Font = new System.Drawing.Font("微软雅黑", 11.26957F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiButton_No.Location = new System.Drawing.Point(344, 243);
            this.uiButton_No.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.uiButton_No.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton_No.Name = "uiButton_No";
            this.uiButton_No.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.uiButton_No.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(127)))), ((int)(((byte)(128)))));
            this.uiButton_No.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(87)))), ((int)(((byte)(89)))));
            this.uiButton_No.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(87)))), ((int)(((byte)(89)))));
            this.uiButton_No.Size = new System.Drawing.Size(150, 45);
            this.uiButton_No.Style = Sunny.UI.UIStyle.Red;
            this.uiButton_No.StyleCustomMode = true;
            this.uiButton_No.TabIndex = 5;
            this.uiButton_No.Text = "No";
            // 
            // Form_TCP设置
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(600, 315);
            this.Controls.Add(this.uiButton_No);
            this.Controls.Add(this.uiButton_Yes);
            this.Controls.Add(this.uiTextBox_Port);
            this.Controls.Add(this.uiLabel2);
            this.Controls.Add(this.uiTextBox_IP);
            this.Controls.Add(this.uiLabel_IP);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(2742, 1533);
            this.MinimizeBox = false;
            this.Name = "Form_TCP设置";
            this.Padding = new System.Windows.Forms.Padding(0, 52, 0, 0);
            this.ShowInTaskbar = false;
            this.Style = Sunny.UI.UIStyle.Custom;
            this.StyleCustomMode = true;
            this.Text = "TCP设置";
            this.Load += new System.EventHandler(this.Form_TCP设置_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UILabel uiLabel_IP;
        private Sunny.UI.UITextBox uiTextBox_IP;
        private Sunny.UI.UITextBox uiTextBox_Port;
        private Sunny.UI.UILabel uiLabel2;
        private Sunny.UI.UIButton uiButton_Yes;
        private Sunny.UI.UIButton uiButton_No;
    }
}