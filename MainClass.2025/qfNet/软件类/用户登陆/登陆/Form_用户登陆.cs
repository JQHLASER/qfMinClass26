
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace qfNet
{
    public partial class Form_用户登陆 : Sunny.UI.UIForm
    {
       // protected override CreateParams CreateParams { get { CreateParams cp = base.CreateParams; cp.ExStyle |= 0x02000000; return cp; } }//双缓冲显示窗体所有子控件
        readonly viewModel_Login _DataContext = new viewModel_Login();
        _LoginShowType_ _LoginShowType = _LoginShowType_.用户登陆;
        System.Windows.Forms.Timer _timer = new System.Windows.Forms.Timer();
        Login登陆 _Login_sys;

        public Form_用户登陆(Login登陆 login_, _LoginShowType_ LoginShowType_ = _LoginShowType_.用户登陆)
        {
            InitializeComponent();
            this._Login_sys = login_;
            this.DataContext = this._DataContext;
            this.DataBindings.Add("Text", this._DataContext, nameof(this._DataContext.Title), false);
            this.DataBindings.Add("ShowInTaskbar", this._DataContext, nameof(this._DataContext.ShowTaskBar), false);

            this.uiButton_登陆.DataBindings.Add("Text", this._DataContext, nameof(this._DataContext.Button_登陆), false);
            this.uiButton_关闭.DataBindings.Add("Text", this._DataContext, nameof(this._DataContext.Button_关闭), false);

            this.uiLabel_时间.DataBindings.Add("Text", this._DataContext, nameof(this._DataContext.时间), false);
            this.uiLabel_用户.DataBindings.Add("Text", this._DataContext, nameof(this._DataContext.label_用户), false);
            this.uiLabel_密码.DataBindings.Add("Text", this._DataContext, nameof(this._DataContext.label_密码), false);

            this.uiComboBox_用户.DataBindings.Add("DataSource", this._DataContext, nameof(this._DataContext.用户信息), false);
            this.uiComboBox_用户.DisplayMember = "UserName";
            this.uiComboBox_用户.DataBindings.Add("SelectedIndex", this._DataContext, nameof(this._DataContext.SelectIndex), false, DataSourceUpdateMode.OnPropertyChanged);

            this._LoginShowType = LoginShowType_;//登陆/切换

            this.uiTextBox_密码.ImeMode = ImeMode.Disable;


            this._DataContext.ShowTaskBar = (this._LoginShowType == _LoginShowType_.用户登陆) ? true : false;

            this._Login_sys.读写本地用户信息(1);


            if (this._Login_sys._类型 == _Login登陆类型_.选择用户)
            {
                this.uiTextBox_用户.Visible = false;
                this._Login_sys.读写本地用户信息(1);
            }
            else if (this._Login_sys._类型 == _Login登陆类型_.输入用户)
            {
                this.uiComboBox_用户.Visible = false;
                this.uiTextBox_用户.Top = this.uiComboBox_用户.Top;
                this.uiTextBox_用户.Left = this.uiComboBox_用户.Left;
            }


            this._DataContext.用户信息 = this._Login_sys._Config.loginInfo_Beff;

            this.uiButton_关闭.Click += (s, e) => this.Close_();
            this.Shown += (s, e) => this.Show_();
            this.KeyDown += (s, e) => this.KeyDown_(e);
            this.FormClosing += (s, e) => this.FormClosing_();

            this.uiButton_登陆.Click += (s, e) => On_登陆();
            this.uiComboBox_用户.SelectedIndexChanged += (s, e) => SelectedIndexChanged_用户();

            On_Datetime();
            this._timer.Interval = 1000;
            this._timer.Tick += On_Timer;
            this._timer.Start();
        }

        #region 本地方法


        void TimesClose()
        {
            this._timer.Stop();
            this._timer.Tick -= On_Timer;
        }

        void SelectedIndexChanged_用户()
        {
            this.uiTextBox_密码.Focus();
        }

        void On_登陆()
        {
            string UserName = this.uiComboBox_用户.SelectedText;
            string password = this.uiTextBox_密码.Text;
            bool rt = true;

            string[] work = new string[]
            {
               "Err",
               "开发者登陆",
               "正常登陆"
            };

            switch (this._Login_sys._类型)
            {
                case _Login登陆类型_.选择用户:

                    #region 登陆

                    foreach (var s in work)
                    {
                        if (!rt)
                        {
                            break;
                        }
                        else if (s == "Err")
                        {
                            #region Err

                            if (password != this._Login_sys._开发者 &&
                              (!Err_未输入用户名称(UserName, out string msgErr1) || !Err_密码不能为空(password, out msgErr1)))
                            {
                                rt = false;
                                MessageBox.Show(this, msgErr1, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                continue;
                            }

                            #endregion
                        }
                        else if (s == "开发者登陆")
                        {
                            #region 开发者


                            //开发者
                            if (password == this._Login_sys._开发者)
                            {
                                this._Login_sys._Config.loginInfo = new _loginInfo_(Language_.Get语言($"{_LoginUserType_.开发者}"), "", _LoginUserType_.开发者);
                                break;
                            }

                            #endregion
                        }
                        else if (s == "正常登陆")
                        {
                            #region 正常登陆

                            int a = this._DataContext.SelectIndex;
                            try
                            {
                                _loginInfo_ info = this._Login_sys._Config.loginInfo_Beff[a];
                                if (info.Passwrord == password)
                                {
                                    //登陆成功
                                    this._Login_sys._Config.loginInfo = info;
                                }
                                else
                                {
                                    Messagebox_密码错误();
                                    this.uiTextBox_密码.Focus();
                                    rt = false;
                                    break;
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                                rt = false;
                                break;
                            }

                            #endregion
                        }
                    }


                    #endregion

                    break;


                case _Login登陆类型_.输入用户:

                    #region 登陆

                    UserName = this.uiTextBox_用户.Text;

                    foreach (var s in work)
                    {
                        if (!rt)
                        {
                            break;
                        }
                        else if (s == "Err")
                        {
                            #region Err

                            if (password != this._Login_sys._开发者 &&
                                (!Err_未输入用户名称(UserName, out string msgErr) || !Err_密码不能为空(password, out msgErr)))
                            {
                                rt = false;
                                MessageBox.Show(this, msgErr, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                            }

                            #endregion
                        }
                        else if (s == "开发者登陆")
                        {
                            #region 开发者登陆

                            if (password == this._Login_sys._开发者)
                            {
                                this._Login_sys._Config.loginInfo = new _loginInfo_(Language_.Get语言($"{_LoginUserType_.开发者}"), "", _LoginUserType_.开发者);
                                break;
                            }

                            #endregion
                        }
                        else if (s == "正常登陆")
                        {
                            #region 正常登陆

                            _login_远程登陆反馈_ info1 = this._Login_sys.On_Event_用户登陆(UserName, password);//登陆
                            if (!info1.state)
                            {
                                //登陆失败
                                this.uiTextBox_密码.Focus();
                                rt = false;
                            }

                            #endregion
                        }
                    }



                    #endregion

                    break;
            }


            if (rt)
            {
                TimesClose();
                this._Login_sys.On_Event_登陆成功();
                this.DialogResult = DialogResult.OK;
            }
        }

        void FormClosing_()
        {
            TimesClose();
            this._Login_sys = null;

        }
        void Close_()
        {
            this.Close();
        }
        void Show_()
        {
            this.uiTextBox_密码.Focus();
        }

        void KeyDown_(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                On_登陆();
            }
        }

        private void Form_用户登陆_Load(object sender, EventArgs e)
        {

        }

        private void On_Timer(object s, EventArgs e)
        {
            On_Datetime();
        }
        /// <summary>
        /// 显示信息
        /// </summary>
        private void On_Datetime()
        {
            this._DataContext.时间 = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        }
        #endregion


        #region Err


        bool Err_未输入用户名称(string UserName, out string msgErr)
        {
            msgErr = string.Empty;
            if (string.IsNullOrEmpty(UserName))
            {
                msgErr = Language_.Get语言("请输入用户名");
                return false;
            }
            return true;
        }
        bool Err_密码不能为空(string Password, out string msgErr)
        {
            msgErr = string.Empty;
            if (string.IsNullOrEmpty(Password))
            {
                msgErr = Language_.Get语言("密码不能为空");
                return false;
            }
            return true;
        }

        void Messagebox_密码错误()
        {
            MessageBox.Show(this, Language_.Get语言("密码错误"), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }



        #endregion


    }
}
