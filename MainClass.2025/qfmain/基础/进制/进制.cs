using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfmain
{
    public class 进制
    {

        /// <summary>
        /// 低位在前,高位在后
        /// </summary>
        /// <param name="二进制位数">如8 或 16</param>
        /// <param name="十进制">十进制值</param>
        /// <returns></returns>
        public virtual string 十进制To二进制(int 二进制位数, int 十进制)
        {
            StringBuilder xt = new StringBuilder();
            for (int i = 0; i < 二进制位数; i++)
            {
                int nbit = 1 << i;//目标位值预置为高
                int nCurBitData = ((int)十进制 & nbit);//目标值与获取值做与运动，仅保留相同位数据

                bool bIsHigh = nCurBitData != 0;//数据为0，表明指定位获得值为0.

                string IOvalue = "0";
                if (bIsHigh)
                {
                    IOvalue = "1";
                }
                else
                {
                    IOvalue = "0";

                }
                xt.Append(IOvalue);
            }
            return xt.ToString();


        }

        public virtual string ByteTo十六进制(byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += bytes[i].ToString("X2");
                }
            }
            return returnStr;
        }

        public virtual byte[] 十六进制ToByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

        public virtual string stringTo十六进制(string str_, string 分隔符)
        {
            string text = "";

            byte[] b = Encoding.Default.GetBytes(str_);

            for (int i = 0; i < b.Length; i++)
            {
                text = text + b[i].ToString("x2") + 分隔符;
            }
            return text;

        }

        public virtual string 十六进制To二进制(int 十六进制)
        {
            return System.Convert.ToString(十六进制, 2);// d为int类型 以0X14为例，输出为10100
        }

        public virtual string 十六进制To二进制(string 十六进制)
        {
            string result = string.Empty;
            foreach (char c in 十六进制)
            {
                int v = Convert.ToInt32(c.ToString(), 16);
                int v2 = int.Parse(Convert.ToString(v, 2));
                // 去掉格式串中的空格，即可去掉每个4位二进制数之间的空格，
                result += string.Format("{0:d4} ", v2);
            }
            return result;


        }



        public virtual long 二进制To十进制(string 二进制)
        {
            return System.Convert.ToInt64(二进制, 2);// d为string类型 以“1010”为例，输出为10
        }


        public virtual string 十六进制To十进制(long 十六进制)
        {
            return System.Convert.ToString(十六进制, 10);// 以0XA为例，输出为10
        }

        public virtual long 十六进制To十进制(string 十六进制)
        {
            return System.Convert.ToInt64(十六进制, 16);//以"0x41"为例，输出为65
        }



        public virtual string 十进制To二进制(long 十进制, int 位数)
        {
            string value = System.Convert.ToString(十进制, 2);// d为int类型 以4为例，输出为100
            return value.PadLeft(位数, '0');
        }


        public virtual string 十进制To十六进制(long 十进制)
        {
            return Convert.ToString(十进制, 16);
        }
        public virtual string 十进制To二进制(long 十进制)
        {
            return Convert.ToString(十进制, 2);
        }


        public virtual string 十进制To十六进制_2位大写(long 十进制)
        {
            return 十进制.ToString("X2");
        }

        public virtual string 十进制To十六进制_2位小写(long 十进制)
        {
            return 十进制.ToString("x2");
        }


        /// <summary>
        /// 标识说明:x2表示小写2位,X2表示大写2位,后面的数字表示位数
        /// </summary>
        /// <param name="十进制"></param>
        /// <param name="标识"></param>
        /// <returns></returns>
        public virtual string 十进制To十六进制_自定义(long 十进制, string 标识)
        {
            return 十进制.ToString(标识);
        }





        public virtual string 十转十六进制(long 十进制)
        {
            return Convert.ToString(十进制, 16);
        }

        public virtual string 十转二进制(long 十进制)
        {
            return Convert.ToString(十进制, 2);
        }

        /// <summary>
        ///  value: true 表示H 即1,false 表示L 即0
        /// </summary>
        /// <param name="十进制"></param>
        /// <param name="位索引">即端口号</param>
        /// <returns></returns>
        public virtual bool 取指定位状态_十进制(int 十进制, int 位索引, int 最大位索引, out bool value, out string msgErr)
        {
            bool rt = true;
            value = false;
            msgErr = string.Empty;
            try
            {
                if (位索引 <= 最大位索引)
                {
                    int nbit = 1 << 位索引;//目标位值预置为高
                    int nCurBitData = ((int)十进制 & nbit);//目标值与获取值做与运动，仅保留相同位数据
                    value = nCurBitData != 0;//数据为0，表明指定位获得值为0.
                }
                else
                {
                    rt = false;
                    msgErr = Language_.Get语言("指定索引不能大于最大索引");
                }
            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }
            return value;

        }

        /// <summary>
        /// 返回修改后的十进制值
        /// </summary>
        /// <param name="十进制值">原来的十进制值</param>
        /// <param name="位索引">第几位,从0开始</param>
        /// <param name="tH_fL">true 为H状态,false 为L状态</param>
        /// <returns></returns>
        public virtual int 修改指定位状态_十进制(int 十进制值, int 位索引, bool status)
        {

            int nCurData = 十进制值;//原来的值
            int nAimData = 1 << 位索引;


            if (status)
            {
                nCurData |= nAimData; //H

            }
            else
            {
                nCurData &= ~nAimData;//L               
            }
            return nCurData;
        }

        /// <summary>
        /// 长度一般都为8,一般为8个一组
        /// </summary>
        /// <param name="value"></param>
        /// <param name="listBeff"></param>
        /// <param name="msgErr"></param>
        /// <param name="长度"></param>
        /// <returns></returns>
        public virtual bool 十进制转bool数组(byte value, out List<bool> listBeff, out string msgErr, int 数组长度 = 8)
        {
            bool rt = true;
            msgErr = string.Empty;
            listBeff = new List<bool>();
            try
            {
                // 分配足够长度的布尔数组
                // bool[] boolArray = new bool[8];         
                // 255的二进制表示是11111111，从最低位到最高位分别对应boolArray的第0位到第7位
                for (int i = 0; i < 数组长度; i++)
                {
                    // 通过位运算的方式判断对应位是否为1
                    bool rtValue = (value & (1 << i)) != 0;
                    listBeff.Add(rtValue);
                }
            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }

            return rt;
        }

        /// <summary>
        /// 长度一般都为8,一般为8个一组
        /// </summary>
        /// <param name="value"></param>
        /// <param name="listBeff"></param>
        /// <param name="msgErr"></param>
        /// <param name="长度"></param>
        /// <returns></returns>
        public virtual bool 十进制转bool数组(int value, out List<bool> listBeff, out string msgErr, int 数组长度 = 8)
        {
            bool rt = true;
            msgErr = string.Empty;
            listBeff = new List<bool>();
            try
            {
                // 分配足够长度的布尔数组
                // bool[] boolArray = new bool[8];         
                // 255的二进制表示是11111111，从最低位到最高位分别对应boolArray的第0位到第7位
                for (int i = 0; i < 数组长度; i++)
                {
                    // 通过位运算的方式判断对应位是否为1
                    bool rtValue = (value & (1 << i)) != 0;
                    listBeff.Add(rtValue);
                }
            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }

            return rt;
        }

        public virtual bool 端口是否有效(int portMin, int portMax, int port)
        {
            if (port < portMin || port > portMax)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 将int转换成bool[]
        /// </summary> 
        public virtual bool[] IntToBoolArray(uint value, uint length = 32)
        {
            bool[] result = new bool[length];
            for (int i = 0; i < length; i++)
            {
                result[i] = (value & (1 << i)) != 0;
            }

            return result;
        }

        /// <summary>
        /// 将bool[] 转换成 int
        /// </summary> 
        public virtual uint BoolArrayToInt(bool[] bits)
        { 
            int result = 0; 
            for (int i = 0; i < bits.Length; i++)
            {
                if (bits[i])
                {
                    result |= (1 << i);
                }
            }

            return (uint)result;
        }




    }
}
