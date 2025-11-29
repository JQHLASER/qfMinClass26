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
    /// Win_用户管理_添加修改.xaml 的交互逻辑
    /// </summary>
    public partial class Win_用户管理_添加修改 : Window
    {
        viewModel_用户管理_添加修改 _dataContext;
        void Close()
        {
            base.Close();
        }




        internal Win_用户管理_添加修改(viewModel_用户管理._操作_ 操作, Window d_父窗体, viewModel_用户管理 viewmodel)
        {
            InitializeComponent();
            this._标题栏.Inistiall(this, false, true, false);
            new qfWPFmain.Border_按钮栏(this._border按钮栏);
            this._dataContext = new viewModel_用户管理_添加修改(操作, viewmodel);
            this.DataContext = this._dataContext;
            this._dataContext.Event_Close += () => Close();
        }
    }
}
