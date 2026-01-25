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
            this.uiTextBox_索引 = new Sunny.UI.UITextBox();
            this.ui_Label_81 = new Sunny.ui_Label_8(this.components);
            this.uiTextBox_分割符 = new Sunny.UI.UITextBox();
            this.ui_Label_85 = new Sunny.ui_Label_8(this.components);
            this.SuspendLayout();
            // 
            // uiTextBox_索引
            // 
            this.uiTextBox_索引.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.uiTextBox_索引.DoubleValue = 1D;
            this.uiTextBox_索引.FillColor = System.Drawing.Color.White;
            this.uiTextBox_索引.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.uiTextBox_索引.HasMinimum = true;
            this.uiTextBox_索引.IntValue = 1;
            this.uiTextBox_索引.Location = new System.Drawing.Point(248, 63);
            this.uiTextBox_索引.Margin = new System.Windows.Forms.Padding(0);
            this.uiTextBox_索引.Maximum = 2147483647D;
            this.uiTextBox_索引.Minimum = 1D;
            this.uiTextBox_索引.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiTextBox_索引.Name = "uiTextBox_索引";
            this.uiTextBox_索引.Padding = new System.Windows.Forms.Padding(5);
            this.uiTextBox_索引.Size = new System.Drawing.Size(100, 26);
            this.uiTextBox_索引.TabIndex = 22;
            this.uiTextBox_索引.Text = "1";
            this.uiTextBox_索引.Type = Sunny.UI.UITextBox.UIEditType.Integer;
            this.uiTextBox_索引.填充颜色 = System.Drawing.Color.White;
            // 
            // ui_Label_81
            // 
            this.ui_Label_81.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.ui_Label_81.ForeColor = System.Drawing.Color.Gray;
            this.ui_Label_81.Location = new System.Drawing.Point(42, 63);
            this.ui_Label_81.Name = "ui_Label_81";
            this.ui_Label_81.Size = new System.Drawing.Size(200, 26);
            this.ui_Label_81.TabIndex = 21;
            this.ui_Label_81.Text = "索引";
            this.ui_Label_81.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // uiTextBox_分割符
            // 
            this.uiTextBox_分割符.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.uiTextBox_分割符.FillColor = System.Drawing.Color.White;
            this.uiTextBox_分割符.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.uiTextBox_分割符.HasMinimum = true;
            this.uiTextBox_分割符.Location = new System.Drawing.Point(248, 32);
            this.uiTextBox_分割符.Margin = new System.Windows.Forms.Padding(0);
            this.uiTextBox_分割符.Maximum = 2147483647D;
            this.uiTextBox_分割符.Minimum = 0D;
            this.uiTextBox_分割符.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiTextBox_分割符.Name = "uiTextBox_分割符";
            this.uiTextBox_分割符.Padding = new System.Windows.Forms.Padding(5);
            this.uiTextBox_分割符.Size = new System.Drawing.Size(100, 26);
            this.uiTextBox_分割符.TabIndex = 20;
            this.uiTextBox_分割符.填充颜色 = System.Drawing.Color.White;
            // 
            // ui_Label_85
            // 
            this.ui_Label_85.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.ui_Label_85.ForeColor = System.Drawing.Color.Gray;
            this.ui_Label_85.Location = new System.Drawing.Point(42, 32);
            this.ui_Label_85.Name = "ui_Label_85";
            this.ui_Label_85.Size = new System.Drawing.Size(200, 26);
            this.ui_Label_85.TabIndex = 19;
            this.ui_Label_85.Text = "分割符";
            this.ui_Label_85.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Control_关联对象_按字符
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.uiTextBox_索引);
            this.Controls.Add(this.ui_Label_81);
            this.Controls.Add(this.uiTextBox_分割符);
            this.Controls.Add(this.ui_Label_85);
            this.FillColor = System.Drawing.SystemColors.Control;
            this.Name = "Control_关联对象_按字符";
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

        private Sunny.UI.UITextBox uiTextBox_索引;
        private Sunny.ui_Label_8 ui_Label_81;
        private Sunny.UI.UITextBox uiTextBox_分割符;
        private Sunny.ui_Label_8 ui_Label_85;
    }
}
