using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.NetworkInformation;
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

namespace qfWPFmain
{
    /// <summary>
    /// ui_功能栏.xaml 的交互逻辑
    /// </summary>
    public partial class ui_功能栏 : UserControl
    {       /// <summary>
            /// 数据源
            /// </summary>
        private  功能栏_ViewModel DataContext_ = new 功能栏_ViewModel();
        public ui_功能栏()
        {
            InitializeComponent();
            this.DataContext = DataContext_;

            // 系统运行时间
            Binding bindingRun = new Binding("DateTimeRun");//OK         
            this.SetBinding(ui_系统时间类Property, bindingRun);


            //商标1
            Binding binding商标1 = new Binding("Image_商标1");//OK         
            this.SetBinding(ui_商标1Property, binding商标1);

            //商标2 
            Binding binding商标2 = new Binding("Image_商标2");//OK         
            this.SetBinding(ui_商标2Property, binding商标2);

            //商标1宽度
            Binding bindingWidth商标1 = new Binding("Width_商标1");//OK         
            this.SetBinding(ui_Wdith商标1Property, bindingWidth商标1);

            //商标2宽度
            Binding bindingWidth商标2 = new Binding("Width_商标2");//OK         
            this.SetBinding(ui_Wdith商标2Property, bindingWidth商标2);

            //显示信息
            Binding bindingItems = new Binding("Items_显示信息");//OK         
            this.SetBinding(ui_ItemsProperty, bindingItems);

            //显示功能标题
            Binding bindingTitle = new Binding("Title");//OK         
            this.SetBinding(ui_TextProperty, bindingTitle);
        }


        #region 对外接口



        /// <summary>
        /// 设置时间类
        /// </summary>
        public void Set时间类()
        {
            this.DataContext_.计算时间类();
        }

        /// <summary>
        /// 添加功能内容
        /// </summary>
        /// <param name="info"></param>
        public void Add(_状态栏_功能栏_Info_ info)
        {
            this.DataContext_.计算显示功能内容(info);
        }

        public void Set功能标题(string Title)
        {
            this.DataContext_.Title = Title;
        }




        #endregion

        void 商标信息(Image control, double width, ColumnDefinition GridColumnName)
        {
            // double a = control._icon2.Source?.Width ?? 0;
            double a = control.Source is not null ? width : 0;
            GridColumnName.Width = new GridLength(a);
        }

        /// <summary>
        /// 显示完整信息
        /// </summary>
        /// <param name="ShowTitle"></param>
        /// <param name="TextInfo_"></param>
        string TextShow(_状态栏_功能栏_Info_[] TextInfo_)
        {
            string show = this.ui_Text;
            foreach (var s in TextInfo_)
            {
                show += $"【{s.内容}】";
            }

            return show;
        }



        #region 属性

        public static readonly DependencyProperty ui_TextProperty =
DependencyProperty.Register(nameof(ui_Text), typeof(string), typeof(ui_功能栏),
    new FrameworkPropertyMetadata("AutoControl", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_Text_Changed));
        private static void On_ui_Text_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_功能栏 control = d as ui_功能栏;

            // 将新值同步到UserControl内部的UI元素（如TextBlock）


            control.ui_Text = e.NewValue.ToString();
            control._Title.Text = control.TextShow(control.ui_Items);


        }





        public static readonly DependencyProperty ui_ForegroundProperty =
                        DependencyProperty.Register(nameof(ui_Foreground),
                         typeof(Brush),
                        typeof(ui_功能栏),
                        new FrameworkPropertyMetadata(Brushes.Black,
                            FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                            On_ui_Foreground_Changed));
        private static void On_ui_Foreground_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_功能栏 control = d as ui_功能栏;

            // 将新值同步到UserControl内部的UI元素（如TextBlock）


            control._Title.Foreground = (Brush)e.NewValue;
            control._DateRun.Foreground = (Brush)e.NewValue;
            control._Times.Foreground = (Brush)e.NewValue;


        }



        public static readonly DependencyProperty ui_ItemsProperty =
                             DependencyProperty.Register(nameof(ui_Items),
                              typeof(_状态栏_功能栏_Info_[]),
                             typeof(ui_功能栏),
                             new FrameworkPropertyMetadata(new _状态栏_功能栏_Info_[0],
                                 FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                                 On_ui_Items_Changed));
        private static void On_ui_Items_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_功能栏 control = d as ui_功能栏;

            // 将新值同步到UserControl内部的UI元素（如TextBlock）


            control._Title.Text = control.TextShow((_状态栏_功能栏_Info_[])e.NewValue);


        }



        public static readonly DependencyProperty ui_系统时间类Property =
