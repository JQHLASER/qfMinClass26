using BarTender;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace qfWork
{
    /// <summary>
    /// Interop.BarTender.dll
    /// <para>注意版本,2021或2016调用各自版本的 dll</para>
    /// <para>2021及以前</para>
    /// </summary>
    internal class BarTender_
    {
        #region 结构


        /// <summary>
        /// 保存方式
        /// </summary>
        public enum _En_SaveOptions_
        {
            /// <summary>
            /// 如果文档被修改，提示用户是否保存
            /// </summary>
            PromptSave = 0,

            /// <summary>
            /// 丢弃所有修改 
            /// </summary>
            DoNotSaveChanges = 1,

            /// <summary>
            /// 保存对当前标签文档.btw的所有参数
            /// </summary>
            SaveChanges = 2,

        }

        /// <summary>
        /// 打印结果
        /// </summary>
        public enum _Enum_打印结果_
        {
            /// <summary>
            /// 成功
            /// </summary>
            打印成功 = 0,
            /// <summary>
            /// 超时
            /// </summary>
            打印超时 = 1,
            /// <summary>
            /// 失败
            /// </summary>
            打印失败 = 2
        }


        public class _cfg_Print_
        {
            /// <summary>
            /// 打印机名称为空时表示不指定
            /// </summary>
            public string 打印机名称 { set; get; } = "";
            public uint 打印张数 { set; get; } = 1;

            /// <summary>
            /// 如果btw模板中没有设置序列号,就无效
            /// </summary>
            public uint 每个序列号打印张数 { set; get; } = 1;

            public bool 等待打印完成 { set; get; } = true;

            public bool 修改变量内容失败报警 { set; get; } = false;

            /// <summary>
            /// 显示图像的分辨率
            /// </summary>
            public int Dpi { set; get; } = 600;

            public bool Is显示图像 { set; get; } = false;

            public _En_SaveOptions_ 保存方式 { set; get; } = _En_SaveOptions_.DoNotSaveChanges;
        }


        public class _cfg_Open_
        {

            public bool 修改变量内容失败报警 { set; get; } = false;

            /// <summary>
            /// 显示图像的分辨率
            /// </summary>
            public int Dpi { set; get; } = 600;

            public bool Is显示图像 { set; get; } = false;

            public _En_SaveOptions_ 保存方式 { set; get; } = _En_SaveOptions_.DoNotSaveChanges;
        }



        public class _cfg_变量信息_
        {
            /// <summary>
            /// 变量名称
            /// </summary>
            public string Name { set; get; }
            /// <summary>
            /// 变量内容
            /// </summary>
            public string Value { set; get; }
        }



        #endregion

        bool _isInistiall = false;
        public BarTender_()
        {
            _isInistiall = true;
        }
        BarTender.Application btApp = new BarTender.Application();

        /// <summary>
        /// imgPath : 图像路径,如果要显示btw图像,读出这张图像就可以了
        /// <para>Dpi:分辨率</para>
        /// <para>Err变量信息 : 行数大于0时表示有变量加载错误信息</para>
        /// </summary> 
        public (bool s, string m, string imgPath, _cfg_变量信息_[] Err变量信息) 打开(string path_btw, _cfg_Open_ cfg, _cfg_变量信息_[] 变量信息)
        {
            string imgPath = Environment.CurrentDirectory + "\\_print1.bmp";
            List<_cfg_变量信息_> lst = new List<_cfg_变量信息_>();
            try
            {
                string[] work = new string[]
                {
                  "加载变量",
                  "显示",
                };

                bool rt = true;
                string msg = string.Empty;
                Format btFormat = btApp.Formats.Open(path_btw);
                try
                {
                    foreach (var s in work)
                    {
                        if (!rt)
                        {
                            break;
                        }
                        else if (s == "加载变量")
                        {
                            #region 加载变量

                            foreach (var item in 变量信息)
                            {
                                try
                                {
                                    btFormat.SetNamedSubStringValue(item.Name, item.Value);
                                }
                                catch (Exception ex)
                                {
                                    #region 传入错误时..一般变量不存在时会提示 

                                    if (!cfg.修改变量内容失败报警)
                                    {
                                        rt = false;
                                        msg = ex.Message;
                                        lst.Add(item);
                                    }

                                    #endregion
                                }
                            }

                            #endregion
                        }
                        else if (s == "显示")
                        {
                            #region 显示

                            (bool s, string m, string imgPath) rts = 显示(btFormat, cfg.Dpi);
                            rt = rts.s;
                            msg = rts.m;
                            imgPath = rts.imgPath;

                            #endregion
                        }

                    }

                }
                finally
                {     //关闭模板
                    btFormat.Close((BtSaveOptions)cfg.保存方式);
                    // 退出Bartender应用程序并释放资源
                    Marshal.ReleaseComObject(btFormat);
                    btFormat = null;
                }

                return (rt, msg, imgPath, lst.ToArray());
            }
            catch (Exception ex)
            {
                return (false, ex.Message, imgPath, lst.ToArray());
            }

        }


        /// <summary>
        /// <para>Err变量信息 : 行数大于0时表示有变量加载错误信息</para>
        /// </summary> 
        /// <returns></returns>
        public (_Enum_打印结果_ s, string m, string imgPath, _cfg_变量信息_[] Err变量信息) 打印(string path_btw, _cfg_Print_ cfg, _cfg_变量信息_[] 变量信息)
        {
            string imgPath = Environment.CurrentDirectory + "\\_print1.bmp";
            List<_cfg_变量信息_> lst = new List<_cfg_变量信息_>();
            _Enum_打印结果_ rtPrint = _Enum_打印结果_.打印失败;

            string[] work = new string[]
            {
                 "设置打印机",
                 "加载变量",
                 "打印",
                 "显示",
            };


            try
            {
                bool rt = true;
                string msg = string.Empty;
                Format btFormat = btApp.Formats.Open(path_btw);
                try
                {


                    foreach (var s in work)
                    {
                        if (!rt)
                        {
                            break;
                        }
                        else if (s == "设置打印机")
                        {
                            #region 设置打印机

                            btFormat.IdenticalCopiesOfLabel = (int)cfg.打印张数;
                            btFormat.Printer = cfg.打印机名称;
                            btFormat.NumberSerializedLabels = (int)cfg.每个序列号打印张数;

                            #endregion
                        }
                        else if (s == "加载变量")
                        {
                            #region 加载变量

                            foreach (var item in 变量信息)
                            {
                                try
                                {
                                    btFormat.SetNamedSubStringValue(item.Name, item.Value);
                                }
                                catch (Exception ex)
                                {
                                    #region 传入错误时..一般变量不存在时会提示 

                                    if (!cfg.修改变量内容失败报警)
                                    {
                                        rt = false;
                                        msg = ex.Message;
                                        lst.Add(item);
                                    }

                                    #endregion
                                }
                            }

                            #endregion
                        }
                        else if (s == "打印")
                        {
                            #region 打印

                            BarTender.Messages btMsgs;

                            //开始打印
                            rtPrint = (_Enum_打印结果_)btFormat.Print(cfg.打印机名称, cfg.等待打印完成, -1, out btMsgs);

                            #endregion
                        }
                        else if (s == "显示")
                        {
                            #region 显示

                            if (!cfg.Is显示图像)
                            {
                                continue;
                            }

                            (bool s, string m, string imgPath) rts = 显示(btFormat, cfg.Dpi);
                            rt = rts.s;
                            msg = rts.m;
                            imgPath = rts.imgPath;

                            #endregion
                        }

                    }

                }
                finally
                {
                    //关闭模板
                    btFormat.Close((BtSaveOptions)cfg.保存方式);
                    // 退出Bartender应用程序并释放资源
                    Marshal.ReleaseComObject(btFormat);
                    btFormat = null;

                }

                return (rtPrint, msg, imgPath, lst.ToArray());
            }
            catch (Exception ex)
            {
                return (rtPrint, ex.Message, imgPath, lst.ToArray());
            }

        }

        public void 释放(_En_SaveOptions_ _En_SaveOptions = _En_SaveOptions_.DoNotSaveChanges)
        {
            if (!_isInistiall)
            {
                return;
            }

            btApp.Quit((BtSaveOptions)_En_SaveOptions);
            Marshal.ReleaseComObject(btApp);
            btApp = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Thread.Sleep(500);
            string jc = "BarTender";
            if (new qfmain.进程().进程是否存在(jc))
            {
                new qfmain.进程().结束指定进程(jc, out string msgErr);
            }
        }


        /// <summary>
        /// <para>Dpi:分辨率</para>
        /// </summary> 
        public (bool s, string m, string imagePath) 显示(Format btFormat, int Dpi)
        {
            //  ExportPrintPreviewToImage的第一个参数是输出的路径，第二个参数是文件名称，第三个参数是文件类型，第四个参数选择btColors24Bit，第五个参数是图片的大小，第六个参数是背景色(0是黑色)，第七个参数是不保存变更，第八个参数是否包含边缘、第九个参数是否包含边框，第十个参数是输出参数。
            string name = "_print.bmp";
            string name1 = "_print1.bmp";
            string name2 = "_print2.bmp";
            // pic.Image = null;
            new qfmain.文件_文件夹().文件_删除文件($"{Environment.CurrentDirectory}\\{name1}", out string msgErr);
            new qfmain.文件_文件夹().文件_删除文件($"{Environment.CurrentDirectory}\\{name2}", out msgErr);
            BarTender.Messages btMessages2;

            btFormat.ExportPrintPreviewToImage(Environment.CurrentDirectory, name, "bmp", BarTender.BtColors.btColors24Bit, Dpi, 0,
                BarTender.BtSaveOptions.btDoNotSaveChanges, true, true, out btMessages2);
            return (true, "", name1);
        }


        /// <summary>
        /// <para>0: 如果文档被修改，提示用户是否保存</para>
        /// <para>1: 丢弃所有修改</para>
        /// <para>2: 保存对当前标签文档.btw的所有参数</para>
        /// </summary> 
        public BtSaveOptions 转换SaveOption(_En_SaveOptions_ enum_)
        {
            return (BtSaveOptions)enum_;
        }

        public (bool rt, string m, Bitmap bitmap) 读出图像(string imagePath)
        {
            bool rt = new qfmain.Image_().读取图片文件(imagePath, out Bitmap img, out string msgErr);
            return (rt, msgErr, img);
        }


    }

}
