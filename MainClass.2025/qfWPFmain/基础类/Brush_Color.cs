using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace qfWPFmain
{
    /// <summary>
    /// 颜色转换
    /// </summary>
    public class Brush_Color
    {
        /// <summary>
        /// System.Windows.Media.Color 颜色转成 brush
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public virtual Brush FromArgb(System.Windows.Media.Color color)
        {
            return new SolidColorBrush(color);
        }

        /// <summary>
        /// System.Drawing.Color 颜色转成 brush
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public virtual Brush FromColor(System.Drawing.Color color)
        {
            SolidColorBrush wpfBrush = new SolidColorBrush(Color.FromArgb(color.A, color.R, color.G, color.B));
            return wpfBrush;
        }

        /// <summary>
        /// "#FFCED201"
        /// <para>颜色字符串转成 color</para>
        /// </summary>
        /// <param name="BurshStr">如"#FFCED201"</param>
        /// <returns></returns>
        public virtual Brush StrToBursh(string ColorStr)
        {
            return FromArgb(StrToColor(ColorStr));
        }


        /// <summary>
        /// "#FFCED201"
        /// <para>颜色字符串转成 color</para>
        /// </summary>
        /// <param name="ColorStr">如"#FFCED201"</param>
        /// <returns></returns>
        public virtual Color StrToColor(string ColorStr)
        {
            return (Color)ColorConverter.ConvertFromString(ColorStr);
        }


        /// <summary>
        /// Color.FromArgb(0xFF, 0xCE, 0xD2, 0x01)
        /// <para>颜色byte 转成 color</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public virtual Color byteToColor(byte a, byte r, byte g, byte b)
        {
            return Color.FromArgb(a, r, g, b);
        }

         


    }
}
