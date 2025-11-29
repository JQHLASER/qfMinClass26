using System;
using System.Collections.Generic;
using System.Drawing;
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
using System.Windows.Shapes;

namespace qfWPFmain
{
    /// <summary>
    /// Win_软件注册.xaml 的交互逻辑
    /// </summary>
    public partial class Win_软件注册 : Window
    {
        软件注册 软件注册_sys;      

        Win_软件注册_viewModel DataContext_;
        public Win_软件注册(软件注册 软件注册_sys_)
        {
            InitializeComponent();
            this.软件注册_sys = 软件注册_sys_;
            DataContext_ = new Win_软件注册_viewModel(软件注册_sys_);
            this.DataContext = DataContext_;
            this._标题栏.Inistiall(this, false, true, false, $"{Language_.Get语言("软件授权")}--{软件注册_sys.版本}", false);
             
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }


        private void _button_注册_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            bool rt = this.DataContext_.注册(this.软件注册_sys, out string msgErr);
            MessageBoxImage image = rt ? MessageBoxImage.None : MessageBoxImage.Error;
            MessageBox.Show(this, msgErr, "", MessageBoxButton.OK, image);

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.DataContext_.释放();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F12)
            {
                this.DataContext_.TcpClien_设置窗体(this);
            }
        }

        private void CheckBox_试用_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.DataContext_.试用();
        }


    }
}
