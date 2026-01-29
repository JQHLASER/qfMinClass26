
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;



namespace qfSqlSugar
{
    /* NuGet包下载说明
   Sqlite 数据库
   1>	:	使用NuGet包下载 SqlSugar.dll
   2>	:	调用Sqlite时, 使用NuGet包下载 System.Data.SQLite 就可以了
   3>	:	将SQLite.Interop.dll复制到文件夹下即可


   最后再发布到Win86或64就可以了


   注意事项, (实测OK的)
   使用mainclassqf类库, 由于里面已调用过SqlSugar及sqlite相关的库,
   只需要将SQLite.Interop.dll复制到发布好的目录下就可以了,
   不需要使用NuGet包下载了

SqlServer 数据库....使用最新库
   直接使用.net8时, 必须使用NuGet下载SqlSugarCore

   使用.net4.8开发dll给.net8调用时,
   必须使用NuGet下载 System.Data.SqlClient
                        System.Security.Permissions
   最好将 SqlSugarCore 也一起下载, 然后DB初始化时选择"新驱动"


   必须将发布编译时生成的sni.dll复制到程序目录下, 否则会提示连接字符串不正确
  */


    /*
     * 正确使用方法,支持多线程
              using (var db = RootDb.CopyNew())
             {
                 var sqliteDb = db.GetConnection("sqlite");
                 var sqlDb = db.GetConnection("sqlserver");

                 // 执行操作
             }
     * */

    /// <summary>
    /// 下载SqlSugar.dll包  
    /// </summary>
    public class SqlSugar_DB
    {
        #region 说明

        //数据库连接
        //private SqlSugarScope Db = new SqlSugarScope(new ConnectionConfig()
        //{
        //    //连接字符串
        //    ConnectionString = ConfigurationManager.ConnectionStrings["connstring"].ToString(),
        //    DbType = SqlSugar.DbType.MySql,//设置数据库类型
        //    IsAutoCloseConnection = true,//自动释放数据务，如果存在事务，在事务结束后释放
        //    InitKeyType = InitKeyType.Attribute //从实体特性中读取主键自增列信息
        //});


        #endregion

        public SqlSugarScope Db;

        /// <summary>
        /// 支持多库,非事件模式,直接传入ConnectionConfig
        /// </summary>   
        public virtual async Task<(bool s, string m)> 初始化(List<ConnectionConfig> lst, int 超时时间 = 1000 * 10)
        {
            string msgErr = string.Empty;
            if (lst is null || lst.Count == 0)
            {
                msgErr = "ConnectionConfig " + qfmain.Language_.Get语言("不能为空");
                this.Db = null;
                return (false, msgErr);
            }
            bool rt = true;

            try
            {
                this.Db = new SqlSugarScope(lst);
                this.Db.Ado.CommandTimeOut = 超时时间 > 0 ? (int)超时时间 : 1000 * 10;

                var tasks = new List<Task>();

                foreach (ConnectionConfig config in lst)
                {
                    if (config.DbType == DbType.Sqlite)
                    {
                        #region Sqlite   
                        tasks.Add(Task.Run(() =>
                        {
                            using (var dbTemp = new SqlSugarScope(config))
                            {
                                优化_Sqlite(dbTemp);
                            }
                        }));

                        #endregion
                    }
                    else if (config.DbType == DbType.SqlServer)
                    {
                        #region SqlServer
                        tasks.Add(Task.Run(() =>
                        {
                            using (var dbTemp = new SqlSugarScope(config))
                            {
                                优化_SqlServer(dbTemp);
                            }
                        }));
                        #endregion 
                    }
                    else if (config.DbType == DbType.MySql)
                    {
                        #region MySql
                        tasks.Add(Task.Run(() =>
                        {
                            using (var dbTemp = new SqlSugarScope(config))
                            {
                                优化_MySql(dbTemp);
                            }
                        }));
                        #endregion
                    }

                }

                // 等待所有数据库初始化完成
                await Task.WhenAll(tasks);
            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }


            Event_初始化结束?.Invoke(rt, this);
            Event_初始化结束1?.Invoke(rt, msgErr, this);
            return (rt, msgErr);
        }

