using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfCode
{
    internal class 计算_班次
    {
        internal (bool s, string m, string v) 计算(_班次_[] info, DateTime 时间)
        {
            bool rt = true;
            string 结果 = "";
            string msgErr = string.Empty;
            try
            {
                foreach (var s in info)
                {
                    _元素_.班次 info班次S = new _元素_.班次();
                    string 代码 = s.代码;
                    DateTime 上班时间 = DateTime.Parse(s.上班时间);
                    DateTime 下班时间 = DateTime.Parse(s.下班时间);

                    //开始计算结果 
                    DateTime 当前时间 = new DateTime(
                        时间.Hour,
                        时间.Minute,
                        时间.Second
                        );
                    // DateTime.Parse(时间.ToString("HH:mm:ss")); 
                    if (上班时间 <= 下班时间)
                    {
                        if (当前时间 >= 上班时间 && 当前时间 <= 下班时间)
                        {
                            结果 = 代码;
                        }
                    }
                    else
                    {
                        if (当前时间 <= 上班时间 && 当前时间 <= 下班时间)
                        {
                            结果 = 代码;
                        }
                        else if (当前时间 >= 上班时间 && 当前时间 >= 下班时间)
                        {
                            结果 = 代码;
                        }
                    }


                }

            }
            catch (Exception ex)
            {
                msgErr = ex.Message;
                rt = false;
            }

            return (rt, msgErr, 结果);
        }
    }
}
