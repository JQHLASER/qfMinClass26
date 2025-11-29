namespace qfNet
{
    partial class Form_语言
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_语言));
            this.uiComboBox_语言 = new Sunny.UI.UIComboBox();
            this.uiButton_Yes = new Sunny.UI.UIButton();
            this.uiButton_No = new Sunny.UI.UIButton();
            this.SuspendLayout();
            // 
            // uiComboBox_语言
            // 
            this.uiComboBox_语言.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList;
            this.uiComboBox_语言.FillColor = System.Drawing.Color.White;
            this.uiComboBox_语言.Font = new System.Drawing.Font("微软雅黑", 10.01739F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiComboBox_语言.Location = new System.Drawing.Point(116, 113);
            this.uiComboBox_语言.Margin = new System.Windows.Forms.Padding(0);
            this.uiComboBox_语言.MinimumSize = new System.Drawing.Size(63, 0);
            this.uiComboBox_语言.Name = "uiComboBox_语言";
            this.uiComboBox_语言.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.uiComboBox_语言.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(83)))), ((int)(((byte)(124)))));
            this.uiComboBox_语言.Size = new System.Drawing.Size(367, 29);
            this.uiComboBox_语言.Style = Sunny.UI.UIStyle.深色;
            this.uiComboBox_语言.TabIndex = 0;
            this.uiComboBox_语言.Text = "uiComboBox1";
            this.uiComboBox_语言.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiButton_Yes
            // 
            this.uiButton_Yes.Cursor = System.Windows.Forms.Cursors.Default;
            this.uiButton_Yes.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(83)))), ((int)(((byte)(124)))));
            this.uiButton_Yes.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.uiButton_Yes.Location = new System.Drawing.Point(116, 208);
            this.uiButton_Yes.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton_Yes.Name = "uiButton_Yes";
            this.uiButton_Yes.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(83)))), ((int)(((byte)(124)))));
            this.uiButton_Yes.Size = new System.Drawing.Size(152, 50);
            this.uiButton_Yes.Style = Sunny.UI.UIStyle.深色;
            this.uiButton_Yes.TabIndex = 1;
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
            this.uiButton_No.Location = new System.Drawing.Point(331, 208);
            this.uiButton_No.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton_No.Name = "uiButton_No";
            this.uiButton_No.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.uiButton_No.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(127)))), ((int)(((byte)(128)))));
            this.uiButton_No.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(87)))), ((int)(((byte)(89)))));
            this.uiButton_No.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(87)))), ((int)(((byte)(89)))));
            this.uiButton_No.Size = new System.Drawing.Size(152, 50);
            this.uiButton_No.Style = Sunny.UI.UIStyle.Red;
            this.uiButton_No.StyleCustomMode = true;
            this.uiButton_No.TabIndex = 2;
            this.uiButton_No.Text = "No";
            // 
            // Form_语言
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(600, 300);
            this.Controls.Add(this.uiButton_No);
            this.Controls.Add(this.uiButton_Yes);
            this.Controls.Add(this.uiComboBox_语言);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(2743, 1533);
            this.MinimizeBox = false;
            this.Name = "Form_语言";
            this.Padding = new System.Windows.Forms.Padding(0, 56, 0, 0);
            this.ShowInTaskbar = false;
            this.Style = Sunny.UI.UIStyle.Custom;
            this.StyleCustomMode = true;
            this.Text = "";
            this.Load += new System.EventHandler(this.Form_语言_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UIComboBox uiComboBox_语言;
        private Sunny.UI.UIButton uiButton_Yes;
        private Sunny.UI.UIButton uiButton_No;
    }
}