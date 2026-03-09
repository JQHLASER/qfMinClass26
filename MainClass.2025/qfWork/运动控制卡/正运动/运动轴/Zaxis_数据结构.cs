using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfWork
{
    public class Zaxis_数据结构
    {
        #region Enum枚举

        public enum _em_轴停止模式_
        {
            取消当前运动,
            取消缓冲的运动,
            取消当前运动和缓冲运动,
            立即中断脉冲发送,
        }

        public enum _em_轴IO_
        {
            原点,
            正限位,
            负限位,
            急停,
            伺服报警,
            伺服准备好,
        }


        public enum _em_轴使能_
        {
            不使能 = 0,
            使能 = 1,
        }

        public enum _em_轴方向_
        {
            正向 = 1,
            反向 = 3,
        }

        public enum _em_轴类型_
        {
            脉冲加方向 = 1,
        }

        public enum _em_轴回零方向_
        {
            正向 = 3,
            反向 = 4,
        }

        /// <summary>
        /// 常开(反转)/常闭(不反转)
        /// </summary>
        public enum _em_信号极性_
        {    /// <summary>
             /// 不反转
             /// </summary>
            常闭 = 0,

            /// <summary>
            /// 反转
            /// </summary>
            常开 = 1,

        }

        public enum _em_回零状态_
        {
            已回零 = 0,
            未回零 = 1,
        }

        public enum _em_IO信号状态_
        {
            /// <summary>
            /// 不使能
            /// </summary>
            None,
            On,
            Off,
        }


        public enum _em_轴运动状态_
        {
            停止,
            运动中,
        }

        public enum _em_操作指令_
        {
            停止运动 = 0,
            回零运动 = 1,
            正向运动 = 2,
            负向运动 = 3,
            急停 = 4,
        }



        #endregion

        #region 参数构造

        public class _inf_轴IO_
        {
            public int 原点 { set; get; } = -1;
            public int 正限位 { set; get; } = -1;
            public int 负限位 { set; get; } = -1;

            public int 急停 { set; get; } = -1;
            public int 伺服报警 { set; get; } = -1;

            public int 伺服准备好 { set; get; } = -1;

        }

        /// <summary>
        /// 速度,脉冲当量
        /// </summary>
        public class _inf_速度及脉冲参数_
        {

            public float 脉冲当量 { set; get; } = 10000;
            public float 加速度 { set; get; } = 10;
            public float 减速度 { set; get; } = 10;
            public float 轴异常快减速度 { set; get; } = 100000;
            public float 最大速度 { set; get; } = 10;

            public float 起始速度 { set; get; } = 10;
            public float 工作速度 { set; get; } = 10;

            public float 手动高速度 { set; get; } = 10;
            public float 手动低速度 { set; get; } = 1;

        }



        public class _inf_轴参数_
        {
            public string 轴名称 { set; get; } = "";

            public int 轴号 { set; get; } = 0;

            public _em_轴类型_ 轴类型 { set; get; } = _em_轴类型_.脉冲加方向;
            public _em_轴使能_ 轴使能 { set; get; } = _em_轴使能_.不使能;
            public _em_轴方向_ 轴方向 { set; get; } = _em_轴方向_.正向;
            public _inf_速度及脉冲参数_ 轴参数_速度及脉冲 { set; get; } = new _inf_速度及脉冲参数_();

            public _inf_轴IO_ IO { set; get; } = new _inf_轴IO_();

            public _inf_轴参数_ Clone()
            {
                return new _inf_轴参数_
                {
                    轴名称 = this.轴名称,
                    轴类型 = this.轴类型,
                    轴使能 = this.轴使能,
                    轴方向 = this.轴方向,
                    轴参数_速度及脉冲 = this.轴参数_速度及脉冲,
                    IO = this.IO,
                };
            }
        }

        public class _inf_轴指令_
        {

            public int 轴号 { set; get; } = 0;
            public float 轴速度 { set; get; } = 0f;
            public _em_操作指令_ 指令 { set; get; } = _em_操作指令_.停止运动;

        }


        public class _inf_轴状态_
        {
            public float 轴坐标 { set; get; } = 0f;
            public float 轴速度 { set; get; } = 0f;
            public _em_轴运动状态_ 轴运动状态 { set; get; } = _em_轴运动状态_.停止;
            public _em_IO信号状态_ 原点 { set; get; } = _em_IO信号状态_.None;
            public _em_IO信号状态_ 正限位 { set; get; } = _em_IO信号状态_.None;
            public _em_IO信号状态_ 负限位 { set; get; } = _em_IO信号状态_.None;
            public _em_IO信号状态_ 急停 { set; get; } = _em_IO信号状态_.None;
            public _em_IO信号状态_ 伺服报警 { set; get; } = _em_IO信号状态_.None;
            public _em_IO信号状态_ 伺服准备好 { set; get; } = _em_IO信号状态_.None;

        }



        #endregion

    }
}
