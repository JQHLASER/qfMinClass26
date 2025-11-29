using Newtonsoft.Json;
using NPOI.POIFS.Crypt.Dsig;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace qfmain
{
    public class 软件注册_TCP通讯_服务端
    {



        static string _path = 软件类.Files_Cfg.Files_sysConfig + "\\Gtcpser.dll";
        public Socket_Server TcpServer_sys = new Socket_Server(_path,null);


        public _通讯中状态_ _通讯中状态 = _通讯中状态_.闲置;



        public 软件注册_TCP通讯_服务端(int Port = const常量._软件授权_服务器_port)
        {
            this.TcpServer_sys._参数.Port = Port;
            this.TcpServer_sys.StartListen(out string smgErr);
            this.isInistiall = true;
        }
        bool isInistiall = false;

        public void 释放()
        {
            if (!this.isInistiall)
            {
                return;
            }

            this.TcpServer_sys.StopListen(out string smgErr);
        }





        #region 事件

        #endregion



    }
}
