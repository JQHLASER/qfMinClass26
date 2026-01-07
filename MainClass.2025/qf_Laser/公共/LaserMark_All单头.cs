using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qf_Laser
{
    public class LaserMark_All单头
    { 
        public IWork Create(_em_打标卡类型_ type)
        {
            switch (type)
            {
                case _em_打标卡类型_.Ezd2: return new MarkEzd_Ezd2();

                default: throw new Exception("Not type");
            };
           

        }


    }
}
