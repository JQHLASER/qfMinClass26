using Newtonsoft.Json;
using qfmain;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static qfmain.log日志;

namespace qf_Contol
{
    public class Log日志
    {

        #region 数据结构


        /// <summary>
        /// 系统参数
        /// </summary>
        public class _cfg_
        {
            /// <summary>
            /// 日志文件标识 : 空时为日期,否则,为日期_标识,
            /// <para>在多个激光头时,分开存储日志,以方便查看,</para>
            /// <para>例:2025-01-01_Laser1</para>
            /// </summary>
            public string 文件标识 { set; get; } = "";

            public bool 使能_保存 { set; get; } = true;

            /// <summary>
            /// 存放日志的文件夹
            /// </summary>
            public string Files_Log { set; get; } = qfmain.软件类.Files_Cfg.Files_LogMyApp;

            /// <summary>
            /// 单位:天,=0时表示不清理
            /// </summary>
            public int 保存天数 { set; get; } = 90;

            /// <summary>
            /// 清除超过保存天数的文件
            /// </summary>
            public bool 使能_清除线程 { set; get; } = true;

            /// <summary>
            ///  ms,默认为1小时一次
            /// </summary>
            public int 清除线程周期 { set; get; } = 1000 * 60 * 60 * 1;

            /// <summary>
            /// 默认超过20M后,重新保存文件
            /// </summary>
            public bool 使能_文件大小限制 { set; get; } = true;

            /// <summary>
            /// 单位: MB
            /// </summary>
            public int 日志文件大小 { set; get; } = 20;

            public _cfg_()
            {

            }

            /// <summary>
            /// Files_Log_为空时表示使能默认路径
            /// </summary>      
            public _cfg_(string 文件标识_,
                         bool 使能_保存_,
                         int 保存天数_,
                         bool 使能_清除线程_,
                         string Files_Log_ = "",
                         int 清除线程周期_ = 1000 * 60 * 60 * 1)
            {

                this.文件标识 = 文件标识_;
                this.使能_保存 = 使能_保存_;
                this.保存天数 = 保存天数_;
                this.使能_清除线程 = 使能_清除线程_;
                this.Files_Log = string.IsNullOrEmpty(Files_Log_) ? qfmain.软件类.Files_Cfg.Files_LogMyApp : Files_Log_;
                this.清除线程周期 = 清除线程周期_;
            }



        }



        public enum enum状态
        {
            /// <summary>
            /// 警告
            /// </summary>
            Warning,
            /// <summary>
            /// 错误
            /// </summary>
            Error,
            /// <summary>
            /// 正常
            /// </summary>
            Info,
            /// <summary>
            /// 清空日志
            /// </summary>
            Clear,
        }

        /// <summary>
        /// 日志信息
        /// </summary>
        public class _logValue_
        {
            public enum状态 状态 { set; get; }
            public DateTime 时间 { set; get; }
            public string 内容 { set; get; }

            public _logValue_()
            {

            }
            public _logValue_(enum状态 状态, DateTime 时间, string 内容)
            {
                this.状态 = 状态;
                this.时间 = 时间;
                this.内容 = 内容;
            }
        }


        #endregion


        public _cfg_ 参数 { set; get; } = new _cfg_();



        string 日期分割符 = "-";
        /// <summary>
        /// 日志文件标识不为空时有效
        /// </summary>
        string 文件标识分割符 = "_";


        #region 事件



        public event Action Event_清理日志;
        void On_清理日志()
        {
            Event_清理日志?.Invoke();
        }


        #endregion


        #region 变量

        bool isRun = true;
        readonly object _lock = new object();

        #endregion


        public Log日志(_cfg_ cfg)
        {
            初始化(cfg);
        }


