using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace qfNet
{
    internal partial class Form_Login_修改or添加 : Sunny.UI.UIForm
    {
        //    protected override CreateParams CreateParams { get { CreateParams cp = base.CreateParams; cp.ExStyle |= 0x02000000; return cp; } }//双缓冲显示窗体所有子控件
        Form_Login_管理._操作方式_ _操作方式 = Form_Login_管理._操作方式_.添加;
        Form_Login_管理 _form管理;
        public _loginInfo_ _当前选中用户信息;
        _loginInfo_ _当前登陆用户信息;



        List<_LoginUserType_> lst用户权限 = new List<_LoginUserType_>();
        int _修改_选中行索引 = -1;



        internal Form_Login_修改or添加(Form_Login_管理 form管理, Form_Login_管理._操作方式_ 操作方式_)
        {
            InitializeComponent();
            this._操作方式 = 操作方式_;
            this._form管理 = form管理;
            this._当前登陆用户信息 = this._form管理._Login_sys._Config.loginInfo;
            if (操作方式_ == Form_Login_管理._操作方式_.修改)
            {
                this._修改_选中行索引 = this._form管理.获取选中行数();
                this._当前选中用户信息 = this._form管理._lstInfo[this._修改_选中行索引].Clone();
            }


            this.uiLabel_用户.Text = Language_.Get语言("用户");
            this.uiLabel_密码.Text = Language_.Get语言("密码");
            this.uiLabel_权限.Text = Language_.Get语言("权限");

            this.uiTextBox_密码.ImeMode = ImeMode.Disable;


            #region 用户权限

            string[] 权限 = Enum.GetNames(typeof(_LoginUserType_));
            for (int i = 0; i < 权限.Length; i++)
            {
                _LoginUserType_ type_ = (_LoginUserType_)i;
                bool is添加 = false;

                if (this._当前登陆用户信息.UserType == _LoginUserType_.开发者 &&
                   type_ < _LoginUserType_.开发者)
                {
                    is添加 = true;
                }
                else if (this._当前登陆用户信息.UserType == _LoginUserType_.超级管理员)
                {
                    if (type_ < _LoginUserType_.超级管理员 ||
                        (type_ == _LoginUserType_.超级管理员 &&
                        (this._操作方式 == Form_Login_管理._操作方式_.修改 && this._当前选中用户信息.UserName == this._当前登陆用户信息.UserName)))
                    {
                        is添加 = true;
                    }
                }
                else
                {
                    if (type_ < this._当前登陆用户信息.UserType)
                    {
                        is添加 = true;
                    }
                    else if (type_ == this._当前登陆用户信息.UserType &&
                            (this._操作方式 == Form_Login_管理._操作方式_.修改 && this._当前选中用户信息.UserName == this._当前登陆用户信息.UserName))
                    {
                        is添加 = true;

                    }
                }


                if (is添加)
                {
                    lst用户权限.Add((_LoginUserType_)i);
                    this.uiComboBox_权限.Items.Add(Language_.Get语言($"{权限[i]}"));
                }

            }


            #endregion


            switch (操作方式_)
            {
                case Form_Login_管理._操作方式_.添加:
                    this.Text = Language_.Get语言("添加");
                    break;
                case Form_Login_管理._操作方式_.修改:
                    this.Text = Language_.Get语言("修改");

                    #region 修改

                    this.uiTextBox_用户.Text = this._当前选中用户信息.UserName;
                    this.uiTextBox_密码.Text = this._当前选中用户信息.Passwrord;
                    this.uiComboBox_权限.SelectedIndex = this.uiComboBox_权限.Items.IndexOf($"{this._当前选中用户信息.UserType}");

                    if (this._当前登陆用户信息.UserName == this._当前选中用户信息.UserName)
                    {
                        this.uiComboBox_权限.Enabled = false;
                    }
                    this.uiTextBox_用户.Enabled = false;

                    #endregion

                    break;
            }

            this.uiButton_No.Click += (s, e) => No_();
            this.uiButton_Yes.Click += (s, e) => Yes_();

            this.FormClosing += (s, e) => FormClosing_();


        }


        #region 事件

        private void Form_Login_修改or添加_Load(object sender, EventArgs e)
        {




        }

        private void FormClosing_()
        {
            Form_Login_管理 _form管理 = null;
            _loginInfo_ _当前选中用户信息 = null;
            _loginInfo_ _当前登陆用户信息 = null;
        }


        void No_()
        {
            this.Close();
        }

        void Yes_()
        {
            int index = this.uiComboBox_权限.SelectedIndex;
            string userName = this.uiTextBox_用户.Text.Trim();
            string password = this.uiTextBox_密码.Text.Trim();

            if (index == -1)
            {
                MessageBox.Show(Language_.Get语言("未选择用户权限"), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (string.IsNullOrEmpty(userName))
            {
                MessageBox.Show(Language_.Get语言("请输入用户名"), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show(Language_.Get语言("密码不能为空"), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            _LoginUserType_ userType = this.lst用户权限[index];




            switch (this._操作方式)
            {
                case Form_Login_管理._操作方式_.添加:

                    #region 添加

                    foreach (var s in this._form管理._lstInfo)
                    {
                        if (s.UserName == userName)
                        {
                            MessageBox.Show(Language_.Get语言("用户名已存在"), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                    }

                    this._当前选中用户信息   = new _loginInfo_(userName, password, userType);
                  

                    #endregion

                    break;
                case Form_Login_管理._操作方式_.修改:

                    #region 修改

                    this._当前选中用户信息 = new _loginInfo_(userName, password, userType); 

                    #endregion

                    break;

            }

            this.DialogResult = DialogResult.OK;

        }


        #endregion





    }
}
