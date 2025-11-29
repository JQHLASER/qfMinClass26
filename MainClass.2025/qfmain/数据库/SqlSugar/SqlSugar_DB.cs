using Newtonsoft.Json;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using static qfmain.SqlSugar_DB;

namespace qfmain
{
    /// <summary>
    ///  安装 SqlSugar
    /// </summary>
    public class SqlSugar_DB : IDisposable
    {
        public void Dispose()
        {
            Db.Close();
            Db.Dispose();
        }


        public enum enum数据库类型
        {
            MySql = 0,
            SqlServer = 1,
            Sqlite = 2,
            Oracle = 3,
            PostgreSQL = 4,
            Dm = 5,
            Kdbndp = 6,
            Oscar = 7,
            MySqlConnector = 8,
            Access = 9,
            OpenGauss = 10,
            QuestDB = 11,
            HG = 12,
            ClickHouse = 13,
            GBase = 14,
            Odbc = 15,
            Custom = 900,
        }

        /// <summary>
        /// SQLserver连接参数
        /// </summary>
        public class _info_SQLserver_
        {


            /// <summary>
            /// 数据库地址,本地可为: 127.0.0.1
            /// </summary>
            public string 数据库地址 { set; get; } = "127.0.0.1";

            /// <summary>
            /// 数据库名称
            /// </summary>
            public string 数据库名称 { set; get; } = "database";

            /// <summary>
            /// 用户名
            /// </summary>
            public string 用户 { set; get; } = "sa";

            /// <summary>
            /// 密码
            /// </summary>
            public string 密码 { set; get; } = "QIFENG8888";



        }

        /// <summary>
        /// Oracle连接参数
        /// </summary>
        public class _info_Oracle_
        {

            /// <summary>
            /// 数据库地址,本地可为: 127.0.0.1
            /// </summary>
            public string 数据库地址 { set; get; } = "127.0.0.1";

            /// <summary>
            /// 数据库名称
            /// </summary>
            public string 数据库名称 { set; get; } = "database";

            /// <summary>
            /// 用户名
            /// </summary>
            public string 用户 { set; get; } = "sa";

            /// <summary>
            /// 密码
            /// </summary>
            public string 密码 { set; get; } = "QIFENG8888";

            /// <summary>
            /// 默认值: 1521
            /// </summary>
            public int 端口 { set; get; } = 1521;
        }





        /// <summary>
        ///  Mysql连接参数
        /// </summary>
        public class _info_Mysql_
        {


            /// <summary>
            /// 服务器地址
            /// </summary>
            public string 数据库地址 { set; get; } = "127.0.0.1";


            /// <summary>
            /// 数据库端口号
            /// </summary>
            public int 端口 { set; get; } = 3306;


            /// <summary>
            /// 数据库名称
            /// </summary>
            public string 数据库名称 { set; get; } = "database";

            /// <summary>
            /// 登陆用户名
            /// </summary>
            public string 用户名 { set; get; } = "root";

            /// <summary>
            /// 登陆密码
            /// </summary>
            public string 密码 { set; get; } = "abc";

            /// <summary>
            /// 编码,默认 utf8
            /// </summary>
            public string 编码 { set; get; } = "utf8";
        }

        /// <summary>
        /// SQLite连接参数
        /// </summary>
        public class _info_SQLite_
        {
            /// <summary>
            /// sqlite路径
            /// </summary>
            public string Path { set; get; }
        }

        public enum _SQLite连接类型_
        {
            /// <summary>
            /// 不含  Version = 3;
            /// </summary>
            V2,
            /// <summary>
            /// 含Version = 3;
            /// </summary>
            V3,
        }



        public SqlSugarScope Db;

        //数据库连接
        //private SqlSugarScope Db = new SqlSugarScope(new ConnectionConfig()
        //{
        //    //连接字符串
        //    ConnectionString = ConfigurationManager.ConnectionStrings["connstring"].ToString(),
        //    DbType = SqlSugar.DbType.MySql,//设置数据库类型
        //    IsAutoCloseConnection = true,//自动释放数据务，如果存在事务，在事务结束后释放
        //    InitKeyType = InitKeyType.Attribute //从实体特性中读取主键自增列信息
        //});


