using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfCode
{
    public class _功能_工具_
    {

        public bool 文本 { set; get; } = true;
        public bool 序列号 { set; get; } = true;
        public bool 日期 { set; get; } = true;
        public bool 时间 { set; get; } = true;
        public bool 关联对象 { set; get; } = false;
        public bool 班次 { set; get; } = false;
        public bool 特殊码 { set; get; } = false;
        public bool Excel { set; get; } = false;
        public bool csv { set; get; } = false;
        public bool txt { set; get; } = false;
        public bool 通讯TCP { set; get; } = false;
        public bool 通讯COM { set; get; } = false;

    }
    public class _功能_文件_
    {
        public bool 新建 { set; get; } = false;
        public bool 打开 { set; get; } = false;
        public bool 另存为 { set; get; } = false;
        public bool 删除 { set; get; } = false;
        public bool 模板 { set; get; } = false;
    }
    public class _功能_对象属性_
    {
        public bool 防重 { set; get; } = false;
        public bool 读码 { set; get; } = false;
        /// <summary>
        /// 判断加载内容到激光模板是否成功
        /// </summary>
        public bool 变量 { set; get; } = false;
        public bool 校验_位数 { set; get; } = false;
        public bool 校验_关键字 { set; get; } = false;
        public bool 校验码 { set; get; } = false;
    }
    public class _功能_文本_
    {
        public bool 外部文本 { set; get; } = false;
    }
    public class _功能_序列号_
    {
        public bool 十六进制_HEX { set; get; } = false;
        public bool 十六进制_hex { set; get; } = false;
        public bool 复位_按最大序列号 { set; get; } = true;
        public bool 复位_按年 { set; get; } = true;
        public bool 复位_按月 { set; get; } = true;
        public bool 复位_按日 { set; get; } = true;
        public bool 复位_按周 { set; get; } = true;
        public bool 每个加工数量 { set; get; } = false;
    }
    public class _功能_日期时间_
    {
        public bool 自定义编码 { set; get; } = false;
        public bool 偏移 { set; get; } = false;

    }

    public class _功能_定制_
    {
        /// <summary>
        /// 定制,比如有客户要以08.30为换班时间,那么00:00~08:30这段日期还是昨天的日期
        /// </summary>
        public bool 日期更新时间点 { set; get; } = false;
    }



    public class _功能_
    {
     
        public _功能_工具_ 工具箱 { set; get; } = new _功能_工具_();
        public _功能_文件_ 文件类 { set; get; } = new _功能_文件_();
        public _功能_对象属性_ 对象属性 { set; get; } = new _功能_对象属性_();
        public _文本_ 文本 { set; get; } = new _文本_();
        public _功能_序列号_ 序列号 { set; get; } = new _功能_序列号_();
        public _功能_日期时间_ 日期时间 { set; get; } = new _功能_日期时间_();

        /// <summary>
        /// 定制的功能
        /// </summary>
        public _功能_定制_ 定制 { set; get; } = new _功能_定制_();

        /// <summary>
        /// 默认为EncodeBM
        /// </summary>
        public string _主文件夹 { set; get; } = Environment.CurrentDirectory + "\\EncodeBM";
        /// <summary>
        /// 默认为.edm
        /// </summary>
        public string _后缀 { set; get; } = ".edm";

        /// <summary>
        /// 文件保存方式
        /// </summary>
        public _文件类型_ _文件类型 { set; get; } = _文件类型_.ini;
    }
}
