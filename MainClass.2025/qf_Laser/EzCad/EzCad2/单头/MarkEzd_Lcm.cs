using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace qf_Laser 
{
    internal class MarkEzd_Lcm
    {



        /// <summary>
        /// 初始化函数库
        /// PathName 是MarkEzd.dll所在的目录
        /// </summary>     
        [DllImport("Ezd2\\MarkEzd", EntryPoint = "lmc1_Initial2", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int Initialize(string PathName, bool bTestMode);

        /// <summary>
        /// 释放函数库
        /// </summary>     
        [DllImport("MarkEzd", EntryPoint = "lmc1_Close", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int Close();

        /// <summary>
        /// 得到设备参数配置对话框  
        /// </summary> 
        [DllImport("MarkEzd", EntryPoint = "lmc1_SetDevCfg2", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetDevCfg();

        /// <summary>
        /// 载入ezd文件到当前数据库里面,并清除旧的数据库
        /// </summary>   
        [DllImport("MarkEzd", EntryPoint = "lmc1_LoadEzdFile", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int LoadEzdFile(string FileName);


        /// <summary>
        /// 得到当前数据库里面数据的预览图片
        /// </summary>  
        [DllImport("gdi32.dll")]
        internal static extern bool DeleteObject(IntPtr hObject);
        [DllImport("MarkEzd", EntryPoint = "lmc1_GetPrevBitmap2", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr GetCurPrevBitmap(int bmpwidth, int bmpheight);
        public static Image GetCurPreviewImage(int bmpwidth, int bmpheight)
        {
            IntPtr pBmp = GetCurPrevBitmap(bmpwidth, bmpheight);
            Image img = Image.FromHbitmap(pBmp);
            JczLmc.DeleteObject(pBmp);
            return img;
        }

        /// <summary>
        /// 得到对象总数
        /// </summary> 
        [DllImport("MarkEzd", EntryPoint = "lmc1_GetEntityCount", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern ushort GetEntityCount();


        /// <summary>
        /// 得到指定索引号的对象的名称。
        /// </summary>   
        [DllImport("MarkEzd", EntryPoint = "lmc1_GetEntityName", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int lmc1_GetEntityNameByIndex(int nEntityIndex, StringBuilder entname);
        public static string GetEntityNameByIndex(int nEntityIndex)
        {
            StringBuilder str = new StringBuilder("", 255);
            lmc1_GetEntityNameByIndex(nEntityIndex, str);
            return str.ToString();
        }

        /// <summary>
        ///  启动USB设备监控
        /// </summary>
        /// <param name="hWndMonitor">数据接收窗体句柄</param>
        /// <returns></returns>
        [DllImport("MarkEzd", EntryPoint = "lmc1_InitUsbMonitor2", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern bool InitUsbMonitor2(IntPtr hWndMonitor);

        /// <summary>
        ///  关闭USB设备监控
        /// </summary>
        [DllImport("MarkEzd", EntryPoint = "lmc1_CloseUsbMonitor", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern bool CloseUsbMonitor();

        /// <summary>
        /// 判断当前设备是否掉电
        /// </summary>
        /// <param name="bValid">当前设备是否正常</param>
        /// <returns></returns>
        [DllImport("MarkEzd", EntryPoint = "lmc1_CheckCurrentUsbDev", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int CheckCurrentUsbDev(ref bool bValid);


        /// <summary>
        /// 判断当前卡是否需要初始化
        /// </summary>
        /// <param name="CardID"></param>
        /// <returns>返回成功标识有效设备</returns>
        [DllImport("MarkEzd", EntryPoint = "lmc1_CheckNewUsbDev", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int CheckNewUsbDev(int CardID);


        /// <summary>
        ///  获取新设备
        /// </summary>
        /// <returns>返回设备ID编号</returns>
        [DllImport("MarkEzd", EntryPoint = "lmc1_UsbMonitorGetNewDevice", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int UsbMonitorGetNewDevice();
     
}
}
