using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfCode
{
    public interface Iwork_文件
    {
        
        (bool s, string m, _文件_属性_ cfg) Read(string FileName);
        (bool s, string m) Save(string FileName, _文件_属性_ cfg);
        (bool s,string m) Delete(string FileName);
        (bool s,string m) 另存为(string FileName,string NewFileName);


    }
}
