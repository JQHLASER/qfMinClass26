using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace qfWPFmain
{

    public partial class Log_viewModel : ObservableObject
    {
        public Log_viewModel()
        {

        }

        /// <summary>
        /// 数据源
        /// </summary>
        [ObservableProperty]
        private ObservableCollection<info_log_> logItems = new ObservableCollection<info_log_>();

        [ObservableProperty]
        private double size内容栏 = 500;

        /// <summary>
        ///选中行
        /// </summary>
        [ObservableProperty]
        private double selectedIndex = -1;

        ///// <summary>
        ///// 最大显示行
        ///// </summary>
        //[ObservableProperty]
        //private int maxCount = 200;

        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="logvalue"></param>
        void Add_Log_(qfmain.log日志._logValue_ logvalue)
        {
            if (!logvalue.内容.Contains(qfmain.log日志._不显示到日志栏))
            {
                info_log_ infoShow = new info_log_();
                infoShow.Dates = logvalue.时间.ToString("[HH:mm:ss.fff]");
                infoShow.States = $"{logvalue.状态}";
                infoShow.Logvalue = logvalue.内容;
                infoShow.LogvalueShow = new 文本().替换(infoShow.Logvalue, "\r", " ");
                infoShow.LogvalueShow = new 文本().替换(infoShow.LogvalueShow, "\n", " ");
                infoShow.TextColor = logvalue.状态 == qfmain.log日志.enum状态.Error ? Brushes.Red : logvalue.状态 == qfmain.log日志.enum状态.Info ? Brushes.Green : Brushes.Gray;

                this.LogItems.Add(infoShow);
            }
        }


        /// <summary>
        /// 清空日志
        /// </summary> 
        internal void Clear_Log()
        {
            this.LogItems.Clear();
        }


        /// <summary>
        /// 添加日志,在log添加事件中使用
        /// </summary>
        /// <param name="logvalue"></param>
        internal void Add_Log(qfmain.log日志._logValue_ logvalue, int 最大行数)
        {

            this.Add_Log_(logvalue);
            if (this.LogItems.Count > 最大行数)
            {
                this.LogItems.RemoveAt(0);

            }
            int count = this.LogItems.Count;
            if (count > 0)
            {
                //选中并滚动到最后一项....在数据上下文中操作
                this.SelectedIndex = this.LogItems.Count - 1;
            }

        }





    }
}
