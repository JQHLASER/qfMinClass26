using NPOI.OpenXmlFormats.Wordprocessing;
using NPOI.SS.Formula.Functions;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Finance.Implementations;
using Org.BouncyCastle.Tls.Crypto.Impl.BC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Markup;

namespace qfmain
{
    public class 软件注册
    {

        public string 版本 { get; } = "QF2510";
        internal _软件授权_注册信息_ _注册信息 = new _软件授权_注册信息_();
        public _软件授权_机器码信息_ _机器码信息 = new _软件授权_机器码信息_();
        public _Dog_Cfg_ _Dog数据信息 = new _Dog_Cfg_();
        public _Dog_硬件信息_ _Dog硬件信息 = new _Dog_硬件信息_();

        public string _机器码 = "";
        private string _注册码 = "";
        /// <summary>
        /// 注册结果 
        /// </summary>
        public _软件授权_Err_ _err = _软件授权_Err_.未注册;

        /// <summary>
        /// 注册消息
        /// </summary>
        public string _msgErr = "";

        /// <summary>
        /// 注册类型,本地/加密狗等
        /// </summary>
        public _软件授权_注册类型_ _注册类型 { set; get; } = _软件授权_注册类型_.本地;

        /// <summary>
        /// 使用加密狗时, 如需试用,此值为true,会将注册类型改为本地,
        /// <para>是否为试用模式</para>
        /// </summary>
        public bool _是否试用 { set; get; } = false;

        /// <summary>
        /// 程序进入时
        /// </summary>
        /// <param name="注册类型_"></param>
        public 软件注册(_软件授权_注册类型_ 注册类型_)
        {
            this._注册类型 = 注册类型_;
        }


        string[] WorkBeff = new string[]
        {

            "加密狗处理",
            "获取注册码信息",
            "Get机器码",

            "Err",
            "注册",
        };

        /// <summary>
        /// 初始化
        /// </summary>
        public void Inistiall()
        {
            On_注册结果(_软件授权_Err_.开始注册);
            bool rt = true;
            foreach (var s in WorkBeff)
            {
                if (!rt)
                {
                    break;
                }
                else if (s == "Err")
                {
                    rt = 获取信息();

                }
                else if (s == "Get机器码")
                {
                    this._机器码 = 软件注册_子程序.Get机器码(this._机器码信息);

                }
                else if (s == "注册")
                {
                    if (this._注册类型 == _软件授权_注册类型_.不注册)
                    {
                        this._err = _软件授权_Err_.已完全注册;
                        break;
                    }

                    this._err = 注册(this._注册码, this._机器码信息, out this._注册信息, out this._msgErr);

                }
            }

            rt = rt ? ((this._err != _软件授权_Err_.已完全注册 && this._err != _软件授权_Err_.已日期注册) ? false : rt) : rt;

            On_Log(rt, this._msgErr);
            On_注册结果(this._err);
        }


        /// <summary>
        /// 软件运行时判断是否注册过期
        /// </summary>
        public void 注册检测()
        {
            if (this._err == _软件授权_Err_.已日期注册)
            {
                //this._err = 注册(this._注册码, this._机器码信息, out this._注册信息, out string msgErr);
                if (DateTime.TryParse(this._注册信息.授权值, out DateTime dates) &&
                    (dates - DateTime.Today).Days <= 0)
                {
                    this._msgErr = Language_.Get语言("注册码授权已过期");
                    this._err = _软件授权_Err_.已到期;
                    //检测到注册的日期已过期时
                    On_Log(false, this._msgErr);
                    On_注册结果(this._err);
                }

            }
        }


        public _软件授权_Err_ 注册(string 注册码, _软件授权_机器码信息_ 机器码信息, out _软件授权_注册信息_ 注册信息, out string msgErr)
        {
            注册信息 = new _软件授权_注册信息_();
            _软件授权_Err_ rt结果 = 软件注册_子程序.解析注册码(注册码, this._是否试用, 机器码信息, out 注册信息, out msgErr);
            return rt结果;
        }


        public bool 生成注册码(ref _软件授权_注册信息_ 注册信息, _软件授权_注册方式_ 注册方式, out string msgErr)
        {
            return 软件注册_子程序.生成注册码(ref 注册信息, 注册方式, out msgErr);
        }

        /// <summary>
        /// 读写注册码
        /// </summary>
        /// <param name="model"></param>
        /// <param name="Id"></param>
        public void 是否试用读写_从本地(ushort model, ref bool 是否试用)
        {
            string path =  Path .Combine (AppDomain .CurrentDomain .BaseDirectory , "Gsyf.dll");
            string sn = 软件注册_子程序.加密(是否试用.ToString());
            new 文件_文件夹().WriteReadText(path, model, ref sn, out string smgErr, null, true);
            string vxt = 软件注册_子程序.解密(sn);
            bool.TryParse(vxt, out bool isTest);
            是否试用 = isTest;
        }
        public void 注册码读写_从本地(ushort model, ref string 注册码)
        {
            软件注册_本地注册.注册码读写(model, ref 注册码);
        }
        internal void 客户ID读写_从本地(ushort model, ref string 客户id)
        {
            软件注册_本地注册.客户Id(model, ref 客户id);
        }

