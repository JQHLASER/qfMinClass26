
using qfmain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace qfWPFmain
{
    public class Socket_Client : qfmain.Socket_Client
    {

        public Socket_Client(string path_, _解码_Cfg_ 解码参数_) : base(path_,  解码参数_)
        {

        }

        public Socket_Client(string path_) : base(path_)
        {

        }

        public Socket_Client(): base()
        {

        }



        public void 窗体_设置(Window d, string Title = "TCP/IP Client")
        {
            MessageBoxResult rt = new Win_Set_Socket(this, null, Title) { Owner = Window.GetWindow(d) }.ShowDialog();
            if (rt == MessageBoxResult.OK)
            {
                this.Connect连接Async( );
            }
        }

        public void 窗体_设置(Window d, qfmain.Socket_Client Socket_Client_, string Title = "TCP/IP Client")
        {
            MessageBoxResult rt = new Win_Set_Socket(Socket_Client_, null, Title) { Owner = Window.GetWindow(d) }.ShowDialog();
            if (rt == MessageBoxResult.OK)
            {
                Socket_Client_.Connect连接Async( );
            }
        }







        public void 标题栏状态(ui_window_Title 标题栏, string Name)
        {
            _windowInfo_[] beff = new _windowInfo_[]
            {
                new _windowInfo_(Name,(int)qfmain._连接状态_.已连接, $"{Name}"+Language_.Get语言("已连接")),
                new _windowInfo_(Name,(int)qfmain._连接状态_.连接中, $"{Name}"+Language_.Get语言("连接中")),
                new _windowInfo_(Name,(int)qfmain._连接状态_.未连接, $"{Name}"+Language_.Get语言("未连接")),
            };

            标题栏.Add(beff, (int)this._连接状态);
        }

        /// <summary>
        /// 通讯状态
        /// </summary>
        /// <param name="标题栏"></param>
        /// <param name="Name"></param>
        /// <param name="is通讯中"></param>
        public void 标题栏状态(ui_window_Title 标题栏, string Name, bool is通讯中)
        {
            _windowInfo_[] beff = new _windowInfo_[]
            {
                new _windowInfo_(Name,(int)qfmain._通讯中状态_.闲置, $""),
                new _windowInfo_(Name,(int)qfmain._通讯中状态_.通讯中, $"{Name}"+Language_.Get语言("通讯中")),
            };
            this._通讯状态 = is通讯中 ? _通讯中状态_.通讯中 : _通讯中状态_.闲置;
            int a = is通讯中 ? 1 : 0;
            标题栏.Add(beff, a);
        }





    }
}
