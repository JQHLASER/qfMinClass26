using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfCode 
{
    public class 文件目录_本地 : Iworker_文件目录
    {
        编辑_ _编辑;
        public 文件目录_本地(编辑_ 编辑)
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


    }
}
