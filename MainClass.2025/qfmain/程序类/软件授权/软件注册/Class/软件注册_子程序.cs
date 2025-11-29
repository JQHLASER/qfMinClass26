using NPOI.SS.Formula.Functions;
using SqlSugar.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfmain
{
    internal class 软件注册_子程序
    {

        static string 密码 = "QIFNGLASERqifenglaser";
        internal static string 加密(string value)
        {
            return new 加解密_AES().加密_1(value, 密码);
        }

        internal static string 解密(string value)
        {
            return new 加解密_AES().解密_1(value, 密码);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="机器码信息"></param>
        /// <returns>反回机器码 设备Sn-客户Id</returns>
        internal static string Get机器码(_软件授权_机器码信息_ 机器码信息)
        {
            string snd = $"{机器码信息.Sn}-{机器码信息.Uid}";
            if ((string.IsNullOrEmpty(机器码信息.Sn) && string.IsNullOrEmpty(机器码信息.Uid)) || snd == "-")
            {
                snd = "";
            }
            snd = new 文本().替换(snd, " ", "").Trim();
            return snd;
        }

        internal static void Get机器码(string 机器码, out _软件授权_机器码信息_ info)
        {
            info = new _软件授权_机器码信息_();
            string[] beff = 机器码.Split('-');
            if (beff.Length >= 2)
            {
                info.Sn = beff[0].Trim();
                info.Uid = beff[1].Trim();
            }
        }

        internal static bool 生成注册码(ref _软件授权_注册信息_ 注册信息, _软件授权_注册方式_ 注册方式, out string msgErr)
        {
            bool rt = true;
            msgErr = string.Empty;

            string[] workBeff = new string[]
           {
                "Err",
                "Err_日期",
                "注册",

           };

            try
            {

                注册信息.机器码 = new 文本().替换(注册信息.机器码, " ", "").Trim();

                DateTime 日期授权值 = DateTime.Parse(注册信息.授权值);
                foreach (var s in workBeff)
                {
                    if (!rt)
                    {
                        break;
                    }
                    else if (s == "Err")
                    {
                        #region Err

                        if (string.IsNullOrEmpty(注册信息.机器码))
                        {
                            msgErr = Language_.Get语言("机器码不能为空");
                            rt = false;

                        }
                        else if (注册方式 == _软件授权_注册方式_.日期限制 && string.IsNullOrEmpty(注册信息.授权值))
                        {
                            msgErr = Language_.Get语言("授权日期不能为空");
                            rt = false;

                        }


                        #endregion
                    }
                    else if (s == "Err_日期")
                    {
                        #region Err_日期

                        bool rt日期 = DateTime.TryParse(注册信息.授权值, out 日期授权值);
                        if (注册方式 == _软件授权_注册方式_.日期限制)
                        {
                            if (!rt日期)
                            {
                                msgErr = Language_.Get语言("授权日期类型错误");
                                rt = false;
                            }
                            else if ((日期授权值 - DateTime.Today).Days <= 0)
                            {
                                msgErr = Language_.Get语言("授权日期必须大于当前日期");
                                rt = false;
                            }
                        }

                        #endregion
                    }
                    else if (s == "注册")
                    {
                        #region 注册

                        switch (注册方式)
                        {
                            case _软件授权_注册方式_.完全:

                                string 授权值 = DateTime.Now.ToString("HHALmmLss");
                                string code = $"{注册信息.机器码}-{授权值}";
                                注册信息.注册码 = new 加解密_注册码算法().加密(code);

                                break;
                            case _软件授权_注册方式_.日期限制:
                                string 授权值1 = 日期授权值.ToString("yyyyMMdd");
                                string code1 = $"{注册信息.机器码}-{授权值1}";
                                注册信息.注册码 = new 加解密_注册码算法().加密(code1);

                                break;
                        }

                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }
            return rt;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="注册码"></param>
        /// <param name="是否试用">有加密狗时,有时客户需要试用一段时间</param>
        /// <param name="机器码信息"></param>
        /// <param name="注册信息"></param>
        /// <param name="msgErr"></param>
        /// <returns></returns>
        internal static _软件授权_Err_ 解析注册码(string 注册码, bool 是否试用, _软件授权_机器码信息_ 机器码信息, out _软件授权_注册信息_ 注册信息, out string msgErr)
        {
            _软件授权_Err_ rtKeys = _软件授权_Err_.未注册;
            msgErr = string.Empty;
            注册信息 = new _软件授权_注册信息_();
            bool rt = true;
            try
            {

                string[] workBeff = new string[]
                {
                "Err",
                "解密注册码",
                "比较客户ID和设备Sn",
                "注册判断_ALL完全注册",
                "注册判断_日期限制",
                };

                DateTime nows = DateTime.Now;

                _软件授权_机器码信息_ info机器码 = new _软件授权_机器码信息_();
                string keyValue = string.Empty;

                foreach (var s in workBeff)
                {
                    if (!rt)
                    {
                        break;
                    }
                    else if (s == "Err")
                    {
                        #region Err

                        if (string.IsNullOrEmpty(注册码))
                        {
                            rt = false;
                            rtKeys = _软件授权_Err_.未注册;
                            msgErr = Language_.Get语言("系统未授权");
                        }

                        #endregion
                    }
                    else if (s == "解密注册码")
                    {

                        #region 解密注册码

                        //解密后的注册码
                        string keyCode = new 加解密_注册码算法().解密(注册码);
                        string[] beff = keyCode.Split('-');
                        if (beff.Length < 3)
                        {
                            rt = false;
                            msgErr = Language_.Get语言("注册码错误");
                        }
                        else
                        {
                            info机器码.Sn = beff[0];
                            info机器码.Uid = beff[1];
                            keyValue = beff[2];
                        }

                        #endregion

                    }
                    else if (s == "比较客户ID和设备Sn")
                    {

                        #region 比较客户ID和设备Sn...如果不一致时,则为注册码错误

                        if (机器码信息.Sn != info机器码.Sn || 机器码信息.Uid != info机器码.Uid)
                        {
                            rt = false;
                            msgErr = Language_.Get语言("注册码错误");
                        }

                        #endregion

                    }
                    else if (s == "注册判断_ALL完全注册")
                    {

                        #region 注册判断_ALL完全注册

                        注册信息.机器码 = $"{info机器码.Sn}-{info机器码.Uid}";
                        注册信息.注册码 = 注册码;

                        if (string.IsNullOrEmpty(注册码))
                        {
                            rt = false;
                            注册信息.授权值 = "";
                            rtKeys = _软件授权_Err_.未注册;
                            msgErr = Language_.Get语言("系统未授权");
                        }
                        else if (keyValue.Contains("A") && keyValue.Contains("L") && keyValue.Contains("L"))
                        {
                            if (是否试用)
                            {
                                msgErr = $"{Language_.Get语言("试用模式,注册码授权不能大于7天")},{注册信息.授权值}";
                                注册信息.授权值 = string.Empty;
                                rtKeys = _软件授权_Err_.未注册;

                                break;
                            }


                            注册信息.授权值 = "";
                            rtKeys = _软件授权_Err_.已完全注册;
                            msgErr = Language_.Get语言("系统已完全授权");
                            break;
                        }

                        #endregion

                    }
                    else if (s == "注册判断_日期限制")
                    {

                        #region 注册判断_日期限制

                        string y = new 文本().获取(keyValue, 0, 4);
                        string M = new 文本().获取(keyValue, 4, 2);
                        string d = new 文本().获取(keyValue, 6, 2);
                        string 授权值 = $"{y}-{M}-{d}";
                        bool rtDT = DateTime.TryParse(授权值, out DateTime 日期);
                        if (!rtDT)
                        {
                            msgErr = Language_.Get语言("注册码错误,日期类型错误");
                            注册信息.授权值 = string.Empty;
                            rtKeys = _软件授权_Err_.未注册;
                        }
                        if (是否试用 && (日期 - nows).Days > 7)
                        {
                            msgErr = $"{Language_.Get语言("试用模式,注册码授权不能大于7天")},{注册信息.授权值}";
                            注册信息.授权值 = string.Empty;
                            rtKeys = _软件授权_Err_.未注册;
                        }
                        else if (日期 > nows)
                        {
                            注册信息.授权值 = 授权值;
                            rtKeys = _软件授权_Err_.已日期注册;
                            msgErr = $"{Language_.Get语言("系统已授权")},{Language_.Get语言("试用到期")}: {注册信息.授权值}";
                        }
                        else
                        {
                            msgErr = Language_.Get语言("注册码授权已过期");
                            注册信息.授权值 = 授权值;
                            rtKeys = _软件授权_Err_.已到期;
                        }
                        break;

                        #endregion

                    }

                }



            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = $"{Language_.Get语言("注册码错误")}";
            }


            rtKeys = rt ? rtKeys : _软件授权_Err_.未注册;
            return rtKeys;
        }




    }
}
