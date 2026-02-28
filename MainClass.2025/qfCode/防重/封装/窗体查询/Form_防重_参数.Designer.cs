namespace qfCode
{
    partial class Form_防重_参数
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_防重_参数));
            this.uiGroupBox1 = new Sunny.UI.UIGroupBox();
            this.uiButton_执行清理数据 = new Sunny.UI.UIButton();
            this.uiTextBox_保存天数 = new Sunny.UI.UITextBox();
            this.ui_Label_81 = new Sunny.ui_Label_8(this.components);
            this.uiButton_保存 = new Sunny.UI.UIButton();
            this.uiGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.uiButton_执行清理数据);
            this.uiGroupBox1.Controls.Add(this.uiTextBox_保存天数);
            this.uiGroupBox1.Controls.Add(this.ui_Label_81);
            this.uiGroupBox1.FillColor = System.Drawing.SystemColors.Control;
            this.uiGroupBox1.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.uiGroupBox1.Location = new System.Drawing.Point(21, 52);
            this.uiGroupBox1.Margin = new System.Windows.Forms.Padding(0);
            this.uiGroupBox1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Padding = new System.Windows.Forms.Padding(5, 25, 5, 5);
            this.uiGroupBox1.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(83)))), ((int)(((byte)(124)))));
            this.uiGroupBox1.Size = new System.Drawing.Size(400, 92);
            this.uiGroupBox1.Style = Sunny.UI.UIStyle.深色;
            this.uiGroupBox1.TabIndex = 0;
            this.uiGroupBox1.Text = "数据保存期限";
            // 
            // uiButton_执行清理数据
            // 
            this.uiButton_执行清理数据.Cursor = System.Windows.Forms.Cursors.Default;
            this.uiButton_执行清理数据.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(83)))), ((int)(((byte)(124)))));
            this.uiButton_执行清理数据.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.uiButton_执行清理数据.Location = new System.Drawing.Point(286, 35);
            this.uiButton_执行清理数据.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton_执行清理数据.Name = "uiButton_执行清理数据";
            this.uiButton_执行清理数据.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(83)))), ((int)(((byte)(124)))));
            this.uiButton_执行清理数据.Size = new System.Drawing.Size(79, 35);
            this.uiButton_执行清理数据.Style = Sunny.UI.UIStyle.深色;
            this.uiButton_执行清理数据.TabIndex = 2;
            this.uiButton_执行清理数据.Text = "执行";
            // 
            // uiTextBox_保存天数
            // 
            this.uiTextBox_保存天数.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.uiTextBox_保存天数.DoubleValue = 366D;
            this.uiTextBox_保存天数.FillColor = System.Drawing.Color.White;
            this.uiTextBox_保存天数.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.uiTextBox_保存天数.HasMinimum = true;
            this.uiTextBox_保存天数.IntValue = 366;
            this.uiTextBox_保存天数.Location = new System.Drawing.Point(155, 39);
            this.uiTextBox_保存天数.Margin = new System.Windows.Forms.Padding(0);
            this.uiTextBox_保存天数.Maximum = 2147483647D;
            this.uiTextBox_保存天数.Minimum = 0D;
            this.uiTextBox_保存天数.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiTextBox_保存天数.Name = "uiTextBox_保存天数";
            this.uiTextBox_保存天数.Padding = new System.Windows.Forms.Padding(5);
            this.uiTextBox_保存天数.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(83)))), ((int)(((byte)(124)))));
            this.uiTextBox_保存天数.Size = new System.Drawing.Size(115, 26);
            this.uiTextBox_保存天数.Style = Sunny.UI.UIStyle.深色;
            this.uiTextBox_保存天数.TabIndex = 1;
            this.uiTextBox_保存天数.Text = "366";
            this.uiTextBox_保存天数.textAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.uiTextBox_保存天数.Type = Sunny.UI.UITextBox.UIEditType.Integer;
            this.uiTextBox_保存天数.填充颜色 = System.Drawing.Color.White;
            // 
            // ui_Label_81
            // 
            this.ui_Label_81.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.ui_Label_81.ForeColor = System.Drawing.Color.Gray;
            this.ui_Label_81.Location = new System.Drawing.Point(24, 38);
            this.ui_Label_81.Name = "ui_Label_81";
            this.ui_Label_81.Size = new System.Drawing.Size(125, 26);
            this.ui_Label_81.Style = Sunny.UI.UIStyle.深色;
            this.ui_Label_81.TabIndex = 0;
            this.ui_Label_81.Text = "保存天数:";
            this.ui_Label_81.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // uiButton_保存
            // 
            this.uiButton_保存.Cursor = System.Windows.Forms.Cursors.Default;
            this.uiButton_保存.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(83)))), ((int)(((byte)(124)))));
            this.uiButton_保存.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.uiButton_保存.Location = new System.Drawing.Point(146, 200);
            this.uiButton_保存.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton_保存.Name = "uiButton_保存";
            this.uiButton_保存.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(83)))), ((int)(((byte)(124)))));
            this.uiButton_保存.Size = new System.Drawing.Size(150, 35);
            this.uiButton_保存.Style = Sunny.UI.UIStyle.深色;
            this.uiButton_保存.TabIndex = 3;
            this.uiButton_保存.Text = "保存";
            // 
            // Form_防重_参数
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(445, 255);
            this.Controls.Add(this.uiButton_保存);
            this.Controls.Add(this.uiGroupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_防重_参数";
            this.ShowShadow = false;
            this.Text = "";
            this.uiGroupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UIGroupBox uiGroupBox1;
        private Sunny.ui_Label_8 ui_Label_81;
        internal Sunny.UI.UIButton uiButton_执行清理数据;
        internal Sunny.UI.UITextBox uiTextBox_保存天数;
        internal Sunny.UI.UIButton uiButton_保存;
    }
}