using OfficeOpenXml.ConditionalFormatting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace qfmain
{
    public class 程序_捕获异常_Win
    {

        //public delegate void 异常退出事件_event();
        //public event 异常退出事件_event Event_异常退出事件 = null;

        public Action Action_异常退出事件;
        void On_退出()
        {
            Action_异常退出事件?.Invoke();
        }



        /// <summary>
        /// 是否退出应用程序
        /// </summary>
        bool glExitApp = true;
        // static string path=

        /// <summary>
        /// 处理未捕获异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {

            SaveLog($"<{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff")}>" + "-----------------------begin--------------------------");
            SaveLog("CurrentDomain_UnhandledException" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss:fff"));
            SaveLog("IsTerminating : " + e.IsTerminating.ToString());
            SaveLog(e.ExceptionObject.ToString());
            SaveLog("-----------------------end----------------------------");
            while (glExitApp)
            {//循环处理，否则应用程序将会退出
                try
                {
                    if (glExitApp)
                    {//标志应用程序可以退出，否则程序退出后，进程仍然在运行
                        SaveLog("ExitApp");
                        On_退出();
                        return;
                    }
                    System.Threading.Thread.Sleep(1 * 1000);
                }
                catch (Exception)
                {
                }
            }
            ;
        }

        /// <summary>
        /// 处理UI主线程异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            string xt = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "-----------------------<UI>begin--------------------------";
            xt += "\r\n" + "Application_ThreadException:" + e.Exception.Message;
            xt += "\r\n" + e.Exception.StackTrace;
            xt += "-----------------------end----------------------------";
            SaveLog(xt);

            // SaveLog(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "-----------------------<UI>begin--------------------------");
            // SaveLog("Application_ThreadException:" + e.Exception.Message);
            // SaveLog(e.Exception.StackTrace);
            // SaveLog("-----------------------end----------------------------");
            On_退出();
        }


        void SaveLog(string log)
        {
            //string filePath = AppDomain.CurrentDomain.BaseDirectory + @"\Err MyApp.txt";

            //string filePath = AppDomain.CurrentDomain.BaseDirectory + @"\Err MyApp.txt";
            ////采用using关键字，会自动释放
            //using (FileStream fs = new FileStream(filePath, FileMode.Append))
            //{
            //    using (StreamWriter sw = new StreamWriter(fs, Encoding.Default))
            //    {
            //        sw.WriteLine(log);
            //    }
            //}

            string filePath = Environment.CurrentDirectory + @"\Err MyApp.txt";
            new 文本().Save_25(filePath, log, false, out string msgErr, true, Encoding.Default);
        }


        public virtual void 捕获异常()
        {
            glExitApp = true;//标志应用程序可以退出

            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            //处理UI线程异常
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            //处理非线程异常
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

        }

        public virtual void 释放()
        {
            glExitApp = false;
        }

    }
}
