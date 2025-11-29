
using qfWPFmain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.Xml;
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
using System.Windows.Threading;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace qfWPFmain
{
    /// <summary>
    /// ui_TextBox.xaml 的交互逻辑
    /// </summary>
    public partial class ui_TextBox : UserControl
    {
        public ui_TextBox()
        {
            InitializeComponent();
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            this.TextBox_.Clip = new Clip_().圆角矩形(this.TextBox_.ActualWidth, this.TextBox_.ActualHeight, CornerRadiusUi_, CornerRadiusUi_);

        }



        #region 依赖属性的标识符

        // /// <summary>
        ///// 依赖属性的标识符
        ///// </summary>
        //public static readonly DependencyProperty ui_TextProperty =
        //    DependencyProperty.Register(
        //        "ui_Text",  // 属性名称
        //        typeof(string),   // 属性类型
        //        typeof(ui_TextBox),  // 拥有者类型
        //        new PropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnTextChanged)// 默认值
        //        );

        public static readonly DependencyProperty ui_TextProperty =
         DependencyProperty.Register(nameof(ui_Text), typeof(string), typeof(ui_TextBox),
             new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_Text_Changed));
        private static void On_ui_Text_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_TextBox control = d as ui_TextBox;
            control.TextBox_.Text = e.NewValue.ToString();
        }

        public static readonly DependencyProperty ui_TextIntProperty =
    DependencyProperty.Register(nameof(ui_TextInt), typeof(int), typeof(ui_TextBox),
        new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_int_Changed));
        private static void On_ui_int_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_TextBox control = d as ui_TextBox;

            // 将新值同步到UserControl内部的UI元素（如TextBlock）
            control.ui_Text = $"{e.NewValue}";


        }

        public static readonly DependencyProperty ui_TextLongProperty =
DependencyProperty.Register(nameof(ui_TextLong), typeof(long), typeof(ui_TextBox),
  new FrameworkPropertyMetadata((long)0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_Long_Changed));
        private static void On_ui_Long_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_TextBox control = d as ui_TextBox;

            // 将新值同步到UserControl内部的UI元素（如TextBlock）
            control.ui_Text = $"{e.NewValue}";
        }

        public static readonly DependencyProperty ui_TextDoubleProperty =
DependencyProperty.Register(nameof(ui_TextDouble), typeof(double), typeof(ui_TextBox),
 new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_TextDouble_Changed));
        private static void On_ui_TextDouble_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_TextBox control = d as ui_TextBox;
            // 将新值同步到UserControl内部的UI元素（如TextBlock）        
            control.ui_Text = control.处理小数点((double)e.NewValue);

        }



        public static readonly DependencyProperty ui_FontFamilyProperty =
DependencyProperty.Register(nameof(ui_FontFamily), typeof(FontFamily), typeof(ui_TextBox),
new FrameworkPropertyMetadata(new FontFamily("微软雅黑"), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_FontFamily_Changed));
        private static void On_ui_FontFamily_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_TextBox control = d as ui_TextBox;

            // 将新值同步到UserControl内部的UI元素（如TextBlock）

            control.TextBox_.FontFamily = (FontFamily)e.NewValue;

        }

        public static readonly DependencyProperty ui_FontSizeProperty =
DependencyProperty.Register(nameof(ui_FontSize), typeof(double), typeof(ui_TextBox),
new FrameworkPropertyMetadata(14.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_FontSize_Changed));
        private static void On_ui_FontSize_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_TextBox control = d as ui_TextBox;

            // 将新值同步到UserControl内部的UI元素（如TextBlock）

            control.TextBox_.FontSize = (double)e.NewValue;

            // control.处理宽高();

        }


        public static readonly DependencyProperty ui_输入值类型Property =
DependencyProperty.Register(nameof(ui_输入值类型), typeof(输入值类型), typeof(ui_TextBox),
new FrameworkPropertyMetadata(输入值类型.String, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_输入值类型_Changed));
        private static void On_ui_输入值类型_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_TextBox control = d as ui_TextBox;

            // 将新值同步到UserControl内部的UI元素（如TextBlock）
            if ((输入值类型)e.NewValue != 输入值类型.String)
            {
                control.ui_输入法是否启用 = false;
            }


            // control.处理宽高();

        }



        public static readonly DependencyProperty ui_BackGroundProperty =
