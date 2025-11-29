using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace qfWPFmain
{
    internal partial class Win_软件注册_viewModel : ObservableObject
    {

        qfmain.软件注册 keys_sys;

        public class _语言_
        {
            public string 注册 { set; get; } = Language_.Get语言("注册");
            public string 试用 { set; get; } = Language_.Get语言("试用");

        }


         
        internal Win_软件注册_viewModel(qfmain.软件注册 keys_sys_)
        {

            this.keys_sys = keys_sys_;
            tcpClient_sys = new qfmain.软件注册_TCP通讯_终端版(this.keys_sys);
            tcpClient_sys.Event_TCP信息 += this.On_TCP信息;
            tcpClient_sys.Event_注册结果 += this.On_注册结果;
            tcpClient_sys.Event_更新机器码 += On_更新机器码;

            this.Code_机器码 = this.keys_sys._机器码;
            Show二维码_机器码();
            this.SoftInfo = this.keys_sys._msgErr;
            this.Show_状态栏();


            this.Is试用 = keys_sys_._是否试用;
            if (this.Is试用 ||
               (this.keys_sys._注册类型 == qfmain._软件授权_注册类型_.加密狗 &&
               this.keys_sys._err != qfmain._软件授权_Err_.已完全注册 &&
               this.keys_sys._err != qfmain._软件授权_Err_.已日期注册))
            {
                this.Vis_试用 = Visibility.Visible;
            }
            else
            {
                this.Vis_试用 = Visibility.Collapsed;
            }
        }

        internal void 释放()
        {
            tcpClient_sys.Event_TCP信息 -= this.On_TCP信息;
            tcpClient_sys.Event_注册结果 -= this.On_注册结果;
            tcpClient_sys.Event_更新机器码 -= On_更新机器码;


            tcpClient_sys.释放();
        }


        [ObservableProperty]
        private string code_机器码 = "";

        [ObservableProperty]
        private BitmapImage qRcode_二维码 = new BitmapImage();

        [ObservableProperty]
        private _语言_ language_语言 = new _语言_ ();

        [ObservableProperty]
        private string code_注册码 = "";

        [ObservableProperty]
        private string label_状态栏 = "";

        /// <summary>
        /// 服务器连接状态
        /// </summary>
        [ObservableProperty]
        private string tcpInfo = $"server:{Language_.Get语言("已连接")}";

        /// <summary>
        /// 软件注册状态
        /// </summary>
        [ObservableProperty]
        private string softInfo = "";

        [ObservableProperty]
        private bool is试用 = false;

        /// <summary>
        /// 显示试用控件
        /// </summary>
        [ObservableProperty]
        private Visibility vis_试用 = Visibility.Collapsed;




        private readonly object _lock = new object();
        internal void Show_状态栏()
        {
            lock (_lock)
            {
                string show = this.keys_sys._是否试用 ? $"【{Language_.Get语言("试用模式")}】" : "";
                this.Label_状态栏 = $"【{this.tcpInfo}】{show}{this.softInfo}";
            }
        }



        void Show二维码_机器码()
        {
            qfmain._QRcode_Cfg_ info = new qfmain._QRcode_Cfg_();
            info.像素大小 = 100;
            info.是否绘制空白边框 = false;
            new QRcode().生成(this.Code_机器码, out System.Drawing.Bitmap img, info, out string msgErr);
            new bitmapImage_().ImageToBitmapImage(img, out BitmapImage Bitimage, out string msgErr1);
            this.QRcode_二维码 = Bitimage;

        }


        internal bool 注册(软件注册 keys_sys, out string msgErr)
        {
            if (string.IsNullOrEmpty(this.code_注册码))
            {
                msgErr = Language_.Get语言("注册码不能为空");
                return false;
            }

            bool rt = true;
            msgErr = string.Empty;
            qfmain._软件授权_Err_ Err = keys_sys.注册(this.Code_注册码, keys_sys._机器码信息, out qfmain._软件授权_注册信息_ 注册信息, out msgErr);
            rt = (Err == qfmain._软件授权_Err_.已完全注册 || Err == qfmain._软件授权_Err_.已日期注册) ? true : false;
            this.Label_状态栏 = msgErr;

            if (rt)
            {
                保存注册码(keys_sys);
            }



            return rt;
        }

        void 保存注册码(软件注册 keys_sys)
        {
            string value = this.Code_注册码;
            keys_sys.注册码读写_从本地(0, ref value);
            this.Code_注册码 = value;
        }

        internal void 试用()
        {
            if (!this.Is试用 && MessageBox.Show($"{Language_.Get语言("使能试用")}?", "", MessageBoxButton.YesNo) == MessageBoxResult.No)
            {
                return;
            }
            else if (this.Is试用 && MessageBox.Show($"{Language_.Get语言("取消试用")}?", "", MessageBoxButton.YesNo) == MessageBoxResult.No)
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
                this.Code_机器码 = string.Empty;
                this.Code_注册码 = string.Empty;
            }


            MessageBox.Show($"{Language_.Get语言("请重启软件")}", "", MessageBoxButton.OK, MessageBoxImage.Error);
        }



        #region TCP远程注册通讯

        qfmain.软件注册_TCP通讯_终端版 tcpClient_sys;

        internal void TcpClien_设置窗体(Window d)
        {
            if (this.tcpClient_sys.TcpClient_sys is null)
            {
                return;
            }
            MessageBoxResult rt = new Win_Set_Socket(this.tcpClient_sys.TcpClient_sys, null, "QF Server") { Owner = Window.GetWindow(d) }.ShowDialog();
            if (rt == MessageBoxResult.OK)
            {
                this.tcpClient_sys.TcpClient_sys.Connect连接Async( );
            }
        }



        void On_TCP信息(string msg)
        {
            this.TcpInfo = msg;
            Show_状态栏();
        }

        void On_注册结果(bool 是否成功, string 注册码, string msg)
        {
            this.Code_注册码 = 注册码;
            this.SoftInfo = msg;
            Show_状态栏();
        }

        void On_更新机器码(string 机器码)
        {
            this.Code_机器码 = 机器码;
        }

        #endregion




    }
}