        /// <summary>
        /// 支持多库,事件模式
        /// </summary>       
        public virtual async Task<(bool s, string m)> 初始化(int 超时时间 = 1000 * 10)
        {
            List<ConnectionConfig> lst = new List<ConnectionConfig>();
            On_Event_ConnectionConfig(lst, this);
            return await 初始化(lst, 超时时间);
        }

        /// <summary>
        /// 多库时使用
        /// <para>id 连接数据库的id</para>
        /// </summary>     
        public virtual ConnectionConfig 生成连接信息(string 连接字符串, string id, SqlSugar.DbType DbType)
        {
            return new ConnectionConfig()
            {
                //连接字符串
                ConfigId = id,//ID区分多库
                ConnectionString = 连接字符串,
                DbType = DbType,//设置数据库类型
                IsAutoCloseConnection = true,//自动释放数据务，如果存在事务，在事务结束后释放
                InitKeyType = InitKeyType.Attribute,//从实体特性中读取主键自增列信息
                MoreSettings = new ConnMoreSettings
                {
                    IsAutoRemoveDataCache = true
                }
            };



        }


        /// <summary>
        /// 获取要使用的数据库 
        /// </summary> 
        private SqlSugarProvider Get连接的数据库(SqlSugarScope _SqlSugarScope, string ID)
        {
            return _SqlSugarScope.GetConnection(ID);
        }

        /// <summary>
        /// 获取要使用的数据库 
        /// </summary> 
        private SqlSugarProvider Get连接的数据库(SqlSugarClient _SqlSugarClient, string ID)
        {
            return _SqlSugarClient.GetConnection(ID);
        }


        private readonly object _Lock = new object();
        /// <summary>
        /// model: =0:写 =1:读
        /// </summary>      
        public virtual bool 读取参数<T>(ushort model, ref T Info, string path, out string msgErr)
        {
            lock (_Lock)
            {
                return new qfmain.文件_文件夹().WriteReadJson(path, model, ref Info, out msgErr);
            }
        }


        /// <summary>
        /// 复制 Db
        /// </summary> 
        public virtual SqlSugarClient CopyNew_Db()
        {
            return this.Db.CopyNew();
        }

        #region 优化

