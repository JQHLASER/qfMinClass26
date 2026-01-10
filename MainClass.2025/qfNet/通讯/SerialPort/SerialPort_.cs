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






    }
}
