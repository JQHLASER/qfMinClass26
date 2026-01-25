using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfCode
{
    public class _对象_
    {

        public string 对象名 { set; get; } = "";
        public _对象_属性 属性 { set; get; } = new _对象_属性();
        /// <summary>
        /// json格式
        /// </summary>
        public List<string> 元素 { set; get; } = new List<string>();

        /// <summary>
        /// 计算后的内容
        /// </summary>
        public string 内容 { set; get; } = string.Empty;
        public _对象_ Clone()
        {
            return new _对象_
            {
                对象名 = this.对象名,
                属性 = this.属性.Clone(),
                元素 = this.元素 is null ? null : new List<string>(this.元素),
                内容 = this.内容,
            };
        }


    }
}
