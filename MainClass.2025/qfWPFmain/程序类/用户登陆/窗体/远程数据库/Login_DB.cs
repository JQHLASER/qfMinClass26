using qfWork;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfWPFmain
{
    internal class Login_DB : IDisposable
    {
        private qfmain.SqlSugar_DB db_sys;
        private string _path = qfmain.软件类.Files_Cfg.Files_Config + "\\loginCfg.txt";

        public Login_DB()
        {
            qfmain.SqlSugar_DB._info_参数_ 参数 = new qfmain.SqlSugar_DB._info_参数_();
            参数.数据库类型 = qfmain.SqlSugar_DB.enum数据库类型.SqlServer;



            this.db_sys = new qfmain.SqlSugar_DB(this._path, 参数);
            Get表_loginUserInfo();
        }



        #region 表申明

        internal qfmain.SqlSugarClient<Login_DB_表.loginUserInfo> _Table_loginUserInfo;
        qfmain.SqlSugarClient<Login_DB_表.loginUserInfo> Get表_loginUserInfo()
        {
            _Table_loginUserInfo = new qfmain.SqlSugarClient<Login_DB_表.loginUserInfo>(this.db_sys);
            return _Table_loginUserInfo;
        }

        #endregion




        public void Dispose()
        {
            this.db_sys.Dispose();
        }

        internal bool 添加(Login_DB_表.loginUserInfo[] loginUserInfo, out string msgErr)
        {
            bool rt = this._Table_loginUserInfo.Storageable(loginUserInfo.ToList(), out int 受影响行, out msgErr);
            if (rt && 受影响行 == 0)
            {
                rt = false;
                msgErr = "受影响0行";
            }
            else if (rt)
            {
                msgErr = $"受影响{受影响行}行";
            }

            return rt;
        }
        internal bool 查询(out Login_DB_表.loginUserInfo[] loginUserInfo, out string msgErr)
        {
            bool rt = this._Table_loginUserInfo.GetList(out List<Login_DB_表.loginUserInfo> lst, out msgErr);
            loginUserInfo = lst.ToArray();
            return rt;
        }



    }
}
