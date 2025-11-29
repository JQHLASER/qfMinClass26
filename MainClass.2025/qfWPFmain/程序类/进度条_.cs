using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace qfWPFmain
{
    public class 进度条_ : Language_
    {
        static Window win = null;
        public void 打开(Window d, string 标题 = "")
        {
            if (string.IsNullOrEmpty (标题 ))
            {
                标题 = Get语言("初始化");
            }
            win = new Win_进度条(标题) { Owner = Window.GetWindow(d) };
            win.Show();
        }


        public void 关闭()
        {
            if (win is null)
            {
                return;
            }
            win.Close();
        }


    }
}
