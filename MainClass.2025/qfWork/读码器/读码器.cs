
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static qfWork.读码器;

namespace qfWork
{
    public class 读码器
    {

        #region 结构


        public class _cfg_主参数_
        {
            public bool 使能_读码器 { set; get; } = true;
            public bool 使能_评级 { set; get; } = false;
            public bool 使能_检测 { set; get; } = false;


            public _通讯方式_ 通讯方式 { set; get; } = _通讯方式_.TcpClient;
            public _cfg_读码器参数_ 读码器 { set; get; } = new _cfg_读码器参数_();
            public _cfg_检测_ 检测 { set; get; } = new _cfg_检测_();

            public _cfg_评级_ 评级 { set; get; } = new _cfg_评级_();

            public _cfg_等级作假_[] 自定义等级 { set; get; } = new _cfg_等级作假_[]
            {
                new _cfg_等级作假_("A","A"),
                new _cfg_等级作假_("B","B"),
                new _cfg_等级作假_("C","C"),
                new _cfg_等级作假_("D","D"),
                new _cfg_等级作假_("E","E"),
                new _cfg_等级作假_("F","F"),
            };

            public _cfg_主参数_ Clone()
            {
                return new _cfg_主参数_
                {
                    使能_读码器 = this.使能_读码器,
                    使能_评级 = this.使能_评级,
                    使能_检测 = this.使能_检测,
                    通讯方式 = this.通讯方式,
                    读码器 = this.读码器,
                    检测 = this.检测,
                    评级 = this.评级,
                    自定义等级 = this.自定义等级,
                };
            }

        }



        public enum _通讯方式_
        {
            /// <summary>
            /// 网口
            /// </summary>
            TcpClient,
            /// <summary>
            /// 串口
            /// </summary>
            SerialPort,
        }


        public class _cfg_读码器参数_
        {

            public string 指令_启动 { set; get; } = "LON";
            public string 指令_停止 { set; get; } = "LOFF";

            /// <summary>
            /// 未读出内容
            /// </summary>
            public string 错误标识 { set; get; } = "ERROR";

            /// <summary>
            ///通讯解码时超时
            /// </summary>
            public int 通讯超时 { set; get; } = 1000;

            public int 读码超时 { set; get; } = 5000;
            public int 读码前延时 { set; get; } = 1000;


            /// <summary>
            /// 单次最大的读码次数
            /// </summary>
            public int 读码次数 { set; get; } = 1;
            public int 多次读码间隔 { set; get; } = 100;

            public bool 使能_停止指令 { set; get; } = true;
            public _cfg_数据前后缀_发送_ 前后缀_发送 { set; get; } = new _cfg_数据前后缀_发送_();
            public _cfg_数据前后缀_接收_ 前后缀_接收 { set; get; } = new _cfg_数据前后缀_接收_();

            public _cfg_读码器参数_ Clone()
            {
                return new _cfg_读码器参数_
                {
                    指令_启动 = this.指令_启动,
                    指令_停止 = this.指令_停止,
                    错误标识 = this.错误标识,
                    通讯超时 = this.通讯超时,
                    读码超时 = this.读码超时,
                    读码前延时 = this.读码前延时,
                    读码次数 = this.读码次数,
                    多次读码间隔 = this.多次读码间隔,
                    使能_停止指令 = this.使能_停止指令,
                    前后缀_发送 = this.前后缀_发送,
                    前后缀_接收 = this.前后缀_接收,

                };
            }


        }


        public class _cfg_检测_
        {
            public int 读码超时 { set; get; } = 5000;
            public int 读码前延时 { set; get; } = 1000;
            /// <summary>
            /// 单次最大的读码次数
            /// </summary>
            public int 读码次数 { set; get; } = 1;
            public int 多次读码间隔 { set; get; } = 100;

            public _cfg_检测_ Clone()
            {
                return new _cfg_检测_
                {
                    读码超时 = this.读码超时,
                    读码前延时 = this.读码前延时,
                    读码次数 = this.读码次数,
                    多次读码间隔 = this.多次读码间隔,
                };
            }

        }

        public class _cfg_评级_
        {
            public string 分割符 { set; get; } = ":";
            public string[] 合格等级 { set; get; } = new string[]
            {
                "A",
                "B",
            };

            public _cfg_评级_ Clone()
            {
                return new _cfg_评级_
                {
                    分割符 = this.分割符,
                    合格等级 = this.合格等级,
                };
            }
        }

