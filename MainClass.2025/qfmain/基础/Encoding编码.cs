using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfmain 
{
    /// <summary>
    /// 安装 System.Text.Encoding.CodePages  安装8.0的
    /// </summary>
    public  class Encoding编码
    {
             
        public enum enum编码
        {

            Default,
            UTF_7,
            UTF_8,
            UTF_16,
            UTF_32,
            Unicode,
            GB2312,
            GBK,
            BIG5,
            ISO_8859_1,
            ASCII,
            BigEndianUnicode,
        }



        /// <summary>
        /// 编码: =GB2312,GBK,支持中文
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="编码"></param>
        /// <returns></returns>
        public string bytesToString(byte[] bytes, enum编码 encoding = enum编码.GB2312)
        {

            return Encoding_编码(encoding).GetString(bytes);
        }


        /// <summary>
        /// 编码: =GB2312,GBK支持中文
        /// </summary>
        /// <param name="data"></param>
        /// <param name="编码"></param>
        /// <returns></returns>
        public byte[] stringToBytes(string data, enum编码 encoding = enum编码.GB2312)
        {

            return Encoding_编码(encoding).GetBytes(data);
        }

        /// <summary>
        /// 编码: =GB2312,GBK,支持中文
        /// </summary>
        /// <param name="编码"></param>
        /// <returns></returns>
        public Encoding Encoding_编码(enum编码 encoding = enum编码.GB2312)
        {


            Encoding endg = Encoding.Default;
            if (encoding == enum编码.GB2312 || encoding == enum编码.GBK || encoding == enum编码.BIG5 || encoding == enum编码.ISO_8859_1 || encoding == enum编码.UTF_16)
            {
               Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                string 编码 = $"{encoding}";
                switch (encoding)
                {
                    case enum编码.GB2312:
                        编码 = $"GB2312";
                        break;
                    case enum编码.GBK:
                        编码 = $"GBK";
                        break;
                    case enum编码.BIG5:
                        编码 = $"BIG5";
                        break;
                    case enum编码.ISO_8859_1:
                        编码 = $"ISO-8859-1";
                        break;
                    case enum编码.UTF_16:
                        编码 = $"UTF-16";
                        break;

                }

                endg = Encoding.GetEncoding(编码);
            }
            else
            {
                switch (encoding)
                {
                    case enum编码.Default:
                        endg = Encoding.Default;
                        break;
                    case enum编码.UTF_8:

                        break; endg = Encoding.UTF8;
                    case enum编码.UTF_7:
                        endg = Encoding.UTF7;
                        break;
                    case enum编码.UTF_32:
                        endg = Encoding.UTF32;
                        break;
                    case enum编码.Unicode:
                        endg = Encoding.Unicode;
                        break;
                    case enum编码.ASCII:
                        endg = Encoding.ASCII;
                        break;
                    case enum编码.BigEndianUnicode:
                        endg = Encoding.BigEndianUnicode;
                        break;
                }

            }

            return endg;

        }




    }
}
