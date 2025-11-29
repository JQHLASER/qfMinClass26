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
    /// Win_jcz2_调试.xaml 的交互逻辑
    /// </summary>
    public partial class Win_jcz2_调试 : Window
    {
        private viewMoel_jcz2_调试 DataContext_;
        MarkEzd markezd;

        void On_加工状态(qfWork._激光加工状态_ state)
        {
            this.markezd.标题栏状态_加工状态(this._标题栏 );
        }

        public Win_jcz2_调试(MarkEzd markezd_)
        {
            InitializeComponent();
            DataContext_ = new viewMoel_jcz2_调试(markezd_);
            this.DataContext = DataContext_;
            this._标题栏.Inistiall(this, false, true, false);

            this.markezd = markezd_;
            markezd.Event_IO_IN += this.DataContext_.On_In;
            markezd.Event_IO_OUT += this.DataContext_.On_Out;
            markezd.Event_加工状态 += this.On_加工状态;
 

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!this.markezd.Err_出激光标刻中(out string msgErr) ||
                !this.markezd.Err_红光指示中(out msgErr))
            {
                MessageBox.Show(msgErr, "", MessageBoxButton.OK, MessageBoxImage.Error);
                e.Cancel = true;
                return;
            }

            markezd.Event_IO_IN -= this.DataContext_.On_In;
            markezd.Event_IO_OUT -= this.DataContext_.On_Out;
            markezd.Event_加工状态 -= this.On_加工状态;
        }

        private void _Out_0_Event_操作(ushort arg1)
        {
            this.DataContext_.On_SetOut_0(arg1);
        }

        private void _Out_1_Event_操作(ushort arg1)
        {
            this.DataContext_.On_SetOut_1(arg1);
        }

        private void _button_停止_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Task.Run(() => { this.markezd.停止标刻和红光(); });
        }

        private void _button_设置_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.markezd.窗体_设置(this);
        }

        private void _button_红光_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            new Thread(() =>
            {
                bool rt = this.markezd.加工_红光指示(out string msgErr);
                if (!rt)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        MessageBox.Show(msgErr, "", MessageBoxButton.OK, MessageBoxImage.Error);
                    });
                }
            })
            { IsBackground = true }.Start();
        }

        private void _button_标刻_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            new Thread(() =>
            {
                bool rt = this.markezd.标刻(false,out string msgErr);               
                if (!rt)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        MessageBox.Show(msgErr, "", MessageBoxButton.OK, MessageBoxImage.Error);
                    });
                }
            })
            { IsBackground = true }.Start();
        }
    }
}
