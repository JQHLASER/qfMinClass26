using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mainclassqf
{
    public class 颜色
    {
        public uint 转换_RGB_To_uint(Color color)
        {
            return (uint)(((uint)color.B << 16) | (ushort)(((ushort)color.G << 8) | color.R));
        }

        public Color 转换_uint_To_RGB(int color)
        {
            int r = 0xFF & color;
            int g = 0xFF00 & color;
            g >>= 8;
            int b = 0xFF0000 & color;
            b >>= 16;
            return Color.FromArgb(r, g, b);
        }




    }
}
