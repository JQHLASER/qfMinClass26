using OfficeOpenXml.ConditionalFormatting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace qfmain
{
    public class 程序_捕获异常_Win
    {
        /// <summary>异常时的统一回调（关闭设备 / 保存状态等）</summary>
        public Action<Exception> Event_Exception;

        /// <summary>是否自动退出程序</summary>
        public bool AutoExit { get; set; } = false;

        /// <summary>是否只记录一次异常（防止连环崩）</summary>
        public bool LogOnce { get; set; } = true;

        private int _handledFlag = 0;

        #region 初始化

        public void Init()
        {
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            Application.ThreadException += UI线程异常;
            AppDomain.CurrentDomain.UnhandledException += 非UI异常;
            TaskScheduler.UnobservedTaskException += Task异常;
        }

        #endregion

        #region 异常入口

        /// <summary>
        /// UI 主线程异常
        /// </summary>
        private void UI线程异常(object sender, ThreadExceptionEventArgs e)
        {
            HandleException(e.Exception, "UI线程异常");

            if (AutoExit)
                Application.Exit();
        }

        /// <summary>
        /// 非 UI 未捕获异常
        /// </summary>
        private void 非UI异常(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = e.ExceptionObject as Exception;
            HandleException(ex, "非UI未捕获异常");

            // CLR 已决定是否终止进程，这里只做善后
        }

        /// <summary>
        /// Task / async 异常
        /// </summary>
        private void Task异常(object sender, UnobservedTaskExceptionEventArgs e)
        {
            HandleException(e.Exception, "Task未观察异常");
            e.SetObserved(); // 防止 CLR 直接杀进程
        }

        #endregion

        #region 核心处理

        private void HandleException(Exception ex, string source)
        {
            if (ex == null) return;

            // 保证只处理一次（线程安全）
            if (LogOnce && Interlocked.Exchange(ref _handledFlag, 1) == 1)
                return;

            try
            {
                SaveLog(BuildLog(ex, source));
            }
            catch
            {
                // 日志失败也不能再抛异常
            }

            try
            {
                Event_Exception?.Invoke(ex);
            }
            catch
            {
                // 回调异常必须吞掉，防止递归崩溃
            }
        }

        #endregion

        #region 日志

        private string BuildLog(Exception ex, string source)
        {
            var sb = new StringBuilder(512);
            sb.AppendLine("======================================");
            sb.AppendLine($"时间: {DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}");
            sb.AppendLine($"来源: {source}");
            sb.AppendLine(ex.ToString());
            sb.AppendLine("======================================");
            return sb.ToString();
        }

        private void SaveLog(string log)
        { 
            string path = Path .Combine (  AppDomain.CurrentDomain.BaseDirectory , "ErrMyApp.log");
            System.IO.File.AppendAllText(path, log, Encoding.UTF8);
        }

        #endregion

        
        public virtual void 捕获异常()
        { 
            Init(); 
        }



        public Action Event_异常退出事件;
        void On_退出()
        {
            Event_异常退出事件?.Invoke();
        }













    }
}
