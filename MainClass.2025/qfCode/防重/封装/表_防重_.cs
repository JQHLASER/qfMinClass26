using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            [DisplayName("时间")]
            public string 时间 { set; get; } = "";
            [DisplayName("内容")]
            public string 内容 { set; get; } = "";
            [DisplayName("信息")]
            public string 信息 { set; get; } = "";

        }
    }
}
