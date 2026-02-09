using qfmain;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
namespace qfNet
{
    public class DataGridview_
    {
        DataGridView _datagridview;
        public DataGridview_(DataGridView datagridview_)
        {
            this._datagridview = datagridview_;

        }

        #region 方法

        public DataGridview_ 删除指定行(int 行号)
        {
            this._datagridview.Rows.RemoveAt(行号);
            return this;
        }

        public DataGridview_ 删除指定列(int 列号)
        {
            this._datagridview.Columns.RemoveAt(列号);
            return this;
        }

        public DataGridview_ 设置背景颜色_列标题(dynamic 列号, Color color_)
        {
            this._datagridview.EnableHeadersVisualStyles = false;//需要
            this._datagridview.Columns[列号].HeaderCell.Style.BackColor = color_;
            return this;
        }
        public DataGridview_ 设置文本颜色_列标题(dynamic 列号, Color color_)
        {
            this._datagridview.EnableHeadersVisualStyles = false;//需要
            this._datagridview.Columns[列号].HeaderCell.Style.ForeColor = color_;
            return this;
        }

        public DataGridview_ 设置文本颜色_列标题(  Color color_)
        {
            this._datagridview.EnableHeadersVisualStyles = false;//需要
            this._datagridview.ColumnHeadersDefaultCellStyle.ForeColor = color_;
            return this;
       
        }
        public DataGridview_ 设置背景颜色_列标题(Color color_)
        {
            this._datagridview.EnableHeadersVisualStyles = false;//需要
            this._datagridview.ColumnHeadersDefaultCellStyle.BackColor = color_;
            return this;

        }


        public DataGridview_ 设置字体_列标题(dynamic 列号, Font fonts)
        {
            this._datagridview.EnableHeadersVisualStyles = false;//需要
            this._datagridview.Columns[列号].headerCell.stale.Font = fonts;
            return this;
        }



        public DataGridview_ 设置列标题高度(int 高度)
        {
            this._datagridview.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this._datagridview.ColumnHeadersHeight = 高度;
            this._datagridview.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            return this;
        }


        /// <summary>
        /// 显示格式:yyyy/MM/dd HH:mm:ss
        /// </summary>
        /// <param name="datagridview"></param>
        /// <param name="列号"></param>
        /// <param name="显示格式">yyyy/MM/dd HH:mm:ss</param>
        public DataGridview_ 设置时间显示格式(dynamic 列号, string 显示格式)
        {
            this._datagridview.Columns[列号].DefaultCellStyle.Format = 显示格式;
            return this;
        }

        public DataGridview_ 设置网格线颜色(Color color_)
        {
            this._datagridview.GridColor = color_;
            return this;
        }


        public DataGridview_ 显示or隐藏标题(bool T显示F隐藏)
        {
            this._datagridview.ColumnHeadersVisible = T显示F隐藏;
            return this;
        }

        public DataGridview_ 显示or隐藏列(dynamic 列号, bool T显示F隐藏)
        {
            this._datagridview.Columns[列号].Visible = T显示F隐藏;
            return this;
        }

        public DataGridview_ 显示or隐藏行(int 行号, bool T显示F隐藏)
        {
            this._datagridview.Rows[行号].Visible = T显示F隐藏; //隐藏行
            return this;
        }

        public DataGridview_ 使能修改列宽(bool T使能F不使能)
        {
            this._datagridview.AllowUserToResizeColumns = T使能F不使能;


            return this;
        }

        public DataGridview_ 使能修改行高(bool T使能F不使能)
        {
            this._datagridview.AllowUserToResizeRows = T使能F不使能;

            return this;
        }

        public DataGridview_ 使能同时选择多个行or列or单元格(bool T使能F不使能)
        {
            this._datagridview.MultiSelect = T使能F不使能;
            return this;
        }

        /// <summary>
        /// 将光标也移到指定行
        /// </summary>
        /// <param name="datagridview"></param>
        /// <param name="列号"></param>
        /// <param name="行号"></param>
        public DataGridview_ 选中指定行(dynamic 列号, int 行号)
        {
            this._datagridview.Rows[行号].Selected = true;
            this._datagridview.CurrentCell = this._datagridview[列号, 行号]; //将光标定位到指定的行
            return this;
        }

