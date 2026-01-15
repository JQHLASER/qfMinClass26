using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace qfCode
{
    public class Sqlite文件_ : Iwork_文件
    {
        编码_ _CodeSys;
        public Sqlite文件_(编码_ CodeSys)
        {
            this._CodeSys = CodeSys;
            this._CodeSys._Db_sqlSugar.Event_ConnectionConfig += (s) =>
            {

            };
            this._CodeSys._Db_sqlSugar.Event_初始化结束 += (s, e) =>
            {

            };
        }

    }
}
