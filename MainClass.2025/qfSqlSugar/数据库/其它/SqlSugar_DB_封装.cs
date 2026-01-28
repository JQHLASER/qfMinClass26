using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfSqlSugar
{
    /// <summary>
    /// 由于只能有一个 SqlSugarScope(Db),所以就封装成静态
    /// </summary>
    public class SqlSugar_DB_封装
    {
        /// <summary>
        /// SqlSugar_DB
        /// </summary>
        public static SqlSugar_DB _DB = new SqlSugar_DB();

        /// <summary>
        /// 多数据库时,通过Event_ConnectionConfig事件配置完后,最后执行初始化
        /// </summary> 
        public static async Task<(bool s, string m)> 初始化(int 超时时间 = 1000 * 10)
        {
            _DB.Event_ConnectionConfig += (s, e) =>
            {
                Event_ConnectionConfig?.Invoke(s, e);
            };

            _DB.Event_初始化结束1 += (s, m, e) =>
            {
                Event_初始化结束?.Invoke(s, m, e);
            };

            return await _DB.初始化(超时时间);
        }

          

        /// <summary>
        /// 初始化,连接数据库
        /// </summary>
        public static event Action<List<ConnectionConfig>, SqlSugar_DB> Event_ConnectionConfig;
        /// <summary>
        /// 参数(初始化状态,消息,DB)
        /// </summary>
        public static event Action<bool, string, SqlSugar_DB> Event_初始化结束;



    }
}