        /// <summary>
        /// 注册前获取信息
        /// </summary>
        /// <returns></returns>
        public bool 获取信息()
        {
            this._是否试用 = false;//在加密狗时,些参数有效
            bool rt = true;
            foreach (var s in WorkBeff)
            {
                if (!rt)
                {
                    break;
                }
                else if (s == "加密狗处理")
                {
                    if (this._注册类型 == _软件授权_注册类型_.加密狗)
                    {
                        #region 加密狗类

                        //下面为使用加密狗时的信息
                        _Dog_Err_ rtErrDog = DogTW.Get狗信息(out this._Dog数据信息, out this._Dog硬件信息, out string msgErrDog);
                        this._机器码信息.Sn = this._Dog硬件信息.Id ;
                        this._机器码信息.Uid = this._Dog数据信息.Uid;
                        this._注册码 = this._Dog数据信息.注册码;                     

                        if (rtErrDog != _Dog_Err_.正常)
                        {
                            bool isTest = false;
                            是否试用读写_从本地(1, ref isTest);
                            this._是否试用 = isTest;
                            if (this._是否试用)
                            {
                                //试用模式时,只能为本地注册模式,并且只能使用最多7天
                                this._注册类型 = _软件授权_注册类型_.本地;

                                continue;
                            }

                            #region Err

                            rt = false;
                            switch (rtErrDog)
                            {
                                case _Dog_Err_.加密狗检测故障:
                                    this._err = _软件授权_Err_.未检测到加密狗;
                                    this._msgErr = Language_.Get语言("未检测到加密狗");
                                    break;
                                case _Dog_Err_.未检测到加密狗:
                                    this._err = _软件授权_Err_.未检测到加密狗;
                                    this._msgErr = Language_.Get语言("未检测到加密狗");
                                    break;
                                case _Dog_Err_.加密狗不匹配:
                                    this._err = _软件授权_Err_.未检测到匹配的加密狗;
                                    this._msgErr = Language_.Get语言("未检测到匹配的加密狗");
                                    break;
                            }

                            #endregion

                            break;
                        }


                        #endregion
                    }

                }
                else if (s == "获取注册码信息")
                {
                    #region 获取注册码信息

                    if (this._注册类型 == _软件授权_注册类型_.不注册)
                    {
                        this._msgErr = Language_.Get语言("系统已完全授权");
                        this._err = _软件授权_Err_.已完全注册;
                        break;
                    }
                    else if (this._注册类型 == _软件授权_注册类型_.本地)
                    {
                        #region 本地

                        this._机器码 = 软件注册_本地注册.Get机器码(out this._机器码信息);
                        注册码读写_从本地(1, ref this._注册码);

                        #endregion
                    }
                    else if (this._注册类型 == _软件授权_注册类型_.加密狗)
                    {
                        #region 加密狗

                        this._机器码信息.Sn = this._Dog硬件信息 .Id ;
                        this._机器码信息.Uid = this._Dog数据信息.Uid;
                        this._注册码 = this._Dog数据信息.注册码;

                        #endregion
                    }
                    else
                    {
                        this._机器码信息 = On_获取机器码信息();
                    }

                    #endregion

                }
                else if (s == "Err")
                {
                    #region Err

                    if (string.IsNullOrEmpty(this._机器码信息.Sn))
                    {
                        rt = false;
                        this._err = _软件授权_Err_.未检测到设备信息;
                        this._msgErr = Language_.Get语言("未检测到设备信息");
                    }
                    else if (string.IsNullOrEmpty(this._机器码信息.Uid))
                    {
                        rt = false;
                        this._err = _软件授权_Err_.未检测到客户ID;
                        this._msgErr = Language_.Get语言("未检测到客户ID");
                    }
                    else if (string.IsNullOrEmpty(this._注册码))
                    {
                        rt = false;
                        this._err = _软件授权_Err_.未注册;
                        this._msgErr = Language_.Get语言("系统未授权");
                    }
                    #endregion

                }
                else if (s == "Get机器码")
                {
                    this._机器码 = 软件注册_子程序.Get机器码(this._机器码信息);

                }
            }
            return rt;
        }

        #region 事件

        public event Action<_软件授权_Err_> Event_注册结果;
        public void On_注册结果(_软件授权_Err_ Err)
        {
            Event_注册结果?.Invoke(Err);
        }

        public event Func<_软件授权_机器码信息_> Event_获取机器码信息;
        _软件授权_机器码信息_ On_获取机器码信息()
        {
            return Event_获取机器码信息?.Invoke();
        }

        /// <summary>
        /// 日志
        /// </summary>
        public event Action<bool, string> Event_Log;
        void On_Log(bool state, string logValue)
        {
            Event_Log?.Invoke(state, logValue);
        }


        #endregion

    }
}
