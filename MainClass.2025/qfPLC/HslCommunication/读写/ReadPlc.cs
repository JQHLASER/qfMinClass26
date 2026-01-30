using HslCommunication;
using HslCommunication.CNC.Fanuc;
using HslCommunication.Core;
using HslCommunication.ModBus;
using HslCommunication.Profinet.Melsec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfPLC
{
    internal class ReadPlc
    {


        #region MelsecMcAsciiNet....三菱_MC_Ascii_Qna3E


        public virtual (bool state, string msg, T v) Read<T>(MelsecMcAsciiNet Plc, _ReadType_ Read_Type, string address, ushort length = 0, Encoding encoding = null) 
        {
            switch (Read_Type)
            {
                case _ReadType_.Read:
                    var rt = Read<T>(Plc, address, length, encoding);
                    return rt;
                   default :
                    return (false, $"{Get语言_无此功能()}: {Read_Type}", qfmain.T_实例化泛型.FastNew<T>.Create());
            }
        }

        public virtual async Task<(bool state, string msg, T v)> ReadAsync<T>(MelsecMcAsciiNet Plc, _ReadTypeAsync_ Read_Type, string address, ushort length = 0, Encoding encoding = null) 
        {
            switch (Read_Type)
            {
                case _ReadTypeAsync_.ReadAsync:
                    var rt = await ReadAsync<T>(Plc, address, length, encoding);
                    return rt;
                    default :
                    return (false, $"{Get语言_无此功能()}: {Read_Type}", qfmain.T_实例化泛型.FastNew<T>.Create());
            }
        }

        #endregion

        #region MelsecFxSerial...三菱_FX

        public virtual (bool state, string msg, T v) Read<T>(MelsecFxSerial Plc, _ReadType_ Read_Type, string address, ushort length = 0, Encoding encoding = null) 
        {
            switch (Read_Type)
            {
                case _ReadType_.Read:
                    var rt = Read<T>(Plc, address, length, encoding);
                    return rt;
                    default :
                    return (false, $"{Get语言_无此功能()}: {Read_Type}", qfmain.T_实例化泛型.FastNew<T>.Create());
            }
        }

        public virtual async Task<(bool state, string msg, T v)> ReadAsync<T>(MelsecFxSerial Plc, _ReadTypeAsync_ Read_Type, string address, ushort length = 0, Encoding encoding = null) 
        {
            switch (Read_Type)
            {
                case _ReadTypeAsync_.ReadAsync:
                    var rt = await ReadAsync<T>(Plc, address, length, encoding);
                    return rt;
                    default :
                    return (false, $"{Get语言_无此功能()}: {Read_Type}", qfmain.T_实例化泛型.FastNew<T>.Create());
            }
        }


        #endregion


        #region ModbusTcp....包含( ReadDiscrete / ReadCoil )


        public virtual (bool state, string msg, T v) Read<T>(ModbusTcpNet Plc, _ReadType_ Read_Type, string address, ushort length = 0, Encoding encoding = null) 
        {
            switch (Read_Type)
            {
                case _ReadType_.Read:
                    var rt = Read<T>(Plc, address, length, encoding);
                    return rt;
                case _ReadType_.ReadDiscrete:

                    #region ReadDiscrete

                    Type typeDiscrete = typeof(T);
                    if (typeDiscrete == typeof(bool))
                    {
                        OperateResult<bool> result = Plc.ReadDiscrete(address);
                        //var rts = new 解析().OperateResult(result); 
                        // return (rts.s, rts.m, rts.value);
                        return result.IsSuccess
                                ? (true, "", (T)(object)result.Content)
                                : (false, result.Message, qfmain.T_实例化泛型.FastNew<T>.Create());
                    }
                    else if (typeDiscrete == typeof(bool[]))
                    {
                        var result = Plc.ReadDiscrete(address, length);
                        //  var rts = new 解析().OperateResult(result);
                        //return (rts.s, rts.m, (T)(object)rts.value);
                        return result.IsSuccess
                                   ? (true, "", (T)(object)result.Content)
                                   : (false, result.Message, qfmain.T_实例化泛型.FastNew<T>.Create());
                    }
                    else
                    {
                        return (false, $"{Get语言_不支持类型()}: {typeDiscrete.FullName}", qfmain.T_实例化泛型.FastNew<T>.Create());
                    }

                #endregion

                case _ReadType_.ReadCoil:

                    #region ReadCoil

                    Type typeCoil = typeof(T);
                    if (typeCoil == typeof(bool))
                    {
                        OperateResult<bool[]> result = Plc.ReadCoil(address, length);
                        var rts = new 解析().OperateResult(result);
                        return (rts.s, rts.m, (T)(object)rts.value);
                    }
                    else if (typeCoil == typeof(bool[]))
                    {
                        var result = Plc.ReadCoil(address, length);
                        var rts = new 解析().OperateResult(result);
                        return (rts.s, rts.m, (T)(object)rts.value);

                    }
                    else
                    {
                        return (false, $"{Get语言_不支持类型()}: {typeCoil.FullName}", qfmain.T_实例化泛型.FastNew<T>.Create());
                    }

                    #endregion

                   default :
                    return (false, "", qfmain.T_实例化泛型.FastNew<T>.Create());
            }
        }

        public virtual async Task<(bool state, string msg, T v)> ReadAsync<T>(ModbusTcpNet Plc, _ReadTypeAsync_ Read_Type, string address, ushort length = 0, Encoding encoding = null) 
        {
            switch (Read_Type)
            {
                case _ReadTypeAsync_.ReadAsync:
                    var rt = await ReadAsync<T>(Plc, address, length, encoding);
                    return rt;
                case _ReadTypeAsync_.ReadDiscreteAsync:

                    #region ReadDiscreteAsync

                    Type typeDiscrete = typeof(T);
                    if (typeDiscrete == typeof(bool))
                    {
                        OperateResult<bool> result = await Plc.ReadDiscreteAsync(address);
                        return (result.IsSuccess, result.Message, (T)(object)result.Content);
                    }
                    else if (typeDiscrete == typeof(bool[]))
                    {
                        var result = await Plc.ReadDiscreteAsync(address, length);
                        return (result.IsSuccess, result.Message, (T)(object)result.Content);
                    }
                    else
                    {
                        return (false, $"{Get语言_不支持类型()}: {typeDiscrete.FullName}", qfmain.T_实例化泛型.FastNew<T>.Create());
                    }

                #endregion

                case _ReadTypeAsync_.ReadCoilAsync:

                    #region ReadCoil

                    Type typeCoil = typeof(T);
                    if (typeCoil == typeof(bool))
                    {
                        OperateResult<bool[]> result = await Plc.ReadCoilAsync(address, length);
                        return (result.IsSuccess, result.Message, (T)(object)result.Content);
                    }
                    else if (typeCoil == typeof(bool[]))
                    {
                        var result = await Plc.ReadCoilAsync(address, length);

                        return (result.IsSuccess, result.Message, (T)(object)result.Content);

                    }
                    else
                    {
                        return (false, $"{Get语言_不支持类型()}: {typeCoil.FullName}", qfmain.T_实例化泛型.FastNew<T>.Create());
                    }

                #endregion

                default:
                    return (false, "", qfmain.T_实例化泛型.FastNew<T>.Create());
            }
        }


        #endregion








        #region 本地方法

        (bool rt, string msgErr, T value) Read<T>(IReadWriteNet PlcNet, string address, ushort length, Encoding encoding = null) 
        {
            try
            {
                Type type = typeof(T);

                if (type == typeof(bool))
                {
                    OperateResult<bool> result = PlcNet.ReadBool(address);
                    return result.IsSuccess ?
                          (true, "", (T)(object)result.Content) :
                          (false, result.Message, qfmain.T_实例化泛型.FastNew<T>.Create());
                     
                }
                else if (type == typeof(short))
                {
                    OperateResult<short> result = PlcNet.ReadInt16(address);
                    return result.IsSuccess ?
                           (true, "", (T)(object)result.Content) :
                           (false, result.Message, qfmain.T_实例化泛型.FastNew<T>.Create());
                }
                else if (type == typeof(ushort))
                {
                    OperateResult<ushort> result = PlcNet.ReadUInt16(address);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, qfmain.T_实例化泛型.FastNew<T>.Create());
                }
                else if (type == typeof(int))
                {
                    OperateResult<int> result = PlcNet.ReadInt32(address);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, qfmain.T_实例化泛型.FastNew<T>.Create());
                }
                else if (type == typeof(uint))
                {
                    OperateResult<uint> result = PlcNet.ReadUInt32(address);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, qfmain.T_实例化泛型.FastNew<T>.Create());
                }
                else if (type == typeof(long))
                {
                    OperateResult<long> result = PlcNet.ReadInt64(address);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, qfmain.T_实例化泛型.FastNew<T>.Create());
                }
                else if (type == typeof(ulong))
                {
                    OperateResult<ulong> result = PlcNet.ReadUInt64(address);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, qfmain.T_实例化泛型.FastNew<T>.Create());
                }
                else if (type == typeof(float))
                {
                    OperateResult<float> result = PlcNet.ReadFloat(address);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, qfmain.T_实例化泛型.FastNew<T>.Create());
                }
                else if (type == typeof(double))
                {
                    OperateResult<double> result = PlcNet.ReadDouble(address);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, qfmain.T_实例化泛型.FastNew<T>.Create());
                }
                else if (type == typeof(bool[]))
                {
                    OperateResult<bool[]> result = PlcNet.ReadBool(address, length);
                    return result.IsSuccess ?
                          (true, "", (T)(object)result.Content) :
                          (false, result.Message, qfmain.T_实例化泛型.FastNew<T>.Create());
                }
                else if (type == typeof(short[]))
                {
                    OperateResult<short[]> result = PlcNet.ReadInt16(address, length);
                    return result.IsSuccess ?
                           (true, "", (T)(object)result.Content) :
                           (false, result.Message, qfmain.T_实例化泛型.FastNew<T>.Create());
                }
                else if (type == typeof(ushort[]))
                {
                    OperateResult<ushort[]> result = PlcNet.ReadUInt16(address, length);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, qfmain.T_实例化泛型.FastNew<T>.Create());
                }
                else if (type == typeof(int[]))
                {
                    OperateResult<int[]> result = PlcNet.ReadInt32(address, length);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, qfmain.T_实例化泛型.FastNew<T>.Create());
                }
                else if (type == typeof(uint[]))
                {
                    OperateResult<uint[]> result = PlcNet.ReadUInt32(address, length);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, qfmain.T_实例化泛型.FastNew<T>.Create());
                }
                else if (type == typeof(long[]))
                {
                    OperateResult<long[]> result = PlcNet.ReadInt64(address, length);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, qfmain.T_实例化泛型.FastNew<T>.Create());
                }
                else if (type == typeof(ulong[]))
                {
                    OperateResult<ulong[]> result = PlcNet.ReadUInt64(address, length);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, qfmain.T_实例化泛型.FastNew<T>.Create());
                }
                else if (type == typeof(float[]))
                {
                    OperateResult<float[]> result = PlcNet.ReadFloat(address, length);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, qfmain.T_实例化泛型.FastNew<T>.Create());
                }
                else if (type == typeof(double[]))
                {
                    OperateResult<double[]> result = PlcNet.ReadDouble(address, length);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, qfmain.T_实例化泛型.FastNew<T>.Create());
                }
                else if (type == typeof(string))
                {
                    OperateResult<string> result = PlcNet.ReadString(address, length, encoding ?? Encoding.ASCII);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, qfmain.T_实例化泛型.FastNew<T>.Create());
                }
                else
                {
                    return (false, "不支持读取类型", qfmain.T_实例化泛型.FastNew<T>.Create());
                }
            }
            catch (Exception ex)
            {

                return (false, ex.Message, qfmain.T_实例化泛型.FastNew<T>.Create());
            }
        }

        async Task<(bool rt, string msgErr, T value)> ReadAsync<T>(IReadWriteNet PlcNet, string address, ushort length, Encoding encoding = null) 
        {
            try
            {
                Type type = typeof(T);

                if (type == typeof(bool))
                {
                    OperateResult<bool> result = await PlcNet.ReadBoolAsync(address);
                    return result.IsSuccess ?
                          (true, "", (T)(object)result.Content) :
                          (false, result.Message, qfmain.T_实例化泛型.FastNew<T>.Create());
                }
                else if (type == typeof(short))
                {
                    OperateResult<short> result = await PlcNet.ReadInt16Async(address);
                    return result.IsSuccess ?
                           (true, "", (T)(object)result.Content) :
                           (false, result.Message, qfmain.T_实例化泛型.FastNew<T>.Create());
                }
                else if (type == typeof(ushort))
                {
                    OperateResult<ushort> result = await PlcNet.ReadUInt16Async(address);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, qfmain.T_实例化泛型.FastNew<T>.Create());
                }
                else if (type == typeof(int))
                {
                    OperateResult<int> result = await PlcNet.ReadInt32Async(address);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, qfmain.T_实例化泛型.FastNew<T>.Create());
                }
                else if (type == typeof(uint))
                {
                    OperateResult<uint> result = await PlcNet.ReadUInt32Async(address);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, qfmain.T_实例化泛型.FastNew<T>.Create());
                }
                else if (type == typeof(long))
                {
                    OperateResult<long> result = await PlcNet.ReadInt64Async(address);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, qfmain.T_实例化泛型.FastNew<T>.Create());
                }
                else if (type == typeof(ulong))
                {
                    OperateResult<ulong> result = await PlcNet.ReadUInt64Async(address);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, qfmain.T_实例化泛型.FastNew<T>.Create());
                }
                else if (type == typeof(float))
                {
                    OperateResult<float> result = await PlcNet.ReadFloatAsync(address);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, qfmain.T_实例化泛型.FastNew<T>.Create());
                }
                else if (type == typeof(double))
                {
                    OperateResult<double> result = await PlcNet.ReadDoubleAsync(address);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, qfmain.T_实例化泛型.FastNew<T>.Create());
                }
                else if (type == typeof(bool[]))
                {
                    OperateResult<bool[]> result = await PlcNet.ReadBoolAsync(address, length);
                    return result.IsSuccess ?
                          (true, "", (T)(object)result.Content) :
                          (false, result.Message, qfmain.T_实例化泛型.FastNew<T>.Create());
                }
                else if (type == typeof(short[]))
                {
                    OperateResult<short[]> result = await PlcNet.ReadInt16Async(address, length);
                    return result.IsSuccess ?
                           (true, "", (T)(object)result.Content) :
                           (false, result.Message, qfmain.T_实例化泛型.FastNew<T>.Create());
                }
                else if (type == typeof(ushort[]))
                {
                    OperateResult<ushort[]> result = await PlcNet.ReadUInt16Async(address, length);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, qfmain.T_实例化泛型.FastNew<T>.Create());
                }
                else if (type == typeof(int[]))
                {
                    OperateResult<int[]> result = await PlcNet.ReadInt32Async(address, length);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, qfmain.T_实例化泛型.FastNew<T>.Create());
                }
                else if (type == typeof(uint[]))
                {
                    OperateResult<uint[]> result = await PlcNet.ReadUInt32Async(address, length);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, qfmain.T_实例化泛型.FastNew<T>.Create());
                }
                else if (type == typeof(long[]))
                {
                    OperateResult<long[]> result = await PlcNet.ReadInt64Async(address, length);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, qfmain.T_实例化泛型.FastNew<T>.Create());
                }
                else if (type == typeof(ulong[]))
                {
                    OperateResult<ulong[]> result = await PlcNet.ReadUInt64Async(address, length);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, qfmain.T_实例化泛型.FastNew<T>.Create());
                }
                else if (type == typeof(float[]))
                {
                    OperateResult<float[]> result = await PlcNet.ReadFloatAsync(address, length);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, qfmain.T_实例化泛型.FastNew<T>.Create());
                }
                else if (type == typeof(double[]))
                {
                    OperateResult<double[]> result = await PlcNet.ReadDoubleAsync(address, length);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, qfmain.T_实例化泛型.FastNew<T>.Create());
                }
                else if (type == typeof(string))
                {
                    OperateResult<string> result = await PlcNet.ReadStringAsync(address, length, encoding ?? Encoding.ASCII);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, qfmain.T_实例化泛型.FastNew<T>.Create());
                }
                else
                {
                    return (false, "不支持读取类型", qfmain.T_实例化泛型.FastNew<T>.Create());
                }
            }
            catch (Exception ex)
            {

                return (false, ex.Message, qfmain.T_实例化泛型.FastNew<T>.Create());
            }
        }


        internal string Get语言_不支持类型()
        {
            return qfmain.Language_.Get语言("不支持类型");
        }

        internal string Get语言_无此功能()
        {
            return qfmain.Language_.Get语言("无此功能");
        }



        #endregion

    }
}
