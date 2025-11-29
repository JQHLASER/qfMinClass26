using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfWork
{
    public class IO类_
    {
        public bool 端口是否有效(int 端口, ushort 最小端口号, ushort 最大端口号)
        {
            bool rt = false;

            if (端口 >= 最小端口号 && 端口 <= 最大端口号)
            {
                rt = true;
            }

            return rt;
        }



    }
}
