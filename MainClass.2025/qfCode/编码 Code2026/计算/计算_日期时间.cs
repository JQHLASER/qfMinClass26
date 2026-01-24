using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfCode
{
    internal class 计算_日期时间
    {
        internal DateTime  偏移计算(编码_ 编码sys, DateTime 时间, _元素_.日期 info)
        {
            DateTime 当前时间 = 时间;
            if (编码sys._功能.日期时间.偏移计算)
            {
                switch (info.偏移类型)
                {
                    case _日期时间_._em_偏移类型_.无:
                        break;
                    case _日期时间_._em_偏移类型_.年:
                        当前时间 = new qfmain.日期时间_().增减时间(时间, 0, info.偏移值);
                        break;
                    case _日期时间_._em_偏移类型_.月:
                        当前时间 = new qfmain.日期时间_().增减时间(时间, 1, info.偏移值);
                        break;
                    case _日期时间_._em_偏移类型_.日:
                        当前时间 = new qfmain.日期时间_().增减时间(时间, 2, info.偏移值);
                        break;
                    case _日期时间_._em_偏移类型_.周:

                        int a = info.偏移值 * 7;
                        当前时间 = new qfmain.日期时间_().增减时间(时间, 2, a);

                        break;
                }

            }
            return 当前时间;
        }
    }
}
