using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace qfNet
{
    public class winForm窗体
    {
        /// <summary>
        /// 使用前先new一下
        /// </summary>
        /// <param name="要嵌入的窗体"></param>
        /// <param name="容器"></param>
        /// <param name="是否最顶层"></param>
        /// <param name="边框"></param>
        public void 窗体_将窗体嵌入到容器中(Form 要嵌入的窗体, Control 容器, bool 是否最顶层, FormBorderStyle 边框)
        {
            //panel控件.Width = width;
            //panel控件.Height = height;

            要嵌入的窗体.BackColor = 容器.BackColor;
            容器.Controls.Clear();
            要嵌入的窗体.TopLevel = 是否最顶层;//不是最顶层窗体
            要嵌入的窗体.Dock = DockStyle.Fill;//在父窗体中占满
            要嵌入的窗体.FormBorderStyle = 边框;// FormBorderStyle.None;//无边框
            容器.Controls.Add(要嵌入的窗体);
            要嵌入的窗体.Show();
        }


        /// <summary>
        /// 0:为隐藏
        /// </summary>
        /// <param name="窗体"></param>
        /// <param name="透明度"></param>
        public void 设置窗体透明度(Form 窗体, double 透明度)
        {
            窗体.Opacity = 透明度;
        }


        public const int HWND_TOP = 0;
        public const int HWND_BOTTOM = 1;
        public const int HWND_TOPMOST = -1;
        public const int HWND_NOTOPMOST = -2;

        //设置此窗体为活动窗体：
        //将创建指定窗口的线程带到前台并激活该窗口。键盘输入直接指向窗口，并为用户更改各种视觉提示。
        //系统为创建前台窗口的线程分配的优先级略高于其他线程。
        [DllImport("user32.dll", EntryPoint = "SetForegroundWindow")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        //设置此窗体为活动窗体：
        //激活窗口。窗口必须附加到调用线程的消息队列。
        [DllImport("user32.dll", EntryPoint = "SetActiveWindow")]
        public static extern IntPtr SetActiveWindow(IntPtr hWnd);

        //设置窗体位置
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int y, int Width, int Height, int flags);



        public void 窗体显示在最上层(IntPtr intptr_)
        {
            // 设置窗体显示在最上层
            SetWindowPos(intptr_, -1, 0, 0, 0, 0, 0x0001 | 0x0002 | 0x0010 | 0x0080);
        }


        public void 本窗体为活动窗体(IntPtr intptr_)
        {
            // 设置本窗体为活动窗体
            SetActiveWindow(intptr_);
            SetForegroundWindow(intptr_);
        }

        public void 窗体置顶(Form forms)
        {
            // 设置窗体置顶
            forms.TopMost = true;
        }

        public void 窗体置顶(Sunny.UI.UIForm forms)
        {
            // 设置窗体置顶
            forms.TopMost = true;
        }



        public void 居中(Sunny.UI.UIForm forms, Control 容器_main, FormWindowState Form状态)
        {
            forms.WindowState = Form状态;
            int width = forms.Width;
            int height = forms.Height;
            int w = width / 2 - 容器_main.Width / 2;
            int h = height / 2 - (容器_main.Height - forms.TitleHeight) / 2;
            容器_main.Left = w;
            容器_main.Top = h;
        }

        public void 居中(Sunny.UI.UIForm forms, Control 容器_main)
        {

            int width = forms.Width;
            int height = forms.Height;
            int w = width / 2 - 容器_main.Width / 2;
            int h = height / 2 - (容器_main.Height - forms.TitleHeight) / 2;
            容器_main.Left = w;
            容器_main.Top = h;
        }



        public void 居中(Control 容器_Max, Control 容器_main)
        {
            int w = 容器_Max.Width / 2 - 容器_main.Width / 2;
            int h = 容器_Max.Height / 2 - 容器_main.Height / 2;
            容器_main.Left = w;
            容器_main.Top = h;
        }


        public class _cfg_winForm_
        {
            
            /// <summary>
            /// 最大化框
            /// </summary>
            public bool MaximizeBox { set; get; } = false;
            /// <summary>
            /// 最小化框
            /// </summary>
            public bool MinimizeBox { set; get; } = false;
            /// <summary>
            /// 关闭按钮
            /// </summary>
            public bool CloseimizeBox { set; get; } = true;

            /// <summary>
            /// 窗体控制按钮
            /// </summary>
            public bool ControlBox { set; get; } = true;

            /// <summary>
            /// 显示在任务栏
            /// </summary>
            public bool ShowInTaskbar { set; get; } = false;

            public bool ShowIcon { set; get; } = true;

            /// <summary>
            /// 边距
            /// </summary>
            public int Padding { set; get; } = 5;

            /// <summary>
            /// 窗体状态
            /// </summary>
            public FormWindowState WindowState_ { set; get; } = FormWindowState.Normal;

           
        }


        public void Set设置(Sunny.UI.UIForm forms, _cfg_winForm_ cfg, string Title = "")
        {
            forms.MaximizeBox = cfg.MaximizeBox;
            forms.MinimizeBox = cfg.MinimizeBox;
            forms.CloseimizeBox = cfg.CloseimizeBox;

            forms.ControlBox = cfg.ControlBox;
            forms.ShowInTaskbar = cfg.ShowInTaskbar;
            forms.ShowIcon = cfg.ShowIcon;

            Padding pad = forms.Padding;
            pad.Top += cfg.Padding;
            pad.Left += cfg.Padding;
            pad.Bottom += cfg.Padding;
            pad.Right += cfg.Padding;
            forms.Padding = pad;

            forms.WindowState = cfg.WindowState_;
        }
        public void Set设置_Padding(Sunny.UI.UIForm forms,int 边距=5)
        { 
            Padding pad = forms.Padding;
            pad.Top += 边距;
            pad.Left += 边距;
            pad.Bottom += 边距;
            pad.Right += 边距;
            forms.Padding = pad;
             
        }



    }
}