        (bool s, string m) 优化_Sqlite(SqlSugarScope db_)
        {
            try
            {
                var Ado = db_.Ado;
                // WAL 模式：提升并发读写性能
                Ado.ExecuteCommand("PRAGMA journal_mode = WAL;");

                // 缓存大小调整（加快查询性能）
                Ado.ExecuteCommand("PRAGMA cache_size = -20000;"); // 约 20MB

                // 写安全系数降低一点提高性能（正常使用足够安全）
                Ado.ExecuteCommand("PRAGMA synchronous = NORMAL;");

                // 使用内存表加速排序、分组等操作
                Ado.ExecuteCommand("PRAGMA temp_store = MEMORY;");

                // 自动清理 WAL 文件，提高长期运行稳定性
                Ado.ExecuteCommand("PRAGMA wal_checkpoint(TRUNCATE);");

                return (true, "");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
        (bool s, string m) 优化_SqlServer(SqlSugarScope db)
        {
            try
            {
                var ado = db.Ado;

                // 更新统计信息
                ado.ExecuteCommand("EXEC sp_updatestats;");

                // 重建所有表索引（数据库小可以使用）
                // ado.ExecuteCommand("EXEC sys.sp_MSforeachtable 'ALTER INDEX ALL ON ? REBUILD';");

                return (true, "");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
        (bool s, string m) 优化_MySql(SqlSugarScope db)
        {
            try
            {
                var tables = db.DbMaintenance.GetTableInfoList();
                foreach (var t in tables)
                {
                    var table = t.Name;

                    db.Ado.ExecuteCommand($"ANALYZE TABLE `{table}`;");
                    db.Ado.ExecuteCommand($"OPTIMIZE TABLE `{table}`;");
                    // MyISAM 才需要 REPAIR，InnoDB 不需要
                }

                return (true, "");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        #endregion


        #region  生成连接字符串

        public virtual string 生成连接字符串(_cfg_SQLite_ SqlLitePath, _SQLite_连接类型_ 连接类型 = _SQLite_连接类型_.V2)
        {
            switch (连接类型)
            {
                case _SQLite_连接类型_.V2:
                    return $"data source={SqlLitePath.Path};Cache=Shared;Mode=ReadWriteCreate;";
                case _SQLite_连接类型_.V3:
                    return $"data source={SqlLitePath.Path};Version=3;Cache=Shared;Mode=ReadWriteCreate;";
                default:
                    return "";
            }

        }

        /// <summary>
        /// _SqlServer_连接类型_ 连接类型 = _SqlServer_连接类型_.新驱动
        /// </summary> 
        public virtual string 生成连接字符串(_cfg_SQLserver_ Info_SQLserver)
        {
            //switch (连接类型)
            //{
            //    case _SqlServer_连接类型_.旧驱动:
            //        return $"Server={Info_SQLserver.数据库地址};database={Info_SQLserver.数据库名称};User Id={Info_SQLserver.用户};Password={Info_SQLserver.密码};";
            //    // return $"Server={_info_SQLserver.数据库地址};database={_info_SQLserver.数据库名称};uid={_info_SQLserver.用户};pwd={_info_SQLserver.密码};Pooling=true; ";

            //    case _SqlServer_连接类型_.新驱动:
            //        return $"Server={Info_SQLserver.数据库地址};database={Info_SQLserver.数据库名称};User Id={Info_SQLserver.用户};Password={Info_SQLserver.密码};Encrypt=True;TrustServerCertificate=True;";
            //    // return $"Server={_info_SQLserver.数据库地址};database={_info_SQLserver.数据库名称};uid={_info_SQLserver.用户};pwd={_info_SQLserver.密码};Pooling=true; ";

            //    default:
            //        return "";

            //}

            //下面是要解决长期连接会失效的问题 
            StringBuilder sb = new StringBuilder();
            sb.Append($"Data Source={Info_SQLserver.数据库地址},{Info_SQLserver.端口};");
            sb.Append($"database={Info_SQLserver.数据库名称};");
            sb.Append($"User ID={Info_SQLserver.用户};");
            sb.Append($"Password={Info_SQLserver.密码};");
            sb.Append($"Integrated Security=False;");
            sb.Append($"Pooling=True;");
            sb.Append($"Max Pool Size=100;");
            sb.Append($"Min Pool Size=0;");
            sb.Append($"Connect Timeout=30;");
            sb.Append($"Encrypt=False;");
            sb.Append($"MultipleActiveResultSets=True;");


            return sb.ToString();

        }

        public virtual string 生成连接字符串(_cfg_Oracle_ Info_Oracle)
        {
            //    return $"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={_info_Oracle.数据库地址})(PORT={_info_Oracle.端口}))(CONNECT_DATA=(SERVICE_NAME={_info_Oracle.数据库名称})));Persist Security Info=True;User ID={_info_Oracle.用户};Password={_info_Oracle.密码};";

            string connectionString = $"User Id={Info_Oracle.用户};Password={Info_Oracle.密码};Data Source={Info_Oracle.数据库地址}:{Info_Oracle.端口}/{Info_Oracle.数据库名称};Pooling=True;Max Pool Size=100;Min Pool Size=5;Connection Timeout=30;Validate Connection=True;";
            return connectionString;

        }

        public virtual string 生成连接字符串(_cfg_Mysql_ _info_Mysql)
        {
            //StringBuilder sb = new StringBuilder();
            //sb.Append($"server={_info_Mysql.数据库地址};port={_info_Mysql.端口};database={_info_Mysql.数据库名称};user={_info_Mysql.用户名};password={_info_Mysql.密码};Charset = '{_info_Mysql.编码}'");//服务器地址；端口号；数据库；用户名；密码;

            ////以下解决长时间连接mysql时断开的问题 
            //sb.Append($"Pooling=true;Max Pool Size=100;Min Pool Size=5;");
            //sb.Append($"Connection Timeout=30;Default Command Timeout=30;");// 添加连接超时和生命周期设置
            //sb.Append($"Connection Lifetime=60;");// 设置生命周期为 60 秒（这个值应小于 MySQL 服务器的 wait_timeout 设置）

            //return sb.ToString ();

            StringBuilder sb = new StringBuilder();
            sb.Append($"Server={_info_Mysql.数据库地址};");
            sb.Append($"Port={_info_Mysql.端口};");
            sb.Append($"Database={_info_Mysql.数据库名称};");
            sb.Append($"User ID={_info_Mysql.用户名};");
            sb.Append($"Password={_info_Mysql.密码};");
            sb.Append($"Charset={_info_Mysql.编码};");
            sb.Append("Pooling=true;");
            sb.Append("MinimumPoolSize=0;");          // 关键
            sb.Append("MaximumPoolSize=500;");
            sb.Append("ConnectionTimeout=3;");       // 关键
            sb.Append("DefaultCommandTimeout=60;");
            sb.Append("Keepalive=300;");

            sb.Append("SslMode=None;");               // 避免SSL延迟
            sb.Append("Allow User Variables=true;");
            sb.Append("Allow Zero Datetime=true;");
            sb.Append("Convert Zero Datetime=true;");

            return sb.ToString();
        }



        #endregion

        #region 事件


        /// <summary>
        /// 连接信息跟
        /// </summary>
        public event Action<List<ConnectionConfig>, SqlSugar_DB> Event_ConnectionConfig;
        /// <summary>
        /// 此时可以获取表结构和操作数据库了
        /// </summary>
        public event Action<bool, SqlSugar_DB> Event_初始化结束;
        /// <summary>
        /// 此时可以获取表结构和操作数据库了
        /// <para>参数 (状态,消息,DB)</para>
        /// </summary>
        public event Action<bool, string, SqlSugar_DB> Event_初始化结束1;

        private void On_Event_ConnectionConfig(List<ConnectionConfig> lst, SqlSugar_DB db)
        {
            Event_ConnectionConfig?.Invoke(lst, db);
        }


        #endregion


        #region 连接的判断及处理

        /// <summary>
        /// Db中是否存在指定id
        /// </summary> 
        public bool 是否存在id(string id)
        {
            return Db.IsAnyConnection(id);

        }
        public void 加入连接(ConnectionConfig cfg)
        {
            Db.AddConnection(cfg);//删除连接

        }

        /// <summary>
        /// ID或 ConnectionConfig
        /// <para>一般用ID</para>
        /// </summary>
        /// <param name="cfg"></param>
        public void 删除(dynamic cfg)
        {
            Db.RemoveConnection(cfg);//删除连接

        }

        /// <summary>
        /// ConnectionConfig 数据库是否可以连接上
        /// <para>SqlSugarClient 方式</para>
        /// </summary> 
        public bool Is是否能连接(ConnectionConfig cfg)
        {
            try
            {
                using (var testDb = new SqlSugarClient(cfg))
                {
                    testDb.Ado.Open();  // 尝试打开连接 
                    return true;
                }
            }
            catch
            {
                return false;  // 失败就跳过
            }
        }

        /// <summary>
        /// 查询方式
        /// </summary> 
        public bool Is是否能连接(SqlSugarScope _db)
        {
            try
            {
                var result = _db.Ado.GetScalar("SELECT 1");
                return (int)result == 1;
            }
            catch
            {
                return false;  // 连接无效
            }
        }

        /// <summary>
        /// 查询方式
        /// </summary> 
        public bool Is是否能连接(SqlSugarProvider _db)
        {
            try
            {
                var result = _db.Ado.GetScalar("SELECT 1");
                return (int)result == 1;
            }
            catch
            {
                return false;  // 连接无效
            }
        }



        /// <summary>
        /// 删除id...判断id是否连接...能连就添加到Db中
        /// <para>远程连SQLserver,MySql时用此方法</para>
        /// <para>解决远程数据库连接池耗尽断开的问题</para>
        /// </summary> 
        private (bool s, string m) Is连接是否有效1(SqlSugar_DB db, string _id, ConnectionConfig cfg, bool Is先删后加_cfg = true)
        {
            if (!Err_Db为Ull(db, out string msgErr))
            {
                return (false, msgErr);
            }

            string[] work = new string[]
            {
                "先删id",
                "判断id是否存在",
                "数据库检测",
            };

            bool rtRun = true;//为false时退出
            bool rt = false;
            string msg = "";

            //第一次失败时,删除连接后,重新初始化后再来一次,
            for (global::System.Int32 i = 0; i < 2; i++)
            {
                if (!rtRun)
                {
                    break;
                }
                foreach (var item in work)
                {
                    if (item == "先删id")
                    {
                        #region MyRegion

                        if (Is先删后加_cfg)
                        {
                            this.Db.RemoveConnection(cfg);
                        }

                        #endregion
                    }
                    else if (item == "判断id是否存在")
                    {
                        #region MyRegion

                        if (!this.Db.IsAnyConnection(_id))
                        {
                            this.Db.AddConnection(cfg);
                        }

                        #endregion
                    }
                    else if (item == "数据库检测")
                    {
                        #region MyRegion

                        try
                        {
                            using (var testDb = new SqlSugarClient(cfg))
                            {
                                testDb.Ado.Open();  // 尝试打开连接 
                                                    // this.Db.AddConnection(cfg);//添加id
                                return (true, "Connection ok");
                            }
                        }
                        catch
                        {
                            this.Db.RemoveConnection(_id);//删除id
                            return (true, "Connection ng");   // 失败就跳过
                        }
                        #endregion
                    }


                }

            }

            return (rt, msg);
        }


        /// <summary>
        /// 查询方式判断,比较推荐,
        /// <para></para>
        /// </summary> 
        private (bool s, string m) Is连接是否有效2(SqlSugar_DB db, string _id, ConnectionConfig cfg, bool Is先删后加_cfg = true)
        {
            if (!Err_Db为Ull(db, out string msgErr))
            {
                return (false, msgErr);
            }

            string[] work = new string[]
            {
                "先删id",
                "判断id是否存在",
                "数据库检测",
            };

            bool rtRun = true;//为false时退出
            bool rt = false;
            string msg = "";

            //第一次失败时,删除连接后,重新初始化后再来一次,
            for (global::System.Int32 i = 0; i < 2; i++)
            {
                if (!rtRun)
                {
                    break;
                }
                foreach (var item in work)
                {
                    if (item == "先删id")
                    {
                        #region MyRegion

                        if (Is先删后加_cfg)
                        {
                            this.Db.RemoveConnection(cfg);
                        }

                        #endregion
                    }
                    else if (item == "判断id是否存在")
                    {
                        #region MyRegion

                        if (!this.Db.IsAnyConnection(_id))
                        {
                            this.Db.AddConnection(cfg);
                        }

                        #endregion
                    }
                    else if (item == "数据库检测")
                    {
                        #region MyRegion

                        using (var a0 = this.Db.CopyNew())
                        {
                            using (var b0 = a0.GetConnection(_id))
                            {
                                rt = Is是否能连接(b0);
                                if (!rt)
                                {
                                    //失败时删除连接
                                    this.Db.RemoveConnection(_id);
                                    msg = "Connection ng";
                                }
                                else
                                {
                                    msg = "Connection ok";
                                    //成功时退出
                                    rtRun = false;
                                    break;
                                }
                            }
                        }
                        #endregion
                    }
                }

            }

            return (rt, msg);
        }





        /// <summary>
        /// 两种检测方式可选
        /// <para>远程连SQLserver,MySql时用此方法</para>
        /// <para>解决远程数据库连接池耗尽断开的问题</para>
        /// <para>模式 =0:查询方式检测,=1:SqlSugarClient方式重连检测</para>
        /// </summary> 
        public (bool s, string m) Is连接是否有效(SqlSugar_DB db, string _id, ConnectionConfig cfg, bool Is先删后加_cfg = true, int 模式 = 0)
        {
            if (!Err_Db为Ull(db, out string msgErr))
            {
                return (false, msgErr);
            }
             
            (bool s, string m) rt = (false, "");

            #region 检测

            if (模式 == 0)
            {
                rt = Is连接是否有效2(db, _id, cfg, Is先删后加_cfg);

            }
            else
            {
                rt = Is连接是否有效1(db, _id, cfg, Is先删后加_cfg);
            }


            #endregion

            return rt;
        }


        bool Err_Db为Ull(SqlSugar_DB db, out string msgErr)
        {
            msgErr = "";
            if (db is null)
            {
                msgErr = "db is null";
                return false;
            }
            return true;
        }



        #endregion
    }
}
