using Lierda.WPFHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfmain
{
    /// <summary>
    /// 自动GC,安装库: Lierda.WPFHelper
    /// </summary>
    public class GC_
    {
        //自动GC
        LierdaCracker cracker = new LierdaCracker();
        public GC_(int sleepSpan = 30)
        {
            cracker.Cracker(sleepSpan);
        }


        /// <summary>
        /// windowss专用
        /// </summary>
        public GC_()
        {
            new GC_Windows().Start();
        }
    }
}
