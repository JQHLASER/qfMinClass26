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
    /// Win_Set_SerialPort.xaml 的交互逻辑
    /// </summary>
    public partial class Win_Set_SerialPort : Window
    {

        // 重写 ShowDialog() 方法，直接返回 MessageBoxResult
        public new MessageBoxResult ShowDialog()
        {
            base.ShowDialog();
            // 根据实际按钮点击返回对应结果
            return _selectedResult; // 需要自己实现 GetResult() 逻辑
        }
        private MessageBoxResult _selectedResult = MessageBoxResult.None;


        void Inistiall()
        {
            this._串口名称_Label.Content = Language_.Get语言("串口");
            this._波特率_Label.Content = Language_.Get语言("波特率");
            this._数据位_Label.Content = Language_.Get语言("数据位");
            this._停止位_Label.Content = Language_.Get语言("停止位");
            this._校验位_Label.Content = Language_.Get语言("校验位");


            this._串口名称_Combobox.ItemsSource = this.SerialPort_sis.Get_串口名称();
            this._波特率_Combobox.ItemsSource = this.SerialPort_sis.Get_波特率();
            this._数据位_Combobox.ItemsSource = this.SerialPort_sis.Get_数据位();
            this._停止位_Combobox.ItemsSource = this.SerialPort_sis.Get_停止位();
            this._校验位_Combobox.ItemsSource = this.SerialPort_sis.Get_校验位();

        }

        /// <summary>
        /// 读取参数
        /// </summary>
        void Get()
        {
            this.SerialPort_sis.读写参数(1);
            this._串口名称_Combobox.SelectedIndex = this._串口名称_Combobox.Items.IndexOf(this.SerialPort_sis._参数.串口名称);
            this._波特率_Combobox.SelectedIndex = this._波特率_Combobox.Items.IndexOf(this.SerialPort_sis._参数.波特率);
            this._数据位_Combobox.SelectedIndex = this._数据位_Combobox.Items.IndexOf(this.SerialPort_sis._参数.数据位);
            this._停止位_Combobox.SelectedIndex = this._停止位_Combobox.Items.IndexOf(this.SerialPort_sis._参数.停止位.ToString());
            this._校验位_Combobox.SelectedIndex = this._校验位_Combobox.Items.IndexOf(this.SerialPort_sis._参数.校验位.ToString());
        }

        void Set()
        {

            this.SerialPort_sis._参数.串口名称 = this._串口名称_Combobox.Text ;
            this.SerialPort_sis._参数.波特率 = int.Parse(this._波特率_Combobox.Text );
            this.SerialPort_sis._参数.数据位 = int.Parse(this._数据位_Combobox.Text );
            this.SerialPort_sis._参数.停止位 = (System.IO.Ports.StopBits)this._停止位_Combobox.SelectedIndex;
            this.SerialPort_sis._参数.校验位 = (System.IO.Ports.Parity)this._校验位_Combobox.SelectedIndex;

            this.SerialPort_sis.读写参数(0);
        }

        qfmain.SerialPort_ SerialPort_sis = null;
        public Win_Set_SerialPort(qfmain.SerialPort_ serialport_, string Title_)
        {
            InitializeComponent();
            this._标题栏.Inistiall(this, false, true, false, Title_);
            SerialPort_sis = serialport_;
            Inistiall();
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
            if (string.IsNullOrEmpty(this._串口名称_Combobox.Text))
            {
                MessageBox.Show("请选择串口","",MessageBoxButton.OK,MessageBoxImage.Error);
                return;
            }
            Set();
            MessageBox.Show("保存成功");
            this._selectedResult = MessageBoxResult.OK;
            this.Close();
        }
    }
}
