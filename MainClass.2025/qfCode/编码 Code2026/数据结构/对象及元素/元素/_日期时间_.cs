using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfCode
{
    public class _日期时间_
    {
        public enum _em_编码类型_
        {
            年4位,
            年2位,
            月,
            日,
            时,
            分,
            秒,
            星期,
            周
        }

        public enum _em_日期_
        {
            年4位,
            年2位,
            月,
            日,
            天,
            星期,
            周,
        }

        public enum _em_时间_
        {
            时24,
            时12,
            分,
            秒,
            毫秒,
        }

        /// <summary>
        /// 无年月日周
        /// </summary>
        public enum _em_偏移类型_
        {
            无,
            年,
            月,
            日,
            周,
        }

    


    }

}
