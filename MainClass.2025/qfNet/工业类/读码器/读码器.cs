
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace qfNet
{
    public class 读码器 : qfWork.读码器
    {

        internal string _自定义等级密码 = "QF8888";
        public 读码器(string 读码器名称 = "读码器", string 文件夹名 = "ReadCode", string 自定义等级密码 = "QF8888")
            : base(读码器名称, 文件夹名)
        {
            this._自定义等级密码 = 自定义等级密码;
        }

        public override async Task 初始化()
        {
            if (!this._功能.使能)
            {
                return;
            } 

            base.Event_读码器连接状态 += On_连接状态;
            base.Event_读码状态 += On_读码状态;
            await base.初始化(); 
            IsInistiall = true;
        }

        bool IsInistiall = false;


        public override void 释放()
        {
            if (!IsInistiall || !this._功能.使能)
            {
                return;
            }
            base.Event_读码器连接状态 -= On_连接状态;
            base.Event_读码状态 -= On_读码状态;
            base.释放();
        }



        public async Task Win_设置()
        {
            using (Form_读码器 forms = new Form_读码器(this))
            {
                forms.ShowDialog();             
                if (!_参数.使能_读码器)
                {
                    断开读码器();
                }
                else
                {
                    await 连接读码器();
                }
            }
        }




        #region 事件响应


        public void On_连接状态(qfmain._连接状态_ state)
        {           
            On_标题栏状态(标题栏状态信息_连接状态(), (int)state);
        }

        void On_读码状态(_读码状态_ state)
        {
            On_标题栏状态(标题栏状态信息_读码状态(), (int)state);
        }

        #endregion







        _cfg_标题栏状态_[] 标题栏状态信息_连接状态()
        {
            return new _cfg_标题栏状态_[]
                  {
                     new _cfg_标题栏状态_(this._读码器名称 ,$"{this._读码器名称}{Language_ .Get语言 ("未连接")}",(int)qfmain . _连接状态_ .未连接 ),
                     new _cfg_标题栏状态_(this._读码器名称  ,$"{this._读码器名称}{Language_ .Get语言 ("连接中")}",(int)qfmain . _连接状态_ .连接中),
                     new _cfg_标题栏状态_(this._读码器名称 ,$"{this._读码器名称}{Language_ .Get语言 ("已连接")}" ,(int)qfmain . _连接状态_ .已连接 ),
                  };
        }

        _cfg_标题栏状态_[] 标题栏状态信息_读码状态()
        {
            return new _cfg_标题栏状态_[]
                  {
                     new _cfg_标题栏状态_(this._读码器名称  ,$"",(int) _读码状态_  .None ),
                     new _cfg_标题栏状态_(this._读码器名称 ,$"{this._读码器名称}{Language_ .Get语言 ("读码中")}" ,(int) _读码状态_ .读码中 ),
                  };
        }



        /// <summary>
        /// 参数(状态信息,状态)
        /// </summary>
        public event Action<_cfg_标题栏状态_[], int> Event_标题栏状态;
        void On_标题栏状态(_cfg_标题栏状态_[] info, int state)
        {
            Event_标题栏状态?.Invoke(info, state);
        }

    }
}