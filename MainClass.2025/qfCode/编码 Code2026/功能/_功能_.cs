using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfCode
{
    public class _功能_
    {
        /// <summary>
        /// 编码文件信息后缀
        /// </summary>
        public _功能_结构_._em_文件类型_ 文件类型 { set; get; } = _功能_结构_._em_文件类型_.ini;

        /// <summary>
        /// 编码信息文件后缀
        /// </summary>
        public string 后缀 { set; get; } = 常量.后缀; 

        public _功能_结构_._序列号_ 序列号 { set; get; } = new _功能_结构_._序列号_();

        public _功能_结构_._日期_  日期 { set; get; } = new _功能_结构_._日期_();

        public _功能_结构_._工具箱_ 工具箱 { set; get; } = new _功能_结构_._工具箱_();
        public _功能_结构_._编辑_ 编辑 { set; get; } = new _功能_结构_._编辑_();
        public _对象_属性 对象属性 { set; get; } = new _对象_属性();


    }
}
