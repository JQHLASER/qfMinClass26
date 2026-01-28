using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfSqlSugar
{
    /// <summary>
    /// 数据库类型
    /// </summary>
    public enum _Model_数据类型_
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
    public class _cfg_SQLserver_
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
        /// 默认值: 1433
        /// </summary>
        public int 端口 { set; get; } = 1433;

    }


    /// <summary>
    /// Oracle连接参数
    /// </summary>
    public class _cfg_Oracle_
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
    public class _cfg_Mysql_
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
        /// 编码,默认 utf8mb4
        /// </summary>
        public string 编码 { set; get; } = "utf8mb4";
    }


    /// <summary>
    /// SQLite连接参数
    /// </summary>
    public class _cfg_SQLite_
    {
        /// <summary>
        /// sqlite路径
        /// </summary>
        public string Path { set; get; }

        public _cfg_SQLite_(string path)
        {
            this.Path = path;
        }

        public _cfg_SQLite_()
        {
        }
    }



    public enum _SQLite_连接类型_
    {
        /// <summary>
        /// net8支持,不含  Version = 3;
        /// </summary>
        V2,
        /// <summary>
        /// .net4.5时代支持的,含Version = 3;
        /// </summary>
        V3,
    }


    public enum _SqlServer_连接类型_
    {
        旧驱动,
        新驱动,
    }







}
