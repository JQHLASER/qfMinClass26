using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace qfmain
{
    /// <summary>
    /// SqlSugar不使能事务,安全性不高,但速度快,适合单表及要求不高的操作
    /// </summary>
    public class SqlSugar_DB_Context
    {
        SqlSugar.SugarUnitOfWork DbWork;

        /// <summary>
        /// IsNoTran : true=不开事务,
        /// </summary> 
        public SqlSugar_DB_Context(SqlSugar_DB db, bool 是否开事务 = false)
        {
            bool isOpen = 是否开事务 ? !db.Db.Ado.IsNoTran() : db.Db.Ado.IsNoTran();
            DbWork = db.Db.CreateContext(isOpen);
        }






    }
}
