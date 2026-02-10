using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfmain
{
    public class 生产计数
    {

        public class _cfg_生产计数_
        {
            public long 良品 { set; get; } = 0;
            public long 不良品 { set; get; } = 0;
            public long 零件 { set; get; } = 0;

            public _cfg_生产计数_ Clone()
            {
                return new _cfg_生产计数_()
                {
                    良品 =this.良品 ,
                    不良品 =this.不良品 ,
                    零件  =this.零件  ,
                };
            }

        }

        public bool _使能_零件 = false;
        public bool _使能_良品计数 = false;
        public bool _使能_不良品计数 = false;



        string _文件名称 = "";
        public 生产计数(string 文件名称 = "js")
        {
            this._文件名称 = 文件名称;
            读写信息(1);
        }


        /// <summary>
        /// 当前数据信息
        /// </summary>
        public _cfg_生产计数_ _当前计数信息 = new _cfg_生产计数_();
        public virtual  void 读写信息(ushort model)
        {
            string path = qfmain.软件类.Files_Cfg.Files_sysConfig + $"\\{this._文件名称}.dll";
            _cfg_生产计数_ info = this._当前计数信息;
            new qfmain.文件_文件夹().WriteReadIni(path, model, ref info, out string msgErr);
            this._当前计数信息 = info;

            this._当前计数信息.零件 = _使能_零件 ? this._当前计数信息.零件 : 0;

        }

          
        public  virtual void 计数递增_良品(int 递增量 = 1)
        {
            if (this._使能_良品计数)
            {
                this._当前计数信息.良品 += 递增量;
                读写信息(0);
          
            }

        }

        public virtual void 计数递增_不良品(int 递增量 = 1)
        {
            if (this._使能_不良品计数)
            {
                this._当前计数信息.不良品 += 递增量;
                读写信息(0);             
            }
        }


        public virtual void 设置(long 零件, long 良品, long 不良品)
        {
           this. _当前计数信息.零件 = this._使能_零件 ? 零件 : this._当前计数信息.零件;
            this._当前计数信息.良品 = this._使能_良品计数 ? 良品 : this._当前计数信息.良品;
            this._当前计数信息.不良品 = this._使能_不良品计数 ? 不良品 : this._当前计数信息.不良品;
            读写信息(0);

        }





    }

}
