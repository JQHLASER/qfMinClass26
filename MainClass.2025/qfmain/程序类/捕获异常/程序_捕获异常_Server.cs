using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfmain
{
    public class 程序_捕获异常_Server
    {
        //public delegate void 异常退出事件_event();
        //public event 异常退出事件_event Event_异常退出事件 = null;

        public Action Event_异常退出事件;


        public virtual void 捕获异常()
        { 
            var va = new 程序捕获异常_helper()
            {
                ExitProcess = true,//否则会认为服务假死
            };
            va.Event_Exception += (s) =>
            {
                Event_异常退出事件?.Invoke();
            };
            va.Init();
        }




    }
}
