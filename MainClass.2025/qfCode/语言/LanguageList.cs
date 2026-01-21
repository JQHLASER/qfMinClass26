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
            new _language_Value_ ( keys, "关联的对象名不能为空值","关联的对象名不能为空值"),
            new _language_Value_ ( keys, "不能为空","不能为空"),
            new _language_Value_ ( keys, "检测到已重复","检测到已重复"),
            new _language_Value_ ( keys, "添加","添加"),
            new _language_Value_ ( keys, "修改","修改"),
            new _language_Value_ (keys,"未选中要操作的对象","未选中要操作的对象"),
            new _language_Value_ (keys,"确认删除?","确认删除?"),






            new _language_Value_ ( keys, "文本","文本"),
            new _language_Value_ ( keys, "序列号","序列号"),
            new _language_Value_ ( keys, "日期","日期"),
            new _language_Value_ ( keys, "时间","时间"),
            new _language_Value_ ( keys, "关联对象","关联对象"),
            new _language_Value_ ( keys, "班次","班次"),














        };
        
    }
}
