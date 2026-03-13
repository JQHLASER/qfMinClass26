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
        public _功能_结构_._em_配方文件类型_ 配方文件类型 { set; get; } = _功能_结构_._em_配方文件类型_.Sqlite;

        /// <summary>
        /// 使能时,在添加对象名称 为指定的  的对象时,不显示序列号元素
        /// <para>明码: 明码 或 codeM 或 barcodeM</para>
        /// <para>二维码: 二维码 或 barcode</para>
        /// </summary>
        public bool 定制_二维码_明码强制防呆 { set; get; } = false;
   
        /// <summary>
        /// 编码信息文件后缀,
        /// <para>.cqf</para>
        /// </summary>
        public string 后缀 { set; get; } = 常量.后缀;

        public _功能_结构_._序列号_ 序列号 { set; get; } = new _功能_结构_._序列号_();

        public _功能_结构_._日期时间_ 日期时间 { set; get; } = new _功能_结构_._日期时间_();

        public _功能_结构_._工具箱_ 工具箱 { set; get; } = new _功能_结构_._工具箱_();
        public _功能_结构_._编辑_ 编辑 { set; get; } = new _功能_结构_._编辑_();
        public _功能_结构_._对象属性_ 对象属性 { set; get; } = new _功能_结构_._对象属性_();


    }
}
