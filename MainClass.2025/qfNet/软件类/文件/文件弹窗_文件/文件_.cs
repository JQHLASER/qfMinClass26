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
        public 文件_(string File, string 文件类型, string 后缀 )
        {
            this._Iwork = new 文件_ini<T>(File, 文件类型, 后缀);
        }

        /// <summary>
        /// 使用SQLite
        /// </summary> 
        public 文件_(string File, string 文件类型 )
        {
            this._Iwork = new 文件_SQLite<T>(File, 文件类型);
        }

         
        public bool 读写最后一次打开的文件(ushort model, ref string FileName, string path)
        {
            return new qfmain.文件_文件夹().WriteReadIni(path, model, ref FileName, out string msgErr);
        }

    }
}
