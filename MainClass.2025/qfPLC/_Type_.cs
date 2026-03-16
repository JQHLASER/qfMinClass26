using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfPLC
{
    /// <summary>
    /// PLC类型
    /// </summary>
    public enum _PLC_Type_
    {
        /// <summary>
        /// 三菱,FX编程口
        /// </summary>
        FX_Serial,
        /// <summary>
        /// 三菱,MC协议,Qna3E
        /// </summary>
        MC_ASCII,
        ModbusTcp,
        /// <summary>
        /// 西门子,S7系列
        /// </summary>
        S7,


    }

    /// <summary>
    /// 读取类型
    /// </summary>
    public enum _ReadType_
    { 
        Read,

        /// <summary>
        /// 读取输入线圈,有的用来读X端
        /// <para>功能码: 0x02</para>
        /// </summary>
        ReadDiscrete, 

        /// <summary>
        /// 读取输入线圈,有的用来读X端
        /// <para>功能码: 0x01</para>
        /// </summary>
        ReadCoil,

        /// <summary>
        /// 有的没有这个功能
        /// </summary>
        ReadByte,
      
    }


    /// <summary>
    /// 读取类型
    /// </summary>
    public enum _ReadTypeAsync_
    {

        /// <summary>
        /// 异步读取 
        /// </summary>
        ReadAsync,

        /// <summary>
        /// 读取输入线圈,有的用来读X端
        /// <para>功能码: 0x02</para>
        /// </summary>
        
        ReadDiscreteAsync,
        
        /// <summary>
        /// 读取输入线圈,有的用来读X端
        /// <para>功能码: 0x01</para>
        /// </summary>
        ReadCoilAsync,
    }










}
