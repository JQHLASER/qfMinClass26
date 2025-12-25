
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfPLC
{
    public class HslCommunication_
    {
        public bool _Is激活状态 = false;

        /// <summary>
        /// 激活PLC库
        /// </summary>
        /// <returns></returns>
        public (bool rt, string msgErr) 激活()
        {
            // 授权示例 Authorization example...
            //V116.1 : 8cb26b16-6848-46b8-a9e4-6f57336b2872
            string keys = "8cb26b16-6848-46b8-a9e4-6f57336b2872";
            bool rt = HslCommunication.Authorization.SetAuthorizationCode(keys);
            string msgErr = qfmain.Language_.Get语言("HSL激活成功");
            if (!rt)
            {
                msgErr = qfmain.Language_.Get语言("HSL激活失败");
            }
            _Is激活状态 = rt;
            return (rt, msgErr);
        }
         
        public (bool rt, string msgErr) Err_未激活()
        {
            bool rt = true;
            string msgErr = string.Empty;
            if (!this._Is激活状态)
            {
                msgErr = qfmain.Language_.Get语言("HSL激活失败");
                rt = false;
            }
            return (rt, msgErr);
        }

    }
}
