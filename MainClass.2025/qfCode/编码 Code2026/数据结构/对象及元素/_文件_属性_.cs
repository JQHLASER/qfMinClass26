using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfCode
{
    public class _文件_属性_
    {
        /// <summary>
        /// 最后一次加工时间
        /// </summary>
        public string Datetimes { set; get; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        public string 备注 { set; get; } = ""; 
        public _对象_[] 对象 { set; get; } = new _对象_[0] ; 
        /// <summary>
        /// 日期更新的时间点
        /// </summary>
        public string 更新时间 { set; get; } = "00:00:00";
         

    }
}
