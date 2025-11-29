using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace qfWPFmain
{


    public class info_log_
    {
        /// <summary>
        /// 时间
        /// </summary>
        public string Dates { set; get; } = "";
        /// <summary>
        /// 状态
        /// </summary>
        public string States { set; get; } = "";
        /// <summary>
        /// 内容
        /// </summary>
        public string Logvalue { set; get; } = "";

        /// <summary>
        /// 内容,显示
        /// </summary>
        public string LogvalueShow { set; get; } = "";
        public Brush TextColor { set; get; } = Brushes.Red;
                 
    }



}
