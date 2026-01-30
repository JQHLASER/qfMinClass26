using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace qfNet
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class _loginInfo_
    {
        public _loginInfo_()
        {

        }
        public _loginInfo_(string UserName_, string Password_, _LoginUserType_ UserType_)
        {
            UserName = UserName_;
            Passwrord = Password_;
            UserType = UserType_;
        }

        public _loginInfo_ Clone()
        {
            return new _loginInfo_
            {
                UserName = this.UserName,
                Passwrord = this.Passwrord,
                UserType = this.UserType
            };
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
        public _LoginUserType_ UserType { set; get; }





    }


    /// <summary>
    /// 用户权限
    /// </summary>
    public enum _LoginUserType_
    {
        操作员,
        技术员,
        管理员,
        超级管理员,
        开发者,
    }

    /// <summary>
    /// 登陆方式
    /// </summary>
    public enum _LoginModelType_
    {
        /// <summary>
        /// 用户信息保存在本地
        /// </summary>
        本地数据,
        /// <summary>
        /// 用户信息保存在服务器
        /// </summary>
        远程数据,
    }


    /// <summary>
    /// 登陆方式
    /// </summary>
    public enum _Login登陆类型_
    {
        /// <summary>
        /// 选择用户
        /// </summary>
        选择用户,
        /// <summary>
        /// 输入用户
        /// </summary>
        输入用户,
    }



    /// <summary>
    /// 登陆界面/切换界面
    /// </summary>
    public enum _LoginShowType_
    {
        用户登陆,
        用户切换,
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




    public class Login登陆
    {


        public string _开发者 = "JQHLASER";
        /// <summary>
        /// 本地数据 / 远端数据
        /// </summary>
        public _LoginModelType_ _登陆方式 = _LoginModelType_.本地数据;

        /// <summary>
        /// 选择用户 / 输入用户
        /// </summary>
        public _Login登陆类型_ _类型 = _Login登陆类型_.选择用户;

        /// <summary>
        /// 信息
        /// </summary>
        public Config _Config = new Config();



        public class Config
        {
            /// <summary>
            /// 所有用户信息
            /// </summary>
            public _loginInfo_[] loginInfo_Beff { set; get; } = new _loginInfo_[0];

            /// <summary>
            /// 当前已登陆的用户
            /// </summary>
            public _loginInfo_ loginInfo { set; get; } = null;

        }

        public _loginInfo_[] 生成原始数据()
        {
            List<_loginInfo_> lst = new List<_loginInfo_>();
            lst.Add(new _loginInfo_("admin", "QF8888", _LoginUserType_.超级管理员));
            lst.Add(new _loginInfo_(Language_.Get语言("管理员"), "9999", _LoginUserType_.管理员));
            lst.Add(new _loginInfo_(Language_.Get语言("技术员"), "8888", _LoginUserType_.技术员));
            lst.Add(new _loginInfo_(Language_.Get语言("操作员"), "0000", _LoginUserType_.操作员));
            return lst.ToArray();
        }


        public void 读写本地用户信息(ushort model)
        {
            new qfmain.软件类();
            switch (this._登陆方式)
            {
                case _LoginModelType_.本地数据:

                    #region 本地


                    string path = qfmain.软件类.Files_Cfg.Files_sysConfig + "\\login.dll";
                    if (!new qfmain.文件_文件夹().文件_是否存在(path))
                    {
                        this._Config.loginInfo_Beff = 生成原始数据();
                    }
                    _loginInfo_[] info = this._Config.loginInfo_Beff.Select(i => i.Clone()).ToArray();
                    new qfmain.文件_文件夹().WriteReadJson(path, model, ref info, out string msgErr, qfmain._em_json类型_.SystemIOjsontext ,null, true);
                    this._Config.loginInfo_Beff = info;

                    #endregion

                    break;
                case _LoginModelType_.远程数据:

                    #region 远端

                    switch (model)
                    {
                        case 1:
                            this._Config.loginInfo_Beff = On_Event_获取全部用户信息();
                            break;
                        case 0:
                            this._Config.loginInfo_Beff = Event_保存全部用户信息(this._Config.loginInfo_Beff);
                            break;
                    }

                    #endregion

                    break;
            }

        }


        /// <summary>
        /// 用户登陆,OK表示成功
        /// </summary>
        /// <param name="_LoginShowType__"></param>
        /// <param name="LoginModelType_"></param>
        /// <returns>成功: MessageBoxResult.Yes </returns>
        public DialogResult 登陆(_LoginShowType_ LoginShowType_ = _LoginShowType_.用户登陆)
        {
            DialogResult result = DialogResult.None;
            using (Form_用户登陆 forms = new Form_用户登陆(this, LoginShowType_))
            {
                result = forms.ShowDialog();
            }

            return result;
        }


        public string 语言_用户权限(_LoginUserType_ type)
        {
            return Language_.Get语言($"{type}");
        }

        public void Win_用户管理()
        {
            if (this._Config.loginInfo.UserType < _LoginUserType_.管理员)
            {
                MessageBox.Show(Language_.Get语言("权限过低"), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            using (Form_Login_管理 forms = new Form_Login_管理(this))
            {
                forms.ShowDialog();
            }
        }





        /// <summary>
        /// 远程数据时使用此事件
        /// <para>参数(stirng)用户名称,(string)密码</para>
        /// <return>返回 (_login_远程登陆反馈_) 登陆的用户信息</return>
        /// </summary>
        public event Func<string, string, _login_远程登陆反馈_> Event_用户登陆;

        /// <summary>
        /// 当前登陆成功的用存放在 Login登陆.Config.loginInfo 中
        /// </summary>
        public event Action Event_登陆成功;
        internal void On_Event_登陆成功()
        {
            Event_登陆成功?.Invoke();
        }




        internal _login_远程登陆反馈_ On_Event_用户登陆(string UserName, string Password)
        {
            _login_远程登陆反馈_ info = new _login_远程登陆反馈_();

            if (Event_用户登陆 != null)
            {
                info = Event_用户登陆(UserName, Password);
            }
            return info;
        }


        public event Func<_loginInfo_[]> Event_获取全部用户信息;
        private _loginInfo_[] On_Event_获取全部用户信息()
        {
            return Event_获取全部用户信息 is null ? new _loginInfo_[0] : Event_获取全部用户信息.Invoke();
        }

        public event Func<_loginInfo_[], _loginInfo_[]> Event_保存全部用户信息;
        private _loginInfo_[] On_Event_保存全部用户信息(_loginInfo_[] loginInfo)
        {
            return Event_保存全部用户信息 is null ? new _loginInfo_[0] : Event_保存全部用户信息.Invoke(loginInfo);
        }




    }
}
