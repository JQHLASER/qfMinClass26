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
    /// ui_ButtonBorder.xaml 的交互逻辑
    /// </summary>
    public partial class ui_ButtonBorder : UserControl
    {


        public ui_ButtonBorder()
        {
            InitializeComponent();

        }


        #region 事件

        /// <summary>
        /// 鼠标按下
        /// </summary>
        public event Action<object, MouseButtonEventArgs> Event_PreviewMouseDown;
        /// <summary>
        /// 鼠标抬起
        /// </summary>
        public event Action<object, MouseButtonEventArgs> Event_PreviewMouseUp;



        /// <summary>
        /// 鼠标按下
        /// </summary>
        public event Action<object, MouseButtonEventArgs> Event_PreviewMouseLeftButtonDown;
        /// <summary>
        /// 鼠标抬起
        /// </summary>
        public event Action<object, MouseButtonEventArgs> Event_PreviewMouseLeftButtonUp;

        #endregion


        #region 属性

        public static readonly DependencyProperty ui_TextProperty =
DependencyProperty.Register(nameof(ui_Text), typeof(string), typeof(ui_ButtonBorder),
    new FrameworkPropertyMetadata("Button", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_Text_Changed));
        private static void On_ui_Text_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_ButtonBorder control = d as ui_ButtonBorder;

            // 将新值同步到UserControl内部的UI元素（如TextBlock）


            control._textblock.Text = e.NewValue.ToString();

        }


        public static readonly DependencyProperty ui_ForegroundProperty =
DependencyProperty.Register(nameof(ui_Foreground), typeof(Brush), typeof(ui_ButtonBorder),
new FrameworkPropertyMetadata(Brushes.Black, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_Foreground_Changed));
        private static void On_ui_Foreground_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_ButtonBorder control = d as ui_ButtonBorder;

            // 将新值同步到UserControl内部的UI元素（如TextBlock）


            control._textblock.Foreground = (Brush)e.NewValue;


        }

        public static readonly DependencyProperty ui_BorderBrushMouseEnterProperty =
DependencyProperty.Register(nameof(ui_BorderBrushMouseEnter), typeof(Brush), typeof(ui_ButtonBorder),
new FrameworkPropertyMetadata(Brushes.Gray, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_BorderBrushMouseEnter_Changed));
        private static void On_ui_BorderBrushMouseEnter_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_ButtonBorder control = d as ui_ButtonBorder;

            // 将新值同步到UserControl内部的UI元素（如TextBlock）

            control.ui_BorderBrushMouseEnter = (Brush)e.NewValue;


        }


        public static readonly DependencyProperty ui_BorderBrushProperty =
DependencyProperty.Register(nameof(ui_BorderBrush), typeof(Brush), typeof(ui_ButtonBorder),
new FrameworkPropertyMetadata(Brushes.Silver, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_BorderBrush_Changed));
        private static void On_ui_BorderBrush_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_ButtonBorder control = d as ui_ButtonBorder;

            // 将新值同步到UserControl内部的UI元素（如TextBlock）

            control._button_border.BorderBrush = (Brush)e.NewValue;


        }

        public static readonly DependencyProperty ui_FontSizeProperty =
DependencyProperty.Register(nameof(ui_FontSize), typeof(double), typeof(ui_ButtonBorder),
new FrameworkPropertyMetadata(14d, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_FontSize_Changed));
        private static void On_ui_FontSize_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_ButtonBorder control = d as ui_ButtonBorder;
            control._textblock.FontSize = (double)e.NewValue;
        }


        public static readonly DependencyProperty ui_BackgroundProperty =
DependencyProperty.Register(nameof(ui_Background), typeof(Brush), typeof(ui_ButtonBorder),
new FrameworkPropertyMetadata(Brushes.WhiteSmoke, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_Background_Changed));
        private static void On_ui_Background_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_ButtonBorder control = d as ui_ButtonBorder;
            control._button_border.Background = (Brush)e.NewValue;
        }

        public static readonly DependencyProperty ui_CornerRadiusProperty =
DependencyProperty.Register(nameof(ui_CornerRadius), typeof(CornerRadius), typeof(ui_ButtonBorder),
new FrameworkPropertyMetadata(new CornerRadius(5), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_CornerRadius_Changed));
        private static void On_ui_CornerRadius_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_ButtonBorder control = d as ui_ButtonBorder;
            control._button_border.CornerRadius = (CornerRadius)e.NewValue;
        }


        public static readonly DependencyProperty ui_FontFamilyProperty =
DependencyProperty.Register(nameof(ui_FontFamily), typeof(FontFamily), typeof(ui_ButtonBorder),
new FrameworkPropertyMetadata(new FontFamily("微软雅黑"), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_FontFamily_Changed));
        private static void On_ui_FontFamily_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_ButtonBorder control = d as ui_ButtonBorder;
            control._textblock.FontFamily = (FontFamily)e.NewValue;
        }



        public static readonly DependencyProperty ui_BorderThicknessProperty =
DependencyProperty.Register(nameof(ui_BorderThickness), typeof(Thickness), typeof(ui_ButtonBorder),
new FrameworkPropertyMetadata(new Thickness(3), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_BorderThickness_Changed));
        private static void On_BorderThickness_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_ButtonBorder control = d as ui_ButtonBorder;
            control._button_border.BorderThickness = (Thickness)e.NewValue;
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
        /// 鼠标放上时的边框颜色
        /// </summary>
        [Category("ui")]
        [Description("鼠标放上时的边框颜色")]
        public Brush ui_BorderBrushMouseEnter
        {
            get
            {
                return (Brush)GetValue(ui_BorderBrushMouseEnterProperty);
            }
            set
            {
                SetValue(ui_BorderBrushMouseEnterProperty, value);
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


        /// <summary>
        /// 背景颜色
        /// </summary>
        [Category("ui")]
        [Description("背景颜色")]
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









        #endregion

        #region Command


        // 2. 定义 Command 依赖属性（核心）
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

    


        // 注册依赖属性，允许绑定
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(ui_ButtonBorder),
                new PropertyMetadata(null));


        //  定义 CommandParameter 依赖属性（传递命令参数）
        public object CommandParameter
        {
            get { return GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly DependencyProperty CommandParameterProperty =
        DependencyProperty.Register("CommandParameter", typeof(object), typeof(ui_ButtonBorder),
            new PropertyMetadata(null));


        // 4. 处理控件交互事件（如点击），触发命令执行
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            // 触发命令：检查命令是否可执行，然后执行
            if (Command != null && Command.CanExecute(CommandParameter))
            {
                Command.Execute(CommandParameter);
            }
        }


        #endregion



        private void _button_border_MouseEnter(object sender, MouseEventArgs e)
        {
            this._button_border.BorderBrush = ui_BorderBrushMouseEnter;
        }

        private void _button_border_MouseLeave(object sender, MouseEventArgs e)
        {
            this._button_border.BorderBrush = ui_BorderBrush;
        }

        private void _button_border_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Event_PreviewMouseDown?.Invoke(sender, e);
        }

        private void _button_border_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            Event_PreviewMouseUp?.Invoke(sender, e);
        }

        private void _button_border_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Event_PreviewMouseLeftButtonDown?.Invoke(sender, e);
        }

        private void _button_border_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Event_PreviewMouseLeftButtonUp?.Invoke(sender, e);
        }
    }
}
