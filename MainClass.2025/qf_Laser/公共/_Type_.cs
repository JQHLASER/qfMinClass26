using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qf_Laser
{
    public enum _em_打标卡类型_
    {
        /// <summary>
        /// 不使能,无
        /// </summary>
        None,
        Ezd2,
        Ezd3,
    }








    public class _变量信息_
    {
        public _变量信息_(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }

        /// <summary>
        /// 变量名
        /// </summary>
        public string Name { set; get; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Value { set; get; }
    }
    public enum _连接状态_
    {
        连接中 = -2,
        已连接 = 0,
        未连接 = -1,
        功能码不匹配 = -3,
    }

    public enum _初始化状态_
    {
        已初始化,
        初始化中 = -2,
        未初始化 = -1,
    }

    public enum _激光加工状态_
    {
        闲置,
        红指示光中 = 1,
        出激光标刻中 = 2,
        加载激光模板中 = 3,
        加工中 = 1,
    }



    public enum _激光_获取图像_
    {
        获取,
        清除,
    }

    public enum _激光_红光指示_
    {
        外框,
        轮郭,
    }

    public class _激光IO_IN_
    {
        public ushort 启动标刻 { set; get; } = 16;
        public ushort 停止 { set; get; } = 16;
        public ushort 复位 { set; get; } = 16;

    }

    public class _激光IO_OUT_
    {
        public ushort 软件准备好 { set; get; } = 16;
        public ushort 红光 { set; get; } = 16;
        public ushort 标刻中 { set; get; } = 16;
        public ushort 标刻完成 { set; get; } = 16;
        public ushort 报警 { set; get; } = 16;
        /// <summary>
        /// ms
        /// </summary>
        public int 输出脉宽 { set; get; } = 500;
    }

    public class _激光参数_
    {
        public _激光IO_IN_ IN { set; get; } = new _激光IO_IN_();
        public _激光IO_OUT_ OUT { set; get; } = new _激光IO_OUT_();
        /// <summary>
        /// ms
        /// </summary>
        public int 线程周期 { set; get; } = 100;
        /// <summary>
        /// ms
        /// </summary>
        public int 连续加工周期 { set; get; } = 100;
        public bool 红光指示轮廓 { set; get; } = false;
        /// <summary>
        /// 初始化成功后,自动加载最后一次的激光模板
        /// </summary>
        public bool 进入时加载激光模板 { set; get; } = false;
        public bool 双击查看图像 { set; get; } = false;

        public bool 加工时使能红光 { set; get; } = false;

        public string 激光软件名称 { set; get; } = "EzCad2";

    }

    public class  _笔参数_
    {
        public int 笔号 = 0;
        public int 加工次数 = 1;
        public double 标刻速度 = 1000;
        public double 功率百分比 = 20;
        public double 电流 = 10;
        /// <summary>
        /// 单位Hz
        /// </summary>
        public int 频率 = 20;
        public double Q脉冲宽度 = 4;
        public int 开光延时 = -100;
        public int 关光延时 = 100;
        public int 结束延时 = 500;
        public int 拐角延时 = 50;
        public double 跳转速度 = 8000;
        public int 跳转位置延时 = 20;
        public int 跳转距离延时 = 10;
        public double 末点补偿 = 0;
        public double 加速距离 = 0;
        public double 打点时间 = 0;
        public bool 脉冲点模式 = false;
        public int 脉冲点数目 = 0;
        public double 流水线速度 = 0;
    }







}
