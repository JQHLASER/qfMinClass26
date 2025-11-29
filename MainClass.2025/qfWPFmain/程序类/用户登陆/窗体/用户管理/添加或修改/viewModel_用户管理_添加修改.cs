using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using qfmain;
using System.Text;
using System.Windows;

namespace qfWPFmain
{
    internal partial class viewModel_用户管理_添加修改 : ObservableObject
    {
        viewModel_用户管理 _viewmodel;
        viewModel_用户管理._操作_ _操作 = viewModel_用户管理._操作_.添加;



        public viewModel_用户管理_添加修改(viewModel_用户管理._操作_ 操作, viewModel_用户管理 viewmodel)
        {
            this._操作 = 操作;
            this._viewmodel = viewmodel;

            string[] work = new string[]
            {
              "获取",
              "加载用户权限",
              "选择用户权限"
            };

            foreach (string s in work)
            {
                switch (s)
                {
                    case "获取":

                        #region 操作

                        switch (操作)
                        {
                            case viewModel_用户管理._操作_.修改:
                                this.IsEnable_用户 = false;
                                Get_选中信息();
                                break;
                            case viewModel_用户管理._操作_.添加:


                                break;
                        }

                        #endregion

                        break;
                    case "加载用户权限":

                        #region 用户权限

                        List<string> lst = new List<string>();
                        string[] userType = Enum.GetNames(typeof(LoginUserType));
                        for (int i = 0; i < userType.Length; i++)
                        {
                            if ((LoginUserType)i > Login登陆.Config.loginInfo.UserType) //当权限大于当前用户登陆的权限时
                            {
                                continue;
                            }

                            switch ((LoginUserType)i)
                            {
                                case LoginUserType.管理员:

                                    #region 管理员

                                    if (Login登陆.Config.loginInfo.UserType == LoginUserType.管理员 && //当前用户为管理员
                                        Login登陆.Config.loginInfo.UserName == this.Info_当前信息.UserName) //当前用户=当前登陆用户
                                    {
                                        lst.Add(Language_.Get语言($"{userType[i]}"));
                                        this.IsEnable_权限 = false;
                                        continue;

                                    }


                                    #endregion

                                    break;
                                case LoginUserType.超级管理员:

                                    #region 超级管理员

                                    if (Login登陆.Config.loginInfo.UserType == LoginUserType.超级管理员 && //当前用户为管理员
                                         Login登陆.Config.loginInfo.UserName == this.Info_当前信息.UserName) //当前
                                    {
                                        lst.Add(Language_.Get语言($"{userType[i]}"));
                                        this.IsEnable_权限 = false;
                                        break;
                                    }


                                    #endregion

                                    break;
                                default:
                                    lst.Add(Language_.Get语言($"{userType[i]}"));
                                    break;
                            }

                        }
                        this.info_权限 = lst.ToArray();

                        #endregion

                        break;
                    case "选择用户权限":

                        this.SelectedIndex = (int)this.Info_当前信息.UserType;//选择用户权限

                        break;
                }

            }


        }

        [ObservableProperty]
        private _loginInfo_ info_当前信息 = new _loginInfo_("", "", LoginUserType.操作员);

        [ObservableProperty]
        private bool isEnable_权限 = true;

        [ObservableProperty]
        private bool isEnable_密码 = true;

        [ObservableProperty]
        private bool isEnable_用户 = true;

        [ObservableProperty]
        private string[] info_权限 = new string[0];

        /// <summary>
        /// 用户权限选中
        /// </summary>
        [ObservableProperty]
        private int selectedIndex = -1;


        #region 本地方法

        /// <summary>
        /// 显示选中的用户信息
        /// </summary>
        void Get_选中信息()
        {
            this.Info_当前信息.UserName = this._viewmodel.lst_用户信息[this._viewmodel.Index_ListView选中行].UserName;
            this.Info_当前信息.Passwrord = this._viewmodel.lst_用户信息[this._viewmodel.Index_ListView选中行].Passwrord;
            this.Info_当前信息.UserType = this._viewmodel.lst_用户信息[this._viewmodel.Index_ListView选中行].UserType;
        }

        bool Add()
        {
            if (this.SelectedIndex < 0)
            {
                MessageBox.Show(Language_.Get语言("未选择用户权限"), "", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (string.IsNullOrEmpty(this.info_当前信息.UserName.Trim()))
            {
                MessageBox.Show(Language_.Get语言("请输入用户名"), "", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (string.IsNullOrEmpty(this.info_当前信息.Passwrord.Trim()))
            {
                MessageBox.Show(Language_.Get语言("密码不能为空"), "", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            _loginInfo_[] a = this._viewmodel.lst_用户信息.Where(p => p.UserName == this.info_当前信息.UserName).ToArray();
            if (a.Length > 0)
            {
                MessageBox.Show(Language_.Get语言("用户名已存在"), "", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            this.Info_当前信息.UserType = (LoginUserType)this.SelectedIndex;
            this._viewmodel.同步用户信息_添加(this.Info_当前信息);
            return true;
        }
        bool Set_修改信息()
        {
            string password = this.info_当前信息.Passwrord.Trim();
            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show(Language_.Get语言("密码不能为空"), "", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            //  int lenght = Encoding.Default.GetByteCount(password);
            //if (lenght <4)
            //{
            //    MessageBox.Show(Language_.Get语言("密码最少为4位"), "", MessageBoxButton.OK, MessageBoxImage.Error);
            //    return false;
            //}
            this.Info_当前信息.UserType = (LoginUserType)this.SelectedIndex;
            this._viewmodel.同步用户信息_修改(this.Info_当前信息);
            return true;
        }


        [RelayCommand]
        public void save(object p)
        {
            switch (this._操作)
            {
                case viewModel_用户管理._操作_.添加:
                    if (!Add())
                    {
                        return;
                    }
                    MessageBox.Show("Save OK");
                    break;
                case viewModel_用户管理._操作_.修改:
                    if (!Set_修改信息())
                    {
                        return;
                    }
                    MessageBox.Show("Update OK");
                    break;
            }


            Event_Close?.Invoke();

        }

        [RelayCommand]
        public void Close(object p)
        {
            Event_Close?.Invoke();

        }
        #endregion


        #region 事件

        /// <summary>
        /// 关闭窗体
        /// </summary>
        internal event Action? Event_Close;

        #endregion

    }
}
