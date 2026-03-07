using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace qfWork
{
    internal class zmcdll
    {
        //zmotion.dll

        [DllImport("zmotion.dll", EntryPoint = "ZMC_DownZar", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32  下载ZAR包文件(IntPtr 连接句柄, string zar名称, string 缺省zar名称);



        /// <summary>
        /// 
        /// </summary>
        /// <param name="项目文件路径"></param>
        /// <param name="Zar文件名"></param>
        /// <param name="项目密码">无密码时为空</param>
        /// <param name="控制器id">0:不绑定,</param>
        /// <returns></returns>
        [DllImport("zmotion.dll", EntryPoint = "ZMC_MakeZar", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32  生成ZAR程序包(string 项目文件路径, string Zar文件名, string 项目密码,int 控制器id );



        


         

    }


    public class zmc
    {


        public int  下载ZAR包文件(IntPtr 连接句柄, string zar名称, string 缺省zar名称)
        {
            return zmcdll.下载ZAR包文件(连接句柄, zar名称, 缺省zar名称);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="项目文件路径"></param>
        /// <param name="Zar文件名"></param>
        /// <param name="项目密码">无密码时为空</param>
        /// <param name="控制器id">0:不绑定,</param>
        /// <returns></returns>
        public int  生成ZAR程序包(string 项目文件路径, string Zar文件名, string 项目密码, int 控制器id)
        {
            return zmcdll. 生成ZAR程序包(项目文件路径, Zar文件名, 项目密码,  控制器id);
        }


    }






}
