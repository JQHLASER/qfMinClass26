using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfCode
{
    internal class 常量
    {
        public class _文件夹
        {
            internal static string 主文件夹 { get; } = Environment.CurrentDirectory + "\\Code26";
            internal static string 参数 { get; } = 主文件夹 + "\\Cfg";
            internal static string 班次 { get; } = 主文件夹 + "\\Class";
            internal static string 日期时间 { get; } = 主文件夹 + "\\DateTime";
            /// <summary>
            /// 存放信息文件
            /// </summary>
            internal static string 信息 { get; } = 主文件夹 + "\\Main";
            internal static string 后缀_信息文件 { get; } = ".cqf";
            internal static string 默认文件名 {  get; } = "Default";
             
        }





    }
}
