
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace qfNet
{
    public class Socket_Server : qfmain.Socket_Server
    {
        public Socket_Server(string path_, qfmain._解码_Cfg_ cfg = null) : base(path_, cfg)
        {


        }
        public Socket_Server() : base()
        {

        }

        public DialogResult 窗体设置(qfmain.Socket_Server Server, string Title)
        {
            DialogResult result = DialogResult.None;
            using (Form_TCP设置 forms = new Form_TCP设置(Title, null, Server))
            {
                result = forms.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Server.StartListen(out string msgErr);
                }
            }
            return result;
        }
        public DialogResult 窗体设置(string Title)
        {
            return 窗体设置(this, Title);
        }


        public void 标题栏状态(窗体_标题栏状态 标题栏, string Name, qfmain._启动状态_ state)
        {
            _cfg_标题栏状态_[] beff = new _cfg_标题栏状态_[]
            {
                new _cfg_标题栏状态_(Name, $"{Name}"+Language_.Get语言("已启动"),(int)qfmain._启动状态_ .已启动),
                new _cfg_标题栏状态_(Name, $"{Name}"+Language_.Get语言("启动中"),(int)qfmain._启动状态_.启动中),
                new _cfg_标题栏状态_(Name, $"{Name}"+Language_.Get语言("未启动"),(int)qfmain._启动状态_.未启动),
            };

            标题栏.Add(beff, (int)state);
        }




    }
}
