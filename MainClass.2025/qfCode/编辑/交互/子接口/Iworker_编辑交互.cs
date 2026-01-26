using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfCode
{
    public  interface Iworker_编辑交互
    {

        string[] Get配置文件_日期时间();
        string[] Get配置文件_班次();
        string[] Get配方文件();

        (bool s, string m, string v) 计算编码_对象(_配方文件_属性_ files, DateTime dates, string   objects);

    }
}
