using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfCode
{
    public interface Iwork_配置文件
    {


        /// <summary>
        /// 文件路径
        /// </summary> 
        string GetPath_班次(string FileName);

        /// <summary>
        /// 文件路径
        /// </summary> 
        string GetPath_日期时间(string FileName);


        (bool s, string msg, string[] cfg) Get目录_班次();

        (bool s, string msg, string[] cfg) Get目录_日期时间();

        (bool s, string m, _班次_[] cfg) Get_班次(string FileName);

        /// <summary>
        /// 获取日期时间,
        /// <para>section : 节名称,如年4等</para>
        /// <para>keys : 字段,如2022=22 等</para>
        /// </summary> 
        (bool s, string m, string cfg) Get_日期时间(string FileName, string section, string keys);







    }
}
