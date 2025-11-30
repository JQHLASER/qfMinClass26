using MathNet.Numerics.LinearAlgebra.Factorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfmain
{
    /// <summary>
    /// 不阻塞UI程序,
    /// </summary>
    public class 等待_Task
    {
        TaskCompletionSource<bool> tcs  ;


        /// <summary>
        /// 等待
        /// </summary>
        public async Task 等待()
        {
            if (tcs != null)
            {
                tcs = null;
            }
            tcs = new TaskCompletionSource<bool>();
            // 等待
            await tcs.Task;
        }
         
        public bool 结束等待(out string msgErr)
        {
            bool rt = true;
            msgErr = "";
            try
            {
                // 设置 	 
                tcs.SetResult(true); //结束 
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
