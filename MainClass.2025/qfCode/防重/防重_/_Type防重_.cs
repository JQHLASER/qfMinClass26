using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfCode
{
    public class _Type防重_
    {

        public enum _em_数据库格式_
        {
            SQLite,
            SQLserver,
        }


        public class _查询信息_
        {
            public DateTime start { set; get; }
            public DateTime end { set; get; }

            public string 内容 { set; get; }  
        }


    }
}
