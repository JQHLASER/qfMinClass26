using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace qfWPFmain
{
    /// <summary>
    /// 窗体
    /// </summary>
    public class Window窗体_
    {


        /// <summary>
        /// 获取控件的父窗口
        /// </summary>
        /// <param name="child"></param>
        /// <returns></returns>
        public Window GetParentWindow(DependencyObject child)
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);

            // 如果已经找到顶层窗口，直接返回
            if (parentObject is Window window)
                return window;

            // 递归向上查找
            if (parentObject is not null)
                return GetParentWindow(parentObject);

            // 未找到窗口（通常是控件未加载到视觉树中）
            return null;
        }


    }
}
