using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfCode
{
    public class _关联对象_
    {

        public enum _em_类型_
        {
            全部,
            按位,
            按字符,
            按首尾,
        }
         
        public class _按位_
        {
            /// <summary>
            /// 从1开始;=0时表示取全部数据,
            /// </summary>
            public uint 开始位 { set; get; } = 0;
            /// <summary>
            /// =0时表示取剩下全部数据
            /// </summary>
            public uint 数量 { set; get; } = 0;
        }

        public class _按字符_
        {
            public string 分割符 { set; get; } = ";";
            public uint 索引 { set; get; } = 0;
        }

        public class _按首尾_
        {
            public string 首 { set; get; } = "";
            public string 尾{ set; get; } = "";
            public uint 索引 { set; get; } = 0;
        }



    }
}
