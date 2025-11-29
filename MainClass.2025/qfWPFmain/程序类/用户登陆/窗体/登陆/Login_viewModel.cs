using CommunityToolkit.Mvvm.ComponentModel;
using qfmain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace qfWPFmain
{
    public partial class Login_viewModel : ObservableObject
    {
        public class _语言_
        {
            public string 登陆 { get; set; } = Language_.Get语言("登陆");
            public string 关闭 { get; set; } = Language_.Get语言("关闭");
            public string 标题 { get; set; } = Language_.Get语言("用户登陆标题");
        }



        public Login_viewModel()
        {
           
        }

        [ObservableProperty]
        private _语言_ language_语言 = new _语言_();


        /// <summary>
        /// 本地数据时,所有的用户信息
        /// </summary>
        [ObservableProperty]
        private _loginInfo_[] items_LoginInfo = new _loginInfo_[0];

        [ObservableProperty]
        private string dateTimeNow = string.Empty;

        [ObservableProperty]
        private double value进度条 = 0;

        [ObservableProperty]
        private bool isRun = true;

        [ObservableProperty]
        private bool isRun_进度条 = true;

        internal void Get_获取本地用户信息()
        {
            Login登陆.读写本地用户信息(1 );
            Items_LoginInfo = Login登陆.Config.loginInfo_Beff;
        }


        internal void 系统时间()
        {

            while (isRun)
            {
                DateTimeNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:sss");

                if (isRun_进度条)
                {
                    this.Value进度条++;
                }

                Thread.Sleep(1000);
            }
        }




    }
}
