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
    /// Win_语言.xaml 的交互逻辑
    /// </summary>
    public partial class Win_语言设置 : Window
    {
        language_viewModel DataContext_ViewModel = new language_viewModel();
        public Win_语言设置()
        {
            InitializeComponent();
            this.DataContext = DataContext_ViewModel;
            this._标题栏.Inistiall(this, false, true, false,  "", false);


        }



        private void _Combobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = this._Combobox.SelectedIndex;
            if (index == -1)
            {
                return;
            }
            this.DataContext_ViewModel.Selectedindex = index;

            Language_.Config.LangeuageCfg.LangeuageName = this._Combobox.SelectedItem.ToString();
            Language_.读写参数(0);

        }
    }
}
