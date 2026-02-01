using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace qfmain
{
    internal class 程序捕获异常_helper
    {

        /// <summary>异常发生时的回调（释放资源 / 通知外部）</summary>
        public Action<Exception> Event_Exception;

        /// <summary>是否只记录一次异常</summary>
        public bool HandleOnce { get; set; } = true;

        /// <summary>是否在异常后主动退出进程（Service 建议 true）</summary>
        public bool ExitProcess { get; set; } = true ;

        private int _handledFlag = 0;

        #region 初始化

        public void Init()
        {
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
            TaskScheduler.UnobservedTaskException += OnUnobservedTaskException;
        }

        #endregion

        #region 异常入口

        private void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            HandleException(
                e.ExceptionObject as Exception,
                "AppDomain.UnhandledException"
            );

            // CLR 已决定是否终止，这里只做善后
            if (ExitProcess && e.IsTerminating)
            {
                Environment.Exit(-1);
            }
        }

        private void OnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            HandleException(
                e.Exception,
                "TaskScheduler.UnobservedTaskException"
            );

            e.SetObserved(); // 防止 CLR 直接杀进程
        }

        #endregion

        #region 核心处理

        private void HandleException(Exception ex, string source)
        {
            if (ex == null) return;

            // 多线程 + 多入口只处理一次
            if (HandleOnce &&
                Interlocked.Exchange(ref _handledFlag, 1) == 1)
                return;

            try
            {
                WriteLog(BuildLog(ex, source));
            }
            catch
            {
                // 日志失败必须吞掉
            }

            try
            {
                Event_Exception?.Invoke(ex);
            }
            catch
            {
                // 回调异常必须吞掉
            }
        }

        #endregion

        #region 日志

        private string BuildLog(Exception ex, string source)
        {
            var sb = new StringBuilder(512);
            sb.AppendLine("======================================");
            sb.AppendLine($"Time   : {DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}");
            sb.AppendLine($"Source : {source}");
            sb.AppendLine(ex.ToString());
            sb.AppendLine("======================================");
            return sb.ToString();
        }

        private void WriteLog(string log)
        {
            string path = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "ErrMyApp.log"
            );

            File.AppendAllText(path, log, Encoding.UTF8);
        }

        #endregion
    }



}
