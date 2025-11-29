using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfWPFmain
{
    internal partial class viewModel_软件信息 : ObservableObject
    {
        public viewModel_软件信息()
        {

        }

         

        [ObservableProperty]
        private string info_信息 = "";


        internal void Set(  string 信息)
        {
            this.Info_信息 = 信息;
    
        }




    }
}
