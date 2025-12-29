using HslCommunication;
using HslCommunication.CNC.Fanuc;
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


        public virtual (bool rt, string msgErr, T value) Read<T>(MelsecMcAsciiNet PlcNet, string address)
        {
            try
            {

                Type type = typeof(T);
                if (type == typeof(bool))
                {
                    OperateResult<bool> result = PlcNet.ReadBool(address);
                    return result.IsSuccess ?
                          (true, "", (T)(object)result.Content) :
                          (false, result.Message, default);
                }
                else if (type == typeof(short))
                {
                    OperateResult<short> result = PlcNet.ReadInt16(address);
                    return result.IsSuccess ?
                           (true, "", (T)(object)result.Content) :
                           (false, result.Message, default);
                }
                else if (type == typeof(ushort))
                {
                    OperateResult<ushort> result = PlcNet.ReadUInt16(address);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }
                else if (type == typeof(int))
                {
                    OperateResult<int> result = PlcNet.ReadInt32(address);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }
                else if (type == typeof(uint))
                {
                    OperateResult<uint> result = PlcNet.ReadUInt32(address);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }
                else if (type == typeof(long))
                {
                    OperateResult<long> result = PlcNet.ReadInt64(address);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }
                else if (type == typeof(ulong))
                {
                    OperateResult<ulong> result = PlcNet.ReadUInt64(address);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }
                else if (type == typeof(float))
                {
                    OperateResult<float> result = PlcNet.ReadFloat(address);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }
                else if (type == typeof(double))
                {
                    OperateResult<double> result = PlcNet.ReadDouble(address);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }


                else
                {
                    return (false, "Not Object", default);
                }
            }
            catch (Exception ex)
            {

                return (false, ex.Message, default);
            }
        }

        public virtual (bool rt, string msgErr, T value) Read<T>(MelsecMcAsciiNet PlcNet, string address, ushort length)
        {
            try
            {


                Type type = typeof(T);
                if (type == typeof(bool[]))
                {
                    OperateResult<bool[]> result = PlcNet.ReadBool(address, length);
                    return result.IsSuccess ?
                          (true, "", (T)(object)result.Content) :
                          (false, result.Message, default);
                }
                else if (type == typeof(short[]))
                {
                    OperateResult<short[]> result = PlcNet.ReadInt16(address, length);
                    return result.IsSuccess ?
                           (true, "", (T)(object)result.Content) :
                           (false, result.Message, default);
                }
                else if (type == typeof(ushort[]))
                {
                    OperateResult<ushort[]> result = PlcNet.ReadUInt16(address, length);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }
                else if (type == typeof(int[]))
                {
                    OperateResult<int[]> result = PlcNet.ReadInt32(address, length);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }
                else if (type == typeof(uint[]))
                {
                    OperateResult<uint[]> result = PlcNet.ReadUInt32(address, length);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }
                else if (type == typeof(long[]))
                {
                    OperateResult<long[]> result = PlcNet.ReadInt64(address, length);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }
                else if (type == typeof(ulong[]))
                {
                    OperateResult<ulong[]> result = PlcNet.ReadUInt64(address, length);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }
                else if (type == typeof(float[]))
                {
                    OperateResult<float[]> result = PlcNet.ReadFloat(address, length);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }
                else if (type == typeof(double[]))
                {
                    OperateResult<double[]> result = PlcNet.ReadDouble(address, length);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }

                else
                {
                    return (false, "Not Object", default);
                }
            }
            catch (Exception ex)
            {

                return (false, ex.Message, default);
            }

        }

        public virtual (bool rt, string msgErr, string value) Read(MelsecMcAsciiNet PlcNet, string address, ushort length, Encoding encoding)
        {
            try
            {

                OperateResult<string> result = PlcNet.ReadString(address, length, encoding);
                return result.IsSuccess ?
                      (true, "", result.Content) :
                      (false, result.Message, "");

            }
            catch (Exception ex)
            {

                return (false, ex.Message, default);
            }
        }
        public virtual (bool rt, string msgErr, string value) Read(MelsecMcAsciiNet PlcNet, string address, ushort length )
        {
            try
            {

                OperateResult<string> result = PlcNet.ReadString(address, length );
                return result.IsSuccess ?
                      (true, "", result.Content) :
                      (false, result.Message, "");

            }
            catch (Exception ex)
            {

                return (false, ex.Message, default);
            }
        }


        public virtual async Task<(bool rt, string msgErr, T value)> ReadAsync<T>(MelsecMcAsciiNet PlcNet, string address)
        {
            try
            {


                Type type = typeof(T);
                if (type == typeof(bool))
                {
                    OperateResult<bool> result = await PlcNet.ReadBoolAsync(address);
                    return result.IsSuccess ?
                          (true, "", (T)(object)result.Content) :
                          (false, result.Message, default);
                }
                else if (type == typeof(short))
                {
                    OperateResult<short> result = await PlcNet.ReadInt16Async(address);
                    return result.IsSuccess ?
                           (true, "", (T)(object)result.Content) :
                           (false, result.Message, default);
                }
                else if (type == typeof(ushort))
                {
                    OperateResult<ushort> result = await PlcNet.ReadUInt16Async(address);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }
                else if (type == typeof(int))
                {
                    OperateResult<int> result = await PlcNet.ReadInt32Async(address);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }
                else if (type == typeof(uint))
                {
                    OperateResult<uint> result = await PlcNet.ReadUInt32Async(address);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }
                else if (type == typeof(long))
                {
                    OperateResult<long> result = await PlcNet.ReadInt64Async(address);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }
                else if (type == typeof(ulong))
                {
                    OperateResult<ulong> result = await PlcNet.ReadUInt64Async(address);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }
                else if (type == typeof(float))
                {
                    OperateResult<float> result = await PlcNet.ReadFloatAsync(address);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }
                else if (type == typeof(double))
                {
                    OperateResult<double> result = await PlcNet.ReadDoubleAsync(address);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }


                else
                {
                    return (false, "Not Object", default);
                }
            }
            catch (Exception ex)
            {

                return (false, ex.Message, default);
            }
        }

        public virtual async Task<(bool rt, string msgErr, T value)> ReadAsync<T>(MelsecMcAsciiNet PlcNet, string address, ushort length)
        {
            try
            {

                Type type = typeof(T);
                if (type == typeof(bool[]))
                {
                    OperateResult<bool[]> result = await PlcNet.ReadBoolAsync(address, length);
                    return result.IsSuccess ?
                          (true, "", (T)(object)result.Content) :
                          (false, result.Message, default);
                }
                else if (type == typeof(short[]))
                {
                    OperateResult<short[]> result = await PlcNet.ReadInt16Async(address, length);
                    return result.IsSuccess ?
                           (true, "", (T)(object)result.Content) :
                           (false, result.Message, default);
                }
                else if (type == typeof(ushort[]))
                {
                    OperateResult<ushort[]> result = await PlcNet.ReadUInt16Async(address, length);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }
                else if (type == typeof(int[]))
                {
                    OperateResult<int[]> result = await PlcNet.ReadInt32Async(address, length);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }
                else if (type == typeof(uint[]))
                {
                    OperateResult<uint[]> result = await PlcNet.ReadUInt32Async(address, length);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }
                else if (type == typeof(long[]))
                {
                    OperateResult<long[]> result = await PlcNet.ReadInt64Async(address, length);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }
                else if (type == typeof(ulong[]))
                {
                    OperateResult<ulong[]> result = await PlcNet.ReadUInt64Async(address, length);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }
                else if (type == typeof(float[]))
                {
                    OperateResult<float[]> result = await PlcNet.ReadFloatAsync(address, length);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }
                else if (type == typeof(double[]))
                {
                    OperateResult<double[]> result = await PlcNet.ReadDoubleAsync(address, length);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }




                else
                {
                    return (false, "Not Object", default);
                }
            }
            catch (Exception ex)
            {

                return (false, ex.Message, default);
            }
        }

        public virtual async Task<(bool rt, string msgErr, string value)> ReadAsync(MelsecMcAsciiNet PlcNet, string address, ushort length, Encoding encoding)
        {
            try
            {

                OperateResult<string> result = await PlcNet.ReadStringAsync(address, length, encoding);
                return result.IsSuccess ?
                      (true, "", result.Content) :
                      (false, result.Message, "");

            }
            catch (Exception ex)
            {

                return (false, ex.Message, default);
            }
        }
        public virtual async Task<(bool rt, string msgErr, string value)> ReadAsync(MelsecMcAsciiNet PlcNet, string address, ushort length )
        {
            try
            {

                OperateResult<string> result = await PlcNet.ReadStringAsync(address, length);
                return result.IsSuccess ?
                      (true, "", result.Content) :
                      (false, result.Message, "");

            }
            catch (Exception ex)
            {

                return (false, ex.Message, default);
            }
        }

        #endregion

        #region MelsecFxSerial...三菱_FX


        public virtual (bool rt, string msgErr, T value) Read<T>(MelsecFxSerial PlcNet, string address)
        {
            try
            {

                Type type = typeof(T);
                if (type == typeof(bool))
                {
                    OperateResult<bool> result = PlcNet.ReadBool(address);
                    return result.IsSuccess ?
                          (true, "", (T)(object)result.Content) :
                          (false, result.Message, default);
                }
                else if (type == typeof(short))
                {
                    OperateResult<short> result = PlcNet.ReadInt16(address);
                    return result.IsSuccess ?
                           (true, "", (T)(object)result.Content) :
                           (false, result.Message, default);
                }
                else if (type == typeof(ushort))
                {
                    OperateResult<ushort> result = PlcNet.ReadUInt16(address);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }
                else if (type == typeof(int))
                {
                    OperateResult<int> result = PlcNet.ReadInt32(address);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }
                else if (type == typeof(uint))
                {
                    OperateResult<uint> result = PlcNet.ReadUInt32(address);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }
                else if (type == typeof(long))
                {
                    OperateResult<long> result = PlcNet.ReadInt64(address);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }
                else if (type == typeof(ulong))
                {
                    OperateResult<ulong> result = PlcNet.ReadUInt64(address);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }
                else if (type == typeof(float))
                {
                    OperateResult<float> result = PlcNet.ReadFloat(address);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }
                else if (type == typeof(double))
                {
                    OperateResult<double> result = PlcNet.ReadDouble(address);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }


                else
                {
                    return (false, "Not Object", default);
                }
            }
            catch (Exception ex)
            {

                return (false, ex.Message, default);
            }
        }

        public virtual (bool rt, string msgErr, T value) Read<T>(MelsecFxSerial PlcNet, string address, ushort length)
        {
            try
            {


                Type type = typeof(T);
                if (type == typeof(bool[]))
                {
                    OperateResult<bool[]> result = PlcNet.ReadBool(address, length);
                    return result.IsSuccess ?
                          (true, "", (T)(object)result.Content) :
                          (false, result.Message, default);
                }
                else if (type == typeof(short[]))
                {
                    OperateResult<short[]> result = PlcNet.ReadInt16(address, length);
                    return result.IsSuccess ?
                           (true, "", (T)(object)result.Content) :
                           (false, result.Message, default);
                }
                else if (type == typeof(ushort[]))
                {
                    OperateResult<ushort[]> result = PlcNet.ReadUInt16(address, length);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }
                else if (type == typeof(int[]))
                {
                    OperateResult<int[]> result = PlcNet.ReadInt32(address, length);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }
                else if (type == typeof(uint[]))
                {
                    OperateResult<uint[]> result = PlcNet.ReadUInt32(address, length);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }
                else if (type == typeof(long[]))
                {
                    OperateResult<long[]> result = PlcNet.ReadInt64(address, length);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }
                else if (type == typeof(ulong[]))
                {
                    OperateResult<ulong[]> result = PlcNet.ReadUInt64(address, length);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }
                else if (type == typeof(float[]))
                {
                    OperateResult<float[]> result = PlcNet.ReadFloat(address, length);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }
                else if (type == typeof(double[]))
                {
                    OperateResult<double[]> result = PlcNet.ReadDouble(address, length);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }

                else
                {
                    return (false, "Not Object", default);
                }
            }
            catch (Exception ex)
            {

                return (false, ex.Message, default);
            }

        }

        public virtual (bool rt, string msgErr, string value) Read(MelsecFxSerial PlcNet, string address, ushort length, Encoding encoding)
        {
            try
            {

                OperateResult<string> result = PlcNet.ReadString(address, length, encoding);
                return result.IsSuccess ?
                      (true, "", result.Content) :
                      (false, result.Message, "");

            }
            catch (Exception ex)
            {

                return (false, ex.Message, default);
            }
        }
        public virtual (bool rt, string msgErr, string value) Read(MelsecFxSerial PlcNet, string address, ushort length)
        {
            try
            {

                OperateResult<string> result = PlcNet.ReadString(address, length);
                return result.IsSuccess ?
                      (true, "", result.Content) :
                      (false, result.Message, "");

            }
            catch (Exception ex)
            {

                return (false, ex.Message, default);
            }
        }

        public virtual async Task<(bool rt, string msgErr, T value)> ReadAsync<T>(MelsecFxSerial PlcNet, string address)
        {
            try
            {


                Type type = typeof(T);
                if (type == typeof(bool))
                {
                    OperateResult<bool> result = await PlcNet.ReadBoolAsync(address);
                    return result.IsSuccess ?
                          (true, "", (T)(object)result.Content) :
                          (false, result.Message, default);
                }
                else if (type == typeof(short))
                {
                    OperateResult<short> result = await PlcNet.ReadInt16Async(address);
                    return result.IsSuccess ?
                           (true, "", (T)(object)result.Content) :
                           (false, result.Message, default);
                }
                else if (type == typeof(ushort))
                {
                    OperateResult<ushort> result = await PlcNet.ReadUInt16Async(address);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }
                else if (type == typeof(int))
                {
                    OperateResult<int> result = await PlcNet.ReadInt32Async(address);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }
                else if (type == typeof(uint))
                {
                    OperateResult<uint> result = await PlcNet.ReadUInt32Async(address);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }
                else if (type == typeof(long))
                {
                    OperateResult<long> result = await PlcNet.ReadInt64Async(address);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }
                else if (type == typeof(ulong))
                {
                    OperateResult<ulong> result = await PlcNet.ReadUInt64Async(address);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }
                else if (type == typeof(float))
                {
                    OperateResult<float> result = await PlcNet.ReadFloatAsync(address);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }
                else if (type == typeof(double))
                {
                    OperateResult<double> result = await PlcNet.ReadDoubleAsync(address);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }


                else
                {
                    return (false, "Not Object", default);
                }
            }
            catch (Exception ex)
            {

                return (false, ex.Message, default);
            }
        }

        public virtual async Task<(bool rt, string msgErr, T value)> ReadAsync<T>(MelsecFxSerial PlcNet, string address, ushort length)
        {
            try
            {

                Type type = typeof(T);
                if (type == typeof(bool[]))
                {
                    OperateResult<bool[]> result = await PlcNet.ReadBoolAsync(address, length);
                    return result.IsSuccess ?
                          (true, "", (T)(object)result.Content) :
                          (false, result.Message, default);
                }
                else if (type == typeof(short[]))
                {
                    OperateResult<short[]> result = await PlcNet.ReadInt16Async(address, length);
                    return result.IsSuccess ?
                           (true, "", (T)(object)result.Content) :
                           (false, result.Message, default);
                }
                else if (type == typeof(ushort[]))
                {
                    OperateResult<ushort[]> result = await PlcNet.ReadUInt16Async(address, length);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }
                else if (type == typeof(int[]))
                {
                    OperateResult<int[]> result = await PlcNet.ReadInt32Async(address, length);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }
                else if (type == typeof(uint[]))
                {
                    OperateResult<uint[]> result = await PlcNet.ReadUInt32Async(address, length);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }
                else if (type == typeof(long[]))
                {
                    OperateResult<long[]> result = await PlcNet.ReadInt64Async(address, length);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }
                else if (type == typeof(ulong[]))
                {
                    OperateResult<ulong[]> result = await PlcNet.ReadUInt64Async(address, length);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }
                else if (type == typeof(float[]))
                {
                    OperateResult<float[]> result = await PlcNet.ReadFloatAsync(address, length);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }
                else if (type == typeof(double[]))
                {
                    OperateResult<double[]> result = await PlcNet.ReadDoubleAsync(address, length);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }




                else
                {
                    return (false, "Not Object", default);
                }
            }
            catch (Exception ex)
            {

                return (false, ex.Message, default);
            }
        }

        public virtual async Task<(bool rt, string msgErr, string value)> ReadAsync(MelsecFxSerial PlcNet, string address, ushort length, Encoding encoding)
        {
            try
            {

                OperateResult<string> result = await PlcNet.ReadStringAsync(address, length, encoding);
                return result.IsSuccess ?
                      (true, "", result.Content) :
                      (false, result.Message, "");

            }
            catch (Exception ex)
            {

                return (false, ex.Message, default);
            }
        }
        public virtual async Task<(bool rt, string msgErr, string value)> ReadAsync(MelsecFxSerial PlcNet, string address, ushort length )
        {
            try
            {

                OperateResult<string> result = await PlcNet.ReadStringAsync(address, length);
                return result.IsSuccess ?
                      (true, "", result.Content) :
                      (false, result.Message, "");

            }
            catch (Exception ex)
            {

                return (false, ex.Message, default);
            }
        }


        #endregion

        #region ModbusTcp


        public virtual (bool rt, string msgErr, T value) Read<T>(ModbusTcpNet PlcNet, string address)
        {
            try
            {

                Type type = typeof(T);
                if (type == typeof(bool))
                {
                    OperateResult<bool> result = PlcNet.ReadBool(address);
                    return result.IsSuccess ?
                          (true, "", (T)(object)result.Content) :
                          (false, result.Message, default);
                }
                else if (type == typeof(short))
                {
                    OperateResult<short> result = PlcNet.ReadInt16(address);
                    return result.IsSuccess ?
                           (true, "", (T)(object)result.Content) :
                           (false, result.Message, default);
                }
                else if (type == typeof(ushort))
                {
                    OperateResult<ushort> result = PlcNet.ReadUInt16(address);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }
                else if (type == typeof(int))
                {
                    OperateResult<int> result = PlcNet.ReadInt32(address);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }
                else if (type == typeof(uint))
                {
                    OperateResult<uint> result = PlcNet.ReadUInt32(address);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }
                else if (type == typeof(long))
                {
                    OperateResult<long> result = PlcNet.ReadInt64(address);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }
                else if (type == typeof(ulong))
                {
                    OperateResult<ulong> result = PlcNet.ReadUInt64(address);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }
                else if (type == typeof(float))
                {
                    OperateResult<float> result = PlcNet.ReadFloat(address);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }
                else if (type == typeof(double))
                {
                    OperateResult<double> result = PlcNet.ReadDouble(address);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }


                else
                {
                    return (false, "Not Object", default);
                }
            }
            catch (Exception ex)
            {

                return (false, ex.Message, default);
            }
        }

        public virtual (bool rt, string msgErr, T value) Read<T>(ModbusTcpNet PlcNet, string address, ushort length)
        {
            try
            {


                Type type = typeof(T);
                if (type == typeof(bool[]))
                {
                    OperateResult<bool[]> result = PlcNet.ReadBool(address, length);
                    return result.IsSuccess ?
                          (true, "", (T)(object)result.Content) :
                          (false, result.Message, default);
                }
                else if (type == typeof(short[]))
                {
                    OperateResult<short[]> result = PlcNet.ReadInt16(address, length);
                    return result.IsSuccess ?
                           (true, "", (T)(object)result.Content) :
                           (false, result.Message, default);
                }
                else if (type == typeof(ushort[]))
                {
                    OperateResult<ushort[]> result = PlcNet.ReadUInt16(address, length);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }
                else if (type == typeof(int[]))
                {
                    OperateResult<int[]> result = PlcNet.ReadInt32(address, length);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }
                else if (type == typeof(uint[]))
                {
                    OperateResult<uint[]> result = PlcNet.ReadUInt32(address, length);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }
                else if (type == typeof(long[]))
                {
                    OperateResult<long[]> result = PlcNet.ReadInt64(address, length);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }
                else if (type == typeof(ulong[]))
                {
                    OperateResult<ulong[]> result = PlcNet.ReadUInt64(address, length);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }
                else if (type == typeof(float[]))
                {
                    OperateResult<float[]> result = PlcNet.ReadFloat(address, length);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }
                else if (type == typeof(double[]))
                {
                    OperateResult<double[]> result = PlcNet.ReadDouble(address, length);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }

                else
                {
                    return (false, "Not Object", default);
                }
            }
            catch (Exception ex)
            {

                return (false, ex.Message, default);
            }

        }

        public virtual (bool rt, string msgErr, string value) Read(ModbusTcpNet PlcNet, string address, ushort length, Encoding encoding)
        {
            try
            {

                OperateResult<string> result = PlcNet.ReadString(address, length, encoding);
                return result.IsSuccess ?
                      (true, "", result.Content) :
                      (false, result.Message, "");

            }
            catch (Exception ex)
            {

                return (false, ex.Message, default);
            }
        }
        public virtual (bool rt, string msgErr, string value) Read(ModbusTcpNet PlcNet, string address, ushort length )
        {
            try
            {

                OperateResult<string> result = PlcNet.ReadString(address, length );
                return result.IsSuccess ?
                      (true, "", result.Content) :
                      (false, result.Message, "");

            }
            catch (Exception ex)
            {

                return (false, ex.Message, default);
            }
        }



        public virtual async Task<(bool rt, string msgErr, T value)> ReadAsync<T>(ModbusTcpNet PlcNet, string address)
        {
            try
            {


                Type type = typeof(T);
                if (type == typeof(bool))
                {
                    OperateResult<bool> result = await PlcNet.ReadBoolAsync(address);
                    return result.IsSuccess ?
                          (true, "", (T)(object)result.Content) :
                          (false, result.Message, default);
                }
                else if (type == typeof(short))
                {
                    OperateResult<short> result = await PlcNet.ReadInt16Async(address);
                    return result.IsSuccess ?
                           (true, "", (T)(object)result.Content) :
                           (false, result.Message, default);
                }
                else if (type == typeof(ushort))
                {
                    OperateResult<ushort> result = await PlcNet.ReadUInt16Async(address);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }
                else if (type == typeof(int))
                {
                    OperateResult<int> result = await PlcNet.ReadInt32Async(address);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }
                else if (type == typeof(uint))
                {
                    OperateResult<uint> result = await PlcNet.ReadUInt32Async(address);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }
                else if (type == typeof(long))
                {
                    OperateResult<long> result = await PlcNet.ReadInt64Async(address);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }
                else if (type == typeof(ulong))
                {
                    OperateResult<ulong> result = await PlcNet.ReadUInt64Async(address);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }
                else if (type == typeof(float))
                {
                    OperateResult<float> result = await PlcNet.ReadFloatAsync(address);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }
                else if (type == typeof(double))
                {
                    OperateResult<double> result = await PlcNet.ReadDoubleAsync(address);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }


                else
                {
                    return (false, "Not Object", default);
                }
            }
            catch (Exception ex)
            {

                return (false, ex.Message, default);
            }
        }

        public virtual async Task<(bool rt, string msgErr, T value)> ReadAsync<T>(ModbusTcpNet PlcNet, string address, ushort length)
        {
            try
            {

                Type type = typeof(T);
                if (type == typeof(bool[]))
                {
                    OperateResult<bool[]> result = await PlcNet.ReadBoolAsync(address, length);
                    return result.IsSuccess ?
                          (true, "", (T)(object)result.Content) :
                          (false, result.Message, default);
                }
                else if (type == typeof(short[]))
                {
                    OperateResult<short[]> result = await PlcNet.ReadInt16Async(address, length);
                    return result.IsSuccess ?
                           (true, "", (T)(object)result.Content) :
                           (false, result.Message, default);
                }
                else if (type == typeof(ushort[]))
                {
                    OperateResult<ushort[]> result = await PlcNet.ReadUInt16Async(address, length);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }
                else if (type == typeof(int[]))
                {
                    OperateResult<int[]> result = await PlcNet.ReadInt32Async(address, length);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }
                else if (type == typeof(uint[]))
                {
                    OperateResult<uint[]> result = await PlcNet.ReadUInt32Async(address, length);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }
                else if (type == typeof(long[]))
                {
                    OperateResult<long[]> result = await PlcNet.ReadInt64Async(address, length);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }
                else if (type == typeof(ulong[]))
                {
                    OperateResult<ulong[]> result = await PlcNet.ReadUInt64Async(address, length);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }
                else if (type == typeof(float[]))
                {
                    OperateResult<float[]> result = await PlcNet.ReadFloatAsync(address, length);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }
                else if (type == typeof(double[]))
                {
                    OperateResult<double[]> result = await PlcNet.ReadDoubleAsync(address, length);
                    return result.IsSuccess ?
                            (true, "", (T)(object)result.Content) :
                            (false, result.Message, default);
                }




                else
                {
                    return (false, "Not Object", default);
                }
            }
            catch (Exception ex)
            {

                return (false, ex.Message, default);
            }
        }

        public virtual async Task<(bool rt, string msgErr, string value)> ReadAsync(ModbusTcpNet PlcNet, string address, ushort length, Encoding encoding)
        {
            try
            {

            OperateResult<string> result = await PlcNet.ReadStringAsync(address, length, encoding);
            return result.IsSuccess ?
                  (true, "", result.Content) :
                  (false, result.Message, "");

            }
            catch (Exception ex) 
            {

               return (false ,ex.Message, default);
            }
        }

        public virtual async Task<(bool rt, string msgErr, string value)> ReadAsync(ModbusTcpNet PlcNet, string address, ushort length )
        {
            try
            {

                OperateResult<string> result = await PlcNet.ReadStringAsync(address, length);
                return result.IsSuccess ?
                      (true, "", result.Content) :
                      (false, result.Message, "");

            }
            catch (Exception ex)
            {

                return (false, ex.Message, default);
            }
        }


        #endregion




    }
}
