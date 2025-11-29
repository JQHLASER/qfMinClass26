using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfmain 
{
    public  class SerialPort_方法
    {
        /// <summary>
        /// 枚举系统中所有串口名称
        /// </summary>
        /// <returns></returns>
        public string[] Get_串口名称()
        {
            return SerialPort.GetPortNames();
        }


        /// <summary>
        /// 枚举校验位
        /// </summary>
        /// <returns></returns>
        public string[] Get_校验位()
        {
            return Enum.GetNames(typeof(Parity));
        }

        /// <summary>
        /// 枚举停止位
        /// </summary>
        /// <returns></returns>
        public string[] Get_停止位()
        {
            return Enum.GetNames(typeof(StopBits));
        }

        /// <summary>
        /// 枚举数据位
        /// </summary>
        /// <returns></returns>
        public int[] Get_数据位()
        {
            return new int[] { 5, 6, 7, 8 };
        }

        /// <summary>
        /// 枚举波特率
        /// </summary>
        /// <returns></returns>
        public int[] Get_波特率()
        {
            return new int[] { 110, 300, 600, 1200, 2400, 4800, 9600, 14400, 19200, 38400, 56000, 57600, 115200 };
        }

    }
}
