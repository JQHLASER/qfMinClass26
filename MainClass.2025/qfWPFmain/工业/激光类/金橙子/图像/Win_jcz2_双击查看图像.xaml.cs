using qfWork;
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
    /// Win_jcz2_双击查看图像.xaml 的交互逻辑
    /// </summary>
    public partial class Win_jcz2_双击查看图像 : Window
    {
        MarkEzd markezd;
        public Win_jcz2_双击查看图像(MarkEzd markezd_)
        {
            InitializeComponent();
            this._标题栏.Inistiall(this, true, true, false);
            this.markezd = markezd_;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            markezd.显示图像((int)this.ActualHeight - 20, (int)this.ActualHeight - 20, out ImageSource img_, out string msgErr);
            this._image.Source = img_;

        }
    }
}
