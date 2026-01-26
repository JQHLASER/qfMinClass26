using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfCode
{
    public class 编辑交互_本地 : Iworker_编辑交互
    {
        编辑_ _编辑;
        public 编辑交互_本地(编辑_ 编辑)
        {
            this._编辑 = 编辑;
        }

        public string[] Get配置文件_日期时间()
        {
            return this._编辑._编码._文件类.Get目录_日期时间();
        }
        public string[] Get配置文件_班次()
        {
            return this._编辑._编码._文件类.Get目录_班次();
        }
        public string[] Get配方文件()
        {
            return this._编辑._编码._文件类.Get目录_配方();
        }

        public (bool s, string m, string v) 计算编码_对象(_配方文件_属性_ 配方,DateTime dates, string  对象名)
        {
            (bool s, string m, string v) rt = (true, "", "");

            this._编辑._编码.计算编码_对象(配方, dates,对象名);


            return rt;
        }



    }
}
