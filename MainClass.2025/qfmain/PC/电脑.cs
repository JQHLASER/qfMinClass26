using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace qfmain 
{
    /// <summary>
    /// 安装 System.Management
    /// </summary>
    public class 电脑
    {

        #region  电脑关机/重启/注销/休眠

        public void 电脑_关机()
        {
            //System.Diagnostics.Process myProcess = new System.Diagnostics.Process();
            //myProcess.StartInfo.FileName = "cmd.exe";//启动cmd命令
            //myProcess.StartInfo.UseShellExecute = false;//是否使用系统外壳程序启动进程
            //myProcess.StartInfo.RedirectStandardInput = true;//是否从流中读取
            //myProcess.StartInfo.RedirectStandardOutput = true;//是否写入流
            //myProcess.StartInfo.RedirectStandardError = true;//是否将错误信息写入流
            //myProcess.StartInfo.CreateNoWindow = true;//是否在新窗口中启动进程
            //myProcess.Start();//启动进程
            //myProcess.StandardInput.WriteLine("shutdown -s -t 0");//执行关机命令

            Process.Start("shutdown", "/s /t 0");    // 参数 /s 的意思是要关闭计算机
                                                     // 参数 /t 0 的意思是告诉计算机 0 秒之后执行命令



        }

        public void 电脑_重启()
        {
            //System.Diagnostics.Process myProcess = new System.Diagnostics.Process();
            //myProcess.StartInfo.FileName = "cmd.exe";//启动cmd命令
            //myProcess.StartInfo.UseShellExecute = false;//是否使用系统外壳程序启动进程
            //myProcess.StartInfo.RedirectStandardInput = true;//是否从流中读取
            //myProcess.StartInfo.RedirectStandardOutput = true;//是否写入流
            //myProcess.StartInfo.RedirectStandardError = true;//是否将错误信息写入流
            //myProcess.StartInfo.CreateNoWindow = true;//是否在新窗口中启动进程
            //myProcess.Start();//启动进程
            //myProcess.StandardInput.WriteLine("shutdown -r -t 0");//执行重启计算机命令

            Process.Start("shutdown", "/r /t 0"); // 参数 /r 的意思是要重新启动计算机
        }



        //C#关机代码
        //这个结构体将会传递给API。使用StructLayout
        //（...特性，确保其中成员是按顺序排列的，C#编译器不会对其进行调整）
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct TokPriv1Luid
        {
            public int Count; public long Luid; public int Attr;
        }

        //以下使用DLLImport特性导入了所需的Windows API。
        //导入这些方法必须是static extern的，并且没有方法体。
        //调用这些方法就相当于调用Windows API。
        [DllImport("kernel32.dll", ExactSpelling = true)]
        internal static extern IntPtr GetCurrentProcess();

        [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
        internal static extern bool OpenProcessToken(IntPtr h, int acc, ref IntPtr phtok);

        [DllImport("advapi32.dll", SetLastError = true)]
        internal static extern bool LookupPrivilegeValueA(string host, string name, ref long pluid);

        [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
        internal static extern bool AdjustTokenPrivileges(IntPtr htok, bool disall, ref TokPriv1Luid newst, int len, IntPtr prev, IntPtr relen);

        //C#关机代码
        //以下定义了在调用WinAPI时需要的常数。
        //这些常数通常可以从Platform SDK的包含文件（头文件）中找到。
        public const int SE_PRIVILEGE_ENABLED = 0x00000002;
        public const int TOKEN_QUERY = 0x00000008;
        public const int TOKEN_ADJUST_PRIVILEGES = 0x00000020;
        public const string SE_SHUTDOWN_NAME = "SeShutdownPrivilege";
        public const int EWX_LOGOFF = 0x00000000;
        public const int EWX_SHUTDOWN = 0x00000001;
        public const int EWX_REBOOT = 0x00000002;
        public const int EWX_FORCE = 0x00000004;
        public const int EWX_POWEROFF = 0x00000008;
        public const int EWX_FORCEIFHUNG = 0x00000010;



        void DoExitWin(int flg)
        {
            bool ok;
            TokPriv1Luid tp;
            IntPtr hproc = GetCurrentProcess();
            IntPtr htok = IntPtr.Zero;
            ok = OpenProcessToken(hproc, TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, ref htok);
            tp.Count = 1;
            tp.Luid = 0;
            tp.Attr = SE_PRIVILEGE_ENABLED;
            ok = LookupPrivilegeValueA(null, SE_SHUTDOWN_NAME, ref tp.Luid);
            ok = AdjustTokenPrivileges(htok, false, ref tp, 0, IntPtr.Zero, IntPtr.Zero);
            ok = ExitWindowsEx(flg, 0);
        }

        /// <summary>
        /// 强制
        /// </summary>
        public void 重启()
        {
            DoExitWin(EWX_FORCE | EWX_REBOOT);
        }

        /// <summary>
        /// 强制
        /// </summary>
        public void 关机()
        {
            DoExitWin(EWX_FORCE | EWX_POWEROFF);
        }

        /// <summary>
        /// 强制
        /// </summary>
        public void 注销()
        {
            DoExitWin(EWX_FORCE | EWX_LOGOFF);
        }





        #region 电脑休眠/睡眠
        [DllImport("PowrProf.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool SetSuspendState(bool hiberate, bool forceCritical, bool disableWakeEvent);
        public void 电脑_休眠()
        {
            SetSuspendState(true, true, true);
        }

        public void 电脑_睡眠()
        {
            SetSuspendState(false, true, true);
        }

        #endregion

        [DllImport("user32")]
        public static extern void LockWorkStation();
        public void 电脑_锁定()
        {
            LockWorkStation();
        }




        [DllImport("user32.dll", EntryPoint = "ExitWindowsEx", CharSet = CharSet.Ansi)]    //code www.codesc.net
        private static extern bool ExitWindowsEx(int uFlags, int dwReserved);



        public void 电脑_注销()
        {
            ExitWindowsEx(0, 0);//注销计算机
        }

        #endregion

        //取得设备硬盘的卷标号
        public virtual  string 获取_硬盘的卷标号()
        {
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObject disk = new ManagementObject("win32_logicaldisk.deviceid=\"d:\"");
            disk.Get();
            return disk.GetPropertyValue("VolumeSerialNumber").ToString();
        }
        //获得CPU的序列号
        public virtual string 获取_CPU的序列号()
        {
            string strCpu = null;
            ManagementClass myCpu = new ManagementClass("win32_Processor");
            ManagementObjectCollection myCpuConnection = myCpu.GetInstances();
            foreach (ManagementObject myObject in myCpuConnection)
            {
                strCpu = myObject.Properties["Processorid"].Value.ToString();
                break;
            }
            return strCpu;
        }

        public virtual string 获取_硬盘ID()
        {
            ManagementClass mc = new ManagementClass("Win32_PhysicalMedia");
            //网上有提到，用Win32_DiskDrive，但是用Win32_DiskDrive获得的硬盘信息中并不包含SerialNumber属性。   
            ManagementObjectCollection moc = mc.GetInstances();
            string strID = null;
            foreach (ManagementObject mo in moc)
            {
                strID = mo.Properties["SerialNumber"].Value.ToString();
                break;
            }
            return strID.Trim();
        }

        public virtual string 获取_cpuID()
        {
            ManagementClass mc = new ManagementClass("Win32_Processor");
            ManagementObjectCollection moc = mc.GetInstances();
            string strID = null;
            foreach (ManagementObject mo in moc)
            {
                strID = mo.Properties["ProcessorId"].Value.ToString();
                break;
            }
            return strID.Trim();
        }

        /// <summary>
        /// 硬盘卷标号+CPU序列号
        /// </summary>
        /// <returns></returns>
        public virtual string 获取_机器码()
        {
            string value = string.Empty;

            //   string cpu = 获取_CPU的序列号();
            // string desk = 获取_硬盘的卷标号();
            string bosi = 获取_主板ID();



            //value = 获取_CPU的序列号() + 获取_硬盘的卷标号();//获得24位Cpu和硬盘序列号
            //string[] strid = new string[24];
            //for (int i = 0; i < 24; i++)//把字符赋给数组
            //{
            //    strid[i] = value.Substring(i, 1);
            //}
            //value = string.Empty;
            //Random rdid = new Random();
            //for (int i = 0; i < 24; i++)//从数组随机抽取24个字符组成新的字符生成机器码
            //{
            //    value += strid[rdid.Next(0, 24)];
            //}
            文本 textSys = new 文本();
            value = textSys.替换(bosi.Trim(), "-", "");
            value = textSys.替换(value.Trim(), "/", "");
            value = textSys.替换(value.Trim(), "~", "");
            value = textSys.替换(value.Trim(), "\\", "");
            value = textSys.替换(value.Trim(), "|", "");


            return value;
        }


        public virtual void 获取_显示器尺寸(ref int width, ref int height, bool 是否含任务栏)
        {
            if (是否含任务栏)
            {
                width = Screen.PrimaryScreen.Bounds.Width;
                height = Screen.PrimaryScreen.Bounds.Height;
            }
            else
            {
                width = Screen.PrimaryScreen.WorkingArea.Width;
                height = Screen.PrimaryScreen.WorkingArea.Height;
            }
        }

        public virtual string 获取_主板ID()
        {
            ManagementClass mc = new ManagementClass("Win32_BaseBoard");
            ManagementObjectCollection moc = mc.GetInstances();
            string strID = null;
            foreach (ManagementObject mo in moc)
            {
                strID = mo.Properties["SerialNumber"].Value.ToString();
                break;
            }
            return strID.Trim();
            //ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from Win32_baseboard");
            //string biosNumber = null;
            //foreach (ManagementObject mgt in searcher.Get())
            //{
            //    biosNumber = mgt["SerialNumber"].ToString();
            //}
            //return biosNumber;
        }

        public virtual string 获取_BIOS()
        {
            ManagementClass mc = new ManagementClass("Win32_Processor");
            ManagementClass mc4 = new ManagementClass("Win32_BIOS");
            ManagementObjectCollection moc4 = mc.GetInstances();
            string strID = string.Empty;
            foreach (ManagementObject mo in moc4)
            {
                strID += mo.Properties["SerialNumber"].Value.ToString();
                break;
            }
            return strID;
        }

        public virtual string 获取_主板信息()
        {
            // 读取主板信息：
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from Win32_baseboard");

            string xt = string.Empty;
            foreach (ManagementObject share in searcher.Get())
            {
                xt += "主板制造商:" + share["Manufacturer"] + "\r\n";
                xt += "型号:" + share["Product"] + "\r\n";
                xt += "序列号:" + share["SerialNumber"] + "\r\n";
            }
            return xt;

        }

        public virtual string 获取_系统所在盘符()
        {
            //  return System.Environment.SystemDirectory;

            return System.Environment.GetEnvironmentVariable("SystemDrive");

        }

        public virtual void 软件随电脑开机启动_注册表(bool 是否启动, string 名称, string path)
        {
            RegistryKey HKLM = Registry.LocalMachine;
            RegistryKey Run = HKLM.CreateSubKey(@"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run\");
            if (是否启动 == true)
            {
                try
                {
                    Run.SetValue(名称, path);
                    HKLM.Close();
                }
                catch (Exception Err)
                {
                    MessageBox.Show(Err.Message.ToString(), @"MUS\", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                try
                {
                    Run.DeleteValue(名称);
                    HKLM.Close();
                }
                catch (Exception)
                {
                    // 
                }
            }
        }

        public virtual string 获取_系统自动启动目录()
        {

            return Environment.GetFolderPath(Environment.SpecialFolder.Startup);

        }


        public virtual string 获取_程序完整路径
        {
            get
            {
                return Process.GetCurrentProcess().MainModule.FileName;
            }
        }

        public virtual string 获取_桌面目录()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        }



        public virtual string 获取_本机电脑名称()
        {
            return System.Net.Dns.GetHostName();//取本机电脑名称  

        }

        public virtual string[] 获取_本机IP()
        {
            string 本机名 = 获取_本机电脑名称();

            IPAddress[] addresses = System.Net.Dns.GetHostByName(本机名).AddressList;
            List<string> lst = new List<string>();
            foreach (IPAddress address in addresses)
            {
                lst.Add(address.ToString());
            }

            // System.Net.Dns.GetHostByName(本机名).AddressList.GetValue(1).ToString();


            return lst.ToArray();
        }


        /// <summary>
        /// 获取所有IP,含本地IP和外网IP
        /// </summary>
        /// <param name="lst_IPV4"></param>
        /// <param name="lst_IPV6"></param>
        public virtual void 获取_本机IP_1(List<string> lst_IPV4, List<string> lst_IPV6)
        {
            string hostName = Dns.GetHostName();
            IPAddress[] addresses = Dns.GetHostAddresses(hostName);
            foreach (IPAddress address in addresses)
            {
                if (address.AddressFamily == AddressFamily.InterNetwork)
                {
                    //本机IPV4 地址
                    lst_IPV4.Add(address.ToString());
                }
                else if (address.AddressFamily == AddressFamily.InterNetworkV6)
                {
                    //本机IPV6 地址
                    lst_IPV6.Add(address.ToString());
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ipAddress">IP</param>
        /// <param name="subnetMask">子网掩码</param>
        /// <param name="gateway">网关</param>
        public virtual void 设置_本机IP(string ipAddress, string subnetMask, string gateway)
        {
            IPAddress ethernetIPAddress = GetEthernetIPAddress();
            ManagementBaseObject inPar = null;
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration"); ;
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                if (!(bool)mo["IPEnabled"])
                {
                    continue;
                }
                if (((string[])mo["IPAddress"])[0] == ethernetIPAddress.ToString())
                {
                    inPar = mo.GetMethodParameters("Enablestatic");
                    //设置ip地址和子网掩码
                    inPar["IPAddress"] = new string[] { ipAddress };
                    inPar["SubnetMask"] = new string[] { subnetMask };
                    mo.InvokeMethod("EnableStatic", inPar, null);

                    //设置网关地址
                    if (gateway != null)
                    {
                        inPar = mo.GetMethodParameters("SetGateways");
                        inPar["DefaultIPGateway"] = new string[] { gateway };
                        mo.InvokeMethod("SetGateways", inPar, null);
                    }
                    break;
                }

            }

        }


        /// <summary>
        /// 查找以太网IP
        /// </summary>
        /// <returns></returns>
        public virtual IPAddress GetEthernetIPAddress()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface adapter in nics)
            {
                if (adapter.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                {
                    foreach (var item in adapter.GetIPProperties().UnicastAddresses)
                    {
                        if (item.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            return item.Address; //获取IP
                                                 // item.IPv4Mask;获取掩码
                        }
                    }

                }
                // adapter.GetIPProperties().GatewayAddresses;//获取网关
            }

            throw new Exception("Ethernet not connected");

        }













        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(int Description, int ReservedValue);

        /// <summary>
        /// true已连接,false已断开
        /// </summary>
        /// <returns></returns>
        public bool 网络_是否已连接()
        {
            try
            {
                int Description = 0;
                return InternetGetConnectedState(Description, 0);
            }
            catch (Exception)
            {
                return false;
                // throw;
            }
        }

        /// <summary>
        /// fase已断开,true已连接
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public bool 网络_指定IP是否已断开(string ip)
        {
            try
            {
                Ping p = new Ping();
                PingReply pr;
                pr = p.Send(ip);
                if (pr.Status != IPStatus.Success)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
                // throw;
            }

        }

        public bool 网络_Ping地址是否已连接(string IPorUrl, int 超时时间, List<string> lst返回值)
        {
            //构造Ping实例
            Ping pingSender = new Ping();

            //数据
            string data = "Ping " + IPorUrl;
            byte[] buf = Encoding.ASCII.GetBytes(data);
            //设置超时时间
            int timeout = 超时时间;
            //调用同步send方法发送数据,结果存入reply对象;
            PingReply reply = pingSender.Send(IPorUrl, timeout, buf);

            if (reply.Status == IPStatus.Success)
            {
                lst返回值.Add("答复的主机地址:" + reply.Address.ToString());
                lst返回值.Add("往返时间:" + reply.RoundtripTime);
                lst返回值.Add("生存时间(TTL):" + reply.Options.Ttl);
                lst返回值.Add("缓冲区大小:" + reply.Buffer.Length);


                return true;
            }
            else
            {
                return false;
            }

        }


        public string[] 获取_U盘盘符()
        {
            List<string> lst = new List<string>();

            DriveInfo[] allDrives = DriveInfo.GetDrives();
            foreach (DriveInfo d in allDrives)
            {
                //жU盘
                if (d.DriveType == DriveType.Removable)
                {
                    lst.Add(d.Name);
                }
            }
            return lst.ToArray();
        }



        public void 获取_系统中所有硬盘盘符(List<string> lst盘符, List<string> lst类型)
        {
            List<string> lst = new List<string>();

            DriveInfo[] allDrives = DriveInfo.GetDrives();
            string valur = "";
            foreach (DriveInfo d in allDrives)
            {
                //其它盘符
                lst盘符.Add(d.Name);
                lst类型.Add(d.DriveType.ToString());

            }

        }





    }
}
