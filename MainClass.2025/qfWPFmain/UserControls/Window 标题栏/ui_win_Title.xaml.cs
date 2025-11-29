using qfmain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
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
using System.Xml.Linq;

namespace qfWPFmain
{
    /// <summary>
    /// ui_window_Title.xaml 的交互逻辑
    /// </summary>
    public partial class ui_window_Title : UserControl
    {

        Window window_ = null;


        #region 对外接口

      //  private WindowTitleState _windowTitleState = WindowTitleState.闲置中;


        /// <summary>
        /// 添加标题状态
        /// </summary>
        /// <param name="windowInfo"></param>
        /// <param name="state">状态,=0的状态也要写进去</param>
        public void Add(_windowInfo_[] windowInfo, int state)
        {
            this.DataContext_.Add(windowInfo, state);
        }


        /// <summary>
        /// 获取系统状态
        /// </summary>
        /// <returns></returns>
        public WindowTitleState Get系统状态()
        {
            return this.DataContext_._windowTitleState;
        }


        public bool Err_系统忙(out string msgErr)
        {
            msgErr = string.Empty;
            if (Get系统状态() == WindowTitleState.加工中)
            {
                msgErr = Language_.Get语言("系统忙");
                return false;
            }
            return true;
        }

        public bool Err_系统报警中(out string msgErr)
        {
            msgErr = string.Empty;
            if (Get系统状态() == WindowTitleState.报警中)
            {
                msgErr = Language_.Get语言("系统报警中");
                return false;
            }
            return true;
        }



        #endregion

        private WindowTitle_ViewModel DataContext_ = new WindowTitle_ViewModel();
        public ui_window_Title()
        {
            InitializeComponent();
            this.DataContext = DataContext_;
            //Items_WindowInfo
            //Binding Items_WindowInfo = new Binding("Items_WindowInfo");
            //this.SetBinding(ui_Items_WindowInfoProperty, Items_WindowInfo);

        }

        /// <summary>
        /// 记录默认背影颜色
        /// </summary>
        Brush Background_ui = Brushes.WhiteSmoke;
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Inistiall(Window d, bool 启动时最大化 = true, bool 显示任务栏 = true, bool 任务栏中显示Icon = true, string Title = null, bool is启动输入法 = true)
        {
            // window_ = new Window窗体_().GetParentWindow(this);
            this.ui_is启动时最大化显示 = 启动时最大化;
            this.ui_is任务栏中显示Icon = 任务栏中显示Icon;
            this.ui_is显示任务栏 = 显示任务栏;

            window_ = d;
            window_.SizeChanged += Window_SizeChanged;
            //window_.Loaded += Window_Loaded;
            Background_ui = this.Background;
            if (Title is not null)
            {
                this.ui_Text = Title;
            }

            imgMax = window_.WindowState == WindowState.Maximized ? img_最大化 : img_最大化双;
            Max_state(Stretch.Uniform);

            window_.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            //边框线
            window_.BorderBrush = Brushes.Silver;
            window_.BorderThickness = new Thickness(0);

            //显示任务栏
            if (this.ui_is显示任务栏)
            {
                window_.ResizeMode = ResizeMode.CanResize;
                window_.MaxWidth = SystemParameters.WorkArea.Width + this.ui_最大化时窗体补偿值;
                window_.MaxHeight = SystemParameters.WorkArea.Height + this.ui_最大化时窗体补偿值;
            }
            //使能输入法
            InputMethod.SetIsInputMethodEnabled(window_, is启动输入法);

            window_.ShowInTaskbar = this.ui_is任务栏中显示Icon;
            window_.WindowState = this.ui_is启动时最大化显示 ? WindowState.Maximized : WindowState.Normal;
            this.DataContext_.默认背景色 = this.Background;
          
                 
        }

        bool isWindow()
        {
            if (window_ is null)
            {
                return false;
            }
            return true;
        }


        private void BorderBrush鼠标进入时(Border border, double 边框宽度)
        {
            border.BorderBrush = this.ui_BorderBrush鼠标进入时;
            border.BorderThickness = new Thickness(边框宽度);

        }



        #region 事件

        /// <summary>
        /// 关闭窗体,在关闭事件中写入 Window_Closing(this, new System.ComponentModel.CancelEventArgs());
        /// </summary>
        public event Action<object, System.ComponentModel.CancelEventArgs> Event_Closing;

        /// <summary>
        /// 最小化窗体
        /// </summary>
        public event Action Event_Min;

        /// <summary>
        /// 最大化窗体
        /// </summary>
        public event Action Event_Max;



        #endregion


        #region 本地事件

