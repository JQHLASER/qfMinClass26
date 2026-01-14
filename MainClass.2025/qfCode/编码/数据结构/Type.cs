using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfCode
{
    public class Type
    {

        public class _文件夹_
        {
            /// <summary>
            /// 父文件夹
            /// </summary>
            public string 主文件夹 { set; get; } = 常量._文件夹.主文件夹;
            /// <summary>
            /// 存放参数文件夹
            /// </summary>
            public string 参数 { set; get; } = 常量._文件夹.参数;
            /// <summary>
            /// 存放班次文件夹
            /// </summary>
            public string 班次 { set; get; } = 常量._文件夹.班次;
            /// <summary>
            /// 存放日期时间文件夹
            /// </summary>
            public string 日期时间 { set; get; } = 常量._文件夹.日期时间;

            /// <summary>
            /// 存放信息文件的文件夹
            /// </summary>
            public string 信息 { set; get; } = 常量._文件夹.信息;
        }

        public class _文件参数_
        {
            public string 后缀_信息文件 { set; get; } = 常量._文件夹.后缀_信息文件;
            public string 默认文件名 { set; get; } = 常量._文件夹.默认文件名;
        }






    }
}
