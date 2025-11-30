using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfWPFmain
{
    internal partial class viewModel_进度条 : ObservableObject
    {

        [ObservableProperty]
        private int value进度 = 0;

        [ObservableProperty]
        private string title = "初始化...";

        qfWPFmain.延时_Task delay_sys = new 延时_Task();
        bool isRun = true;
        internal void 线程()
        {
            isRun = true;
            while (isRun)
            {
                this.Value进度 += 1;
                Thread.Sleep(1000);
            }
        }

        internal void 释放()
        {
            isRun = false;
            this.Value进度 = 100;
            delay_sys.中断延时();
        }

    }
}
