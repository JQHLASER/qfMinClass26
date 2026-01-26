using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfCode
{
    /// <summary>
    /// 编辑
    /// </summary>
    internal class 编辑交互_统一接口
    {
        Iworker_编辑交互 _Iworker;


        public 编辑交互_统一接口(type_编辑._文件类型_ 文件类型, 编辑_ 编辑)
        {
            switch (文件类型)
            {
                case type_编辑._文件类型_.本地:
                    this._Iworker = new 编辑交互_本地(编辑); break;
            }
        }

        public string[] Get目录_日期时间()
        {
            return _Iworker.Get配置文件_日期时间();
        }

        public string[] Get目录_班次()
        {
            return _Iworker.Get配置文件_班次();
        }

        public string[] Get目录_配方文件()
        {
            return _Iworker.Get配方文件();
        }

        public (bool s, string m, string v) 计算编码_对象(_配方文件_属性_ 配方, DateTime dates, string 对象名)
        {
            (bool s, string m, string v) rt = _Iworker.计算编码_对象(配方, dates, 对象名); 
            return rt;
        }






    }
}
