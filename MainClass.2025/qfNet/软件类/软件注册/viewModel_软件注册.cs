using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace qfNet
{
    internal class viewModel_软件注册 : ViewModelBase
    {
        qfNet.软件注册 keys_sys;

        public viewModel_软件注册(qfNet.软件注册 keys_sys_)
        {
            _uiSyncContext = SynchronizationContext.Current;

            this.keys_sys = keys_sys_;
            tcpClient_sys = new qfmain.软件注册_TCP通讯_终端版(this.keys_sys);
            tcpClient_sys.Event_TCP信息 += this.On_TCP信息;
            tcpClient_sys.Event_注册结果 += this.On_注册结果;
            tcpClient_sys.Event_更新机器码 += On_更新机器码;

            this.机器码 = this.keys_sys._机器码;
            Show二维码_机器码();
            this.softInfo = this.keys_sys._msgErr;
            this.Show_状态栏();

            this.Is试用 = keys_sys_._是否试用;
            if (this.Is试用 ||
               (this.keys_sys._注册类型 == qfmain._软件授权_注册类型_.加密狗 &&
               this.keys_sys._err != qfmain._软件授权_Err_.已完全注册 &&
               this.keys_sys._err != qfmain._软件授权_Err_.已日期注册))
            {
                this.CheckShow = true;
            }
            else
            {
                this.CheckShow = false;
            }

        }



        #region 数据


        private string _title = "";
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        private string _机器码 = "";
        public string 机器码
        {
            get => _机器码;
            set
            {
                _机器码 = value;
                OnPropertyChanged();
            }
        }

        private string _注册码 = "";
        public string 注册码
        {
            get => _注册码;
            set
            {
                _注册码 = value;
                OnPropertyChanged();
            }
        }

        private bool _is试用 = false;
        public bool Is试用
        {
            get => _is试用;
            set
            {
                _is试用 = value;
                OnPropertyChanged();
            }
        }

        private bool _CheckShow = false;
        /// <summary>
        /// 显示试用控件
        /// </summary>
        public bool CheckShow
        {
            get => _CheckShow;
            set
            {
                _CheckShow = value;
                OnPropertyChanged();
            }
        }
        private Image _image机器码 = null;
        /// <summary>
        /// 显示试用控件
        /// </summary>
        public Image image机器码
        {
            get => _image机器码;
            set
            {
                _image机器码 = value;
                OnPropertyChanged();
            }
        }


        private string _信息 = "";
        public string 信息
        {
            get => _信息;
            set
            {
                _信息 = value;
                OnPropertyChanged();
            }
        }


        private string _注册码_language = Language_.Get语言("注册码");
        public string 注册码_language
        {
            get => _注册码_language;
            set
            {
                _注册码_language = value;
                OnPropertyChanged();
            }
        }

        private string _机器码_language = Language_.Get语言("机器码");
        public string 机器码_language
        {
            get => _机器码_language;
            set
            {
                _机器码_language = value;
                OnPropertyChanged();
            }
        }


        private string _试用_language = Language_.Get语言("试用");
        public string 试用_language
        {
            get => _试用_language;
            set
            {
                _试用_language = value;
                OnPropertyChanged();
            }
        }


        private string _注册_language = Language_.Get语言("注册");
        public string 注册_language
        {
            get => _注册_language;
            set
            {
                _注册_language = value;
                OnPropertyChanged();
            }
        }


        #endregion

        #region 方法

        internal void 释放()
        {
            tcpClient_sys.Event_TCP信息 -= this.On_TCP信息;
            tcpClient_sys.Event_注册结果 -= this.On_注册结果;
            tcpClient_sys.Event_更新机器码 -= On_更新机器码;


            tcpClient_sys.释放();
        }

        string tcpInfo = "";
        string softInfo = "";

        private readonly object _lock = new object();
        internal void Show_状态栏()
        {
            lock (_lock)
            {
                string show = this.keys_sys._是否试用 ? $"【{Language_.Get语言("试用模式")}】" : "";
                _uiSyncContext.Post(_ => { this.信息 = $"【{this.tcpInfo}】{show}{this.softInfo}"; }, null);
            }
        }



        void Show二维码_机器码()
        {
            qfmain._QRcode_Cfg_ info = new qfmain._QRcode_Cfg_();
            info.像素大小 = 100;
            info.是否绘制空白边框 = true;

            new qfmain.QRcode().生成(this.机器码, out System.Drawing.Bitmap img, info, out string msgErr);

            this.image机器码 = img;

        }


        internal bool 注册(qfNet.软件注册 keys_sys, out string msgErr)
        {
            if (string.IsNullOrEmpty(this.注册码))
            {
                msgErr = Language_.Get语言("注册码不能为空");
                return false;
            }

            bool rt = true;
            msgErr = string.Empty;
            qfmain._软件授权_Err_ Err = keys_sys.注册(this.注册码, keys_sys._机器码信息, out qfmain._软件授权_注册信息_ 注册信息, out msgErr);
            rt = (Err == qfmain._软件授权_Err_.已完全注册 || Err == qfmain._软件授权_Err_.已日期注册) ? true : false;
            this.信息 = msgErr;

            if (rt)
            {
                保存注册码(keys_sys);
            }



            return rt;
        }

        void 保存注册码(qfNet.软件注册 keys_sys)
        {
            string value = this.注册码;
            keys_sys.注册码读写_从本地(0, ref value);
            this.注册码 = value;
        }

        internal void 试用()
        {
            if (!this.Is试用 && MessageBox.Show($"{Language_.Get语言("使能试用")}?", "", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }
            else if (this.Is试用 && MessageBox.Show($"{Language_.Get语言("取消试用")}?", "", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }

            bool a = !this.Is试用;
            this.keys_sys.是否试用读写_从本地(0, ref a);
            this.Is试用 = a;
            this.keys_sys._是否试用 = this.Is试用;
            Show_状态栏();
            this.keys_sys.获取信息();
            if (!a)
            {
                this.机器码 = string.Empty;
                this.注册码 = string.Empty;
            }


            MessageBox.Show($"{Language_.Get语言("请重启软件")}", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }



        #region TCP远程注册通讯

        qfmain.软件注册_TCP通讯_终端版 tcpClient_sys;

        internal async void TcpClien_设置窗体()
        {
            if (this.tcpClient_sys.TcpClient_sys is null)
            {
                return;
            }
       
            DialogResult rt = new Socket_Client().窗体设置(tcpClient_sys.TcpClient_sys, "QF Server");
            if (rt == DialogResult.OK)
            {
              await   this.tcpClient_sys.TcpClient_sys.Connect连接Async();
            }
        }


        private readonly SynchronizationContext _uiSyncContext;
        void On_TCP信息(string msg)
        {
            this.tcpInfo = msg;
            Show_状态栏();
        }

        void On_注册结果(bool 是否成功, string 注册码, string msg)
        {
            _uiSyncContext.Post(_ =>
            {
                this.注册码 = 注册码;
                this.softInfo = msg;
            }, null);
            Show_状态栏();
        }

        void On_更新机器码(string 机器码)
        {
            _uiSyncContext.Post(_ =>
            {
                this.机器码 = 机器码;
            }, null);
        }

        #endregion





        #endregion


    }
}
