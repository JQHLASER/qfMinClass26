using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfCode
{
    public class txt文件_ : Iwork_文件
    {
        public (bool s, string m) Write(string Path, _文件_属性_ cfg)
        {
            string jsonStr = new Json序列化().转成String(cfg);
            bool rt = new qfmain.文本().Save_25(Path, jsonStr, true, out string msgErr, false);
            return (rt, msgErr);
        }

        public (bool s, string m, _文件_属性_ cfg) Read(string Path)
        {
            (bool s, string m, string json) rt = new qfmain.文本().Read_25(Path);
            (bool s, string m, _文件_属性_ cfg) rtCfg = new Json序列化().转成Json(rt.json);
            if (!rt.s || !rtCfg.s)
            {
                return (rt.s, rt.m, rtCfg.cfg);
            }
            return (rt.s, rt.m, rtCfg.cfg);
        }


    }
}
