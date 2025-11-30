using System;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace qf_WebSocket
{
    public class Client
    {


        private ClientWebSocket _ws;
        private CancellationTokenSource _cts;
        private readonly Uri _uri;

        private bool _autoReconnect;
        private int _reconnectDelay;
        private int _maxReconnectAttempts;
        private int _currentReconnectAttempts;
        private int _缓存区大小 = 1024 * 1024;
        public bool IsConnected => _ws != null && _ws.State == WebSocketState.Open;

        public qfmain._连接状态_ _连接状态 = qfmain._连接状态_.未连接;
        public event Action<qfmain._连接状态_> Event_连接状态;
        void On_连接状态(qfmain._连接状态_ state)
        {
            this._连接状态 = state;
            Event_连接状态?.Invoke(state);
        }

        public event Action<string> Event_接收数据str;
        public event Action<byte[]> Event_接收数据bytes;

        public Client(string url, int 缓存区大小 = 1024 * 1024, bool autoReconnect = true, int reconnectDelay = 3000, int maxReconnectAttempts = int.MaxValue)
        {
            _uri = new Uri(url);
            _autoReconnect = autoReconnect;
            _reconnectDelay = reconnectDelay;
            _maxReconnectAttempts = maxReconnectAttempts;
            this._缓存区大小 = 缓存区大小;
        }

        /// <summary>
        /// 连接服务端
        /// </summary>
        /// <returns></returns>
        public async Task ConnectAsync()
        {
            _cts = new CancellationTokenSource();
            _ws = new ClientWebSocket();
            _currentReconnectAttempts = 0;

            await TryConnectAsync();
        }

        private async Task TryConnectAsync()
        {
            On_连接状态( qfmain._连接状态_.连接中);
            while (_autoReconnect && (_currentReconnectAttempts < _maxReconnectAttempts))
            {
                try
                {
                    await _ws.ConnectAsync(_uri, _cts.Token);
                    //Connected?.Invoke();
                    On_连接状态(qfmain._连接状态_.已连接);
                    _currentReconnectAttempts = 0; // 重置重连计数
                    _ = ReceiveLoopAsync();
                    return;
                }
                catch
                {
                    _currentReconnectAttempts++;
                    // Disconnected?.Invoke();
                    On_连接状态(qfmain._连接状态_.未连接);
                    await Task.Delay(_reconnectDelay);
                    _ws?.Dispose();
                    _ws = new ClientWebSocket();
                }
            }
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendTextAsync(string message)
        {
            if (IsConnected)
            {
                var buffer = Encoding.UTF8.GetBytes(message);

                var segment = new ArraySegment<byte>(buffer);
                await _ws.SendAsync(segment, WebSocketMessageType.Text, true, _cts.Token);
            }
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task SendBinaryAsync(byte[] data)
        {
            if (IsConnected)
            {
                var segment = new ArraySegment<byte>(data);
                await _ws.SendAsync(segment, WebSocketMessageType.Binary, true, _cts.Token);
            }
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        /// <returns></returns>
        public async Task CloseAsync()
        {
            _autoReconnect = false;
            if (_ws != null)
            {
                try
                {
                    await _ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "Client closing", CancellationToken.None);
                }
                catch { }
                _ws.Dispose();
                _ws = null;
            }
            _cts?.Cancel();
            // Disconnected?.Invoke();
            On_连接状态(qfmain._连接状态_.未连接);
        }

        private async Task ReceiveLoopAsync()
        {
            try
            {
                while (IsConnected)
                {
                    using (var ms = new MemoryStream())
                    {
                        WebSocketReceiveResult result;
                        var buffer = new byte[this._缓存区大小]; // 分片接收用的缓冲区

                        do
                        {
                            result = await _ws.ReceiveAsync(new ArraySegment<byte>(buffer), _cts.Token);

                            if (result.MessageType == WebSocketMessageType.Close)
                            {
                                await HandleDisconnectAsync();
                                return;
                            }

                            ms.Write(buffer, 0, result.Count);
                        } while (!result.EndOfMessage);

                        ms.Seek(0, SeekOrigin.Begin);

                        if (result.MessageType == WebSocketMessageType.Text)
                        {
                            using (var reader = new StreamReader(ms, Encoding.UTF8))
                            {
                                var message = await reader.ReadToEndAsync();
                                Event_接收数据str?.Invoke(message);
                            }
                        }
                        else if (result.MessageType == WebSocketMessageType.Binary)
                        {
                            Event_接收数据bytes?.Invoke(ms.ToArray());
                        }
                    }
                }
            }
            catch
            {
                await HandleDisconnectAsync();
            }
        }


        private async Task HandleDisconnectAsync()
        {
            On_连接状态(qfmain._连接状态_.未连接);
            //  Disconnected?.Invoke();
            _ws?.Dispose();
            _ws = null;

            if (_autoReconnect && _currentReconnectAttempts < _maxReconnectAttempts)
            {
                _currentReconnectAttempts++;
                await Task.Delay(_reconnectDelay);
                _ws = new ClientWebSocket();
                await TryConnectAsync();
            }
        }




    }
}
