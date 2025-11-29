using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace qfWPFmain 
{
    /// <summary>
    /// 裁剪控件
    /// </summary>
    public class Clip_
    {
        /// <summary>
        /// this.Button.Clip =  圆角矩形(double width,double height,double radiusX,double  radiusY,double left=0,double top=0)
        /// </summary>
        /// <param name="control"></param>
        /// <param name="半径"></param>
        /// <returns></returns>
        public virtual RectangleGeometry 圆角矩形(double width, double height, double radiusX, double radiusY, double left = 0, double top = 0)
        {
            return 圆角矩形(new Rect(left, top, width, height), radiusX, radiusY);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rect">[left,top,width,height]</param>
        /// <param name="radiusX"></param>
        /// <param name="radiusY"></param>
        /// <returns></returns>
        public RectangleGeometry 圆角矩形(Rect rect, double radiusX, double radiusY)
        {
            return new RectangleGeometry(rect, radiusX, radiusY);
        }


        /// <summary>
        /// this.Button.Clip =  椭圆(Point center, double radiusX, double radiusY)
        /// </summary>
        /// <param name="中心x"></param>
        /// <param name="中心y"></param>
        /// <param name="radiusX"></param>
        /// <param name="radiusY"></param>
        /// <returns></returns>
        public virtual EllipseGeometry 椭圆(double 中心x, double 中心y, double radiusX, double radiusY)
        {
            return 椭圆(new Point(中心x, 中心y), radiusX, radiusY);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="center">[中心x,中心y]</param>
        /// <param name="radiusX">半径x</param>
        /// <param name="radiusY">半径y</param>
        /// <returns></returns>
        public virtual EllipseGeometry 椭圆(Point center, double radiusX, double radiusY)
        {
            return new EllipseGeometry(center, radiusX, radiusY);
        }




    }
}
