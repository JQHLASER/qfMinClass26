using HslCommunication;
using Sunny.UI.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace qfPLC
{
    public class 解析
    {
        /// <summary>
        /// 对OperateResult的解析
        /// </summary> 
        public (bool s, string m, T value) OperateResult<T>(OperateResult<T> result)
        {
            return result.IsSuccess ?
                      (true, "", (T)(object)result.Content) :
                      (false, result.Message, default(T));
        }

        /// <summary>
        /// 对OperateResult的解析
        /// </summary> 
        public (bool s, string m) OperateResult(OperateResult result)
        {
            bool rt = true;
            string msgErr = string.Empty;

            if (result.IsSuccess)
            {
                //msgErr = DateTime.Now.ToString("[HH:mm:ss] ") + $"[{address}] {result.Content}{Environment.NewLine}";  
            }
            else
            {
                msgErr = result.Message;
                rt = false;
            }
            return (rt, msgErr);

        }

         
    }
}
