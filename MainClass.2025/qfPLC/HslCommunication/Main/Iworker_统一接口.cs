using HslCommunication;
using HslCommunication.ModBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace qfPLC
{
    /// <summary>
    ///工厂
    /// </summary>
    public interface IWorker
    {
        /// <summary>
        /// 存放连接参数的路径
        /// </summary>
        string _path { set; get; }
        qfmain._连接状态_ _连接状态 { set; get; }



        qfmain._连接状态_ Get连接状态();

        (bool rt, string msgErr) 连接(bool 是否先读参数 = true);
        (bool rt, string msgErr) 断开();
        void 读写参数(ushort model);

        DialogResult 窗体设置(string Title, bool 重连);

        #region Write

        (bool rt, string msgErr) Write(string address, object value, Encoding encoding = null);

        Task<(bool rt, string msgErr)> WriteAsync(string address, object value, Encoding encoding = null);

        #endregion

        #region Read

        (bool state, string msg, T v) Read<T>( _ReadType_ Read_Type, string address, ushort length=1, Encoding encoding=null );
        Task<(bool state, string msg, T v)> ReadAsync<T>( _ReadTypeAsync_ Read_Type, string address, ushort length=1, Encoding encoding = null);

        #endregion



    }
}
