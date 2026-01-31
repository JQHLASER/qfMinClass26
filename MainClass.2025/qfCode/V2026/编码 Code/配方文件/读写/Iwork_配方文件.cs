using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfCode
{
    public   interface Iwork_文件
    {
        /// <summary>
        /// 读取
        /// </summary> 
        (bool s, string m, _配方文件_属性_ cfg) Read(string FileName);
        /// <summary>
        /// 保存
        /// </summary> 
        (bool s, string m) Save(string FileName, _配方文件_属性_ cfg);
        /// <summary>
        /// 删除
        /// </summary> 
        (bool s,string m) Delete(string FileName);

        /// <summary>
        /// 另存为
        /// </summary> 
        (bool s,string m) 复制(string FileName,string NewFileName );
        (bool s, string m, string[] v) Get目录();

    }
}