DependencyProperty.Register(nameof(ui_BackGround), typeof(Brush), typeof(ui_TextBox),
new FrameworkPropertyMetadata(Brushes.White, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_BackGround_Changed));
        private static void On_ui_BackGround_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_TextBox control = d as ui_TextBox;
            control.Border.Background = (Brush)e.NewValue;

        }

        public static readonly DependencyProperty ui_IsReadOnlyProperty =
DependencyProperty.Register(nameof(ui_IsReadOnly), typeof(bool), typeof(ui_TextBox),
new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_IsReadOnly_Changed));
        private static void On_ui_IsReadOnly_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_TextBox control = d as ui_TextBox;
            control.TextBox_.IsReadOnly = (bool)e.NewValue;

        }


        public static readonly DependencyProperty ui_TextWrappingProperty =
DependencyProperty.Register(nameof(ui_TextWrapping), typeof(TextWrapping), typeof(ui_TextBox),
new FrameworkPropertyMetadata(TextWrapping.NoWrap, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_TextWrapping_Changed));
        private static void On_ui_TextWrapping_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_TextBox control = d as ui_TextBox;
            control.TextBox_.TextWrapping = (TextWrapping)e.NewValue;

        }

        public static readonly DependencyProperty ui_AcceptsReturn是否换行Property =
DependencyProperty.Register(nameof(ui_AcceptsReturn是否换行), typeof(bool), typeof(ui_TextBox),
new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_AcceptsReturn是否换行_Changed));
        private static void On_ui_AcceptsReturn是否换行_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_TextBox control = d as ui_TextBox;
            control.TextBox_.AcceptsReturn = (bool)e.NewValue;

        }

        public static readonly DependencyProperty ui_输入法是否启用Property =
