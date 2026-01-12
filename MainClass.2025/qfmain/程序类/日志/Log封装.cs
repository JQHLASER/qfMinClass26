using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace qfmain
{
    public class Log封装
    {
        qfmain.log日志 log_sys;
        bool _Is日志栏 = true;

        /// <summary>
        /// <para>Is日志栏 : 是否使用日志栏控件</para>
        /// </summary> 
        public Log封装(log日志._cfg_ cfg, bool Is日志栏)
        {
            this._Is日志栏 = Is日志栏;
            log_sys = new qfmain.log日志(cfg);
            isInistiall = true;
        }

        bool isInistiall = false;
         
        public void Add(bool state, string LogValue, bool 显示到日志栏 = true)
        {
            if (!isInistiall)
            {
                return;
            }
            LogValue = (!this._Is日志栏 || !显示到日志栏)? LogValue: $"{qfmain.log日志._不显示到日志栏}  {LogValue}"; 
            log_sys.Add(state, LogValue);
        }
        public void Add(string LogValue, bool 显示到日志栏 = true, bool state = true)
        {
            if (!isInistiall)
            {
                return;
            }
            LogValue = (!this._Is日志栏 || !显示到日志栏) ? LogValue : $"{qfmain.log日志._不显示到日志栏}  {LogValue}";
            log_sys.Add(state, LogValue);
        }
         
        public void 释放()
        {
            if (!isInistiall)
            {
                return;
            }
            log_sys.释放();
        }
         
    }
}