        /// <summary>
        /// 数据库的连接参数
        /// </summary>
        public class _info_数据库连接参数_
        {
            public _info_SQLite_ SQLite_Cfg { set; get; } = new _info_SQLite_();
            public _info_SQLserver_ SQLserver_Cfg { set; get; } = new _info_SQLserver_();
            public _info_Mysql_ MySql_Cfg { set; get; } = new _info_Mysql_();
            public _info_Oracle_ Oracle_Cfg { set; get; } = new _info_Oracle_();

        }



        /// <summary>
        /// 主参数
        /// </summary>
        public class _info_参数_
        {
            public enum数据库类型 数据库类型 { set; get; } = enum数据库类型.Sqlite;

            public _info_数据库连接参数_ 数据库连接参数 { set; get; } = new _info_数据库连接参数_();
        }




        public _info_参数_ 参数 = new _info_参数_();

        /// <summary>
        /// path : 保存参数路径,SQLite时无效,不读参数
        /// </summary>
        /// <param name="info参数"></param>
        /// <param name="path"></param>
        public SqlSugar_DB(string path, _info_参数_ info参数, bool 读取参数 = true, enumSQLserver SQLserver驱动 = enumSQLserver.新驱动)
        {

            List<string> lstWork = new List<string>();
            lstWork.Add("读参数");
            lstWork.Add("初始化");

            foreach (var s in lstWork)
            {
                if (s == "读参数")
                {
                    if (!读取参数 || string.IsNullOrEmpty(path))
                    {
                        continue;
                    }
                    else
                    {
                        #region 读参数

                        _info_SQLite_ SQLite_Cfg = info参数.数据库连接参数.SQLite_Cfg;
                        _info_SQLserver_ SQLserver_Cfg = info参数.数据库连接参数.SQLserver_Cfg;
                        _info_Mysql_ MySql_Cfg = info参数.数据库连接参数.MySql_Cfg;
                        _info_Oracle_ Oracle_Cfg = info参数.数据库连接参数.Oracle_Cfg;
                        switch (info参数.数据库类型)
                        {
                            case enum数据库类型.Sqlite:
                                //读取数据库信息(1, ref SQLite_Cfg, path,out string msgErr);
                                SQLite_Cfg.Path = path;
                                break;
                            case enum数据库类型.SqlServer:
                                读取数据库信息(1, ref SQLserver_Cfg, path, out string msgErr);
                                break;
                            case enum数据库类型.MySql:
                                读取数据库信息(1, ref MySql_Cfg, path, out msgErr);
                                break;
                            case enum数据库类型.Oracle:
                                读取数据库信息(1, ref Oracle_Cfg, path, out msgErr);
                                break;

                        }

                        info参数.数据库连接参数.SQLite_Cfg = SQLite_Cfg;
                        info参数.数据库连接参数.SQLserver_Cfg = SQLserver_Cfg;
                        info参数.数据库连接参数.MySql_Cfg = MySql_Cfg;
                        info参数.数据库连接参数.Oracle_Cfg = Oracle_Cfg;

                        #endregion
                    }
                }
                else if (s == "初始化")
                {
                    #region 初始化

                    switch (info参数.数据库类型)
                    {
                        case enum数据库类型.Sqlite:
                            初始化(info参数.数据库连接参数.SQLite_Cfg);
                            break;
                        case enum数据库类型.SqlServer:
                            初始化(info参数.数据库连接参数.SQLserver_Cfg, SQLserver驱动);
                            break;
                        case enum数据库类型.MySql:
                            初始化(info参数.数据库连接参数.MySql_Cfg);
                            break;
                        case enum数据库类型.Oracle:
                            初始化(info参数.数据库连接参数.Oracle_Cfg);
                            break;
                    }

                    #endregion
                }
            }



            this.参数 = info参数;

        }




        //***************************************


        /// <summary>
        /// model: =0:写 =1:读
        /// </summary>
        /// <param name="model"></param>
        /// <param name="_info_SQLite"></param>
        /// <param name="name"></param>
        public void 读取数据库信息<T>(ushort model, ref T Info, string path, out string msgErr)
        {
            new 文件_文件夹().WriteReadJson(path, model, ref Info, out msgErr);
        }


        #region 停用,有一个泛型的了