        BitmapImage img_最大化双 = new BitmapImage(new Uri("pack://application:,,,/qfWPFmain;component/UserControls/Window 标题栏/Images/最大化双.png", UriKind.Absolute));
        BitmapImage img_最大化 = new BitmapImage(new Uri("pack://application:,,,/qfWPFmain;component/UserControls/Window 标题栏/Images/最大化.png", UriKind.Absolute));
        BitmapImage imgMax = null;

        BitmapImage img_Min = new BitmapImage(new Uri("pack://application:,,,/qfWPFmain;component/UserControls/Window 标题栏/Images/最小化.png", UriKind.Absolute));
        BitmapImage img_Close = new BitmapImage(new Uri("pack://application:,,,/qfWPFmain;component/UserControls/Window 标题栏/Images/关闭.png", UriKind.Absolute));


        private void _Min_MouseEnter(object sender, MouseEventArgs e)
        {
            BorderBrush鼠标进入时(this._Min, 1);

            //this._Min.Background = new ImageBrush
            //{
            //    ImageSource = img_Min,
            //    Stretch = Stretch.Fill  // 背景图片填充方式
            //};

        }

        private void _Min_MouseLeave(object sender, MouseEventArgs e)
        {
            BorderBrush鼠标进入时(this._Min, 0);
            //this._Min.Background = new ImageBrush
            //{
            //    ImageSource = img_Min,
            //    Stretch = Stretch.Uniform  // 背景图片填充方式
            //};


        }


        private void _Max_MouseEnter(object sender, MouseEventArgs e)
        {
            BorderBrush鼠标进入时(this._Max, 1);
            //Max_state(Stretch.Fill);
        }

        private void _Max_MouseLeave(object sender, MouseEventArgs e)
        {
            BorderBrush鼠标进入时(this._Max, 0);
            // Max_state(Stretch.Uniform);
        }

        void Max_state(Stretch Stretch_)
        {
            this._Max.Background = new ImageBrush
            {
                ImageSource = imgMax,
                Stretch = Stretch_  // 背景图片填充方式
            };

        }

        private void _Close_MouseEnter(object sender, MouseEventArgs e)
        {
            BorderBrush鼠标进入时(this._Close, 1);
            //this._Close.Background = new ImageBrush
            //{
            //    ImageSource = img_Close,
            //    Stretch = Stretch.Fill  // 背景图片填充方式
            //};
        }

        private void _Close_MouseLeave(object sender, MouseEventArgs e)
        {
            BorderBrush鼠标进入时(this._Close, 0);
            //this._Close.Background = new ImageBrush
            //{
            //    ImageSource = img_Close,
            //    Stretch = Stretch.Uniform  // 背景图片填充方式
            //};


        }


        private void _Close_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!isWindow())
            {
                return;
            }
            window_.Close();
            Event_Closing?.Invoke(null, null);
        }

        private void _Min_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!isWindow())
            {
                return;
            }

            window_.WindowState = WindowState.Minimized;

            Event_Min?.Invoke();
        }

        private void _Max_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!isWindow())
            {
                return;
            }
            window_.WindowState = window_.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
            Event_Max?.Invoke();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {

            if (window_.WindowState == WindowState.Maximized)
            {
                imgMax = img_最大化双;
            }
            else if (window_.WindowState == WindowState.Normal)
            {
                imgMax = img_最大化;
            }
            Max_state(Stretch.Uniform);
        }


        //DateTime now_ = DateTime.Now;
        //int a = 0;
        //double  双击间隔 = 200;
        private void 标题栏_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!isWindow())
            {
                return;
            }
            if (e.LeftButton == MouseButtonState.Pressed)
            {

                window_.DragMove();

                #region 双击最大化


                //if (a == 0)
                //{
                //    now_ = DateTime.Now;
                //    a = 1;
                //}
                //else
                //{
                //    DateTime now = DateTime.Now;
                //    TimeSpan elapsed = now - now_;
                //    double ms = elapsed.TotalMilliseconds;

                //    // 判断是否在双击时间间隔内
                //    if (ms <= 双击间隔)
                //    {                      
                //        a = 0;
                //        // 触发双击事件处理
                //        if (window_.WindowState != WindowState.Maximized)
                //        {                    
                //            window_.WindowState = WindowState.Maximized;
                //        }
                //        // 重置计时器
                //        now_ = DateTime.MinValue;
                //    }
                //    else
                //    {
                //        now_ = now;
                //        a = 0;
                //    }
                //}


                #endregion

            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (!isWindow())
            {
                return;
            }
            window_.WindowState = this.ui_is启动时最大化显示 ? WindowState.Maximized : WindowState.Normal;
        }

        #endregion


        #region 属性

        public static readonly DependencyProperty ui_TextProperty =
DependencyProperty.Register(nameof(ui_Text), typeof(string), typeof(ui_window_Title),
    new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_Text_Changed));
        private static void On_ui_Text_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_window_Title control = d as ui_window_Title;

            // 将新值同步到UserControl内部的UI元素（如TextBlock）
            control.DataContext_.Title = e.NewValue.ToString();


        }


        public static readonly DependencyProperty ui_IsMinButtonProperty =
