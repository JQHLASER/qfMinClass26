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
            /// 从1开始; 
            /// </summary>
            public uint 开始位 { set; get; } = 1;
            /// <summary>
            /// 从1开始
            /// </summary>
            public uint 数量 { set; get; } = 1;

            public _按位_ Clone()
            {
                return new _按位_
                {
                    开始位 = this.开始位,
                    数量 = this.数量,
                };
            }

        }

        public class _按字符_
        {
            public string 分割符 { set; get; } = ";";
            /// <summary>
            /// 从1开始
            /// </summary>
            public uint 索引 { set; get; } = 1;


            public _按字符_ Clone()
            {
                return new _按字符_
                {
                    分割符 = this.分割符,
                    索引 = this.索引,
                };
            }
        }

        public class _按首尾_
        {
            public string 首 { set; get; } = "";
            public string 尾{ set; get; } = "";
            /// <summary>
            /// 从1开始
            /// </summary>
            public uint 索引 { set; get; } = 1;

            public _按首尾_ Clone()
            {
                return new _按首尾_
                {
                    首 = this.首,
                    尾= this.尾,
                    索引 = this.索引,
                };
            }
        }



    }
}
