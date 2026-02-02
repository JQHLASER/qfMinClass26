namespace qfCode
{
    partial class Control_关联对象_按位
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
            this.uiTextBox_起始位 = new Sunny.UI.UITextBox();
            this.ui_Label_85 = new Sunny.ui_Label_8(this.components);
            this.uiTextBox_数量 = new Sunny.UI.UITextBox();
            this.ui_Label_81 = new Sunny.ui_Label_8(this.components);
            this.SuspendLayout();
            // 
            // uiTextBox_起始位
            // 
            this.uiTextBox_起始位.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.uiTextBox_起始位.DoubleValue = 1D;
            this.uiTextBox_起始位.FillColor = System.Drawing.Color.White;
            this.uiTextBox_起始位.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.uiTextBox_起始位.HasMinimum = true;
            this.uiTextBox_起始位.IntValue = 1;
            this.uiTextBox_起始位.Location = new System.Drawing.Point(242, 31);
            this.uiTextBox_起始位.Margin = new System.Windows.Forms.Padding(0);
            this.uiTextBox_起始位.Maximum = 2147483647D;
            this.uiTextBox_起始位.Minimum = 1D;
            this.uiTextBox_起始位.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiTextBox_起始位.Name = "uiTextBox_起始位";
            this.uiTextBox_起始位.Padding = new System.Windows.Forms.Padding(5);
            this.uiTextBox_起始位.Size = new System.Drawing.Size(100, 26);
            this.uiTextBox_起始位.Style = Sunny.UI.UIStyle.Custom;
            this.uiTextBox_起始位.TabIndex = 16;
            this.uiTextBox_起始位.Text = "1";
            this.uiTextBox_起始位.Type = Sunny.UI.UITextBox.UIEditType.Integer;
            this.uiTextBox_起始位.填充颜色 = System.Drawing.Color.White;
            // 
            // ui_Label_85
            // 
            this.ui_Label_85.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.ui_Label_85.ForeColor = System.Drawing.Color.Gray;
            this.ui_Label_85.Location = new System.Drawing.Point(36, 31);
            this.ui_Label_85.Name = "ui_Label_85";
            this.ui_Label_85.Size = new System.Drawing.Size(200, 26);
            this.ui_Label_85.Style = Sunny.UI.UIStyle.Custom;
            this.ui_Label_85.TabIndex = 15;
            this.ui_Label_85.Text = "起始";
            this.ui_Label_85.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // uiTextBox_数量
            // 
            this.uiTextBox_数量.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.uiTextBox_数量.DoubleValue = 1D;
            this.uiTextBox_数量.FillColor = System.Drawing.Color.White;
            this.uiTextBox_数量.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.uiTextBox_数量.HasMinimum = true;
            this.uiTextBox_数量.IntValue = 1;
            this.uiTextBox_数量.Location = new System.Drawing.Point(242, 62);
            this.uiTextBox_数量.Margin = new System.Windows.Forms.Padding(0);
            this.uiTextBox_数量.Maximum = 2147483647D;
            this.uiTextBox_数量.Minimum = 0D;
            this.uiTextBox_数量.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiTextBox_数量.Name = "uiTextBox_数量";
            this.uiTextBox_数量.Padding = new System.Windows.Forms.Padding(5);
            this.uiTextBox_数量.Size = new System.Drawing.Size(100, 26);
            this.uiTextBox_数量.Style = Sunny.UI.UIStyle.Custom;
            this.uiTextBox_数量.TabIndex = 18;
            this.uiTextBox_数量.Text = "1";
            this.uiTextBox_数量.Type = Sunny.UI.UITextBox.UIEditType.Integer;
            this.uiTextBox_数量.填充颜色 = System.Drawing.Color.White;
            // 
            // ui_Label_81
            // 
            this.ui_Label_81.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.ui_Label_81.ForeColor = System.Drawing.Color.Gray;
            this.ui_Label_81.Location = new System.Drawing.Point(36, 62);
            this.ui_Label_81.Name = "ui_Label_81";
            this.ui_Label_81.Size = new System.Drawing.Size(200, 26);
            this.ui_Label_81.Style = Sunny.UI.UIStyle.Custom;
            this.ui_Label_81.TabIndex = 17;
            this.ui_Label_81.Text = "数量";
            this.ui_Label_81.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Control_关联对象_按位
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.uiTextBox_数量);
            this.Controls.Add(this.ui_Label_81);
            this.Controls.Add(this.uiTextBox_起始位);
            this.Controls.Add(this.ui_Label_85);
            this.FillColor = System.Drawing.SystemColors.Control;
            this.Name = "Control_关联对象_按位";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.Size = new System.Drawing.Size(580, 120);
            this.Style = Sunny.UI.UIStyle.Custom;
            this.StyleCustomMode = true;
            this.Text = "";
            this.ResumeLayout(false);

        }

        #endregion
        private Sunny.ui_Label_8 ui_Label_85;
        private Sunny.ui_Label_8 ui_Label_81;
        public Sunny.UI.UITextBox uiTextBox_起始位;
        public Sunny.UI.UITextBox uiTextBox_数量;
    }
}
