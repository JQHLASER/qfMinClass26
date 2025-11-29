using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Windows.Shapes;

namespace qfWPFmain
{
    /// <summary>
    /// Win_软件信息xaml.xaml 的交互逻辑
    /// </summary>
    public partial class Win_软件信息 : Window
    {
        viewModel_软件信息 DataContext_ = new viewModel_软件信息();

        public Win_软件信息(string 版本, string 信息)
        {
            InitializeComponent();
            this._标题栏.Inistiall(this, false , true, false);
            this.DataContext = DataContext_;
            this.DataContext_.Set(信息);
            this._标题栏.ui_Text = 版本;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void _button_Ok_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }


    }
}
