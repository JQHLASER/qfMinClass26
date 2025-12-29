using HslCommunication;
using HslCommunication.ModBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfPLC
{
    /// <summary>
    ///工厂
    /// </summary>
    public interface IWorker
    {
        qfmain._连接状态_ Get连接状态();

        (bool rt, string msgErr) 连接(bool 是否先读参数 = true);
        (bool rt, string msgErr) 断开();
        void 读写参数(ushort model);

        void 窗体设置(string Title,bool 重连);

        #region Write

        (bool rt, string msgErr) Write<T>( string address, T value) where T : struct;
        (bool rt, string msgErr) Write( string address, string value);
        (bool rt, string msgErr) Write( Encoding encoding, string address, string value);


        Task<(bool rt, string msgErr)> WriteAsync<T>( string address, T value) where T : struct;
        Task<(bool rt, string msgErr)> WriteAsync( string address, string value);
        Task<(bool rt, string msgErr)> WriteAsync( Encoding encoding, string address, string value);


        #endregion

        #region Read

        (bool rt, string msgErr, T value) Read<T>(string address);

        (bool rt, string msgErr, T value) Read<T>(string address, ushort length);

        (bool rt, string msgErr, string value) Read(string address, ushort length, Encoding encoding);
        (bool rt, string msgErr, string value) Read(string address, ushort length );

        Task<(bool rt, string msgErr, T value)> ReadAsync<T>(string address);

        Task<(bool rt, string msgErr, T value)> ReadAsync<T>(string address, ushort length);

        Task<(bool rt, string msgErr, string value)> ReadAsync(string address, ushort length, Encoding encoding);
        Task<(bool rt, string msgErr, string value)> ReadAsync(string address, ushort length );


        #endregion



    }
}
