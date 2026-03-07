
using SqlSugar.Extensions;
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
    public class 配方文件_数据库_ : Iwork_文件
    {
        _功能_结构_._em_配方文件类型_ _文件格式 = _功能_结构_._em_配方文件类型_.Sqlite;
        编码_ _CodeSys;
        string _ConfigID = Guid.NewGuid().ToString("N");
        private static readonly object _lock = new object();

        public 配方文件_数据库_(编码_ CodeSys, _功能_结构_._em_配方文件类型_ 文件格式_)
        {
            this._CodeSys = CodeSys;
            this._CodeSys.On_初始化状态(qfmain._初始化状态_.初始化中, "");

            qfSqlSugar.SqlSugar_DB_封装.Event_ConnectionConfig += (s, db) =>
            {
                SqlSugar.ConnectionConfig config = null;
                switch (this._文件格式)
                {
                    case _功能_结构_._em_配方文件类型_.Sqlite:
                        #region SQLite

                        string _path = Path.Combine(this._CodeSys._文件夹_属性.参数, "Code26.db");
                        config = db.生成连接信息(
                                db.生成连接字符串(new qfSqlSugar._cfg_SQLite_
                                {
                                    Path = _path,
                                })
                                , this._ConfigID, SqlSugar.DbType.Sqlite);

                        #endregion
                        break;
                    case _功能_结构_._em_配方文件类型_.SqlServer:
                        #region SQLserver
                        string _pathSqlserver = Path.Combine(this._CodeSys._文件夹_属性.参数, "SqlServer_Code26.txt");
                        qfSqlSugar._cfg_SQLserver_ cfgSqlserver = new qfSqlSugar._cfg_SQLserver_
                        {
                            数据库地址 = "127.0.0.1",
                            数据库名称 = "Code26",
                            用户 = "sa",
                            密码 = "QF8888",
                        };
                        db.读取参数<qfSqlSugar._cfg_SQLserver_>(1, ref cfgSqlserver, _pathSqlserver, out string msgErr);
                        config = db.生成连接信息(
                                  db.生成连接字符串(cfgSqlserver)
                                 , this._ConfigID, SqlSugar.DbType.SqlServer);
                        #endregion
                        break;
                }
                s.Add(config);

            };
            qfSqlSugar.SqlSugar_DB_封装.Event_初始化结束 += async (s, m, db) =>
                 {
                     if (s)
                     {
                         await Task.Run(() =>
                         {
                             (bool s, string m, _配方文件_属性_ cfg) rt = Read("text^%&");
                             this._CodeSys._初始化状态 = rt.s ? qfmain._初始化状态_.已初始化 : qfmain._初始化状态_.未初始化;
                             this._CodeSys.On_初始化状态(this._CodeSys._初始化状态, rt.m);
                         });
                     }
                     else
                     {
                         this._CodeSys.On_初始化状态(qfmain._初始化状态_.未初始化, m);
                     }
                 };
        }

        public (bool s, string m, _配方文件_属性_ cfg) Read(string FileName)
        {
            lock (_lock)
            {

                using (qfSqlSugar.SqlSugar_GetDB db_ = new qfSqlSugar.SqlSugar_GetDB(qfSqlSugar.SqlSugar_DB_封装._DB, _ConfigID))
                {
                    using (qfSqlSugar.SqlSugar_Table<qfNet.表.Code26> _Table = new qfSqlSugar.SqlSugar_Table<qfNet.表.Code26>(db_.Db))
                    {
                        bool rt = _Table.GetList(u => u.FileName == FileName, out List<qfNet.表.Code26> lst, out string msgErr);
                        if (rt && lst.Count == 0)
                        {
                            return (rt, Language_.Get语言("未找到文件"), new _配方文件_属性_());
                        }
                        else if (rt && lst.Count > 0)
                        {
                            return new Json序列化().转成Json<_配方文件_属性_>(lst[0].CodeValue);
                        }
                        else
                        {
                            return (rt, msgErr, new _配方文件_属性_());
                        }
                    }
                }
            }

        }

        public (bool s, string m) Save(string FileName, _配方文件_属性_ cfg)
        {
            lock (_lock)
            {

                qfNet.表.Code26 cdoe = new qfNet.表.Code26
                {
                    FileName = FileName,
                    CodeValue = new Json序列化().转成String(cfg),
                };
                using (qfSqlSugar.SqlSugar_GetDB db_ = new qfSqlSugar.SqlSugar_GetDB(qfSqlSugar.SqlSugar_DB_封装._DB, _ConfigID))
                {
                    using (qfSqlSugar.SqlSugar_Table<qfNet.表.Code26> _Table = new qfSqlSugar.SqlSugar_Table<qfNet.表.Code26>(db_.Db))
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

        /// <summary>
        /// 导出全部时用
        /// </summary> 
        public (bool s, string m, qfNet.表.Code26[] cfg) ReadAll( )
        {
            lock (_lock)
            {

                using (qfSqlSugar.SqlSugar_GetDB db_ = new qfSqlSugar.SqlSugar_GetDB(qfSqlSugar.SqlSugar_DB_封装._DB, _ConfigID))
                {
                    using (qfSqlSugar.SqlSugar_Table<qfNet.表.Code26> _Table = new qfSqlSugar.SqlSugar_Table<qfNet.表.Code26>(db_.Db))
                    {
                        bool rt = _Table.GetList(  out List<qfNet.表.Code26> lst, out string msgErr);
                        if (rt && lst.Count == 0)
                        {
                            return (rt, Language_.Get语言("未找到文件"), new qfNet.表.Code26[0]);
                        }
                        else if (rt && lst.Count > 0)
                        {
                            return (rt, msgErr, lst.ToArray());
                        }
                        else
                        {
                            return (rt, msgErr, new qfNet.表.Code26[0]);
                        }
                    }
                }
            }

        }

        /// <summary>
        /// 导入全部时用
        /// </summary> 
        public (bool s, string m) SaveAll(  qfNet.表.Code26[] cfg)
        {
            lock (_lock)
            {
                using (qfSqlSugar.SqlSugar_GetDB db_ = new qfSqlSugar.SqlSugar_GetDB(qfSqlSugar.SqlSugar_DB_封装._DB, _ConfigID))
                {
                    using (qfSqlSugar.SqlSugar_Table<qfNet.表.Code26> _Table = new qfSqlSugar.SqlSugar_Table<qfNet.表.Code26>(db_.Db))
                    {
                        bool rt = _Table.Storageable(cfg.ToList (), out int count, out string msgErr);
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
                    using (qfSqlSugar.SqlSugar_Table<qfNet.表.Code26> _Table = new qfSqlSugar.SqlSugar_Table<qfNet.表.Code26>(db_.Db))
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
                using (qfSqlSugar.SqlSugar_Table<qfNet.表.Code26> _Table = new qfSqlSugar.SqlSugar_Table<qfNet.表.Code26>(db_.Db))
                {
                    rt = _Table.GetList(out List<qfNet.表.Code26> lst, out msgErr);
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
