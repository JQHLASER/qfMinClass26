
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfCode
{
    internal class 编码计算
    {
        private 编码 _sys;
        public 编码计算(编码 _sys_)
        {
            this._sys = _sys_;
        }

        internal bool 计算(_元素_文本_ info,  out string 结果, out string msgErr)
        {
            bool rt = true;
            结果 = info.内容;
            msgErr = string.Empty;
            try
            {
                switch (info.类型)
                {
                    case _文本_.文本:
                        结果 = info.内容;
                        break;
                    case _文本_.换行符:
                        结果 = "\n";
                        break;
                    case _文本_.空格:
                        结果 = " ";
                        break;
                    case _文本_.外部文本:
                        break;
                }
                info.内容 = 结果;
                
            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }

            return rt;
        }
        internal bool 计算(_班次_结构_[] info, DateTime 时间, out string 结果, out string msgErr)
        {
            bool rt = true;
            结果 = "";
            msgErr = string.Empty;
            try
            {
                foreach (var s in info)
                {
                    _班次_结构_ info班次S = new _班次_结构_();
                    string 代码 = s.代码;
                    DateTime 上班时间 = DateTime.Parse(s.上班时间);
                    DateTime 下班时间 = DateTime.Parse(s.下班时间);

                    //开始计算结果
                    DateTime 当前时间 = DateTime.Parse(时间.ToString("HH:mm:ss"));

                    if (上班时间 <= 下班时间)
                    {
                        if (当前时间 >= 上班时间 && 当前时间 <= 下班时间)
                        {
                            结果 = 代码;
                        }
                    }
                    else
                    {
                        if (当前时间 <= 上班时间 && 当前时间 <= 下班时间)
                        {
                            结果 = 代码;
                        }
                        else if (当前时间 >= 上班时间 && 当前时间 >= 下班时间)
                        {
                            结果 = 代码;
                        }
                    }


                }

            }
            catch (Exception ex)
            {
                msgErr = ex.Message;
                rt = false;
            }

            return rt;
        }

        /// <summary>
        /// 除了强制复位,其它都带判断复位了, 
        /// <para>非强制的结果为计算前的结果,强制为计算后的结果</para>
        /// </summary>     
        internal bool 计算(ref _元素_序列号_ info, _序列号操作_ 操作, DateTime 时间, DateTime 最后一次加工时间, _班次_结构_[] 班次结构, out string 结果, out string msgErr)
        {

            msgErr = string.Empty;
            bool rt = true;
            结果 = "";

            //如果不使能加工,但每个加工数量又>1时,初始化一下
            if (!this._sys._功能.序列号.每个加工数量 && info.加工.每个数量 != 1)
            {
                info.加工.每个数量 = 1;
                info.加工.当前计数 = 0;
            }

            try
            {


                序列号_转换成可计算值(info, out _fz_序列号_ 序号fz);

                switch (操作)
                {
                    //不操作
                    case _序列号操作_.不操作:
                        序列号结果(ref info, 序号fz, out 结果);
                        break;

                    //判断复位
                    case _序列号操作_.判断复位:
                        序列号复位(ref info, 最后一次加工时间, 时间, 班次结构, 序号fz);
                        序列号结果(ref info, 序号fz, out 结果);
                        break;

                    //强制复位
                    case _序列号操作_.强制复位:

                        info.加工.当前计数 = 0;
                        序号fz.当前序号 = 序号fz.开始序号;
                        序列号结果(ref info, 序号fz, out 结果);
                        break;

                    //计算递增
                    case _序列号操作_.计算递增:

                        序列号复位(ref info, 最后一次加工时间, 时间, 班次结构, 序号fz);
                        序列号结果(ref info, 序号fz, out 结果);
                        序列号递增计算(ref 序号fz);


                        break;

                    //计算递减
                    case _序列号操作_.计算递减:
                        序列号复位(ref info, 最后一次加工时间, 时间, 班次结构, 序号fz);
                        序列号结果(ref info, 序号fz, out 结果);
                        序列号递减计算(ref 序号fz);

                        break;

                    //强制递增
                    case _序列号操作_.强制递增:
                        //  序列号复位(ref info, 文件, 时间, 班次结构, 序号结构);
                        序列号强制递增(ref 序号fz);
                        序列号结果(ref info, 序号fz, out 结果);
                        break;

                    //强制递减
                    case _序列号操作_.强制递减:
                        // 序列号复位(ref info, 文件, 时间, 班次结构, 序号结构);
                        序列号强制递减(ref 序号fz);
                        序列号结果(ref info, 序号fz, out 结果);
                        break;
                    case _序列号操作_.加工数量递增:
                        序列号复位(ref info, 最后一次加工时间, 时间, 班次结构, 序号fz);
                        序列号结果(ref info, 序号fz, out 结果);
                        int a = 序列号_加工数量递增计算_(ref 序号fz);
                        break;

                }

                if (序列号大小计算(序号fz) != 0)
                {
                    序号fz.当前序号 = 序号fz.开始序号;
                }
                序列号_转换成最终值(ref info, 序号fz);
            }
            catch (Exception ex)
            {
                msgErr = ex.Message;
                rt = false;
            }
            return rt;
        }

        internal bool 计算(_元素_日期_ info, DateTime 时间, out string 结果, out string msgErr)
        {
            bool rt = true;
            msgErr = string.Empty;
            结果 = "";
            _日期时间编码类型_ 编码类型 = _日期时间编码类型_.年4位;
            try
            {

                DateTime nows = 日期时间_偏移计算(时间, info);
                switch (info.类型)
                {
                    case _日期_.年4位:
                        结果 = nows.ToString("yyyy").ToString();
                        编码类型 = _日期时间编码类型_.年4位;
                        break;
                    case _日期_.年2位:
                        结果 = nows.ToString("yy").ToString();
                        编码类型 = _日期时间编码类型_.年2位;
                        break;
                    case _日期_.月:
                        结果 = nows.ToString("MM").ToString();
                        编码类型 = _日期时间编码类型_.月;
                        break;
                    case _日期_.日:
                        结果 = nows.ToString("dd").ToString();
                        编码类型 = _日期时间编码类型_.日;
                        break;
                    case _日期_.天:
                        结果 = new qfmain.日期时间_().Get_days(nows).ToString("000");
                        编码类型 = _日期时间编码类型_.日;
                        break;
                    case _日期_.星期:
                        结果 = new qfmain.日期时间_().Get_星期(nows).ToString("0");
                        编码类型 = _日期时间编码类型_.星期;
                        break;
                    case _日期_.周:
                        结果 = new qfmain.日期时间_().Get_weeks(nows).ToString("00");
                        编码类型 = _日期时间编码类型_.周;
                        break;
                }

                结果 = this._sys._ac_文件.Get_日期时间编码(编码类型, info.编码文件, 结果);

            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }
            return rt;
        }

        internal bool 计算(_元素_时间_ info, DateTime 时间, out string 结果, out string msgErr)
        {
            bool rt = true;
            msgErr = string.Empty;
            结果 = "";
            _日期时间编码类型_ 编码类型 = _日期时间编码类型_.时;
            try
            {
                switch (info.类型)
                {
                    case _时间_.时24:
                        结果 = 时间.ToString("HH").ToString();
                        编码类型 = _日期时间编码类型_.时;
                        break;
                    case _时间_.时12:
                        结果 = 时间.ToString("hh").ToString();
                        编码类型 = _日期时间编码类型_.时;
                        break;
                    case _时间_.分:
                        结果 = 时间.ToString("mm").ToString();
                        编码类型 = _日期时间编码类型_.分;
                        break;
                    case _时间_.秒:
                        结果 = 时间.ToString("ss").ToString();
                        编码类型 = _日期时间编码类型_.秒;
                        break;
                    case _时间_.毫秒:
                        结果 = 时间.ToString("fff").ToString();
                        break;

                }

                if (info.类型 != _时间_.毫秒)
                {
                    结果 = this._sys._ac_文件.Get_日期时间编码(编码类型, info.编码文件, 结果);
                }

            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }

            return rt;
        }

        /// <summary>
        ///  结果:正确时为计算结果,错误时为错误原因
        /// </summary>
        /// <param name="info"></param>
        /// <param name="info加工数据"></param>
        /// <param name="结果"></param>
        /// <returns></returns>
        internal bool 计算(_元素_关联对象_ info, string 对象名, string 关联对象的内容, out string 结果, out string msgErr)
        {
            msgErr = string.Empty;
            结果 = string.Empty;
            if (string.IsNullOrEmpty(info.关联对象))
            {
                msgErr = Language_.Get语言("要关联的对象不能为空");
                return false;
            }
            bool rt = true;
            string 数据 = 关联对象的内容;

            try
            {
                switch (info.类型)
                {
                    case _关联类型_.按位:

                        #region 按位取

                        try
                        {

                            _关联对象_按位_ info按位 = new json().json_解析<_关联对象_按位_>(info.参数);

                            if (info按位.开始位 <= 0)
                            {
                                结果 = 数据;
                            }
                            else
                            {
                                int 开始位 = (int)(info按位.开始位 - 1);
                                if (info按位.数量 == 0)
                                {
                                    结果 = string.Empty;
                                }
                                else if (info按位.数量 >= 数据.Length)
                                {
                                    结果 = new qfmain . 文本().获取(数据, 开始位, 数据.Length - 开始位);
                                }
                                else
                                {
                                    结果 = new qfmain.文本().获取(数据, 开始位, (int)info按位.数量);
                                }
                            }


                        }
                        catch (Exception ex)
                        {
                            rt = false;
                            msgErr = string.Empty;
                            结果 = string.Empty;
                        }

                        #endregion

                        break;

                    case _关联类型_.按字符分割:

                        #region 按字符分割

                        try
                        {
                            _关联对象_按字符分割_  info按字符 = new json().json_解析<_关联对象_按字符分割_>(info.参数);
                            string[] dataBeff = 数据.Split(new string[] { info按字符.分割符 }, StringSplitOptions.None);
                            if (info按字符.索引 >= dataBeff.Length)
                            {
                                结果 = dataBeff[dataBeff.Length - 1];
                            }
                            else
                            {
                                结果 = dataBeff[info按字符.索引];
                            }

                        }
                        catch (Exception ex)
                        {
                            结果 = string.Empty;
                            rt = false;
                            msgErr = ex.Message;
                        }

                        #endregion

                        break;
                    case _关联类型_.按首尾字符:

                        #region 按开始和结束字符

                        try
                        {
                         _关联对象_按首尾字符_  info_2 = new json().json_解析<_关联对象_按首尾字符_>(info.参数);                      
                            string[] data取两个之符之间 = new qfmain.文本().获取(数据, info_2.首字符, info_2.尾字符);

                            if (data取两个之符之间.Length == 0)
                            {
                                结果 = "";
                            }
                            else if (info_2.索引 >= data取两个之符之间.Length)
                            {
                                结果 = data取两个之符之间[data取两个之符之间.Length - 1];
                            }
                            else
                            {
                                结果 = data取两个之符之间[info_2.索引];
                            }

                        }
                        catch (Exception ex)
                        {
                            rt = false;
                            msgErr = ex.Message;
                            结果 = string.Empty;
                        }

                        #endregion


                        break;
                }

            }
            catch (Exception ex)
            {
                msgErr = ex.Message;
                rt = false;
            }


            return rt;
        }















        #region 本地 序列号


        /// <summary>
        /// 计算序列号用
        /// </summary>
        internal class _fz_序列号_
        {
            internal long 当前序号 { set; get; } = 0;
            internal long 开始序号 { set; get; } = 0;
            internal long 最大序号 { set; get; } = 0;
            internal long 递增量 { set; get; } = 0;
            internal int 每个加工数量 { set; get; } = 1;
            internal int 当前加工计数 { set; get; } = 0;
        }




        void 序列号_转换成可计算值(_元素_序列号_ info, out _fz_序列号_ cfgFZ)
        {
            cfgFZ = new _fz_序列号_();
            cfgFZ.递增量 = info.递增量;
            cfgFZ.每个加工数量 = info.加工.每个数量;
            cfgFZ.当前加工计数 = info.加工.当前计数;


            if (info.类型 == _序列号类型_.十进制)
            {
                cfgFZ.当前序号 = long.Parse(info.当前序号);
                cfgFZ.开始序号 = long.Parse(info.开始序号);
                cfgFZ.最大序号 = long.Parse(info.最大序号);
            }
            else if (info.类型 == _序列号类型_.十六进制HEX || info.类型 == _序列号类型_.十六进制hex)
            {
                cfgFZ.当前序号 = new qfmain.进制().十六进制To十进制(info.当前序号);
                cfgFZ.开始序号 = new qfmain.进制().十六进制To十进制(info.开始序号);
                cfgFZ.最大序号 = new qfmain.进制().十六进制To十进制(info.最大序号);
            }
            //else if (info.类型 == _序列号类型_.三十六进制)
            //{
            //    序号结构.当前序号 = new 进制_自定义().i36ToInt(info.当前序号);
            //    序号结构.开始序号 = new 进制_自定义().i36ToInt(info.开始序号);
            //    序号结构.最大序号 = new 进制_自定义().i36ToInt(info.最大序号);
            //}
        }

        void 序列号_转换成最终值(ref _元素_序列号_ info, _fz_序列号_ cfgFZ)
        {
            string 结果 = info.当前序号;
            int rt = 序列号大小计算(cfgFZ);
            int 位数 = info.开始序号.Length;

            if (rt != 0)
            {
                结果 = info.当前序号;
            }
            else
            {
                switch (info.类型)
                {
                    case _序列号类型_.十进制:
                        结果 = cfgFZ.当前序号.ToString().PadLeft(位数, '0');
                        break;

                    case _序列号类型_.十六进制HEX:
                        结果 = new qfmain.进制().十进制To十六进制_2位大写(cfgFZ.当前序号).PadLeft(位数, '0');
                        break;
                    case _序列号类型_.十六进制hex:
                        结果 = new qfmain.进制().十进制To十六进制_2位小写(cfgFZ.当前序号).PadLeft(位数, '0');
                        break;

                }
            }

            info.加工.当前计数 = cfgFZ.当前加工计数;
            info.当前序号 = 结果;
        }

        /// <summary>
        /// 0:正常,-1:小,1:大
        /// </summary>
        /// <param name="当前序号"></param> 
        /// <param name="开始序号"></param>
        /// <returns></returns>
        int 序列号大小计算(_fz_序列号_ cfgFZ)
        {
            if (cfgFZ.当前序号 < cfgFZ.开始序号)
            {
                return -1;
            }
            else if (cfgFZ.当前序号 > cfgFZ.最大序号)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 0:不复位,1:复位
        /// </summary>
        /// <param name="当前时间"></param>
        /// <param name="当前序号"></param>
        /// <param name="最大序号"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        internal int 序列号判断复位计算(DateTime 时间, DateTime 最后一次加工时间, _班次_结构_[] 班次, _元素_序列号_ info, _fz_序列号_ cfgFz)
        {
            int rt = 0;

            List<string> lstWork = new List<string>();
            lstWork.Add("判断_是否需要复位_加工计数");
            lstWork.Add("按最大");
            lstWork.Add("其它");

            bool rtA = true;
            foreach (var s in lstWork)
            {
                if (!rtA)
                {
                    break;
                }
                else if (s == "判断_是否需要复位_加工计数")
                {
                    #region 判断_是否需要复位_加工计数

                    if (info.加工.每个数量 <= 1)
                    {
                        continue;
                    }
                    //设置的每个加工数量未加工完,不判断复位,
                    if (判断_序列号_加工计数复位(info) == 1)
                    {
                        rtA = false;
                        break;
                    }
                    else
                    {
                        //DateTime date0 = 时间;
                        //时间 = DateTime.Parse(info.加工.最后一次日期);
                        //info.加工.最后一次日期 = date0.ToString("yyyy-MM-dd HH:mm:ss");
                    }

                    #endregion
                }
                else if (s == "按最大")
                {
                    #region 按最大

                    if (序列号大小计算(cfgFz) != 0)
                    {
                        rt = 1;
                        rtA = false;
                        break;
                    }

                    #endregion
                }
                else if (s == "其它")
                {
                    #region 其它

                    switch (info.复位方式)
                    {
                        case _序列号复位_.按最大:

                            #region 按最大

                            if (序列号大小计算(cfgFz) != 0)
                            {
                                rt = 1;
                            }

                            #endregion

                            break;
                        case _序列号复位_.按年:

                            #region 按年

                            string Y_当前 = 时间.ToString("yyyy");
                            string Y_最后一次加工 = 最后一次加工时间.ToString("yyyy");
                            if (Y_当前 != Y_最后一次加工)
                            {
                                rt = 1;
                            }

                            #endregion

                            break;

                        case _序列号复位_.按月:

                            #region 按月

                            string YM_当前 = 时间.ToString("yyyy-MM");
                            string YM_最后一次加工 = 最后一次加工时间.ToString("yyyy-MM");
                            if (YM_当前 != YM_最后一次加工)
                            {
                                rt = 1;
                            }


                            #endregion

                            break;

                        case _序列号复位_.按日:

                            #region 按日

                            string YMD_当前 = 时间.ToString("yyyy-MM-dd");
                            string YMD_最后一次加工 = 最后一次加工时间.ToString("yyyy-MM-dd");
                            if (YMD_当前 != YMD_最后一次加工)
                            {
                                rt = 1;
                            }

                            #endregion

                            break;

                        case _序列号复位_.按周:

                            #region 按周

                            int 周_当前 = new qfmain.日期时间_().Get_weeks(时间);
                            int 周_最后一次加工 = new qfmain.日期时间_().Get_weeks(最后一次加工时间);
                            if (周_当前 != 周_最后一次加工)
                            {
                                rt = 1;
                            }

                            #endregion

                            break;


                        case _序列号复位_.按班次:

                            #region 按班次

                            计算(班次, 时间, out string 结果, out string msgErr);
                            计算(班次, 最后一次加工时间, out string 结果_最后一次, out msgErr);

                            if (结果 != 结果_最后一次)
                            {
                                rt = 1;
                            }

                            #endregion

                            break;
                    }


                    #endregion
                }
            }



            return rt;
        }

        /// <summary>
        /// 返回: =0:可复位判断, 1:不复位判断
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        internal int 判断_序列号_加工计数复位(_元素_序列号_ info)
        {
            int rt = 0;
            if (info.加工.每个数量 > 1 && (info.加工.当前计数 > 0 && info.加工.当前计数 < info.加工.每个数量))
            {
                rt = 1;
            }
            return rt;
        }





        void 序列号复位(ref _元素_序列号_ info, DateTime 时间, DateTime 最后一次加工时间, _班次_结构_[] 班次结构, _fz_序列号_ 序号结构)
        {
            int rt1 = 序列号判断复位计算(时间, 最后一次加工时间, 班次结构, info, 序号结构);
            if (rt1 == 1)
            {
                info.加工.当前计数 = 0;
                序号结构.当前序号 = 序号结构.开始序号;
            }
        }


        void 序列号结果(ref _元素_序列号_ info, _fz_序列号_ 序号结构, out string 结果)
        {
            序列号_转换成最终值(ref info, 序号结构);
            结果 = info.当前序号;
        }





        #endregion


        #region 序列号　强制计算

        void 序列号递增计算(ref _fz_序列号_ info)
        {
            info.当前序号 += info.递增量;
            if (info.当前序号 > info.最大序号)
            {
                info.当前序号 = info.开始序号;
            }
        }

        void 序列号递减计算(ref _fz_序列号_ info)
        {
            info.当前序号 -= info.递增量;
            if (info.当前序号 < info.开始序号)
            {
                info.当前序号 = info.开始序号;
            }
        }

        void 序列号强制递增(ref _fz_序列号_ info)
        {
            info.当前序号 += info.递增量;
            if (info.当前序号 > info.最大序号)
            {
                info.当前序号 = info.开始序号;
            }
        }

        void 序列号强制递减(ref _fz_序列号_ info)
        {
            info.当前序号 -= info.递增量;
            if (info.当前序号 < info.开始序号)
            {
                info.当前序号 = info.开始序号;
            }
        }


        /// <summary>
        /// 返回值,=0:None,=1:复位判断
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        int 序列号_加工数量递增计算_(ref _fz_序列号_ info)
        {
            int a = 0;

            List<string> lstWork = new List<string>();
            lstWork.Add("加工计数");
            lstWork.Add("加工不计数递增");

            bool rt = true;
            foreach (string s in lstWork)
            {
                if (!rt)
                {
                    break;
                }
                else if (s == "加工计数")
                {
                    #region 加工计数

                    if (info.每个加工数量 > 1)
                    {
                        info.当前加工计数++;
                        if (info.当前加工计数 >= info.每个加工数量)
                        {
                            序列号递增计算(ref info);
                            info.当前加工计数 = 0;
                            a = 1;
                        }
                        rt = false;
                        break;
                    }

                    #endregion

                }
                else if (s == "加工不计数递增")
                {
                    序列号递增计算(ref info);
                    info.当前加工计数 = 0;
                    a = 1;

                }
            }

            return a;
        }


        #endregion


        #region 日期偏移计算

        DateTime 日期时间_偏移计算(DateTime 时间, _元素_日期_ info)
        {
            DateTime 当前时间 = 时间;
            if (this._sys._功能.日期时间.自定义编码)
            {
                switch (info.偏移类型)
                {
                    case _日期偏移类型_.无:
                        break;
                    case _日期偏移类型_.年:
                        当前时间 = new qfmain.日期时间_().增减时间(时间, 0, info.偏移值);
                        break;
                    case _日期偏移类型_.月:
                        当前时间 = new qfmain.日期时间_().增减时间(时间, 1, info.偏移值);
                        break;
                    case _日期偏移类型_.日:
                        当前时间 = new qfmain.日期时间_().增减时间(时间, 2, info.偏移值);
                        break;
                    case _日期偏移类型_.周:

                        int a = info.偏移值 * 7;
                        当前时间 = new qfmain.日期时间_().增减时间(时间, 2, a);

                        break;
                }

            }
            return 当前时间;
        }

        #endregion



    }
}
