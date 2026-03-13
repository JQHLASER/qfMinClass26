using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace qfWork
{
    /// <summary>
    /// 高川 IO 卡
    /// <para>scio32</para>
    /// </summary>
    public class GaoCH_IO_scio32
    {


        //设备信息结构
        public struct TDevInfo
        {
            public ushort address;              // 在上位机系统中分配的地址序号,如USB
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string idStr;               // 识别字符串 
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string description;      // 描述符
            public ushort ID;                   // 板上的ID(未用)
        };

        public struct CALENDAR
        {
            public ushort w_year;
            public byte month;
            public byte day;
            public byte week;
            public byte hour;
            public byte min;
            public byte sec;
        };



        #region Err 


        public enum _em_Err_
        {
            指令执行成功 = 0,
            指令执行失败 = -1,
            指令参数错误 = -2,
            不支持 = -3,
            无效句柄 = -4,



            未知错误 = -9999,
        }


        public string ErrToMsg(_em_Err_ err)
        {
            switch (err)
            {
                case _em_Err_.指令执行成功:
                    return Language_.Get语言("指令执行成功");
                case _em_Err_.指令执行失败:
                    return Language_.Get语言("指令执行失败");
                case _em_Err_.指令参数错误:
                    return Language_.Get语言("指令参数错误");
                case _em_Err_.不支持:
                    return Language_.Get语言("不支持");
                case _em_Err_.无效句柄:
                    return Language_.Get语言("无效句柄");
                default:
                    return Language_.Get语言("未知错误");
            }
        }

        #endregion



        /// <summary>
        /// <para>count : 搜寻到的搜寻到的设备的数目</para>
        /// <para>TDevInfo : 搜导到的设备</para>
        /// </summary> 
        public (bool s, string m, ushort count, TDevInfo cfg) 板卡搜寻()
        {
            var dev_info_list = new TDevInfo();
            ushort dev_no = 0;
            string msg = "";
            bool rt = true;
            try
            {
                var rtn = (_em_Err_)scio32.NIO_Search(ref dev_no, ref dev_info_list);
                msg = ErrToMsg(rtn);
                rt = rtn == _em_Err_.指令执行成功 ? true : false;
            }
            catch (Exception ex)
            {
                rt = false;
                msg = ex.ToString();
            }
            return (rt, msg, dev_no, dev_info_list);
        }

        /// <summary>
        /// <para>cfg : 搜导到的设备</para>
        /// <para>devHandle : 设备句柄</para>
        /// </summary> 
        public (bool s, string m, IntPtr devHandle) 连接(TDevInfo cfg)
        {
            bool rt = true;
            string msg = string.Empty;
            IntPtr devHandle = IntPtr.Zero;
            try
            {

                var rtn = (_em_Err_)scio32.NIO_OpenByID(cfg.idStr, ref devHandle);
                msg = ErrToMsg(rtn);
                rt = rtn == _em_Err_.指令执行成功 ? true : false;
            }
            catch (Exception ex)
            {
                rt = false;
                msg = ex.ToString();
            }
            return (rt, msg, devHandle);
        }

        /// <summary>
        /// devHandle : 设备句柄
        /// </summary> 
        public (bool s, string m) 关闭(IntPtr devHandle)
        {
            bool rt = true;
            string msg = string.Empty;

            try
            {
                var rtn = (_em_Err_)scio32.NIO_Close(devHandle);
                msg = ErrToMsg(rtn);
                rt = rtn == _em_Err_.指令执行成功 ? true : false;
            }
            catch (Exception ex)
            {
                rt = false;
                msg = ex.ToString();
            }
            return (rt, msg);
        }


        #region 输出

        /// <summary>
        /// devHandle : 设备句柄
        /// <para>起始端口号 : 取值范围(0-1)</para>
        /// <para>要获取端口数量: 取值范围(1-2)</para>
        /// <para>获取的值(value): 用于存储端口状态值缓冲(每个端口16个点位), 每个点位:  1 代表触发; 0 代表未触发</para>
        /// </summary> 
        public (bool s, string m, ushort value) 获取_输出状态(IntPtr devHandle, ushort 起始端口号, ushort 要获取端口数量)
        {
            bool rt = true;
            string msg = string.Empty;
            ushort value = 0;
            try
            {
                var rtn = (_em_Err_)scio32.NIO_GetDO(devHandle, 起始端口号, 要获取端口数量, ref value);
                msg = ErrToMsg(rtn);
                rt = rtn == _em_Err_.指令执行成功 ? true : false;
            }
            catch (Exception ex)
            {
                rt = false;
                msg = ex.ToString();
            }
            return (rt, msg, value);
        }

        /// <summary>
        /// devHandle : 设备句柄
        /// <para>起始端口号 : 取值范围(0-1)</para>
        /// <para>要设置端口数量: 取值范围(1-2) 1:前16位，2:后16位</para>
        /// <para>value: 要设置的端口状态值缓冲(每个端口16个点位), 每个点位:  1 代表触发; 0 代表未触发</para>
        /// </summary> 
        public (bool s, string m) 设置_输出(IntPtr devHandle, ushort 起始端口号, ushort 要设置端口数量, ushort value)
        {
            bool rt = true;
            string msg = string.Empty;
            try
            {
                var rtn = (_em_Err_)scio32.NIO_SetDO(devHandle, 起始端口号, 要设置端口数量, value);
                msg = ErrToMsg(rtn);
                rt = rtn == _em_Err_.指令执行成功 ? true : false;
            }
            catch (Exception ex)
            {
                rt = false;
                msg = ex.ToString();
            }
            return (rt, msg);
        }


        /// <summary>
        /// devHandle : 设备句柄
        /// <para>端口号 : 点位位置, 取值范围: 0-31</para> 
        /// <para>获取的值(value): 用于存储获取的点位状态  1 代表触发; 0 代表未触发</para>
        /// </summary> 
        public (bool s, string m, ushort value) 获取_输出状态(IntPtr devHandle, ushort 端口号)
        {
            bool rt = true;
            string msg = string.Empty;
            ushort value = 0;
            try
            {
                var rtn = (_em_Err_)scio32.NIO_GetDOBit(devHandle, 端口号, ref value);
                msg = ErrToMsg(rtn);
                rt = rtn == _em_Err_.指令执行成功 ? true : false;
            }
            catch (Exception ex)
            {
                rt = false;
                msg = ex.ToString();
            }
            return (rt, msg, value);
        }


        /// <summary>
        /// devHandle : 设备句柄
        /// <para>端口号 : 点位位置, 取值范围: 0-31</para> 
        /// <para>value: 要设置的值  1 代表触发; 0 代表未触发</para>
        /// </summary> 
        public (bool s, string m) 设置_输出(IntPtr devHandle, ushort 端口号, ushort value)
        {
            bool rt = true;
            string msg = string.Empty;

            try
            {
                var rtn = (_em_Err_)scio32.NIO_SetDOBit(devHandle, 端口号, value);
                msg = ErrToMsg(rtn);
                rt = rtn == _em_Err_.指令执行成功 ? true : false;
            }
            catch (Exception ex)
            {
                rt = false;
                msg = ex.ToString();
            }
            return (rt, msg);
        }


        #endregion


        #region 输入


        /// <summary>
        /// devHandle : 设备句柄
        /// <para>起始端口号 : 取值范围(0-1)</para>
        /// <para>要获取端口数量: 取值范围(1-2)</para>
        /// <para>获取的值(value): 用于存储端口状态值缓冲(每个端口16个点位), 每个点位:  1 代表触发; 0 代表未触发</para>
        /// </summary> 
        public (bool s, string m, ushort value) 获取_输入状态(IntPtr devHandle, ushort 起始端口号, ushort 要获取端口数量)
        {
            bool rt = true;
            string msg = string.Empty;
            ushort value = 0;
            try
            {
                var rtn = (_em_Err_)scio32.NIO_GetDI(devHandle, 起始端口号, 要获取端口数量, ref value);
                msg = ErrToMsg(rtn);
                rt = rtn == _em_Err_.指令执行成功 ? true : false;
            }
            catch (Exception ex)
            {
                rt = false;
                msg = ex.ToString();
            }
            return (rt, msg, value);
        }
        /// <summary>
        /// devHandle : 设备句柄
        /// <para>端口号 : 点位位置, 取值范围: 0-31</para> 
        /// <para>获取的值(value): 用于存储获取的点位状态  1 代表触发; 0 代表未触发</para>
        /// </summary> 
        public (bool s, string m, ushort value) 获取_输入状态(IntPtr devHandle, ushort 端口号)
        {
            bool rt = true;
            string msg = string.Empty;
            ushort value = 0;
            try
            {
                var rtn = (_em_Err_)scio32.NIO_GetDIBit(devHandle, 端口号, ref value);
                msg = ErrToMsg(rtn);
                rt = rtn == _em_Err_.指令执行成功 ? true : false;
            }
            catch (Exception ex)
            {
                rt = false;
                msg = ex.ToString();
            }
            return (rt, msg, value);
        }


        #endregion


        #region IO逻辑

        /// <summary>
        /// value : 用于存储IO逻辑状态,（1，正逻辑；0，负逻辑）
        /// </summary> 
        public (bool s, string m, ushort value) 获取_设备IO口逻辑(IntPtr devHandle)
        {
            bool rt = true;
            string msg = string.Empty;
            ushort value = 0;
            try
            {
                var rtn = (_em_Err_)scio32.NIO_GetIOLogic(devHandle, ref value);
                msg = ErrToMsg(rtn);
                rt = rtn == _em_Err_.指令执行成功 ? true : false;
            }
            catch (Exception ex)
            {
                rt = false;
                msg = ex.ToString();
            }
            return (rt, msg, value);
        }

        /// <summary>
        /// value : 要设置的IO逻辑状态（1，正逻辑；0，负逻辑）
        /// </summary> 
        public (bool s, string m) 设置_设备IO口逻辑(IntPtr devHandle, ushort value)
        {
            bool rt = true;
            string msg = string.Empty;

            try
            {
                var rtn = (_em_Err_)scio32.NIO_SetIOLogic(devHandle, value);
                msg = ErrToMsg(rtn);
                rt = rtn == _em_Err_.指令执行成功 ? true : false;
            }
            catch (Exception ex)
            {
                rt = false;
                msg = ex.ToString();
            }
            return (rt, msg);
        }


        #endregion


        #region 模拟量....该接口适用型号：USBIO32AD01

        /// <summary>
        /// <para>起始通道号 : 取值范围(0-3)</para>
        /// <para>通道数量 : 取值范围(1-4)</para>
        /// <para>value : 要设置的端口模拟量输出值缓冲</para>
        /// <para>该接口适用型号：USBIO32AD01</para>
        /// </summary> 
        public (bool s, string m) 设置_模拟量输出(IntPtr devHandle, ushort 起始通道号, ushort 通道数量, ushort value)
        {
            bool rt = true;
            string msg = string.Empty;
            try
            {
                var rtn = (_em_Err_)scio32.NIO_SetDAC(devHandle, 起始通道号, 通道数量, value);
                msg = ErrToMsg(rtn);
                rt = rtn == _em_Err_.指令执行成功 ? true : false;
            }
            catch (Exception ex)
            {
                rt = false;
                msg = ex.ToString();
            }
            return (rt, msg);
        }


        /// <summary>
        /// <para>起始通道号 : 取值范围(0-3)</para>
        /// <para>通道数量 : 取值范围(1-4)</para>
        /// <para>value : 用于存储模拟量输入值缓冲</para>
        /// <para>该接口适用型号：USBIO32AD01</para>
        /// </summary> 
        public (bool s, string m, ushort value) 获取_模拟量输入(IntPtr devHandle, ushort 起始通道号, ushort 通道数量)
        {
            bool rt = true;
            string msg = string.Empty;
            ushort value = 0;
            try
            {
                var rtn = (_em_Err_)scio32.NIO_GetADC(devHandle, 起始通道号, 通道数量, ref value);
                msg = ErrToMsg(rtn);
                rt = rtn == _em_Err_.指令执行成功 ? true : false;
            }
            catch (Exception ex)
            {
                rt = false;
                msg = ex.ToString();
            }
            return (rt, msg, value);
        }



        #endregion


        #region 其它

        /// <summary>
        /// date : 用于存储日期时间的缓冲
        /// </summary> 
        public (bool s, string m, CALENDAR date) 获取_设备时间及日期(IntPtr devHandle)
        {
            bool rt = true;
            string msg = string.Empty;
            CALENDAR value = new CALENDAR();
            try
            {
                var rtn = (_em_Err_)scio32.NIO_GetTime(devHandle, ref value);
                msg = ErrToMsg(rtn);
                rt = rtn == _em_Err_.指令执行成功 ? true : false;
            }
            catch (Exception ex)
            {
                rt = false;
                msg = ex.ToString();
            }
            return (rt, msg, value);
        }

        /// <summary>
        /// idStr :  新的设备名称, 最长16字节包括末尾'\0'字符
        /// </summary> 
        public (bool s, string m) 设置_SIO设备在系统中名称(IntPtr devHandle, string idStr)
        {
            bool rt = true;
            string msg = string.Empty;
            try
            {
                var rtn = (_em_Err_)scio32.NIO_WriteID(devHandle, idStr);
                msg = ErrToMsg(rtn);
                rt = rtn == _em_Err_.指令执行成功 ? true : false;
            }
            catch (Exception ex)
            {
                rt = false;
                msg = ex.ToString();
            }
            return (rt, msg);
        }

        /// <summary>
        /// idStr :  用于存放设备名称缓冲区, 最长16字节
        /// </summary> 
        public (bool s, string m, byte idStr) 获取_SIO设备名称(IntPtr devHandle)
        {
            bool rt = true;
            string msg = string.Empty;
            byte idStr = 0;
            try
            {
                var rtn = (_em_Err_)scio32.NIO_ReadID(devHandle, ref idStr);
                msg = ErrToMsg(rtn);
                rt = rtn == _em_Err_.指令执行成功 ? true : false;
            }
            catch (Exception ex)
            {
                rt = false;
                msg = ex.ToString();
            }
            return (rt, msg, idStr);
        }



        /// <summary>
        /// （用于Modbus通讯的设备ID）
        /// <para>devId : 新的设备序号 </para>
        /// </summary> 
        public (bool s, string m) 设置_SIO设备ID序号(IntPtr devHandle, byte[] devId)
        {
            bool rt = true;
            string msg = string.Empty;
            try
            {
                var rtn = (_em_Err_)scio32.NIO_WriteDevID(devHandle, devId);
                msg = ErrToMsg(rtn);
                rt = rtn == _em_Err_.指令执行成功 ? true : false;
            }
            catch (Exception ex)
            {
                rt = false;
                msg = ex.ToString();
            }
            return (rt, msg);
        }

        /// <summary>
        /// （用于Modbus通讯的设备ID）
        /// <para>productId : 用于存放设备ID序号 </para>
        /// </summary>
        public (bool s, string m, byte devId) 获取_SIO设备ID序号(IntPtr devHandle)
        {
            bool rt = true;
            string msg = string.Empty;
            byte devId = 0;
            try
            {
                var rtn = (_em_Err_)scio32.NIO_ReadDevID(devHandle, ref devId);
                msg = ErrToMsg(rtn);
                rt = rtn == _em_Err_.指令执行成功 ? true : false;
            }
            catch (Exception ex)
            {
                rt = false;
                msg = ex.ToString();
            }
            return (rt, msg, devId);
        }



        /// <summary>
        /// productId :  用于存放设备ID的缓冲(12字节共96位)
        /// </summary> 
        public (bool s, string m, ushort productId) 获取_产品唯一ID(IntPtr devHandle)
        {
            bool rt = true;
            string msg = string.Empty;
            ushort productId = 0;
            try
            {
                var rtn = (_em_Err_)scio32.NIO_ReadProductID(devHandle, ref productId);
                msg = ErrToMsg(rtn);
                rt = rtn == _em_Err_.指令执行成功 ? true : false;
            }
            catch (Exception ex)
            {
                rt = false;
                msg = ex.ToString();
            }
            return (rt, msg, productId);
        }


        /// <summary>
        /// userData : 用于存放用户数据的缓冲(84字节)
        /// </summary> 
        public (bool s, string m, byte userData) 获取_用户数据(IntPtr devHandle)
        {
            bool rt = true;
            string msg = string.Empty;
            byte userData = 0;
            try
            {
                var rtn = (_em_Err_)scio32.NIO_ReadUserData(devHandle, ref userData);
                msg = ErrToMsg(rtn);
                rt = rtn == _em_Err_.指令执行成功 ? true : false;
            }
            catch (Exception ex)
            {
                rt = false;
                msg = ex.ToString();
            }
            return (rt, msg, userData);
        }

        /// <summary>
        /// <para> 用于存放用户数据的缓冲(最多存储84字节, 可用于存储加密) 注意：数据一次性读出和写入，超出部分将忽略</para>
        /// </summary>
        /// <param name="devHandle"></param>
        /// <param name="userData"></param>
        /// <returns></returns>
        public (bool s, string m) 设置_用户数据(IntPtr devHandle, byte[] userData)
        {
            bool rt = true;
            string msg = string.Empty;

            try
            {
                var rtn = (_em_Err_)scio32.NIO_WriteUserData(devHandle, userData);
                msg = ErrToMsg(rtn);
                rt = rtn == _em_Err_.指令执行成功 ? true : false;
            }
            catch (Exception ex)
            {
                rt = false;
                msg = ex.ToString();
            }
            return (rt, msg);
        }






        /// <summary>
        /// <para>value : 用于存储错误信息的缓冲</para> 
        /// </summary> 
        public (bool s, string m, byte value) 获取_设备最后出现的错误(IntPtr devHandle, ushort 缓冲区大小)
        {
            bool rt = true;
            string msg = string.Empty;
            byte value = 0;
            try
            {
                var rtn = (_em_Err_)scio32.NIO_GetError(devHandle, ref value, 缓冲区大小);
                msg = ErrToMsg(rtn);
                rt = rtn == _em_Err_.指令执行成功 ? true : false;
            }
            catch (Exception ex)
            {
                rt = false;
                msg = ex.ToString();
            }
            return (rt, msg, value);
        }




        #endregion


    }
}
