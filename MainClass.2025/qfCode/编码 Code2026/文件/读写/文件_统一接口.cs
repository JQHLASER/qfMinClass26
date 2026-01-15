using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfCode
{
    internal class 文件_统一接口
    {
        编码_ _CodeSys;
        public Iwork_文件 _Iwork文件;

        public 文件_统一接口(编码_ CodeSys)
        {
            this._CodeSys = CodeSys;

            switch (this._CodeSys._功能.文件类型)
            {
                case _功能_结构_._em_文件类型_.ini:
                    this._Iwork文件 = new ini文件_();
                    break;
                case _功能_结构_._em_文件类型_.txt:
                    this._Iwork文件 = new txt文件_();
                    break;
                case _功能_结构_._em_文件类型_.Sqlite:
                    this._Iwork文件 = new Sqlite文件_(this._CodeSys);
                    break;
                case _功能_结构_._em_文件类型_.SqlServer :
                    this._Iwork文件 = new SqlServer文件_(this._CodeSys);
                    break;
            }

        }


        /// <summary>
        /// 获取编码文件信息的路径
        /// <para>仅ini和txt类型有效</para>
        /// </summary> 
        public string GetPath_文件(string FileName)
        {
            return $"{this._CodeSys._文件夹_属性.信息}\\{FileName}.{this._CodeSys._功能.后缀}";
        }





    }
}
