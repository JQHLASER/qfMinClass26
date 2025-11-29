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
    public class Label_
    {
        public Label _Label;
        public Label_(Label label_)
        {           
            label_.Content = this._Conftent;
            label_.FontFamily = this._FontFamily;
            label_.FontSize = this.FontSize;
            label_.HorizontalAlignment = this._HorizontalAlignment;
            label_.HorizontalContentAlignment = this._HorizontalContentAlignment;
            label_.VerticalAlignment = this._VerticalAlignment;
            label_.VerticalContentAlignment = this._VerticalContentAlignment;
            label_.Foreground = this._Foreground;

            this._Label = label_;
        }

        public string _Conftent = "Label";
        public FontFamily _FontFamily = new FontFamily("微软雅黑");
        public double FontSize = 14;
        public HorizontalAlignment _HorizontalAlignment = HorizontalAlignment.Stretch;
        public HorizontalAlignment _HorizontalContentAlignment = HorizontalAlignment.Center;
        public VerticalAlignment _VerticalContentAlignment = VerticalAlignment.Stretch;
        public VerticalAlignment _VerticalAlignment = VerticalAlignment.Center;
        public Brush _Foreground = Brushes.Gray;




    }
}
