using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace qfCode
{
    public class 编码_计算
    {
        private 编码_ _sys;
        public 编码_计算(编码_ _sys_)
        {
            this._sys = _sys_;
        }

        #region 计算

        internal (bool s, string m, string v) 计算(_元素_.文本 info)
        {
            bool rt = true;
            string 结果 = info.内容;
            string msgErr = string.Empty;
            try
            {
                switch (info.types)
                {
                    case _文本_._em_文本_.文本:
                        结果 = info.内容;
                        break;
                    case _文本_._em_文本_.换行:
                        结果 = "\n";
                        break;
                    case _文本_._em_文本_.空格:
                        结果 = " ";
                        break;
                }
                info.内容 = 结果;

            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }
            return (rt, msgErr, 结果);
        }


        internal (bool s, string m, string v) 计算(_班次_[] info, DateTime 时间)
        {
            bool rt = true;
            string 结果 = "";
            string msgErr = string.Empty;

            try
            {
                if (info == null || info.Length == 0)
                {
                    return (false, "班次信息为空", "");
                }

                TimeSpan 当前时间 = 时间.TimeOfDay;

                foreach (var s in info)
                {
                    string 代码 = s.代码.Trim();
                    TimeSpan 上班时间 = TimeSpan.Parse(s.上班时间.Trim());
                    TimeSpan 下班时间 = TimeSpan.Parse(s.下班时间.Trim());

                    // 调试输出
                    //  MessageBox.Show($"当前:{当前时间} 上班:{上班时间} 下班:{下班时间}");

                    if (上班时间 <= 下班时间)
                    {
                        // 正常班次
                        if (当前时间 >= 上班时间 && 当前时间 <= 下班时间)
                        {
                            结果 = 代码;
                            //  MessageBox.Show(代码+结果);
                            break;
                        }
                    }
                    else
                    {
                        // 跨天班次
                        if (当前时间 >= 上班时间 || 当前时间 <= 下班时间)
                        {
                            结果 = 代码;
                            break;
                        }
                    }
                }

                if (string.IsNullOrWhiteSpace(结果))
                {
                    rt = false;
                    msgErr = "未计算出结果";
                }
            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }


            return (rt, msgErr, 结果);
        }




        /// <summary>
        /// 除了强制复位,其它都带判断复位了, 
        /// <para>非强制的结果为计算前的结果,强制为计算后的结果</para>
        /// </summary>     
        internal (bool s, string m, string v) 计算(ref _元素_.序列号 info, _序列号_._em_操作_ 操作, DateTime 时间, DateTime 最后一次加工时间, _班次_[] 班次结构)
        {

            string msgErr = string.Empty;
            bool rt = true;
            string 结果 = "";

            //如果不使能加工,但每个加工数量又>1时,初始化一下
            if (!this._sys._功能.序列号.加工 && info.加工.数量 != 1)
            {
                info.加工.数量 = 1;
                info.加工.计数 = 0;
            }

            try
            {


                new 计算_序列号().序列号_转换成可计算值(info, out 计算_序列号._fz_序列号_ 序号fz);

                switch (操作)
                {
                    //不操作
                    case _序列号_._em_操作_.不操作:
                        new 计算_序列号().序列号结果(ref info, 序号fz, out 结果);
                        break;

                    //判断复位
                    case _序列号_._em_操作_.判断复位:
                        new 计算_序列号().序列号复位(ref info, 最后一次加工时间, 时间, 班次结构, 序号fz);
                        new 计算_序列号().序列号结果(ref info, 序号fz, out 结果);
                        break;

                    //强制复位
                    case _序列号_._em_操作_.强制复位:

                        info.加工.计数 = 0;
                        序号fz.当前序号 = 序号fz.开始序号;
                        new 计算_序列号().序列号结果(ref info, 序号fz, out 结果);
                        break;

                    //计算递增
                    case _序列号_._em_操作_.计算递增:

                        new 计算_序列号().序列号复位(ref info, 最后一次加工时间, 时间, 班次结构, 序号fz);
                        new 计算_序列号().序列号结果(ref info, 序号fz, out 结果);
                        new 计算_序列号().序列号递增计算(ref 序号fz);


                        break;

                    //计算递减
                    case _序列号_._em_操作_.计算递减:
                        new 计算_序列号().序列号复位(ref info, 最后一次加工时间, 时间, 班次结构, 序号fz);
                        new 计算_序列号().序列号结果(ref info, 序号fz, out 结果);
                        new 计算_序列号().序列号递减计算(ref 序号fz);

                        break;

                    //强制递增
                    case _序列号_._em_操作_.强制递增:
                        //  序列号复位(ref info, 文件, 时间, 班次结构, 序号结构);
                        new 计算_序列号().序列号强制递增(ref 序号fz);
                        new 计算_序列号().序列号结果(ref info, 序号fz, out 结果);
                        break;

                    //强制递减
                    case _序列号_._em_操作_.强制递减:
                        // 序列号复位(ref info, 文件, 时间, 班次结构, 序号结构);
                        new 计算_序列号().序列号强制递减(ref 序号fz);
                        new 计算_序列号().序列号结果(ref info, 序号fz, out 结果);
                        break;
                    case _序列号_._em_操作_.加工数量递增:
                        new 计算_序列号().序列号复位(ref info, 最后一次加工时间, 时间, 班次结构, 序号fz);
                        new 计算_序列号().序列号结果(ref info, 序号fz, out 结果);
                        int a = new 计算_序列号().序列号_加工数量递增计算_(ref 序号fz);
                        break;

                }

                if (new 计算_序列号().序列号大小计算(序号fz) != 0)
                {
                    序号fz.当前序号 = 序号fz.开始序号;
                }
                new 计算_序列号().序列号_转换成最终值(ref info, 序号fz);
            }
            catch (Exception ex)
            {
                msgErr = ex.Message;
                rt = false;
            }
            return (rt, msgErr, 结果);
        }

        /// <summary>
        /// <para>编码文件 : 不含路径,但含后缀</para>
        /// </summary> 
        internal (bool s, string m, string v) 计算(_元素_.日期 info, DateTime 时间)
        {
            bool rt = true;
            string msgErr = string.Empty;
            string 结果 = "";

            _日期时间_._em_编码类型_ 编码类型 = _日期时间_._em_编码类型_.年4位;
            try
            {
                DateTime nows = 时间;
                if (this._sys._功能.日期时间.偏移计算)
                {
                    nows = new 计算_日期时间().偏移计算(this._sys, 时间, info);
                }

                switch (info.types)
                {
                    case _日期时间_._em_日期_.年4位:
                        结果 = nows.ToString("yyyy").ToString();
                        编码类型 = _日期时间_._em_编码类型_.年4位;
                        break;
                    case _日期时间_._em_日期_.年2位:
                        结果 = nows.ToString("yy").ToString();
                        编码类型 = _日期时间_._em_编码类型_.年2位;
                        break;
                    case _日期时间_._em_日期_.月:
                        结果 = nows.ToString("MM").ToString();
                        编码类型 = _日期时间_._em_编码类型_.月;
                        break;
                    case _日期时间_._em_日期_.日:
                        结果 = nows.ToString("dd").ToString();
                        编码类型 = _日期时间_._em_编码类型_.日;
                        break;
                    case _日期时间_._em_日期_.天:
                        结果 = new qfmain.日期时间_().Get_days(nows).ToString("000");
                        编码类型 = _日期时间_._em_编码类型_.日;
                        break;
                    case _日期时间_._em_日期_.星期:
                        结果 = new qfmain.日期时间_().Get_星期(nows).ToString("0");
                        编码类型 = _日期时间_._em_编码类型_.星期;
                        break;
                    case _日期时间_._em_日期_.周:
                        结果 = new qfmain.日期时间_().Get_weeks(nows).ToString("00");
                        编码类型 = _日期时间_._em_编码类型_.周;
                        break;
                }

                if (this._sys._功能.日期时间.配置编码)
                {
                    var rts = this._sys._配置文件.Get_日期时间(info.配置, $"{编码类型}", 结果);
                    rt = rts.s;
                    msgErr = rts.m;
                    结果 = rts.cfg;
                }

            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }
            return (rt, msgErr, 结果);
        }

        /// <summary>
        /// <para>编码文件 : 不含路径,但含后缀</para>
        /// </summary> 
        internal (bool s, string m, string v) 计算(_元素_.时间 info, DateTime 时间)
        {
            bool rt = true;
            string msgErr = string.Empty;
            string 结果 = "";
            _日期时间_._em_编码类型_ 编码类型 = _日期时间_._em_编码类型_.时;
            try
            {
                switch (info.types)
                {
                    case _日期时间_._em_时间_.时24:
                        结果 = 时间.ToString("HH").ToString();
                        编码类型 = _日期时间_._em_编码类型_.时;
                        break;
                    case _日期时间_._em_时间_.时12:
                        结果 = 时间.ToString("hh").ToString();
                        编码类型 = _日期时间_._em_编码类型_.时;
                        break;
                    case _日期时间_._em_时间_.分:
                        结果 = 时间.ToString("mm").ToString();
                        编码类型 = _日期时间_._em_编码类型_.分;
                        break;
                    case _日期时间_._em_时间_.秒:
                        结果 = 时间.ToString("ss").ToString();
                        编码类型 = _日期时间_._em_编码类型_.秒;
                        break;
                    case _日期时间_._em_时间_.毫秒:
                        结果 = 时间.ToString("fff").ToString();
                        break;

                }

                if (this._sys._功能.日期时间.配置编码 && info.types != _日期时间_._em_时间_.毫秒)
                {
                    var rts = this._sys._配置文件.Get_日期时间(info.配置, $"{编码类型}", 结果);
                    rt = rts.s;
                    msgErr = rts.m;
                    结果 = rts.cfg;
                }

            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }

            return (rt, msgErr, 结果);
        }

        /// <summary>
        ///  结果:正确时为计算结果,错误时为错误原因
        /// </summary> 
        internal (bool s, string m, string v) 计算(_元素_.关联对象 info, string 关联对象的内容)
        {
            string msgErr = string.Empty;
            string 结果 = string.Empty;
            if (string.IsNullOrEmpty(info.对象))
            {
                msgErr = Language_.Get语言("关联的对象名不能为空值");
                return (false, msgErr, 结果);
            }
            bool rt = true;
            string 数据 = 关联对象的内容;

            try
            {
                switch (info.types)
                {
                    case _关联对象_._em_类型_.全部:
                        结果 = 关联对象的内容;
                        break;

                    case _关联对象_._em_类型_.按位:

                        #region 按位

                        var rt按位 = new 计算_关联对象().按位(info, 关联对象的内容);
                        rt = rt按位.s;
                        msgErr = rt按位.m;
                        结果 = rt按位.v;

                        #endregion

                        break;

                    case _关联对象_._em_类型_.按字符:

                        #region 按字符 

                        var rt按字符 = new 计算_关联对象().按字符(info, 关联对象的内容);
                        rt = rt按字符.s;
                        msgErr = rt按字符.m;
                        结果 = rt按字符.v;

                        #endregion

                        break;
                    case _关联对象_._em_类型_.按首尾:

                        #region 按首尾

                        var rt按首尾 = new 计算_关联对象().按首尾(info, 关联对象的内容);
                        rt = rt按首尾.s;
                        msgErr = rt按首尾.m;
                        结果 = rt按首尾.v;

                        #endregion

                        break;
                }

            }
            catch (Exception ex)
            {
                msgErr = ex.Message;
                rt = false;
            }
            return (rt, msgErr, 结果);
        }


        #endregion


        #region 计算

        internal (bool s, string m, string v, _元素_.文本 cfg) 文本(string jsonStr)
        {
            var rt = new Json序列化().转成Json<_元素_.文本>(jsonStr);
            _元素_.文本 cfg = rt.cfg;
            if (!rt.s)
            {
                return (rt.s, rt.m, "", cfg);
            }
            var rtT = new 编码_计算(this._sys).计算(rt.cfg);
            return (rtT.s, rtT.m, rtT.v, cfg);
        }

        internal (bool s, string m, string v) 序列号(ref string jsonStr, _序列号_._em_操作_ 操作, DateTime 时间, DateTime 最后一次加工时间, _班次_[] 班次结构)
        {
            var rt = new Json序列化().转成Json<_元素_.序列号>(jsonStr);
            if (!rt.s)
            {
                return (rt.s, rt.m, "");
            }
            _元素_.序列号 cfg = rt.cfg;
            var rtSn = new 编码_计算(this._sys).计算(ref cfg, 操作, 时间, 最后一次加工时间, 班次结构);
            jsonStr = new Json序列化().转成String<_元素_.序列号>(cfg);
            return rtSn;
        }

        internal (bool s, string m, string v) 时间(string jsonStr, DateTime 时间)
        {
            var rt = new Json序列化().转成Json<_元素_.时间>(jsonStr);
            if (!rt.s)
            {
                return (rt.s, rt.m, "");
            }
            return new 编码_计算(this._sys).计算(rt.cfg, 时间);
        }

        internal (bool s, string m, string v) 日期(string jsonStr, DateTime 时间)
        {
            var rt = new Json序列化().转成Json<_元素_.日期>(jsonStr);
            if (!rt.s)
            {
                return (rt.s, rt.m, "");
            }
            return new 编码_计算(this._sys).计算(rt.cfg, 时间);
        }

        internal (bool s, string m, string v) 班次(string jsonStr, _班次_[] info, DateTime 时间)
        {
            // var rt = new Json序列化().转成Json<_元素_.班次>(jsonStr);
            return new 编码_计算(this._sys).计算(info, 时间);
        }

        internal (bool s, string m, string v) 关联对象(string jsonStr, List<_对象_内容_> lst)
        {
            var rt = new Json序列化().转成Json<_元素_.关联对象>(jsonStr);
            if (!rt.s)
            {
                return (rt.s, rt.m, "");
            }
            string v = "";
            foreach (var s in lst)
            {
                if (s.对象.对象名 == rt.cfg.对象)
                {
                    v = s.Value;
                    break;
                }
            }
            return new 编码_计算(this._sys).计算(rt.cfg, v);
        }




        #endregion

    }
}
