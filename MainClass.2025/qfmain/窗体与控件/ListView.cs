 using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;
 


namespace mainclassqf
{
    public class ListView
    {
        System.Windows.Forms.ListView listview_;
        public ListView(System.Windows.Forms.ListView listview)
        {
            listview_ = listview;
        }


        public void 上移指定行(int 当前行号, int 移动行数)
        {
            if (当前行号 <= 0)
            {
                return;
            }

            listview_.BeginUpdate();
            foreach (ListViewItem lvi in listview_.SelectedItems)
            {
                ListViewItem item = lvi;
                int index = lvi.Index;
                listview_.Items.RemoveAt(index);
                listview_.Items.Insert(index - 移动行数, item);
                listview_.Items[当前行号].Focused = true;
            }
            listview_.EndUpdate();



        }

        public void 下移指定行(int 当前行号, int 移动行数)
        {
            if (当前行号 >= listview_.Items.Count - 1)
            {
                return;
            }

            listview_.BeginUpdate();
            foreach (ListViewItem lvi in listview_.SelectedItems)
            {
                ListViewItem item = lvi;
                int index = lvi.Index;
                listview_.Items.RemoveAt(index);
                listview_.Items.Insert(index + 移动行数, item);
                listview_.Items[当前行号].Focused = true;
            }
            listview_.EndUpdate();

        }



        /// <summary>
        /// 在添加数据后,调用此方法
        /// </summary>
        /// <param name="listbox_"></param>
        public void 在指定位置插入()
        {
            int index = listview_.SelectedIndices[0];
            if (index < 0)
            {
                return;
            }

            int count = listview_.Items.Count;
            int 上移数量 = count - index - 2;
            上移指定行(count - 1, 上移数量);
            选中指定行(index + 1);
            光标置于选定行(index);
        }

        /// <summary>
        /// 单行选中时
        /// </summary>
        /// <returns></returns>
        public int 获取选中行索引()
        {
            int index = -1;
            try
            {
                if (listview_.SelectedIndices.Count > 0)
                {
                    index = listview_.SelectedIndices[0];
                }
                return index;
            }
            catch (Exception)
            {
                return -1;
            }
        
        }

        public void 光标置于选定行(int 当前行号)
        {
            listview_.Items[当前行号].Focused = true;
        }


        public void 选中指定行(int 当前行号)
        {
            listview_.Items[当前行号].Selected = true;//选中指定行  
        }


        public void 滚动到指定位置(int 当前行号)
        {
            listview_.EnsureVisible(当前行号);//滚动到指定位置
        }
        public void 清除选中状态()
        {
            listview_.SelectedIndices.Clear();//清除被选中状态
        }
        public void 清除(int 当前行号)
        {
            listview_.Items.Clear();
        }


        public void 删除指定行(int 当前行号)
        {
            if (当前行号 >= listview_.Items.Count | 当前行号 < 0)
            {
                return;
            }
            listview_.Items[当前行号].Remove();


        }

        public void 设置指定列宽(int 列号, int width)
        {

            listview_.Columns[列号].Width = width;
        }

        public void 设置列标题(int 列号, string 列标题)
        {
            listview_.Columns[列号].Text = 列标题;
        }
        public string 获取列标题(int 列号)
        {
            return listview_.Columns[列号].Text;
        }

        public void 是否选中指定行(int 行号, bool 是否选中)
        {
            listview_.Items[行号].Selected = 是否选中;
        }

        public void 滚动到指定行(int 行号)
        {

            listview_.Items[行号].EnsureVisible();

        }

        public void 修改行高度(int 高度)
        {
            ImageList iList = new ImageList();
            iList.ImageSize = new Size(1, 高度);//宽度和高度值必须大于等于1且不超过256
            listview_.SmallImageList = iList;//这样的结果在第一列的前面多出了1个分量的宽，所有行的高度为24

        }

        public string 获取内容(int 行索引, int 列索引)
        {
            string xt = this.listview_.Items[行索引].SubItems[列索引].Text;
            return xt;
        }
        public void 设置内容(int 行索引, int 列索引, string 内容)
        {
            this.listview_.Items[行索引].SubItems[列索引].Text = 内容;
        }


        public void 清空全部数据()
        {
            this.listview_.Items.Clear();
        }

   

    }
     
}



