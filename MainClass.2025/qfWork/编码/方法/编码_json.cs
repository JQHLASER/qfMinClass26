using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfWork
{
    internal class 编码_json
    {

        internal string json_生成<T>(T info)
        {
            return JsonConvert.SerializeObject(info);
        }

        internal T json_解析<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }

    }
}
