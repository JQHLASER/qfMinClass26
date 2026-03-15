using HslCommunication;
using HslCommunication.ModBus;
using HslCommunication.Profinet.Siemens;
using Sunny.UI.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace qfPLC
{
    public class S7 : IWorker
    {
        //private static SiemensPLCS siemensPLCSelected = SiemensPLCS.S200Smart;
        //private SiemensS7Net siemensTcpNet = new SiemensS7Net(siemensPLCSelected);

        /// <summary>
        /// PLC对象
        /// </summary>
        public SiemensS7Net _siemensTcpNet = null;
        //private SiemensPLCS _siemensPLCSelected = SiemensPLCS.S1200;
        public _cfg_ _参数 = new _cfg_();


        public class _cfg_
        {
            public string IP { set; get; } = "192.168.0.100";
            public uint Port { set; get; } = 102;

            public SiemensPLCS PlcType { set; get; } = SiemensPLCS.S1200;

            /// <summary>
            /// PLC的机架号，针对S7-400的PLC设置的
            /// </summary>
            public byte Rack { set; get; } = 0;


            /// <summary>      
            /// PLC的槽号，针对S7-400的PLC设置的
            /// </summary>
            public byte Slot { set; get; } = 0;
        }


        /// <summary>
        /// 参数保存路径
        /// </summary>
        public string _path { set; get; } = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "S7.txt");
        public qfmain._连接状态_ _连接状态 { set; get; } = qfmain._连接状态_.未连接;

         

        /// <summary>
        /// path_:参数保存路径
        /// </summary> 
        public S7(string path_)
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

        /// <summary>
        /// <para>奇偶校验:0无1奇2偶;一般为偶校验</para>
        /// </summary> 
        public (bool rt, string msgErr) 连接(_cfg_ cfg)
        {
            bool rt = true;
            string msgErr = string.Empty;
            On_连接状态(qfmain._连接状态_.未连接);

            // 连接
            if (!System.Net.IPAddress.TryParse(cfg.IP, out System.Net.IPAddress address))
            {
                rt = false;
                msgErr = qfmain.Language_.Get语言("IP错误");
                return (rt, msgErr);
            }
            On_连接状态(qfmain._连接状态_.连接中);

            //  SiemensPLCS siemensPLCS = SiemensPLCS.S1200;
            //  siemensPLCSelected = siemensPLCSelected;
            // siemensTcpNet = new SiemensS7Net(siemensPLCSelected);
            _siemensTcpNet = new SiemensS7Net(cfg.PlcType);

            // 连接
            try
            {

                _siemensTcpNet.IpAddress = cfg.IP;
                _siemensTcpNet.Port = (int)cfg.Port;
                PlcType(cfg.PlcType);
                _siemensTcpNet?.ConnectClose();//先关闭连接 
                OperateResult connect = _siemensTcpNet.ConnectServer();
                msgErr = connect.Message;
                if (!connect.IsSuccess)
                    rt = false;

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

                OperateResult connect = _siemensTcpNet?.ConnectClose();
                rt = connect.IsSuccess;
                if (!rt)
                    msgErr = connect.Message;

            }
            catch (Exception ex)
            {
                msgErr = ex.Message;
                rt = false;
            }
            return (rt, msgErr);
        }

        public virtual DialogResult 窗体设置(string Title, bool 重连)
        {
            using (Form_西门子S7  forms = new Form_西门子S7(this, Title))
            {
                DialogResult dlt = forms.ShowDialog();
                if (重连 && dlt == DialogResult.OK)
                {
                    Task.Run(() => { 连接(true); });
                }
                return dlt;
            }
        }


        #region Write

        public virtual (bool rt, string msgErr) Write(string address, object value, Encoding encoding = null)
        {
            return new WritePlc().Write(this._siemensTcpNet, address, value);
        }

        public virtual async Task<(bool rt, string msgErr)> WriteAsync(string address, object value, Encoding encoding = null)
        {
            return await new WritePlc().WriteAsync(this._siemensTcpNet, address, value);
        }


        #endregion


        #region Read
        public virtual (bool state, string msg, T v) Read<T>(_ReadType_ Read_Type, string address, ushort length = 0, Encoding encoding = null)
        {
            return new ReadPlc().Read<T>(this._siemensTcpNet, Read_Type, address, length, encoding);
        }

        public virtual async Task<(bool state, string msg, T v)> ReadAsync<T>(_ReadTypeAsync_ Read_Type, string address, ushort length = 0, Encoding encoding = null)
        {
            return await new ReadPlc().ReadAsync<T>(this._siemensTcpNet, Read_Type, address, length, encoding);
        }



        #endregion


        #region 事件


        public event Action<qfmain._连接状态_> Event_连接状态;
        private void On_连接状态(qfmain._连接状态_ state)
        {
            this._连接状态 = state;
            Event_连接状态?.Invoke(state);
        }

        #endregion


        public qfmain._连接状态_ Get连接状态()
        {
            return this._连接状态;
        }


        #region 本地方法


        /// <summary>
        /// 根据不同的型号来配置Rack和slot
        /// </summary>
        /// <param name="PlcType">plc的类型,S1200/S1500/S300/S400/S200/S200Smart</param>
        internal void PlcType(SiemensPLCS PlcType)
        {
            /// PLC的机架号，针对S7-400的PLC设置的 
            byte Rack = 0;

            ///PLC的槽号，针对S7-400的PLC设置的
            byte Slot = 0;


            switch (PlcType)
            {
                case SiemensPLCS.S1200:
                    // _siemensPLCSelected = SiemensPLCS.S1200;
                    Rack = 0;
                    Slot = 0;
                    break;
                case SiemensPLCS.S1500:
                    //_siemensPLCSelected = SiemensPLCS.S1500;
                    Rack = 0;
                    Slot = 0;
                    break;
                case SiemensPLCS.S300:
                    // _siemensPLCSelected = SiemensPLCS.S300;
                    Rack = 0;
                    Slot = 2;
                    break;
                case SiemensPLCS.S400:
                    // _siemensPLCSelected = SiemensPLCS.S400;
                    Rack = 0;
                    Slot = 3;
                    break;
                case SiemensPLCS.S200:
                    // _siemensPLCSelected = SiemensPLCS.S200;
                    Rack = 0;
                    Slot = 0;
                    break;
                case SiemensPLCS.S200Smart:
                    // _siemensPLCSelected = SiemensPLCS.S200Smart;
                    Rack = 0;
                    Slot = 0;
                    break;
            }
            if (PlcType != SiemensPLCS.S200Smart)
            {
                _siemensTcpNet.Rack = Rack;
                _siemensTcpNet.Slot = Slot;
            }
        }




        #endregion




    }
}
