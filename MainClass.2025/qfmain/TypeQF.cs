using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfmain
{

    public enum _em_json类型_
    {
        NewtonsoftJson,
        SystemIOjsontext
    }




    /// <summary>
    /// Socket参数,客户端与服务端共用参数
    /// </summary>
    public class _Socket_Cfg_
    {
        /// <summary>
        /// 客户端
        /// </summary>
        public string IP { set; get; } = "127.0.0.1";
        public int Port { set; get; } = 2000;

        public int 接收区大小 { set; get; } = 1024 * 1024;
        public int 发送区大小 { set; get; } = 1024 * 1024;

        /// <summary>
        /// 客户端
        /// <para>如需要通讯响应快可以降低这个值</para>
        /// </summary>
        public int 线程周期 { set; get; } = 100;

        /// <summary>
        /// 客户端
        /// <para>如需要通讯响应快可以降低这个值</para>
        /// </summary>
        public int 线程周期_接收数据 { set; get; } = 0;

        /// <summary>
        /// 客户端
        /// </summary>
        public int 重连周期 { set; get; } = 100;

    }

    /// <summary>
    /// 串口参数
    /// </summary>
    public class _SerialPort_Cfg_
    {
        public string 串口名称 { set; get; } = "";
        public int 波特率 { set; get; } = 9600;
        public int 数据位 { set; get; } = 8;
        /// <summary>
        /// None
        /// </summary>
        public Parity 校验位 { set; get; } = Parity.None;

        /// <summary>
        /// One
        /// </summary>
        public StopBits 停止位 { set; get; } = StopBits.One;


        public int 输入缓冲区大小 { set; get; } = 1024 * 1024;
        public int 输出缓冲区大小 { set; get; } = 1024 * 1024;

        /// <summary>
        /// 启用数接终端就绪(DTR信号)
        /// </summary>
        public bool DtrEnable { set; get; } = true;

        /// <summary>
        /// 启用请求发送(RTS信号)
        /// </summary>
        public bool RtsEnable { set; get; } = false;

        /// <summary>
        /// 传输的握手协议
        /// </summary>
        public Handshake Handshake { set; get; } = Handshake.None;

        /// <summary>
        /// 读取未完成时的超时ms
        /// </summary>
        public int ReadTimeout { set; get; } = -1;


        /// <summary>
        /// 写入超时时间(毫秒)
        /// </summary>
        public int WriteTimeout { set; get; } = -1;           // 写入超时时间(毫秒)



    }

    /// <summary>
    /// 一般在通讯中使用
    /// </summary>
    public enum _解码Type
    {
        /// <summary>
        /// 缓存时间超时后反馈数据
        /// </summary>
        无前后缀,
        前后缀,
        前缀,
        后缀,

    }

    /// <summary>
    /// 一般通讯中使用
    /// </summary>
    public class _解码_Cfg_
    {
        public _解码_Cfg_(byte[] 前缀_, byte[] 后缀_, int 超时时间_ = 200)
        {
            if (前缀_ is null)
            {
                前缀_ = new byte[0];
            }

            if (后缀_ is null)
            {
                后缀_ = new byte[0];
            }

            this.超时时间 = 超时时间_;
            this.前缀 = 前缀_;
            this.后缀 = 后缀_;
        }

        public _解码_Cfg_()
        {

        }


        /// <summary>
        /// ms
        /// </summary>
        public int 超时时间 { set; get; } = 200;

        public byte[] 前缀 { set; get; } = new byte[0];
        public byte[] 后缀 { set; get; } = new byte[] { 0x0D, 0X0A };

    }

    /// <summary>
    /// 窗体标题栏状态
    /// </summary>
    public enum WindowTitleState
    {
        闲置中,
        报警中,
        加工中,
    }

    /// <summary>
    /// 窗体标题栏信息
    /// </summary>
    public class _windowInfo_
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="状态_">=0:无,=小于0:红,=大于0:黄色</param>
        /// <param name="内容_"></param>
        public _windowInfo_(string Name_, int 状态_, string 内容_)
        {
            Name = Name_;
            状态 = 状态_;
            内容 = 内容_;
        }

        /// <summary>
        /// 功能标题,表示状态的种类
        /// </summary>
        public string Name { set; get; } = "";

        public string 内容 { set; get; } = "";

        /// <summary>
        /// =0:无,=小于0:红,=大于0:黄色
        /// </summary>
        public int 状态 { set; get; } = 0;

    }


    /// <summary>
    /// 软件授权,机器码=SN-ID
    /// </summary>
    public class _软件授权_机器码信息_
    {
        /// <summary>
        /// 设备SN码
        /// </summary>
        public string Sn { set; get; } = string.Empty;
        /// <summary>
        /// 客户ID
        /// </summary>
        public string Uid { set; get; } = string.Empty;
    }

    public class _软件授权_注册信息_
    {


        /// <summary>
        /// 设备Id-客户Id
        /// </summary>
        public string 机器码 { set; get; } = string.Empty;


        /// <summary>
        /// 为时间限制时,格式为 年-月-日
        /// </summary>
        public string 授权值 { set; get; } = string.Empty;

        public string 注册码 { set; get; } = string.Empty;

    }

    /// <summary>
    /// 注册结果
    /// </summary>
    public enum _软件授权_Err_
    {
        未注册 = -1,
        已到期 = -2,
        已完全注册 = 0,
        已日期注册 = 1,
        未检测到加密狗 = -3,
        未检测到匹配的加密狗 = -4,
        未检测到设备信息 = -5,
        未检测到客户ID = -6,
        开始注册 = -7,
    }

    /// <summary>
    /// 完全注册/日期限制
    /// </summary>
    public enum _软件授权_注册方式_
    {
        完全,
        日期限制,
    }

    /// <summary>
    /// 注册类型
    /// </summary>
    public enum _软件授权_注册类型_
    {
        本地,
        加密狗,
        远程,
        不注册,
    }

    /// <summary>
    /// 加密狗信息
    /// </summary>
    public class _Dog_Cfg_
    {


        /// <summary>
        /// 客户识别码ID
        /// </summary>
        public string Uid { set; get; } = string.Empty;
        public string 注册码 { set; get; } = string.Empty;
        /// <summary>
        /// 软件的功能码
        /// </summary>
        public int 功能码{ set; get; } = 0;    

        /// <summary>
        /// 加密狗类型,代理或万能这些
        /// </summary>
        public string Types { set; get; } = string.Empty;
        public string 备注 { set; get; } = string.Empty;
    }

    public class _Dog_硬件信息_
    {
        public string Id { set; get; } = string.Empty;
        public short 版本号 { set; get; } = 0;
    }


    /// <summary>
    /// 加密狗类型,常用的,定制的为自己输入的
    /// </summary>
    public enum _Dog_Type
    {
        万能,
        代理,
    }

    public enum _Dog_Err_
    {
        未检测到加密狗 = -1,
        正常 = 0,
        加密狗不匹配 = -3,
        加密狗检测故障 = -4,


    }

    /// <summary>
    /// 使用QRcode库生成二维码的参数
    /// </summary>
    public class _QRcode_Cfg_
    {
        public int 像素大小 { set; get; } = 20;

        /// <summary>
        /// 文本颜色
        /// </summary>
        public Color darkColor { set; get; } = Color.Black;
        /// <summary>
        /// 背景颜色
        /// </summary>
        public Color darkClightColorolor { set; get; } = Color.Transparent;

        public Bitmap 水印图标 { set; get; } = null;
        public int 水印大小比例 { set; get; } = 15;
        public int 水印边框宽度 { set; get; } = 0;
        public bool 是否绘制空白边框 { set; get; } = false;
        public Color 水印背景色 { set; get; } = Color.White;




    }

    /// <summary>
    /// 远程注册通讯类型
    /// </summary>
    public enum _软件授权_Tcp_指令_
    {

        本地_写信息,

        加密狗_获取硬件信息,
        加密狗_获取信息,
        加密狗_写信息,
        加密狗_恢复出厂设置,


        发送注册码,


        /// <summary>
        /// 使用此指令,获得终端使用的是什么注册类型
        /// </summary>
        获取基本信息,


    }

    public enum _软件授权_TCP通讯功能_
    {
        软件终端,
        注册机终端,
    }


    /// <summary>
    /// 远程注册时通讯信息
    /// </summary>
    public class _软件授权_Tcp注册_通讯协议_
    {
        /// <summary>
        /// 软件终端/注册机终端
        /// </summary>
        public _软件授权_TCP通讯功能_ 通讯功能 { set; get; } = _软件授权_TCP通讯功能_.软件终端;

        /// <summary>
        /// 消息
        /// </summary>
        public string msg { set; get; } = "";

        /// <summary>
        /// =0:成功,否则失败
        /// </summary>
        public int Code { set; get; } = -1;

        public string 机器码 { set; get; } = "";
        public string 注册码 { set; get; } = "";

        /// <summary>
        /// 使用加密狗时, 如需试用,此值为true,会将注册类型改为本地,
        /// </summary>
        public bool 是否试用 { set; get; } = false;

        /// <summary>
        /// 本地/加密狗等
        /// </summary>
        public _软件授权_注册类型_ 注册类型 { set; get; } = _软件授权_注册类型_.本地;

        /// <summary>
        /// 通讯类型
        /// </summary>
        public _软件授权_Tcp_指令_ 通讯指令
        { set; get; } = _软件授权_Tcp_指令_.获取基本信息;

        /// <summary>
        /// 本地注册信息
        /// </summary>
        public _软件授权_注册信息_ 注册信息 { set; get; } = new _软件授权_注册信息_();

        /// <summary>
        /// 本地机器码信息
        /// </summary>
        public _软件授权_机器码信息_ 机器码信息 { set; get; } = new _软件授权_机器码信息_();

        public _Dog_Cfg_ dog数据信息 { set; get; } = new _Dog_Cfg_();

        public _Dog_硬件信息_ dog硬件信息 { set; get; } = new _Dog_硬件信息_();

        public _软件授权_Err_ err_注册结果 { set; get; } = _软件授权_Err_.未注册;
    }




    /// <summary>
    /// 通讯中/闲置
    /// </summary>
    public enum _通讯中状态_
    {
        通讯中 = 1,
        闲置 = 0,
    }

    /// <summary>
    /// 等待反馈中/已反馈/闲置
    /// </summary>
    public enum _通讯过程_
    {
        等待反馈中 = 1,
        已反馈 = 2,
        闲置 = 0,
    }
 

    public enum _连接状态_
    {
        连接中 = -2,
        已连接 = 0,
        未连接 = -1,
        无响应 = -3,
        功能码不匹配 = -4,
    }

    public enum _初始化状态_
    {
        已初始化,
        初始化中 = -2,
        未初始化 = -1,
    }



    public enum _启动状态_
    {
        未启动 = -1,
        已启动 = 0,
        启动中 = -2,
    }
    public enum _打开状态_
    {
        未打开 = -1,
        已打开 = 0,
        打开中 = -2,
    }



}
