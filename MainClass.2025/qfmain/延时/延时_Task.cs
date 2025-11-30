using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace qfmain
{
    /// <summary>
    /// 本类使有和的是 CancellationToken
    /// <para>UI 程序用 async / await</para>
    /// <para>服务用 CancellationToken</para>
    /// <para>多任务不要共享 CTS</para>
    /// <para>永远不要 while 死等</para>
    /// </summary>
    public class 延时_Task
    {
        bool isSleep = false;
        public CancellationTokenSource cts = null;

        /// <summary>
        /// 延时并等待延时结束
        /// <para>return: =true:正常延时结束,=false:延时被中断</para>
        /// </summary>
        public virtual async Task<bool> 延时(int delay)
        {
            isSleep = true;
            bool rt = true;
            using (cts = new CancellationTokenSource())
            {

                try
                {
                    await Task.Delay(delay, cts.Token);
                    rt = true;//正常结束
                }
                catch (OperationCanceledException ex)
                {
                    rt = false;//补中断
                }

            }

            isSleep = false;
            return rt;
        }


        /// <summary>
        /// 会阻塞UI,Task.Delay(delay, cts.Token).Wait ();
        /// </summary>
        /// <param name="delay"></param>
        /// <returns></returns>
        public virtual async Task<bool> 延时_Wait(int delay)
        {
            isSleep = true;
            bool rt = true;
            using (cts = new CancellationTokenSource())
            {
                try
                {
                    Task.Delay(delay, cts.Token).Wait();
                }
                catch (OperationCanceledException ex)
                {
                    rt = false;
                }
            }
            isSleep = false;
            return rt;
        }



        /// <summary>
        /// 中断延时
        /// </summary>
        public virtual bool 中断延时()
        {
            bool rt = true;
            //if (cts != null && isSleep)
            if (isSleep)
            {
                cts.Cancel();
            }
            else
            {
                rt = false;
            }
            isSleep = false;
            return rt;
        }


    }
}
