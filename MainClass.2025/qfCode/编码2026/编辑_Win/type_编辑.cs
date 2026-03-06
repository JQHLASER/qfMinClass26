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

        /// <summary>
        /// 与编码交互类型
        /// </summary>
        public enum _交互类型_
        {
            本地,
            /// <summary>
            /// 内存通讯
            /// </summary>
            Pipc通讯,
            /// <summary>
            /// 网络通讯
            /// </summary>
            TCP通讯,
            /// <summary>
            /// 网络通讯
            /// </summary>
            http通讯,
        }


        public class _视图_
        {
            public int 左边栏 { set; get; } = 250;
            public int 下边栏 { set; get; } = 100;
            public int 信息宽度 { set; get; } = 300;
            public _视图_ Clone()
            {
                return new _视图_
                {
                    左边栏 = this.左边栏,
                    下边栏 = this.下边栏,
                    信息宽度 = this.信息宽度,
                };
            }
        }
 


    }
}
