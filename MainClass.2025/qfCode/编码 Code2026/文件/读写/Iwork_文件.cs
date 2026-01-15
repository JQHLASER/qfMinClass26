using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfCode
{
    public interface Iwork_文件
    {
        (bool s, string m, _文件_属性_ cfg) Read(string Path);
        (bool s, string m) Write(string Path, _文件_属性_ cfg);

    }
}
