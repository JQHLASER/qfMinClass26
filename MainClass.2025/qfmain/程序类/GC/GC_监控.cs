using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
 

namespace qfmain 
{
    public  class GC_监控 : IDisposable
    {
        public class PerfInfo
        {
            /// <summary>
            /// CPU
            /// </summary>
            public double ProcessCpu { get; set; }
            /// <summary>
            ///内存
            /// </summary>
            public double ProcessMemoryMB { get; set; }

            /// <summary>
            /// 系统内存
            /// </summary>
            public double SystemMemoryPercent { get; set; }

            /// <summary>
            /// /时间
            /// </summary>
            public System.DateTime Time { get; set; }
        }


        private readonly Process _process = Process.GetCurrentProcess();

        private TimeSpan _lastCpuTime;
        private DateTime _lastCheckTime;

        private Timer _timer;

        /// <summary>
        /// 监听周期
        /// </summary>
        public int IntervalMs { get; set; } = 2000;

        /// <summary>
        /// CPU上限
        /// </summary>
        public double CpuLimit { get; set; } = 80;

        /// <summary>
        /// 内存上线
        /// </summary>
        public double MemoryLimitMB { get; set; } = 1024;

        /// <summary>
        /// 是否触发自动GC
        /// </summary>
        public bool AutoGC { get; set; } = false;

        /// <summary>
        /// 更新事件
        /// </summary>
        public event Action<PerfInfo> OnUpdate;
        /// <summary>
        /// 警告事件
        /// </summary>
        public event Action<string, PerfInfo> OnWarning;

        public GC_监控()
        {
            _lastCpuTime = _process.TotalProcessorTime;
            _lastCheckTime = DateTime.Now;
        }

        public void Start()
        {
            _timer = new Timer(Check, null, 0, IntervalMs);
        }

        public void Stop()
        {
            _timer?.Dispose();
        }

        private void Check(object state)
        {
            try
            {
                var info = Collect();

                OnUpdate?.Invoke(info);

                if (info.ProcessCpu > CpuLimit)
                    OnWarning?.Invoke("CPU过高", info);

                if (info.ProcessMemoryMB > MemoryLimitMB)
                {
                    OnWarning?.Invoke("内存过高", info);

                    if (AutoGC)
                        GC.Collect();
                }
            }
            catch
            {
            }
        }

        public PerfInfo Collect()
        {
            DateTime now = DateTime.Now;
            TimeSpan cpuTime = _process.TotalProcessorTime;

            double cpu =
                (cpuTime - _lastCpuTime).TotalMilliseconds /
                (Environment.ProcessorCount *
                (now - _lastCheckTime).TotalMilliseconds) * 100;

            _lastCpuTime = cpuTime;
            _lastCheckTime = now;

            double procMem = _process.WorkingSet64 / 1024d / 1024d;

            double sysMem = GetSystemMemoryPercent();

            return new PerfInfo
            {
                ProcessCpu = cpu,
                ProcessMemoryMB = procMem,
                SystemMemoryPercent = sysMem,
                Time = DateTime.Now
            };
        }

        /// ⭐ 获取系统内存使用率（纯WinAPI方式）
        private double GetSystemMemoryPercent()
        {
            MEMORYSTATUSEX mem = new MEMORYSTATUSEX();
            GlobalMemoryStatusEx(mem);

            return mem.dwMemoryLoad;
        }

        public void Dispose()
        {
            Stop();
        }

        #region WinAPI
        [System.Runtime.InteropServices.StructLayout(
            System.Runtime.InteropServices.LayoutKind.Sequential)]
        public class MEMORYSTATUSEX
        {
            public uint dwLength = (uint)
                System.Runtime.InteropServices.Marshal.SizeOf(typeof(MEMORYSTATUSEX));

            public uint dwMemoryLoad;
            public ulong ullTotalPhys;
            public ulong ullAvailPhys;
            public ulong ullTotalPageFile;
            public ulong ullAvailPageFile;
            public ulong ullTotalVirtual;
            public ulong ullAvailVirtual;
            public ulong ullAvailExtendedVirtual;
        }

        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        public static extern bool GlobalMemoryStatusEx(MEMORYSTATUSEX lpBuffer);
        #endregion
    


}
}