        /// <summary>
        /// 清除行被选中的背景
        /// </summary>
        /// <param name="datagridview"></param>
        /// <param name="行号"></param>
        public DataGridview_ 清除行选中光标(int 行号)
        {
            this._datagridview.Rows[行号].Selected = false;
            return this;
        }


        public DataGridview_ 将光标移到指定行(dynamic 列号, int 行号)
        {
            this._datagridview.CurrentCell = this._datagridview[列号, 行号];//将光标定位到指定的行
            return this;
        }

        /// <summary>
        /// 由于行多时被其它的挡住了,这个方法可以显示出来
        /// </summary>
        /// <param name="datagridview"></param>
        /// <param name="行号"></param>
        public DataGridview_ 显示到指定行_行多时始终从指定行开始显示(int 行号)
        {
            this._datagridview.FirstDisplayedScrollingRowIndex = 行号;
            return this;
        }


        public DataGridview_ 使能整列选中()
        {
            this._datagridview.SelectionMode = DataGridViewSelectionMode.FullColumnSelect;
            return this;
        }

        public DataGridview_ 使能整行选中()
        {
            this._datagridview.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            return this;
        }

        public DataGridview_ 清除全部列()
        {
            this._datagridview.Columns.Clear();
            return this;
        }

        public DataGridview_ 清除全部行()
        {
            this._datagridview.Rows.Clear();
            return this;
        }

        public DataGridview_ 清空全部()
        {
            this._datagridview.DataSource = null;
            return this;
        }

        public DataGridview_ 清除底下空白行()
        {
            this._datagridview.AllowUserToAddRows = false;
            return this;
        }

        public DataGridview_ 清除左边空白列()
        {
            this._datagridview.RowHeadersVisible = false;
            return this;
        }

        public DataGridview_ 列为只读(dynamic 列号)
        {
            this._datagridview.Columns[列号].ReadOnly = true;
            return this;
        }
        public DataGridview_ 列为只读()
        {
            foreach (DataGridViewColumn column in this._datagridview.Columns)
            {
                column.ReadOnly = true;
            }
            return this;
        }

        public DataGridview_ 行为只读(int 行号)
        {
            this._datagridview.Rows[行号].ReadOnly = true;
            return this;
        }

        public DataGridview_ 取总行数(out int count)
        {
            count = this._datagridview.RowCount;
            return this;
        }

        public DataGridview_ 取总列数(out int count)
        {
            count = this._datagridview.ColumnCount;
            return this;
        }

        public DataGridview_ 设置行数(int 行数)
        {
            this._datagridview.RowCount = 行数;
            return this;
        }

        public DataGridview_ 获取行高(int 行号, out int height)
        {
            height = this._datagridview.Rows[行号].Height;
            return this;
        }

        public DataGridview_ 设置行高(int 行号, int height)
        {
            this._datagridview.Rows[行号].Height = height;
            return this;
        }

        //设置所有新行的默认高度
        public DataGridview_ 设置行高(int height)
        {
            this._datagridview.RowTemplate.Height = height; // 所有新行的默认高度
            return this;
        }

        /// <summary>
        /// 遍历式
        /// </summary> 
        public DataGridview_ 设置所有行高(int height)
        {
            foreach (DataGridViewRow s in this._datagridview.Rows)
            {
                s.Height = height;
            }
            return this;
        }
        public DataGridview_ 设置自动适应内容高度(int height)
        {
            this._datagridview.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            return this;
        }




        public DataGridview_ 设置列数(int 列数)
        {
            this._datagridview.ColumnCount = 列数;
            return this;
        }

        public DataGridview_ 获取列宽(dynamic 列号, out int value)
        {
            value = this._datagridview.Columns[列号].Width;

            return this;
        }




        public DataGridview_ 设置列宽(dynamic 列号, int width)
        {
            this._datagridview.Columns[列号].Width = width;
            return this;
        }





        public DataGridview_ 置数据(int 行号, dynamic 列号, string value)
        {
            this._datagridview[列号, 行号].Value = value;
            // datagridview.Rows[行号].Cells[列号].Value = value;
            return this;
        }

