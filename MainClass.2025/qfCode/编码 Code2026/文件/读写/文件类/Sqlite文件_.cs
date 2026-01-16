using qfmain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace qfCode
{
    public class Sqlite文件_ : Iwork_文件
    {
        编码_ _CodeSys;
        string _path = "";
        string _ConfigID = "_Code26_sqlite_";
        qfSqlSugar.SqlSugar_Table<表.Code26> _Table;


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
                _Table = new qfSqlSugar.SqlSugar_Table<表.Code26>(e, this._ConfigID);
                (bool s, string m, _文件_属性_ cfg) rt = Read("text^%&");
                this._CodeSys._初始化状态 = !rt.s ? _初始化状态_.已初始化 : _初始化状态_.未初始化;

            };
        }


        public (bool s, string m, _文件_属性_ cfg) Read(string FileName)
        {
            if (this._Table is null)
            {
                return (false, "_Table is Null", default);
            }

            bool rt = this._Table.GetList(u => u.FileName == FileName, out List<表.Code26> lst, out string msgErr);
            if (rt && lst.Count == 0)
            {
                return (rt, "not cfg", default);
            }
            else if (rt && lst.Count > 0)
            {
                return new Json序列化().转成Json(lst[0].CodeValue);
            }
            else
            {
                return (rt, msgErr, default);
            }


        }

        public (bool s, string m) Write(string FileName, _文件_属性_ cfg)
        {
            if (this._Table is null)
            {
                return (false, "_Table is Null");
            }
            表.Code26 cdoe = new 表.Code26
            {
                FileName = FileName,
                CodeValue = new Json序列化().转成String(cfg),
            };

            bool rt = this._Table.Storageable(cdoe, out int count, out string msgErr);
            if (!rt)
            {
                return (rt, msgErr);
            }
            else if (count == 0)
            {
                return (false, "受影响0行");
            }
            return (rt, "成功");

        }





    }
}
