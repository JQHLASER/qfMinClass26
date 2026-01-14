using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfCode
{
    public interface Iwork_文件_统一接口
    {
        (bool s, string m,string jsonStr) Read(string FileName);
        (bool s, string m) Write(string FileName,string jsonStr);

    }
}
