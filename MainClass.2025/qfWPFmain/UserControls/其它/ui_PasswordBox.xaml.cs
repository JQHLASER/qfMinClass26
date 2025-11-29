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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace qfWPFmain
{
    /// <summary>
    /// ui_PasswordBox.xaml 的交互逻辑
    /// <para>调用GetPassword()获取输入的密码</para>
    /// <para>调用SetPassword()设置密码</para>
    /// </summary>
    public partial class ui_PasswordBox : UserControl
    {
        public ui_PasswordBox()
        {
            InitializeComponent();

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.PasswordBox_.Clip = new Clip_().圆角矩形(this.PasswordBox_.ActualWidth, this.PasswordBox_.ActualHeight, CornerRadiusUi_, CornerRadiusUi_);
        }


        #region 依赖属性

        public static readonly DependencyProperty ui_PasswordProperty =
        DependencyProperty.Register(nameof(ui_Password), typeof(string), typeof(ui_PasswordBox),
            new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_Content_Changed));
        private static void On_ui_Content_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_PasswordBox control = d as ui_PasswordBox;

            // 将新值同步到UserControl内部的UI元素（如TextBlock）
            control.PasswordBox_.Password = e.NewValue.ToString();


        }


        public static readonly DependencyProperty ui_FontFamilyProperty =
DependencyProperty.Register(nameof(ui_FontFamily), typeof(FontFamily), typeof(ui_PasswordBox),
new FrameworkPropertyMetadata(new FontFamily("微软雅黑"), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_FontFamily_Changed));
        private static void On_ui_FontFamily_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_PasswordBox control = d as ui_PasswordBox;

            // 将新值同步到UserControl内部的UI元素（如TextBlock）

            control.PasswordBox_.FontFamily = (FontFamily)e.NewValue;

        }

        public static readonly DependencyProperty ui_FontSizeProperty =
DependencyProperty.Register(nameof(ui_FontSize), typeof(double), typeof(ui_PasswordBox),
new FrameworkPropertyMetadata(15.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_FontSize_Changed));
        private static void On_ui_FontSize_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_PasswordBox control = d as ui_PasswordBox;

            // 将新值同步到UserControl内部的UI元素（如TextBlock）

            control.PasswordBox_.FontSize = (double)e.NewValue;

            // control.处理宽高();

        }







        #endregion

        #region 事件

        public event Action<object, KeyEventArgs> Event_KeyDown;


        #endregion


        #region 属性



        /// <summary>
        /// 字体
        /// </summary>
        [Description("Password,密码")]
        public string ui_Password
        {
            get
            {
                return (string)GetValue(ui_PasswordProperty);
            }
            set
            {
                SetValue(ui_PasswordProperty, value);
            }
        }



        private FontFamily FontFamilyUi_ = new FontFamily("微软雅黑");
        /// <summary>
        /// 字体
        /// </summary>
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
                this.PasswordBox_.Foreground = ForegroundUi_;
            }
        }


        private Brush BackGroundUi_ = Brushes.White;
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
                this.Border.Background = BackGroundUi_;
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
                this.PasswordBox_.VerticalAlignment = VerticalAlignmentUi_;
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
                this.PasswordBox_.HorizontalAlignment = HorizontalAlignmentUi_;
            }
        }


        private VerticalAlignment VerticalContentAlignmentUi_ = VerticalAlignment.Center;
        /// <summary>
        /// 垂直内容对齐
        /// </summary>
        [Category("ui")]
        [Description("垂直内容对齐")]
        public VerticalAlignment ui_VerticalContentAlignment
        {
            get
            {
                return VerticalContentAlignmentUi_;
            }
            set
            {
                VerticalContentAlignmentUi_ = value;
                this.PasswordBox_.VerticalContentAlignment = VerticalContentAlignmentUi_;
            }
        }


        private HorizontalAlignment HorizontalContentAlignmentUi_ = HorizontalAlignment.Center;
        /// <summary>
        /// 水平对齐
        /// </summary>
        [Category("ui")]
        [Description("水平内容对齐")]
        public HorizontalAlignment ui_HorizontalContentAlignment
        {
            get
            {
                return HorizontalContentAlignmentUi_;
            }
            set
            {
                HorizontalContentAlignmentUi_ = value;
                this.PasswordBox_.HorizontalContentAlignment = HorizontalContentAlignmentUi_;
            }
        }








        private Thickness PaddingUi_ = new Thickness(0);
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


        private char PasswordCharUi_ = '*';
        /// <summary>
        /// 掩码字符
        /// </summary>
        [Description("掩码字符")]
        [Category("ui")]
        public char ui_PasswordChar
        {
            get
            {
                return PasswordCharUi_;
            }
            set
            {
                PasswordCharUi_ = value;
                this.PasswordBox_.PasswordChar = PasswordCharUi_;
            }
        }

        private bool 输入法是否启用Ui_ = false;
        /// <summary>
        /// 输入法是否启用,true:启动,false:禁止
        /// </summary>
        [Category("ui")]
        [Description("输入法是否启用,true:启动,false:禁止")]
        public bool ui_输入法是否启用
        {
            get
            {
                return 输入法是否启用Ui_;
            }
            set
            {
                输入法是否启用Ui_ = value;
                InputMethod.SetIsInputMethodEnabled(this.PasswordBox_, 输入法是否启用Ui_);

            }
        }

        #endregion



        /// <summary>
        /// 获取输入的密码
        /// </summary>
        /// <returns></returns>
        public string GetPassword()
        {
            string password = "";
            this.Dispatcher.Invoke(() =>
           {
               password = this.PasswordBox_.Password;
           });

            return password;
        }

        /// <summary>
        /// 设置密码
        /// </summary>
        /// <param name="password"></param>
        public void SetPassword(string password)
        {
            this.ui_Password = password;
        }

        /// <summary>
        /// 设置焦点
        /// </summary>
        public new void Focus()
        {
            this.PasswordBox_.Focus();
        }


        private void PasswordBox_KeyDown(object sender, KeyEventArgs e)
        {
            Event_KeyDown?.Invoke(sender, e);
        }


    }
}
