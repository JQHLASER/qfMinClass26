using HslCommunication;
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

        #region Write

        (bool rt, string msgerr) Write(string address, dynamic value);
        Task<(bool rt, string msgerr)> WriteAsync(string address, dynamic value);


        #endregion



        #region Read

        (bool rt, string msgErr, T value) Read<T>(string address);

        (bool rt, string msgErr, T value) Read<T>(string address, ushort length);

        (bool rt, string msgErr, string value) Read(string address, ushort length, Encoding encoding);


        Task<(bool rt, string msgErr, T value)> ReadAsync<T>(string address);

        Task<(bool rt, string msgErr, T value)> ReadAsync<T>(string address, ushort length);

        Task<(bool rt, string msgErr, string value)> ReadAsync(string address, ushort length, Encoding encoding);



        #endregion

        #region 事件

        event Action<qfmain._连接状态_> Event_连接状态;
        void On_连接状态(qfmain._连接状态_ state);

        #endregion

    }
}
