namespace qfPLC
{
    partial class Form_西门子S7
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_西门子S7));
            this.uiButton_yes = new Sunny.UI.UIButton();
            this.ui_Label_81 = new Sunny.ui_Label_8(this.components);
            this.uiTextBox_ip = new Sunny.UI.UITextBox();
            this.ui_Combobox2_PlcType = new Sunny.ui_Combobox2();
            this.uiTextBox_port = new Sunny.UI.UITextBox();
            this.ui_Label_82 = new Sunny.ui_Label_8(this.components);
            this.ui_Label_83 = new Sunny.ui_Label_8(this.components);
            this.uiButton_No = new Sunny.UI.UIButton();
            this.SuspendLayout();
            // 
            // uiButton_yes
            // 
            this.uiButton_yes.Cursor = System.Windows.Forms.Cursors.Default;
            this.uiButton_yes.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(83)))), ((int)(((byte)(124)))));
            this.uiButton_yes.Font = new System.Drawing.Font("微软雅黑", 10.01739F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiButton_yes.Location = new System.Drawing.Point(118, 400);
            this.uiButton_yes.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton_yes.Name = "uiButton_yes";
            this.uiButton_yes.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(83)))), ((int)(((byte)(124)))));
            this.uiButton_yes.Size = new System.Drawing.Size(200, 50);
            this.uiButton_yes.Style = Sunny.UI.UIStyle.深色;
            this.uiButton_yes.TabIndex = 0;
            this.uiButton_yes.Text = "Yes";
            // 
            // ui_Label_81
            // 
            this.ui_Label_81.Font = new System.Drawing.Font("微软雅黑", 10.01739F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ui_Label_81.ForeColor = System.Drawing.Color.Gray;
            this.ui_Label_81.Location = new System.Drawing.Point(99, 159);
            this.ui_Label_81.Name = "ui_Label_81";
            this.ui_Label_81.Size = new System.Drawing.Size(186, 26);
            this.ui_Label_81.Style = Sunny.UI.UIStyle.深色;
            this.ui_Label_81.TabIndex = 1;
            this.ui_Label_81.Text = "IP";
            this.ui_Label_81.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // uiTextBox_ip
            // 
            this.uiTextBox_ip.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.uiTextBox_ip.FillColor = System.Drawing.Color.White;
            this.uiTextBox_ip.Font = new System.Drawing.Font("微软雅黑", 10.01739F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiTextBox_ip.Location = new System.Drawing.Point(293, 159);
            this.uiTextBox_ip.Margin = new System.Windows.Forms.Padding(0);
            this.uiTextBox_ip.Maximum = 2147483647D;
            this.uiTextBox_ip.Minimum = -2147483648D;
            this.uiTextBox_ip.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiTextBox_ip.Name = "uiTextBox_ip";
            this.uiTextBox_ip.Padding = new System.Windows.Forms.Padding(5);
            this.uiTextBox_ip.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(83)))), ((int)(((byte)(124)))));
            this.uiTextBox_ip.Size = new System.Drawing.Size(300, 29);
            this.uiTextBox_ip.Style = Sunny.UI.UIStyle.深色;
            this.uiTextBox_ip.TabIndex = 2;
            this.uiTextBox_ip.Text = "192.168.0.100";
            this.uiTextBox_ip.填充颜色 = System.Drawing.Color.White;
            // 
            // ui_Combobox2_PlcType
            // 
            this.ui_Combobox2_PlcType.BackColor = System.Drawing.Color.Transparent;
            this.ui_Combobox2_PlcType.FillColor = System.Drawing.SystemColors.Control;
            this.ui_Combobox2_PlcType.Font = new System.Drawing.Font("微软雅黑", 10.01739F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ui_Combobox2_PlcType.Location = new System.Drawing.Point(293, 233);
            this.ui_Combobox2_PlcType.Margin = new System.Windows.Forms.Padding(0);
            this.ui_Combobox2_PlcType.MinimumSize = new System.Drawing.Size(1, 1);
            this.ui_Combobox2_PlcType.Name = "ui_Combobox2_PlcType";
            this.ui_Combobox2_PlcType.Padding = new System.Windows.Forms.Padding(1);
            this.ui_Combobox2_PlcType.Radius = 0;
            this.ui_Combobox2_PlcType.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(83)))), ((int)(((byte)(124)))));
            this.ui_Combobox2_PlcType.Size = new System.Drawing.Size(300, 30);
            this.ui_Combobox2_PlcType.Style = Sunny.UI.UIStyle.深色;
            this.ui_Combobox2_PlcType.TabIndex = 3;
            this.ui_Combobox2_PlcType.Text = "ui_Combobox21";
            // 
            // uiTextBox_port
            // 
            this.uiTextBox_port.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.uiTextBox_port.DoubleValue = 102D;
            this.uiTextBox_port.FillColor = System.Drawing.Color.White;
            this.uiTextBox_port.Font = new System.Drawing.Font("微软雅黑", 10.01739F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiTextBox_port.HasMinimum = true;
            this.uiTextBox_port.IntValue = 102;
            this.uiTextBox_port.Location = new System.Drawing.Point(293, 196);
            this.uiTextBox_port.Margin = new System.Windows.Forms.Padding(0);
            this.uiTextBox_port.Maximum = 2147483647D;
            this.uiTextBox_port.Minimum = 0D;
            this.uiTextBox_port.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiTextBox_port.Name = "uiTextBox_port";
            this.uiTextBox_port.Padding = new System.Windows.Forms.Padding(5);
            this.uiTextBox_port.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(83)))), ((int)(((byte)(124)))));
            this.uiTextBox_port.Size = new System.Drawing.Size(300, 29);
            this.uiTextBox_port.Style = Sunny.UI.UIStyle.深色;
            this.uiTextBox_port.TabIndex = 4;
            this.uiTextBox_port.Text = "102";
            this.uiTextBox_port.Type = Sunny.UI.UITextBox.UIEditType.Integer;
            this.uiTextBox_port.填充颜色 = System.Drawing.Color.White;
            // 
            // ui_Label_82
            // 
            this.ui_Label_82.Font = new System.Drawing.Font("微软雅黑", 10.01739F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ui_Label_82.ForeColor = System.Drawing.Color.Gray;
            this.ui_Label_82.Location = new System.Drawing.Point(99, 196);
            this.ui_Label_82.Name = "ui_Label_82";
            this.ui_Label_82.Size = new System.Drawing.Size(186, 26);
            this.ui_Label_82.Style = Sunny.UI.UIStyle.深色;
            this.ui_Label_82.TabIndex = 3;
            this.ui_Label_82.Text = "Port";
            this.ui_Label_82.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ui_Label_83
            // 
            this.ui_Label_83.Font = new System.Drawing.Font("微软雅黑", 10.01739F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ui_Label_83.ForeColor = System.Drawing.Color.Gray;
            this.ui_Label_83.Location = new System.Drawing.Point(99, 233);
            this.ui_Label_83.Name = "ui_Label_83";
            this.ui_Label_83.Size = new System.Drawing.Size(186, 30);
            this.ui_Label_83.Style = Sunny.UI.UIStyle.深色;
            this.ui_Label_83.TabIndex = 5;
            this.ui_Label_83.Text = "Type";
            this.ui_Label_83.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // uiButton_No
            // 
            this.uiButton_No.Cursor = System.Windows.Forms.Cursors.Default;
            this.uiButton_No.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.uiButton_No.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(127)))), ((int)(((byte)(128)))));
            this.uiButton_No.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(87)))), ((int)(((byte)(89)))));
            this.uiButton_No.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(87)))), ((int)(((byte)(89)))));
            this.uiButton_No.Font = new System.Drawing.Font("微软雅黑", 10.01739F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiButton_No.Location = new System.Drawing.Point(469, 400);
            this.uiButton_No.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton_No.Name = "uiButton_No";
            this.uiButton_No.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.uiButton_No.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(127)))), ((int)(((byte)(128)))));
            this.uiButton_No.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(87)))), ((int)(((byte)(89)))));
            this.uiButton_No.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(87)))), ((int)(((byte)(89)))));
            this.uiButton_No.Size = new System.Drawing.Size(200, 50);
            this.uiButton_No.Style = Sunny.UI.UIStyle.Red;
            this.uiButton_No.StyleCustomMode = true;
            this.uiButton_No.TabIndex = 6;
            this.uiButton_No.Text = "No";
            // 
            // Form_西门子S7
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(800, 500);
            this.ControlBox = false;
            this.Controls.Add(this.uiButton_No);
            this.Controls.Add(this.ui_Label_83);
            this.Controls.Add(this.uiTextBox_port);
            this.Controls.Add(this.ui_Label_82);
            this.Controls.Add(this.ui_Combobox2_PlcType);
            this.Controls.Add(this.uiTextBox_ip);
            this.Controls.Add(this.ui_Label_81);
            this.Controls.Add(this.uiButton_yes);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_西门子S7";
            this.Text = "";
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UIButton uiButton_yes;
        private Sunny.ui_Label_8 ui_Label_81;
        private Sunny.UI.UITextBox uiTextBox_ip;
        private Sunny.ui_Combobox2 ui_Combobox2_PlcType;
        private Sunny.UI.UITextBox uiTextBox_port;
        private Sunny.ui_Label_8 ui_Label_82;
        private Sunny.ui_Label_8 ui_Label_83;
        private Sunny.UI.UIButton uiButton_No;
    }
}