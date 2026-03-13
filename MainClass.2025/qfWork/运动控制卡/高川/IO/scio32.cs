using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

using HAND = System.IntPtr;


namespace qfWork
{
    public class scio32
    {
        public const string DLL_PATH = @"scio64.dll";

        /********************************宏定义****************************************/
        public const Int16 MAX_SUPPORT_DEVICES = 16;

        /* 指令返回值定义 */

        public const Int16 RTN_CMD_SUCCESS = 0;        //指令执行成功
        public const Int16 RTN_CMD_ERROR = -1;        //指令执行失败
        public const Int16 RTN_LIB_PARA_ERROR = -2;        //指令参数错误
        public const Int16 RTN_NO_SUPPORT = -3;        //不支持
        public const Int16 RTN_INVALID_HANDLE = -4;        //无效句柄


        ////设备信息结构
        //public struct TDevInfo
        //{
        //    public ushort address;              // 在上位机系统中分配的地址序号,如USB
        //    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        //    public string idStr;               // 识别字符串 
        //    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        //    public string description;      // 描述符
        //    public ushort ID;                   // 板上的ID(未用)
        //};

        //public struct CALENDAR
        //{
        //    public ushort w_year;
        //    public byte month;
        //    public byte day;
        //    public byte week;
        //    public byte hour;
        //    public byte min;
        //    public byte sec;
        //};


        /********************************函数声明**************************************/
        /*******************************************************************************
        * 函数名称: NIO_Search
        * 功能说明: 板卡搜寻
        * 入口参数: pDevNo:     用于接收搜寻到的设备的数目
        * 出口参数: 指令执行状态
        *******************************************************************************/
        [DllImport(DLL_PATH, CallingConvention = CallingConvention.StdCall)]
        public static extern Int16 NIO_Search(ref ushort pDevNo, ref GaoCH_IO_scio32.TDevInfo pInfoList);
        //public static extern Int16  NIO_Search( ref ushort pDevNo, byte[] pInfoList);

        /*******************************************************************************
        * 函数名称: NIO_OpenByID
        * 功能说明: 根据SIO 设备在系统中名称，打开相应的设备。
        * 入口参数: idStr:      设备名称
                    pDevHandle: 用于接收设备操作句柄
        * 出口参数: 指令执行成功
        *******************************************************************************/
        [DllImport(DLL_PATH, CallingConvention = CallingConvention.StdCall)]
        public static extern Int16 NIO_OpenByID(string idStr, ref HAND pDevHandle);

        /*******************************************************************************
        * 函数名称: NIO_Close
        * 功能说明: 关闭打开的设备
        * 入口参数: devHandle:  设备操作句柄
        * 出口参数: 指令执行状态
        *******************************************************************************/
        [DllImport(DLL_PATH, CallingConvention = CallingConvention.StdCall)]
        public static extern Int16 NIO_Close(HAND devHandle);

        /*******************************************************************************
        * 函数名称: NIO_GetDO
        * 功能说明: 获取设备点位输出状态
        * 入口参数: devHandle:  设备操作句柄
                    portStart:  起始端口号, 取值范围(0-1)
                    portCount:  要获取的端口数量, 取值范围(1-2)
                    value:      用于存储端口状态值缓冲(每个端口16个点位), 每个点位: 
                                1 代表触发; 0 代表未触发
        * 出口参数: 指令执行状态
        *******************************************************************************/
        [DllImport(DLL_PATH, CallingConvention = CallingConvention.StdCall)]
        public static extern Int16 NIO_GetDO(HAND devHandle, ushort portStart, ushort portCount, ref ushort value);

        /*******************************************************************************
        * 函数名称: NIO_SetDO
        * 功能说明: 设置设备输出端口状态
        * 入口参数: devHandle:  设备操作句柄
                    portStart:  起始端口号, 取值范围(0-1)
                    portCount:  要获取的端口数量, 取值范围(1-2)
                    value:      要设置的端口状态值缓冲(每个端口16个点位), 每个点位: 
                        1 代表触发; 0 代表不触发
        * 出口参数: 指令执行状态
        *******************************************************************************/
        [DllImport(DLL_PATH, CallingConvention = CallingConvention.StdCall)]
        public static extern Int16 NIO_SetDO(HAND devHandle, ushort portStart, ushort portCount, ushort value);
        // public static extern Int16 NIO_SetDO(HAND devHandle, ushort portStart, ushort portCount, ref ushort value);

        /*******************************************************************************
        * 函数名称: NIO_GetDOBit
        * 功能说明: 获取设备单个点位输出状态
        * 入口参数: devHandle:  设备操作句柄
                    channel:    点位位置, 取值范围: 0-31
                    value:      用于存储获取的点位状态, 1 代表触发; 0 代表未触发
        * 出口参数: 指令执行状态
        *******************************************************************************/
        [DllImport(DLL_PATH, CallingConvention = CallingConvention.StdCall)]
        public static extern Int16 NIO_GetDOBit(HAND devHandle, ushort channel, ref ushort value);

