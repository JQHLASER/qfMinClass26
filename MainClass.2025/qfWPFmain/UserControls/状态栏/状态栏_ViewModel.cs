using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfWPFmain
{
    public partial class 状态栏_ViewModel : ObservableObject
    {

        public 状态栏_ViewModel()
        {

        }


        /// <summary>
        /// 状态数据
        /// </summary>
        [ObservableProperty]
        private _状态栏_功能栏_Info_[] items_显示信息 = new _状态栏_功能栏_Info_[0];



        List<string> lstName = new List<string>();


        // 用于同步的锁对象
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
                    if (string.IsNullOrEmpty(info_.内容))
                    {
                        lstName.RemoveAt(a);
                        lstInfo.RemoveAt(a);
                    }
                    else
                    {
                        lstInfo[a] = info_;
                    }
                }

                this.Items_显示信息 = lstInfo.ToArray();
            }
        }
    }
}
