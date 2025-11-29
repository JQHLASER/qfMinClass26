using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace qfNet
{
    public class ListView_
    {
        ListView _ListView;

        public ListView_(ListView _ListView_)
        {
            this._ListView = _ListView_;
        }

        public ListView_ 上移指定行(int 当前行号, int 移动行数)
        {
            if (当前行号 <= 0)
            {
                return this;
            }

            this._ListView.BeginUpdate();
            foreach (ListViewItem lvi in this._ListView.SelectedItems)
            {
                ListViewItem item = lvi;
                int index = lvi.Index;
                this._ListView.Items.RemoveAt(index);
                this._ListView.Items.Insert(index - 移动行数, item);
                this._ListView.Items[当前行号].Focused = true;
            }
            this._ListView.EndUpdate();


            return this;
        }

        public ListView_ 下移指定行(int 当前行号, int 移动行数)
        {
            if (当前行号 >= this._ListView.Items.Count - 1)
            {
                return this;
            }

            this._ListView.BeginUpdate();
            foreach (ListViewItem lvi in this._ListView.SelectedItems)
            {
                ListViewItem item = lvi;
                int index = lvi.Index;
                this._ListView.Items.RemoveAt(index);
                this._ListView.Items.Insert(index + 移动行数, item);
                this._ListView.Items[当前行号].Focused = true;
            }
            this._ListView.EndUpdate();
            return this;
        }


        /// <summary>
        /// 在添加数据后,调用此方法
        /// </summary>
        /// <param name="listbox_"></param>
        public ListView_ 在指定位置插入()
        {
            int index = this._ListView.SelectedIndices[0];
            if (index < 0)
            {
                return this;
            }

            int count = this._ListView.Items.Count;
            int 上移数量 = count - index - 2;
            this.上移指定行(count - 1, 上移数量);
            this.选中指定行(index + 1);
            this.光标置于选定行(index);
            return this;
        }

        public ListView_ 光标置于选定行(int 当前行号)
        {
            this._ListView.Items[当前行号].Focused = true;
            return this;
        }

        public ListView_ 选中指定行(int 当前行号)
        {
            this._ListView.Items[当前行号].Selected = true;//选中指定行  
            return this;
        }

        public ListView_ 滚动到指定位置(int 当前行号)
        {
            this._ListView.EnsureVisible(当前行号);//滚动到指定位置
            return this;
        }
        public ListView_ 清除选中状态(int 当前行号)
        {
            this._ListView.SelectedIndices.Clear();//清除被选中状态
            return this;
        }

        public ListView_ 删除指定行(int 当前行号)
        {
            if (当前行号 >= this._ListView.Items.Count | 当前行号 < 0)
            {
                return this;
            }
            this._ListView.Items[当前行号].Remove();
            return this;
        }

        public ListView_ 设置指定列宽(int 列号, int width)
        {

            this._ListView.Columns[列号].Width = width;
            return this;
        }

        public ListView_ 设置列标题(int 列号, string 列标题)
        {
            this._ListView.Columns[列号].Text = 列标题;
            return this;
        }
        public ListView_ 获取列标题(int 列号, out string value)
        {
            value = this._ListView.Columns[列号].Text;
            return this;
        }

        public ListView_ 是否选中指定行(int 行号, bool 是否选中)
        {
            this._ListView.Items[行号].Selected = 是否选中;
            return this;
        }

        public ListView_ 滚动到指定行(int 行号)
        {
            this._ListView.Items[行号].EnsureVisible();
            return this;
        }

        public ListView_ 修改行高度(int 高度)
        {
            ImageList iList = new ImageList();
            iList.ImageSize = new Size(1, 高度);//宽度和高度值必须大于等于1且不超过256
            this._ListView.SmallImageList = iList;//这样的结果在第一列的前面多出了1个分量的宽，所有行的高度为24
            return this;
        }


    }

}

