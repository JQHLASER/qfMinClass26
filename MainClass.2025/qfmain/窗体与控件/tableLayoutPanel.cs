using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mainclassqf
{
    public class tableLayoutPanel_
    {
        public void 设置列宽度(TableLayoutPanel tab, int 列索引,int width)
        {
            tab.ColumnStyles[列索引].Width = width;
        }
        public void 设置行高度(TableLayoutPanel tab, int 行索引, int height)
        {
            tab.RowStyles[行索引].Height  = height;
        }


    }
}
