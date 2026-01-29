using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfSqlSugar
{
    public class 存储过程 : IDisposable
    {
        public SqlSugarProvider Db { get; private set; } = null;
        private SqlSugarClient _scope;

        /// <summary>
        /// id:连接数据库的ID
        /// </summary> 
        public 存储过程(SqlSugar_DB Db_, string id)
        {
            this._scope = Db_.Db.CopyNew();
            this.Db = this._scope.GetConnection(id);
        }

        /// <summary>
        /// id:连接数据库的ID
        /// </summary> 
        public 存储过程(SqlSugarProvider Db_)
        {
            this.Db = Db_;
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
                this.Db = null;
            }
        }




        public class _cfg_信息_
        {
            /// <summary>
            /// <para>传入时为参数名称,如 @ordernumber</para>
            /// <para>输出结果时为字段名称</para>
            /// </summary>
            public string key { set; get; }
            /// <summary>
            /// <para>内容</para>
            /// </summary>
            public object value { set; get; }
        }

        /// <summary>
        /// 过程名,如 SP_Get_Scan
        /// <para>传入参数,如 new SugarParameter[] { new SugarParameter  ("@ordernumber", scanCode) }</para>
        /// </summary> 
        public (bool s, string m, _cfg_信息_[] cfg) Get查询(string 过程名, SugarParameter[] 传入参数)
        {
            try
            {
                var dt = this.Db.Ado.UseStoredProcedure()
                   .GetDataTable(
                       过程名,
                      传入参数
                   );
                return DataTable转List(dt);
            }
            catch (Exception ex)
            {
                return (false, ex.Message, default);
            }
        }

        /// <summary>
        /// 过程名,如 SP_Get_Scan
        /// <para>传入参数,如new SugarParameter("@ordernumber","123456")</para>
        /// </summary> 
        public (bool s, string m, _cfg_信息_[] cfg) Get查询(string 过程名, SugarParameter 传入参数)
        {
            try
            {
                var dt = this.Db.Ado.UseStoredProcedure()
                   .GetDataTable(
                       过程名,
                      传入参数
                   );
                return DataTable转List(dt);
            }
            catch (Exception ex)
            {
                return (false, ex.Message, default);
            }
        }

        /// <summary>
        /// 转换 DataTable 为 List{Dictionary}
        /// </summary>
        public (bool s, string m, _cfg_信息_[] cfg) DataTable转List(DataTable dt)
        {
            try
            {
                var list = dt.AsEnumerable()
              .Select(r => dt.Columns.Cast<DataColumn>()
                  .ToDictionary(c => c.ColumnName, c => r[c]))
              .ToList();


                List<_cfg_信息_> lst = new List<_cfg_信息_>();
                foreach (var row in list)
                {
                    foreach (var c in row)
                    {
                        lst.Add(new _cfg_信息_
                        {
                            key = c.Key,
                            value = c.Value,
                        });

                    }
                }

                return (true, "", lst.ToArray());
            }
            catch (Exception ex)
            {
                return (false, ex.Message, default);
            }
        }


        /// <summary>
        /// 过程名,如 exec [dbo].[Scan_Finish]  
        /// <para>传入参数,如new { param = "P26-001169-1" }</para>
        /// <para>count : 一般为受影响行</para>
        /// </summary> 
        public (bool s, string m, int count) Add(string 过程名, SugarParameter 传入参数)
        {
            try
            {
                var count = this.Db.Ado.UseStoredProcedure()
                   .ExecuteCommand(
                       过程名,
                      传入参数
                   );
                return (true, "", count);
            }
            catch (Exception ex)
            {
                return (false, ex.Message, -1);
            }
        }

        /// <summary>
        /// 过程名,如 exec [dbo].[Scan_Finish]  
        /// <para>传入参数,如new { param = "P26-001169-1" }</para>
        /// <para>count : 一般为受影响行</para>
        /// </summary> 
        public (bool s, string m, int count) Add(string 过程名, SugarParameter[] 传入参数)
        {
            try
            {
                var count = this.Db.Ado.UseStoredProcedure()
                   .ExecuteCommand(
                       过程名,
                      传入参数
                   );
                return (true, "", count);
            }
            catch (Exception ex)
            {
                return (false, ex.Message, -1);
            }
        }
    }
}
