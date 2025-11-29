using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using qfmain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace qfWPFmain
{
    internal partial class viewModel_用户管理 : ObservableObject
    {
        internal enum _操作_
        {
            添加,
            修改,
        }

        public class _用户信息_
        {
            public string 用户 { set; get; }
            public string 权限 { set; get; }
        }

        public class _语言_
        {
            public string 添加 { set; get; } = Language_.Get语言("添加");
            public string 修改 { set; get; } = Language_.Get语言("修改");
            public string 删除 { set; get; } = Language_.Get语言("删除");
            public string 保存 { set; get; } = Language_.Get语言("保存");

        }

        public viewModel_用户管理(Window d)
        {
            this.Win当前窗体 = d;
            Login登陆.读写本地用户信息(1);
            同步用户信息();


        }

        void 同步用户信息()
        {

            this.lst_用户信息 = Login登陆.Config.loginInfo_Beff.ToList();
            this.LoginInfo_.Clear();
            foreach (var s in lst_用户信息)
            {
                _用户信息_ info = new _用户信息_();
                info.用户 = s.UserName;
                info.权限 = Language_.Get语言($"{s.UserType}");
                this.LoginInfo_.Add(info);
            }
        }





        internal void 同步用户信息_添加(_loginInfo_ s)
        {
            this.lst_用户信息.Add(s);
            _用户信息_ info = new _用户信息_();
            info.用户 = s.UserName;
            info.权限 = Language_.Get语言($"{s.UserType}");
            this.LoginInfo_.Add(info);
        }

        internal void 同步用户信息_修改(_loginInfo_ s)
        {
            this.lst_用户信息[Index_ListView选中行] = s;
            _用户信息_ info = new _用户信息_();
            info.用户 = s.UserName;
            info.权限 = Language_.Get语言($"{s.UserType}");
            this.LoginInfo_[Index_ListView选中行] = info;
        }


        [ObservableProperty]
        private _语言_ languang = new _语言_();

        /// <summary>
        /// 用户信息集
        /// </summary>
        [ObservableProperty]
        private ObservableCollection<_用户信息_> loginInfo_ = new ObservableCollection<_用户信息_>();

        internal List<_loginInfo_> lst_用户信息 = new List<_loginInfo_>();

        [ObservableProperty]
        private Window win当前窗体 = new Window();

        [ObservableProperty]
        private int index_ListView选中行 = -1;

        [RelayCommand]
        internal void 添加(object p)
        {
            new Win_用户管理_添加修改(_操作_.添加, this.Win当前窗体, this) { Owner = Window.GetWindow(this.win当前窗体) }.ShowDialog();
        }

        [RelayCommand]
        internal void 修改(object p)
        {
            if (!Err_未选中对象() || !Err_权限过低())
            {
                return;
            }
            new Win_用户管理_添加修改(_操作_.修改, this.Win当前窗体, this) { Owner = Window.GetWindow(this.win当前窗体) }.ShowDialog();

        }

        [RelayCommand]
        internal void 删除(object p)
        {
            if (!Err_未选中对象() || !Err_权限过低() || !Err_不能操作当前登陆用户())
            {
                return;
            }
            else if (MessageBox.Show($"{Language_.Get语言("是否确认删除")}?", "", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                return;
            }

            this.lst_用户信息.RemoveAt(this.Index_ListView选中行);
            this.LoginInfo_.RemoveAt(this.Index_ListView选中行);

        }


        [RelayCommand]
        internal void 保存(object p)
        {
            Login登陆.Config.loginInfo_Beff = this.lst_用户信息.ToArray();
            Login登陆.读写本地用户信息(0);
            this.同步用户信息();
            MessageBox.Show(Language_.Get语言("保存成功"));
        }



        #region Err

        bool Err_未选中对象()
        {
            if (this.Index_ListView选中行 == -1)
            {
                MessageBox.Show(Language_.Get语言("未选中对象"), "", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        bool Err_权限过低()
        {
            _loginInfo_ info = this.lst_用户信息[this.Index_ListView选中行];
            LoginUserType userType = Login登陆.Config.loginInfo.UserType;
            if (info.UserType > userType)
            {
                MessageBox.Show(Language_.Get语言("权限过低"), "", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;

            }
            return true;
        }

        bool Err_不能操作当前登陆用户()
        {
            _loginInfo_ info = this.lst_用户信息[this.Index_ListView选中行];
            LoginUserType userType = Login登陆.Config.loginInfo.UserType;
            string userName = Login登陆.Config.loginInfo.UserName;
            if (info.UserType == userType && info.UserName == userName)
            {
                MessageBox.Show(Language_.Get语言("不能操作当前登陆用户"), "", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;

            }
            return true;
        }




        #endregion
    }
}
