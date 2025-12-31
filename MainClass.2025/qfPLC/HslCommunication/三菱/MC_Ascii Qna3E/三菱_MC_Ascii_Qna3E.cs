using HslCommunication;
using HslCommunication.Profinet.Melsec;
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


    /// <summary>
    /// MC Qna3E ASCII
    /// </summary>
    public class 三菱_MC_Ascii_Qna3E : IWorker
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

        private readonly object _lock=new object();

        /// <summary>
        /// 0:写,1:读
        /// </summary>   
        public void 读写参数(ushort model)
        {
            lock (_lock)
            {
                _Cfg_ info = this._参数;
                new qfmain.文件_文件夹().WriteReadJson(this._path, model, ref info, out string msgErr);
                this._参数 = info;
            }
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
                msgErr = qfmain .Language_ .Get语言 ("IP错误");
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

        public virtual (bool rt, string msgErr) 断开()
        {
            On_连接状态(qfmain._连接状态_.未连接);
            bool rt = true;
            string msgErr = string.Empty;
            try
            {
                OperateResult connect = this._MelsecMcAsciiNet.ConnectClose();
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
         

        public DialogResult   窗体设置(string Title, bool 重连)
        {
            return DialogResult.None;
        }



        /// <summary>
        ///   true成功,false失败
        /// </summary>
        /// <param name="address_开始"></param>
        /// <param name="length_address长度"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool 读取型号(out string 型号, out string MsgErr)
        {
            型号 = string.Empty;


            try
            {


                // 读取型号
                OperateResult<string> readResult = this._MelsecMcAsciiNet.ReadPlcType();
                if (readResult.IsSuccess)
                {
                    MsgErr = "Type:" + readResult.Content;
                    型号 = readResult.Content;
                    return true;
                }
                else
                {
                    MsgErr = "Failed: " + readResult.ToMessageShowString();

                    return false;
                }
            }
            catch (Exception ex)
            {
                MsgErr = ex.Message;
                return false;
            }



        }


        /// <summary>
        ///   true成功,false失败
        /// </summary>
        /// <param name="address_开始"></param>
        /// <param name="length_address长度"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool 远程启动(out string MsgErr)
        {
            try
            {


                // 远程启动
                OperateResult runResult = this._MelsecMcAsciiNet.RemoteRun();
                if (runResult.IsSuccess)
                {
                    MsgErr = "Run Success";
                    return true;
                }
                else
                {
                    MsgErr = "Failed: " + runResult.ToMessageShowString();
                    return false;
                }
            }
            catch (Exception ex)
            {
                MsgErr = ex.Message;
                return false;
            }
        }


        /// <summary>
        ///   true成功,false失败
        /// </summary>
        /// <param name="address_开始"></param>
        /// <param name="length_address长度"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool 远程停止(out string MsgErr)
        {
            try
            {
                // 远程停止
                OperateResult runResult = this._MelsecMcAsciiNet.RemoteStop();
                if (runResult.IsSuccess)
                {
                    MsgErr = "Stop Success";
                    return true;
                }
                else
                {
                    MsgErr = "Failed: " + runResult.ToMessageShowString();
                    return false;
                }
            }
            catch (Exception ex)
            {
                MsgErr = ex.Message;
                return false;
            }
        }


        #region Write



        public virtual (bool rt, string msgErr) Write<T>(string address, T value)  
        {
            return new WritePlc().Write(this._MelsecMcAsciiNet , address, value);
        }

        public virtual (bool rt, string msgErr) Write(string address, string value)
        {
            return new WritePlc().Write(this._MelsecMcAsciiNet, address, value);
        }
        public virtual (bool rt, string msgErr) Write(Encoding encoding, string address, string value)
        {
            return new WritePlc().Write(this._MelsecMcAsciiNet, encoding, address, value);
        }


        public virtual async Task<(bool rt, string msgErr)> WriteAsync<T>(string address, T value)  
        {
            return await new WritePlc().WriteAsync(this._MelsecMcAsciiNet, address, value);
        }
        public virtual async Task<(bool rt, string msgErr)> WriteAsync(string address, string value)
        {
            return await new WritePlc().WriteAsync(this._MelsecMcAsciiNet, address, value);
        }
        public virtual async Task<(bool rt, string msgErr)> WriteAsync(Encoding encoding, string address, string value)
        {
            return await new WritePlc().WriteAsync(this._MelsecMcAsciiNet, encoding, address, value);
        }




        #endregion


        #region Read

        public virtual (bool rt, string msgErr, T value) Read<T>(string address)
        {
            return new ReadPlc().Read<T>(this._MelsecMcAsciiNet, address);
        }
        public virtual (bool rt, string msgErr, T value) Read<T>(string address, ushort length)
        {
            return new ReadPlc().Read<T>(this._MelsecMcAsciiNet, address, length);
        }
        public virtual (bool rt, string msgErr, string value) Read(string address, ushort length, Encoding encoding)
        {
            return new ReadPlc().Read(this._MelsecMcAsciiNet, address, length, encoding);
        }

        public virtual (bool rt, string msgErr, string value) Read(string address, ushort length )
        {
            return new ReadPlc().Read(this._MelsecMcAsciiNet, address, length );
        }


        public virtual async Task<(bool rt, string msgErr, T value)> ReadAsync<T>(string address)
        {
            return await new ReadPlc().ReadAsync<T>(this._MelsecMcAsciiNet, address);
        }
        public virtual async Task<(bool rt, string msgErr, T value)> ReadAsync<T>(string address, ushort length)
        {
            return await new ReadPlc().ReadAsync<T>(this._MelsecMcAsciiNet, address, length);
        }
        public virtual async Task<(bool rt, string msgErr, string value)> ReadAsync(string address, ushort length, Encoding encoding)
        {
            return await new ReadPlc().ReadAsync(this._MelsecMcAsciiNet, address, length, encoding);
        }
        public virtual async Task<(bool rt, string msgErr, string value)> ReadAsync(string address, ushort length )
        {
            return await new ReadPlc().ReadAsync(this._MelsecMcAsciiNet, address, length );
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
