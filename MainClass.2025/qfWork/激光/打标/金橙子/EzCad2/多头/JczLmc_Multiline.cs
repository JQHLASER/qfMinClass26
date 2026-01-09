using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace qfWork 
{
    public  class JczLmc_Multiline : Language_
    {
        /// <summary>
        /// EzCad2.exe目录
        /// </summary>
        internal static string path_EzCad2 = Environment.CurrentDirectory + "\\Ezd2";
        static JczLmc_Multiline()
        {
            new qfmain.软件类().DllImport相对路径("Ezd2");
        }

        public static string ErrMsg(_Err_jczMarkEzd2_ errs)
        {
            switch (errs)
            {
                case _Err_jczMarkEzd2_.成功:
                    return Get语言("成功");
                case _Err_jczMarkEzd2_.发现EZCAD在运行:
                    return Get语言("发现EZCAD在运行");
                case _Err_jczMarkEzd2_.找不到EZCAD_CFG文件:
                    return Get语言("找不到EZCAD_CFG文件");
                case _Err_jczMarkEzd2_.打开LMC1失败:
                    return Get语言("打开LMC1失败");
                case _Err_jczMarkEzd2_.没有有效的lmc1设备:
                    return Get语言("没有有效的lmc1设备");
                case _Err_jczMarkEzd2_.lmc1版本错误:
                    return Get语言("lmc1版本错误");
                case _Err_jczMarkEzd2_.找不到设备配置文件:
                    return Get语言("找不到设备配置文件");
                case _Err_jczMarkEzd2_.报警信号:
                    return Get语言("报警信号");
                case _Err_jczMarkEzd2_.用户停止:
                    return Get语言("用户停止");
                case _Err_jczMarkEzd2_.不明错误:
                    return Get语言("不明错误");
                case _Err_jczMarkEzd2_.超时:
                    return Get语言("超时");
                case _Err_jczMarkEzd2_.未初始化:
                    return Get语言("未初始化");
                case _Err_jczMarkEzd2_.读文件错误:
                    return Get语言("读文件错误");
                case _Err_jczMarkEzd2_.窗口为空:
                    return Get语言("窗口为空");
                case _Err_jczMarkEzd2_.找不到指定名称的字体:
                    return Get语言("找不到指定名称的字体");
                case _Err_jczMarkEzd2_.错误的笔号:
                    return Get语言("错误的笔号");
                case _Err_jczMarkEzd2_.指定名称的对象不是文本对象:
                    return Get语言("指定名称的对象不是文本对象");
                case _Err_jczMarkEzd2_.保存文件失败:
                    return Get语言("保存文件失败");
                case _Err_jczMarkEzd2_.找不到指定对象:
                    return Get语言("找不到指定对象");
                case _Err_jczMarkEzd2_.当前状态下不能执行此操作:
                    return Get语言("当前状态下不能执行此操作");
                case _Err_jczMarkEzd2_.硬件不可开发:
                    return Get语言("硬件不可开发");
                case _Err_jczMarkEzd2_.未找到ezd文件:
                    return Get语言("未找到ezd文件");



                case _Err_jczMarkEzd2_.DL故障:
                    return Get语言("DL故障");
                default:
                    return Get语言("未知错误");
            }
            
        }

        public static string ErrMsg(int errs)
        {
            return ErrMsg((_Err_jczMarkEzd2_)errs);
        }



        #region Dll区

        /// <summary>
        /// 激光是否工作中
        /// </summary>
        static internal int laserOn = 0;

        [DllImport("Ezd2\\Miksdle.dll.dll", EntryPoint = "lmc1_ClearEntLib", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int 清空所有对象(int 卡ID);


        /// <summary>
        /// 初始化;pathName: Environment.CurrentDirectory +"\\"
        /// </summary>
        /// <param name="PathName"> Environment.CurrentDirectory +"\\"</param>
        /// <param name="bTestMode"></param>
        /// <param name="handle"></param>
        /// <returns></returns>
        [DllImport("Ezd2\\Miksdle.dll", EntryPoint = "lmc1_Initial", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int 初始化(string PathName, bool bTestMode, int handle);


        [DllImport("Ezd2\\Miksdle.dll", EntryPoint = "lmc1_Close", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int Close();

        [DllImport("Ezd2\\Miksdle.dll", EntryPoint = "lmc1_GetAllCardSN", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int 得到所有卡序号(ref int nFirstCardSN);


        /// <summary>
        /// 配置参数
        /// </summary>
        /// <param name="卡ID"></param>
        /// <returns></returns>
        [DllImport("Ezd2\\Miksdle.dll", EntryPoint = "lmc1_SetDevCfg", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int 参数SetDevCfg(int 卡ID序号);

        /// <summary>
        /// 载入ezd文件到当前数据库里面,并清除旧的数据库
        /// </summary>   
        [DllImport("Ezd2\\Miksdle.dll", EntryPoint = "lmc1_LoadEzdFile", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int 加载ezd(int 卡ID, string FileName);

        [DllImport("Ezd2\\Miksdle.dll", EntryPoint = "lmc1_SaveEntLibToFile", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int 保存ezd文件(int CardSN, string strFileName);




        /// <summary>
        /// 获取预览图
        /// </summary>
        /// <param name="卡ID"></param>
        /// <param name="Null"></param>
        /// <param name="bmpwidth"></param>
        /// <param name="bmpheight"></param>
        /// <returns></returns>
        [DllImport("Ezd2\\Miksdle.dll", EntryPoint = "lmc1_GetPrevBitmap2",   CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr GetPrevBitmap2(int 卡ID, int nhandle, int bmpwidth, int bmpheight);




        /// <summary>
        /// 获取预览图
        /// </summary>
        /// <param name="卡ID"></param>
        /// <param name="Null"></param>
        /// <param name="bmpwidth"></param>
        /// <param name="bmpheight"></param>
        /// <returns></returns>
        [DllImport("Ezd2\\Miksdle.dll", EntryPoint = "lmc1_GetPrevBitmap",  CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr GetPrevBitmap(int 卡ID, int bmpwidth, int bmpheight);







        /// <summary>
        /// 标刻函数 此接口为非阻塞型函数
        /// </summary>
        /// <param name="卡ID"></param>
        /// <param name="Fly"></param>
        /// <returns></returns>

        [DllImport("Ezd2\\Miksdle.dll", EntryPoint = "lmc1_Mark", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int Mark(int 卡ID, bool Fly);

        [DllImport("Ezd2\\Miksdle.dll", EntryPoint = "lmc1_MarkEntity", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int Mark指定对象(int CardSN, string 对象名称);




        /// <summary>
        /// 停止加工函数
        /// </summary>
        /// <param name="卡ID"></param>
        /// <returns></returns>
        [DllImport("Ezd2\\Miksdle.dll", EntryPoint = "lmc1_StopMark", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int StopMark(int 卡ID);

        /// <summary>
        /// 判断控制卡是否为空闲状态，1为空闲(1),0为加工中(0)
        /// </summary>
        /// <param name="卡ID"></param>
        /// <returns></returns>
        [DllImport("Ezd2\\Miksdle.dll", EntryPoint = "lmc1_IsMarkFinish", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int 获取卡是否为空闲状态(int 卡ID);


        [DllImport("Ezd2\\Miksdle.dll", EntryPoint = "lmc1_SetRotateMoveParam", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int 设置数据库的所有对象的平移旋转(int 卡ID, double X方向移动距离, double Y方向移动距离, double X旋转中心坐标, double Y旋转中心坐标, double 旋转角度);




        [DllImport("Ezd2\\Miksdle.dll", EntryPoint = "lmc1_RedLightMark", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int 红光指示外框(int CardSN);

        //[DllImport("Ezd2\\Miksdle.dll", EntryPoint = "lmc1_RedLightMarkContour", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        //internal static extern int 红光指示轮廓(int CardSN);


        [DllImport("Ezd2\\Miksdle.dll", EntryPoint = "lmc1_RedLightMarkContourByEnt", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int 红光指示指定对象的轮廓(int CardSN, string 对象名称);






        [DllImport("Ezd2\\Miksdle.dll", EntryPoint = "lmc1_GetEntityCount", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int 获取对象总数(int CardSN);


        [DllImport("Ezd2\\Miksdle.dll", EntryPoint = "lmc1_ChangeTextByName", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int 修改指定对象的内容(int CardSN, string 对象名称, string 新的内容);

        [DllImport("Ezd2\\Miksdle.dll", EntryPoint = "lmc1_TextResetSn", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int 重置序列号(int 卡ID, string 对象名称);


        [DllImport("Ezd2\\Miksdle.dll", EntryPoint = "lmc1_GetEntityName", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int 获取指定序号的对象名称(int 卡ID, int 序号, StringBuilder 对象名称);


        [DllImport("Ezd2\\Miksdle.dll", EntryPoint = "lmc1_GetTextByName", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int 获取指定对象的内容(int 卡ID, string 对象名称, StringBuilder 内容);






        /// <summary>
        /// 读输入口
        /// </summary>
        /// <param name="卡ID"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        [DllImport("Ezd2\\Miksdle.dll", EntryPoint = "lmc1_ReadPort", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int 读输入口状态(int 卡ID, ref ushort data);


        [DllImport("Ezd2\\Miksdle.dll", EntryPoint = "lmc1_GetOutPort", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int 读输出口状态(int 卡ID, ref ushort data);


        [DllImport("Ezd2\\Miksdle.dll", EntryPoint = "lmc1_WritePort", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int 写输出口(int 卡ID, int data);


        /// <summary>
        /// 设置笔号接口
        /// </summary>
        /// <returns></returns>
        [DllImport("Ezd2\\Miksdle.dll", EntryPoint = "lmc1_SetPenParam", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int 设置笔号参数(int 卡ID,
            int 笔号,
                           int 加工次数,
                           double 标刻速度,
                           double 功率百分比,
                           double 电流,
                           int 频率,
                           double Q脉冲宽度,
                           int 开始延时,
                           int 激光关闭延时,
                           int 结束延时,
                           int 拐角延时,
                           double 跳转速度,
                           int 跳转位置延时,
                           int 跳转距离延时,
                           double 末点补偿,
                           double 加速距离,
                           double 打点时间,
                           bool 脉冲点模式,
                           int 脉冲点数目,
                           double 流水线速度);



        /// <summary>
        /// 得到笔号接口
        /// </summary>
        /// <returns></returns>
        [DllImport("Ezd2\\Miksdle.dll", EntryPoint = "lmc1_GetPenParam", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int 获取笔号参数(int 卡ID,
            int 笔号,
                    ref int 加工次数,
                    ref double 标刻速度,
                    ref double 功率百分比,
                    ref double 电流,
                    ref int 频率,
                    ref double Q脉冲宽度,
                    ref int 开始延时,
                    ref int 激光关闭延时,
                    ref int 结束延时,
                    ref int 拐角延时,
                    ref double 跳转速度,
                    ref int 跳转位置延时,
                    ref int 跳转距离延时,
                    ref double 末点补偿,
                    ref double 加速距离,
                    ref double 打点时间,
                    ref bool 脉冲点模式,
                    ref int 脉冲点数目,
                    ref double 流水线速度);


        /// <summary>
        /// false表示出光使能;true表示不出光不使能
        /// </summary>
        /// <param name="卡ID"></param>
        /// <param name="笔号"></param>
        /// <param name="是否使能打标"></param>
        /// <returns></returns>
        [DllImport("Ezd2\\Miksdle.dll", EntryPoint = "lmc1_SetPenDisableState", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int 设置笔号是否打标(int 卡ID, int 笔号, bool 是否使能打标);

        /// <summary>
        /// false表示出光使能;true表示不出光不使能
        /// </summary>
        /// <param name="卡ID"></param>
        /// <param name="笔号"></param>
        /// <param name="是否使能打标"></param>
        /// <returns></returns>
        [DllImport("Ezd2\\Miksdle.dll", EntryPoint = "lmc1_GetPenDisableState", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int 获取笔号是否打标(int 卡ID, int 笔号, ref bool 是否使能打标);


        [DllImport("Ezd2\\Miksdle.dll", EntryPoint = "lmc1_MoveEnt", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int 指定对象移动相对位置(int 卡ID,
                                                           string 对象名称,
                                                           ref double 移动的X坐标,
                                                           ref double 移动的Y坐标);






        [DllImport("Ezd2\\Miksdle.dll", EntryPoint = "lmc1_RotateEnt", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int 指定对象旋转(int 卡ID,
                                                       string 对象名称,
                                                     double 旋转中心的X坐标,
                                                       double 旋转中心的Y坐标,
                                                      double 旋转角度);


        [DllImport("Ezd2\\Miksdle.dll", EntryPoint = "lmc1_GetEntSize", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int 获取指定对象的最大最小坐标(int 卡ID,
                                                                 string 对象名称,
                                                                 ref double 最小X坐标,
                                                                 ref double 最小Y坐标,
                                                                 ref double 最大X坐标,
                                                                 ref double 最大Y坐标,
                                                                 ref double Z坐标);



        [DllImport("Ezd2\\Miksdle.dll", EntryPoint = "lmc1_SetEntityName", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int 设置指定序号的对象名称(int 卡ID,
                                                             int 序号,
                                                           string 新的对象名称
                                                           );







        #endregion


    }
}
