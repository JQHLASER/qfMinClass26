using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace qfPLC
{
    /// <summary>
    /// 工厂模式,统一接口
    /// </summary>
    public class PLC_Hsl
    {

        _PLC_Type_ _PLC类型 = _PLC_Type_.MC_ASCII;

        string _path = "";
        /// <summary>
        /// PLC库
        /// </summary>
        public IWorker _PLC库;


        /// <summary>
        /// <para>Path_ : 存放信息文件路径</para>
        /// </summary> 
        public PLC_Hsl(_PLC_Type_ PLC类型_, string Path_)
        {
            this._path = Path_;
            this._PLC类型 = PLC类型_;
            this._PLC库 = 获取PLC库();

            this._PLC库.Event_连接状态 += (s) => On_连接状态(s);
            this._PLC库.On_连接状态(qfmain._连接状态_.未连接);
        }

        private IWorker 获取PLC库()
        {
            switch (this._PLC类型)
            {
                case _PLC_Type_.MC_ASCII:
                    return new 三菱_MC_Ascii_Qna3E(this._path);
                case _PLC_Type_.FX_Serial_:
                    return new 三菱_FX(this._path);








                default:
                    return new 三菱_MC_Ascii_Qna3E(this._path);

            }
        }

        public (bool rt, string msgErr) 连接(bool 是否先读参数 = true)
        {
            if (!Err_HSL未激活(out string msgErr, false))
            {
                this.获取PLC库().On_连接状态(qfmain._连接状态_.未连接);
                return (false, msgErr);
            }
            return this.获取PLC库().连接(是否先读参数);
        }

        public (bool rt, string msgErr) 断开()
        {
            return this.获取PLC库().断开();
        }

        void On_连接状态(qfmain._连接状态_ state)
        {
            Event_连接状态?.Invoke(state);
        }


        #region 事件

        public event Action<qfmain._连接状态_> Event_连接状态;
        public event Action<bool, string> Event_Log;
        void On_Log(bool state, string msg)
        {
            Event_Log?.Invoke(state, msg);
        }


        #endregion

        #region Err

        public virtual bool Err_HSL未激活(out string msgErr, bool 显示日志 = false)
        {
            bool rt = true;
            msgErr = "";
            if (!HslCommunication_._Is激活状态)
            {
                rt = false;
                msgErr = qfmain.Language_.Get语言("HSL未激活");
                if (显示日志)
                {
                    On_Log(rt, msgErr);
                }
            }
            return rt;
        }

        public virtual bool Err_PLC未连接(string Name, out string msgErr, bool 显示日志 = false)
        {
            bool rt = true;
            msgErr = "";
            if (this._PLC库.Get连接状态() != qfmain._连接状态_.已连接)
            {
                rt = false;
                msgErr = $"{Name}{qfmain.Language_.Get语言("未连接")}";
                if (显示日志)
                {
                    On_Log(rt, msgErr);
                }
            }
            return rt;
        }

        #endregion
    }
}
