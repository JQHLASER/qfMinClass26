using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace qfmain
{
    public  class 内存
    {

        /// <summary>
        /// 清理内存,有的时候需要用来清理内存,以防显示内存不足
        /// </summary>
        public void 内存清理()
        {
            GC.Collect();
        }



        public void 申明内存地址变量(ref IntPtr 内存变量, int 缓冲区长度)
        {
            内存变量 = Marshal.AllocHGlobal(缓冲区长度);//申明并分配内存
        }

        public string 从内存中取出内容(IntPtr 内存变量)
        {
            return Marshal.PtrToStringAnsi(内存变量);//从内存地址中取出内容   
        }

        public void 写入内存_IntPtr(IntPtr 内存变量, int 长度, IntPtr value)
        {
            Marshal.WriteIntPtr(内存变量, 长度, value);

        }

        public void 写入内存_Byte(IntPtr 内存变量, int 长度, byte value)
        {
            Marshal.WriteByte(内存变量, 长度, value);

        }

        public void 写入内存_short(IntPtr 内存变量, int 长度, short value)
        {
            Marshal.WriteInt16(内存变量, 长度, value);

        }

        public void 写入内存_int(IntPtr 内存变量, int 长度, int value)
        {
            Marshal.WriteInt32(内存变量, 长度, value);

        }

        public void 写入内存_long(IntPtr 内存变量, int 长度, long value)
        {
            Marshal.WriteInt64(内存变量, 长度, value);
        }






        public void 释放内存地址变量(IntPtr 内存变量)
        {
            Marshal.FreeHGlobal(内存变量);//释放内存;
        }




        [DllImport("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize")]
        public static extern int SetProcessWorkingSetSize(IntPtr process, int minSize, int maxSize);

        /// <summary>
        /// 释放内存
        /// </summary>
        public void 释放内存()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        /// <summary>
        /// 释放内存,可获取占用内存大小和当前工作进程
        /// </summary>
        public void 释放内存2()
        {
            //获得当前工作进程
            Process proc = Process.GetCurrentProcess();
            long usedMemory = proc.PrivateMemorySize64;
            if (usedMemory > 1024 * 1024 * 20)
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                {
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
                }
            }
        }

    }
}
