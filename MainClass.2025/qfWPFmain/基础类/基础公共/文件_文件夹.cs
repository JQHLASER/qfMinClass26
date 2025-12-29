using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace qfWPFmain
{
    public class 文件_文件夹 : qfmain.文件_文件夹
    {

        #region 文件的读写



        /// <summary>
        ///  Model: =0写,=1读
        /// <para>读写文件,以json格式保存</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="Model"></param>
        /// <param name="cfg"></param>
        /// <param name="msgErr"></param>
        /// <param name="encoding_"></param>
        /// <param name="加密"></param>
        /// <param name="密码"></param>
        /// <param name="bufferSize"></param>
        /// <returns></returns>
        public override bool WriteReadIni<T>(string path, ushort Model, ref T cfg, out string msgErr, string section = "信息", string key_ = "data" )
        {
            bool rt = true;
            msgErr = string.Empty;
            try
            {


                List<string> lstWork = new List<string>();
                lstWork.Add("是否强制写");
                lstWork.Add("写");
                lstWork.Add("读");

                foreach (var s in lstWork)
                {
                    if (!rt)
                    {
                        break;
                    }
                    else if (s == "是否强制写")
                    {
                        if (Model != 0 && !new 文件_文件夹().文件_是否存在(path))
                        {
                            Model = 0;
                        }
                    }
                    else if (s == "写")
                    {
                        if (Model != 0)
                        {
                            continue;
                        }

                        string vxt = JsonConvert.SerializeObject(cfg, Formatting.None);
                        new qfmain.ini_win().Write(section, key_, vxt, path);


                    }
                    else if (s == "读")
                    {
                        string rxt = new qfmain.ini_win ().Read(section, key_, JsonConvert.SerializeObject(cfg), path);
                        if (!string.IsNullOrEmpty(rxt))
                        {
                            cfg = JsonConvert.DeserializeObject<T>(rxt);
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

        public override bool WriteReadIni (string path, ushort Model, ref string  cfg, out string msgErr, string section = "信息", string key_ = "data")
        {
            bool rt = true;
            msgErr = string.Empty;
            try
            {


                List<string> lstWork = new List<string>();
                lstWork.Add("是否强制写");
                lstWork.Add("写");
                lstWork.Add("读");

                foreach (var s in lstWork)
                {
                    if (!rt)
                    {
                        break;
                    }
                    else if (s == "是否强制写")
                    {
                        if (Model != 0 && !new 文件_文件夹().文件_是否存在(path))
                        {
                            Model = 0;
                        }
                    }
                    else if (s == "写")
                    {
                        if (Model != 0)
                        {
                            continue;
                        }
 
                        new qfmain.ini_win().Write(section, key_, cfg , path);


                    }
                    else if (s == "读")
                    {
                      cfg  = new qfmain.ini_win().Read(section, key_, "", path);
                       
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

    }
}
