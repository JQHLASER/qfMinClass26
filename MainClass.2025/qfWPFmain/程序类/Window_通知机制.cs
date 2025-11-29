using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace qfWPFmain
{
    internal class Window_通知机制
    {
        public class UsbDeviceMonitor : IDisposable
        {

            /// <summary>
            /// 
            /// </summary>
            /// <param name="windowHandle"></param>
            /// <param name="Guid">硬件GUID地址</param>
            public UsbDeviceMonitor(IntPtr windowHandle, string Guid = "")
            {
                if (!string.IsNullOrEmpty(Guid))
                {
                    GUID_DEVINTERFACE_USB_DEVICE = new Guid(Guid);
                }
                else
                {
                    GUID_DEVINTERFACE_USB_DEVICE = new Guid("A5DCBF10-6530-11D2-901F-00C04FD7C15B");
                }
                _windowHandle = windowHandle;
                RegisterUsbDeviceNotification();
            }


            private IntPtr _windowHandle;
            private IntPtr _notificationHandle;

            // 设备通知的委托
            public delegate void DeviceChangedEventHandler(bool isConnected, string deviceName);
            public event DeviceChangedEventHandler DeviceChanged;

            // 导入必要的Windows API
            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            private static extern IntPtr RegisterDeviceNotification(
                IntPtr hRecipient,
                IntPtr lpNotificationFilter,
                uint dwFlags);

            [DllImport("user32.dll")]
            private static extern bool UnregisterDeviceNotification(IntPtr hHandle);

            [StructLayout(LayoutKind.Sequential)]
            private struct DEV_BROADCAST_DEVICEINTERFACE
            {
                public int dbcc_size;
                public int dbcc_devicetype;
                public int dbcc_reserved;
                public Guid dbcc_classguid;
                public short dbcc_name;
            }

            private const int DEVICE_NOTIFY_WINDOW_HANDLE = 0x00000000;
            private const int WM_DEVICECHANGE = 0x0219;
            private const int DBT_DEVICEARRIVAL = 0x8000;
            private const int DBT_DEVICEREMOVECOMPLETE = 0x8004;
            private const int DBT_DEVTYP_DEVICEINTERFACE = 0x00000005;

            // USB设备的GUID
            //private static readonly Guid GUID_DEVINTERFACE_USB_DEVICE = new Guid("A5DCBF10-6530-11D2-901F-00C04FD7C15B");

            //USB设备的GUID
            private readonly Guid GUID_DEVINTERFACE_USB_DEVICE;




            private void RegisterUsbDeviceNotification()
            {
                int size = Marshal.SizeOf(typeof(DEV_BROADCAST_DEVICEINTERFACE));
                DEV_BROADCAST_DEVICEINTERFACE deviceInterface = new DEV_BROADCAST_DEVICEINTERFACE
                {
                    dbcc_size = size,
                    dbcc_devicetype = DBT_DEVTYP_DEVICEINTERFACE,
                    dbcc_classguid = GUID_DEVINTERFACE_USB_DEVICE
                };

                IntPtr buffer = Marshal.AllocHGlobal(size);
                Marshal.StructureToPtr(deviceInterface, buffer, true);

                _notificationHandle = RegisterDeviceNotification(
                    _windowHandle,
                    buffer,
                    DEVICE_NOTIFY_WINDOW_HANDLE);
            }

            // 处理窗口消息
            public void HandleWindowMessage(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
            {
                if (msg == WM_DEVICECHANGE)
                {
                    switch (wParam.ToInt32())
                    {
                        case DBT_DEVICEARRIVAL:
                            OnDeviceChanged(true, GetDeviceName(lParam));
                            handled = true;
                            break;
                        case DBT_DEVICEREMOVECOMPLETE:
                            OnDeviceChanged(false, GetDeviceName(lParam));
                            handled = true;
                            break;
                    }
                }
            }

            private string GetDeviceName(IntPtr lParam)
            {
                if (lParam == IntPtr.Zero)
                    return string.Empty;

                DEV_BROADCAST_DEVICEINTERFACE deviceInterface =
                    Marshal.PtrToStructure<DEV_BROADCAST_DEVICEINTERFACE>(lParam);

                int offset = Marshal.OffsetOf(typeof(DEV_BROADCAST_DEVICEINTERFACE), "dbcc_name").ToInt32();
                IntPtr pDeviceName = new IntPtr(lParam.ToInt64() + offset);

                return Marshal.PtrToStringAuto(pDeviceName);
            }

            protected virtual void OnDeviceChanged(bool isConnected, string deviceName)
            {
                DeviceChanged?.Invoke(isConnected, deviceName);
            }

            public void Dispose()
            {
                if (_notificationHandle != IntPtr.Zero)
                {
                    UnregisterDeviceNotification(_notificationHandle);
                    _notificationHandle = IntPtr.Zero;
                }
            }





        }

    }
}
