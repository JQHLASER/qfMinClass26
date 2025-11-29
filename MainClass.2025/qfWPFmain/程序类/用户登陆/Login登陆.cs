using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace qfWPFmain
{

    /// <summary>
    /// 用户信息
    /// </summary>
    public class _loginInfo_
    {
        public _loginInfo_()
        {

        }
        public _loginInfo_(string UserName_, string Password_, LoginUserType UserType_)
        {
            UserName = UserName_;
            Passwrord = Password_;
            UserType = UserType_;
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { set; get; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Passwrord { set; get; }
        /// <summary>
        /// 用户权限
        /// </summary>
        public LoginUserType UserType { set; get; }
    }

    public class _login_远程登陆反馈_
    {
        /// <summary>
        /// 登陆的信息
        /// </summary>
        public _loginInfo_ loginfo { set; get; }

        /// <summary>
        /// 登陆状态,Yes:成功,No:失败
        /// </summary>
        public bool state { set; get; } = false;
    }

    public enum _login_登陆方式_
    {
        本地,
        远端,
    }


    public class Login登陆
    {
        public static string _开发者 = "JQHLASER";
        public class Config
        {
            /// <summary>
            /// 所有用户信息
            /// </summary>
            public static _loginInfo_[] loginInfo_Beff { set; get; } = new _loginInfo_[0];

            /// <summary>
            /// 当前已登陆的用户
            /// </summary>
            public static _loginInfo_ loginInfo { set; get; } = null;

            public static _login_登陆方式_ 登陆方式 { set; get; } = _login_登陆方式_.本地;
        }

        public static _loginInfo_[] 生成原始数据()
        {
            List<_loginInfo_> lst = new List<_loginInfo_>();
            lst.Add(new _loginInfo_("admin", "QF8888", LoginUserType.超级管理员));
            lst.Add(new _loginInfo_(Language_.Get语言("管理员"), "9999", LoginUserType.管理员));
            lst.Add(new _loginInfo_(Language_.Get语言("技术员"), "8888", LoginUserType.技术员));
            lst.Add(new _loginInfo_(Language_.Get语言("操作员"), "0000", LoginUserType.操作员));
            return lst.ToArray();
        }


        public static void 读写本地用户信息(ushort model)
        {
            new 软件类();
            switch (Config.登陆方式)
            {
                case _login_登陆方式_.本地:

                    #region 本地


                    string path = 软件类.Files_Cfg.Files_sysConfig + "\\login.dll";
                    if (!new 文件_文件夹().文件_是否存在(path))
                    {
                        Config.loginInfo_Beff = 生成原始数据();
                    }
                    _loginInfo_[] info = Config.loginInfo_Beff;
                    new 文件_文件夹().WriteReadJson(path, model, ref info, out string msgErr, null, true);
                    Config.loginInfo_Beff = info;

                    #endregion

                    break;
                case _login_登陆方式_.远端:

                    #region 远端

                    switch (model)
                    {
                        case 1:
                            Config.loginInfo_Beff = On_Event_获取全部用户信息();
                            break;
                        case 0:
                            Config.loginInfo_Beff = Event_保存全部用户信息(Config.loginInfo_Beff);
                            break;
                    }

                    #endregion

                    break;
            }

        }


        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <param name="LoginShowType_"></param>
        /// <param name="LoginModelType_"></param>
        /// <returns>成功: MessageBoxResult.Yes </returns>
        public static MessageBoxResult 登陆(string Caption = "用户登陆", LoginShowType LoginShowType_ = LoginShowType.用户登陆, LoginModelType LoginModelType_ = LoginModelType.本地数据)
        {
            return new Win_UserLogin(Caption, LoginShowType_, LoginModelType_).ShowDialog();
        }

        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <param name="LoginShowType_"></param>
        /// <param name="LoginModelType_"></param>
        /// <returns>成功: MessageBoxResult.Yes </returns>
        public static MessageBoxResult 登陆(Window d, string Caption = "用户登陆", LoginShowType LoginShowType_ = LoginShowType.用户登陆, LoginModelType LoginModelType_ = LoginModelType.本地数据)
        {
            return new Win_UserLogin(Caption, LoginShowType_, LoginModelType_) { Owner = Window.GetWindow(d) }.ShowDialog();
        }


        public static void Win_用户管理(Window d)
        {
            if (Config.loginInfo.UserType < LoginUserType.管理员)
            {
                MessageBox.Show(Language_.Get语言("权限过低"), "", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            new Win_用户管理(Config.loginInfo_Beff) { Owner = Window.GetWindow(d) }.ShowDialog();
        }





        /// <summary>
        /// 远程数据时使用此事件
        /// <para>参数(stirng)用户名称,(string)密码</para>
        /// <return>返回 (_login_远程登陆反馈_) 登陆的用户信息</return>
        /// </summary>
        public static event Func<string, string, _login_远程登陆反馈_> Event_用户登陆;

        /// <summary>
        /// 当前登陆成功的用存放在 Login登陆.Config.loginInfo 中
        /// </summary>
        public static event Action Event_登陆成功;

        internal static _login_远程登陆反馈_ On_Event_用户登陆(string UserName, string Password)
        {
            _login_远程登陆反馈_ info = new _login_远程登陆反馈_();

            if (Event_用户登陆 != null)
            {
                info = Event_用户登陆(UserName, Password);
            }
            return info;
        }
        internal static void On_Event_登陆成功()
        {
            Event_登陆成功?.Invoke();
        }


        public static event Func<_loginInfo_[]> Event_获取全部用户信息;
        private static _loginInfo_[] On_Event_获取全部用户信息()
        {
            return Event_获取全部用户信息 is null ? new _loginInfo_[0] : Event_获取全部用户信息.Invoke();
        }

        public static event Func<_loginInfo_[], _loginInfo_[]> Event_保存全部用户信息;
        private static _loginInfo_[] On_Event_保存全部用户信息(_loginInfo_[] loginInfo)
        {
            return Event_保存全部用户信息 is null ? new _loginInfo_[0] : Event_保存全部用户信息.Invoke(loginInfo);
        }




    }
}