        ///// <summary>
        ///// model: =0:写 =1:读
        ///// </summary>
        ///// <param name="model"></param>
        ///// <param name="_info_SQLite"></param>
        ///// <param name="name"></param>
        //public void 读取数据库信息(ushort model, ref _info_SQLite_ _info_SQLite, string name = "Lite")
        //{

        //    string path = Environment.CurrentDirectory + $"\\{name}.txt";
        //    if (!new 文件_文件夹().文件是否存在(path))
        //    {
        //        model = 0;
        //    }
        //    if (model == 0)
        //    {
        //        string vxt = JsonConvert.SerializeObject(_info_SQLite, Formatting.Indented);
        //        new 文本().写文本(path, vxt, true, Encoding.Default, out string msgErr);
        //    }
        //    new 文本().读取文件(path, Encoding.Default, out string rxt);
        //    _info_SQLite = JsonConvert.DeserializeObject<_info_SQLite_>(rxt);
        //}

        ///// <summary>
        ///// model: =0:写 =1:读
        ///// </summary>
        ///// <param name="model"></param>
        ///// <param name="_info_SQLserver"></param>
        ///// <param name="name"></param>
        //public void 读取数据库信息(ushort model, ref _info_SQLserver_ _info_SQLserver, string name = "SQLserver")
        //{
        //    string path = Environment.CurrentDirectory + $"\\{name}.txt";
        //    if (!new 文件_文件夹().文件是否存在(path))
        //    {
        //        model = 0;
        //    }
        //    if (model == 0)
        //    {
        //        string vxt = JsonConvert.SerializeObject(_info_SQLserver, Formatting.Indented);
        //        new 文本().写文本(path, vxt, true, Encoding.Default);
        //    }
        //    new 文本().读取文件(path, Encoding.Default, out string rxt);
        //    _info_SQLserver = JsonConvert.DeserializeObject<_info_SQLserver_>(rxt);
        //}

        ///// <summary>
        ///// model: =0:写 =1:读
        ///// </summary>
        ///// <param name="model"></param>
        ///// <param name="_info_Oracle"></param>
        ///// <param name="name"></param>
        //public void 读取数据库信息(ushort model, ref _info_Oracle_ _info_Oracle, string name = "Oracle")
        //{
        //    model = 1;
        //    string path = Environment.CurrentDirectory + $"\\{name}.txt";
        //    if (!new 文件_文件夹().文件是否存在(path))
        //    {
        //        model = 0;
        //    }
        //    if (model == 0)
        //    {
        //        string vxt = JsonConvert.SerializeObject(_info_Oracle, Formatting.Indented);
        //        new 文本().写文本(path, vxt, true, Encoding.Default);
        //    }
        //    new 文本().读取文件(path, Encoding.Default, out string rxt);
        //    _info_Oracle = JsonConvert.DeserializeObject<_info_Oracle_>(rxt);
        //}

        ///// <summary>
        ///// model: =0:写 =1:读
        ///// </summary>
        ///// <param name="model"></param>
        ///// <param name="_info_Mysql"></param>
        ///// <param name="name"></param>
        //public void 读取数据库信息(ushort model, ref _info_Mysql_ _info_Mysql, string name = "Mysql")
        //{
        //    string path = Environment.CurrentDirectory + $"\\{name}.txt";
        //    if (!new 文件_文件夹().文件是否存在(path))
        //    {
        //        model = 0;
        //    }
        //    if (model == 0)
        //    {
        //        string vxt = JsonConvert.SerializeObject(_info_Mysql, Formatting.Indented);
        //        new 文本().写文本(path, vxt, true, Encoding.Default);
        //    }
        //    new 文本().读取文件(path, Encoding.Default, out string rxt);
        //    _info_Mysql = JsonConvert.DeserializeObject<_info_Mysql_>(rxt);
        //}


        #endregion




        string 数据库连接字符中(_info_SQLite_ SqlLitePath, _SQLite连接类型_ 连接类型 = _SQLite连接类型_.V2)
        {
            if (连接类型 == _SQLite连接类型_.V2)
            {
                return $"data source={SqlLitePath.Path} ";
            }
            else
            {
                return $"data source={SqlLitePath.Path}; Version = 3;";
            }


        }

