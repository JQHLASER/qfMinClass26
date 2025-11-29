using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfmain 
{
    public  class 数组
    {

        /// <summary>
        /// 重载
        /// </summary>
        /// <param name="原数组"></param>
        /// <param name="要添加的数组"></param>
        /// <returns></returns>
        public virtual  byte[] 拼接(byte[] 原数组, byte[] 要添加的数组)
        {
            return 原数组.Concat(要添加的数组).ToArray();
        }

        /// <summary>
        /// 重载
        /// </summary>
        /// <param name="原数组"></param>
        /// <param name="要添加的数组"></param>
        /// <returns></returns>
        public virtual string[] 拼接(string[] 原数组, string[] 要添加的数组)
        {
            return 原数组.Concat(要添加的数组).ToArray();
        }

        /// <summary>
        /// 重载
        /// </summary>
        /// <param name="原数组"></param>
        /// <param name="要添加的数组"></param>
        /// <returns></returns>
        public virtual int[] 拼接(int[] 原数组, int[] 要添加的数组)
        {
            return 原数组.Concat(要添加的数组).ToArray();
        }


        /// <summary>
        /// 重载
        /// </summary>
        /// <param name="原数组"></param>
        /// <param name="新数组"></param>
        /// <returns></returns>
        public virtual double[] 拼接(double[] 原数组, double[] 要添加的数组)
        {
            return 原数组.Concat(要添加的数组).ToArray();
        }

        /// <summary>
        /// 重载
        /// </summary>
        /// <param name="原数组"></param>
        /// <param name="要添加的数组"></param>
        /// <returns></returns>
        public virtual float[] 拼接(float[] 原数组, float[] 要添加的数组)
        {
            return 原数组.Concat(要添加的数组).ToArray();
        }

        /// <summary>
        /// 重载
        /// </summary>
        /// <param name="原数组"></param>
        /// <param name="要添加的数组"></param>
        /// <returns></returns>
        public virtual long[] 拼接(long[] 原数组, long[] 要添加的数组)
        {
            return 原数组.Concat(要添加的数组).ToArray();
        }

        /// <summary>
        /// 重载
        /// </summary>
        /// <param name="原数组"></param>
        /// <param name="要添加的数组"></param>
        /// <returns></returns>
        public virtual bool[] 拼接(bool[] 原数组, bool[] 要添加的数组)
        {
            return 原数组.Concat(要添加的数组).ToArray();
        }












        public virtual byte[] 数组_byte压缩(byte[] data)
        {
            MemoryStream ms = new MemoryStream();
            Stream zipStream = null;
            zipStream = new GZipStream(ms, CompressionMode.Compress, true);
            zipStream.Write(data, 0, data.Length);
            zipStream.Close();
            ms.Position = 0;
            byte[] compressed_data = new byte[ms.Length];
            ms.Read(compressed_data, 0, int.Parse(ms.Length.ToString()));
            return compressed_data;
        }

        public virtual byte[] 数组_byte解压(byte[] data)
        {
            try
            {
                MemoryStream ms = new MemoryStream(data);
                Stream zipStream = null;
                zipStream = new GZipStream(ms, CompressionMode.Decompress);
                byte[] dc_data = null;
                dc_data = EtractBytesFormStream(zipStream, data.Length);
                return dc_data;
            }
            catch
            {
                return null;
            }
        }
        private   byte[] EtractBytesFormStream(Stream zipStream, int dataBlock)
        {
            try
            {
                byte[] data = null;
                int totalBytesRead = 0;
                while (true)
                {
                    Array.Resize(ref data, totalBytesRead + dataBlock + 1);
                    int bytesRead = zipStream.Read(data, totalBytesRead, dataBlock);
                    if (bytesRead == 0)
                    {
                        break;
                    }
                    totalBytesRead += bytesRead;
                }
                Array.Resize(ref data, totalBytesRead);
                return data;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 压缩数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public virtual byte[] 数组_byte压缩_1(byte[] data)
        {
            byte[] bData;
            MemoryStream ms = new MemoryStream();
            GZipStream stream = new GZipStream(ms, CompressionMode.Compress, true);
            stream.Write(data, 0, data.Length);
            stream.Close();
            stream.Dispose();
            //必须把stream流关闭才能返回ms流数据,不然数据会不完整
            //并且解压缩方法stream.Read(buffer, 0, buffer.Length)时会返回0
            bData = ms.ToArray();
            ms.Close();
            ms.Dispose();
            return bData;
        }

        /// <summary>
        /// 解压数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// 
        public virtual byte[] 数组_byte解压_1(byte[] data)
        {
            byte[] bData;
            MemoryStream ms = new MemoryStream();
            ms.Write(data, 0, data.Length);
            ms.Position = 0;
            GZipStream stream = new GZipStream(ms, CompressionMode.Decompress, true);
            byte[] buffer = new byte[1024];
            MemoryStream temp = new MemoryStream();
            int read = stream.Read(buffer, 0, buffer.Length);
            while (read > 0)
            {
                temp.Write(buffer, 0, read);
                read = stream.Read(buffer, 0, buffer.Length);
            }
            //必须把stream流关闭才能返回ms流数据,不然数据会不完整
            stream.Close();
            stream.Dispose();
            ms.Close();
            ms.Dispose();
            bData = temp.ToArray();
            temp.Close();
            temp.Dispose();
            return bData;
        }


        public virtual byte[] 数组_byte压缩_2(byte[] bytes)
        {
            // byte[] bytes = socks.sys.文件转成byte数组(@"D:\1.txt");
            //// byte[]  et = socks.sys.压缩byte数组 (x);        
            //return x.Length ;


            // 压缩            
            MemoryStream oStream = new MemoryStream();
            DeflateStream zipStream = new DeflateStream(oStream, CompressionMode.Compress);
            zipStream.Write(bytes, 0, bytes.Length);
            zipStream.Flush();
            zipStream.Close();
            return oStream.ToArray();
        }

        public virtual byte[] 数组_byte解压_2(byte[] bytes)
        {
            // 初始化流，设置读取位置
            MemoryStream mStream = new System.IO.MemoryStream(bytes);
            mStream.Seek(0, SeekOrigin.Begin);
            // 解压缩
            //DeflateStream unZipStream = new DeflateStream(mStream, CompressionMode.Decompress, true);
            //// 反序列化得到数据集

            //DataSet dsResult = new DataSet();
            //dsResult.RemotingFormat = SerializationFormat.Binary;
            //BinaryFormatter bFormatter = new BinaryFormatter();
            //dsResult = (DataSet)bFormatter.Deserialize(unZipStream);
            return mStream.ToArray();
        }


    }
}
