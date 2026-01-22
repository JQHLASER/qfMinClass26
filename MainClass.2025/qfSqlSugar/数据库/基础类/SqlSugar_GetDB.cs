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
    /// 
    /// </summary>
    public class SqlSugar_GetDB : IDisposable
    {
        /// <summary>
        /// 获取到的SqlSugarProvider(Db),通过它调用 SqlSugar_Table 
        /// </summary>
        public SqlSugarProvider Db { get; private set; } = null;
        private SqlSugarClient _scope;
         
        /// <summary>
        /// id:连接数据库的ID
        /// </summary> 
        public SqlSugar_GetDB(SqlSugar_DB Db_, string id)
        {
            this._scope = Db_.Db.CopyNew();
            this.Db = this._scope.GetConnection(id);
        }
          
        public void Dispose()
        {
            if (this._scope != null)
            {
                this._scope.Dispose();
                this._scope = null;
                this.Db = null;
            }
        }

    }
}
