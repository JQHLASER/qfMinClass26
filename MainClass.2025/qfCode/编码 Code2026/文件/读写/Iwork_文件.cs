using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfCode
{
    public interface Iwork_文件
    {
        /// <summary>
        /// 
        /// </summary> 
        (bool s, string m, _文件_属性_ cfg) Read(string FileName);
        (bool s, string m) Write(string FileName, _文件_属性_ cfg);

    }
}
