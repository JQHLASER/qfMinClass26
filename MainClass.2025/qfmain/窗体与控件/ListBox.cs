using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
 

namespace mainclassqf
{
    public class Listbox
    {

        public class info_颜色标识_
        {
            public string 颜色标识 { set; get; }
            public Color 文本颜色 { set; get; }
        }




        private ListBox listbox_;

        public Listbox(ListBox listbox_my)
        {
            this.listbox_ = listbox_my;
        }



        /// <summary>
        /// 控件listBox,事件DrawItem
        /// </summary>
        /// <param name="ese"></param>
        /// <param name="listbox_"></param>
        /// <param name="标识">包含字符串时改变颜色</param>
        /// <param name="颜色">目前:Color.Black,Red,Lime,White</param>
        public void 设置指定行颜色_自定义颜色(DrawItemEventArgs ese, List<info_颜色标识_> lst)
        {

            if (ese.Index >= 0)
            {
                Brush mybsh = new SolidBrush(Color.Green);

                for (int i = 0; i < lst.Count; i++)
                {
                    string 标识 = lst[i].颜色标识;
                    Color Color颜色 = lst[i].文本颜色;

                    if (listbox_.Items[ese.Index].ToString().IndexOf(标识) != -1)
                    {
                        mybsh = new SolidBrush(Color颜色);


                    }

                }

                // 焦点框
               // ese.DrawFocusRectangle();//启用后选择其中一行会留下残影
                //文本 
                ese.Graphics.DrawString(listbox_.Items[ese.Index].ToString(), ese.Font, mybsh, ese.Bounds, StringFormat.GenericDefault);


            }

        }

        public void 上移( int 上移数量)
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
        public void 下移( int 下移数量)
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
 
 
        public void 下移指定行( int 下移数量, int 当前行索引)
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
        public void 上移指定行( int 上移数量, int 当前行索引)
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
   

        /// <summary>
        /// 在添加数据后,调用此方法
        /// </summary>
        /// <param name="listbox_"></param>
        public void 在指定位置插入( )
        {

            int count = listbox_.Items.Count;
            int index = listbox_.SelectedIndex;
            if (index < 0)
            {
                return;
            }
            int 上移数量 = count - index - 2;
            上移指定行( 上移数量, count - 1);
            listbox_.SelectedIndex = index + 1;
        }

        public void 选中指定行( int index)
        {
            listbox_.SelectedIndex = index;
        }

    }
}
