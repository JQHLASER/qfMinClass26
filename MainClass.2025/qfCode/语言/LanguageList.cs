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
            new _language_Value_ ( keys,"未选中要操作的对象","未选中要操作的对象"),
            new _language_Value_ ( keys,"未选中要操作的元素","未选中要操作的元素"),
            new _language_Value_ ( keys,"确认删除?","确认删除?"),
            new _language_Value_ ( keys, "对象","对象"),
            new _language_Value_ ( keys, "防重","防重"),
            new _language_Value_ ( keys, "读码","读码"),
            new _language_Value_ ( keys, "模板","模板"),
            new _language_Value_ ( keys, "关键字","关键字"),
            new _language_Value_ ( keys, "位数","位数"),

            new _language_Value_ ( keys, "配方名不能为空","配方名不能为空"),
            new _language_Value_ ( keys, "未找到对象","未找到对象"),
            new _language_Value_ ( keys, "索引超出范围","索引超出范围"),
            new _language_Value_ ( keys, "请输入分割符","请输入分割符"),
            new _language_Value_ ( keys, "请输入首分割符","请输入首分割符"),
            new _language_Value_ ( keys, "请输入尾分割符","请输入尾分割符"),
            new _language_Value_ ( keys, "保存成功","保存成功"),
            new _language_Value_ ( keys, "首对象时无法添加","首对象时无法添加"),
            new _language_Value_ ( keys, "请选择关联对象","请选择关联对象"),
            new _language_Value_ ( keys, "请选择偏移类型","请选择偏移类型"),
            new _language_Value_ ( keys, "请选择配置文件","请选择配置文件"),


            new _language_Value_ ( keys, "文本","文本"),
            new _language_Value_ ( keys, "序列号","序列号"),
            new _language_Value_ ( keys, "日期","日期"),
            new _language_Value_ ( keys, "时间","时间"),
            new _language_Value_ ( keys, "关联对象","关联对象"),
            new _language_Value_ ( keys, "班次","班次"),

            new _language_Value_ ( keys, "年","年"),
            new _language_Value_ ( keys, "月","月"),
            new _language_Value_ ( keys, "日","日"),
            new _language_Value_ ( keys, "天","天"),
            new _language_Value_ ( keys, "周","周"),
            new _language_Value_ ( keys, "星期","星期"),
            new _language_Value_ ( keys, "时","时"),
            new _language_Value_ ( keys, "分","分"),

            new _language_Value_ ( keys, "新建?","新建?"),
            new _language_Value_ ( keys, "删除?","删除?"),
            new _language_Value_ ( keys, "删除成功","删除成功"),


            




        };

    }
}
