using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace qfmain
{
    /// <summary>
    /// Windows下的智能GC处理,
    /// 使用方法: static GC_Windows mem = new GC_Windows(); mem.Start();
    /// </summary>
    public class GC_Windows : IDisposable
    {
         
 
        [DllImport("psapi.dll")]
        private static extern bool EmptyWorkingSet(IntPtr hProcess);

        private readonly Timer _timer;
        private readonly Queue<long> _memoryHistory = new Queue<long>();
        private readonly object _lock = new object();
        private bool _running;

        // 配置参数
        public int IntervalMs = 10000;         // 检测间隔
        public int HistorySize = 6;            // 内存趋势长度
        public long BaseTriggerMB = 400;       // 智能GC触发阈值
        public long HardLimitMB = 900;         // 硬限制直接全GC
        public long TrendThresholdMB = 80;     // 上涨趋势触发GC

        public GC_Windows()
        {
            _timer = new Timer(CheckMemory, null, Timeout.Infinite, Timeout.Infinite);
        }

        public void Start()
        {
            if (_running) return;
            _timer.Change(3000, IntervalMs);
            _running = true;
        }

        private void CheckMemory(object state)
        {
            try
            {
                lock (_lock)
                {
                    long mem = GetMemoryMB();

                    RecordMemory(mem);

                    // 硬限制直接全GC
                    if (mem > HardLimitMB)
                    {
                        FullGC();
                        TrimWorkingSet();
                        return;
                    }

                    // 内存趋势上涨，智能触发GC
                    if (IsMemoryTrendingUp())
                    {
                        SmartCollect(mem);
                    }
                }
            }
            catch
            {
                // 安全忽略异常，保证定时器继续运行
            }
        }

        #region 内存趋势分析
        private void RecordMemory(long mem)
        {
            _memoryHistory.Enqueue(mem);
            while (_memoryHistory.Count > HistorySize)
                _memoryHistory.Dequeue();
        }

        private bool IsMemoryTrendingUp()
        {
            if (_memoryHistory.Count < HistorySize) return false;

            long first = 0;
            long last = 0;
            int i = 0;
            foreach (var m in _memoryHistory)
            {
                if (i == 0) first = m;
                last = m;
                i++;
            }

            return (last - first) > TrendThresholdMB;
        }
        #endregion

        #region 智能GC
        private void SmartCollect(long mem)
        {
            if (mem > BaseTriggerMB + 200)
            {
                FullGC();
            }
            else if (mem > BaseTriggerMB)
            {
                // 中代GC，尽量减少暂停
                GC.Collect(1, GCCollectionMode.Optimized);
            }
        }

        private void FullGC()
        {
            // LOH压缩
            GCSettings.LargeObjectHeapCompactionMode = GCLargeObjectHeapCompactionMode.CompactOnce;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }
        #endregion

        #region 压缩工作集
        private void TrimWorkingSet()
        {
            try
            {
                EmptyWorkingSet(Process.GetCurrentProcess().Handle);
            }
            catch { }
        }
        #endregion

        #region 内存获取
        private static long GetMemoryMB()
        {
            return Process.GetCurrentProcess().WorkingSet64 / 1024 / 1024;
        }
        #endregion

        public void Dispose()
        {
            _timer.Dispose();
        }
     

}
}