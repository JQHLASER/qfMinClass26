using qfWPFmain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    public partial class ui_Button : UserControl
    {

        public ui_Button()
        {
            InitializeComponent();

        }


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.Button.Clip = new Clip_().圆角矩形(this.Button.ActualWidth, this.Button.ActualHeight, CornerRadiusUi_, CornerRadiusUi_);



        }

        #region 依赖属性的标识符


        public static readonly DependencyProperty ui_ContentProperty =
         DependencyProperty.Register(nameof(ui_Content), typeof(string), typeof(ui_Button),
             new FrameworkPropertyMetadata("Button", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_Content_Changed));
        private static void On_ui_Content_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_Button control = d as ui_Button;
           control.Button.Content = e.NewValue.ToString(); 
        }


        public static readonly DependencyProperty ui_FontFamilyProperty =
       DependencyProperty.Register(nameof(ui_FontFamily), typeof(FontFamily), typeof(ui_Button),
           new FrameworkPropertyMetadata(new FontFamily("微软雅黑"), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_FontFamily_Changed));
        private static void On_ui_FontFamily_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_Button control = d as ui_Button;
           control.Button.FontFamily = (FontFamily)e.NewValue;  
        }

        public static readonly DependencyProperty ui_FontSizeProperty =
    DependencyProperty.Register(nameof(ui_FontSize), typeof(double), typeof(ui_Button),
        new FrameworkPropertyMetadata(14.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_FontSize_Changed));
        private static void On_ui_FontSize_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_Button control = d as ui_Button;
           control.Button.FontSize = (double)e.NewValue;  
        }



        #endregion




        #region 事件

        /// <summary>
        /// Action事件,被单击
        /// </summary>
        [Description("单击")]
        public event Action<object, RoutedEventArgs> Event_Click;

        /// <summary>
        /// 鼠标按下
        /// </summary>
        [Description("鼠标按下")]
        public event Action<object, MouseButtonEventArgs> Event_PreviewMouseDown;

        /// <summary>
        /// 鼠标松开
        /// </summary>
        [Description("鼠标松开")]
        public event Action<object, MouseButtonEventArgs> Event_PreviewMouseUp;

        /// <summary>
        /// 鼠标左键按下
        /// </summary>
        [Description("鼠标左键按下")]
        public event Action<object, MouseButtonEventArgs> Event_PreviewMouseLeftButtonDown;

        /// <summary>
        /// 鼠标左键松开
        /// </summary>
        [Description("鼠标左键松开")]
        public event Action<object, MouseButtonEventArgs> Event_PreviewMouseLeftButtonUp;




        #endregion

        #region 属性


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
        /// 字体大小
        /// </summary>
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
               this.Button.Foreground = ForegroundUi_;  
            }
        }


        private Brush BackGroundUi_ = Brushes.WhiteSmoke;
        /// <summary>
        /// 背景颜色
        /// </summary>
        [Category("ui")]
        [Description("背景颜色")]
        public Brush ui_BackGround
        {
            get
            {
                return BackGroundUi_;
            }
            set
            {
                BackGroundUi_ = value;
               this.Button.Background = BackGroundUi_;  

            }
        }

        /// <summary>
        /// "#FFDDDBDA"
        /// </summary>
        private Brush BorderBrushUi_ = new SolidColorBrush(Color.FromArgb(0xFF, 0xDD, 0xDB, 0xDA));
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

        private Thickness BorderThicknessUi_ = new Thickness(2);
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
        /// 垂直对齐
        /// </summary>
        [Category("ui")]
        [Description("垂直对齐")]
        public VerticalAlignment ui_VerticalAlignment
        {
            get
            {
                return VerticalAlignmentUi_;
            }
            set
            {
                VerticalAlignmentUi_ = value;
               this.Border.VerticalAlignment = VerticalAlignmentUi_;   
            }
        }


        private HorizontalAlignment HorizontalAlignmentUi_ = HorizontalAlignment.Stretch;
        /// <summary>
        /// 水平对齐
        /// </summary>
        [Category("ui")]
        [Description("水平对齐")]
        public HorizontalAlignment ui_HorizontalAlignment
        {
            get
            {
                return HorizontalAlignmentUi_;
            }
            set
            {
                HorizontalAlignmentUi_ = value;
               this.Border.HorizontalAlignment = HorizontalAlignmentUi_; 
            }
        }





        #endregion


        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Event_Click?.Invoke(sender, e);
        }

        private void Button_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Event_PreviewMouseLeftButtonDown?.Invoke(sender, e);
        }

        private void Button_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Event_PreviewMouseLeftButtonUp?.Invoke(sender, e);
        }

        private void Button_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Event_PreviewMouseDown?.Invoke(sender, e);
        }

        private void Button_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            Event_PreviewMouseUp?.Invoke(sender, e);
        }

        /// <summary>
        /// 设置焦点
        /// </summary>
        public new void Focus()
        {
            this.Button.Focus();
        }

    }
}
