using HslCommunication.Profinet.Melsec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfPLC
{
    internal class 三菱_FX
    {
        /// <summary>
        /// 参数保存路径
        /// </summary>
        private string _path = Environment.CurrentDirectory + "\\Fx.txt";
        public _连接状态_ _连接状态 = _连接状态_.未连接;

       /// <summary>
       /// 
       /// </summary>
        public  MelsecFxSerial melsecSerial = new MelsecFxSerial();


        /// <summary>
        /// path_:参数保存路径
        /// </summary> 
        public 三菱_FX(string path_)
        {
            this._path = path_;

        }





        #region 事件

        public event Action<_连接状态_> Event_连接状态;
        private void On_连接状态(_连接状态_ state)
        {
            this._连接状态 = state;
        }



        #endregion


    }
}
