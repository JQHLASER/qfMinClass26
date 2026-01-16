using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfCode
{
    public class ini文件_ : Iwork_文件
    {
        编码_ _codeSys;
        public ini文件_(编码_ codeSys)
        {
            this._codeSys = codeSys;
        }

        public (bool s, string m) Write(string FileName, _文件_属性_ cfg)
        { 
            string jsonStr = new Json序列化().转成String(cfg);
            return new qfmain.ini_sharpconfig(Path).Write<string>("data", "data", jsonStr, true);
        }

        public (bool s, string m, _文件_属性_ cfg) Read(string Path)
        {
            (bool s, string m, string json) rt = new qfmain.ini_sharpconfig(Path).ReadStr("data", "data", "");
            (bool s, string m, _文件_属性_ cfg) rtCfg = new Json序列化().转成Json(rt.json);
            if (!rt.s || !rtCfg.s)
            {
                return (rt.s, rt.m, rtCfg.cfg);
            }
            return (rt.s, rt.m, rtCfg.cfg);
        }

    }
}
