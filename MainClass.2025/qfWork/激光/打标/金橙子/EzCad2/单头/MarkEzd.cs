using qfmain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;


namespace qfWork
{
    public class MarkEzd : Language_
    {
        /// <summary>
        /// 打标卡初始化状态
        /// </summary>
        public _初始化状态_ _初始化状态 { set; get; } = _初始化状态_.未初始化;
        public _激光加工状态_ _激光加工状态 { set; get; } = _激光加工状态_.闲置;


        /// <summary>
        /// 激光模板
        /// </summary>
        public string _Path_ezd { set; get; } = string.Empty;
        public string _Path_ezd_最后一次 { set; get; } = string.Empty;

        public _激光参数_ _参数 { set; get; } = new _激光参数_();


        #region 方法


        public virtual _Err_jczMarkEzd2_ 初始化打标卡()
        {
            int nErr = (int)_Err_jczMarkEzd2_.未初始化;
            _初始化状态_ rt = _初始化状态_.初始化中;
            On_初始化状态(rt);
            if (EzCad2软件是否打开())
            {
                rt = _初始化状态_.未初始化;
                On_Log(false, $"{Get语言("发现EzCad进程")}");
                On_初始化状态(rt);

                return (_Err_jczMarkEzd2_)rt;
            }
            else
            {
                nErr = JczLmc.Initialize2(JczLmc.path_EzCad2, false);
                if (nErr == 0)
                {
                    rt = _初始化状态_.已初始化;
                    On_Log(true, $"{Get语言("已初始化")}");
                    On_初始化状态(rt);
                }
                else
                {
                    rt = _初始化状态_.未初始化;
                    On_Log(false, $"{Get语言("未初始化")},{JczLmc.ErrMsg(nErr)}");
                    释放打标卡(false);
                }
            }
            return (_Err_jczMarkEzd2_)nErr;
        }

        public virtual _Err_jczMarkEzd2_ 释放打标卡(bool 产生日志 = true)
        {

            On_初始化状态(_初始化状态_.未初始化);
            if (产生日志)
            {
                On_Log(false, $"{Get语言("已释放")}");
            }
            int nErr = JczLmc.Close();
            this._Path_ezd = string.Empty;
            return (_Err_jczMarkEzd2_)nErr;
        }

