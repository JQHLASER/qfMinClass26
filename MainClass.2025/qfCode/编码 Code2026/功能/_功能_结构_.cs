using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace qfCode
{
    public class _功能_结构_
    {
        public class _工具箱_
        { 
            public bool 关联对象 { set; get; } = false;
            public bool 班次 { set; get; } = false;
            public bool 特殊码 { set; get; } = false;
            public bool 自定义 { set; get; } = false;
            public bool 外部文件 { set; get; } = false;
            public bool 通讯TCP { set; get; } = false;
            public bool 通讯COM { set; get; } = false;

        }

        public class _序列号_
        {
            /// <summary>
            /// 每个加工数量
            /// </summary>
            public bool 加工  { set; get; } = false;

            /// <summary>
            /// 十六进制序列号
            /// </summary>
            public bool 类型_HEX { set; get; } = false;
        }


        public class _日期_
        { 
            public bool 偏移计算 { set; get; } = false;
 
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

        public enum _em_文件类型_
        {
            ini,
            txt,
            Sqlite,
            SqlServer,
        }
    }
}