        public DataGridview_ 获取数据(int 行号, dynamic 列号, out string value)
        {
            value = this._datagridview[列号, 行号].Value.ToString();
            // return datagridview.Rows[行号].Cells[列号].Value;

            return this;
        }
        public DataGridview_ 获取数据_dynamic(int 行号, dynamic 列号, dynamic value)
        {
            value = this._datagridview[列号, 行号].Value;
            // return datagridview.Rows[行号].Cells[列号].Value;
            return this;
        }
        public DataGridview_ 添加行()
        {
            this._datagridview.RowCount += 1;
            return this;
        }

        public DataGridview_ 添加列()
        {
            this._datagridview.ColumnCount += 1;
            return this;
        }

        public DataGridview_ 设置字体_整体(Font fonts)
        {
            this._datagridview.Font = fonts;
            return this;
        }



        public DataGridview_ 设置字体颜色_整体(Color colors)
        {
            this._datagridview.ForeColor = colors;
            return this;
        }


        public DataGridview_ 设置文本颜色_指定单元格(int 行号, dynamic 列号, Color color_)
        {
            this._datagridview.Rows[行号].Cells[列号].Style.ForeColor = color_;
            return this;
        }

        public DataGridview_ 设置背景颜色_指定单元格(int 行号, dynamic 列号, Color color_)
        {
            this._datagridview.Rows[行号].Cells[列号].Style.BackColor = color_;
            return this;
        }

        public DataGridview_ 设置文本颜色_指定行(int 行号, Color color_)
        {
            this._datagridview.Rows[行号].DefaultCellStyle.ForeColor = color_;
            return this;
        }
        public DataGridview_ 设置背景颜色_指定行(int 行号, Color color_)
        {
            this._datagridview.Rows[行号].DefaultCellStyle.BackColor = color_;
            return this;
        }
        public DataGridview_ 设置文本颜色_指定列(dynamic 列号, Color color_)
        {
            this._datagridview.Columns[列号].DefaultCellStyle.ForeColor = color_;
            return this;
        }

        public DataGridview_ 获取文本颜色_指定行(int 行号, out Color colors)
        {
            colors = this._datagridview.Rows[行号].DefaultCellStyle.ForeColor;
            return this;
        }


        public DataGridview_ 设置背景颜色_指定列(dynamic 列号, Color color_)
        {
            this._datagridview.Columns[列号].DefaultCellStyle.BackColor = color_;
            return this;
        }

        public DataGridview_ 获取背景颜色_指定行(int 行号, out Color colors)
        {
            colors = this._datagridview.Rows[行号].DefaultCellStyle.BackColor;
            return this;
        }

        /// <summary>
        /// 重载,不设置行号时为选中区域的颜色
        /// </summary>
        /// <param name="datagridview"></param>
        /// <param name="行号"></param>
        /// <param name="color_"></param>
        public DataGridview_ 设置背景颜色_选中区(int 行号, Color color_)
        {
            this._datagridview.Rows[行号].DefaultCellStyle.SelectionBackColor = color_;
            return this;
        }

        /// <summary>
        /// 重载,不设置行号时为选中区域的颜色
        /// </summary>
        /// <param name="datagridview"></param>
        /// <param name="color_"></param>
        public DataGridview_ 设置背景颜色_选中区(Color color_)
        {
            this._datagridview.DefaultCellStyle.SelectionBackColor = color_;
            return this;
        }

        /// <summary>
        /// 重载:不设置行号时为选中区的文本颜色
        /// </summary>
        /// <param name="datagridview"></param>
        /// <param name="行号"></param>
        /// <param name="color_"></param>
        public DataGridview_ 设置文本颜色_选中区(int 行号, Color color_)
        {
            this._datagridview.Rows[行号].DefaultCellStyle.SelectionForeColor = color_;
            return this;
        }

        /// <summary>
        /// 重载:不设置行号时为选中区的文本颜色
        /// </summary>
        /// <param name="datagridview"></param>
        /// <param name="color_"></param>
        public DataGridview_ 设置文本颜色_选中区(Color color_)
        {
            this._datagridview.DefaultCellStyle.SelectionForeColor = color_;
            return this;
        }






