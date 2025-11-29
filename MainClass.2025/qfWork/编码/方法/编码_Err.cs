using Newtonsoft.Json.Serialization;
using qfmain;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace qfWork
{
    public class 编码_Err
    {
        private 编码 _BM_sys;
        public 编码_Err(编码 bM_sys)
        {
            this._BM_sys = bM_sys;
        }


        public bool Err_序列号(_BM_元素_序列号_ info, out string msgErr)
        {
            int L当前序号 = info.当前序号.Length;
            int L开始序列号 = info.开始序号.Length;
            int L最大序号 = info.最大序号.Length;

            msgErr = string.Empty;
            bool rt = true;
            List<string> lstWork = new List<string>()
            {
                "位数不一致",
                "大小",
            };

            foreach (string s in lstWork)
            {
                if (!rt)
                {
                    break;
                }
                else if (s == "位数不一致")
                {
                    #region 位数不一致

                    msgErr = Language_.Get语言("位数不一致");
                    if (L当前序号 != L开始序列号 ||
                        L当前序号 != L最大序号)
                    {
                        rt = false;
                    }


                    #endregion
                }
                else if (s == "大小")
                {
                    #region 大小

                    long 开始 = 0;
                    long 当前 = 0;
                    long 最大 = 0;

                    switch (info.类型)
                    {
                        case _BM_序列号类型_.十进制:
                            开始 = long.Parse(info.开始序号);
                            当前 = long.Parse(info.当前序号);
                            最大 = long.Parse(info.最大序号);
                            break;
                        case _BM_序列号类型_.十六进制hex:
                            开始 = new qfmain.进制().十六进制To十进制(info.开始序号);
                            当前 = new qfmain.进制().十六进制To十进制(info.当前序号);
                            最大 = new qfmain.进制().十六进制To十进制(info.最大序号);
                            break;
                        case _BM_序列号类型_.十六进制HEX:
                            开始 = new qfmain.进制().十六进制To十进制(info.开始序号);
                            当前 = new qfmain.进制().十六进制To十进制(info.当前序号);
                            最大 = new qfmain.进制().十六进制To十进制(info.最大序号);
                            break;

                             
                    }

                    #region Err处理

                    if (当前 < 开始)
                    {
                        rt = false;
                        msgErr = Language_.Get语言("当前序号不能小于开始序号");
                    }
                    else if (当前 > 最大)
                    {
                        rt = false;
                        msgErr = Language_.Get语言("当前序号不能大于最大序号");
                    }
                    else if (开始 > 最大)
                    {
                        rt = false;
                        msgErr = Language_.Get语言("开始序号不能大于最大序号");
                    }

                    #endregion

                    #endregion
                }
            }







            return rt;
        }



    }
}
