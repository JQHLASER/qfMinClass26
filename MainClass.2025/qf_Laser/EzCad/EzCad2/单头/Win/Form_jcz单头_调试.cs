using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace qf_Laser
{
    public partial class Form_jcz单头_调试 : Sunny.UI.UIForm
    {
        //双缓冲显示窗体所有子控件
        protected override CreateParams CreateParams { get { CreateParams cp = base.CreateParams; cp.ExStyle |= 0x02000000; return cp; } }
        MarkEzd_Ezd2 _markEzd = null;
        MultilineMarkEzd _multilineMarkEzd = null;
        int _Cardindex = -1;

        internal Form_jcz单头_调试(MarkEzd_Ezd2 markEzd_, MultilineMarkEzd multilineMarkEzd_ = null, int cardindex = -1)
        {
            InitializeComponent();
            this._markEzd = markEzd_;
            this._multilineMarkEzd = multilineMarkEzd_;
            this._Cardindex = cardindex;


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


            if (this._markEzd != null)
            {
                this._markEzd.Event_IO_IN += On_输入;
                this._markEzd.Event_IO_OUT += On_输出;
            }
            else if (this._multilineMarkEzd != null)
            {
                this._multilineMarkEzd.Event_IO_IN += On_输入;
                this._multilineMarkEzd.Event_IO_OUT += On_输出;
                this.Text = $"Laser {this._Cardindex + 1}";
            }



            this.FormClosing += (s, e) => FormClosing_();
            this.uiButton_设置.Click += (s, e) => On_设置();
            this.uiButton_标刻.Click += (s, e) => On_标刻();
            this.uiButton_红光.Click += (s, e) => On_红光();
            this.uiButton_停止.Click += (s, e) => On_停止();

        }

        private void Form_jcz单头_调试_Load(object sender, EventArgs e)
        {

        }

        private void FormClosing_()
        {
            if (this._markEzd != null)
            {
                this._markEzd.Event_IO_IN -= On_输入;
                this._markEzd.Event_IO_OUT -= On_输出;
            }
            else if (this._multilineMarkEzd != null)
            {
                this._multilineMarkEzd.Event_IO_IN -= On_输入;
                this._multilineMarkEzd.Event_IO_OUT -= On_输出;
            }
            this._markEzd = null;
            this._multilineMarkEzd = null;
        }



        #region 方法.... 

        /// <summary>
        /// 输出
        /// </summary>
        void Out(ushort port)
        {
            if (port >= StateOut.Length)
            {
                return;
            }
            bool a = !StateOut[port];
            if (this._markEzd != null)
            {
                this._markEzd.输出(port, a);
            }
            else if (this._multilineMarkEzd != null)
            {
                this._multilineMarkEzd.输出(this._Cardindex, port, a);
            }
        }


        #endregion

        #region 事件...响应

        void On_输入(bool[] state)
        {
            try
            {

                this.Invoke((Action)(() =>
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
            catch (Exception)
            {
            }
        }

        bool[] StateOut = new bool[0];
        void On_输出(bool[] state)
        {
            try
            {

                StateOut = state;
                this.Invoke((Action)(() =>
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
            catch (Exception)
            {
            }
        }

        void On_输入(int Cardindex, bool[] state)
        {
            if (this._Cardindex != Cardindex)
            {
                return;
            }

            try
            {

                this.Invoke((Action)(() =>
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
            catch (Exception)
            {
            }

        }

        void On_输出(int Cardindex, bool[] state)
        {
            if (this._Cardindex != Cardindex)
            {
                return;
            }
            try
            {

                StateOut = state;
                this.Invoke((Action)(() =>
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
            catch (Exception)
            {

            }

        }


        void On_设置()
        {

            if (this._markEzd != null)
            {
                this._markEzd.win_设置();
            }
            else if (this._multilineMarkEzd != null)
            {
                this._multilineMarkEzd.Win_设置激光参数(this._Cardindex);
            }
        }

        async void On_标刻()
        {
            if (this._markEzd != null)
            {
                var rt = await this._markEzd.调试_标刻();
            }
            else if (this._multilineMarkEzd != null)
            {
                var rt1 = await Mark_MultiLine();
            }
        }



        async Task<bool> Mark_MultiLine()
        {
            bool rt = true;
            Task t0 = Task.Run(() =>
            {
                this._multilineMarkEzd.On_加工状态(this._Cardindex, _激光加工状态_.出激光标刻中);
                rt = this._multilineMarkEzd.标刻(this._Cardindex, false, out string msgErr);
                this._multilineMarkEzd.等待_标刻结束(this._Cardindex, 100);
                this._multilineMarkEzd.On_加工状态(this._Cardindex, _激光加工状态_.闲置);

                if (!rt)
                {
                    this._multilineMarkEzd.On_Log_指定卡(this._Cardindex, rt, msgErr);
                }


            });
            await t0;
            return true;
        }


        async void On_红光()
        {
            if (this._markEzd != null)
            {
                var t0 = await this._markEzd.调试_红光();
            }
            else if (this._multilineMarkEzd != null)
            {
                var t1 = await Red_multiline();
            }
        }

        async Task<bool> Red_multiline()
        {
            bool rt = true;
            Task t0 = Task.Run(() =>
            {
                rt = this._multilineMarkEzd.连续_红光指示(this._Cardindex, out string msgErr, true);
                if (!rt)
                {
                    this._multilineMarkEzd.On_Log_指定卡(this._Cardindex, rt, msgErr);
                }
            });
            await t0;
            return true;
        }



        void On_停止()
        {
            if (this._markEzd != null)
            {
                this._markEzd.停止标刻和红光();
            }
            else if (this._multilineMarkEzd != null)
            {
                this._multilineMarkEzd.停止标刻和红光(this._Cardindex);
            }
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



    }
}
