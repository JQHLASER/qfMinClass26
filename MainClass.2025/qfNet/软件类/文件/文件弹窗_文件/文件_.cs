using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace qfNet
{


    public class 文件_<T>
    {

        public Iwork_文件_<T> _Iwork;

        /// <summary>
        /// 使用INI文件,File:文件所在文件夹
        /// </summary> 
        public void 初始化(_em_文件保存方式_ 文件保存方式_, string File, string 文件类型, string 后缀or数据库ID)
        {
            if (文件保存方式_ == _em_文件保存方式_.ini
                || 文件保存方式_ == _em_文件保存方式_.txt)
            {
                this._Iwork = new 文件_ini_txt<T>(); ;
            }
            else if (文件保存方式_ == _em_文件保存方式_.SQLite
                || 文件保存方式_ == _em_文件保存方式_.SQLserver)
            {
                this._Iwork = new 文件_数据库<T>();
            }
            this._Iwork.Event_初始化状态 += (s, e) => On_初始化状态(s, e);
            this._Iwork.初始化(文件保存方式_, File, 文件类型, 后缀or数据库ID);
        }

         

        public bool 读写最后一次打开的文件(ushort model, ref string FileName, string path)
        {
            return new qfmain.文件_文件夹().WriteReadIni(path, model, ref FileName, out string msgErr);
        }


        public event Action<qfmain._初始化状态_, string> Event_初始化状态;
        void On_初始化状态(qfmain._初始化状态_ state, string msgErr)
        {
            Event_初始化状态?.Invoke(state, msgErr);
        }


    }
}
