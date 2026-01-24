using Sunny.UI;
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
        public List<_对象_> 对象 { set; get; } = new List<_对象_>();
        /// <summary>
        /// 日期更新的时间点
        /// </summary>
        public string 更新时间 { set; get; } = "00:00:00";
        /// <summary>
        /// 班次规则的配置文件
        /// </summary>
        public string 班次文件 { set; get; } = 常量 .配置文件名_默认 ;



        public _文件_属性_ Clone()
        {
            return new _文件_属性_
            {
                Datetimes = this.Datetimes,
                备注 = this.备注,
                对象 = this.对象 is null ? null : this.对象.Select(obj => obj.Clone()).ToList(),
                更新时间 = this.更新时间,
                班次文件 = this.班次文件,
            };
        }


    }
}
