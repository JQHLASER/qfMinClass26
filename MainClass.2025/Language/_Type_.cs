using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfLanguage
{
    /// <summary>
    /// 语言包
    /// </summary>
    public class _language_Value_
    {
        public _language_Value_(string KeyValue_, string TypeValue_, string languageValue_)
        {
            KeyValue = KeyValue_;
            TypeValue = TypeValue_;
            LanguageValue = languageValue_;
        }

        /// <summary>
        /// 节点,生成语言文件使用
        /// </summary>
        public string KeyValue { set; get; }

        /// <summary>
        /// 基础内容,内部调用时用
        /// </summary>
        public string TypeValue { set; get; }

        /// <summary>
        /// 语言内容,对外显示时用
        /// </summary>
        public string LanguageValue { set; get; }
    }
}
