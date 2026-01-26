using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfCode
{
    internal class Err
    {

        /// <summary>
        /// 索引从1开始
        /// </summary> 
        internal bool Err_关联对象_索引超出范围(uint 总数量, uint 索引, out string msgErr)
        {
            msgErr = "";
            if (索引 <= 0)
            {
                msgErr = Language_.Get语言("索引超出范围");
                return false;
            }
            return true;
        }



    }
}
