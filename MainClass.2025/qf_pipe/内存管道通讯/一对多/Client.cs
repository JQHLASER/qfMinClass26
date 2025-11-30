using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace qf_pipe
{
    /// <summary>
    /// 客户端,多客户端连接服务器
    /// </summary>
    public class Client
    {
        /// <summary>
        /// 事件
        /// </summary>
        public event Action<string> Event_StringReceived;
        /// <summary>
        /// 事件
        /// </summary>
        public event Action<byte[]> Event_ImageReceived;

        public qfmain._连接状态_ _连接状态 = qfmain._连接状态_.未连接;
        public event Action<qfmain._连接状态_> Event_连接状态;
        private void On_连接状态(qfmain._连接状态_ newState)
        {
            _连接状态 = newState;
            Event_连接状态?.Invoke(newState);
        }




        private readonly string _pipeName;
        private NamedPipeClientStream _client;
        private CancellationTokenSource _cts;

        /// <summary>
        /// 是否自动重连
        /// </summary>
        private bool _autoReconnect = true;
        /// <summary>
        /// 是否手动断开
        /// </summary>
        private bool _manualDisconnect = false;
        private readonly object _reconnectLock = new object();
        private Task _reconnectTask;



        public Client(string pipeName)
        {
            _pipeName = pipeName;
        }
        /// <summary>
        /// 连接
        /// </summary>  
        public async Task ConnectAsync(int timeout = 3000)
        {

            if (_连接状态 != qfmain._连接状态_.未连接)
            {
                Disconnect();//如果已连接,先断开
                await Task.Delay(500);
            }

            _manualDisconnect = false;
            On_连接状态(qfmain._连接状态_.连接中);
            _cts?.Cancel();
            _cts = new CancellationTokenSource();

            _client?.Dispose();
            _client = new NamedPipeClientStream(
                ".", _pipeName,
                PipeDirection.InOut,
                PipeOptions.Asynchronous);

            await _client.ConnectAsync(timeout, _cts.Token);
            On_连接状态(qfmain._连接状态_.已连接);
            _ = Task.Run(() => ReceiveLoopAsync(_cts.Token));
        }

        private async Task ReceiveLoopAsync(CancellationToken token)
        {
            try
            {
                while (!token.IsCancellationRequested)
                {
                    var (type, data) =
                        await PipeHelper.ReceiveMessageAsync(_client, token);

                    if (type == PipeHelper.TYPE_STRING)
                        Event_StringReceived?.Invoke(
                            Encoding.UTF8.GetString(data));
                    else if (type == PipeHelper.TYPE_IMAGE)
                        Event_ImageReceived?.Invoke(data);
                }
            }
            catch (OperationCanceledException)
            {
                // 正常取消
                On_连接状态(qfmain._连接状态_.未连接);
            }
            catch (Exception)
            {
                On_连接状态(qfmain._连接状态_.未连接);
                // 任意异常 = 断线
                StartReconnectLoop();
            }
        }

        private void StartReconnectLoop()
        {
            if (_manualDisconnect || !_autoReconnect)
                return;

            lock (_reconnectLock)
            {
                if (_reconnectTask != null && !_reconnectTask.IsCompleted)
                    return;

                _reconnectTask = Task.Run(async () =>
                {
                    int delay = 1000;

                    while (!_manualDisconnect)
                    {
                        try
                        {
                            On_连接状态(qfmain._连接状态_.连接中);
                            await Task.Delay(delay);
                            await ConnectAsync();

                            // 连接成功 → 退出重连循环
                            return;
                        }
                        catch
                        {
                            // 失败 → 延长等待时间
                            delay = Math.Min(delay * 2, 10000);
                        }
                    }
                });
            }
        }



        /// <summary>
        /// 发送信息
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public async Task SendStringAsync(string text)
        {
            await PipeHelper.SendStringAsync(_client, text);
        }
        /// <summary>
        /// 发送信息
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public async Task SendImageAsync(byte[] bytes)
        {
            await PipeHelper.SendImageAsync(_client, bytes);
        }

        /// <summary>
        /// 释放或断开连接
        /// </summary>
        public void Disconnect()
        {
            if (_连接状态 == qfmain._连接状态_.未连接)
            {
                return;
            }
            //if (是否产生事件)
            //{
            //    On_连接状态(qfmain._连接状态_.未连接);
            //}
            _manualDisconnect = true;//手动断开   
            _cts?.Cancel();
            _client?.Dispose();
        }

    }
}
