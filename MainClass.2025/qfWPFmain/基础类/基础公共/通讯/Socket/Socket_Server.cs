using qfmain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace qfWPFmain
{
    public class Socket_Server : qfmain.Socket_Server
    {

        public Socket_Server(string path_) : base(path_)
        {

        }
         
        public Socket_Server() : base()
        {

        }


        public void 窗体_设置(Window d, string Title = "TCP/IP Server")
        {
            MessageBoxResult rt = new Win_Set_Socket(null, this, Title) { Owner = Window.GetWindow(d) }.ShowDialog();
            if (rt == MessageBoxResult.OK)
            {
                this.StartListen(out string msgErr);
            }
        }
        public void 窗体_设置(Window d, qfmain.Socket_Server Socket_Server_, string Title = "TCP/IP Client")
        {
            MessageBoxResult rt = new Win_Set_Socket(null, Socket_Server_, Title) { Owner = Window.GetWindow(d) }.ShowDialog();
            if (rt == MessageBoxResult.OK)
            {
                Socket_Server_.StartListen(out string msgErr);
            }
        }




        public void 标题栏状态(ui_window_Title 标题栏, string Name)
        {


            标题栏状态(标题栏, Name, this._侦听启动状态);
        }

        public void 标题栏状态(ui_window_Title 标题栏, string Name, _启动状态_ state)
        {
            _windowInfo_[] beff = new _windowInfo_[]
            {
                new _windowInfo_(Name,(int)qfmain._启动状态_ .已启动, $"{Name}"+Language_.Get语言("已启动")),
                new _windowInfo_(Name,(int)qfmain._启动状态_.启动中, $"{Name}"+Language_.Get语言("启动中")),
                new _windowInfo_(Name,(int)qfmain._启动状态_.未启动, $"{Name}"+Language_.Get语言("未启动")),
            };

            标题栏.Add(beff, (int)state);
        }

        /// <summary>
        /// 通讯状态
        /// </summary>
        /// <param name="标题栏"></param>
        /// <param name="Name"></param>
        /// <param name="is通讯中"></param>
        public void 标题栏状态(ui_window_Title 标题栏, string Name, bool is通讯中, _通讯中状态_ 通讯状态)
        {
            _windowInfo_[] beff = new _windowInfo_[]
            {
                new _windowInfo_(Name,(int)qfmain._通讯中状态_.闲置, $""),
                new _windowInfo_(Name,(int)qfmain._通讯中状态_.通讯中, $"{Name}"+Language_.Get语言("通讯中")),
            };

            通讯状态 = is通讯中 ? _通讯中状态_.通讯中 : _通讯中状态_.闲置;
            int a = is通讯中 ? 1 : 0;
            标题栏.Add(beff, a);
        }

    }
}
