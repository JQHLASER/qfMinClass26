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
        /// <typeparam name="T"></typeparam>
        /// <param name="T_"></param>
        /// <param name="jsonStr"></param>
        /// <param name="msgErr"></param>
        /// <param name="Formatting_"></param>
        /// <returns></returns>
        public virtual bool JsonTo_T<T>(T T_, out string jsonStr, out string msgErr, Formatting Formatting_ = Formatting.Indented)
        {
            bool rt = true;
            msgErr = string.Empty;
            jsonStr = string.Empty;
            try
            {
                jsonStr = JsonConvert.SerializeObject(T_, Formatting_);
            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }

            return rt;
        }

        /// <summary>
        /// Newtonsoft.Json;
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="T_"></param>
        /// <param name="jsonStr"></param>
        /// <param name="msgErr"></param>
        /// <returns></returns>
        public virtual bool T_ToJson<T>(ref T T_, string jsonStr, out string msgErr)
        {
            bool rt = true;
            msgErr = string.Empty;
            try
            {
                rt = 是否为json格式(jsonStr, out msgErr);
                if (rt)
                {
                    T_ = JsonConvert.DeserializeObject<T>(jsonStr);
                }
            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }

            return rt;
        }

         

        /// <summary>
        /// 序列化,使用自带的 System.Text.Json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="T_"></param>
        /// <param name="格式化输出">方便阅读</param>
        /// <returns></returns>
        public virtual string json序列化<T>(T T_, bool 格式化输出 = false)
        {
            string xt = System.Text.Json.JsonSerializer.Serialize(T_, new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),// 配置编码器以支持中文,数据不被转义
                WriteIndented = 格式化输出, //可选：格式化输出，便于阅读
            });

            return xt;
        }

        /// <summary>
        /// 反序列化, 使用自带的 System.Text.Json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonStr"></param>
        /// <returns></returns>
        public virtual T json反序列化<T>(string jsonStr)
        {
            T b = System.Text.Json.JsonSerializer.Deserialize<T>(jsonStr);
            return b;
        }


        /// <summary>
        /// <para>判断字符串是否为有效的 JSON 格式</para>
        /// <para>使用自带的 System.Text.Json</para>
        /// </summary>
        /// <param name="jsonStr"></param>
        /// <param name="msgErr"></param>
        /// <returns></returns>
        public virtual bool 是否为json格式(string jsonStr, out string msgErr)
        {
            msgErr = string.Empty;
            if (string.IsNullOrWhiteSpace(jsonStr))
            {
                msgErr = Language_.Get语言("不能为空");
                return false;
            }
            try
            {
                // 尝试将字符串解析为 JsonDocument
                // 这种方式不依赖具体的实体类型，可验证任何 valid JSON
                using (JsonDocument doc = JsonDocument.Parse(jsonStr))
                {
                    // 解析成功则视为有效的 JSON
                    return true;
                }
            }
            catch (System.Text.Json.JsonException ex)
            {
                msgErr = ex.Message;
                // 解析失败会抛出 JsonException，说明不是有效的 JSON
                return false;
            }
            catch (ArgumentException ex)
            {
                msgErr = ex.Message;
                // 处理可能的参数异常（如字符串包含无效的 UTF-8 字节）
                return false;
            }
        }



    }
}
