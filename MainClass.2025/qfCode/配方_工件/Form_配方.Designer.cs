namespace qfCode
{
    partial class Form_配方
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_配方));
            this.ui_工具栏_文件操作1 = new Sunny.ui_工具栏_文件操作();
            this.panel_设计区 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // ui_工具栏_文件操作1
            // 
            this.ui_工具栏_文件操作1.Dock = System.Windows.Forms.DockStyle.Right;
            this.ui_工具栏_文件操作1.Location = new System.Drawing.Point(674, 35);
            this.ui_工具栏_文件操作1.Name = "ui_工具栏_文件操作1";
            this.ui_工具栏_文件操作1.Size = new System.Drawing.Size(126, 565);
            this.ui_工具栏_文件操作1.TabIndex = 1;
            // 
            // panel_设计区
            // 
            this.panel_设计区.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_设计区.Location = new System.Drawing.Point(0, 35);
            this.panel_设计区.Name = "panel_设计区";
            this.panel_设计区.Size = new System.Drawing.Size(674, 565);
            this.panel_设计区.TabIndex = 2;
            // 
            // Form_配方
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.panel_设计区);
            this.Controls.Add(this.ui_工具栏_文件操作1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_配方";
            this.ShowInTaskbar = false;
            this.Text = "";
            this.ResumeLayout(false);

        }

        #endregion
        public Sunny.ui_工具栏_文件操作 ui_工具栏_文件操作1;
        public System.Windows.Forms.Panel panel_设计区;
    }
}