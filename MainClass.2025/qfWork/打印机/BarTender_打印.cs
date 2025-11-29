using BarTender;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace qfWork
{
    /// <summary>
    /// 打印机，
    /// </summary>
    public class BarTender_打印
    {
        public class info_变量_
        {
            /// <summary>
            /// 变量名
            /// </summary>
            public string Name { set; get; } = "";
            /// <summary>
            /// 内容
            /// </summary>
            public string Value { set; get; } = "";
        }


        public class info_打印参数_
        {
            public int 打印张数 { set; get; } = 1;
            public bool 判断修改内容结果 { set; get; } = false;

            /// <summary>
            /// =0:不显示
            /// </summary>
            public int dpi { set; get; } = 0;
        }

        public class info_日志_
        {

            public bool 状态 { set; get; } = false;
            public info_变量_ info变量 { set; get; } = new info_变量_();
            public string 日志消息 { set; get; } = "";
        }







        //创建一个实例
        BarTender.Application btApp = null;
        Format btFormat = null;

        /// <summary>
        /// 初始化实例
        /// </summary>
        public void 初始化()
        {
            btApp = new BarTender.Application();
        }


        public bool 打开btw(string path, out string msgErr)
        {
            bool rt = true;
            msgErr = string.Empty;
            try
            {
                // 打开一个模板
                btFormat = btApp.Formats.Open(path);
            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }
            return rt;
        }

        public bool 修改变量内容(string Name, string Value, out string msgErr)
        {
            bool rt = true;
            msgErr = string.Empty;
            try
            {
                // 设置变量值(可选)
                btFormat.SetNamedSubStringValue(Name, Value);
            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }
            return rt;
        }

        public bool 设置打印张数(int 份数, out string msgErr)
        {
            bool rt = true;
            msgErr = string.Empty;
            try
            {
                //设置打印份数
                btFormat.IdenticalCopiesOfLabel = 份数;
            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }
            return rt;
        }

        /// <summary>
        /// 标签索引: 从1开始,默认为1
        /// </summary>
        /// <param name="标签索引"></param>
        /// <param name="msgErr"></param>
        /// <returns></returns>
        public bool 设置打印标签索引(int 标签索引, out string msgErr)
        {
            bool rt = true;
            msgErr = string.Empty;
            try
            {
                //设置打印份数
                btFormat.PrintSetup.NumberSerializedLabels = 标签索引;
            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }
            return rt;
        }



        /// <summary>
        /// 名称:在打印任务队列中显示的名称
        /// </summary>
        /// <param name="msgErr"></param>
        /// <param name="名称"></param>
        /// <returns></returns>
        public bool 打印(out string msgErr, string 名称 = "PrintingJobName")
        {
            bool rt = true;
            msgErr = string.Empty;
            try
            {
                BarTender.Messages btMsgs;
                //开始打印
                var btPrintRtn = btFormat.Print(名称, true, -1, out btMsgs);
                ////输出错误日志
                //if (btPrintRtn != BarTender.BtPrintResult.btSuccess)
                //{
                //    foreach (BarTender.Message msg in btMsgs)
                //    {
                //        rt = false;
                //        msgErr = msg.Message;
                //        MessageBox.Show(msgErr);
                //    }
                //}
            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }
            return rt;
        }

        public bool 关闭btw(out string msgErr)
        {
            bool rt = true;
            msgErr = string.Empty;
            try
            {
                //关闭模板
                btFormat.Close(BarTender.BtSaveOptions.btDoNotSaveChanges);

            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }
            return rt;
        }

        public bool 释放(out string msgErr)
        {
            bool rt = true;
            msgErr = string.Empty;
            try
            {
                //退出打印程序
                btApp.Quit(BarTender.BtSaveOptions.btDoNotSaveChanges);

                // 退出Bartender应用程序并释放资源
                if (btApp != null)
                {
                    btApp.Quit();
                    Marshal.ReleaseComObject(btApp);
                    btApp = null;
                    // 强制垃圾回收
                    GC.Collect();
                    //GC.WaitForPendingFinalizers();
                }

            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }

            释放进程();
            return rt;
        }


        public void 释放进程()
        {
            string jc = "BarTender";
            if (new qfmain.进程().进程是否存在(jc))
            {
                new qfmain.进程().结束指定进程(jc, out string msgErr);
            }
        }


        public void 显示(int dpi)
        {
            //  ExportPrintPreviewToImage的第一个参数是输出的路径，第二个参数是文件名称，第三个参数是文件类型，第四个参数选择btColors24Bit，第五个参数是图片的大小，第六个参数是背景色(0是黑色)，第七个参数是不保存变更，第八个参数是否包含边缘、第九个参数是否包含边框，第十个参数是输出参数。
            string name = "test.bmp";
            string name1 = "test1.bmp";
            string name2 = "test2.bmp";
            // pic.Image = null;
            new qfmain.文件_文件夹().文件_删除文件($"{Environment.CurrentDirectory}\\{name1}", out string msgErr);
            new qfmain.文件_文件夹().文件_删除文件($"{Environment.CurrentDirectory}\\{name2}", out msgErr);
            BarTender.Messages btMessages2;

            btFormat.ExportPrintPreviewToImage(Environment.CurrentDirectory, name, "bmp", BarTender.BtColors.btColors24Bit, dpi, 0,
                BarTender.BtSaveOptions.btDoNotSaveChanges, true, true, out btMessages2);

            string path = $"{Environment.CurrentDirectory}\\{name1}";
            On_读图片(path);

        }


        /// <summary>
        /// 已包含了初始化
        /// </summary>
        /// <param name="path"></param>
        /// <param name="info"></param>
        /// <param name="info参数"></param>
        /// <param name="msgErr"></param>
        /// <returns></returns>
        public bool 打印(string path, info_变量_[] info, info_打印参数_ info参数, out string msgErr)
        {
            bool rt = true;
            msgErr = string.Empty;

            if (info is null)
            {
                info = new info_变量_[0];
            }
            if (info参数 is null)
            {
                info参数 = new info_打印参数_();
            }

            List<string> lstWork = new List<string>();
            lstWork.Add("打开");
            lstWork.Add("修改变量内容");
            lstWork.Add("设置打印张数");
            lstWork.Add("打印");
            lstWork.Add("显示");


            try
            {


                初始化();
                foreach (var s in lstWork)
                {
                    if (!rt)
                    {
                        break;
                    }
                    else if (s == "打开")
                    {
                        rt = 打开btw(path, out msgErr);
                    }
                    else if (s == "修改变量内容")
                    {
                        #region 修改变量内容

                        foreach (var sV in info)
                        {
                            bool rtXg = 修改变量内容(sV.Name, sV.Value, out msgErr);
                            if (info参数.判断修改内容结果)
                            {
                                rt = rtXg;
                            }

                            info_日志_ info日志 = new info_日志_();
                            info日志.状态 = rtXg;
                            info日志.info变量 = sV;
                            info日志.日志消息 = msgErr;
                            On_修改内容日志(info日志);
                        }


                        #endregion

                    }
                    else if (s == "设置打印张数")
                    {
                        rt = 设置打印张数(info参数.打印张数, out msgErr);
                    }
                    else if (s == "打印")
                    {
                        new qfmain.文件_文件夹().文件_获取文件名_不含后缀(path, out string name, out msgErr);
                        rt = 打印(out msgErr, name);
                    }
                    else if (s == "显示")
                    {
                        if (info参数.dpi <= 0)
                        {
                            continue;
                        }
                        显示(info参数.dpi);
                    }

                }

            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }

            关闭btw(out string msgErr1);
            释放(out string msgErr2);

            return rt;


        }

        /// <summary>
        /// 已包含了初始化
        /// </summary>
        /// <param name="path"></param>
        /// <param name="info"></param>
        /// <param name="info参数"></param>
        /// <param name="msgErr"></param>
        /// <returns></returns>
        public bool 打开(string path, info_变量_[] info, info_打印参数_ info参数, out string msgErr)
        {
            bool rt = true;
            msgErr = string.Empty;

            if (info is null)
            {
                info = new info_变量_[0];
            }

            if (info参数 is null)
            {
                info参数 = new info_打印参数_();
            }


            List<string> lstWork = new List<string>();
            lstWork.Add("打开");
            lstWork.Add("修改变量内容");
            lstWork.Add("显示");

            try
            {

                初始化();
                foreach (var s in lstWork)
                {
                    if (!rt)
                    {
                        break;
                    }
                    else if (s == "打开")
                    {
                        rt = 打开btw(path, out msgErr);
                    }
                    else if (s == "修改变量内容")
                    {
                        #region 修改变量内容

                        foreach (var sV in info)
                        {
                            bool rtXg = 修改变量内容(sV.Name, sV.Value, out msgErr);
                            if (info参数.判断修改内容结果)
                            {
                                rt = rtXg;
                            }

                            info_日志_ info日志 = new info_日志_();
                            info日志.状态 = rtXg;
                            info日志.info变量 = sV;
                            info日志.日志消息 = msgErr;
                            On_修改内容日志(info日志);
                        }


                        #endregion

                    }

                    else if (s == "显示")
                    {
                        if (info参数.dpi <= 0)
                        {
                            continue;
                        }
                        显示(info参数.dpi);
                    }

                }

            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }

            关闭btw(out string msgErr1);
            释放(out string msgErr2);

            return rt;
        }


        public void 服务BarTender()
        {
            // 检查并重启 BarTender 服务 (需要管理员权限)

            try
            {
                var processes = System.Diagnostics.Process.GetProcessesByName("BtSrvr");
                foreach (var process in processes)
                {
                    process.Kill(); // 终止现有服务
                    process.WaitForExit();
                }

                // 启动服务
                System.Diagnostics.Process.Start("BtSrvr.exe");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"重启服务失败: {ex.Message}");
            }

        }

        #region 事件

        /// <summary>
        /// (string:图片路径)
        /// </summary>
        public event Action<string> Event_读图片;
        private void On_读图片(string path)
        {
            Event_读图片?.Invoke(path);
        }


        /// <summary>
        /// 在打印方法时用,(bool)状态,(string)名称,(string)内容,(string)msgErr
        /// </summary>
        public Action<info_日志_> Action_修改内容日志;

        void On_修改内容日志(info_日志_ info日志)
        {
            if (Action_修改内容日志 != null)
            {
                Action_修改内容日志(info日志);
            }
        }




        #endregion


        #region 参考

        private void Print()//打印标签，如果有错误则输出Bartender报出的错误信息
        {
            //_btFormat.PrintOut(false, false);
            BarTender.Messages btMessages;
            BarTender.BtPrintResult btResult;
            btFormat.PrintSetup.IdenticalCopiesOfLabel = 1;
            btFormat.PrintSetup.NumberSerializedLabels = 1;
            btResult = btFormat.Print("", false, -1, out btMessages);
            BarTender.Messages btMessages2;
            btFormat.ExportPrintPreviewToImage(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Test123.jpg", "jpg", BarTender.BtColors.btColors24Bit, 1500, 0,
                BarTender.BtSaveOptions.btDoNotSaveChanges, true, true, out btMessages2);

        }


        #endregion



    }
}
