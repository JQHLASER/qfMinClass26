using qfmain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace qfCode
{
    public class txt文件_ : Iwork_文件
    {
        编码_ _codeSys;
        private static readonly object _lock = new object();
        public txt文件_(编码_ codeSys)
        {
            this._codeSys = codeSys;
        }
        public (bool s, string m) Save(string FileName, _配方文件_属性_ cfg)
        {
            lock (_lock)
            {
                string Path = this._codeSys._文件类.GetPath_配方(FileName);
                string jsonStr = new Json序列化().转成String(cfg);
                bool rt = new qfmain.文本().Save_25(Path, jsonStr, true, out string msgErr, false);
                return (rt, msgErr);
            }
        }

        public (bool s, string m, _配方文件_属性_ cfg) Read(string FileName)
        {
            lock (_lock)
            {
                string Path = this._codeSys._文件类.GetPath_配方(FileName);
                (bool s, string m, string json) rt = new qfmain.文本().Read_25(Path);
                (bool s, string m, _配方文件_属性_ cfg) rtCfg = new Json序列化().转成Json<_配方文件_属性_>(rt.json); 
                if (!rt.s || !rtCfg.s)
                {
                    return (rt.s, rt.m, rtCfg.cfg);
                }
                return (rt.s, rt.m, rtCfg.cfg);
            }
        }

        public (bool s, string m) Delete(string FileName)
        {
            lock (_lock)
            {
                string Path = this._codeSys._文件类.GetPath_配方(FileName);
                bool rt = new qfmain.文件_文件夹().文件_删除文件(Path, out string msgErr);
                return (rt, msgErr);
            }
        }

        public (bool s, string m) 复制(string FileName, string NewFileName)
        {
            lock (_lock)
            {
                string Path = this._codeSys._文件类.GetPath_配方(FileName);
                string PathNew = this._codeSys._文件类.GetPath_配方(FileName);
                bool rt = new qfmain.文件_文件夹().文件_复制文件(Path, PathNew, out string msgErr, true);
                return (rt, msgErr);
            }
        }

      

    }
}
