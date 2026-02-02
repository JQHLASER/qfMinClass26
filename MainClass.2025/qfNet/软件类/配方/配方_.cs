using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfNet
{
    public class 配方_<T>
    {
        public enum _em_文件类型_
        {
            ini,
            SQLite,
        }
        public 文件_<T> Gj_sys;

        public 配方_(string 文件夹, string 后缀, _em_文件类型_ 类型 = _em_文件类型_.SQLite )
        {
            switch (类型)
            {
                case _em_文件类型_.ini:
                    Gj_sys = new 文件_<T>(文件夹, "", 后缀);break;
                case _em_文件类型_.SQLite:
                    Gj_sys = new 文件_<T>(文件夹, ""); break;

            }
           
        }
 





    }
}
