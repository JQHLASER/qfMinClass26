using Newtonsoft.Json;
using NPOI.SS.Formula.Functions;
using Org.BouncyCastle.Tls.Crypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace qfmain
{
    public class 软件注册_TCP通讯_终端版 : Language_
    {
        static string _path = 软件类.Files_Cfg.Files_sysConfig + "\\Gtcpser.dll";


        /// <summary>
        /// 远程注册接口
        /// </summary>
        public Socket_Client TcpClient_sys = new Socket_Client(_path, new _解码_Cfg_(null, new byte[] { 0x0D, 0x0A }, 5000));
        private 软件注册 keys_sys;


        /// <summary>
        /// <param name="ip">服务器IP</param>
        /// <param name="Port">服务器Port</param>
        /// </summary>
        /// <param name="keys_"></param>
        /// <param name="ip"></param>
        /// <param name="Port"></param>
        public 软件注册_TCP通讯_终端版(软件注册 keys_, string ip = const常量._软件授权_服务器_ip, int Port = const常量._软件授权_服务器_port)
        {
            keys_sys = keys_;
            this.TcpClient_sys.Event_连接状态 += this.On_连接状态;
            this.TcpClient_sys.Event_接收数据_jm += this.On_接收数据;


            this.TcpClient_sys._参数.IP = ip;
            this.TcpClient_sys._参数.Port = Port;
            this.TcpClient_sys.Connect连接Async();
            this.isInistiall = true;
        }
        bool isInistiall = false;

        public void 释放()
        {
            if (!this.isInistiall)
            {
                return;
            }
            this.TcpClient_sys.Event_连接状态 -= this.On_连接状态;
            this.TcpClient_sys.Event_接收数据_jm -= this.On_接收数据;


            this.TcpClient_sys.Stop关闭连接(out string smgErr);
        }

        void On_连接状态(_连接状态_ state)
        {
            switch (state)
            {
                case qfmain._连接状态_.连接中:
                    On_TCP信息($"server:{Language_.Get语言("连接中")}");
                    break;
                case qfmain._连接状态_.已连接:
                    On_TCP信息($"server:{Language_.Get语言("已连接")}");
                    //Task.Run(() =>
                    //{
                    //    Task.Delay(500).Wait();
                    //    _软件授权_Tcp注册_通讯协议_ info反馈 = new _软件授权_Tcp注册_通讯协议_();
                    //    发送基本信息(info反馈, out string msgErr);
                    //});
                    break;
                case qfmain._连接状态_.未连接:
                    On_TCP信息($"server:{Language_.Get语言("未连接")}");
                    break;
            }
        }

        void On_发送开始()
        {


        }

        void On_接收数据(byte[] data)
        {
            // string xt = Encoding.Default .GetString(data);
            string xt = new Encoding编码().bytesToString(data);
            // MessageBox.Show(xt);
            解析(xt);
        }


        static string[] WorkBeff = new string[]
        {
            "判断版本",
            "解析指令",
        };

        void 解析(string data)
        {
            _软件授权_Tcp注册_通讯协议_ info = JsonConvert.DeserializeObject<_软件授权_Tcp注册_通讯协议_>(data);
            _软件授权_Tcp注册_通讯协议_ info反馈 = new _软件授权_Tcp注册_通讯协议_();

            bool rt = true;
            string msgErr = string.Empty;

            try
            {



                foreach (var s in WorkBeff)
                {
                    if (!rt)
                    {
                        break;
                    }
                    else if (s == "判断版本")
                    {
                        #region 判断版本

                        if (info.通讯功能 != _软件授权_TCP通讯功能_.软件终端)
                        {
                            rt = false;
                            msgErr = $"功能不匹配,<{info.通讯功能}>";
                        }

                        #endregion
                    }
                    else if (s == "解析指令")
                    {
                        #region 解析指令

                        switch (info.通讯指令)
                        {
                            case _软件授权_Tcp_指令_.获取基本信息:

                                #region 获取基本信息

                                //info反馈.是否试用 = this.keys_sys._是否试用;
                                //info反馈.注册类型 = this.keys_sys._注册类型;
                                //info反馈.注册信息 = this.keys_sys._注册信息;
                                //info反馈.机器码 = this.keys_sys._机器码;
                                //info反馈.机器码信息 = this.keys_sys._机器码信息;

                                //msgErr = "获取基本信息,成功";

                                发送基本信息(info反馈, out msgErr);


                                #endregion

                                break;

                            case _软件授权_Tcp_指令_.本地_写信息:

                                #region 本地_写信息

                                string 客户ID = info.机器码信息.Uid;
                                if (string.IsNullOrEmpty(客户ID))
                                {
                                    rt = false;
                                    msgErr = "客户ID不能为空";
                                    continue;
                                }

                                if (客户ID != keys_sys._机器码信息.Uid)
                                {
                                    keys_sys._err = _软件授权_Err_.未注册;
                                    On_注册结果(false, "", $"{Get语言("系统未授权")}");
                                }

                                this.keys_sys.客户ID读写_从本地(0, ref 客户ID);
                                this.keys_sys.获取信息();
                                On_更新机器码(this.keys_sys._机器码);
                                info反馈.机器码信息.Uid = 客户ID;
                                info反馈.机器码 = keys_sys._机器码;
                                msgErr = "写本地信息,成功";


                                #endregion

                                break;
                            case _软件授权_Tcp_指令_.发送注册码:

                                #region 发送注册码

                                if (string.IsNullOrEmpty(info.注册码))
                                {
                                    rt = false;
                                    msgErr = "注册码不能为空";
                                    continue;
                                }

                                _软件授权_Err_ err = this.keys_sys.注册(info.注册码, this.keys_sys._机器码信息, out _软件授权_注册信息_ 注册信息, out msgErr);
                                if (err == _软件授权_Err_.已完全注册 ||
                                    err == _软件授权_Err_.已日期注册)
                                {
                                    this.keys_sys._err = err;
                                    string xt = info.注册码;


                                    //注册成功后保存注册码
                                    switch (this.keys_sys._注册类型)
                                    {
                                        case _软件授权_注册类型_.加密狗:

                                            this.keys_sys._Dog数据信息.注册码 = xt;
                                            rt = DogTW.dog_写信息(this.keys_sys._Dog数据信息, out msgErr);
                                            msgErr = rt ? $"{Get语言("写狗成功")},{msgErr}" : $"{Get语言("写狗失败")},{msgErr}";

                                            break;
                                        default:
                                            this.keys_sys.注册码读写_从本地(0, ref xt);
                                            break;
                                    }

                                    if (rt)
                                    {

                                        On_注册结果(true, xt, $"{Get语言("请重启软件")},{msgErr}");
                                    }
                                }
                                else
                                {
                                    rt = false;
                                    msgErr = $"{msgErr}";
                                }
                                #endregion

                                break;
                            case _软件授权_Tcp_指令_.加密狗_获取信息:

                                #region 加密狗_获取信息

                                _Dog_Err_ nErr = DogTW.Get狗信息(out _Dog_Cfg_ dogcfg, out _Dog_硬件信息_ dog硬件, out msgErr);
                                rt = nErr == _Dog_Err_.正常 ? true : false;

                                info反馈.dog硬件信息 = dog硬件;
                                info反馈.dog数据信息 = dogcfg;
                                info反馈.机器码信息.Sn = dog硬件.Id;
                                info反馈.机器码信息.Uid = dogcfg.Uid;

                                info反馈.机器码 = 软件注册_子程序.Get机器码(info反馈.机器码信息);

                                this.keys_sys.获取信息();



                                #endregion

                                break;
                            case _软件授权_Tcp_指令_.加密狗_写信息:

                                #region 加密狗_写信息

                                if (string.IsNullOrEmpty(info.dog数据信息.Uid))
                                {
                                    rt = false;
                                    msgErr = "客户ID不能为空";
                                    break;
                                }
                                else if (string.IsNullOrEmpty(info.dog数据信息.Types))
                                {
                                    rt = false;
                                    msgErr = "狗类型不能为空";
                                    break;
                                }


                                string uid = keys_sys._Dog数据信息.Uid;
                                rt = DogTW.dog_写信息(info.dog数据信息, out msgErr);
                                msgErr = rt ? $"写狗成功,{msgErr}" : $"写狗失败,{msgErr}";
                                if (rt)
                                {
                                    keys_sys.获取信息();
                                    On_更新机器码($"{keys_sys._Dog硬件信息.Id}-{keys_sys._Dog数据信息.Uid}");
                                    if (uid != info.dog数据信息.Uid)
                                    {
                                        keys_sys._err = _软件授权_Err_.未注册;
                                        On_注册结果(false, "", $"{Get语言("系统未授权")}");
                                    }
                                }


                                #endregion

                                break;
                            case _软件授权_Tcp_指令_.加密狗_恢复出厂设置:

                                #region 加密狗_恢复出厂设置

                                DogTW.恢复出厂状态(out msgErr);
                                rt = DogTW.设置_dog读写密码(out msgErr);

                                msgErr = rt ? $"狗恢复出厂状态成功,{msgErr}" : $"狗恢复出厂状态失败,{msgErr}";

                                #endregion

                                break;

                            case _软件授权_Tcp_指令_.加密狗_获取硬件信息:

                                #region 加密狗_获取硬件信息

                                rt = DogTW.获取_dog硬件信息(out _Dog_硬件信息_ Dog硬件信息, out msgErr);
                                info反馈.dog硬件信息 = Dog硬件信息;
                                msgErr = rt ? $"获取狗硬件信息成功" : $"获取狗硬件信息失败,{msgErr}";

                                #endregion

                                break;
                            default:
                                rt = false;
                                info反馈.msg = "未知通讯指令";
                                break;

                        }

                        #endregion
                    }
                    else if (s == "")
                    {
                        #region MyRegion

                        #endregion
                    }


                }
            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }

            info反馈.注册类型 = this.keys_sys._注册类型;
            info反馈.通讯指令 = info.通讯指令;
            info反馈.通讯功能 = _软件授权_TCP通讯功能_.软件终端;
            info反馈.是否试用 = this.keys_sys._是否试用;
            info反馈.Code = rt ? 0 : -1;
            info反馈.msg = msgErr;
            info反馈.err_注册结果 = this.keys_sys._err;
            string sendStr = $"{JsonConvert.SerializeObject(info反馈)}\r\n";
            // byte[] sendBytes = Encoding.Default .GetBytes(sendStr);
            byte[] sendBytes = new Encoding编码().stringToBytes(sendStr);
            this.TcpClient_sys.Send发送(sendBytes);
        }

        void 发送基本信息(_软件授权_Tcp注册_通讯协议_ info反馈, out string msgErr, bool is主动发送 = false)
        {
            bool rt = true;

            #region 获取基本信息

            info反馈.是否试用 = this.keys_sys._是否试用;
            info反馈.注册类型 = this.keys_sys._注册类型;
            info反馈.注册信息 = this.keys_sys._注册信息;
            info反馈.机器码 = this.keys_sys._机器码;
            info反馈.机器码信息 = this.keys_sys._机器码信息;
            info反馈.dog数据信息 = this.keys_sys._Dog数据信息;
            info反馈.dog硬件信息 = this.keys_sys._Dog硬件信息;

            msgErr = is主动发送 ? "主动发送基本信息,成功" : "获取基本信息,成功";
            #endregion

            if (is主动发送)
            {
                info反馈.注册类型 = this.keys_sys._注册类型;
                info反馈.通讯指令 = _软件授权_Tcp_指令_.获取基本信息;
                info反馈.通讯功能 = _软件授权_TCP通讯功能_.软件终端;
                info反馈.是否试用 = this.keys_sys._是否试用;
                info反馈.Code = rt ? 0 : -1;
                info反馈.msg = msgErr;
                info反馈.err_注册结果 = this.keys_sys._err;

                Task.Delay(500).Wait();
                string sendStr = $"{JsonConvert.SerializeObject(info反馈)}\r\n";
                // byte[] sendBytes = Encoding.Default .GetBytes(sendStr);
                byte[] sendBytes = new Encoding编码().stringToBytes(sendStr);
                this.TcpClient_sys.Send发送(sendBytes);
            }
        }


        #region 事件

        /// <summary>
        /// 参数:(是否成功,注册码,信息)
        /// </summary>
        public event Action<bool, string, string> Event_注册结果;
        /// <summary>
        /// 参数:(是否成功,注册码,信息)
        /// </summary>
        /// <param name="注册码"></param>
        /// <param name="msg"></param>
        void On_注册结果(bool 是否成功, string 注册码, string msg)
        {
            Event_注册结果?.Invoke(是否成功, 注册码, msg);
        }

        public event Action<string> Event_TCP信息;
        void On_TCP信息(string msg)
        {
            Event_TCP信息?.Invoke(msg);
        }
        public event Action<string> Event_更新机器码;
        void On_更新机器码(string 机器码)
        {
            Event_更新机器码?.Invoke(机器码);
        }

        #endregion




    }
}
