using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace qfCode
{
    public class 编辑交互_本地 : Iworker_编辑交互
    {
        编辑_ _编辑;
        public 编辑交互_本地(编辑_ 编辑)
        {
            this._编辑 = 编辑;
        }

        public string[] Get目录_配置文件_日期时间()
        {
            var rt = this._编辑._编码._配置文件.Get目录_日期时间();
            return rt.cfg;
        }
        public string[] Get目录_配置文件_班次()
        {
            var rt = this._编辑._编码._配置文件.Get目录_班次();
            return rt.cfg;
        }
        public (bool s, string m, string[] v) Get目录_配方文件()
        {
            return this._编辑._编码._配方文件操作._Iwork文件.Get目录();
        }

        public (bool s, string m) 配方_保存(_配方文件_属性_ 配方, string 配方名称, DateTime dates)
        {
            return this._编辑._编码.配方_保存(配方, 配方名称, dates);
        }

        public (bool s, string m) 配方_复制(string 配方名称, string New配方名称)
        {
            return this._编辑._编码.配方_复制(配方名称, New配方名称);
        }

        public (bool s, string m) 配方_删除(string 配方名称)
        {
            return this._编辑._编码.配方_删除(配方名称);
        }

        public (bool s, string m, _配方文件_属性_ cfg) 配方_打开(string 配方名称)
        {
            return this._编辑._编码.配方_打开(配方名称);
        }

        public (bool s, string m, string v) 计算编码_对象(_配方文件_属性_ 配方, DateTime dates, string 对象名)
        {
            string v = string.Empty;
            var rt = this._编辑._编码.计算编码_对象(配方, dates, 对象名);
            if (rt.s)
            {
                foreach (var s in rt.lstObject)
                {
                    if (对象名 == s.对象.对象名)
                    {
                        v = s.Value;
                        break;
                    }
                }
            }

            return (rt.s, rt.m, v);
        }
        public (bool s, string m, _元素_Str_ cfg) 计算元素(_配方文件_属性_ 配方文件, List<_对象_内容_> lst对象内容, DateTime dates, _对象_ 对象, string Json元素)
        {
            return this._编辑._编码.计算元素(配方文件, lst对象内容, dates, 对象, Json元素);
        }
        public (bool s, string m, List<_对象_内容_> lstObject) 计算编码(string 配方文件名, _配方文件_属性_ 配方文件, DateTime dates, _em_计算类型_ 计算类型, bool Is计算完保存 = false)
        {
            return this._编辑._编码.计算编码(配方文件名, 配方文件, dates, 计算类型, Is计算完保存);
        }

    }
}
