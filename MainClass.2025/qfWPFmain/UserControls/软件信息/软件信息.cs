using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace qfWPFmain
{
    public class 软件信息
    {

        public virtual void Show(Window d, string 版本, string 信息)
        {
            new Win_软件信息(版本, 信息) { Owner = Window.GetWindow(d) }.ShowDialog();
        }


        public virtual void Show(string 版本, string 信息)
        {
            new Win_软件信息(版本, 信息).ShowDialog();
        }


    }
}
