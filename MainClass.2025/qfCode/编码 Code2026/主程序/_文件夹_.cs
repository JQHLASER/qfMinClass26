using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfCode
{
    /// <summary>
    /// 存放信息及参数的文件夹
    /// </summary>
    public class _文件夹_
    {
        public class _属性_
        {

            /// <summary>
            /// 父文件夹
            /// </summary>
            public string 主文件夹 { set; get; } = 常量.主文件夹;
            /// <summary>
            /// 存放参数文件夹
            /// </summary>
            public string 参数 { set; get; } = 常量.参数;
            /// <summary>
            /// 存放班次文件夹
            /// </summary>
            public string 班次 { set; get; } = 常量.班次;
            /// <summary>
            /// 存放日期时间文件夹
            /// </summary>
            public string 日期时间 { set; get; } = 常量.日期时间;

            /// <summary>
            /// 存编码信息的文件夹
            /// </summary>
            public string 编码文件 { set; get; } = 常量.信息;
        } 
        public _文件夹_( 编码_  codeSys)
        {
            new qfmain.文件_文件夹().文件夹_新建(codeSys._文件夹_属性 .主文件夹, out string msgErr);
            new qfmain.文件_文件夹().文件夹_新建(codeSys._文件夹_属性.参数, out msgErr);
            new qfmain.文件_文件夹().文件夹_新建(codeSys._文件夹_属性.班次, out msgErr);
            new qfmain.文件_文件夹().文件夹_新建(codeSys._文件夹_属性.日期时间, out msgErr);
            new qfmain.文件_文件夹().文件夹_新建(codeSys._文件夹_属性.编码文件, out msgErr);
        }




    }
}
