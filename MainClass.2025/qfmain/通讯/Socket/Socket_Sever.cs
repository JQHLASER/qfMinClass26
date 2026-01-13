using MySqlX.XDevAPI;
using NPOI.Util;
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
    public class Socket_Server : Language_
    {


        /// <summary>
        /// 
        /// </summary>
        /// <param name="path_"> path : 存储文件路径</param>
        public Socket_Server(string path_, _解码_Cfg_ cfg = null)
        {
            if (!string.IsNullOrEmpty(path_))
            {
                path = path_;
            }

            if (cfg != null)
            {
                jm_sys = new 解码(cfg);
                jm_sys.Event_解码_Sockets += On_接收数据_jm;
            }
        }



        public Socket_Server()
        {

        }


        private 解码 jm_sys;
        public _启动状态_ _侦听启动状态 { set; get; } = _启动状态_.未启动;

        /// <summary>
        /// 当前在线的客户端
        /// </summary>
        public List<Socket> _lstSocket = new List<Socket>();
        public _Socket_Cfg_ _参数 { set; get; } = new _Socket_Cfg_();

        /// <summary>
        /// 存储文件路径
        /// </summary>
        string path { set; get; } = 软件类.Files_Cfg.Files_Config + $"\\TcpServer.txt";

        Socket socketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);



        #region 事件


        /// <summary>
        /// 客户端上线
        /// </summary>
        public event Action<Socket> Event_客户端上线;
        /// <summary>
        /// Action_客户端下线
        /// </summary>
        public event Action<Socket> Event_客户端下线;
        /// <summary>
        /// 侦听启动状态
        /// </summary>
        public event Action<_启动状态_> Event_侦听启动状态;
        /// <summary>
        /// 接收数据
        /// </summary>
        public event Action<Socket, byte[]> Event_接收数据;



        void On_客户端上线(Socket socket_)
        {
            this._lstSocket.Add(socket_);
            Event_客户端上线?.Invoke(socket_);
        }

        void On_客户端下线(Socket socket_)
        {
            this._lstSocket.Remove(socket_);
            if (this._侦听启动状态 == _启动状态_.已启动)
            {
                Event_客户端下线?.Invoke(socket_);
            }
        }


        void On_接收数据(Socket socket_, byte[] data)
        {
            Event_接收数据?.Invoke(socket_, data);
            if (this.jm_sys != null)
            {
                this.jm_sys.On_解码(data, socket_);
            }
        }


        void On_侦听启动状态(_启动状态_ status)
        {
            this._侦听启动状态 = status;
            Event_侦听启动状态?.Invoke(this._侦听启动状态);

        }


        /// <summary>
        /// 接收数据
        /// </summary>
        public event Action<Socket, byte[]> Event_接收数据_jm;
        void On_接收数据_jm(Socket socket_, byte[] data)
        {
            this.Event_接收数据_jm?.Invoke(socket_, data);
        }


        #endregion

        #region 方法

        //public Socket_server(string server, int port)
        //{
        //    this.server = server;
        //    this.port = port;
        //}


        public void 参数读写(ushort model)
        {
            _Socket_Cfg_ info = this._参数;
            if (!new 文件_文件夹().文件_是否存在(path))
            {
                info.IP = string.Empty;
            }
            new 文件_文件夹().WriteReadJson(path, model, ref info, out string msgErr);
            this._参数 = info;
        }


        /// <summary>
        /// 开启监听,,,ip为空时监听所有的网卡
        /// </summary>
        public bool StartListen(out string msgErr, bool 是否先读参数 = true)
        {
            if (是否先读参数)
            {
                参数读写(1);
            }
            return StartListen(this._参数, out msgErr);
        }

        /// <summary>
        /// 开启监听,,,ip为空时监听所有的网卡
        /// </summary>
        public bool StartListen(int Port, out string msgErr)
        {
            return StartListen("", Port, out msgErr);
        }


        /// <summary>
        /// 开启监听,,,ip为空时监听所有的网卡
        /// </summary>
        public bool StartListen(_Socket_Cfg_ info, out string msgErr)
        {
            return StartListen(info.IP, info.Port, out msgErr);
        }

        /// <summary>
        /// 开启监听,,,ip为空时监听所有的网卡
        /// </summary>
        public bool StartListen(string ip, int port, out string msgErr)
        {
            bool rt = true;
            msgErr = string.Empty;

            if (!string.IsNullOrEmpty(ip) && !IPAddress.TryParse(ip, out IPAddress ip_))
            {
                msgErr = Language_.Get语言("IP错误");
                return false;
            }
            On_侦听启动状态(_启动状态_.启动中);

            try
            {
                StopListen(out msgErr, false);
                this._lstSocket.Clear();
                Thread.Sleep(100);



                socketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                socketServer.SendBufferSize = this._参数.发送区大小;
                //recvDataBuffer = new byte[this._参数.接收区大小];

                IPEndPoint ipep = null;
                if (!string.IsNullOrEmpty(ip))
                {
                    ipep = new IPEndPoint(IPAddress.Parse(ip), port);
                }
                else
                {
                    ipep = new IPEndPoint(IPAddress.Any, port);
                }
                socketServer.Bind(ipep);
                socketServer.Listen(0);
                socketServer.BeginAccept(AcceptCallBack, socketServer);


            }
            catch (Exception ex)
            {
                msgErr = ex.Message;
                rt = false;

            }
            _启动状态_ status = rt ? _启动状态_.已启动 : _启动状态_.未启动;
            On_侦听启动状态(status);
            return rt;
        }

        /// <summary>
        /// 停止侦听
        /// </summary>
        /// <param name="msgErr"></param>
        /// <returns></returns>
        public bool StopListen(out string msgErr, bool IS产生事件 = true)
        {
            bool rt = true;
            msgErr = string.Empty;
            if (IS产生事件)
            {
                On_侦听启动状态(_启动状态_.未启动);
            }
            try
            {
                断开所有客户端();
                Thread.Sleep(100);
                this._lstSocket.Clear();
                socketServer.Close();
                socketServer.Dispose();

            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }

            return rt;
        }

        public bool 判断是否连接(Socket socket_, int 检测超时 = 1000)
        {
            if (socket_ == null || !socket_.Connected)
                return false;

            try
            {
                if (socket_.Poll(检测超时, SelectMode.SelectRead) && socket_.Available == 0)
                    return false;

                // ★ 不用 Send 空包(风险大)，用 IOControl 检测
                return !(socket_.Poll(0, SelectMode.SelectError));
            }
            catch
            {
                return false;
            }
        }


        public void 断开所有客户端()
        {
            foreach (var s in this._lstSocket)
            {
                断开指定客户端(s);
            }
        }

        public void 断开指定客户端(Socket socket_)
        {
            //socket_.Shutdown(SocketShutdown.Both);

            if (socket_ == null) return;

            try { socket_.Shutdown(SocketShutdown.Both); } catch { }
            try { socket_.Close(); } catch { }
            try { socket_.Dispose(); } catch { }
        }


        public (bool s, string m) 取sokcket_ID_ip_Port(Socket socket_, out string id, out string ip, out int port)
        {
            id = "";
            ip = "";
            port = 0;
            try
            {
                id = socket_.RemoteEndPoint.ToString(); ;
                ip = ((IPEndPoint)socket_.RemoteEndPoint).Address.ToString();
                port = ((IPEndPoint)socket_.RemoteEndPoint).Port;
                return (true, "");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }


        #region 源码


        /// <summary>
        /// 连接
        /// </summary>
        /// <param name="iar"></param>
        private void AcceptCallBack(IAsyncResult iar)
        {
            Socket socket = iar.AsyncState as Socket; 
            try
            {
                Socket client = socket.EndAccept(iar);
                string ip = ((IPEndPoint)client.RemoteEndPoint).Address.ToString();
                int port = ((IPEndPoint)client.RemoteEndPoint).Port;


                //if (AcceptAction != null)
                //{
                //    AcceptAction("客户端：" + ip + "  端口：" + port + "连接成功");
                //}          


                #region 客户端进入


                //info_IPaddress_ cl = new info_IPaddress_();
                //cl.IP = ip;
                //cl.Port = port;

                //string id = $"{cl.IP}:{cl.Port}";


                //this._lstSocket.Add(client);
                On_客户端上线(client);

                #endregion

                // ★ 每个 client 必须有独立 buffer
                var state = new ClientState()
                {
                    Socket = client,
                    Buffer = new byte[this._参数.接收区大小]
                };

                //接收数据
                client.BeginReceive(state.Buffer, 0, state.Buffer.Length, SocketFlags.None, ReceiveCallBack,
                    state);



            }
            catch (Exception)
            {

            }



            // ★ 永远继续等待下一个客户端，不要放在 try 里
            try
            {//等待下一次连接
                socket.BeginAccept(AcceptCallBack, socket);
            }
            catch { }
        }

       
        private class ClientState
        {
            public Socket Socket;
            public byte[] Buffer;
        }

        /// <summary>
        /// 接收数据
        /// </summary>
        private byte[] recvDataBuffer_ = new byte[1024];


        /// <summary>
        /// 接收数据
        /// </summary>
        /// <param name="iar"></param>
        private void ReceiveCallBack(IAsyncResult iar)
        {
            var state = (ClientState)iar.AsyncState;
            // Socket socket = iar.AsyncState as Socket;
            Socket socket = state.Socket;
            try
            {
                string ip = ((IPEndPoint)socket.RemoteEndPoint).Address.ToString();
                int port = ((IPEndPoint)socket.RemoteEndPoint).Port;


                int recvSize = socket.EndReceive(iar);
                if (recvSize > 0)
                {
                    byte[] b = new byte[recvSize];
                    Array.Copy(state.Buffer, 0, b, 0, recvSize);

                    //if (ReveiveDataAction != null)
                    //{
                    //    ReveiveDataAction(b);//处理收到的数据
                    //}
                    //接收数据
                    socket.BeginReceive(state.Buffer, 0, state.Buffer.Length, SocketFlags.None, ReceiveCallBack,
                        state);
                  
                    //Send发送("", Encoding.Default.GetBytes(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " ,ok"));
                    //Console.Write(Encoding.Default.GetString(b));

                    byte[] data = Array.FindAll(b, i => i != 0X0).ToArray();
                    On_接收数据(socket, data);
                }
                else
                {

                    //Console.Write("客户退出\r\n");
                    ////断开后移除
                    //this._lstClient.RemoveAll(temp => temp.IP.Contains(ip) && temp.Port == port);
                    //if (DisconnectAction != null)
                    //    DisconnectAction("客户端：" + ip + "  端口：" + port + "断开连接");



                    #region 客户退出

                    //int index =this._lstSocket.IndexOf(socket);
                    //if (index > -1)
                    //{
                    //  this._lstSocket.RemoveAt(index);                      
                    //    On_客户端下线(socket);
                    //}


                    // this._lstSocket.Remove(socket);
                    On_客户端下线(socket);

                    #endregion


                }


            }
            catch (ObjectDisposedException)
            {
                // 已关闭
            }
            catch (SocketException)
            {
                // 断开指定客户端(client);
                On_客户端下线(socket);
            }
            catch (Exception)
            {
                //  On_侦听启动状态(-1);
                On_客户端下线(socket);
            }
        }
      


        #endregion


        #region send发送



        /// <summary>
        /// 返回发送到的字节数
        /// </summary>
        /// <param name="socket_"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public int Send发送(Socket socket_, byte[] data)
        {

            return socket_.Send(data);
        }

        public bool Send发送Async(Socket socket_, byte[] data)
        {
            SocketAsyncEventArgs a = new SocketAsyncEventArgs();
            a.SetBuffer(data, 0, data.Length);
            return socket_.SendAsync(a);
        }



        /// <summary>
        /// 返回发送到的字节数
        /// </summary>
        /// <param name="socket_"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public int Send发送(Socket socket_, string data)
        {
            return socket_.Send(new Encoding编码().stringToBytes(data));
        }

        public bool Send发送Async(Socket socket_, string data)
        {
            return Send发送Async(socket_, new Encoding编码().stringToBytes(data));
        }

        public bool Send发送Async(Socket socket_, string data, Encoding encoding)
        {
            return Send发送Async(socket_, encoding.GetBytes(data));
        }


        public void SendFile发送(Socket socket_, string fileName)
        {
            socket_.SendFile(fileName);
        }




        #endregion


        #endregion






        #region Err

        public bool Err_未启动(string Name, out string msgErr)
        {
            msgErr = string.Empty;
            if (this._侦听启动状态 != _启动状态_.未启动)
            {
                msgErr = Name + Get语言("未启动");
                return false;
            }
            return true;
        }


        public bool Err_通讯中(string Name, _通讯中状态_ 通讯状态_, out string msgErr)
        {
            msgErr = string.Empty;
            if (通讯状态_ != _通讯中状态_.通讯中)
            {
                msgErr = Name + Get语言("通讯中");
                return false;
            }
            return true;
        }

        #endregion



    }
}