        public class _cfg_数据前后缀_接收_
        {
            public string[] 前缀 { set; get; } = new string[] { };
            public string[] 后缀 { set; get; } = new string[]
            {
                "{0D}",
                "{0A}"
            };
            public _cfg_数据前后缀_接收_ Clone()
            {
                return new _cfg_数据前后缀_接收_
                {
                    前缀 = this.前缀,
                    后缀 = this.后缀,
                };
            }
        }

        public class _cfg_数据前后缀_发送_
        {
            public string[] 前缀 { set; get; } = new string[] { };
            public string[] 后缀 { set; get; } = new string[]
            {
                "{0D}",
                "{0A}"
            };

            public _cfg_数据前后缀_发送_ Clone()
            {
                return new _cfg_数据前后缀_发送_
                {
                    前缀 = this.前缀,
                    后缀 = this.后缀,
                };

            }
        }


        /// <summary>
        /// 等级作假参数
        /// </summary>
        public class _cfg_等级作假_
        {
            /// <summary>
            /// 原始等级
            /// </summary>
            public string Name { set; get; }
            /// <summary>
            /// 自定义等级
            /// </summary>
            public string Value { set; get; }

            public _cfg_等级作假_(string Name_, string Value_)
            {
                this.Name = Name_;
                this.Value = Value_;
            }

            public _cfg_等级作假_()
            {

            }
            public _cfg_等级作假_ Clone()
            {
                return new _cfg_等级作假_
                {
                    Name = this.Name,
                    Value = this.Value
                };
            }
        }

        public class _cfg_读码内容_
        {
            public string 原始内容 { set; get; } = "";
            public string 内容 { set; get; } = "";
            public string 等级 { set; get; } = "";

            public _cfg_读码内容_ Clone()
            {
                return new _cfg_读码内容_
                {
                    原始内容 = this.原始内容,
                    内容 = this.内容,
                    等级 = this.等级,
                };
            }

        }

        public class _cfg_功能_
        {
            /// <summary>
            /// 是否使能读码器
            /// </summary>
            public bool 使能 { set; get; } = true;
            public bool 评级 { set; get; } = false;
            public bool 检测 { set; get; } = false;
            public bool 通讯方式选择 { set; get; } = false;

            public _cfg_功能_ Clone()
            {
                return new _cfg_功能_
                {
                    使能 = this.使能,
                    评级 = this.评级,
                    检测 = this.检测,
                    通讯方式选择 = this.通讯方式选择,
                };
            }

        }


        /// <summary>
        /// 通讯状态
        /// </summary>
        public enum _读码状态_
        {
            None = 0,
            读码中 = 1,
        }



        public enum _err_
        {
            OK,
            未连接,
            读码超时,
            未读出内容,
            评级不合格,
            解析评级故障,
            读码中,
            /// <summary>
            /// 检测时会出现
            /// </summary>
            读出内容,
        }







        #endregion


        public _cfg_功能_ _功能 = new _cfg_功能_();
        public _cfg_主参数_ _参数 = new _cfg_主参数_();
        public qfmain.Socket_Client TcpClient_sys;
        public qfmain.SerialPort_ Com_sys;
        /// <summary>
        /// 编码
        /// </summary>
        public Encoding _Encoding = Encoding.Default;

        bool IsInistiall = false;

        /// <summary>
        /// 存放参数的文件夹
        /// </summary>
        string _File = qfmain.软件类.Files_Cfg.Files_Config + "\\ReadCode";

        public string _读码器名称 = "ReadCode";

        public 读码器(string 读码器名称 = "读码器", string 文件夹名 = "ReadCode")
        {
            this._读码器名称 = 读码器名称;
            this._File = Path.Combine(qfmain.软件类.Files_Cfg.Files_Config, $"{文件夹名}");
            new qfmain.文件_文件夹().文件夹_新建(this._File, out string msgErr);
            读写参数(1);
            读取前后缀文件();
        }

        public byte[] _前缀_接收 = new byte[] { };
        public byte[] _后缀_接收 = new byte[] { 0x0D, 0x0A };

        public byte[] _前缀_发送 = new byte[] { };
        public byte[] _后缀_发送 = new byte[] { 0x0D, 0x0A };

