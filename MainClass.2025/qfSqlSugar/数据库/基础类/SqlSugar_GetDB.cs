using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace qfSqlSugar
{
    /// <summary>
    /// 复制Db出来用
    /// </summary>
    public class SqlSugar_GetDB : IDisposable
    {
        /// <summary>
        /// 获取到的SqlSugarProvider(Db),通过它调用 SqlSugar_Table 
        /// </summary>
        public SqlSugarProvider Db { get; private set; } = null;
        private SqlSugarClient _scope;
        public bool _Is线程是否有效 = false;
 

        /// <summary>
        /// id:连接数据库的ID
        /// </summary> 
        public SqlSugar_GetDB(SqlSugar_DB Db_, string id  )
        {
            this._scope = Db_.Db.CopyNew();
            this.Db = this._scope.GetConnection(id); 
        }


        /// <summary>
        /// 两种检测方式可选,一般远程数据库时用
        /// <para>从 _Is线程是否有效 判断是否可以连接</para>
        /// <para>远程连SQLserver,MySql时用此方法</para>
        /// <para>解决远程数据库连接池耗尽断开的问题</para>
        /// <para>模式 =0:查询方式检测(默认),=1:SqlSugarClient方式重连检测</para>
        /// </summary> 
        public SqlSugar_GetDB(SqlSugar_DB Db_, string id, ConnectionConfig cfg, int 模式 = 0)
        {
            var rt = Db_.Is连接是否有效(Db_, id, cfg, 模式);
            _Is线程是否有效 = rt.s;
            if (_Is线程是否有效)
            {
                this._scope = Db_.Db.CopyNew();
                this.Db = this._scope.GetConnection(id);
            }
        }

        /// <summary>
        /// 打开,一般不用,远程数据库时可以用
        /// </summary>
        public void Open()
        {
            this.Db.Open();
        }

        /// <summary>
        /// 关闭,一般不用,远程数据库时可以用
        /// </summary>
        public void Close()
        {
            this.Db.Close();
        }

        public void Dispose()
        {
            if (this._scope != null)
            {
                this._scope.Dispose();
                this._scope = null;
            }

            if (this.Db != null)
            {
                this.Db.Dispose();
                this.Db = null;
            }
        }

    }
}
