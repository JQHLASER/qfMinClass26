using SqlSugar;
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
    internal partial class Form_Zaxis_IO查看 : Sunny.UI.UIForm
    {
       // protected override CreateParams CreateParams { get { CreateParams cp = base.CreateParams; cp.ExStyle |= 0x02000000; return cp; } }//双缓冲显示窗体所有子控件
        qfNet.Zaxis _Zaxis;

        internal Form_Zaxis_IO查看(qfNet.Zaxis zaxis_)
        {
            InitializeComponent();
            this._Zaxis = zaxis_;

            #region IN

            checkBox_IN(this.IN_0, "0");
            checkBox_IN(this.IN_1, "1");
            checkBox_IN(this.IN_2, "2");
            checkBox_IN(this.IN_3, "3");
            checkBox_IN(this.IN_4, "4");
            checkBox_IN(this.IN_5, "5");
            checkBox_IN(this.IN_6, "6");
            checkBox_IN(this.IN_7, "7");
            checkBox_IN(this.IN_8, "8");
            checkBox_IN(this.IN_9, "9");
            checkBox_IN(this.IN_10, "10");
            checkBox_IN(this.IN_11, "11");
            checkBox_IN(this.IN_12, "12");
            checkBox_IN(this.IN_13, "13");
            checkBox_IN(this.IN_14, "14");
            checkBox_IN(this.IN_15, "15");

            #endregion

            #region OUT

            checkBox_OUT(this.OUT_0, "0");
            checkBox_OUT(this.OUT_1, "1");
            checkBox_OUT(this.OUT_2, "2");
            checkBox_OUT(this.OUT_3, "3");
            checkBox_OUT(this.OUT_4, "4");
            checkBox_OUT(this.OUT_5, "5");
            checkBox_OUT(this.OUT_6, "6");
            checkBox_OUT(this.OUT_7, "7");
            checkBox_OUT(this.OUT_8, "8");
            checkBox_OUT(this.OUT_9, "9");
            checkBox_OUT(this.OUT_10, "10");
            checkBox_OUT(this.OUT_11, "11");
            checkBox_OUT(this.OUT_12, "12");
            checkBox_OUT(this.OUT_13, "13");
            checkBox_OUT(this.OUT_14, "14");
            checkBox_OUT(this.OUT_15, "15");


            #endregion

            #region Event...OUT

            this.OUT_0.uiCheckBox.Click += (s, e) => Out(0);
            this.OUT_1.uiCheckBox.Click += (s, e) => Out(1);
            this.OUT_2.uiCheckBox.Click += (s, e) => Out(2);
            this.OUT_3.uiCheckBox.Click += (s, e) => Out(3);
            this.OUT_4.uiCheckBox.Click += (s, e) => Out(4);
            this.OUT_5.uiCheckBox.Click += (s, e) => Out(5);
            this.OUT_6.uiCheckBox.Click += (s, e) => Out(6);
            this.OUT_7.uiCheckBox.Click += (s, e) => Out(7);
            this.OUT_8.uiCheckBox.Click += (s, e) => Out(8);
            this.OUT_9.uiCheckBox.Click += (s, e) => Out(9);
            this.OUT_10.uiCheckBox.Click += (s, e) => Out(10);
            this.OUT_11.uiCheckBox.Click += (s, e) => Out(11);
            this.OUT_12.uiCheckBox.Click += (s, e) => Out(12);
            this.OUT_13.uiCheckBox.Click += (s, e) => Out(13);
            this.OUT_14.uiCheckBox.Click += (s, e) => Out(14);
            this.OUT_15.uiCheckBox.Click += (s, e) => Out(15);

            #endregion

            this.FormClosing += (s, e) => FormClosing_();
            this._Zaxis.Event_IO输入B += On_输入;
            this._Zaxis.Event_IO输出B += On_输出;


        }

        #region 方法.... 

        /// <summary>
        /// 输出
        /// </summary>
        void Out(short port)
        {
            if (port >= StateOut.Length)
            {
                return;
            }
            uint a = StateOut[port] ? (uint)0 : 1;
            this._Zaxis.IO_设置输出口状态(port, a);

        }


        #endregion

        #region 事件

        void On_输入(bool[] state)
        {
            this.BeginInvoke((Action)(() =>
            {
                for (int i = 0; i < state.Length; i++)
                {
                    bool b = state[i];
                    switch (i)
                    {
                        case 0:
                            this.IN_0.uiCheckBox.Checked = b;
                            break;
                        case 1:
                            this.IN_1.uiCheckBox.Checked = b;
                            break;
                        case 2:
                            this.IN_2.uiCheckBox.Checked = b;
                            break;
                        case 3:
                            this.IN_3.uiCheckBox.Checked = b;
                            break;
                        case 4:
                            this.IN_4.uiCheckBox.Checked = b;
                            break;
                        case 5:
                            this.IN_5.uiCheckBox.Checked = b;
                            break;
                        case 6:
                            this.IN_6.uiCheckBox.Checked = b;
                            break;
                        case 7:
                            this.IN_7.uiCheckBox.Checked = b;
                            break;
                        case 8:
                            this.IN_8.uiCheckBox.Checked = b;
                            break;
                        case 9:
                            this.IN_9.uiCheckBox.Checked = b;
                            break;
                        case 10:
                            this.IN_10.uiCheckBox.Checked = b;
                            break;
                        case 11:
                            this.IN_11.uiCheckBox.Checked = b;
                            break;
                        case 12:
                            this.IN_12.uiCheckBox.Checked = b;
                            break;
                        case 13:
                            this.IN_13.uiCheckBox.Checked = b;
                            break;
                        case 14:
                            this.IN_14.uiCheckBox.Checked = b;
                            break;
                        case 15:
                            this.IN_15.uiCheckBox.Checked = b;
                            break;


                    }


                }
            }));
        }

        bool[] StateOut = new bool[0];
        void On_输出(bool[] state)
        {
            StateOut = state;
            this.BeginInvoke((Action)(() =>
            {
                for (int i = 0; i < state.Length; i++)
                {
                    bool b = state[i];
                    switch (i)
                    {
                        case 0:
                            this.OUT_0.uiCheckBox.Checked = b;
                            break;
                        case 1:
                            this.OUT_1.uiCheckBox.Checked = b;
                            break;
                        case 2:
                            this.OUT_2.uiCheckBox.Checked = b;
                            break;
                        case 3:
                            this.OUT_3.uiCheckBox.Checked = b;
                            break;
                        case 4:
                            this.OUT_4.uiCheckBox.Checked = b;
                            break;
                        case 5:
                            this.OUT_5.uiCheckBox.Checked = b;
                            break;
                        case 6:
                            this.OUT_6.uiCheckBox.Checked = b;
                            break;
                        case 7:
                            this.OUT_7.uiCheckBox.Checked = b;
                            break;
                        case 8:
                            this.OUT_8.uiCheckBox.Checked = b;
                            break;
                        case 9:
                            this.OUT_9.uiCheckBox.Checked = b;
                            break;
                        case 10:
                            this.OUT_10.uiCheckBox.Checked = b;
                            break;
                        case 11:
                            this.OUT_11.uiCheckBox.Checked = b;
                            break;
                        case 12:
                            this.OUT_12.uiCheckBox.Checked = b;
                            break;
                        case 13:
                            this.OUT_13.uiCheckBox.Checked = b;
                            break;
                        case 14:
                            this.OUT_14.uiCheckBox.Checked = b;
                            break;
                        case 15:
                            this.OUT_15.uiCheckBox.Checked = b;
                            break;


                    }


                }
            }));
        }


        #endregion

        private void checkBox_IN(Sunny.ui_CheckBox_竖式 checkbox, string Text_, Sunny.UI.UIStyle style_ = Sunny.UI.UIStyle.Gray)
        {
            checkbox.uiCheckBox.ReadOnly = true;
            checkbox.uiLabel.Text = Text_;
            checkbox.uiCheckBox.Style = style_;
        }
        private void checkBox_OUT(Sunny.ui_CheckBox_竖式 checkbox, string Text_, Sunny.UI.UIStyle style_ = Sunny.UI.UIStyle.深色)
        {
            checkbox.uiCheckBox.ReadOnly = true;
            checkbox.uiLabel.Text = Text_;
            checkbox.uiCheckBox.Style = style_;
        }

        private void FormClosing_()
        {
            this._Zaxis.Event_IO输入B -= On_输入;
            this._Zaxis.Event_IO输出B -= On_输出;
            this._Zaxis = null;
        }

        private void Form_Zaxis_IO查看_Load(object sender, EventArgs e)
        {

        }
    }
}
