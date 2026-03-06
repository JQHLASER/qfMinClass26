namespace qfCode
{
    partial class Form_主窗体
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_主窗体));
            this.uiTitlePanel_元素列表 = new Sunny.UI.UITitlePanel();
            this.dataGridView_元素 = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ui_Button_元素_下移 = new Sunny.ui_Button2();
            this.ui_Button_元素_上移 = new Sunny.ui_Button2();
            this.ui_Button_元素_删除 = new Sunny.ui_Button2();
            this.ui_Button_元素_修改 = new Sunny.ui_Button2();
            this.ui_Button_元素_添加 = new Sunny.ui_Button2();
            this.uiTitlePanel_对象列表 = new Sunny.UI.UITitlePanel();
            this.uiListBox_对象列表 = new Sunny.UI.UIListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ui_Button_对象_预览 = new Sunny.ui_Button2();
            this.ui_Button_对象_保存 = new Sunny.ui_Button2();
            this.ui_Button_对象_下移 = new Sunny.ui_Button2();
            this.ui_Button_对象_上移 = new Sunny.ui_Button2();
            this.ui_Button_对象_删除 = new Sunny.ui_Button2();
            this.ui_Button_对象_修改 = new Sunny.ui_Button2();
            this.ui_Button_对象_添加 = new Sunny.ui_Button2();
            this.tableLayoutPanel_配方 = new System.Windows.Forms.TableLayoutPanel();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新建ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.另存为ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导入ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关闭ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.视图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel_下边栏 = new System.Windows.Forms.TableLayoutPanel();
            this.textBox_信息 = new System.Windows.Forms.TextBox();
            this.textBox_备注 = new System.Windows.Forms.TextBox();
            this.uiTitlePanel_元素列表.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_元素)).BeginInit();
            this.panel2.SuspendLayout();
            this.uiTitlePanel_对象列表.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel_配方.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.tableLayoutPanel_下边栏.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiTitlePanel_元素列表
            // 
            this.uiTitlePanel_元素列表.Controls.Add(this.dataGridView_元素);
            this.uiTitlePanel_元素列表.Controls.Add(this.panel2);
            this.uiTitlePanel_元素列表.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiTitlePanel_元素列表.FillColor = System.Drawing.SystemColors.Control;
            this.uiTitlePanel_元素列表.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.uiTitlePanel_元素列表.ForeColor = System.Drawing.Color.White;
            this.uiTitlePanel_元素列表.Location = new System.Drawing.Point(255, 5);
            this.uiTitlePanel_元素列表.Margin = new System.Windows.Forms.Padding(0);
            this.uiTitlePanel_元素列表.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiTitlePanel_元素列表.Name = "uiTitlePanel_元素列表";
            this.uiTitlePanel_元素列表.Padding = new System.Windows.Forms.Padding(2, 35, 2, 2);
            this.uiTitlePanel_元素列表.RectColor = System.Drawing.Color.Gray;
            this.uiTitlePanel_元素列表.Size = new System.Drawing.Size(545, 420);
            this.uiTitlePanel_元素列表.Style = Sunny.UI.UIStyle.Custom;
            this.uiTitlePanel_元素列表.StyleCustomMode = true;
            this.uiTitlePanel_元素列表.TabIndex = 4;
            this.uiTitlePanel_元素列表.Text = null;
            this.uiTitlePanel_元素列表.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.uiTitlePanel_元素列表.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(83)))), ((int)(((byte)(124)))));
            this.uiTitlePanel_元素列表.TitleHeight = 30;
            this.uiTitlePanel_元素列表.标题栏字体Font = new System.Drawing.Font("微软雅黑", 8.765218F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            // 
            // dataGridView_元素
            // 
            this.dataGridView_元素.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_元素.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_元素.Location = new System.Drawing.Point(2, 35);
            this.dataGridView_元素.Name = "dataGridView_元素";
            this.dataGridView_元素.RowHeadersWidth = 49;
            this.dataGridView_元素.Size = new System.Drawing.Size(441, 383);
            this.dataGridView_元素.TabIndex = 7;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel2.Controls.Add(this.ui_Button_元素_下移);
            this.panel2.Controls.Add(this.ui_Button_元素_上移);
            this.panel2.Controls.Add(this.ui_Button_元素_删除);
            this.panel2.Controls.Add(this.ui_Button_元素_修改);
            this.panel2.Controls.Add(this.ui_Button_元素_添加);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.panel2.Location = new System.Drawing.Point(443, 35);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(5, 2, 2, 2);
            this.panel2.Size = new System.Drawing.Size(100, 383);
            this.panel2.TabIndex = 6;
            // 
            // ui_Button_元素_下移
            // 
            this.ui_Button_元素_下移.BackColor = System.Drawing.Color.Transparent;
            this.ui_Button_元素_下移.Dock = System.Windows.Forms.DockStyle.Top;
            this.ui_Button_元素_下移.Location = new System.Drawing.Point(5, 154);
            this.ui_Button_元素_下移.Name = "ui_Button_元素_下移";
            this.ui_Button_元素_下移.Padding = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.ui_Button_元素_下移.Radius_圆角 = 5;
            this.ui_Button_元素_下移.Size = new System.Drawing.Size(93, 38);
            this.ui_Button_元素_下移.TabIndex = 21;
            this.ui_Button_元素_下移.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.ui_Button_元素_下移.Text文本 = "下移";
            this.ui_Button_元素_下移.文本颜色 = System.Drawing.Color.White;
            this.ui_Button_元素_下移.背景颜色 = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.ui_Button_元素_下移.背景颜色_鼠标按下 = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(131)))), ((int)(((byte)(229)))));
            this.ui_Button_元素_下移.背景颜色_鼠标移上 = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(168)))), ((int)(((byte)(255)))));
            this.ui_Button_元素_下移.背景颜色_鼠标选中 = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(131)))), ((int)(((byte)(229)))));
            this.ui_Button_元素_下移.边框颜色 = System.Drawing.Color.Silver;
            this.ui_Button_元素_下移.边框颜色_鼠标按下 = System.Drawing.Color.Silver;
            this.ui_Button_元素_下移.边框颜色_鼠标移上 = System.Drawing.Color.Silver;
            this.ui_Button_元素_下移.边框颜色_鼠标选中 = System.Drawing.Color.Silver;
            // 
            // ui_Button_元素_上移
            // 
            this.ui_Button_元素_上移.BackColor = System.Drawing.Color.Transparent;
            this.ui_Button_元素_上移.Dock = System.Windows.Forms.DockStyle.Top;
            this.ui_Button_元素_上移.Location = new System.Drawing.Point(5, 116);
            this.ui_Button_元素_上移.Name = "ui_Button_元素_上移";
            this.ui_Button_元素_上移.Padding = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.ui_Button_元素_上移.Radius_圆角 = 5;
            this.ui_Button_元素_上移.Size = new System.Drawing.Size(93, 38);
            this.ui_Button_元素_上移.TabIndex = 20;
            this.ui_Button_元素_上移.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.ui_Button_元素_上移.Text文本 = "上移";
            this.ui_Button_元素_上移.文本颜色 = System.Drawing.Color.White;
            this.ui_Button_元素_上移.背景颜色 = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.ui_Button_元素_上移.背景颜色_鼠标按下 = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(131)))), ((int)(((byte)(229)))));
            this.ui_Button_元素_上移.背景颜色_鼠标移上 = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(168)))), ((int)(((byte)(255)))));
            this.ui_Button_元素_上移.背景颜色_鼠标选中 = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(131)))), ((int)(((byte)(229)))));
            this.ui_Button_元素_上移.边框颜色 = System.Drawing.Color.Silver;
            this.ui_Button_元素_上移.边框颜色_鼠标按下 = System.Drawing.Color.Silver;
            this.ui_Button_元素_上移.边框颜色_鼠标移上 = System.Drawing.Color.Silver;
            this.ui_Button_元素_上移.边框颜色_鼠标选中 = System.Drawing.Color.Silver;
            // 
            // ui_Button_元素_删除
            // 
            this.ui_Button_元素_删除.BackColor = System.Drawing.Color.Transparent;
            this.ui_Button_元素_删除.Dock = System.Windows.Forms.DockStyle.Top;
            this.ui_Button_元素_删除.Location = new System.Drawing.Point(5, 78);
            this.ui_Button_元素_删除.Name = "ui_Button_元素_删除";
            this.ui_Button_元素_删除.Padding = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.ui_Button_元素_删除.Radius_圆角 = 5;
            this.ui_Button_元素_删除.Size = new System.Drawing.Size(93, 38);
            this.ui_Button_元素_删除.TabIndex = 19;
            this.ui_Button_元素_删除.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.ui_Button_元素_删除.Text文本 = "删除";
            this.ui_Button_元素_删除.文本颜色 = System.Drawing.Color.White;
            this.ui_Button_元素_删除.背景颜色 = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.ui_Button_元素_删除.背景颜色_鼠标按下 = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(131)))), ((int)(((byte)(229)))));
            this.ui_Button_元素_删除.背景颜色_鼠标移上 = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(168)))), ((int)(((byte)(255)))));
            this.ui_Button_元素_删除.背景颜色_鼠标选中 = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(131)))), ((int)(((byte)(229)))));
            this.ui_Button_元素_删除.边框颜色 = System.Drawing.Color.Silver;
            this.ui_Button_元素_删除.边框颜色_鼠标按下 = System.Drawing.Color.Silver;
            this.ui_Button_元素_删除.边框颜色_鼠标移上 = System.Drawing.Color.Silver;
            this.ui_Button_元素_删除.边框颜色_鼠标选中 = System.Drawing.Color.Silver;
            // 
            // ui_Button_元素_修改
            // 
            this.ui_Button_元素_修改.BackColor = System.Drawing.Color.Transparent;
            this.ui_Button_元素_修改.Dock = System.Windows.Forms.DockStyle.Top;
            this.ui_Button_元素_修改.Location = new System.Drawing.Point(5, 40);
            this.ui_Button_元素_修改.Name = "ui_Button_元素_修改";
            this.ui_Button_元素_修改.Padding = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.ui_Button_元素_修改.Radius_圆角 = 5;
            this.ui_Button_元素_修改.Size = new System.Drawing.Size(93, 38);
            this.ui_Button_元素_修改.TabIndex = 18;
            this.ui_Button_元素_修改.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.ui_Button_元素_修改.Text文本 = "修改";
            this.ui_Button_元素_修改.文本颜色 = System.Drawing.Color.White;
            this.ui_Button_元素_修改.背景颜色 = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.ui_Button_元素_修改.背景颜色_鼠标按下 = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(131)))), ((int)(((byte)(229)))));
            this.ui_Button_元素_修改.背景颜色_鼠标移上 = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(168)))), ((int)(((byte)(255)))));
            this.ui_Button_元素_修改.背景颜色_鼠标选中 = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(131)))), ((int)(((byte)(229)))));
            this.ui_Button_元素_修改.边框颜色 = System.Drawing.Color.Silver;
            this.ui_Button_元素_修改.边框颜色_鼠标按下 = System.Drawing.Color.Silver;
            this.ui_Button_元素_修改.边框颜色_鼠标移上 = System.Drawing.Color.Silver;
            this.ui_Button_元素_修改.边框颜色_鼠标选中 = System.Drawing.Color.Silver;
            // 
            // ui_Button_元素_添加
            // 
            this.ui_Button_元素_添加.BackColor = System.Drawing.Color.Transparent;
            this.ui_Button_元素_添加.Dock = System.Windows.Forms.DockStyle.Top;
            this.ui_Button_元素_添加.Location = new System.Drawing.Point(5, 2);
            this.ui_Button_元素_添加.Name = "ui_Button_元素_添加";
            this.ui_Button_元素_添加.Padding = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.ui_Button_元素_添加.Radius_圆角 = 5;
            this.ui_Button_元素_添加.Size = new System.Drawing.Size(93, 38);
            this.ui_Button_元素_添加.TabIndex = 17;
            this.ui_Button_元素_添加.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.ui_Button_元素_添加.Text文本 = "添加";
            this.ui_Button_元素_添加.文本颜色 = System.Drawing.Color.White;
            this.ui_Button_元素_添加.背景颜色 = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.ui_Button_元素_添加.背景颜色_鼠标按下 = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(131)))), ((int)(((byte)(229)))));
            this.ui_Button_元素_添加.背景颜色_鼠标移上 = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(168)))), ((int)(((byte)(255)))));
            this.ui_Button_元素_添加.背景颜色_鼠标选中 = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(131)))), ((int)(((byte)(229)))));
            this.ui_Button_元素_添加.边框颜色 = System.Drawing.Color.Silver;
            this.ui_Button_元素_添加.边框颜色_鼠标按下 = System.Drawing.Color.Silver;
            this.ui_Button_元素_添加.边框颜色_鼠标移上 = System.Drawing.Color.Silver;
            this.ui_Button_元素_添加.边框颜色_鼠标选中 = System.Drawing.Color.Silver;
            // 
            // uiTitlePanel_对象列表
            // 
            this.uiTitlePanel_对象列表.Controls.Add(this.uiListBox_对象列表);
            this.uiTitlePanel_对象列表.Controls.Add(this.panel1);
            this.uiTitlePanel_对象列表.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiTitlePanel_对象列表.FillColor = System.Drawing.SystemColors.Control;
            this.uiTitlePanel_对象列表.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.uiTitlePanel_对象列表.ForeColor = System.Drawing.Color.White;
            this.uiTitlePanel_对象列表.Location = new System.Drawing.Point(0, 5);
            this.uiTitlePanel_对象列表.Margin = new System.Windows.Forms.Padding(0);
            this.uiTitlePanel_对象列表.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiTitlePanel_对象列表.Name = "uiTitlePanel_对象列表";
            this.uiTitlePanel_对象列表.Padding = new System.Windows.Forms.Padding(2, 35, 2, 2);
            this.uiTitlePanel_对象列表.RectColor = System.Drawing.Color.Gray;
            this.uiTitlePanel_对象列表.Size = new System.Drawing.Size(250, 420);
            this.uiTitlePanel_对象列表.Style = Sunny.UI.UIStyle.Custom;
            this.uiTitlePanel_对象列表.StyleCustomMode = true;
            this.uiTitlePanel_对象列表.TabIndex = 3;
            this.uiTitlePanel_对象列表.Text = "对象列表";
            this.uiTitlePanel_对象列表.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.uiTitlePanel_对象列表.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(83)))), ((int)(((byte)(124)))));
            this.uiTitlePanel_对象列表.TitleHeight = 30;
            this.uiTitlePanel_对象列表.标题栏字体Font = new System.Drawing.Font("微软雅黑", 8.765218F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            // 
            // uiListBox_对象列表
            // 
            this.uiListBox_对象列表.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiListBox_对象列表.FillColor = System.Drawing.Color.White;
            this.uiListBox_对象列表.Font = new System.Drawing.Font("微软雅黑", 8.765218F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiListBox_对象列表.HoverColor = System.Drawing.Color.Transparent;
            this.uiListBox_对象列表.ItemHeight = 35;
            this.uiListBox_对象列表.Items.AddRange(new object[] {
            "序列号",
            "固定文本"});
            this.uiListBox_对象列表.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.uiListBox_对象列表.Location = new System.Drawing.Point(102, 35);
            this.uiListBox_对象列表.Margin = new System.Windows.Forms.Padding(0);
            this.uiListBox_对象列表.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiListBox_对象列表.Name = "uiListBox_对象列表";
            this.uiListBox_对象列表.Padding = new System.Windows.Forms.Padding(2);
            this.uiListBox_对象列表.RectColor = System.Drawing.Color.Transparent;
            this.uiListBox_对象列表.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.uiListBox_对象列表.Size = new System.Drawing.Size(146, 383);
            this.uiListBox_对象列表.Style = Sunny.UI.UIStyle.Custom;
            this.uiListBox_对象列表.StyleCustomMode = true;
            this.uiListBox_对象列表.TabIndex = 8;
            this.uiListBox_对象列表.Text = "uiListBox2";
            this.uiListBox_对象列表.文本颜色 = System.Drawing.Color.Gray;
            this.uiListBox_对象列表.背景颜色 = System.Drawing.Color.White;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.Controls.Add(this.ui_Button_对象_预览);
            this.panel1.Controls.Add(this.ui_Button_对象_保存);
            this.panel1.Controls.Add(this.ui_Button_对象_下移);
            this.panel1.Controls.Add(this.ui_Button_对象_上移);
            this.panel1.Controls.Add(this.ui_Button_对象_删除);
            this.panel1.Controls.Add(this.ui_Button_对象_修改);
            this.panel1.Controls.Add(this.ui_Button_对象_添加);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.Location = new System.Drawing.Point(2, 35);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(2, 2, 5, 2);
            this.panel1.Size = new System.Drawing.Size(100, 383);
            this.panel1.TabIndex = 6;
            // 
            // ui_Button_对象_预览
            // 
            this.ui_Button_对象_预览.BackColor = System.Drawing.Color.Transparent;
            this.ui_Button_对象_预览.Dock = System.Windows.Forms.DockStyle.Top;
            this.ui_Button_对象_预览.Location = new System.Drawing.Point(2, 230);
            this.ui_Button_对象_预览.Name = "ui_Button_对象_预览";
            this.ui_Button_对象_预览.Padding = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.ui_Button_对象_预览.Radius_圆角 = 5;
            this.ui_Button_对象_预览.Size = new System.Drawing.Size(93, 38);
            this.ui_Button_对象_预览.TabIndex = 16;
            this.ui_Button_对象_预览.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.ui_Button_对象_预览.Text文本 = "预览";
            this.ui_Button_对象_预览.文本颜色 = System.Drawing.Color.White;
            this.ui_Button_对象_预览.背景颜色 = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.ui_Button_对象_预览.背景颜色_鼠标按下 = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(131)))), ((int)(((byte)(229)))));
            this.ui_Button_对象_预览.背景颜色_鼠标移上 = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(168)))), ((int)(((byte)(255)))));
            this.ui_Button_对象_预览.背景颜色_鼠标选中 = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(131)))), ((int)(((byte)(229)))));
            this.ui_Button_对象_预览.边框颜色 = System.Drawing.Color.Silver;
            this.ui_Button_对象_预览.边框颜色_鼠标按下 = System.Drawing.Color.Silver;
            this.ui_Button_对象_预览.边框颜色_鼠标移上 = System.Drawing.Color.Silver;
            this.ui_Button_对象_预览.边框颜色_鼠标选中 = System.Drawing.Color.Silver;
            // 
            // ui_Button_对象_保存
            // 
            this.ui_Button_对象_保存.BackColor = System.Drawing.Color.Transparent;
            this.ui_Button_对象_保存.Dock = System.Windows.Forms.DockStyle.Top;
            this.ui_Button_对象_保存.Location = new System.Drawing.Point(2, 192);
            this.ui_Button_对象_保存.Name = "ui_Button_对象_保存";
            this.ui_Button_对象_保存.Padding = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.ui_Button_对象_保存.Radius_圆角 = 5;
            this.ui_Button_对象_保存.Size = new System.Drawing.Size(93, 38);
            this.ui_Button_对象_保存.TabIndex = 15;
            this.ui_Button_对象_保存.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.ui_Button_对象_保存.Text文本 = "保存";
            this.ui_Button_对象_保存.文本颜色 = System.Drawing.Color.White;
            this.ui_Button_对象_保存.背景颜色 = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.ui_Button_对象_保存.背景颜色_鼠标按下 = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(131)))), ((int)(((byte)(229)))));
            this.ui_Button_对象_保存.背景颜色_鼠标移上 = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(168)))), ((int)(((byte)(255)))));
            this.ui_Button_对象_保存.背景颜色_鼠标选中 = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(131)))), ((int)(((byte)(229)))));
            this.ui_Button_对象_保存.边框颜色 = System.Drawing.Color.Silver;
            this.ui_Button_对象_保存.边框颜色_鼠标按下 = System.Drawing.Color.Silver;
            this.ui_Button_对象_保存.边框颜色_鼠标移上 = System.Drawing.Color.Silver;
            this.ui_Button_对象_保存.边框颜色_鼠标选中 = System.Drawing.Color.Silver;
            // 
            // ui_Button_对象_下移
            // 
            this.ui_Button_对象_下移.BackColor = System.Drawing.Color.Transparent;
            this.ui_Button_对象_下移.Dock = System.Windows.Forms.DockStyle.Top;
            this.ui_Button_对象_下移.Location = new System.Drawing.Point(2, 154);
            this.ui_Button_对象_下移.Name = "ui_Button_对象_下移";
            this.ui_Button_对象_下移.Padding = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.ui_Button_对象_下移.Radius_圆角 = 5;
            this.ui_Button_对象_下移.Size = new System.Drawing.Size(93, 38);
            this.ui_Button_对象_下移.TabIndex = 14;
            this.ui_Button_对象_下移.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.ui_Button_对象_下移.Text文本 = "下移";
            this.ui_Button_对象_下移.文本颜色 = System.Drawing.Color.White;
            this.ui_Button_对象_下移.背景颜色 = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.ui_Button_对象_下移.背景颜色_鼠标按下 = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(131)))), ((int)(((byte)(229)))));
            this.ui_Button_对象_下移.背景颜色_鼠标移上 = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(168)))), ((int)(((byte)(255)))));
            this.ui_Button_对象_下移.背景颜色_鼠标选中 = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(131)))), ((int)(((byte)(229)))));
            this.ui_Button_对象_下移.边框颜色 = System.Drawing.Color.Silver;
            this.ui_Button_对象_下移.边框颜色_鼠标按下 = System.Drawing.Color.Silver;
            this.ui_Button_对象_下移.边框颜色_鼠标移上 = System.Drawing.Color.Silver;
            this.ui_Button_对象_下移.边框颜色_鼠标选中 = System.Drawing.Color.Silver;
            // 
            // ui_Button_对象_上移
            // 
            this.ui_Button_对象_上移.BackColor = System.Drawing.Color.Transparent;
            this.ui_Button_对象_上移.Dock = System.Windows.Forms.DockStyle.Top;
            this.ui_Button_对象_上移.Location = new System.Drawing.Point(2, 116);
            this.ui_Button_对象_上移.Name = "ui_Button_对象_上移";
            this.ui_Button_对象_上移.Padding = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.ui_Button_对象_上移.Radius_圆角 = 5;
            this.ui_Button_对象_上移.Size = new System.Drawing.Size(93, 38);
            this.ui_Button_对象_上移.TabIndex = 13;
            this.ui_Button_对象_上移.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.ui_Button_对象_上移.Text文本 = "上移";
            this.ui_Button_对象_上移.文本颜色 = System.Drawing.Color.White;
            this.ui_Button_对象_上移.背景颜色 = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.ui_Button_对象_上移.背景颜色_鼠标按下 = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(131)))), ((int)(((byte)(229)))));
            this.ui_Button_对象_上移.背景颜色_鼠标移上 = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(168)))), ((int)(((byte)(255)))));
            this.ui_Button_对象_上移.背景颜色_鼠标选中 = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(131)))), ((int)(((byte)(229)))));
            this.ui_Button_对象_上移.边框颜色 = System.Drawing.Color.Silver;
            this.ui_Button_对象_上移.边框颜色_鼠标按下 = System.Drawing.Color.Silver;
            this.ui_Button_对象_上移.边框颜色_鼠标移上 = System.Drawing.Color.Silver;
            this.ui_Button_对象_上移.边框颜色_鼠标选中 = System.Drawing.Color.Silver;
            // 
            // ui_Button_对象_删除
            // 
            this.ui_Button_对象_删除.BackColor = System.Drawing.Color.Transparent;
            this.ui_Button_对象_删除.Dock = System.Windows.Forms.DockStyle.Top;
            this.ui_Button_对象_删除.Location = new System.Drawing.Point(2, 78);
            this.ui_Button_对象_删除.Name = "ui_Button_对象_删除";
            this.ui_Button_对象_删除.Padding = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.ui_Button_对象_删除.Radius_圆角 = 5;
            this.ui_Button_对象_删除.Size = new System.Drawing.Size(93, 38);
            this.ui_Button_对象_删除.TabIndex = 12;
            this.ui_Button_对象_删除.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.ui_Button_对象_删除.Text文本 = "删除";
            this.ui_Button_对象_删除.文本颜色 = System.Drawing.Color.White;
            this.ui_Button_对象_删除.背景颜色 = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.ui_Button_对象_删除.背景颜色_鼠标按下 = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(131)))), ((int)(((byte)(229)))));
            this.ui_Button_对象_删除.背景颜色_鼠标移上 = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(168)))), ((int)(((byte)(255)))));
            this.ui_Button_对象_删除.背景颜色_鼠标选中 = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(131)))), ((int)(((byte)(229)))));
            this.ui_Button_对象_删除.边框颜色 = System.Drawing.Color.Silver;
            this.ui_Button_对象_删除.边框颜色_鼠标按下 = System.Drawing.Color.Silver;
            this.ui_Button_对象_删除.边框颜色_鼠标移上 = System.Drawing.Color.Silver;
            this.ui_Button_对象_删除.边框颜色_鼠标选中 = System.Drawing.Color.Silver;
            // 
            // ui_Button_对象_修改
            // 
            this.ui_Button_对象_修改.BackColor = System.Drawing.Color.Transparent;
            this.ui_Button_对象_修改.Dock = System.Windows.Forms.DockStyle.Top;
            this.ui_Button_对象_修改.Location = new System.Drawing.Point(2, 40);
            this.ui_Button_对象_修改.Name = "ui_Button_对象_修改";
            this.ui_Button_对象_修改.Padding = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.ui_Button_对象_修改.Radius_圆角 = 5;
            this.ui_Button_对象_修改.Size = new System.Drawing.Size(93, 38);
            this.ui_Button_对象_修改.TabIndex = 11;
            this.ui_Button_对象_修改.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.ui_Button_对象_修改.Text文本 = "修改";
            this.ui_Button_对象_修改.文本颜色 = System.Drawing.Color.White;
            this.ui_Button_对象_修改.背景颜色 = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.ui_Button_对象_修改.背景颜色_鼠标按下 = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(131)))), ((int)(((byte)(229)))));
            this.ui_Button_对象_修改.背景颜色_鼠标移上 = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(168)))), ((int)(((byte)(255)))));
            this.ui_Button_对象_修改.背景颜色_鼠标选中 = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(131)))), ((int)(((byte)(229)))));
            this.ui_Button_对象_修改.边框颜色 = System.Drawing.Color.Silver;
            this.ui_Button_对象_修改.边框颜色_鼠标按下 = System.Drawing.Color.Silver;
            this.ui_Button_对象_修改.边框颜色_鼠标移上 = System.Drawing.Color.Silver;
            this.ui_Button_对象_修改.边框颜色_鼠标选中 = System.Drawing.Color.Silver;
            // 
            // ui_Button_对象_添加
            // 
            this.ui_Button_对象_添加.BackColor = System.Drawing.Color.Transparent;
            this.ui_Button_对象_添加.Dock = System.Windows.Forms.DockStyle.Top;
            this.ui_Button_对象_添加.Location = new System.Drawing.Point(2, 2);
            this.ui_Button_对象_添加.Name = "ui_Button_对象_添加";
            this.ui_Button_对象_添加.Padding = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.ui_Button_对象_添加.Radius_圆角 = 5;
            this.ui_Button_对象_添加.Size = new System.Drawing.Size(93, 38);
            this.ui_Button_对象_添加.TabIndex = 10;
            this.ui_Button_对象_添加.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.ui_Button_对象_添加.Text文本 = "添加";
            this.ui_Button_对象_添加.文本颜色 = System.Drawing.Color.White;
            this.ui_Button_对象_添加.背景颜色 = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.ui_Button_对象_添加.背景颜色_鼠标按下 = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(131)))), ((int)(((byte)(229)))));
            this.ui_Button_对象_添加.背景颜色_鼠标移上 = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(168)))), ((int)(((byte)(255)))));
            this.ui_Button_对象_添加.背景颜色_鼠标选中 = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(131)))), ((int)(((byte)(229)))));
            this.ui_Button_对象_添加.边框颜色 = System.Drawing.Color.Silver;
            this.ui_Button_对象_添加.边框颜色_鼠标按下 = System.Drawing.Color.Silver;
            this.ui_Button_对象_添加.边框颜色_鼠标移上 = System.Drawing.Color.Silver;
            this.ui_Button_对象_添加.边框颜色_鼠标选中 = System.Drawing.Color.Silver;
            // 
            // tableLayoutPanel_配方
            // 
            this.tableLayoutPanel_配方.ColumnCount = 3;
            this.tableLayoutPanel_配方.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250F));
            this.tableLayoutPanel_配方.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel_配方.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_配方.Controls.Add(this.uiTitlePanel_对象列表, 0, 1);
            this.tableLayoutPanel_配方.Controls.Add(this.uiTitlePanel_元素列表, 2, 1);
            this.tableLayoutPanel_配方.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_配方.Location = new System.Drawing.Point(0, 70);
            this.tableLayoutPanel_配方.Name = "tableLayoutPanel_配方";
            this.tableLayoutPanel_配方.RowCount = 3;
            this.tableLayoutPanel_配方.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel_配方.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_配方.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel_配方.Size = new System.Drawing.Size(800, 430);
            this.tableLayoutPanel_配方.TabIndex = 3;
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新建ToolStripMenuItem,
            this.打开ToolStripMenuItem,
            this.删除ToolStripMenuItem,
            this.保存ToolStripMenuItem,
            this.另存为ToolStripMenuItem,
            this.导入ToolStripMenuItem,
            this.导出ToolStripMenuItem,
            this.关闭ToolStripMenuItem});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(56, 25);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // 新建ToolStripMenuItem
            // 
            this.新建ToolStripMenuItem.Name = "新建ToolStripMenuItem";
            this.新建ToolStripMenuItem.Size = new System.Drawing.Size(217, 26);
            this.新建ToolStripMenuItem.Text = "新建";
            // 
            // 打开ToolStripMenuItem
            // 
            this.打开ToolStripMenuItem.Name = "打开ToolStripMenuItem";
            this.打开ToolStripMenuItem.Size = new System.Drawing.Size(217, 26);
            this.打开ToolStripMenuItem.Text = "打开";
            // 
            // 删除ToolStripMenuItem
            // 
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.Size = new System.Drawing.Size(217, 26);
            this.删除ToolStripMenuItem.Text = "删除";
            // 
            // 保存ToolStripMenuItem
            // 
            this.保存ToolStripMenuItem.Name = "保存ToolStripMenuItem";
            this.保存ToolStripMenuItem.Size = new System.Drawing.Size(217, 26);
            this.保存ToolStripMenuItem.Text = "保存";
            // 
            // 另存为ToolStripMenuItem
            // 
            this.另存为ToolStripMenuItem.Name = "另存为ToolStripMenuItem";
            this.另存为ToolStripMenuItem.Size = new System.Drawing.Size(217, 26);
            this.另存为ToolStripMenuItem.Text = "另存为";
            // 
            // 导入ToolStripMenuItem
            // 
            this.导入ToolStripMenuItem.Name = "导入ToolStripMenuItem";
            this.导入ToolStripMenuItem.Size = new System.Drawing.Size(217, 26);
            this.导入ToolStripMenuItem.Text = "导入";
            // 
            // 导出ToolStripMenuItem
            // 
            this.导出ToolStripMenuItem.Name = "导出ToolStripMenuItem";
            this.导出ToolStripMenuItem.Size = new System.Drawing.Size(217, 26);
            this.导出ToolStripMenuItem.Text = "导出";
            // 
            // 关闭ToolStripMenuItem
            // 
            this.关闭ToolStripMenuItem.Name = "关闭ToolStripMenuItem";
            this.关闭ToolStripMenuItem.Size = new System.Drawing.Size(217, 26);
            this.关闭ToolStripMenuItem.Text = "关闭";
            // 
            // 视图ToolStripMenuItem
            // 
            this.视图ToolStripMenuItem.Name = "视图ToolStripMenuItem";
            this.视图ToolStripMenuItem.Size = new System.Drawing.Size(61, 25);
            this.视图ToolStripMenuItem.Text = "视图";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("微软雅黑", 10.01739F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(19, 19);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.视图ToolStripMenuItem,
            this.配置ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 35);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5);
            this.menuStrip1.Size = new System.Drawing.Size(800, 35);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 配置ToolStripMenuItem
            // 
            this.配置ToolStripMenuItem.Name = "配置ToolStripMenuItem";
            this.配置ToolStripMenuItem.Size = new System.Drawing.Size(56, 25);
            this.配置ToolStripMenuItem.Text = "配置";
            // 
            // tableLayoutPanel_下边栏
            // 
            this.tableLayoutPanel_下边栏.ColumnCount = 3;
            this.tableLayoutPanel_下边栏.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_下边栏.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel_下边栏.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tableLayoutPanel_下边栏.Controls.Add(this.textBox_信息, 2, 0);
            this.tableLayoutPanel_下边栏.Controls.Add(this.textBox_备注, 0, 0);
            this.tableLayoutPanel_下边栏.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel_下边栏.Location = new System.Drawing.Point(0, 500);
            this.tableLayoutPanel_下边栏.Name = "tableLayoutPanel_下边栏";
            this.tableLayoutPanel_下边栏.RowCount = 1;
            this.tableLayoutPanel_下边栏.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_下边栏.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel_下边栏.Size = new System.Drawing.Size(800, 100);
            this.tableLayoutPanel_下边栏.TabIndex = 5;
            // 
            // textBox_信息
            // 
            this.textBox_信息.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBox_信息.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_信息.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_信息.Font = new System.Drawing.Font("微软雅黑", 10.01739F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_信息.ForeColor = System.Drawing.Color.Teal;
            this.textBox_信息.Location = new System.Drawing.Point(503, 3);
            this.textBox_信息.Multiline = true;
            this.textBox_信息.Name = "textBox_信息";
            this.textBox_信息.ReadOnly = true;
            this.textBox_信息.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_信息.Size = new System.Drawing.Size(294, 94);
            this.textBox_信息.TabIndex = 8;
            // 
            // textBox_备注
            // 
            this.textBox_备注.BackColor = System.Drawing.Color.White;
            this.textBox_备注.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_备注.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_备注.Font = new System.Drawing.Font("微软雅黑", 10.01739F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_备注.ForeColor = System.Drawing.Color.Teal;
            this.textBox_备注.Location = new System.Drawing.Point(3, 3);
            this.textBox_备注.Multiline = true;
            this.textBox_备注.Name = "textBox_备注";
            this.textBox_备注.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_备注.Size = new System.Drawing.Size(484, 94);
            this.textBox_备注.TabIndex = 0;
            // 
            // Form_主窗体
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.tableLayoutPanel_配方);
            this.Controls.Add(this.tableLayoutPanel_下边栏);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form_主窗体";
            this.Text = "";
            this.uiTitlePanel_元素列表.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_元素)).EndInit();
            this.panel2.ResumeLayout(false);
            this.uiTitlePanel_对象列表.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel_配方.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tableLayoutPanel_下边栏.ResumeLayout(false);
            this.tableLayoutPanel_下边栏.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Sunny.UI.UITitlePanel uiTitlePanel_元素列表;
        private System.Windows.Forms.Panel panel2;
        private Sunny.UI.UITitlePanel uiTitlePanel_对象列表;
        private Sunny.UI.UIListBox uiListBox_对象列表;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_配方;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 新建ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 保存ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 另存为ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关闭ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 视图ToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 配置ToolStripMenuItem;
        private Sunny.ui_Button2 ui_Button_对象_修改;
        private Sunny.ui_Button2 ui_Button_对象_添加;
        private Sunny.ui_Button2 ui_Button_元素_下移;
        private Sunny.ui_Button2 ui_Button_元素_上移;
        private Sunny.ui_Button2 ui_Button_元素_删除;
        private Sunny.ui_Button2 ui_Button_元素_修改;
        private Sunny.ui_Button2 ui_Button_元素_添加;
        private Sunny.ui_Button2 ui_Button_对象_预览;
        private Sunny.ui_Button2 ui_Button_对象_保存;
        private Sunny.ui_Button2 ui_Button_对象_下移;
        private Sunny.ui_Button2 ui_Button_对象_上移;
        private Sunny.ui_Button2 ui_Button_对象_删除;
        public System.Windows.Forms.DataGridView dataGridView_元素;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_下边栏;
        private System.Windows.Forms.TextBox textBox_信息;
        private System.Windows.Forms.TextBox textBox_备注;
        private System.Windows.Forms.ToolStripMenuItem 导入ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导出ToolStripMenuItem;
    }
}