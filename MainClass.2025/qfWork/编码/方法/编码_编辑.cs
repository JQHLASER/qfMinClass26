using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfWork
{
    internal class 编码_编辑
    {
        private 编码 _BM_sys;
        public 编码_编辑(编码 _bM_sys)
        {
            this._BM_sys = _bM_sys;
        }

        internal void 元素_添加<T>(ref _BM_对象信息_ cfg, T 元素)
        {
            List<string> lst元素 = cfg.元素.ToList();
            string str = new 编码_json().json_生成<T>(元素);
            lst元素.Add(str);
            cfg.元素 = lst元素.ToArray();
        }
        internal void 元素_删除(ref _BM_对象信息_ cfg, int 索引)
        {
            List<string> lst元素 = cfg.元素.ToList();
            lst元素.RemoveAt(索引);
            cfg.元素 = lst元素.ToArray();
        }
        internal void 元素_修改<T>(ref _BM_对象信息_ cfg, T 元素, int 索引)
        {
            List<string> lst元素 = cfg.元素.ToList();
            string str = new 编码_json().json_生成<T>(元素);
            lst元素[索引] = str;
            cfg.元素 = lst元素.ToArray();
        }


        internal void 对象_添加<T>(ref _BM_文件信息_ cfg, _BM_对象信息_ 对象)
        {
            List<_BM_对象信息_> lst对象 = cfg.对象.ToList();
            lst对象.Add(对象);
            cfg.对象 = lst对象.ToArray();
        }
        internal void 对象_删除(ref _BM_文件信息_ cfg, int 索引)
        {
            List<_BM_对象信息_> lst对象 = cfg.对象.ToList();
            lst对象.RemoveAt(索引);
            cfg.对象 = lst对象.ToArray();
        }
        internal void 对象_修改(ref _BM_文件信息_ cfg, _BM_对象信息_ 对象, int 索引)
        {
            List<_BM_对象信息_> lst对象 = cfg.对象.ToList();
            lst对象[索引]= 对象;
            cfg.对象 = lst对象.ToArray();
        }






    }
}
