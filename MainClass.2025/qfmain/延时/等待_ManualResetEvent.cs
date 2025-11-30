using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace qfmain
{
    /// <summary>
    /// 后台线程,会阻塞UI的
    /// </summary>
    public class 等待_ManualResetEvent
    {

        ManualResetEvent evt = null;

        /// <summary>
        ///  阻塞等待，不耗 CPU
        /// </summary>
        public void 等待()
        {
            evt?.Close();
            evt?.Dispose();
            using (evt = new ManualResetEvent(false))
            {
                // 等待线程
                evt.WaitOne();   // 阻塞等待，不耗 CPU
            }
        }


        public bool 结束等待(out string msgErr)
        {
            msgErr = "";
            bool rt = true;
            try
            {

                // 设置线程
                evt.Set();       // 通知 ,等待结束


            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }

            return rt;
        }





    }
}
