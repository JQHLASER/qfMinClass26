
using Sunny.UI.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace qfCode
{
    public class Sqlite文件_ : Iwork_文件
    {
        编码_ _CodeSys;
        string _path = "";
        string _ConfigID = "_Code26_sqlite_";
        private static readonly object _lock = new object();

        public Sqlite文件_(编码_ CodeSys)
        {
            this._CodeSys = CodeSys;
            this._path = Path.Combine(this._CodeSys._文件夹_属性.参数, "Code26.db");

            this._CodeSys.On_初始化状态(qfmain._初始化状态_.初始化中);
            qfSqlSugar.SqlSugar_DB_封装.Event_ConnectionConfig += (s, e) =>
            {
                #region 连接数据库

                s.Add(e.生成连接信息(
                      e.生成连接字符串(new qfSqlSugar._cfg_SQLite_
                      {
                          Path = this._path,
                      })
                      , this._ConfigID, SqlSugar.DbType.Sqlite));

                #endregion
            };
            qfSqlSugar.SqlSugar_DB_封装.Event_初始化结束 += (s, m, e) =>
             {
                 (bool s, string m, _配方文件_属性_ cfg) rt = Read("text^%&");
                 this._CodeSys._初始化状态 = !rt.s ? qfmain._初始化状态_.已初始化 : qfmain._初始化状态_.未初始化;
                 this._CodeSys.On_初始化状态(this._CodeSys._初始化状态);
             };
        }

        public (bool s, string m, _配方文件_属性_ cfg) Read(string FileName)
        {
            lock (_lock)
            {

                using (qfSqlSugar.SqlSugar_GetDB db_ = new qfSqlSugar.SqlSugar_GetDB(qfSqlSugar.SqlSugar_DB_封装._DB, _ConfigID))
                {
                    using (qfSqlSugar.SqlSugar_Table<表.Code26> _Table = new qfSqlSugar.SqlSugar_Table<表.Code26>(db_.Db))
                    {
                        bool rt = _Table.GetList(u => u.FileName == FileName, out List<表.Code26> lst, out string msgErr);
                        if (rt && lst.Count == 0)
                        {
                            return (rt, Language_.Get语言("未找到文件"), default);
                        }
                        else if (rt && lst.Count > 0)
                        {
                            return new Json序列化().转成Json<_配方文件_属性_>(lst[0].CodeValue);
                        }
                        else
                        {
                            return (rt, msgErr, default);
                        }
                    }
                }
            }

        }

        public (bool s, string m) Save(string FileName, _配方文件_属性_ cfg)
        {
            lock (_lock)
            {

                表.Code26 cdoe = new 表.Code26
                {
                    FileName = FileName,
                    CodeValue = new Json序列化().转成String(cfg),
                };
                using (qfSqlSugar.SqlSugar_GetDB db_ = new qfSqlSugar.SqlSugar_GetDB(qfSqlSugar.SqlSugar_DB_封装._DB, _ConfigID))
                {
                    using (qfSqlSugar.SqlSugar_Table<表.Code26> _Table = new qfSqlSugar.SqlSugar_Table<表.Code26>(db_.Db))
                    {
                        bool rt = _Table.Storageable(cdoe, out int count, out string msgErr);
                        if (!rt)
                        {
                            return (rt, msgErr);
                        }
                        else if (count == 0)
                        {
                            return (false, Language_.Get语言("受影响0行"));
                        }
                        return (rt, Language_.Get语言("执行成功"));
                    }
                }
            }
        }

        public (bool s, string m) Delete(string FileName)
        {
            lock (_lock)
            {
                using (qfSqlSugar.SqlSugar_GetDB db_ = new qfSqlSugar.SqlSugar_GetDB(qfSqlSugar.SqlSugar_DB_封装._DB, _ConfigID))
                {
                    using (qfSqlSugar.SqlSugar_Table<表.Code26> _Table = new qfSqlSugar.SqlSugar_Table<表.Code26>(db_.Db))
                    {
                        bool rt = _Table.Delete(u => u.FileName == FileName, out int count, out string msgErr);
                        return (rt, msgErr);
                    }
                }
            }
        }

        public (bool s, string m) 复制(string FileName, string NewFileName)
        {
            string[] work = new string[]
            {
                "查询",
                "复制"
            };

            lock (_lock)
            {
                (bool s, string m) rt = (true, "");
                _配方文件_属性_ cfg = new _配方文件_属性_();
                foreach (var s in work)
                {
                    if (!rt.s)
                    {
                        break;
                    }
                    else if (s == "查询")
                    {
                        (bool s, string m, _配方文件_属性_ cfg) rtGet = Read(FileName);
                        rt.s = rtGet.s;
                        rt.m = rtGet.m;
                        cfg = rtGet.cfg;
                    }
                    else if (s == "复制")
                    {
                        rt = Save(NewFileName, cfg);
                    }
                }

                return rt;
            }
        }

        public (bool s, string m, string[] v) Get目录()
        {
            bool rt = true;
            string msgErr = string.Empty;
            string[] v = new string[0];
            using (var db_ = new qfSqlSugar.SqlSugar_GetDB(qfSqlSugar.SqlSugar_DB_封装._DB, _ConfigID))
            {
                using (qfSqlSugar.SqlSugar_Table<表.Code26> _Table = new qfSqlSugar.SqlSugar_Table<表.Code26>(db_.Db))
                {
                    rt = _Table.GetList(out List<表.Code26> lst, out msgErr);
                    if (rt)
                    {
                        v = lst.Select(i => i.FileName).ToArray();
                    }
                }
            }
            return (true, "", v);
        }



    }
}
