using MySqlX.XDevAPI;
using NPOI.OpenXmlFormats.Spreadsheet;
using NPOI.POIFS.Crypt.Dsig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace qfmain
{
    /// <summary>
    /// net自带库
    /// </summary>
    public class Socket_Client : Language_
    {

        /// <summary>
        /// path : 存储文件路径
        /// </summary>
        /// <param name="path_"></param>
        public Socket_Client(string path_, _解码_Cfg_ cfg)
        {
            if (!string.IsNullOrEmpty(path_))
            {
                path = path_;
            }

            if (cfg != null)
            {
                jm_sys = new 解码(cfg);
                jm_sys.Event_解码 += On_接收数据_jm;
            }
        }




        /// <summary>
        /// path : 存储文件路径
        /// </summary>
        /// <param name="path_"></param>
        public Socket_Client(string path_)
        {
            if (!string.IsNullOrEmpty(path_))
            {
                path = path_;
            }
        }

        public Socket_Client()
        {

        }


        private 解码 jm_sys;
        Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        public _连接状态_ _连接状态 { set; get; } = _连接状态_.未连接;

        public _通讯中状态_ _通讯状态 { set; get; } = _通讯中状态_.闲置;


        public _Socket_Cfg_ _参数 { set; get; } = new _Socket_Cfg_();

        /// <summary>
        /// 存储文件路径
        /// </summary>
        string path { set; get; } = 软件类.Files_Cfg.Files_Config + $"\\TcpClient.txt";





        #region 事件


        /// <summary>
        ///  连接状态
        /// </summary>
        public event Action<_连接状态_> Event_连接状态;

        /// <summary>
        /// 接收数据
        /// </summary>
        public event Action<byte[]> Event_接收数据;

        /// <summary>
        /// 接收数据
        /// </summary>
        public event Action<byte[]> Event_接收数据_jm;

        void On_接收数据(byte[] data)
        {
            this.Event_接收数据?.Invoke(data);
            if (this.jm_sys != null)
            {
                this.jm_sys.On_解码(data);
            }
        }

        void On_接收数据_jm(byte[] data)
        {
            this.Event_接收数据_jm?.Invoke(data);
        }


        void On_连接状态(_连接状态_ state)
        {
            this._连接状态 = state;
            this.Event_连接状态?.Invoke(state);
        }


        #endregion





        /// <summary>
        /// 接收数据
        /// </summary>
        private byte[] recvDataBuffer = new byte[1024];


        //public Socket_server(string server, int port)
        //{
        //    this.server = server;
        //    this.port = port;
        //}












        public void 参数读写(ushort model)
        {
            _Socket_Cfg_ info = this._参数;
            new 文件_文件夹().WriteReadJson(path, model, ref info, out string msgErr);
            this._参数 = info;

        }


        /// <summary>
        /// 连接服务端
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <param name="msgErr"></param>
        /// <returns></returns>
        public bool Connect连接(out string msgErr, bool 是否先读取参数 = true, bool 是否先断开 = true)
        {
            if (是否先读取参数)
            {
                参数读写(1);
            }
            return Connect连接(this._参数, out msgErr, 是否先断开);
        }

        /// <summary>
        /// 连接服务端
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <param name="msgErr"></param>
        /// <returns></returns>
        public async Task<(bool rt, string msgErr)> Connect连接Async(bool 是否先读取参数 = true, bool 是否先断开 = true)
        {
            if (是否先读取参数)
            {
                参数读写(1);
            }
            return await Connect连接Async(this._参数, 是否先断开);
        }


        /// <summary>
        /// 连接服务端
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <param name="msgErr"></param>
        /// <returns></returns>
        public bool Connect连接(_Socket_Cfg_ info, out string msgErr, bool 是否先断开 = true)
        {
            return Connect连接(info.IP, info.Port, out msgErr, 是否先断开);
        }

        /// <summary>
        /// 连接服务端
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <param name="msgErr"></param>
        /// <returns></returns>
        public async Task<(bool rt, string MsgErr)> Connect连接Async(_Socket_Cfg_ info, bool 是否先断开 = true)
        {
            return await Connect连接Async(info.IP, info.Port, 是否先断开);
        }


        bool 是否第一次 = true;

        /// <summary>
        /// 连接服务端
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <param name="msgErr"></param>
        /// <returns></returns>
        public bool Connect连接(string ip, int port, out string msgErr, bool 是否先断开 = true)
        {
            bool rt = true;
            msgErr = string.Empty;

            if (!IPAddress.TryParse(ip, out IPAddress ip_))
            {
                msgErr = Language_.Get语言("IP错误");
                return false;
            }

            this._参数.IP = ip;
            this._参数.Port = port;
            连接中 = true;


            try
            {
                if (是否先断开)
                {
                    Stop关闭连接(out msgErr, false);

                    if (this._参数.重连周期 > 0)
                    {
                        Thread.Sleep(this._参数.重连周期);
                    }

                }

                On_连接状态(_连接状态_.连接中);

                if (是否第一次)
                {
                    是否第一次 = false;
                    Thread th_接收数据 = th_接收数据 = new Thread(线程_接收数据);
                    th_接收数据.IsBackground = true;
                    th_接收数据.Start();
                }

                client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                recvDataBuffer = new byte[this._参数.接收区大小];
                client.SendBufferSize = this._参数.发送区大小;
                IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(ip), port);
                client.Connect(ipep);
                // client.ConnectAsync (ipep);
                rt = client.Connected;

            }
            catch (Exception ex)
            {
                msgErr = ex.Message;
                rt = false;

            }
            _连接状态_ status = rt ? _连接状态_.已连接 : _连接状态_.未连接;
            On_连接状态(status);

            连接中 = false;
            return rt;
        }

        /// <summary>
        /// 连接服务端
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <param name="msgErr"></param>
        /// <returns></returns>
        public async Task<(bool rt, string msgErr)> Connect连接Async(string ip, int port, bool 是否先断开 = true)
        {
            bool rt = true;
            string msgErr = string.Empty;
            if (!IPAddress.TryParse(ip, out IPAddress ip_))
            {
                msgErr = Language_.Get语言("IP错误");
                return (rt, msgErr);
            }

            this._参数.IP = ip;
            this._参数.Port = port;
            连接中 = true;


            try
            {
                if (是否先断开)
                {
                    Stop关闭连接(out msgErr, false);
                    if (this._参数.重连周期 > 0)
                    {
                        Thread.Sleep(this._参数.重连周期);
                    }

                }

                On_连接状态(_连接状态_.连接中);

                if (是否第一次)
                {
                    是否第一次 = false;
                    Thread th_接收数据 = th_接收数据 = new Thread(线程_接收数据);
                    th_接收数据.IsBackground = true;
                    th_接收数据.Start();
                }

                client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                recvDataBuffer = new byte[this._参数.接收区大小];
                client.SendBufferSize = this._参数.发送区大小;
                IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(ip), port);

                await client.ConnectAsync(ipep);
                rt = client.Connected;
            }
            catch (Exception ex)
            {
                msgErr = ex.Message;
                rt = false;

            }

            _连接状态_ status = rt ? _连接状态_.已连接 : _连接状态_.未连接;
            On_连接状态(status);

            连接中 = false;
            return (rt, msgErr);
        }


        bool 连接中 = true;



        /// <summary>
        /// 关闭连接
        /// </summary>
        /// <param name="msgErr"></param>
        /// <returns></returns>
        public bool Stop关闭连接(out string msgErr, bool 释放 = true)
        {
            bool rt = true;
            msgErr = string.Empty;


            if (释放)
            {
                is接收线程 = false;
                // th_接收数据.Abort();//此方法不太安全

                if (this.jm_sys != null)
                {
                    this.jm_sys.Event_解码 -= On_接收数据_jm;
                }
                
            }


            On_连接状态(_连接状态_.未连接);
            try
            {
                client.Shutdown(SocketShutdown.Both);
                client.Close();
                client.Dispose();
            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }



            return rt;
        }

        /// <summary>
        /// true:停止,
        /// </summary>
        bool is接收线程 = true;



        public void 线程_接收数据()
        {
            bool 是否需要延时 = false;

            while (is接收线程)
            {
                if (this._参数.线程周期 < 0)
                {
                    this._参数.线程周期 = 100;
                }

                if (是否需要延时)
                {
                    Thread.Sleep(this._参数.线程周期);
                    是否需要延时 = false;
                }

                if (!is接收线程)
                {
                    break;
                }

                if (client == null)
                {
                    continue;
                }

                try
                {
                    if (this._连接状态 == _连接状态_.未连接)
                    {
                        if (!this.连接中)
                        {
                            Connect连接(out string msgErr, false);
                        }
                        else
                        {
                            是否需要延时 = true;
                        }
                        continue;
                    }
                    else if (this._连接状态 == _连接状态_.连接中)
                    {
                        是否需要延时 = true;
                        continue;
                    }
                    else if (this._连接状态 == _连接状态_.已连接)
                    {
                        if (!判断是否连接_1(1000))
                        {
                            On_连接状态(_连接状态_.未连接);
                            是否需要延时 = true;
                            continue;
                        }
                    }
                    recvDataBuffer = new byte[this._参数.接收区大小];
                    int bytesRec = client.Receive(recvDataBuffer);
                    if (bytesRec > 0)
                    {
                        byte[] data = Array.FindAll(recvDataBuffer, i => i != 0X0).ToArray();
                        On_接收数据(data);

                        if (this._参数.线程周期_接收数据 > 0)
                        {
                            Thread.Sleep(this._参数.线程周期_接收数据);
                        }

                    }
                    else
                    {
                        On_连接状态(_连接状态_.未连接);
                        是否需要延时 = true;
                    }

                }
                catch (Exception)
                {
                    On_连接状态(_连接状态_.未连接);
                    是否需要延时 = true;
                }
            }



        }

      

        public bool Err_未连接()
        {
            if (this._连接状态 != 0)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Poll()...可能这个好用点
        /// </summary>
        /// <param name="socket_"></param>
        /// <returns></returns>
        public bool 判断是否连接_1(int 检测超时 = 1000)
        { 
            if (client == null)
                return false;

            try
            {
                if (!client.Connected)
                    return false;

                // 优雅断开检测
                if (client.Poll(检测超时, SelectMode.SelectRead) && client.Available == 0)
                    return false;

                // 强制触发底层 TCP 状态检测
                client.Send(new byte[0]);

                return true;
            }
            catch
            {
                return false;
            }
       


        }



        /// <summary>
        /// 断开到重连成功,间隔至少50ms
        /// </summary>
        public void 重连_不停止线程()
        {
            Stop关闭连接(out string msgErr, false);
        }



        #region send发送



        /// <summary>
        /// 返回发送到的字节数
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int Send发送(byte[] data)
        {
            return client.Send(data);
        }

        public bool Send发送Async(byte[] data)
        {
            SocketAsyncEventArgs a = new SocketAsyncEventArgs();
            a.SetBuffer(data, 0, data.Length);
            return client.SendAsync(a);
        }

        /// <summary>
        /// 返回发送到的字节数
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int Send发送(string data)
        {
            return client.Send(new Encoding编码().stringToBytes(data));
        }

        public bool Send发送Async(string data)
        {
            return Send发送Async(new Encoding编码().stringToBytes(data));
        }

        public int Send发送(string data, Encoding encoding)
        {
            return client.Send(encoding.GetBytes(data));
        }
        public bool Send发送Async(string data, Encoding encoding)
        {
            return Send发送Async(encoding.GetBytes(data));
        }


        public void SendFile发送(string fileName)
        {
            client.SendFile(fileName);
        }




        #endregion





        #region Err

        public bool Err_未连接(string Nmae, out string msgErr)
        {
            msgErr = string.Empty;
            if (this._连接状态 != _连接状态_.已连接)
            {
                msgErr = $"{Nmae}" + Get语言("未连接");
                return false;
            }
            return true;
        }

        public bool Err_通讯中(string Nmae, out string msgErr)
        {
            msgErr = string.Empty;
            if (this._通讯状态 != _通讯中状态_.闲置)
            {
                msgErr = $"{Nmae}" + Get语言("通讯中");
                return false;
            }
            return true;
        }



        #endregion


    }
}
