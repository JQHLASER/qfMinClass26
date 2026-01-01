using HslCommunication;
using HslCommunication.ModBus;
using HslCommunication.Profinet.Melsec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfPLC
{
    internal class WritePlc
    {
        #region 三菱_MC_Ascii_Qna3E

        public (bool rt, string msgErr) Write<T>(MelsecMcAsciiNet PlcNet, string address, T value)  
        {
            try
            {

                OperateResult result;
                Type type = typeof(T);

                if (value is bool boola)
                {
                    result = PlcNet.Write(address, boola);

                }
                else if (value is short shorta)
                {
                    result = PlcNet.Write(address, shorta);
                }
                else if (value is ushort ushorta)
                {
                    result = PlcNet.Write(address, ushorta);
                }
                else if (value is int inta)
                {
                    result = PlcNet.Write(address, inta);
                }
                else if (value is uint uinta)
                {
                    result = PlcNet.Write(address, uinta);
                }
                else if (value is long longa)
                {
                    result = PlcNet.Write(address, longa);
                }
                else if (value is ulong ulonga)
                {
                    result = PlcNet.Write(address, ulonga);
                }
                else if (value is float floata)
                {
                    result = PlcNet.Write(address, floata);
                }
                else if (value is double doublea)
                {
                    result = PlcNet.Write(address, doublea);
                }


                else if (value is bool[] bools)
                {
                    result = PlcNet.Write(address, bools);

                }
                else if (value is short[] shorts)
                {
                    result = PlcNet.Write(address, shorts);
                }
                else if (value is ushort[] ushorts)
                {
                    result = PlcNet.Write(address, ushorts);
                }
                else if (value is int[] ints)
                {
                    result = PlcNet.Write(address, ints);
                }
                else if (value is uint[] uints)
                {
                    result = PlcNet.Write(address, uints);
                }
                else if (value is long[] longs)
                {
                    result = PlcNet.Write(address, longs);
                }
                else if (value is ulong[] ulongs)
                {
                    result = PlcNet.Write(address, ulongs);
                }
                else if (value is float[] floats)
                {
                    result = PlcNet.Write(address, floats);
                }
                else if (value is double[] doubles)
                {
                    result = PlcNet.Write(address, doubles);
                }
                else if (value is byte[] bytes)
                {
                    result = PlcNet.Write(address, bytes);

                }


                else
                {
                    return (false, "Not Object");
                }

                return new 解析().OperateResult(result);

            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
        public (bool rt, string msgErr) Write(MelsecMcAsciiNet PlcNet, string address, string value)
        {
            try
            {

            OperateResult result;
            result = PlcNet.Write(address, value);
            return new 解析().OperateResult(result);

            }
            catch (Exception ex)
            {
                return (false ,ex.Message );               
            }
        }
        public (bool rt, string msgErr) Write(MelsecMcAsciiNet PlcNet, Encoding encoding, string address, string value)
        {
            try
            {

            OperateResult result;
            result = PlcNet.Write(address, value, encoding);
            return new 解析().OperateResult(result);

            }
            catch (Exception ex)
            {

                return (false ,ex.Message);
            }
        }



        public async Task<(bool rt, string msgErr)> WriteAsync<T>(MelsecMcAsciiNet PlcNet, string address, T value) 
        {
            try
            {

                OperateResult result;
                Type type = typeof(T);

                if (value is bool boola)
                {
                    result = PlcNet.Write(address, boola);

                }
                else if (value is short shorta)
                {
                    result = PlcNet.Write(address, shorta);
                }
                else if (value is ushort ushorta)
                {
                    result = PlcNet.Write(address, ushorta);
                }
                else if (value is int inta)
                {
                    result = PlcNet.Write(address, inta);
                }
                else if (value is uint uinta)
                {
                    result = PlcNet.Write(address, uinta);
                }
                else if (value is long longa)
                {
                    result = PlcNet.Write(address, longa);
                }
                else if (value is ulong ulonga)
                {
                    result = PlcNet.Write(address, ulonga);
                }
                else if (value is float floata)
                {
                    result = PlcNet.Write(address, floata);
                }
                else if (value is double doublea)
                {
                    result = PlcNet.Write(address, doublea);
                }


                else if (value is bool[] bools)
                {
                    result = PlcNet.Write(address, bools);

                }
                else if (value is short[] shorts)
                {
                    result = PlcNet.Write(address, shorts);
                }
                else if (value is ushort[] ushorts)
                {
                    result = PlcNet.Write(address, ushorts);
                }
                else if (value is int[] ints)
                {
                    result = PlcNet.Write(address, ints);
                }
                else if (value is uint[] uints)
                {
                    result = PlcNet.Write(address, uints);
                }
                else if (value is long[] longs)
                {
                    result = PlcNet.Write(address, longs);
                }
                else if (value is ulong[] ulongs)
                {
                    result = PlcNet.Write(address, ulongs);
                }
                else if (value is float[] floats)
                {
                    result = PlcNet.Write(address, floats);
                }
                else if (value is double[] doubles)
                {
                    result = PlcNet.Write(address, doubles);
                }
                else if (value is byte[] bytes)
                {
                    result = PlcNet.Write(address, bytes);

                }


                else
                {
                    return (false, "Not Object");
                }

                return new 解析().OperateResult(result);

            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
        public async Task<(bool rt, string msgErr)> WriteAsync(MelsecMcAsciiNet PlcNet, string address, string value)
        {
            try
            {

            OperateResult result;
            result = await PlcNet.WriteAsync(address, value);
            return new 解析().OperateResult(result);
            }
            catch (Exception ex)
            {

               return (true, ex.Message);
            }
        }
        public async Task<(bool rt, string msgErr)> WriteAsync(MelsecMcAsciiNet PlcNet, Encoding encoding, string address, string value)
        {
            try
            {

            OperateResult result;
            result = await PlcNet.WriteAsync(address, value, encoding);
            return new 解析().OperateResult(result);

            }
            catch (Exception ex)
            {
              return (false, ex.Message);
            }
        }


        #endregion


        #region 三菱_FX

        public (bool rt, string msgErr) Write<T>(MelsecFxSerial PlcNet, string address, T value)  
        {
            try
            {

                OperateResult result;
                Type type = typeof(T);

                if (value is bool boola)
                {
                    result = PlcNet.Write(address, boola);

                }
                else if (value is short shorta)
                {
                    result = PlcNet.Write(address, shorta);
                }
                else if (value is ushort ushorta)
                {
                    result = PlcNet.Write(address, ushorta);
                }
                else if (value is int inta)
                {
                    result = PlcNet.Write(address, inta);
                }
                else if (value is uint uinta)
                {
                    result = PlcNet.Write(address, uinta);
                }
                else if (value is long longa)
                {
                    result = PlcNet.Write(address, longa);
                }
                else if (value is ulong ulonga)
                {
                    result = PlcNet.Write(address, ulonga);
                }
                else if (value is float floata)
                {
                    result = PlcNet.Write(address, floata);
                }
                else if (value is double doublea)
                {
                    result = PlcNet.Write(address, doublea);
                }


                else if (value is bool[] bools)
                {
                    result = PlcNet.Write(address, bools);

                }
                else if (value is short[] shorts)
                {
                    result = PlcNet.Write(address, shorts);
                }
                else if (value is ushort[] ushorts)
                {
                    result = PlcNet.Write(address, ushorts);
                }
                else if (value is int[] ints)
                {
                    result = PlcNet.Write(address, ints);
                }
                else if (value is uint[] uints)
                {
                    result = PlcNet.Write(address, uints);
                }
                else if (value is long[] longs)
                {
                    result = PlcNet.Write(address, longs);
                }
                else if (value is ulong[] ulongs)
                {
                    result = PlcNet.Write(address, ulongs);
                }
                else if (value is float[] floats)
                {
                    result = PlcNet.Write(address, floats);
                }
                else if (value is double[] doubles)
                {
                    result = PlcNet.Write(address, doubles);
                }
                else if (value is byte[] bytes)
                {
                    result = PlcNet.Write(address, bytes);

                }


                else
                {
                    return (false, "Not Object");
                }

                return new 解析().OperateResult(result);

            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
        public (bool rt, string msgErr) Write(MelsecFxSerial PlcNet, string address, string value)
        {
            try
            {

                OperateResult result;
                result = PlcNet.Write(address, value);
                return new 解析().OperateResult(result);

            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
        public (bool rt, string msgErr) Write(MelsecFxSerial PlcNet, Encoding encoding, string address, string value)
        {
            try
            {

                OperateResult result;
                result = PlcNet.Write(address, value, encoding);
                return new 解析().OperateResult(result);

            }
            catch (Exception ex)
            {

                return (false, ex.Message);
            }
        }



        public async Task<(bool rt, string msgErr)> WriteAsync<T>(MelsecFxSerial PlcNet, string address, T value)  
        {
            try
            {

                OperateResult result;
                Type type = typeof(T);

                if (value is bool boola)
                {
                    result = PlcNet.Write(address, boola);

                }
                else if (value is short shorta)
                {
                    result = PlcNet.Write(address, shorta);
                }
                else if (value is ushort ushorta)
                {
                    result = PlcNet.Write(address, ushorta);
                }
                else if (value is int inta)
                {
                    result = PlcNet.Write(address, inta);
                }
                else if (value is uint uinta)
                {
                    result = PlcNet.Write(address, uinta);
                }
                else if (value is long longa)
                {
                    result = PlcNet.Write(address, longa);
                }
                else if (value is ulong ulonga)
                {
                    result = PlcNet.Write(address, ulonga);
                }
                else if (value is float floata)
                {
                    result = PlcNet.Write(address, floata);
                }
                else if (value is double doublea)
                {
                    result = PlcNet.Write(address, doublea);
                }


                else if (value is bool[] bools)
                {
                    result = PlcNet.Write(address, bools);

                }
                else if (value is short[] shorts)
                {
                    result = PlcNet.Write(address, shorts);
                }
                else if (value is ushort[] ushorts)
                {
                    result = PlcNet.Write(address, ushorts);
                }
                else if (value is int[] ints)
                {
                    result = PlcNet.Write(address, ints);
                }
                else if (value is uint[] uints)
                {
                    result = PlcNet.Write(address, uints);
                }
                else if (value is long[] longs)
                {
                    result = PlcNet.Write(address, longs);
                }
                else if (value is ulong[] ulongs)
                {
                    result = PlcNet.Write(address, ulongs);
                }
                else if (value is float[] floats)
                {
                    result = PlcNet.Write(address, floats);
                }
                else if (value is double[] doubles)
                {
                    result = PlcNet.Write(address, doubles);
                }
                else if (value is byte[] bytes)
                {
                    result = PlcNet.Write(address, bytes);

                }


                else
                {
                    return (false, "Not Object");
                }

                return new 解析().OperateResult(result);

            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
        public async Task<(bool rt, string msgErr)> WriteAsync(MelsecFxSerial PlcNet, string address, string value)
        {
            try
            {

                OperateResult result;
                result = await PlcNet.WriteAsync(address, value);
                return new 解析().OperateResult(result);
            }
            catch (Exception ex)
            {

                return (true, ex.Message);
            }
        }
        public async Task<(bool rt, string msgErr)> WriteAsync(MelsecFxSerial PlcNet, Encoding encoding, string address, string value)
        {
            try
            {

                OperateResult result;
                result = await PlcNet.WriteAsync(address, value, encoding);
                return new 解析().OperateResult(result);

            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }


        #endregion


        #region ModbusTcp

        public (bool rt, string msgErr) Write<T>(ModbusTcpNet PlcNet, string address, T value) 
        {
            try
            {

                OperateResult result;
                Type type = typeof(T);

                if (value is bool boola)
                {
                    result = PlcNet.Write(address, boola);

                }
                else if (value is short shorta)
                {
                    result = PlcNet.Write(address, shorta);
                }
                else if (value is ushort ushorta)
                {
                    result = PlcNet.Write(address, ushorta);
                }
                else if (value is int inta)
                {
                    result = PlcNet.Write(address, inta);
                }
                else if (value is uint uinta)
                {
                    result = PlcNet.Write(address, uinta);
                }
                else if (value is long longa)
                {
                    result = PlcNet.Write(address, longa);
                }
                else if (value is ulong ulonga)
                {
                    result = PlcNet.Write(address, ulonga);
                }
                else if (value is float floata)
                {
                    result = PlcNet.Write(address, floata);
                }
                else if (value is double doublea)
                {
                    result = PlcNet.Write(address, doublea);
                }


                else if (value is bool[] bools)
                {
                    result = PlcNet.Write(address, bools);

                }
                else if (value is short[] shorts)
                {
                    result = PlcNet.Write(address, shorts);
                }
                else if (value is ushort[] ushorts)
                {
                    result = PlcNet.Write(address, ushorts);
                }
                else if (value is int[] ints)
                {
                    result = PlcNet.Write(address, ints);
                }
                else if (value is uint[] uints)
                {
                    result = PlcNet.Write(address, uints);
                }
                else if (value is long[] longs)
                {
                    result = PlcNet.Write(address, longs);
                }
                else if (value is ulong[] ulongs)
                {
                    result = PlcNet.Write(address, ulongs);
                }
                else if (value is float[] floats)
                {
                    result = PlcNet.Write(address, floats);
                }
                else if (value is double[] doubles)
                {
                    result = PlcNet.Write(address, doubles);
                }
                else if (value is byte[] bytes)
                {
                    result = PlcNet.Write(address, bytes);

                }


                else
                {
                    return (false, "Not Object");
                }

                return new 解析().OperateResult(result);

            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
        public (bool rt, string msgErr) Write(ModbusTcpNet PlcNet, string address, string value)
        {
            try
            {

                OperateResult result;
                result = PlcNet.Write(address, value);
                return new 解析().OperateResult(result);

            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
        public (bool rt, string msgErr) Write(ModbusTcpNet PlcNet, Encoding encoding, string address, string value)
        {
            try
            {

                OperateResult result;
                result = PlcNet.Write(address, value, encoding);
                return new 解析().OperateResult(result);

            }
            catch (Exception ex)
            {

                return (false, ex.Message);
            }
        }

       
        public async Task<(bool rt, string msgErr)> WriteAsync<T>(ModbusTcpNet PlcNet, string address, T value)    
        {
            try
            {

                OperateResult result;
                Type type = typeof(T);

                if (value is bool boola)
                {
                    result = PlcNet.Write(address, boola);

                }
                else if (value is short shorta)
                {
                    result = PlcNet.Write(address, shorta);
                }
                else if (value is ushort ushorta)
                {
                    result = PlcNet.Write(address, ushorta);
                }
                else if (value is int inta)
                {
                    result = PlcNet.Write(address, inta);
                }
                else if (value is uint uinta)
                {
                    result = PlcNet.Write(address, uinta);
                }
                else if (value is long longa)
                {
                    result = PlcNet.Write(address, longa);
                }
                else if (value is ulong ulonga)
                {
                    result = PlcNet.Write(address, ulonga);
                }
                else if (value is float floata)
                {
                    result = PlcNet.Write(address, floata);
                }
                else if (value is double doublea)
                {
                    result = PlcNet.Write(address, doublea);
                }


                else if (value is bool[] bools)
                {
                    result = PlcNet.Write(address, bools);

                }
                else if (value is short[] shorts)
                {
                    result = PlcNet.Write(address, shorts);
                }
                else if (value is ushort[] ushorts)
                {
                    result = PlcNet.Write(address, ushorts);
                }
                else if (value is int[] ints)
                {
                    result = PlcNet.Write(address, ints);
                }
                else if (value is uint[] uints)
                {
                    result = PlcNet.Write(address, uints);
                }
                else if (value is long[] longs)
                {
                    result = PlcNet.Write(address, longs);
                }
                else if (value is ulong[] ulongs)
                {
                    result = PlcNet.Write(address, ulongs);
                }
                else if (value is float[] floats)
                {
                    result = PlcNet.Write(address, floats);
                }
                else if (value is double[] doubles)
                {
                    result = PlcNet.Write(address, doubles);
                }
                else if (value is byte[] bytes)
                {
                    result = PlcNet.Write(address, bytes);

                }


                else
                {
                    return (false, "Not Object");
                }

                return new 解析().OperateResult(result);

            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
        public async Task<(bool rt, string msgErr)> WriteAsync(ModbusTcpNet PlcNet, string address, string value)
        {
            try
            {

                OperateResult result;
                result = await PlcNet.WriteAsync(address, value);
                return new 解析().OperateResult(result);
            }
            catch (Exception ex)
            {

                return (true, ex.Message);
            }
        }
        public async Task<(bool rt, string msgErr)> WriteAsync(ModbusTcpNet PlcNet, Encoding encoding, string address, string value)
        {
            try
            {

                OperateResult result;
                result = await PlcNet.WriteAsync(address, value, encoding);
                return new 解析().OperateResult(result);

            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        #endregion








    }
}
