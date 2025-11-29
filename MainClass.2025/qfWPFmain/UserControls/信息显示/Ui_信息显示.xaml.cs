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
    /// Ui_信息显示.xaml 的交互逻辑
    /// </summary>
    public partial class Ui_信息显示 : UserControl
    {
        viewModel_信息显示 DataContext_ = new viewModel_信息显示();
        public Ui_信息显示()
        {
            InitializeComponent();
            this.DataContext = DataContext_;
        }


        public void Clear()
        {
            this.Dispatcher.Invoke(() =>
            {
                DataContext_.Clear();
            });
        }

        public void Add(_信息显示_ info)
        {
            this.Dispatcher.Invoke(() =>
            {
                DataContext_.Add(info);
            });
        }




    }
}
