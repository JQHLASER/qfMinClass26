using qfmain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace qfNet
{
    public class Socket_Client : qfmain.Socket_Client
    {

        /// <summary>
        /// path : 存储文件路径
        /// </summary>
        /// <param name="path_"></param>
        public Socket_Client(string path_, _解码_Cfg_ cfg) : base(path_, cfg)
        {

        }


        /// <summary>
        /// path : 存储文件路径
        /// </summary>
        /// <param name="path_"></param>
        public Socket_Client(string path_) : base(path_)
        {

        }

        public Socket_Client() : base()
        {

        }

        public DialogResult 窗体设置(qfmain.Socket_Client Client, string Title)
        {
            DialogResult result = DialogResult.None;
            using (Form_TCP设置 forms = new Form_TCP设置(Title, Client, null))
            {
                result = forms.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Client.重连_不停止线程();
                }
            }
            return result;
        }

        public DialogResult 窗体设置(string Title)
        {
            return 窗体设置(this, Title);
        }


        public void 标题栏状态(窗体_标题栏状态 标题栏, string Name, _连接状态_ state)
        {
            _cfg_标题栏状态_[] beff = new _cfg_标题栏状态_[]
            {
                new _cfg_标题栏状态_(Name, $"{Name}"+Language_.Get语言("已启动"),(int)qfmain._连接状态_ .已连接 ),
                new _cfg_标题栏状态_(Name, $"{Name}"+Language_.Get语言("启动中"),(int)qfmain._连接状态_.连接中 ),
                new _cfg_标题栏状态_(Name, $"{Name}"+Language_.Get语言("未启动"),(int)qfmain._连接状态_.未连接 ),
            };

            标题栏.Add(beff, (int)state);
        }


    }
}
