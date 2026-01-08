using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace qf_Laser
{
    public class LaserMark_All单头
    {
        /// <summary>
        /// IWork_LaserMark 车间
        /// </summary>
        public IWork_LaserMark _IWork_LaserMark;
        public LaserMark_All单头(_em_打标卡类型_ type)
        {
            this._IWork_LaserMark = Create库(type);
        }

        /// <summary>
        /// 获取打标卡车间
        /// </summary> 
        public IWork_LaserMark Create库(_em_打标卡类型_ type)
        {
            switch (type)
            {
                case _em_打标卡类型_.Ezd2: return new MarkEzd_Ezd2();

                default: throw new Exception("Not type");
            }
            ;


        }


    }
}
