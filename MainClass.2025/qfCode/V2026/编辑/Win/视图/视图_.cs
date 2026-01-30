using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfCode
{
    internal class 视图_
    {

        internal type_编辑._视图_ _cfg = new type_编辑._视图_();
        编辑_ _编辑;
        public 视图_ (编辑_ 编辑)
        {
            this._编辑 = 编辑;
        }


        internal void 读写参数(ushort model)
        {
            string path = this._编辑._编码._文件夹_属性.参数 + "\\viewcfg.dll";
            type_编辑._视图_ cfg = this._cfg.Clone();
            new qfmain.文件_文件夹().WriteReadIni(path , model, ref cfg, out string msgErr);
            this._cfg = cfg;
        }






    }
}