        public virtual async Task 初始化()
        {
            if (!this._功能.使能)
            {
                return;
            }
            读写参数(1);

            if (this._参数.通讯方式 == _通讯方式_.TcpClient)
            {
                string path = Path.Combine(this._File, $"Tcp.cfg");
                this.TcpClient_sys = new qfmain.Socket_Client(path, new qfmain._解码_Cfg_(this._前缀_接收, this._后缀_接收, this._参数.读码器.通讯超时));
                this.TcpClient_sys.Event_接收数据_jm += On_接收数据;
                this.TcpClient_sys.Event_连接状态 += On_连接状态;
                await this.TcpClient_sys.Connect连接Async();
            }
            else if (this._参数.通讯方式 == _通讯方式_.SerialPort)
            {
                string path = Path.Combine(this._File, $"Com.dll");
                this.Com_sys = new qfmain.SerialPort_(path, new qfmain._解码_Cfg_(this._前缀_接收, this._后缀_接收, this._参数.读码器.通讯超时));
                this.Com_sys.Event_接收数据_jm += On_接收数据;
                this.Com_sys.Event_isOpen += On_连接状态;
                this.Com_sys.初始化(true);
            }

            IsInistiall = true;
        }

        public virtual void 释放()
        {
            if (!IsInistiall || !this._功能.使能)
            {
                return;
            }

            if (this._参数.通讯方式 == _通讯方式_.TcpClient)
            {
                this.TcpClient_sys.Stop关闭连接(out string msgErr);
                this.TcpClient_sys.Event_接收数据_jm -= On_接收数据;
                this.TcpClient_sys.Event_连接状态 -= On_连接状态;
            }
            else if (this._参数.通讯方式 == _通讯方式_.SerialPort)
            {
                this.Com_sys.Close(out string msgerr);
                this.Com_sys.Event_接收数据_jm -= On_接收数据;
                this.Com_sys.Event_isOpen -= On_连接状态;
            }

        }

        public void 读写参数(ushort model)
        {

            string path = Path.Combine(this._File, $"ReadCode.dll");
            _cfg_主参数_ cfg = this._参数;
            new qfmain.文件_文件夹().WriteReadJson(path, model, ref cfg, out string msgErr);
            this._参数 = cfg;


            this._前缀_发送 = 解析前后缀(this._参数.读码器.前后缀_发送.前缀);
            this._后缀_发送 = 解析前后缀(this._参数.读码器.前后缀_发送.后缀);

            this._前缀_接收 = 解析前后缀(this._参数.读码器.前后缀_接收.前缀);
            this._后缀_接收 = 解析前后缀(this._参数.读码器.前后缀_接收.后缀);


            #region 功能....使能

            if (!this._功能.使能)
            {
                this._功能.检测 = false;
                this._功能.评级 = false;

                this._参数.使能_读码器 = false;
                this._参数.使能_检测 = false;
                this._参数.使能_评级 = false;
            }
            else
            {
                this._参数.使能_检测 = !this._功能.检测 ? false : this._参数.使能_检测;
                this._参数.使能_评级 = !this._功能.评级 ? false : this._参数.使能_评级;
            }

            #endregion

        }

        public string[] 读取前后缀文件()
        {
            string path = Path.Combine(this._File, $"AsciiSE.txt");
            string[] se = new string[]
                {
                    "",
                    "{0D},{0A}",
                    "{0D}",
                    "{0A}",
                    "{02}",
                    "{03}",
                };//默认值
            new qfmain.文件_文件夹().WriteReadJson<string[]>(path, 1, ref se, out string msgErr);
            return se;
        }


        #region 本地方法

        byte[] 解析前后缀(string[] byte代码)
        {
            List<byte> lst = new List<byte>();
            foreach (var item in byte代码)
            {
                if (item == "{02}")
                {
                    lst.Add(0x02);
                }
                else if (item == "{03}")
                {
                    lst.Add(0x03);
                }
                else if (item == "{0D}")
                {
                    lst.Add(0x0D);
                }
                else if (item == "{0A}")
                {
                    lst.Add(0x0A);
                }
            }

            return lst.ToArray();
        }
         
        #endregion


        #region 事件响应


        string _接收数据 = string.Empty;
        qfmain._通讯过程_ _通讯辅助 = qfmain._通讯过程_.闲置;
        qfmain.延时_Task delay_sys = new qfmain.延时_Task();
        _读码状态_ _读码状态 = _读码状态_.None;
        public qfmain._连接状态_ _连接状态 = qfmain._连接状态_.未连接;


        void On_接收数据(byte[] data)
        {
            On_接收读码数据(data);
            this._接收数据 = _Encoding.GetString(data).Trim();
            On_Log(true, $"{this._读码器名称},{Language_.Get语言("接收")},{this._接收数据}");
            if (_通讯辅助 == qfmain._通讯过程_.等待反馈中)
            {
                this._通讯辅助 = qfmain._通讯过程_.已反馈;
                this.delay_sys.中断延时();

            }

        }

