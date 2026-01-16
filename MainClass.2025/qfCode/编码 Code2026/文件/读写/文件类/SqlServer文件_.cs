using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static qfCode.表;

namespace qfCode
{
    public class SqlServer文件_ : Iwork_文件
    {
        编码_ _CodeSys;
        string _ConfigID = "_Code26_sqlserver_";
     


        public SqlServer文件_(编码_ CodeSys)
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
