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


            switch (CodeSys._功能.配方文件类型)
            {
                case _功能_结构_._em_配方文件类型_.ini:
                    this._Iwork文件 = new ini文件_(CodeSys);
                    break;
                case _功能_结构_._em_配方文件类型_.txt:
                    this._Iwork文件 = new txt文件_(CodeSys);
                    break;
                case _功能_结构_._em_配方文件类型_.Sqlite:
                    this._Iwork文件 = new Sqlite文件_(CodeSys);
                    break;
                case _功能_结构_._em_配方文件类型_.SqlServer:
                    this._Iwork文件 = new SqlServer文件_(CodeSys);
                    break;
                case _功能_结构_._em_配方文件类型_.外部文件:
                    this._Iwork文件 = new 外部文件(CodeSys);
                    break;
            }

        }




    }
}
