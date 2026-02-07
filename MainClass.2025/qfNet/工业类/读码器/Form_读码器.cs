using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace qfNet
{
    internal partial class Form_读码器 : Sunny.UI.UIForm
    {
        qfNet.读码器 _readcode;
        internal List<string> _lst前后缀 = new List<string>();
        internal Form_读码器(qfNet.读码器 readcode_)
        {
            InitializeComponent();
            this._readcode = readcode_;


            this.ui_Button2_保存._Button.StyleCustomMode = true;
            this.ui_Button2_保存._Button.Style = Sunny.UI.UIStyle.Blue;

            this.ui_Button2_关闭._Button.StyleCustomMode = true;
            this.ui_Button2_关闭._Button.Style = Sunny.UI.UIStyle.Red;

            this._lst前后缀 = this._readcode.读取前后缀文件().ToList();
            this.con_前后缀.ui_Combobox2_发送_前缀._ComboBox.DataSource = this._lst前后缀;
            this.con_前后缀.ui_Combobox2_发送_后缀._ComboBox.DataSource = this._lst前后缀;
            this.con_前后缀.ui_Combobox2_接收_前缀._ComboBox.DataSource = this._lst前后缀;
            this.con_前后缀.ui_Combobox2_接收_后缀._ComboBox.DataSource = this._lst前后缀;

            语言();
            this.Text = this._readcode._读码器名称;

            this.ui_Button2_关闭.Event_Click += () => Close_();
            this.ui_Button2_保存.Event_Click += () => Save();
            this.KeyDown += (s, e) => KeyDown_(e);
            this.FormClosing += (s, e) => FormClosing_();
            this.con_功能.uiButton_串口.Click += (s, e) => 串口();
            this.con_功能.uiButton_网口.Click += async (s, e) => await 网口();


            this.con_功能.uiButton_串口.Visible = false;
            this.con_功能.uiButton_网口.Visible = false;






            #region 选择功能

            this.uiRadioButton_功能.ValueChanged += (s, e) =>
            {
                this.uiPanel_控件区.Controls.Clear();
                this.uiPanel_控件区.Controls.Add(this.con_功能);
            };

            this.uiRadioButton_读码器.ValueChanged += (s, e) =>
            {
                this.uiPanel_控件区.Controls.Clear();
                this.uiPanel_控件区.Controls.Add(this.con_读码器);
            };

            this.uiRadioButton_检测.ValueChanged += (s, e) =>
            {
                this.uiPanel_控件区.Controls.Clear();
                this.uiPanel_控件区.Controls.Add(this.con_检测);
            };

            this.uiRadioButton_评级.ValueChanged += (s, e) =>
            {
                this.uiPanel_控件区.Controls.Clear();
                this.uiPanel_控件区.Controls.Add(this.con_评级);
            };

            this.uiRadioButton_前后缀.ValueChanged += (s, e) =>
            {
                this.uiPanel_控件区.Controls.Clear();
                this.uiPanel_控件区.Controls.Add(this.con_前后缀);
            };


            #endregion

            #region 功能限制

            this.uiRadioButton_检测.Visible = this._readcode._功能.检测;
            this.uiRadioButton_评级.Visible = this._readcode._功能.评级;

            if (this._readcode._功能.通讯方式选择)
            {
                this.con_功能.uiButton_串口.Visible = true;
                this.con_功能.uiButton_网口.Visible = true;
            }
            else if (this._readcode._参数.通讯方式 == qfWork.读码器._通讯方式_.TcpClient)
            {
                this.con_功能.uiButton_网口.Visible = true;
            }
            else if (this._readcode._参数.通讯方式 == qfWork.读码器._通讯方式_.SerialPort)
            {
                this.con_功能.uiButton_串口.Visible = true;
            }

            this.con_功能.uiCheckBox_功能_检测.Visible = this._readcode._功能.检测;
            this.con_功能.uiCheckBox_功能_评级.Visible = this._readcode._功能.评级;

            #endregion


            Show();
            this.uiRadioButton_功能.Checked = true;

        }




        #region 控件

        Control_功能 con_功能 = new Control_功能();
        Control_读码器 con_读码器 = new Control_读码器();
        Control_检测 con_检测 = new Control_检测();
        Control_评级 con_评级 = new Control_评级();
        Control_前后缀 con_前后缀 = new Control_前后缀();


        #endregion

        #region 方法...事件响应

        private void FormClosing_()
        {
            this._readcode = null;
        }


        async Task 网口()
        {
            using (Form_TCP设置 forms = new Form_TCP设置($"{this._readcode._读码器名称}", this._readcode.TcpClient_sys, null))
            {
                DialogResult result = forms.ShowDialog();
                await this._readcode.TcpClient_sys.Connect连接Async();
            }

        }

        void 串口()
        {
            using (Form_SerialPort_Set forms = new Form_SerialPort_Set(this._readcode.Com_sys, $"{this._readcode._读码器名称}"))
            {
                DialogResult rt = forms.ShowDialog();
                if (rt == DialogResult.OK)
                {
                    this._readcode.Com_sys.Open(out string msgErr);
                }
            }
        }

        void Save(bool Is成功弹窗 = true)
        {
            #region 使能...功能

            this._readcode._参数.使能_读码器 = this.con_功能.uiCheckBox_功能_使能.Checked;
            this._readcode._参数.使能_检测 = this.con_功能.uiCheckBox_功能_检测.Checked;
            this._readcode._参数.使能_评级 = this.con_功能.uiCheckBox_功能_评级.Checked;

            #endregion

            #region 读码器

            this._readcode._参数.读码器.指令_启动 = this.con_读码器.uiTextBox_读码器_启动指令.Text;
            this._readcode._参数.读码器.指令_停止 = this.con_读码器.uiTextBox_读码器_停止指令.Text;
            this._readcode._参数.读码器.错误标识 = this.con_读码器.uiTextBox_读码器_错误标识.Text;
            this._readcode._参数.读码器.通讯超时 = this.con_读码器.uiTextBox_读码器_通讯超时.IntValue;
            this._readcode._参数.读码器.读码超时 = this.con_读码器.uiTextBox_读码器_读码超时.IntValue;
            this._readcode._参数.读码器.读码前延时 = this.con_读码器.uiTextBox_读码器_读码前延时.IntValue;
            this._readcode._参数.读码器.读码次数 = this.con_读码器.uiTextBox_读码器_读码次数.IntValue;
            this._readcode._参数.读码器.多次读码间隔 = this.con_读码器.uiTextBox_读码器_多次读码间隔.IntValue;

            this._readcode._参数.读码器.使能_停止指令 = this.con_读码器.uiCheckBox_读码器_停止指令.Checked;

            #endregion

            #region 检测

            this._readcode._参数.检测.读码超时 = this.con_检测.uiTextBox_检测_读码超时.IntValue;
            this._readcode._参数.检测.读码前延时 = this.con_检测.uiTextBox_检测_读码前延时.IntValue;
            this._readcode._参数.检测.读码次数 = this.con_检测.uiTextBox_检测_读码次数.IntValue;
            this._readcode._参数.检测.多次读码间隔 = this.con_检测.uiTextBox_检测_多次读码间隔.IntValue;

            #endregion

            #region 评级

            this._readcode._参数.评级.分割符 = this.con_评级.uiTextBox_评级_分割符.Text;
            this._readcode._参数.评级.合格等级 = strToStringBeff(this.con_评级.uiTextBox_评级_合格等级.Text.Trim());


            #endregion

            #region 前后缀...发送

            int index0 = this.con_前后缀.ui_Combobox2_发送_前缀._ComboBox.SelectedIndex;
            string 发送前缀 = index0 >= 0 ? this._lst前后缀[index0] : "";
            this._readcode._参数.读码器.前后缀_发送.前缀 = strToStringBeff(发送前缀);

            index0 = this.con_前后缀.ui_Combobox2_发送_后缀._ComboBox.SelectedIndex;
            string 发送后缀 = index0 >= 0 ? this._lst前后缀[index0] : "";
            this._readcode._参数.读码器.前后缀_发送.后缀 = strToStringBeff(发送后缀);

            #endregion

            #region 前后缀...接收

            index0 = this.con_前后缀.ui_Combobox2_接收_前缀._ComboBox.SelectedIndex;
            string 接收前缀 = index0 >= 0 ? this._lst前后缀[index0] : "";
            this._readcode._参数.读码器.前后缀_接收.前缀 = strToStringBeff(接收前缀);

            index0 = this.con_前后缀.ui_Combobox2_接收_后缀._ComboBox.SelectedIndex;
            string 接收后缀 = index0 >= 0 ? this._lst前后缀[index0] : "";
            this._readcode._参数.读码器.前后缀_接收.后缀 = strToStringBeff(接收后缀);

            #endregion

            this._readcode.读写参数(0);


            if (Is成功弹窗)
            {
                MessageBox.Show($"OK");
            }
            //this.DialogResult = DialogResult.OK;
        }

        void Close_()
        {
            if (MessageBox.Show(Language_.Get语言("是否关闭?"), "", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }
            else if (MessageBox.Show(Language_.Get语言("关闭前是否保存?"), "", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                Save(false);
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                this.Close();
            }
        }

        void KeyDown_(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12)
            {
                if (new qfNet.软件类().Win_密码输入框(this._readcode._自定义等级密码) == DialogResult.Yes)
                {
                    using (Form_读码器_自定义等级 forms = new Form_读码器_自定义等级(this._readcode))
                    {
                        forms.ShowDialog();
                    }
                }
            }
        }

        #endregion

        #region 方法

        string[] strToStringBeff(string str)
        {
            List<string> lstStr = new List<string>();
            string[] Bf_0 = str.Split(new string[] { "," }, StringSplitOptions.None);
            return Bf_0;
        }

        string stringBeffToString(string[] beff)
        {
            string xt = string.Join(",", beff.Select(s => s?.Trim())
        .Where(s => !string.IsNullOrEmpty(s)));
            return xt.Trim();
        }



        void Show()
        {

            #region 使能...功能

            this.con_功能.uiCheckBox_功能_使能.Checked = this._readcode._参数.使能_读码器;
            this.con_功能.uiCheckBox_功能_检测.Checked = this._readcode._参数.使能_检测;
            this.con_功能.uiCheckBox_功能_评级.Checked = this._readcode._参数.使能_评级;

            #endregion

            #region 读码器

            this.con_读码器.uiTextBox_读码器_启动指令.Text = this._readcode._参数.读码器.指令_启动;
            this.con_读码器.uiTextBox_读码器_停止指令.Text = this._readcode._参数.读码器.指令_停止;
            this.con_读码器.uiTextBox_读码器_错误标识.Text = this._readcode._参数.读码器.错误标识;
            this.con_读码器.uiTextBox_读码器_通讯超时.IntValue = this._readcode._参数.读码器.通讯超时;
            this.con_读码器.uiTextBox_读码器_读码超时.IntValue = this._readcode._参数.读码器.读码超时;
            this.con_读码器.uiTextBox_读码器_读码前延时.IntValue = this._readcode._参数.读码器.读码前延时;
            this.con_读码器.uiTextBox_读码器_读码次数.IntValue = this._readcode._参数.读码器.读码次数;
            this.con_读码器.uiTextBox_读码器_多次读码间隔.IntValue = this._readcode._参数.读码器.多次读码间隔;

            this.con_读码器.uiCheckBox_读码器_停止指令.Checked = this._readcode._参数.读码器.使能_停止指令;

            #endregion

            #region 检测

            this.con_检测.uiTextBox_检测_读码超时.IntValue = this._readcode._参数.检测.读码超时;
            this.con_检测.uiTextBox_检测_读码前延时.IntValue = this._readcode._参数.检测.读码前延时;
            this.con_检测.uiTextBox_检测_读码次数.IntValue = this._readcode._参数.检测.读码次数;
            this.con_检测.uiTextBox_检测_多次读码间隔.IntValue = this._readcode._参数.检测.多次读码间隔;

            #endregion

            #region 评级

            this.con_评级.uiTextBox_评级_分割符.Text = this._readcode._参数.评级.分割符;
            this.con_评级.uiTextBox_评级_合格等级.Text = stringBeffToString(this._readcode._参数.评级.合格等级);


            #endregion

            #region 前后缀...发送

            int index_发送前缀 = this._lst前后缀.IndexOf(stringBeffToString(this._readcode._参数.读码器.前后缀_发送.前缀));
            this.con_前后缀.ui_Combobox2_发送_前缀._ComboBox.SelectedIndex = index_发送前缀;
            int index_发送后缀 = this._lst前后缀.IndexOf(stringBeffToString(this._readcode._参数.读码器.前后缀_发送.后缀));
            this.con_前后缀.ui_Combobox2_发送_后缀._ComboBox.SelectedIndex = index_发送后缀;

            #endregion

            #region 前后缀...接收

            int index_接收前缀 = this._lst前后缀.IndexOf(stringBeffToString(this._readcode._参数.读码器.前后缀_接收.前缀));
            this.con_前后缀.ui_Combobox2_接收_前缀._ComboBox.SelectedIndex = index_接收前缀;
            int index_接收后缀 = this._lst前后缀.IndexOf(stringBeffToString(this._readcode._参数.读码器.前后缀_接收.后缀));
            this.con_前后缀.ui_Combobox2_接收_后缀._ComboBox.SelectedIndex = index_接收后缀;

            #endregion


            #region 通讯方式

            if (this._readcode._参数.通讯方式 == qfNet.读码器._通讯方式_.TcpClient)
            {
                this.con_功能.uiButton_网口.Visible = true;
                this.con_功能.uiButton_串口.Visible = false;
            }
            else if (this._readcode._参数.通讯方式 == qfNet.读码器._通讯方式_.SerialPort)
            {
                this.con_功能.uiButton_网口.Visible = false;
                this.con_功能.uiButton_串口.Visible = true;
            }

            #endregion

        }


        #endregion

        #region 语言

        void 语言()
        {



        }

        #endregion

    }
}
