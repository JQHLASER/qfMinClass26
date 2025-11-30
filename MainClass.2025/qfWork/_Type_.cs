using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfWork
{

    #region 正运动


    /// <summary>
    /// 控制器的连接参数
    /// </summary>
    public class _Zaxis_连接参数_
    { 
        public string 提示 { set; get; } = "连接类型:0:网口,1:串口";
        public _连接类型_ 连接类型 { set; get; } = _连接类型_.网口;
        public _Zaxis_串口参数_ 串口 { set; get; } = new _Zaxis_串口参数_();
        public string IP { set; get; } = "127.0.0.1";
        public int 线程周期 { set; get; } = 100;
    }

    public class _Zaxis_串口参数_
    {
        public string 提示 { set; get; } = "串口号搜寻:0:自动搜寻,1:不自动搜寻";
        public uint 串口号 { set; get; } = 1;
        public _串口搜寻_ 串口号搜寻 { set; get; } = _串口搜寻_.自动搜寻;
    }


    /// <summary>
    /// 控制器参数
    /// </summary>
    public class _Zaxis_控制器参数_
    {
        /// <summary>
        /// 硬件写入的功能码
        /// </summary>
        public string 功能码 { get; set; } = string.Empty;
        public string 硬件型号 { get; set; } = string.Empty;
        public string 软件型号 { get; set; } = string.Empty;

        /// <summary>
        /// 控制器的Sn码
        /// </summary>
        public string Sn { get; set; } = string.Empty;


    }


    #endregion



    /// <summary>
    /// 使能串口时的搜寻方式
    /// </summary>
    public enum _串口搜寻_
    {
        自动搜寻 = 0,
        不自动搜寻 = 1,
    }

    /// <summary>
    /// 控制器的连接类型
    /// </summary>
    public enum _连接类型_
    {
        网口 = 0,
        串口 = 1,
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

   


    public class _激光IO_IN_
    {
        public short 启动标刻 { set; get; } = 16;
        public short 停止 { set; get; } = 16;
        public short 复位 { set; get; } = 16;

    }

    public class _激光IO_OUT_
    {
        public short 软件准备好 { set; get; } = 16;
        public short 红光 { set; get; } = 16;
        public short 标刻中 { set; get; } = 16;
        public short 标刻完成 { set; get; } = 16;
        public short 报警 { set; get; } = 16;
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

    public class _激光jcz2_笔参数_
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

    public enum _Err_jczMarkEzd2_
    {
        成功 = 0,
        发现EZCAD在运行 = 1,
        找不到EZCAD_CFG文件 = 2,
        打开LMC1失败 = 3,
        没有有效的lmc1设备 = 4,
        lmc1版本错误 = 5,
        找不到设备配置文件 = 6,
        报警信号 = 7,
        用户停止 = 8,
        不明错误 = 9,
        超时 = 10,
        未初始化 = 11,
        读文件错误 = 12,
        窗口为空 = 13,
        找不到指定名称的字体 = 14,
        错误的笔号 = 15,
        指定名称的对象不是文本对象 = 16,
        保存文件失败 = 17,
        找不到指定对象 = 18,
        当前状态下不能执行此操作 = 19,
        硬件不可开发 = 21,

        端口不在有效范置 = 97,
        未找到ezd文件 = 98,
        DL故障 = 99,

    }
}