        /// <summary>
        /// jcz参数
        /// </summary>
        /// <returns></returns>
        public virtual _Err_jczMarkEzd2_ SetDevcfg(out string msgErr)
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
        public virtual _Err_jczMarkEzd2_ LoadEzdFile(string pathEzd, bool 是否刷新图像 = true)
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
                    this._Path_ezd = pathEzd;
                    this._Path_ezd_最后一次 = this._Path_ezd;
                    读写_最后一次ezdpath(0);
                    if (是否刷新图像)
                    {
                        获取_图像(_激光_获取图像_.获取);
                    }
                    On_加载Ezd成功(pathEzd);
                }
            }
            On_加工状态(_激光加工状态_.闲置);
            return (_Err_jczMarkEzd2_)nErr;
        }


        public virtual _Err_jczMarkEzd2_ 删除所有对象()
        {
            int nErr = JczLmc.删除所有对象();
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
        public virtual _Err_jczMarkEzd2_ 添加曲线(double[,] 曲线顶点数组, int 顶点数量, string 对象名称, int 笔号, int 是否填充)
        {
            int nErr = JczLmc.添加曲线(曲线顶点数组, 顶点数量, 对象名称, 笔号, 是否填充);
            return (_Err_jczMarkEzd2_)nErr;
        }

        public virtual _Err_jczMarkEzd2_ 删除指定对象(string 对象名称)
        {
            int nErr = JczLmc.删除指定对象(对象名称);
            return (_Err_jczMarkEzd2_)nErr;
        }

        public virtual _Err_jczMarkEzd2_ 保存ezd文件(string ezdpath)
        {
            int nErr = JczLmc.保存ezd(ezdpath);
            return (_Err_jczMarkEzd2_)nErr;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x坐标"></param>
        /// <param name="y坐标"></param>
        /// <param name="x旋转中心"></param>
        /// <param name="y旋转中心"></param>
        /// <param name="A"></param>
        /// <returns>反回值不准确,一般不采集</returns>
        public virtual int 旋转变换(double x坐标, double y坐标, double x旋转中心, double y旋转中心, double A)
        {
            return JczLmc.旋转变换(x坐标, y坐标, x旋转中心, y旋转中心, Math.PI / 180 * A);
        }

        public virtual IntPtr 获取_图形Intptr(int width, int height)
        {
            IntPtr pr = JczLmc.获取图象2(width, height);
            return pr;
        }


        [DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        private readonly object _lock = new object();
        public virtual Bitmap 获取_图形(int width, int height)
        {
            lock (_lock)
            {
                IntPtr pr = JczLmc.获取图象2(width, height);
                using (Bitmap img = Bitmap.FromHbitmap(pr))
                {
                    DeleteObject(pr);
                    GC.Collect();
                    return new Bitmap(img);
                }
            }
        }


        public virtual _Err_jczMarkEzd2_ 修改_指定对象的内容(string strName, string strDate)
        {
            int nErr = JczLmc.修改指定对象的内容(strName, strDate);
            return (_Err_jczMarkEzd2_)nErr;
        }


        public virtual _Err_jczMarkEzd2_ 获取_对象名称(int 对象索引, ref string Name)
        {
            StringBuilder str = new StringBuilder(255);
            int nErr = JczLmc.获取指定序号的对象名称(对象索引, str);
            return (_Err_jczMarkEzd2_)nErr;
        }

        public virtual _Err_jczMarkEzd2_ 获取_对象内容(string strName, ref string strDate)
        {
            StringBuilder xt = new StringBuilder(255);
            int nErr = JczLmc.获取指定对象的内容(strName, xt);
            strDate = xt.ToString();
            return (_Err_jczMarkEzd2_)nErr;
        }


        public virtual _Err_jczMarkEzd2_ 扩展轴_使能(bool 轴0使能, bool 轴1使能)
        {
            int nErr = JczLmc.扩展轴_使能(轴0使能, 轴1使能);
            return (_Err_jczMarkEzd2_)nErr;
        }

        public virtual _Err_jczMarkEzd2_ 扩展轴_回零(int 轴索引)
        {
            int nErr = JczLmc.扩展轴_回零(轴索引);
            return (_Err_jczMarkEzd2_)nErr;
        }

        public virtual _Err_jczMarkEzd2_ 扩展轴_到指定坐标(int 轴索引, double 坐标)
        {
            int nErr = JczLmc.扩展轴_移动到指定坐标(轴索引, 坐标);
            return (_Err_jczMarkEzd2_)nErr;
        }

        public virtual _Err_jczMarkEzd2_ 扩展轴_到指定脉冲位置(int 轴索引, int 脉冲数)
        {
            int nErr = JczLmc.扩展轴_移动到指定脉冲位置(轴索引, 脉冲数);
            return (_Err_jczMarkEzd2_)nErr;
        }

        public virtual double 扩展轴_获取当前坐标(int 轴索引)
        {
            return JczLmc.扩展轴_获取当前坐标(轴索引);
        }

        public virtual int 扩展轴_获取当前脉冲数坐标(int 轴索引)
        {
            return JczLmc.扩展轴_获取当前脉冲数坐标(轴索引);
        }

        public virtual _Err_jczMarkEzd2_ 设置_对象名称(int 对象索引, string 对象名称)
        {
            int nErr = JczLmc.设置指定索引的对象名称(对象索引, 对象名称);
            return (_Err_jczMarkEzd2_)nErr;
        }

        public virtual int 获取_对象总数()
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
        public virtual _Err_jczMarkEzd2_ 平移对象(string 对象名称, double x, double y)
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
        public virtual bool 平移所有对象(double x, double y, out string msgErr)
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

        public virtual bool 旋转所有对象(double A, out string msgErr)
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

        public virtual _Err_jczMarkEzd2_ 旋转对象(string 对象名称, double A)
        {
            double x = 0, y = 0, width = 0, height = 0;
            获取_对象中心坐标及尺寸(对象名称, ref x, ref y, ref width, ref height);
            int nErr = JczLmc.指定对象旋转(对象名称, x, y, A);

            return (_Err_jczMarkEzd2_)nErr;

        }

        public virtual _Err_jczMarkEzd2_ 获取_对象中心坐标及尺寸(string 对象名称, ref double x, ref double y, ref double width, ref double height)
        {



            #region 计算旋转中心点



            //再获取这个对象的最大最小坐标
            double x小 = 0, y小 = 0, x大 = 0, y大 = 0, z = 0;
            _Err_jczMarkEzd2_ nErr = 获取_对象的最大最小坐标(对象名称, ref x小, ref y小, ref x大, ref y大, ref z);

            width = x大 - x小;
            height = y大 - y小;

            x = (double)(((double)width / 2.0) + x小);
            y = (double)(((double)height / 2.0) + y小);


            #endregion

            return nErr;

        }

        public virtual bool 获取_图形中心坐标及尺寸(ref double x, ref double y, ref double width, ref double height, out string msgErr)
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

        public virtual _Err_jczMarkEzd2_ 获取_对象的最大最小坐标(string 对象名称, ref double x小, ref double y小, ref double x大, ref double y大, ref double z)
        {
            int nErr = JczLmc.获取指定对象的最大最小坐标(对象名称, ref x小, ref y小, ref x大, ref y大, ref z);
            return (_Err_jczMarkEzd2_)nErr;
        }

        public virtual bool 获取_指定对象的笔号(string 对象名称, out int 笔号)
        {
            bool rt = true;
            笔号 = 0;
            笔号 = JczLmc.获取指定对象笔号(对象名称);
            return rt;
        }

        public virtual _Err_jczMarkEzd2_ 获取笔参数(int 笔号, _激光jcz2_笔参数_ info_笔参数)
        {


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
        public virtual _Err_jczMarkEzd2_ 设置笔参数(int 笔号, _激光jcz2_笔参数_ info_笔参数)
        {
            info_笔参数.笔号 = 笔号;
            int nErr = JczLmc.设置笔参数(笔号,
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

        public virtual _Err_jczMarkEzd2_ 标刻(bool bFlyMark, bool is加工状态 = true)
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
        public virtual bool 标刻(bool bFlyMark, out string msgErr, bool is加工状态 = true)
        {
            _Err_jczMarkEzd2_ nErr = 标刻(bFlyMark, is加工状态);
            bool rt = ErrToMsg((int)nErr, out msgErr);
            return rt;
        }

        public virtual _Err_jczMarkEzd2_ Mark对象(string 对象)
        {
            int nErr = JczLmc.Mark指定对象(对象);
            return (_Err_jczMarkEzd2_)nErr;
        }

        public virtual _Err_jczMarkEzd2_ 停止标刻和红光()
        {
            _is连续加工 = false;
            int nErr = JczLmc.StopMark();
            return (_Err_jczMarkEzd2_)nErr;
        }

        public virtual _Err_jczMarkEzd2_ 获取_输入状态(out int InputPort)
        {
            int a = 0;
            int nErr = JczLmc.ReadInput(ref a);
            InputPort = a;
            return (_Err_jczMarkEzd2_)nErr;
        }

        public virtual _Err_jczMarkEzd2_ 获取_输出状态(out int outputPort)
        {
            int a = 0;
            int nErr = JczLmc.ReadOutput(ref a);
            outputPort = a;
            return (_Err_jczMarkEzd2_)nErr;
        }

        public virtual _Err_jczMarkEzd2_ 设置_输出状态(int outputPort)
        {
            int nErr = JczLmc.WriteOutput(outputPort);
            return (_Err_jczMarkEzd2_)nErr;
        }

        public virtual _Err_jczMarkEzd2_ 红光指示_外框()
        {
            int nErr = JczLmc.Red外框();
            return (_Err_jczMarkEzd2_)nErr;
        }

        public virtual _Err_jczMarkEzd2_ 红光指示_轮廓()
        {
            int nErr = JczLmc.Red轮廓();
            return (_Err_jczMarkEzd2_)nErr;
        }
        public virtual _Err_jczMarkEzd2_ 红光指示_指定对象(string 对象, bool 显示轮廓)
        {
            int nErr = JczLmc.Red指定对象(对象, 显示轮廓);
            return (_Err_jczMarkEzd2_)nErr;
        }

        public virtual _Err_jczMarkEzd2_ 标刻指定对象(string 对象)
        {
            int nErr = JczLmc.Mark指定对象(对象);
            return (_Err_jczMarkEzd2_)nErr;
        }


        #endregion


        #region 连续加工 / 红光

        private bool _is连续加工 = false;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status">轮廓或外框</param>
        /// <param name="msgErr"></param>
        /// <returns></returns>
        public bool 加工_红光指示(_激光_红光指示_ status, out string msgErr)
        {
            if (!Err_未初始化(out msgErr) || !Err_加载激光模板中(out msgErr) ||
                !Err_出激光标刻中(out msgErr) || !Err_红光指示中(out msgErr) ||
                !Err_无可加工数据(out msgErr))
            {
                return false;
            }


            this._is连续加工 = true;
            On_加工状态(_激光加工状态_.红指示光中);
            输出(this._参数.OUT.红光, true);
            while (this.isRun && this._is连续加工)
            {
                Thread.Sleep(this._参数.连续加工周期);
                if (!this.isRun || !this._is连续加工)
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

            return true;
        }
        public bool 加工_红光指示(out string msgErr)
        {
            _激光_红光指示_ status = this._参数.红光指示轮廓 ? _激光_红光指示_.轮郭 : _激光_红光指示_.外框;
            return this.加工_红光指示(status, out msgErr);
        }


        public bool 加工_连续标刻(out string msgErr, bool 是否飞行 = false)
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
            while (this.isRun && this._is连续加工)
            {
                Thread.Sleep(this._参数.连续加工周期);
                if (!this.isRun || !this._is连续加工)
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
            msgErr = rt ? $"{errMsg}" : $"{Get语言("失败")},{errMsg}";
            return rt;
        }

        public void 读写参数(ushort model)
        {
            string path = qfmain.软件类.Files_Cfg.Files_Config + "\\jczCfg.dll";
            _激光参数_ info = this._参数;
            new qfmain.文件_文件夹().WriteReadIni<_激光参数_>(path, model, ref info, out string msgErr);
            this._参数 = info;

        }

        void 读写_最后一次ezdpath(ushort model)
        {
            string path = Environment.CurrentDirectory + "\\jczdf.crc";
            string pathEzd = this._Path_ezd_最后一次;
            new qfmain.文件_文件夹().WriteReadIni<string>(path, model, ref pathEzd, out string msgErr);
            if (!string.IsNullOrEmpty(pathEzd) && new qfmain.文件_文件夹().文件_是否存在(path))
            {
                this._Path_ezd_最后一次 = pathEzd;
            }
            else
            {
                this._Path_ezd_最后一次 = string.Empty;
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



        #region Err

        public virtual bool Err_加载激光模板中(out string msgErr)
        {
            msgErr = "";
            if (this._激光加工状态 == _激光加工状态_.加载激光模板中)
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

        public virtual bool Err_红光指示中(out string msgErr)
        {
            msgErr = "";
            if (this._激光加工状态 == _激光加工状态_.红指示光中)
            {
                msgErr = Get语言("红光指示中");
                On_Log(false, msgErr);
                return false;
            }
            return true;
        }

        public virtual bool Err_出激光标刻中(out string msgErr)
        {
            msgErr = "";
            if (this._激光加工状态 == _激光加工状态_.出激光标刻中)
            {
                msgErr = Get语言("出激光标刻中");
                On_Log(false, msgErr);
                return false;
            }
            return true;
        }

        public virtual bool Err_无可加工数据(out string msgErr)
        {
            msgErr = "";
            if (获取_对象总数() == 0)
            {
                msgErr = Get语言("激光模板中无可加工数据");
                On_Log(false, msgErr);
                return false;
            }
            return true;
        }

        public virtual bool Err_未加载激光模板(out string msgErr)
        {
            msgErr = "";
            if (string.IsNullOrEmpty(this._Path_ezd))
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
            return new IO类_().端口是否有效(端口, 0, 15);
        }

        /// <summary>
        /// DLL路径
        /// </summary>
        string path_MarkEzd = Environment.CurrentDirectory + "\\Ezd2\\Miks.dll";
        public virtual bool Err_dll是否存在(out string msgErr)
        {
            msgErr = string.Empty;
            if (!new 文件_文件夹().文件_是否存在(path_MarkEzd))
            {
                msgErr = Get语言("DL故障");
                On_Log(false, msgErr);
                return false;
            }

            return true;
        }





        #endregion


        #region 事件



        public event Action<string> Event_加载Ezd成功;
        void On_加载Ezd成功(string ezdPath)
        {
            Event_加载Ezd成功?.Invoke(ezdPath);
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

            Task.Run(() =>
            {
                switch (state)
                {
                    case _初始化状态_.已初始化:
                        //On_Log(true, Get语言("已初始化"));

                        if (!is第一次初始化 || this._参数.进入时加载激光模板)
                        {
                            this.LoadEzdFile(this._Path_ezd_最后一次);
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

            Event_初始化状态?.Invoke(state);
        }

        bool is第一次初始化 = true;

        public event Action<_激光_获取图像_> Event_获取图像;
        /// <summary>
        /// 触发获取图像事件
        /// </summary>
        /// <param name="state"></param>
        public virtual void 获取_图像(_激光_获取图像_ state)
        {
            if (this._初始化状态 != _初始化状态_.已初始化)
            {
                return;
            }
            Event_获取图像?.Invoke(state);
        }

        public event Action<bool[]> Event_IO_IN;
        public event Action<bool[]> Event_IO_OUT;
        void On_IO_IN(bool[] state)
        {
            Event_IO_IN?.Invoke(state);
        }
        void On_IO_OUT(bool[] state)
        {
            Event_IO_OUT?.Invoke(state);
        }


        public event Action<_激光加工状态_> Event_加工状态;
        void On_加工状态(_激光加工状态_ state)
        {
            this._激光加工状态 = state;
            Event_加工状态?.Invoke(state);
        }


        #endregion


        #region 封装

        void 线程Main()
        {

            while (this.isRun)
            {
                Thread.Sleep(this._参数.线程周期);
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
                    获取_输入状态(out int inputPort);
                    获取_输出状态(out this._OUTstatus);
                    IO状态转换(inputPort, this._OUTstatus, out bool[] IN, out bool[] OUT);
                    On_IO_IN(IN);
                    On_IO_OUT(OUT);

                    On_IO_IN(IN);
                    On_IO_OUT(OUT);

                }
                catch (Exception ex)
                {
                    On_Log(false, $"jczRun,Err,{ex.Message}");
                }

            }
        }

        bool isRun = true;

        bool _Isinistiall = false;
        public virtual void 初始化(bool 使能线程 = true)
        {
            On_初始化状态(_初始化状态_.未初始化);
            读写参数(1);
            读写_最后一次ezdpath(1);
            读EzCadName();
            if (使能线程)
            {
                new Thread(() => { 线程Main(); }) { IsBackground = true }.Start();
            }
            new Thread(() => { 线程_处理ezCad(); }) { IsBackground = true }.Start();

            _Isinistiall = true;
        }

        public virtual void 释放()
        {
            if (!_Isinistiall)
            {
                return;
            }

            isRun = false;
            释放打标卡(true);
        }

        int _OUTstatus = 0;
        /// <summary>
        /// 设置输出口状态
        /// </summary>
        /// <param name="port"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public _Err_jczMarkEzd2_ 输出(int port, bool state)
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


        /// <summary>
        /// 脉冲输出口状态
        /// </summary>
        /// <param name="port"></param>
        public void 输出(int port)
        {
            if (this._初始化状态 != _初始化状态_.已初始化 ||
                !Err_端口是否有效(port))
            {
                return;
            }


            输出(port, true);
            Thread.Sleep(this._参数.OUT.输出脉宽);
            输出(port, false);

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


        public async Task <bool> EzCad2软件_打开()
        {
         Task t0=   Task.Run(() =>
            {
                try
                {
                    string ezdPath_ = this._Path_ezd_最后一次;
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

        bool 是否第一次运行 = true;

        IntPtr EzCadHwnd = IntPtr.Zero;
        _Err_jczMarkEzd2_ rtJcz = _Err_jczMarkEzd2_.发现EZCAD在运行;





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

                                rtJcz = 初始化打标卡();
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


    }
}
