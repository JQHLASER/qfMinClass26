using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace qfWPFmain
{

    public interface IWindowService
    {
      bool? ShowDialog<TWindow>(object viewModel = null) where TWindow : Window, new();
    }


    public  class WindowsShow : IWindowService
    {
        public bool? ShowDialog<TWindow>(object viewModel = null) where TWindow : Window, new()
        {
            var window = new TWindow { DataContext = viewModel, Owner = Application.Current.MainWindow };
            return window.ShowDialog();
        }
         

    }


   


}
