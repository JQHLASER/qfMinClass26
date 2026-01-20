 
using System;
using System.Collections.Generic;
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
    /// Control_jcz_图像.xaml 的交互逻辑
    /// </summary>
    public partial class ui_jcz_图像_单头 : UserControl
    {

        viewModel_jcz单头_显示图像 DataContent_ = new viewModel_jcz单头_显示图像();

        Label _controLabel_未初始化提示 = new Label()
        {
            FontFamily = new FontFamily("微软雅黑"),
            FontSize = 15,
            Foreground = Brushes.White,
            HorizontalAlignment = HorizontalAlignment.Stretch,
            VerticalAlignment = VerticalAlignment.Stretch,
            VerticalContentAlignment = VerticalAlignment.Center,
            HorizontalContentAlignment = HorizontalAlignment.Center,
            Background = Brushes.Gray,

        };

        Image _control_Img = new Image()
        {
            HorizontalAlignment = HorizontalAlignment.Stretch,
            VerticalAlignment = VerticalAlignment.Stretch,

            Width = double.NaN,
            Height = double.NaN,        
        };


        public ui_jcz_图像_单头()
        {
            InitializeComponent();
            this.DataContext = this.DataContent_;


            this._controLabel_未初始化提示.SetBinding(Label.ContentProperty, new Binding("Text_未初始化提示"));
            this._controLabel_未初始化提示.SetBinding(Image.VisibilityProperty, new Binding("Visibility_未初始化"));

            this._control_Img.SetBinding(Image.SourceProperty, new Binding("Img"));
            this._control_Img.SetBinding(Image.VisibilityProperty , new Binding("Visibility_img"));

            this._grid.Children.Clear();
            this._grid.Children.Add(this._controLabel_未初始化提示);
            this._grid.Children.Add(this._control_Img);


        }
        MarkEzd markezd;
        public void Inistiall(MarkEzd markezd_)
        {
            this.markezd = markezd_;
            this.markezd.Event_获取图像 += On_获取图像;
            this.markezd.Event_初始化状态 += On_初始化状态;
        }


        private readonly object _lock = new object();
        void On_获取图像(qf_Laser._激光_获取图像_ state)
        {
            lock (this._lock)
            {

                if (state == qf_Laser ._激光_获取图像_.获取)
                {
                    this.markezd.显示图像((int)this.DataContent_.Width, (int)this.DataContent_.Height, out ImageSource img_, out string msgErr);
                    this.DataContent_.Img = img_;
                }
                else if (state == qf_Laser._激光_获取图像_.清除)
                {
                    this.DataContent_.Img = new BitmapImage();
                }

            }
        }





        void On_初始化状态(qf_Laser._初始化状态_ state)
        { 
           
            switch (state)
            {

                case qf_Laser._初始化状态_.已初始化:

                    #region 已初始化
 
                     
                    this.DataContent_.Visibility_未初始化 = Visibility.Collapsed;
                    this.DataContent_.Visibility_img = Visibility.Visible;

                     
                    // this.DataContent_.Height_未初始化提示 = new GridLength(0, GridUnitType.Star);
                    //  this.DataContent_.Height_菜单栏 = new GridLength(40);
                    // this.DataContent_.Height_菜单栏 = new GridLength(0, GridUnitType.Star);
                    // this.DataContent_.Height_image = new GridLength(1, GridUnitType.Star);

                    #endregion

                    break;
                case qf_Laser._初始化状态_.未初始化:

                    #region 未初始化

                    // this.DataContent_.Height_未初始化提示 = new GridLength(1, GridUnitType.Star);
                    //this.DataContent_.Height_菜单栏 = new GridLength(0, GridUnitType.Star);
                    //  this.DataContent_.Height_image = new GridLength(0, GridUnitType.Star);

                    this.DataContent_.Visibility_未初始化 = Visibility.Visible ;
                    this.DataContent_.Visibility_img = Visibility.Collapsed;




                    #endregion

                    break;
            }

        }

        private void UserControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (!this.markezd._参数.双击查看图像)
            {
                return;
            }
            this.markezd.窗体_显示图像(Window.GetWindow(this));
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.DataContent_.Width = this.ActualWidth;
            this.DataContent_.Height = this.ActualHeight;
        }
    }
}
