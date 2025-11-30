using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace qf_WebSocket
{
    public class Server
    {
         
        private HttpListener _listener;
        private CancellationTokenSource _cts;
        private int _clientIdSeed = 0;

        /// <summary>
        /// 所有客户端
        /// </summary>
        private ConcurrentDictionary<int, WebSocket> _clients
            = new ConcurrentDictionary<int, WebSocket>();

        #region 事件

        /// <summary>
        /// 服务启动状态 true=启动 false=停止
        /// </summary>
        public event Action<bool> Event_ServerStatus;

        /// <summary>
        /// 客户端连接 / 断开
        /// </summary>
        public event Action<int, bool> Event_客户端上下线; // clientId, online




        /// <summary>
        /// 接收到字符串消息
        /// </summary>
        public event Action<int, string> Event_StringReceived;

        /// <summary>
        /// 接收到二进制消息
        /// </summary>
        public event Action<int, byte[]> Event_BinaryReceived;




        #endregion

        /// <summary>
        /// 启动 WebSocket 服务
        /// </summary>
        /// <param name="prefix">如: http://+:5000/ws/</param>
        public async Task StartAsync(string prefix)
        {
            if (_listener != null)
            {
                //服务已启动
                Stop();
                await Task.Delay(500);
            }

            _cts = new CancellationTokenSource();
            _listener = new HttpListener();
            _listener.Prefixes.Add(prefix);
            _listener.Start();

            Event_ServerStatus?.Invoke(true);

            await Task.Run(async () =>
            {
                while (!_cts.IsCancellationRequested)
                {
                    HttpListenerContext context = null;
                    try
                    {
                        context = await _listener.GetContextAsync();
                    }
                    catch
                    {
                        break;
                    }

                    if (!context.Request.IsWebSocketRequest)
                    {
                        context.Response.StatusCode = 400;
                        context.Response.Close();
                        continue;
                    }

                    _ = HandleClientAsync(context);
                }
            });
        }

        /// <summary>
        /// 停止服务
        /// </summary>
        public async Task Stop()
        {
            try
            {
                _cts?.Cancel();

                foreach (var ws in _clients.Values)
                {
                    try
                    {
                        await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "Server stop", CancellationToken.None);
                    }
                    catch
                    {
                    }
                }

                _clients.Clear();

                _listener?.Stop();
                _listener = null;

                Event_ServerStatus?.Invoke(false);
            }
            catch { }
        }

        private async Task HandleClientAsync(HttpListenerContext context)
        {
            WebSocketContext wsContext = null;
            try
            {
                wsContext = await context.AcceptWebSocketAsync(null);
            }
            catch
            {
                context.Response.StatusCode = 500;
                context.Response.Close();
                return;
            }

            int clientId = Interlocked.Increment(ref _clientIdSeed);
            WebSocket socket = wsContext.WebSocket;
            _clients[clientId] = socket;

            Event_客户端上下线?.Invoke(clientId, true);

            byte[] buffer = new byte[4096];

            try
            {
                while (socket.State == WebSocketState.Open)
                {
                    var result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), _cts.Token);

                    if (result.MessageType == WebSocketMessageType.Close)
                        break;

                    byte[] data = new byte[result.Count];
                    Array.Copy(buffer, data, result.Count);

                    if (result.MessageType == WebSocketMessageType.Text)
                    {
                        string msg = Encoding.UTF8.GetString(data);
                        Event_StringReceived?.Invoke(clientId, msg);
                    }
                    else
                    {
                        Event_BinaryReceived?.Invoke(clientId, data);
                    }
                }
            }
            catch { }

            _clients.TryRemove(clientId, out _);
            Event_客户端上下线?.Invoke(clientId, false);

            try { socket.Abort(); } catch { }
        }

        #region 发送数据

        /// <summary>
        /// 给指定客户端发送字符串
        /// </summary>
        public async Task SendAsync(int clientId, string message)
        {
            if (!_clients.TryGetValue(clientId, out var ws))
                return;

            if (ws.State != WebSocketState.Open)
                return;

            byte[] data = Encoding.UTF8.GetBytes(message);
            await ws.SendAsync(new ArraySegment<byte>(data),
                WebSocketMessageType.Text, true, CancellationToken.None);
        }

        /// <summary>
        /// 广播字符串
        /// </summary>
        public async Task BroadcastAsync(string message)
        {
            foreach (var id in _clients.Keys)
            {
                await SendAsync(id, message);
            }
        }

        #endregion




        /// <summary>
        /// 断开指定客户端
        /// </summary>
        /// <param name="clientId">客户端ID</param>
        /// <param name="reason">断开原因，可选</param>
        public async Task 断开指定客户端(int clientId, string reason = "Server disconnect")
        {
            if (!_clients.TryGetValue(clientId, out var ws))
                return;

            if (ws.State == WebSocketState.Open || ws.State == WebSocketState.CloseReceived)
            {
                try
                {
                    await ws.CloseAsync(WebSocketCloseStatus.NormalClosure,
                        reason, CancellationToken.None);
                }
                catch { }
            }

            _clients.TryRemove(clientId, out _);
            Event_客户端上下线?.Invoke(clientId, false);
        }

        /// <summary>
        /// 断开所有客户端
        /// </summary>
        public async Task 断开所有客户端(string reason = "Server disconnect all")
        {
            var clientIds = _clients.Keys;
            foreach (var id in clientIds)
            {
                await 断开指定客户端(id, reason);
            }
        }

    }
}
