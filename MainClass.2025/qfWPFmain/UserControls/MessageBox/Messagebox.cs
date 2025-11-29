using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace qfWPFmain
{
    public class Messagebox
    {

        public static MessageBoxResult Show(Window d, string ShowValue, string Caption = "", MessageboxButton MessageboxButton = MessageboxButton.Ok, MessageboxState MessageBoxState_ = MessageboxState.None, bool is最大化 = false, int width = 450, int height = 300)
        {
            return new win_MessageBox(ShowValue, Caption, MessageboxButton, MessageBoxState_, is最大化, width, height) { Owner = Window.GetWindow(d) }.ShowDialog();
        }

        public static MessageBoxResult Show(string ShowValue, string Caption = "", MessageboxButton MessageboxButton = MessageboxButton.Ok, MessageboxState MessageBoxState_ = MessageboxState.None, bool is最大化 = false, int width = 450, int height = 300)
        {
            return new win_MessageBox(ShowValue, Caption, MessageboxButton, MessageBoxState_, is最大化, width, height).ShowDialog();
        }




    }
}
