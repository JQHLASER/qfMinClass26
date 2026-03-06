using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace qfCode
{
    public class 配置文件_ 
    {
        public 编码_ _codeSys;
        public 配置文件_ (编码_ codeSys)
        {
            this._codeSys = codeSys;
            Get_班次(常量.配置文件名_默认);
            Get_日期时间(常量.配置文件名_默认, "月", "1");

        }


        /// <summary>
        /// 文件路径
        /// </summary> 
        public string GetPath_班次(string FileName)
        {
            return Path.Combine(this._codeSys._文件夹_属性.班次, $"{FileName}.txt");
        }

        /// <summary>
        /// 文件路径
        /// </summary> 
        public string GetPath_日期时间(string FileName)
        {
            return Path.Combine(this._codeSys._文件夹_属性.日期时间, $"{FileName}.txt");
        }

        public (bool s, string msg, string[] cfg) Get目录_班次()
        {
            new qfmain.文件_文件夹().文件夹_获取所有文件_无后缀(this._codeSys._文件夹_属性.班次, out List<string> lst, "*.txt");
            return (true, "", lst.ToArray());
        }
        public (bool s, string msg, string[] cfg) Get目录_日期时间()
        {
            new qfmain.文件_文件夹().文件夹_获取所有文件_无后缀(this._codeSys._文件夹_属性.日期时间, out List<string> lst, "*.txt");
            return (true, "", lst.ToArray());
        }



        /// <summary>
        /// 获取班次
        /// </summary> 
        public (bool s, string m, _班次_[] cfg) Get_班次(string FileName)
        {
            string path = GetPath_班次(FileName);
            _班次_[] Beff = 配置文件_初始数据.班次();
            bool rt = new qfmain.文件_文件夹().WriteReadJson(path, 1, ref Beff, out string msgErr);
            return (rt, msgErr, Beff);
        }

        /// <summary>
        /// 获取日期时间,
        /// <para>section : 节名称,如年4等</para>
        /// <para>keys : 字段,如2022=22 等</para>
        /// </summary> 
        public (bool s, string m, string cfg) Get_日期时间(string FileName, string section, string keys)
        {
            string path = GetPath_日期时间(FileName);
            string vxt = 配置文件_初始数据.日期时间();
            if (!new qfmain.文件_文件夹().文件_是否存在(path))
            {
                bool rt = new qfmain.文本().Save_25(path, vxt, true, out string msgErr, false, null, 1024 * 1024);
            }
            var values = new qfmain.ini_sharpconfig(path).Read(section, keys, keys);
            return (true, "", values);

        }
    }
}
