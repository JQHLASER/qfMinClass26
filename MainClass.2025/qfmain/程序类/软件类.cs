using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace qfmain
{
    public class 软件类
    {
        /// <summary>
        /// 文件夹类
        /// </summary>
        public class Files_Cfg
        {
            /// <summary>
            /// 日志文件夹
            /// </summary>
            public static string Files_LogMyApp { set; get; } = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LogMyApp");
            /// <summary>
            /// 主文件夹
            /// </summary>
            public static string Files_ConfigMyApp { set; get; } = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ConfigMyApp");


            /// <summary>
            /// 模板文件夹
            /// </summary>
            public static string Files_Template { set; get; } = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Template");

            /// <summary>
            /// 程序参数
            /// </summary>
            public static string Files_Config { set; get; } = Path.Combine(Files_ConfigMyApp,"Config");
            /// <summary>
            /// 图片
            /// </summary>
            public static string Files_Image { set; get; } = Path.Combine(Files_ConfigMyApp,"Image");

            /// <summary>
            /// 商标
            /// </summary>
            public static string Files_res { set; get; } = Path.Combine(Files_ConfigMyApp,"res");
            /// <summary>
            /// 系统参数
            /// </summary>
            public static string Files_sysConfig { set; get; } = Path.Combine(Files_ConfigMyApp,"sysConfig");

            /// <summary>
            /// 语言
            /// </summary>
            public static string Files_Langeuage { set; get; } = Path.Combine(Files_ConfigMyApp,"Langeuage");

            /// <summary>
            /// 帮助
            /// </summary>
            public static string Files_help { set; get; } = Path.Combine(Files_ConfigMyApp,"help");
        }


        public 软件类()
{
    new 文件_文件夹().文件夹_新建(软件类.Files_Cfg.Files_LogMyApp, out string msgerr);
    new 文件_文件夹().文件夹_新建(软件类.Files_Cfg.Files_ConfigMyApp, out msgerr);
    new 文件_文件夹().文件夹_新建(软件类.Files_Cfg.Files_sysConfig, out msgerr);
    new 文件_文件夹().文件夹_新建(软件类.Files_Cfg.Files_Config, out msgerr);
    new 文件_文件夹().文件夹_新建(软件类.Files_Cfg.Files_Image, out msgerr);
    new 文件_文件夹().文件夹_新建(软件类.Files_Cfg.Files_res, out msgerr);
    new 文件_文件夹().文件夹_新建(软件类.Files_Cfg.Files_Langeuage, out msgerr);
    new 文件_文件夹().文件夹_新建(软件类.Files_Cfg.Files_help, out msgerr);
    new 文件_文件夹().文件夹_新建(软件类.Files_Cfg.Files_Template, out msgerr);



}


// 导入 kernel32.dll 用于设置 DLL 搜索路径
[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
private static extern bool SetDllDirectory(string lpPathName);

/// <summary>
/// DllImport调用dll的相对路径
/// </summary>
/// <param name="目录下文件夹名"></param>

public void DllImport相对路径(string 目录下文件夹名)
{
    #region 设置相对目录,在.net8中需要这样设置一下

    // 获取应用程序运行目录
    string appDir = Environment.CurrentDirectory;
    // 设置 DLL 搜索路径为运行目录下的 lib 子目录
    string dllPath = System.IO.Path.Combine(appDir, 目录下文件夹名);
    SetDllDirectory(dllPath);

    #endregion
}

/// <summary>
/// AppDomain.CurrentDomain.BaseDirectory
/// </summary> 
public string Get程序目录()
{
    return AppDomain.CurrentDomain.BaseDirectory;
}

public string 耗时(DateTime start, DateTime end)
{
    TimeSpan time = end - start;
    return $"{time.Days.ToString("0")}.{time.Hours.ToString("00")}:{time.Minutes.ToString("00")}:{time.Seconds.ToString("00")}.{time.Milliseconds.ToString("000")}";
}


    }
}
 