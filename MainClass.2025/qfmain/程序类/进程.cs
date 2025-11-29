using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace qfmain 
{
    public  class 进程
    {




        /// <summary>
        /// true表示已运行,false表示未运行
        /// </summary>
        /// <returns></returns>
        public bool 进程是否存在(string 进程Name)
        {
            return 进程是否存在(进程Name, out string msgErr);
        }

        /// <summary>
        /// true表示已运行,false表示未运行
        /// </summary>
        /// <returns></returns>
        public bool 进程是否存在(string 进程Name, out string msgErr)
        {

            msgErr = string.Empty;
            try
            {
                //  System.Diagnostics.Process[] myProcesses = System.Diagnostics.Process.GetProcessesByName(进程Name);//获取指定的进程名 

                Process[] myProcesses = Process.GetProcessesByName(进程Name);

                if (myProcesses.Length > 0) //如果可以获取到知道的进程名则说明已经启动
                {
                    //  MessageBox.Show("程序已启动！");
                    // Application.Exit();//关闭系统
                    msgErr = "进程存在";
                    return true;
                }
                else
                {
                    msgErr = "进程不存在";
                    return false;
                }

            }
            catch (Exception ex)
            {
                msgErr = $"Err:{ex.Message}";
                return false;
            }

        }


        public string 获取自身进程名()
        {

            return System.Diagnostics.Process.GetCurrentProcess().ProcessName;
        }


        public bool 程序是否已运行()
        {
            if (System.Diagnostics.Process.GetProcessesByName(System.Diagnostics.Process.GetCurrentProcess().ProcessName).GetUpperBound(0) > 0)
            {

                return true;
                //if (UBound(Diagnostics.Process.GetProcessesByName(Diagnostics.Process.GetCurrentProcess.ProcessName)) > 0)


            }
            else
            {
                return false;
            }


        }


        public IntPtr 进程名取主窗体句柄(string 进程名)
        {
            IntPtr s = Process.GetProcessesByName(进程名)[0].MainWindowHandle;
            return s;
        }


        public IntPtr 进程名取主窗体句柄(string 进程名, out string msgErr)
        {
            IntPtr s = IntPtr.Zero;
            msgErr = string.Empty;
            try
            {
                s = Process.GetProcessesByName(进程名)[0].MainWindowHandle;
            }
            catch (Exception ex)
            {
                msgErr = ex.Message;
                s = IntPtr.Zero;
            }
            return s;
        }



        public void 结束自身进程3(int exitCode)
        {
            Environment.Exit(0);//结束当前进程    
        }

        public void 结束自身进程()
        {
            Application.Exit();//结束当前进程    
        }

        /// <summary>
        /// 在使用前,必须安全的释放资源
        /// </summary>
        public void 结束自身进程_暴力()
        {
            Process.GetCurrentProcess().Kill();//暴力关闭进程
        }


        /// <summary>
        /// 反馈执行结果
        /// </summary>
        /// <returns></returns>
        public void 结束自身进程2()
        {
            string cmmd = 获取自身进程名();
            Process[] myProcess = Process.GetProcessesByName(cmmd);
            TerminateProcess(int.Parse(myProcess[0].Handle.ToString()), 0);

        }



        [DllImport("kernel32")]
        public static extern long TerminateProcess(int handle, int exitCode);


        public bool 结束指定进程_1(string 进程名, out string Msg)//关闭线程
        {
            try
            {
                Process[] thisproc = Process.GetProcessesByName(进程名);
                //thisproc.lendth:名字为进程总数
                if (thisproc.Length > 0)
                {
                    for (int i = 0; i < thisproc.Length; i++)
                    {
                        if (!thisproc[i].CloseMainWindow()) //尝试关闭进程 释放资源
                        {
                            thisproc[i].Kill(); //强制关闭
                        }
                        Msg = "进程关闭成功";
                        return true;

                    }
                    Msg = String.Empty;
                    return true;
                }
                else
                {
                    Msg = "进程关闭失败!";
                    return false;
                }



            }
            catch //出现异常，表明 kill 进程失败
            {
                Msg = "进程关闭失败!";
                return false;
            }

        }

        public void 结束指定进程(string 进程名)
        {
            Process[] myProcess = Process.GetProcessesByName(进程名);
            TerminateProcess(int.Parse(myProcess[0].Handle.ToString()), 0);
        }

        public bool 结束指定进程(string 进程名, out string msgErr)
        {
            bool rt = true;
            msgErr = string.Empty;
            try
            {
                结束指定进程(进程名);
            }
            catch (Exception ex)
            {
                msgErr = ex.Message;
                rt = false;
            }

            return rt;
        }


        public string 结束指定进程_cmd(string 进程名)
        {
            //實例一個Process類，啟動一個獨立進程  
            System.Diagnostics.Process p = new System.Diagnostics.Process();

            //Process類有一個StartInfo屬性，這個是ProcessStartInfo類，包括了一些屬性和方法，下面我們用到了他的幾個屬性：  

            p.StartInfo.FileName = "cmd.exe";           //設定程序名  
            p.StartInfo.Arguments = "/c " + 进程名;    //設定程式執行參數  
            p.StartInfo.UseShellExecute = false;        //關閉Shell的使用  
            p.StartInfo.RedirectStandardInput = true;   //重定向標準輸入  
            p.StartInfo.RedirectStandardOutput = true;  //重定向標準輸出  
            p.StartInfo.RedirectStandardError = true;   //重定向錯誤輸出  
            p.StartInfo.CreateNoWindow = true;          //設置不顯示窗口  

            p.Start();   //啟動  

            //p.StandardInput.WriteLine(command);       //也可以用這種方式輸入要執行的命令  
            //p.StandardInput.WriteLine("exit");        //不過要記得加上Exit要不然下一行程式執行的時候會當機  

            return p.StandardOutput.ReadToEnd();        //從輸出流取得命令執行結果  

        }



        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        public bool 根据句柄设置父窗口(IntPtr 窗体句柄)
        {
            return SetForegroundWindow(窗体句柄);
        }





        [DllImport("user32.dll")]
        private static extern IntPtr FindWindowA(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        [DllImport("user32")]
        private static extern int SetWindowPos(IntPtr hwnd, int hWndInsertAfter, int x, int y, int w, int h, int flag);

        [DllImport("user32.dll", EntryPoint = "FindWindow", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);


        [System.Runtime.InteropServices.DllImport("win32.dll", EntryPoint = "win_Get_handle")]
        private static extern IntPtr 取句柄dll(string 一级类名, string 窗口标题, int 二级控件ID, int 三级控件ID);

        [System.Runtime.InteropServices.DllImport("win32.dll", EntryPoint = "win_Set_state")]
        private static extern int 隐藏控件dll(IntPtr 句柄, bool T显示F隐藏);




        public IntPtr 取句柄(string 一级类名, string 窗口标题, int 二级控件ID, int 三级控件ID)
        {
            return 取句柄dll(一级类名, 窗口标题, 二级控件ID, 三级控件ID);
        }

        public int 隐藏控件(IntPtr 句柄, bool T显示F隐藏)
        {
            return 隐藏控件dll(句柄, T显示F隐藏);
        }





        public void 根据句柄显示或去掉窗体边框(IntPtr 句柄, bool T显示边框F去掉边框)
        {

            const int GWL_STYLE = (-16);
            const int WS_CAPTION = 0xC00000;
            if (T显示边框F去掉边框)
            {
                SetWindowLong(句柄, GWL_STYLE, GetWindowLong(句柄, GWL_STYLE) | WS_CAPTION);
            }
            else
            {
                SetWindowLong(句柄, GWL_STYLE, GetWindowLong(句柄, GWL_STYLE) & ~WS_CAPTION);
            }




        }

        //public void 根据句柄显示窗体边框(IntPtr 句柄)
        //{
        //    const int GWL_STYLE = (-16);
        //    const int WS_CAPTION = 0xC00000;
        //    SetWindowLong(句柄, GWL_STYLE, GetWindowLong(句柄, GWL_STYLE) | WS_CAPTION);
        //}

        public void 根据句柄设置窗体位置大小(IntPtr 句柄, int left, int top, int width, int height)
        {
            if (句柄.ToInt32() != 0)
            {
                SetWindowPos(句柄, 0, left, top, width, height, 0x46);
            }

        }


        [DllImport("user32.dll")]
        private extern static int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        public string 根据句柄获取窗体标题(IntPtr 句柄)
        {
            StringBuilder s = new StringBuilder(512);
            int i = GetWindowText(句柄, s, s.Capacity); //把this.handle换成你需要的句柄                
            return s.ToString();
        }






        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int Left;                             //最左坐标
            public int Top;                             //最上坐标
            public int Right;                           //最右坐标
            public int Bottom;                        //最下坐标

        }


        public void 根据句柄取窗体大小(IntPtr 句柄, out int width, out int height, out int top, out int left)
        {
            //  InPtr awin = GetForegroundWindow();    //获取当前窗口句柄
            RECT rc = new RECT();
            GetWindowRect(句柄, ref rc);
            width = rc.Right - rc.Left;  //窗口的宽度
            height = rc.Bottom - rc.Top;  //窗口的高度
            left = rc.Left;
            top = rc.Top;

        }



        /// <summary>
        /// <para> 如果打开的是文件,窗口标题每次都不一样,就使用窗口类,不使用就填null</para>
        /// <para>例: IntPtr chwnd = CcdClass1.FindWindow(null, "CCD Parameter Set");</para>
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public IntPtr 根据窗体标题取句柄(string 类名, string title)
        {
            return FindWindow(类名, title);
        }


        /// <summary>     
        ///例: CcdClass1.SetWindowPos(句柄, -1, 0, 0, 0, 0, 1 | 2)置前的;
        /// </summary>
        /// <param name="hwnd">句柄</param>
        /// <param name="hWndInsertAfter">用于标识在z-顺序的此 CWnd 对象之前的 CWnd 对象。如果uFlags参数中设置了SWP_NOZORDER标记则本参数将被忽略。可为下列值之一：
        /// // HWND_BOTTOM：值为1，将窗口置于Z序的底部。如果参数hWnd标识了一个顶层窗口，则窗口失去顶级位置，并且被置在其他窗口的底部。
        ///   //HWND_NOTOPMOST：值为-2，将窗口置于所有非顶层窗口之上（即在所有顶层窗口之后）。如果窗口已经是非顶层窗口则该标志不起作用。
        ///  //HWND_TOP：值为0，将窗口置于Z序的顶部。
        ///   //HWND_TOPMOST：值为-1，将窗口置于所有非顶层窗口之上。即使窗口未被激活窗口也将保持顶级位置。</param>
        /// <param name="left">以客户坐标指定窗口新位置的左边界</param>
        /// <param name="top">以客户坐标指定窗口新位置的顶边界。</param>
        /// <param name="width">以像素指定窗口的新的宽度。</param>
        /// <param name="height">以像素指定窗口的新的高度。</param>
        /// <param name="flag">窗口尺寸和定位的标志。该参数可以是下列值的组合：
        ///   1:   SWP_ASYNCWINDOWPOS：如果调用进程不拥有窗口，系统会向拥有窗口的线程发出需求。这就防止调用线程在其他线程处理需求的时候发生死锁。
        ///    2:SWP_DEFERERASE：防止产生WM_SYNCPAINT消息。
        ///    3: SWP_DRAWFRAME：在窗口周围画一个边框（定义在窗口类描述中）。
        ///   4:SWP_FRAMECHANGED：给窗口发送WM_NCCALCSIZE消息，即使窗口尺寸没有改变也会发送该消息。如果未指定这个标志，只有在改变了窗口尺寸时才发送WM_NCCALCSIZE。
        ///   5:  SWP_HIDEWINDOW;隐藏窗口。
        ///   6:  SWP_NOACTIVATE：不激活窗口。如果未设置标志，则窗口被激活，并被设置到其他最高级窗口或非最高级组的顶部（根据参数hWndlnsertAfter设置）。
        ///    7: SWP_NOCOPYBITS：清除客户区的所有内容。如果未设置该标志，客户区的有效内容被保存并且在窗口尺寸更新和重定位后拷贝回客户区。
        ///   8: SWP_NOMOVE：维持当前位置（忽略X和Y参数）。
        ///    9: SWP_NOOWNERZORDER：不改变z序中的所有者窗口的位置。
        ///     10: SWP_NOREDRAW:不重画改变的内容。如果设置了这个标志，则不发生任何重画动作。适用于客户区和非客户区（包括标题栏和滚动条）和任何由于窗回移动而露出的父窗口的所有部分。如果设置了这个标志，应用程序必须明确地使窗口无效并区重画窗口的任何部分和父窗口需要重画的部分。
        ///    11: SWP_NOREPOSITION：与SWP_NOOWNERZORDER标志相同。
        ///    12: SWP_NOSENDCHANGING：防止窗口接收WM_WINDOWPOSCHANGING消息。
        ///      13:  SWP_NOSIZE：维持当前尺寸（忽略cx和Cy参数）。
        ///    14: SWP_NOZORDER：维持当前Z序（忽略hWndlnsertAfter参数）。
        ///     15:SWP_SHOWWINDOW：显示窗口。</param>
        ///     /// <returns></returns>
        public int 根据句柄操作窗体(IntPtr hwnd, int hWndInsertAfter, int left, int top, int width, int height, int flag)
        {
            return SetWindowPos(hwnd, hWndInsertAfter, left, top, width, height, flag);
        }



        /// <summary>
        /// true为有效,flse为无效
        /// </summary>
        /// <param name="句柄"></param>
        /// <returns></returns>
        public bool 根据句柄判断窗体是否有效(IntPtr 句柄)
        {
            if (句柄 != IntPtr.Zero)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



    }
}
