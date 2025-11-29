using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfNet
{
    public class Color_
    {
        // 全局 Random 实例（避免重复创建导致颜色重复）
        private readonly Random _random = new Random();

        public Color 随机生成颜色()
        {
            // 随机生成 R、G、B 值（0-255）
            int r = _random.Next(0, 256);
            int b = _random.Next(0, 256);
            int g = _random.Next(0, 256);
            return Color.FromArgb(r, b, g);              
        }





    }
}
