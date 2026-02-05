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
        public string _id = "FC26";


        public void 初始化(_Type防重_._em_数据库格式_ 数据库格式 = _Type防重_._em_数据库格式_.SQLite)
        {
            new qfmain.文件_文件夹().文件夹_新建(this._SQLiteFC, out string msgErr);
            On_初始化状态(qfmain._初始化状态_.初始化中,"");
            qfSqlSugar.SqlSugar_DB_封装._DB .Event_ConnectionConfig += (s, db) =>
            {
                switch (数据库格式)
                {
                    case _Type防重_._em_数据库格式_.SQLite:

                        #region SQLite 

                        string path = Path.Combine(this._SQLiteFC, "FC26.db");
                        string conStr = db.生成连接字符串(new qfSqlSugar._cfg_SQLite_ { Path = path });
                        var connCfg = db.生成连接信息(conStr, this._id, SqlSugar.DbType.Sqlite);
                        s.Add(connCfg);

                        #endregion

                        break;
                    case _Type防重_._em_数据库格式_.SQLserver:

                        #region SQLserver

                        string pathServer = Path.Combine(this._SQLiteFC, "SqlServer_FC26.txt");
                        qfSqlSugar._cfg_SQLserver_ cfgServer = new qfSqlSugar._cfg_SQLserver_();
                        db.读取参数<qfSqlSugar._cfg_SQLserver_>(1, ref cfgServer, pathServer, out string msgErr1);
                        string conStrServer = db.生成连接字符串(cfgServer);
                        var connCfgServer = db.生成连接信息(conStrServer, this._id, SqlSugar.DbType.SqlServer);
                        s.Add(connCfgServer);

                        #endregion

                        break;
                }
            };


        }

        public void 窗体标题栏状态(string Name, qfNet.窗体_标题栏状态 con, qfmain._初始化状态_ state)
        {
            new qfNet.窗体_标题栏状态_方法().标题栏状态(con, Name, Name, state);
        }



        #region 事件

        /// <summary>
        /// 参数(初始化状态,错误消息)
        /// </summary>
        public event Action<qfmain._初始化状态_,string > Event_初始化状态;
        public void On_初始化状态(qfmain._初始化状态_ state,string msgErr)
        {
            Event_初始化状态?.Invoke(state,msgErr );
        }



        #endregion






    }
}
