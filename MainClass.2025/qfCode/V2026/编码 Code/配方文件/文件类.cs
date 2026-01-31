using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace qfCode
{
    internal class 文件类
    {
        public 编码_ _codeSys;
        public 文件类(编码_ codeSys)
        {
            this._codeSys = codeSys;
            Get_班次(常量.配置文件名_默认);
            Get_日期时间(常量.配置文件名_默认, "", "");

        }



        #region 生成路径


        /// <summary>
        /// 文件路径
        /// </summary> 
        public string GetPath_班次(string FileName)
        {
            return $"{this._codeSys._文件夹_属性.班次}\\{FileName}.txt";
        }

        /// <summary>
        /// 文件路径
        /// </summary> 
        public string GetPath_日期时间(string FileName)
        {
            return $"{this._codeSys._文件夹_属性.日期时间}\\{FileName}.txt";
        }


        /// <summary>
        /// 获取存放编码信息的文件路径
        /// <para>仅ini或txt类型时有效</para>
        /// </summary> 
        public string GetPath_配方(string FileName)
        {
            return this._codeSys._文件夹_属性.配方 + $"\\{FileName}{this._codeSys._功能.后缀}";
        }

        public string[] Get目录_班次()
        {
            new qfmain.文件_文件夹().文件夹_获取所有文件_无后缀(this._codeSys._文件夹_属性.班次, out List<string> lst, "*.txt");
            return lst.ToArray();
        }
        public string[] Get目录_日期时间()
        {
            new qfmain.文件_文件夹().文件夹_获取所有文件_无后缀(this._codeSys._文件夹_属性.日期时间, out List<string> lst, "*.txt");
            return lst.ToArray();
        }


        public (bool s,string m , string[] v) Get目录_配方()
        {
            return this._codeSys._配方文件操作._Iwork文件 .Get目录();
        }


        #endregion


        #region 获取


        /// <summary>
        /// 获取班次
        /// </summary> 
        internal _班次_[] Get_班次(string FileName)
        {
            string path = GetPath_班次(FileName);
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
            Beff = lst.ToArray();
            new qfmain.文件_文件夹().WriteReadJson(path, 1, ref Beff, out string msgErr);
            return Beff;
        }

        /// <summary>
        /// 获取日期时间,
        /// <para>section : 节名称,如年4等</para>
        /// <para>keys : 字段,如2022=22 等</para>
        /// </summary> 
        internal string Get_日期时间(string FileName, string section, string keys)
        {
            string path = GetPath_日期时间(FileName);
             
            qfmain.ini_sharpconfig ini_sys = new qfmain.ini_sharpconfig(path);
            if (!new qfmain.文件_文件夹().文件_是否存在(path))
            {

                #region 生成默认数据 年4位

                for (int i = 2020; i <= 2050; i++)
                {
                    ini_sys.Write($"{_日期时间_._em_日期_.年4位}", $"{i}", $"{i}", false);
                }

                #endregion

                #region 生成默认数据 年2位

                for (int i = 20; i <= 50; i++)
                {
                    ini_sys.Write($"{_日期时间_._em_日期_.年2位}", $"{i}", $"{i}", false);
                }

                #endregion

                #region 生成默认数据 月

                for (int i = 1; i <= 12; i++)
                {
                    ini_sys.Write($"{_日期时间_._em_日期_.月}", $"{i}", ($"{i}").PadLeft(2, '0'), false);
                }

                #endregion

                #region 生成默认数据 日


                for (int i = 1; i <= 31; i++)
                {
                    ini_sys.Write($"{_日期时间_._em_日期_.日}", $"{i}", ($"{i}").PadLeft(2, '0'), false);
                }

                #endregion

                #region 生成默认数据 星期

                for (int i = 1; i <= 7; i++)
                {
                    ini_sys.Write($"{_日期时间_._em_日期_.星期}", $"{i}", $"{i}", false);
                }

                #endregion

                #region 生成默认数据 周

                for (int i = 1; i <= 60; i++)
                {
                    ini_sys.Write($"{_日期时间_._em_日期_.周}", $"{i}", ($"{i}").PadLeft(2, '0'), false);
                }

                #endregion

                #region 生成默认数据 时

                for (int i = 0; i <= 24; i++)
                {
                    ini_sys.Write($"{_日期时间_._em_时间_.时24}", $"{i}", ($"{i}").PadLeft(2, '0'), false);
                }

                #endregion

                #region 生成默认数据 分

                for (int i = 0; i <= 60; i++)
                {
                    ini_sys.Write($"{_日期时间_._em_时间_.分}", $"{i}", ($"{i}").PadLeft(2, '0'), false);
                }

                #endregion

                #region 生成默认数据 秒

                for (int i = 0; i <= 60; i++)
                {
                    ini_sys.Write($"{_日期时间_._em_时间_.秒}", $"{i}", ($"{i}").PadLeft(2, '0'), false);
                }

                #endregion

                ini_sys.Save();
            }
            return ini_sys.Read(section, keys, keys);

        }

        #endregion

    }
}
