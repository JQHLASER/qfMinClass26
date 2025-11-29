

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Printing;
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

namespace qfWPFmain
{
    /// <summary>
    /// ui_Log.xaml 的交互逻辑
    /// </summary>
    public partial class ui_Log : UserControl
    {
        private Log_viewModel DataContext_ = new Log_viewModel();
        public ui_Log()
        {
            InitializeComponent();
            this.DataContext = DataContext_;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.ListView_.Clip = new Clip_().圆角矩形(this.ListView_.ActualWidth, this.ListView_.ActualHeight, CornerRadiusUi_, CornerRadiusUi_);



            // 最大显示行数
            //Binding bindingMaxCount = new Binding("MaxCount");
            //this.SetBinding(ui_MaxCountProperty, bindingMaxCount);



        }


        #region Log日志操作

        /// <summary>
        /// 添加日志,在log添加事件中使用
        /// </summary>
        /// <param name="logvalue"></param>
        public void Add_Log(qfmain.log日志._logValue_ logvalue)
        {
            this.Dispatcher.Invoke(() => { this.DataContext_.Add_Log(logvalue, this.Ui_MaxCount); });
        }


        /// <summary>
        /// 清空日志,在log清除事件中使用
        /// </summary> 
        public void Clear_Log()
        {
            this.DataContext_.Clear_Log();
        }

        public void Width_内容列(double width)
        {
            this.DataContext_.Size内容栏 = width - ui_Width_时间列 - ui_Width_状态列;
          //  this.ListView_ .Background  = Brushes .Red ;
        }

      

        public class _set_
        {
            public int 时间列宽度 { set; get; } = 120;
            public int 状态列宽度 { set; get; } = 0;
            public int 字体大小 { set; get; } = 15;
            public int 显示最大行数 { set; get; } = 200;
        }

        /// <summary>
        /// 需要手动设置时调用
        /// </summary>
        void 读参数()
        {
            if (Ui_is读设置参数)
            {
                new 软件类();
                string path = 软件类.Files_Cfg.Files_sysConfig + "\\controlLog.txt";
                _set_ info = new _set_();
                new 文件_文件夹().WriteReadJson(path, 1, ref info, out string msgErr);

                this.ui_Width_时间列 = info.时间列宽度;
                this.ui_Width_状态列 = info.状态列宽度;
                this.Ui_MaxCount = info.显示最大行数;
                this.ui_FontSize = info.字体大小;
            }
        }

        #endregion

        #region 属性


        public static readonly DependencyProperty ui_MaxCountProperty =
       DependencyProperty.RegisterAttached(nameof(Ui_MaxCount), typeof(int), typeof(ui_Log),
           new PropertyMetadata(200, On_Ui_MaxCount_Changed));
        private static void On_Ui_MaxCount_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }




        private double Width_状态列Ui_ = 0;
        /// <summary>
        /// 状态列宽度,默认75...最小值=1
        /// </summary>
        [Category("ui")]
        [Description("状态列宽度,默认75...最小值=0")]
        public double ui_Width_状态列
        {
            get
            {
                return Width_状态列Ui_;
            }
            set
            {
                Width_状态列Ui_ = value;
                this.Dispatcher.Invoke(() => { this.状态.Width = Width_状态列Ui_; });
            }
        }


        private double Width_时间列Ui_ = 110;
        /// <summary>
        /// 时间列宽度,默认110...最小值=1
        /// </summary>
        [Category("ui")]
        [Description("时间列宽度,默认110...最小值=0")]
        public double ui_Width_时间列
        {
            get
            {
                return Width_时间列Ui_;
            }
            set
            {
                Width_时间列Ui_ = value;
                this.时间.Width = Width_时间列Ui_;
            }
        }



        ///// <summary>
        ///// 日志显示最大行数
        ///// </summary>
        //private int MaxCountUi_ = 200;
        /// <summary>
        /// 日志显示最大行数
        /// </summary>
        [Category("ui")]
        [Description("日志显示最大行数")]
        public int Ui_MaxCount
        {
            get
            {
                return (int)GetValue(ui_MaxCountProperty);
            }
            set
            {
                SetValue(ui_MaxCountProperty, value);
            }
        }


        private FontFamily FontFamilyUi_ = new FontFamily("微软雅黑");
        /// <summary>
        /// 字体
        /// </summary>
        [Category("ui")]
        [Description("字体")]
        public FontFamily ui_FontFamily
        {
            get
            {
                return FontFamilyUi_;
            }
            set
            {
                FontFamilyUi_ = value;
                this.ListView_.FontFamily = FontFamilyUi_;
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
                return FontSizeUi_;
            }
            set
            {
                FontSizeUi_ = value;
                this.ListView_.FontSize = FontSizeUi_;
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

        private Brush BorderBrushUi_ = Brushes.Gray;
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




        private bool is读设置参数Ui_ = false;
        /// <summary>
        /// 是否读设置参数
        /// </summary>
        [Category("ui")]
        [Description("是否读设置参数")]
        public bool Ui_is读设置参数
        {
            get
            {
                return is读设置参数Ui_;
            }
            set
            {
                is读设置参数Ui_ = value;
                读参数();
            }
        }




        #endregion

        private void ListView__PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int index = this.ListView_.SelectedIndex;
            if (index > -1)
            {
                new win_Log查看日志(DataContext_.LogItems[index]) { Owner = Window.GetWindow(this) }.ShowDialog();
            }
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Width_内容列(this.ActualWidth);
        }
    }
}
