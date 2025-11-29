using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace qfNet
{

    /// <summary>
    /// WndProc
    /// </summary>
    public class Window_通知机制
    {

        static string _Guid = "4D1E55B2-F16F-11CF-88CB-001111000030";

        public Window_通知机制(string Guid_)
        {
            _Guid = Guid_;
        }


        public const int WM_DEVICECHANGE = 0x0219;
        public const int DBT_DEVICEARRIVAL = 0x8000, // systemdetected a new device
                           DBT_DEVICEREMOVECOMPLETE = 0x8004; // device is gone
        public const int DEVICE_NOTIFY_WINDOW_HANDLE = 0,
            DEVICE_NOTIFY_SERVICE_HANDLE = 1;
        public const int DBT_DEVTYP_DEVICEINTERFACE = 0x00000005; // deviceinterface class
        public static Guid GUID_DEVINTERFACE_USB_DEVICE = new
            Guid(_Guid);

        [StructLayout(LayoutKind.Sequential)]
        public class DEV_BROADCAST_HDR
        {
            public int dbcc_size;
            public int dbcc_devicetype;
            public int dbcc_reserved;
        }
        [StructLayout(LayoutKind.Sequential)]
        public class
            DEV_BROADCAST_DEVICEINTERFACE
        {
            public int dbcc_size;
            public int dbcc_devicetype;
            public int dbcc_reserved;
            public Guid dbcc_classguid;
            public short dbcc_name;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public class DEV_BROADCAST_DEVICEINTERFACE1
        {
            public int dbcc_size;
            public int dbcc_devicetype;
            public int dbcc_reserved;
            [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U1,
                SizeConst = 16)]
            public byte[] dbcc_classguid;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
            public char[]
                dbcc_name;
        }

        [DllImport("user32.dll", SetLastError = true)]
        public static extern
            IntPtr RegisterDeviceNotification(IntPtr hRecipient, IntPtr
            NotificationFilter, Int32 Flags);
        [DllImport("kernel32.dll")] public static extern int GetLastError();



        #region 用法


        //protected override void WndProc(ref Message m)
        //{
        //    switch (m.Msg)
        //    {
        //        case Win32.WM_DEVICECHANGE: OnDeviceChange(ref m); break;
        //    }
        //    base.WndProc(ref m);
        //}


        //void OnDeviceChange(ref Message msg)
        //{
        //    int wParam = (int)msg.WParam;
        //    if (wParam == Win32.DBT_DEVICEARRIVAL)
        //    {
        //        int devType = Marshal.ReadInt32(msg.LParam, 4);
        //        if (devType == Win32.DBT_DEVTYP_DEVICEINTERFACE)
        //        {
        //            Win32.DEV_BROADCAST_DEVICEINTERFACE1 DeviceInfo = (Win32.DEV_BROADCAST_DEVICEINTERFACE1)Marshal.PtrToStructure(msg.LParam, typeof(Win32.DEV_BROADCAST_DEVICEINTERFACE1));
        //            // 如果需要知道是否我们的设备插入或拨出，可以查看这个结构中的dbcc_name
        //            string KeyPath = "";
        //            for (int n = 0; n < DeviceInfo.dbcc_name.GetUpperBound(0); n++) KeyPath = KeyPath + DeviceInfo.dbcc_name[n];
        //            MessageBox.Show(" 加密锁被插入。锁的设备路径是：" + KeyPath);
        //        }
        //    }
        //    if (wParam == Win32.DBT_DEVICEREMOVECOMPLETE)
        //    {
        //        int devType = Marshal.ReadInt32(msg.LParam, 4);
        //        if (devType == Win32.DBT_DEVTYP_DEVICEINTERFACE)
        //        {
        //            Win32.DEV_BROADCAST_DEVICEINTERFACE1 DeviceInfo = (Win32.DEV_BROADCAST_DEVICEINTERFACE1)Marshal.PtrToStructure(msg.LParam, typeof(Win32.DEV_BROADCAST_DEVICEINTERFACE1));
        //            // 如果需要知道是否我们的设备插入或拨出，可以查看这个结构中的dbcc_name
        //            MessageBox.Show(" 加密锁被拨出。");
        //        }

        //    }
        //}



        #endregion
    }





}

