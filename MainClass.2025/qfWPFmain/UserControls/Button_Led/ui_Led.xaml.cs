using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace qfWPFmain 
{
    /// <summary>
    /// ui_Led.xaml 的交互逻辑
    /// </summary>
    public partial class ui_Led : UserControl 
    {
        public ui_Led()
        {
            InitializeComponent();
        }

        #region 属性

        public static readonly DependencyProperty ui_TextProperty =
DependencyProperty.Register(nameof(ui_Text), typeof(string), typeof(ui_Led),
    new FrameworkPropertyMetadata("Led", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_Text_Changed));
        private static void On_ui_Text_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_Led control = d as ui_Led;

            // 将新值同步到UserControl内部的UI元素（如TextBlock）


            control._textblock.Text = e.NewValue.ToString();

        }


        public static readonly DependencyProperty ui_ForegroundProperty =
DependencyProperty.Register(nameof(ui_Foreground), typeof(Brush), typeof(ui_Led),
new FrameworkPropertyMetadata(Brushes.Black, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_Foreground_Changed));
        private static void On_ui_Foreground_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_Led control = d as ui_Led;

            // 将新值同步到UserControl内部的UI元素（如TextBlock）


            control._textblock.Foreground = (Brush)e.NewValue;


        }

      

        public static readonly DependencyProperty ui_BorderBrushProperty =
DependencyProperty.Register(nameof(ui_BorderBrush), typeof(Brush), typeof(ui_Led),
new FrameworkPropertyMetadata(Brushes.Silver, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_BorderBrush_Changed));
        private static void On_ui_BorderBrush_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_Led control = d as ui_Led;

            // 将新值同步到UserControl内部的UI元素（如TextBlock）

            control._Led_border.BorderBrush = (Brush)e.NewValue;


        }

        public static readonly DependencyProperty ui_FontSizeProperty =
DependencyProperty.Register(nameof(ui_FontSize), typeof(double), typeof(ui_Led),
new FrameworkPropertyMetadata(14d, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_FontSize_Changed));
        private static void On_ui_FontSize_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_Led control = d as ui_Led;
            control._textblock.FontSize = (double)e.NewValue;
        }


        public static readonly DependencyProperty ui_BackgroundProperty =
DependencyProperty.Register(nameof(ui_Background), typeof(Brush), typeof(ui_Led),
new FrameworkPropertyMetadata(Brushes.White, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_Background_Changed));
        private static void On_ui_Background_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_Led control = d as ui_Led;
            control._Led_border.Background = (Brush)e.NewValue;
        }

        public static readonly DependencyProperty ui_CornerRadiusProperty =
DependencyProperty.Register(nameof(ui_CornerRadius), typeof(CornerRadius), typeof(ui_Led),
new FrameworkPropertyMetadata(new CornerRadius(0), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_CornerRadius_Changed));
        private static void On_ui_CornerRadius_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_Led control = d as ui_Led;
            control._Led_border.CornerRadius = (CornerRadius)e.NewValue;
        }


        public static readonly DependencyProperty ui_FontFamilyProperty =
DependencyProperty.Register(nameof(ui_FontFamily), typeof(FontFamily), typeof(ui_Led),
new FrameworkPropertyMetadata(new FontFamily("微软雅黑"), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_FontFamily_Changed));
        private static void On_ui_FontFamily_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_Led control = d as ui_Led;
            control._textblock.FontFamily = (FontFamily)e.NewValue;
        }



        public static readonly DependencyProperty ui_BorderThicknessProperty =
DependencyProperty.Register(nameof(ui_BorderThickness), typeof(Thickness), typeof(ui_Led),
new FrameworkPropertyMetadata(new Thickness(3), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_BorderThickness_Changed));
        private static void On_BorderThickness_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_Led control = d as ui_Led;
            control._Led_border.BorderThickness = (Thickness)e.NewValue;
        }


     





        /// <summary>
        /// 圆角
        /// </summary>
        [Category("ui")]
        [Description("圆角")]
        public CornerRadius ui_CornerRadius
        {
            get
            {
                return (CornerRadius)GetValue(ui_CornerRadiusProperty);
            }
            set
            {
                SetValue(ui_CornerRadiusProperty, value);
            }
        }







        /// <summary>
        /// 边框宽度
        /// </summary>
        [Category("ui")]
        [Description("边框宽度")]
        public Thickness ui_BorderThickness
        {
            get
            {
                return (Thickness)GetValue(ui_BorderThicknessProperty);
            }
            set
            {
                SetValue(ui_BorderThicknessProperty, value);
            }
        }


 



        /// <summary>
        /// 字体
        /// </summary>
        [Category("ui")]
        [Description("字体")]
        public FontFamily ui_FontFamily
        {
            get
            {
                return (FontFamily)GetValue(ui_FontFamilyProperty);
            }
            set
            {
                SetValue(ui_FontFamilyProperty, value);
            }
        }


        /// <summary>
        /// 显示状态内容
        /// </summary>
        [Category("ui")]
        [Description("显示文本内容")]
        public virtual string ui_Text
        {
            get
            {
                return (string)GetValue(ui_TextProperty);
            }
            set
            {
                SetValue(ui_TextProperty, value);
            }
        }



        /// <summary>
        /// 文本颜色
        /// </summary>
        [Category("ui")]
        [Description("文本颜色")]
        public virtual Brush ui_Foreground
        {
            get
            {
                return (Brush)GetValue(ui_ForegroundProperty);
            }
            set
            {
                SetValue(ui_ForegroundProperty, value);
            }
        }



         

        /// <summary>
        /// 边框颜色
        /// </summary>
        [Category("ui")]
        [Description("边框颜色")]
        public Brush ui_BorderBrush
        {
            get
            {
                return (Brush)GetValue(ui_BorderBrushProperty);
            }
            set
            {
                SetValue(ui_BorderBrushProperty, value);
            }
        }



        /// <summary>
        /// 字体大小
        /// </summary>
        [Category("ui")]
        [Description("字体大小")]
        public double ui_FontSize
        {
            get
            {
                return (double)GetValue(ui_FontSizeProperty);
            }
            set
            {
                SetValue(ui_FontSizeProperty, value);
            }
        }

        /// <summary>
        /// 背景颜色
        /// </summary>
        [Category("ui")]
        [Description("背景颜色")]
        public Brush ui_Background
        {
            get
            {
                return (Brush)GetValue(ui_BackgroundProperty);
            }
            set
            {
                SetValue(ui_BackgroundProperty, value);
            }
        }


    


        #endregion
    }
}
