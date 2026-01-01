using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace qfPLC
{
    /// <summary>
    /// 地址转换,x=1;0 表示1站点,0的值
    /// </summary>
    public class Modbus_地址转换
    {
        public class 三菱_Fx5U
        {

            bool _is首地址从0开始 = true;
            public 三菱_Fx5U(bool 首地址从0开始)
            {
                this._is首地址从0开始 = 首地址从0开始;
            }


            /// <summary>
            /// 默认从0x2000(8192)开始,分配点数: 7680
            /// </summary> 
            public int M(ushort v)
            {
                int a = v + 8192;
                return this._is首地址从0开始 ? a : a + 1;
            }

            /// <summary>
            /// 默认从0x00000(0)开始,分配点数: 1024
            /// <para>组索引: 输入的值v属于哪个组,组索引从0开始,=0:0~7;=1:8~15...按此方法</para>
            /// <para>ReadDiscrete读离散线圈</para>
            /// </summary> 
            public int X(ushort v, ushort 组索引)
            {
                int a = v - 组索引 * 2;
                return this._is首地址从0开始 ? a + 0 : a + 0 + 1;
            }


            /// <summary>
            /// 默认从0x00000(0)开始,分配点数: 1024
            /// <para>组索引: 输入的值v属于哪个组,组索引从0开始,=0:0~7;=1:8~15...按此方法</para>
            /// <para>ReadDiscrete读离散线圈</para>
            /// </summary> 
            public int X(ushort v)
            {
                short 组索引 = 0;
                if (v >= 100)
                {
                    组索引 = 10;
                }
                else if (v >= 90)
                {
                    组索引 = 9;
                }
                else if (v >= 80)
                {
                    组索引 = 8;
                }
                else if (v >= 70)
                {
                    组索引 = 7;
                }
                else if (v >= 60)
                {
                    组索引 = 6;
                }
                else if (v >= 50)
                {
                    组索引 = 5;
                }
                else if (v >= 40)
                {
                    组索引 = 4;
                }
                else if (v >= 30)
                {
                    组索引 = 3;
                }
                else if (v >= 20)
                {
                    组索引 = 2;
                }
                else if (v >= 10)
                {
                    组索引 = 1;
                }
                else
                {
                    组索引 = 0;
                }

                int a = v - 组索引 * 2;
                return this._is首地址从0开始 ? a + 0 : a + 0 + 1;
            }



            /// <summary>
            /// 默认从0x00000(0)开始,分配点数: 1024
            /// <para>组索引: 输入的值v属于哪个组,组索引从0开始,=0:0~7;=1:8~15...按此方法</para>
            /// <para>ReadDiscrete读离散线圈</para>
            /// </summary> 
            public int Y(ushort v, ushort 组索引)
            {
                int a = v - 组索引 * 8;
                return this._is首地址从0开始 ? a + 0 : a + 0 + 1;
            }

            /// <summary>
            /// 默认从0x0000(0)开始,分配点数: 80000
            /// </summary> 
            public int D(ushort v)
            {
                return this._is首地址从0开始 ? v + 0 : v + 0 + 1;
            }



        }



    }
}
