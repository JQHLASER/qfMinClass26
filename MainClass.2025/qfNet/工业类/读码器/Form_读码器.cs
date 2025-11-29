using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace qfNet
{
    internal partial class Form_读码器 : Sunny.UI.UIForm
    {
        qfNet.读码器 _readcode;
        internal Form_读码器(qfNet.读码器 readcode_)
        {
            InitializeComponent();
            this._readcode = readcode_;

            语言();
            this.Text = this._readcode._读码器名称;

            this.toolStripButton_关闭.Click += (s, e) => Close_();
            this.toolStripButton_保存.Click += (s, e) => Save();
            this.KeyDown += (s, e) => KeyDown_(e);
            this.FormClosing += (s, e) => FormClosing_();
            this.uiButton_网口.Click += (s, e) => 网口();
            this.uiButton_串口.Click += (s, e) => 串口();

            this.uiButton_串口.Visible = false;
            this.uiButton_网口.Visible = false;
            Show();

        }

        private void Form_读码器_Load(object sender, EventArgs e)
        {

        }

        #region 方法...事件响应

        private void FormClosing_()
        {
            this._readcode = null;
        }


        void 网口()
        {
            using (Form_TCP设置 forms = new Form_TCP设置($"{this._readcode._读码器名称}", this._readcode.TcpClient_sys, null))
            {
                DialogResult result = forms.ShowDialog();
                this._readcode.TcpClient_sys.Connect连接Async();
            }

        }

        void 串口()
        {
            using (Form_SerialPort_Set forms = new Form_SerialPort_Set(this._readcode.Com_sys))
            {
                DialogResult rt = forms.ShowDialog();
                if (rt == DialogResult.OK)
                {
                    this._readcode.Com_sys.Open(out string msgErr);
                }
            }
        }

        void Save()
        {
            #region 使能...功能

            this._readcode._参数.使能_读码器 = this.uiCheckBox_功能_使能.Checked;
            this._readcode._参数.使能_检测 = this.uiCheckBox_功能_检测.Checked;
            this._readcode._参数.使能_评级 = this.uiCheckBox_功能_评级.Checked;

            #endregion

            #region 读码器

            this._readcode._参数.读码器.指令_启动 = this.uiTextBox_读码器_启动指令.Text;
            this._readcode._参数.读码器.指令_停止 = this.uiTextBox_读码器_停止指令.Text;
            this._readcode._参数.读码器.错误标识 = this.uiTextBox_读码器_错误标识.Text;
            this._readcode._参数.读码器.通讯超时 = this.uiTextBox_读码器_通讯超时.IntValue;
            this._readcode._参数.读码器.读码超时 = this.uiTextBox_读码器_读码超时.IntValue;
            this._readcode._参数.读码器.读码前延时 = this.uiTextBox_读码器_读码前延时.IntValue;
            this._readcode._参数.读码器.读码次数 = this.uiTextBox_读码器_读码次数.IntValue;
            this._readcode._参数.读码器.多次读码间隔 = this.uiTextBox_读码器_多次读码间隔.IntValue;

            this._readcode._参数.读码器.使能_停止指令 = this.uiCheckBox_读码器_停止指令.Checked;

            #endregion

            #region 检测

            this._readcode._参数.检测.读码超时 = this.uiTextBox_检测_读码超时.IntValue;
            this._readcode._参数.检测.读码前延时 = this.uiTextBox_检测_读码前延时.IntValue;
            this._readcode._参数.检测.读码次数 = this.uiTextBox_检测_读码次数.IntValue;
            this._readcode._参数.检测.多次读码间隔 = this.uiTextBox_检测_多次读码间隔.IntValue;

            #endregion


            #region 评级

            this._readcode._参数.评级.分割符 = this.uiTextBox_评级_分割符.Text;
            this._readcode._参数.评级.合格等级 = strToStringBeff(this.uiTextBox_评级_合格等级.Text.Trim());


            #endregion

            #region 前后缀...发送

            this._readcode._参数.读码器.前后缀_发送.前缀 = strToStringBeff(this.uiTextBox_前缀_发送.Text.Trim());
            this._readcode._参数.读码器.前后缀_发送.后缀 = strToStringBeff(this.uiTextBox_后缀_发送.Text.Trim());

            #endregion

            #region 前后缀...接收

            this._readcode._参数.读码器.前后缀_接收.前缀 = strToStringBeff(this.uiTextBox_前缀_接收.Text.Trim());
            this._readcode._参数.读码器.前后缀_接收.后缀 = strToStringBeff(this.uiTextBox_后缀_接收.Text.Trim());



            #endregion

            MessageBox.Show($"OK\r\n{Language_.Get语言("请重启软件")}");
            this.DialogResult = DialogResult.OK;
        }

        void Close_()
        {
            if (MessageBox.Show(Language_.Get语言("是否关闭?"), "", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }
            this.Close();
        }

        void KeyDown_(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12)
            {
                using (Form_读码器_自定义等级 forms = new Form_读码器_自定义等级(this._readcode))
                {
                    forms.ShowDialog();
                }
            }
        }

        #endregion


        #region 方法

        string[] strToStringBeff(string str)
        {
            List<string> lstStr = new List<string>();
            string[] Bf_0 = str.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            foreach (var s in Bf_0)
            {
                if (!string.IsNullOrEmpty(s.Trim()))
                {
                    lstStr.Add(s);
                }
            }
            return lstStr.ToArray();
        }

        string stringBeffToString(string[] beff)
        {

            string xt = string.Empty;
            for (int i = 0; i < beff.Length; i++)
            {
                string str = beff[i].Trim();
                if (string.IsNullOrEmpty(str))
                {
                    continue;
                }

                if (i == 0)
                {
                    xt = str;
                }
                else
                {
                    xt += $"\r\n{str}";
                }
            }

            return xt.Trim();
        }



        void Show()
        {
            #region 使能...功能

            this.uiCheckBox_功能_使能.Checked = this._readcode._参数.使能_读码器;
            this.uiCheckBox_功能_检测.Checked = this._readcode._参数.使能_检测;
            this.uiCheckBox_功能_评级.Checked = this._readcode._参数.使能_评级;

            #endregion

            #region 读码器

            this.uiTextBox_读码器_启动指令.Text = this._readcode._参数.读码器.指令_启动;
            this.uiTextBox_读码器_停止指令.Text = this._readcode._参数.读码器.指令_停止;
            this.uiTextBox_读码器_错误标识.Text = this._readcode._参数.读码器.错误标识;
            this.uiTextBox_读码器_通讯超时.IntValue = this._readcode._参数.读码器.通讯超时;
            this.uiTextBox_读码器_读码超时.IntValue = this._readcode._参数.读码器.读码超时;
            this.uiTextBox_读码器_读码前延时.IntValue = this._readcode._参数.读码器.读码前延时;
            this.uiTextBox_读码器_读码次数.IntValue = this._readcode._参数.读码器.读码次数;
            this.uiTextBox_读码器_多次读码间隔.IntValue = this._readcode._参数.读码器.多次读码间隔;

            this.uiCheckBox_读码器_停止指令.Checked = this._readcode._参数.读码器.使能_停止指令;

            #endregion

            #region 检测

            this.uiTextBox_检测_读码超时.IntValue = this._readcode._参数.检测.读码超时;
            this.uiTextBox_检测_读码前延时.IntValue = this._readcode._参数.检测.读码前延时;
            this.uiTextBox_检测_读码次数.IntValue = this._readcode._参数.检测.读码次数;
            this.uiTextBox_检测_多次读码间隔.IntValue = this._readcode._参数.检测.多次读码间隔;

            #endregion

            #region 评级

            this.uiTextBox_评级_分割符.Text = this._readcode._参数.评级.分割符;
            this.uiTextBox_评级_合格等级.Text = stringBeffToString(this._readcode._参数.评级.合格等级);


            #endregion

            #region 前后缀...发送

            this.uiTextBox_前缀_发送.Text = stringBeffToString(this._readcode._参数.读码器.前后缀_发送.前缀);
            this.uiTextBox_后缀_发送.Text = stringBeffToString(this._readcode._参数.读码器.前后缀_发送.后缀);

            #endregion

            #region 前后缀...接收

            this.uiTextBox_前缀_接收.Text = stringBeffToString(this._readcode._参数.读码器.前后缀_接收.前缀);
            this.uiTextBox_后缀_接收.Text = stringBeffToString(this._readcode._参数.读码器.前后缀_接收.后缀);


            #endregion


            #region 通讯方式

            if (this._readcode._参数.通讯方式 == qfNet.读码器._通讯方式_.TcpClient)
            {
                this.uiButton_网口.Visible = true;
                this.uiButton_串口.Visible = false;
            }
            else if (this._readcode._参数.通讯方式 == qfNet.读码器._通讯方式_.SerialPort)
            {
                this.uiButton_网口.Visible = false;
                this.uiButton_串口.Visible = true;
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