DependencyProperty.Register(nameof(ui_IsMinButton), typeof(bool), typeof(ui_window_Title),
  new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_IsMinButton_Changed));
        private static void On_ui_IsMinButton_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_window_Title control = d as ui_window_Title;

            bool rt = control.ui_isBox ? (bool)e.NewValue : false;
            control._MinGrid.Width = new GridLength(rt ? 45 : 0);

        }


        public static readonly DependencyProperty ui_IsMaxButtonProperty =
DependencyProperty.Register(nameof(ui_IsMaxButton), typeof(bool), typeof(ui_window_Title),
new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_IsMaxButton_Changed));
        private static void On_ui_IsMaxButton_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_window_Title control = d as ui_window_Title;

            bool rt = control.ui_isBox ? (bool)e.NewValue : false;
            control._MaxGrid.Width = new GridLength(rt ? 45 : 0);

        }

        public static readonly DependencyProperty ui_IsCloseButtonProperty =
DependencyProperty.Register(nameof(ui_IsCloseButton), typeof(bool), typeof(ui_window_Title),
new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_IsCloseButton_Changed));
        private static void On_ui_IsCloseButton_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_window_Title control = d as ui_window_Title;
            bool rt = control.ui_isBox ? (bool)e.NewValue : false;
            control._CloseGrid.Width = new GridLength(rt ? 45 : 0);


        }

        public static readonly DependencyProperty ui_ForegroundProperty =
DependencyProperty.Register(nameof(ui_Foreground), typeof(Brush), typeof(ui_window_Title),
new FrameworkPropertyMetadata(Brushes.Black, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_Foreground_Changed));
        private static void On_ui_Foreground_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_window_Title control = d as ui_window_Title;

            // 将新值同步到UserControl内部的UI元素（如TextBlock）
            control._标题栏_TextBlock.Foreground = (Brush)e.NewValue;

        }


        public static readonly DependencyProperty ui_Items_WindowInfoProperty =
DependencyProperty.Register(nameof(ui_Items_WindowInfo), typeof(_windowInfo_[]), typeof(ui_window_Title),
new FrameworkPropertyMetadata(new _windowInfo_[0], FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_WindowInfo_Changed));
        private static void On_ui_WindowInfo_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //ui_window_Title control = d as ui_window_Title??new ui_window_Title ();

            //if (control.is普标题栏Ui_)
            //{
            //    return;
            //}

            //#region 计算
            //_windowInfo_[] beff = (_windowInfo_[])e.NewValue;
            //string show = "";
            ////=0:None,=-1:红色,=1:黄色
            //int a = 0;
            //foreach (var s in beff)
            //{
            //    //状态为0时不参与计算
            //    if (s.状态 == 0)
            //    {
            //        continue;
            //    }
            //    show += $"【{s.内容}】";
            //    if (s.状态 < 0 && a >= 0)
            //    {
            //        a = -1;//红色 
            //    }
            //    else if (s.状态 > 0 && a == 0)
            //    {
            //        a = 1;//黄色
            //    }
            //}


            //#endregion


            //// 将新值同步到UserControl内部的UI元素（如TextBlock）

            //control.DataContext_.Title = beff.Length == 0 ? control.ui_TextDefault : show;
            //control.Background = a == -1 ? Brushes.Red : a == 1 ? Brushes.Yellow : control.Background_ui;
            //control._windowTitleState = a == -1 ? WindowTitleState.报警中 : a == 1 ? WindowTitleState.加工中 : WindowTitleState.闲置中;
        }


        public static readonly DependencyProperty ui_FontSizeProperty =
