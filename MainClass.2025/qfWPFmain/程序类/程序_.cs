using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace qfWPFmain
{
    public class 程序_
    {

        /// <summary>
        /// 程序启动时
        /// </summary>
        /// <returns>true:未运行,false:已运行</returns>
        public bool App_Onstart()
        {

            bool rtJC = new qfWPFmain.进程_().程序是否已运行();
            if (rtJC)
            {
                MessageBox.Show(qfWPFmain.Language_.Get语言("程序已运行"), "", MessageBoxButton.OK, MessageBoxImage.Error);
                结束自身进程();
                return false;
            }
            return true;
        }

        public void 结束自身进程()
        {
            new 进程_().Wpf_结束自身进程();
        }





        #region 捕获程序异常




        static bool Is异常结束自身进程 = false;

        /// <summary>
        /// 需要在App中添加事件
        /// <para>DispatcherUnhandledException += App_DispatcherUnhandledException;</para>
        /// </summary>
        public void Err_捕获全局异常(bool Is异常结束自身进程_=false )
        {
            Is异常结束自身进程 = Is异常结束自身进程_;
            //// 捕获UI线程未处理异常
            // DispatcherUnhandledException += App_DispatcherUnhandledException;

            // 捕获非UI线程未处理异常
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            // 捕获Task线程中的未处理异常
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;


        }


        string ErrPath = Environment.CurrentDirectory + "\\Err_myApp.txt";
        void WriteErr(string ErrValue)
        {
            string xt = "-------------------- Err --------------------\r\n";
            xt += $"{ErrValue}\r\n";
            xt += "------------------------------End\r\n";
            new 文本().Save(ErrPath, xt, false, out string msgErr);
        }

        /// <summary>
        /// UI线程未处理异常
        /// <para>需要在App中添加事件:</para>
        /// <para>DispatcherUnhandledException += App_DispatcherUnhandledException;</para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            string err = $"UI线程错误: {e.Exception?.Message}";
            err += $"堆栈跟踪: {e.Exception?.StackTrace}\r\n";

            // 处理内部异常（如果有）
            if (e.Exception.InnerException is not null)
            {
                err += $"内部异常：{e.Exception.InnerException?.Message}\r\n";
                err += $"内部异常堆栈：{e.Exception.InnerException?.StackTrace}";
            }

            WriteErr(err);
            if (Is异常结束自身进程)
            {
                结束自身进程();
            }
            else
            {
                结束自身进程();
                e.Handled = true; // 标记为已处理，防止应用崩溃
            }
            
        }

        /// <summary>
        /// 非UI线程未处理异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = e.ExceptionObject as Exception;
            string err = $"非UI线程错误: {ex?.Message}\r\n";
            err += $"堆栈跟踪: {ex?.StackTrace}\r\n";


            // 处理内部异常（如果有）
            if (ex.InnerException is not null)
            {
                err += $"内部异常：{ex.InnerException?.Message}\r\n";
                err += $"内部异常堆栈：{ex.InnerException?.StackTrace}";
            }

            WriteErr(err);

            // 对于严重错误，可能无法阻止应用退出
            if (e.IsTerminating)
            {
                // 可以在这里做一些最后的清理工作
                结束自身进程();
            }

            if (Is异常结束自身进程)
            {
                结束自身进程();
            }
        }

        /// <summary>
        ///  Task未观察到的异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {


            // e.SetObserved(); // 标记为已观察，防止应用在GC时崩溃


            // 获取异常对象（可能包含多个异常，需遍历）
            var exception = e.Exception;

            // 输出异常信息（调试时可断点查看）
            string err = $"未处理的Task异常：{exception?.Message}\r\n";
            err += $"堆栈跟踪：{exception?.StackTrace}\r\n";

            // 处理内部异常（如果有）
            if (exception.InnerException is not null)
            {
                err += $"内部异常：{exception.InnerException?.Message}\r\n";
                err += $"内部异常堆栈：{exception.InnerException?.StackTrace}";
            }
            WriteErr(err);
            if (Is异常结束自身进程)
            {
                结束自身进程();
            }
        }


        #endregion



    }
}

