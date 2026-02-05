using mainclassqf;
using qfmain;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace qfSqlSugar
{
    /*说明

      //将所有数据分成50条一次删除
      // db.Utilities.PageEach(deleteList, 50, pageList =>
      //                        {
      //                            //开启事务
      //                            db.BeginTran();
      //                            deleteCount += db.Deleteable(pageList).ExecuteCommand();

      ////提交事务
      //db.CommitTran();
      //                        });


      */

    /* //开启事务
      //this.Db.Ado.BeginTran();
      //提交事务
      //this.Db.Ado.CommitTran();
      //事务回滚,异常时
      //this.Db.Ado.RollbackTran();
    * */

    /// <summary>
    /// 安装 SqlSugar
    /// </summary>
    public class SqlSugar_Table<T> : IDisposable
        where T : class, new()
    {
        public SqlSugarProvider Db { get; private set; } = null;
        private SqlSugarClient _scope;


        /// <summary>
        /// id:连接数据库的ID
        /// </summary> 
        public SqlSugar_Table(SqlSugar_DB Db_, string id)
        {
            this._scope = Db_.Db.CopyNew();
            this.Db = this._scope.GetConnection(id);
        }

        public SqlSugar_Table(SqlSugarProvider db_)
        {
            this.Db = db_;
        }


        /// <summary>
        /// id:连接数据库的ID
        /// <para>封装, SqlSugar_DB_封装 ._DB </para>
        /// </summary> 
        public SqlSugar_Table(string id)
        {
            this._scope = SqlSugar_DB_封装._DB.Db.CopyNew();
            this.Db = this._scope.GetConnection(id);
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


        #region Get查询

        /// <summary>
        /// 查询全部记录 
        /// </summary> 
        public bool GetList(out List<T> lst, out string msgErr)
        {
            bool rt = true;
            lst = default;
            msgErr = string.Empty;
            try
            {
                lst = this.Db.Queryable<T>().ToList();
            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }
            return rt;
        }

        /// <summary>
        /// 按条件查询 ,用法
        /// <para> GetList(u =) u.Name == "张三", out lst, out msg);</para>
        /// <para> GetList(u =) u.Age = 20 and u.Name.Contains("小"), out lst, out msg);</para>
        /// </summary> 
        public bool GetList(Expression<Func<T, bool>> exp, out List<T> lst, out string msgErr)
        {
            bool rt = true;
            lst = default;
            msgErr = string.Empty;
            try
            {
                lst = this.Db.Queryable<T>().Where(exp).ToList();
            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }

            return rt;
        }

        /// <summary>
        /// 根据主键，获取对象 
        /// </summary> 
        public bool GetT(int pkid, out T t_, out string msgErr)
        {
            bool rt = true;

            msgErr = string.Empty;
            try
            {
                t_ = this.Db.Queryable<T>().InSingle(pkid);
            }
            catch (Exception ex)
            {
                t_ = default;
                rt = false;
                msgErr = ex.Message;
            }
            return rt;

        }

        /// <summary>
        /// 分页获取
        /// <para>pageIndex : 索引</para>
        /// <para>pageSize :每页数量 </para>
        /// <para>total : 总页数</para>
        /// </summary>
        /// <param name="sqlstr">  
        public bool GetList(string sqlstr, int pageIndex, int pageSize, out int total, out List<T> list, out string msgErr)
        {
            list = default;
            msgErr = string.Empty;
            total = 0;

            try
            {
                list = this.Db.SqlQueryable<T>(sqlstr).ToPageList(pageIndex, pageSize, ref total);
                return true;
            }
            catch (Exception ex)
            {
                msgErr = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 自定义sql语句查询 
        /// </summary> 
        public bool GetList(string sqlstr, out List<T> list, out string msgErr)
        {
            list = default;
            msgErr = string.Empty;
            bool rt = true;
            try
            {
                list = this.Db.SqlQueryable<T>(sqlstr).ToList();
            }
            catch (Exception ex)
            {
                msgErr = ex.Message;
                rt = false;
            }
            return rt;
        }


        /// <summary>
        /// 自定义sql语句查询 
        /// <para>var sb = new StringBuilder("select * from FC26 where 1=1 ");</para>
        /// <para>var pars = new List(SugarParameter)();</para>
        /// <para>sb.Append(" and 内容 = @内容");</para>
        /// <para>pars.Add(new SugarParameter("@内容", cfg.内容));</para>
        /// </summary> 
        public bool GetList(string sqlstr, List<SugarParameter> pars ,out List<T> list, out string msgErr)
        {
            list = default;
            msgErr = string.Empty;
            bool rt = true;
            try
            {
                list = this.Db.Ado .SqlQuery<T>(sqlstr, pars).ToList();
            }
            catch (Exception ex)
            {
                msgErr = ex.Message;
                rt = false;
            }
            return rt;
        }

        public bool GetList<B>(List<B> lst主键内容, string 表名, string 主键字段名, out List<T> list, out string msgErr)
        {
            msgErr = string.Empty;
            list = default;

            try
            {
                list = this.Db.Queryable<T>()
                              .AS(表名)
                              .In(主键字段名, lst主键内容)
                              .ToList();

                return true;
            }
            catch (Exception ex)
            {
                msgErr = ex.Message;
                return false;
            }
        }


        public bool Get总行数(string 表名, out long totalCount, out string msgErr)
        {
            msgErr = string.Empty;
            totalCount = 0;
            try
            {
                totalCount = this.Db.Queryable<object>()
                                    .AS(表名)   // 指定表名
                                    .Count();   // 获取数量
                return true;
            }
            catch (Exception ex)
            {
                msgErr = ex.Message;
                return false;
            }
        }





        #endregion

        #region Insertable 添加


        /// <summary>
        /// 添加一条,添加一条不需要显式事务
        /// </summary> 
        public bool Insertable(T insertObj, out int 受影响行, out string msgErr)
        {
            bool rt = true;
            msgErr = string.Empty;
            受影响行 = 0;
            try
            {

                受影响行 = this.Db.Insertable(insertObj).ExecuteCommand();

            }
            catch (Exception ex)
            {
                msgErr = ex.Message;
                rt = false;
            }
            return rt;
        }

        /// <summary>
        /// 批量操作，添加多条 
        /// </summary> 
        public bool Insertable(List<T> listObj, out int 受影响行, out string msgErr)
        {
            //只有一行时,无需开事务
            if (listObj .Count ==1)
            {
                return Insertable(listObj[0], out 受影响行, out msgErr);
            } 

            bool rt = true;
            msgErr = string.Empty;
            int count = 0;
            var result = this.Db.Ado.UseTran(() =>
            {
                count = this.Db.Insertable(listObj).ExecuteCommand();
            });
            受影响行 = count;
            rt = result.IsSuccess;
            msgErr = !rt ? result.ErrorMessage : "";
            return rt;
        }

        #endregion


        #region Storageable 添加

        /// <summary>
        /// 添加,自动去重的,已自带事务
        /// <para>使用时,需要在主键结构上增加 [SugarColumn(IsPrimaryKey = true,IsIdentity = true)] </para>
        /// 首先在实体类的 ID 字段上添加 [SugarColumn(IsPrimaryKey = true, IsIdentity = true)] 特性
        /// IsPrimaryKey = true：标识该字段为主键，
        ///IsIdentity = true：标识该字段为自增长字段
        /// </summary> 
        public bool Storageable(List<T> lstObj, out int 受影响行, out string msgErr)
        {
            bool rt = true;
            msgErr = string.Empty;
            受影响行 = 0;
            try
            { 
                受影响行 = this.Db.Storageable(lstObj).ExecuteCommand(); 
            }
            catch (Exception ex)
            {
                msgErr = ex.Message;
                rt = false;
            }

            return rt;
        }

        /// <summary>
        /// 添加,自动去重的
        /// <para>使用时,需要在主键结构上增加 [SugarColumn(IsPrimaryKey = true,IsIdentity = true)] </para>
        /// 首先在实体类的 ID 字段上添加 [SugarColumn(IsPrimaryKey = true, IsIdentity = true)] 特性
        /// IsPrimaryKey = true：标识该字段为主键，
        ///IsIdentity = true：标识该字段为自增长字段
        /// </summary>
        /// <param name="StorageableObj"></param>
        /// <returns></returns>
        public bool Storageable(T StorageableObj, out int 受影响行, out string msgErr)
        {
            bool rt = true;
            msgErr = string.Empty;
            受影响行 = 0;
            try
            {
                受影响行 = this.Db.Storageable(StorageableObj).ExecuteCommand();
            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }
            return rt;
        }



        #endregion

        #region Update 更新


        /// <summary>
        /// 修改，更新一条,事务处理
        /// <para>使用时,需要在主键结构上增加 [SugarColumn(IsPrimaryKey = true,IsIdentity = true)] </para>
        /// 首先在实体类的 ID 字段上添加 [SugarColumn(IsPrimaryKey = true, IsIdentity = true)] 特性
        /// IsPrimaryKey = true：标识该字段为主键，
        ///IsIdentity = true：标识该字段为自增长字段
        /// </summary> 
        public bool Update(T obj, out int 受影响行, out string msgErr)
        {
            msgErr = string.Empty;
            bool rt = true;

            int count = 0;
            var result = this.Db.Ado.UseTran(() =>
            {
                count = this.Db.Updateable<T>(obj).ExecuteCommand();
            });
            受影响行 = count;
            rt = result.IsSuccess;
            msgErr = !rt ? result.ErrorMessage : "";

            return rt;
        }

        /// <summary>
        /// 修改，更新一条,事务处理
        /// <para>使用时,需要在主键结构上增加 [SugarColumn(IsPrimaryKey = true,IsIdentity = true)] </para>
        /// 首先在实体类的 ID 字段上添加 [SugarColumn(IsPrimaryKey = true, IsIdentity = true)] 特性
        /// IsPrimaryKey = true：标识该字段为主键，
        ///IsIdentity = true：标识该字段为自增长字段
        /// </summary> 
        public bool Update(List<T> obj, out int 受影响行, out string msgErr)
        {
            msgErr = string.Empty;
            bool rt = true;

            int count = 0;
            var result = this.Db.Ado.UseTran(() =>
            {
                count = this.Db.Updateable<T>(obj).ExecuteCommand();
            });
            受影响行 = count;
            rt = result.IsSuccess;
            msgErr = !rt ? result.ErrorMessage : "";
            return rt;
        }


        #endregion

        #region Delete 删除


        /// <summary>
        /// 批量删除多条 
        /// </summary> 
        public bool Delete(T obj, out int 受影响行, out string msgErr)
        {
            bool rt = true;
            int count = 0;
            msgErr = string.Empty;
            var result = this.Db.Ado.UseTran(() =>
            {
                count = this.Db.Deleteable<T>(obj).ExecuteCommand();
            });
            受影响行 = count;
            rt = result.IsSuccess;
            msgErr = !rt ? result.ErrorMessage : "";
            return rt;
        }


        /// <summary>
        /// 批量删除多条 
        /// </summary> 
        public bool Delete(List<T> obj, out int 受影响行, out string msgErr)
        {
            bool rt = true;
            int count = 0;
            msgErr = string.Empty;
            var result = this.Db.Ado.UseTran(() =>
            {
                count = this.Db.Deleteable<T>(obj).ExecuteCommand();
            });
            受影响行 = count;
            rt = result.IsSuccess;
            msgErr = !rt ? result.ErrorMessage : "";
            return rt;
        }


        /// <summary>
        /// 条件,用法:
        /// <para>Delete(u => u.Id == 10)</para>
        /// <para> Delete(u => u.Name == "张三" && u.Id > 100);</para>
        /// </summary> 
        public bool Delete(Expression<Func<T, bool>> exp, out int 受影响行, out string msgErr)
        {
            bool rt = true;
            int count = 0;
            msgErr = string.Empty;
            var result = this.Db.Ado.UseTran(() =>
            {
                count = this.Db.Deleteable<T>().Where(exp).ExecuteCommand();
            });
            受影响行 = count;
            rt = result.IsSuccess;
            msgErr = !rt ? result.ErrorMessage : "";
            return rt;
        }


        #endregion


        #region 原生

        /// <summary>
        /// Is事务 : 操作单行数据时为false,多条时为True;
        /// </summary> 
        public bool 执行SQL(string sqlStr, bool Is事务, out string msgErr)
        {
            bool rt = true;
            msgErr = string.Empty;

            if (Is事务)
            {
                var result = this.Db.Ado.UseTran(() =>
                {
                    DataTable dt = this.Db.SqlQueryable<T>(sqlStr).ToDataTable();
                });
                rt = result.IsSuccess;
                msgErr = !rt ? result.ErrorMessage : "";
            }
            else
            {
                try
                {
                    DataTable dt = this.Db.SqlQueryable<T>(sqlStr).ToDataTable();
                }
                catch (Exception ex)
                {
                    rt = false;
                    msgErr = ex.Message;
                }
            }
            return rt;
        }

        /// <summary>
        /// 添加
        /// </summary> 
        public bool 执行SQL_添加(string 表名, List<string> lst_字段, List<string[]> lst_内容, bool Is事务, out string msgErr)
        {
            #region SQL语句

            string 字段 = string.Join(",", lst_字段);


            string sqlStr = $"insert into{表名}({字段})";

            for (int i = 0; i < lst_内容.Count; i++)
            {
                string[] value = lst_内容[i];
                string xt = string.Join(",", value);
                if (i == 0)
                {
                    sqlStr += " select " + xt;
                }
                else
                {
                    sqlStr += "union all ";//添加所有，包括重复项,使用union不添加重复项，最后一项不写union
                    sqlStr += " select " + xt;
                }
            }


            #endregion


            return 执行SQL(sqlStr, Is事务, out msgErr);

        }




        /// <summary>
        /// 原生SQL查询
        /// </summary> 
        public bool Get_DataTable(string sql语句, out DataTable row, out string msgErr)
        {
            #region 例

            //        // 定义参数（避免 SQL 注入）
            //        var m1 = "实际的ROLL_PLAN_NO值"; // 例如 "RP20240817001"

            //        // 执行原生 SQL 查询
            //        var result = db.Ado.SqlQuery<string>(@"
            //select 
            //  TRIM(cast(CONVERT(FLOAT,OUT_MAT_THICK) as char(20)))+'*'+
            //  TRIM(cast(CONVERT(FLOAT,OUT_MAT_WIDTH) as char(20)))+'*'+
            //  TRIM(cast(CONVERT(FLOAT,OUT_MAT_LEN) as char(20))) as Dimensions
            //from SPTBusRollPlan 
            //where ROLL_PLAN_NO = @M1",
            //            new { M1 = m1 } // 参数化查询，传递 @M1 的值
            //        );

            //        // 输出结果
            //        if (result.Any())
            //        {
            //            string dimensions = result.First(); // 获取第一条记录的 Dimensions 值
            //            Console.WriteLine("查询结果：" + dimensions); // 例如输出 "3*1500*5000"
            //        }
            //        else
            //        {
            //            Console.WriteLine("未找到匹配的记录");
            //        }

            #endregion

            bool rt = true;
            msgErr = string.Empty;
            row = new DataTable();
            try
            {
                // 执行原生 SQL 查询
                row = this.Db.SqlQueryable<T>(sql语句).ToDataTable();

            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }
            return rt;
        }

        /// <summary>
        ///  原生SQL
        /// </summary> 
        public bool 执行(string sql语句, out int 受影响行, out string msgErr)
        {
            bool rt = true;
            msgErr = string.Empty;
            int count = 0;
            var result = this.Db.Ado.UseTran(() =>
            {
                // 执行原生 SQL  
                count = this.Db.Ado.ExecuteCommand(sql语句);
            });
            rt = result.IsSuccess;
            msgErr = !rt ? result.ErrorMessage : "";
            受影响行 = count;
            return rt;
        }

        public bool 执行(string sql语句, out List<T> lst, out string msgErr)
        {
            bool rt = true;
            msgErr = string.Empty;
            List<T> lst0 = default;
            var result = this.Db.Ado.UseTran(() =>
            {
                lst0 = this.Db.Ado.SqlQuery<T>(sql语句);
            });
            rt = result.IsSuccess;
            msgErr = !rt ? result.ErrorMessage : "";
            lst = lst0;
            return rt;
        }

        #endregion


        #region 其它 

        /// <summary>
        /// 不同的数据库对时间格式是不一样的
        /// </summary>
        /// <param name="时间"></param>
        /// <returns></returns>
        public string 时间处理(DateTime 时间, SqlSugar.DbType DbType)
        {
            switch (DbType)
            {
                case SqlSugar.DbType.Sqlite:
                    return $"'{时间.ToString($"yyyy-MM-dd HH:mm:ss")}'";
                case SqlSugar.DbType.SqlServer:
                    return $"  CONVERT(datetime,'{时间}')";
                case SqlSugar.DbType.MySql:
                    return $"  CONVERT(datetime,'{时间}')";//还没测试,
            }
            return "";
        }

        #endregion





    }
}
