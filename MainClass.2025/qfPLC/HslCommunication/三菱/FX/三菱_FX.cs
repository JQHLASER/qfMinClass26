using HslCommunication;
using HslCommunication.Profinet.Melsec;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace qfPLC
{
    public class 三菱_FX : IWorker
    {
        /// <summary>
        /// 串口信息
        /// </summary>
        public class _cfg_
        {
            public string 串口名称 { set; get; } = "";
            public int 波特率 { set; get; } = 9600;
            public int 数据位 { set; get; } = 8;
            /// <summary>
            /// None
            /// </summary>
            public Parity 校验位 { set; get; } = Parity.None;

            /// <summary>
            /// One
            /// </summary>
            public StopBits 停止位 { set; get; } = StopBits.One;
        }


        /// <summary>
        /// 参数保存路径
        /// </summary>
        private string _path = Environment.CurrentDirectory + "\\Fx.txt";
        public qfmain._连接状态_ _连接状态 = qfmain._连接状态_.未连接;

        /// <summary>
        /// plc对象
        /// </summary>
        public MelsecFxSerial _MelsecFxSerial = new MelsecFxSerial();
        public _cfg_ _参数 = new _cfg_();



        /// <summary>
        /// path_:参数保存路径
        /// </summary> 
        public 三菱_FX(string path_)
        {
            this._path = path_;

        }

        private readonly object _lock = new object();

        /// <summary>
        /// 0:写,1:读
        /// </summary>   
        public void 读写参数(ushort model)
        {
            lock (_lock)
            {
                _cfg_ info = this._参数;
                new qfmain.文件_文件夹().WriteReadJson(this._path, model, ref info, out string msgErr);
                this._参数 = info;
            }
        }


        public (bool rt, string msgErr) 连接(bool 是否先读参数 = true)
        {
            读写参数(1);
            return 连接(this._参数);
        }

        public (bool rt, string msgErr) 连接(_cfg_ cfg)
        {
            return 连接(cfg.串口名称, cfg.波特率, cfg.数据位, cfg.停止位, cfg.校验位);

        }

        /// <summary>
        /// <para>奇偶校验:0无1奇2偶;一般为偶校验</para>
        /// </summary> 
        public (bool rt, string msgErr) 连接(string 串口名称, int 波特率 = 9600, int 数据位 = 7, StopBits 停止位 = StopBits.One, Parity 校验位 = Parity.Even)
        {
            string msgErr = string.Empty;

            On_连接状态(qfmain._连接状态_.连接中);

            bool rt = true;
            this._MelsecFxSerial?.Close();
            this._MelsecFxSerial = new MelsecFxSerial();
            try
            {
                this._MelsecFxSerial.SerialPortInni(sp =>
                {
                    sp.PortName = 串口名称;
                    sp.BaudRate = 波特率;
                    sp.DataBits = 数据位;
                    sp.StopBits = 停止位;
                    sp.Parity = 校验位;

                    //sp.StopBits = stopBits == 0 ? System.IO.Ports.StopBits.None : (stopBits == 1 ? System.IO.Ports.StopBits.One : System.IO.Ports.StopBits.Two);
                    //sp.Parity = 奇偶校验 == 0 ? System.IO.Ports.Parity.None : (奇偶校验 == 1 ? System.IO.Ports.Parity.Odd : System.IO.Ports.Parity.Even);
                });
              

                this._MelsecFxSerial.Open();
                rt = this._MelsecFxSerial.IsOpen();
            }
            catch (Exception ex)
            {
                msgErr = ex.Message;
                rt = false;
            }

            qfmain._连接状态_ rtState = rt ? qfmain._连接状态_.已连接 : qfmain._连接状态_.未连接;
            On_连接状态(rtState);
            return (rt, msgErr);
        }

        public virtual (bool rt, string msgErr) 断开()
        {
            On_连接状态(qfmain._连接状态_.未连接);
            bool rt = true;
            string msgErr = string.Empty;
            try
            {
                this._MelsecFxSerial.Close();               
                rt = !this._MelsecFxSerial.IsOpen();

            }
            catch (Exception ex)
            {
                msgErr = ex.Message;
                rt = false;
            }
            return (rt, msgErr);
        }

        public virtual void 窗体设置(string Title, bool 重连)
        {
            using (Form_FX forms = new Form_FX(this, Title))
            {
                DialogResult dlt = forms.ShowDialog();
                if ( 重连 &&dlt == DialogResult.OK)
                {
                    连接(true);
                }
            }
        }

        #region Write



        public virtual (bool rt, string msgErr) Write<T>(string address, T value) where T : struct
        {
            return new WritePlc().Write(this._MelsecFxSerial, address, value);
        }

        public virtual (bool rt, string msgErr) Write(string address, string value)
        {
            return new WritePlc().Write(this._MelsecFxSerial, address, value);
        }
        public virtual (bool rt, string msgErr) Write(Encoding encoding, string address, string value)
        {
            return new WritePlc().Write(this._MelsecFxSerial, encoding, address, value);
        }


        public virtual async Task<(bool rt, string msgErr)> WriteAsync<T>(string address, T value) where T : struct
        {
            return await new WritePlc().WriteAsync(this._MelsecFxSerial, address, value);
        }
        public virtual async Task<(bool rt, string msgErr)> WriteAsync(string address, string value)
        {
            return await new WritePlc().WriteAsync(this._MelsecFxSerial, address, value);
        }
        public virtual async Task<(bool rt, string msgErr)> WriteAsync(Encoding encoding, string address, string value)
        {
            return await new WritePlc().WriteAsync(this._MelsecFxSerial, encoding, address, value);
        }




        #endregion


        #region Read

        public virtual (bool rt, string msgErr, T value) Read<T>(string address)
        {
            return new ReadPlc().Read<T>(this._MelsecFxSerial, address);
        }
        public virtual (bool rt, string msgErr, T value) Read<T>(string address, ushort length)
        {
            return new ReadPlc().Read<T>(this._MelsecFxSerial, address, length);
        }
        public virtual (bool rt, string msgErr, string value) Read(string address, ushort length, Encoding encoding)
        {
            return new ReadPlc().Read(this._MelsecFxSerial, address, length, encoding);
        }
        public virtual (bool rt, string msgErr, string value) Read(string address, ushort length )
        {
            return new ReadPlc().Read(this._MelsecFxSerial, address, length );
        }


        public virtual async Task<(bool rt, string msgErr, T value)> ReadAsync<T>(string address)
        {
            return await new ReadPlc().ReadAsync<T>(this._MelsecFxSerial, address);
        }
        public virtual async Task<(bool rt, string msgErr, T value)> ReadAsync<T>(string address, ushort length)
        {
            return await new ReadPlc().ReadAsync<T>(this._MelsecFxSerial, address, length);
        }
        public virtual async Task<(bool rt, string msgErr, string value)> ReadAsync(string address, ushort length, Encoding encoding)
        {
            return await new ReadPlc().ReadAsync(this._MelsecFxSerial, address, length, encoding);
        }
        public virtual async Task<(bool rt, string msgErr, string value)> ReadAsync(string address, ushort length )
        {
            return await new ReadPlc().ReadAsync(this._MelsecFxSerial, address, length );
        }



        #endregion




        #region 事件

        public event Action<qfmain._连接状态_> Event_连接状态;
        public void On_连接状态(qfmain._连接状态_ state)
        {
            this._连接状态 = state;
            Event_连接状态?.Invoke(state);
        }

        #endregion

        public qfmain._连接状态_ Get连接状态()
        {
            return this._连接状态;
        }
    }
}
