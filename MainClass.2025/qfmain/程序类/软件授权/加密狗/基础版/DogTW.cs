using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static qfmain.WindProc;

namespace qfmain
{
    public class DogTW
    {


        public class Config
        {
            /// <summary>
            /// 加密狗的硬件信息,如ID,版本等
            /// </summary>
            public static _Dog_硬件信息_ dog_硬件信息 { set; get; } = new _Dog_硬件信息_();

            /// <summary>
            /// 加密狗的数据信息,如注册码,软件功能信息等
            /// </summary>
            public static _Dog_Cfg_ dog_数据信息 { set; get; } = new _Dog_Cfg_();


            public static _Dog_Err_ 加密狗状态 { set; get; } = _Dog_Err_.未检测到加密狗;

            /// <summary>
            /// 如果万能|代理|定制 等, 用 | 做为分割符
            /// </summary>
            public static string 功能类型 { set; get; } = string.Empty;


        }








        /// <summary>
        /// 读写锁密码
        /// </summary>
        class pasd_读写锁
        {
            internal static string 写锁密码_H { get; } = "JQHLASER";
            internal static string 写锁密码_L { get; } = "JQHLASER";

            internal static string 读锁密码_H { get; } = "JQHLASER";
            internal static string 读锁密码_L { get; } = "JQHLASER";

        }


        #region 操作狗


