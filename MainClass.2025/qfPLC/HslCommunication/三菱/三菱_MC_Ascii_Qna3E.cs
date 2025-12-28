using HslCommunication;
using HslCommunication.Profinet.Melsec;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace qfPLC
{
    /// <summary>
    /// MC Qna3E ASCII
    /// </summary>
    public class 三菱_MC_Ascii_Qna3E
    {
        public class _Cfg_
        {
            public string Ip { set; get; } = "127.0.0.1";
            public int Port { set; get; } = 6000;

            /// <summary>
            /// ms
            /// </summary>
            public int 连接超时 { set; get; } = 3000;     // 连接超时，单位毫秒

            /// <summary>
            /// ms
            /// </summary>
            public int 接收超时 { set; get; } = 5000;     // 接收超时，单位毫秒
            public byte NetworkNumber { set; get; } = 0;
            public byte NetworkStationNumber { set; get; } = 0;
            public bool EnableWriteBitToWordRegister { set; get; } = false;
            public bool IsStringReverseByteWord { set; get; } = false;

        }


        /// <summary>
        /// 参数保存路径
        /// </summary>
        private string _path = Environment.CurrentDirectory + "\\McAscii.txt";
        public qfmain._连接状态_ _连接状态 = qfmain._连接状态_.未连接;

        public _Cfg_ _参数 = new _Cfg_();

        /// <summary>
        /// plc对象
        /// </summary>
        private MelsecMcAsciiNet _MelsecMcAsciiNet = new MelsecMcAsciiNet();


        /// <summary>
        /// path_:参数保存路径
        /// </summary> 
        public 三菱_MC_Ascii_Qna3E(string path_)
        {
            this._path = path_;

        }

        /// <summary>
        /// 0:写,1:读
        /// </summary>   
        public void 读写参数(ushort model)
        {
            _Cfg_ info = this._参数;
            new qfmain.文件_文件夹().WriteReadJson(this._path, model, ref info, out string msgErr);
            this._参数 = info;
        }


        public (bool rt, string msgErr) 连接(bool 是否先读参数 = true)
        {
            读写参数(1);
            return 连接(this._参数);
        }

        public (bool rt, string msgErr) 连接(_Cfg_ cfg)
        {
            bool rt = true;
            string msgErr = string.Empty;
            On_连接状态(qfmain._连接状态_.连接中);
            // 连接
            if (!System.Net.IPAddress.TryParse(cfg.Ip, out System.Net.IPAddress address))
            {
                msgErr = "IP错误";
                rt = false;
                return (rt, msgErr);
            }



            try
            {

                this._MelsecMcAsciiNet?.ConnectClose();
                this._MelsecMcAsciiNet = new MelsecMcAsciiNet();
                this._MelsecMcAsciiNet.IpAddress = cfg.Ip;
                this._MelsecMcAsciiNet.Port = cfg.Port;
                this._MelsecMcAsciiNet.ConnectTimeOut = cfg.连接超时;     // 连接超时，单位毫秒
                this._MelsecMcAsciiNet.ReceiveTimeOut = cfg.接收超时;     // 接收超时，单位毫秒
                this._MelsecMcAsciiNet.NetworkNumber = cfg.NetworkNumber;
                this._MelsecMcAsciiNet.NetworkStationNumber = cfg.NetworkStationNumber;
                this._MelsecMcAsciiNet.EnableWriteBitToWordRegister = cfg.EnableWriteBitToWordRegister;
                this._MelsecMcAsciiNet.ByteTransform.IsStringReverseByteWord = cfg.IsStringReverseByteWord;

                OperateResult connect = this._MelsecMcAsciiNet.ConnectServer();
                (bool rt, string msgErr) rts = new 解析().OperateResult(connect);
                rt = rts.rt;
                msgErr = rts.msgErr;
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

        public virtual (bool rt, string msgErr) 释放()
        {
            On_连接状态(qfmain._连接状态_.未连接);
            bool rt = true;
            string msgErr = string.Empty;
            try
            {
                OperateResult connect = this._MelsecMcAsciiNet.ConnectClose();
                rt = connect.IsSuccess;
            }
            catch (Exception ex)
            {
                msgErr = ex.Message;
                rt = false;
            }
            return (rt, msgErr);
        }

         

        /// <summary>
        /// 写入 
        /// <para>类型:byte,short,ushort,int,uint,long,ulong,float,double,string,bool</para>
        /// <para>类型:byte[],ushort[],short[],int[],uint[],long[],ulong[],float[],double[],bool[]</para>
        /// </summary>
        /// <returns></returns>
        public virtual (bool rt, string msgerr) Write(string address, dynamic value)
        {
            OperateResult result = this._MelsecMcAsciiNet.Write(address, new byte[] { value });
            return new 解析().OperateResult(result);
        }

        /// <summary>
        /// 写入 
        /// <para>类型:byte,short,ushort,int,uint,long,ulong,float,double,string,bool</para>
        /// <para>类型:byte[],ushort[],short[],int[],uint[],long[],ulong[],float[],double[],bool[]</para>
        /// </summary>
        /// <returns></returns>
        public virtual async Task<(bool rt, string msgerr)> WriteAsync(string address, dynamic value)
        {
            OperateResult result = await this._MelsecMcAsciiNet.WriteAsync(address, new byte[] { value });
            return new 解析().OperateResult(result);
        }

        public virtual (bool rt, string msgErr, bool value) Read(string address)
        {
            OperateResult<bool> result = this._MelsecMcAsciiNet.ReadBool(address);
            return new 解析().OperateResult<bool>(result);
        }

        public virtual async Task<(bool rt, string msgErr, bool value)> ReadAsync(string address)
        {
            OperateResult<bool> result = await this._MelsecMcAsciiNet.ReadBoolAsync(address);
            return new 解析().OperateResult<bool>(result);
        }



        #region 事件

        public event Action<qfmain._连接状态_> Event_连接状态;
        private void On_连接状态(qfmain._连接状态_ state)
        {
            this._连接状态 = state;
            Event_连接状态?.Invoke(state);
        }

        #endregion



    }
}
