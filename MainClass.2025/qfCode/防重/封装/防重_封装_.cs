using SqlSugar;
using Sunny.UI;
using Sunny.UI.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Forms;
using static qfCode.表_防重_;
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
        public _Type_防重_._cfg_防重参数_ _参数 = new _Type_防重_._cfg_防重参数_();

        bool _IsRun = true;
        qfSqlSugar.SqlSugar_DB _db;
        public void 初始化(_Type防重_._em_数据库格式_ 数据库格式 = _Type防重_._em_数据库格式_.SQLite)
        {
            读写参数(1);

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


            new Thread(() =>
            {
                do
                {
                    Thread.Sleep(1000);
                    if (FCcode_sys._初始化状态 != qfmain._初始化状态_.已初始化)
                    {
                        continue;
                    }
                    删除_清除过期数据(this._参数.数据保存时间);
                    Thread.Sleep(1000 * 60 * 60);
                }
                while (_IsRun);
            })
            { IsBackground = true }.Start();

        }

        public void 释放()
        {
            _IsRun = false;
            if (!_Inistiall)
            {
                return;
            }
        }

        public void 读写参数(ushort Model)
        {
            _Type_防重_._cfg_防重参数_ cfg = _参数.Clone();
            string path = Path.Combine(FCcode_sys._SQLiteFC, "sysFC.dll");
            new qfmain.文件_文件夹().WriteReadJson(path, Model, ref cfg, out string msgErr);
            _参数 = cfg.Clone();
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

        public (bool s, string m) 查询_防重(List<string> lst内容)
        {
            if (lst内容 is null || lst内容.Count == 0)
            {
                return (false, Language_.Get语言("无查询条件"));
            }
            using (var getDb = new qfSqlSugar.SqlSugar_GetDB(this.FCcode_sys._id))
            {
                using (var table = new qfSqlSugar.SqlSugar_Table<表_防重_.FC26>(getDb.Db))
                {
                    lst内容 = lst内容
                              .Where(s => !string.IsNullOrWhiteSpace(s))
                              .Distinct()
                              .ToList();

                    bool exist = table.Db.Queryable<FC26>()
                     .In(u => u.内容, lst内容)
                     .Any();
                    string msg = !exist ? Language_.Get语言("检测到无重码") : Language_.Get语言("检测到有重码");
                    return (!exist, msg);
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
        public (bool s, string m, int count) 修改(List<表_防重_.FC26> cfg)
        {
            using (var getDb = new qfSqlSugar.SqlSugar_GetDB(this.FCcode_sys._id))
            {
                using (var table = new qfSqlSugar.SqlSugar_Table<表_防重_.FC26>(getDb.Db))
                {
                    var rt = table.Update(cfg, out int Count, out string msgErr);
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
                    var rt = table.Insertable(cfg, out int Count, out string msgErr);
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
                        DateTime start = cfg.start;
                        DateTime end = cfg.end.AddDays(1);

                        sb.Append($" and  时间>=@start");
                        pars.Add(new SugarParameter("@start", start.Date));

                        sb.Append($" and  时间<=@end");
                        pars.Add(new SugarParameter("@end", end.Date));

                    }
                    bool rt = table.GetList(sb.ToString(), pars.ToArray(), out List<表_防重_.FC26> lst, out string msgErr);
                    return (rt, msgErr, lst);
                }
            }

        }

        /// <summary>
        /// 删除指定日期之前的数据
        /// </summary> 
        public (bool s, string m) 删除_清除过期数据(DateTime now)
        {
            using (var getDb = new qfSqlSugar.SqlSugar_GetDB(this.FCcode_sys._id))
            {
                using (var table = new qfSqlSugar.SqlSugar_Table<表_防重_.FC26>(getDb.Db))
                {
                    StringBuilder sb = new StringBuilder($"delete  from FC26 where 1=1 ");
                    var pars = new List<SugarParameter>();
                    sb.Append($" and  时间 <= @now");
                    pars.Add(new SugarParameter("@now", now.Date));

                    bool exist = table.Delete(sb.ToString(), pars.ToArray(), out int count, out string msgErr);
                    string msg = exist ? "OK" : $"NG,{msgErr}";
                    return (!exist, msg);
                }
            }
        }

        public (bool s, string m) 删除_清除过期数据(int 保存天数)
        {
            if (保存天数 == 0)
            {
                return (true, Language_.Get语言("未使能"));
            }

            DateTime nowDate = new qfmain.日期时间_().增减时间(DateTime.Now, 2, 保存天数 * -1);
            return this.删除_清除过期数据(nowDate);
        }


        public (bool s, string m, int count) 删除(List<表_防重_.FC26> cfg)
        {
            using (var getDb = new qfSqlSugar.SqlSugar_GetDB(this.FCcode_sys._id))
            {
                using (var table = new qfSqlSugar.SqlSugar_Table<表_防重_.FC26>(getDb.Db))
                {
                    bool exist = table.Delete(cfg, out int count, out string msgErr);
                    return (exist, msgErr, count);
                }
            }
        }


        public void Win_查询(_Type_防重_._功能_查询窗体_ 功能_)
        {
            using (防重_查询窗体 forms = new 防重_查询窗体())
            {
                forms.初始化(this, 功能_);
                Event_查询窗体?.Invoke(forms);
                forms.窗体();

            }

        }

        public event Action<防重_查询窗体> Event_查询窗体;





    }
}
