using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace qfWPFmain
{
    public class 进程_ : qfmain.进程
    {
        /// <summary>
        /// wpf
        /// </summary>
        public void Wpf_结束自身进程()
        {
            Process current = Process.GetCurrentProcess();
            Application.Current.Shutdown();
        }
         
    }
}
