using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfCode
{
    public class _功能_
    {
        public class _cfg_
        {
            /// <summary>
            /// 编码文件信息后缀
            /// </summary>
            public _Type_._em_文件类型_  类型 { set; get; } = _Type_._em_文件类型_.ini;

            /// <summary>
            /// 编码信息文件后缀
            /// </summary>
            public string 后缀 { set; get; } = 常量.后缀;
             

        }

        public class _工具箱_
        { 
            public bool 文本 { set; get; } = true;
            public bool 序列号 { set; get; } = true;
            public bool 日期 { set; get; } = true;
            public bool 时间 { set; get; } = true;
            public bool 关联对象 { set; get; } = false;
            public bool 班次 { set; get; } = false;
            public bool 特殊码 { set; get; } = false;
            public bool 外部文件 { set; get; } = false;           
            public bool 通讯TCP { set; get; } = false;
            public bool 通讯COM { set; get; } = false;

        }

        public class _编辑_
        {
            public bool 新建 { set; get; } = false;
            public bool 打开 { set; get; } = false;
            public bool 另存为 { set; get; } = false;
            public bool 删除 { set; get; } = false;
            public bool 模板 { set; get; } = false;
        }

        public class _对象属性_
        {
            public bool 防重 { set; get; } = false;
            public bool 读码 { set; get; } = false;
            /// <summary>
            /// 判断加载内容到激光模板是否成功
            /// </summary>
            public bool 校验模板 { set; get; } = false;
            public bool 校验位数 { set; get; } = false;
            public bool 校验关键字 { set; get; } = false;
         
             
        }




    }
}