        public DataGridview_ 获取当前选中的行号(out int index)
        {
            index = -1;
            try
            {
                index = this._datagridview.CurrentRow.Index;
            }
            catch (Exception)
            {

                index = -1;
            }
            return this;
        }

        public DataGridview_ 获取当前选中的列号(out int value)
        {
            value = this._datagridview.CurrentCell.ColumnIndex;

            return this;
        }






        public DataGridview_ 查询数据_返回行号(dynamic 列号, string 查询数据, out int[] count)
        {
            int 总行数 = this._datagridview.RowCount;
            List<int> lst行号 = new List<int>();
            for (int i = 0; i < 总行数; i++)
            {
                string value = this._datagridview[列号, i].Value.ToString();
                if (value == 查询数据)
                {
                    lst行号.Add(i);
                }
            }

            count = lst行号.ToArray();
            return this;
        }

        public DataGridview_ 设置列标题(dynamic 列号, string 列标题)
        {
            this._datagridview.Columns[列号].HeaderText = 列标题;
            return this;
        }

        public DataGridview_ 获取列标题(dynamic 列号, out string value)
        {
            value = this._datagridview.Columns[列号].HeaderText;
            return this;
        }


        public DataGridview_ 列排序_禁用(dynamic 列号)
        {
            this._datagridview.Columns[列号].SortMode = DataGridViewColumnSortMode.NotSortable;//禁用列排序
            return this;
        }

        public DataGridview_ 列排序_自动(dynamic 列号)
        {
            this._datagridview.Columns[列号].SortMode = DataGridViewColumnSortMode.Automatic;
            return this;
        }
        public DataGridview_ 列排序_编程方式(dynamic 列号)
        {
            this._datagridview.Columns[列号].SortMode = DataGridViewColumnSortMode.Programmatic;
            return this;
        }

        /// <summary>
        /// 可以使用键盘上的上下键
        /// <para>使用时,必须将最下面的一行空白行删除</para>
        /// </summary>
        /// <param name="datagridview"></param>
        public DataGridview_ 上移行(int 移动行数)
        {
            try
            {
                获取当前选中的行号(out int index);


                // 选择的行号   
                int 当前行号 = this._datagridview.CurrentRow.Index;
                if (当前行号 >= 1)
                {
                    // 拷贝选中的行   
                    DataGridViewRow newRow = this._datagridview.Rows[当前行号];
                    // 删除选中的行   
                    this._datagridview.Rows.Remove(this._datagridview.Rows[当前行号]);

                    // 将拷贝的行，插入到选中的上一行位置   
                    this._datagridview.Rows.Insert(当前行号 - 移动行数, newRow);
                    this._datagridview.ClearSelection();
                    // 选中最初选中的行 
                    // 选中指定行(datagridview, 0, 当前行号);
                    选中指定行(0, index);
                    将光标移到指定行(0, index);
                }


            }
            catch (Exception)
            {


            }
            return this;
        }

        /// <summary>
        /// 可以使用键盘上的上下键
        /// <para>使用时,必须将最下面的一行空白行删除</para>
        /// </summary>
        /// <param name="datagridview"></param>
        public DataGridview_ 下移行(int 移动行数)
        {
            try
            {
                获取当前选中的行号(out int index);

                // 选择的行号   
                int 当前行号 = this._datagridview.CurrentRow.Index;
                if (当前行号 < this._datagridview.Rows.Count - 1)
                {

                    // 拷贝选中的行   
                    DataGridViewRow newRow = this._datagridview.Rows[当前行号];
                    // 删除选中的行   
                    this._datagridview.Rows.Remove(this._datagridview.Rows[当前行号]);
                    // 将拷贝的行，插入到选中的下一行位置   
                    this._datagridview.Rows.Insert(当前行号 + 移动行数, newRow);
                    this._datagridview.ClearSelection();
                    // 选中最初选中的行   
                    // 选中指定行(datagridview, 0, 当前行号);

                    选中指定行(0, index);
                    将光标移到指定行(0, index);
                }

            }
            catch (Exception)
            {


            }
            return this;
        }



