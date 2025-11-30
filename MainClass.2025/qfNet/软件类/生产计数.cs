using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfNet
{
    public class 生产计数 : qfmain.生产计数
    {
        /// <summary>
        /// 文件名称
        /// </summary>
        /// <param name="文件名称"></param>
        public 生产计数(string 文件名称 = "js") : base(文件名称)
        {

        }

        public override void 读写信息(ushort model)
        {
            base.读写信息(model);
            状态栏显示();
        }

        public override void 计数递增_良品(int 递增量 = 1)
        {
            base.计数递增_良品(递增量);
            状态栏显示();
        }

        public override void 计数递增_不良品(int 递增量 = 1)
        {
            base.计数递增_不良品(递增量);
            状态栏显示();
        }


        public override  void 设置(long 零件, long 良品, long 不良品)
        {
            base.设置(零件,  良品,  不良品);
            状态栏显示();

        }



        /// <summary>
        /// 控件显示,需要放在窗体进入后事件
        /// </summary>
        void 状态栏显示()
        {
            string xt = "";
            if (_使能_零件)
            {
                xt = $"{Language_.Get语言("零件")} : {this._当前计数信息.零件}";
                Event_状态栏信息("生产计数零件", xt.Trim());
            }

            if (_使能_良品计数)
            {
                xt = $"{Language_.Get语言("良品")} : {_当前计数信息.良品}";
                Event_状态栏信息("生产计数良品", xt.Trim());
            }

            if (_使能_不良品计数)
            {
                xt = $"{Language_.Get语言("不良品")} : {_当前计数信息.不良品}";
                Event_状态栏信息("生产计数不良品", xt.Trim());
            }

        }

        /// <summary>
        /// 参数(状态名称,显示信息)
        /// </summary>
        public event Action<string, string> Event_状态栏信息;
        void On_状态栏信息(string 状态名称, string value)
        {
            Event_状态栏信息?.Invoke(状态名称, value);
        }



    }
}
