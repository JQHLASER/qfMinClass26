using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace qfPLC
{
    public partial class Form_ModbusTcp : Sunny.UI.UIForm
    {
        ModbusTcp _modbusTcp;

        public Form_ModbusTcp(ModbusTcp modbusTpc, string Title)
        {
            InitializeComponent();
            this._modbusTcp = modbusTpc;
            this.Text = Title;

            this.uiComboBox_字符串模式.DataSource = Enum.GetNames(typeof(HslCommunication.Core.DataFormat));
            Show();

            this.FormClosing += (s, e) => On_FormClosing();
            this.uiButton_No.Click += (s, e) => On_No();
            this.uiButton_Yes.Click += (s, e) => On_Yes();

        }

        void On_No()
        {
            this.Close();
        }

        void On_Yes()
        {
            this._modbusTcp._参数.IP = this.uiTextBox_IP.Text.Trim();
            this._modbusTcp._参数.Port = (uint)this.uiTextBox_Port.IntValue;
            this._modbusTcp._参数.站号 = (byte)this.uiTextBox_站号.IntValue;
            this._modbusTcp._参数.字符串模式 = (HslCommunication.Core.DataFormat)this.uiComboBox_字符串模式.SelectedIndex;

            this._modbusTcp._参数.字符串是否颠倒 = this.uiCheckBox_字符串颠倒.Checked;
            this._modbusTcp._参数.首地址从0开始 = this.uiCheckBox_首地址从0开始.Checked;

            this._modbusTcp.读写参数(0);
            Show();
            MessageBox.Show("ok");
            this.DialogResult = DialogResult.OK;
        }

        void Show()
        {
            this.uiTextBox_IP.Text = this._modbusTcp._参数.IP;
            this.uiTextBox_Port.IntValue = (int)this._modbusTcp._参数.Port;
            this.uiTextBox_站号.IntValue = this._modbusTcp._参数.站号;
            this.uiComboBox_字符串模式.SelectedIndex = (int)this._modbusTcp._参数.字符串模式;

            this.uiCheckBox_字符串颠倒.Checked = this._modbusTcp._参数.字符串是否颠倒;
            this.uiCheckBox_首地址从0开始.Checked = this._modbusTcp._参数.首地址从0开始;

        }

        void On_FormClosing()
        {
            this._modbusTcp = null;
        }



    }
}
