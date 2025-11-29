using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace qfWPFmain
{
    internal partial class viewModel_信息显示 : ObservableObject
    { 
        public viewModel_信息显示()
        {

        }

        [ObservableProperty]
        private ObservableCollection<_信息显示_> items = new ObservableCollection<_信息显示_>();


        internal void Add(_信息显示_ info)
        {
            this.Items.Add(info);
        }

        internal void Clear()
        {
            this.Items.Clear();
        }


    }
}
