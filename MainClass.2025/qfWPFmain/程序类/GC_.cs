using Lierda.WPFHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace qfWPFmain
{
    /// <summary>
    /// 自动GC,安装库: Lierda.WPFHelper
    /// </summary>
    public partial class GC_
    {
        //自动GC
        LierdaCracker cracker = new LierdaCracker();
        public GC_( int sleepSapn=30)
        {
            cracker.Cracker(sleepSapn);
        }

    }
}
