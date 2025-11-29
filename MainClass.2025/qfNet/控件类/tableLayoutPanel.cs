using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace qfNet
{
    public class tableLayoutPanel_
    {
        TableLayoutPanel _TableLayoutPanel;
        public tableLayoutPanel_(TableLayoutPanel TableLayoutPanel_)
        {
            this._TableLayoutPanel = TableLayoutPanel_;
        }

        public tableLayoutPanel_ 设置列宽度(int 列索引, int width)
        {
            this._TableLayoutPanel.ColumnStyles[列索引].Width = width;
            return this;
        }


        public tableLayoutPanel_ 设置行高度(int 行索引, int height)
        {
            this._TableLayoutPanel.RowStyles[行索引].Height = height;

            return this;
        }

        public tableLayoutPanel_ 添加控件(Control cons,int 行索引, int 列索引)
        {
            this._TableLayoutPanel.Controls.Add (cons,列索引 ,行索引 );

            return this;
        }





    }
}                                                   