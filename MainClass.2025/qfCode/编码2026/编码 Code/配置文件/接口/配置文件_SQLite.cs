using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfCode
{
    public class 配置文件_SQLite : Iwork_配置文件
    {

        public 编码_ _codeSys;

        /// <summary>
        /// 班次
        /// </summary>
        string path_class = "";
        /// <summary>
        /// 日期时间
        /// </summary>
        string path_dateTimes = "";
        string _id_class = "_db_class";
        string _id_dateTimes = "_db_dateTimes";


        public 配置文件_SQLite(编码_ codeSys)
        {
            this._codeSys = codeSys;
            path_class = Path.Combine(this._codeSys._文件夹_属性.参数, "Classes.db");
            path_dateTimes = Path.Combine(this._codeSys._文件夹_属性.日期时间, "DateTimes.db");

            qfSqlSugar.SqlSugar_DB_封装._DB.Event_ConnectionConfig += (s, db) =>
            {
                var sql_class = db.生成连接字符串(new qfSqlSugar._cfg_SQLite_ { Path = path_class });
                var sql_dateTimes = db.生成连接字符串(new qfSqlSugar._cfg_SQLite_ { Path = path_dateTimes });

                var db_class = db.生成连接信息(sql_class, _id_class, SqlSugar.DbType.Sqlite);
                var db_dateTimes = db.生成连接信息(sql_dateTimes, _id_class, SqlSugar.DbType.Sqlite);

                s.Add(db_class);
                s.Add(db_dateTimes);
            };

            qfSqlSugar.SqlSugar_DB_封装._DB.Event_初始化结束1 += async (s, m, db) =>
            {
                if (s)
                {
                    await Task.Run(() =>
                    {
                        string[] work = new string[]
                        {
                            "classes",
                            "dateTimes",
                        };

                        (bool s, string msg) rt = (true, "");
                        foreach (var item in work)
                        {
                            if (!rt.s)
                            {
                                break;
                            }
                            else if (item == "classes")
                            {
                                rt = 测试_class();
                            }
                            else if (item == "dateTimes")
                            {
                                rt = 测试_dateTimes();
                            }
                        }


                        if (rt.s)
                        {
                            Get_班次(常量.配置文件名_默认);
                            Get_日期时间(常量.配置文件名_默认, "月", "1");
                            this._codeSys._初始化状态 = qfmain._初始化状态_.已初始化 ;
                            this._codeSys.On_初始化状态_配置文件模块(this._codeSys._初始化状态, rt.msg);
                        }
                        else
                        {
                            this._codeSys._初始化状态 = qfmain._初始化状态_.未初始化;
                            this._codeSys.On_初始化状态_配置文件模块(this._codeSys._初始化状态, rt.msg);
                        }


                    });
                }
            };

             
        }



        /// <summary>
        /// 文件路径,数据库不需要
        /// </summary> 
        public string GetPath_班次(string FileName)
        {
            return "";
        }

        /// <summary>
        /// 文件路径,数据库不需要
        /// </summary> 
        public string GetPath_日期时间(string FileName)
        {
            return "";
        }

        public (bool s,string msg, string[] cfg) Get目录_班次()
        {
            using (var db = new qfSqlSugar.SqlSugar_GetDB(qfSqlSugar.SqlSugar_DB_封装._DB, _id_class))
            {
                using (var table = new qfSqlSugar.SqlSugar_Table<表.Code26>(db.Db))
                {
                    var rt = table.GetList(out List<表.Code26> lstSql, out string msgErr);
                    List<string> lst = lstSql.Select(u => u.FileName).ToList();

                    return (rt,msgErr  , lst.ToArray());
                }
            }

        }
        public (bool s, string msg, string[] cfg) Get目录_日期时间()
        {

            using (var db = new qfSqlSugar.SqlSugar_GetDB(qfSqlSugar.SqlSugar_DB_封装._DB, _id_dateTimes))
            {
                using (var table = new qfSqlSugar.SqlSugar_Table<表.Code26>(db.Db))
                {
                    var rt = table.GetList(out List<表.Code26> lstSql, out string msgErr);
                    List<string> lst = lstSql.Select(u => u.FileName).ToList();
                    return (rt, msgErr, lst.ToArray());
                }
            }
        }




        /// <summary>
        /// 获取班次
        /// </summary> 
        public (bool s, string m, _班次_[] cfg) Get_班次(string FileName)
        {
            bool rt = true;
            string msgErr = "";

            string[] work = new string[]
            {
                "查询",
                "无数据时写入默认数据",
                "查询",
            };
            using (var db = new qfSqlSugar.SqlSugar_GetDB(qfSqlSugar.SqlSugar_DB_封装._DB, _id_class))
            {
                using (var table = new qfSqlSugar.SqlSugar_Table<表.Code26>(db.Db))
                {
                    _班次_[] Beff = 配置文件_初始数据.班次();
                    List<表.Code26> lstSql = new List<表.Code26>();
                    foreach (var s in work)
                    {
                        if (!rt)
                        {
                            break;
                        }
                        else if (s == "查询")
                        {
                            rt = table.GetList(u => u.FileName == FileName, out lstSql, out msgErr);
                        }
                        else if (s == "无数据时写入默认数据")
                        {
                            #region 无数据时写入默认数据

                            if (lstSql.Count == 0)
                            {
                                string vxt = JsonConvert.SerializeObject(lstSql);
                                var sql = new 表.Code26
                                {
                                    FileName = 常量.配置文件名_默认,
                                    CodeValue = vxt,
                                };
                                rt = table.Storageable(sql, out int count, out msgErr);
                            }

                            #endregion
                        }
                    }


                    if (rt && lstSql.Count > 0)
                    {
                        //转成班次信息
                        Beff = JsonConvert.DeserializeObject<_班次_[]>(lstSql[0].CodeValue);
                    }
                    return (rt, msgErr, Beff);
                }
            }

        }

        /// <summary>
        /// 获取日期时间,
        /// <para>section : 节名称,如年4等</para>
        /// <para>keys : 字段,如2022=22 等</para>
        /// </summary> 
        public (bool s, string m, string cfg) Get_日期时间(string FileName, string section, string keys)
        {

            bool rt = true;
            string msgErr = "";

            string[] work = new string[]
            {
                "查询",
                "无数据时写入默认数据",
                "查询",
            };

            using (var db = new qfSqlSugar.SqlSugar_GetDB(qfSqlSugar.SqlSugar_DB_封装._DB, _id_dateTimes))
            {
                using (var table = new qfSqlSugar.SqlSugar_Table<表.Code26>(db.Db))
                {

                    List<表.Code26> lstSql = new List<表.Code26>();
                    foreach (var s in work)
                    {
                        if (!rt)
                        {
                            break;
                        }
                        else if (s == "查询")
                        {
                            rt = table.GetList(u => u.FileName == FileName, out lstSql, out msgErr);
                        }
                        else if (s == "无数据时写入默认数据")
                        {
                            #region 无数据时写入默认数据

                            if (lstSql.Count == 0)
                            {
                                var sql = new 表.Code26
                                {
                                    FileName = 常量.配置文件名_默认,
                                    CodeValue = 配置文件_初始数据.日期时间(),
                                };
                                rt = table.Storageable(sql, out int count, out msgErr);
                            }

                            #endregion
                        }
                    }

                    string keys_ = keys;
                    if (rt && lstSql.Count > 0)
                    {
                        keys_ = new qfmain.ini_sharpconfig_ReadString(lstSql[0].CodeValue).Read(section, keys, keys);
                    }
                    return (rt, msgErr, keys_);
                }
            }

        }

        private (bool s, string m) 测试_class()
        {
            using (var db = new qfSqlSugar.SqlSugar_GetDB(qfSqlSugar.SqlSugar_DB_封装._DB, _id_class))
            {
                using (var table = new qfSqlSugar.SqlSugar_Table<表.Code26>(db.Db))
                {
                    var rt = table.Get总行数("Code26", out long count, out string msgErr);
                    return (rt, msgErr);
                }
            }
        }
        private (bool s, string m) 测试_dateTimes()
        {
            using (var db = new qfSqlSugar.SqlSugar_GetDB(qfSqlSugar.SqlSugar_DB_封装._DB, _id_dateTimes))
            {
                using (var table = new qfSqlSugar.SqlSugar_Table<表.Code26>(db.Db))
                {
                    var rt = table.Get总行数("Code26", out long count, out string msgErr);
                    return (rt, msgErr);
                }
            }
        }




    }
}
