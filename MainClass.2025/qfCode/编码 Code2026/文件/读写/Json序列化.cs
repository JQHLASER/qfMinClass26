using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfCode
{
    internal class Json序列化
    {

        public string 转成String(_文件_属性_ cfg)
        {
            return JsonConvert.SerializeObject(cfg);
        }

        public (bool s, string m, _文件_属性_ cfg) 转成Json(string jsonStr)
        {
            return new qfmain.Json_().是否为json格式<_文件_属性_>(jsonStr);
        }




    }
}
