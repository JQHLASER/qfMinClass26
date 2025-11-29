using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace qfmain 
{
    public class CPU_
    {


        /// <summary>   
        /// 智能分配CPU  从高到低 从高到低分配   
        /// </summary>   
        public void 分配CPU(string 进程名)
        {
            #region 智能随机分配   
            try
            {
                Process[] ps = Process.GetProcessesByName(进程名);
                if (ps.Length > 0)
                {
                    int zCPU = Environment.ProcessorCount;//总CPU颗数   
                    int cCPU = zCPU;//当前分配到了哪一颗CPU   
                    for (int i = 0; i < ps.Length; i++)//进程数循环   
                    {
                        if (cCPU == 0)
                            cCPU = zCPU - 1;
                        else
                            cCPU--;
                        try
                        {
                            int p = (int)Math.Pow(2, cCPU);
                            ps[i].ProcessorAffinity = (IntPtr)p;
                        }
                        catch { }
                    }
                }
            }
            catch { }
            #endregion
        }


        //获取系统运行时间毫秒级别
        [DllImport("kernel32.dll")]
        public static extern uint GetTickCount();


        //SetThreadAffinityMask 指定hThread 运行在 核心 dwThreadAffinityMask
        [DllImport("kernel32.dll")]
        public static extern UIntPtr SetThreadAffinityMask(IntPtr hThread, UIntPtr dwThreadAffinityMask);

        //得到当前线程的handler
        [DllImport("kernel32.dll")]
        public static extern IntPtr GetCurrentThread();

        //获取cpu的id号
        public virtual ulong 获取CPU的ID号(int id)
        {
            ulong cpuid = 0;
            if (id < 0 || id >= System.Environment.ProcessorCount)
            {
                id = 0;
            }
            cpuid |= 1UL << id;

            return cpuid;
        }

        public virtual uint 获取系统运行时间_毫秒级()
        {
            return GetTickCount();
        }

        public virtual UIntPtr 当前线程运行在指定核心(IntPtr 线程句柄, UIntPtr CpuID)
        {
            return SetThreadAffinityMask(线程句柄, CpuID);
        }

        public virtual UIntPtr 当前线程运行在指定核心(int Cpu索引)
        {
            return SetThreadAffinityMask(获取当前线程的句柄(), new UIntPtr(获取CPU的ID号(Cpu索引)));
        }

        public virtual IntPtr 获取当前线程的句柄()
        {
            return GetCurrentThread();
        }


    }
}
