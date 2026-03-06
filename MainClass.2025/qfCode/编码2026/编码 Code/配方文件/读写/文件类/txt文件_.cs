using qfmain;
using System;
using System.Collections.Generic;
using System.IO;
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
            this._codeSys.On_初始化状态(qfmain._初始化状态_.已初始化, "");
        }
        public (bool s, string m) Save(string FileName, _配方文件_属性_ cfg)
        {
            lock (_lock)
            {
                string Path = this.GetPath_配方(FileName);
                string jsonStr = new Json序列化().转成String(cfg);
                bool rt = new qfmain.文本().Save_25(Path, jsonStr, true, out string msgErr, false);
                return (rt, msgErr);
            }
        }

        public (bool s, string m, _配方文件_属性_ cfg) Read(string FileName)
        {
            lock (_lock)
            {
                string Path = this.GetPath_配方(FileName);
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
                string Path = this.GetPath_配方(FileName);
                bool rt = new qfmain.文件_文件夹().文件_删除文件(Path, out string msgErr);
                return (rt, msgErr);
            }
        }

        public (bool s, string m) 复制(string FileName, string NewFileName)
        {
            lock (_lock)
            {
                string Path = this.GetPath_配方(FileName);
                string PathNew = this.GetPath_配方(NewFileName);
                bool rt = new qfmain.文件_文件夹().文件_复制文件(Path, PathNew, out string msgErr, true);
                return (rt, msgErr);
            }
        }

        public (bool s, string m, string[] v) Get目录()
        {
            new qfmain.文件_文件夹().文件夹_获取所有文件_无后缀(this._codeSys._文件夹_属性.配方, out List<string> lst, $"*{this._codeSys._功能.后缀}");
            return (true, "", lst.ToArray());
        }

        string GetPath_配方(string FileName)
        {
            return Path.Combine(this._codeSys._文件夹_属性.配方, $"{FileName}{this._codeSys._功能.后缀}");
        }

    }
}
