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
            /// FX5U → Modbus 报文地址（严格按三菱规则）
            /// </summary>
            public int 转换(string fxAddress)
            {
                var m = _r.Match(fxAddress);
                if (!m.Success)
                    throw new FormatException("地址格式错误");

                string dev = m.Groups["dev"].Value.ToUpper();
                string addrStr = m.Groups["addr"].Value;

                switch (dev)
                {
                    // 八进制位设备
                    case "X":
                    case "Y":
                    case "B":
                        return Convert.ToInt32(addrStr, 8);

                    // 十进制位设备
                    case "M":
                    case "L":
                    case "F":
                        return int.Parse(addrStr);

                    // 字设备
                    case "D":
                    case "R":
                    case "W":
                        return int.Parse(addrStr);

                    default:
                        throw new NotSupportedException($"不支持设备：{dev}");
                }
            }
        }



    }
}
