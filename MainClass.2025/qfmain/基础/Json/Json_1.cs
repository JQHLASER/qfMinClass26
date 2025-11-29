

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace qfmain
{
    /// <summary>
    /// 安装 Newtonsoft.Json
    ///<para> { } 时使用JObject,[ ] 时便用JArray</para>
    /// </summary>
    internal class Json_1
    {

        public JArray 读取json_从文件_JArray(string jsonPath)
        {
            return JArray.Parse(File.ReadAllText(jsonPath, Encoding.Default));
        }


        public JArray 读取json_Str_JArray(string josnString)
        {

            return JArray.Parse(josnString);
        }


        public JObject 读取json_从文件_JObject(string jsonPath)
        {
            return JObject.Parse(File.ReadAllText(jsonPath, Encoding.Default));
        }


        public JObject 读取json_Str_JObject(string josnString)
        {
            return JObject.Parse(josnString);
        }

        /// <summary>
        /// { } 时使用
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        public string[] 获取_名称(JObject jo)
        {
            return (from item in jo.Properties()
                    select item.Name.ToString()).ToArray<string>();
        }


        public string[] 获取_值Value(JObject jo)
        {
            return (from item in jo.Properties()
                    select item.Value.ToString()).ToArray<string>();
        }



        public void 获取_名称(JObject jo, out string[] 名称)
        {
            名称 = 获取_名称(jo);
        }


        public void 获取_值Value(JObject jo, out string[] 值value)
        {
            值value = 获取_值Value(jo);
        }

        public void 获取_名称和值(JObject jo, out string[] name, out string[] value)
        {
            name = (from item in jo.Properties() select item.Name.ToString()).ToArray<string>();
            value = (from item in jo.Properties() select item.Value.ToString()).ToArray<string>();
        }


        public class Info_名称和值
        {
            public string 名称 { set; get; } = "";
            public string 值 { set; get; } = "";
        }


        /// <summary>
        /// lstName_Value: [名称,值]
        /// </summary>
        /// <param name="jo"></param>
        /// <param name="lstName_Value"></param>
        public void 获取_名称和值(JObject jo, out List<string[]> lstName_Value)
        {
            string[] name = (from item in jo.Properties() select item.Name.ToString()).ToArray<string>();
            string[] value = (from item in jo.Properties() select item.Value.ToString()).ToArray<string>();

            lstName_Value = new List<string[]>();
            for (int i = 0; i < name.Length; i++)
            {
                List<string> lst = new List<string>();
                lst.Add(name[i]);
                lst.Add(value[i]);
                lstName_Value.Add(lst.ToArray());
            }
        }


        public void 获取_名称和值(JObject jo, out List<Info_名称和值> lstName_Value)
        {
            string[] name = (from item in jo.Properties() select item.Name.ToString()).ToArray<string>();
            string[] value = (from item in jo.Properties() select item.Value.ToString()).ToArray<string>();

            lstName_Value = new List<Info_名称和值>();
            for (int i = 0; i < name.Length; i++)
            {
                Info_名称和值 info = new Info_名称和值();
                info.名称 = name[i];
                info.值 = value[i];

                lstName_Value.Add(info);
            }
        }

        public void 获取_名称和值(JObject jo, out Info_名称和值[] Value)
        {
            获取_名称和值(jo, out List<Info_名称和值> lstName_Value);
            Value = lstName_Value.ToArray();

        }


        public void 去除换行符(ref string jsonStr)
        {

            jsonStr = new 文本().替换(jsonStr, "\r\n", "");
            jsonStr = new 文本().替换(jsonStr, "\r", "");
            jsonStr = new 文本().替换(jsonStr, "\n ", "");
        }





        private bool 删除(JObject jo, string Name)
        {
            return jo.Remove(Name);
        }


        private void 修改(JObject jo, string Name, string value)
        {
            jo[Name] = value;
        }



        private T 添加<T>(string value, T t)
        {
            T jo = JsonConvert.DeserializeObject<T>(value);  //json格式化

            return jo;
        }

        /// <summary>
        /// value也可以是datatable类型
        /// </summary>
        /// <param name="jo"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private JObject 添加(JObject jo, string value)
        {
            JObject jObject = new JObject();



            //jObject.Add("appid",appid);   //根据自己要求，可以添加数据
            //jObject.Add("pushtime",pushtime); 
            jObject.Add("datas", value);


            return jObject;


        }



    }
}
