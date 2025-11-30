using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfCode
{
    internal class 编辑
    {
        private 编码 _sys;
        public 编辑(编码 _sys)
        {
            this._sys = _sys;
        }

        internal void 元素_添加<T>(ref _对象信息_ cfg, T 元素)
        {
            List<string> lst元素 = cfg.元素.ToList();
            string str = new json().json_生成<T>(元素);
            lst元素.Add(str);
            cfg.元素 = lst元素.ToArray();
        }
        internal void 元素_删除(ref _对象信息_ cfg, int 索引)
        {
            List<string> lst元素 = cfg.元素.ToList();
            lst元素.RemoveAt(索引);
            cfg.元素 = lst元素.ToArray();
        }
        internal void 元素_修改<T>(ref _对象信息_ cfg, T 元素, int 索引)
        {
            List<string> lst元素 = cfg.元素.ToList();
            string str = new json().json_生成<T>(元素);
            lst元素[索引] = str;
            cfg.元素 = lst元素.ToArray();
        }


        internal void 对象_添加<T>(ref _文件信息_ cfg, _对象信息_ 对象)
        {
            List<_对象信息_> lst对象 = cfg.对象.ToList();
            lst对象.Add(对象);
            cfg.对象 = lst对象.ToArray();
        }
        internal void 对象_删除(ref _文件信息_ cfg, int 索引)
        {
            List<_对象信息_> lst对象 = cfg.对象.ToList();
            lst对象.RemoveAt(索引);
            cfg.对象 = lst对象.ToArray();
        }
        internal void 对象_修改(ref _文件信息_ cfg, _对象信息_ 对象, int 索引)
        {
            List<_对象信息_> lst对象 = cfg.对象.ToList();
            lst对象[索引]= 对象;
            cfg.对象 = lst对象.ToArray();
        }






    }
}