        string 数据库连接字符中_旧驱动(_info_SQLserver_ _info_SQLserver)
        {
            return $"Server={_info_SQLserver.数据库地址};database={_info_SQLserver.数据库名称};User Id={_info_SQLserver.用户};Password={_info_SQLserver.密码};";
            // return $"Server={_info_SQLserver.数据库地址};database={_info_SQLserver.数据库名称};uid={_info_SQLserver.用户};pwd={_info_SQLserver.密码};Pooling=true; ";

        }


        string 数据库连接字符中_新驱动(_info_SQLserver_ _info_SQLserver)
        {
            return $"Server={_info_SQLserver.数据库地址};database={_info_SQLserver.数据库名称};User Id={_info_SQLserver.用户};Password={_info_SQLserver.密码};Encrypt=True;TrustServerCertificate=True;";
            // return $"Server={_info_SQLserver.数据库地址};database={_info_SQLserver.数据库名称};uid={_info_SQLserver.用户};pwd={_info_SQLserver.密码};Pooling=true; ";

        }



        string 数据库连接字符中(_info_Oracle_ _info_Oracle)
        {
            return $"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={_info_Oracle.数据库地址})(PORT={_info_Oracle.端口}))(CONNECT_DATA=(SERVICE_NAME={_info_Oracle.数据库名称})));Persist Security Info=True;User ID={_info_Oracle.用户};Password={_info_Oracle.密码};";
        }

        string 数据库连接字符中(_info_Mysql_ _info_Mysql)
        {
            return $"server={_info_Mysql.数据库地址};port={_info_Mysql.端口};database={_info_Mysql.数据库名称};user={_info_Mysql.用户名};password={_info_Mysql.密码};Charset = '{_info_Mysql.编码}'";//服务器地址；端口号；数据库；用户名；密码;                    

        }



        void 初始化(_info_SQLite_ info)
        {
            string connectStr = 数据库连接字符中(info);
            Db = new SqlSugarScope(new ConnectionConfig()
            {
                //连接字符串
                ConnectionString = connectStr,
                DbType = SqlSugar.DbType.Sqlite,//设置数据库类型
                IsAutoCloseConnection = true,//自动释放数据务，如果存在事务，在事务结束后释放
                InitKeyType = InitKeyType.Attribute //从实体特性中读取主键自增列信息
            });


        }

        /// <summary>
        /// 驱动
        /// </summary>
        public enum enumSQLserver
        {
            旧驱动,
            新驱动,
        }
        void 初始化(_info_SQLserver_ info, enumSQLserver 驱动 = enumSQLserver.新驱动)
        {
            string connectStr = 数据库连接字符中_旧驱动(info);
            if (驱动 == enumSQLserver.新驱动)
            {
                connectStr = 数据库连接字符中_新驱动(info);
            }

            Db = new SqlSugarScope(new ConnectionConfig()
            {
                //连接字符串
                ConnectionString = connectStr,
                DbType = SqlSugar.DbType.SqlServer,//设置数据库类型
                IsAutoCloseConnection = true,//自动释放数据务，如果存在事务，在事务结束后释放
                InitKeyType = InitKeyType.Attribute //从实体特性中读取主键自增列信息
            });
        }

        void 初始化(_info_Oracle_ info)
        {
            string connectStr = 数据库连接字符中(info);
            Db = new SqlSugarScope(new ConnectionConfig()
            {
                //连接字符串
                ConnectionString = connectStr,
                DbType = SqlSugar.DbType.Oracle,//设置数据库类型
                IsAutoCloseConnection = true,//自动释放数据务，如果存在事务，在事务结束后释放
                InitKeyType = InitKeyType.Attribute //从实体特性中读取主键自增列信息
            });
        }

        void 初始化(_info_Mysql_ info)
        {

            string connectStr = 数据库连接字符中(info);
            Db = new SqlSugarScope(new ConnectionConfig()
            {
                //连接字符串
                ConnectionString = connectStr,
                DbType = SqlSugar.DbType.MySql,//设置数据库类型
                IsAutoCloseConnection = true,//自动释放数据务，如果存在事务，在事务结束后释放
                InitKeyType = InitKeyType.Attribute //从实体特性中读取主键自增列信息
            });
        }



    }
}
