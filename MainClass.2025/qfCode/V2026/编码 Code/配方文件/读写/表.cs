using qfmain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfCode
{
    public class 表
    {
        public class Code26
        {
            /// <summary>
            /// 文件名称,唯一
            /// </summary>
            public string FileName { set; get; } = "";
            /// <summary>
            /// json内容
            /// </summary>
            public string CodeValue { set; get; } = "";
        }
        /*  SQLserver风格
            CREATE TABLE Code26
            (
                   FileName[varchar](255) PRIMARY KEY, //主键 
                   CodeValue[varchar](TEXT) NULL, //长文本 
            );

        /*
         SQLite风格
            CREATE TABLE Code26 
            (
                FileName  INTEGER PRIMARY KEY ,  -- SQLite中主键 
                CodeValue  TEXT NULL  -- 不指定Text长度 
            ); 
         */
    }
}
