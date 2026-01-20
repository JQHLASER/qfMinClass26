using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Markup;

namespace qfmain
{
    /// <summary>
    /// 语言
    /// </summary>
    public class Language_
    {

        public Language_()
        {
            Inistiall();

        }


        public class _languageInfo_
        {
            /// <summary>
            /// 当前使使的语言
            /// </summary>
            public string LangeuageName { set; get; } = "Chinese";
        }


        public class Config
        {

            /// <summary>
            /// 参数信息
            /// </summary>
            public static _languageInfo_ LangeuageCfg { set; get; } = new _languageInfo_();

        }




        /// <summary>
        /// 初始化
        /// </summary>
        public static void Inistiall()
        {
            new 文件_文件夹().文件夹_新建(软件类.Files_Cfg.Files_Langeuage, out string msgerr);
            读写参数(1);
            string path = Get语言文件路径();
            ini_sys = new ini_sharpconfig(path);

            Set语言包(qfLanguage.LanguageList.lst_Language);//本地语言包
        }

        /// <summary>
        /// 生成语言文件路径
        /// </summary>
        /// <returns></returns>
        public static string Get语言文件路径()
        {
            return 软件类.Files_Cfg.Files_Langeuage + $"\\{Config.LangeuageCfg.LangeuageName}.ini";
        }



        private static ini_sharpconfig ini_sys;
        private static readonly object _lock = new object();
        /// <summary>
        /// 从文件获取语言
        /// </summary> 
        /// <param name="lst"></param>
        /// <param name="section">节名称</param>
        public static void Get语言包(List<qfLanguage._language_Value_> lst)
        {
            lock (_lock)
            {
                new 文件_文件夹().文件夹_新建(软件类.Files_Cfg.Files_Langeuage, out string msgerr);
                string path = Get语言文件路径();
                ini_sys = new ini_sharpconfig(path);
                bool 是否写入 = false;


                for (int i = 0; i < lst.Count; i++)
                {
                    var s = lst[i];
                    string TypeValue = s.TypeValue;
                    string languageVlaue = ini_sys.Read(s.KeyValue, $"{TypeValue}", "");

                    if (string.IsNullOrEmpty(languageVlaue))
                    {
                        languageVlaue = s.LanguageValue;
                        ini_sys.Write(s.KeyValue, $"{TypeValue}", languageVlaue, false);
                        是否写入 = true;
                    }
                    lst[i].LanguageValue = languageVlaue;

                }

                if (是否写入)
                {
                    ini_sys.Save();
                }
            }
        }

        /// <summary>
        /// 读取所有字段,并赋值到 qfLanguage.LanguageList.lst_Language ,
        /// <para>查询时统一到 qfLanguage.LanguageList.lst_Language 去查询</para>
        /// </summary> 
        public static qfLanguage._language_Value_[] Get语言包()
        {
            List<qfLanguage._language_Value_> lst = new List<qfLanguage._language_Value_>();
            new 文件_文件夹().文件夹_新建(软件类.Files_Cfg.Files_Langeuage, out string msgerr);
            string path = Get语言文件路径();
            var rt = ini_sys.GetAll();
              
            foreach (var s in rt.v)
            {
                foreach (var b in s.Value)
                {
                    var vb = new qfLanguage._language_Value_(s.Key, b.Key, b.Value); 
                    lst.Add(vb);
                }
            }
            qfLanguage.LanguageList.lst_Language = lst;
            return lst.ToArray();
        }
         
        public static void Set语言包(List<qfLanguage._language_Value_> lst)
        {
            lock (_lock)
            {
                new 文件_文件夹().文件夹_新建(软件类.Files_Cfg.Files_Langeuage, out string msgerr);
                string path = Get语言文件路径();
                ini_sys = new ini_sharpconfig(path);
                for (int i = 0; i < lst.Count; i++)
                {
                    var s = lst[i];
                    string TypeValue = s.TypeValue;
                    string languageVlaue = ini_sys.Read(s.KeyValue, $"{TypeValue}", "");

                    if (string.IsNullOrEmpty(languageVlaue))
                    {
                        languageVlaue = s.LanguageValue;
                        ini_sys.Write(s.KeyValue, $"{TypeValue}", languageVlaue, false);

                    }
                    lst[i].LanguageValue = languageVlaue; 
                }

                ini_sys.Save(); 
            }
        } 
        public static (string languageValue, qfLanguage._language_Value_[] beff) Get语言(string TypeValue, List<qfLanguage._language_Value_> lst)
        {
            qfLanguage._language_Value_[] ma = lst.Where(p => p.TypeValue == TypeValue).ToArray();
            string value = ma.Length > 0 ? ma[0].LanguageValue : TypeValue;

            return (value, ma);
        }
         
      
        public static string Get语言(string TypeValue)
        {
            (string languageValue, qfLanguage._language_Value_[] beff) rt = Get语言(TypeValue, qfLanguage.LanguageList.lst_Language);
            return rt.languageValue;
        }
         

        /// <summary>
        /// 获取语言目录
        /// </summary>
        /// <returns></returns>
        public static bool Get语言目录(out string[] FilesName, out string msgErr)
        {
            bool rt = new 文件_文件夹().文件_获取_所有文件名(软件类.Files_Cfg.Files_Langeuage, out string[] files, out msgErr);
            List<string> lst = new List<string>();
            foreach (var s in files)
            {
                new 文件_文件夹().文件_获取文件名_不含后缀(s, out string name, out msgErr);
                lst.Add(name);
            }
            FilesName = lst.ToArray();
            return rt;
        }

        /// <summary>
        /// 读写参数
        /// </summary>
        public static void 读写参数(ushort model)
        {
            string path = 软件类.Files_Cfg.Files_sysConfig + "\\language.cfg";
            _languageInfo_ cfg = Config.LangeuageCfg;
            new 文件_文件夹().WriteReadJson(path, model, ref cfg, out string msgErr);
            Config.LangeuageCfg = cfg;
        }

        
    }
}
