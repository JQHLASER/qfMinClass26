using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace qfCode
{
    internal class 文件
    {
        private 编码 _sys;

        public 文件(编码 _sys_)
        {
            this._sys = _sys_;
            string FileMain = this._sys._功能._主文件夹;
            this._后缀 = this._sys._功能._后缀;
            this._类型 = this._sys._功能._文件类型;

            this._File_Main = $"{FileMain}\\Main";
            this._File_Class = $"{FileMain}\\Class";
            this._File_DateTime = $"{FileMain}\\DateTime";
            this._File_Cfg = $"{FileMain}\\Cfg";
            this._File_Times = $"{FileMain}\\Times";

            new qfmain.文件_文件夹().文件夹_新建(this._File_Main, out string msgErr);
            new qfmain.文件_文件夹().文件夹_新建(this._File_Class, out msgErr);
            new qfmain.文件_文件夹().文件夹_新建(this._File_DateTime, out msgErr);
            new qfmain.文件_文件夹().文件夹_新建(this._File_Cfg, out msgErr);
            new qfmain.文件_文件夹().文件夹_新建(this._File_Times, out msgErr);

            初始化_日期及时间编码();
            Get_班次(out _班次_结构_[] Beff, this._默认文件);

        }
        /// <summary>
        /// 存放主文件
        /// </summary>
        string _File_Main;
        /// <summary>
        /// 存放班次文件
        /// </summary>
        string _File_Class;
        /// <summary>
        /// 存放日期及时间编码文件
        /// </summary>
        string _File_DateTime;
        /// <summary>
        /// 存放参数的文件夹
        /// </summary>
        string _File_Cfg;
        /// <summary>
        /// 存放更新时间文件
        /// </summary>
        string _File_Times;
        string _后缀;

        internal string _默认文件 = "Default";
        _文件类型_ _类型 = _文件类型_.ini;



        #region 配置文件


        void 初始化_日期及时间编码()
        {
            string path = GetPath_日期时间编码(_默认文件);
            if (new qfmain.文件_文件夹().文件_是否存在(path))
            {
                return;
            }
            qfmain.ini_sharpconfig ini_sys = new qfmain.ini_sharpconfig(path);
             
            #region 生成默认数据 年4位

            for (int i = 2020; i <= 2050; i++)
            {
                ini_sys.Write($"{_日期时间编码类型_.年4位}", $"{i}", $"{i}",false );
            }

            #endregion

            #region 生成默认数据 年

            for (int i = 20; i <= 50; i++)
            {
                ini_sys.Write($"{_日期时间编码类型_.年2位}", $"{i}", $"{i}", false);
            }

            #endregion

            #region 生成默认数据 月

            for (int i = 1; i <= 12; i++)
            {
                ini_sys.Write($"{_日期时间编码类型_.月}", $"{i}", ($"{i}").PadLeft(2, '0'), false);
            }

            #endregion

            #region 生成默认数据 日


            for (int i = 1; i <= 31; i++)
            {
                ini_sys.Write($"{_日期时间编码类型_.日}", $"{i}", ($"{i}").PadLeft(2, '0'), false);
            }

            #endregion

            #region 生成默认数据 星期

            for (int i = 1; i <= 7; i++)
            {
                ini_sys.Write($"{_日期时间编码类型_.星期}", $"{i}", $"{i}", false);
            }

            #endregion

            #region 生成默认数据 周

            for (int i = 1; i <= 60; i++)
            {
                ini_sys.Write($"{_日期时间编码类型_.周}", $"{i}", ($"{i}").PadLeft(2, '0'), false);
            }

            #endregion

            #region 生成默认数据 时

            for (int i = 0; i <= 24; i++)
            {
                ini_sys.Write($"{_日期时间编码类型_.时}", $"{i}", ($"{i}").PadLeft(2, '0'), false);
            }

            #endregion

            #region 生成默认数据 分

            for (int i = 0; i <= 60; i++)
            {
                ini_sys.Write($"{_日期时间编码类型_.分}", $"{i}", ($"{i}").PadLeft(2, '0'), false) ;
            }

            #endregion

            #region 生成默认数据 秒

            for (int i = 0; i <= 60; i++)
            {
                ini_sys.Write($"{_日期时间编码类型_.秒}", $"{i}", ($"{i}").PadLeft(2, '0'), false);
            }

            #endregion

            ini_sys.Save();
        }

        /// <summary>
        /// 获取班次
        /// </summary>
        internal void Get_班次(out _班次_结构_[] Beff, string 文件名)
        {
            文件名 = get_文件名_不含后缀(文件名);
            string path = $"{this._File_Class}\\{文件名}.txt";
            Beff = new _班次_结构_[0];
            if (!new qfmain.文件_文件夹().文件_是否存在(path))
            {
                List<_班次_结构_> lst = new List<_班次_结构_>();
                _班次_结构_ cs = new _班次_结构_();
                cs.代码 = "A";
                cs.上班时间 = "08:00:00";
                cs.下班时间 = "16:00:00";
                lst.Add(cs);

                cs = new _班次_结构_();
                cs.代码 = "B";
                cs.上班时间 = "16:00:00";
                cs.下班时间 = "01:00:00";
                lst.Add(cs);

                cs = new _班次_结构_();
                cs.代码 = "C";
                cs.上班时间 = "01:00:00";
                cs.下班时间 = "08:00:00";
                lst.Add(cs);
                Beff = lst.ToArray();
            }


            new qfmain.文件_文件夹().WriteReadJson(path, 1, ref Beff, out string msgErr);

        }

        /// <summary>
        /// 生成日期时间编码的的路径
        /// </summary>     
        string GetPath_日期时间编码(string 文件名)
        {
            文件名 = get_文件名_不含后缀(文件名);
            string path = $"{this._File_DateTime}\\{文件名}.ini";
            return path;
        }

        /// <summary>
        /// 获取日期时间的编码
        /// </summary>      
        internal string Get_日期时间编码(_日期时间编码类型_ 类型, string 文件名, string value)
        {
            string path = GetPath_日期时间编码(文件名);
            string section = $"{类型}";
            return new qfmain.ini_sharpconfig(path).Read(section, value, value);
        }

        /// <summary>
        /// 获取日期更新时间点
        /// </summary>    
        internal void Get_日期更新时间点(out _日期更新时间点_ cfg, string 文件名)
        {
            文件名 = get_文件名_不含后缀(文件名);
            string path = $"{this._File_Times}\\{文件名}.txt";
            cfg = new _日期更新时间点_();
            new qfmain.文件_文件夹().WriteReadJson(path, 1, ref cfg, out string msgErr);

        }


        #endregion

        string get_文件名_不含后缀(string 文件名)
        {
            new qfmain.文件_文件夹().文件_获取文件名_不含后缀(文件名, out string name, out string msgErr);
            return name;
        }







        #region edm文件


        string Get_edm文件路径(string 文件名)
        {
            文件名 = get_文件名_不含后缀(文件名);
            return $"{this._File_Main}\\{文件名}{this._后缀}";
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        internal bool Write_edm(string 文件名, _文件信息_ Cfg, out string msgErr)
        {
            string path = Get_edm文件路径(文件名);
            bool rt = true;
            msgErr = string.Empty;
            switch (this._类型)
            {
                case _文件类型_.ini:
                    rt = new qfmain.文件_文件夹().WriteReadIni(path, 0, ref Cfg, out msgErr);
                    break;
                case _文件类型_.txt:
                    rt = new qfmain.文件_文件夹().WriteReadJson(path, 0, ref Cfg, out msgErr);
                    break;
            }

            return rt;
        }

        /// <summary>
        /// 读文件
        /// </summary>
        internal bool Read_edm(string 文件名, out _文件信息_ Cfg, out string msgErr)
        {
            string path = Get_edm文件路径(文件名);
            bool rt = true;
            msgErr = string.Empty;
            Cfg = new _文件信息_();
            switch (this._类型)
            {
                case _文件类型_.ini:
                    rt = new qfmain.文件_文件夹().WriteReadIni(path, 1, ref Cfg, out msgErr);
                    break;
                case _文件类型_.txt:
                    rt = new qfmain.文件_文件夹().WriteReadJson(path, 1, ref Cfg, out msgErr);
                    break;
            }

            return rt;
        }

        /// <summary>
        /// 删文件
        /// </summary>
        internal bool Delete_edm(string 文件名, out _文件信息_ Cfg, out string msgErr)
        {
            string path = Get_edm文件路径(文件名);
            bool rt = true;
            msgErr = string.Empty;
            Cfg = new _文件信息_();

            if (this._类型 != _文件类型_.网络版 &&
                this._类型 != _文件类型_.SqlLite &&
                this._类型 != _文件类型_.SqlServer &&
                this._类型 != _文件类型_.MySql)
            {
                rt = new qfmain.文件_文件夹().文件_删除文件(path, out msgErr);
            }

            return rt;
        }
        /// <summary>
        /// 复制文件
        /// </summary>
        internal bool Copy_edm(string 文件名, string 新文件名, out string msgErr)
        {
            string path = Get_edm文件路径(文件名);
            string path1 = Get_edm文件路径(新文件名);
            return new qfmain.文件_文件夹().文件_复制文件(path1, path, out msgErr, true);
        }



        #endregion




    }
}
