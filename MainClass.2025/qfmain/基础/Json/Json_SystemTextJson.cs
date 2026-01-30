using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;
 

namespace qfmain
{
    /// <summary>
    /// System.Text.Json
    /// </summary>
    public class Json_SystemTextJson
    {

        // 全局配置（只创建一次，线程安全）
        private    JsonSerializerOptions Options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // JSON驼峰
            WriteIndented = false, // 高性能：不格式化
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All), // 支持中文
            PropertyNameCaseInsensitive = true, // 反序列化忽略大小写
            AllowTrailingCommas = true,
            ReadCommentHandling = JsonCommentHandling.Skip
        };


        /// <summary>
        /// 序列化对象 -> JSON
        /// <para>Is格式化 =true时,格式化,方便查看,性能会略微隆低,=false:不格式化,性能高点</para>
        /// </summary>
        public (bool s, string m, string v) 序列化<T>(T obj, bool Is格式化 = false)
        {
            if (obj == null)
            {
                return (false, "null", "");
            }
            try
            {
                Options.WriteIndented = Is格式化;// 高性能：不格式化
                string v = JsonSerializer.Serialize(obj, Options);
                return (true, "", v);
            }
            catch (Exception ex)
            {
                return (false, ex.Message, "");
            }
        }

        /// <summary>
        /// 反序列化 JSON -> 对象
        /// <para>Is忽略字段大小写 =true:忽略字段大小写,=false:严格要求字段大小写</para>
        /// </summary>
        public (bool s, string m, T v) 反序列化<T>(string json, bool Is忽略字段大小写 = true)
        {
            T t = T_实例化泛型.FastNew<T>.Create();
            var rt = Is是否为json(json);
            if (!rt.s)
            {
                return (rt.s, rt.m, t);
            }
            try
            {
                Options.PropertyNameCaseInsensitive = Is忽略字段大小写;
                t = JsonSerializer.Deserialize<T>(json, Options);
                return (true, "", t);
            }
            catch (Exception ex)
            {
                return (false, ex.Message, t);
            }


        }

        public (bool s, string m) Is是否为json(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
                return (false, Language_.Get语言("不能为空"));

            try
            {
                JsonDocument.Parse(json);
                return (true, "");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }

        }



        /// <summary>
        /// System.Text.Json 最佳性能是 byte[] 而不是 string, 适合日志、高频 IO。
        /// </summary> 
        public byte[] 序列化_高性能<T>(T obj)
        {
            return JsonSerializer.SerializeToUtf8Bytes(obj, Options);
        }
        /// <summary>
        /// System.Text.Json 最佳性能是 byte[] 而不是 string, 适合日志、高频 IO。
        /// </summary> 
        public T 反序列化_高性能<T>(byte[] data)
        {
            return JsonSerializer.Deserialize<T>(data, Options);
        }






    }
}
