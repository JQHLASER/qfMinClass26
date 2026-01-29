using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfCode
{
    /// <summary>
    /// 编辑
    /// </summary>
    internal class 编辑交互_统一接口
    {
        public Iworker_编辑交互 _Iworker;


        public 编辑交互_统一接口(编辑_ 编辑)
        {
            switch (编辑._交互类型)
            {
                case type_编辑._交互类型_.本地:
                    this._Iworker = new 编辑交互_本地(编辑); break;
            }
        }



    }
}
