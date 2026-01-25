using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfCode
{
    public class _序列号_
    {

        public enum _em_类型_
        {
            十进制,
            十六进制HEX,
            十六进制hex,

        }

        public enum _em_复位_
        {
            按最大,
            按年,
            按月,
            按日,
            按周,
            按班次,
        }

        public enum _em_操作_
        {
            不操作,
            判断复位,
            强制复位,
            计算递增,
            计算递减,
            强制递增,
            强制递减,
            加工数量递增,
        }
      

        public class _加工_
        {
            /// <summary>
            /// 每个序号加工多少次
            /// </summary>
            public int 数量 { set; get; } = 1;
            /// <summary>
            /// 当前序号已加工多少次了
            /// </summary>
            public int 计数 { set; get; } = 0;
            public _加工_ Clone()
            {
                return new _加工_
                {
                    数量 = this.数量,
                    计数 = this.计数,
                };
            }
        }
    }
}
