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
    /// ui_Label22.xaml 的交互逻辑
    /// </summary>
    public partial class ui_Label2 : UserControl
    {
        public ui_Label2()
        {
            InitializeComponent();
        }


        #region 依赖属性

        public static readonly DependencyProperty ui_ContentProperty =
        DependencyProperty.Register(nameof(ui_Content), typeof(string), typeof(ui_Label2),
            new FrameworkPropertyMetadata("Label", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_Content_Changed));
        private static void On_ui_Content_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_Label2 control = d as ui_Label2;

            // 将新值同步到UserControl内部的UI元素（如TextBlock）
            control._Label.Content = e.NewValue.ToString();


        }


        public static readonly DependencyProperty ui_FontFamilyProperty =
DependencyProperty.Register(nameof(ui_FontFamily), typeof(FontFamily), typeof(ui_Label2),
new FrameworkPropertyMetadata(new FontFamily("微软雅黑"), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_FontFamily_Changed));
        private static void On_ui_FontFamily_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_Label2 control = d as ui_Label2;

            // 将新值同步到UserControl内部的UI元素（如TextBlock）


            control._Label.FontFamily = (FontFamily)e.NewValue;

        }

        public static readonly DependencyProperty ui_FontSizeProperty =
DependencyProperty.Register(nameof(ui_FontSize), typeof(double), typeof(ui_Label2),
new FrameworkPropertyMetadata(14.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_FontSize_Changed));
        private static void On_ui_FontSize_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_Label2 control = d as ui_Label2;

            // 将新值同步到UserControl内部的UI元素（如TextBlock）

            control._Label.FontSize = (double)e.NewValue;

            // control.处理宽高();

        }

        public static readonly DependencyProperty ui_VerticalContentAlignmentProperty =
DependencyProperty.Register(nameof(ui_VerticalContentAlignment), typeof(VerticalAlignment), typeof(ui_Label2),
new FrameworkPropertyMetadata(VerticalAlignment.Center, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_VerticalContentAlignment_Changed));
        private static void On_ui_VerticalContentAlignment_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_Label2 control = d as ui_Label2;

            // 将新值同步到UserControl内部的UI元素（如TextBlock）

            control._Label.VerticalContentAlignment = (VerticalAlignment)e.NewValue;

            // control.处理宽高();

        }


        public static readonly DependencyProperty ui_HorizontalContentAlignmentProperty =
DependencyProperty.Register(nameof(ui_HorizontalContentAlignment), typeof(HorizontalAlignment), typeof(ui_Label2),
new FrameworkPropertyMetadata(HorizontalAlignment.Center, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_HorizontalContentAlignment_Changed));
        private static void On_ui_HorizontalContentAlignment_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_Label2 control = d as ui_Label2;

            // 将新值同步到UserControl内部的UI元素（如TextBlock）

            control._Label.HorizontalContentAlignment = (HorizontalAlignment)e.NewValue;

            // control.处理宽高();

        }

        public static readonly DependencyProperty ui_ForegroundProperty =
DependencyProperty.Register(nameof(ui_Foreground), typeof(Brush), typeof(ui_Label2),
new FrameworkPropertyMetadata(Brushes.Gray, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_Foreground_Changed));
        private static void On_ui_Foreground_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_Label2 control = d as ui_Label2;

            // 将新值同步到UserControl内部的UI元素（如TextBlock）

            control._Label.Foreground = (Brush)e.NewValue;

            // control.处理宽高();

        }





        #endregion


        #region 属性




        [Category("ui")]
        [Description("")]
        public Brush ui_Foreground
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





        //private HorizontalAlignment HorizontalContentAlignmentUi_ = HorizontalAlignment.Center;
        /// <summary>
        /// 垂直内容对齐方式
        /// </summary>
        [Category("ui")]
        [Description("水平内容对齐方式")]
        public HorizontalAlignment ui_HorizontalContentAlignment
        {
            get
            {
                return (HorizontalAlignment)GetValue(ui_HorizontalContentAlignmentProperty);
            }
            set
            {
                SetValue(ui_HorizontalContentAlignmentProperty, value);
            }
        }


        // private VerticalAlignment VerticalContentAlignmentUi_ = VerticalAlignment.Center;
        /// <summary>
        /// 垂直内容对齐方式
        /// </summary>
        [Category("ui")]
        [Description("垂直内容对齐方式")]
        public VerticalAlignment ui_VerticalContentAlignment
        {
            get
            {
                return (VerticalAlignment)GetValue(ui_VerticalContentAlignmentProperty);
            }
            set
            {
                SetValue(ui_VerticalContentAlignmentProperty, value);
            }
        }




        // private string ContentUi_ = "Label";
        /// <summary>
        /// 显示文本
        /// </summary>
        [Category("ui")]
        [Description("显示文本")]
        public string ui_Content
        {
            get
            {
                return (string)GetValue(ui_ContentProperty);
            }
            set
            {
                SetValue(ui_ContentProperty, value);

            }
        }

        // private FontFamily FontFamilyUi_ = new FontFamily("微软雅黑");
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

        private double FontSizeUi_ = 15;
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




        private VerticalAlignment VerticalAlignmentUi_ = VerticalAlignment.Stretch;
        /// <summary>
        /// 垂直对齐,Lable高度
        /// </summary>
        [Category("ui")]
        [Description("垂直对齐,Lable高度")]
        public VerticalAlignment ui_VerticalAlignment
        {
            get
            {
                return VerticalAlignmentUi_;
            }
            set
            {
                VerticalAlignmentUi_ = value;
                this._Label.VerticalAlignment = VerticalAlignmentUi_;
            }
        }


        private HorizontalAlignment HorizontalAlignmentUi_ = HorizontalAlignment.Stretch;
        /// <summary>
        /// 水平对齐,Lable宽度
        /// </summary>
        [Category("ui")]
        [Description("水平对齐,Lable宽度")]
        public HorizontalAlignment ui_HorizontalAlignment
        {
            get
            {
                return HorizontalAlignmentUi_;
            }
            set
            {
                HorizontalAlignmentUi_ = value;
                this._Label.HorizontalAlignment = HorizontalAlignmentUi_;
            }
        }













        #endregion



    }
}