        /// <summary>
        /// 可以使用键盘上的上下键
        /// <para>使用时,必须将最下面的一行空白行删除</para>
        /// </summary>
        /// <param name="datagridview"></param>
        public DataGridview_ 上移指定行(int 移动行数, int 当前行号)
        {
            try
            {
                获取当前选中的行号(out int index);
                // 选择的行号   
                // int 当前行号 = datagridview.CurrentRow.Index;
                if (当前行号 >= 1)
                {
                    // 拷贝选中的行   
                    DataGridViewRow newRow = this._datagridview.Rows[当前行号];
                    // 删除选中的行   
                    this._datagridview.Rows.Remove(this._datagridview.Rows[当前行号]);

                    // 将拷贝的行，插入到选中的上一行位置   
                    this._datagridview.Rows.Insert(当前行号 - 移动行数, newRow);
                    this._datagridview.ClearSelection();
                    // 选中最初选中的行 
                    选中指定行(0, index);
                    将光标移到指定行(0, index);
                }


            }
            catch (Exception)
            {


            }
            return this;
        }


        /// <summary>
        /// 在插入行后调用此方法
        /// </summary>
        public DataGridview_ 在指定位置插入数据()
        {
            int count = this._datagridview.Rows.Count;

            if (count == 0)
            {
                return this; ;
            }
            获取当前选中的行号(out int index);

            if (index < 0)
            {
                return this; ;
            }

            int 上移行数 = count - 2 - index;
            上移指定行(上移行数, count);
            index = index + 1;

            选中指定行(1, index);
            将光标移到指定行(1, index);
            return this;
        }


        /// <summary>
        /// 可以使用键盘上的上下键
        /// <para>使用时,必须将最下面的一行空白行删除</para>
        /// </summary>
        /// <param name="datagridview"></param>
        public DataGridview_ 下移指定行(int 移动行数, int 当前行号)
        {
            try
            {
                获取当前选中的行号(out int index);
                // 选择的行号   
                // int 当前行号 = datagridview.CurrentRow.Index;
                if (当前行号 < this._datagridview.Rows.Count - 1)
                {

                    // 拷贝选中的行   
                    DataGridViewRow newRow = this._datagridview.Rows[当前行号];
                    // 删除选中的行   
                    this._datagridview.Rows.Remove(this._datagridview.Rows[当前行号]);
                    // 将拷贝的行，插入到选中的下一行位置   
                    this._datagridview.Rows.Insert(当前行号 + 移动行数, newRow);
                    this._datagridview.ClearSelection();
                    // 选中最初选中的行   


                    选中指定行(0, index);
                    将光标移到指定行(0, index);
                }

            }
            catch (Exception)
            {


            }
            return this;
        }




        /// <summary>
        ///  重载:不指定列号为所有列对齐
        ///  <para>对齐方式: DataGridViewContentAlignment.MiddleCenter;</para>
        /// </summary>
        /// <param name="datagridview"></param>
        /// <param name="列号"></param>
        /// <param name="对齐方式"></param>
        public DataGridview_ 列对齐(dynamic 列号, DataGridViewContentAlignment 对齐方式)
        {
            this._datagridview.Columns[列号].DefaultCellStyle.Alignment = 对齐方式;
            return this;
        }


        /// <summary>
        ///  重载:不指定列号为所有列对齐
        ///  <para>对齐方式: DataGridViewContentAlignment.MiddleCenter;</para>
        /// </summary>
        /// <param name="datagridview"></param>
        /// <param name="对齐方式"></param>
        public DataGridview_ 列对齐(DataGridViewContentAlignment 对齐方式)
        {
            this._datagridview.DefaultCellStyle.Alignment = 对齐方式;
            return this;
        }



        /// <summary>
        /// 对齐方式: DataGridViewContentAlignment.MiddleCenter;
        /// </summary>
        /// <param name="datagridview"></param>
        /// <param name="列号"></param>
        /// <param name="对齐方式"></param>
        public DataGridview_ 列标题对齐(dynamic 列号, DataGridViewContentAlignment 对齐方式)
        {

            this._datagridview.ColumnHeadersDefaultCellStyle.Alignment = 对齐方式;
            return this;
        }


