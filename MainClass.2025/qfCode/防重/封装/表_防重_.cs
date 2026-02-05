using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfCode
{
    public class 表_防重_
    {
        /// <summary>
        /// /防重表
        /// </summary>
        public class FC26
        {
            [SugarColumn(IsPrimaryKey = true)]
            public string GUID { set; get; } = "";
            public string 时间 { set; get; } = "";
            public string 内容 { set; get; } = "";
            public string 信息 { set; get; } = "";

        }
    }
}
