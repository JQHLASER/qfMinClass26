using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace qfNet
{
    public enum _em文件_类型_
    {
        ini,
        SQLite,
    }

    public class 文件_<T>
    {

        public Iwork_文件_<T> _Iwork;
        public 文件_(string File, string 文件类型, string 后缀, _em文件_类型_ 类型 = _em文件_类型_.ini)
        {
            this._Iwork = new 文件_本地<T>(File, 文件类型, 后缀);
        }

        public 文件_(string 文件类型, string 后缀, _em文件_类型_ 类型 = _em文件_类型_.SQLite)
        {

        }


        public bool 读写最后一次打开的文件(ushort model, ref string FileName, string path)
        {
            return new qfmain.文件_文件夹().WriteReadIni(path, model, ref FileName, out string msgErr);
        }

    }
}
