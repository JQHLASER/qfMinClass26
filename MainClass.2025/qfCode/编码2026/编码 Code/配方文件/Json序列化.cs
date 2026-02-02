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
            return new qfmain.Json_SystemTextJson  ().序列化<T>(cfg).v;
        }

        public (bool s, string m, T cfg) 转成Json<T>(string jsonStr) where T : new()
        {
            return new qfmain.Json_SystemTextJson ().反序列化<T>(jsonStr);
        }




    }
}
