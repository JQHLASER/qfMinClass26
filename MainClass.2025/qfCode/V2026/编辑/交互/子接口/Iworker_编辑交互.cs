using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfCode
{
    public  interface Iworker_编辑交互
    {

        string[] Get目录_配置文件_日期时间();
        string[] Get目录_配置文件_班次();
        string[] Get目录_配方文件();
          
        (bool s, string m) 配方_保存(_配方文件_属性_ 配方, string 配方名称, DateTime dates);
        (bool s, string m) 配方_复制(string 配方名称, string New配方名称);
        (bool s, string m) 配方_删除(string 配方名称);
        (bool s, string m, _配方文件_属性_ cfg) 配方_打开(string 配方名称);

        (bool s, string m, string v) 计算编码_对象(_配方文件_属性_ files, DateTime dates, string objects);
        (bool s, string m, _元素_Str_ cfg) 计算元素(_配方文件_属性_ 配方文件, List<_对象_内容_> lst对象内容, DateTime dates, _对象_ 对象, string Json元素);
        (bool s, string m, List<_对象_内容_> lstObject) 计算编码(string 配方文件名, _配方文件_属性_ 配方文件, DateTime dates, _em_计算类型_ 计算类型, bool Is计算完保存 = false);


    }
}
