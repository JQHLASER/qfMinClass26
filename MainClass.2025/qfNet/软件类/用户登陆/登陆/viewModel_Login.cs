using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfNet
{
    internal class viewModel_Login : ViewModelBase
    {

        private string _Title = Language_.Get语言("用户登陆");
        public string Title
        {
            get { return _Title; }
            set
            {
                _Title = value;
                OnPropertyChanged();
            }
        }


        private string _button_登陆 = Language_.Get语言("登陆");
        public string Button_登陆
        {
            get { return _button_登陆; }
            set
            {
                _button_登陆 = value;
                OnPropertyChanged();
            }
        }

        private string _button_关闭 = Language_.Get语言("关闭");
        public string Button_关闭
        {
            get { return _button_关闭; }
            set
            {
                _button_关闭 = value;
                OnPropertyChanged();
            }
        }


        private string _时间 = "0000/00/00 00:00:00";
        public string 时间
        {
            get { return _时间; }
            set
            {
                _时间 = value;
                OnPropertyChanged();
            }
        }

        private string _label_用户 = Language_.Get语言("用户");
        public string label_用户
        {
            get { return _label_用户; }
            set
            {
                _label_用户 = value;
                OnPropertyChanged();
            }
        }

        private string _label_密码 = Language_.Get语言("密码");
        public string label_密码
        {
            get { return _label_密码; }
            set
            {
                _label_密码 = value;
                OnPropertyChanged();
            }
        }

        private _loginInfo_[] _用户信息 = new _loginInfo_[0];
        public _loginInfo_[] 用户信息
        {
            get { return _用户信息; }
            set
            {
                _用户信息 = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///选中的索引
        /// </summary>
        private int _SelectIndex = -1;
        public int SelectIndex
        {
            get { return _SelectIndex; }
            set
            {
                _SelectIndex = value;
                OnPropertyChanged();
            }
        }



        private bool _ShowTaskBar = true;
        /// <summary>
        /// 是否显示在任务栏上显示ICO
        /// </summary>
        public bool ShowTaskBar
        {
            get { return _ShowTaskBar; }
            set
            {
                _ShowTaskBar = value;
                OnPropertyChanged();
            }
        }







    }
}
