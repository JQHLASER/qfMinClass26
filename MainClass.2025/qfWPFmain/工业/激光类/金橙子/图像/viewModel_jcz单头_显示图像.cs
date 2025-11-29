using CommunityToolkit.Mvvm.ComponentModel;
using qfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace qfWPFmain
{
    internal partial class viewModel_jcz单头_显示图像 : ObservableObject
    {
        public viewModel_jcz单头_显示图像()
        {

        }

        
 
     


        /// <summary>
        /// 图像
        /// </summary>
        [ObservableProperty]
        private ImageSource img = new BitmapImage();

        [ObservableProperty]
        private string text_未初始化提示 = Language_.Get语言("打标卡")+Language_ .Get语言 ("未初始化");

        [ObservableProperty]
        private GridLength height_菜单栏 = new GridLength(0, GridUnitType.Star);

        [ObservableProperty]
        private GridLength height_未初始化提示 = new GridLength(1, GridUnitType.Star);

        [ObservableProperty]
        private GridLength height_image = new GridLength(0, GridUnitType.Star);

        /// <summary>
        /// 未初始化标签
        /// </summary>
        [ObservableProperty]
        private Visibility  visibility_未初始化 = Visibility.Visible ;

        /// <summary>
        /// 图像
        /// </summary>
        [ObservableProperty]
        private Visibility visibility_img= Visibility.Collapsed;




        /// <summary>
        /// 控件宽度
        /// </summary>
        [ObservableProperty]
        private double width = 100;

        /// <summary>
        /// 控件高度
        /// </summary>
        [ObservableProperty]
        private double height = 100;

        /// <summary>
        /// 父窗体
        /// </summary>
        [ObservableProperty]
        private Window ui = new Window();


      
    }
}
