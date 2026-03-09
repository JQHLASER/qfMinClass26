using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static qfmain.log日志;

namespace qfWork
{
    public class Zaxis_运动轴
    {
        public Zaxis_数据结构._inf_轴状态_ _轴状态 = new Zaxis_数据结构._inf_轴状态_();
        public Zaxis_数据结构._inf_轴参数_[] _轴参数 = new Zaxis_数据结构._inf_轴参数_[0];
        public string path = Path.Combine(qfmain.软件类.Files_Cfg.Files_Config, "zaxisCfg.dll");

        Zaxis _Zaxis_sys;
        public Zaxis_运动轴(Zaxis zaxis_)
        {
            this._Zaxis_sys = zaxis_;
            this._Zaxis_sys.Event_其它 += () =>
            {

            };

        }

        public (bool s, string m) 参数读写_轴参数(ushort model, ref Zaxis_数据结构._inf_轴参数_[] cfg)
        { 
            Zaxis_数据结构._inf_轴参数_[] _cfg = _轴参数 ;
            bool rt = new qfmain.文件_文件夹().WriteReadJson(path, model, ref _cfg, out string msgErr);
            _轴参数 = _cfg;
            return (rt, msgErr);
        }












    }
}