DependencyProperty.Register(nameof(ui_系统时间类), typeof(_功能栏_时间类_), typeof(ui_功能栏),
new FrameworkPropertyMetadata(new _功能栏_时间类_(DateTime.Now, new TimeSpan()), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_系统时间类_Changed));
        private static void On_ui_系统时间类_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_功能栏 control = d as ui_功能栏;
            _功能栏_时间类_ tm = (_功能栏_时间类_)e.NewValue;
            string ymd = tm.Datetimes.ToString("yyyy-MM-dd");
            string Hm = tm.Datetimes.ToString("HH:mm");


            #region 系统运行时间

            string run = tm.RunTime.Days.ToString();
            run += ".";
            run += tm.RunTime.Hours.ToString().PadLeft(2, '0');
            run += ".";
            run += tm.RunTime.Minutes.ToString().PadLeft(2, '0');
            run += ".";
            run += tm.RunTime.Seconds.ToString().PadLeft(2, '0');

            #endregion


            control._Times.Text = Hm;
            control._DateRun.Text = $"{run}\r\n{ymd}";


        }


        public static readonly DependencyProperty ui_商标1Property =
DependencyProperty.Register(nameof(ui_商标1), typeof(BitmapImage), typeof(ui_功能栏),
new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_商标1_Changed));
        private static void On_ui_商标1_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_功能栏 control = d as ui_功能栏;

            control._icon1.Source = (BitmapImage)e.NewValue;


        }

        public static readonly DependencyProperty ui_商标2Property =
DependencyProperty.Register(nameof(ui_商标2), typeof(BitmapImage), typeof(ui_功能栏),
new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_商标2_Changed));
        private static void On_ui_商标2_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_功能栏 control = d as ui_功能栏;


            control._icon2.Source = (BitmapImage)e.NewValue;
            control.商标信息(control._icon2, control.ui_Width商标2, control._Title_icon_2Grid);


        }

        public static readonly DependencyProperty ui_Wdith商标1Property =
DependencyProperty.Register(nameof(ui_Width商标1), typeof(double), typeof(ui_功能栏),
new FrameworkPropertyMetadata(80d, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_Width商标1_Changed));
        private static void On_ui_Width商标1_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_功能栏 control = d as ui_功能栏;
            control.ui_Width商标1 = (double)e.NewValue;

            control.商标信息(control._icon1, control.ui_Width商标1, control._Title_icon_1Grid);

        }

        public static readonly DependencyProperty ui_Wdith商标2Property =
DependencyProperty.Register(nameof(ui_Width商标2), typeof(double), typeof(ui_功能栏),
new FrameworkPropertyMetadata(80d, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_Width商标2_Changed));
        private static void On_ui_Width商标2_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_功能栏 control = d as ui_功能栏;
            control.ui_Width商标2 = (double)e.NewValue;


            control.商标信息(control._icon2, control.ui_Width商标2, control._Title_icon_2Grid);

        }


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


        /// <summary>
        /// 显示信息内容
        /// </summary>
        [Category("ui")]
        [Description("显示信息内容")]
        public _状态栏_功能栏_Info_[] ui_Items
        {
            get
            {
                return (_状态栏_功能栏_Info_[])GetValue(ui_ItemsProperty);
            }
            set
            {
                SetValue(ui_ItemsProperty, value);
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
        /// 系统运行时间及时间类
        /// </summary>
        [Category("ui")]
        [Description("系统运行时间及时间类")]
        public _功能栏_时间类_ ui_系统时间类
        {
            get
            {
                return (_功能栏_时间类_)GetValue(ui_系统时间类Property);
            }
            set
            {
                SetValue(ui_系统时间类Property, value);
            }
        }

        /// <summary>
        /// 商标1
        /// </summary>
        [Category("ui")]
        [Description("商标1")]
        public BitmapImage ui_商标1
        {
            get
            {
                return (BitmapImage)GetValue(ui_商标1Property);
            }
            set
            {
                SetValue(ui_商标1Property, value);
            }
        }

        /// <summary>
        /// 商标2
        /// </summary>
        [Category("ui")]
        [Description("商标2")]
        public BitmapImage ui_商标2
        {
            get
            {
                return (BitmapImage)GetValue(ui_商标2Property);
            }
            set
            {
                SetValue(ui_商标2Property, value);
            }
        }




        ///<summary>
        /// 商标1宽度
        ///</summary>
        [Category("ui")]
        [Description("商标1宽度")]
        public double ui_Width商标1
        {
            get
            {
                return (double)GetValue(ui_Wdith商标1Property);
            }
            set
            {
                SetValue(ui_Wdith商标1Property, value);
            }
        }


        /// <summary>
        /// 商标2宽度
        /// </summary>
        [Category("ui")]
        [Description("商标2宽度")]
        public double ui_Width商标2
        {
            get
            {
                return (double)GetValue(ui_Wdith商标2Property);
            }
            set
            {
                SetValue(ui_Wdith商标2Property, value);
            }
        }








        #endregion






    }
}
