using CommunityToolkit.Mvvm.ComponentModel;
using qfmain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace qfWPFmain
{
    public partial class 功能栏_ViewModel : ObservableObject
    {

        public 功能栏_ViewModel()
        {

        }

        /// <summary>
        /// 运行时间/系统时间
        /// </summary>
        [ObservableProperty]
        private _功能栏_时间类_ dateTimeRun = new _功能栏_时间类_(DateTime.Now, new TimeSpan());


        /// <summary>
        /// 商标1
        /// </summary>
        [ObservableProperty]
        private BitmapImage image_商标1 = null;



        /// <summary>
        /// 商标2
        /// </summary>
        [ObservableProperty]
        private BitmapImage image_商标2 = null;

        /// <summary>
        /// 商标1宽度
        /// </summary>
        [ObservableProperty]
        private double width_商标1 = 80;


        /// <summary>
        /// 商标2宽度
        /// </summary>
        [ObservableProperty]
        private double width_商标2 = 80;

        /// <summary>
        /// 显示信息内容
        /// </summary>    
        [ObservableProperty]
        private _状态栏_功能栏_Info_[] items_显示信息 = new _状态栏_功能栏_Info_[0];

        /// <summary>
        /// 功能内容
        /// </summary>    
        [ObservableProperty]
        private string title = "AutoControl";
        DateTime now0 = DateTime.Now;
        internal void 计算时间类()
        {
            DateTime now = DateTime.Now;
            TimeSpan tm = now - now0;
            DateTimeRun = new _功能栏_时间类_(now, tm);
        }



        List<string> lstName = new List<string>();
        
        private readonly object _lock = new object();
        internal void 计算显示功能内容(_状态栏_功能栏_Info_ info_)
        {
            lock (_lock)
            {
                List<_状态栏_功能栏_Info_> lstInfo = this.Items_显示信息.ToList();
                int a = lstName.IndexOf(info_.Name);
                if (a == -1)
                {
                    lstName.Add(info_.Name);
                    lstInfo.Add(info_);
                }
                else
                {
                    lstInfo[a] = info_;
                }
                this.Items_显示信息 = lstInfo.ToArray();
            }
        }


    }
}
