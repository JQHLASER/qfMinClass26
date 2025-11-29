using qfWPFmain;
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
    /// ui_Label.xaml 的交互逻辑
    /// </summary>
    public partial class ui_Label : UserControl
    {
        public ui_Label()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.label_.Clip = new Clip_().圆角矩形(this.label_.ActualWidth, this.label_.ActualHeight, CornerRadiusUi_, CornerRadiusUi_);
        }

        #region 依赖属性

        public static readonly DependencyProperty ui_ContentProperty =
        DependencyProperty.Register(nameof(ui_Content), typeof(string), typeof(ui_Label),
            new FrameworkPropertyMetadata("Label", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_Content_Changed));
        private static void On_ui_Content_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_Label control = d as ui_Label;

            // 将新值同步到UserControl内部的UI元素（如TextBlock）
            control.label_.Content = e.NewValue.ToString();


        }


        public static readonly DependencyProperty ui_FontFamilyProperty =
DependencyProperty.Register(nameof(ui_FontFamily), typeof(FontFamily), typeof(ui_Label),
new FrameworkPropertyMetadata(new FontFamily("微软雅黑"), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_FontFamily_Changed));
        private static void On_ui_FontFamily_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_Label control = d as ui_Label;

            // 将新值同步到UserControl内部的UI元素（如TextBlock）


            control.label_.FontFamily = (FontFamily)e.NewValue;

        }

        public static readonly DependencyProperty ui_FontSizeProperty =
DependencyProperty.Register(nameof(ui_FontSize), typeof(double), typeof(ui_Label),
new FrameworkPropertyMetadata(13.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_FontSize_Changed));
        private static void On_ui_FontSize_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_Label control = d as ui_Label;

            // 将新值同步到UserControl内部的UI元素（如TextBlock）

            control.label_.FontSize = (double)e.NewValue;

            // control.处理宽高();

        }

        public static readonly DependencyProperty ui_VerticalContentAlignmentProperty =
DependencyProperty.Register(nameof(ui_VerticalContentAlignment), typeof(VerticalAlignment), typeof(ui_Label),
new FrameworkPropertyMetadata(VerticalAlignment.Center, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_VerticalContentAlignment_Changed));
        private static void On_ui_VerticalContentAlignment_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_Label control = d as ui_Label;

            // 将新值同步到UserControl内部的UI元素（如TextBlock）

            control.label_.VerticalContentAlignment = (VerticalAlignment)e.NewValue;

            // control.处理宽高();

        }


        public static readonly DependencyProperty ui_HorizontalContentAlignmentProperty =
DependencyProperty.Register(nameof(ui_HorizontalContentAlignment), typeof(HorizontalAlignment), typeof(ui_Label),
new FrameworkPropertyMetadata(HorizontalAlignment.Center, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_HorizontalContentAlignment_Changed));
        private static void On_ui_HorizontalContentAlignment_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_Label control = d as ui_Label;

            // 将新值同步到UserControl内部的UI元素（如TextBlock）

            control.label_.HorizontalContentAlignment   = (HorizontalAlignment)e.NewValue;

            // control.处理宽高();

        }



        #endregion


        #region 属性


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
                return (HorizontalAlignment)GetValue (ui_HorizontalContentAlignmentProperty );
            }
            set
            {
               SetValue (ui_HorizontalContentAlignmentProperty, value);
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

        private double FontSizeUi_ = 14;
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

        private Brush ForegroundUi_ = Brushes.Black;
        /// <summary>
        /// 文本颜色
        /// </summary>
        [Category("ui")]
        [Description("文本颜色")]
        public Brush ui_Foreground
        {
            get
            {
                return ForegroundUi_;
            }
            set
            {
                ForegroundUi_ = value;
                this.label_.Foreground = ForegroundUi_;
            }
        }


        private Brush BackgroundUi_ = Brushes.White;
        /// <summary>
        /// 背景颜色
        /// </summary>
        [Category("ui")]
        [Description("背景颜色")]
        public Brush ui_Background
        {
            get
            {
                return BackgroundUi_;
            }
            set
            {
                BackgroundUi_ = value;
                this.Border.Background = BackgroundUi_;
            }
        }

        private Brush BorderBrushUi_ = Brushes.Silver;
        /// <summary>
        /// 边框颜色
        /// </summary>
        [Category("ui")]
        [Description("边框颜色")]
        public Brush ui_BorderBrush
        {
            get
            {
                return BorderBrushUi_;
            }
            set
            {
                BorderBrushUi_ = value;
                this.Border.BorderBrush = BorderBrushUi_;
            }
        }

        private double CornerRadiusUi_ = 5;
        /// <summary>
        /// 圆角半径
        /// </summary>
        [Category("ui")]
        [Description("圆角半径")]
        public double ui_CornerRadius
        {
            get
            {
                return CornerRadiusUi_;
            }
            set
            {
                CornerRadiusUi_ = value;
                this.Border.CornerRadius = new CornerRadius(CornerRadiusUi_);
            }
        }

        private Thickness BorderThicknessUi_ = new Thickness(1);
        /// <summary>
        /// 边框宽度
        /// </summary>
        [Category("ui")]
        [Description("边框宽度")]
        public Thickness ui_BorderThickness
        {
            get
            {
                return BorderThicknessUi_;
            }
            set
            {
                BorderThicknessUi_ = value;
                this.Border.BorderThickness = BorderThicknessUi_;
            }
        }

        private Thickness MarginUi_ = new Thickness(0);
        /// <summary>
        /// Margin
        /// </summary>
        [Category("ui")]
        [Description("Margin")]
        public Thickness ui_Margin
        {
            get
            {
                return MarginUi_;
            }
            set
            {
                MarginUi_ = value;
                this.Border.Margin = MarginUi_;
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
                this.label_.VerticalAlignment = VerticalAlignmentUi_;
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
                this.label_.HorizontalAlignment = HorizontalAlignmentUi_;
            }
        }






       




        private Thickness PaddingUi_ = new Thickness(2);
        /// <summary>
        /// Padding
        /// </summary>
        [Category("ui")]
        [Description("Padding")]
        public Thickness ui_Padding
        {
            get
            {
                return PaddingUi_;
            }
            set
            {
                PaddingUi_ = value;
                this.Border.Padding = PaddingUi_;
            }
        }


        #endregion

        /// <summary>
        /// 设置焦点
        /// </summary>
        public new void Focus()
        {
            this.label_.Focus();
        }
    }
}
