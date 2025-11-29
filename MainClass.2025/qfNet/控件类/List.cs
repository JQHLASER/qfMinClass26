using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfNet
{
    public class List
    {

        /// <summary>
        /// 重载...int/string/short/byte
        /// </summary>
        /// <param name="lst"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public int 统计指定值的数量(List<int> lst, int value)
        {
            return lst.Count(s => s == value);
        }


        /// <summary>
        /// 重载...int/string/short/byte
        /// </summary>
        /// <param name="lst"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public int 统计指定值的数量(List<string> lst, string value)
        {
            return lst.Count(s => s == value);
        }


        /// <summary>
        /// 重载...int/string/short/byte
        /// </summary>
        /// <param name="lst"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public int 统计指定值的数量(List<short> lst, short value)
        {
            return lst.Count(s => s == value);
        }




        /// <summary>
        /// 重载...int/string/short/byte
        /// </summary>
        /// <param name="lst"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public int 统计指定值的数量(List<byte> lst, byte value)
        {
            return lst.Count(s => s == value);
        }










    }
}
