using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfLanguage
{
    public class LanguageList
    {
        static string keys = "编码";
        /// <summary>
        /// 语言包
        /// </summary>
        public List<_language_Value_> lst_Language = new List<_language_Value_>()
        {
            new _language_Value_ ( keys, "编码模块未初始化","编码模块未初始化"),
            new _language_Value_ ( keys, "未找到文件","未找到文件"),
            new _language_Value_ ( keys, "受影响0行","受影响0行"),
            new _language_Value_ ( keys, "执行成功","执行成功"),

            
        };
        
    }
}
