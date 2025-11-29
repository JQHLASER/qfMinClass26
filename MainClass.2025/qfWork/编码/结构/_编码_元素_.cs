using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfWork
{
     
        #region 元素结构体


        public class _BM_元素_文本_
        {
            public _BM_工具箱_ 工具 { set; get; } = _BM_工具箱_.文本;
            public _BM_文本_ 类型 { set; get; } = _BM_文本_.文本;
            public string 内容 { set; get; } = "TEXT";
        }
        public class _BM_元素_序列号_
        {
            public _BM_工具箱_ 工具 { set; get; } = _BM_工具箱_.序列号;
            public _BM_序列号类型_ 类型 { set; get; } = _BM_序列号类型_.十进制;
            public string 当前序号 { set; get; } = "0001";
            public string 开始序号 { set; get; } = "0001";
            public string 最大序号 { set; get; } = "9999";
            public int 递增量 { set; get; } = 1;
            public _BM_序列号_加工_ 加工 { set; get; } = new _BM_序列号_加工_();
            public _BM_序列号复位_ 复位方式 { set; get; } = _BM_序列号复位_.按日;

        }
        public class _BM_元素_日期_
        {
            public _BM_工具箱_ 工具 { set; get; } = _BM_工具箱_.日期;
            public _BM_日期_ 类型 { set; get; } = _BM_日期_.年4位;
            public _BM_日期偏移类型_ 偏移类型 { set; get; } = _BM_日期偏移类型_.无;
            public int 偏移值 { set; get; } = 0;
            public string 编码文件 { set; get; } = "default.ini";

        }
        public class _BM_元素_时间_
        {
            public _BM_工具箱_ 工具 { set; get; } = _BM_工具箱_.时间;
            public _BM_时间_ 类型 { set; get; } = _BM_时间_.时24;
            public string 编码文件 { set; get; } = "default.ini";
        }
        public class _BM_元素_关联对象_
        {
            public _BM_工具箱_ 工具 { set; get; } = _BM_工具箱_.关联对象;
            public _BM_关联类型_ 类型 { set; get; } = _BM_关联类型_.按位;
            public string 关联对象 { set; get; } = "";
            public string 参数 { set; get; } = "";

        }     
        public class _BM_元素_班次_
        {
            public _BM_工具箱_ 工具 { set; get; } = _BM_工具箱_.班次;
        }





        #endregion




 
}
