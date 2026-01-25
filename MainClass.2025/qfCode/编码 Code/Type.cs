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
    }

    public enum _em_计算类型_
    {
        测试,
        加工,
    }



}
