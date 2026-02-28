using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfCode
{
    public class _Type_防重_
    {
        public class _cfg_防重参数_
        {
            /// <summary>
            /// 单位:天
            /// </summary>
            public int 数据保存时间 { set; get; } = 366;


            public _Type_防重_._cfg_防重参数_ Clone()
            {
                return new _Type_防重_._cfg_防重参数_()
                {
                   数据保存时间 =this.数据保存时间 ,
                };
            }
        }




    }
}
