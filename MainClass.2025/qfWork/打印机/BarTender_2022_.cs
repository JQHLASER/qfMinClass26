
using mainclassqf;
using Microsoft.SqlServer.Server;
using Seagull.BarTender.Print;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;


namespace qfWork
{
    /// <summary>
    /// 2022及以上版本
    /// </summary>
    public class BarTender_Engine
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

            public bool Is修改变量内容失败报警 { set; get; } = false;

            public Size 图像大小 { set; get; } = new Size();

            public bool Is显示图像 { set; get; } = false;

            public _En_SaveOptions_ 保存方式 { set; get; } = _En_SaveOptions_.DoNotSaveChanges;
        }


        public class _cfg_Open_
        {
            public Size 图像大小 { set; get; } = new Size();
            public bool Is修改变量内容失败报警 { set; get; } = false;

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



        /// <summary>
        /// <para>0: 如果文档被修改，提示用户是否保存</para>
        /// <para>1: 丢弃所有修改</para>
        /// <para>2: 保存对当前标签文档.btw的所有参数</para>
        /// </summary> 
        public SaveOptions 转换SaveOption(_En_SaveOptions_ enum_)
        {
            return (SaveOptions)enum_;
        }


        public (_Enum_打印结果_ state, string msg, string imagePath, _cfg_变量信息_[] Err变量信息) 打印(string path_btw, _cfg_Print_ cfg, _cfg_变量信息_[] 变量信息)
        {
            string imagePath = $"{Environment.CurrentDirectory}\\_print.jpg";//临时中转的文件 ,防止文件损坏
            List<_cfg_变量信息_> lst = new List<_cfg_变量信息_>();
            _Enum_打印结果_ rtPrint = _Enum_打印结果_.打印失败;
            try
            {
                using (Engine engine = new Engine())
                {
                    string[] work = new string[]
                     {
                         "设置打印机",
                         "加载变量",
                         "打印",
                         "显示",
                     };

                    bool rt = true;
                    string msg = string.Empty;
                    engine.Start();
                    var doc = engine.Documents.Open(path_btw);
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

                                doc.PrintSetup.PrinterName = cfg.打印机名称;
                                doc.PrintSetup.IdenticalCopiesOfLabel = (int)cfg.打印张数;
                                doc.PrintSetup.NumberOfSerializedLabels = (int)cfg.每个序列号打印张数;

                                #endregion
                            }
                            else if (s == "加载变量")
                            {
                                #region 加载变量
                                // 给变量赋值
                                foreach (var v in 变量信息)
                                {
                                    try
                                    {
                                        doc.SubStrings[v.Name].Value = v.Value;
                                    }
                                    catch (Exception ex)
                                    {
                                        if (cfg.Is修改变量内容失败报警)
                                        {
                                            rt = false;
                                            msg = ex.Message;
                                            lst.Add(v);
                                        }
                                    }
                                }
                                #endregion
                            }
                            else if (s == "打印")
                            {
                                #region 打印
                                // 打印
                                rtPrint = (_Enum_打印结果_)doc.Print();

                                #endregion
                            }
                            else if (s == "显示")
                            {
                                #region 显示
                                if (!cfg.Is显示图像)
                                {
                                    continue;
                                }

                                //预览图像
                                doc.ExportImageToFile(imagePath, ImageType.JPEG, Seagull.BarTender.Print.ColorDepth.ColorDepth24bit, new Resolution(cfg.图像大小.Width, cfg.图像大小.Height), OverwriteOptions.Overwrite);


                                #endregion
                            }
                        }


                        doc.Close((SaveOptions)cfg.保存方式);
                        engine.Stop();

                    }
                    finally
                    {
                        doc?.Close((SaveOptions)cfg.保存方式);
                    }
                }

                return (rtPrint, "", imagePath, lst.ToArray());

            }
            catch (Exception ex)
            {
                return (rtPrint, ex.Message, imagePath, lst.ToArray());
            }
        }

        public (bool state, string msg, string imagePath, _cfg_变量信息_[] Err变量信息) 打开(string path_btw, _cfg_Open_ cfg, _cfg_变量信息_[] 变量信息)
        {
            string imagePath = $"{Environment.CurrentDirectory}\\_print.jpg";//临时中转的文件 ,防止文件损坏
            List<_cfg_变量信息_> lst = new List<_cfg_变量信息_>();
            try
            {
                using (Engine engine = new Engine())
                {
                    string[] work = new string[]
                     {
                         "加载变量",
                         "显示",
                     };

                    bool rt = true;
                    string msg = string.Empty;
                    engine.Start();
                    var doc = engine.Documents.Open(path_btw);
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
                                // 给变量赋值
                                foreach (var v in 变量信息)
                                {
                                    try
                                    {
                                        doc.SubStrings[v.Name].Value = v.Value;
                                    }
                                    catch (Exception ex)
                                    {
                                        if (cfg.Is修改变量内容失败报警)
                                        {
                                            rt = false;
                                            msg = ex.Message;
                                            lst.Add(v);
                                        }
                                    }
                                }
                                #endregion
                            }
                            else if (s == "显示")
                            {
                                #region 显示
                                if (!cfg.Is显示图像)
                                {
                                    continue;
                                }

                                //预览图像
                                doc.ExportImageToFile(imagePath, ImageType.JPEG, Seagull.BarTender.Print.ColorDepth.ColorDepth24bit, new Resolution(cfg.图像大小.Width, cfg.图像大小.Height), OverwriteOptions.Overwrite);


                                #endregion
                            }
                        }


                        doc.Close((SaveOptions)cfg.保存方式);
                        engine.Stop();

                    }
                    finally
                    {
                        doc?.Close((SaveOptions)cfg.保存方式);
                    }
                }

                return (true, "", imagePath, lst.ToArray());

            }
            catch (Exception ex)
            {
                return (false, ex.Message, imagePath, lst.ToArray());
            }
        }


    }
}
