 
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace qf_Laser
{
    public class MultilineMarkEzd : qfmain. Language_
    {
        [DllImport("gdi32.dll")]
        internal static extern bool DeleteObject(IntPtr hObject);//`D_显示指定卡的图形用的函数

        #region 结构体


        public class info_笔参数_
        {
            public int 笔号 { get; set; } = 0;
            public int 加工次数 { get; set; } = 1;
            public double 标刻速度 { get; set; } = 1000.0;
            public double 功率 { get; set; } = 50.0;
            public double 电流 { get; set; } = 10.0;
            /// <summary>
            /// 单位为HZ,
            /// </summary>
            public int 频率 { get; set; } = 20000;
            public double Q脉冲宽度 { get; set; } = 4.0;
            public int 开光延时 { get; set; } = -100;
            public int 关光延时 { get; set; } = 200;
            public int 结束延时 { get; set; } = 500;
            public int 拐角延时 { get; set; } = 100;
            public double 跳转速度 { get; set; } = 8000.0;
            public int 跳转位置延时 { get; set; } = 50;
            public int 跳转距离延时 { get; set; } = 25;
            public double 末点补偿 { get; set; } = 1.0;
            public double 加速距离 { get; set; } = 1.0;
            public double 打点时间 { get; set; } = 0.1;
            public bool 脉冲点模式 { get; set; } = false;
            public int 脉冲点数目 { get; set; } = 1;
            public double 流水线速度 { get; set; } = 0.0;
            public bool 是否不使能打码 { get; set; } = false;
        }

        #endregion

        /// <summary>
        /// 所有的卡ID.未设置的,随机获取的
        /// </summary>
        public int[] _CardSN_原始 = new int[8];

        /// <summary>
        /// 所有的卡ID...已设置过的
        /// </summary>
        public int[] _CardID { set; get; } = new int[8];








        /***********************************************************/
        string _Files = qfmain.软件类.Files_Cfg.Files_Config + "\\ezdMultiline";


        void 初始化参数文件夹()
        {
            new qfmain.文件_文件夹().文件夹_新建(this._Files, out string msgErr);
        }

        /// <summary>
        /// model: 0:写,1:读...自定义设置
        /// </summary>
        /// <param name="model"></param>
        public virtual bool 读写参数_CardID索引码(ushort model, out string msgErr)
        {
            初始化参数文件夹();
            string path = this._Files + "\\cardsn.dll";

            if (!new qfmain.文件_文件夹().文件_是否存在(path))
            {
                List<int> lst = new List<int>();
                for (global::System.Int32 i = 0; i < 8; i++)
                {
                    int a = -1;
                    try
                    {
                        a = this._CardSN_原始[i];
                    }
                    catch (Exception ex)
                    {
                    }
                    lst.Add(a);
                }
                this._CardID = lst.ToArray();
                model = 0;
            }

            int[] info = this._CardID;
            bool rt = new qfmain.文件_文件夹().WriteReadIni(path, model, ref info, out msgErr);
            this._CardID = info;
            if (!rt)
            {
                On_Log(rt, msgErr);
            }
            return rt;

        }

        public virtual bool 读写参数_参数(ushort model, out string msgErr)
        {
            初始化参数文件夹();
            string path = this._Files + "\\sys.dll";
            List<_激光参数_> lst = new List<_激光参数_>();
            bool rt = true;
            msgErr = string.Empty;
            try
            {

                for (global::System.Int32 i = 0; i < 8; i++)
                {
                    lst.Add(new _激光参数_());
                }

                int a = 获取卡数量();
                if (this._lst_参数.Count > 0 && model == 0)
                {
                    for (int i = 0; i < a; i++)
                    {
                        lst[i] = this._lst_参数[i]._参数;
                    }
                }

                _激光参数_[] cfg = lst.ToArray();
                rt = new qfmain.文件_文件夹().WriteReadIni(path, model, ref cfg, out msgErr);


                for (int i = 0; i < this._lst_参数.Count; i++)
                {
                    this._lst_参数[i]._参数 = cfg[i];
                }

            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = $"cfg,{ex.Message}";
            }
            if (!rt)
            {
                On_Log(rt, msgErr);
            }
            return rt;
        }

        public virtual bool 读写参数_最后一次ezd文件(ushort model, out string msgErr)
        {
            bool rt = true;
            msgErr = string.Empty;
            初始化参数文件夹();
            string path = this._Files + "\\ezdend.dll";
            List<string> lst = new List<string>();
            try
            {

                for (global::System.Int32 i = 0; i < 8; i++)
                {
                    lst.Add(string.Empty);
                }

                int a = 获取卡数量();
                if (this._lst_参数.Count > 0 && model == 0)
                {
                    for (int i = 0; i < a; i++)
                    {
                        lst[i] = this._lst_参数[i]._Path_ezd_最后一次;
                    }
                }
                string[] cfg = lst.ToArray();
                rt = new qfmain.文件_文件夹().WriteReadIni(path, model, ref cfg, out msgErr);

                if (this._lst_参数.Count > 0)
                {
                    for (int i = 0; i < this._lst_参数.Count; i++)
                    {
                        this._lst_参数[i]._Path_ezd_最后一次 = cfg[i];
                    }
                }
            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = $"ezd,{ex.Message}";
            }
            if (!rt)
            {
                On_Log(rt, msgErr);
            }

            return rt;
        }


        bool 保存ezd最后一次的路径(int Cardindex, out string msgErr)
        {
            this._lst_参数[Cardindex]._Path_ezd_最后一次 = this._lst_参数[Cardindex]._Path_ezd;
            return 读写参数_最后一次ezd文件(0, out msgErr);
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

        void 初始化数据()
        {
            if (this._lst_参数.Count == 0)
            {
                for (global::System.Int32 i = 0; i < 8; i++)
                {
                    _cfg_参数_ info = new _cfg_参数_();
                    this._lst_参数.Add(info);

                }
            }
        }

        /***********************************************************/


        #region 卡ID


        /// <summary>
        /// 通过索引判断卡是否存在,不存在返回-1,存在返卡索引,即Cardindex      
        /// </summary>
        /// <param name="Cardindex"></param>
        /// <returns></returns>
        public virtual int 获取卡ID(int Cardindex)
        {
            int result = -1;
            try
            {
                int value = this._CardID[Cardindex];
                int[] beff = this._CardSN_原始;
                List<int> lst = this._CardSN_原始.ToList();
                result = lst.IndexOf(value);

            }
            catch (Exception)
            {
                result = -1;
            }
            return result;
        }

        internal virtual int[] 获取所有CardID()
        {
            int[] id = new int[8];

            for (int i = 0; i < id.Length; i++)
            {
                id[i] = -1;
            }

            JczLmc_Multiline.得到所有卡序号(ref id[0]);

            int[] cardSn = id;
            List<int> lstSn = new List<int>();
            for (int i = 0; i < cardSn.Length; i++)
            {
                if (cardSn[i] > -1)
                {
                    lstSn.Add(cardSn[i]);
                }
            }
            this._CardSN_原始 = lstSn.ToArray();
            return this._CardSN_原始;
        }

        /// <summary>
        /// -1:不存在
        /// </summary>
        /// <param name="Cardindex"></param>
        /// <returns></returns>
        public virtual int 判断卡ID是否存在(int Cardindex)
        {
            return 获取卡ID(Cardindex);
        }

        /// <summary>
        /// 这个是排序的,不需要了,反正也排不准
        /// </summary>
        /// <param name="R"></param>
        private void BubbleSort_(int[] R)
        {
            int i, j, temp; //交换标志 
            bool exchange;
            for (i = 0; i < R.Length; i++) //最多做R.Length-1趟排序 
            {
                exchange = false; //本趟排序开始前，交换标志应为假
                for (j = R.Length - 2; j >= i; j--)
                {
                    if (R[j + 1] < R[j]) //交换条件
                    {
                        temp = R[j + 1];
                        R[j + 1] = R[j];
                        R[j] = temp;
                        exchange = true; //发生了交换，故将交换标志置为真 
                    }
                }
                if (!exchange) //本趟排序未发生交换，提前终止算法 
                {
                    break;
                }
            }

        }


        #endregion



        #region 方法

        //**************************************************************

        /// <summary>
        /// 只是对打标卡初始化,一般只调用初始化_线程就可以了,成功读取所有参数
        /// </summary>
        /// <returns></returns>
        public virtual int 初始化打标卡()
        {
            On_初始化状态(_初始化状态_.初始化中);
            _初始化状态_ rt = _初始化状态_.未初始化;
            _Err_jczMarkEzd2_ nErr = _Err_jczMarkEzd2_.未初始化;
            初始化数据();
            if (new MarkEzd_().EzCad2软件是否打开())
            {
                On_Log(false, $"{Get语言("发现EzCad进程")}");
                On_初始化状态(_初始化状态_.未初始化);

                return (int)rt;
            }
            else
            {
                int nrt = JczLmc_Multiline.初始化(JczLmc.path_EzCad2 + "\\", false, 0);
                nErr = (_Err_jczMarkEzd2_)nrt;
                if (nrt == 0)
                {
                    rt = _初始化状态_.已初始化;
                    this._CardSN_原始 = this.获取所有CardID();
                    this.读写参数_CardID索引码(1, out string msgErr);
                    this._打标卡数量 = 获取卡数量();
                    On_Log(true, $"{Get语言("已初始化")},<count:{this._打标卡数量}>");
                    On_初始化状态(rt);
                }
                else
                {
                    rt = _初始化状态_.未初始化;
                    On_Log(false, $"{Get语言("未初始化")},{JczLmc.ErrMsg(nErr)}");
                    释放打标卡(false);
                }
            }

            return (int)nErr;
        }

        public virtual int 释放打标卡(bool 产生日志 = true)
        {
            On_初始化状态(_初始化状态_.未初始化);
            if (产生日志)
            {
                On_Log(false, $"{Get语言("已释放")}");
            }
            int rt = (int)_Err_jczMarkEzd2_.不明错误;

            try
            {
                rt = JczLmc_Multiline.Close();
            }
            catch (Exception ex)
            { 
                On_Log(false, $"{ex.Message}");
            }
             
            return rt;
        }

        /// <summary>
        ///  Cardindex : 索引,0~7
        /// </summary>
        /// <param name="Cardindex"></param>
        /// <returns></returns>
        public virtual int 参数SetDevCfg(int Cardindex)
        {
            int id = 获取卡ID(Cardindex);
            int rt = JczLmc_Multiline.参数SetDevCfg(id);
            return rt;
        }

        /// <summary>
        ///  Cardindex : 索引,0~7
        ///  注意,刷新图像次数多了容易崩
        /// </summary>
        /// <param name="Cardindex"></param>
        /// <returns></returns>
        public virtual int 加载ezd(int Cardindex, string ezdPath, bool 是否刷新图像)
        {
            int rt = (int)_Err_jczMarkEzd2_.未找到ezd文件;
            On_加工状态(Cardindex, _激光加工状态_.加载激光模板中);
            if (!new qfmain.文件_文件夹().文件_是否存在(ezdPath))
            {
                rt = (int)_Err_jczMarkEzd2_.未找到ezd文件;
            }
            else
            {
                int id = 获取卡ID(Cardindex);
                rt = JczLmc_Multiline.加载ezd(id, ezdPath);
                if (_Err_jczMarkEzd2_.成功 == (_Err_jczMarkEzd2_)rt)
                {
                    _lst_参数[Cardindex]._Path_ezd = ezdPath;
                    _lst_参数[Cardindex]._Path_ezd_最后一次 = ezdPath;
                    this.保存ezd最后一次的路径(0, out string msgErr);
                    if (是否刷新图像)
                    {
                        刷新图形(Cardindex, _激光_获取图像_.获取);
                    }
                }

            }

            On_加载Ezd(Cardindex, ezdPath, (_Err_jczMarkEzd2_)rt);
            On_加工状态(Cardindex, _激光加工状态_.闲置);

            return rt;
        }

        /// <summary>
        ///  Cardindex : 索引,0~7
        /// </summary>
        /// <param name="Cardindex"></param>
        /// <param name="ezdPath"></param>
        /// <returns></returns>
        public virtual int 保存ezd(int Cardindex, string ezdPath)
        {
            int id = 获取卡ID(Cardindex);
            return JczLmc_Multiline.保存ezd文件(id, ezdPath);
        }

        /// <summary>
        /// Config_Main.cardID[Cardindex]
        /// </summary>
        /// <param name="Cardindex"></param>
        /// <returns></returns>
        public virtual int 清空所有对象(int Cardindex)
        {
            int id = 获取卡ID(Cardindex);
            int rt = JczLmc_Multiline.清空所有对象(id);
            if (_Err_jczMarkEzd2_.成功 == (_Err_jczMarkEzd2_)rt)
            {
                this._lst_参数[Cardindex]._Path_ezd = string.Empty;
            }
            return rt;
        }

        private readonly object _lock = new object();
        /// <summary>
        ///    Cardindex : 索引,0~7
        /// </summary>   
        public virtual Bitmap 获取图像(int Cardindex, int bmpwidth, int bmpheight)
        {
            lock (_lock)
            {
                int id = 获取卡ID(Cardindex);
                // IntPtr pBmp = JczLmc_Multiline.GetPrevBitmap2(id, (int)handle, bmpwidth, bmpheight);
                IntPtr pBmp = JczLmc_Multiline.GetPrevBitmap2(id, 0, bmpwidth, bmpheight);
                using (Bitmap img = Bitmap.FromHbitmap(pBmp))
                {
                    DeleteObject(pBmp);
                    return new Bitmap(img);
                }
            }
        }


        /// <summary>
        ///   Cardindex : 索引,0~7
        /// </summary>
        /// <param name="Cardindex"></param>
        /// <param name="X方向移动距离"></param>
        /// <param name="Y方向移动距离"></param>
        /// <param name="X旋转中心坐标"></param>
        /// <param name="Y旋转中心坐标"></param>
        /// <param name="旋转角度"></param>
        /// <returns></returns>
        public virtual int 平移旋转_绝对坐标(int Cardindex, double X方向移动距离, double Y方向移动距离, double X旋转中心坐标, double Y旋转中心坐标, double 旋转角度)
        {
            int id = 获取卡ID(Cardindex);
            int rt = JczLmc_Multiline.设置数据库的所有对象的平移旋转(id, X方向移动距离, Y方向移动距离, X旋转中心坐标, Y旋转中心坐标, Math.PI / 180 * 旋转角度);
            return rt;
        }


        /// <summary>
        ///   Cardindex : 索引,0~7
        /// </summary>
        /// <param name="Cardindex"></param>
        /// <param name="对象索引"></param>
        /// <param name="内容"></param>
        /// <returns></returns>
        public virtual int 获取对象名称(int Cardindex, int 对象索引, ref string 内容)
        {
            int id = 获取卡ID(Cardindex);
            StringBuilder data = new StringBuilder(255);

            int rt = JczLmc_Multiline.获取指定序号的对象名称(id, 对象索引, data);
            内容 = data.ToString();

            return rt;



        }


        /// <summary>
        ///   Cardindex : 索引,0~7
        ///   <para>返回数量</para>
        /// </summary>
        /// <param name="Cardindex"></param> 
        /// <returns></returns>
        public virtual int 获取对象数量(int Cardindex)
        {
            int id = 获取卡ID(Cardindex);
            StringBuilder data = new StringBuilder(255);
            int rt = JczLmc_Multiline.获取对象总数(id);
            return rt;
        }


        /// <summary>
        ///   Cardindex : 索引,0~7
        /// </summary>
        /// <param name="Cardindex"></param>
        /// <param name="对象名称"></param>
        /// <param name="内容"></param>
        /// <returns></returns>
        public virtual int 获取指定对象的内容(int Cardindex, string 对象名称, ref string 内容)
        {
            int id = 获取卡ID(Cardindex);
            StringBuilder xt = new StringBuilder(255);
            int rt = JczLmc_Multiline.获取指定对象的内容(id, 对象名称, xt);
            内容 = xt.ToString();
            return rt;
        }


        /// <summary>
        ///   Cardindex : 索引,0~7
        /// </summary>
        /// <param name="Cardindex"></param>
        /// <param name="对象名称"></param>
        /// <param name="内容"></param>
        /// <returns></returns>
        public virtual int 修改指定对象的内容(int Cardindex, string 对象名称, string 内容)
        {
            int id = 获取卡ID(Cardindex);
            int rt = JczLmc_Multiline.修改指定对象的内容(id, 对象名称, 内容);
            return rt;
        }



        /// <summary>
        ///   Cardindex : 索引,0~7
        /// </summary>
        /// <param name="卡ID"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public virtual int 获取input(int Cardindex, ref ushort data)
        {
            int id = 获取卡ID(Cardindex);
            //  ushort data = 0;
            int rt = JczLmc_Multiline.读输入口状态(id, ref data);
            //str = System.Convert.ToString(data, 2);
            //str = str.PadLeft(16, '0');
            return rt;
        }


        /// <summary>
        ///   Cardindex : 索引,0~7
        /// </summary>
        /// <param name="Cardindex"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public virtual int 获取output(int Cardindex, ref ushort data)
        {
            int id = 获取卡ID(Cardindex);
            // ushort data = 0;
            int rt = JczLmc_Multiline.读输出口状态(id, ref data);
            //str = System.Convert.ToString(data, 2);
            //str = str.PadLeft(16, '0');
            return rt;
        }


        /// <summary>
        ///   Cardindex : 索引,0~7
        /// </summary>
        /// <param name="Cardindex"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual int 设置output(int Cardindex, int value)
        {
            int id = 获取卡ID(Cardindex);
            // int xt = System.Convert.ToInt32(str, 2);
            int rt = JczLmc_Multiline.写输出口(id, value);
            return rt;
        }


        /// <summary>
        ///  Cardindex : 索引,0~7 
        /// </summary>
        /// <param name="Cardindex"></param>
        /// <param name="对象名称"></param>
        /// <param name="旋转中心的X坐标"></param>
        /// <param name="旋转中心的Y坐标"></param>
        /// <param name="旋转角度"></param>
        /// <returns></returns>
        public virtual int 指定对象旋转(int Cardindex, string 对象名称, double 旋转中心的X坐标, double 旋转中心的Y坐标, double 旋转角度)
        {
            int id = 获取卡ID(Cardindex);
            // Math.PI / 180 * 旋转角度
            int rt = JczLmc_Multiline.指定对象旋转(id, 对象名称, 旋转中心的X坐标, 旋转中心的Y坐标, 旋转角度);
            return rt;
        }


        /// <summary>
        ///  Cardindex : 索引,0~7 
        /// </summary>
        /// <param name="Cardindex"></param>
        /// <param name="对象索引"></param>
        /// <param name="对象名称"></param>
        /// <returns></returns>
        public virtual int 设置对象名称(int Cardindex, int 对象索引, string 对象名称)
        {
            int id = 获取卡ID(Cardindex);
            int rt = JczLmc_Multiline.设置指定序号的对象名称(id, 对象索引, 对象名称);
            return rt;
        }


        /// <summary>
        ///   Cardindex : 索引,0~7 
        /// </summary>
        /// <param name="Cardindex"></param>
        /// <param name="对象名称"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public virtual int 平移指定对象_相对坐标(int Cardindex, string 对象名称, double x, double y)
        {
            int id = 获取卡ID(Cardindex);
            int rt = JczLmc_Multiline.指定对象移动相对位置(id, 对象名称, ref x, ref y);

            return rt;
        }


        /// <summary>
        ///   Cardindex : 索引,0~7 ... 处理了图像显示
        /// </summary>
        /// <param name="Cardindex"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public virtual bool 平移所有对象_相对坐标(int Cardindex, double x, double y, out string msgErr)
        {
            bool rt = true;
            msgErr = string.Empty;
            try
            {

                #region 平移对象

                int s = 获取对象数量(Cardindex);
                for (int i = 0; i < s; i++)
                {
                    string at = string.Empty;
                    获取对象名称(Cardindex, i, ref at);
                    string Name = "!@#$%^&*(*()(" + i.ToString();
                    设置对象名称(Cardindex, i, Name);
                    平移指定对象_相对坐标(Cardindex, Name, x, y);
                    设置对象名称(Cardindex, i, at);
                }

                #endregion


            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }

            return rt;
        }


        /// <summary>
        /// 
        /// Cardindex : 索引,0~7 ... 旋转所有对象,处理了图像显示
        /// </summary>
        /// <param name="Cardindex"></param> 
        virtual public bool 旋转所有对象(int Cardindex, double A, out string msgErr)
        {
            bool rt = true;
            msgErr = string.Empty;

            try
            {

                int s = 获取对象数量(Cardindex);
                double x小 = 0, y小 = 0, x大 = 0, y大 = 0, xMin = 0, xMax = 0, x = 0, yMin = 0, yMax = 0, y = 0, z = 0;

                #region 计算旋转中心点

                for (int i = 0; i < s; i++)
                {
                    string at = string.Empty;
                    获取对象名称(Cardindex, i, ref at);

                    string Name = "!@#$%^&*(*()(" + i.ToString();
                    设置对象名称(Cardindex, i, Name);

                    获取指定对象的最大最小坐标(Cardindex, Name, ref x小, ref y小, ref x大, ref y大, ref z);

                    设置对象名称(Cardindex, i, at);

                    if (i == 0)
                    {
                        xMin = x小;
                        xMax = x大;
                        yMin = y小;
                        yMax = y大;
                    }
                    else
                    {
                        if (x小 < xMin)
                        {
                            xMin = x小;
                        }
                        if (x大 > xMax)
                        {
                            xMax = x大;
                        }
                        if (y小 < yMin)
                        {
                            yMin = y小;
                        }
                        if (y大 > yMax)
                        {
                            yMax = y大;
                        }
                    }
                }


                x = (double)((xMax - xMin) / 2) + xMin;
                y = (double)((yMax - yMin) / 2) + yMin;




                #endregion


                #region 还原对象名称


                for (int i = 0; i < s; i++)
                {
                    string at = string.Empty;
                    获取对象名称(Cardindex, i, ref at);
                    string Name = "!@#$%^&*(*()(" + i.ToString();
                    设置对象名称(Cardindex, i, Name);
                    指定对象旋转(Cardindex, Name, x, y, A);
                    设置对象名称(Cardindex, i, at);

                }

                #endregion


            }
            catch (Exception ex)
            {
                msgErr = ex.Message;
                rt = false;
            }
            return rt;
        }

        public virtual int 获取笔参数(int Cardindex, int 笔号Int, out info_笔参数_ 笔参数)
        {

            int id = 获取卡ID(Cardindex);


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


            int rt = JczLmc_Multiline.获取笔号参数(id,
                                              笔号Int,
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
            笔参数 = new info_笔参数_();
            笔参数.笔号 = 笔号Int;
            笔参数.加工次数 = 加工次数;
            笔参数.标刻速度 = 标刻速度;
            笔参数.功率 = 功率;
            笔参数.电流 = 电流;
            笔参数.频率 = 频率;
            笔参数.Q脉冲宽度 = Q脉冲宽度;
            笔参数.开光延时 = 开光延时;
            笔参数.关光延时 = 关光延时;
            笔参数.结束延时 = 结束延时;
            笔参数.拐角延时 = 拐角延时;
            笔参数.流水线速度 = 流水线速度;

            笔参数.跳转速度 = 跳转速度;
            笔参数.跳转位置延时 = 跳转位置延时;
            笔参数.跳转距离延时 = 跳转距离延时;
            笔参数.末点补偿 = 末点补偿;
            笔参数.加速距离 = 加速距离;
            笔参数.打点时间 = 打点时间;
            笔参数.脉冲点模式 = 脉冲点模式;
            笔参数.脉冲点数目 = 脉冲点数目;
            笔参数.流水线速度 = 流水线速度;

            return rt;
        }

        public virtual int 设置笔参数(int Cardindex, info_笔参数_ 笔参数)
        {
            int id = 获取卡ID(Cardindex);

            int rt = JczLmc_Multiline.设置笔号参数(id,
                                        笔参数.笔号,
                                        笔参数.加工次数,
                                        笔参数.标刻速度,
                                        笔参数.功率,
                                        笔参数.电流,
                                        笔参数.频率,
                                        笔参数.Q脉冲宽度,
                                        笔参数.开光延时,
                                        笔参数.关光延时,
                                        笔参数.结束延时,
                                        笔参数.拐角延时,
                                        笔参数.跳转速度,
                                        笔参数.跳转位置延时,
                                        笔参数.跳转距离延时,
                                        笔参数.末点补偿,
                                        笔参数.加速距离,
                                        笔参数.打点时间,
                                        笔参数.脉冲点模式,
                                        笔参数.脉冲点数目,
                                        笔参数.流水线速度);



            return rt;
        }


        /// <summary>
        ///  Cardindex : 索引,0~7 
        /// </summary>
        /// <param name="卡号"></param>
        /// <param name="对象名称"></param>
        /// <param name="a"></param>
        /// <param name="pic图像"></param>
        public virtual int 旋转指定对象_以对象为中心_相对坐标(int Cardindex, string 对象名称, double a)
        {
            double x小 = 0, x大 = 0, y小 = 0, y大 = 0, z = 0, x中心 = 0, y中心 = 0;
            int rt = 获取指定对象的最大最小坐标(Cardindex, 对象名称, ref x小, ref y小, ref x大, ref y大, ref z);
            if (rt == 0)
            {
                x中心 = (double)((x大 - x小) / 2) + x小;
                y中心 = (double)((y大 - y小) / 2) + y小;
                rt = 指定对象旋转(Cardindex, 对象名称, x中心, y中心, a);
            }
            return rt;
        }


        /// <summary>
        ///  Cardindex : 索引,0~7 ...处理了图像显示的
        /// </summary> 
        public virtual bool 旋转指定对象_多对象_以选中所有对象的中心_相对坐标(int Cardindex, List<string> lst对象名称, double a, out string msgErr)
        {
            bool rt = true;
            msgErr = string.Empty;

            try
            {
                #region 旋转对象


                int s = lst对象名称.Count;

                double x小 = 0, y小 = 0, x大 = 0, y大 = 0, xMin = 0, xMax = 0, x = 0, yMin = 0, yMax = 0, y = 0, z = 0;

                #region 计算旋转中心点

                for (int i = 0; i < s; i++)
                {
                    string Name = lst对象名称[i];
                    获取指定对象的最大最小坐标(Cardindex, Name, ref x小, ref y小, ref x大, ref y大, ref z);

                    if (i == 0)
                    {
                        xMin = x小;
                        xMax = x大;
                        yMin = y小;
                        yMax = y大;
                    }
                    else
                    {
                        if (x小 < xMin)
                        {
                            xMin = x小;
                        }
                        if (x大 > xMax)
                        {
                            xMax = x大;
                        }
                        if (y小 < yMin)
                        {
                            yMin = y小;
                        }
                        if (y大 > yMax)
                        {
                            yMax = y大;
                        }
                    }
                }


                x = (double)((xMax - xMin) / 2) + xMin;
                y = (double)((yMax - yMin) / 2) + yMin;



                #endregion

                for (int i = 0; i < s; i++)
                {
                    string Name = lst对象名称[i];
                    指定对象旋转(Cardindex, Name, x, y, a);
                }


                #endregion

            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }

            return rt;
        }


        public virtual int 获取指定对象的最大最小坐标(int Cardindex, string 对象名称, ref double x小, ref double y小, ref double x大, ref double y大, ref double z)
        {
            int id = 获取卡ID(Cardindex);
            return JczLmc_Multiline.获取指定对象的最大最小坐标(id, 对象名称, ref x小, ref y小, ref x大, ref y大, ref z);
        }

        /// <summary>
        ///  Cardindex : 索引,0~7 ...
        /// </summary>
        /// <param name="Cardindex"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public virtual bool 获取整体图形尺寸(int Cardindex, ref double width, ref double height, out string msgErr)
        {
            bool rt = true;
            msgErr = string.Empty;

            try
            {

                int s = 获取对象数量(Cardindex);

                double x小 = 0, y小 = 0, x大 = 0, y大 = 0, xMin = 0, xMax = 0, x = 0, yMin = 0, yMax = 0, y = 0, z = 0;

                for (int i = 0; i < s; i++)
                {
                    string at = string.Empty;
                    获取对象名称(Cardindex, i, ref at);

                    string Name = "!@#$%^&*(*()(" + i.ToString();
                    设置对象名称(Cardindex, i, Name);
                    获取指定对象的最大最小坐标(Cardindex, Name, ref x小, ref y小, ref x大, ref y大, ref z);
                    设置对象名称(Cardindex, i, at);


                    if (i == 0)
                    {
                        xMin = x小;
                        xMax = x大;
                        yMin = y小;
                        yMax = y大;
                    }
                    else
                    {
                        if (x小 < xMin)
                        {
                            xMin = x小;
                        }
                        if (x大 > xMax)
                        {
                            xMax = x大;
                        }
                        if (y小 < yMin)
                        {
                            yMin = y小;
                        }
                        if (y大 > yMax)
                        {
                            yMax = y大;
                        }
                    }
                }


                width = xMax - xMin;
                height = yMax - yMin;


            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }

            return rt;

        }


        public virtual void 获取_图形中心坐标及尺寸(int Cardindex, ref double x, ref double y, ref double width, ref double height)
        {
            int num = 获取对象数量(Cardindex);
            double num2 = 0.0;
            double num3 = 0.0;
            double num4 = 0.0;
            double num5 = 0.0;
            double num6 = 0.0;
            double num7 = 0.0;
            double num8 = 0.0;
            double num9 = 0.0;
            double num10 = 0.0;
            for (int i = 0; i < num; i++)
            {
                string empty = string.Empty;
                获取对象名称(Cardindex, i, ref empty);
                string 对象名称 = "!@#$%^&*(*()(" + i.ToString();
                设置对象名称(Cardindex, i, 对象名称);
                获取指定对象的最大最小坐标(Cardindex, 对象名称, ref num2, ref num3, ref num4, ref num5, ref num10);
                设置对象名称(Cardindex, i, empty);
                if (i == 0)
                {
                    num6 = num2;
                    num7 = num4;
                    num8 = num3;
                    num9 = num5;
                }
                else
                {
                    if (num2 < num6)
                    {
                        num6 = num2;
                    }
                    if (num4 > num7)
                    {
                        num7 = num4;
                    }
                    if (num3 < num8)
                    {
                        num8 = num3;
                    }
                    if (num5 > num9)
                    {
                        num9 = num5;
                    }
                }
            }
            width = num7 - num6;
            height = num9 - num8;
            x = width / 2.0 + num6;
            y = height / 2.0 + num8;
        }

        public virtual void 获取_对象中心坐标及尺寸(string 对象名称, int Cardindex, ref double x, ref double y, ref double width, ref double height)
        {
            double num = 0.0;
            double num2 = 0.0;
            double num3 = 0.0;
            double num4 = 0.0;
            double num5 = 0.0;
            获取指定对象的最大最小坐标(Cardindex, 对象名称, ref num, ref num3, ref num2, ref num4, ref num5);
            width = num2 - num;
            height = num4 - num3;
            x = width / 2.0 + num;
            y = height / 2.0 + num3;
        }


        /// <summary>
        ///  Cardindex : 索引,0~7 ...
        /// </summary>
        /// <param name="Cardindex"></param>
        /// <param name="fly"></param>
        /// <returns></returns>
        public virtual int 标刻(int Cardindex, bool fly)
        {
            int id = 获取卡ID(Cardindex);
            return JczLmc_Multiline.Mark(id, fly);
        }

        /// <summary>
        ///  Cardindex : 索引,0~7 ...
        /// </summary>
        /// <param name="Cardindex"></param>
        /// <param name="fly"></param>
        /// <returns></returns>
        public virtual int 标刻对象(int Cardindex, string 对象)
        {
            int id = 获取卡ID(Cardindex);
            return JczLmc_Multiline.Mark指定对象(id, 对象);
        }

        /// <summary>
        ///   Cardindex : 索引,0~7 ...
        /// </summary>
        /// <param name="Cardindex"></param>
        /// <returns></returns>
        public virtual int 红光对象轮廓(int Cardindex, string 对象)
        {
            int id = 获取卡ID(Cardindex);
            int rt = JczLmc_Multiline.红光指示指定对象的轮廓(id, 对象);
            return rt;
        }

        /// <summary>
        ///   Cardindex : 索引,0~7 ...
        /// </summary>
        /// <param name="Cardindex"></param>
        /// <returns></returns>
        public virtual int 红光(int Cardindex)
        {
            int id = 获取卡ID(Cardindex);
            return JczLmc_Multiline.红光指示外框(id);
        }

        /// <summary>
        ///  Cardindex : 卡序号,0~7 ...
        /// <para>1:为空闲,0:加工中</para>
        /// </summary>
        public virtual int 获取卡工作状态(int Cardindex)
        {
            int index = 获取卡ID(Cardindex);
            return JczLmc_Multiline.获取卡是否为空闲状态(index);
        }


        /// <summary>
        ///   Cardindex : 索引,0~7 ...
        /// </summary>
        /// <param name="Cardindex"></param>
        /// <returns></returns>
        public virtual _Err_jczMarkEzd2_ 停止标刻和红光(int Cardindex)
        {
            _Err_jczMarkEzd2_ nerr = _Err_jczMarkEzd2_.不明错误;
            try
            {
                int id = 获取卡ID(Cardindex);
                nerr = (_Err_jczMarkEzd2_)JczLmc_Multiline.StopMark(id);
                this._lst_参数[Cardindex]._Is连续加工 = false;
            }
            catch (Exception ex)
            {
                On_Log_指定卡(Cardindex, false, ex.Message);
            }

            return nerr;
        }

        public virtual bool 标刻(int Cardindex, bool fly, out string msgErr)
        {
            bool rt = true;
            try
            {
                int nerr = 标刻(Cardindex, fly);
                rt = this.ErrToMsg(nerr, out msgErr);
            }
            catch (Exception ex)
            {
                msgErr = ex.Message;
                rt = false;
            }
            return rt;
        }


        #endregion



        #region 封装


        public int 获取卡数量()
        {
            List<int> lst = this._CardSN_原始.ToList();
            lst.Remove(-1);
            return lst.Count;
        }

        public void 刷新图形(int CardIndex,  _激光_获取图像_ state)
        {
            Event_刷新图像?.Invoke(CardIndex, state);
        }


        public virtual void 初始化(bool 使能线程 = true)
        {
            On_初始化状态(_初始化状态_.未初始化);
            初始化数据();
            读写参数_参数(1, out string msgErr);
            读写参数_最后一次ezd文件(1, out msgErr);
            读EzCadName();
            if (使能线程)
            {
                new Thread(() => { 线程Main(); }) { IsBackground = true }.Start();
            }
            new Thread(() => { 线程_处理ezCad(); }) { IsBackground = true }.Start();


            _Isinistiall = true;
        }

        bool _Isinistiall = false;
        public virtual void 释放()
        {
            if (!_Isinistiall)
            {
                return;
            }
            释放打标卡();
            isRun = false;
        }


        void 线程Main()
        {

            while (this.isRun)
            {
                Thread.Sleep(this._线程周期);
                if (!this.isRun)
                {
                    break;
                }

                if (this._初始化状态 != _初始化状态_.已初始化)
                {
                    continue;
                }

                try
                {

                    ushort input = 0;
                    ushort output = 0;

                    for (global::System.Int32 i = 0; i < this._打标卡数量; i++)
                    {
                        try
                        {
                            if (!Err_CardIndex是否有效(i))
                            {
                                continue;
                            }
                            else if (this._lst_参数[i]._激光加工状态 != _激光加工状态_.闲置)
                            {
                                continue;
                            }

                            获取input(i, ref input);
                            获取output(i, ref output);
                            IO状态转换(input, output, out bool[] IN, out bool[] OUT);

                            On_IO_IN(i, IN);
                            On_IO_OUT(i, OUT);

                            On_IO_IN(i, IN);
                            On_IO_OUT(i, OUT);
                            this._lst_参数[i].OutPort = output;

                        }
                        catch (Exception ex)
                        {
                            if (_is第一次)
                            {
                                _is第一次 = false;
                                On_Log(false, $"jczRun.IO,Err,{ex.Message}");
                            }
                        }
                    }


                }
                catch (Exception ex)
                {
                    if (_is第一次)
                    {
                        _is第一次 = false;
                        On_Log(false, $"jczRun,Err,{ex.Message}");
                    }
                }

            }
        }


        public bool 获取加工状态(int Cardindex)
        {
            int rt = this.获取卡工作状态(Cardindex);
            return rt == 1 ? false : true;
        }
        bool _is第一次 = true;

        bool isMark = true;
        public void 等待_标刻结束(int CardIndex, int 等待周期 = 100)
        {
            isMark = true;
            while (isRun && isMark)
            {
                Thread.Sleep(等待周期);
                if (!isRun && !isMark)
                {
                    break;
                }
                else if (!获取加工状态(CardIndex))
                {
                    break;
                }

            }
        }

        /// <summary>
        /// 停止等待标刻结束
        /// </summary>
        public void 等待_标刻结束_停止()
        {
            isMark = false;
        }


        public bool 输出(int Cardindex, int 端口, bool 状态)
        {

            if (!this.Err_端口是否有效(端口))
            {
                return true;
            }
            int a = new qfmain.进制().修改指定位状态_十进制(this._lst_参数[Cardindex].OutPort, 端口, 状态);
            _Err_jczMarkEzd2_ nerr = (_Err_jczMarkEzd2_)设置output(Cardindex, a);
            if (nerr == _Err_jczMarkEzd2_.成功)
            {
                this._lst_参数[Cardindex].OutPort = a;
            }

            return nerr == _Err_jczMarkEzd2_.成功 ? true : false;

        }





        #endregion


        #region 连续加工 / 红光

        /// <summary>
        ///  
        /// </summary>
        /// <param name="status">轮廓或外框</param>
        /// <param name="msgErr"></param>
        /// <returns></returns>
        public bool 连续_红光指示(int Cardindex, out string msgErr, bool 是否调试 = false)
        {
            if (!Err_未初始化(out msgErr) || !Err_加载激光模板中(Cardindex, out msgErr) ||
                !Err_出激光标刻中(Cardindex, out msgErr) || !Err_红光指示中(Cardindex, out msgErr) ||
                !Err_无可加工数据(Cardindex, out msgErr))
            {
                return false;
            }


            this._lst_参数[Cardindex]._Is连续加工 = true;
            输出(Cardindex, this._lst_参数[Cardindex]._参数.OUT.红光, true);
            if (是否调试)
            {
                Thread.Sleep(this._lst_参数[Cardindex]._参数.线程周期 * 2);
            }
            else
            {
                Thread.Sleep(100);
            }
            On_加工状态(Cardindex, _激光加工状态_.红指示光中);

            while (this.isRun && this._lst_参数[Cardindex]._Is连续加工)
            {
                Thread.Sleep(this._lst_参数[Cardindex]._参数.连续加工周期);
                if (!this.isRun || !this._lst_参数[Cardindex]._Is连续加工)
                {
                    break;
                }
                this.红光(Cardindex);
            }
            Thread.Sleep(100);
            输出(Cardindex, this._lst_参数[Cardindex]._参数.OUT.红光, false);
            this._lst_参数[Cardindex]._Is连续加工 = false;
            On_加工状态(Cardindex, _激光加工状态_.闲置);

            return true;
        }

        public bool 连续_连续标刻(int Cardindex, out string msgErr, bool 是否飞行 = false)
        {
            if (!Err_未初始化(out msgErr) || !Err_加载激光模板中(Cardindex, out msgErr) ||
              !Err_出激光标刻中(Cardindex, out msgErr) || !Err_红光指示中(Cardindex, out msgErr) ||
              !Err_无可加工数据(Cardindex, out msgErr))
            {
                return false;
            }

            bool rt = true;
            this._lst_参数[Cardindex]._Is连续加工 = true;
            On_加工状态(Cardindex, _激光加工状态_.出激光标刻中);
            while (this.isRun && this._lst_参数[Cardindex]._Is连续加工)
            {
                Thread.Sleep(this._lst_参数[Cardindex]._参数.连续加工周期);
                if (!this.isRun || !this._lst_参数[Cardindex]._Is连续加工)
                {
                    break;
                }
                On_加工状态(Cardindex, _激光加工状态_.出激光标刻中);
                标刻(Cardindex, 是否飞行);
                等待_标刻结束(Cardindex);
                On_加工状态(Cardindex, _激光加工状态_.闲置);
            }
            this._lst_参数[Cardindex]._Is连续加工 = false;
            On_加工状态(Cardindex, _激光加工状态_.闲置);
            return rt;
        }



        #endregion





        /// <summary>
        /// 打标卡初始化状态
        /// </summary>
        public _初始化状态_ _初始化状态 { set; get; } = _初始化状态_.未初始化;
        public List<_cfg_参数_> _lst_参数 = new List<_cfg_参数_>();
        public int _打标卡数量 = 0;
        public string _EzCad软件名称 { set; get; } = "EzCad2";
        public int _线程周期 { set; get; } = 100;

        /// <summary>
        /// 参数结构
        /// </summary>
        public class _cfg_参数_
        {
            public _激光参数_ _参数 { set; get; } = new _激光参数_();
            public _激光加工状态_ _激光加工状态 { set; get; } = _激光加工状态_.闲置;

            /// <summary>
            /// 激光模板
            /// </summary>
            public string _Path_ezd { set; get; } = string.Empty;
            public string _Path_ezd_最后一次 { set; get; } = string.Empty;
            public int OutPort { set; get; } = 0;

            public bool _Is连续加工 { set; get; } = false;

        }







        /// <summary>
        /// 错误代码转换
        /// </summary>
        /// <param name="nErr"></param>
        /// <returns></returns>
        public _Err_jczMarkEzd2_ intToErr(int nErr)
        {
            return (_Err_jczMarkEzd2_)nErr;
        }

        #region EzCad

        enum _EzCad2打开状态_
        {
            None = 0,
            打开 = 1,
            已处理 = -1,
        }


        _EzCad2打开状态_ EzCad2打开状态 = _EzCad2打开状态_.None;

        /// <summary>
        /// CardIndex=-1,表示直接打开软件
        /// </summary>
        /// <param name="CardIndex"></param>
        public async Task<bool> EzCad2软件_打开(int CardIndex)
        {
            Task t0 = Task.Run(() =>
               {
                   try
                   {
                       string ezdPath_ = "";
                       if (CardIndex != -1)
                       {
                           try
                           {
                               ezdPath_ = this._lst_参数[CardIndex]._Path_ezd_最后一次;
                           }
                           catch (Exception)
                           {
                               ezdPath_ = "";
                           }
                       }

                       EzCad2打开状态 = _EzCad2打开状态_.已处理;
                       if (this._初始化状态 == _初始化状态_.已初始化)
                       {
                           this.释放打标卡(true);
                           Thread.Sleep(1000);
                       }

                       if (CardIndex == -1
                       || string.IsNullOrEmpty(ezdPath_)
                       || !new qfmain.文件_文件夹().文件_是否存在(ezdPath_))
                       {
                           new qfmain.文件_文件夹().文件_打开(JczLmc.path_EzCad2 + $"\\{this._EzCad软件名称}.exe", out string msgErr, "", ProcessWindowStyle.Maximized);

                       }
                       else
                       {
                           new qfmain.文件_文件夹().文件_打开(ezdPath_, out string msgErr, "", ProcessWindowStyle.Maximized);
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

            await t0;
            return true;
        }


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



        IntPtr EzCadHwnd = IntPtr.Zero;
        _Err_jczMarkEzd2_ rtJcz = _Err_jczMarkEzd2_.发现EZCAD在运行;


        bool isRun = true;
        void 线程_处理ezCad()
        {
            IntPtr EzCadHwnd = IntPtr.Zero;
            _Err_jczMarkEzd2_ rtJcz = _Err_jczMarkEzd2_.发现EZCAD在运行;

            while (this.isRun)
            {
                Thread.Sleep(1000);
                if (!this.isRun)
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

                                rtJcz = (_Err_jczMarkEzd2_)this.初始化打标卡();
                                EzCad2打开状态 = _EzCad2打开状态_.已处理;
                                // this.is第一次初始化_EzCad = true;
                            }
                            else if (this.is第一次初始化_EzCad)
                            {
                                this.is第一次初始化_EzCad = false;
                                rtJcz = _Err_jczMarkEzd2_.未初始化;
                                On_Log(false, $"{Get语言("发现EzCad进程")}");
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
                else if (new qfmain.进程().进程是否存在(s, out string msgErr))
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


        #region 事件

        /// <summary>
        /// 参数(卡索引,_刷新图像_)
        /// </summary>
        public event Action<int, _激光_获取图像_> Event_刷新图像;


        public event Action<int, string, _Err_jczMarkEzd2_> Event_加载Ezd;
        public void On_加载Ezd(int CardIndex, string ezdPath, _Err_jczMarkEzd2_ state)
        {
            Event_加载Ezd?.Invoke(CardIndex, ezdPath, state);
        }

        public event Action<bool, string> Event_Log;
        public void On_Log(bool state, string msg)
        {
            Event_Log?.Invoke(state, msg);
        }

        public event Action<_初始化状态_> Event_初始化状态;
        void On_初始化状态(_初始化状态_ state)
        {

            this._初始化状态 = state;
            Event_初始化状态?.Invoke(state);
        }

        bool is第一次初始化 = true;



        public event Action<int, bool[]> Event_IO_IN;
        public event Action<int, bool[]> Event_IO_OUT;
        void On_IO_IN(int CardIndex, bool[] state)
        {
            Event_IO_IN?.Invoke(CardIndex, state);
        }
        void On_IO_OUT(int CardIndex, bool[] state)
        {
            Event_IO_OUT?.Invoke(CardIndex, state);
        }


        public event Action<int, _激光加工状态_> Event_加工状态;
        public void On_加工状态(int CardIndex, _激光加工状态_ state)
        {
            this._lst_参数[CardIndex]._激光加工状态 = state;
            Event_加工状态?.Invoke(CardIndex, state);
        }

        /// <summary>
        /// 参数(卡索引,状态,信息)
        /// </summary>
        public event Action<int, bool, string> Event_Log_指定卡;
        public void On_Log_指定卡(int CardIndex, bool state, string msg)
        {
            Event_Log_指定卡?.Invoke(CardIndex, state, msg);
        }




        #endregion

        #region Err


        /// <summary>
        /// 返回值转换成消息
        /// </summary>
        /// <param name="nErr"></param>
        /// <param name="msgErr"></param>
        /// <returns></returns>
        public bool ErrToMsg(int nErr, out string msgErr)
        {
            bool rt = nErr == 0 ? true : false;

            string errMsg = JczLmc_Multiline.ErrMsg(nErr);
            msgErr = rt ? $"{errMsg}" : $"{Get语言("失败")},{errMsg}";
            return rt;
        }


        public virtual bool Err_加载激光模板中(int CardIndex, out string msgErr)
        {
            msgErr = "";
            if (this._lst_参数[CardIndex]._激光加工状态 == _激光加工状态_.加载激光模板中)
            {
                msgErr = Get语言("加载激光模板中");
                On_Log(false, msgErr);
                return false;
            }
            return true;
        }

        public virtual bool Err_未初始化(out string msgErr)
        {
            msgErr = "";
            if (this._初始化状态 != _初始化状态_.已初始化)
            {
                msgErr = Get语言("未初始化");
                On_Log(false, msgErr);
                return false;
            }
            return true;
        }

        public virtual bool Err_红光指示中(int CardIndex, out string msgErr)
        {
            msgErr = "";
            if (this._lst_参数[CardIndex]._激光加工状态 == _激光加工状态_.红指示光中)
            {
                msgErr = Get语言("红光指示中");
                On_Log(false, msgErr);
                return false;
            }
            return true;
        }

        public virtual bool Err_出激光标刻中(int CardIndex, out string msgErr)
        {
            msgErr = "";
            if (this._lst_参数[CardIndex]._激光加工状态 == _激光加工状态_.出激光标刻中)
            {
                msgErr = Get语言("出激光标刻中");
                On_Log(false, msgErr);
                return false;
            }
            return true;
        }

        public virtual bool Err_无可加工数据(int CardIndex, out string msgErr)
        {
            msgErr = "";
            if (获取对象数量(CardIndex) == 0)
            {
                msgErr = Get语言("激光模板中无可加工数据");
                On_Log(false, msgErr);
                return false;
            }
            return true;
        }

        public virtual bool Err_未加载激光模板(int CardIndex, out string msgErr)
        {
            msgErr = "";
            if (string.IsNullOrEmpty(this._lst_参数[CardIndex]._Path_ezd))
            {
                msgErr = Get语言("未加载激光模板");
                On_Log(false, msgErr);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 这个只是判断,并非错误
        /// </summary>
        /// <param name="端口"></param>
        /// <returns></returns>
        public virtual bool Err_端口是否有效(int 端口)
        {
            return new IO_().端口是否有效(端口, 0, 15);
        }

        /// <summary>
        /// DLL路径
        /// </summary>
        string path_MarkEzd = Environment.CurrentDirectory + "\\Ezd2\\Miksdle.dll";
        public virtual bool Err_dll是否存在(out string msgErr)
        {
            msgErr = string.Empty;
            if (!new qfmain.文件_文件夹().文件_是否存在(path_MarkEzd))
            {
                msgErr = Get语言("DL故障");
                On_Log(false, msgErr);
                return false;
            }

            return true;
        }


        public virtual bool Err_CardIndex是否有效(int CardIndex)
        {
            if (CardIndex < 0 || CardIndex >= this._lst_参数.Count)
            {
                return false;
            }
            return true;
        }


        #endregion


        #region win

        public void Win_设置卡ID()
        {
            using (Form_jcz多头_卡ID设置 forms = new Form_jcz多头_卡ID设置(this))
            {
                forms.ShowDialog();
            }
        }

        public void Win_调试(int CardIndex)
        {
            using (Form_jcz单头_调试 forms = new Form_jcz单头_调试(null, this, CardIndex))
            {
                forms.ShowDialog();
            }
        }

        public void Win_设置激光参数(int CardIndex)
        {
            using (Form_jcz单头_设置 forms = new Form_jcz单头_设置(null, this, CardIndex))
            {
                forms.ShowDialog();
            }
        }


        /// <summary>
        /// OK为成功, None为不加载
        /// </summary>
        /// <param name="CardIndex"></param>
        /// <param name="File_默认文件夹"></param>
        /// <param name="msgErr"></param>
        /// <returns></returns>
        public DialogResult Win_打开(int CardIndex, string File_默认文件夹, out string msgErr)
        {
            msgErr = string.Empty;
            DialogResult dlt = DialogResult.None;
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "ezd|*.ezd";
            open.InitialDirectory = File_默认文件夹;
            dlt = open.ShowDialog();
            if (dlt == DialogResult.OK)
            {
                string ezdName = open.FileName;
                _Err_jczMarkEzd2_ nerr = (_Err_jczMarkEzd2_)加载ezd(CardIndex, ezdName, true);
                bool rt = nerr == _Err_jczMarkEzd2_.成功 ? true : false;
                msgErr = JczLmc_Multiline.ErrMsg(nerr);
                dlt = rt ? dlt : DialogResult.No;

                //On_加载Ezd(CardIndex, ezdName, nerr);

            }
            return dlt;
        }

        #endregion
    }
}
