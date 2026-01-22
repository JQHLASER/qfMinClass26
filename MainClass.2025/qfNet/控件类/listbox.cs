
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace qfNet
{
    public class listbox_
    {
       

        /// <summary>
        /// NG标识用来将当前有故障的行变成红色;;代码放在DrawItem事件下
        /// </summary>
        /// <param name="ese"></param>
        /// <param name="listbox_"></param>
        /// <param name="NG标识"></param>
        public void 设置指定行颜色(DrawItemEventArgs ese, ListBox listbox_, string NG标识)
        {

            if (ese.Index >= 0)
            {

                Brush mybsh = Brushes.Black;

                if (listbox_.Items[ese.Index].ToString().IndexOf(NG标识) != -1)
                {
                    mybsh = Brushes.Red;
                }
                else
                {
                    mybsh = Brushes.Black;
                }
                // 判断是什么类型的标签
                //if (listbox_.Items[ese.Index].ToString().IndexOf("Err") != -1)
                //{
                //    mybsh = Brushes.Red ;
                //}
                //else if (listbox_.Items[ese.Index].ToString().IndexOf("Err") != -1)
                //{
                //    mybsh = Brushes.Red;
                //}
                // e.Graphics.FillRectangle(Brushes.Red, e.Bounds);//背景色

                // 焦点框
                ese.DrawFocusRectangle();
                //文本 

                ese.Graphics.DrawString(listbox_.Items[ese.Index].ToString(), ese.Font, mybsh, ese.Bounds, StringFormat.GenericDefault);



            }

        }



        /// <summary>
        /// 控件listBox,事件DrawItem
        /// </summary>
        /// <param name="ese"></param>
        /// <param name="listbox_"></param>
        /// <param name="标识">包含字符串时改变颜色</param>
        /// <param name="颜色">目前:Color.Black,Red,Lime,White</param>
        public void 设置指定行颜色_自定义颜色(DrawItemEventArgs ese, ListBox listbox_, List<string> lst标识集合, List<Color> LstColor颜色)
        {

            if (ese.Index >= 0)
            {
                Brush mybsh = Brushes.Black;

                for (int i = 0; i < lst标识集合.Count; i++)
                {
                    string 标识 = lst标识集合[i];
                    Color Color颜色 = LstColor颜色[i];

                    if (listbox_.Items[ese.Index].ToString().IndexOf(标识) != -1)
                    {
                        if (Color颜色 == Color.Black)
                        {
                            mybsh = Brushes.Black;
                        }
                        if (Color颜色 == Color.Red)
                        {
                            mybsh = Brushes.Red;
                        }
                        if (Color颜色 == Color.Lime)
                        {
                            mybsh = Brushes.Lime;
                        }

                        if (Color颜色 == Color.White)
                        {
                            mybsh = Brushes.White;
                        }
                        if (Color颜色 == Color.Blue)
                        {
                            mybsh = Brushes.Blue;
                        }






                    }

                }








                //if (listbox_.Items[ese.Index].ToString().IndexOf(标识) != -1)
                //{
                //    if (Color颜色 == Color.Black)
                //    {
                //        mybsh = Brushes.Black;
                //    }
                //    if (Color颜色 == Color.Red)
                //    {
                //        mybsh = Brushes.Red;
                //    }
                //    if (Color颜色 == Color.Lime)
                //    {
                //        mybsh = Brushes.Lime;
                //    }

                //    if (Color颜色 == Color.White)
                //    {
                //        mybsh = Brushes.White;
                //    }







                //}
                //else
                //{
                //    mybsh = Brushes.Black;
                //}
                //// 判断是什么类型的标签
                //if (listbox_.Items[ese.Index].ToString().IndexOf("Err") != -1)
                //{
                //    mybsh = Brushes.Red ;
                //}
                //else if (listbox_.Items[ese.Index].ToString().IndexOf("Err") != -1)
                //{
                //    mybsh = Brushes.Red;
                //}
                // e.Graphics.FillRectangle(Brushes.Red, e.Bounds);//背景色

                // 焦点框
                ese.DrawFocusRectangle();
                //文本 

                ese.Graphics.DrawString(listbox_.Items[ese.Index].ToString(), ese.Font, mybsh, ese.Bounds, StringFormat.GenericDefault);



            }

        }


        public void 上移(ListBox listBox, int MoveCount)
        {
            if (MoveCount <= 0 || MoveCount >= listBox.Items.Count)
                return;
            var indices = listBox.SelectedIndices.Cast<int>().OrderBy(i => i).ToList();
            foreach (var index in indices)
            {
                int newIndex = index - MoveCount;
                if (newIndex < 0) continue;

                var item = listBox.Items[index];
                listBox.Items.RemoveAt(index);
                listBox.Items.Insert(newIndex, item);
                listBox.SetSelected(newIndex, true);
            }
        }

        public void 下移(ListBox listBox, int MoveCount)
        {
            if (MoveCount <= 0 || MoveCount >= listBox.Items.Count)
                return;

            var indices = listBox.SelectedIndices.Cast<int>()
                            .OrderByDescending(i => i) // 关键点：倒序
                            .ToList();

            foreach (var index in indices)
            {
                int newIndex = index + MoveCount;
                if (newIndex >= listBox.Items.Count) continue;

                var item = listBox.Items[index];
                listBox.Items.RemoveAt(index);
                listBox.Items.Insert(newIndex, item);
                listBox.SetSelected(newIndex, true);

            }
        }


        public void 上移(Sunny.UI.UIListBox listBox, int MoveCount)
        {
            if (MoveCount <= 0 || MoveCount >= listBox.Items.Count)
                return;
            int index = listBox.SelectedIndex;
            if (index < 0) return; // 没有选中项就直接返回

            int newIndex = index - MoveCount;
            if (newIndex < 0)
                newIndex = 0; // 防止越界

            var item = listBox.Items[index];
            listBox.Items.RemoveAt(index);
            listBox.Items.Insert(newIndex, item);

            listBox.SelectedIndex = newIndex; // 重新选中新位置
        }


        public void 下移(Sunny.UI.UIListBox listBox, int MoveCount)
        {
            if (MoveCount <= 0 || MoveCount >= listBox.Items.Count)
                return;
            int index = listBox.SelectedIndex;
            if (index < 0) return;

            int newIndex = index + MoveCount;
            if (newIndex >= listBox.Items.Count)
                newIndex = listBox.Items.Count - 1;

            var item = listBox.Items[index];
            listBox.Items.RemoveAt(index);
            listBox.Items.Insert(newIndex, item);

            listBox.SelectedIndex = newIndex; // 重新选中新位置
        }


        public void 在指定位置插入<T>(UIListBox listbox_, T item, int index)
        {
            listbox_.Items.Insert(index, item);
        }
        public void 在指定位置插入<T>(ListBox listbox_, T item, int index)
        {
            listbox_.Items.Insert(index, item);
        }

        public void 选中指定行(ListBox listbox_, int index)
        {
            listbox_.SelectedIndex = index;
        }

    }
}
