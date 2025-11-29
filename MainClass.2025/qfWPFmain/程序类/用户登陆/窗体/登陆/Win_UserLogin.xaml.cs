using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace qfWPFmain
{
    /// <summary>
    /// Win_UserLogin.xaml 的交互逻辑
    /// </summary>
    public partial class Win_UserLogin : Window
    {
        // 重写 ShowDialog() 方法，直接返回 MessageBoxResult
        public new MessageBoxResult ShowDialog()
        {
            base.ShowDialog();
            // 根据实际按钮点击返回对应结果
            return GetResult(); // 需要自己实现 GetResult() 逻辑
        }
        private MessageBoxResult GetResult()
        {
            // 根据内部状态返回对应的 MessageBoxResult
            // 例如根据用户点击的按钮判断
            return _selectedResult; // _selectedResult 是内部存储的结果变量
        }
        private MessageBoxResult _selectedResult = MessageBoxResult.None;


        #region 本地方法

        bool Err_未输入用户名称(string UserName, out string msgErr)
        {
            msgErr = string.Empty;
            if (string.IsNullOrEmpty(UserName))
            {
                this._TextBox_userName.Focus();
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
                this._PasswordBox.Focus();
                msgErr = Language_.Get语言("密码不能为空");
                return false;
            }
            return true;
        }

        void Messagebox_密码错误()
        {
            MessageBox.Show(this, Language_.Get语言("密码错误"), "", MessageBoxButton.OK, MessageBoxImage.Error);
        }



        #endregion




        LoginModelType loginModelType_my = LoginModelType.本地数据;
        LoginShowType LoginShowType_my = LoginShowType.用户登陆;

        public Login_viewModel DataContext_ViewMode = new Login_viewModel();
        public Win_UserLogin(string Caption, LoginShowType LoginShowType_, LoginModelType LoginModelType_)
        {
            InitializeComponent();
            this.DataContext = DataContext_ViewMode;
            this.DataContext_ViewMode.Language_语言.标题 = Caption;
            this._进度条.Visibility = Visibility.Collapsed;


            bool is显示Icon = LoginShowType_ == LoginShowType.用户登陆 ? true : false;
            //if (is显示Icon)
            //{
            //    this.ResizeMode = ResizeMode.CanResize;
            //}
            this._标题栏.Inistiall(this, true, true, is显示Icon, "", false);

            loginModelType_my = LoginModelType_;
            switch (loginModelType_my)
            {
                case LoginModelType.本地数据:
                    this._TextBox_userName.Visibility = Visibility.Collapsed;
                    this.DataContext_ViewMode.Get_获取本地用户信息();
                    break;
                case LoginModelType.远程数据:
                    this._Combobox_UserName.Visibility = Visibility.Collapsed;
                    this._TextBox_userName.Focus();
                    break;
            }

            new Thread(() => { this.DataContext_ViewMode.系统时间(); }) { IsBackground = true }.Start();
        }


        private void _Combobox_UserName_DropDownClosed(object sender, EventArgs e)
        {
            this._PasswordBox.Focus();
        }


        private void _Close_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        //登陆
        private void _Login_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            string UserName = this._Combobox_UserName.Text;
            string password = this._PasswordBox.GetPassword();

            switch (loginModelType_my)
            {
                case LoginModelType.本地数据:

                    #region 登陆

                    if (password != Login登陆._开发者 &&
                        (!Err_未输入用户名称(UserName, out string msgErr) || !Err_密码不能为空(password, out msgErr)))
                    {
                        MessageBox.Show(this, msgErr, "", MessageBoxButton.OK, MessageBoxImage.Error);

                        break;
                    }
                    //开发者
                    else if (password == Login登陆._开发者)
                    {
                        Login登陆.Config.loginInfo = new _loginInfo_(Language_.Get语言($"{LoginUserType.开发者}"), "", LoginUserType.开发者);
                        this._selectedResult = MessageBoxResult.Yes;
                        Login登陆.On_Event_登陆成功();
                        this.Close();
                        return;
                    }

                    int a = this._Combobox_UserName.SelectedIndex;
                    _loginInfo_ info = this.DataContext_ViewMode.Items_LoginInfo[a];
                    if (info.Passwrord == password)
                    {
                        //登陆成功
                        Login登陆.Config.loginInfo = new _loginInfo_(UserName, password, info.UserType);
                        this._selectedResult = MessageBoxResult.Yes;
                        Login登陆.On_Event_登陆成功();
                        this.Close();
                    }
                    else
                    {
                        Messagebox_密码错误();
                        this._PasswordBox.Focus();
                        break;
                    }
                    #endregion

                    break;


                case LoginModelType.远程数据:

                    #region 远程登陆

                    UserName = this._TextBox_userName.ui_Text;
                    if (password != Login登陆._开发者 &&
                        (!Err_未输入用户名称(UserName, out msgErr) || !Err_密码不能为空(password, out msgErr)))
                    {
                        MessageBox.Show(this, msgErr, "", MessageBoxButton.OK, MessageBoxImage.Error);

                        break;
                    }
                    else if (password == Login登陆._开发者)
                    {
                        Login登陆.Config.loginInfo = new _loginInfo_(Language_.Get语言($"{LoginUserType.开发者}"), "", LoginUserType.开发者);
                        this._selectedResult = MessageBoxResult.Yes;
                        Login登陆.On_Event_登陆成功();
                        this.Close();
                        return;
                    }



                    this._状态栏.Visibility = Visibility.Collapsed;//隐藏状态栏
                    this._进度条.Visibility = Visibility.Visible;//进度条显示
                    this.DataContext_ViewMode.Value进度条 = 0;//进度条值
                    this.DataContext_ViewMode.IsRun_进度条 = true;//进度条开启计算

                    _login_远程登陆反馈_ info1 = Login登陆.On_Event_用户登陆(UserName, password);//登陆

                    this.DataContext_ViewMode.IsRun_进度条 = false;//进度条线程关闭
                    this._进度条.Visibility = Visibility.Collapsed;//进度条隐藏
                    this._状态栏.Visibility = Visibility.Visible;//显示状态栏
                    if (info1.state)
                    {
                        //登陆成功
                        Login登陆.Config.loginInfo = info1.loginfo;
                        this._selectedResult = MessageBoxResult.Yes;
                        Login登陆.On_Event_登陆成功();
                        this.Close();
                    }
                    else
                    {
                        this._PasswordBox.Focus();
                    }

                    #endregion

                    break;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.DataContext_ViewMode.IsRun = false;//线程
            this.DataContext_ViewMode.IsRun_进度条 = false;//进度条线程

        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                _Login_PreviewMouseLeftButtonUp(null, null);
            }
        }
    }
}