        /*******************************************************************************
        * 函数名称: NIO_SetDOBit
        * 功能说明: 设置设备单个点位输出状态
        * 入口参数: devHandle:  设备操作句柄
                    channel:    点位位置, 取值范围: 0-31
                    value:      要设置的值, 1 代表触发; 0 代表不触发
        * 出口参数: 指令执行状态
        *******************************************************************************/
        [DllImport(DLL_PATH, CallingConvention = CallingConvention.StdCall)]
        public static extern Int16 NIO_SetDOBit(HAND devHandle, ushort channel, ushort value);

        /*******************************************************************************
        * 函数名称: NIO_GetDI
        * 功能说明: 获取设备输入状态，按端口获取
        * 入口参数: devHandle:  设备操作句柄
                    portStart:  起始端口号, 取值范围(0-1)
                    portCount:  要获取的端口数量, 取值范围(1-2)
                    value:      用于存储端口状态值缓冲(每个端口16个点位), 每个点位: 
                                1 代表有效（接通）; 0 代表无效（未接通）
        * 出口参数: 指令执行状态
        *******************************************************************************/
        [DllImport(DLL_PATH, CallingConvention = CallingConvention.StdCall)]
        public static extern Int16 NIO_GetDI(HAND devHandle, ushort portStart, ushort portCount, ref ushort value);

        /*******************************************************************************
        * 函数名称: NIO_GetDIBit
        * 功能说明: 获取设备单个点位输入状态
        * 入口参数: devHandle:  设备操作句柄
                    channel:    点位位置, 取值范围: 0-31
                    value:      用于存储获取的点位状态, 1 代表有效; 0 代表无效
        * 出口参数: 指令执行状态
        *******************************************************************************/
        [DllImport(DLL_PATH, CallingConvention = CallingConvention.StdCall)]
        public static extern Int16 NIO_GetDIBit(HAND devHandle, ushort channel, ref ushort value);

        /*******************************************************************************
        * 函数名称: NIO_GetTime
        * 功能说明: 获取设备时期及时间
        * 入口参数: devHandle:  设备操作句柄
                    date:       用于存储日期时间的缓冲
        * 出口参数: 指令执行状态
        *******************************************************************************/
        [DllImport(DLL_PATH, CallingConvention = CallingConvention.StdCall)]
        public static extern Int16 NIO_GetTime(HAND devHandle, ref GaoCH_IO_scio32. CALENDAR date);

        /*******************************************************************************
        * 函数名称: NIO_WriteID
        * 功能说明: 修改SIO 设备在系统中名称
        * 入口参数: devHandle:  操作句柄
                    idStr:      新的设备名称, 最长16字节包括末尾'\0'字符
        * 出口参数: 指令执行状态
        *******************************************************************************/
        [DllImport(DLL_PATH, CallingConvention = CallingConvention.StdCall)]
        public static extern Int16 NIO_WriteID(HAND devHandle, string idStr);

        /*******************************************************************************
        * 函数名称: NIO_ReadID
        * 功能说明: 读取SIO 设备名称
        * 入口参数: devHandle:  设备操作句柄
                    idStr:  用于存放设备名称缓冲区, 最长16字节
        * 出口参数: 指令执行状态
        *******************************************************************************/
        [DllImport(DLL_PATH, CallingConvention = CallingConvention.StdCall)]
        public static extern Int16 NIO_ReadID(HAND devHandle, ref byte idStr);

        /*******************************************************************************
        * 函数名称: NIO_WriteDevID
        * 功能说明: 修改SIO 设备ID序号（用于Modbus通讯的设备ID）
        * 入口参数: devHandle:  操作句柄
                    idStr:      新的设备序号
        * 出口参数: 指令执行状态
        *******************************************************************************/
        [DllImport(DLL_PATH, CallingConvention = CallingConvention.StdCall)]
        public static extern Int16 NIO_WriteDevID(HAND devHandle, [MarshalAs(UnmanagedType.LPArray, SizeConst = 16)] byte[] devId);

        /*******************************************************************************
        * 函数名称: NIO_ReadDevID
        * 功能说明: 读取SIO 设备ID序号（用于Modbus通讯的设备ID）
        * 入口参数: devHandle:  设备操作句柄
                    productId:  用于存放设备ID序号
        * 出口参数: 指令执行状态
        *******************************************************************************/
        [DllImport(DLL_PATH, CallingConvention = CallingConvention.StdCall)]
        public static extern Int16 NIO_ReadDevID(HAND devHandle, ref byte devId);

