using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfCode
{
    public class _元素_
    {
        public class 文本
        {
            /// <summary>
            /// 工具
            /// </summary>
            public _em_工具箱_ Tool { set; get; } = _em_工具箱_.文本;
            /// <summary>
            /// 类型
            /// </summary>
            public _文本_._em_文本_ types { set; get; } = _文本_._em_文本_.文本;
            public string 内容 { set; get; } = "TEXT";

            public 文本 Clone()
            {
                return new 文本
                {
                    Tool = this.Tool,
                    types = this.types,
                    内容 = this.内容,
                };

            }
        }
        public class 序列号
        {  /// <summary>
           /// 工具
           /// </summary>
            public _em_工具箱_ Tool { set; get; } = _em_工具箱_.序列号;
            /// <summary>
            /// 类型
            /// </summary>
            public _序列号_._em_类型_ types { set; get; } = _序列号_._em_类型_.十进制;
            public string 当前序号 { set; get; } = "0001";
            public string 开始序号 { set; get; } = "0001";
            public string 最大序号 { set; get; } = "9999";
            public int 递增量 { set; get; } = 1;
            public _序列号_._加工_ 加工 { set; get; } = new _序列号_._加工_();
            public _序列号_._em_复位_ 复位方式 { set; get; } = _序列号_._em_复位_.按日;

            public 序列号 Clone()
            {
                return new 序列号
                {
                    Tool = this.Tool,
                    types = this.types,
                    当前序号 = this.当前序号,
                    开始序号 = this.开始序号,
                    最大序号 = this.最大序号,
                    递增量 = this.递增量,
                    加工 = this.加工,
                    复位方式 = this.复位方式,
                };

            }

        }
        public class 日期
        {
            public _em_工具箱_ Tool { set; get; } = _em_工具箱_.日期;
            /// <summary>
            /// 类型
            /// </summary>
            public _日期时间_._em_日期_ types { set; get; } = _日期时间_._em_日期_.年4位;
            public _日期时间_._em_偏移类型_ 偏移类型 { set; get; } = _日期时间_._em_偏移类型_.无;
            public int 偏移值 { set; get; } = 0;

            /// <summary>
            /// 自定义编码的配置文件
            /// </summary>
            public string 配置 { set; get; } = 常量.配置文件名_默认;
            public 日期 Clone()
            {
                return new 日期
                {
                    Tool = this.Tool,
                    types = this.types,
                    偏移类型 = this.偏移类型,
                    偏移值 = this.偏移值,
                    配置 = this.配置,
                };

            }
        }
        public class 时间
        {  /// <summary>
           /// 工具
           /// </summary>
            public _em_工具箱_ Tool { set; get; } = _em_工具箱_.时间;
            /// <summary>
            /// 类型
            /// </summary>
            public _日期时间_._em_时间_ types { set; get; } = _日期时间_._em_时间_.时24;

            /// <summary>
            /// 自定义编码的配置文件
            /// </summary>
            public string 配置 { set; get; } = 常量.配置文件名_默认;


            public 时间 Clone()
            {
                return new 时间
                {
                    Tool = this.Tool,
                    types = this.types,
                    配置 = this.配置,
                };

            }


        }
        public class 关联对象
        {  /// <summary>
           /// 工具
           /// </summary>
            public _em_工具箱_ Tool { set; get; } = _em_工具箱_.关联对象;
            /// <summary>
            /// 类型
            /// </summary>
            public _关联对象_._em_类型_ types { set; get; } = _关联对象_._em_类型_.全部;
            /// <summary>
            /// 要关联的对象
            /// </summary>
            public string 对象 { set; get; } = "";
            /// <summary>
            /// json参数
            /// </summary>
            public string param { set; get; } = "";

            public 关联对象 Clone()
            {
                return new 关联对象
                {
                    Tool = this.Tool,
                    types = this.types,
                    对象 = this.对象,
                    param = this.param,
                };

            }

        }
        public class 班次
        {  /// <summary>
           /// 工具
           /// </summary>
            public _em_工具箱_ Tool { set; get; } = _em_工具箱_.班次;
        }

        /// <summary>
        /// 解析json格式时,用来获取当前工具
        /// </summary>
        public class 工具
        { 
            /// <summary>
           /// 工具
           /// </summary>
            public _em_工具箱_ Tool { set; get; } 
        }

    }
}