        public DataGridview_ 列标题显示or隐藏(bool 是否显示)
        {
            this._datagridview.ColumnHeadersVisible = 是否显示;
            return this;
        }

        public DataGridview_ 列显示时间格式(dynamic 列号, string 日期格式)
        {
            this._datagridview.Columns[列号].DefaultCellStyle.Format = 日期格式;
            return this;
        }

        /// <summary>
        /// 列显示或隐藏
        /// </summary>
        public DataGridview_ 列Show(dynamic 列号, bool T显示F隐藏)
        {
            this._datagridview.Columns["id"].Visible = T显示F隐藏;
            return this;
        }

        /// <summary>
        /// 防止选中某一行时,列表题也会变成选中颜色
        /// </summary>
        /// <returns></returns>
        public DataGridview_ 列标题不选中()
        {
            this._datagridview.ColumnHeadersDefaultCellStyle.SelectionBackColor = this._datagridview.ColumnHeadersDefaultCellStyle.BackColor;
            return this;
        }

        /// <summary>
        /// 为默认样式
        /// </summary>
        /// <returns></returns>
        public DataGridview_ 列标题样式()
        {
            //this._datagridview.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            //this._datagridview.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

            this._datagridview.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            this._datagridview.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            this._datagridview.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

            return this;
        }


        public DataGridview_ 系统样式(bool 开关)
        {
            this._datagridview.EnableHeadersVisualStyles = 开关;
            return this;
        }

        public DataGridview_ 选中行_取消所有选中()
        {
            this._datagridview.ClearSelection();
            return this;
        }

        public DataGridview_ 选中行_取消所有行_保留焦点()
        {
            foreach (DataGridViewRow row in this._datagridview.SelectedRows)
            {
                row.Selected = false;
            }
            return this;
        }

        public DataGridview_ 选中行_取消当前选中选中()
        {
            if (this._datagridview.CurrentRow != null)
            {
                this._datagridview.CurrentRow.Selected = false;
            }
            return this;
        }

        public DataGridview_ 选中行_永不选中()
        {
            this._datagridview.SelectionMode = DataGridViewSelectionMode.CellSelect;
            return this;
        }

        /// <summary>
        /// SelectionChanged事件
        /// </summary> 
        public DataGridview_ Event_选中行_视觉上永不选中_SelectionChanged()
        {
            this._datagridview.SelectionChanged += (s, e) =>
            {
                this._datagridview.ClearSelection();
            };

            return this;
        }

        public DataGridview_ 选中行_设置只选择一行()
        {
            this._datagridview.MultiSelect = false;
            this._datagridview.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            return this;
        }
        public DataGridview_ 选中行_取消默认第一行被选中()
        {
            this._datagridview.ClearSelection();
            return this;
        }

        public DataGridview_ DataGrid虚拟化(bool IsEnable = true)
        {
            this._datagridview.VirtualMode = IsEnable;//开启DataGrid虚拟化
            return this;
        }

        #endregion


        #region 封装

        public virtual DataGridview_ 格式化()
        {

            this.DataGrid虚拟化();
            this.清除左边空白列();
            this.清除底下空白行();
            this.使能修改列宽(false);
            this.使能修改行高(false);
            this.使能整行选中();
            列标题不选中();
            this.设置网格线颜色(Color.FromArgb(224, 224, 224));
            this.取总列数(out int countColumns);
            this.设置字体_整体(new Font("微软雅黑", 11f, FontStyle.Regular));
            this.设置字体颜色_整体(Color.Black);
            this.设置文本颜色_列标题(Color.Gray);          
            列标题样式();
            

            //for (int i = 0; i < countColumns; i++)
            //{
            //    this.设置文本颜色_列标题(i, Color.Black);
            //    this.列为只读(i);
            //    this.列排序_禁用(i);
            //    this.列标题对齐(i, DataGridViewContentAlignment.MiddleLeft);
            //}
            选中行_设置只选择一行();

            return this;

        }


        #endregion
    }
}