using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace qfmain
{
    /// <summary>
    /// 安装 Newtonsoft.Json
    /// </summary>
    public class Json_
    {
        /// <summary>
        /// Newtonsoft.Json;
        /// </summary> 
        public virtual (bool s, string m, string v) 序列化<T>(T T_, Formatting Formatting_ = Formatting.Indented)
        {
            bool rt = true;
            string msgErr = string.Empty;
            string jsonStr = string.Empty;
            try
            {
                jsonStr = JsonConvert.SerializeObject(T_, Formatting_);
            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }

            return (rt, msgErr, jsonStr);
        }
         
         
        /// <summary>
        /// 用的 Newtonsoft.Json
        /// </summary> 
        public virtual (bool s, string m, T v) 反序列化<T>(string jsonStr) 
        {
            T result = T_实例化泛型.FastNew<T>.Create();
            if (string.IsNullOrEmpty(jsonStr))
            {
                return (false, Language_.Get语言("不能为空"), result);
            }

            try
            {
                JToken jt = JToken.Parse(jsonStr);
                result = JsonConvert.DeserializeObject<T>(jsonStr);
                return (true, "", result);
            }
            catch (Exception ex)
            {
                return (false, ex.Message, result);
            }
        }




    }
}
