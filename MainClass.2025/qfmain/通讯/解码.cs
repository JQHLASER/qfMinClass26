using NPOI.POIFS.Crypt.Dsig;
using Org.BouncyCastle.Tls.Crypto;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using static qfmain.log日志;


namespace qfmain
{

    #region 解码

    public class 解码
    {

        public _解码_Cfg_ _参数 { set; get; } = new _解码_Cfg_(new byte[0], new byte[0], 200);

        public 解码(_解码_Cfg_ 参数_)
        {
            _参数 = 参数_;
            lst设置前缀_ = this._参数.前缀.ToList();
            lst设置后缀_ = this._参数.后缀.ToList();

            if (lst设置前缀_.Count == 0 && lst设置后缀_.Count == 0)
            {
                this._解码类型 = _解码Type.无前后缀;
            }
            else if (lst设置前缀_.Count > 0 && lst设置后缀_.Count == 0)
            {
                this._解码类型 = _解码Type.前缀;
            }
            else if (lst设置前缀_.Count == 0 && lst设置后缀_.Count > 0)
            {
                this._解码类型 = _解码Type.后缀;
            }
            else if (lst设置前缀_.Count > 0 && lst设置后缀_.Count > 0)
            {
                this._解码类型 = _解码Type.前后缀;
            }


        }


        private _解码Type _解码类型 = _解码Type.无前后缀;
        /// <summary>
        /// 最后一次接收的时间
        /// </summary>
        DateTime now = DateTime.Now;

        /// <summary>
        /// 缓存
        /// </summary>
        List<byte> lstHC = new List<byte>();


        Queue<byte[]> queBytes = new Queue<byte[]>();
        List<byte> lst前缀 = new List<byte>();
        List<byte> lst后缀 = new List<byte>();
        /// <summary>
        /// 设置的前缀
        /// </summary>
        List<byte> lst设置前缀_ = new List<byte>();
        /// <summary>
        /// 设置的后缀
        /// </summary>
        List<byte> lst设置后缀_ = new List<byte>();

        string[] WorkB = new string[]
        {
            "处理缓存",
            "解码",
            "处理",
        };

        private readonly object _lock = new object();
        void 解码方法(byte[] data, Socket socket_)
        {
            lock (_lock)
            {
                foreach (var s0 in WorkB)
                {
                    if (s0 == "处理缓存")
                    {
                        #region 处理缓存

                        if (this._解码类型 != qfmain._解码Type.无前后缀)
                        {
                            if (isDeylay)
                            {
                                isDeylay = false;
                                delay_sis.中断延时();
                            }
                        }

                        DateTime nows = DateTime.Now;
                        if (this._解码类型 != _解码Type.无前后缀 && !判断是否超时(nows, this._参数.超时时间))
                        {
                            this.lstHC.Clear();
                            this.lst后缀.Clear();
                            this.lst前缀.Clear();
                            queBytes.Clear();
                        }

                        now = nows;

                        #endregion
                    }
                    else if (s0 == "解码")
                    {
                        isEnd = false;

                        #region 解码

                        foreach (var s in data)
                        {
                            switch (this._解码类型)
                            {
                                case qfmain._解码Type.后缀:

                                    #region 后缀

                                    if (this.lst设置后缀_.IndexOf(s) == -1)
                                    {
                                        lstHC.Add(s);
                                    }
                                    else if (this.lst后缀.IndexOf(s) == -1)
                                    {
                                        this.lst后缀.Add(s);
                                    }

                                    if (this.lst后缀.Count > 0 && this.lst后缀.Count == this._参数.后缀.Length)
                                    {
                                        queBytes.Enqueue(lstHC.ToArray());
                                        lstHC.Clear();
                                        this.lst后缀.Clear();
                                        isEnd = true;
                                    }



                                    #endregion

                                    break;
                                case qfmain._解码Type.前后缀:

                                    #region 前后缀

                                    //前缀
                                    if (this.lst后缀.Count == 0 &&
                                        (this.lst前缀.IndexOf(s) == -1 && this.lst设置前缀_.IndexOf(s) > -1))
                                    {
                                        lstHC.Clear();
                                        this.lst前缀.Add(s);
                                    }
                                    //多次收到前缀时
                                    else if (this.lst前缀.Count > 0 && this.lst前缀.Count == this.lst设置前缀_.Count &&
                                            (this.lst设置前缀_.IndexOf(s) > -1))
                                    {
                                        lstHC.Clear();
                                        this.lst前缀.Add(s);
                                    }
                                    //后缀
                                    else if (this.lst前缀.Count > 0 &&
                                        (this.lst后缀.IndexOf(s) == -1 && this.lst设置后缀_.IndexOf(s) > -1))
                                    {
                                        this.lst后缀.Add(s);
                                    }
                                    else if ((this.lst前缀.Count > 0 && lst设置后缀_.Count == 0) &&
                                              this.lst后缀.Count == 0)
                                    {
                                        lstHC.Add(s);
                                    }


                                    if ((this.lst前缀.Count > 0 && lst设置前缀_.Count == this.lst前缀.Count) &&
                                        (this.lst后缀.Count > 0 && lst设置后缀_.Count == this.lst后缀.Count))
                                    {
                                        queBytes.Enqueue(lstHC.ToArray());
                                        lstHC.Clear();
                                        this.lst后缀.Clear();
                                        this.lst前缀.Clear();
                                        isEnd = true;
                                    }



                                    #endregion

                                    break;
                                case qfmain._解码Type.无前后缀:

                                    this.lstHC.Add(s);

                                    break;

                            }
                        }


                        #endregion

                    }
                    else if (s0 == "处理")
                    {
                        #region 处理                   

                        if (this._解码类型 == _解码Type.无前后缀 ||
                            !isEnd)
                        {
                            if (isDeylay)
                            {
                                isDeylay = false;
                                delay_sis.中断延时();
                            }
                            var t = 缓存(socket_);
                        }
                        else
                        {
                            On_(socket_);

                        }

                        #endregion
                    }
                }
            }
        }


