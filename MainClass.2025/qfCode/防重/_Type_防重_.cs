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
            public int 数据保存时间 { set; get; } = 366 * 2;


            public _Type_防重_._cfg_防重参数_ Clone()
            {
                return new _Type_防重_._cfg_防重参数_()
                {
                    数据保存时间 = this.数据保存时间,
                };
            }
        }

        public class _功能_查询窗体_
        {
            public bool _功能_窗体_参数设置 { set; get; } = false;

            public bool _功能_右键_删除指定数据 { set; get; } = false;

            public string 密码_防重参数 { set; get; } = "QF8888";

        }


    }
}