DependencyProperty.Register(nameof(ui_FontSize), typeof(double), typeof(ui_window_Title),
new FrameworkPropertyMetadata(14d, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_FontSize_Changed));
        private static void On_ui_FontSize_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_window_Title control = d as ui_window_Title;

            // 将新值同步到UserControl内部的UI元素（如TextBlock）
            control._标题栏_TextBlock.FontSize = (double)e.NewValue;

        }






        [Category("ui")]
        [Description(" ")]
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


        // private string ContentUi_ = "Label";
        /// <summary>
        /// 显示标题内容
        /// </summary>
        [Category("ui")]
        [Description("显示标题内容")]
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

        string TextDefaultUi_ = "";
        /// <summary>
        /// 默认标题内容
        /// </summary>
        [Category("ui")]
        [Description("默认标题内容")]
        public string ui_TextDefault
        {
            get
            {
                return TextDefaultUi_;
            }
            set
            {
                TextDefaultUi_ = value;
                this.DataContext_.默认标题 = this.TextDefaultUi_;
            }
        }



        /// <summary>
        /// 文本颜色
        /// </summary>
        [Category("ui")]
        [Description("文本颜色")]
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



        /// <summary>
        /// 使能最小化按钮
        /// </summary>
        [Category("ui")]
        [Description("使能最小化按钮")]
        public bool ui_IsMinButton
        {
            get
            {
                return (bool)GetValue(ui_IsMinButtonProperty);
            }
            set
            {
                SetValue(ui_IsMinButtonProperty, value);

            }
        }


        /// <summary>
        /// 使能最大化按钮
        /// </summary>
        [Category("ui")]
        [Description("使能最大化按钮")]
        public bool ui_IsMaxButton
        {
            get
            {
                return (bool)GetValue(ui_IsMaxButtonProperty);
            }
            set
            {
                SetValue(ui_IsMaxButtonProperty, value);

            }
        }

        /// <summary>
        /// 使能关闭按钮
        /// </summary>
        [Category("ui")]
        [Description("使能关闭按钮")]
        public bool ui_IsCloseButton
        {
            get
            {
                return (bool)GetValue(ui_IsCloseButtonProperty);
            }
            set
            {
                SetValue(ui_IsCloseButtonProperty, value);

            }
        }


        /// <summary>
        /// 标题栏状态
        /// </summary>
        [Category("ui")]
        [Description("标题栏状态")]
        public _windowInfo_[] ui_Items_WindowInfo
        {
            get
            {
                return (_windowInfo_[])GetValue(ui_Items_WindowInfoProperty);
            }
            set
            {
                SetValue(ui_Items_WindowInfoProperty, value);

            }
        }




        private bool is显示任务栏Ui_ = true;
        /// <summary>
        /// 显示任务栏
        /// </summary>
        [Category("ui")]
        [Description("显示任务栏")]
        public bool ui_is显示任务栏
        {
            get
            {
                return is显示任务栏Ui_;
            }
            set
            {
                is显示任务栏Ui_ = value;

            }
        }

        private bool is启动时最大化显示Ui_ = true;
        /// <summary>
        /// 启动时最大化显示
        /// </summary>
        [Category("ui")]
        [Description("启动时最大化显示")]
        public bool ui_is启动时最大化显示
        {
            get
            {
                return is启动时最大化显示Ui_;
            }
            set
            {
                is启动时最大化显示Ui_ = value;

            }
        }

        private bool is任务栏中显示iconUi_ = true;
        /// <summary>
        /// 任务栏中显示Icon
        /// </summary>
        [Category("ui")]
        [Description("任务栏中显示Icon")]
        public bool ui_is任务栏中显示Icon
        {
            get
            {
                return is任务栏中显示iconUi_;
            }
            set
            {
                is任务栏中显示iconUi_ = value;

            }
        }


        private int 最大化时窗体补偿值Ui_ = 15;
        /// <summary>
        /// 最大化时窗体补偿值
        /// </summary>
        [Category("ui")]
        [Description("最大化时窗体补偿值")]
        public int ui_最大化时窗体补偿值
        {
            get
            {
                return 最大化时窗体补偿值Ui_;
            }
            set
            {
                最大化时窗体补偿值Ui_ = value;

            }
        }


        private Brush 鼠标进入时边框颜色Ui_ = Brushes.Gray;
        /// <summary>
        /// 鼠标进入时边框颜色,最大化,最小化,关闭会出现边框
        /// </summary>
        [Category("ui")]
        [Description("鼠标进入时边框颜色,最大化,最小化,关闭会出现边框")]
        public Brush ui_BorderBrush鼠标进入时
        {
            get
            {
                return 鼠标进入时边框颜色Ui_;

            }
            set
            {
                鼠标进入时边框颜色Ui_ = value;
            }
        }

        private bool isBoxUi_ = true;
        /// <summary>
        /// 显示操作按钮,最大化,最小化,关闭
        /// </summary>
        [Category("ui")]
        [Description("显示操作按钮,最大化,最小化,关闭")]
        public bool ui_isBox
        {
            get
            {
                return isBoxUi_;
            }
            set
            {
                isBoxUi_ = value;
                if (!isBoxUi_)
                {
                    this.ui_IsMaxButton = false;
                    this.ui_IsMinButton = false;
                    this.ui_IsCloseButton = false;
                }
            }
        }


        private bool is普标题栏Ui_ = true;
        /// <summary>
        /// 普通标题栏,则显示标题栏内容,否则显示提示信息
        /// </summary>
        [Category("ui")]
        [Description("普通标题栏,则显示标题栏内容,否则显示提示信息")]
        public bool ui_is普通标题栏
        {
            get
            {
                return is普标题栏Ui_;
            }
            set
            {
                is普标题栏Ui_ = value;
                this.DataContext_.Is普通标题栏UI_ = this.is普标题栏Ui_;
            }
        }




        #endregion



    }
}
