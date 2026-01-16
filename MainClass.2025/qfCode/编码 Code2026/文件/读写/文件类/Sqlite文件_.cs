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
        string _path = "";
        string _ConfigID = "_Code26_sqlite_";

        public Sqlite文件_(编码_ CodeSys)
        {
            this._CodeSys = CodeSys;
            this._path = this._CodeSys._文件夹_属性.参数 + "\\Code26.db";

            this._CodeSys._Db_sqlSugar.Event_ConnectionConfig += (s) =>
            {
                #region 连接数据库

                s.Add(this._CodeSys._Db_sqlSugar.生成连接信息(
               this._CodeSys._Db_sqlSugar.生成连接字符串(new qfSqlSugar._cfg_SQLite_
               {
                   Path = this._path,
               })
               , this._ConfigID, SqlSugar.DbType.Sqlite));

                #endregion
            };
            this._CodeSys._Db_sqlSugar.Event_初始化结束 += (s, e) =>
            {

            };
        }

    }
}
