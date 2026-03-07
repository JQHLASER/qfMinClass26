using qfNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace qfCode
{
    public class 文件_统一接口
    {

        public Iwork_文件 _Iwork文件;

        public 文件_统一接口(编码_ CodeSys)
        {

            if (CodeSys._功能.配方文件类型 == _功能_结构_._em_配方文件类型_.外部文件)
            {
                this._Iwork文件 = new 外部文件(CodeSys);
            }
            else
            {
                this._Iwork文件 = new 配方文件_txt_ini_(CodeSys, CodeSys._功能.配方文件类型);
            }
             

        }




    }
}
