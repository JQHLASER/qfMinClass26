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
        /// 使用INI文件
        /// </summary>
        /// <param name="File"></param>
        /// <param name="文件类型"></param>
        /// <param name="后缀"></param>
        public void 初始化_ini(string File, string 文件类型, string 后缀)
        {
            this._Iwork = new 文件_ini<T>( );
            this._Iwork.Event_初始化状态 += (s, e) => On_初始化状态(s, e);
            this._Iwork.初始化(File, 文件类型, 后缀);
        }

        /// <summary>
        /// 使用SQLite
        /// </summary> 
        public void  初始化_SQLite(string File, string 文件类型)
        {
            this._Iwork = new 文件_SQLite<T>();
            this._Iwork.Event_初始化状态 +=(s,e)=> On_初始化状态(s,e);
            this._Iwork.初始化(File, 文件类型,"");
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
