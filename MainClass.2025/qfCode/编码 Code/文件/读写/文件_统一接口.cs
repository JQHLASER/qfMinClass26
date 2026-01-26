using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfCode
{
    public class 文件_统一接口
    {
        编码_ _CodeSys;
        Iwork_文件 _Iwork文件;

        public 文件_统一接口(编码_ CodeSys)
        {
            this._CodeSys = CodeSys;

            switch (this._CodeSys._功能.文件类型)
            {
                case _功能_结构_._em_文件类型_.ini:
                    this._Iwork文件 = new ini文件_(this._CodeSys);
                    break;
                case _功能_结构_._em_文件类型_.txt:
                    this._Iwork文件 = new txt文件_(this._CodeSys);
                    break;
                case _功能_结构_._em_文件类型_.Sqlite:
                    this._Iwork文件 = new Sqlite文件_(this._CodeSys);
                    break;
                case _功能_结构_._em_文件类型_.SqlServer:
                    this._Iwork文件 = new SqlServer文件_(this._CodeSys);
                    break;
            }

        }

        /// <summary>
        /// 保存
        /// </summary> 
        public (bool s, string m) Save(string 配方文件名, _配方文件_属性_ 配方)
        {
            return this._Iwork文件.Save(配方文件名, 配方);
        }

        public (bool s, string m) 复制(string 配方文件名, string New配方文件名)
        {
            return this._Iwork文件.复制(配方文件名, New配方文件名);
        }

        /// <summary>
        /// 读取
        /// </summary> 
        public (bool s, string m, _配方文件_属性_ 配方) Read(string 配方文件名 )
        {
            return this._Iwork文件.Read(配方文件名);
        }

        /// <summary>
        /// 删除
        /// </summary> 
        public (bool s, string m) Delete(string 配方文件名)
        {
            return this._Iwork文件.Delete(配方文件名);
        }



    }
}
