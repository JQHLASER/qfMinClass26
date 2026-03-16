using HslCommunication.Profinet.Siemens;
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
    public partial class Form_西门子S7 : Sunny.UI.UIForm
    {

        public Form_西门子S7(S7 s7_, string Title)
        {
            InitializeComponent();

            this.ui_Combobox2_PlcType._ComboBox.DataSource = Enum.GetNames(typeof(SiemensPLCS));
            this.Text = Title;
            show(s7_);

            this.uiButton_No.Click += (s, e) =>
            {
                this.Close();
            };
            this.uiButton_yes.Click += (s, e) =>
            {
                s7_._参数.IP = this.uiTextBox_ip.Text;
                s7_._参数.Port = (uint)this.uiTextBox_port.IntValue;
                s7_._参数.PlcType = (SiemensPLCS)this.ui_Combobox2_PlcType._ComboBox.SelectedIndex + 1;

                //保存参数
                s7_.读写参数(0);
                MessageBox.Show("ok");
                show(s7_);
                this.DialogResult = DialogResult.OK;
            };
        }

        void show(S7 s7_)
        {
            this.uiTextBox_ip.Text = s7_._参数.IP;
            this.uiTextBox_port.IntValue = (int)s7_._参数.Port;
            this.ui_Combobox2_PlcType._ComboBox.SelectedIndex = (int)s7_._参数.PlcType - 1;

        }


    }
}
