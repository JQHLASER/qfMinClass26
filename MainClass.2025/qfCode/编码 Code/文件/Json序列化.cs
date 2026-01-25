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

        public string 转成String<T>(T cfg)
        {
            return JsonConvert.SerializeObject(cfg);
        }

        public (bool s, string m, T cfg) 转成Json<T>(string jsonStr)
        {
            return new qfmain.Json_().是否为json格式<T>(jsonStr);
        }




    }
}
