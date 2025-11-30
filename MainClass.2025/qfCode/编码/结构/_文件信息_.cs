using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfCode
{
    public  class _文件信息_
    {
        /// <summary>
        /// 最后一次加工的信息
        /// </summary>
        public DateTime dateTimes { set; get; } = DateTime.Parse("2000-01-01 00:00:00");
        public _对象信息_[] 对象 { set; get; } = new _对象信息_[0];
        /// <summary>
        ///文件名: 班次信息文件
        /// </summary>
        public string Classes { set; get; } = "Default";
        /// <summary>
        /// 文件名: 日期更新时间点
        /// </summary>
        public string Times { set; get; } = "Default";
    }
}
