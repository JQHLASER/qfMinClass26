using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfWork
{

    public enum _BM_工具箱_
    {
        文本,
        序列号,
        日期,
        时间,
        关联对象,
        班次,
        Excel,
        csv,
        txt,
        通讯TCP,
        通讯COM,
    }

    public enum _BM_文件类型_
    {
        txt,
        ini,
        SqlLite,
        SqlServer,
        MySql,
        网络版,
    }



    public class _BM_日期更新时间点_
    {
        /// <summary>
        /// 日期更新的时间点
        /// </summary>
        public string 时间 { set; get; } = "00:00:00";
    }

    #region 日期时间编码

    public enum _BM_日期时间编码类型_
    {
        年4位,
        年2位,
        月,
        日,
        时,
        分,
        秒,
        星期,
        周
    }


    #endregion


    #region 元素属性

    public enum _BM_文本_
    {
        文本,
        换行符,
        空格,
        外部文本,
        特殊,
    }

    public enum _BM_日期_
    {
        年4位,
        年2位,
        月,
        日,
        天,
        星期,
        周,
    }

    public enum _BM_时间_
    {
        时24,
        时12,
        分,
        秒,
        毫秒,
    }

    public enum _BM_日期偏移类型_
    {
        无,
        年,
        月,
        日,
        周,
    }


    public enum _BM_序列号类型_
    {
        十进制,
        十六进制HEX,
        十六进制hex,

    }

    public enum _BM_序列号复位_
    {
        按最大,
        按年,
        按月,
        按日,
        按周,
        按班次,
    }

    public enum _BM_关联类型_
    {
        按位,
        按字符分割,
        按首尾字符,
    }

    public class _BM_序列号_加工_
    {
        public int 每个数量 { set; get; } = 1;
        public int 当前计数 { set; get; } = 0;
        //  public string 最后一次日期 { set; get; } = "2024-10-12 01:01:01";
    }


    public class _BM_关联对象_按位_
    {
        /// <summary>
        /// 从1开始;=0时表示取全部数据,
        /// </summary>
        public uint 开始位 { set; get; } = 0;
        /// <summary>
        /// =0时表示取剩下全部数据
        /// </summary>
        public uint 数量 { set; get; } = 0;
    }
    public class _BM_关联对象_按字符分割_
    {
        public string 分割符 { set; get; } = ";";
        public uint 索引 { set; get; } = 0;
    }
    public class _BM_关联对象_按首尾字符_
    {
        public string 首字符 { set; get; } = "";
        public string 尾字符 { set; get; } = "";
        public uint 索引 { set; get; } = 0;
    }


    public class _BM_班次_结构_
    {
        public string 代码 { set; get; } = "";
        public string 上班时间 { set; get; } = "08:00:00";
        public string 下班时间 { set; get; } = "16:00:00";
    }



    #endregion


    public enum _BM_序列号操作_
    {
        不操作,
        判断复位,
        强制复位,
        计算递增,
        计算递减,
        强制递增,
        强制递减,
        加工数量递增,
    }

    public enum _BM_生成数据类型_
    {
        /// <summary>
        /// 加工时生成
        /// </summary>
        加工递增,
        /// <summary>
        /// 测试时生成,并判断复位
        /// </summary>
        测试并判断复位,
 
    }




}
