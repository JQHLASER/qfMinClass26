using HslCommunication;
using HslCommunication.Core;
using HslCommunication.ModBus;
using HslCommunication.Profinet.Melsec;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace qfPLC
{
    internal class WritePlc
    {


        #region 公共方法


        /// <summary>
        /// 通用安全写入
        /// </summary>
        public (bool s, string m) Write(IReadWriteNet plc, string address, object value, Encoding encoding = null)
        {
            if (plc == null)
                return (false, "PLC is null");

            if (string.IsNullOrEmpty(address))
                return (false, "Address is null");

            if (value == null)
                return (false, "Value is null");

            Type type = Nullable.GetUnderlyingType(value.GetType()) ?? value.GetType();

            Func<IReadWriteNet, string, object, Encoding, OperateResult> func;
            if (!WriteMap.TryGetValue(type, out func))
            {
                return (false, $"{new ReadPlc().Get语言_不支持类型()}: " + type.FullName);
            }

            try
            {
                var rt = func(plc, address, value, encoding);
                return (rt.IsSuccess, rt.Message);
            }
            catch (Exception ex)
            {
                return (false, $"{Get语言_写入异常()}: " + ex.Message);
            }
        }

        /// <summary>
        /// 通用异步写入
        /// </summary>
        public async Task<(bool, string m)> WriteAsync(IReadWriteNet plc, string address, object value, Encoding encoding = null)
        {
            if (plc == null)
                return (false, "PLC is null");

            if (string.IsNullOrEmpty(address))
                return (false, "Address is null");

            if (value == null)
                return (false, "Value is null");

            Type type = Nullable.GetUnderlyingType(value.GetType()) ?? value.GetType();

            Func<IReadWriteNet, string, object, Encoding, Task<OperateResult>> func;
            if (!WriteMapAsync.TryGetValue(type, out func))
            {
                return (false, $"{new ReadPlc().Get语言_不支持类型()}: " + type.FullName);
            }

            try
            {
                var rt = await func(plc, address, value, encoding);
                return (rt.IsSuccess, rt.Message);
            }
            catch (Exception ex)
            {
                return (false, $"{Get语言_写入异常()}: " + ex.Message);
            }
        }



        #endregion


        #region 本地方法

        // 类型映射表（高性能）
        private readonly Dictionary<Type, Func<IReadWriteNet, string, object, Encoding, OperateResult>> WriteMap
            = new Dictionary<Type, Func<IReadWriteNet, string, object, Encoding, OperateResult>>
        {
        { typeof(bool),  (plc, a, v, e) => plc.Write(a, (bool)v) },
        { typeof(byte),  (plc, a, v, e) => plc.Write(a, (byte)v) },
        { typeof(short), (plc, a, v, e) => plc.Write(a, (short)v) },
        { typeof(ushort),(plc, a, v, e) => plc.Write(a, (ushort)v) },
        { typeof(int),   (plc, a, v, e) => plc.Write(a, (int)v) },
        { typeof(uint),  (plc, a, v, e) => plc.Write(a, (uint)v) },
        { typeof(long),  (plc, a, v, e) => plc.Write(a, (long)v) },
        { typeof(ulong), (plc, a, v, e) => plc.Write(a, (ulong)v) },
        { typeof(float), (plc, a, v, e) => plc.Write(a, (float)v) },
        { typeof(double),(plc, a, v, e) => plc.Write(a, (double)v) },

        { typeof(bool[]),  (plc, a, v, e) => plc.Write(a, (bool[])v) },
        { typeof(byte[]),  (plc, a, v, e) => plc.Write(a, (byte[])v) },
        { typeof(short[]), (plc, a, v, e) => plc.Write(a, (short[])v) },
        { typeof(ushort[]),(plc, a, v, e) => plc.Write(a, (ushort[])v) },
        { typeof(int[]),   (plc, a, v, e) => plc.Write(a, (int[])v) },
        { typeof(uint[]),  (plc, a, v, e) => plc.Write(a, (uint[])v) },
        { typeof(long[]),  (plc, a, v, e) => plc.Write(a, (long[])v) },
        { typeof(ulong[]), (plc, a, v, e) => plc.Write(a, (ulong[])v) },
        { typeof(float[]), (plc, a, v, e) => plc.Write(a, (float[])v) },
        { typeof(double[]),(plc, a, v, e) => plc.Write(a, (double[])v) },

        { typeof(string),  (plc, a, v, e) => plc.Write(a, (string)v, e ?? Encoding.ASCII) },
        };

        // 类型映射表（高性能）
        private readonly Dictionary<Type, Func<IReadWriteNet, string, object, Encoding, Task<OperateResult>>> WriteMapAsync
            = new Dictionary<Type, Func<IReadWriteNet, string, object, Encoding, Task<OperateResult>>>
        {
        { typeof(bool),  (plc, a, v, e) => plc.WriteAsync(a, (bool)v) },
        { typeof(byte),  (plc, a, v, e) => plc.WriteAsync(a, (byte)v) },
        { typeof(short), (plc, a, v, e) => plc.WriteAsync(a, (short)v) },
        { typeof(ushort),(plc, a, v, e) => plc.WriteAsync(a, (ushort)v) },
        { typeof(int),   (plc, a, v, e) => plc.WriteAsync(a, (int)v) },
        { typeof(uint),  (plc, a, v, e) => plc.WriteAsync(a, (uint)v) },
        { typeof(long),  (plc, a, v, e) => plc.WriteAsync(a, (long)v) },
        { typeof(ulong), (plc, a, v, e) => plc.WriteAsync(a, (ulong)v) },
        { typeof(float), (plc, a, v, e) => plc.WriteAsync(a, (float)v) },
        { typeof(double),(plc, a, v, e) => plc.WriteAsync(a, (double)v) },

        { typeof(bool[]),  (plc, a, v, e) => plc.WriteAsync(a, (bool[])v) },
        { typeof(byte[]),  (plc, a, v, e) => plc.WriteAsync(a, (byte[])v) },
        { typeof(short[]), (plc, a, v, e) => plc.WriteAsync(a, (short[])v) },
        { typeof(ushort[]),(plc, a, v, e) => plc.WriteAsync(a, (ushort[])v) },
        { typeof(int[]),   (plc, a, v, e) => plc.WriteAsync(a, (int[])v) },
        { typeof(uint[]),  (plc, a, v, e) => plc.WriteAsync(a, (uint[])v) },
        { typeof(long[]),  (plc, a, v, e) => plc.WriteAsync(a, (long[])v) },
        { typeof(ulong[]), (plc, a, v, e) => plc.WriteAsync(a, (ulong[])v) },
        { typeof(float[]), (plc, a, v, e) => plc.WriteAsync(a, (float[])v) },
        { typeof(double[]),(plc, a, v, e) => plc.WriteAsync(a, (double[])v) },

        { typeof(string),  (plc, a, v, e) => plc.WriteAsync(a, (string)v, e ?? Encoding.ASCII) },
        };

         


        internal string Get语言_写入异常()
        {
            return qfmain.Language_.Get语言("写入异常");
        }


        #endregion
    }
}
