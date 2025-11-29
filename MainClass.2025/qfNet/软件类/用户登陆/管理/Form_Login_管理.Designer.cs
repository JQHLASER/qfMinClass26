namespace qfNet
{
    partial class Form_Login_管理
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Login_管理));
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("");
            this.工具栏1 = new Sunny.UI.myControls.工具栏();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton_添加 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton_修改 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton_删除 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton_保存 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.listView_userInfo = new System.Windows.Forms.ListView();
            this.UserName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.UserType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.工具栏1.SuspendLayout();
            this.SuspendLayout();
            // 
            // 工具栏1
            // 
            this.工具栏1.BackColor = System.Drawing.Color.Transparent;
            this.工具栏1.Dock = System.Windows.Forms.DockStyle.Right;
            this.工具栏1.Font = new System.Drawing.Font("微软雅黑", 10.01739F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.工具栏1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.工具栏1.ImageScalingSize = new System.Drawing.Size(19, 19);
            this.工具栏1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.toolStripButton_添加,
            this.toolStripSeparator2,
            this.toolStripButton_修改,
            this.toolStripSeparator3,
            this.toolStripButton_删除,
            this.toolStripSeparator4,
            this.toolStripButton_保存,
            this.toolStripSeparator5});
            this.工具栏1.Location = new System.Drawing.Point(685, 50);
            this.工具栏1.Name = "工具栏1";
            this.工具栏1.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.工具栏1.Size = new System.Drawing.Size(105, 540);
            this.工具栏1.TabIndex = 0;
            this.工具栏1.Text = "工具栏1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(94, 6);
            // 
            // toolStripButton_添加
            // 
            this.toolStripButton_添加.AutoSize = false;
            this.toolStripButton_添加.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton_添加.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.toolStripButton_添加.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_添加.Image")));
            this.toolStripButton_添加.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_添加.Name = "toolStripButton_添加";
            this.toolStripButton_添加.Size = new System.Drawing.Size(100, 40);
            this.toolStripButton_添加.Text = "添加";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(94, 6);
            // 
            // toolStripButton_修改
            // 
            this.toolStripButton_修改.AutoSize = false;
            this.toolStripButton_修改.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton_修改.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.toolStripButton_修改.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_修改.Image")));
            this.toolStripButton_修改.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_修改.Name = "toolStripButton_修改";
            this.toolStripButton_修改.Size = new System.Drawing.Size(100, 40);
            this.toolStripButton_修改.Text = "修改";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(94, 6);
            // 
            // toolStripButton_删除
            // 
            this.toolStripButton_删除.AutoSize = false;
            this.toolStripButton_删除.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton_删除.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.toolStripButton_删除.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_删除.Image")));
            this.toolStripButton_删除.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_删除.Name = "toolStripButton_删除";
            this.toolStripButton_删除.Size = new System.Drawing.Size(100, 40);
            this.toolStripButton_删除.Text = "删除";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(94, 6);
            // 
            // toolStripButton_保存
            // 
            this.toolStripButton_保存.AutoSize = false;
            this.toolStripButton_保存.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton_保存.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.toolStripButton_保存.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_保存.Image")));
            this.toolStripButton_保存.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_保存.Name = "toolStripButton_保存";
            this.toolStripButton_保存.Size = new System.Drawing.Size(100, 40);
            this.toolStripButton_保存.Text = "保存";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(94, 6);
            // 
            // listView_userInfo
            // 
            this.listView_userInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listView_userInfo.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.UserName,
            this.UserType});
            this.listView_userInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView_userInfo.Font = new System.Drawing.Font("微软雅黑", 10.01739F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listView_userInfo.FullRowSelect = true;
            this.listView_userInfo.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listView_userInfo.HideSelection = false;
            this.listView_userInfo.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.listView_userInfo.Location = new System.Drawing.Point(10, 50);
            this.listView_userInfo.MultiSelect = false;
            this.listView_userInfo.Name = "listView_userInfo";
            this.listView_userInfo.Size = new System.Drawing.Size(675, 540);
            this.listView_userInfo.TabIndex = 1;
            this.listView_userInfo.UseCompatibleStateImageBehavior = false;
            this.listView_userInfo.View = System.Windows.Forms.View.Details;
            // 
            // UserName
            // 
            this.UserName.Text = "UserName";
            this.UserName.Width = 350;
            // 
            // UserType
            // 
            this.UserType.Text = "Type";
            this.UserType.Width = 250;
            // 
            // Form_Login_管理
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.listView_userInfo);
            this.Controls.Add(this.工具栏1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(2194, 1460);
            this.MinimizeBox = false;
            this.Name = "Form_Login_管理";
            this.Padding = new System.Windows.Forms.Padding(10, 50, 10, 10);
            this.ShowInTaskbar = false;
            this.Style = Sunny.UI.UIStyle.Custom;
            this.StyleCustomMode = true;
            this.Text = "Login";
            this.Load += new System.EventHandler(this.Form_Login_管理_Load);
            this.工具栏1.ResumeLayout(false);
            this.工具栏1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Sunny.UI.myControls.工具栏 工具栏1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton_添加;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButton_修改;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripButton_删除;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton toolStripButton_保存;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ColumnHeader UserName;
        private System.Windows.Forms.ColumnHeader UserType;
        internal System.Windows.Forms.ListView listView_userInfo;
    }
}