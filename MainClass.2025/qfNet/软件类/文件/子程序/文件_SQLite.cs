using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace qfNet
{
    internal class 文件_SQLite<T> : Iwork_文件_<T>
    {
        string _File = Path.Combine(qfmain.软件类.Files_Cfg.Files_LogMyApp, "GJS");
        string _文件类型 = "FLS";

        string _path = "";
        string _ConfigID = "_file26_sqlite_";
        private static readonly object _lock = new object();

        public qfmain._初始化状态_ _初始化状态 { set; get; } = qfmain._初始化状态_.未初始化;

        /// <summary>
        /// File : 存放Code26.db的文件夹
        /// </summary> 
        public void 初始化(string File, string 文件类型 = "FLS", string 后缀 = "")
        {

            if (!string.IsNullOrWhiteSpace(File)) this._File = File;
            this._文件类型 = 文件类型;
            new qfmain.文件_文件夹().文件夹_新建(this._File, out string msgErr);

            this._path = Path.Combine(this._File, "Code26.db");

            On_初始化状态(qfmain._初始化状态_.初始化中, "");
            qfSqlSugar.SqlSugar_DB_封装._DB.Event_ConnectionConfig += (s, e) =>
            {
                #region 连接数据库
                string conStr = e.生成连接字符串(new qfSqlSugar._cfg_SQLite_
                {
                    Path = this._path,
                });
                var connent = e.生成连接信息(conStr, this._ConfigID, SqlSugar.DbType.Sqlite);
                s.Add(connent);

                #endregion
            };
            qfSqlSugar.SqlSugar_DB_封装._DB.Event_初始化结束1 += async (s, m, e) =>
            {
                if (s)
                {
                    (bool s, string m, T cfg) rt = Read("text^%&");
                    this._初始化状态 = rt.s ? qfmain._初始化状态_.已初始化 : qfmain._初始化状态_.未初始化;
                    this.On_初始化状态(this._初始化状态, rt.m);
                }
                else
                {
                    this.On_初始化状态(qfmain._初始化状态_.未初始化, m);
                }
            };
        }



        public string 获取文件路径(string FileName)
        {
            return "";
        }
        public bool 读写(string FileName, ushort model, ref T t, out string msgerr)
        {
            msgerr = "此功能无效";
            return true;
        }


        public bool 文件是否存在(string FileName)
        {
            using (qfSqlSugar.SqlSugar_GetDB db_ = new qfSqlSugar.SqlSugar_GetDB(qfSqlSugar.SqlSugar_DB_封装._DB, _ConfigID))
            {
                using (qfSqlSugar.SqlSugar_Table<表.Code26> _Table = new qfSqlSugar.SqlSugar_Table<表.Code26>(db_.Db))
                {
                    bool rt = _Table.GetList(u => u.FileName == FileName, out List<表.Code26> lst, out string msgErr);
                    if (!rt || lst.Count == 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
        }

        /// <summary>
        /// <para> 返回 DialogResult.Yes ,成功</para>
        /// <para> 返回 DialogResult.No ,失败</para>
        /// <para> 返回 其它,None</para>
        /// </summary> 
        public DialogResult 打开_弹窗(ref T t, out string FileName, out string msgerr, Func<string, (bool s, string m)> Event_删除文件 = null)
        {
            msgerr = string.Empty;
            FileName = string.Empty;
            DialogResult dlt = DialogResult.None;
            var rt = Get目录();
            msgerr = rt.m;
            if (rt.s)
            {
                var rtDlt = new qfNet.软件类().Win_文件类弹窗(rt.v, this._文件类型, "", _文件弹窗类型_.打开, Event_删除文件);
                dlt = rtDlt.s;
                if (dlt == DialogResult.OK)
                {
                    var rtRead = Read(rtDlt.文件名);
                    t = rtRead.cfg;
                    msgerr = rtRead.m;
                    dlt = rtRead.s ? DialogResult.Yes : DialogResult.No;
                }
            }

            return dlt;

        }

        /// <summary>
        /// <para> 返回 DialogResult.Yes ,成功</para>
        /// <para> 返回 DialogResult.No ,失败</para>
        /// <para> 返回 其它,None</para>
        /// <para>FileName:源文件名称,为空时为弹窗保存</para>
        /// </summary> 
        public DialogResult 另存为_弹窗(string FileName, T t, out string NewFileName, out string msgerr, Func<string, (bool s, string m)> Event_删除文件 = null)
        {
            msgerr = string.Empty;
            NewFileName = string.Empty;
            DialogResult dlt = DialogResult.None;
            var rts = Get目录();
            msgerr = rts.m;
            if (rts.s)
            {
                var rtDlt = new qfNet.软件类().Win_文件类弹窗(rts.v, this._文件类型, "", _文件弹窗类型_.保存, Event_删除文件);
                dlt = rtDlt.s;
                bool rt = false;
                if (dlt == DialogResult.OK)
                {
                    rt = true;
                    if (!string.IsNullOrEmpty(FileName))
                    {
                        rt = 另存为(FileName, NewFileName, out msgerr);
                    }
                    else
                    {
                        rt = 保存(NewFileName, t, out msgerr);
                    }

                    dlt = rt ? DialogResult.Yes : DialogResult.No;
                }
            }
            return dlt;
        }

        /// <summary>
        /// <para> 返回 DialogResult.Yes ,成功</para>
        /// <para> 返回 DialogResult.No ,失败</para>
        /// <para> 返回 其它,None</para>
        /// 文件名不为空时直接保存
        /// <para>文件名为空时弹窗另存为</para>
        /// </summary> 
        public DialogResult 保存_弹窗(string FileName, T t, out string NewFileName, out string msgerr, Func<string, (bool s, string m)> Event_删除文件 = null)
        {
            DialogResult dr = DialogResult.None;
            if (string.IsNullOrEmpty(FileName))
            {
                dr = 另存为_弹窗(FileName, t, out NewFileName, out msgerr, Event_删除文件);
            }
            else
            {
                NewFileName = FileName;
                bool rt = 保存(FileName, t, out msgerr);
                dr = rt ? DialogResult.Yes : DialogResult.No;
            }

            return dr;
        }
        public bool 打开(string FileName, ref T t, out string msgerr)
        {
            var v1 = Read(FileName);
            msgerr = v1.m;
            t = v1.cfg;
            return v1.s;
        }
        public bool 保存(string FileName, T t, out string msgerr)
        {
            var v2 = Save(FileName, t);
            msgerr = v2.m;
            return v2.s;
        }
        /// <summary>
        /// <para>FileName:源文件名称</para>
        /// <para>NewFileName:新文件名称</para>
        /// </summary> 
        public bool 另存为(string FileName, string NewFileName, out string msgErr)
        {
            string[] work = new string[]
            {
                "查询",
                "保存",
            };

            bool rt = true;
            msgErr = string.Empty;
            T t1 = qfmain.T_实例化泛型.FastNew<T>.Create();
            foreach (var s in work)
            {
                if (!rt)
                {
                    break;
                }
                else if (s == "查询")
                {
                    var v1 = Read(FileName);
                    rt = v1.s;
                    msgErr = v1.m;
                    t1 = v1.cfg;
                }
                else if (s == "保存")
                {
                    var v2 = Save(NewFileName, t1);
                    rt = v2.s;
                    msgErr = v2.m;

                }
            }

            return rt;

        }

        public bool 删除(string FileName, out string msgErr)
        {
            var rt = Delete(FileName);
            msgErr = rt.m;
            return rt.s;
        }

        /// <summary>
        /// <para> 返回 DialogResult.Yes ,成功</para>
        /// <para> 返回 DialogResult.No ,失败</para>
        /// <para> 返回 其它,None</para>
        /// </summary> 
        public DialogResult 弹窗(out string NewFileName, out string msgerr, _文件弹窗类型_ 类型 = _文件弹窗类型_.打开, Func<string, (bool s, string m)> Event_删除文件 = null)
        {
            msgerr = string.Empty;
            NewFileName = string.Empty;
            DialogResult dlt = DialogResult.None;
            var rts = Get目录();
            msgerr = rts.m;
            if (rts.s)
            {
                var rtDlt = new qfNet.软件类().Win_文件类弹窗(rts.v, this._文件类型, "", _文件弹窗类型_.保存, Event_删除文件);
                dlt = rtDlt.s;

                if (dlt == DialogResult.OK)
                {
                    NewFileName = rtDlt.文件名;
                    dlt = dlt == DialogResult.OK ? DialogResult.Yes : DialogResult.None;
                }
            }
            return dlt;
        }




        #region 本地方法

        public (bool s, string m, T cfg) Read(string FileName)
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
                            return new qfmain.Json_().反序列化<T>(lst[0].CodeValue);
                        }
                        else
                        {
                            return (rt, msgErr, default);
                        }
                    }
                }
            }

        }

        public (bool s, string m) Save(string FileName, T cfg)
        {
            lock (_lock)
            {

                表.Code26 cdoe = new 表.Code26
                {
                    FileName = FileName,
                    CodeValue = new qfmain.Json_().序列化<T>(cfg).v,
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
                T cfg = qfmain.T_实例化泛型.FastNew<T>.Create();
                foreach (var s in work)
                {
                    if (!rt.s)
                    {
                        break;
                    }
                    else if (s == "查询")
                    {
                        (bool s, string m, T cfg) rtGet = Read(FileName);
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

        private (bool s, string m, string[] v) Get目录()
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


        #endregion


        #region 事件

        public event Action<qfmain._初始化状态_, string> Event_初始化状态;
        private void On_初始化状态(qfmain._初始化状态_ state, string msgErr)
        {
            Event_初始化状态?.Invoke(state, msgErr);
        }

        #endregion


    }
}
