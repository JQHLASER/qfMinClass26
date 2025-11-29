using Dapper;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfmain
{
    public   class Database_参数类_
    {
        public   class info_SQLserver参数_
        {

            /// <summary>
            /// 服务器地址
            /// </summary>
            public virtual string 服务器地址 { set; get; } = "127.0.0.1";

            /// <summary>
            /// 数据库名称
            /// </summary>
            public virtual string 数据库名称 { set; get; } = "SQLserver";

            /// <summary>
            /// 登陆用户名
            /// </summary>
            public virtual string 登陆用户名 { set; get; } = "sa";

            /// <summary>
            /// 登陆密码
            /// </summary>
            public virtual string 登陆密码 { set; get; } = "1234";

        }
        public   class info_MySql参数_
        {

            /// <summary>
            /// 服务器地址
            /// </summary>
            public virtual string 服务器地址 { set; get; } = "127.0.0.1";
            /// <summary>
            /// 数据库端口号
            /// </summary>
            public virtual int 端口 { set; get; } = 3306;

            /// <summary>
            /// 数据库名称
            /// </summary>
            public virtual string 数据库名称 { set; get; } = "databalse";

            /// <summary>
            /// 登陆用户名
            /// </summary>
            public virtual string 登陆用户名 { set; get; } = "root";

            /// <summary>
            /// 登陆密码
            /// </summary>
            public virtual string 登陆密码 { set; get; } = "root";
            public virtual string Charset { set; get; } = "utf8";

        }
        public   class info_Oracle参数_
        {
            /// <summary>
            /// 服务器地址
            /// </summary>
            public virtual string 服务器地址 { set; get; } = "127.0.0.1";
            /// <summary>
            /// 数据库端口号
            /// </summary>
            public virtual int 端口 { set; get; } = 1521;

            /// <summary>
            /// 数据库名称
            /// </summary>
            public virtual string 数据库名称 { set; get; } = "Oracle";

            /// <summary>
            /// 登陆用户名
            /// </summary>
            public virtual string 登陆用户名 { set; get; } = "root";

            /// <summary>
            /// 登陆密码
            /// </summary>
            public virtual string 登陆密码 { set; get; } = "1234";

        }
    }

    /*************************************/

    public   class Access_ODBC
    {
        OdbcConnection Access = new OdbcConnection();

        /// <summary>
        ///  连接 ,Uid为用户帐号,Pasd为密码,一般为空即可
        /// </summary>
        /// <param name="AccessPath">Access路径</param>
        /// <param name="Uid">用户帐号</param>
        /// <param name="Pasd">密码</param>
        /// <param name="msg">错误信息</param>
        /// <returns></returns>
        public virtual void Open(string AccessPath, string Uid, string Pasd)
        {

            Access = new OdbcConnection();
            Access.ConnectionString = "dbc:odbc:" +
                  "Driver={Microsoft Access Driver (*.mdb,*.accdb)};" +
                  "Dbq=" + AccessPath + ";" +
                  "Uid=" + Uid + ";" +
                  "Pwd=" + Pasd;

            Access.Open();


        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="msg">错误信息</param>
        /// <returns></returns>
        public virtual void Close()
        {

            Access.Close();
            Access.Dispose();


        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="Str">语句</param>
        /// <param name="msg">错误信息</param>
        /// <returns></returns>
        public virtual void 执行(string Str)
        {

            OdbcCommand comm = new OdbcCommand(Str, Access);
            comm.ExecuteNonQuery();
            comm.Dispose();



        }


        /// <summary>
        ///  查询
        /// </summary>
        /// <param name="str">语句</param>
        /// <param name="dataT">dataTable</param>
        /// <param name="msg">错误信息</param>
        /// <returns></returns>
        public virtual void Select(string str, DataTable dataT)
        {

            OdbcCommand comm = new OdbcCommand(str, Access);
            OdbcDataReader dr = comm.ExecuteReader();
            dataT.Load(dr);
            comm.Dispose();
            dr.Close();



        }


    }

    public   class Access_ADO
    {

        OleDbConnection Con;


        /// <summary>
        /// 连接
        /// </summary>
        /// <param name="AccessPath">路径</param>
        /// <param name="Pasd">密码</param>
        /// <param name="msg">错误信息</param>
        /// <returns></returns>
        public virtual void Open(string AccessPath, string Pasd = "", string Access版本 = "4.0")
        {

            Con = new OleDbConnection();
            Con.ConnectionString = "provider=Microsoft.Jet.OleDb." + Access版本 + ";" +
                                    "Data Source=" + AccessPath + ";" +
                                    "Jet OLEDB:Database Password = " + Pasd + ";";
            Con.Open();

            //  OleDbConnection strConnection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + "员工信息.mdb" + ";Persist Security Info=False");
            //            win32：
            //Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\Data\Access\dat.mdb
            //     x64:
            //Provider = Microsoft.Jet.OLEDB.4.0; Data Source = C:\Data\Access\dat.mdb


        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="msg">错误信息</param>
        /// <returns></returns>
        public virtual void Close()
        {

            Con.Close();
            Con.Dispose();


        }

        /// <summary>
        ///  执行
        /// </summary>
        /// <param name="Str">Sql语句</param>
        /// <param name="msg">错误信息</param>
        /// <returns></returns>
        public virtual void 执行(string Str)
        {
            OleDbCommand comm = new OleDbCommand(Str, Con);
            comm.ExecuteNonQuery();
            comm.Dispose();

        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="Str">SQL语句</param>
        /// <param name="dataT">dataTable</param>
        /// <param name="msg">错误信息</param>
        /// <returns></returns>
        public virtual void Select(string Str, DataTable dataT)
        {


            OleDbCommand comm = new OleDbCommand(Str, Con);
            OleDbDataReader dr = comm.ExecuteReader();
            dataT.Load(dr);
            comm.Dispose();
            dr.Close();



        }



    }


    public   class SQLserver_ODBC
    {

        OdbcConnection sqlS;//= new OdbcConnection();

        public   Database_参数类_.info_SQLserver参数_ info_连接参数 = new Database_参数类_.info_SQLserver参数_();

        public   SQLserver_ODBC(string path)
        {
            new 文件_文件夹().WriteReadJson(path, 1, ref info_连接参数, out string msgErr);

        }



        public virtual void Open()
        {
            Open(info_连接参数);
        }


        public virtual void Open(Database_参数类_.info_SQLserver参数_ info)
        {
            Open(info.服务器地址, info.数据库名称, info.登陆用户名, info.登陆密码);
        }




        /// <summary>
        /// 连接
        /// </summary>
        /// <param name="Server">服务器名称</param>
        /// <param name="Name">数据库名称</param>
        /// <param name="User">登陆用户名</param>
        /// <param name="Pasd">密码</param>
        /// <param name="msg">错误信息</param>
        /// <returns></returns>
        public virtual void Open(string 服务器地址, string 数据库名称, string 登陆用户名, string 登陆密码)
        {
            sqlS = new OdbcConnection();
            sqlS.ConnectionString = "Server=" + 服务器地址 + ";database=" + 数据库名称 + ";" + "uid=" + 登陆用户名 + ";pwd=" + 登陆密码 + "";
            sqlS.Open();


        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <returns></returns>
        public virtual void Close()
        {
            sqlS.Close();
            sqlS.Dispose();
        }



        /// <summary>
        /// 执行语句
        /// </summary>
        /// <param name="SQLstr">SQL语句</param>
        /// <param name="msg">错误信息</param>
        /// <returns></returns>
        public virtual void 执行(string SQLstr)
        {

            OdbcCommand comm = new OdbcCommand(SQLstr, sqlS);
            comm.ExecuteNonQuery();
            comm.Dispose();




        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="SQLstr">SQL语句</param>
        /// <param name="dataT">dataTabe</param>
        /// <param name="msg">错语信息</param>
        /// <returns></returns>
        public virtual void Select(string SQLstr, DataTable dataT)
        {

            OdbcCommand comm = new OdbcCommand(SQLstr, sqlS);
            OdbcDataReader dr = comm.ExecuteReader();
            dataT.Load(dr);
            comm.Dispose();
            dr.Close();






        }


    }


    public   class SQLserver_ADO
    {

        SqlConnection sql;


        public   Database_参数类_.info_SQLserver参数_ info_连接参数 = new Database_参数类_.info_SQLserver参数_();

        public   SQLserver_ADO(string path)
        {
            new 文件_文件夹().WriteReadJson(path, 1, ref info_连接参数, out string msgErr);

        }


        public virtual void Open()
        {
            Open(info_连接参数);
        }


        public virtual void Open(Database_参数类_.info_SQLserver参数_ info)
        {
            Open(info.服务器地址, info.数据库名称, info.登陆用户名, info.登陆密码);
        }


        /// <summary>
        /// 连接数据库
        /// </summary>
        /// <param name="Server">服务器名称</param>
        /// <param name="Name">数据库名称</param>
        /// <param name="User">登陆帐号</param>
        /// <param name="Pasd">密码</param>  
        /// <param name="msg">错误信息</param>
        /// <returns></returns>
        public virtual void Open(string 服务器地址, string 数据库名称, string 登陆用户名, string 登陆密码)
        {

            sql = new SqlConnection();

            sql.ConnectionString = "Server='" + 服务器地址 + "';" +
                                   "database='" + 数据库名称 + "';" +
                                   "UID='" + 登陆用户名 + "';" +
                                   "PWD='" + 登陆密码 + "'";

            sql.Open();
            // sql.Close();             

        }

        /// <summary>
        /// 关闭数据库
        /// </summary>
        public virtual void Close()
        {
            sql.Close();
            sql.Dispose();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="SQLstr">SQL语句</param>
        /// <param name="msg">错误信息</param>
        /// <returns></returns>
        public virtual void 执行(string SQLstr)
        {
            SqlCommand comm = new SqlCommand(SQLstr, sql);
            comm.ExecuteNonQuery();
            comm.Dispose();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="SQLstr">SQL语句</param>
        /// <param name="dataT">dataTabe</param>
        /// <param name="msg">错语信息</param>
        /// <returns></returns>
        public virtual void Select(string SQLstr, DataTable dataT)
        {

            SqlCommand comm = new SqlCommand(SQLstr, sql);
            SqlDataReader dr = comm.ExecuteReader();
            dataT.Load(dr);
            comm.Dispose();
            dr.Close();
            comm.Dispose();

        }

    }

    /// <summary>
    /// 安装 Oracle.ManagedDataAccess
    /// </summary>
    public   class Oracle
    {
        public   Database_参数类_.info_Oracle参数_ info_连接参数 = new Database_参数类_.info_Oracle参数_();


        public   Oracle(string path)
        {
            new 文件_文件夹().WriteReadJson(path, 1, ref info_连接参数, out string msgErr);
        }



        //private  string connStr = "User Id=admin;Password=123;Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.0.1)(PORT=1521)))(CONNECT_DATA=(SERVICE_NAME=test)))";


        OracleConnection conn;



        public virtual void Open()
        {
            Open(info_连接参数);
        }
        public virtual void Open(Database_参数类_.info_Oracle参数_ info)
        {
            Open(info.服务器地址, info.数据库名称, info.登陆用户名, info.登陆密码, info.端口);
        }

        /// <summary>
        /// 连接
        /// </summary>
        /// <param name="服务器地址"></param>
        /// <param name="数据库名称"></param>
        /// <param name="用户名"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public virtual void Open(string 服务器地址, string 数据库名称, string 用户名, string password, int port = 1521)
        {


            string connString = $"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={服务器地址})(PORT={port}))(CONNECT_DATA=(SERVICE_NAME={数据库名称})));Persist Security Info=True;User ID={用户名};Password={password};";

            conn = new OracleConnection(connString);
            conn.Open();
            //if (conn.State == ConnectionState.Open)
            //{


            //}




        }


        /// <summary>
        /// 断开
        /// </summary>
        public virtual void Close()
        {

            conn.Close();
            //if (conn.State == ConnectionState.Open)
            //{
            //    return false;
            //}
            //else
            //{

            //}
            conn.Dispose();



        }


        /// <summary>
        /// 执行
        /// </summary>
        /// <returns></returns>
        public virtual void 执行(string SQLstr)
        {

            // SqlCommand comm = new SqlCommand(SQLstr, sql);

            OracleCommand comm = new OracleCommand(SQLstr, conn);

            comm.ExecuteNonQuery();
            comm.Dispose();


        }



        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="SQLstr"></param>
        /// <param name="dataT"></param>
        /// <returns></returns>
        public virtual void Select(string SQLstr, DataTable dataT)
        {

            // string sql = "select * from " + 表名 + " where 1=1 " + xt.ToString() + " order by score desc";

            OracleCommand command = new OracleCommand(SQLstr, conn);

            OracleDataReader reader = command.ExecuteReader();
            dataT.Load(reader);
            reader.Close();
            command.Dispose();

        }






    }

    /// <summary>
    /// 安装 MySql.Data
    /// </summary>
    public   class MySql
    {


        // 服务器地址；端口号；数据库；用户名；密码
        string connectStr = string.Empty;
        MySqlConnection conn;// new MySqlConnection(connectStr);// 创建连接




        public   Database_参数类_.info_MySql参数_ info_连接参数 = new Database_参数类_.info_MySql参数_();


        public   MySql(string path)
        {
            new 文件_文件夹().WriteReadJson(path, 1, ref info_连接参数, out string msgErr);

        }


        public virtual void Open()
        {

            Open(info_连接参数);
        }


        public virtual void Open(Database_参数类_.info_MySql参数_ info)
        {
            Open(info.服务器地址, info.数据库名称, info.登陆用户名, info.登陆密码, info.端口);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="服务器地址"></param>
        /// <param name="端口号"></param>
        /// <param name="数据库"></param>
        /// <param name="用户名"></param>
        /// <param name="密码"></param>
        /// <returns></returns>
        public virtual void Open(string 服务器地址, string 数据库名称, string 登陆用户名, string 登陆密码, int 端口, string Charset = "utf8")
        {
            connectStr = "server=" + 服务器地址 + ";" +
                         "port=" + 端口 + ";" +
                         "database=" + 数据库名称 + ";" +
                         "user=" + 登陆用户名 + ";" +
                         "password=" + 登陆密码 + ";" +
                         $"Charset = '{Charset}'"; //"Charset = 'utf8'//服务器地址；端口号；数据库；用户名；密码;
            conn = new MySqlConnection(connectStr);// 创建连接
            conn.Open();

        }


        /// <summary>
        /// 关闭
        /// </summary>
        public virtual void Close()
        {
            conn.Close();
            conn.Dispose();
        }



        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="名称"></param>
        /// <param name="SQL语句"></param>
        /// <param name="dataT"></param>
        /// <returns></returns>
        public virtual void select(string SQL语句, DataTable dataT)
        {

            // 创建命令           
            MySqlCommand cmd = new MySqlCommand(SQL语句, conn);

            // 读取数据
            MySqlDataReader reader = cmd.ExecuteReader();
            dataT.Load(reader);
            reader.Close();
            cmd.Dispose();
        }


        /// <summary>
        /// 查询数据库中所有的表名
        /// </summary>
        /// <param name="数据库名称"></param>
        /// <param name="dataT"></param>
        public virtual void select_数据库中所有表名(string 数据库名称, DataTable dataT)
        {

            // 创建命令           
            MySqlCommand cmd = new MySqlCommand("select table_name from information_schema.tables where 1=1 and table_schema = '" + 数据库名称 + "'", conn);

            // 读取数据
            MySqlDataReader reader = cmd.ExecuteReader();
            dataT.Load(reader);

        }


        /// <summary>
        /// 查询所有数据库的名称
        /// </summary>
        /// <param name="dataT"></param>
        public virtual void select_所有数据库名称(DataTable dataT)
        {
            // 创建命令           
            MySqlCommand cmd = new MySqlCommand("SELECT `SCHEMA_NAME`FROM `information_schema`.`SCHEMATA` where 1=1 and `SCHEMA_NAME`='encoding'", conn);

            // 读取数据
            MySqlDataReader reader = cmd.ExecuteReader();
            dataT.Load(reader);
            reader.Close();

        }


        /// <summary>
        /// 查询指定数据库名称
        /// </summary>
        /// <param name="数据库名称"></param>
        /// <param name="dataT"></param>
        public virtual void select__指定数据库名称(string 数据库名称, DataTable dataT)
        {
            // 创建命令           
            MySqlCommand cmd = new MySqlCommand("SELECT `SCHEMA_NAME`FROM `information_schema`.`SCHEMATA` where 1=1 and `SCHEMA_NAME`='" + 数据库名称 + "'", conn);

            // 读取数据
            MySqlDataReader reader = cmd.ExecuteReader();
            dataT.Load(reader);
        }


        /// <summary>
        /// 查询指定表的所有安段名称
        /// </summary>
        /// <param name="表名"></param>
        /// <param name="dataT"></param>
        /// <returns></returns>
        public virtual void select_指定表的所有字段名称(string 表名, DataTable dataT)
        {

            // 创建命令           
            MySqlCommand cmd = new MySqlCommand("select column_name from information_schema.columns where 1=1 and table_name='" + 表名 + "'", conn);

            // 读取数据
            MySqlDataReader reader = cmd.ExecuteReader();
            dataT.Load(reader);

        }



        /// <summary>
        ///  执行
        /// </summary>
        /// <param name="SQL语句"></param>
        /// <returns></returns>
        public virtual void 执行(string SQL语句)
        {

            // 创建命令          
            MySqlCommand cmd = new MySqlCommand(SQL语句, conn);
            // 添加一条记录
            int result = cmd.ExecuteNonQuery();

            cmd.Dispose();


        }


        /// <summary>
        /// 先要连接另一个存在的数据库,然后再新建数据库
        /// </summary>
        /// <param name="数据库名称"></param>
        /// <returns></returns>
        public virtual void 执行_新建数据库(string 数据库名称)
        {


            // 创建命令          
            MySqlCommand cmd = new MySqlCommand("CREATE DATABASE " + 数据库名称 + "", conn);
            // 添加一条记录
            int result = cmd.ExecuteNonQuery();

            cmd.Dispose();
        }


        ///// <summary>
        /////  插入，增加数据
        ///// </summary>
        ///// <param name="SQL语句"></param>
        ///// <returns></returns>
        //public  bool Add_添加(string SQL语句)
        //{
        //    try
        //    {
        //        // 创建命令          
        //        MySqlCommand cmd = new MySqlCommand(SQL语句, conn);
        //        // 添加一条记录
        //        int result = cmd.ExecuteNonQuery();
        //        MsgErr = result.ToString();
        //      
        //    }
        //    catch (Exception ex)
        //    {
        //        MsgErr = ex.Message;
        //        return false;
        //    }


        //}




        ///// <summary>
        ///// 更新，改数据
        ///// </summary>
        ///// <param name="SQL语句"></param>
        //public  bool Update_更新(string SQL语句)
        //{
        //    try
        //    {
        //        // 创建命令         
        //        MySqlCommand cmd = new MySqlCommand(SQL语句, conn);
        //        // 更新记录
        //        int result = cmd.ExecuteNonQuery();
        //        MsgErr = result.ToString();

        //      
        //    }
        //    catch (Exception ex)
        //    {
        //        MsgErr = ex.Message;
        //        return false;
        //    }

        //}



        ///// <summary>
        ///// 删除数据
        ///// </summary>
        ///// <param name="SQL语句"></param>
        //public  bool Delete_删除(string SQL语句)
        //{
        //    try
        //    {

        //        // 创建命令           
        //        MySqlCommand cmd = new MySqlCommand(SQL语句, conn);
        //        // 删除记录
        //        int result = cmd.ExecuteNonQuery();
        //        MsgErr = result.ToString();
        //      
        //    }
        //    catch (Exception ex)
        //    {
        //        MsgErr = ex.Message;
        //        return false;
        //    }
        //}


    }


    /// <summary>
    /// SQLite 操作类
    /// </summary>
    class SqLiteHelper
    {
        /// <summary>
        /// 数据库连接定义
        /// </summary>

        public   SQLiteConnection dbConnection;

        /// <summary>
        /// SQL命令定义
        /// </summary>
        private SQLiteCommand dbCommand;

        /// <summary>
        /// 数据读取定义
        /// </summary>
        private SQLiteDataReader dataReader;

        /// <summary>
        /// 数据库连接字符串定义
        /// </summary>
        private SQLiteConnectionStringBuilder dbConnectionstr;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connectionString">连接SQLite库字符串</param>
        public   SqLiteHelper(string connectionString)
        {
            try
            {
                dbConnection = new SQLiteConnection();

                dbConnectionstr = new SQLiteConnectionStringBuilder();
                dbConnectionstr.DataSource = connectionString;
                dbConnectionstr.Password = "admin";      //设置密码，SQLite ADO.NET实现了数据库密码保护
                dbConnection.ConnectionString = dbConnectionstr.ToString();
                dbConnection.Open();
            }
            catch (Exception e)
            {
                Log(e.ToString());
            }
        }

        /// <summary>
        /// 执行SQL命令
        /// </summary>
        /// <returns>The query.</returns>
        /// <param name="queryString">SQL命令字符串</param>
        public virtual SQLiteDataReader ExecuteQuery(string queryString)
        {
            try
            {
                dbCommand = dbConnection.CreateCommand();
                dbCommand.CommandText = queryString;       //设置SQL语句
                dataReader = dbCommand.ExecuteReader();
            }
            catch (Exception e)
            {
                Log(e.Message);
            }

            return dataReader;
        }

        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        public virtual void CloseConnection()
        {
            //销毁Command
            if (dbCommand != null)
            {
                dbCommand.Cancel();
            }
            dbCommand = null;
            //销毁Reader
            if (dataReader != null)
            {
                dataReader.Close();
            }
            dataReader = null;
            //销毁Connection
            if (dbConnection != null)
            {
                dbConnection.Close();
            }
            dbConnection = null;

        }

        /// <summary>
        /// 读取整张数据表
        /// </summary>
        /// <returns>The full table.</returns>
        /// <param name="tableName">数据表名称</param>
        public virtual SQLiteDataReader ReadFullTable(string tableName)
        {
            string queryString = "SELECT * FROM " + tableName;  //获取所有可用的字段   

            return ExecuteQuery(queryString);
        }

        /// <summary>
        /// 向指定数据表中插入数据
        /// </summary>
        /// <returns>The values.</returns>
        /// <param name="tableName">数据表名称</param>
        /// <param name="values">插入的数值</param>
        public virtual SQLiteDataReader InsertValues(string tableName, string[] values)
        {
            //获取数据表中字段数目
            int fieldCount = ReadFullTable(tableName).FieldCount;
            //当插入的数据长度不等于字段数目时引发异常
            if (values.Length != fieldCount)
            {
                throw new SQLiteException("values.Length!=fieldCount");
            }
            string queryString = "INSERT INTO " + tableName + " VALUES (" + "'" + values[0] + "'";
            for (int i = 1; i < values.Length; i++)
            {
                queryString += ", " + "'" + values[i] + "'";
            }
            queryString += " )";
            return ExecuteQuery(queryString);
        }

        /// <summary>
        /// 更新指定数据表内的数据
        /// </summary>
        /// <returns>The values.</returns>
        /// <param name="tableName">数据表名称</param>
        /// <param name="colNames">字段名</param>
        /// <param name="colValues">字段名对应的数据</param>
        /// <param name="key">关键字</param>
        /// <param name="value">关键字对应的值</param>
        /// <param name="operation">运算符：=,(,),...，默认“=”</param>
        public virtual SQLiteDataReader UpdateValues(string tableName, string[] colNames, string[] colValues, string key, string value, string operation)
        {
            // operation="=";  //默认
            //当字段名称和字段数值不对应时引发异常
            if (colNames.Length != colValues.Length)
            {
                throw new SQLiteException("colNames.Length!=colValues.Length");
            }
            string queryString = "UPDATE " + tableName + " SET " + colNames[0] + "=" + "'" + colValues[0] + "'";

            for (int i = 1; i < colValues.Length; i++)
            {
                queryString += ", " + colNames[i] + "=" + "'" + colValues[i] + "'";
            }
            queryString += " WHERE " + key + operation + "'" + value + "'";

            return ExecuteQuery(queryString);
        }
        /// <summary>
        /// 更新指定数据表内的数据
        /// </summary>
        /// <returns>The values.</returns>
        /// <param name="tableName">数据表名称</param>
        /// <param name="colNames">字段名</param>
        /// <param name="colValues">字段名对应的数据</param>
        /// <param name="key">关键字</param>
        /// <param name="value">关键字对应的值</param>
        /// <param name="operation">运算符：=,(,),...，默认“=”</param>
        public virtual SQLiteDataReader UpdateValues(string tableName, string[] colNames, string[] colValues, string key1, string value1, string operation, string key2, string value2)
        {
            // operation="=";  //默认
            //当字段名称和字段数值不对应时引发异常
            if (colNames.Length != colValues.Length)
            {
                throw new SQLiteException("colNames.Length!=colValues.Length");
            }
            string queryString = "UPDATE " + tableName + " SET " + colNames[0] + "=" + "'" + colValues[0] + "'";

            for (int i = 1; i < colValues.Length; i++)
            {
                queryString += ", " + colNames[i] + "=" + "'" + colValues[i] + "'";
            }
            //表中已经设置成int类型的不需要再次添加‘单引号’，而字符串类型的数据需要进行添加‘单引号’
            queryString += " WHERE " + key1 + operation + "'" + value1 + "'" + "OR " + key2 + operation + "'" + value2 + "'";

            return ExecuteQuery(queryString);
        }


        /// <summary>
        /// 删除指定数据表内的数据
        /// </summary>
        /// <returns>The values.</returns>
        /// <param name="tableName">数据表名称</param>
        /// <param name="colNames">字段名</param>
        /// <param name="colValues">字段名对应的数据</param>
        public virtual SQLiteDataReader DeleteValuesOR(string tableName, string[] colNames, string[] colValues, string[] operations)
        {
            //当字段名称和字段数值不对应时引发异常
            if (colNames.Length != colValues.Length || operations.Length != colNames.Length || operations.Length != colValues.Length)
            {
                throw new SQLiteException("colNames.Length!=colValues.Length || operations.Length!=colNames.Length || operations.Length!=colValues.Length");
            }

            string queryString = "DELETE FROM " + tableName + " WHERE " + colNames[0] + operations[0] + "'" + colValues[0] + "'";
            for (int i = 1; i < colValues.Length; i++)
            {
                queryString += "OR " + colNames[i] + operations[0] + "'" + colValues[i] + "'";
            }
            return ExecuteQuery(queryString);
        }

        /// <summary>
        /// 删除指定数据表内的数据
        /// </summary>
        /// <returns>The values.</returns>
        /// <param name="tableName">数据表名称</param>
        /// <param name="colNames">字段名</param>
        /// <param name="colValues">字段名对应的数据</param>
        public virtual SQLiteDataReader DeleteValuesAND(string tableName, string[] colNames, string[] colValues, string[] operations)
        {
            //当字段名称和字段数值不对应时引发异常
            if (colNames.Length != colValues.Length || operations.Length != colNames.Length || operations.Length != colValues.Length)
            {
                throw new SQLiteException("colNames.Length!=colValues.Length || operations.Length!=colNames.Length || operations.Length!=colValues.Length");
            }

            string queryString = "DELETE FROM " + tableName + " WHERE " + colNames[0] + operations[0] + "'" + colValues[0] + "'";

            for (int i = 1; i < colValues.Length; i++)
            {
                queryString += " AND " + colNames[i] + operations[i] + "'" + colValues[i] + "'";
            }
            return ExecuteQuery(queryString);
        }


        /// <summary>
        /// 创建数据表
        /// </summary> +
        /// <returns>The table.</returns>
        /// <param name="tableName">数据表名</param>
        /// <param name="colNames">字段名</param>
        /// <param name="colTypes">字段名类型</param>
        public virtual SQLiteDataReader CreateTable(string tableName, string[] colNames, string[] colTypes)
        {
            string queryString = "CREATE TABLE IF NOT EXISTS " + tableName + "( " + colNames[0] + " " + colTypes[0];
            for (int i = 1; i < colNames.Length; i++)
            {
                queryString += ", " + colNames[i] + " " + colTypes[i];
            }
            queryString += "  ) ";
            return ExecuteQuery(queryString);
        }

        /// <summary>
        /// Reads the table.
        /// </summary>
        /// <returns>The table.</returns>
        /// <param name="tableName">Table name.</param>
        /// <param name="items">Items.</param>
        /// <param name="colNames">Col names.</param>
        /// <param name="operations">Operations.</param>
        /// <param name="colValues">Col values.</param>
        public virtual SQLiteDataReader ReadTable(string tableName, string[] items, string[] colNames, string[] operations, string[] colValues)
        {
            string queryString = "SELECT " + items[0];
            for (int i = 1; i < items.Length; i++)
            {
                queryString += ", " + items[i];
            }
            queryString += " FROM " + tableName + " WHERE " + colNames[0] + " " + operations[0] + " " + colValues[0];
            for (int i = 0; i < colNames.Length; i++)
            {
                queryString += " AND " + colNames[i] + " " + operations[i] + " " + colValues[0] + " ";
            }
            return ExecuteQuery(queryString);
        }

        /// <summary>
        /// 本类log
        /// </summary>
        /// <param name="s"></param>
        void Log(string s)
        {
            Console.WriteLine("class SqLiteHelper:::" + s);
        }





    }


    /// <summary>
    /// 日期时间为varchar类型,不然查询时会报错
    ///  <para>安装 System.Data.SQLite</para>
    /// </summary>
    public   class SQLite
    {
        DbProviderFactory factory;// = SQLiteFactory.Instance;
        // SQLiteConnection con;
        DbConnection con;// = factory.CreateConnection()
        /// <summary>
        /// 数据库后缀为.db3或.db
        /// </summary>
        /// <param name="SQLitePath"></param>
        public virtual void 创建数据库文件(string SQLitePath)
        {
            // 创建数据库文件
            // File.Delete(SQLitePath);
            SQLiteConnection.CreateFile(SQLitePath);

        }


        /// <summary>
        /// 数据库后缀为.db3或.db
        /// </summary>
        /// <param name="SQLitePath"></param>
        public virtual void 删除数据库文件(string SQLitePath)
        {
            File.Delete(SQLitePath);
        }




        /// <summary>
        /// 打开数据库
        /// </summary>
        /// <param name="SQLitePath"></param>
        /// <returns></returns>
        public virtual void Open(string SQLitePath)
        {
            factory = SQLiteFactory.Instance;
            con = factory.CreateConnection();


            // con = new SQLiteConnection("data source=" + SQLitePath + "; Version = 3;");
            con.ConnectionString = "data source=" + SQLitePath + "; Version = 3;";
            con.Open();
        }



        /// <summary>
        /// 关闭数据库
        /// </summary>
        /// <returns></returns>
        public virtual void Close()
        {
            con.Close();
            con.Dispose();

        }



        /// <summary>
        /// 添加数据SQL语句"insert into [test1] ([s]) values (?)",在添加的内容前加@
        /// </summary>
        /// <param name="表名"></param>
        /// <param name="字段名"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual void 执行_单条命令(string SQL语句)
        {

            DbTransaction trans = con.BeginTransaction(); // <-------------------
            DbCommand cmd = con.CreateCommand();
            try
            {

                cmd.CommandText = SQL语句;
                // cmd.Parameters[0].Value = i.ToString();
                cmd.ExecuteNonQuery();

                trans.Commit(); // <-------------------
                cmd.Dispose();
                trans.Dispose();
            }
            catch (Exception)
            {
                trans.Rollback(); // <-------------------
                trans.Dispose();
                cmd.Dispose();
                //throw; // 
            }

            //SQLiteCommand command = con.CreateCommand();
            //command.CommandText = SQL语句;
            ////command.ExecuteScalar();
            //command.ExecuteNonQueryAsync();
            ////string sql = SQL语句;
            ////SQLiteCommand command = new SQLiteCommand(sql, con);

            ////  command.ExecuteNonQuery();
            //command.Dispose();




        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="表名"></param>
        /// <param name="条件">条件,为空时全部查询</param>
        /// <returns></returns>
        public virtual void 查询(string SQL语句, DataTable dataT)
        {

            // string sql = "select * from " + 表名 + " where 1=1 " + xt.ToString() + " order by score desc";
            //string sql = SQL语句;
            //SQLiteCommand command = new SQLiteCommand(sql, con);
            //SQLiteDataReader reader = command.ExecuteReader();
            //dataT.Load(reader);
            //reader.Close();
            //command.Dispose();









            DbTransaction trans = con.BeginTransaction(); // <-------------------
            DbCommand cmd = con.CreateCommand();
            try
            {

                cmd.CommandText = SQL语句;
                // cmd.Parameters[0].Value = i.ToString();

                DbDataReader reader = cmd.ExecuteReader();
                dataT.Load(reader);

                reader.Close();
                trans.Commit(); // <-------------------
                cmd.Dispose();
                trans.Dispose();
            }
            catch (Exception)
            {
                trans.Rollback(); // <-------------------
                trans.Dispose();
                cmd.Dispose();
                //throw; // 
            }









        }



        /// <summary>
        /// 效率高,SQL语句 添加数据"insert into [test1] ([s]) values (?)",在添加的内容前加@
        /// </summary>
        public virtual void 执行(string[] SQL语句)
        {
            DbTransaction trans = con.BeginTransaction(); // <-------------------   //手动设置开始事务
            DbCommand cmd = con.CreateCommand();
            try
            {


                int count = SQL语句.Length;
                // 连续插入记录
                for (int i = 0; i < count; i++)
                {
                    cmd.CommandText = SQL语句[i];
                    // cmd.Parameters[0].Value = i.ToString();
                    cmd.ExecuteNonQuery();
                }

                trans.Commit(); // <-------------------
                cmd.Dispose();
                trans.Dispose();
            }
            catch (Exception)
            {
                trans.Rollback(); // <-------------------
                trans.Dispose();
                cmd.Dispose();
                throw; // 
            }


        }


        /// <summary>
        ///  SQL语句"create table [test1] ([id] INTEGER PRIMARY KEY, [s] TEXT COLLATE NOCASE)"
        /// </summary>
        /// <param name="SQL语句"></param>
        public virtual void 创建表数据(string SQL语句)
        {
            // 创建数据表
            string sql = SQL语句;
            DbCommand cmd = con.CreateCommand();
            cmd.Connection = con;
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();

            // 添加参数
            cmd.Parameters.Add(cmd.CreateParameter());
            cmd.Dispose();
        }



    }



    /// <summary>
    /// 日期时间为varchar类型,不然查询时会报错
    /// <para>安装 System.Data.SQLite</para>
    /// <para>安装 Dapper</para>
    /// </summary> 
    public   class SQLite_Dpper
    {


        SQLiteConnectionStringBuilder sb;//= new SQLiteConnectionStringBuilder();

        SQLiteConnection con;// = new SQLiteConnection(sb.ToString());






        /// <summary>
        /// 连接
        /// </summary>
        /// <param name="SQLitePath"></param>
        public virtual void Open(string SQLitePath)
        {
            sb = new SQLiteConnectionStringBuilder();
            sb.DataSource = SQLitePath;
            con = new SQLiteConnection(sb.ToString());
            con.Open();
        }

        /// <summary>
        /// 关闭
        /// </summary>
        public virtual void Close()
        {
            con.Close();
            con.Dispose();
            sb.Clear();
        }


        public virtual void 查询(string SQL语句, DataTable dataT)
        {

            var reader = con.ExecuteReader(SQL语句);
            dataT.Load(reader);

        }

        public virtual void 执行(string SQL语句)
        {
            //  con.ExecuteAsync(SQL语句);
            // con.Execute(SQL语句);

            con.ExecuteScalar(SQL语句);
        }


        #region 事务添加,据说写入很快的

        //          // 闪电插入（10万条/3秒）
        //            using (var transaction = connection.BeginTransaction())
        //            {
        //                var command = new SQLiteCommand(
        //                  "INSERT INTO Logs (DeviceId, Value) VALUES (@devId, @val)",
        //                   connection,
        //                   transaction);

        //    var param1 = new SQLiteParameter("@devId");
        //    var param2 = new SQLiteParameter("@val");
        //    command.Parameters.Add(param1);
        //                command.Parameters.Add(param2);

        //                foreach (var log in logs)
        //                {
        //                    param1.Value = log.DeviceId;
        //                    param2.Value = log.Value;
        //                    command.ExecuteNonQuery();
        //                }
        //transaction.Commit();
        // }


        #endregion

        #region 按页查询

        //public List<DeviceLog> GetPagedLogs(int pageIndex, int pageSize)
        //    {
        //        return connection.Query<DeviceLog>(@"
        //    SELECT * FROM Logs 
        //    ORDER BY LogTime DESC 
        //    LIMIT @PageSize 
        //    OFFSET @Offset",
        //          new
        //          {
        //              PageSize = pageSize,
        //              Offset = pageIndex * pageSize
        //          }).ToList();
        //    }

        #endregion


    }


}
