using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace qfWork
{
    internal   class ZmotionAxis
    {

        [DllImport("zmotion.dll", EntryPoint = "ZMC_Lock", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 锁定(IntPtr 连接句柄, string 密码);


        [DllImport("zmotion.dll", EntryPoint = "ZMC_UnLock", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 解锁(IntPtr 连接句柄, string 密码);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="连接句柄"></param>
        /// <param name="zarPath"></param>
        /// <param name="控制器上文件的名字">控制器上文件的名字 , BASIC系统只有一个包文件，可以不指定.</param>
        /// <returns></returns>
        [DllImport("zmotion.dll", EntryPoint = "ZMC_DownZar", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 下载Zar文件程序(IntPtr 连接句柄, string zarPath,string 控制器上文件的名字);



        /// <summary>
        /// 
        /// </summary>
        /// <param name="zpj项目文件路径">项目文件名 带路径</param>
        /// <param name="Zar程序名">ZAR文件名</param>
        /// <param name="APP_PASS">软件密码, 绑定APP_PASS  没有密码时pPass = NULL</param>
        /// <param name="绑定控制器ID"> 绑定控制器唯一ID， 0-不绑定</param>
        /// <returns></returns>
        [DllImport("zmotion.dll", EntryPoint = "ZMC_MakeZar", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 生成Zar文件(string zpj项目文件路径, string Zar程序名,string APP_PASS,int 绑定控制器ID);

 

    }
}
