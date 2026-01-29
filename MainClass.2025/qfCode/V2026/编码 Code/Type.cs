using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfCode
{

    public enum _初始化状态_
    {
        已初始化,
        初始化中 = -2,
        未初始化 = -1,
    }


    /// <summary>
    /// 对象名及对象内容
    /// </summary>
    public class _对象_内容_
    {
        public _对象_ 对象 { set; get; }

        /// <summary>
        /// 对象内容
        /// </summary>
        public string Value { set; get; }

        public _对象_内容_ Clone()
        {
            return new _对象_内容_
            {
                对象 = this.对象,
                Value = this.Value,
            };
        }
    }


    /// <summary>
    /// 元素及计算出的内容,编辑用
    /// </summary>
    public class _元素_Str_
    {
        public _em_工具箱_ 工具 { set; get; } = _em_工具箱_.文本;

        /// <summary>
        /// 元素内容
        /// </summary>
        public string Value { set; get; } = "";



        public _元素_Str_ Clone()
        {
            return new _元素_Str_
            {
                工具 = this.工具,
                Value = this.Value,
            };
        }
         
    }


    public enum _em_计算类型_
    {
        测试,
        加工,
    }


    /// <summary>
    /// 对象名称和计算的内容
    /// </summary>
    public class _对象_str_
    {
        /// <summary>
        /// 对象名
        /// </summary>
        public string Name { set; get; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Value { set; get; }
    }

}