        void On_连接状态(qfmain._连接状态_ state)
        {
            this._连接状态 = state;
            On_读码器连接状态(state);
        }
        void On_连接状态(qfmain._打开状态_ state)
        {
            qfmain._连接状态_ a = 0;
            switch (state)
            {
                case qfmain._打开状态_.打开中:
                    a = qfmain._连接状态_.连接中;
                    break;
                case qfmain._打开状态_.未打开:
                    a = qfmain._连接状态_.未连接;
                    break;
                case qfmain._打开状态_.已打开:
                    a = qfmain._连接状态_.已连接;
                    break;
            }
            this._连接状态 = a;

            On_读码器连接状态(a);

        }


        #endregion


        #region 事件

        /// <summary>
        /// 参数(状态,信息)
        /// </summary>
        public event Action<bool, string> Event_Log;
        void On_Log(bool state, string msg)
        {
            Event_Log?.Invoke(state, msg);
        }

        public event Action<byte[]> Event_接收数据;
        /// <summary>
        /// 接收到读码数据
        /// </summary>
        /// <param name="data"></param>
        void On_接收读码数据(byte[] data)
        {
            Event_接收数据?.Invoke(data);
        }


        public event Action<qfmain._连接状态_> Event_读码器连接状态;
        void On_读码器连接状态(qfmain._连接状态_ state)
        {
            this._连接状态 = state;
            Event_读码器连接状态?.Invoke(state);
        }

        public event Action<_读码状态_> Event_读码状态;
        void On_读码状态(_读码状态_ state)
        {
            this._读码状态 = state;
            Event_读码状态?.Invoke(state);
        }



        #endregion

        #region 方法

        enum _操作_
        {
            检测,
            读码,
        }

        byte[] 生成发送数据(string 指令)
        {
            List<byte> lstSend = new List<byte>();
            //前缀
            foreach (var item in this._前缀_发送)
            {
                lstSend.Add(item);
            }

            //指令
            byte[] send = _Encoding.GetBytes(指令);
            foreach (var item in send)
            {
                lstSend.Add(item);
            }

            //后缀
            foreach (var item in this._后缀_发送)
            {
                lstSend.Add(item);
            }

            return lstSend.ToArray();
        }

        void 发送_启动()
        {
            if (string.IsNullOrEmpty(this._参数.读码器.指令_启动)
                && this._前缀_发送.Length == 0
                && this._后缀_发送.Length == 0)
            {
                return;
            }
            else if (string.IsNullOrEmpty(this._参数.读码器.指令_启动)
                      && this._前缀_发送.Length == 0)
            {
                return;
            }

            byte[] _指令_启动 = 生成发送数据(this._参数.读码器.指令_启动);

            if (this._参数.通讯方式 == _通讯方式_.TcpClient)
            {
                this.TcpClient_sys.Send发送(_指令_启动);
            }
            else if (this._参数.通讯方式 == _通讯方式_.SerialPort)
            {
                this.Com_sys.Send_发送(_指令_启动, out string msgErr);
            }

        }

