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

namespace qfPLC
{
    internal partial class Form_FX : Sunny.UI.UIForm
    {

        private string[] _串口名称;
        private int[] _波特率;
        private int[] _数据位;
        private string[] _停止位;
        private string[] _校验位;
        private 三菱_FX _fxsys;

        internal Form_FX(三菱_FX fxsys_,string Title)
        {
            InitializeComponent();
            this._fxsys = fxsys_;
            this.Text = Title;
            this._串口名称 = new qfmain.SerialPort_方法().Get_串口名称();
            this._波特率 = new qfmain.SerialPort_方法().Get_波特率();
            this._数据位 = new qfmain.SerialPort_方法().Get_数据位();
            this._停止位 = new qfmain.SerialPort_方法().Get_停止位();
            this._校验位 = new qfmain.SerialPort_方法().Get_校验位();

            this.uiComboBox_串口.DataSource = this._串口名称;
            this.uiComboBox_波特率.DataSource = this._波特率;
            this.uiComboBox_数据位.DataSource = this._数据位;
            this.uiComboBox_停止位.DataSource = this._停止位;
            this.uiComboBox_校验位.DataSource = this._校验位;
            Show();



            this.uiButton_No.Click += (s, e) => { this.Close(); };
            this.uiButton_Yes.Click += (s, e) => On_Yes();
            this.FormClosing += (s, e) => On_FormClosing();
        }

        private void Form_FX_Load(object sender, EventArgs e)
        {

        }

        private void On_FormClosing()
        {
            this._fxsys = null;
        }

        int Combobox_Selectedindex(Sunny.UI.UIComboBox combobox, object value)
        {
            return combobox.SelectedIndex = combobox.Items.IndexOf(value);
        }

        void Show()
        {
            Combobox_Selectedindex(this.uiComboBox_串口, this._fxsys._参数.串口名称);
            Combobox_Selectedindex(this.uiComboBox_波特率, this._fxsys._参数.波特率);
            Combobox_Selectedindex(this.uiComboBox_数据位, this._fxsys._参数.数据位);

            this.uiComboBox_停止位.SelectedIndex = (int)this._fxsys._参数.停止位;
            this.uiComboBox_校验位.SelectedIndex = (int)this._fxsys._参数.校验位;
        }

        void On_Yes()
        {
            this._fxsys ._参数.串口名称 = this.uiComboBox_串口.SelectedText;
            this._fxsys._参数.波特率 = this._波特率[this.uiComboBox_波特率.SelectedIndex];
            this._fxsys._参数.数据位 = this._数据位[this.uiComboBox_数据位.SelectedIndex];
            this._fxsys._参数.停止位 = (StopBits)this.uiComboBox_停止位.SelectedIndex;
            this._fxsys._参数.校验位 = (Parity)this.uiComboBox_校验位.SelectedIndex;

            this._fxsys.读写参数(0);
            Show();
            MessageBox.Show("OK");
            this.DialogResult = DialogResult.OK;
        }




    }
}
