using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace qfmain
{
    public class http_Server
    {
        public class _info_请求信息_
        {
            /// <summary>
            /// 路由,如http://127.0.0.1/mark/ 其中/mark为路由
            /// </summary>
            public string AbsolutePath { set; get; } = "/";

            public string 数据 { set; get; } = "";

        }

        public class Config_
        {
            public int 线程周期 { set; get; } = 1;

            /// <summary>
            /// -2:启动中,-1:未启动,0:已启动
            /// </summary>
            public int 服务启动状态 { set; get; } = -1;

            public string[] Url { set; get; } = new string[0];
        }


        public Config_ Config = new Config_();
        private bool run_ = true;
        //private readonly HttpListener _listener;
        //private readonly string[] _url;

        private HttpListener _listener;
        private string[] _url;



        //public http服务_25(string[] url)
        //{
        //    _url = url;
        //    _listener = new HttpListener();
        //    foreach (string s in _url)
        //    {
        //        _listener.Prefixes.Add(s);
        //    }
        //}

        /// <summary>
        /// 启动服务
        /// </summary>
        public virtual void Start(string[] url)
        {
            _url = url;
            _listener = new HttpListener();
            foreach (string s in _url)
            {
                _listener.Prefixes.Add(s);
            }

            this.Config.服务启动状态 = -2;
            try
            {
                _listener.Start();
                this.Config.服务启动状态 = _listener.IsListening ? 0 : -1;
            }
            catch (Exception ex)
            {
                this.Config.服务启动状态 = -1;
            }



            string show = this.Config.服务启动状态 == 0 ? "OK" : "NG";
            bool rt = this.Config.服务启动状态 == 0 ? true : false;
            On_日志(rt, $"httpServer,{Language_.Get语言("启动")},{show},{JsonConvert.SerializeObject(_url)}");

            if (!rt)
            {
                return;
            }

            while (run_)
            {
                if (this.Config.线程周期 > 0)
                {
                    Thread .Sleep (this.Config.线程周期);
                }
                if (!run_)
                {
                    break;
                }


                try
                {
                    var context = _listener.GetContext();
                    ProcessRequest(context);
                }
                catch (Exception ex)
                {
                    On_日志(false, $"httpServer,Error: {ex.Message}");
                }
            }




        }

        public virtual bool Start(string[] url, out string msgErr)
        {
            bool rt = true;
            msgErr = string.Empty;
            try
            {
                Start(url);
            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.ToString();
            }
            if (!rt)
            {
                Config.服务启动状态 = -1;
            }


            return rt;
        }



        void ProcessRequest(HttpListenerContext context)
        {
            // 获取请求信息
            var request = context.Request;
            var response = context.Response;

            // log($"{request.HttpMethod} {request.Url}  ");

            try
            {
                //// string mm = request.Headers.GetValues("");
                //string responseString;
                //// 根据请求路径处理不同路由
                //if (request.Url.AbsolutePath == "/")
                //{
                //    //  responseString = "<h1>Simple HTTP Server</h1><p>Hello World!</p>";

                //    responseString = new StreamReader(request.InputStream).ReadToEndAsync().Result;
                //}
                //else if (request.Url.AbsolutePath == "/json")
                //{
                //    responseString = "{\"message\":\"Hello JSON\"}";
                //    response.ContentType = "application/json";
                //}
                //else
                //{
                //    response.StatusCode = (int)HttpStatusCode.NotFound;
                //    responseString = "<h1>404 Not Found</h1>";
                //}



                _info_请求信息_ info = new _info_请求信息_();
                info.AbsolutePath = request.Url.AbsolutePath;
                info.数据 = new StreamReader(request.InputStream).ReadToEndAsync().Result;
                if (!string.IsNullOrEmpty(info.数据))
                {
                    string responseString = On_请求处理(info);
                    // 写入响应
                    byte[] buffer = Encoding.UTF8.GetBytes(responseString);
                    response.ContentLength64 = buffer.Length;
                    response.OutputStream.Write(buffer, 0, buffer.Length);
                }

                

                //  log(responseString);
            }
            finally
            {
                response.OutputStream.Close();
            }


        }

        /// <summary>
        /// 停止/释放
        /// </summary>
        public virtual void Stop()
        {
            Config.服务启动状态 = -1;
            run_ = false;
            _listener.Stop();
            _listener.Close();

        }

        public virtual bool Stop(out string msgErr)
        {
            bool rt = true;
            msgErr = string.Empty;
            try
            {
                Stop();
            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }
            return rt;
        }


        public virtual void 读参数(string path)
        {
            string[] url = Config.Url;
            new 文件_文件夹().WriteReadJson(path, 1, ref url, out string msgErr);
            Config.Url = url;

        }



        #region 事件

        /// <summary>
        /// 参数：(bool)状态,(string)日志
        /// </summary>
        public event Action<bool, string> Event_日志;
        /// <summary>
        /// 返回:处理后的结果
        /// <para>参数(info_请求信息_)请求信息</para>
        /// </summary>
        public event Func<_info_请求信息_, string> Event_请求处理;

        void On_日志(bool status, string logStr)
        {

            Event_日志?.Invoke(status, logStr);

        }
        string On_请求处理(_info_请求信息_ info)
        {
            string xt = Event_请求处理 is null ? "" : Event_请求处理?.Invoke(info);
            return xt;
        }





        #endregion



    }
}
