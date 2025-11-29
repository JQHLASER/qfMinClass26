using System;
using System.Collections.Generic;
using System.Configuration;
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
    /// Log_查看日志信息.xaml 的交互逻辑
    /// </summary>
    public partial class win_Log查看日志 : Window
    {

        internal win_Log查看日志(info_log_ logInfo_)
        {
            InitializeComponent();
            this._Title.Inistiall(this, false, true, false);

            string value = $"[{logInfo_.States}] {logInfo_.Dates}";
            value += $"\r\n{logInfo_.Logvalue}";
            this.LogValue.Foreground = logInfo_.TextColor;
            this.LogValue.Text = value;
        }




    }
}
