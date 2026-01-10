using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace qfmain
{
    public class SerialPort_ : Language_
    {


        /// <summary>
        /// path_ : 存储文件路径
        /// </summary>    
        public SerialPort_(string path_, _解码_Cfg_ cfg)
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
        /// path_ : 存储文件路径
        /// </summary>       
        public SerialPort_(string path_)
        {
            if (!string.IsNullOrEmpty(path_))
            {
                path = path_;
            }
        }

        public SerialPort_()
        {

        }

        private 解码 jm_sys;
        public _打开状态_ _打开状态 { set; get; } = _打开状态_.未打开;
        public _通讯中状态_ _通讯状态 { set; get; } = _通讯中状态_.闲置;

        public _SerialPort_Cfg_ _参数 { set; get; } = new _SerialPort_Cfg_();
        public SerialPort Com_sys = new SerialPort();

        /// <summary>
        /// 参数存储的路径
        /// </summary>
        string path { set; get; } = 软件类.Files_Cfg.Files_Config + "\\Com.txt";


        #region 事件


        /// <summary>
        /// 串口打开状态
        /// </summary>
        public event Action<_打开状态_> Event_isOpen;

        public event Action<byte[]> Event_接收数据;



        /// <summary>
        /// 参数:(object) sender,(System.IO.Ports.SerialErrorReceivedEventArgs) e
        /// </summary>
        public event Action<object, System.IO.Ports.SerialErrorReceivedEventArgs> Event_ErrorReceived;

        /// <summary>
        /// 参数:(object) sender,(System.IO.Ports.SerialPinChangedEventArgs) e
        /// </summary>
        public event Action<object, System.IO.Ports.SerialPinChangedEventArgs> Event_PinChanged;
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


        /// <summary>
        /// 打开状态
        /// </summary>
        /// <param name="status"></param>
        void On_Event_isOpen(_打开状态_ status)
        {
            this._打开状态 = status;
            Event_isOpen?.Invoke(status);
        }


        #endregion


        void On_PinChanged(object sender, System.IO.Ports.SerialPinChangedEventArgs e)
        {

            Event_PinChanged?.Invoke(sender, e);

        }

        void On_ErrorReceived(object sender, System.IO.Ports.SerialErrorReceivedEventArgs e)
        {

            Event_ErrorReceived?.Invoke(sender, e);

        }


        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int n = Com_sys.BytesToRead;//先记录下来，避免丢失
            byte[] buffer = new byte[n];
            Com_sys.Read(buffer, 0, n);//读取缓冲区数据
            byte[] data = buffer;
            On_接收数据(data);
            Com_sys.DiscardOutBuffer();
        }


        /// <summary>
        /// 0:写,1:读
        /// </summary>   
        public void 读写参数(ushort model)
        {
            _SerialPort_Cfg_ info = this._参数;
            new 文件_文件夹().WriteReadJson(path, model, ref info, out string msgErr);
            this._参数 = info;
        }

        void Com参数设置()
        {
            Com_sys.ReadBufferSize = this._参数.输入缓冲区大小;
            Com_sys.WriteBufferSize = this._参数.输出缓冲区大小;
            Com_sys.DtrEnable = this._参数.DtrEnable;
            Com_sys.RtsEnable = this._参数.RtsEnable;
            Com_sys.Handshake = this._参数.Handshake;
            Com_sys.ReadTimeout = this._参数.ReadTimeout;
        }

        /// <summary>
        /// <para>缓冲区大小,一般设为1,就是接收缓冲区的大小</para>
        /// <para>数据编码: null时为默认值 Default</para> 
        /// <para>已经Open()</para>
        /// </summary>
        /// <param name="事件发生前缓冲区大小"></param>
        /// <param name="数据编码"></param>
        public void 初始化(bool 打开串口 = true, int 事件发生前缓冲区大小 = 1, Encoding 数据编码 = null)
        {
            //设置事件发生前缓冲区大小,默认为1
            //com.ReceivedBytesThreshold = 1;
            Com_sys.ReceivedBytesThreshold = 事件发生前缓冲区大小;

            //SerialPort 内置多线程 收发不在一个线程
            Com_sys.DataReceived +=(s,e)=> SerialPort_DataReceived(s,e);
            Com_sys.ErrorReceived +=(s,e)=> On_ErrorReceived(s,e);
            Com_sys.PinChanged +=(s,e)=> On_PinChanged(s,e);


            //默认为: Encoding.ASCII;
            //Com_sys.Encoding = Encoding.ASCII;s

            读写参数(1);
            if (数据编码 == null)
            {
                Com_sys.Encoding = Encoding.Default;
            }
            else
            {
                Com_sys.Encoding = 数据编码;
            }

            if (打开串口)
            {
                Open(out string msErr);
            }

            isInistiall = true;
        }

        bool isInistiall = false;
        bool about_线程 = true;
        public void 释放()
        {
            if (!isInistiall)
            {
                return;
            }
             
            Close(out string msgErr);

            about_线程 = false;
            if (this.jm_sys != null)
            {
                this.jm_sys.Event_解码 -= On_接收数据_jm;
            }
           
        }


        /// <summary>
        /// 打开连接
        /// </summary>
        /// <param name="串口名称"></param>
        /// <param name="波特率"></param>
        /// <param name="数据位数"></param>
        /// <param name="停止位数"></param>
        /// <param name="校验位数"></param>
        /// <param name="msgErr"></param>
        /// <returns></returns>
        public bool Open(string 串口名称, int 波特率, int 数据位数, StopBits 停止位数, Parity 校验位数, out string msgErr, int ReadTimeout = -1, int WriteTimeout = -1)
        {
            On_Event_isOpen(_打开状态_.打开中);
            msgErr = string.Empty;
            bool rt = false;
            try
            {
                if (Com_sys.IsOpen)
                {
                    Com_sys.Close();
                }

                Com_sys.PortName = 串口名称;
                Com_sys.BaudRate = 波特率;
                Com_sys.DataBits = 数据位数;
                //Com_sys.Parity = (Parity)Enum.Parse(typeof(Parity), 校验位数);
                //Com_sys.StopBits = (StopBits)Enum.Parse(typeof(StopBits), 停止位数);
                Com_sys.Parity = 校验位数;
                Com_sys.StopBits = 停止位数;
                Com参数设置();

                Com_sys.ReadTimeout = ReadTimeout;
                Com_sys.WriteTimeout = WriteTimeout;

                Com_sys.Open();
                rt = Com_sys.IsOpen;
                if (rt)
                {
                    On_Event_isOpen(_打开状态_.已打开);
                }
                else
                {
                    On_Event_isOpen(_打开状态_.未打开);
                }
            }
            catch (Exception ex)
            {
                msgErr = ex.Message;
                On_Event_isOpen(_打开状态_.未打开);
                rt = false;
            }

            return rt;
        }
        /// <summary>
        /// 打开连接
        /// </summary>
        /// <param name="info"></param>
        /// <param name="msgErr"></param>
        /// <returns></returns>
        public bool Open(_SerialPort_Cfg_ info, out string msgErr)
        {
            // 读写参数(1, info);
            bool rt = Open(info.串口名称, info.波特率, info.数据位, info.停止位, info.校验位, out msgErr);
            return rt;
        }
        /// <summary>
        /// 打开连
        /// </summary>
        /// <param name="msgErr"></param>
        /// <param name="是否先读参数"></param>
        /// <returns></returns>
        public bool Open(out string msgErr, bool 是否先读参数 = true)
        {
            if (是否先读参数)
            {
                读写参数(1);
            }
            bool rt = Open(this._参数.串口名称, this._参数.波特率, this._参数.数据位, this._参数.停止位, this._参数.校验位, out msgErr, this._参数.ReadTimeout, this._参数.WriteTimeout);

            return rt;
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <returns></returns>
        public bool Close(out string msgErr)
        {
            On_Event_isOpen(_打开状态_.未打开);
            bool rt = true;
            msgErr = string.Empty;
            try
            {
                Com_sys.Close();
                return true;
            }
            catch (Exception ex)
            {
                msgErr = ex.Message;
                rt = false;
            }

            return rt;
        }

        public bool 获取_当前连接状态()
        {
            return Com_sys.IsOpen;
        }


        public bool Send_发送(byte[] data, out string msgErr)
        {
            msgErr = string.Empty;
            try
            {
                Com_sys.Write(data, 0, data.Length);
                return true;
            }
            catch (Exception ex)
            {
                msgErr = ex.Message;
                return false;
            }
        }
        public bool Send_发送(string data, out string msgErr)
        {
            msgErr = string.Empty;
            try
            {
                byte[] str = new Encoding编码().stringToBytes(data);
                Com_sys.Write(str, 0, str.Length);
                return true;
            }
            catch (Exception ex)
            {
                msgErr = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 枚举系统中所有串口名称
        /// </summary>
        /// <returns></returns>
        public string[] Get_串口名称()
        {
            return SerialPort.GetPortNames();
        }

        /// <summary>
        /// 枚举校验位
        /// </summary>
        /// <returns></returns>
        public string[] Get_校验位()
        {
            return Enum.GetNames(typeof(Parity));
        }

        /// <summary>
        /// 枚举停止位
        /// </summary>
        /// <returns></returns>
        public string[] Get_停止位()
        {
            return Enum.GetNames(typeof(StopBits));
        }

        /// <summary>
        /// 枚举数据位
        /// </summary>
        /// <returns></returns>
        public int[] Get_数据位()
        {
            return new int[] { 5, 6, 7, 8 };
        }

        /// <summary>
        /// 枚举波特率
        /// </summary>
        /// <returns></returns>
        public int[] Get_波特率()
        {
            return new int[] { 110, 300, 600, 1200, 2400, 4800, 9600, 14400, 19200, 38400, 56000, 57600, 115200 };
        }





        #region Err

        public bool Err_未连接(out string msgErr)
        {
            msgErr = string.Empty;
            if (this._打开状态 != _打开状态_.已打开)
            {
                msgErr = Get语言("未打开");
                return false;
            }
            return true;
        }

        public bool Err_通讯中(out string msgErr)
        {
            msgErr = string.Empty;
            if (this._通讯状态 != _通讯中状态_.通讯中)
            {
                msgErr = Get语言("通讯中");
                return false;
            }
            return true;
        }



        #endregion

    }
}
