using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
 

namespace qfWPFmain
{
    public class DogTW基础_window通知机制
    {
        Window d;
        public DogTW基础_window通知机制(Window d_)
        {
            d = d_;
            d.Loaded += Loaded;
            d.Closed += Closed;
        }

        private Window_通知机制 . UsbDeviceMonitor _usbMonitor;

        /// <summary>
        /// 进入窗体时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Loaded(object sender, RoutedEventArgs e)
        {
            // 获取窗口句柄
            IntPtr windowHandle = new WindowInteropHelper(d).Handle;

            // 创建USB监控器
            _usbMonitor = new Window_通知机制. UsbDeviceMonitor(windowHandle, "4D1E55B2-F16F-11CF-88CB-001111000030");
            _usbMonitor.DeviceChanged += UsbMonitor_DeviceChanged;

            // 挂钩窗口消息处理
           HwndSource.FromHwnd(windowHandle).AddHook(WndProc);
        }

        /// <summary>
        /// 退出窗体时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Closed(object sender, EventArgs e)
        {
            _usbMonitor?.Dispose();
        }


        /// <summary>
        /// 窗体通知
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="msg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <param name="handled"></param>
        /// <returns></returns>
        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            _usbMonitor?.HandleWindowMessage(hwnd, msg, wParam, lParam, ref handled);
            Event_WndProc?.Invoke(hwnd, msg, wParam, lParam);
            return IntPtr.Zero;
        }


        /// <summary>
        /// 加密狗插入或拔出产生事件
        /// </summary>
        /// <param name="isConnected"></param>
        /// <param name="deviceName"></param>
        private void UsbMonitor_DeviceChanged(bool isConnected, string deviceName)
        {
            Event_DogTw?.Invoke(isConnected);
        }

        /// <summary>
        /// 加密狗插入或拔出事件
        /// </summary>
        public event Action<bool> Event_DogTw;

        /// <summary>
        /// IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam
        /// </summary>
        public event Action<IntPtr, int, IntPtr, IntPtr> Event_WndProc;

        /// <summary>
        ///
        /// </summary>
        /// <param name="ui标题栏"></param>
        /// <param name="状态">true:正常,false:未检测到加密狗</param>
        public void 标题栏状态(ui_window_Title ui标题栏, bool 状态)
        {
            qfWPFmain._windowInfo_[] info = new _windowInfo_[]
               {
                new qfWPFmain._windowInfo_(Language_ .Get语言("加密狗"),0,""),
                new qfWPFmain._windowInfo_(Language_ .Get语言("加密狗"),-1,Language_ .Get语言("未检测到加密狗")),
               };
            int a = 状态 ? 0 : -1;
            ui标题栏.Add(info, a);
        }



    }
}
