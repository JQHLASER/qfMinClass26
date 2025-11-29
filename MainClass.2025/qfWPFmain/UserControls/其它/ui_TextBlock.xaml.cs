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
    /// ui_TextBlock.xaml 的交互逻辑
    /// </summary>
    public partial class ui_TextBlock : UserControl
    {
        public ui_TextBlock()
        {
            InitializeComponent();

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            this.TextBlock_.Clip = new Clip_().圆角矩形(this.TextBlock_.ActualWidth, this.TextBlock_.ActualHeight, CornerRadiusUi_, CornerRadiusUi_);
        }

        #region 依赖属性

        public static readonly DependencyProperty ui_TextProperty =
        DependencyProperty.Register(nameof(ui_Text), typeof(string), typeof(ui_TextBlock),
            new FrameworkPropertyMetadata("TextBlock", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_Content_Changed));
        private static void On_ui_Content_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_TextBlock control = d as ui_TextBlock;

            // 将新值同步到UserControl内部的UI元素（如TextBlock）
            control.TextBlock_.Text = e.NewValue.ToString(); 


        }


        public static readonly DependencyProperty ui_FontFamilyProperty =
DependencyProperty.Register(nameof(ui_FontFamily), typeof(FontFamily), typeof(ui_TextBlock),
new FrameworkPropertyMetadata(new FontFamily("微软雅黑"), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_FontFamily_Changed));
        private static void On_ui_FontFamily_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_TextBlock control = d as ui_TextBlock;

            // 将新值同步到UserControl内部的UI元素（如TextBlock）
      
            
                control.TextBlock_.FontFamily = (FontFamily)e.NewValue;
            
        }

        public static readonly DependencyProperty ui_FontSizeProperty =
DependencyProperty.Register(nameof(ui_FontSize), typeof(double), typeof(ui_TextBlock),
new FrameworkPropertyMetadata(14.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_FontSize_Changed));
        private static void On_ui_FontSize_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_TextBlock control = d as ui_TextBlock;

            // 将新值同步到UserControl内部的UI元素（如TextBlock）
      
           
                control.TextBlock_.FontSize = (double)e.NewValue;
         
            // control.处理宽高();

        }







        #endregion

        #region 属性

        // private string TextUi_ = "TextBlock";
        /// <summary>
        /// 显示文本
        /// </summary>
        [Category("ui")]
        [Description("显示文本")]
        public string ui_Text
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

        //  private FontFamily FontFamilyUi_ = new FontFamily("微软雅黑");
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

        //   private double FontSizeUi_ = 15;
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
               this.TextBlock_.Foreground = ForegroundUi_; 
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
               this.Border .Background = BackgroundUi_;  
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
               this.Border.CornerRadius = new CornerRadius (CornerRadiusUi_);  
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


        private VerticalAlignment VerticalAlignmentUi_ = VerticalAlignment.Center;
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
               this.TextBlock_ .VerticalAlignment = VerticalAlignmentUi_;  
            }
        }


        private TextAlignment TextAlignmentUi_ = TextAlignment.Center;
        /// <summary>
        /// 水平对齐
        /// </summary>
        [Category("ui")]
        [Description("文本对齐方式")]
        public TextAlignment ui_TextAlignment
        {
            get
            {
                return TextAlignmentUi_;
            }
            set
            {
                TextAlignmentUi_ = value;
               this.TextBlock_.TextAlignment = TextAlignmentUi_; 
            }
        }


        private HorizontalAlignment HorizontalAlignmentUi_ = HorizontalAlignment.Center;
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
               this.TextBlock_.HorizontalAlignment = HorizontalAlignmentUi_;  
            }
        }







        private Thickness PaddingUi_ = new Thickness(5);
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
                  this.Border .Padding  = PaddingUi_;  
            }
        }



        private TextWrapping TextWrappingUi_ = TextWrapping.Wrap;
        /// <summary>
        /// 换行设置
        /// </summary>
        [Category("ui")]
        [Description("换行设置")]
        public TextWrapping ui_TextWrapping
        {
            get
            {
                return TextWrappingUi_;
            }
            set
            {
                TextWrappingUi_ = value;
               this.TextBlock_.TextWrapping = TextWrappingUi_;   
            }
        }



        #endregion


        /// <summary>
        /// 设置焦点
        /// </summary>
        public new void Focus()
        {
            this.TextBlock_.Focus();
        }
    }
}