        public void 初始化(_cfg_ cfg)
        {
            if (cfg is null)
            {
                this.参数 = new _cfg_();
            }
            else
            {
                this.参数 = cfg;
            }

            new 文件_文件夹().文件夹_新建(this.参数.Files_Log, out string msgErr);

            清理过期文件();
            if (this.参数.使能_清除线程)
            {
                new Thread(async () =>
                {
                    while (isRun)
                    {
                        Thread.Sleep(this.参数.清除线程周期);
                        if (!isRun)
                        {
                            return;
                        }
                        清理过期文件();
                    }
                })
                { IsBackground = true }.Start();
            }




        }

        public virtual void 释放()
        {
            isRun = false;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state">状态</param>
        /// <param name="logValue">日志内容</param>
        public virtual void Add(enum状态 state, string logValue)
        {
            lock (this._lock)
            {
                SaveLog(
                        new _logValue_
                        {
                            状态 = state,
                            时间 = DateTime.Now,
                            内容 = logValue,
                        });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="logValue">包含</param>
        public virtual void Add(bool status, string logValue)
        {
            enum状态 state = status ? enum状态.Info : enum状态.Error;
            Add(state, logValue);
        }

        /// <summary>
        /// 文件必须以年-月-日开头
        /// </summary>
        /// <param name="FilesPath"></param>
        /// <param name="保存天数"></param>
        public virtual void 清理过期文件()
        {
            new 文件_文件夹().文件_获取_所有文件名(this.参数.Files_Log, out string[] logBeff, out string msgErr);
            foreach (string s in logBeff)
            {
                new 文件_文件夹().文件_获取文件名_不含后缀(s, out string name, out msgErr);
                string[] logNmaeBeff = name.Split(new string[] { 文件标识分割符 }, StringSplitOptions.None);
                bool rt = DateTime.TryParse(logNmaeBeff[0].Trim(), out DateTime dateMin);
                if (rt)
                {
                    TimeSpan span = DateTime.Now - dateMin;
                    if (span.Days >= this.参数.保存天数)
                    {
                        new 文件_文件夹().文件_删除文件(s, out msgErr);
                    }
                }
            }

        }


        #region 方法



        //保存日志
        async void SaveLog(_logValue_ logInfo)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.文件标识分割符);
            sb.Append(this.参数.文件标识);
            string show = string.IsNullOrEmpty(this.参数.文件标识) ? "" : sb.ToString();

            DateTime now = DateTime.Now;
            string y = now.ToString("yyyy");
            string M = now.ToString("MM");
            string d = now.ToString("dd");

            sb.Clear();
            sb.Append(y);
            sb.Append(this.日期分割符);
            sb.Append(M);
            sb.Append(this.日期分割符);
            sb.Append(d);
            string fileName = sb.ToString();

            sb.Clear();
            sb.Append(this.参数.Files_Log);
            sb.Append("\\");
            sb.Append(fileName);
            sb.Append(show);
            sb.Append(".log");
            string path = sb.ToString();
            if (this.参数.使能_文件大小限制)
            {
                #region 文件大小限制

                for (int i = 0; i < 999; i++)
                {
                    if (!new qfmain.文件_文件夹().文件_是否存在(path))
                    {
                        break;
                    }
                    else
                    {
                        new 文件_文件夹().文件_获取文件大小(path, out long B, out string msgErr);
                        if (B >= 1024 * 1024 * this.参数.日志文件大小)
                        {
                            sb.Clear();
                            sb.Append(this.参数.Files_Log);
                            sb.Append("\\");
                            sb.Append(fileName);
                            sb.Append(show);
                            sb.Append(this.文件标识分割符);
                            sb.Append(($"{i + 1}").PadLeft(3, '0'));
                            sb.Append(".log");

                            path = sb.ToString();
                            continue;
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                #endregion
            }

            _saveLog_ saveLog = new _saveLog_();
            saveLog.date = logInfo.时间.ToString("yyyy-MM-dd HH:mm:ss.fff");
            saveLog.state = $"{logInfo.状态}".PadRight(10, ' ');
            saveLog.log = logInfo.内容;
            string logValue = JsonConvert.SerializeObject(saveLog);

            new 文本().Save_25(path, $"{logValue}", false, true);

        }

        #endregion


    }
}
