using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfCode
{
    public class _对象属性_
    {
        public bool 防重 { set; get; } = false;
        public bool 读码 { set; get; } = false;
        /// <summary>
        /// 向激光模板中传入是否成功
        /// </summary>
        public bool 变量 { set; get; } = false;

        /// <summary>
        /// =0;不使能,>0:使能
        /// </summary>
        public int 校验_位数 { set; get; } = 0;
               
        /// <summary>
        /// 为空时为不校验
        /// </summary>
        public string 校验_关键字 { set; get; } = "";
    }

    public class _对象信息_
    {
        public string 对象名 { set; get; } = "";
        public _对象属性_ 属性 { set; get; } = new _对象属性_();
        public string[] 元素 { set; get; } = new string[0];

        /// <summary>
        /// 对象内容
        /// </summary>
        public string Value { set; get; } = string.Empty;

    }

    public class _对象内容_
    {
        public string 对象名 { set; get; }
        public string 对象内容 { set; get; }

        /// <summary>
        /// 一般为文本类型
        /// </summary>
        public _工具箱_ 工具 { set; get; } = _工具箱_.文本;
    }




}
