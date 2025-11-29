using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace qfWPFmain
{

    public partial class ListVieW附加属性
    {
        /// <summary>
        /// 光标跳转到最后一行
        /// </summary>
        public static readonly DependencyProperty ui_光标跳转到最后一行Property =
            DependencyProperty.RegisterAttached("AutoScrollToLastItem", typeof(bool), typeof(ListVieW附加属性),
                new PropertyMetadata(false, OnAutoScrollToLastItemChanged));
        private static void OnAutoScrollToLastItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var listView = d as ListView;
            if (listView == null)
            {
                return;
            }
            var newValue = (bool)e.NewValue;
            if (newValue)
            {
                ((INotifyCollectionChanged)listView.Items).CollectionChanged += (s, args) =>
                {
                    if (listView.Items.Count > 0)
                    {
                        listView.ScrollIntoView(listView.Items[listView.Items.Count - 1]);
                    }
                };
            }
        }



        /// <summary>
        /// Get
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool Getui_光标跳转到最后一行(DependencyObject obj)
        {
            return (bool)obj.GetValue(ui_光标跳转到最后一行Property);
        }
        /// <summary>
        /// Set
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void Setui_光标跳转到最后一行(DependencyObject obj, bool value)
        {
            obj.SetValue(ui_光标跳转到最后一行Property, value);
        }




    }
}
