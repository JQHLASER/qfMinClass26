using Sunny.UI.Win32;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static qfmain.WindProc;

namespace qfNet
{
    /// <summary>
    /// WndProc
    /// </summary>
    public class DogTW基础_window通知机制
    {
        //加密狗GUID  "4D1E55B2-F16F-11CF-88CB-001111000030"
        public qfNet.Window_通知机制 _Window_通知机制 = new qfNet.Window_通知机制("4D1E55B2-F16F-11CF-88CB-001111000030");

     
        private void 监测狗是否被拔出来(ref System.Windows.Forms.Message msg)
        {
            int wParam = (int)msg.WParam;
            if (wParam == Win32.DBT_DEVICEARRIVAL)
            {
                int devType = Marshal.ReadInt32(msg.LParam, 4);
                if (devType == Win32.DBT_DEVTYP_DEVICEINTERFACE)
                {
                    Win32.DEV_BROADCAST_DEVICEINTERFACE1 DeviceInfo = (Win32.DEV_BROADCAST_DEVICEINTERFACE1)Marshal.PtrToStructure(msg.LParam, typeof(Win32.DEV_BROADCAST_DEVICEINTERFACE1));
                    // 如果需要知道是否我们的设备插入或拨出，可以查看这个结构中的dbcc_name
                    string KeyPath = "";
                    for (int n = 0; n < DeviceInfo.dbcc_name.GetUpperBound(0); n++) KeyPath = KeyPath + DeviceInfo.dbcc_name[n];
                    // MessageBox.Show(" 加密锁被插入。锁的设备路径是：" + KeyPath);

                    Event_DogTw(true);


                }
            }
            if (wParam == Win32.DBT_DEVICEREMOVECOMPLETE)
            {
                int devType = Marshal.ReadInt32(msg.LParam, 4);
                if (devType == Win32.DBT_DEVTYP_DEVICEINTERFACE)
                {
                    Win32.DEV_BROADCAST_DEVICEINTERFACE1 DeviceInfo = (Win32.DEV_BROADCAST_DEVICEINTERFACE1)Marshal.PtrToStructure(msg.LParam, typeof(Win32.DEV_BROADCAST_DEVICEINTERFACE1));
                    // 如果需要知道是否我们的设备插入或拨出，可以查看这个结构中的dbcc_name
                    // MessageBox.Show(" 加密锁被拨出。");

                    Event_DogTw(false);
                }

            }
        }

        /// <summary>
        /// 放在Form的Load事件中
        /// </summary>
        public void 监测狗是否被拔出来(IntPtr Handle)
        {
            Win32.DEV_BROADCAST_DEVICEINTERFACE dbi = new
                Win32.DEV_BROADCAST_DEVICEINTERFACE();
            int size = Marshal.SizeOf(dbi);
            dbi.dbcc_size = size;
            dbi.dbcc_devicetype = Win32.DBT_DEVTYP_DEVICEINTERFACE;
            dbi.dbcc_reserved = 0;
            dbi.dbcc_classguid = Win32.GUID_DEVINTERFACE_USB_DEVICE;
            dbi.dbcc_name = 0;
            IntPtr buffer = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(dbi, buffer, true);
            IntPtr r = Win32.RegisterDeviceNotification(Handle, buffer,
                Win32.DEVICE_NOTIFY_WINDOW_HANDLE);
            if (r == IntPtr.Zero)
            {
                // MessageBox.Show(Win32.GetLastError().ToString());
            }
        }


        /// <summary>
        /// WndProc放大WndProc中
        /// </summary>
        /// <param name="m"></param>
        public void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case qfNet.Window_通知机制.WM_DEVICECHANGE:
                    监测狗是否被拔出来(ref m);
                    break;
            }
        }


        /// <summary>
        /// 加密狗插入或拔出事件
        /// </summary>
        public event Action<bool> Event_DogTw;


        /// <summary>
        ///
        /// </summary>
        /// <param name="ui标题栏"></param>
        /// <param name="状态">true:正常,false:未检测到加密狗</param>
        public void 标题栏状态(窗体_标题栏状态 ui标题栏, bool 状态)
        {
            _cfg_标题栏状态_[] info = new _cfg_标题栏状态_[]
                {
                new _cfg_标题栏状态_(Language_ .Get语言("加密狗"),"",0),
                new _cfg_标题栏状态_(Language_ .Get语言("加密狗"),Language_ .Get语言("未检测到加密狗"),-1),
                };
            int a = 状态 ? 0 : -1;
            ui标题栏.Add(info, a);
        }

         

    }
}