DependencyProperty.Register(nameof(ui_输入法是否启用), typeof(bool), typeof(ui_TextBox),
new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_输入法是否启用_Changed));
        private static void On_ui_输入法是否启用_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_TextBox control = d as ui_TextBox;
            InputMethod.SetIsInputMethodEnabled(control.TextBox_, (bool)e.NewValue);
        }



        #endregion


        #region 事件

        /// <summary>
        /// 键盘键被按下时
        /// </summary>    
        [Description("回车键")]
        public event Action<object, KeyEventArgs> Event_KeyDown;

        /// <summary>
        ///内容被修改时
        /// </summary>     
        [Description("TextChanged被改变时")]
        public event Action<object, TextChangedEventArgs> Event_TextChanged;
        /// <summary>
        /// 输入
        /// </summary>
        [Description("输入")]
        public event Action<object, TextCompositionEventArgs> Event_PreviewTextInput;

        /// <summary>
        /// 失去焦点
        /// </summary>
        [Description("失去焦点")]
        public event Action<object, RoutedEventArgs> Event_LostFocus;


        #endregion

        #region 属性




        /// <summary>
        /// 是否回车换行,true:换行,false:禁止换行
        /// </summary>
        [Category("ui")]
        [Description("是否回车换行,true:启用,false:禁止")]
        public bool ui_AcceptsReturn是否换行
        {
            get
            {
                return (bool)GetValue(ui_AcceptsReturn是否换行Property);
            }
            set
            {
                SetValue(ui_AcceptsReturn是否换行Property, value);

            }
        }



        /// <summary>
        /// 输入法是否启用,true:启动,false:禁止
        /// </summary>
        [Category("ui")]
        [Description("输入法是否启用,true:启动,false:禁止")]
        public bool ui_输入法是否启用
        {
            get
            {
                return (bool)GetValue(ui_输入法是否启用Property);
            }
            set
            {
                SetValue(ui_输入法是否启用Property, value);
            }
        }






        /// <summary>
        /// 换行设置
        /// </summary>
        [Category("ui")]
        [Description("换行设置")]
        public TextWrapping ui_TextWrapping
        {
            get
            {
                return (TextWrapping)GetValue(ui_TextWrappingProperty);
            }
            set
            {
                SetValue(ui_TextWrappingProperty, value);
            }
        }


        /// <summary>
        /// 是否只读
        /// </summary>
        [Category("ui")]
        [Description("是否只读")]
        public bool ui_IsReadOnly
        {
            get
            {
                return (bool)GetValue(ui_IsReadOnlyProperty);
            }
            set
            {
                SetValue(ui_IsReadOnlyProperty, value);
            }
        }


        /// <summary>
        /// 背景颜色
        /// </summary>
        [Category("ui")]
        [Description("背景颜色")]
        public Brush ui_BackGround
        {
            get
            {
                return (Brush)GetValue(ui_BackGroundProperty);
            }
            set
            {
                SetValue(ui_BackGroundProperty, value);
            }
        }



        /// <summary>
        /// 输入值类型
        /// </summary>
        [Category("ui")]
        [Description("输入值类型")]
        public 输入值类型 ui_输入值类型
        {
            get
            {
                return (输入值类型)GetValue(ui_输入值类型Property);
            }
            set
            {
                SetValue(ui_输入值类型Property, value);
            }
        }





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


        /// <summary>
        /// int值
        /// </summary>       
        [Category("ui")]
        [Description("int值")]
        public int ui_TextInt
        {
            get
            {
                return (int)GetValue(ui_TextIntProperty);
            }
            set
            {
                SetValue(ui_TextIntProperty, value);
            }
        }

        private long TextLong_Ui_ = 0;
        /// <summary>
        /// Long值
        /// </summary>
        [Category("ui")]
        [Description("Long值")]
        public long ui_TextLong
        {
            get
            {
                return (long)GetValue(ui_TextLongProperty);
            }
            set
            {
                SetValue(ui_TextLongProperty, value);
            }
        }


        /// <summary>
        /// double值
        /// </summary>
        [Category("ui")]
        [Description("double值")]
        public double ui_TextDouble
        {
            get
            {
                return (double)GetValue(ui_TextDoubleProperty);
            }
            set
            {
                SetValue(ui_TextDoubleProperty, value);
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
                this.TextBox_.Foreground = ForegroundUi_;
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

        private double CornerRadiusUi_ = 0;
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





        private TextAlignment TextAlignmentUi_ = TextAlignment.Center;
        /// <summary>
        /// 文本对齐
        /// </summary>
        [Category("ui")]
        [Description("文本对齐")]
        public TextAlignment ui_TextAlignment
        {
            get
            {
                return TextAlignmentUi_;
            }
            set
            {
                TextAlignmentUi_ = value;
                this.TextBox_.TextAlignment = TextAlignmentUi_;
            }
        }

        

        private VerticalAlignment VerticalContentAlignmentUi_ = VerticalAlignment.Center;
        /// <summary>
        /// 垂直对齐
        /// </summary>
        [Category("ui")]
        [Description("垂直内容对齐方式")]
        public VerticalAlignment ui_VerticalContentAlignment
        {
            get
            {
                return VerticalContentAlignmentUi_;
            }
            set
            {
                VerticalContentAlignmentUi_ = value;
                this.TextBox_.VerticalContentAlignment = VerticalContentAlignmentUi_;
            }
        }

        private HorizontalAlignment HorizontalContentAlignmentUi_ = HorizontalAlignment.Center;
        /// <summary>
        /// 垂直对齐
        /// </summary>
        [Category("ui")]
        [Description("水平内容对齐方式")]
        public HorizontalAlignment ui_HorizontalContentAlignment
        {
            get
            {
                return HorizontalContentAlignmentUi_;
            }
            set
            {
                HorizontalContentAlignmentUi_ = value;
                this.TextBox_.HorizontalContentAlignment = HorizontalContentAlignmentUi_;
            }
        }

        private HorizontalAlignment HorizontalAlignmentUi_ = HorizontalAlignment.Stretch;
        /// <summary>
        /// 获取或设置当此元素嵌入到父元素（如面板或项目控件）中时应用于该元素的水平对齐特性。
        /// </summary>
        [Category("ui")]
        [Description("获取或设置当此元素嵌入到父元素（如面板或项目控件）中时应用于该元素的水平对齐特性。")]
        public HorizontalAlignment ui_HorizontalAlignment
        {
            get
            {
                return HorizontalAlignmentUi_;
            }
            set
            {
                HorizontalAlignmentUi_ = value;
                this.TextBox_.HorizontalAlignment = HorizontalAlignmentUi_;
            }
        }


        private VerticalAlignment VerticalAlignmentUi_ = VerticalAlignment.Stretch;
        /// <summary>
        /// 垂直对齐
        /// </summary>
        [Category("ui")]
        [Description("垂直对齐,textBox高度")]
        public VerticalAlignment ui_VerticalAlignment
        {
            get
            {
                return VerticalAlignmentUi_;
            }
            set
            {
                VerticalAlignmentUi_ = value;
                this.TextBox_.VerticalAlignment = VerticalAlignmentUi_;
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




        private double 最小值UI_ = -2147483648;
        /// <summary>
        /// 最小值,int/double有效
        /// </summary>
        [Category("ui")]
        [Description("最小值,int/double有效")]
        public double ui_最小值
        {
            get
            {
                return 最小值UI_;
            }
            set
            {
                最小值UI_ = value;

            }
        }

        private double 最大值UI_ = 2147483647;
        /// <summary>
        /// 最小值,int/double有效
        /// </summary>
        [Category("ui")]
        [Description("最大值,int/double有效")]
        public double ui_最大值
        {
            get
            {
                return 最大值UI_;
            }
            set
            {
                最大值UI_ = value;

            }
        }


        private bool is最大值限制UI_ = true;
        /// <summary>
        /// 使能最大值限制
        /// </summary>
        [Category("ui")]
        [Description("使能最大值限制")]
        public bool ui_is最大值限制
        {
            get
            {
                return is最大值限制UI_;
            }
            set
            {
                is最大值限制UI_ = value;

            }
        }

        private bool is最小值限制UI_ = true;
        /// <summary>
        /// 使能最小值限制
        /// </summary>
        [Category("ui")]
        [Description("使能最小值限制")]
        public bool ui_is最小值限制
        {
            get
            {
                return is最小值限制UI_;
            }
            set
            {
                is最小值限制UI_ = value;

            }
        }


        private uint 小数点位数UI_ = 2;
        /// <summary>
        /// 小数位数,值类型为double时有效
        /// </summary>
        [Category("ui")]
        [Description("小数位数,值类型为double时有效")]
        public uint ui_小数点位数
        {
            get
            {
                return 小数点位数UI_;
            }
            set
            {
                小数点位数UI_ = value;

            }
        }




        #endregion

        private void TextBox__KeyDown(object sender, KeyEventArgs e)
        {
            if (ui_输入值类型 != 输入值类型.String)
            {
                最大最小值处理(this.ui_Text);
                //if (this.ui_输入值类型 == 输入值类型.Double)
                //{
                //    this.ui_Text = 处理小数点(this.ui_TextDouble);
                //}
            }
            Event_KeyDown?.Invoke(sender, e);
        }

        private void TextBox__TextChanged(object sender, TextChangedEventArgs e)
        {
            Event_TextChanged?.Invoke(sender, e);

            #region 处理数据类型

            if (ui_输入值类型 != 输入值类型.String)
            {
                TextBox textBox = sender as TextBox;
                string xt = textBox.Text.Trim();
                if (string.IsNullOrEmpty(xt))
                {
                    xt = "0";
                }

                最大最小值处理(xt);

            }

            #endregion
        }


        private void TextBox__PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Event_PreviewTextInput?.Invoke(sender, e);
            TextBox textBox = sender as TextBox;

            if (ui_输入值类型 != 输入值类型.String)
            {
                string v = e.Text;
                string text = this.ui_Text.Trim();
                if (string.IsNullOrEmpty(text))
                {
                    return;
                }
                else if (v == " ")
                {
                    e.Handled = true;
                    return;
                }


                #region 负号处理

                // 允许负号，但只能在开头
                if (e.Text == "-" && (ui_is最小值限制 && ui_最小值 < 0))
                {
                    e.Handled = textBox.Text.Contains("-") || textBox.SelectionStart != 0;
                    return;
                }

                #endregion

                #region 小数点处理

                if (ui_输入值类型 == 输入值类型.Double)
                {
                    // 允许小数点，但只能有一个
                    if (e.Text == ".")
                    {
                        e.Handled = textBox.Text.Contains(".");
                        return;
                    }
                }

                #endregion

                #region 输入的是否为数字

                //e.Handled = !char.IsDigit(e.Text,0);
                //if (e.Handled)
                //{
                //    return;
                //}

                bool rtv = int.TryParse(v, out int vs);
                e.Handled = !rtv;
                if (!rtv)
                {
                    return;
                }

                #endregion



            }


        }



        private void TextBox__LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string xt = this.ui_Text.Trim();
            if (ui_输入值类型 != 输入值类型.String)
            {
                if (string.IsNullOrEmpty(xt))
                { xt = "0"; }
                最大最小值处理(xt.Trim());
                //if (this.ui_输入值类型 == 输入值类型.Double)
                //{
                //    this.ui_Text = 处理小数点(this.ui_TextDouble);
                //}
            }

            Event_LostFocus?.Invoke(sender, e);
        }


        /// <summary>
        /// 设置焦点
        /// </summary>
        public new void Focus()
        {
            this.TextBox_.Focus();
        }



        #region 内部方法


        string 处理小数点(double value)
        {
            string du = "";
            if (ui_小数点位数 > 0)
            {
                du = "0.".PadRight((int)ui_小数点位数 + 2, '0');
            }
            return value.ToString(du);
        }


        bool 最大最小值处理(string text)
        {
            bool rt = true;
            bool rtMin = true;
            bool rtMax = true;
            switch (ui_输入值类型)
            {
                case 输入值类型.Int:

                    rt = int.TryParse(text.Trim(), out int a);
                    rtMin = 最小值处理(ref a);
                    rtMax = 最大值处理(ref a);
                    if (rt)
                    {
                        rt = !rtMax ? false : !rtMin ? false : true;

                    }
                    this.ui_TextInt = a;
                    this.ui_Text = ui_TextInt.ToString();

                    break;
                case 输入值类型.Long:
                    rt = long.TryParse(text.Trim(), out long b);
                    rtMin = 最小值处理(ref b);
                    rtMax = 最大值处理(ref b);
                    if (rt)
                    {
                        rt = !rtMax ? false : !rtMin ? false : true;

                    }
                    this.ui_TextLong = b;
                    this.ui_Text = ui_TextLong.ToString();

                    break;
                case 输入值类型.Double:
                    rt = double.TryParse(text.Trim(), out double c);
                    rtMin = 最小值处理(ref c);
                    rtMax = 最大值处理(ref c);
                    if (rt)
                    {
                        rt = !rtMax ? false : !rtMin ? false : true;
                    }
                    this.ui_TextDouble = c;
                    //  this.ui_Text = 处理小数点(this.ui_TextDouble);


                    break;

            }

            return rt;
        }


        /// <summary>
        /// =true:ok,=false:ng
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        bool 最小值处理(ref int value)
        {
            if (ui_is最小值限制 && value < ui_最小值)
            {
                value = (int)ui_最小值;
                return false;
            }

            return true;
        }
        /// <summary>
        /// =true:ok,=false:ng
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        bool 最小值处理(ref long value)
        {
            if (ui_is最小值限制 && value < ui_最小值)
            {
                value = (long)ui_最小值;
                return false;
            }

            return true;
        }
        /// <summary>
        /// =true:ok,=false:ng
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        bool 最小值处理(ref double value)
        {
            if (ui_is最小值限制 && value < ui_最小值)
            {
                value = (double)ui_最小值;
                return false;
            }

            return true;
        }


        /// <summary>
        /// =true:ok,=false:ng
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        bool 最大值处理(ref int value)
        {
            if (ui_is最大值限制 && value > ui_最大值)
            {
                value = (int)ui_最大值;
                return false;
            }

            return true;
        }
        /// <summary>
        /// =true:ok,=false:ng
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        bool 最大值处理(ref long value)
        {
            if (ui_is最大值限制 && value > ui_最大值)
            {
                value = (long)ui_最大值;
                return false;
            }

            return true;
        }
        /// <summary>
        /// =true:ok,=false:ng
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        bool 最大值处理(ref double value)
        {
            if (ui_is最大值限制 && value > ui_最大值)
            {
                value = (double)ui_最大值;
                return false;
            }

            return true;
        }


        #endregion

    }

}