        /// <summary>
        /// 这是恢复出厂设置,慎用
        /// </summary>
        /// <returns></returns>
        internal static bool 恢复出厂状态(out string msgErr)
        {
            msgErr = "成功";
            bool rt = true;

            try
            {
                List<string> lstWork = new List<string>();
                lstWork.Add("恢复出厂设置");
                lstWork.Add("恢复写锁密码");
                lstWork.Add("恢复读锁密码");
                int rtInt = 0;
                foreach (var s in lstWork)
                {
                    if (!rt)
                    {
                        break;
                    }
                    else if (s == "恢复出厂设置")
                    {
                        rtInt = DogTWclass.设置_恢复出厂设置();
                        if (rtInt != 0)
                        {
                            rt = false;
                            msgErr = "恢复出厂设置失败";
                        }
                    }

                    else if (s == "恢复写锁密码")
                    {
                        rtInt = DogTWclass.设置_写锁密码("00000000", "00000000", "FFFFFFFF", "FFFFFFFF");

                        if (rtInt != 0)
                        {
                            rt = false;
                            msgErr = "恢复写锁密码失败";
                        }
                    }

                    else if (s == "恢复读锁密码")
                    {
                        rtInt = DogTWclass.设置_读锁密码("FFFFFFFF", "FFFFFFFF", "FFFFFFFF", "FFFFFFFF");
                        if (rtInt != 0)
                        {
                            rt = false;
                            msgErr = "恢复读锁密码失败";
                        }
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

        internal static bool 获取_dog硬件信息(out _Dog_硬件信息_ info, out string msgErr)
        {
            bool rt = true;
            info = new _Dog_硬件信息_();
            msgErr = "成功";

            try
            {
                List<string> lstWork = new List<string>();
                lstWork.Add("读ID");
                lstWork.Add("读版本");
                int rtInt = 0;
                foreach (var s in lstWork)
                {
                    if (!rt)
                    {
                        break;
                    }
                    else if (s == "读ID")
                    {

                        rtInt = DogTWclass.获取_狗id(out string id);
                        if (rtInt != 0)
                        {
                            rt = false;
                            msgErr = "获取ID失败";
                        }
                        else
                        {
                            info.Id = id;
                        }


                    }
                    else if (s == "读版本")
                    {
                        rtInt = DogTWclass.获取_狗版本(out short 版本);
                        if (rtInt != 0)
                        {
                            rt = false;
                            msgErr = "获取版本失败";
                        }
                        else
                        {
                            info.版本号 = 版本;
                        }
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
        /// 通过出厂时的密码来设置
        /// </summary>
        /// <returns></returns>
        internal static bool 设置_dog读写密码(out string msgErr)
        {
            bool rt = true;
            msgErr = "成功";
            try
            {
                List<string> lstWork = new List<string>();
                lstWork.Add("设置写锁密码");
                lstWork.Add("设置读锁密码");

                int rtInt = 0;
                foreach (var s in lstWork)
                {
                    if (!rt)
                    {
                        break;
                    }
                    else if (s == "设置写锁密码")
                    {
                        rtInt = DogTWclass.设置_写锁密码("FFFFFFFF", "FFFFFFFF", pasd_读写锁.写锁密码_H, pasd_读写锁.写锁密码_L);
                        if (rtInt != 0)
                        {
                            rt = false;
                            msgErr = "设置写锁密码失败";
                        }

                    }
                    else if (s == "设置读锁密码")
                    {
                        rtInt = DogTWclass.设置_读锁密码(pasd_读写锁.写锁密码_H, pasd_读写锁.写锁密码_L, pasd_读写锁.读锁密码_H, pasd_读写锁.读锁密码_L);
                        if (rtInt != 0)
                        {
                            rt = false;
                            msgErr = "设置读锁密码失败";
                        }
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



        #endregion

        #region 信息

        public static bool dog_写信息(_Dog_Cfg_ info, out string msgErr)
        {
            bool rt = true;
            msgErr = string.Empty;
            try
            {
                string xt = JsonConvert.SerializeObject(info);
                rt = DogTWclass.写入_字符串_任意长度(0, pasd_读写锁.写锁密码_H, pasd_读写锁.写锁密码_L, xt);
                msgErr = rt ? "写信息成功" : "写信息失败";
            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }
            return rt;

        }

        public static bool dog_读信息(out _Dog_Cfg_ info, out string msgErr)
        {
            bool rt = true;
            msgErr = string.Empty;
            info = new _Dog_Cfg_();
            try
            {
                string xt = string.Empty;
                rt = DogTWclass.获取_字符串_任意长度(0, pasd_读写锁.写锁密码_H, pasd_读写锁.写锁密码_L, ref xt);
                if (rt && !string.IsNullOrEmpty(xt))
                {
                    JToken jToken = JToken.FromObject(info);
                    info = JsonConvert.DeserializeObject<_Dog_Cfg_>(xt);

                }

                msgErr = rt ? Language_.Get语言("读信息成功") : Language_.Get语言("读信息失败");
            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }
            return rt;
        }



        #endregion


        /// <summary>
        /// 调用后从Config中,查看加密狗状态
        /// <para>有产生事件</para>
        /// </summary>
        /// <param name="msgErr"></param>
        /// <returns></returns>
        public static _Dog_Err_ Get狗信息(out _Dog_Cfg_ 数据信息, out _Dog_硬件信息_ 硬件信息, out string msgErr)
        {
            数据信息 = new _Dog_Cfg_();
            硬件信息 = new _Dog_硬件信息_();
            msgErr = "成功";
            bool rt = true;
            _Dog_Err_ rtErr = _Dog_Err_.未检测到加密狗;
            string[] Work = new string[]
            {
                "get硬件信息",
                "get数据信息",

            };
            foreach (var s in Work)
            {
                if (!rt)
                {
                    break;
                }
                else if (s == "get硬件信息")
                {
                    rt = 获取_dog硬件信息(out 硬件信息, out msgErr);
                }
                else if (s == "get数据信息")
                {
                    rt = dog_读信息(out 数据信息, out msgErr);
                }

            }

            Config.dog_硬件信息 = 硬件信息;
            Config.dog_数据信息 = 数据信息;

            if (!rt || string.IsNullOrEmpty(硬件信息.Id))
            {
                msgErr = Language_.Get语言("未获取加密狗sn");
                rtErr = _Dog_Err_.加密狗检测故障;
            }
            else if (string.IsNullOrEmpty(数据信息.Uid))
            {
                msgErr = Language_.Get语言("未检测到匹配的加密狗");
                rt = false;
                rtErr = _Dog_Err_.加密狗不匹配;
            }
            else if (数据信息.Types.Contains($"{_Dog_Type.万能}".Trim()) ||
                     Config.功能类型.Contains($"{_Dog_Type.代理}".Trim ()) ||
                     Config.功能类型.Contains(数据信息.Types.Trim()))
            {
                //代理必须在开发软件中设置,否则显示加密狗不匹配
                rtErr = _Dog_Err_.正常;
                msgErr = Language_.Get语言("正常");
            }
            else
            {
                rt = false;
                rtErr = _Dog_Err_.加密狗不匹配;
                msgErr = Language_.Get语言("未检测到匹配的加密狗");     
            }

            On_加密狗状态(rtErr);
            return rtErr;
        }



        static void On_加密狗状态(_Dog_Err_ 状态)
        {
            Config.加密狗状态 = 状态;
        }


    }
}
