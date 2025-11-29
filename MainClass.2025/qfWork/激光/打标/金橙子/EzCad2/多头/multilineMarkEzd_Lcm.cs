using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace qfWork 
{
    public class MultiMarkEzd
    {

        /// <summary>
        /// 初始化函数库
        /// </summary>
        /// <param name="PathName"></param> 初始话路径
        /// <param name="bTestMode"></param> 默认为false
        /// <param name="handle"></param>  默认为0
        /// <returns></returns>
        [DllImport("MultiMarkEzd", EntryPoint = "lmc1_Initial", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int Initialize(string PathName, bool bTestMode, int handle);

        /// <summary>
        /// 释放函数库
        /// </summary>     
        [DllImport("MultiMarkEzd", EntryPoint = "lmc1_Close", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int Close();

        /// <summary>
        /// 得到所有卡的序号 , 卡序号=-1表示没有卡  0－7表示有效的序号
        /// </summary>     
        [DllImport("MultiMarkEzd", EntryPoint = "lmc1_GetAllCardSN", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetAllCardSN(ref int nFirstCardSN);


        /// <summary>
        /// 载入ezd文件到当前数据库里面,并清除旧的数据库
        /// </summary>   
        [DllImport("MultiMarkEzd", EntryPoint = "lmc1_LoadEzdFile", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int LoadEzdFile(int nCard, string FileName);

        /// <summary>
        /// 载入指定数据文件,用于加载矢量图位图
        /// </summary> 
        [DllImport("MultiMarkEzd", EntryPoint = "lmc1_AddFileToLib", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int AddFileToLib(int nCard, string strFileName, string strEntName, double dPosX, double dPosY, double dPosZ, int nAlign, double dRatio, int nPenNo, int bHatchFile);

        /// <summary>
        /// 标刻函数 此接口为非阻塞型函数
        /// </summary>
        /// <param name="nCard"></param>
        /// <param name="Fly"></param>
        /// <returns></returns>

        [DllImport("MultiMarkEzd", EntryPoint = "lmc1_Mark", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int Mark(int nCard, bool Fly);


        [DllImport("gdi32.dll")]
        internal static extern bool DeleteObject(IntPtr hObject);



        /// <summary>
        /// 获取预览图
        /// </summary>
        /// <param name="nCard"></param>
        /// <param name="Null"></param>
        /// <param name="bmpwidth"></param>
        /// <param name="bmpheight"></param>
        /// <returns></returns>
        [DllImport("MultiMarkEzd", EntryPoint = "lmc1_GetPrevBitmap2", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr GetPrevBitmap(int nCard, int nhandle, int bmpwidth, int bmpheight);

        /// <summary>
        /// 得到指定控制卡里面数据的预览图片
        /// </summary>   
        public static Image GetEzdFilePreviewImage(int nCard, int bmpwidth, int bmpheight)
        {
            IntPtr pBmp = GetPrevBitmap(nCard, 0, bmpwidth, bmpheight);
            Image img = Image.FromHbitmap(pBmp);
            DeleteObject(pBmp);
            return img;
        }



        /// <summary>
        /// 判断控制卡是否为空闲状态，true为空闲
        /// </summary>
        /// <param name="nCard"></param>
        /// <returns></returns>
        [DllImport("MultiMarkEzd", EntryPoint = "lmc1_IsMarkFinish", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern bool IsMarkFinish(int nCard);

        /// <summary>
        /// 停止加工函数
        /// </summary>
        /// <param name="nCard"></param>
        /// <returns></returns>
        [DllImport("MultiMarkEzd", EntryPoint = "lmc1_StopMark", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern bool StopMark(int nCard);


        /// <summary>
        /// 配置参数
        /// </summary>
        /// <param name="nCard"></param>
        /// <returns></returns>
        [DllImport("MultiMarkEzd", EntryPoint = "lmc1_SetDevCfg", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetDevCfg(int nCard);

        /// <summary>
        /// 设置笔号接口
        /// </summary>
        /// <returns></returns>
        [DllImport("MultiMarkEzd", EntryPoint = "lmc1_SetPenParam", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetPenParam(int nCard,
            int nPenNo,
                           int nMarkLoop,
                           double dMarkSpeed,
                           double dPowerRatio,
                           double dCurrent,
                           int nFreq,
                           double dQPulseWidth,
                           int nStartTC,
                           int nLaserOffTC,
                           int nEndTC,
                           int nPolyTC,
                           double dJumpSpeed,
                           int nJumpPosTC,
                           int nJumpDistTC,
                           double dEndComp,
                           double dAccDist,
                           double dPointTime,
                           bool bPulsePointMode,
                           int nPulseNum,
                           double dFlySpeed);
        /// <summary>
        /// 得到笔号接口
        /// </summary>
        /// <returns></returns>
        [DllImport("MultiMarkEzd", EntryPoint = "lmc1_GetPenParam", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetPenParam(int nCard,
            int nPenNo,
                    ref int nMarkLoop,
                    ref double dMarkSpeed,
                    ref double dPowerRatio,
                    ref double dCurrent,
                    ref int nFreq,
                    ref double dQPulseWidth,
                    ref int nStartTC,
                    ref int nLaserOffTC,
                    ref int nEndTC,
                    ref int nPolyTC,
                    ref double dJumpSpeed,
                    ref int nJumpPosTC,
                    ref int nJumpDistTC,
                    ref double dEndComp,
                    ref double dAccDist,
                    ref double dPointTime,
                    ref bool bPulsePointMode,
                    ref int nPulseNum,
                    ref double dFlySpeed);

        [DllImport("MultiMarkEzd", EntryPoint = "lmc1_ReadPort", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int ReadPort(int nCard, ref ushort data);

        [DllImport("MultiMarkEzd", EntryPoint = "lmc1_WritePort", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int WritePort(int nCard, ushort data);



    }
}
