using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static qfCode._功能_结构_;
using static qfCode.编码_计算;
using static qfCode.计算_序列号;

namespace qfCode
{
    internal class 计算_序列号
    {


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




        internal void 序列号_转换成可计算值(_元素_.序列号 info, out _fz_序列号_ cfgFZ)
        {
            cfgFZ = new _fz_序列号_();
            cfgFZ.递增量 = info.递增量;
            cfgFZ.每个加工数量 = info.加工.数量;
            cfgFZ.当前加工计数 = info.加工.计数;


            if (info.types  == _序列号_._em_类型_.十进制)
            {
                cfgFZ.当前序号 = long.Parse(info.当前序号);
                cfgFZ.开始序号 = long.Parse(info.开始序号);
                cfgFZ.最大序号 = long.Parse(info.最大序号);
            }
            else if (info.types  == _序列号_._em_类型_.十六进制HEX || info.types  == _序列号_._em_类型_.十六进制hex)
            {
                cfgFZ.当前序号 = new qfmain.进制().十六进制To十进制(info.当前序号);
                cfgFZ.开始序号 = new qfmain.进制().十六进制To十进制(info.开始序号);
                cfgFZ.最大序号 = new qfmain.进制().十六进制To十进制(info.最大序号);
            }
            //else if (info.类型 == _序列号_._em_类型_.三十六进制)
            //{
            //    序号结构.当前序号 = new 进制_自定义().i36ToInt(info.当前序号);
            //    序号结构.开始序号 = new 进制_自定义().i36ToInt(info.开始序号);
            //    序号结构.最大序号 = new 进制_自定义().i36ToInt(info.最大序号);
            //}
        }

        internal void 序列号_转换成最终值(ref _元素_.序列号 info, _fz_序列号_ cfgFZ)
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
                switch (info.types )
                {
                    case _序列号_._em_类型_.十进制:
                        结果 = cfgFZ.当前序号.ToString().PadLeft(位数, '0');
                        break;

                    case _序列号_._em_类型_.十六进制HEX:
                        结果 = new qfmain.进制().十进制To十六进制_2位大写(cfgFZ.当前序号).PadLeft(位数, '0');
                        break;
                    case _序列号_._em_类型_.十六进制hex:
                        结果 = new qfmain.进制().十进制To十六进制_2位小写(cfgFZ.当前序号).PadLeft(位数, '0');
                        break;

                }
            }

            info.加工.计数 = cfgFZ.当前加工计数;
            info.当前序号 = 结果;
        }

        /// <summary>
        /// 0:正常,-1:小,1:大
        /// </summary>
        /// <param name="当前序号"></param> 
        /// <param name="开始序号"></param>
        /// <returns></returns>
        internal int 序列号大小计算(_fz_序列号_ cfgFZ)
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
        internal int 序列号判断复位计算(DateTime 时间, DateTime 最后一次加工时间, _班次_[] 班次, _元素_.序列号 info, _fz_序列号_ cfgFz)
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

                    if (info.加工.数量 <= 1)
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
                        case _序列号_._em_复位_.按最大:

                            #region 按最大

                            if (序列号大小计算(cfgFz) != 0)
                            {
                                rt = 1;
                            }

                            #endregion

                            break;
                        case _序列号_._em_复位_.按年:

                            #region 按年

                            string Y_当前 = 时间.ToString("yyyy");
                            string Y_最后一次加工 = 最后一次加工时间.ToString("yyyy");
                            if (Y_当前 != Y_最后一次加工)
                            {
                                rt = 1;
                            }

                            #endregion

                            break;

                        case _序列号_._em_复位_.按月:

                            #region 按月

                            string YM_当前 = 时间.ToString("yyyy-MM");
                            string YM_最后一次加工 = 最后一次加工时间.ToString("yyyy-MM");
                            if (YM_当前 != YM_最后一次加工)
                            {
                                rt = 1;
                            }


                            #endregion

                            break;

                        case _序列号_._em_复位_.按日:

                            #region 按日

                            string YMD_当前 = 时间.ToString("yyyy-MM-dd");
                            string YMD_最后一次加工 = 最后一次加工时间.ToString("yyyy-MM-dd");
                            if (YMD_当前 != YMD_最后一次加工)
                            {
                                rt = 1;
                            }

                            #endregion

                            break;

                        case _序列号_._em_复位_.按周:

                            #region 按周

                            int 周_当前 = new qfmain.日期时间_().Get_weeks(时间);
                            int 周_最后一次加工 = new qfmain.日期时间_().Get_weeks(最后一次加工时间);
                            if (周_当前 != 周_最后一次加工)
                            {
                                rt = 1;
                            }

                            #endregion

                            break;


                        case _序列号_._em_复位_.按班次:

                            #region 按班次

                            var rtClass1 = new 计算_班次().计算(班次, 时间);
                            var rtClass2 = new 计算_班次().计算(班次, 最后一次加工时间);
                            if (rtClass1.v != rtClass2.v)
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
        internal int 判断_序列号_加工计数复位(_元素_.序列号 info)
        {
            int rt = 0;
            if (info.加工.数量 > 1 && (info.加工.计数 > 0 && info.加工.计数 < info.加工.数量))
            {
                rt = 1;
            }
            return rt;
        }





        internal void 序列号复位(ref _元素_.序列号 info, DateTime 时间, DateTime 最后一次加工时间, _班次_[] 班次结构, _fz_序列号_ 序号结构)
        {
            int rt1 = 序列号判断复位计算(时间, 最后一次加工时间, 班次结构, info, 序号结构);
            if (rt1 == 1)
            {
                info.加工.计数 = 0;
                序号结构.当前序号 = 序号结构.开始序号;
            }
        }


        internal void 序列号结果(ref _元素_.序列号 info, _fz_序列号_ 序号结构, out string 结果)
        {
            序列号_转换成最终值(ref info, 序号结构);
            结果 = info.当前序号;
        }





        #endregion




        internal void 序列号递增计算(ref _fz_序列号_ info)
        {
            info.当前序号 += info.递增量;
            if (info.当前序号 > info.最大序号)
            {
                info.当前序号 = info.开始序号;
            }
        }

        internal void 序列号递减计算(ref _fz_序列号_ info)
        {
            info.当前序号 -= info.递增量;
            if (info.当前序号 < info.开始序号)
            {
                info.当前序号 = info.开始序号;
            }
        }

        internal void 序列号强制递增(ref _fz_序列号_ info)
        {
            info.当前序号 += info.递增量;
            if (info.当前序号 > info.最大序号)
            {
                info.当前序号 = info.开始序号;
            }
        }

        internal void 序列号强制递减(ref _fz_序列号_ info)
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
        internal int 序列号_加工数量递增计算_(ref _fz_序列号_ info)
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


    }
}
