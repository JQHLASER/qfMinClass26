
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



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
        public virtual bool 初始化(List<ConnectionConfig> lst, out string msgErr, int 超时时间 = 1000 * 10)
        {
            msgErr = string.Empty;
            if (lst is null || lst.Count == 0)
            {
                msgErr = "ConnectionConfig " + qfmain.Language_.Get语言("不能为空");
                this.Db = null;
                return false;
            }
            bool rt = true;

            try
            {
                this.Db = new SqlSugarScope(lst);
                this.Db.Ado.CommandTimeOut = 超时时间 > 0 ? (int)超时时间 : 1000 * 10;


                foreach (ConnectionConfig config in lst)
                {
                    if (config.DbType == DbType.Sqlite)
                    {
                        #region Sqlite   

                        using (var dbTemp = new SqlSugarScope(config))
                        {
                            优化_Sqlite(dbTemp);
                        }

                        #endregion
                    }
                    else if (config.DbType == DbType.SqlServer)
                    {
                        #region SqlServer

                        using (var dbTemp = new SqlSugarScope(config))
                        {
                            优化_SqlServer(dbTemp);
                        }

                        #endregion 
                    }
                    else if (config.DbType == DbType.MySql)
                    {
                        #region MySql

                        using (var dbTemp = new SqlSugarScope(config))
                        {
                            优化_MySql(dbTemp);
                        }

                        #endregion
                    }

                }
            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }

            Event_初始化结束?.Invoke(rt, this);
            return rt;
        }

        /// <summary>
        /// 支持多库,事件模式
        /// </summary>       
        public virtual bool 初始化(out string msgErr, int 超时时间 = 1000 * 10)
        {
            List<ConnectionConfig> lst = new List<ConnectionConfig>();
            On_ConnectionConfig(lst);
            bool rt = 初始化(lst, out msgErr, 超时时间);
            return rt;
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
        /// <param name="ID"></param>
        /// <returns></returns>
        public virtual SqlSugarProvider Get连接的数据库(string ID)
        {
            return this.Db.GetConnection(ID);
        }


        /// <summary>
        /// model: =0:写 =1:读
        /// </summary>      
        public virtual bool 读取参数<T>(ushort model, ref T Info, string path, out string msgErr)
        {
            return new qfmain.文件_文件夹().WriteReadJson(path, model, ref Info, out msgErr);
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

        public virtual string 生成连接字符串(_cfg_SQLite_ SqlLitePath, _SQLite_连接类型_ 连接类型 = _SQLite_连接类型_.V3)
        {
            switch (连接类型)
            {
                case _SQLite_连接类型_.V2:
                    return $"data source={SqlLitePath.Path};";
                case _SQLite_连接类型_.V3:
                    return $"data source={SqlLitePath.Path};Version=3;";
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
            sb.Append($"Pooling=true;");
            sb.Append($"MinimumPoolSize=5;");
            sb.Append($"MaximumPoolSize=100;");
            sb.Append($"ConnectionTimeout=30;");
            sb.Append($"DefaultCommandTimeout=30;");
            sb.Append($"Keepalive=60;");

            return sb.ToString();
        }



        #endregion

        #region 事件

        /// <summary>
        /// 连接信息跟
        /// </summary>
        public event Action<List<ConnectionConfig>> Event_ConnectionConfig;
        void On_ConnectionConfig(List<ConnectionConfig> lst)
        {
            Event_ConnectionConfig?.Invoke(lst);
        }

        /// <summary>
        /// 此时可以获取表结构和操作数据库了
        /// </summary>
        public event Action<bool, SqlSugar_DB> Event_初始化结束;

        #endregion


    }
}
