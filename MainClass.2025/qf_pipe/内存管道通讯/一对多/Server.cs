using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace qf_pipe
{
    /// <summary>
    /// 服务端,一对多客户端
    /// </summary>
    public class Server
    {

        /// <summary>
        /// 事件,消息+客户端ID
        /// </summary>
        public event Action<string, int> Event_StringReceived;  // 消息内容 + 客户端ID
        /// <summary>
        /// 事件,消息+客户端ID
        /// </summary>
        public event Action<byte[], int> Event_ImageReceived;
        /// <summary>
        /// 客户端上线
        /// </summary>
        public event Action<int> Event_客户端上线;

        /// <summary>
        /// 客户端下线
        /// </summary>
        public event Action<int> Event_客户端下线;




        private readonly string _pipeName;
        private readonly ConcurrentDictionary<int, NamedPipeServerStream> _clients = new ConcurrentDictionary<int, NamedPipeServerStream>();
        private int _clientIdCounter = 0;
        private CancellationTokenSource _cts;

        public Server(string pipeName)
        {
            _pipeName = pipeName;
        }

        /// <summary>
        /// 启动服务
        /// </summary>
        public void Start()
        {
            _cts = new CancellationTokenSource();
            _ = Task.Run(() => AcceptLoopAsync(_cts.Token));
        }

        private async Task AcceptLoopAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                int clientId = Interlocked.Increment(ref _clientIdCounter);
                var serverStream = new NamedPipeServerStream(
                    _pipeName,
                    PipeDirection.InOut,
                    NamedPipeServerStream.MaxAllowedServerInstances,
                    PipeTransmissionMode.Byte,
                    PipeOptions.Asynchronous);

                await serverStream.WaitForConnectionAsync(token);
                Event_客户端上线?.Invoke(clientId);//客户端上线
                _clients[clientId] = serverStream;
                _ = Task.Run(() => ClientReceiveLoopAsync(clientId, serverStream, token));
            }
        }

        private async Task ClientReceiveLoopAsync(int clientId, NamedPipeServerStream stream, CancellationToken token)
        {
            try
            {
                while (!token.IsCancellationRequested)
                {
                    var (type, data) = await PipeHelper.ReceiveMessageAsync(stream, token);

                    if (type == PipeHelper.TYPE_STRING)
                        Event_StringReceived?.Invoke(System.Text.Encoding.UTF8.GetString(data), clientId);
                    else if (type == PipeHelper.TYPE_IMAGE)
                        Event_ImageReceived?.Invoke(data, clientId);
                }
            }
            catch (EndOfStreamException)
            {
                Event_客户端下线?.Invoke(clientId);
                // 客户端断开
                _clients.TryRemove(clientId, out _);
                stream.Dispose();
            }
        }

        /// <summary>
        /// 发送信息,当ClientId为空时,群发
        /// </summary>  
        public async Task SendStringAsync(string text, int? clientId = null)
        {
            byte[] data = System.Text.Encoding.UTF8.GetBytes(text);

            if (clientId.HasValue)
            {
                if (_clients.TryGetValue(clientId.Value, out var stream))
                    await PipeHelper.SendMessageAsync(stream, PipeHelper.TYPE_STRING, data);
            }
            else
            {
                foreach (var kv in _clients)
                    await PipeHelper.SendMessageAsync(kv.Value, PipeHelper.TYPE_STRING, data);
            }
        }

        /// <summary>
        /// 发送信息,当ClientId为空时,群发
        /// </summary>  
        public async Task SendImageAsync(byte[] bytes, int? clientId = null)
        {
            if (clientId.HasValue)
            {
                if (_clients.TryGetValue(clientId.Value, out var stream))
                    await PipeHelper.SendMessageAsync(stream, PipeHelper.TYPE_IMAGE, bytes);
            }
            else
            {
                foreach (var kv in _clients)
                    await PipeHelper.SendMessageAsync(kv.Value, PipeHelper.TYPE_IMAGE, bytes);
            }
        }

        /// <summary>
        /// 关闭服务
        /// </summary>
        public void Stop(bool 踢掉所有客户 = true)
        {
            if (踢掉所有客户)
            {
                KickAllClients();
            }
            _cts?.Cancel();

            //foreach (var kv in _clients)
            //{
            //    kv.Value.Dispose();
            //}
            _clients.Clear();
        }


        /// <summary>
        /// 踢掉指定客户端
        /// </summary>
        public void KickClient(int clientId)
        {
            if (_clients.TryRemove(clientId, out var stream))
            {
                try
                {
                    if (stream.IsConnected)
                    {
                        // 可选：先断开（.NET Framework 可用）
                        stream.Disconnect();
                    }
                }
                catch { }

                stream.Dispose();

                // 主动触发下线事件
                // Event_客户端下线?.Invoke(clientId);
            }
        }

        /// <summary>
        /// 踢掉全部客户端
        /// </summary>
        public void KickAllClients()
        {
            foreach (var clientId in _clients.Keys)
            {
                KickClient(clientId);
            }
        }


    }
}
