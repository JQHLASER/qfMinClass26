using Newtonsoft.Json;
using qfmain;
using qfNet;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace qfCode
{
    public class 配方文件_txt_ini_ : Iwork_文件
    {
        _功能_结构_._em_配方文件类型_ _文件格式 = _功能_结构_._em_配方文件类型_.ini;
        编码_ _codeSys;
        private static readonly object _lock = new object();

        public 配方文件_txt_ini_(编码_ codeSys, _功能_结构_._em_配方文件类型_ 文件格式_)
        {
            this._文件格式 = 文件格式_;
            this._codeSys = codeSys;
            this._codeSys.On_初始化状态(qfmain._初始化状态_.已初始化, "");
        }

        public (bool s, string m) Save(string FileName, _配方文件_属性_ cfg)
        {
            lock (_lock)
            {
                string jsonStr = new Json序列化().转成String(cfg);
                string Path = this.GetPath_配方(FileName);
                switch (this._文件格式)
                {
                    case _功能_结构_._em_配方文件类型_.ini:
                        return new qfmain.ini_sharpconfig(Path).Write<string>("data", "data", jsonStr, true);
                    case _功能_结构_._em_配方文件类型_.txt:
                        bool rt = new qfmain.文本().Save_25(Path, jsonStr, true, out string msgErr, false);
                        return (rt, msgErr);
                    default:
                        return (false, "无此功能");
                }

            }
        }

        public (bool s, string m, _配方文件_属性_ cfg) Read(string FileName)
        {
            lock (_lock)
            {
                string Path = this.GetPath_配方(FileName);
                switch (this._文件格式)
                {
                    case _功能_结构_._em_配方文件类型_.ini:
                        #region ini

                        (bool s, string m, string json) rt = new qfmain.ini_sharpconfig(Path).ReadStr("data", "data", "{}");
                        (bool s, string m, _配方文件_属性_ cfg) rtCfg = new Json序列化().转成Json<_配方文件_属性_>(rt.json);

                        if (!rt.s)
                        {
                            return (rt.s, rt.m, default);
                        }
                        else if (!rtCfg.s)
                        {
                            return (rtCfg.s, rtCfg.m, default);
                        }

                        #endregion
                        return (rtCfg.s, rtCfg.m, rtCfg.cfg);
                    case _功能_结构_._em_配方文件类型_.txt:
                        (bool s, string m, string json) rttxt = new qfmain.文本().Read_25(Path);
                        (bool s, string m, _配方文件_属性_ cfg) rtCfgtxt = new Json序列化().转成Json<_配方文件_属性_>(rttxt.json);
                        if (!rttxt.s || !rtCfgtxt.s)
                        {
                            return (rttxt.s, rttxt.m, rtCfgtxt.cfg);
                        }
                        return (rttxt.s, rttxt.m, rtCfgtxt.cfg);

                    default:
                        return (false, "无此功能", new _配方文件_属性_()); 
                }
            }
        }


        /// <summary>
        /// 导出全部时用
        /// </summary> 
        public (bool s, string m, qfNet.表.Code26[] cfg) ReadAll( )
        {
            return (true, "无此功能", new 表.Code26[0]);
        }

        /// <summary>
        /// 导出全部时用
        /// </summary> 
        public (bool s, string m  ) SaveAll(  qfNet.表.Code26[] cfg)
        {
            return (true, "无此功能" );
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
