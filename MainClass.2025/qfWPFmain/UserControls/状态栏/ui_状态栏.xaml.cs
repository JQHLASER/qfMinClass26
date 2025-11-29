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
    /// ui_状态栏.xaml 的交互逻辑
    /// </summary>
    public partial class ui_状态栏 : UserControl
    {
        /// <summary>
        /// 数据源
        /// </summary>
        public 状态栏_ViewModel DataContext_ = new 状态栏_ViewModel();
        public ui_状态栏()
        {
            InitializeComponent();
            this.DataContext = DataContext_;


            Binding bindingItems = new Binding("Items_显示信息");//OK         
            this.SetBinding(ui_ItemsProperty, bindingItems);

        }


        #region 对外接口

        /// <summary>
        /// 添加状态栏信息
        /// </summary>
        /// <param name="info"></param>
        public void Add(_状态栏_功能栏_Info_ info)
        {
           this.DataContext_.计算显示功能内容(info);
  
        }

  
      
        #endregion




        /// <summary>
        /// 显示完整信息
        /// </summary>
        /// <param name="ShowTitle"></param>
        /// <param name="TextInfo_"></param>
        string TextShow(_状态栏_功能栏_Info_[] info_)
        {
            string show = string.Empty;
            foreach (var s in info_)
            {
                show += $"【{s.内容}】";             
            }

            return show;
        }



        #region 属性

        public static readonly DependencyProperty ui_ItemsProperty =
DependencyProperty.Register(nameof(ui_Items), typeof(_状态栏_功能栏_Info_[]), typeof(ui_状态栏),
    new FrameworkPropertyMetadata(new _状态栏_功能栏_Info_[0], FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_Items_Changed));
        private static void On_ui_Items_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_状态栏 control = d as ui_状态栏;            
            control._Value.Text = control.TextShow((_状态栏_功能栏_Info_[])e.NewValue);

        }


        public static readonly DependencyProperty ui_ForegroundProperty =
DependencyProperty.Register(nameof(ui_Foreground), typeof(Brush), typeof(ui_状态栏),
new FrameworkPropertyMetadata(Brushes.Black, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_Foreground_Changed));
        private static void On_ui_Foreground_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_状态栏 control = d as ui_状态栏;

            // 将新值同步到UserControl内部的UI元素（如TextBlock）

            control._Value.Foreground = (Brush)e.NewValue;

        }






        /// <summary>
        /// 显示状态内容
        /// </summary>
        [Category("ui")]
        [Description("显示状态内容")]
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












        #endregion

    }
}
