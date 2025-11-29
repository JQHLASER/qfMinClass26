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

            Get语言包(LanguageList.lst_Language);//本地语言包
        }

        /// <summary>
        /// 生成语言文件路径
        /// </summary>
        /// <returns></returns>
        public static string Get语言文件路径()
        {
            return 软件类.Files_Cfg.Files_Langeuage + $"\\{Config.LangeuageCfg.LangeuageName}.ini";
        }

        /// <summary>
        /// 生成语言文件
        /// </summary>   
        /// <param name="lst"></param>
        /// <param name="section">节名称</param>
        public static void Set语言包(List<_language_Value_> lst)
        {
            lock (_lock)
            {
                new 文件_文件夹().文件夹_新建(软件类.Files_Cfg.Files_Langeuage, out string msgerr);
                string path = Get语言文件路径();

                for (int i = 0; i < lst.Count; i++)
                {
                    var s = lst[i];
                    string TypeValue = s.TypeValue;
                    int index = i + 1;
                    new ini().Write(s.KeyValue, $"{TypeValue}", s.LanguageValue, path);
                }
            }
        }

        static readonly object _lock = new object();
      

        /// <summary>
        /// 从文件获取语言
        /// </summary> 
        /// <param name="lst"></param>
        /// <param name="section">节名称</param>
        public static void Get语言包(List<_language_Value_> lst)
        {
            lock (_lock)
            {
                new 文件_文件夹().文件夹_新建(软件类.Files_Cfg.Files_Langeuage, out string msgerr);
                string path = Get语言文件路径();
                for (int i = 0; i < lst.Count; i++)
                {
                    var s = lst[i];
                    string TypeValue = s.TypeValue;
                    int index = i + 1;
                    string languageVlaue = new ini().Read(s.KeyValue, $"{TypeValue}", "", path);

                    if (string.IsNullOrEmpty(languageVlaue))
                    {
                        languageVlaue = s.LanguageValue;
                        new ini().Write(s.KeyValue, $"{TypeValue}", languageVlaue, path);
                    }
                    lst[i].LanguageValue = languageVlaue;
                }
            }
        }


        public static string Get语言(string TypeValue, List<_language_Value_> lst)
        {
            _language_Value_[] ma = lst.Where(p => p.TypeValue == TypeValue).ToArray();
            if (ma.Length == 0)
            {
                return TypeValue;
            }
            return ma[0].LanguageValue;
        }

        /// <summary>
        /// 本地使用
        /// </summary>
        /// <param name="TypeValue"></param>
        /// <param name=""></param>
        /// <returns></returns>
        internal static string Get语言(string TypeValue)
        {
            return Get语言(TypeValue, LanguageList.lst_Language);
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

        //public   static void 窗体设置()
        //{
        //    On_窗体设置();
        //}

        //public static Action Action_窗体设置;

        //static void On_窗体设置()
        //{
        //    Action_窗体设置?.Invoke();
        //}

    }
}