        /// <summary>
        ///  已经解析到结果
        /// </summary>
        bool isEnd = false;
        public void On_解码(byte[] data, Socket socket_ = null)
        {
            解码方法(data, socket_);
        }

        /// <summary>
        /// 是否缓存中
        /// </summary>
        bool isDeylay = false;
        /// <summary>
        /// 缓存延时
        /// </summary>
        延时_Task delay_sis = new 延时_Task();



        async Task 缓存(Socket socket_)
        {
            isDeylay = true;
            bool rt = await delay_sis.延时(_参数.超时时间);
            isDeylay = false;
            if (rt)
            {
                lock (_lock)
                {
                    this.queBytes.Enqueue(this.lstHC.ToArray());
                    this.lstHC.Clear();
                    this.lst后缀.Clear();
                    this.lst前缀.Clear();
                    On_(socket_);
                }

            }

        }

        #region 本地方法

        void On_(Socket socket_)
        {
            List<byte> lstBeff = new List<byte>();
            bool isOn = false;
            if (queBytes.Count > 0)
            {
                isOn = true;
                byte[] beuff = queBytes.Dequeue();
                foreach (var s in beuff)
                {
                    lstBeff.Add(s);
                }

            }
            if (isOn)
            {
                On(lstBeff.ToArray());
                On(socket_, lstBeff.ToArray());

            }


        }



        #endregion



        public void 清空缓存区()
        {
            lock (_lock)
            {
                this.lstHC.Clear();
                queBytes.Clear();
            }
        }
         

        /// <summary>
        /// 超时时间:ms
        /// <para>返回:true:未超时,false:超时</para> 
        /// </summary>
        /// <param name="now"></param>
        /// <param name="超时时间"></param>
        /// <returns></returns>
        public bool 判断是否超时(DateTime nows, int 超时时间)
        {
            return new 日期时间_().是否超时(nows, now, 超时时间);
        }


        /// <summary>
        /// 普通
        /// </summary>
        public event Action<byte[]> Event_解码;

        /// <summary>
        /// 参数(Socket)参数,Tcp/IP通讯端,(byte[])数据
        /// </summary>
        public event Action<Socket, byte[]> Event_解码_Sockets;



        void On(byte[] data)
        {
            Event_解码?.Invoke(data);
        }

        void On(Socket TcpSocket, byte[] data)
        {
            Event_解码_Sockets?.Invoke(TcpSocket, data);
        }
 
    }


    #endregion

     
}
