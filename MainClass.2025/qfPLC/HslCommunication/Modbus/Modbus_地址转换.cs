using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace qfPLC
{
    public class Modbus_地址转换
    {
        public class 三菱_Fx5U
        {
            private readonly Regex _r =
                new Regex(@"^(?<dev>[A-Z]+)(?<addr>[0-9]+)$",
                    RegexOptions.IgnoreCase);


            /// <summary>
            /// FX5U → Modbus 地址转换器
            /// （严格按三菱软元件规则，内部 0 基）
            /// </summary>

            // 解析 FX 地址：如 X10、Y7、M100、D200
            private static readonly Regex _regex =
                new Regex(@"^(?<dev>[A-Z]+)(?<addr>[0-9]+)$",
                    RegexOptions.IgnoreCase | RegexOptions.Compiled);

            
            /// <summary>
            /// <para>FX5U 地址 → Modbus 地址</para>
            /// <para>fxAddress : FX 地址（如 Y10、M100）</para>
            /// <para>首地址从0开始 :Modbus 首地址偏移, 0 = 协议层（推荐）; 1 = 某些工具 / 触摸屏</para>
            /// </summary> 
            public int 转换(string fxAddress, bool 首地址从0开始)
            {
                if (string.IsNullOrWhiteSpace(fxAddress))
                    throw new ArgumentNullException("fxAddress");

                Match m = _regex.Match(fxAddress);
                if (!m.Success)
                    throw new FormatException("FX 地址格式错误");

                string dev = m.Groups["dev"].Value.ToUpper();
                string addrStr = m.Groups["addr"].Value;

                int baseAddr;

                switch (dev)
                {
                    // 八进制位设备
                    case "X":
                    case "Y":
                    case "B":
                        baseAddr = Convert.ToInt32(addrStr, 8);
                        break;

                    // 十进制位设备
                    case "M":
                    case "L":
                    case "F":
                        baseAddr = int.Parse(addrStr);
                        break;

                    // 字设备
                    case "D":
                    case "R":
                    case "W":
                        baseAddr = int.Parse(addrStr);
                        break;

                    default:
                        throw new NotSupportedException("不支持的 FX 设备：" + dev);
                }
                int 首地址 = 首地址从0开始 ? 0 : 1;
                return baseAddr + 首地址;
            }

            

        }



    }
}
