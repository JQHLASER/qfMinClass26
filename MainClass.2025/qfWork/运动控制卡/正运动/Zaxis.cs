
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace qfWork
{
    /// <summary>
    /// 正运动
    /// </summary>
    public class Zaxis
    {
        /// <summary>
        /// 系统会检测与写入到控制器的功能ID匹配检测
        /// </summary>
        public string _功能码 { set; get; } = "QIFENG22A1801U";



        //每组8个IO口
        public Zaxis(string 控制器名称, int IO输入组数 = 2, int IO输出组数 = 2, string pathCfg = "", bool is匹配功能码_ = true)
        {
            _控制器名称 = 控制器名称;
            _is匹配功能码 = is匹配功能码_;
            _IO输入组数 = IO输入组数;
            _IO输出组数 = IO输出组数;
            _Path_Cfg = !string.IsNullOrEmpty(pathCfg) ? pathCfg : _pathCfgDefault;
            读参数();

        }

        public Zaxis()
        {

            读参数();
        }



        #region 变量区

        private int _IO输入组数 = 2;
        private int _IO输出组数 = 2;
        /// <summary>
        /// 是否匹配功能码
        /// </summary>
        private bool _is匹配功能码 = true;

        /// <summary>
        /// 存放参数文件的路径默认值
        /// </summary>
        static string _pathCfgDefault = qfmain.软件类.Files_Cfg.Files_Config + "\\zaxiscfg.zax";
        /// <summary>
        /// 存放参数文件的路径
        /// </summary>
        private string _Path_Cfg = _pathCfgDefault;
        private string _控制器名称 = "";

        public _连接状态_ _连接状态 = _连接状态_.未连接;
        /// <summary>
        /// 控制器句柄
        /// </summary>
        public IntPtr _handle { set; get; } = IntPtr.Zero;
        public _Zaxis_连接参数_ _连接参数 { set; get; } = new _Zaxis_连接参数_();
        public _Zaxis_控制器参数_ _控制器参数 { set; get; } = new _Zaxis_控制器参数_();

        #endregion

        #region 方法区

        public void 读参数(ushort model = 1)
        {
            _Zaxis_连接参数_ info = _连接参数;
            bool rt = new qfmain.文件_文件夹().WriteReadJson(_Path_Cfg, model, ref info, out string msgErr);
            _连接参数 = info;
            if (!rt)
            {
                On_Event_Log(rt, $"{Language_.Get语言("读参数文件故障")}");
            }

        }


        /// <summary>
        ///  异步
        /// </summary>
        /// <param name="组数">每组8个IO口</param>
        async void IO_关闭全部输出(int 组数)
        {
            await Task.Run(() =>
               {
                   //方法一
                   int a = _IO输出组数 * 8;
                   for (int i = 0; i < a; i++)
                   {
                       try
                       {
                           IO_设置输出口状态(i, 0);
                       }
                       catch (Exception)
                       {
                       }
                   }

                   //方法二
                   try
                   {
                       uint[] aa = new uint[0];
                       IO_设置多路输出(0, (ushort)(a - 1), aa, out string smgErr);
                   }
                   catch (Exception)
                   {
                   }
               });
        }


        #endregion

        #region 事件

        /// <summary>
        /// 日志信息
        /// </summary>
        public event Action<bool, string> Event_Log;
        void On_Event_Log(bool state, string Log)
        {
            Event_Log?.Invoke(state, $"{_控制器名称},{Log}");
        }

        public event Action<_连接状态_> Event_连接状态;
        void On_Event_连接状态(_连接状态_ state)
        {
            if (state == _连接状态_.已连接 && this._is匹配功能码)
            {

                //处理功能码
                if (!获取控制器信息(false))
                {
                    state = _连接状态_.功能码不匹配;
                }
            }


            _连接状态 = state;
            Event_连接状态?.Invoke(state);
            switch (state)
            {
                case _连接状态_.已连接:
                    On_Event_连接控制器();
                    break;
                case _连接状态_.未连接:
                    On_Event_关闭控制器();
                    break;
                case _连接状态_.功能码不匹配:
                    On_Event_关闭控制器();
                    break;
            }
        }

        /// <summary>
        /// 连接上控制器时发生
        /// </summary>
        public event Action Event_连接控制器;
        void On_Event_连接控制器()
        {
            IO_关闭全部输出(_IO输出组数);
            Event_连接控制器?.Invoke();
        }

        /// <summary>
        /// 关闭控制器时发生
        /// </summary>
        public event Action Event_关闭控制器;
        void On_Event_关闭控制器()
        {
            IO_关闭全部输出(_IO输出组数);
            Event_关闭控制器?.Invoke();
        }

        /// <summary>
        /// 其它线程
        /// </summary>
        public event Action Event_其它;
        void On_Event_其它()
        {
            Event_其它?.Invoke();
        }


        public event Action<byte[]> Event_IO输入;
        void On_Event_IO输入(byte[] state)
        {
            Event_IO输入?.Invoke(state);
        }

        public event Action<byte[]> Event_IO输出;
        void On_Event_IO输出(byte[] state)
        {
            Event_IO输出?.Invoke(state);
        }

        public event Action<bool[]> Event_IO输入B;
        void On_Event_IO输入(bool[] state)
        {
            Event_IO输入B?.Invoke(state);
        }

        public event Action<bool[]> Event_IO输出B;
        void On_Event_IO输出(bool[] state)
        {
            Event_IO输出B?.Invoke(state);
        }





        #endregion


        #region  正运动方法

        zmc zmcSys = new zmc();

        #region 控制器



        /// <summary>
        /// ZarPath:ZAR文件路径
        /// </summary>
        /// <param name="ZarPath"></param>
        /// <returns></returns>
        public bool 硬件更新(string ZarPath)
        {

            int rt = zmcSys.下载ZAR包文件(_handle, ZarPath, "");
            if (rt == 0)
            {
                return true;
            }
            else
            {
                return false;
            }


        }


        public bool IP设置(string ip, out string msgErr)
        {
            bool rt = true;
            msgErr = string.Empty;
            try
            {
                if (_连接状态 != 0)
                {
                    msgErr = "控制器未连接";
                    rt = false;
                }
                else
                {
                    int a = zmcaux.ZAux_SetIp(_handle, ip);
                    //if (a==0)
                    //{
                    //    msgErr = "未设置成功";
                    //    rt = false;
                    //}

                }
            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }
            return rt;
        }

        public int 连接_网口(string ip)
        {
            if (_连接状态 == _连接状态_.连接中 || _连接状态 == _连接状态_.已连接)
            {
                关闭控制器链接(out string msgErr);
            }

            int rt = -1;
            try
            {
                IntPtr hwnd = IntPtr.Zero;
                On_Event_连接状态(_连接状态_.连接中);
                //  Config.网口ip = ip;
                rt = zmcaux.ZAux_OpenEth(ip, out hwnd);


                if (rt == 0)
                {

                    _handle = hwnd;
                    On_Event_连接状态(_连接状态_.已连接);
                }
                else
                {
                    rt = -1;
                    zmcaux.ZAux_Close(_handle);
                    On_Event_连接状态(_连接状态_.未连接);
                }
            }
            catch (Exception ex)
            {
                if (_Is第一次)
                {
                    _Is第一次 = false;
                    On_Event_Log(false, ex.Message);
                }
                On_Event_连接状态(_连接状态_.未连接);
            }
            return rt;
        }
        bool _Is第一次 = true;
        public int 连接_串口(uint 串口号)
        {
            if (_连接状态 == _连接状态_.连接中 || _连接状态 == _连接状态_.已连接)
            {
                关闭控制器链接(out string msgErr);
            }
            int rt = -1;
            try
            {
                IntPtr hwnd = IntPtr.Zero;
                On_Event_连接状态(_连接状态_.连接中);
                rt = zmcaux.ZAux_OpenCom(串口号, out hwnd);
                if (rt == 0)
                {
                    // this.Config.连接参数.Com = 串口号;
                    _handle = hwnd;
                    On_Event_连接状态(_连接状态_.已连接);
                }
                else
                {
                    rt = -1;
                    zmcaux.ZAux_Close(_handle);
                    On_Event_连接状态(_连接状态_.未连接);
                }
            }
            catch (Exception ex)
            {
                if (_Is第一次)
                {
                    _Is第一次 = false;
                    On_Event_Log(false, ex.Message);
                }
                rt = -1;
                On_Event_连接状态(_连接状态_.未连接);

            }
            return rt;
        }

        /// <summary>
        /// 自动检测系统中所有串口
        /// </summary>
        /// <returns></returns>
        public int 连接_串口(_串口搜寻_ 自动寻找串口)
        {

            uint comPort = _连接参数.串口.串口号;
            int rt = 连接_串口(comPort);

            //下面自动寻找匹配的串口
            if (rt != 0 && 自动寻找串口 == _串口搜寻_.自动搜寻)
            {
                string[] com = new qfmain.SerialPort_方法().Get_串口名称();
                List<string> lstWork = new List<string>();
                lstWork.Add("处理COM");
                lstWork.Add("连接串口");

                foreach (var sCom in com)
                {
                    if (rt == 0)
                    {
                        break;
                    }

                    foreach (var s in lstWork)
                    {
                        if (s == "处理COM")
                        {
                            string comStr = new qfmain.文本().替换(sCom, "COM", "");
                            comStr = new qfmain.文本().替换(comStr, "com", "");
                            comPort = uint.Parse(comStr);

                        }
                        else if (s == "连接串口")
                        {
                            rt = 连接_串口(comPort);
                            if (rt == 0)
                            {
                                _连接参数.串口.串口号 = comPort;
                                读参数(0);
                                break;
                            }

                        }

                    }
                }
            }

            return rt;
        }






        /// <summary>
        /// 会触发连接失败事件
        /// </summary>
        /// <returns></returns>
        public bool 关闭控制器链接()
        {
            if (_连接状态 == _连接状态_.未连接 || _连接状态 == _连接状态_.功能码不匹配)
            {
                return true;
            }

            On_Event_连接状态(_连接状态_.未连接);
            try
            {
                return 关闭控制器链接(out string msgErr);
            }
            catch (Exception)
            {
                return false;
            }
        }


        /// <summary>
        /// 不触发事件
        /// </summary>
        /// <returns></returns>
        bool 关闭控制器链接(out string msgErr)
        {
            bool rt = true;
            msgErr = string.Empty;
            try
            {
                if (_handle != IntPtr.Zero)
                {
                    int a = zmcaux.ZAux_Close(_handle);
                }
            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }
            return rt;
        }




        #endregion


        #region IO


        public int IO_设置输出口状态(int 输出口, bool 状态)
        {
            uint a = 状态 ? (uint)1 : 0;
            return IO_设置输出口状态(输出口, a);
        }

        public void IO_设置输出口状态_脉冲(int 输出口, uint 输出脉宽)
        {
            IO_设置输出口状态(输出口, 1);
            if (输出脉宽 > 0)
            {
                Thread.Sleep((int)输出脉宽);
            }
            IO_设置输出口状态(输出口, 0);
        }



        /// <summary>
        ///  快速读取多个输入状态 ,申明 byte[] inBf =new byte[8]
        /// </summary>
        /// <param name="起始输入口"></param>
        /// <param name="输入口数量"></param>
        /// <param name="inBf"></param>
        /// <returns></returns>
        public int IO_快速读取多个输入口状态(int 起始端口, int 结束端口, out byte[] inBff)
        {
            inBff = new byte[8];
            int a = zmcaux.ZAux_GetModbusIn(_handle, 起始端口, 结束端口, inBff);

            return a;
        }

        public int IO_读取多个输入信号(int 起始端口, int 结束端口, int[] infb)
        {
            return zmcaux.ZAux_Direct_GetInMulti(_handle, 起始端口, 结束端口, infb);
        }

        /// <summary>
        /// 设置反转状态:0/1
        /// </summary>
        /// <param name="输入端口"></param>
        /// <param name="设置反转状态"></param>
        /// <returns></returns>
        public int IO_设置输入状态反转(int 输入端口, int 设置反转状态)
        {
            return zmcaux.ZAux_Direct_SetInvertIn(_handle, 输入端口, 设置反转状态);
        }

        /// <summary>
        /// 反转状态值 0/1
        /// </summary>
        /// <param name="输入端口"></param>
        /// <param name="反转状态"></param>
        /// <returns></returns>
        public int IO_读取输入反转状态(int 输入端口, ref int 反转状态)
        {
            int value = 0;
            int rt = zmcaux.ZAux_Direct_GetInvertIn(_handle, 输入端口, ref value);
            反转状态 = value;
            return rt;
        }

        public int IO_读输入口状态_单个(int 输入口, ref uint 输入口状态)
        {
            return zmcaux.ZAux_Direct_GetIn(_handle, 输入口, ref 输入口状态);
        }



        /// <summary>
        ///  快速读取多个输出状态 ,申明 byte[] inBf =new byte[8]
        /// </summary>
        /// <param name="起始端口"></param>
        /// <param name="端口数量"></param>
        /// <param name="inBf"></param>
        /// <returns></returns>
        public int IO_快速读取多个输出口状态(int 起始端口, int 结束端口, out byte[] inBf)
        {
            inBf = new byte[8];
            return zmcaux.ZAux_GetModbusOut(_handle, 起始端口, 结束端口, inBf);
        }

        public bool IO_设置多路输出(ushort 起始端口, ushort 结束端口, uint[] inBf, out string msg)
        {
            bool rt = true;
            try
            {
                msg = string.Empty;
                int a = zmcaux.ZAux_Direct_SetOutMulti(_handle, 起始端口, 结束端口, inBf);
                rt = a == 0 ? true : false;
                msg = a.ToString();
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                rt = false;
            }

            return rt;
        }

        public int IO_读取多路输出(ushort 起始端口, ushort 结束端口, uint[] 输出口状态)
        {
            return zmcaux.ZAux_Direct_GetOutMulti(_handle, 起始端口, 结束端口, 输出口状态);
        }

        public int IO_读取输出口状态_单个(int 输出口, ref uint 输出口状态)
        {
            return zmcaux.ZAux_Direct_GetOp(_handle, 输出口, ref 输出口状态);
        }


        /// <summary>
        /// <returns>-9999:未连接</returns>
        public int IO_设置输出口状态(int 输出口, uint 输出口状态)
        {
            if (_连接状态 != _连接状态_.已连接)
            {
                return -9999;
            }
            return zmcaux.ZAux_Direct_SetOp(_handle, 输出口, 输出口状态);

        }



        #endregion


        #region Modbus



        public int Modbus_读取MODBUS_STRING寄存器(ushort 起始地址, ushort 地址数量, StringBuilder value)
        {
            return zmcaux.ZAux_Modbus_Get4x_String(_handle, 起始地址, 地址数量, value);
        }

        /// <summary>
        /// ushort
        /// </summary>
        /// <param name="起始地址"></param>
        /// <param name="地址数量"></param>
        /// <param name="Str"></param>
        /// <returns></returns>
        public int Modbus_读取MODBUS_REG寄存器(ushort 起始地址, ushort 地址数量, ushort[] value)
        {
            return zmcaux.ZAux_Modbus_Get4x(_handle, 起始地址, 地址数量, value);
        }


        /// <summary>
        /// ushort
        /// </summary>
        /// <param name="起始地址"></param>
        /// <param name="地址数量"></param>
        /// <param name="Str"></param>
        /// <returns></returns>
        public int Modbus_读取MODBUS_REG寄存器(ushort 起始地址, ushort 地址数量, short[] value)
        {
            return zmcaux.ZAux_Modbus_Get4x(_handle, 起始地址, 地址数量, value);
        }

        public int Modbus_读取MODBUS_LONG寄存器(ushort 起始地址, ushort 地址数量, int[] value)
        {
            return zmcaux.ZAux_Modbus_Get4x_Long(_handle, 起始地址, 地址数量, value);
        }

        public int Modbus_读取MOBUS_BIT位寄存器(ushort 起始地址, ushort 地址数量, byte[] value)
        {
            return zmcaux.ZAux_Modbus_Get0x(_handle, 起始地址, 地址数量, value);
        }

        public int Modbus_读取MODBUS_IEEE寄存器_float(ushort 起始地址, ushort 地址数量, float[] value)
        {
            return zmcaux.ZAux_Modbus_Get4x_Float(_handle, 起始地址, 地址数量, value);
        }





        public int Modbus_设置MODBUS_BIT位寄存器(ushort 起始地址, ushort 地址数量, byte[] value)
        {
            return zmcaux.ZAux_Modbus_Set0x(_handle, 起始地址, 地址数量, value);


        }


        public int Modbus_设置MODBUS_IEEE寄存器_float(ushort 起始地址, ushort 地址数量, float[] value)
        {
            return zmcaux.ZAux_Modbus_Set4x_Float(_handle, 起始地址, 地址数量, value);


        }


        public int Modbus_设置MODBUS_LONG寄存器(ushort 起始地址, ushort 地址数量, int[] value)
        {
            return zmcaux.ZAux_Modbus_Set4x_Long(_handle, 起始地址, 地址数量, value);


        }

        /// <summary>
        /// ushort
        /// </summary>
        /// <param name="起始地址"></param>
        /// <param name="地址数量"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public int Modbus_设置MODBUS_REG字寄存器(ushort 起始地址, ushort 地址数量, ushort[] value)
        {
            return zmcaux.ZAux_Modbus_Set4x(_handle, 起始地址, 地址数量, value);


        }

        /// <summary>
        /// ushort
        /// </summary>
        /// <param name="起始地址"></param>
        /// <param name="地址数量"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public int Modbus_设置MODBUS_REG字寄存器(ushort 起始地址, ushort 地址数量, short[] value)
        {
            return zmcaux.ZAux_Modbus_Set4x(_handle, 起始地址, 地址数量, value);


        }



        public int Modbus_设置MODBUS_STRING寄存器(ushort 起始地址, ushort 地址数量, string value)
        {
            return zmcaux.ZAux_Modbus_Set4x_String(_handle, 起始地址, 地址数量, value);
        }


        #endregion


        #region VR


        public int VR_设置float寄存器(ushort 起始地址, ushort 地址数量, float[] value)
        {
            return zmcaux.ZAux_Direct_SetVrf(_handle, 起始地址, 地址数量, value);
        }

        public int VR_读取float寄存器(ushort 起始地址, ushort 地址数量, float[] value)
        {
            return zmcaux.ZAux_Direct_GetVrf(_handle, 起始地址, 地址数量, value);
        }

        public int VR_INT寄存器_读取(ushort 起始地址, ushort 地址数量, int[] value)
        {
            return zmcaux.ZAux_Direct_GetVrInt(_handle, 起始地址, 地址数量, value);
        }



        #endregion


        #region axis




        public int axis_读取_编码轴反馈位置坐标(int 轴号, ref float 坐标)
        {
            return zmcaux.ZAux_Direct_GetMpos(this._handle, 轴号, ref 坐标);
        }
        public int axis_读取_轴命令位置坐标(int 轴号, ref float 坐标)
        {
            return zmcaux.ZAux_Direct_GetDpos(this._handle, 轴号, ref 坐标);
        }

        public int axis_读取_快速读取多个轴的反馈位置(int 轴号, out float[] 坐标)
        {
            float[] a = new float[0];
            int rt = zmcaux.ZAux_GetModbusMpos(_handle, 轴号, a);
            坐标 = a;

            return rt;
        }
        public int axis_读取_快速读取多个轴的命令位置(int 轴号, out float[] 坐标)
        {
            float[] a = new float[0];
            int rt = zmcaux.ZAux_GetModbusDpos(_handle, 轴号, a);
            坐标 = a;

            return rt;
        }


        /// <summary>
        /// 状态:运动状态反馈值 0-运动中 -1 停止
        /// </summary>
        /// <param name="轴号"></param>
        /// <param name="状态"></param>
        /// <returns></returns>
        public int axis_读取_轴运动状态(int 轴号, ref int 状态)
        {
            return zmcaux.ZAux_Direct_GetIfIdle(this._handle, 轴号, ref 状态);
        }







        public int axis_设置_轴脉冲当量(int 轴号, float 脉冲当量)
        {
            return zmcaux.ZAux_Direct_SetUnits(_handle, 轴号, 脉冲当量);
        }
        public int axis_读取_轴脉冲当量(int 轴号, ref float 脉冲当量)
        {
            return zmcaux.ZAux_Direct_GetUnits(_handle, 轴号, ref 脉冲当量);
        }

        public int axis_设置_轴速度(int 轴号, float 速度)
        {
            return zmcaux.ZAux_Direct_SetSpeed(_handle, 轴号, 速度);
        }
        public int axis_读取_轴速度(int 轴号, ref float 速度)
        {
            return zmcaux.ZAux_Direct_GetSpeed(_handle, 轴号, ref 速度);
        }

        public int axis_设置_加速度(int 轴号, float 速度)
        {
            return zmcaux.ZAux_Direct_SetAccel(_handle, 轴号, 速度);
        }
        public int axis_读取_加速度(int 轴号, ref float 速度)
        {
            return zmcaux.ZAux_Direct_GetAccel(_handle, 轴号, ref 速度);
        }

        public int axis_设置_减速度(int 轴号, float 速度)
        {
            return zmcaux.ZAux_Direct_SetDecel(_handle, 轴号, 速度);
        }
        public int axis_读取_减速度(int 轴号, ref float 速度)
        {
            return zmcaux.ZAux_Direct_GetDecel(_handle, 轴号, ref 速度);
        }

        public int axis_设置_轴异常快速减速度(int 轴号, float 速度)
        {
            return zmcaux.ZAux_Direct_SetFastDec(_handle, 轴号, 速度);
        }
        public int axis_读取_轴异常快速减速度(int 轴号, ref float 速度)
        {
            return zmcaux.ZAux_Direct_GetFastDec(_handle, 轴号, ref 速度);
        }

        public int axis_设置_轴起始速度(int 轴号, float 速度)
        {
            return zmcaux.ZAux_Direct_SetLspeed(_handle, 轴号, 速度);
        }
        public int axis_读取_轴起始速度(int 轴号, ref float 速度)
        {
            return zmcaux.ZAux_Direct_GetLspeed(_handle, 轴号, ref 速度);
        }

        public int axis_设置_回零爬行速度(int 轴号, float 速度)
        {
            return zmcaux.ZAux_Direct_SetCreep(_handle, 轴号, 速度);
        }
        public int axis_读取_回零爬行速度(int 轴号, ref float 速度)
        {
            return zmcaux.ZAux_Direct_GetCreep(_handle, 轴号, ref 速度);
        }

        public int axis_设置_回零反找等待时间(int 轴号, int 反找时间)
        {
            return zmcaux.ZAux_Direct_SetHomeWait(_handle, 轴号, 反找时间);
        }
        public int axis_读取_回零反找等待时间(int 轴号, ref int 速度)
        {
            return zmcaux.ZAux_Direct_GetHomeWait(_handle, 轴号, ref 速度);
        }

        /// <summary>
        /// 使能状态设定值 0-关闭 1- 打开
        /// </summary>
        /// <param name="轴号"></param>
        /// <param name="使能状态"></param>
        /// <returns></returns>
        public int axis_设置_轴使能(int 轴号, int 使能状态)
        {
            return zmcaux.ZAux_Direct_SetAxisEnable(_handle, 轴号, 使能状态);
        }
        /// <summary>
        /// 使能状态设定值 0-关闭 1- 打开
        /// </summary>
        /// <param name="轴号"></param>
        /// <param name="使能状态"></param>
        /// <returns></returns>
        public int axis_读取_轴使能状态(int 轴号, ref int 使能状态)
        {
            return zmcaux.ZAux_Direct_GetAxisEnable(_handle, 轴号, ref 使能状态);
        }

        /// <summary>
        /// Atype:...轴类型设置值,一般为1:脉冲方式步进或伺服
        /// <para>1:脉冲+方向</para>
        /// </summary>
        /// <param name="轴号"></param>
        /// <param name="轴类型"></param>
        /// <returns></returns>
        public int axis_设置_轴类型(int 轴号, int 轴类型)
        {
            return zmcaux.ZAux_Direct_SetAtype(_handle, 轴号, 轴类型);
        }
        /// <summary>
        /// Atype:...轴类型设置值,一般为1:脉冲方式步进或伺服
        /// <para>1:脉冲+方向</para>
        /// </summary>
        /// <param name="轴号"></param>
        /// <param name="轴类型"></param>
        /// <returns></returns>
        public int axis_读取_轴类型(int 轴号, ref int 轴类型)
        {
            return zmcaux.ZAux_Direct_GetAtype(_handle, 轴号, ref 轴类型);
        }

        /// <summary>
        /// InvertStep:脉冲模式设定值 0-3脉冲+方向 4-7双脉冲
        /// <para>参考值: 0/2 或1/3 ,一般为1/3</para>
        /// </summary>
        /// <param name="轴号"></param>
        /// <param name="输出脉冲模式"></param>
        /// <returns></returns>
        public int axis_设置_轴脉冲输出模式_即轴方向(int 轴号, int 输出脉冲模式)
        {
            return zmcaux.ZAux_Direct_SetInvertStep(_handle, 轴号, 输出脉冲模式);
        }
        /// <summary>
        /// InvertStep:脉冲模式设定值 0-3脉冲+方向 4-7双脉冲
        /// <para>参考值: 0/2 或1/3 ,一般为1/3</para>
        /// </summary>
        /// <param name="轴号"></param>
        /// <param name="输出脉冲模式"></param>
        /// <returns></returns>
        public int axis_读取_轴脉冲输出模式_即轴方向(int 轴号, ref int 输出脉冲模式)
        {
            return zmcaux.ZAux_Direct_GetInvertStep(_handle, 轴号, ref 输出脉冲模式);
        }

        public int axis_设置_轴原点硬限位输入口(int 轴号, int 输入端口)
        {
            return zmcaux.ZAux_Direct_SetDatumIn(_handle, 轴号, 输入端口);
        }
        public int axis_读取_轴原点硬限位输入口(int 轴号, ref int 输入端口)
        {
            return zmcaux.ZAux_Direct_GetDatumIn(_handle, 轴号, ref 输入端口);
        }

        public int axis_设置_轴正向硬限位输入口(int 轴号, int 输入端口)
        {
            return zmcaux.ZAux_Direct_SetFwdIn(_handle, 轴号, 输入端口);
        }
        public int axis_读取_轴正向硬限位输入口(int 轴号, ref int 输入端口)
        {
            return zmcaux.ZAux_Direct_GetFwdIn(_handle, 轴号, ref 输入端口);
        }

        public int axis_设置_轴负向硬限位输入口(int 轴号, int 输入端口)
        {
            return zmcaux.ZAux_Direct_SetRevIn(_handle, 轴号, 输入端口);
        }
        public int axis_读取_轴负向硬限位输入口(int 轴号, ref int 输入端口)
        {
            return zmcaux.ZAux_Direct_GetRevIn(_handle, 轴号, ref 输入端口);
        }

        public int axis_设置_轴急停报警信号输入口(int 轴号, int 输入端口)
        {
            return zmcaux.ZAux_Direct_SetAlmIn(_handle, 轴号, 输入端口);
        }
        public int axis_读取_轴急停报警信号输入口(int 轴号, ref int 输入端口)
        {
            return zmcaux.ZAux_Direct_GetAlmIn(_handle, 轴号, ref 输入端口);
        }

        public int axis_单轴相对运动(int 轴号, float value)
        {
            return zmcaux.ZAux_Direct_Single_Move(_handle, 轴号, value);
        }

        public int axis_单轴绝对运动(int 轴号, float 移动坐标)
        {
            return zmcaux.ZAux_Direct_Single_MoveAbs(_handle, 轴号, 移动坐标);
        }


        /// <summary>
        /// 动方向 1正向 -1负向
        /// </summary>
        /// <param name="轴号"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public int axis_单轴连续运动(int 轴号, int value)
        {
            return zmcaux.ZAux_Direct_Single_Vmove(_handle, 轴号, value);
        }

        /// <summary>
        /// 停止模式  0（缺省）取消当前运动 1-取消缓冲的运动 2-取消当前运动和缓冲运动 3-立即中断脉冲发送
        /// </summary>
        /// <param name="轴号"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public int axis_单轴运动停止(int 轴号, int 停止模式)
        {
            return zmcaux.ZAux_Direct_Single_Cancel(_handle, 轴号, 停止模式);
        }

        /// <summary>
        /// 回零模式+10表示碰到限位后反找,不会停止
        /// <para>回零模式0:清除所有轴的错误状态。</para>
        /// <para>回零模式,详情见说明书,可以选择回零的方向</para>
        /// </summary>
        /// <param name="轴号"></param>
        /// <param name="回零模式"></param>
        /// <returns></returns>
        public int axis_单轴回零_Datum(int 轴号, int 回零模式)
        {
            return zmcaux.ZAux_Direct_Single_Datum(_handle, 轴号, 回零模式);
        }
        /// <summary>
        ///  0:回零异常,1:回零正常
        /// </summary>
        /// <param name="轴号"></param>
        /// <param name="回零模式"></param>
        /// <returns></returns>
        public int axis_读取_轴回零状态(int 轴号, ref uint 回零模式)
        {
            return zmcaux.ZAux_Direct_GetHomeStatus(_handle, 轴号, ref 回零模式);
        }


        #endregion

        #endregion


        #region 封装

        public void 初始化()
        {
            On_Event_连接状态(_连接状态_.未连接);
            new Thread(() => { 线程(); }) { IsBackground = true }.Start();
            IsInistiall = true;
        }
        bool IsInistiall = false;
        public void 释放()
        {
            isRun = false;
            if (!IsInistiall)
            {
                return;
            }

            关闭控制器链接(out string msgErr);
        }

        /// <summary>
        /// 封装
        /// </summary>
        /// <returns></returns>
        public bool 控制器连接()
        {
            读参数(1);
            int rt = -1;
            if (this._连接参数.连接类型 == _连接类型_.网口)
            {
                rt = this.连接_网口(this._连接参数.IP);
            }
            else if (this._连接参数.连接类型 == _连接类型_.串口)
            {
                rt = this.连接_串口(this._连接参数.串口.串口号搜寻);
            }

            return rt == 0 ? true : false;
        }


        #region 本地方法

        /// <summary>
        /// 线程启动状态
        /// </summary>
        bool isRun = true;


        /// <summary>
        /// 获取状态
        /// </summary>
        private void 线程()
        {
            List<string> lstWork = new List<string>();
            lstWork.Add("判断是否向下运行");
            lstWork.Add("判断是否需要初始化");
            lstWork.Add("事件处理");
            isRun = true;
            while (isRun)
            {
                Thread.Sleep(this._连接参数.线程周期);

                if (!isRun)
                {
                    break;
                }

                try
                {
                    bool rt = true;
                    foreach (var item in lstWork)
                    {
                        if (!rt)
                        {
                            break;
                        }
                        else if (item == "判断是否向下运行")
                        {

                            #region 判断是否向下运行

                            if (this._连接状态 == _连接状态_.连接中)
                            {
                                rt = false;
                            }
                            else if (this._连接状态 == _连接状态_.功能码不匹配)
                            {
                                rt = false;
                                isRun = false;
                            }

                            #endregion

                        }
                        else if (item == "判断是否需要初始化")
                        {

                            #region 判断是否需要初始化

                            if (this._连接状态 == _连接状态_.未连接)
                            {
                                控制器连接();
                                Thread.Sleep(200);
                                if (this._连接状态 == _连接状态_.未连接)
                                {
                                    rt = false;
                                    break;
                                }
                            }

                            #endregion

                        }
                        else if (item == "事件处理")
                        {
                            #region 事件处理

                            if (this._连接状态 == _连接状态_.已连接)
                            {
                                uint I_0 = 0;
                                if (IO_读取输出口状态_单个(0, ref I_0) != 0)
                                {
                                    On_Event_连接状态(_连接状态_.未连接);
                                    关闭控制器链接(out string msgErr);
                                    rt = false;
                                    continue;
                                }

                                读IO_输入();
                                读IO_输出();
                                On_Event_其它();

                            }

                            #endregion
                        }

                    }

                }
                catch (Exception)
                {
                    On_Event_连接状态(_连接状态_.未连接);
                    关闭控制器链接(out string msgErr);
                }
            }

        }



        /// <summary>
        /// 获取控制器信息 
        /// </summary>
        private bool 获取控制器信息(bool 产生事件 = true)
        {
            bool rt = true;
            if (this._is匹配功能码)
            {
                StringBuilder stringBuilder = new StringBuilder(255);
                this.Modbus_读取MODBUS_STRING寄存器(300, 50, stringBuilder);
                this._控制器参数.功能码 = stringBuilder.ToString();

                stringBuilder.Clear();
                this.Modbus_读取MODBUS_STRING寄存器(350, 20, stringBuilder);
                this._控制器参数.Sn = stringBuilder.ToString();

                stringBuilder.Clear();
                this.Modbus_读取MODBUS_STRING寄存器(370, 20, stringBuilder);
                this._控制器参数.软件型号 = stringBuilder.ToString();

                stringBuilder.Clear();
                this.Modbus_读取MODBUS_STRING寄存器(390, 20, stringBuilder);
                this._控制器参数.硬件型号 = stringBuilder.ToString();

                stringBuilder.Clear();

                rt = this.判断功能ID是否正确(产生事件);
            }
            return rt;
        }

        private bool 判断功能ID是否正确(bool 产生事件 = true)
        {

            if (!this._is匹配功能码)
            {
                return true;
            }

            else if (this._功能码 == this._控制器参数.功能码)
            {
                return true;
            }

            释放();
            if (产生事件)
            {
                On_Event_连接状态(_连接状态_.功能码不匹配);
            }
            return false;
        }



        #endregion


        #region IO读取


        List<bool> List_IO_输入 = new List<bool>();
        List<bool> List_IO_输出 = new List<bool>();

        bool 读IO_输入()
        {
            List_IO_输入.Clear();
            int End_IN = _IO输入组数 * 8 - 1;
            int a = this.IO_快速读取多个输入口状态(0, End_IN, out byte[] inBeff);

            for (int i = 0; i < inBeff.Length; i++)
            {

                byte c = inBeff[i];
                for (int ia = 0; ia < 8; ia++)
                {
                    new qfmain.进制().取指定位状态_十进制(c, ia, 7, out bool Input, out string msgErr);
                    List_IO_输入.Add(Input);
                }

            }

            On_Event_IO输入(inBeff);
            On_Event_IO输入(List_IO_输入.ToArray());

            return a == 0 ? true : false;
        }

        bool 读IO_输出()
        {
            List_IO_输出.Clear();
            int End_OUT = _IO输出组数 * 8 - 1;
            int a = this.IO_快速读取多个输出口状态(0, End_OUT, out byte[] outBeff);

            for (int i = 0; i < outBeff.Length; i++)
            {
                byte c = outBeff[i];
                for (int ia = 0; ia < 8; ia++)
                {
                    new qfmain.进制().取指定位状态_十进制(c, ia, 7, out bool Output, out string msgErr);
                    List_IO_输出.Add(Output);
                }

            }

            On_Event_IO输出(outBeff);
            On_Event_IO输出(List_IO_输出.ToArray());
            return a == 0 ? true : false;
        }

        #endregion


        #endregion


        #region Err

        public bool Err_未连接(out string msgErr, bool 显示日志 = false)
        {
            msgErr = string.Empty;

            if (_连接状态 != _连接状态_.已连接)
            {
                msgErr = $"{_控制器名称},{Language_.Get语言("未连接")}";
                if (显示日志)
                {
                    On_Event_Log(false, msgErr);
                }
                return false;
            }
            return true;
        }

        public bool Err_Dll是否存在(out string msgErr)
        {
            msgErr = string.Empty;
            bool rt = true;
            string zauxdll = Environment.CurrentDirectory + "\\zauxdll.dll";
            string zmotion = Environment.CurrentDirectory + "\\zmotion.dll";

            if (!new qfmain.文件_文件夹().文件_是否存在(zauxdll) ||
                !new qfmain.文件_文件夹().文件_是否存在(zmotion))
            {
                rt = false;
                msgErr = Language_.Get语言("Z控制器相关的文件丢失");
            }

            return rt;
        }

        #endregion

    }
}
