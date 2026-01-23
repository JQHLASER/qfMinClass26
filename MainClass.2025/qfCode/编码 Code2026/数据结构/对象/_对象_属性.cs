using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfCode 
{
    public  class _对象_属性
    {
        public bool 防重 { set; get; } = false;
        public bool 读码 { set; get; } = false;
        /// <summary>
        /// 向模板中传入是否成功
        /// </summary>
        public bool 校验模板 { set; get; } = false;
        public bool 校验关键字 { set; get; } = false;
        /// <summary>
        /// =0;不使能,>0:使能
        /// <para>为功能时,大于表示使能</para>
        /// </summary>
        public int 校验位数 { set; get; } = 0;
         
        public _对象_属性 Clone()
        {
            return new _对象_属性
            {
                防重 = this.防重,
                读码 = this.读码,
                校验模板 = this.校验模板,
                校验关键字 = this.校验关键字,
                校验位数 = this.校验位数,
            };
        }
    }
}
