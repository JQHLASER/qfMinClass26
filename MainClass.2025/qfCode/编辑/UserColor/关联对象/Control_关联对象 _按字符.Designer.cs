namespace qfCode
{
    partial class Control_关联对象_按字符
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.uiTextBox1 = new Sunny.UI.UITextBox();
            this.ui_Label_81 = new Sunny.ui_Label_8(this.components);
            this.uiTextBox4 = new Sunny.UI.UITextBox();
            this.ui_Label_85 = new Sunny.ui_Label_8(this.components);
            this.SuspendLayout();
            // 
            // uiTextBox1
            // 
            this.uiTextBox1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.uiTextBox1.DoubleValue = 1D;
            this.uiTextBox1.FillColor = System.Drawing.Color.White;
            this.uiTextBox1.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.uiTextBox1.HasMinimum = true;
            this.uiTextBox1.IntValue = 1;
            this.uiTextBox1.Location = new System.Drawing.Point(248, 97);
            this.uiTextBox1.Margin = new System.Windows.Forms.Padding(0);
            this.uiTextBox1.Maximum = 2147483647D;
            this.uiTextBox1.Minimum = 0D;
            this.uiTextBox1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiTextBox1.Name = "uiTextBox1";
            this.uiTextBox1.Padding = new System.Windows.Forms.Padding(5);
            this.uiTextBox1.Size = new System.Drawing.Size(100, 26);
            this.uiTextBox1.Style = Sunny.UI.UIStyle.Custom;
            this.uiTextBox1.TabIndex = 22;
            this.uiTextBox1.Text = "1";
            this.uiTextBox1.Type = Sunny.UI.UITextBox.UIEditType.Integer;
            this.uiTextBox1.填充颜色 = System.Drawing.Color.White;
            // 
            // ui_Label_81
            // 
            this.ui_Label_81.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.ui_Label_81.ForeColor = System.Drawing.Color.Gray;
            this.ui_Label_81.Location = new System.Drawing.Point(42, 97);
            this.ui_Label_81.Name = "ui_Label_81";
            this.ui_Label_81.Size = new System.Drawing.Size(200, 26);
            this.ui_Label_81.Style = Sunny.UI.UIStyle.Custom;
            this.ui_Label_81.TabIndex = 21;
            this.ui_Label_81.Text = "索引";
            this.ui_Label_81.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // uiTextBox4
            // 
            this.uiTextBox4.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.uiTextBox4.FillColor = System.Drawing.Color.White;
            this.uiTextBox4.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.uiTextBox4.HasMinimum = true;
            this.uiTextBox4.Location = new System.Drawing.Point(248, 66);
            this.uiTextBox4.Margin = new System.Windows.Forms.Padding(0);
            this.uiTextBox4.Maximum = 2147483647D;
            this.uiTextBox4.Minimum = 0D;
            this.uiTextBox4.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiTextBox4.Name = "uiTextBox4";
            this.uiTextBox4.Padding = new System.Windows.Forms.Padding(5);
            this.uiTextBox4.Size = new System.Drawing.Size(100, 26);
            this.uiTextBox4.Style = Sunny.UI.UIStyle.Custom;
            this.uiTextBox4.TabIndex = 20;
            this.uiTextBox4.填充颜色 = System.Drawing.Color.White;
            // 
            // ui_Label_85
            // 
            this.ui_Label_85.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.ui_Label_85.ForeColor = System.Drawing.Color.Gray;
            this.ui_Label_85.Location = new System.Drawing.Point(42, 66);
            this.ui_Label_85.Name = "ui_Label_85";
            this.ui_Label_85.Size = new System.Drawing.Size(200, 26);
            this.ui_Label_85.Style = Sunny.UI.UIStyle.Custom;
            this.ui_Label_85.TabIndex = 19;
            this.ui_Label_85.Text = "分割符";
            this.ui_Label_85.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Control_关联对象_按字符
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.uiTextBox1);
            this.Controls.Add(this.ui_Label_81);
            this.Controls.Add(this.uiTextBox4);
            this.Controls.Add(this.ui_Label_85);
            this.Name = "Control_关联对象_按字符";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.Size = new System.Drawing.Size(540, 200);
            this.Text = "文本";
            this.TitleHeight = 0;
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UITextBox uiTextBox1;
        private Sunny.ui_Label_8 ui_Label_81;
        private Sunny.UI.UITextBox uiTextBox4;
        private Sunny.ui_Label_8 ui_Label_85;
    }
}
