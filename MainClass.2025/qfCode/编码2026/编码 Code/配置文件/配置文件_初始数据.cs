using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfCode
{
    internal class 配置文件_初始数据
    {
        /// <summary>
        /// 获取班次
        /// </summary> 
        internal static _班次_[] 班次()
        {

            _班次_[] Beff = new _班次_[0];
            List<_班次_> lst = new List<_班次_>
            {
                new _班次_
                {
                    代码 = "A",
                    上班时间 = "08:00:00",
                    下班时间 = "16:00:00",
                },
                 new _班次_
                {
                    代码 = "B",
                    上班时间 = "16:00:00",
                    下班时间 = "01:00:00",
                },  new _班次_
                {
                    代码 = "C",
                    上班时间 = "01:00:00",
                    下班时间 = "08:00:00",
                },
            };

            return Beff;
        }


        #region Ini格式

        /// <summary>
        /// 转成Ini格式...节名称
        /// </summary> 
        static string ToIniSection(string section)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"[{section}]");
            return sb.ToString();
        }

        /// <summary>
        /// 转成Ini格式....字段
        /// </summary> 
        static string ToIniSetting(string setting, string value)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{setting}={value}");
            return sb.ToString();
        }


        #endregion

         
        /// <summary>
        /// 获取日期时间,
        /// <para>section : 节名称,如年4等</para>
        /// <para>keys : 字段,如2022=22 等</para>
        /// </summary> 
        internal static string 日期时间()
        {
            StringBuilder sb = new StringBuilder();

            #region 生成默认数据 年4位

            sb.AppendLine(ToIniSection($"{_日期时间_._em_日期_.年4位}"));
            for (int i = 2020; i <= 2050; i++)
            {
                sb.AppendLine(ToIniSetting($"{i}", $"{i}"));
            }

            #endregion

            #region 生成默认数据 年2位
            sb.AppendLine(ToIniSection($"{_日期时间_._em_日期_.年2位}"));
            for (int i = 20; i <= 50; i++)
            {
                sb.AppendLine(ToIniSetting($"{i}", $"{i}"));
            }

            #endregion

            #region 生成默认数据 月
            sb.AppendLine(ToIniSection($"{_日期时间_._em_日期_.月}"));
            for (int i = 1; i <= 12; i++)
            {
                sb.AppendLine(ToIniSetting($"{i}", ($"{i}").PadLeft(2, '0')));
            }

            #endregion

            #region 生成默认数据 日

            sb.AppendLine(ToIniSection($"{_日期时间_._em_日期_.日}"));
            for (int i = 1; i <= 31; i++)
            {
                sb.AppendLine(ToIniSetting($"{i}", ($"{i}").PadLeft(2, '0')));
            }

            #endregion

            #region 生成默认数据 星期

            sb.AppendLine(ToIniSection($"{_日期时间_._em_日期_.星期}"));
            for (int i = 1; i <= 7; i++)
            {
                sb.AppendLine(ToIniSetting($"{i}", $"{i}"));
            }

            #endregion

            #region 生成默认数据 周

            sb.AppendLine(ToIniSection($"{_日期时间_._em_日期_.周}"));
            for (int i = 1; i <= 60; i++)
            {
                sb.AppendLine(ToIniSetting($"{i}", ($"{i}").PadLeft(2, '0')));
            }

            #endregion

            #region 生成默认数据 时
            sb.AppendLine(ToIniSection($"{_日期时间_._em_时间_.时24}"));
            for (int i = 0; i <= 24; i++)
            {
                sb.AppendLine(ToIniSetting($"{i}", ($"{i}").PadLeft(2, '0')));
            }

            #endregion

            #region 生成默认数据 分
            sb.AppendLine(ToIniSection($"{_日期时间_._em_时间_.分}"));
            for (int i = 0; i <= 60; i++)
            {
                sb.AppendLine(ToIniSetting($"{i}", ($"{i}").PadLeft(2, '0')));
            }

            #endregion

            #region 生成默认数据 秒
            sb.AppendLine(ToIniSection($"{_日期时间_._em_时间_.秒}"));
            for (int i = 0; i <= 60; i++)
            {
                sb.AppendLine(ToIniSetting($"{i}", ($"{i}").PadLeft(2, '0')));
            }

            #endregion

            return sb.ToString();

        }


    }
}
