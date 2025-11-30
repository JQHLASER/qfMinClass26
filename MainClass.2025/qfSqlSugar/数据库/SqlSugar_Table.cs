using qfmain;
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


    /// <summary>
    /// 安装 SqlSugar
    /// </summary>
    public class SqlSugar_Table<T> where T : class, new()
    {
        public SqlSugarProvider Db = null;

        /// <summary>
        /// id:连接数据库的ID
        /// </summary> 
        public SqlSugar_Table(SqlSugar_DB Db_, string id)
        {
            this.Db = Db_.Db.GetConnection(id);
        }

         
        /// <summary>
        /// 查询全部记录,非事务处理
        /// </summary>
        /// <returns></returns>
        public List<T> GetList()
        {
            var list = this.Db.Queryable<T>().ToList();
            return list;
        }


        /// <summary>
        /// 查询全部记录,事务处理
        /// </summary>
        /// <returns></returns>
        public bool GetList(out List<T> list, out string msgErr, bool 是否事务处理 = false)
        {

            list = new List<T>();
            msgErr = string.Empty;
            bool rt = true;

            try
            {
                if (是否事务处理)
                {
                    //开启事务
                    this.Db.Ado.BeginTran();
                }
                list = this.Db.Queryable<T>().ToList();
                if (是否事务处理)
                {
                    //提交事务
                    this.Db.Ado.CommitTran();
                }
            }
            catch (Exception ex)
            {
                if (是否事务处理)
                {
                    //事务回滚
                    this.Db.Ado.RollbackTran();
                }
                msgErr = ex.Message;
                rt = false;
            }

            return rt;
        }


        /// <summary>
        /// 按条件查询,非事务处理
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public List<T> GetList(Expression<Func<T, bool>> exp)
        {
            var list = this.Db.Queryable<T>().Where(exp).ToList();
            return list;
        }

        /// <summary>
        /// 根据主键，获取对象,非事务处理
        /// </summary>
        /// <param name="pkid"></param>
        /// <returns></returns>
        public T GetObj(int pkid)
        {
            return this.Db.Queryable<T>().InSingle(pkid);
        }

        /// <summary>
        /// 根据主键，获取对象 
        /// </summary>
        /// <param name="pkid"></param>
        /// <param name="t_"></param>
        /// <param name="msgErr"></param>
        /// <returns></returns>
        public bool GetObj(int pkid, ref T t_, out string msgErr, bool 是否事务处理 = false)
        {

            msgErr = string.Empty;
            bool rt = true;

            try
            {
                if (是否事务处理)
                {
                    //开启事务
                    this.Db.Ado.BeginTran();
                }
                t_ = this.Db.Queryable<T>().InSingle(pkid);
                if (是否事务处理)
                {
                    //提交事务
                    this.Db.Ado.CommitTran();
                }
            }
            catch (Exception ex)
            {
                if (是否事务处理)
                {
                    //事务回滚
                    this.Db.Ado.RollbackTran();
                }
                msgErr = ex.Message;
                rt = false;
            }
            return rt;
        }


        /// <summary>
        /// 分页获取,非事务处理
        /// </summary>
        /// <param name="sqlstr"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totle"></param>
        /// <returns></returns>
        public List<T> GetList(string sqlstr, int pageIndex, int pageSize, out int totle)
        {
            List<T> list = this.Db.SqlQueryable<T>(sqlstr).ToPageList(pageIndex, pageSize);
            totle = list.Count > 0 ? list.Count : 0;

            return list;
        }

        /// <summary>
        /// 分页获取,事务处理
        /// </summary>
        /// <param name="sqlstr"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totle"></param>
        /// <param name="list"></param>
        /// <param name="msgErr"></param>
        /// <returns></returns>
        public bool GetList(string sqlstr, int pageIndex, int pageSize, out int totle, out List<T> list, out string msgErr, bool 是否事务处理 = false)
        {

            list = new List<T>();
            msgErr = string.Empty;
            bool rt = true;

            try
            {
                if (是否事务处理)
                {
                    //开启事务
                    this.Db.Ado.BeginTran();
                }
                list = this.Db.SqlQueryable<T>(sqlstr).ToPageList(pageIndex, pageSize);
                if (是否事务处理)
                {
                    //提交事务
                    this.Db.Ado.CommitTran();
                }
            }
            catch (Exception ex)
            {
                if (是否事务处理)
                {
                    //事务回滚
                    this.Db.Ado.RollbackTran();
                }
                msgErr = ex.Message;
                rt = false;
            }

            totle = list.Count > 0 ? list.Count : 0;
            return rt;


        }


        /// <summary>
        /// 自定义sql语句查询,非事务处理
        /// </summary>
        /// <param name="sqlstr"></param>
        /// <returns></returns>
        public List<T> GetList(string sqlstr)
        {
            List<T> list = this.Db.SqlQueryable<T>(sqlstr).ToList();
            return list;
        }


        /// <summary>
        /// 自定义sql语句查询,事务处理
        /// </summary>
        /// <param name="sqlstr"></param>
        /// <param name="list"></param>
        /// <param name="msgErr"></param>
        /// <returns></returns>
        public bool GetList(string sqlstr, out List<T> list, out string msgErr, bool 是否事务处理 = false)
        {
            list = new List<T>();
            msgErr = string.Empty;
            bool rt = true; 
            try
            {
                if (是否事务处理)
                {
                    //开启事务 
                    this.Db.Ado.BeginTran();
                }
                list = this.Db.SqlQueryable<T>(sqlstr).ToList();
                if (是否事务处理)
                {
                    //提交事务
                    this.Db.Ado.CommitTran();
                }
            }
            catch (Exception ex)
            {
                if (是否事务处理)
                {
                    //事务回滚
                    this.Db.Ado.RollbackTran();
                }
                msgErr = ex.Message;
                rt = false;
            }

            return rt;
        }



        public bool GetList(List<int> lst主键内容, string 表名, string 主键字段名, out List<T> list, out string msgErr, bool 是否事务处理 = false)
        {
            string sql = $"select * from {表名} where {主键字段名} in ( ";

            for (int i = 0; i < lst主键内容.Count; i++)
            {
                int v = lst主键内容[i];
                if (i == 0)
                {
                    sql += $"{v}";
                }
                else
                {
                    sql += $",{v}";
                }
            }
            sql += ")";
            return GetList(sql, out list, out msgErr, 是否事务处理);
        }

        public bool GetList(List<string> lst主键内容, string 表名, string 主键字段名, out List<T> list, out string msgErr, bool 是否事务处理 = false)
        {
            string sql = $"select * from {表名} where {主键字段名} in ( ";

            for (int i = 0; i < lst主键内容.Count; i++)
            {
                string v = lst主键内容[i];
                if (i == 0)
                {
                    sql += $"'{v}'";
                }
                else
                {
                    sql += $",'{v}'";
                }
            }
            sql += ")";
            return GetList(sql, out list, out msgErr, 是否事务处理);
        }


        /// <summary>
        /// 添加一条,事务处理
        /// </summary>
        /// <param name="insertObj"></param>
        /// <param name="msgErr"></param>
        /// <returns></returns>
        public int Insertable(T insertObj, out string msgErr)
        {
            msgErr = string.Empty;
            int res = 0;
            try
            {
                //开启事务
                this.Db.Ado.BeginTran();
                res = this.Db.Insertable(insertObj).ExecuteCommand();
                //提交事务
                this.Db.Ado.CommitTran();
            }
            catch (Exception ex)
            {
                //事务回滚
                this.Db.Ado.RollbackTran();
                msgErr = ex.Message;
            }
            return res;
        }

        /// <summary>
        /// 批量操作，添加多条，事务处理
        /// </summary>
        /// <param name="listObj"></param>
        /// <param name="msgErr"></param>
        /// <returns></returns>
        public int Insertable(List<T> listObj, out string msgErr)
        {
            msgErr = string.Empty;
            int res = 0;
            try
            {
                //开启事务
                this.Db.Ado.BeginTran();
                res = this.Db.Insertable(listObj).ExecuteCommand();
                //提交事务
                this.Db.Ado.CommitTran();
            }
            catch (Exception ex)
            {
                //事务回滚
                this.Db.Ado.RollbackTran();
                msgErr = ex.Message;

            }
            return res;
        }


        /// <summary>
        /// 批量操作，添加多条，事务处理
        /// </summary>
        /// <param name="listObj"></param>
        /// <param name="msgErr"></param>
        /// <returns></returns>
        public bool Insertable(List<T> listObj, out int 受影响行, out string msgErr)
        {
            bool rt = true;
            msgErr = string.Empty;

            受影响行 = 0;
            try
            {  //开启事务
                this.Db.Ado.BeginTran();
                受影响行 = this.Db.Insertable(listObj).ExecuteCommand();
                //提交事务
                this.Db.Ado.CommitTran();
            }
            catch (Exception ex)
            {
                //事务回滚
                this.Db.Ado.RollbackTran();
                msgErr = ex.Message;
                rt = false;

            }
            return rt;
        }

        /// <summary>
        /// 批量操作，添加多条，事务处理
        /// </summary>
        /// <param name="listObj"></param>
        /// <param name="msgErr"></param>
        /// <returns></returns>
        public bool Insertable(T listObj, out int 受影响行, out string msgErr)
        {
            bool rt = true;
            msgErr = string.Empty;

            受影响行 = 0;
            try
            {  //开启事务
                this.Db.Ado.BeginTran();
                受影响行 = this.Db.Insertable(listObj).ExecuteCommand();
                //提交事务
                this.Db.Ado.CommitTran();
            }
            catch (Exception ex)
            {
                //事务回滚
                this.Db.Ado.RollbackTran();
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
        public bool Storageable(List<T> StorageableObj, out int 受影响行, out string msgErr)
        {
            bool rt = true;
            msgErr = string.Empty;
            受影响行 = 0;
            try
            {   //开启事务
                this.Db.Ado.BeginTran();
                受影响行 = this.Db.Storageable(StorageableObj).ExecuteCommand();
                //提交事务
                this.Db.Ado.CommitTran();
            }
            catch (Exception ex)
            {
                rt = false;
                //事务回滚
                this.Db.Ado.RollbackTran();
                msgErr = ex.Message;
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
            {   //开启事务
                this.Db.Ado.BeginTran();
                受影响行 = this.Db.Storageable(StorageableObj).ExecuteCommand();
                //提交事务
                this.Db.Ado.CommitTran();
            }
            catch (Exception ex)
            {
                rt = false;
                //事务回滚
                this.Db.Ado.RollbackTran();
                msgErr = ex.Message;
            }
            return rt;
        }





        /// <summary>
        /// 修改，更新一条
        /// <para>使用时,需要在主键结构上增加 [SugarColumn(IsPrimaryKey = true,IsIdentity = true)] </para>
        /// 首先在实体类的 ID 字段上添加 [SugarColumn(IsPrimaryKey = true, IsIdentity = true)] 特性
        /// IsPrimaryKey = true：标识该字段为主键，
        ///IsIdentity = true：标识该字段为自增长字段
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int Update(T obj)
        {
            return this.Db.Updateable<T>(obj).ExecuteCommand();
        }

        /// <summary>
        /// 修改，更新一条,事务处理
        /// <para>使用时,需要在主键结构上增加 [SugarColumn(IsPrimaryKey = true,IsIdentity = true)] </para>
        /// 首先在实体类的 ID 字段上添加 [SugarColumn(IsPrimaryKey = true, IsIdentity = true)] 特性
        /// IsPrimaryKey = true：标识该字段为主键，
        ///IsIdentity = true：标识该字段为自增长字段
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="msgErr"></param>
        /// <returns></returns>
        public bool Update(T obj, out int 受影响行, out string msgErr)
        {
            msgErr = string.Empty;
            bool rt = true;
            受影响行 = 0;
            try
            {
                this.Db.Ado.BeginTran();
                受影响行 = this.Db.Updateable<T>(obj).ExecuteCommand();
                this.Db.Ado.CommitTran();
            }
            catch (Exception ex)
            {
                rt = false;
                this.Db.Ado.RollbackTran();
                msgErr = ex.Message;
            }
            return rt;
        }


        /// <summary>
        /// 忽略某些列更新
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public int Update(T obj, List<IgnoreColumn> columns)
        {
            if (columns.Count > 0)
            {
                columns.ForEach((e) =>
                {
                    this.Db.IgnoreColumns.Add(e);
                });
            }
            return this.Db.Updateable<T>(obj).ExecuteCommand();
        }


        /// <summary>
        /// 批量更新多条，事务处理
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool Update(List<T> obj, out int 受影响行, out string msgErr)
        {
            bool rt = true;
            受影响行 = 0;
            msgErr = string.Empty;
            try
            {
                //开启事务
                this.Db.Ado.BeginTran();
                //执行更新
                受影响行 = this.Db.Updateable<T>(obj).ExecuteCommand();
                //提交事务
                this.Db.Ado.CommitTran();
            }
            catch (Exception ex)
            {
                //事务回滚
                this.Db.Ado.RollbackTran();
                //抛出异常
                msgErr = ex.Message;
                rt = false;
            }
            return rt;
        }



        /// <summary>
        /// 添加,自动去重的
        /// 使用时,需要在主键结构上增加 [SugarColumn(IsPrimaryKey = true)]
        /// </summary>
        /// <param name="StorageableObj"></param>
        /// <returns></returns>
        public int Storageable(T StorageableObj)
        {
            return this.Db.Storageable(StorageableObj).ExecuteCommand();
        }

        /// <summary>
        /// 添加,自动去重的
        /// <para>使用时,需要在主键结构上增加 [SugarColumn(IsPrimaryKey = true)] </para>
        /// </summary>
        /// <param name="StorageableObj"></param>
        /// <returns></returns>
        public int Storageable(List<T> StorageableObj)
        {


            int res = 0;
            try
            {
                //开启事务
                this.Db.Ado.BeginTran();
                res = this.Db.Storageable(StorageableObj).ExecuteCommand();
                //提交事务
                this.Db.Ado.CommitTran();
            }
            catch (Exception ex)
            {
                //事务回滚
                this.Db.Ado.RollbackTran();
                throw new Exception(ex.Message);
            }
            return res;
        }



        //删除一条
        public int Delete(T obj)
        {
            return this.Db.Deleteable<T>(obj).ExecuteCommand();
        }
        /// <summary>
        /// 批量删除多条,事务处理
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Delete(List<T> obj, out int 受影响行, out string msgErr)
        {
            bool rt = true;
            受影响行 = 0;
            msgErr = string.Empty;
            try
            {
                this.Db.Ado.BeginTran();
                受影响行 = this.Db.Deleteable<T>(obj).ExecuteCommand();
                this.Db.Ado.CommitTran();
            }
            catch (Exception ex)
            {
                this.Db.Ado.RollbackTran();
                msgErr = ex.Message;
                rt = false;
            }
            return rt;
        }

        /// <summary>
        /// 删除一条,事务处理
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Delete(T obj, out int 受影响行, out string msgErr)
        {
            bool rt = true;
            受影响行 = 0;
            msgErr = string.Empty;
            try
            {
                this.Db.Ado.BeginTran();
                受影响行 = this.Db.Deleteable<T>(obj).ExecuteCommand();
                this.Db.Ado.CommitTran();
            }
            catch (Exception ex)
            {
                this.Db.Ado.RollbackTran();
                msgErr = ex.Message;
                rt = false;
            }
            return rt;
        }

        public int Delete(Expression<Func<T, bool>> exp)
        {
            return this.Db.Deleteable<T>().Where(exp).ExecuteCommand();

        }


        //*****************************SQL语句

        /// <summary>
        /// sql语句 ,事务操作
        /// <para>dt : 影响到的数据</para>
        /// </summary>
        /// <param name="sqlStr"></param>
        public bool SQL_语句(string sqlStr, out DataTable dt, out string msgErr, bool 是否事务处理 = true)
        {
            msgErr = string.Empty;
            dt = new DataTable();
            bool rt = true;

            try
            {
                if (是否事务处理)
                {
                    this.Db.Ado.BeginTran();
                }
                dt = SQL_语句(sqlStr);
                if (是否事务处理)
                {
                    this.Db.Ado.CommitTran();
                }
            }
            catch (Exception ex)
            {
                if (是否事务处理)
                {
                    this.Db.Ado.RollbackTran();
                }
                rt = false;
                msgErr = ex.Message;
            }

            return rt;
        }

        /// <summary>
        /// 非事务操作
        /// </summary> 
        DataTable SQL_语句(string sqlStr)
        {
            DataTable dt = this.Db.SqlQueryable<T>(sqlStr).ToDataTable();
            return dt;
        }



        /// <summary>
        /// 多条保存到一个表
        /// <para>dt : 影响到的数据</para>
        /// </summary>
        /// <param name="lst_字段"></param>
        /// <param name="lst_内容"></param>
        public bool SQL_储存(string 表名, List<string> lst_字段, List<string[]> lst_内容, out DataTable dt, out string msgErr)
        {
            bool rt = true;
            dt = new DataTable();
            msgErr = string.Empty;
            try
            {
                dt = SQL_储存(表名, lst_字段, lst_内容);
            }
            catch (Exception ex)
            {
                msgErr = ex.Message;
                rt = false;
            }
            return rt;
        }

        public DataTable SQL_储存(string 表名, List<string> lst_字段, List<string[]> lst_内容)
        {


            string 字段 = string.Empty;
            for (int i = 0; i < lst_字段.Count; i++)
            {
                if (i == 0)
                {
                    字段 = lst_字段[i];
                }
                else
                {
                    字段 += "," + lst_字段[i];
                }
            }

            string sqlStr = string.Format("insert into {0}({1})", 表名, 字段);

            for (int i = 0; i < lst_内容.Count; i++)
            {
                string[] value = lst_内容[i];
                string xt = string.Empty;
                for (int a = 0; a < value.Length; a++)
                {
                    if (a == 0)
                    {
                        xt = string.Format("'{0}'", value[a]);
                    }
                    else
                    {
                        xt += string.Format(",'{0}'", value[a]);
                    }
                }

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
            return SQL_语句(sqlStr);

        }


        /// <summary>
        /// sql语句 ,非事务
        /// </summary>
        /// <param name="sqlStr"></param>
        public List<T> GetList_sql语句(string sqlStr)
        {
            return this.Db.SqlQueryable<T>(sqlStr).ToList();
        }

        /// <summary>
        /// sql语句 ,事务处理
        /// </summary>
        /// <param name="sqlStr"></param>
        public bool GetList_sql语句(string sqlStr, out List<T> list, out string msgErr, bool 是否事务处理 = false)
        {
            msgErr = string.Empty;
            bool rt = true;
            list = new List<T>();
            int res = 0;
            try
            {
                if (是否事务处理)
                {
                    this.Db.Ado.BeginTran();
                }
                list = this.Db.SqlQueryable<T>(sqlStr).ToList();
                if (是否事务处理)
                {
                    this.Db.Ado.CommitTran();
                }
            }
            catch (Exception ex)
            {
                if (是否事务处理)
                {
                    this.Db.Ado.RollbackTran();
                }
                rt = false;
                msgErr = ex.Message;
            }
            return rt;

        }



        /// <summary>
        /// 多条件查询,并将符合的值拼接成新值
        /// </summary>
        /// <param name="sql语句"></param>
        /// <param name="结果"></param>
        /// <param name="msgErr"></param>
        /// <returns></returns>
        public bool 执行(string sql语句, out DataTable row, out string msgErr, bool 是否事务处理 = true)
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
                if (是否事务处理)
                {
                    this.Db.Ado.BeginTran();
                }
                // 执行原生 SQL 查询
                row = this.Db.SqlQueryable<T>(sql语句).ToDataTable();
                if (是否事务处理)
                {
                    this.Db.Ado.CommitTran();
                }


            }
            catch (Exception ex)
            {
                if (是否事务处理)
                {
                    this.Db.Ado.RollbackTran();
                }
                rt = false;
                msgErr = ex.Message;
            }
            return rt;
        }


        public bool 执行(string sql语句, out int 受影响行, out string msgErr, bool 是否事务处理 = true)
        {
            bool rt = true;
            msgErr = string.Empty;
            受影响行 = 0;
            try
            {
                if (是否事务处理)
                {
                    this.Db.Ado.BeginTran();
                }
                // 执行原生 SQL 查询
                受影响行 = this.Db.Ado.ExecuteCommand(sql语句);
                if (是否事务处理)
                {
                    this.Db.Ado.CommitTran();
                }


            }
            catch (Exception ex)
            {
                if (是否事务处理)
                {
                    this.Db.Ado.RollbackTran();
                }
                rt = false;
                msgErr = ex.Message;
            }
            return rt;
        }

        public bool 执行(string sql语句, out string msgErr, bool 是否事务处理 = true)
        {

            bool rt = true;
            msgErr = string.Empty;

            try
            {
                if (是否事务处理)
                {
                    this.Db.Ado.BeginTran();
                }
                var result = this.Db.Ado.SqlQuery<string[]>(sql语句);
                if (是否事务处理)
                {
                    this.Db.Ado.CommitTran();
                }
            }
            catch (Exception ex)
            {
                if (是否事务处理)
                {
                    this.Db.Ado.RollbackTran();
                }
                rt = false;
                msgErr = ex.Message;
            }
            return rt;
        }



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


        /// <summary>
        /// "SELECT COUNT(*) FROM 表名"
        /// </summary>
        /// <param name="Sql语句"></param>
        /// <returns></returns>
        public long 获取总行数(string Sql语句)
        {
            long totalCount = this.Db.Ado.GetLong(Sql语句);
            return totalCount;
        }

        /// <summary>
        ///  "SELECT COUNT(*) FROM 表名"
        /// </summary>
        /// <param name="Sql语句"></param>
        /// <param name="totalCount"></param>
        /// <param name="msgErr"></param>
        /// <returns></returns>
        public bool 获取总行数(string Sql语句, out long totalCount, out string msgErr,bool 是否事务处理=false )
        {
            msgErr = string.Empty;
            bool rt = true;
            totalCount = 0;
            int res = 0;
            try
            {
                if (是否事务处理)
                {
                    this.Db.Ado.BeginTran();
                }
                totalCount = this.Db.Ado.GetLong(Sql语句);
                if (是否事务处理)
                {
                    this.Db.Ado.CommitTran();
                }
            }
            catch (Exception ex)
            {
                if (是否事务处理)
                {
                    this.Db.Ado.RollbackTran();
                }
                rt = false;
                msgErr = ex.Message;
            }
            return rt;
        }

        /// <summary>
        /// 输入表名就可以了
        ///  <para>使用语句"SELECT COUNT(*) FROM 表名"</para>
        /// </summary> 
        public bool 获取总行数1(string 表名, out long totalCount, out string msgErr,bool 是否事务处理=false )
        {
            msgErr = string.Empty;
            bool rt = true;
            totalCount = 0;
            int res = 0;
            try
            {
                if (是否事务处理)
                {
                    this.Db.Ado.BeginTran();
                }
                totalCount = this.Db.Ado.GetLong($"SELECT COUNT(*) FROM {表名}");
                if (是否事务处理)
                {
                    this.Db.Ado.CommitTran();
                }
            }
            catch (Exception ex)
            {
                if (是否事务处理)
                {
                    this.Db.Ado.RollbackTran();
                }
                rt = false;
                msgErr = ex.Message;
            }
            return rt;
        }




    }
}
