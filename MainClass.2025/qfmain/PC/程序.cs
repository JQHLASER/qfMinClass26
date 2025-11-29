using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfmain
{
    public class 程序
    {

        /// <summary>
        /// Environment.CurrentDirectory
        /// </summary>
        /// <returns></returns>
        public virtual string 获取_当前工作目录的路径()
        {
            return Environment.CurrentDirectory;
        }
        /// <summary>
        /// Environment.CurrentDirectory
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public virtual string 设置_当前工作目录的完全限定路径(string path)
        {
            Environment.CurrentDirectory = path;
            return Environment.CurrentDirectory;
        }

        /// <summary>
        /// AppDomain.CurrentDomain.BaseDirectory
        /// </summary>
        /// <returns></returns>
        public virtual string 获取_程序基目录()
        {
            return System.AppDomain.CurrentDomain.BaseDirectory;
        }

        /// <summary>
        /// Directory.GetCurrentDirectory()
        /// </summary>
        /// <returns></returns>
        public virtual  string 获取_程序目录()
        {
            return System.IO.Directory.GetCurrentDirectory();
        }

    }
}
