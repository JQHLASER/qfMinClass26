
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




        public void 上移(ListBox listbox_, int 上移数量)
        {
            try
            {
                string item = listbox_.SelectedItem.ToString();
                int index = listbox_.SelectedIndex;
                if (index <= 0)
                {
                    return;
                }
                else if (index - 上移数量 < 0)
                {
                    return;
                }
                listbox_.Items.RemoveAt(index);
                listbox_.Items.Insert(index - 上移数量, item);
                listbox_.SelectedIndex = index - 上移数量;
            }
            catch (Exception)
            {
            }

        }


        public void 下移(ListBox listbox_, int 下移数量)
        {
            try
            {
                string item = listbox_.SelectedItem.ToString();
                int count = listbox_.Items.Count;
                int index = listbox_.SelectedIndex;
                if (index >= count - 1 || index < 0)
                {
                    return;
                }
                else if (index + 下移数量 > count - 1)
                {
                    return;
                }
                listbox_.Items.RemoveAt(index);
                listbox_.Items.Insert(index + 下移数量, item);
                listbox_.SelectedIndex = index + 下移数量;
            }
            catch (Exception)
            {
            }

        }



        public void 上移(UIListBox listbox_, int 上移数量)
        {
            try
            {
                int index = listbox_.SelectedIndex;
                string item = listbox_.Items[index].ToString();

                if (index <= 0)
                {
                    return;
                }
                else if (index - 上移数量 < 0)
                {
                    return;
                }
                listbox_.Items.RemoveAt(index);
                listbox_.Items.Insert(index - 上移数量, item);
                listbox_.SelectedIndex = index - 上移数量;
            }
            catch (Exception)
            {
            }

        }


        public void 下移(UIListBox listbox_, int 下移数量)
        {
            try
            {
                int count = listbox_.Items.Count;
                int index = listbox_.SelectedIndex;
                string item = listbox_.Items[index].ToString();


                if (index >= count - 1 || index < 0)
                {
                    return;
                }
                else if (index + 下移数量 > count - 1)
                {
                    return;
                }
                listbox_.Items.RemoveAt(index);
                listbox_.Items.Insert(index + 下移数量, item);
                listbox_.SelectedIndex = index + 下移数量;
            }
            catch (Exception)
            {
            }

        }



        public void 上移指定行(UIListBox listbox_, int 上移数量, int 当前行索引)
        {
            try
            {
                int index = 当前行索引;
                // int indexSet= listbox_.SelectedIndex;
                string item = listbox_.Items[index].ToString();

                if (index <= 0)
                {
                    return;
                }
                else if (index - 上移数量 < 0)
                {
                    return;
                }
                listbox_.Items.RemoveAt(index);
                listbox_.Items.Insert(index - 上移数量, item);
                listbox_.SelectedIndex = index - 上移数量;
            }
            catch (Exception)
            {
            }

        }


        public void 下移指定行(ListBox listbox_, int 下移数量, int 当前行索引)
        {


            try
            {
                string item = listbox_.SelectedItem.ToString();
                int count = listbox_.Items.Count;
                int index = 当前行索引;
                //int indexSel = listbox_.SelectedIndex;
                if (index >= count - 1 || index < 0)
                {
                    return;
                }
                else if (index + 下移数量 > count - 1)
                {
                    return;
                }
                listbox_.Items.Remove(item);
                listbox_.Items.Insert(index + 下移数量, item);
                listbox_.SelectedIndex = index + 下移数量;
            }
            catch (Exception)
            {
            }

        }




        public void 上移指定行(ListBox listbox_, int 上移数量, int 当前行索引)
        {
            try
            {
                int index = 当前行索引;
                // int indexSet= listbox_.SelectedIndex;
                string item = listbox_.Items[index].ToString();

                if (index <= 0)
                {
                    return;
                }
                else if (index - 上移数量 < 0)
                {
                    return;
                }
                listbox_.Items.RemoveAt(index);
                listbox_.Items.Insert(index - 上移数量, item);
                listbox_.SelectedIndex = index - 上移数量;
            }
            catch (Exception)
            {
            }

        }


        public void 下移指定行(UIListBox listbox_, int 下移数量, int 当前行索引)
        {


            try
            {
                string item = listbox_.SelectedItem.ToString();
                int count = listbox_.Items.Count;
                int index = 当前行索引;
                //int indexSel = listbox_.SelectedIndex;
                if (index >= count - 1 || index < 0)
                {
                    return;
                }
                else if (index + 下移数量 > count - 1)
                {
                    return;
                }
                listbox_.Items.Remove(item);
                listbox_.Items.Insert(index + 下移数量, item);
                listbox_.SelectedIndex = index + 下移数量;
            }
            catch (Exception)
            {
            }

        }




        /// <summary>
        /// 在添加数据后,调用此方法
        /// </summary>
        /// <param name="listbox_"></param>
        public void 在指定位置插入(UIListBox listbox_)
        {

            int count = listbox_.Items.Count;
            int index = listbox_.SelectedIndex;
            if (index < 0)
            {
                return;
            }
            int 上移数量 = count - index - 2;
            上移指定行(listbox_, 上移数量, count - 1);
            listbox_.SelectedIndex = index + 1;
        }


        /// <summary>
        /// 在添加数据后,调用此方法
        /// </summary>
        /// <param name="listbox_"></param>
        public void 在指定位置插入(ListBox listbox_)
        {

            int count = listbox_.Items.Count;
            int index = listbox_.SelectedIndex;
            if (index < 0)
            {
                return;
            }
            int 上移数量 = count - index - 2;
            上移指定行(listbox_, 上移数量, count - 1);
            listbox_.SelectedIndex = index + 1;
        }

        public void 选中指定行(ListBox listbox_,int index)
        {
            listbox_.SelectedIndex = index;
        }

    }
}
