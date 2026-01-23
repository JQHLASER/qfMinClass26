using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfCode
{
    public class type_编辑
    {
        public enum _编辑类型_
        {
            添加,
            修改,
        }

        public class _对象信息_
        {
            public string 对象名 { set; get; } = string.Empty;
            /// <summary>
            /// json格式的信息
            /// </summary>
            public List<string> lst元素信息 { set; get; }=new List<string> ();

            public _对象信息_ Clone()
            {
                return new _对象信息_
                {
                    对象名 = this.对象名,
                    lst元素信息 = this.lst元素信息 is null ? null : new List<string>(this.lst元素信息),
                };
            }


        }


    }
}
