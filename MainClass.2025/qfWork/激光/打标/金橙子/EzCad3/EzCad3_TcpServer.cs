
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace qfWork
{
    public class EzCad3_TcpServer : Language_
    {
        public enum _Err_
        {
            ok成功,
            ng失败,
            EzCad3未响应,
            打标或红光中,
            闲置中,
            打开ezd3失败,
            找不到指定对象,
            找不到可以加工的对象,
            打标卡未连接上,


        }


        public class _cfg_
        {
            public int 通讯超时 { set; get; } = 5000;

            /// <summary>
            /// Ezcad3.exe
            /// </summary>
            public string Ezcad3_Path { set; get; } = Environment.CurrentDirectory + "\\Ezd3\\ Ezcad3.exe";

        }



        public EzCad3_TcpServer(string Path_tcpCfg_ = "")
        {
            if (!string.IsNullOrEmpty(Path_tcpCfg_))
            {
                this._Path_tcpCfg = Path_tcpCfg_;
            }


        }



        public qfmain.Socket_Server tcpServer_Sys;
        string _Path_tcpCfg = qfmain.软件类.Files_Cfg.Files_Config + "\\Ezd3tcp.txt";
        public qfmain ._连接状态_ _连接EzCa3状态 { set; get; } = qfmain ._连接状态_.未连接;

        /// <summary>
        /// 加工中(红光或出激光标刻中) / 闲置
        /// </summary>
        public _激光加工状态_ _加工状态 { set; get; } = _激光加工状态_.闲置;
        public _cfg_ _参数 = new _cfg_();
        qfmain._通讯中状态_ _通讯中 { set; get; } = qfmain._通讯中状态_.闲置;
        qfmain._通讯过程_ _通讯辅助 { set; get; } = qfmain._通讯过程_.闲置;

        private string _接收数据 { set; get; } = string.Empty;

        public void 初始化()
        {
            Event_连接状态?.Invoke(qfmain ._连接状态_.未连接);
            tcpServer_Sys = new qfmain.Socket_Server(this._Path_tcpCfg, new qfmain._解码_Cfg_(new byte[0], new byte[0], 200));
            tcpServer_Sys.Event_客户端上线 += 客户端上线;
            tcpServer_Sys.Event_客户端下线 += 客户端下线;
            tcpServer_Sys.Event_接收数据_jm += 接收数据;
            tcpServer_Sys.Event_侦听启动状态 += 侦听状态;

            tcpServer_Sys._参数.IP = "127.0.0.1";
            tcpServer_Sys._参数.Port = 2000;
            tcpServer_Sys.StartListen(out string msgerr);

            _IsInistiall = true;
        }

        bool _IsInistiall = false;
        public void 释放()
        {
            if (!_IsInistiall)
            {
                return;
            }
            tcpServer_Sys.StopListen(out string msgErr);
            tcpServer_Sys.Event_客户端上线 -= 客户端上线;
            tcpServer_Sys.Event_客户端下线 -= 客户端下线;
            tcpServer_Sys.Event_接收数据_jm -= 接收数据;
            tcpServer_Sys.Event_侦听启动状态 -= 侦听状态;
        }



        #region 事件响应


        void 接收数据(Socket socket_, byte[] data)
        {
            tcpServer_Sys.取sokcket_ID_ip_Port(socket_, out string id, out string ip, out int port);

            qfmain.文本 textSys = new qfmain.文本();
            string datas = Encoding.Default.GetString(data).Trim();
            datas = textSys.替换(datas, "\r\n", "");

            if (datas.Contains("Marking start"))
            {
                this._加工状态 = _激光加工状态_.加工中;
                this.Event_加工状态?.Invoke(this._加工状态);
                return;
            }
            else if (datas.Contains("Marking finish"))
            {
                this._加工状态 = _激光加工状态_.闲置;
                this.Event_加工状态?.Invoke(this._加工状态);
                return;
            }


            this._接收数据 = datas;
            if (this._通讯辅助 == qfmain._通讯过程_.等待反馈中)
            {
                this._delay_通讯_sys.中断延时(); ;
                this._通讯辅助 = qfmain._通讯过程_.已反馈;
            }


        }


        void 客户端上线(Socket socket_)
        {
            Event_连接状态?.Invoke(qfmain ._连接状态_.已连接);
        }

        void 客户端下线(Socket socket_)
        {
            Event_连接状态?.Invoke(qfmain ._连接状态_.未连接);
        }

        void 侦听状态(qfmain._启动状态_ status)
        {
            Event_Server启动状态?.Invoke(status);
        }




        #endregion



        #region 事件

        public event Action<qfmain._启动状态_> Event_Server启动状态;

        public event Action<qfmain ._连接状态_> Event_连接状态;

        /// <summary>
        /// 加工中(红光或出激光标刻中)
        /// </summary>

        public event Action<_激光加工状态_> Event_加工状态;



        public event Action<bool, string> Event_Log;
        private void On_Log(bool state, string msg)
        {
            Event_Log?.Invoke(state, msg);
        }


        #endregion




        #region 本地方法

        public bool 打开EzCad3软件(out string msgErr)
        {
            bool rt = true;
            msgErr = string.Empty;

            if (EzCad3进程是否存在())
            {
                msgErr = $"E{Get语言("激光软件已打开")}";
                return false;
            }

            try
            {
                //string exe名称 = "Ezcad3.exe";   
                rt = new qfmain.文件_文件夹().文件_打开(this._参数.Ezcad3_Path, out msgErr, "", ProcessWindowStyle.Maximized);

            }
            catch (Exception ex)
            {
                msgErr = ex.Message;
                rt = false;
            }
            msgErr = rt ? "OK" : $"NG,{msgErr}";
            return rt;
        }

        public bool EzCad3进程是否存在()
        {
            bool jc主体 = new qfmain.进程().进程是否存在("EzCad3");
            bool jc警告 = new qfmain.进程().进程是否存在("EzCad");

            if (jc主体 || jc警告)
            {
                return true;
            }
            return false;
        }

        #endregion









        qfmain.延时_Task _delay_通讯_sys = new qfmain.延时_Task();
        qfmain.延时_Task _delay_等待加工完成_sys = new qfmain.延时_Task();

        Socket 获取最后一个客户端ID()
        {
            return this.tcpServer_Sys._lstSocket[this.tcpServer_Sys._lstSocket.Count - 1];
        }

        async Task<_Err_> 发送指令(string value)
        {
            if (_连接EzCa3状态 != 0)
            {
                return _Err_.打标卡未连接上;
            }

            _通讯中 = qfmain._通讯中状态_.通讯中;
            _接收数据 = string.Empty;
            _通讯辅助 = qfmain._通讯过程_.等待反馈中;

            tcpServer_Sys.Send发送(获取最后一个客户端ID(), value + "\r\n");
            await this._delay_通讯_sys.延时(this._参数.通讯超时);

            _Err_ err = _Err_.ng失败;
            if (this._通讯辅助 == qfmain._通讯过程_.等待反馈中)
            {
                err = _Err_.EzCad3未响应;
            }
            else if (this._接收数据 == "OK")
            {
                err = _Err_.ok成功;
            }
            else if (this._接收数据.Contains("OK:MARKING"))
            {
                err = _Err_.打标或红光中;
            }
            else if (this._接收数据.Contains("OK:READY"))
            {
                err = _Err_.闲置中;
            }
            else if (this._接收数据.Contains("FAIL:Error state"))
            {
                err = _Err_.打标或红光中;
            }
            else if (this._接收数据.Contains("FAIL:file can not open!"))
            {
                err = _Err_.打开ezd3失败;
            }
            else if (this._接收数据.Contains("FAIL:Can not find entity!"))
            {
                err = _Err_.找不到指定对象;
            }
            else if (this._接收数据.Contains("FAIL: No avaliable marking entity!"))
            {
                err = _Err_.找不到可以加工的对象;
            }
            else
            {
                err = _Err_.ng失败;
            }

            _通讯中 = qfmain._通讯中状态_.闲置;
            return err;
        }


        bool _IsRun = true;
        async Task 等待标刻或红光完成()
        {

            _IsRun = true;
            while (_IsRun)
            {
                bool t = await _delay_等待加工完成_sys.延时(1000);
                if (this._加工状态 == _激光加工状态_.闲置 || !_IsRun)
                {
                    break;
                }
            }
            this._加工状态 = _激光加工状态_.闲置;
        }





        public async Task<_Err_> 发送指令_红光指示全部对象(float 角度, float X偏移, float Y偏移, float X旋转中心, float Y旋转中心)
        {
            this._加工状态 = _激光加工状态_.红指示光中;
            string value = "E3_StartMark Red";
            value += " A=" + 角度.ToString();
            value += " X=" + X偏移.ToString();
            value += " Y=" + Y偏移.ToString();
            value += " X2=" + X旋转中心.ToString();
            value += " Y2=" + Y旋转中心.ToString();
            _Err_ rt = await 发送指令(value);
            await 等待标刻或红光完成();
            return rt;
        }

        public async Task<_Err_> 发送指令_红光指示全部对象()
        {
            this._加工状态 = _激光加工状态_.红指示光中;
            string value = "E3_StartMark Red";
            _Err_ rt = await 发送指令(value);
            await 等待标刻或红光完成();
            return rt;
        }

        public async Task<_Err_> 发送指令_红光指示选中对象(float 角度, float X偏移, float Y偏移, float X旋转中心, float Y旋转中心)
        {
            this._加工状态 = _激光加工状态_.红指示光中;
            string value = "E3_StartMark Select Red";
            value += " A=" + 角度.ToString();
            value += " X=" + X偏移.ToString();
            value += " Y=" + Y偏移.ToString();
            value += " X2=" + X旋转中心.ToString();
            value += " Y2=" + Y旋转中心.ToString();
            _Err_ rt = await 发送指令(value);
            await 等待标刻或红光完成();
            return rt;
        }

        public async Task<_Err_> 发送指令_红光指示选中对象()
        {
            this._加工状态 = _激光加工状态_.红指示光中;
            string value = "E3_StartMark Select Red";
            _Err_ rt = await 发送指令(value);
            await 等待标刻或红光完成();
            return rt;
        }


        public async Task<_Err_> 发送指令_标刻全部对象(float 角度, float X偏移, float Y偏移, float X旋转中心, float Y旋转中心)
        {

            string value = "E3_StartMark";
            value += " A=" + 角度.ToString();
            value += " X=" + X偏移.ToString();
            value += " Y=" + Y偏移.ToString();
            value += " X2=" + X旋转中心.ToString();
            value += " Y2=" + Y旋转中心.ToString();
            _Err_ rt = await 发送指令(value);
            await 等待标刻或红光完成();

            return rt;
        }


        public async Task<_Err_> 发送指令_标刻全部对象()
        {
            string value = "E3_StartMark";
            _Err_ rt = await 发送指令(value);
         await    等待标刻或红光完成();

            return rt;
        }


        public async Task<_Err_> 发送指令_标刻选中对象(float 角度, float X偏移, float Y偏移, float X旋转中心, float Y旋转中心)
        {

            string value = "E3_StartMark Select ";
            value += " A=" + 角度.ToString();
            value += " X=" + X偏移.ToString();
            value += " Y=" + Y偏移.ToString();
            value += " X2=" + X旋转中心.ToString();
            value += " Y2=" + Y旋转中心.ToString();
            _Err_ rt = await 发送指令(value);
         await    等待标刻或红光完成();

            return rt;
        }


        public async Task<_Err_> 发送指令_标刻选中对象()
        {

            string value = "E3_StartMark Select ";
            _Err_ rt = await  发送指令(value);
           await  等待标刻或红光完成();

            return rt;
        }


        public async Task<_Err_> 发送指令_获取当前系统状态()
        {
            string value = "E3_GetState";
            return await  发送指令(value);
        }



        public async Task<_Err_> 发送指令_停止红光或标刻()
        {
            if (this._加工状态 != _激光加工状态_.闲置)
            {
                _IsRun = false;
            }
            string value = "E3_MarkerStop";
            return await  发送指令(value);
        }

        public async Task<_Err_> 发送指令_加载ezd3(string ezd3Path)
        {
            string value = "E3_LoadFile " + ezd3Path;
            return await  发送指令(value);
        }


        /// <summary>
        /// 对象名称:空表示选择全部对象
        /// </summary>
        /// <param name="对象名称"></param>
        /// <returns></returns>
        public async Task<_Err_> 发送指令_选择对象(string 对象名称)
        {
            string value = "E3_SelectEnt ALL";
            if (!string.IsNullOrEmpty(对象名称))
            {
                value = "E3_SelectEnt " + 对象名称;
            }
            return await  发送指令(value);
        }

        /// <summary>
        /// 对象名称:空表示取消选择全部对象
        /// </summary>
        /// <param name="对象名称"></param>
        /// <returns></returns>
        public async Task<_Err_> 发送指令_取消选择对象(string 对象名称)
        {
            string value = "E3_DisSelEnt ALL";
            if (!string.IsNullOrEmpty(对象名称))
            {
                value = "E3_DisSelEnt " + 对象名称;
            }
            return await  发送指令(value);
        }

        public async Task<_Err_> 发送指令_修改指定对象的内容(string 对象名称, string 内容)
        {
            string value = "E3_ChangeText " + 对象名称 + " " + 内容;
            return await  发送指令(value);
        }


        /// <summary>
        /// model: 0:最小化,1:最大化,2:隐蒧
        /// </summary>
        public async Task<_Err_> 指令_窗体操作(short model)
        {
            // E3_Show(Min)(Max) （Hide）
            string value = " E3_Show Min";
            if (model == 1)
            {
                value = " E3_Show Max";
            }
            else if (model == 2)
            {
                value = " E3_Show Hide";
            }
            return await  发送指令(value);
        }



    }
}
