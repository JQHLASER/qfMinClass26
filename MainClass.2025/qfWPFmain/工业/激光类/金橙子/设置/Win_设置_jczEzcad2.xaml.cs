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
    /// Win_设置_jczEzcad2.xaml 的交互逻辑
    /// </summary>
    public partial class Win_设置_jczEzcad2 : Window
    {
       

        ViewModel_jcz2单头_设置 DataContent_;
        public Win_设置_jczEzcad2(MarkEzd markezd_)
        {
            InitializeComponent();
            DataContent_ = new ViewModel_jcz2单头_设置(markezd_);
            this.DataContext = DataContent_;
            this._标题栏.Inistiall(this, false , true, false, "Setting");
          

        }

        private void ui_ButtonBorder_No_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void ui_ButtonBorder_Yes_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.DataContent_.Save();
        }
    }
}
