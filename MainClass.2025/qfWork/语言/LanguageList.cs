using qfLanguage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static qfWork.GaoCH_IO_scio32;

namespace qfWork
{
    public class LanguageList
    {
        static string keys = "works"; 
        /// <summary>
        /// 语言包
        /// </summary>
        public static List<_language_Value_> lst_Language = new List<_language_Value_>()
        { 
            new _language_Value_ ( keys, "指令执行成功","指令执行成功"),
            new _language_Value_ ( keys, "指令执行失败","指令执行失败"),
            new _language_Value_ ( keys, "指令参数错误","指令参数错误"),
            new _language_Value_ ( keys, "不支持","不支持"),
            new _language_Value_ ( keys, "无效句柄","无效句柄"),
            new _language_Value_ ( keys, "未知错误","未知错误"),


               
        };

    }
}
