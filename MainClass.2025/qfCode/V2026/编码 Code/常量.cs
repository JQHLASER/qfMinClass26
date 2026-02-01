using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfCode
{
    internal class 常量
    {
        /// <summary>
        /// 主文件夹
        /// </summary>
        internal static string 主文件夹 { get; } = Path .Combine (AppDomain .CurrentDomain .BaseDirectory ,"Code26");
        /// <summary>
        /// 存放参数的文件夹
        /// </summary>
        internal static string 参数 { get; } = 主文件夹 + "\\Cfg";
        /// <summary>
        /// 班次文件夹
        /// </summary>
        internal static string 班次 { get; } = 主文件夹 + "\\Class";
        /// <summary>
        /// 存放日期时间配置文件的文件夹
        /// </summary>
        internal static string 日期时间 { get; } = 主文件夹 + "\\DateTime";
        /// <summary>
        /// 存放信息文件
        /// </summary>
        internal static string 信息 { get; } = 主文件夹 + "\\Main";

        /// <summary>
        /// 编码信息文件后缀
        /// </summary>
        internal static string 后缀 { get; } = ".cqf";
        /// <summary>
        /// 配置文件的默认名称
        /// <para>Default</para>
        /// </summary>
        internal static string 配置文件名_默认 { get; } = "Default";






    }
}
