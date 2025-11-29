using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace qfmain
{

    public class 键盘_鼠标
    {
        // 创建结构体用于返回捕获时间
        [StructLayout(LayoutKind.Sequential)]
        struct LASTINPUTINFO
        {
            // 设置结构体块容量
            [MarshalAs(UnmanagedType.U4)]
            public int cbSize;
            // 捕获的时间
            [MarshalAs(UnmanagedType.U4)]
            public uint dwTime;
        }
        [DllImport("user32.dll")]
        private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);


        /// <summary>
        /// 获取键盘和鼠标没有操作的时间(ms)
        /// </summary>
        /// <returns></returns>
        public virtual long 获取键盘和鼠标没有操作的时间_ms()
        {
            LASTINPUTINFO vLastInputInfo = new LASTINPUTINFO();
            vLastInputInfo.cbSize = Marshal.SizeOf(vLastInputInfo);
            // 捕获时间
            if (!GetLastInputInfo(ref vLastInputInfo))
                return 0;
            else
                return Environment.TickCount - (long)vLastInputInfo.dwTime;
        }


        /*按全屏幕取得鼠标位置*/
        Timer mouseTimer = new Timer();
        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out Point pt);

        #region 模拟按键




        [System.Runtime.InteropServices.DllImport("user32")]
        private static extern int mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);
        //移动鼠标 
        const int MOUSEEVENTF_MOVE = 0x0001;

        //模拟鼠标左键按下 
        const int MOUSEEVENTF_LEFTDOWN = 0x0002;

        //模拟鼠标左键抬起 
        const int MOUSEEVENTF_LEFTUP = 0x0004;

        //模拟鼠标右键按下 
        const int MOUSEEVENTF_RIGHTDOWN = 0x0008;

        //模拟鼠标右键抬起 
        const int MOUSEEVENTF_RIGHTUP = 0x0010;

        //模拟鼠标中键按下 
        const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;

        //模拟鼠标中键抬起 
        const int MOUSEEVENTF_MIDDLEUP = 0x0040;

        //标示是否采用绝对坐标 
        const int MOUSEEVENTF_ABSOLUTE = 0x8000;

        //模拟鼠标滚轮滚动操作，必须配合dwData参数
        const int MOUSEEVENTF_WHEEL = 0x0800;


        /// <summary>
        /// 1:移动鼠标;2:鼠标左键按下,4:鼠标左键抬起;8鼠标右键按下;10鼠标右键抬起;20鼠标中键按下;40鼠标中键抬起;8000:标示是否采用绝对坐标, 800:模拟鼠标滚轮滚动操作，必须配合dwData参数
        /// </summary>
        /// <param name="参数"></param>
        /// <param name="x坐标"></param>
        /// <param name="y坐标"></param>
        /// <param name="dwData">一般为0</param>
        /// <param name="dwExtraInfo">一般为0</param>
        public virtual  void 模拟鼠标按键(int 参数, int x坐标, int y坐标, int dwData, int dwExtraInfo)
        {
            switch (参数)
            {
                case 1:
                    mouse_event(MOUSEEVENTF_MOVE, x坐标, y坐标, dwData, dwExtraInfo);
                    break;
                case 2:
                    mouse_event(MOUSEEVENTF_LEFTDOWN, x坐标, y坐标, dwData, dwExtraInfo);
                    break;
                case 4:
                    mouse_event(MOUSEEVENTF_LEFTUP, x坐标, y坐标, dwData, dwExtraInfo);
                    break;

                case 8:
                    mouse_event(MOUSEEVENTF_RIGHTDOWN, x坐标, y坐标, dwData, dwExtraInfo);
                    break;
                case 10:
                    mouse_event(MOUSEEVENTF_RIGHTUP, x坐标, y坐标, dwData, dwExtraInfo);
                    break;

                case 20:
                    mouse_event(MOUSEEVENTF_MIDDLEDOWN, x坐标, y坐标, dwData, dwExtraInfo);
                    break;

                case 40:
                    mouse_event(MOUSEEVENTF_MIDDLEUP, x坐标, y坐标, dwData, dwExtraInfo);
                    break;

                case 8000:
                    mouse_event(MOUSEEVENTF_ABSOLUTE, x坐标, y坐标, dwData, dwExtraInfo);
                    break;

                case 800:
                    mouse_event(MOUSEEVENTF_WHEEL, x坐标, y坐标, dwData, dwExtraInfo);
                    break;
            }

        }




        #endregion

        #region 模拟键盘

        public class KeyBoard
        {

            public const byte 鼠标左键 = 0x1;    // 鼠标左键
            public const byte 鼠标右键 = 0x2;    // 鼠标右键
            public const byte CANCEL键 = 0x3;     // CANCEL 键
            public const byte 鼠标中键 = 0x4;    // 鼠标中键
            public const byte BACKSPACE键 = 0x8;       // BACKSPACE 键
            public const byte TAB键 = 0x9;        // TAB 键
            public const byte CLEAR键 = 0xC;      // CLEAR 键
            public const byte ENTER键 = 0xD;     // ENTER 键
            public const byte SHIFT键 = 0x10;     // SHIFT 键
            public const byte CTRL键 = 0x11;   // CTRL 键
            public const byte Alt键 = 18;         // Alt 键  (键码18)
            public const byte MENU键 = 0x12;      // MENU 键
            public const byte PAUSE键 = 0x13;     // PAUSE 键
            public const byte CAPS_LOCK键 = 0x14;   // CAPS LOCK 键
            public const byte ESC键 = 0x1B;    // ESC 键
            public const byte SPACEBAR键 = 0x20;     // SPACEBAR 键
            public const byte PAGE_UP键 = 0x21;    // PAGE UP 键
            public const byte End键 = 0x23;       // End 键
            public const byte HOME键 = 0x24;      // HOME 键
            public const byte LEFT键 = 0x25;      // LEFT ARROW 键
            public const byte UP键 = 0x26;        // UP ARROW 键
            public const byte RIGHT键 = 0x27;     // RIGHT ARROW 键
            public const byte DOWN键 = 0x28;      // DOWN ARROW 键
            public const byte Select键 = 0x29;    // Select 键
            public const byte PRINT_SCREEN键 = 0x2A;     // PRINT SCREEN 键
            public const byte EXECUTE键 = 0x2B;   // EXECUTE 键
            public const byte SNAPSHOT键 = 0x2C;  // SNAPSHOT 键
            public const byte Delete键 = 0x2E;    // Delete 键
            public const byte HELP键 = 0x2F;      // HELP 键
            public const byte NUM_LOCK键 = 0x90;   // NUM LOCK 键

            //字母键A到Z
            public const byte A = 65;
            public const byte B = 66;
            public const byte C = 67;
            public const byte D = 68;
            public const byte E = 69;
            public const byte F = 70;
            public const byte G = 71;
            public const byte H = 72;
            public const byte I = 73;
            public const byte J = 74;
            public const byte K = 75;
            public const byte L = 76;
            public const byte M = 77;
            public const byte N = 78;
            public const byte O = 79;
            public const byte P = 80;
            public const byte Q = 81;
            public const byte R = 82;
            public const byte S = 83;
            public const byte T = 84;
            public const byte U = 85;
            public const byte V = 86;
            public const byte W = 87;
            public const byte X = 88;
            public const byte Y = 89;
            public const byte Z = 90;

            //数字键盘0到9
            public const byte vKey0 = 48;    // 0 键
            public const byte vKey1 = 49;    // 1 键
            public const byte vKey2 = 50;    // 2 键
            public const byte vKey3 = 51;    // 3 键
            public const byte vKey4 = 52;    // 4 键
            public const byte vKey5 = 53;    // 5 键
            public const byte vKey6 = 54;    // 6 键
            public const byte vKey7 = 55;    // 7 键
            public const byte vKey8 = 56;    // 8 键
            public const byte vKey9 = 57;    // 9 键


            public const byte vKeyNumpad0 = 0x60;    //0 键
            public const byte vKeyNumpad1 = 0x61;    //1 键
            public const byte vKeyNumpad2 = 0x62;    //2 键
            public const byte vKeyNumpad3 = 0x63;    //3 键
            public const byte vKeyNumpad4 = 0x64;    //4 键
            public const byte vKeyNumpad5 = 0x65;    //5 键
            public const byte vKeyNumpad6 = 0x66;    //6 键
            public const byte vKeyNumpad7 = 0x67;    //7 键
            public const byte vKeyNumpad8 = 0x68;    //8 键
            public const byte vKeyNumpad9 = 0x69;    //9 键
            public const byte vKeyMultiply = 0x6A;   // MULTIPLICATIONSIGN(*)键
            public const byte vKeyAdd = 0x6B;        // PLUS SIGN(+) 键
            public const byte vKeySeparator = 0x6C;  // ENTER 键
            public const byte vKeySubtract = 0x6D;   // MINUS SIGN(-) 键
            public const byte vKeyDecimal = 0x6E;    // DECIMAL POINT(.) 键
            public const byte vKeyDivide = 0x6F;     // DIVISION SIGN(/) 键


            //F1到F12按键
            public const byte F1 = 0x70;   //F1 键
            public const byte F2 = 0x71;   //F2 键
            public const byte F3 = 0x72;   //F3 键
            public const byte F4 = 0x73;   //F4 键
            public const byte F5 = 0x74;   //F5 键
            public const byte F6 = 0x75;   //F6 键
            public const byte F7 = 0x76;   //F7 键
            public const byte F8 = 0x77;   //F8 键
            public const byte F9 = 0x78;   //F9 键
            public const byte F10 = 0x79;  //F10 键
            public const byte F11 = 0x7A;  //F11 键
            public const byte F12 = 0x7B;  //F12 键


            // <param name="bVk" >按键的虚拟键值</param>
            // <param name= "bScan" >扫描码，一般不用设置，用0代替就行</param>
            // <param name= "dwFlags" >选项标志：0：表示按下，2：表示松开</param>
            // <param name= "dwExtraInfo">一般设置为0</param>
            [DllImport("user32.dll")]
            public static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

            public static void keyPress(byte keyName)//定义“按一下”方法
            {
                KeyBoard.keybd_event(keyName, 0, 0, 0);
                KeyBoard.keybd_event(keyName, 0, 2, 0);
            }
        }

        public KeyBoard KeyBoard键值 = new KeyBoard();



        /// <summary>
        /// 延时时间:ms
        /// <para>键值: KeyBoard....例:    mainclassqf .键盘_鼠标.KeyBoard.F11</para>
        /// </summary>
        /// <param name="延时时间"></param>
        public virtual  void 模拟键盘(int 延时时间, byte 键值)
        {
            System.Threading.Thread.Sleep(延时时间);//延迟 msel 毫秒
            KeyBoard.keyPress(键值);//按一下后退(back)键
        }




        #endregion
         
        #region 热键

        [DllImport("kernel32.dll")]
        public static extern uint GetLastError();
        //如果函数执行成功，返回值不为0。
        //如果函数执行失败，返回值为0。要得到扩展错误信息，调用GetLastError。
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool RegisterHotKey(
            IntPtr hWnd,                //要定义热键的窗口的句柄
            int id,                     //定义热键ID（不能与其它ID重复）           
            KeyModifiers fsModifiers,   //标识热键是否在按Alt、Ctrl、Shift、Windows等键时才会生效
            Keys vk                     //定义热键的内容
            );

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool UnregisterHotKey(
            IntPtr hWnd,                //要取消热键的窗口的句柄
            int id                      //要取消热键的ID
            );

        //定义了辅助键的名称（将数字转变为字符以便于记忆，也可去除此枚举而直接使用数值）
        [Flags()]

        public enum KeyModifiers
        {
            None = 0,
            Alt = 1,
            Ctrl = 2,
            Shift = 4,
            WindowsKey = 8
        }
        /// <summary>
        /// 注册热键
        /// </summary>
        /// <param name="hwnd">窗口句柄</param>
        /// <param name="hotKey_id">热键ID</param>
        /// <param name="keyModifiers">组合键</param>
        /// <param name="key">热键</param>
        public virtual void RegKey(IntPtr hwnd, int hotKey_id, KeyModifiers keyModifiers, Keys key)
        {
            try
            {
                if (!RegisterHotKey(hwnd, hotKey_id, keyModifiers, key))
                {
                    if (Marshal.GetLastWin32Error() == 1409) { MessageBox.Show("热键被占用 ！"); }
                    else
                    {
                        MessageBox.Show("注册热键失败！");
                    }
                }
            }
            catch (Exception) { }
        }
        /// <summary>
        /// 注销热键
        /// </summary>
        /// <param name="hwnd">窗口句柄</param>
        /// <param name="hotKey_id">热键ID</param>
        public virtual  void UnRegKey(IntPtr hwnd, int hotKey_id)
        {
            //注销Id号为hotKey_id的热键设定
            UnregisterHotKey(hwnd, hotKey_id);
        }










        #endregion






    }
}
