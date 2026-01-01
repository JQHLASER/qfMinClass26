using HslCommunication;
using HslCommunication.ModBus;
using HslCommunication.Profinet.Melsec;
using Sunny.UI.Win32;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace qfPLC
{
    public class ModbusTcp : IWorker
    {
        public class _cfg_
        {
            public string IP { set; get; } = "192.168.0.100";
            public uint Port { set; get; } = 502;

            public byte 站号 { set; get; } = 1;

            public bool 首地址从0开始 { set; get; } = true;

            public bool 字符串是否颠倒 { set; get; } = false;

            public HslCommunication.Core.DataFormat 字符串模式 { set; get; } = HslCommunication.Core.DataFormat.CDAB;

        }


        /// <summary>
        /// 参数保存路径
        /// </summary>
        private string _path = Environment.CurrentDirectory + "\\ModbusTcp.txt";
        public qfmain._连接状态_ _连接状态 = qfmain._连接状态_.未连接;

        /// <summary>
        /// plc对象
        /// </summary>
        public ModbusTcpNet _ModbusTcpClient = new ModbusTcpNet();
        public _cfg_ _参数 = new _cfg_();



        /// <summary>
        /// path_:参数保存路径
        /// </summary> 
        public ModbusTcp(string path_)
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

            this._ModbusTcpClient?.ConnectClose();


            this._ModbusTcpClient = new ModbusTcpNet(cfg.IP, (int)cfg.Port, cfg.站号);
            this._ModbusTcpClient.AddressStartWithZero = cfg.首地址从0开始;
            this._ModbusTcpClient.DataFormat = cfg.字符串模式;
            // 设置数据服务
            this._ModbusTcpClient.IsStringReverse = cfg.字符串是否颠倒;

            try
            {
                OperateResult connect = this._ModbusTcpClient.ConnectServer();
                msgErr = connect.Message;
                if (!connect.IsSuccess)
                {
                    rt = false;
                }
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

                OperateResult connect = this._ModbusTcpClient.ConnectClose();
                rt = connect.IsSuccess;
                if (!rt)
                {
                    msgErr = connect.Message;
                }
            }
            catch (Exception ex)
            {
                msgErr = ex.Message;
                rt = false;
            }
            return (rt, msgErr);
        }

        public virtual DialogResult  窗体设置(string Title,bool 重连)
        {
            using (Form_ModbusTcp forms = new Form_ModbusTcp(this, Title))
            {
                DialogResult dlt = forms.ShowDialog();
                if (重连 &&  dlt == DialogResult.OK)
                {
                    连接(true);
                }
                return dlt;
            }
        }


        #region Write


       
        public virtual (bool rt, string msgErr) Write<T>(string address, T value) 
        {
            
                return new WritePlc().Write(this._ModbusTcpClient, address, value);
           
        }

        public virtual (bool rt, string msgErr) Write( string address, string value)
        {
            return new WritePlc().Write(this._ModbusTcpClient, address, value);
        }
        public virtual (bool rt, string msgErr) Write( Encoding encoding, string address, string value)
        {
            return new WritePlc().Write(this._ModbusTcpClient, encoding, address, value);
        }


        public virtual async Task<(bool rt, string msgErr)> WriteAsync<T>( string address, T value)  
        {
            return await  new WritePlc().WriteAsync(this._ModbusTcpClient, address, value);
        }
        public virtual async Task<(bool rt, string msgErr)> WriteAsync( string address, string value)
        {
            return await  new WritePlc().WriteAsync(this._ModbusTcpClient, address, value);
        }
        public virtual async Task<(bool rt, string msgErr)> WriteAsync( Encoding encoding, string address, string value)
        {
            return await  new WritePlc().WriteAsync(this._ModbusTcpClient, encoding, address, value);
        }




        #endregion


        #region Read

        public virtual (bool rt, string msgErr, bool  value) ReadDiscrete_离散线圈(string address)
        {
            return new ReadPlc().ReadDiscrete_离散线圈(this._ModbusTcpClient, address);
        }
        public virtual (bool rt, string msgErr, bool[] value) ReadDiscrete_离散线圈(string address, ushort length)
        {
            return new ReadPlc().ReadDiscrete_离散线圈(this._ModbusTcpClient, address, length);
        }

        public virtual async Task<(bool rt, string msgErr, bool value)> ReadDiscreteAysnc_离散线圈(string address)
        {
            return await  new ReadPlc().ReadDiscreteAysnc_离散线圈(this._ModbusTcpClient, address);
        }
        public virtual async Task<(bool rt, string msgErr, bool[] value)> ReadDiscreteAysnc_离散线圈(string address, ushort length)
        {
            return await  new ReadPlc().ReadDiscreteAysnc_离散线圈(this._ModbusTcpClient, address, length);
        }

        public virtual (bool rt, string msgErr, T value) Read<T>(string address)
        {
            return new ReadPlc().Read<T>(this._ModbusTcpClient, address);
        }
        public virtual (bool rt, string msgErr, T value) Read<T>(string address, ushort length)
        {
            return new ReadPlc().Read<T>(this._ModbusTcpClient, address, length);
        }
        public virtual (bool rt, string msgErr, bool[]  value) ReadCoil (string address, ushort length)
        { 
            return new ReadPlc().ReadCoil (this._ModbusTcpClient, address, length);
        }
        public virtual (bool rt, string msgErr, bool value) ReadCoil(string address)
        {
            return new ReadPlc().ReadCoil(this._ModbusTcpClient, address);
        }


        public virtual (bool rt, string msgErr, string value) Read(string address, ushort length, Encoding encoding)
        {
            return new ReadPlc().Read(this._ModbusTcpClient, address, length, encoding);
        }
        public virtual (bool rt, string msgErr, string value) Read(string address, ushort length)
        {
            return new ReadPlc().Read(this._ModbusTcpClient, address, length);
        }


        public virtual async Task<(bool rt, string msgErr, T value)> ReadAsync<T>(string address)
        {
            return await new ReadPlc().ReadAsync<T>(this._ModbusTcpClient, address);
        }
        public virtual async Task<(bool rt, string msgErr, T value)> ReadAsync<T>(string address, ushort length)
        {
            return await new ReadPlc().ReadAsync<T>(this._ModbusTcpClient, address, length);
        }
        public virtual async Task<(bool rt, string msgErr, bool  value)> ReadCoilAsync(string address)
        {
            return await new ReadPlc().ReadCoilAsync (this._ModbusTcpClient, address);
        }
        public virtual async Task<(bool rt, string msgErr, bool[] value)> ReadCoilAsync(string address,ushort length)
        {
            return await new ReadPlc().ReadCoilAsync (this._ModbusTcpClient, address, length);
        }



        public virtual async Task<(bool rt, string msgErr, string value)> ReadAsync(string address, ushort length, Encoding encoding)
        {
            return await new ReadPlc().ReadAsync(this._ModbusTcpClient, address, length, encoding);
        }
        public virtual async Task<(bool rt, string msgErr, string value)> ReadAsync(string address, ushort length )
        {
            return await new ReadPlc().ReadAsync(this._ModbusTcpClient, address, length );
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



    }
}
