using qfmain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace qfWPFmain
{
    public class SerialPort_ : qfmain.SerialPort_
    {
        public SerialPort_(string path_, _解码_Cfg_ 解码参数_) : base(path_,  解码参数_)
        {
        }
        public SerialPort_(string path_) : base(path_)
        {

        }
        public SerialPort_()
        {

        }

        public void 窗体_设置(Window d, string Title)
        {
            MessageBoxResult rt = new Win_Set_SerialPort(this, Title) { Owner = Window.GetWindow(d) }.ShowDialog();
            if (rt == MessageBoxResult.OK)
            {
                this.Open(out string msgErr);
            }
        }


        public void 标题栏状态(ui_window_Title 标题栏, string Name)
        {
            _windowInfo_[] beff = new _windowInfo_[]
           {
                new _windowInfo_(Name,(int)qfmain ._打开状态_ .已打开, $"{Name}"+Language_.Get语言("已打开")),
                new _windowInfo_(Name,(int)qfmain ._打开状态_.打开中, $"{Name}"+Language_.Get语言("打开中")),
                new _windowInfo_(Name,(int)qfmain ._打开状态_.未打开, $"{Name}"+Language_.Get语言("未打开")),
           };

            标题栏.Add(beff, (int)this._打开状态);
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
