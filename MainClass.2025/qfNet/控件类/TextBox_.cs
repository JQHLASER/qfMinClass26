
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
 

namespace qfNet
{
    public class TextBox_
    {

        public void 关闭输入法(TextBox t)
        {
            t.ImeMode = ImeMode.Disable;
            new textBox_关闭输入法().SetEnglishInput(t);
        }

        public void 关闭输入法(Sunny.UI.UITextBox t)
        {
            t.ImeMode = ImeMode.Disable;
            new textBox_关闭输入法().SetEnglishInput(t);
        }



    }

    /// <summary>
    /// 切换成英文
    /// </summary>
    internal class textBox_关闭输入法
    {
        [DllImport("imm32.dll")]
        private static extern IntPtr ImmGetContext(IntPtr hWnd);

        [DllImport("imm32.dll")]
        private static extern bool ImmSetOpenStatus(IntPtr hIMC, bool open);

        [DllImport("imm32.dll")]
        private static extern bool ImmReleaseContext(IntPtr hWnd, IntPtr hIMC);

        /// <summary>
        /// 关闭输入法（切换为英文）
        /// </summary>
        public void SetEnglishInput(Control control)
        {
            if (control == null) return;

            IntPtr hIMC = ImmGetContext(control.Handle);
            if (hIMC != IntPtr.Zero)
            {
                ImmSetOpenStatus(hIMC, false); // false = 关闭中文输入法
                ImmReleaseContext(control.Handle, hIMC);
            }
        }
    }
}
