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
using System.Windows.Shapes;

namespace qfWPFmain
{
    /// <summary>
    /// Win_进度条.xaml 的交互逻辑
    /// </summary>
    public partial class Win_进度条 : Window
    {
        viewModel_进度条 DataContext_ = new viewModel_进度条();
        internal Win_进度条(string 标题)
        {
            InitializeComponent();
            this.DataContext = DataContext_;
            this.DataContext_.Title = 标题;
            new Thread(() => { this.DataContext_.线程(); }) { IsBackground = true }.Start();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.DataContext_.释放();
        }
    }
}
