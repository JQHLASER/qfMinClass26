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

    using Newtonsoft.Json;
    using System;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public class http_Server
    {
        #region 内部类型

        public class _info_请求信息_
        {
            /// <summary>
            /// 路由，如 http://127.0.0.1/mark/ 中的 /mark
            /// </summary>
            public string AbsolutePath { get; set; } = "/";

            /// <summary>
            /// 请求方法 GET / POST
            /// </summary>
            public string Method { get; set; } = "GET";

            /// <summary>
            /// Body 数据
            /// </summary>
            public string 数据 { get; set; } = "";
        }



        #endregion

        #region 字段

        public volatile qfmain._启动状态_ _服务启动状态 = qfmain._启动状态_.未启动;

        public string[] _Url = Array.Empty<string>();

        private HttpListener _listener;
        private CancellationTokenSource _cts;

        #endregion

        #region 启动 / 停止

        /// <summary>
        /// 异步启动（不阻塞调用线程）
        /// </summary>
        public void Start(string[] url)
        {
            if (_listener != null && _listener.IsListening)
                return;

            On_启动状态(_启动状态_.启动中);
            _listener = new HttpListener();
            foreach (var u in url)
                _listener.Prefixes.Add(u);

            _cts = new CancellationTokenSource();

            try
            {
                _listener.Start();
                On_启动状态(_启动状态_.已启动);
            }
            catch (Exception ex)
            { 
                On_启动状态(_启动状态_.未启动);
                On_日志(false, ex.ToString());
                return;
            }

            //On_日志(true, $"httpServer, OK,{JsonConvert.SerializeObject(url)}");

            // 后台监听
            Task.Run(() => ListenLoopAsync(_cts.Token));
        }

        /// <summary>
        /// 停止
        /// </summary>
        public void Stop()
        {
            try
            {

                On_启动状态(_启动状态_.未启动);

                _cts?.Cancel();

                if (_listener != null)
                {
                    if (_listener.IsListening)
                        _listener.Stop();

                    _listener.Close();
                }
            }
            catch { }
        }

        #endregion

        #region 监听循环

        private async Task ListenLoopAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                HttpListenerContext context = null;

                try
                {
                    context = await _listener.GetContextAsync();
                }
                catch (HttpListenerException)
                {
                    break; // Stop 时会进这里
                }
                catch (ObjectDisposedException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    On_日志(false, ex.ToString());
                    continue;
                }

                // 并发处理请求
                _ = Task.Run(() => ProcessRequestAsync(context), token);
            }
        }

        #endregion

        #region 请求处理

        private async Task ProcessRequestAsync(HttpListenerContext context)
        {
            var request = context.Request;
            var response = context.Response;

            try
            {
                var info = new _info_请求信息_
                {
                    AbsolutePath = request.Url.AbsolutePath,
                    Method = request.HttpMethod
                };

                // 读取 Body（POST / PUT）
                if (request.HasEntityBody)
                {
                    using (var reader = new StreamReader(request.InputStream, request.ContentEncoding))
                    {
                        info.数据 = await reader.ReadToEndAsync();
                    }
                }

                string result = "";

                try
                {
                    result = Event_请求处理?.Invoke(info) ?? "";
                }
                catch (Exception ex)
                {
                    On_日志(false, ex.ToString());
                    response.StatusCode = 500;
                    result = "Server Error";
                }

                byte[] buffer = Encoding.UTF8.GetBytes(result);

                response.StatusCode = 200;
                response.ContentType = "application/json; charset=utf-8";
                response.ContentLength64 = buffer.Length;

                await response.OutputStream.WriteAsync(buffer, 0, buffer.Length);
            }
            catch (Exception ex)
            {
                On_日志(false, ex.ToString());
            }
            finally
            {
                try { response.OutputStream.Close(); } catch { }
            }
        }

        #endregion

        #region 参数读取

        public virtual void 读参数(string path)
        {
            string[] url = this._Url;
            new 文件_文件夹().WriteReadJson(path, 1, ref url, out _);
            this._Url = url;
        }

        #endregion

        #region 事件

        /// <summary>
        /// 参数：(bool)状态,(string)日志
        /// </summary>
        public event Action<bool, string> Event_日志;
        private void On_日志(bool status, string log)
        {
            Event_日志?.Invoke(status, log);
        }



        /// <summary>
        /// 请求处理，返回响应内容
        /// </summary>
        public event Func<_info_请求信息_, string> Event_请求处理;

        public event Action<qfmain._启动状态_> Event_启动状态;
        private void On_启动状态(qfmain._启动状态_ state)
        {
            this._服务启动状态 = state;
            Event_启动状态?.Invoke(state);
        }


        #endregion
    }



}
