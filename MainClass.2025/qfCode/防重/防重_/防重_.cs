
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfCode
{
    public class 防重_<T>
    {
        public string _SQLiteFC = Path.Combine("FC26");

        /// <summary>
        /// 数据库id
        /// </summary>
        public string _id = Guid.NewGuid().ToString("N");
        public qfmain._初始化状态_ _初始化状态 = qfmain._初始化状态_.未初始化;

        /// <summary>
        /// 数据库连接参数
        /// </summary>
        public SqlSugar.ConnectionConfig config = null;
        public void 初始化(_Type防重_._em_数据库格式_ 数据库格式 = _Type防重_._em_数据库格式_.SQLite)
        {
            new qfmain.文件_文件夹().文件夹_新建(this._SQLiteFC, out string msgErr);
            On_初始化状态(qfmain._初始化状态_.初始化中, "");
            qfSqlSugar.SqlSugar_DB_封装._DB.Event_ConnectionConfig += (s, db) =>
            {
                config = 生成连接参数_ConnectionConfig(数据库格式);
                s.Add(config);
            };
        }

        private SqlSugar.ConnectionConfig 生成连接参数_ConnectionConfig(_Type防重_._em_数据库格式_ 数据库格式 = _Type防重_._em_数据库格式_.SQLite)
        {
            SqlSugar.ConnectionConfig config = null;
            switch (数据库格式)
            {
                case _Type防重_._em_数据库格式_.SQLite:

                    #region SQLite 

                    string path = Path.Combine(this._SQLiteFC, "FC26.db");
                    string conStr = qfSqlSugar.SqlSugar_DB_封装._DB.生成连接字符串(new qfSqlSugar._cfg_SQLite_ { Path = path });
                    config = qfSqlSugar.SqlSugar_DB_封装._DB.生成连接信息(conStr, this._id, SqlSugar.DbType.Sqlite);

                    #endregion

                    break;
                case _Type防重_._em_数据库格式_.SQLserver:

                    #region SQLserver

                    string pathServer = Path.Combine(this._SQLiteFC, "SqlServer_FC26.txt");
                    qfSqlSugar._cfg_SQLserver_ cfgServer = new qfSqlSugar._cfg_SQLserver_();
                    qfSqlSugar.SqlSugar_DB_封装._DB.读取参数<qfSqlSugar._cfg_SQLserver_>(1, ref cfgServer, pathServer, out string msgErr1);
                    string conStrServer = qfSqlSugar.SqlSugar_DB_封装._DB.生成连接字符串(cfgServer);
                    config = qfSqlSugar.SqlSugar_DB_封装._DB.生成连接信息(conStrServer, this._id, SqlSugar.DbType.SqlServer);

                    #endregion

                    break;
            }

            return config;


        }


        public void 窗体标题栏状态(string Name, qfNet.窗体_标题栏状态 con, qfmain._初始化状态_ state)
        {
            new qfNet.窗体_标题栏状态_方法().标题栏状态(con, Name, Name, state);
        }



        #region 事件

        /// <summary>
        /// 参数(初始化状态,错误消息)
        /// </summary>
        public event Action<qfmain._初始化状态_, string> Event_初始化状态;
        public void On_初始化状态(qfmain._初始化状态_ state, string msgErr)
        {
            this._初始化状态 = state;
            Event_初始化状态?.Invoke(state, msgErr);
        }



        #endregion


        #region Err

        public bool Err_未初始化(string Name, out string msgErr)
        {
            msgErr = "";
            if (this._初始化状态 != qfmain._初始化状态_.已初始化)
            {
                msgErr = $"{Name},{Language_.Get语言("未初始化")}";
                return false;
            }
            return true;
        }


        #endregion



    }
}
