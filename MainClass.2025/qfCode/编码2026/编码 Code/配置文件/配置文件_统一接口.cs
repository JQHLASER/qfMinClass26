using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfCode
{
    public class 配置文件_统一接口
    {
        public Iwork_配置文件 _Iwork;


        public 配置文件_统一接口(编码_ codeSys)
        {
            switch (codeSys._功能.配置文件类型)
            {
                case _em_配置文件类型_.Txt:
                    _Iwork = new 配置文件_txt(codeSys);
                    break;
                case _em_配置文件类型_.SQLite:
                    _Iwork = new 配置文件_SQLite(codeSys);
                    break;
            }
        }




    }
}
