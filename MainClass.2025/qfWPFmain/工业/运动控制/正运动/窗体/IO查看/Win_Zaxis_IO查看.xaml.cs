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
    /// Win_Zaxis_IO查看.xaml 的交互逻辑
    /// </summary>
    public partial class Win_Zaxis_IO查看 : Window
    {
        private Zaxis_ zaxis;
        private viewModel_ZaxisIO杳看 DataContent_ = new viewModel_ZaxisIO杳看();
        public Win_Zaxis_IO查看(Zaxis_ zaxis_)
        {
            InitializeComponent();
            this._标题栏.Inistiall(this, false, true, false);
            this.zaxis = zaxis_;
            this.DataContext = DataContent_;
            this.zaxis.Event_IO输入B += this.DataContent_.On_输入;
            this.zaxis.Event_IO输出B += this.DataContent_.On_输出;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.zaxis.Event_IO输入B -= this.DataContent_.On_输入;
            this.zaxis.Event_IO输出B -= this.DataContent_.On_输出;
        }

        private void ui_IO_Event_操作(ushort obj)
        {
            this.DataContent_.操作(obj, this.zaxis);
        }

        private void ui_IO_Event_操作_1(ushort obj)
        {
            this.DataContent_.操作(obj + 8, this.zaxis);
        }
    }
}
