using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace qfmain
{
    public class http_Client
    {
        public enum enum请求方式
        {
            Post,
            Get,
            PUT,
            DEL,
        }

        public enum enum标头值
        {
            application_json,
            text,
        }



        public class Headers
        {
            public string Key_ { set; get; } = "";
            public string Value_ { set; get; } = "";

        }


        public virtual string HTTP标头值(int dayNumber)
        {
            var days = new Dictionary<int, string>
             {
                { 0, "application/json" },
                { 1, "text" },
               };

            return days.TryGetValue(dayNumber, out var day) ? day : "application/json";
        }

        /// <summary>
        /// lstHeaders : =null 时不使能
        /// </summary>
        /// <param name="请求方式"></param>
        /// <param name="url"></param>
        /// <param name="lstHeaders"></param>
        /// <param name="Body"></param>
        /// <param name="msg"></param>
        /// <param name="HTTP标头值_"></param>
        /// <returns></returns>
        public virtual bool 请求(enum请求方式 请求方式, string url, List<Headers> lstHeaders, string Body, out string msg, int 超时时间 = 10000, enum标头值 HTTP标头值_ = enum标头值.application_json)
        {
            msg = string.Empty;
            bool rt = true;

            try
            {
                // 创建HttpClient实例
                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        client.Timeout = TimeSpan.FromMilliseconds(超时时间); // 根据需求调整;                   


                        if (lstHeaders != null)
                        {
                            foreach (var s in lstHeaders)
                            {
                                client.DefaultRequestHeaders.Add(s.Key_, s.Value_);
                            }
                        }
                        HttpResponseMessage response = null;
                        switch (请求方式)
                        {
                            case enum请求方式.Post:
                                // 发送Post请求
                                string 标头值 = HTTP标头值((int)HTTP标头值_);

                                HttpContent content = new StringContent(Body, Encoding.UTF8, 标头值);
                                //  HttpContent content = new StringContent(Body);    
                                response = client.PostAsync(url, content).Result;
                                break;
                            case enum请求方式.Get:
                                // 发送GET请求
                                response = client.GetAsync(url).Result;
                                break;
                        }


                        // 确保请求成功
                        response.EnsureSuccessStatusCode();

                        // 读取响应内容
                        string responseBody = response.Content.ReadAsStringAsync().Result;

                        // 输出响应内容
                        // Console.WriteLine("Response: " + responseBody);
                        msg = responseBody;
                    }
                    catch (HttpRequestException e)
                    {
                        // 处理请求异常
                        //  Console.WriteLine("Request error: " + e.Message);
                        msg = e.Message;
                        rt = false;
                    }
                }

            }
            catch (Exception ex)
            {
                rt = false;
                msg = ex.Message;
            }
            return rt;
        }



        /// <summary>
        ///  model: 0:Post,1:Get,2:PUT,3:DEL
        ///  <para>msg:反馈数据</para>
        /// </summary>
        /// <param name="url"></param>
        /// <param name="model"></param>
        /// <param name="参数"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public virtual bool 请求_2022(string url, short model, string 参数, out string msg, string HTTP标头值 = "application/json")
        {
            msg = string.Empty;
            if (string.IsNullOrEmpty(url))
            {
                msg = "请输入需要请求的路径";
                return false;
            }
            if (string.IsNullOrEmpty(参数) && model == 0) //post
            {
                msg = "post,请输入请求参数";
                return false;
            }
            if (string.IsNullOrEmpty(HTTP标头值))
            {
                msg = "请输入HTTP标头值";
                return false;
            }

            HttpWebRequest request = null;
            StreamReader sr = null;
            string result = string.Empty;
            Stream st = null;

            bool rt = true;

            try
            {

                request = (HttpWebRequest)WebRequest.Create(url.Trim());   //创建连接


                request.Method = model == 0 ? "Post" : model == 1 ? "Get" : model == 2 ? "Put" : "DELETE";   //请求设置
                if (model == 0 || (model == 2 && !string.IsNullOrWhiteSpace(参数)) || (model == 3 && !string.IsNullOrWhiteSpace(参数)))
                {
                    byte[] by = Encoding.GetEncoding("utf-8").GetBytes(参数.Trim());   //请求参数转码
                                                                                     //  request.UserAgent =  ;
                    request.ContentType = HTTP标头值.Trim();
                    request.ContinueTimeout = 500000;
                    request.ContentLength = by.Length;

                    Stream stw = request.GetRequestStream();     //获取绑定相应流
                    stw.Write(by, 0, by.Length);      //写入流
                    stw.Close();    //关闭流
                }
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();    //返回流接收
                st = response.GetResponseStream();
                sr = new StreamReader(st, Encoding.GetEncoding("utf-8"));
                result = sr.ReadToEnd();    //一次读完
            }
            catch (Exception ex)
            {
                result = "请求异常原因:" + ex.ToString();
                rt = false;
            }
            finally
            {
                if (request != null)
                {
                    request.Abort();
                }
                if (st != null)
                {
                    st.Close();
                }
                if (sr != null)
                {
                    sr.Close();
                }
            }

            msg = result;
            return rt;
        }



        public virtual bool 请求_Request(enum请求方式 请求方式, string url, short model, string Body, out string msg, enum标头值 HTTP标头值_ = enum标头值.application_json)
        {

            bool rt = true;

            try
            {


                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = $"{请求方式}";//POST
                string 标头值 = HTTP标头值((int)HTTP标头值_);
                request.ContentType = 标头值;
                byte[] data = Encoding.UTF8.GetBytes(Body);
                request.ContentLength = data.Length;

                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(data, 0, data.Length);
                }


                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    msg = new StreamReader(response.GetResponseStream()).ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    using (var errorResponse = (HttpWebResponse)ex.Response)
                    {
                        msg = new StreamReader(errorResponse.GetResponseStream()).ReadToEnd();
                    }
                }
                else
                {
                    msg = "No response received: " + ex.Message;

                }

                rt = false;
            }


            return rt;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="jsonBody"></param>
        /// <param name="msgErr"></param>
        /// <returns></returns>
        public virtual  bool 请求_Request_post(string url, string jsonBody, out string msgErr)
        {
            bool rt = true;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";
            byte[] data = Encoding.UTF8.GetBytes(jsonBody);
            request.ContentLength = data.Length;

            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(data, 0, data.Length);
            }

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    msgErr = new StreamReader(response.GetResponseStream()).ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    using (var errorResponse = (HttpWebResponse)ex.Response)
                    {
                        msgErr = new StreamReader(errorResponse.GetResponseStream()).ReadToEnd();
                    }
                }
                else
                {
                    msgErr = "No response received: " + ex.Message;
                }

                rt = false;
            }


            return rt;
        }





    }
}
