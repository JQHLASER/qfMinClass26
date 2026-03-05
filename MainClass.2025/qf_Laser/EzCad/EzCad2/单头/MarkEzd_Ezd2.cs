
using qfmain;
using qfNet;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Deployment.Internal;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace qf_Laser
{
    /// <summary>
    /// 统一接口
    /// </summary>
    public class MarkEzd_Ezd2 : IWork_LaserMark
    {
        #region 变量


        /// <summary>
        /// 输入端口
        /// </summary>
        public byte _IO_InPutByte { set; get; } = 0;
        /// <summary>
        /// 输出端口
        /// </summary>
        public byte _IO_OutPutByte { set; get; } = 0;


        /// <summary>
        /// 输入端口
        /// </summary>
        public bool[] _IO_InPut { set; get; } = new bool[0];
        /// <summary>
        /// 输出端口
        /// </summary>
        public bool[] _IO_OutPut { set; get; } = new bool[0];

        /// <summary>
        /// 打标卡初始化状态
        /// </summary>
        public _初始化状态_ _初始化状态 { set; get; } = _初始化状态_.未初始化;
        public _激光加工状态_ _激光加工状态 { set; get; } = _激光加工状态_.闲置;

        /// <summary>
        /// 激光模板全路径
        /// </summary>
        public string _Path_激光模板 { set; get; } = string.Empty;
        public string _激光模板后缀 { set; get; } = ".ezd";

        public _激光参数_ _参数 { set; get; } = new _激光参数_();

        public string _激光编辑软件名称 { set; get; } = "EzCad2.exe";

        /// <summary>
        /// 最小端口
        /// </summary>
        public ushort _minPort { set; get; } = 0;
        /// <summary>
        /// 最大端口
        /// </summary>
        public ushort _maxPort { set; get; } = 15;

        public string _Path_激光模板_最后一次 { set; get; } = string.Empty;
        bool _isRun = true;
        bool _is连续加工 = false;


        #endregion



        #region 对外接口

        public string 功能说明()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("JCZ,EzCad2开发类");
            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("");

            return sb.ToString();
        }




        public bool 端口是否有效(ushort Port)
        {
            return this.Err_端口是否有效(Port);
        }




        bool _Inistiall = false;
        public void 初始化(bool 使能线程)
        {
         
            标题栏状态_初始化状态();
            标题栏状态_加工状态();

            On_初始化状态(_初始化状态_.未初始化);
            读写参数(1);
            读写_最后一次ezdpath(1);
            读EzCadName();
         

            if (使能线程)
            {
                new Thread(() => { 线程(); }) { IsBackground = true }.Start();
            }
            new Thread(() => { 线程_处理ezCad(); }) { IsBackground = true }.Start();
            _Inistiall = true;
        }

        public void 释放()
        {
            if (!_Inistiall)
            {
                return;
            }
            this._isRun = false;
            释放打标卡();
        }

        public (bool s, string m) 初始化打标卡()
        {
            (bool s, string m, int e) rt = 初始化打标卡_();
            return (rt.s, rt.m);
        }

        public (bool s, string m) 释放打标卡()
        {
            return 释放打标卡(true);
        }

        #region 初始化 / 释放


        /// <summary>
        /// 本地方法
        /// </summary>
        /// <returns></returns>
        (bool s, string m, int e) 初始化打标卡_()
        {
            string msgErr = string.Empty;
            bool rtb = true;
            int nErr = (int)_Err_jczMarkEzd2_.未初始化;

            On_初始化状态(_初始化状态_.初始化中);
            if (EzCad2软件是否打开())
            {
                msgErr = qfmain.Language_.Get语言("发现EzCad进程");
                On_Log(false, msgErr);
                On_初始化状态(_初始化状态_.未初始化);
                return (false, msgErr, nErr);
            }
            else
            {
                nErr = JczLmc.Initialize2(JczLmc.path_EzCad2, false);
                if (nErr == 0)
                {
                    msgErr = qfmain.Language_.Get语言("已初始化");
                    On_Log(true, msgErr);
                    On_初始化状态(_初始化状态_.已初始化);
                }
                else
                {
                    msgErr = $"{qfmain.Language_.Get语言("未初始化")},{JczLmc.ErrMsg(nErr)}";
                    On_Log(false, msgErr);
                    释放打标卡(false);
                }
            }
            rtb = (nErr == (int)_Err_jczMarkEzd2_.成功) ? true : false;
            return (rtb, msgErr, nErr);


        }

        private (bool s, string m) 释放打标卡(bool is日志)
        {
            string msgErr = "";
            On_初始化状态(_初始化状态_.未初始化);
            msgErr = $"{qfmain.Language_.Get语言("已释放")}";
            if (is日志)
            {
                On_Log(false, msgErr);
            }
            int nErr = JczLmc.Close();
            this._Path_激光模板 = string.Empty;
            bool rt = nErr == (int)_Err_jczMarkEzd2_.成功 ? true : false;
            return (rt, msgErr);
        }

        #endregion

        /// <summary>
        /// SetDevCfg
        /// </summary>
        /// <returns></returns>
        public (bool s, string m) 激光参数()
        {
            _Err_jczMarkEzd2_ nErr = SetDevcfg(out string msgErr);
            bool rt = ((_Err_jczMarkEzd2_)nErr == _Err_jczMarkEzd2_.成功) ? true : false;
            return (rt, msgErr);
        }

        public void win_设置()
        {
            using (Form_jcz单头_设置 forms = new Form_jcz单头_设置(this))
            {
                forms.ShowDialog();
            }
        }

        /// <summary>
        /// 打开EzCad2.exe软件
        /// </summary>
        public async Task 打开激光编辑软件()
        {
            await Task.Run(() =>
             {
                 try
                 {
                     string ezdPath_ = this._Path_激光模板_最后一次;
                     EzCad2打开状态 = _EzCad2打开状态_.已处理;
                     if (this._初始化状态 == _初始化状态_.已初始化)
                     {
                         this.释放打标卡(true);
                         Thread.Sleep(1000);
                     }
                     if (!string.IsNullOrEmpty(ezdPath_) && new 文件_文件夹().文件_是否存在(ezdPath_))
                     {
                         new 文件_文件夹().文件_打开(ezdPath_, out string msgErr, "", ProcessWindowStyle.Maximized);

                     }
                     else
                     {
                         new 文件_文件夹().文件_打开(JczLmc.path_EzCad2 + $"\\{this._参数.激光软件名称}.exe", out string msgErr, "", ProcessWindowStyle.Maximized);
                     }
                     Thread.Sleep(3000);
                     EzCad2打开状态 = _EzCad2打开状态_.打开;
                 }
                 catch (Exception ex)
                 {
                     On_Log(false, ex.Message);
                     Thread.Sleep(1000);
                     EzCad2打开状态 = _EzCad2打开状态_.None;
                 }

             });
        }

        public void win_调试()
        {
            if (!this.Err_未初始化(out string msgErr) ||
            !this.Err_红光指示中(out msgErr) || !this.Err_出激光标刻中(out msgErr) ||
             !this.Err_加载激光模板中(out msgErr))
            {
                MessageBox.Show(msgErr, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            using (Form_jcz单头_调试 forms = new Form_jcz单头_调试(this))
            {
                forms.ShowDialog();
            }
        }

        public (bool s, string m) 打开模板(string path, bool Is图像, bool Is显示日志)
        {
            _Err_jczMarkEzd2_ nErr = this.LoadEzdFile(path, Is图像);
            bool rt = ErrToMsg((int)nErr, out string msgErr);

            if (Is显示日志)
            {
                On_Log(rt, $"{qfmain.Language_.Get语言("加载激光模板")},{msgErr},{path}");
            }
            return (rt, msgErr);
        }
        public (bool s, string m) 打开模板_openFileDialog(bool Is图像, bool Is显示日志)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = qfmain.软件类.Files_Cfg.Files_Template;
            openFileDialog.Multiselect = false;//不充许选择多个文件
            openFileDialog.DefaultExt = "ezd";
            openFileDialog.Filter = "(*.ezd)|*.ezd";

            DialogResult dt = openFileDialog.ShowDialog();
            if (dt == DialogResult.OK)
            {
                string path = openFileDialog.FileName;
                return 打开模板(path, Is图像, Is显示日志);
            }
            return (true, "");
        }

        public (bool s, string m) 删除所有对象(bool Is清除最后一打打开的激光模 = true, bool Is清空图形 = true)
        {
            var nErr = 删除所有对象_(Is清除最后一打打开的激光模, Is清空图形);
            bool rt = ErrToMsg((int)nErr, out string msgErr);
            return (rt, msgErr);
        }


        public (bool s, string m) 保存模板(string path, bool Is显示日志)
        {
            _Err_jczMarkEzd2_ nErr = 保存ezd文件(path);
            bool rt = this.ErrToMsg((int)nErr, out string msgErr);

            if (Is显示日志)
            {
                On_Log(rt, $"{qfmain.Language_.Get语言("保存激光模板")},{msgErr},{path}");
            }
            return (rt, msgErr);
        }

        public void ui_图像控件(UserControl control)
        {
            UserColor_图像(control);
        }

        public (bool s, string m) 红光指示(bool is日志)
        {
            _激光_红光指示_ red = this._参数.红光指示轮廓 ? _激光_红光指示_.轮郭 : _激光_红光指示_.外框;
            return this.加工_红光指示(red, is日志);
        }

        public (bool s, string m) 停止()
        {
            _Err_jczMarkEzd2_ nErr = this.停止标刻和红光();
            bool rt = this.ErrToMsg((int)nErr, out string msgErr);
            return (rt, msgErr);
        }
        public (bool s, string m) 标刻(bool bFlyMark, bool is加工状态 = true)
        {
            _Err_jczMarkEzd2_ nErr = this.激光标刻(bFlyMark, is加工状态);
            bool rt = this.ErrToMsg((int)nErr, out string msgErr);
            return (rt, msgErr);
        }

        public (bool s, string m) 输出(ushort port, bool state)
        {
            if (this._初始化状态 != _初始化状态_.已初始化 ||
              !Err_端口是否有效(port))
            {
                return (false, "");
            }

            _Err_jczMarkEzd2_ nerr = this.输出IO(port, state);
            bool rt = this.ErrToMsg((int)nerr, out string msgErr);
            return (rt, msgErr);
        }

        public void 输出脉冲式(ushort port)
        {
            if (this._初始化状态 != _初始化状态_.已初始化 ||
                !Err_端口是否有效(port))
            {
                return;
            }

            输出IO(port, true);
            Thread.Sleep(this._参数.OUT.输出脉宽);
            输出IO(port, false);

        }

        public void 输出_标刻中(bool NF)
        {
            this.输出(this._参数.OUT.标刻中, NF);
        }
        public void 输出_红光(bool NF)
        {
            this.输出(this._参数.OUT.红光, NF);
        }
        public void 输出_Ready(bool NF)
        {
            this.输出(this._参数.OUT.软件准备好, NF);
        }
        public void 输出_报警(bool NF)
        {
            this.输出(this._参数.OUT.报警, NF);
        }
        public void 输出_报警()
        {
            this.输出脉冲式(this._参数.OUT.报警);
        }
        public void 输出_标刻完成()
        {
            this.输出脉冲式(this._参数.OUT.标刻完成);
        }
        public _激光参数_ 读参数()
        {
            this.读写参数(1);
            return this._参数;
        }

        public void 刷新图形(_激光_获取图像_ state = _激光_获取图像_.获取)
        {
            Event_获取图像?.Invoke(state);
        }
        public (bool s, string m, Bitmap v) 获取图形(int width, int height)
        {
            try
            {
                Bitmap b = this.获取_图形(width, height);
                return (true, "", b);
            }
            catch (Exception ex)
            {
                return (false, ex.Message, null);
            }

        }

        public (bool s, string m) 修改对象内容(string 对象名, string 内容)
        {
            _Err_jczMarkEzd2_ nErr = this.修改_指定对象的内容(对象名, 内容);
            bool rt = this.ErrToMsg((int)nErr, out string msgErr);
            return (rt, msgErr);
        }
        public (bool s, string m, string v) 获取对象内容(string 对象名, int lenght = 255)
        {
            string value = string.Empty;
            _Err_jczMarkEzd2_ nErr = this.获取_对象内容(对象名, ref value, lenght);
            bool rt = this.ErrToMsg((int)nErr, out string msgErr);
            return (rt, msgErr, value);
        }
        public (bool s, string m, string v) 获取对象名称(int 对象索引, int lenght = 255)
        {
            string value = string.Empty;
            _Err_jczMarkEzd2_ nErr = this.获取_对象名称(对象索引, ref value, lenght);
            string 对象名 = value;
            bool rt = this.ErrToMsg((int)nErr, out string msgErr);
            return (rt, msgErr, value);
        }
        public (bool s, string m, int v) 获取对象总数()
        {
            try
            {
                int count = this.获取_对象总数();
                return (true, "", count);
            }
            catch (Exception ex)
            {
                return (false, ex.Message, 0);
            }
        }

        // <summary>
        ///  a:角度,
        ///  <para>反馈结果有可能不准确</para>
        /// </summary> 
        public (bool s, string m) 设置绝对坐标(double x, double y, double xCenter, double yCenter, double a)
        {
            string value = string.Empty;
            _Err_jczMarkEzd2_ nErr = this.旋转变换(x, y, xCenter, yCenter, a);
            bool rt = this.ErrToMsg((int)nErr, out string msgErr);
            return (rt, msgErr);
        }

        public _变量信息_[] 获取所有变量对象信息()
        {
            List<_变量信息_> lst = new List<_变量信息_>();
            (bool s, string m, int count) rt = 获取对象总数();
            for (int i = 0; i < rt.count; i++)
            {
                (bool s, string m, string v) rti = 获取对象名称(i);
                if (!string.IsNullOrEmpty(rti.v))
                {
                    (bool s, string m, string v) rtv = 获取对象内容(rti.v);
                    _变量信息_ info = new _变量信息_(rti.v, rtv.v);
                    lst.Add(info);
                }
            }
            return lst.ToArray();
        }

        /// <summary>
        /// xCenter:中心x,yCenter:中心y
        /// </summary> 
        public (bool s, string m, double width, double height, double xCenter, double yCenter) 获取对象尺寸(string 对象名)
        {
            double width = 0, height = 0, xCenter = 0, yCenter = 0;
            _Err_jczMarkEzd2_ nErr = 获取_对象中心坐标及尺寸(对象名, ref xCenter, ref yCenter, ref width, ref height);
            bool rt = ErrToMsg((int)nErr, out string msgErr);
            return (rt, msgErr, width, height, xCenter, yCenter);
        }
        public (bool s, string m) 设置对象尺寸(string 对象名, double width, double height, double xCenter, double yCenter)
        {
            bool rt = true;
            string msgErr;
            (bool s, string m, double width, double height, double xCenter, double yCenter) rtM = 获取对象尺寸(对象名);
            rt = rtM.s;
            msgErr = rtM.m;
            if (rt)
            {
                double xSol = rtM.width <= 0
                              ? 1 : width / rtM.width;
                double ySol = rtM.height <= 0
                              ? 1 : height / rtM.height;

                _Err_jczMarkEzd2_ nErr = 按比例缩放对象(对象名, xCenter, yCenter, xSol, ySol);
                rt = ErrToMsg((int)nErr, out msgErr);
            }
            return (rt, msgErr);
        }

        public (bool s, string m, _笔参数_ pen) 获取笔参数(int 笔号)
        {
            _Err_jczMarkEzd2_ nErr = 获取_笔参数(笔号, out _笔参数_ pen);
            bool rt = ErrToMsg((int)nErr, out string msgErr);
            return (rt, msgErr, pen);
        }

        public (bool s, string m) 设置笔参数(_笔参数_ pen)
        {
            _Err_jczMarkEzd2_ nErr = 设置_笔参数(pen);
            bool rt = ErrToMsg((int)nErr, out string msgErr);
            return (rt, msgErr);
        }



        #endregion



        #region EzCad

        enum _EzCad2打开状态_
        {
            None = 0,
            打开 = 1,
            已处理 = -1,
        }


        _EzCad2打开状态_ EzCad2打开状态 = _EzCad2打开状态_.None;




        string[] _Ezd进程名 = new string[0];
        void 读EzCadName()
        {
            string path = $"{qfmain.软件类.Files_Cfg.Files_sysConfig}\\EzdName.cfg";

            List<string> lst = new List<string>();
            lst.Add("EzCad2");
            lst.Add("EzCad2.7.6");
            lst.Add("EzCad2.15.1");
            lst.Add("EzCad2.15.2");
            lst.Add("EzCad2.14.11脱机版");
            lst.Add("EzCad2.7.0脱机版");
            lst.Add("EzCad2.7.6脱机版");
            lst.Add("EzCad2.14.9");
            lst.Add("EzCad2.14.11");
            lst.Add("EzCad2.14.10");
            lst.Add("EzCad2.7.0");
            this._Ezd进程名 = lst.ToArray();
            new qfmain.文件_文件夹().WriteReadJson<string[]>(path, 1, ref this._Ezd进程名, out string msgErr);

        }

        bool 是否第一次运行 = true;

        IntPtr EzCadHwnd = IntPtr.Zero;
        _Err_jczMarkEzd2_ rtJcz = _Err_jczMarkEzd2_.发现EZCAD在运行;

        void 线程_处理ezCad()
        {
            IntPtr EzCadHwnd = IntPtr.Zero;
            _Err_jczMarkEzd2_ rtJcz = _Err_jczMarkEzd2_.发现EZCAD在运行;

            while (this._isRun)
            {
                Thread.Sleep(1500);
                if (!this._isRun)
                {
                    break;
                }


                List<string> lstWork = new List<string>();
                lstWork.Add("初始化状态");
                lstWork.Add("判断是否需要初始化");
                lstWork.Add("初始化");

                bool rt = true;
                foreach (var item in lstWork)
                {
                    if (!rt)
                    {
                        break;
                    }
                    else if (item == "初始化状态")
                    {
                        if (this._初始化状态 != _初始化状态_.未初始化 || EzCad2打开状态 == _EzCad2打开状态_.已处理)
                        {
                            rt = false;
                        }
                    }
                    else if (item == "判断是否需要初始化")
                    {
                        if (rtJcz == _Err_jczMarkEzd2_.硬件不可开发 ||
                                   rtJcz == _Err_jczMarkEzd2_.DL故障)
                        {
                            rt = false;
                        }
                    }
                    else if (item == "初始化")
                    {
                        try
                        {
                            bool rts = EzCad2软件是否打开();
                            if (!rts)
                            {
                                if (this.is第一次初始化_EzCad)
                                {
                                    this.is第一次初始化_EzCad = false;
                                }

                                (bool s, string m, int e) rtm = 初始化打标卡_();
                                rtJcz = (_Err_jczMarkEzd2_)rtm.e;
                                EzCad2打开状态 = _EzCad2打开状态_.已处理;
                                // this.is第一次初始化_EzCad = true;
                            }
                            else if (this.is第一次初始化_EzCad)
                            {
                                this.is第一次初始化_EzCad = false;
                                rtJcz = _Err_jczMarkEzd2_.未初始化;
                                On_Log(false, $"{qfmain.Language_.Get语言("发现EzCad进程")}");
                                On_初始化状态(_初始化状态_.未初始化);
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }





            }
        }

        bool is第一次初始化_EzCad = true;

        internal bool EzCad2软件是否打开()
        {
            bool rt = false;
            foreach (var s in this._Ezd进程名)
            {
                if (rt)
                {
                    break;
                }
                else if (new 进程().进程是否存在(s, out string msgErr))
                {
                    rt = true;
                    break;
                }
            }

            return rt;
        }

        /// <summary>
        /// 需要在EzdName.cfg配置进程名
        /// </summary>
        public void 关闭EzCad2的进程()
        {
            foreach (var s in this._Ezd进程名)
            {
                if (new qfmain.进程().进程是否存在(s))
                {
                    new qfmain.进程().结束指定进程(s);
                    break;
                }
            }

        }


        #endregion


        #region 本地方法

        /// <summary>
        /// 返回值转换成消息
        /// </summary>
        /// <param name="nErr"></param>
        /// <param name="msgErr"></param>
        /// <returns></returns>
        public bool ErrToMsg(int nErr, out string msgErr)
        {
            bool rt = nErr == 0 ? true : false;
            string errMsg = JczLmc.ErrMsg(nErr);
            msgErr = rt ? $"{errMsg}" : $"{qfmain.Language_.Get语言("失败")},{errMsg}";
            return rt;
        }

        private readonly object _lock_读写参数 = new object();
        public void 读写参数(ushort model)
        {
            lock (this._lock_读写参数)
            {
                string path = qfmain.软件类.Files_Cfg.Files_Config + "\\jczCfg.dll";
                _激光参数_ info = this._参数;
                new qfmain.文件_文件夹().WriteReadIni<_激光参数_>(path, model, ref info, out string msgErr);
                this._参数 = info;
            }
        }

        void 读写_最后一次ezdpath(ushort model)
        {
            string path = Environment.CurrentDirectory + "\\jczdf.crc";
            string pathEzd = this._Path_激光模板_最后一次;
            var rt = new qfmain.文件_文件夹().WriteReadIni<string>(path, model, ref pathEzd, out string msgErr);

            if (!string.IsNullOrEmpty(pathEzd) && new qfmain.文件_文件夹().文件_是否存在(path))
            {
                this._Path_激光模板_最后一次 = pathEzd;
            }
            else
            {
                this._Path_激光模板_最后一次 = string.Empty;
            }
        }


        void IO状态转换(int INstatus, int OUTstatus, out bool[] INbeff, out bool[] OUTbeff)
        {
            List<bool> lstIN = new List<bool>();
            List<bool> lstOUT = new List<bool>();
            for (int i = 0; i < 16; i++)
            {
                bool IN = false;
                bool OUT = false;
                new qfmain.进制().取指定位状态_十进制(INstatus, i, 15, out IN, out string msgErr);
                new qfmain.进制().取指定位状态_十进制(OUTstatus, i, 15, out OUT, out msgErr);

                lstIN.Add(IN);
                lstOUT.Add(OUT);
            }

            INbeff = lstIN.ToArray();
            OUTbeff = lstOUT.ToArray();
        }



        #endregion


        #region 方法....MarkEzd



        /// <summary>
        /// jcz参数
        /// </summary>
        /// <returns></returns>
        internal _Err_jczMarkEzd2_ SetDevcfg(out string msgErr)
        {
            int nErr = JczLmc.SetDevCfg();
            msgErr = nErr == 0 ? "" : JczLmc.ErrMsg(nErr);
            return (_Err_jczMarkEzd2_)nErr;
        }


        /// <summary>
        /// 加载激光模板
        /// </summary>
        /// <param name="pathEzd"></param>
        /// <param name="msgErr"></param>
        /// <returns></returns>
        internal _Err_jczMarkEzd2_ LoadEzdFile(string pathEzd, bool 是否刷新图像 = true)
        {
            On_加工状态(_激光加工状态_.加载激光模板中);
            int nErr = 0;
            if (!new qfmain.文件_文件夹().文件_是否存在(pathEzd))
            {
                nErr = (int)_Err_jczMarkEzd2_.未找到ezd文件;
            }
            else
            {
                nErr = JczLmc.LoadEzdFile(pathEzd);
                if (nErr == 0)
                {
                    this._Path_激光模板 = pathEzd;
                    this._Path_激光模板_最后一次 = this._Path_激光模板;
                    读写_最后一次ezdpath(0);
                    if (是否刷新图像)
                    {
                        刷新图形(_激光_获取图像_.获取);
                    }
                    On_加载激光模板成功(pathEzd);
                }
            }
            On_加工状态(_激光加工状态_.闲置);
            return (_Err_jczMarkEzd2_)nErr;
        }


        internal _Err_jczMarkEzd2_ 删除所有对象_(bool Is清除最后一打打开的激光模 = true, bool Is清空图形 = true)
        {
            int nErr = JczLmc.删除所有对象();
            if (nErr == 0)
            {
                this._Path_激光模板 = "";
                if (Is清除最后一打打开的激光模)
                {
                    this._Path_激光模板_最后一次 = this._Path_激光模板;
                    读写_最后一次ezdpath(0);
                }
                if (Is清空图形)
                {
                    刷新图形(_激光_获取图像_.清除);
                }
            }
            return (_Err_jczMarkEzd2_)nErr;
        }

        /// <summary>
        ///  向数据库添加一条曲线对象
        /// 注意 曲线顶点数组 必须为2维数组,且第一维为2,如 double[5,2],double[n,2],
        /// 顶点数量 为 曲线顶点数组 数组的第2维,如 曲线顶点数组 为double[5,2]数组,则ptNum=5
        /// </summary>
        /// <param name="曲线顶点数组"></param>
        /// <param name="顶点数量"></param>
        /// <param name="对象名称"></param>
        /// <param name="笔号"></param>
        /// <param name="是否填充"></param>
        /// <returns></returns>
        internal _Err_jczMarkEzd2_ 添加曲线(string 对象名称, double[,] 曲线顶点数组, int 顶点数量, int 笔号, int 是否填充)
        {
            int nErr = JczLmc.添加曲线(曲线顶点数组, 顶点数量, 对象名称, 笔号, 是否填充);
            return (_Err_jczMarkEzd2_)nErr;
        }


        /// <summary>
        /// 按比例缩放大小
        /// </summary> 
        internal _Err_jczMarkEzd2_ 按比例缩放对象(string 对象名称, double xCenter, double yCenter, double x比例, double y比例)
        {
            x比例 = x比例 <= 0 ?
                       1 : x比例;
            y比例 = y比例 <= 0 ?
                       1 : y比例;

            int nErr = JczLmc.指定对象按比例缩放(对象名称, xCenter, yCenter, x比例, y比例);
            return (_Err_jczMarkEzd2_)nErr;
        }


        internal _Err_jczMarkEzd2_ 删除指定对象(string 对象名称)
        {
            int nErr = JczLmc.删除指定对象(对象名称);
            return (_Err_jczMarkEzd2_)nErr;
        }

        internal _Err_jczMarkEzd2_ 保存ezd文件(string ezdpath)
        {
            int nErr = JczLmc.保存ezd(ezdpath);
            return (_Err_jczMarkEzd2_)nErr;
        }

        /// <summary>
        /// A : 角度
        /// </summary>
        internal _Err_jczMarkEzd2_ 旋转变换(double x, double y, double xCenter, double yCenter, double A)
        {

            _Err_jczMarkEzd2_ nErr = (_Err_jczMarkEzd2_)JczLmc.旋转变换(x, y, xCenter, yCenter, A * Math.PI / 180d);
            return nErr;
        }

        internal IntPtr 获取_图形Intptr(int width, int height)
        {
            IntPtr pr = JczLmc.获取图象2(width, height);
            return pr;
        }


        [DllImport("gdi32.dll")]
        internal static extern bool DeleteObject(IntPtr hObject);

        internal readonly object _lock = new object();
        internal Bitmap 获取_图形(int width, int height)
        {
            lock (_lock)
            {

                IntPtr ptr = JczLmc.获取图象2(width, height);
                Bitmap bmp = null;

                try
                {
                    bmp = Image.FromHbitmap(ptr);
                }
                finally
                {
                    // 只要 Image.FromHbitmap 成功，就应该释放原句柄
                    DeleteObject(ptr);
                }



                return bmp;
            }
        }


        internal _Err_jczMarkEzd2_ 修改_指定对象的内容(string strName, string strDate)
        {
            int nErr = JczLmc.修改指定对象的内容(strName, strDate);
            return (_Err_jczMarkEzd2_)nErr;
        }


        internal _Err_jczMarkEzd2_ 获取_对象名称(int 对象索引, ref string Name, int lenght = 255)
        {
            StringBuilder str = new StringBuilder(lenght);
            int nErr = JczLmc.获取指定序号的对象名称(对象索引, str);
            Name = str.ToString();
            return (_Err_jczMarkEzd2_)nErr;
        }

        internal _Err_jczMarkEzd2_ 获取_对象内容(string strName, ref string strDate, int lenght = 255)
        {
            StringBuilder xt = new StringBuilder(lenght);
            int nErr = JczLmc.获取指定对象的内容(strName, xt);
            strDate = xt.ToString();
            return (_Err_jczMarkEzd2_)nErr;
        }


        internal _Err_jczMarkEzd2_ 扩展轴_使能(bool 轴0使能, bool 轴1使能)
        {
            int nErr = JczLmc.扩展轴_使能(轴0使能, 轴1使能);
            return (_Err_jczMarkEzd2_)nErr;
        }

        internal _Err_jczMarkEzd2_ 扩展轴_回零(int 轴索引)
        {
            int nErr = JczLmc.扩展轴_回零(轴索引);
            return (_Err_jczMarkEzd2_)nErr;
        }

        internal _Err_jczMarkEzd2_ 扩展轴_到指定坐标(int 轴索引, double 坐标)
        {
            int nErr = JczLmc.扩展轴_移动到指定坐标(轴索引, 坐标);
            return (_Err_jczMarkEzd2_)nErr;
        }

        internal _Err_jczMarkEzd2_ 扩展轴_到指定脉冲位置(int 轴索引, int 脉冲数)
        {
            int nErr = JczLmc.扩展轴_移动到指定脉冲位置(轴索引, 脉冲数);
            return (_Err_jczMarkEzd2_)nErr;
        }

        internal double 扩展轴_获取当前坐标(int 轴索引)
        {
            return JczLmc.扩展轴_获取当前坐标(轴索引);
        }

        internal int 扩展轴_获取当前脉冲数坐标(int 轴索引)
        {
            return JczLmc.扩展轴_获取当前脉冲数坐标(轴索引);
        }

        internal _Err_jczMarkEzd2_ 设置_对象名称(int 对象索引, string 对象名称)
        {
            int nErr = JczLmc.设置指定索引的对象名称(对象索引, 对象名称);
            return (_Err_jczMarkEzd2_)nErr;
        }

        internal int 获取_对象总数()
        {
            return JczLmc.获取对象总数();
        }

        /// <summary>
        /// 重载
        /// </summary>
        /// <param name="对象名称"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        internal _Err_jczMarkEzd2_ 平移对象(string 对象名称, double x, double y)
        {
            int nErr = JczLmc.指定对象移动相对位置(对象名称, x, y);
            return (_Err_jczMarkEzd2_)nErr;
        }

        string Name_临时 = "!@#$% *****";

        /// <summary>
        /// 相对平移所有对象
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        internal bool 平移所有对象(double x, double y, out string msgErr)
        {
            msgErr = string.Empty;
            try
            {
                #region 平移对象

                _Err_jczMarkEzd2_ st = _Err_jczMarkEzd2_.成功;
                int s = 获取_对象总数();
                for (int i = 0; i < s; i++)
                {
                    string at = string.Empty;
                    获取_对象名称(i, ref at);

                    string Name = Name_临时 + i.ToString();
                    设置_对象名称(i, Name);

                    st = 平移对象(Name, x, y);
                    设置_对象名称(i, at);
                }

                #endregion

                return true;
            }
            catch (Exception ex)
            {
                msgErr = ex.Message;

                return false;
            }

        }

        internal bool 旋转所有对象(double A, out string msgErr)
        {
            try
            {
                int s = 获取_对象总数();

                double x = 0, y = 0, x1 = 0, y1 = 0, width = 0, height = 0;
                获取_图形中心坐标及尺寸(ref x, ref y, ref width, ref height, out msgErr);

                for (int i = 0; i < s; i++)
                {

                    string at = string.Empty;
                    获取_对象名称(i, ref at);
                    string Name = Name_临时 + i.ToString();
                    设置_对象名称(i, Name);
                    JczLmc.指定对象旋转(Name, x, y, A);
                    设置_对象名称(i, at);
                }

                获取_图形中心坐标及尺寸(ref x1, ref y1, ref width, ref height, out msgErr);

                double xEnd = x1 - x;
                double yEnd = y1 - y;

                平移所有对象(xEnd * -1.0, yEnd * -1.0, out msgErr);

                return true;

            }
            catch (Exception ex)
            {
                msgErr = ex.Message;
                return false;
            }

        }



        internal _Err_jczMarkEzd2_ 旋转对象(string 对象名称, double A)
        {
            double x = 0, y = 0, width = 0, height = 0;
            获取_对象中心坐标及尺寸(对象名称, ref x, ref y, ref width, ref height);
            int nErr = JczLmc.指定对象旋转(对象名称, x, y, A);

            return (_Err_jczMarkEzd2_)nErr;

        }

        internal _Err_jczMarkEzd2_ 获取_对象中心坐标及尺寸(string 对象名称, ref double xCenter, ref double yCenter, ref double width, ref double height)
        {

            #region 计算中心点

            //再获取这个对象的最大最小坐标
            double x小 = 0, y小 = 0, x大 = 0, y大 = 0, z = 0;
            _Err_jczMarkEzd2_ nErr = 获取_对象的最大最小坐标(对象名称, ref x小, ref y小, ref x大, ref y大, ref z);

            width = x大 - x小;
            height = y大 - y小;

            xCenter = (double)(((double)width / 2.0) + x小);
            yCenter = (double)(((double)height / 2.0) + y小);


            #endregion

            return nErr;

        }

        internal bool 获取_图形中心坐标及尺寸(ref double x, ref double y, ref double width, ref double height, out string msgErr)
        {
            msgErr = string.Empty;
            try
            {
                int s = 获取_对象总数();
                List<double> Lst_x小 = new List<double>();
                List<double> Lst_x大 = new List<double>();
                List<double> Lst_y小 = new List<double>();
                List<double> Lst_y大 = new List<double>();

                //List<double> Lst_z = new List<double>();
                double xMin = 0, xMax = 0, yMin = 0, yMax = 0, z = 0;

                #region 计算旋转中心点

                for (int i = 0; i < s; i++)
                {
                    //将所有的对象重新赋一个临时名称
                    string at = string.Empty;
                    获取_对象名称(i, ref at);
                    string Name = Name_临时 + i.ToString();
                    设置_对象名称(i, Name);

                    //再获取这个对象的最大最小坐标
                    double x小 = 0, y小 = 0, x大 = 0, y大 = 0;
                    获取_对象的最大最小坐标(Name, ref x小, ref y小, ref x大, ref y大, ref z);

                    //先将获取出来的值存起来
                    Lst_x小.Add(x小);
                    Lst_y小.Add(y小);

                    Lst_x大.Add(x大);
                    Lst_y大.Add(y大);
                    // Lst_z.Add(z);

                    //将对象名称改回去
                    设置_对象名称(i, at);
                }
                Lst_x小.Sort();
                Lst_x大.Sort();

                Lst_y小.Sort();
                Lst_y大.Sort();
                //Lst_z.Sort();

                xMin = Lst_x小[0];
                xMax = Lst_x大[Lst_x大.Count - 1];

                yMin = Lst_y小[0];
                yMax = Lst_y大[Lst_y大.Count - 1];

                //zMin = Lst_z[0];
                //zMax = Lst_z[Lst_z.Count - 1];

                width = xMax - xMin;
                height = yMax - yMin;

                x = (double)(((double)width / 2.0) + xMin);
                y = (double)(((double)height / 2.0) + yMin);


                #endregion

                return true;

            }
            catch (Exception ex)
            {
                msgErr = ex.Message;
                return false;
                // throw;
            }
        }

        internal _Err_jczMarkEzd2_ 获取_对象的最大最小坐标(string 对象名称, ref double x小, ref double y小, ref double x大, ref double y大, ref double z)
        {
            int nErr = JczLmc.获取指定对象的最大最小坐标(对象名称, ref x小, ref y小, ref x大, ref y大, ref z);
            return (_Err_jczMarkEzd2_)nErr;
        }

        internal bool 获取_指定对象的笔号(string 对象名称, out int 笔号)
        {
            bool rt = true;
            笔号 = 0;
            笔号 = JczLmc.获取指定对象笔号(对象名称);
            return rt;
        }

        internal _Err_jczMarkEzd2_ 获取_笔参数(int 笔号, out _笔参数_ info_笔参数)
        {
            info_笔参数 = new _笔参数_();

            int 加工次数 = 1;
            double 标刻速度 = 1000;
            double 功率 = 50;
            double 电流 = 10;
            int 频率 = 20000;
            double Q脉冲宽度 = 4;
            int 开光延时 = -100;
            int 关光延时 = 200;
            int 结束延时 = 500;
            int 拐角延时 = 100;
            double 跳转速度 = 8000;
            int 跳转位置延时 = 50;
            int 跳转距离延时 = 25;
            double 末点补偿 = 1;
            double 加速距离 = 1;
            double 打点时间 = 0.1;
            bool 脉冲点模式 = false;
            int 脉冲点数目 = 1;
            double 流水线速度 = 0;

            info_笔参数.笔号 = 笔号;


            int nErr = JczLmc.获取笔参数(笔号,
                                                 ref 加工次数,
                                                 ref 标刻速度,
                                                 ref 功率,
                                                 ref 电流,
                                                 ref 频率,
                                                 ref Q脉冲宽度,
                                                 ref 开光延时,
                                                 ref 关光延时,
                                                 ref 结束延时,
                                                 ref 拐角延时,
                                                 ref 跳转速度,
                                                 ref 跳转位置延时,
                                                 ref 跳转距离延时,
                                                 ref 末点补偿,
                                                 ref 加速距离,
                                                 ref 打点时间,
                                                 ref 脉冲点模式,
                                                 ref 脉冲点数目,
                                                 ref 流水线速度);


            info_笔参数.加工次数 = 加工次数;
            info_笔参数.标刻速度 = 标刻速度;
            info_笔参数.功率百分比 = 功率;
            info_笔参数.电流 = 电流;
            info_笔参数.频率 = 频率 / 1000;
            info_笔参数.Q脉冲宽度 = Q脉冲宽度;
            info_笔参数.开光延时 = 开光延时;
            info_笔参数.关光延时 = 关光延时;
            info_笔参数.结束延时 = 结束延时;
            info_笔参数.拐角延时 = 拐角延时;
            info_笔参数.流水线速度 = 流水线速度;

            return (_Err_jczMarkEzd2_)nErr;
        }


        /// <summary>
        /// iniSyS.写入前,请设置需要修改的参数
        /// </summary>
        internal _Err_jczMarkEzd2_ 设置_笔参数(_笔参数_ info_笔参数)
        {
            int nErr = JczLmc.设置笔参数(info_笔参数.笔号,
                               info_笔参数.加工次数,
                               info_笔参数.标刻速度,
                               info_笔参数.功率百分比,
                               info_笔参数.电流,
                               info_笔参数.频率 * 1000,
                               info_笔参数.Q脉冲宽度,
                               info_笔参数.开光延时,
                               info_笔参数.关光延时,
                               info_笔参数.结束延时,
                               info_笔参数.拐角延时,
                               info_笔参数.跳转速度,
                               info_笔参数.跳转位置延时,
                               info_笔参数.跳转距离延时,
                               info_笔参数.末点补偿,
                               info_笔参数.加速距离,
                              info_笔参数.打点时间,
                               info_笔参数.脉冲点模式,
                               info_笔参数.脉冲点数目,
                              info_笔参数.流水线速度);
            return (_Err_jczMarkEzd2_)nErr;
        }

        internal _Err_jczMarkEzd2_ 激光标刻(bool bFlyMark, bool is加工状态 = true)
        {
            if (is加工状态)
            {
                On_加工状态(_激光加工状态_.出激光标刻中);
            }
            int nErr = JczLmc.Mark(bFlyMark);
            _Err_jczMarkEzd2_ rt = (_Err_jczMarkEzd2_)nErr;
            if (is加工状态)
            {
                On_加工状态(_激光加工状态_.闲置);
            }

            return rt;
        }
        internal bool 标刻(bool bFlyMark, out string msgErr, bool is加工状态 = true)
        {
            _Err_jczMarkEzd2_ nErr = 激光标刻(bFlyMark, is加工状态);
            bool rt = ErrToMsg((int)nErr, out msgErr);
            return rt;
        }

        internal _Err_jczMarkEzd2_ Mark对象(string 对象)
        {
            int nErr = JczLmc.Mark指定对象(对象);
            return (_Err_jczMarkEzd2_)nErr;
        }

        internal _Err_jczMarkEzd2_ 停止标刻和红光()
        {
            _is连续加工 = false;
            int nErr = JczLmc.StopMark();
            return (_Err_jczMarkEzd2_)nErr;
        }

        internal _Err_jczMarkEzd2_ 获取_输入状态(out int InputPort)
        {
            int a = 0;
            int nErr = JczLmc.ReadInput(ref a);
            InputPort = a;
            return (_Err_jczMarkEzd2_)nErr;
        }

        internal _Err_jczMarkEzd2_ 获取_输出状态(out int outputPort)
        {
            int a = 0;
            int nErr = JczLmc.ReadOutput(ref a);
            outputPort = a;
            return (_Err_jczMarkEzd2_)nErr;
        }

        internal _Err_jczMarkEzd2_ 设置_输出状态(int outputPort)
        {
            int nErr = JczLmc.WriteOutput(outputPort);
            return (_Err_jczMarkEzd2_)nErr;
        }

        internal _Err_jczMarkEzd2_ 红光指示_外框()
        {
            int nErr = JczLmc.Red外框();
            return (_Err_jczMarkEzd2_)nErr;
        }

        internal _Err_jczMarkEzd2_ 红光指示_轮廓()
        {
            int nErr = JczLmc.Red轮廓();
            return (_Err_jczMarkEzd2_)nErr;
        }
        internal _Err_jczMarkEzd2_ 红光指示_指定对象(string 对象, bool 显示轮廓)
        {
            int nErr = JczLmc.Red指定对象(对象, 显示轮廓);
            return (_Err_jczMarkEzd2_)nErr;
        }

        internal _Err_jczMarkEzd2_ 标刻指定对象(string 对象)
        {
            int nErr = JczLmc.Mark指定对象(对象);
            return (_Err_jczMarkEzd2_)nErr;
        }

        int _OUTstatus = 0;
        /// <summary>
        /// 设置输出口状态
        /// </summary>
        /// <param name="port"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        internal _Err_jczMarkEzd2_ 输出IO(int port, bool state)
        {
            if (this._初始化状态 != _初始化状态_.已初始化)
            {
                return _Err_jczMarkEzd2_.未初始化;
            }
            else if (!Err_端口是否有效(port))
            {
                return _Err_jczMarkEzd2_.端口不在有效范置;
            }
            int status = new qfmain.进制().修改指定位状态_十进制(this._OUTstatus, port, state);
            return 设置_输出状态(status);
        }




        #endregion

        #region 封装... 连续加工 / 红光



        /// <summary>
        /// 
        /// </summary>
        /// <param name="status">轮廓或外框</param>
        /// <param name="msgErr"></param>
        /// <returns></returns>
        internal (bool s, string m) 加工_红光指示(_激光_红光指示_ status, bool is日志 = false)
        {
            string msgErr = string.Empty;
            if (!Err_未初始化(out msgErr, is日志) || !Err_加载激光模板中(out msgErr, is日志) ||
                !Err_出激光标刻中(out msgErr, is日志) || !Err_红光指示中(out msgErr, is日志) ||
                !Err_无可加工数据(out msgErr, is日志))
            {
                return (false, msgErr);
            }


            this._is连续加工 = true;
            On_加工状态(_激光加工状态_.红光指示光中);
            输出(this._参数.OUT.红光, true);

            while (this._isRun && this._is连续加工)
            {
                Thread.Sleep(this._参数.连续加工周期);
                if (!this._isRun || !this._is连续加工)
                {
                    break;
                }
                switch (status)
                {
                    case _激光_红光指示_.外框:
                        this.红光指示_外框();
                        break;
                    case _激光_红光指示_.轮郭:
                        this.红光指示_轮廓();
                        break;
                }
            }
            this._is连续加工 = false;
            输出(this._参数.OUT.红光, false);
            On_加工状态(_激光加工状态_.闲置);

            return (true, msgErr);
        }
        internal (bool s, string m) 加工_红光指示()
        {
            _激光_红光指示_ status = this._参数.红光指示轮廓 ? _激光_红光指示_.轮郭 : _激光_红光指示_.外框;
            return this.加工_红光指示(status);
        }
        internal bool 加工_连续标刻(out string msgErr, bool 是否飞行 = false)
        {
            if (!Err_未初始化(out msgErr) || !Err_加载激光模板中(out msgErr) ||
              !Err_出激光标刻中(out msgErr) || !Err_红光指示中(out msgErr) ||
              !Err_无可加工数据(out msgErr))
            {
                return false;
            }

            bool rt = true;
            this._is连续加工 = true;
            On_加工状态(_激光加工状态_.出激光标刻中);
            while (this._isRun && this._is连续加工)
            {
                Thread.Sleep(this._参数.连续加工周期);
                if (!this._isRun || !this._is连续加工)
                {
                    break;
                }
                rt = 标刻(是否飞行, out string msgErr1);
                if (!rt)
                {
                    msgErr = msgErr1;
                    break;
                }
            }
            this._is连续加工 = false;
            On_加工状态(_激光加工状态_.闲置);
            return rt;
        }

        internal async Task<(bool s, string m)> 调试_红光()
        {
            (bool s, string m) rt = (true, "");
            await Task.Run(() => { rt = 加工_红光指示(); });
            if (!rt.s)
            {
                MessageBox.Show(rt.m, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return rt;
        }
        internal async Task<bool> 调试_标刻()
        {
            if (!this.Err_红光指示中(out string msgErr, false) || !this.Err_出激光标刻中(out msgErr, false)
                || !this.Err_加载激光模板中(out msgErr, false)
                || !this.Err_加载激光模板中(out msgErr, false) || !this.Err_无可加工数据(out msgErr, false))
            {
                MessageBox.Show(msgErr, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            Task t0 = Task.Run(() =>
            {
                this.On_加工状态(_激光加工状态_.出激光标刻中);
                this.标刻(false, true);
                this.On_加工状态(_激光加工状态_.闲置);
            });
            await t0;
            return true;
        }


        #endregion


        #region Err

        public bool Err_加载激光模板中(out string msgErr, bool 是否日志 = true)
        {
            msgErr = "";
            if (this._激光加工状态 == _激光加工状态_.加载激光模板中)
            {
                msgErr = qfmain.Language_.Get语言("加载激光模板中");
                On_Log(false, msgErr);
                return false;
            }
            return true;
        }

        public bool Err_初始化中(out string msgErr, bool 是否日志 = true)
        {
            msgErr = "";
            if (this._初始化状态 == _初始化状态_.初始化中)
            {
                msgErr = qfmain.Language_.Get语言("初始化中");
                if (是否日志)
                {
                    On_Log(false, msgErr);
                }
                return false;
            }
            return true;
        }

        public bool Err_未初始化(out string msgErr, bool 是否日志 = true)
        {
            msgErr = "";
            if (this._初始化状态 != _初始化状态_.已初始化)
            {
                msgErr = qfmain.Language_.Get语言("未初始化");
                if (是否日志)
                {
                    On_Log(false, msgErr);
                }
                return false;
            }
            return true;
        }

        public bool Err_红光指示中(out string msgErr, bool 是否日志 = true)
        {
            msgErr = "";
            if (this._激光加工状态 == _激光加工状态_.红光指示光中)
            {
                msgErr = qfmain.Language_.Get语言("红光指示中");
                if (是否日志)
                {
                    On_Log(false, msgErr);
                }
                return false;
            }
            return true;
        }

        public bool Err_出激光标刻中(out string msgErr, bool 是否日志 = true)
        {
            msgErr = "";
            if (this._激光加工状态 == _激光加工状态_.出激光标刻中)
            {
                msgErr = qfmain.Language_.Get语言("出激光标刻中");
                if (是否日志)
                {
                    On_Log(false, msgErr);
                }
                return false;
            }
            return true;
        }

        public bool Err_无可加工数据(out string msgErr, bool 是否日志 = true)
        {
            msgErr = "";
            if (获取_对象总数() == 0)
            {
                msgErr = qfmain.Language_.Get语言("激光模板中无可加工数据");
                if (是否日志)
                {
                    On_Log(false, msgErr);
                }
                return false;
            }
            return true;
        }

        public bool Err_未加载激光模板(out string msgErr, bool 是否日志 = true)
        {
            msgErr = "";
            if (string.IsNullOrEmpty(this._Path_激光模板))
            {
                msgErr = qfmain.Language_.Get语言("未加载激光模板");
                {
                    On_Log(false, msgErr);
                }
                return false;
            }
            return true;
        }



        /// <summary>
        /// 这个只是判断,并非错误
        /// </summary>
        /// <param name="端口"></param>
        /// <returns></returns>
        internal bool Err_端口是否有效(int 端口)
        {
            return new IO_().端口是否有效(端口, _minPort, _maxPort);
        }

        /// <summary>
        /// DLL路径
        /// </summary>
        string path_MarkEzd = Environment.CurrentDirectory + "\\Ezd2\\Miks.dll";
        string file_Ezd2 = Environment.CurrentDirectory + "\\Ezd2";
        public bool Err_dll是否存在(out string msgErr, bool 是否日志 = true)
        {
            msgErr = string.Empty;
            if (!new qfmain.文件_文件夹().文件夹_是否存在(file_Ezd2, out msgErr))
            {
                msgErr = $"Not Ezd2";
                if (是否日志)
                {
                    On_Log(false, msgErr);
                }
                return false;
            }
            else if (!new 文件_文件夹().文件_是否存在(path_MarkEzd))
            {
                msgErr = qfmain.Language_.Get语言("DL故障");
                if (是否日志)
                {
                    On_Log(false, msgErr);
                }
                return false;
            }

            return true;
        }





        #endregion


        #region 窗体标题栏状态

        private _cfg_标题栏状态_[] 标题栏状态_加工状态()
        {
            string Name = "jcz2激光加工状态";
            string 名称 = qfmain.Language_.Get语言("打标卡");
            qfNet._cfg_标题栏状态_[] info = new qfNet._cfg_标题栏状态_[]
           {
              new  qfNet ._cfg_标题栏状态_(Name,$"{名称}{qfmain .Language_ .Get语言("闲置")}"  ,(int)_激光加工状态_ .闲置 ),
              new  qfNet ._cfg_标题栏状态_(Name,$"{名称}{qfmain .Language_ .Get语言("出激光标刻中")}"  ,(int)_激光加工状态_.出激光标刻中),
              new  qfNet ._cfg_标题栏状态_(Name  ,$"{名称}{qfmain .Language_ .Get语言("红指示光中")}" ,(int)_激光加工状态_.红光指示光中 ),
              new  qfNet ._cfg_标题栏状态_(Name  ,$"{名称}{qfmain .Language_ .Get语言("加载激光模板中")}" ,(int)_激光加工状态_.加载激光模板中  ),
           };
            _标题栏标题_加工状态 = info;
            return info;
        }
        private _cfg_标题栏状态_[] 标题栏状态_初始化状态()
        {
            string Name = "jcz2激光初始化状态";
            string 名称 = qfmain.Language_.Get语言("打标卡");
            qfNet._cfg_标题栏状态_[] info = new qfNet._cfg_标题栏状态_[]
           {
              new  qfNet ._cfg_标题栏状态_(Name,$"{名称}{qfmain .Language_ .Get语言("已初始化")}"  ,(int)_初始化状态_  .已初始化  ),
              new  qfNet ._cfg_标题栏状态_(Name,$"{名称}{qfmain .Language_ .Get语言("初始化中")}"  ,(int)_初始化状态_.初始化中 ),
              new  qfNet ._cfg_标题栏状态_(Name  ,$"{名称}{qfmain .Language_ .Get语言("未初始化")}" ,(int)_初始化状态_.未初始化  ),

           };
            _标题栏标题_初始化状态 = info;
            return info;
        }

        _cfg_标题栏状态_[] _标题栏标题_初始化状态 = new _cfg_标题栏状态_[0];
        _cfg_标题栏状态_[] _标题栏标题_加工状态 = new _cfg_标题栏状态_[0];

        #endregion

        #region MarkEzd...控件及窗体

        public void UserColor_图像(Control 控件)
        {
            控件.Controls.Clear();
            控件.Controls.Add(new ui_bitmap_jcz单头(this));
        }

        #endregion



        #region 事件

        public event Action<bool, string> Event_Log;
        public event Action<bool[]> Event_IO_IN;
        public event Action<bool[]> Event_IO_OUT;
        public event Action<_初始化状态_> Event_初始化状态;

        public event Action<_激光加工状态_> Event_加工状态;
        public event Action<string> Event_加载激光模板成功;
        public event Action<_激光_获取图像_> Event_获取图像;
        public event Action<qfNet._cfg_标题栏状态_[], _初始化状态_> Event_标题栏状态_初始化状态;
        public event Action<qfNet._cfg_标题栏状态_[], _激光加工状态_> Event_标题栏状态_加工状态;

        void On_Log(bool state, string msg)
        {
            Event_Log?.Invoke(state, msg);
        }
        void On_IO_IN(bool[] state)
        {
            Event_IO_IN?.Invoke(state);
        }
        void On_IO_OUT(bool[] state)
        {
            Event_IO_OUT?.Invoke(state);
        }
        internal void On_加工状态(_激光加工状态_ state)
        {
            On_标题栏状态_加工状态(_标题栏标题_加工状态, state);
            Event_加工状态?.Invoke(state);
        }
        void On_加载激光模板成功(string value)
        {
            Event_加载激光模板成功?.Invoke(value);
        }
        void On_获取图像(_激光_获取图像_ sate)
        {
            Event_获取图像?.Invoke(sate);
        }
        void On_标题栏状态_初始化状态(qfNet._cfg_标题栏状态_[] cfg, _初始化状态_ state)
        {
            Event_标题栏状态_初始化状态?.Invoke(cfg, state);
        }
        void On_标题栏状态_加工状态(qfNet._cfg_标题栏状态_[] cfg, _激光加工状态_ state)
        {
            this._激光加工状态 = state;
            Event_标题栏状态_加工状态?.Invoke(cfg, state);
        }


        bool is第一次初始化 = true;
        private readonly object _lock初始化 = new object();
        async Task On_初始化状态(_初始化状态_ state)
        {
            lock (this._lock初始化)
            {
                this._初始化状态 = state;
                On_标题栏状态_初始化状态(_标题栏标题_初始化状态, state);
                Event_初始化状态?.Invoke(state);
            }
            await Task.Run(() =>
               {
                   switch (state)
                   {
                       case _初始化状态_.已初始化:
                           //On_Log(true, Get语言("已初始化"));

                           if ((is第一次初始化 && this._参数.进入时加载激光模板) || !is第一次初始化)
                           {
                               this.LoadEzdFile(this._Path_激光模板_最后一次);
                           }
                           is第一次初始化 = false;

                           break;
                       case _初始化状态_.初始化中:
                           //On_Log(true, Get语言("初始化中"));
                           break;
                       case _初始化状态_.未初始化:
                           //  On_Log(false, Get语言("未初始化"));
                           break;
                   }
               });

        }


        #endregion

        #region 封装

        void 线程()
        {

            while (this._isRun)
            {
                Thread.Sleep(this._参数.线程周期);
                if (!this._isRun)
                {
                    break;
                }

                if (this._初始化状态 != _初始化状态_.已初始化)
                {
                    continue;
                }

                try
                {
                    获取_输入状态(out int inputPort);
                    获取_输出状态(out this._OUTstatus);
                    IO状态转换(inputPort, this._OUTstatus, out bool[] IN, out bool[] OUT);

                    _IO_InPutByte = (byte)inputPort;
                    _IO_OutPutByte = (byte)this._OUTstatus;
                    _IO_InPut = IN;
                    _IO_OutPut = OUT;
                    On_IO_IN(IN);
                    On_IO_OUT(OUT);
                }
                catch (Exception ex)
                {
                    On_Log(false, $"jczRun,Err,{ex.Message}");
                }

            }
        }

        #endregion

    }
}
