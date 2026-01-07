using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace qf_Laser 
{
    public partial class Form_jcz单头_设置 : Sunny.UI.UIForm
    {
        //双缓冲显示窗体所有子控件
        protected override CreateParams CreateParams { get { CreateParams cp = base.CreateParams; cp.ExStyle |= 0x02000000; return cp; } }
       MarkEzd_Ezd2  _markEzd = null;
       MultilineMarkEzd _multilineMarkEzd = null;
        int _CardIndex = -1;

        public Form_jcz单头_设置(MarkEzd_Ezd2 markEzd_,MultilineMarkEzd multilineMarkEzd = null, int Cardindex = -1)
        {
            InitializeComponent();
            this._multilineMarkEzd = multilineMarkEzd;
            this._markEzd = markEzd_;
            this._CardIndex = Cardindex;


            端口初始化(this.uiComboBox_IN_启动标刻);
            端口初始化(this.uiComboBox_IN_复位);
            端口初始化(this.uiComboBox_IN_停止);

            端口初始化(this.uiComboBox_OUT_ready);
            端口初始化(this.uiComboBox_OUT_报警);
            端口初始化(this.uiComboBox_OUT_标刻中);
            端口初始化(this.uiComboBox_OUT_标刻完成);
            端口初始化(this.uiComboBox_OUT_红光);



            if (this._multilineMarkEzd != null)
            {
                this.Text = $"Laser {this._CardIndex + 1}";
                this.uiLabel_IN_停止.Visible = false;
                this.uiComboBox_IN_停止.Visible = false;

                this.uiCheckBox_红光指示外框.Visible = false;
                this.uiCheckBox_进入时加载模板.Visible = false;
                this.uiCheckBox_加工时使能红光.Visible = false;


                this.uiTextBox_激光软件名称.Visible = false;
                this.uiLabel_激光软件名称.Visible = false;

                this.uiLabel_线程周期.Visible = false;
                this.uiTextBox_线程周期.Visible = false;


            }


            Show();
            this.uiButton_No.Click += (s, e) => No();
            this.uiButton_Yes.Click += (s, e) => Yes();
            this.FormClosing += (s, e) => FormClosing_();
        }

        private void Form_jcz单头_设置_Load(object sender, EventArgs e)
        {

        }
        private void FormClosing_()
        {
            this._markEzd = null;
        }



        void 端口初始化(UIComboBox combobox)
        {
            combobox.Items.Clear();
            for (int i = 0; i < 16; i++)
            {
                combobox.Items.Add($"{i}");
            }
            combobox.Items.Add("Null");
        }

        void No()
        {
            this.Close();
        }

        void Yes()
        {
            if (this._markEzd != null)
            {
                this._markEzd._参数.IN.启动标刻 = (short)this.uiComboBox_IN_启动标刻.SelectedIndex;
                this._markEzd._参数.IN.复位 = (short)this.uiComboBox_IN_复位.SelectedIndex;
                this._markEzd._参数.IN.停止 = (short)this.uiComboBox_IN_停止.SelectedIndex;

                this._markEzd._参数.OUT.软件准备好 = (short)this.uiComboBox_OUT_ready.SelectedIndex;
                this._markEzd._参数.OUT.报警 = (short)this.uiComboBox_OUT_报警.SelectedIndex;
                this._markEzd._参数.OUT.标刻中 = (short)this.uiComboBox_OUT_标刻中.SelectedIndex;
                this._markEzd._参数.OUT.标刻完成 = (short)this.uiComboBox_OUT_标刻完成.SelectedIndex;
                this._markEzd._参数.OUT.红光 = (short)this.uiComboBox_OUT_红光.SelectedIndex;

                this._markEzd._参数.OUT.输出脉宽 = this.uiTextBox_OUT_输出脉宽.IntValue;

                this._markEzd._参数.线程周期 = this.uiTextBox_线程周期.IntValue;
                this._markEzd._参数.连续加工周期 = this.uiTextBox_连续加工周期.IntValue;
                this._markEzd._参数.激光软件名称 = this.uiTextBox_激光软件名称.Text;

                this._markEzd._参数.加工时使能红光 = this.uiCheckBox_加工时使能红光.Checked;
                this._markEzd._参数.双击查看图像 = this.uiCheckBox_双击查看图像.Checked;
                this._markEzd._参数.红光指示轮廓 = this.uiCheckBox_红光指示外框.Checked;
                this._markEzd._参数.进入时加载激光模板 = this.uiCheckBox_进入时加载模板.Checked;

                this._markEzd.读写参数(0);
                MessageBox.Show("OK");
            }
            else if (this._multilineMarkEzd != null)
            {

                this._multilineMarkEzd._lst_参数[this._CardIndex]._参数.IN.启动标刻 = (short)this.uiComboBox_IN_启动标刻.SelectedIndex;
                this._multilineMarkEzd._lst_参数[this._CardIndex]._参数.IN.复位 = (short)this.uiComboBox_IN_复位.SelectedIndex;
                this._multilineMarkEzd._lst_参数[this._CardIndex]._参数.IN.停止 = (short)this.uiComboBox_IN_停止.SelectedIndex;

                this._multilineMarkEzd._lst_参数[this._CardIndex]._参数.OUT.软件准备好 = (short)this.uiComboBox_OUT_ready.SelectedIndex;
                this._multilineMarkEzd._lst_参数[this._CardIndex]._参数.OUT.报警 = (short)this.uiComboBox_OUT_报警.SelectedIndex;
                this._multilineMarkEzd._lst_参数[this._CardIndex]._参数.OUT.标刻中 = (short)this.uiComboBox_OUT_标刻中.SelectedIndex;
                this._multilineMarkEzd._lst_参数[this._CardIndex]._参数.OUT.标刻完成 = (short)this.uiComboBox_OUT_标刻完成.SelectedIndex;
                this._multilineMarkEzd._lst_参数[this._CardIndex]._参数.OUT.红光 = (short)this.uiComboBox_OUT_红光.SelectedIndex;

                this._multilineMarkEzd._lst_参数[this._CardIndex]._参数.OUT.输出脉宽 = this.uiTextBox_OUT_输出脉宽.IntValue;

                this._multilineMarkEzd._lst_参数[this._CardIndex]._参数.线程周期 = this.uiTextBox_线程周期.IntValue;
                this._multilineMarkEzd._lst_参数[this._CardIndex]._参数.连续加工周期 = this.uiTextBox_连续加工周期.IntValue;
                this._multilineMarkEzd._lst_参数[this._CardIndex]._参数.激光软件名称 = this.uiTextBox_激光软件名称.Text;

                this._multilineMarkEzd._lst_参数[this._CardIndex]._参数.加工时使能红光 = this.uiCheckBox_加工时使能红光.Checked;
                this._multilineMarkEzd._lst_参数[this._CardIndex]._参数.双击查看图像 = this.uiCheckBox_双击查看图像.Checked;
                this._multilineMarkEzd._lst_参数[this._CardIndex]._参数.红光指示轮廓 = this.uiCheckBox_红光指示外框.Checked;
                this._multilineMarkEzd._lst_参数[this._CardIndex]._参数.进入时加载激光模板 = this.uiCheckBox_进入时加载模板.Checked;

                if (this._multilineMarkEzd.读写参数_参数(0, out string msgErr))
                {
                    MessageBox.Show("OK");
                }
                else
                {
                    MessageBox.Show(msgErr, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }




        }

        void Show()
        {
            if (this._markEzd != null)
            {
                this.uiComboBox_IN_启动标刻.SelectedIndex = this._markEzd._参数.IN.启动标刻;
                this.uiComboBox_IN_复位.SelectedIndex = this._markEzd._参数.IN.复位;
                this.uiComboBox_IN_停止.SelectedIndex = this._markEzd._参数.IN.停止;

                this.uiComboBox_OUT_ready.SelectedIndex = this._markEzd._参数.OUT.软件准备好;
                this.uiComboBox_OUT_报警.SelectedIndex = this._markEzd._参数.OUT.报警;
                this.uiComboBox_OUT_标刻中.SelectedIndex = this._markEzd._参数.OUT.标刻中;
                this.uiComboBox_OUT_标刻完成.SelectedIndex = this._markEzd._参数.OUT.标刻完成;
                this.uiComboBox_OUT_红光.SelectedIndex = this._markEzd._参数.OUT.红光;

                this.uiTextBox_OUT_输出脉宽.IntValue = this._markEzd._参数.OUT.输出脉宽;

                this.uiTextBox_线程周期.IntValue = this._markEzd._参数.线程周期;
                this.uiTextBox_连续加工周期.IntValue = this._markEzd._参数.连续加工周期;
                this.uiTextBox_激光软件名称.Text = this._markEzd._参数.激光软件名称;

                this.uiCheckBox_加工时使能红光.Checked = this._markEzd._参数.加工时使能红光;
                this.uiCheckBox_双击查看图像.Checked = this._markEzd._参数.双击查看图像;
                this.uiCheckBox_红光指示外框.Checked = this._markEzd._参数.红光指示轮廓;
                this.uiCheckBox_进入时加载模板.Checked = this._markEzd._参数.进入时加载激光模板;
            }
            else if (this._multilineMarkEzd != null)
            {
               MultilineMarkEzd._cfg_参数_ cfg = this._multilineMarkEzd._lst_参数[this._CardIndex];


                this.uiComboBox_IN_启动标刻.SelectedIndex = cfg._参数.IN.启动标刻;
                this.uiComboBox_IN_复位.SelectedIndex = cfg._参数.IN.复位;
                this.uiComboBox_IN_停止.SelectedIndex = cfg._参数.IN.停止;

                this.uiComboBox_OUT_ready.SelectedIndex = cfg._参数.OUT.软件准备好;
                this.uiComboBox_OUT_报警.SelectedIndex = cfg._参数.OUT.报警;
                this.uiComboBox_OUT_标刻中.SelectedIndex = cfg._参数.OUT.标刻中;
                this.uiComboBox_OUT_标刻完成.SelectedIndex = cfg._参数.OUT.标刻完成;
                this.uiComboBox_OUT_红光.SelectedIndex = cfg._参数.OUT.红光;

                this.uiTextBox_OUT_输出脉宽.IntValue = cfg._参数.OUT.输出脉宽;

                this.uiTextBox_线程周期.IntValue = cfg._参数.线程周期;
                this.uiTextBox_连续加工周期.IntValue = cfg._参数.连续加工周期;
                this.uiTextBox_激光软件名称.Text = cfg._参数.激光软件名称;

                this.uiCheckBox_加工时使能红光.Checked = cfg._参数.加工时使能红光;
                this.uiCheckBox_双击查看图像.Checked = cfg._参数.双击查看图像;
                this.uiCheckBox_红光指示外框.Checked = cfg._参数.红光指示轮廓;
                this.uiCheckBox_进入时加载模板.Checked = cfg._参数.进入时加载激光模板;



            }


        }





    }
}
