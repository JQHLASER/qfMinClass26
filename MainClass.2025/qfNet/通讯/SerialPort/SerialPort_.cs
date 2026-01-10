using qfmain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace qfNet
{
    public class SerialPort_ : qfmain.SerialPort_
    {
        /// <summary>
        /// path_: 存储文件路径
        /// </summary>    
        public SerialPort_(string path_, _解码_Cfg_ cfg) : base(path_, cfg)
        {

        }

        /// <summary>
        /// path_: 存储文件路径
        /// </summary>    
        public SerialPort_(string path_) : base(path_)
        {

        }
        public SerialPort_() : base()
        {

        }


        public DialogResult 窗体设置(string Title)
        {
            DialogResult rt = DialogResult.None;

            using (Form_SerialPort_Set forms = new Form_SerialPort_Set(this, Title))
            {
                rt = forms.ShowDialog();
                if (rt == DialogResult.OK)
                {
                    this.Open(out string msgErr);
                }
            }
            return rt;
        }


        public void 标题栏状态(窗体_标题栏状态 标题栏, string Name, _打开状态_  state)
        {
            _cfg_标题栏状态_[] beff = new _cfg_标题栏状态_[]
            {
                new _cfg_标题栏状态_(Name, $"{Name}"+Language_.Get语言("已打开"),(int)qfmain._打开状态_ .已打开  ),
                new _cfg_标题栏状态_(Name, $"{Name}"+Language_.Get语言("打开中"),(int)qfmain._打开状态_.打开中  ),
                new _cfg_标题栏状态_(Name, $"{Name}"+Language_.Get语言("未打开"),(int)qfmain._打开状态_.未打开  ),
            };

            标题栏.Add(beff, (int)state);
        }



    }
}
