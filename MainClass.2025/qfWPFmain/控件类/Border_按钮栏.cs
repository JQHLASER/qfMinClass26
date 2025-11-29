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
    /// 在窗体最底层的按钮栏
    /// </summary>
    public class Border_按钮栏
    {
        public Border_按钮栏(Border border_)
        {
          //  border_.Background = this._Background;
            border_.BorderBrush = this._BorderBrush;
            border_.BorderThickness = this._BorderThickness;

        }
        //背景颜色
        public Brush _Background = Brushes.White;
        public Brush _BorderBrush = Brushes.Silver;
        /// <summary>
        /// 边框宽度
        /// </summary>
        public Thickness _BorderThickness = new Thickness(0, 2, 0, 0);



    }
}
