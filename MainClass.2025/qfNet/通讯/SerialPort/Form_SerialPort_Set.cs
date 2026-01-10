using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace qfNet
{
    internal partial class Form_SerialPort_Set : Sunny.UI.UIForm
    {
        qfmain.SerialPort_ _SerialPort;
        public string[] _串口名称;
        public int[] _波特率;
        public int[] _数据位;
        private string[] _停止位;
        private string[] _校验位;

        internal Form_SerialPort_Set(qfmain.SerialPort_ SerialPort_,string Title)
        {
            InitializeComponent();
            this._SerialPort = SerialPort_;
            this.Text = Title;
            this._串口名称 = this._SerialPort.Get_串口名称();
            this._波特率 = this._SerialPort.Get_波特率();
            this._数据位 = this._SerialPort.Get_数据位();
            this._停止位 = this._SerialPort.Get_停止位();
            this._校验位 = this._SerialPort.Get_校验位();


            this.uiComboBox_串口.DataSource = this._串口名称;
            this.uiComboBox_波特率.DataSource = this._波特率;
            this.uiComboBox_数据位.DataSource = this._数据位;
            this.uiComboBox_停止位.DataSource = this._停止位;
            this.uiComboBox_校验位.DataSource = this._校验位;
            Show();

            this.uiButton_No.Click += (s, e) => Close_();
            this.uiButton_Yes.Click += (s, e) => Yes_();
            this.FormClosing += (s, e) => FormClosing_();
        }

        private void Form_SerialPort_Set_Load(object sender, EventArgs e)
        {

        }
        private void FormClosing_()
        {
            this._SerialPort = null;
        }


        #region 方法

        int Combobox_Selectedindex(Sunny.UI.UIComboBox combobox, object value)
        {
            return combobox.SelectedIndex = combobox.Items.IndexOf(value);
        }
         

        void Show()
        {

            Combobox_Selectedindex(this.uiComboBox_串口, this._SerialPort._参数.串口名称);
            Combobox_Selectedindex(this.uiComboBox_波特率, this._SerialPort._参数.波特率);
            Combobox_Selectedindex(this.uiComboBox_数据位, this._SerialPort._参数.数据位);
         
            this.uiComboBox_停止位.SelectedIndex = (int)this._SerialPort._参数.停止位;
            this.uiComboBox_校验位.SelectedIndex = (int)this._SerialPort._参数.校验位;

        }

        void Close_()
        {
            this.Close();
        }
        void Yes_()
        {
            this._SerialPort._参数.串口名称 = this.uiComboBox_串口.SelectedText;
            this._SerialPort._参数.波特率 = this._波特率[this.uiComboBox_波特率.SelectedIndex];
            this._SerialPort._参数.数据位 = this._数据位[this.uiComboBox_数据位.SelectedIndex];
            this._SerialPort._参数.停止位 = (StopBits)this.uiComboBox_停止位.SelectedIndex;
            this._SerialPort._参数.校验位 = (Parity)this.uiComboBox_校验位.SelectedIndex;

            this._SerialPort.读写参数(0);
            Show();
            MessageBox.Show("OK");
            this.DialogResult = DialogResult.OK;
        }

        #endregion



    }
}
