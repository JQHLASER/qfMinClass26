
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace qfWork
{
    internal class JczLmc : Language_
    {
        /// <summary>
        /// EzCad2.exe目录
        /// </summary>
        internal static string path_EzCad2 = Environment.CurrentDirectory + "\\Ezd2";
        static JczLmc()
        {
            new qfmain.软件类().DllImport相对路径("Ezd2");
        }


        /// <summary>
        /// Err消息
        /// </summary>
        /// <param name="errs"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Err消息
        /// </summary>
        /// <param name="errs"></param>
        /// <returns></returns>
        public static string ErrMsg(int errs)
        {
            return ErrMsg((_Err_jczMarkEzd2_)errs);
        }

         
      
         

        /// <summary>
        /// 初始化函数库
        /// PathName 是Ezd2\\Miks.dll所在的目录
        /// </summary>     
        [DllImport("Ezd2\\Miks.dll", EntryPoint = "lmc1_Initial2", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int Initialize2(string PathName, bool bTestMode);

        /// <summary>
        /// 初始化函数库
        /// PathName 是Ezd2\\Miks.dll所在的目录
        /// <returns></returns>
        [DllImport("Ezd2\\Miks", EntryPoint = "lmc1_Initial", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int Initialize(string PathName, bool bTestMode, int handle);


        /// <summary>
        /// 释放函数库
        /// </summary>     
        [DllImport("Ezd2\\Miks", EntryPoint = "lmc1_Close", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int Close();

        /// <summary>
        /// 得到设备参数配置对话框  
        /// </summary> 
        [DllImport("Ezd2\\Miks", EntryPoint = "lmc1_SetDevCfg", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int SetDevCfg();

        /// <summary>
        /// 得到设备参数配置对话框  
        /// </summary>
        /// <param name="bAxisShow0"></param>
        /// <param name="bAxisShow1"></param>
        /// <returns></returns>
        [DllImport("Ezd2\\Miks", EntryPoint = "lmc1_SetDevCfg2", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int SetDevCfg2(bool bAxisShow0, bool bAxisShow1);


        /// <summary>
        /// 载入ezd文件到当前数据库里面,并清除旧的数据库
        /// </summary>   
        [DllImport("Ezd2\\Miks", EntryPoint = "lmc1_LoadEzdFile", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int LoadEzdFile(string FileName);


        /// <summary>
        /// 得到当前数据库里面数据的预览图片
        /// </summary>  
        [DllImport("gdi32.dll")]
        internal static extern bool DeleteObject(IntPtr hObject);
        [DllImport("Ezd2\\Miks", EntryPoint = "lmc1_GetPrevBitmap2", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr GetCurPrevBitmap(int bmpwidth, int bmpheight);
        internal static Image GetCurPreviewImage(int bmpwidth, int bmpheight)
        {
            IntPtr pBmp = GetCurPrevBitmap(bmpwidth, bmpheight);
            Image img = Image.FromHbitmap(pBmp);
            JczLmc.DeleteObject(pBmp);
            return img;
        }

        /// <summary>
        /// 得到对象总数
        /// </summary> 
        [DllImport("Ezd2\\Miks", EntryPoint = "lmc1_GetEntityCount", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern ushort GetEntityCount();

        /// <summary>
        /// 得到指定索引号的对象的名称。
        /// </summary>   
        [DllImport("Ezd2\\Miks", EntryPoint = "lmc1_GetEntityName", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int lmc1_GetEntityNameByIndex(int nEntityIndex, StringBuilder entname);
        internal static string GetEntityNameByIndex(int nEntityIndex)
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
        [DllImport("Ezd2\\Miks", EntryPoint = "lmc1_InitUsbMonitor2", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern bool InitUsbMonitor2(IntPtr hWndMonitor);

        /// <summary>
        ///  关闭USB设备监控
        /// </summary>
        [DllImport("Ezd2\\Miks", EntryPoint = "lmc1_CloseUsbMonitor", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern bool CloseUsbMonitor();

        /// <summary>
        /// 判断当前设备是否掉电
        /// </summary>
        /// <param name="bValid">当前设备是否正常</param>
        /// <returns></returns>
        [DllImport("Ezd2\\Miks", EntryPoint = "lmc1_CheckCurrentUsbDev", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int CheckCurrentUsbDev(ref bool bValid);


        /// <summary>
        /// 判断当前卡是否需要初始化
        /// </summary>
        /// <param name="CardID"></param>
        /// <returns>返回成功标识有效设备</returns>
        [DllImport("Ezd2\\Miks", EntryPoint = "lmc1_CheckNewUsbDev", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int CheckNewUsbDev(int CardID);


        /// <summary>
        ///  获取新设备
        /// </summary>
        /// <returns>返回设备ID编号</returns>
        [DllImport("Ezd2\\Miks", EntryPoint = "lmc1_UsbMonitorGetNewDevice", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int UsbMonitorGetNewDevice();




        [DllImport("Ezd2\\Miks", EntryPoint = "lmc1_GetFuntionID", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int 获取ID();
        // else if (LmcDll.GetFuntionID() != 474)
        //{
        //	MessageBox.Show(LmcDll.GetFuntionID().ToString());
        //	base.Close();
        //   }



        /// <summary>
        /// 传入绝对坐标
        /// </summary>
        /// <param name="x坐标"></param>
        /// <param name="y坐标"></param>
        /// <param name="x旋转中心"></param>
        /// <param name="y旋转中心"></param>
        /// <param name="A"></param>
        /// <returns></returns>
        [DllImport("Ezd2\\Miks", EntryPoint = "lmc1_SetRotateMoveParam", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int 旋转变换(double x坐标, double y坐标, double x旋转中心, double y旋转中心, double A);



        /// <summary>
        /// 标刻库中所有对象
        /// </summary>
        /// <param name="bFlyMark"></param>
        /// <returns></returns>
        [DllImport("Ezd2\\Miks", EntryPoint = "lmc1_Mark", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int Mark(bool bFlyMark);


        /// <summary>
        /// 标刻库中指定对象
        /// </summary>
        /// <param name="As"></param>
        /// <param name=""></param>
        /// <returns></returns>
        [DllImport("Ezd2\\Miks", EntryPoint = "lmc1_MarkEntity", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int Mark指定对象(string strEntName);


        /// <summary>
        /// 判断卡是否正在处于工作状态,返回：bool 值，true 表示标刻工作状态false表示停止状态
        /// </summary>
        /// <returns></returns>
        [DllImport("Ezd2\\Miks", EntryPoint = "lmc1_IsMarking", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern bool 获取卡是否处于工作状态();

        /// <summary>
        /// 停止标刻或红光
        /// </summary>
        /// <returns></returns>
        [DllImport("Ezd2\\Miks", EntryPoint = "lmc1_StopMark", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int StopMark();


        [DllImport("Ezd2\\Miks", EntryPoint = "lmc1_RedLightMark", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int Red外框();


        [DllImport("Ezd2\\Miks", EntryPoint = "lmc1_RedLightMarkContour", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int Red轮廓();


        [DllImport("Ezd2\\Miks", EntryPoint = "lmc1_RedLightMarkByEnt", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int Red指定对象(string 对象名称, bool 是否显示轮廓);



        [DllImport("Ezd2\\Miks", EntryPoint = "lmc1_GetFlySpeed", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int 获取飞行速度(ref double 飞行速度);


        [DllImport("Ezd2\\Miks", EntryPoint = "lmc1_LoadEzdFile", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int 加载ezd(string ezdPath);


        [DllImport("Ezd2\\Miks", EntryPoint = "lmc1_GetPrevBitmap",  CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr 获取图象_hwnd(IntPtr 需要显示图像的句柄, int 宽度, int 高度);


        [DllImport("Ezd2\\Miks", EntryPoint = "lmc1_GetPrevBitmap2",   CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr 获取图象2(int 宽度, int 高度);


        [DllImport("Ezd2\\Miks", EntryPoint = "lmc1_SaveEntLibToFile", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int 保存ezd(string ezdPath);

        [DllImport("Ezd2\\Miks", EntryPoint = "lmc1_GetEntSize", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int 获取指定对象的最大最小坐标(string 对象名称, ref double x小, ref double y小, ref double x大, ref double y大, ref double z);


        [DllImport("Ezd2\\Miks", EntryPoint = "lmc1_MoveEnt", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int 指定对象移动相对位置(string 对象名称, double x, double y);



        [DllImport("Ezd2\\Miks", EntryPoint = "lmc1_ScaleEnt", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int 指定对象按比例缩放(string 对象名称, double x中心, double y中心, double x缩放比例, double y缩放比例);


        [DllImport("Ezd2\\Miks", EntryPoint = "lmc1_MirrorEnt", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int 指定对象镜像(string 对象名称, double x中心, double y中心, bool 是否x方向镜像, bool 是否y方向镜象);


        [DllImport("Ezd2\\Miks", EntryPoint = "lmc1_RotateEnt", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int 指定对象旋转(string 对象名称, double x中心, double y中心, double A);


        [DllImport("Ezd2\\Miks", EntryPoint = "lmc1_GetEntityCount", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int 获取对象总数();


        [DllImport("Ezd2\\Miks", EntryPoint = "lmc1_GetEntityName", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int 获取指定序号的对象名称(int 对象索引, StringBuilder Name);


        [DllImport("Ezd2\\Miks", EntryPoint = "lmc1_SetEntityName", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int 设置指定索引的对象名称(int 对象索引, string Name);


        [DllImport("Ezd2\\Miks", EntryPoint = "lmc1_ReadPort", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int ReadInput(ref int input);

        [DllImport("Ezd2\\Miks", EntryPoint = "lmc1_WritePort", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int WriteOutput(int OutPut);


        [DllImport("Ezd2\\Miks", EntryPoint = "lmc1_GetOutPort", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int ReadOutput(ref int OutPut);


        [DllImport("Ezd2\\Miks", EntryPoint = "lmc1_LaserOn", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int 激光输出(bool 是否出光);

        [DllImport("Ezd2\\Miks", EntryPoint = "lmc1_ChangeTextByName", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int 修改指定对象的内容(string strName, string strData);



        [DllImport("Ezd2\\Miks", EntryPoint = "lmc1_GetTextByName", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int 获取指定对象的内容(string strName, StringBuilder strData);


        [DllImport("Ezd2\\Miks", EntryPoint = "lmc1_TextResetSn", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int 重置序列号(string strName);


        [DllImport("Ezd2\\Miks", EntryPoint = "lmc1_ClearEntLib", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int 删除所有对象();


        /// <summary>
        /// 判断当前设备是否掉电
        /// </summary>
        /// <param name="是否掉电"></param>
        /// <returns></returns>
        [DllImport("Ezd2\\Miks", EntryPoint = "lmc1_CheckCurrentUsbDev", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int 判断当前设备是否掉电(ref bool 是否掉电);


        [DllImport("Ezd2\\Miks", EntryPoint = "lmc1_Reset", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int 扩展轴_使能(bool axis0使能, bool axis1使能);


        /// <summary>
        /// 0或1
        /// </summary>
        /// <param name="轴号"></param>
        /// <returns></returns>
        [DllImport("Ezd2\\Miks", EntryPoint = "lmc1_AxisCorrectOrigin", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int 扩展轴_回零(int 轴号);



        [DllImport("Ezd2\\Miks", EntryPoint = "lmc1_AxisMoveTo", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int 扩展轴_移动到指定坐标(int 轴号, double 绝对坐标);


        [DllImport("Ezd2\\Miks", EntryPoint = "lmc1_AxisMoveToPulse", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int 扩展轴_移动到指定脉冲位置(int 轴号, int 脉冲数);



        [DllImport("Ezd2\\Miks", EntryPoint = "lmc1_GetAxisCoor", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern double 扩展轴_获取当前坐标(int 轴号);

        [DllImport("Ezd2\\Miks", EntryPoint = "lmc1_GetAxisCoorPulse", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int 扩展轴_获取当前脉冲数坐标(int 轴号);


        [DllImport("Ezd2\\Miks", EntryPoint = "lmc1_GetPenParam", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int 获取笔参数(int 笔号,
                                                 ref int 加工次数,
                                                 ref double 速度,
                                                 ref double 功率,
                                                 ref double 电流,
                                                 ref int 频率,
                                                 ref double Q脉冲宽度,
                                                 ref int 开光延时,
                                                 ref int 关光延时,
                                                 ref int 结束延时,
                                                 ref int 拐角延时,
                                                 ref double 跳转速度,
                                                 ref int 跳转位置延时,
                                                 ref int 跳转距离延时,
                                                 ref double 末点补偿,
                                                 ref double 加速距离,
                                                 ref double 打点时间,
                                                 ref bool 脉冲打点模式,
                                                 ref int 脉冲点数目,
                                                 ref double 流水线速度
                                                 );








        [DllImport("Ezd2\\Miks", EntryPoint = "lmc1_SetPenParam", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int 设置笔参数(int 笔号,
                                                int 加工次数,
                                                double 速度,
                                                double 功率,
                                                double 电流,
                                                int 频率,
                                                double Q脉冲宽度,
                                                int 开光延时,
                                                int 关光延时,
                                                int 结束延时,
                                                int 拐角延时,
                                                double 跳转速度,
                                                int 跳转位置延时,
                                                int 跳转距离延时,
                                                double 末点补偿,
                                                double 加速距离,
                                                double 打点时间,
                                                bool 脉冲打点模式,
                                                int 脉冲点数目,
                                                double 流水线速度
                                                );



        [DllImport("Ezd2\\Miks", EntryPoint = "lmc1_SetTextEntParam", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int 设置指定对象的字体参数(string textName,
                                                             double 字体高度,
                                                             double 字体宽度,
                                                             double 字符弧度,
                                                             double 字符间距,
                                                             double 行间距,
                                                             bool 等字符宽度模式
                                                              );


        /// <summary>
        /// //设置默认字体名称的字体参数__需要重新添加是才生效______2.14以前的,盗版卡用'
        /// </summary>
        /// <param name="字体名称"></param>
        /// <param name="字体高度"></param>
        /// <param name="字体宽度"></param>
        /// <param name="字符弧度"></param>
        /// <param name="字符间距"></param>
        /// <param name="行间距"></param>
        /// <param name="等字符宽度模式"></param>
        /// <returns></returns>
        [DllImport("Ezd2\\Miks", EntryPoint = "lmc1_SetFontParam", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int 设置指定字体的参数(string 字体名称,
                                                          double 字体高度,
                                                          double 字体宽度,
                                                          double 字符弧度,
                                                          double 字符间距,
                                                          double 行间距,
                                                          bool 等字符宽度模式
                                                           );

        [DllImport("Ezd2\\Miks", EntryPoint = "lmc1_GetTextEntParam", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int 获取指定文本的字体参数(string textName,
                                                            StringBuilder 字体名称,
                                                            double 字体高度,
                                                            double 字体宽度,
                                                            double 字符弧度,
                                                            double 字符间距,
                                                            double 行间距,
                                                            bool 等字符宽度模式
                                                              );


        [DllImport("Ezd2\\Miks", EntryPoint = "lmc1_MarkFlyByStartSignal", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int 飞行_标刻所有数据();


        [DllImport("Ezd2\\Miks", EntryPoint = "lmc1_MarkEntityFly", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int 飞行_标刻指定的对象(string textName);


        [DllImport("Ezd2\\Miks", EntryPoint = "lmc1_ContinueBufferClear", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int 清空板卡缓存();



        [DllImport("Ezd2\\Miks", EntryPoint = "lmc1_ContinueBufferSetTextName", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int 飞行_设置加工时需要更改内容的对象名称(string textName1,
                                                                           string textName2,
                                                                           string textName3,
                                                                           string textName4,
                                                                           string textName5,
                                                                           string textName6
                                                                           );



        /// <summary>
        /// 添加需要加工的数据内容。如果缓存区没有空间，加入会产生数据错误'
        /// </summary>
        /// <param name="textName1"></param>
        /// <param name="data2"></param>
        /// <param name="tata3"></param>
        /// <param name="data4"></param>
        /// <param name="data5"></param>
        /// <param name="data6"></param>
        /// <returns></returns>
        [DllImport("Ezd2\\Miks", EntryPoint = "lmc1_ContinueBufferFlyAdd", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int 飞行_添回需要加工的数据(string textName1,
                                                                         string data2,
                                                                         string tata3,
                                                                         string data4,
                                                                         string data5,
                                                                         string data6
                                                                         );


        /// <summary>
        /// 启动连续飞行模式，启动后，系统会按输入顺序实现数据更换，硬件飞行信号触发加工_飞行专用
        /// </summary>
        /// <returns></returns>
        [DllImport("Ezd2\\Miks", EntryPoint = "lmc1_ContinueBufferFlyStart", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int 飞行_启动连续飞行();



        /// <summary>
        /// 检查加工完成数量，缓存区状态, 缓存区数据数量， 如果此数据小于8 说明可以使用
        /// </summary>
        /// <param name="累计完成次数"></param>
        /// <param name="缓存区数量"></param>
        /// <returns></returns>
        [DllImport("Ezd2\\Miks", EntryPoint = "lmc1_ContinueBufferFlyGetParam", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int 飞行_检查加工完成数量(ref int 累计完成次数, ref int 缓存区数量);


        /// <summary>
        /// 添加加工结束点。加工到此结束点后，自动退出连续飞行加工模式
        /// </summary>
        /// <returns></returns>
        [DllImport("Ezd2\\Miks", EntryPoint = "lmc1_ContinueBufferPartFinish", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int 飞行_添加加工结束点();




        /// <summary>
        /// 填充对象
        /// </summary>
        /// <param name="bEnableContour"></param>
        /// <param name="nParamIndex"></param>
        /// <param name="bEnableHatch"></param>
        /// <param name="nPenNo"></param>
        /// <param name="nHatchType"></param>
        /// <param name="bHatchAllCalc"></param>
        /// <param name="bHatchEdge"></param>
        /// <param name="bHatchAverageLine"></param>
        /// <param name="dHatchAngle"></param>
        /// <param name="dHatchLineDist"></param>
        /// <param name="dHatchEdgeDist"></param>
        /// <param name="dHatchStartOffset"></param>
        /// <param name="dHatchEndOffset"></param>
        /// <param name="dHatchLineReduction"></param>
        /// <param name="dHatchLoopDist"></param>
        /// <param name="nEdgeLoop"></param>
        /// <param name="nHatchLoopRev"></param>
        /// <param name="bHatchAutoRotate"></param>
        /// <param name="dHatchRotateAngle"></param>
        /// <param name="bHatchCross"></param>
        /// <returns></returns>
        [DllImport("Ezd2\\Miks", EntryPoint = "lmc1_SetHatchParam3", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int SetHatchParam3(bool bEnableContour,//使能轮廓本身
                                                         int nParamIndex,//填充参数序号值为1,2,3
                                                         int bEnableHatch,//使能填充
                                                         int nPenNo,//填充参数笔号
                                                         int nHatchType,//填充类型 0单向 1双向 2回形 3弓形 4弓形不反向
                                                         bool bHatchAllCalc,//是否全部对象作为整体一起计算
                                                         bool bHatchEdge,//绕边一次
                                                         bool bHatchAverageLine,//自动平均分布线
                                                         double dHatchAngle,//填充线角度 
                                                         double dHatchLineDist,//填充线间距
                                                         double dHatchEdgeDist,//填充线边距    
                                                         double dHatchStartOffset,//填充线起始偏移距离
                                                         double dHatchEndOffset,//填充线结束偏移距离
                                                         double dHatchLineReduction,//直线缩进
                                                         double dHatchLoopDist,//环间距
                                                         int nEdgeLoop,//环数
                                                         bool nHatchLoopRev,//环形反转
                                                         bool bHatchAutoRotate,//是否自动旋转角度
                                                         double dHatchRotateAngle,//自动旋转角度  
                                                         bool bHatchCross
                                                      );


        ///// <summary>
        ///// 向数据库添加一条曲线对象
        ///// 注意PtBuf必须为2维数组,且第一维为2,如 double[5,2],double[n,2],
        ///// ptNum为PtBuf数组的第2维,如PtBuf为double[5,2]数组,则ptNum=5
        ///// </summary> 
        //[DllImport("MarkEzd", EntryPoint = "lmc1_AddCurveToLib", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        //internal static extern int 添加曲线([MarshalAs(UnmanagedType.LPArray)] double[,] PtBuf, int ptNum, string strEntName, int nPenNo, int bHatch);


        /// <summary>
        /// 向数据库添加一条曲线对象
        /// 注意PtBuf必须为2维数组,且第一维为2,如 double[5,2],double[n,2],
        /// ptNum为PtBuf数组的第2维,如PtBuf为double[5,2]数组,则ptNum=5
        /// </summary> 
        [DllImport("Ezd2\\Miks", EntryPoint = "lmc1_AddCurveToLib", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int 添加曲线([MarshalAs(UnmanagedType.LPArray)] double[,] 曲线顶点数组, int 顶点数量, string 对象名称, int 笔号, int 是否填充);



        ///<summary>
        /// 删除指定名称对象
        ///<summary>
        [DllImport("Ezd2\\Miks", EntryPoint = "lmc1_DeleteEnt", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int 删除指定对象(string 对象名称);

        [DllImport("Ezd2\\Miks", EntryPoint = "lmc1_GetPenNumberFromEnt", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int 获取指定对象笔号(string 对象名称);





    }
}
