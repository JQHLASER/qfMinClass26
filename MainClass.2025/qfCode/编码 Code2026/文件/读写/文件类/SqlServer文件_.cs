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
        string _ConfigID = "_Code26_";
        string _path = "";
        public SqlServer文件_(编码_ CodeSys)
        {
            this._CodeSys = CodeSys;
            this._path = this._CodeSys._文件夹_属性.参数 + "\\Code26.db";

            this._CodeSys._Db_sqlSugar.Event_ConnectionConfig += (s) =>
            {
                s.Add(this._CodeSys._Db_sqlSugar.生成连接信息(
                    this._CodeSys._Db_sqlSugar.生成连接字符串(new qfSqlSugar ._cfg_SQLite_
                    {
                        Path = this._path,
                    }, qfSqlSugar._SQLite_连接类型_.V3)
                    , this._ConfigID, SqlSugar.DbType.Sqlite));
            };
            this._CodeSys._Db_sqlSugar.Event_初始化结束 += (s, e) =>
            {

            };
        }


    }
}