        void 发送_停止()
        {
            if (!this._参数.读码器.使能_停止指令 ||
               (string.IsNullOrEmpty(this._参数.读码器.指令_停止)
                && this._前缀_发送.Length == 0
                && this._后缀_发送.Length == 0))
            {
                return;
            }
            else if (string.IsNullOrEmpty(this._参数.读码器.指令_停止)
                   && this._后缀_发送.Length == 0)
            {
                return;
            }

            byte[] _指令_停止 = 生成发送数据(this._参数.读码器.指令_停止);
            if (this._参数.通讯方式 == _通讯方式_.TcpClient)
            {
                this.TcpClient_sys.Send发送(_指令_停止);
            }
            else if (this._参数.通讯方式 == _通讯方式_.SerialPort)
            {
                this.Com_sys.Send_发送(_指令_停止, out string msgErr);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Model">0:检测,1:读码</param>      ..
        /// <returns></returns>
        bool 发送(_操作_ Model, out _err_ err, out _cfg_读码内容_ 内容, out string msgErr)
        {
            内容 = new _cfg_读码内容_();
            msgErr = string.Empty;
            err = _err_.未读出内容;
            On_读码状态(_读码状态_.读码中);

            int 读码超时 = 0;
            int 读码前延时 = 0;
            int 读码次数 = 0;
            int 多次读码间隔 = 0;

            #region 参数

            switch (Model)
            {
                case _操作_.检测:
                    读码超时 = this._参数.检测.读码超时;
                    读码前延时 = this._参数.检测.读码前延时;
                    读码次数 = this._参数.检测.读码次数;
                    多次读码间隔 = this._参数.检测.多次读码间隔;

                    break;
                case _操作_.读码:
                    读码超时 = this._参数.读码器.读码超时;
                    读码前延时 = this._参数.读码器.读码前延时;
                    读码次数 = this._参数.读码器.读码次数;
                    多次读码间隔 = this._参数.读码器.多次读码间隔;
                    break;
            }

            #endregion



            bool rt = true;
            if (读码前延时 > 0)
            {
                Thread.Sleep(读码前延时);
            }

            for (int i = 0; i < 读码次数; i++)
            {
                #region 初始值

                this._接收数据 = string.Empty;
                this._通讯辅助 = qfmain._通讯过程_.等待反馈中;
                this._读码状态 = _读码状态_.读码中;

                #endregion

                发送_启动();
                delay_sys.延时(读码超时).Wait();
                发送_停止();

                #region 解析

                switch (Model)
                {
                    case _操作_.检测:
                        rt = 解析_检测(this._接收数据, out 内容, out err, out msgErr);

                        break;
                    case _操作_.读码:
                        rt = 解析_读码(this._接收数据, out 内容, out err, out msgErr);
                        break;
                }

                #endregion

                if (rt)
                {
                    break;
                }
                else if (!rt && (err == _err_.读码超时 || err == _err_.解析评级故障))
                {
                    break;
                }
                else if (!rt && (i < 读码次数 - 1))
                {
                    if (多次读码间隔 > 0)
                    {
                        Thread.Sleep(多次读码间隔);
                    }
                }
            }

            On_读码状态(_读码状态_.None);

            return rt;
        }



        public virtual bool 解析_检测(string 接收内容, out _cfg_读码内容_ 内容, out _err_ err, out string msgErr)
        {
            内容 = new _cfg_读码内容_();
            内容.原始内容 = _接收数据;
            err = _err_.未读出内容;
            msgErr = string.Empty;
            bool rt = true;
            if (this._通讯辅助 == qfmain._通讯过程_.等待反馈中)
            {
                msgErr = $"{this._读码器名称},{Language_.Get语言("读码超时")}";
                rt = false;
                err = _err_.读码超时;
            }
            else if (string.IsNullOrEmpty(this._接收数据) || this._接收数据 == this._参数.读码器.错误标识)
            {
                msgErr = $"{this._读码器名称},{Language_.Get语言("未读出内容")}";
                rt = true;
                err = _err_.未读出内容;
            }
            else if (!string.IsNullOrEmpty(this._接收数据) && this._接收数据 != this._参数.读码器.错误标识)
            {
                msgErr = $"{this._读码器名称},{Language_.Get语言("读出内容")}";
                err = _err_.读出内容;
                rt = false;
            }


            return rt;
        }

        public virtual bool 解析_读码(string 接收内容, out _cfg_读码内容_ 内容, out _err_ _err, out string msgErr)
        {
            内容 = new _cfg_读码内容_();
            内容.原始内容 = _接收数据;
            _err = _err_.未读出内容;
            msgErr = string.Empty;
            bool rt = true;
            #region 解析

            if (this._通讯辅助 == qfmain._通讯过程_.等待反馈中)
            {
                msgErr = $"{this._读码器名称},{Language_.Get语言("读码超时")}";
                rt = false;
                _err = _err_.读码超时;
            }
            else if (string.IsNullOrEmpty(this._接收数据) || this._接收数据 == this._参数.读码器.错误标识)
            {
                msgErr = $"{this._读码器名称},{Language_.Get语言("未读出内容")}";
                rt = false;
                _err = _err_.未读出内容;
            }
            else if (!string.IsNullOrEmpty(this._接收数据) && this._接收数据 != this._参数.读码器.错误标识)
            {
                string[] work = new string[]
                {
                    "不评级",
                    "评级",
                    "评级作假",
                    "评级分析",
                };

                foreach (string s in work)
                {
                    if (!rt)
                    {
                        break;
                    }
                    else if (s == "不评级")
                    {
                        #region 不评级

                        if (!this._功能.评级 || !this._参数.使能_评级)
                        {
                            内容.内容 = _接收数据;
                            msgErr = $"{this._读码器名称},{Language_.Get语言("读出内容")}";
                            _err = _err_.OK;
                            break;
                        }

                        #endregion
                    }
                    else if (s == "评级")
                    {
                        #region 评级

                        string[] a = 内容.原始内容.Split(new string[] { this._参数.评级.分割符 }, StringSplitOptions.None);
                        try
                        {
                            内容.内容 = a[0];
                            内容.等级 = new qfmain.文本().获取(a[1], 0, 1).Trim();

                        }
                        catch (Exception)
                        {
                            rt = false;
                            msgErr = $"{this._读码器名称},{Language_.Get语言("解析评级失败,请检查设置参数")}";
                            _err = _err_.解析评级故障;

                        }

                        #endregion
                    }
                    else if (s == "评级作假")
                    {
                        #region 等级作假

                        foreach (var t0 in this._参数.自定义等级)
                        {
                            if (t0.Name.Trim() == 内容.等级)
                            {
                                内容.等级 = t0.Value.Trim();
                                break;
                            }
                        }

                        #endregion
                    }
                    else if (s == "评级分析")
                    {
                        #region 评级分析

                        bool is合格 = false;
                        foreach (var t1 in this._参数.评级.合格等级)
                        {
                            if (t1.Trim() == 内容.等级)
                            {
                                is合格 = true;
                                break;
                            }
                        }

                        msgErr = is合格 ? $"{this._读码器名称},{Language_.Get语言("评级合格")}" : $"{this._读码器名称},{Language_.Get语言("评级不合格")}";
                        rt = !is合格 ? false : rt;
                        _err = is合格 ? _err_.OK : _err_.评级不合格;
                        #endregion
                    }

                }

            }

            #endregion

            return rt;
        }


        #endregion


        #region 操作...对外

        /// <summary>
        /// 放在线程中使用
        /// </summary>
        /// <returns></returns>
        public virtual bool 检测(out _cfg_读码内容_ 内容, out string msgErr)
        {
            msgErr = string.Empty;
            bool rt = true;
            内容 = new _cfg_读码内容_();

            if (!Err_未连接(out msgErr) || !Err_读码中(out msgErr))
            {
                rt = false;
                return rt;
            }

            rt = 发送(_操作_.检测, out _err_ err, out 内容, out msgErr);
            this._通讯辅助 = qfmain._通讯过程_.闲置;

            return rt;
        }


        /// <summary>
        /// 放在异步线程中使用
        /// </summary>
        /// <returns></returns>
        public virtual bool 读码(out _cfg_读码内容_ 内容, out _err_ _err, out string msgErr)
        {
            msgErr = string.Empty;
            bool rt = true;
            内容 = new _cfg_读码内容_();
            _err = _err_.未读出内容;


            if (!Err_未连接(out msgErr))
            {
                _err = _err_.未连接;
                rt = false;
                return rt;
            }
            else if (!Err_读码中(out msgErr))
            {
                _err = _err_.读码中;
                rt = false;
                return rt;
            }


            rt = 发送(_操作_.读码, out _err, out 内容, out msgErr);

            this._通讯辅助 = qfmain._通讯过程_.闲置;

            return rt;
        }




        #endregion


        #region Err

        public bool Err_未连接(out string msgErr)
        {
            msgErr = "";
            if (this._连接状态 != qfmain._连接状态_.已连接)
            {
                msgErr = $"{this._读码器名称}{Language_.Get语言("未连接")}";
                return false;
            }
            return true;
        }

        public bool Err_读码中(out string msgErr)
        {
            msgErr = "";
            if (this._读码状态 != _读码状态_.None)
            {
                msgErr = $"{this._读码器名称}{Language_.Get语言("读码中")}";
                return false;
            }
            return true;
        }


        #endregion



        public void 刷新参数()
        {

            if (!IsInistiall)
            {
                return;
            }
            读写参数(1);




            if (this._参数.使能_读码器)
            {

                if (this._参数.通讯方式 == _通讯方式_.TcpClient)
                {
                    this.TcpClient_sys.重连_不停止线程();
                }
                else if (this._参数.通讯方式 == _通讯方式_.SerialPort)
                {
                    this.Com_sys.Open(out string msgErr);
                }

            }
            else
            {
                if (this._参数.通讯方式 == _通讯方式_.TcpClient)
                {
                    this.TcpClient_sys.Stop关闭连接(out string msgerr);
                }
                else if (this._参数.通讯方式 == _通讯方式_.SerialPort)
                {
                    this.Com_sys.Close(out string msgErr);
                }
            }


        }
    }
}
