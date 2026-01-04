using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfmain
{
    public class 日期时间_
    {

        /// <summary>
        /// 超时时间:ms
        /// <para>返回:true:未超时,false:超时</para> 
        /// </summary>
        /// <param name="超时时间"> </param>
        /// <returns></returns>
        public virtual bool 是否超时(DateTime dateMax, DateTime dateMin, int 超时时间)
        {
            bool rt = true;
            TimeSpan ts = dateMax - dateMin;
            if (ts >= TimeSpan.FromMilliseconds(超时时间))
            {
                rt = false;
            }
            return rt;

        }


        /// <summary>
        /// 增加为"-"则减小,无符号则增加......要增加的部份0:年份,1月份,2为日,3为小时,4为分钟,5为秒,6为毫秒
        /// </summary>
        /// <param name="dtnow">要增加的时间</param>
        /// <param name="add_">要增加的符号</param>
        /// <param name="value_">要增加的值</param>
        /// <returns></returns>
        public virtual DateTime 增减时间(DateTime dtnow, int add_, int value_)
        {
            switch (add_)
            {
                case 0: return dtnow.AddYears(value_);
                case 1: return dtnow.AddMonths(value_);
                case 2: return dtnow.AddDays(value_);
                case 3: return dtnow.AddHours(value_);
                case 4: return dtnow.AddMinutes(value_);
                case 5: return dtnow.AddSeconds(value_);
                case 6:
                    return dtnow.AddMilliseconds(value_);
                default:
                    break;
            }
            return dtnow;
        }


        /// <summary>
        /// 一年的第几周
        /// </summary>
        /// <param name="DateTime_"></param>
        /// <returns></returns>
        public virtual int Get_weeks(DateTime DateTime_)
        {
            System.Globalization.GregorianCalendar gc = new System.Globalization.GregorianCalendar();
            int weekofyear = gc.GetWeekOfYear(DateTime_, System.Globalization.CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
            return weekofyear;
        }


        /// <summary>
        /// 一年的第几天
        /// </summary>
        /// <param name="DateTime_"></param>
        /// <returns></returns>
        public virtual int Get_days(DateTime DateTime_)
        {
            int s = DateTime_.DayOfYear;
            return s;
        }



        /// <summary>
        /// 一,二,三,四,五,六,日
        /// </summary>
        /// <param name="Week"></param>
        /// <returns></returns>
        public virtual string 星期_To中文(int Week)
        {
            var days = new Dictionary<int, string>
             {
                   { 1, "一" },
                   { 2, "二" },
                   { 3, "三" },
                   { 4, "四" },
                   { 5, "五" },
                   { 6, "六" },
                   { 7, "日" }
                };

            return days.TryGetValue(Week, out var day) ? day : "无效的天数";

        }
        public virtual int Get_星期(DateTime DateTime_)
        {
            int[] week = { 7, 1, 2, 3, 4, 5, 6 };
            return week[Convert.ToInt16(DateTime_.DayOfWeek)];
        }


        public virtual TimeSpan 计算两个时间差(DateTime 时间1, DateTime 时间2)
        {
            return 时间1.Subtract(时间2); ;
        }



        /// <summary>
        /// -1:表示时间1小,0:表示相等,1表示时间1大
        /// </summary>
        /// <param name="时间1"></param>
        /// <param name="时间2"></param>
        /// <returns></returns>
        public virtual int 比较大小(DateTime 时间1, DateTime 时间2)
        {
            return DateTime.Compare(时间1, 时间2);
        }


        /// <summary>
        /// 可以用来计算运行时间,时间差等
        /// </summary>
        /// <param name="开始时间"></param>
        /// <param name="结束时间"></param>
        /// <param name="timeSpan_"></param>
        /// <param name="msgErr"></param>
        /// <returns></returns>
        public virtual bool 计算_时间间隔(DateTime DateTimeMin, DateTime DateTimeMax, out TimeSpan timeSpan_, out string msgErr)
        {
            bool rt = true;
            timeSpan_ = new TimeSpan();
            msgErr = string.Empty;
            try
            {
                timeSpan_ = DateTimeMax - DateTimeMin;
            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }
            return rt;
        }


        /// <summary>
        /// 不看天及其它
        /// </summary> 
        public   int 相差几月(DateTime start, DateTime end)
        {
            return (end.Year - start.Year) * 12 + (end.Month - start.Month);
        }
        /// <summary>
        /// 不看天及其它
        /// </summary> 
        public   int 相差几年(DateTime start, DateTime end)
        {
            return end.Year - start.Year;
        }


    }
}
