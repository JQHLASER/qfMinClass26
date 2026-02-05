using SqlSugar;
using Sunny.UI.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using static System.Windows.Forms.AxHost;

namespace qfCode
{
    /// <summary>
    /// 一物一码常用封装
    /// </summary>
    public class 防重_封装_
    {
        /// <summary>
        /// 防重方法
        /// </summary>
        public qfCode.防重_<表_防重_.FC26> FCcode_sys = new qfCode.防重_<表_防重_.FC26>();
        bool _Inistiall = false;

        public void 初始化(_Type防重_._em_数据库格式_ 数据库格式 = _Type防重_._em_数据库格式_.SQLite)
        {
            qfSqlSugar.SqlSugar_DB_封装._DB.Event_初始化结束1 += async (s, m, db) =>
            {
                if (s)
                {
                    await Task.Run(() =>
                     {
                         var rt = 查询_防重("&^^123");
                         qfmain._初始化状态_ state = rt.s ? qfmain._初始化状态_.已初始化 : qfmain._初始化状态_.未初始化;
                         this.FCcode_sys.On_初始化状态(state, rt.m);
                     });
                }
                else
                {
                    this.FCcode_sys.On_初始化状态(qfmain._初始化状态_.未初始化, m);
                }
            };

            FCcode_sys.初始化(数据库格式);
        }

        public void 释放()
        {
            if (!_Inistiall)
            {
                return;
            }
        }


        public (bool s, string m, List<表_防重_.FC26> lst) 查询_防重(string 内容)
        {
            using (var getDb = new qfSqlSugar.SqlSugar_GetDB(this.FCcode_sys._id))
            {
                using (var table = new qfSqlSugar.SqlSugar_Table<表_防重_.FC26>(getDb.Db))
                {
                    bool rt = table.GetList(u => u.内容 == 内容, out List<表_防重_.FC26> lst, out string msgErr);
                    return (rt, msgErr, lst);
                }
            }
        }

        public (bool s, string m, List<表_防重_.FC26> lst) 查询_防重(List<string> lst内容)
        {
            if (lst内容 is null || lst内容.Count == 0)
            {
                return (false, Language_.Get语言("无查询条件"), new List<表_防重_.FC26>());
            }
            using (var getDb = new qfSqlSugar.SqlSugar_GetDB(this.FCcode_sys._id))
            {
                using (var table = new qfSqlSugar.SqlSugar_Table<表_防重_.FC26>(getDb.Db))
                {

                    bool rt = table.GetList(u => lst内容.Contains(u.内容), out List<表_防重_.FC26> lst, out string msgErr);
                    return (rt, msgErr, lst);
                }
            }
        }

        /// <summary>
        /// Count:受影响行
        /// </summary> 
        public (bool s, string m, int count) 添加(List<表_防重_.FC26> cfg)
        {
            using (var getDb = new qfSqlSugar.SqlSugar_GetDB(this.FCcode_sys._id))
            {
                using (var table = new qfSqlSugar.SqlSugar_Table<表_防重_.FC26>(getDb.Db))
                {
                    var rt = table.Storageable(cfg, out int Count, out string msgErr);
                    return (rt, msgErr, Count);
                }
            }
        }

        /// <summary>
        /// Count:受影响行
        /// </summary> 
        public (bool s, string m, int count) 添加(表_防重_.FC26 cfg)
        {
            using (var getDb = new qfSqlSugar.SqlSugar_GetDB(this.FCcode_sys._id))
            {
                using (var table = new qfSqlSugar.SqlSugar_Table<表_防重_.FC26>(getDb.Db))
                {
                    var rt = table.Storageable(cfg, out int Count, out string msgErr);
                    return (rt, msgErr, Count);
                }
            }
        }

        /// <summary>
        /// 查询窗体用
        /// </summary> 
        public (bool s, string m, List<表_防重_.FC26> lst) 查询(_Type防重_._查询信息_ cfg)
        {
            using (var getDb = new qfSqlSugar.SqlSugar_GetDB(this.FCcode_sys._id))
            {
                using (var table = new qfSqlSugar.SqlSugar_Table<表_防重_.FC26>(getDb.Db))
                {
                    string start = cfg.start.ToString("yyyy-MM-dd HH:mm:ss");
                    string end = cfg.end.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss");

                    StringBuilder sb = new StringBuilder($"select * from FC26 where 1=1 ");
                    var pars = new List<SugarParameter>();


                    if (!cfg.Is模糊查询 && !string.IsNullOrWhiteSpace(cfg.内容))
                    {
                        sb.Append($" and  内容=@内容");
                        pars.Add(new SugarParameter("@内容", cfg.内容));
                    }
                    else if (cfg.Is模糊查询 && !string.IsNullOrWhiteSpace(cfg.内容))
                    {
                        sb.Append($" and  内容 Like @内容");
                        pars.Add(new SugarParameter("@内容", $"%{cfg.内容}%"));
                    }
                     
                    if (cfg.Is模糊查询 || string.IsNullOrWhiteSpace(cfg.内容))
                    {
                        sb.Append($" and  时间>=@start");
                        pars.Add(new SugarParameter("@start", start));

                        sb.Append($" and  时间<=@end");
                        pars.Add(new SugarParameter("@end", end));

                    }
                    bool rt = table.GetList(sb.ToString(), pars, out List<表_防重_.FC26> lst, out string msgErr);
                    return (rt, msgErr, lst);
                }
            }




        }


    }
}
