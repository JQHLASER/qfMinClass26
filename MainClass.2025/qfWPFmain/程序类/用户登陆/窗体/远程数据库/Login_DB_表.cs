using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace qfWPFmain 
{
    internal class Login_DB_表
    {
        /// <summary>
        /// 表名
        /// </summary>
        public class loginUserInfo
        {
            [SugarColumn(IsPrimaryKey = true)]
            public string infoName { set; get; } = "loginUser";

            public string userInfo { set; get; } = "";
        }


    }
}

/* 新建表
    CREATE TABLE loginUserInfo
    (
           infoName [varchar] (255) PRIMARY KEY, --主键
           userInfo [varchar](MAX) NULL
         
    );

*/





 