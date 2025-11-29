using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    /// Win_Socket_Client.xaml 的交互逻辑
    /// </summary>
    public partial class Win_Set_Socket : Window
    {


        // 重写 ShowDialog() 方法，直接返回 MessageBoxResult
        public new MessageBoxResult ShowDialog()
        {
            base.ShowDialog();
            // 根据实际按钮点击返回对应结果
            return _selectedResult; // 需要自己实现 GetResult() 逻辑
        }

        private MessageBoxResult _selectedResult = MessageBoxResult.None;





        void Get()
        {
            if (Client is not null)
            {
                Client.参数读写(1);
                this._textBox_Ip.ui_Text = Client._参数.IP;
                this._textBox_Port.ui_TextInt = Client._参数.Port;
            }
            else if (Server is not null)
            {
                Server.参数读写(1);
                this._textBox_Ip.ui_Text = "";
                this._IP.Visibility = Visibility.Collapsed;
                this._textBox_Port.ui_TextInt = Server._参数.Port;
            }

        }

        void Set()
        {
            int port = this._textBox_Port.ui_TextInt;
            if (!uint.TryParse(port.ToString(), out uint ports))
            {
                MessageBox.Show(this, Language_.Get语言("Port为大于0的整数"), "", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (Client is not null)
            {
                string ip = this._textBox_Ip.ui_Text;
                if (!IPAddress.TryParse(ip, out IPAddress ips))
                {
                    MessageBox.Show(this, Language_.Get语言("IP格式错误"), "", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                Client._参数.IP = ip;
                Client._参数.Port = port;
                Client.参数读写(0);
            }
            else if (Server is not null)
            {
                Server._参数.IP = "";
                Server._参数.Port = port;
                Server.参数读写(0);
            }

        }





        qfmain.Socket_Client Client = null;
        qfmain.Socket_Server Server = null;


        public Win_Set_Socket(qfmain.Socket_Client Client_, qfmain.Socket_Server Server_, string Title_)
        {
            InitializeComponent();
            this._标题栏.Inistiall(this, false, true, false);
            this.Client = Client_;
            this.Server = Server_;

            this._标题栏.ui_Text = Title_;

            Get();

        }

        /// <summary>
        /// No
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _Button_No_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// Yes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _Button_Yes_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Set();
            MessageBox.Show(this, Language_.Get语言("保存成功"));
            _selectedResult = MessageBoxResult.OK;
            this.Close();
        }


    }
}
