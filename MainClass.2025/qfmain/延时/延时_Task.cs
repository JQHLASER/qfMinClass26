using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace qfmain
{
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
                }
                catch (Exception ex)
                {
                    rt = false;
                }

            }

            isSleep = false;
            return rt;
        }

        /// <summary>
        /// Task.Delay(delay, cts.Token).Wait ();
        /// </summary>
        /// <param name="delay"></param>
        /// <returns></returns>
        public virtual async Task<bool> 延时_无返回值(int delay)
        {
            isSleep = true;
            bool rt = true;
            using (cts = new CancellationTokenSource())
            {
                try
                {
                    await Task.Delay(delay, cts.Token);
                }
                catch (Exception ex)
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
