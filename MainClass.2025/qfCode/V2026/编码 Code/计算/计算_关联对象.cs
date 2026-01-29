using qfmain;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfCode
{
    internal class 计算_关联对象
    {

        internal (bool s, string m, string v) 按位(_元素_.关联对象 info, string 关联的内容)
        {
            List<string> Work = new List<string>
            {
                "解析",
                "计算",
            };
            bool rt = true;
            string 结果 = string.Empty;
            string msg = string.Empty;

            _关联对象_._按位_ cfg_按位 = new _关联对象_._按位_();
            foreach (var s in Work)
            {
                if (!rt)
                {
                    break;
                }
                else if (s == "解析")
                {
                    #region 解析

                    var rt按位 = new Json序列化().转成Json<_关联对象_._按位_>(info.param);
                    rt = rt按位.s;
                    msg = rt按位.m;
                    if (rt)
                    {
                        cfg_按位 = rt按位.cfg;
                    }

                    #endregion
                }
                else if (s == "计算")
                {
                    #region 计算
                    try
                    {
                        if (cfg_按位.开始位 <= 0)
                        {
                            msg = Language_.Get语言("索引超出范围");
                            rt = false;
                        }
                        else
                        {
                            int 开始位 = (int)(cfg_按位.开始位 - 1);
                            if (cfg_按位.数量 == 0)
                            {
                                结果 = 关联的内容;
                            }
                            else if (cfg_按位.数量 >= 关联的内容.Length)
                            {
                                结果 = new qfmain.文本().获取(关联的内容, 开始位, 关联的内容.Length - 开始位);
                            }
                            else
                            {
                                结果 = new qfmain.文本().获取(关联的内容, 开始位, (int)cfg_按位.数量);
                            }
                        }


                    }
                    catch (Exception ex)
                    {
                        rt = false;
                        msg = string.Empty;
                        结果 = "";
                    }
                    #endregion
                }
            }


            return (rt, msg, 结果);



        }

        internal (bool s, string m, string v) 按字符(_元素_.关联对象 info, string 关联的内容)
        {
            List<string> Work = new List<string>
            {
                "解析",
                "计算",
            };
            bool rt = true;
            string 结果 = string.Empty;
            string msg = string.Empty;

            _关联对象_._按字符_ cfg_按字符 = new _关联对象_._按字符_();
            foreach (var s in Work)
            {
                if (!rt)
                {
                    break;
                }
                else if (s == "解析")
                {
                    #region 解析

                    var rt按字符 = new Json序列化().转成Json<_关联对象_._按字符_>(info.param);
                    rt = rt按字符.s;
                    msg = rt按字符.m;
                    if (rt)
                    {
                        cfg_按字符 = rt按字符.cfg;
                    }

                    #endregion
                }
                else if (s == "计算")
                {
                    #region 计算
                    try
                    {
                        string[] dataBeff = 关联的内容.Split(new string[] { cfg_按字符.分割符 }, StringSplitOptions.None);
                        if (cfg_按字符.索引 <= 0)
                        {
                            rt = false;
                            msg = Language_.Get语言("索引超出范围");
                        }
                        else if (cfg_按字符.索引 >= dataBeff.Length)
                        {
                            结果 = dataBeff[dataBeff.Length - 1];
                        }
                        else
                        {
                            结果 = dataBeff[cfg_按字符.索引 - 1];
                        }
                    }
                    catch (Exception ex)
                    {
                        rt = false;
                        msg = string.Empty;
                        结果 = "";
                    }
                    #endregion
                }
            }


            return (rt, msg, 结果);



        }

        internal (bool s, string m, string v) 按首尾(_元素_.关联对象 info, string 关联的内容)
        {
            List<string> Work = new List<string>
            {
                "解析",
                "计算",
            };
            bool rt = true;
            string 结果 = string.Empty;
            string msg = string.Empty;

            _关联对象_._按首尾_ cfg_按首尾 = new _关联对象_._按首尾_();
            foreach (var s in Work)
            {
                if (!rt)
                {
                    break;
                }
                else if (s == "解析")
                {
                    #region 解析

                    var rt按首尾 = new Json序列化().转成Json<_关联对象_._按首尾_>(info.param);
                    rt = rt按首尾.s;
                    msg = rt按首尾.m;
                    if (rt)
                    {
                        cfg_按首尾 = rt按首尾.cfg.Clone ();
                    }

                    #endregion
                }
                else if (s == "计算")
                {
                    #region 计算
                    try
                    {
                        string[] data = new qfmain.文本().获取(关联的内容, cfg_按首尾.首, cfg_按首尾.尾);
                        if (data.Length == 0)
                        {
                            结果 = "";
                        }
                        else if (cfg_按首尾.索引 <= 0)
                        {
                            rt = false;
                            Language_.Get语言("索引超出范围");
                        }
                        else if (cfg_按首尾.索引 >= data.Length)
                        {
                            结果 = data[data.Length - 1];
                        }
                        else
                        {
                            结果 = data[cfg_按首尾.索引];
                        }
                    }
                    catch (Exception ex)
                    {
                        rt = false;
                        msg = string.Empty;
                        结果 = "";
                    }
                    #endregion
                }
            }


            return (rt, msg, 结果);



        }




    }
}