        /*******************************************************************************
        * 函数名称: NIO_ReadProductID
        * 功能说明: 获取产品唯一ID
        * 入口参数: devHandle:  设备操作句柄
                    productId:  用于存放设备ID的缓冲(12字节共96位)
        * 出口参数: 指令执行状态
        *******************************************************************************/
        [DllImport(DLL_PATH, CallingConvention = CallingConvention.StdCall)]
        public static extern Int16 NIO_ReadProductID(HAND devHandle, ref ushort productId);

        /*******************************************************************************
        * 函数名称: NIO_ReadUserData
        * 功能说明: 获取用户数据
        * 入口参数: devHandle:  设备操作句柄
                    userData:   用于存放用户数据的缓冲(84字节)
        * 出口参数: 指令执行状态
        *******************************************************************************/
        [DllImport(DLL_PATH, CallingConvention = CallingConvention.StdCall)]
        public static extern Int16 NIO_ReadUserData(HAND devHandle, ref byte userData);
        /*******************************************************************************
        * 函数名称: NIO_WriteUserData
        * 功能说明: 存储用户数据
        * 入口参数: devHandle:  设备操作句柄
                    userData:   用于存放用户数据的缓冲(最多存储84字节, 可用于存储加密)
		        				注意：数据一次性读出和写入，超出部分将忽略
        * 出口参数: 指令执行状态
        *******************************************************************************/
        [DllImport(DLL_PATH, CallingConvention = CallingConvention.StdCall)]
        public static extern Int16 NIO_WriteUserData(HAND devHandle, [MarshalAs(UnmanagedType.LPArray, SizeConst = 84)] byte[] userData);

        /*******************************************************************************
        * 函数名称: NIO_GetIOLogic
        * 功能说明: 获取设备IO口逻辑
        * 入口参数: devHandle:  设备操作句柄
        value:      用于存储IO逻辑状态
        * 出口参数: 指令执行状态
        *******************************************************************************/
        [DllImport(DLL_PATH, CallingConvention = CallingConvention.StdCall)]
        public static extern Int16 NIO_GetIOLogic(HAND devHandle, ref ushort value);

        /*******************************************************************************
        * 函数名称: NIO_SetIOLogic
        * 功能说明: 设置设备IO口逻辑
        * 入口参数: devHandle:  设备操作句柄
        value:      要设置的IO逻辑状态（1，正逻辑；0，负逻辑）
        * 出口参数: 指令执行状态
        *******************************************************************************/
        [DllImport(DLL_PATH, CallingConvention = CallingConvention.StdCall)]
        public static extern Int16 NIO_SetIOLogic(HAND devHandle, ushort value);
        /*******************************************************************************
        * 函数名称: NIO_GetError
        * 功能说明: SC设备操作过程中，最后出现的错误，以字符串的形式提供
        * 入口参数: handle:     设备句柄
                    str:        用于存储错误信息的缓冲
                    len:        缓冲区大小
        * 出口参数: 成功返回错误字符串，失败返回NULL
        *******************************************************************************/
        [DllImport(DLL_PATH, CallingConvention = CallingConvention.StdCall)]
        public static extern Int16 NIO_GetError(HAND devHandle, ref byte str, ushort len);

        /*******************************************************************************
        * 函数名称: NIO_SetDAC
        * 功能说明: 设置设备模拟量输出
        * 入口参数: devHandle:  设备操作句柄
                    adcStart:   起始通道号, 取值范围(0-3)
                    adcCount:   要获取的通道数量, 取值范围(1-4)
                    value:      要设置的端口模拟量输出值缓冲
        * 出口参数: 指令执行状态
        * 备注    : 该接口适用型号：USBIO32AD01
        *******************************************************************************/
        [DllImport(DLL_PATH, CallingConvention = CallingConvention.StdCall)]
       // public static extern Int16 NIO_SetDAC(HAND devHandle, ushort dacStart, ushort dacCount, ref ushort value);
        public static extern Int16 NIO_SetDAC(HAND devHandle, ushort dacStart, ushort dacCount,   ushort value);


        /*******************************************************************************
        * 函数名称: NIO_GetADC
        * 功能说明: 获取设备模拟量输出
        * 入口参数: devHandle:  设备操作句柄
                    adcStart:   起始通道号, 取值范围(0-3)
                    adcCount:   要获取的通道数量, 取值范围(1-4)
                    value:      用于存储模拟量输入值缓冲
        * 出口参数: 指令执行状态
        * 备注    : 该接口适用型号：USBIO32AD01
        *******************************************************************************/
        [DllImport(DLL_PATH, CallingConvention = CallingConvention.StdCall)]
        public static extern Int16 NIO_GetADC(HAND devHandle, ushort dacStart, ushort dacCount, ref ushort value);
    }
}
