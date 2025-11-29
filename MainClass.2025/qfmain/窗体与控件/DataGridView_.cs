 
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace qfmain
{
    public class DataGridView_
    {
        /// <summary>
        /// 控件
        /// </summary>
        internal DataGridView datagridview;
        public DataGridView_(DataGridView datagridview_)
        {
            datagridview = datagridview_;
        }


        public void 删除_行(int 行号)
        {
            datagridview.Rows.RemoveAt(行号);
        }
        public void 删除_列(int 列号)
        {
            datagridview.Columns.RemoveAt(列号);
        }


        public void 设置_只能选中一行()
        {
            datagridview.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            datagridview.MultiSelect = false;
        }

        public void 设置_选中时列标题背景颜色(dynamic 列号, System.Drawing.Color color_)
        {

            datagridview.EnableHeadersVisualStyles = false;//需要
            datagridview.Columns[列号].HeaderCell.Style.SelectionBackColor = color_;
        }
        public void 设置_列标题背景颜色(dynamic 列号, System.Drawing.Color color_)
        {

            datagridview.EnableHeadersVisualStyles = false;//需要
            datagridview.Columns[列号].HeaderCell.Style.BackColor = color_;
        }
        public void 设置_列标题文本颜色<T>(dynamic 列号, System.Drawing.Color color_)
        {
            datagridview.EnableHeadersVisualStyles = false;//需要
            datagridview.Columns[列号].HeaderCell.Style.ForeColor = color_;
        }
        public void 设置_列标题字体参数(dynamic 列号, string 字体, float 字高, FontStyle style)
        {
           // datagridview.EnableHeadersVisualStyles = false;//需要
            datagridview.Columns[列号].HeaderCell.Style.Font = new Font(字体, 字高, style);

        }
        public void 设置_列标题字体参数(dynamic 列号, Font fonts)
        {
          //  datagridview.EnableHeadersVisualStyles = false;//需要
            datagridview.Columns[列号].HeaderCell.Style.Font = fonts;

        }

        public void 设置_列字体参数(dynamic 列号, Font fonts)
        {
            datagridview.Columns[列号].DefaultCellStyle.Font = fonts;
        }



        public void 设置_列标题高度(int 高度)
        {
            datagridview.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            datagridview.ColumnHeadersHeight = 高度;
            datagridview.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        }

        /// <summary>
        /// 显示格式:yyyy/MM/dd HH:mm:ss
        /// </summary>
        /// <param name="datagridview"></param>
        /// <param name="列号"></param>
        /// <param name="显示格式">yyyy/MM/dd HH:mm:ss</param>
        public void 设置_时间显示格式(dynamic 列号, string 显示格式)
        {
            datagridview.Columns[列号].DefaultCellStyle.Format = 显示格式;
        }
        public void 设置_网格线颜色(System.Drawing.Color color_)
        {
            datagridview.GridColor = color_;
        }

        public void 设置_标题显示(bool T显示F隐藏)
        {
            datagridview.ColumnHeadersVisible = T显示F隐藏;
        }

        public void 设置_列显示(dynamic 列号, bool T显示F隐藏)
        {
            datagridview.Columns[列号].Visible = T显示F隐藏;
        }

        public void 设置_行显示(int 行号, bool T显示F隐藏)
        {
            datagridview.Rows[行号].Visible = T显示F隐藏; //隐藏行
        }

        public void 使能_修改列宽(bool T使能F不使能)
        {
            datagridview.AllowUserToResizeColumns = T使能F不使能;
        }

        public void 使能_修改行高(bool T使能F不使能)
        {
            datagridview.AllowUserToResizeRows = T使能F不使能;
        }

        public void 使能_同时选择多个行or列or单元格(bool T使能F不使能)
        {
            datagridview.MultiSelect = T使能F不使能;
        }

        /// <summary>
        /// 将光标也移到指定行
        /// </summary>
        /// <param name="datagridview"></param>
        /// <param name="列号"></param>
        /// <param name="行号"></param>
        public void 选中指定行(dynamic 列号, int 行号)
        {
            datagridview.Rows[行号].Selected = true;
            datagridview.CurrentCell = datagridview[列号, 行号]; //将光标定位到指定的行
        }

        /// <summary>
        /// 清除行被选中的背景
        /// </summary>
        /// <param name="datagridview"></param>
        /// <param name="行号"></param>
        public void 清除行选中光标(int 行号)
        {
            datagridview.Rows[行号].Selected = false;

        }


        public void 光标移到指定行(dynamic 列号, int 行号)
        {
            datagridview.CurrentCell = datagridview[列号, 行号];//将光标定位到指定的行
        }

        /// <summary>
        /// 由于行多时被其它的挡住了,这个方法可以显示出来
        /// </summary>
        /// <param name="datagridview"></param>
        /// <param name="行号"></param>
        public void 显示到指定行_行多时始终从指定行开始显示(int 行号)
        {
            datagridview.FirstDisplayedScrollingRowIndex = 行号;
        }


        public void 使能_整列选中()
        {
            datagridview.SelectionMode = DataGridViewSelectionMode.FullColumnSelect;
        }

        public void 使能_整行选中()
        {
            datagridview.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        public void 清除_全部列()
        {
            datagridview.Columns.Clear();
        }

        public void 清除_全部行()
        {
            datagridview.Rows.Clear();
        }

        public void 清除_全部()
        {
            datagridview.DataSource = null;
        }

        public void 清除_底下空白行()
        {
            datagridview.AllowUserToAddRows = false;
        }

        public void 清除_左边空白列()
        {
            datagridview.RowHeadersVisible = false;
        }

        public void 设置_列为只读(dynamic 列号)
        {
            datagridview.Columns[列号].ReadOnly = true;
        }


        public void 设置_行为只读(int 行号)
        {
            datagridview.Rows[行号].ReadOnly = true;
        }

        public int 获取_总行数()
        {
            return datagridview.RowCount;
        }

        public int 获取_总列数()
        {

            return datagridview.ColumnCount;
        }

        public void 设置_行数(int 行数)
        {
            datagridview.RowCount = 行数;
        }

        public int 获取_行高(int 行号)
        {
            return datagridview.Rows[行号].Height;
        }

        public void 设置_行高(int 行号, int height)
        {
            datagridview.Rows[行号].Height = height;
        }

        public void 设置_列数(int 列数)
        {
            datagridview.ColumnCount = 列数;
        }

        public int 获取_列宽(dynamic 列号)
        {
            return datagridview.Columns[列号].Width;
        }

        public void 设置_列宽(dynamic 列号, int width)
        {
            datagridview.Columns[列号].Width = width;
        }

        public void 设置_数据(int 行号, dynamic 列号, string value)
        {
            datagridview[列号, 行号].Value = value;
            // datagridview.Rows[行号].Cells[列号].Value = value;
        }

        public string 获取_数据(int 行号, dynamic 列号)
        {
            return datagridview[列号, 行号].Value.ToString();
            // return datagridview.Rows[行号].Cells[列号].Value;
        }
        public dynamic 获取_数据_dynamic(int 行号, dynamic 列号)
        {
            return datagridview[列号, 行号].Value;
            // return datagridview.Rows[行号].Cells[列号].Value;
        }
        public void 添加行()
        {
            datagridview.RowCount += 1;
        }
        public void 添加列()
        {
            datagridview.ColumnCount += 1;
        }
        public void 设置_整体字体参数(string 字体, float 字高, FontStyle style)
        {
            datagridview.Font = new Font(字体, 字高, style);

        }
        public void 设置_整体字体参数(Font fonts)
        {
            datagridview.Font = fonts;

        }
        public void 设置_整体字体颜色(System.Drawing.Color colors)
        {
            datagridview.ForeColor = colors;

        }
        public void 设置_指定单元格的文本颜色(int 行号, dynamic 列号, System.Drawing.Color color_)
        {
            datagridview.Rows[行号].Cells[列号].Style.ForeColor = color_;



        }
        public void 设置_指定单元格的背景颜色(int 行号, dynamic 列号, System.Drawing.Color color_)
        {
            datagridview.Rows[行号].Cells[列号].Style.BackColor = color_;
        }

        public void 设置_指定行的文本颜色(int 行号, System.Drawing.Color color_)
        {
            datagridview.Rows[行号].DefaultCellStyle.ForeColor = color_;
        }
        public void 设置_指定行的背景颜色(int 行号, System.Drawing.Color color_)
        {
            datagridview.Rows[行号].DefaultCellStyle.BackColor = color_;
        }
        public void 设置_指定列的文本颜色(dynamic 列号, System.Drawing.Color color_)
        {
            datagridview.Columns[列号].DefaultCellStyle.ForeColor = color_;
        }

        public System.Drawing.Color 获取_指定行的文本颜色(int 行号)
        {
            return datagridview.Rows[行号].DefaultCellStyle.ForeColor;

        }


        public void 设置_指定列的背景颜色(dynamic 列号, System.Drawing.Color color_)
        {
            datagridview.Columns[列号].DefaultCellStyle.BackColor = color_;
        }

        public System.Drawing.Color 获取_指定行的背景颜色(int 行号)
        {
            return datagridview.Rows[行号].DefaultCellStyle.BackColor;

        }

        /// <summary>
        /// 重载,不设置行号时为选中区域的颜色
        /// </summary>
        /// <param name="datagridview"></param>
        /// <param name="行号"></param>
        /// <param name="color_"></param>
        public void 设置_选中区的背景颜色(int 行号, System.Drawing.Color color_)
        {
            datagridview.Rows[行号].DefaultCellStyle.SelectionBackColor = color_;
        }

        /// <summary>
        /// 重载,不设置行号时为选中区域的颜色
        /// </summary>
        /// <param name="datagridview"></param>
        /// <param name="color_"></param>
        public void 设置_选中区的背景颜色(System.Drawing.Color color_)
        {
            datagridview.DefaultCellStyle.SelectionBackColor = color_;
        }

        /// <summary>
        /// 重载:不设置行号时为选中区的文本颜色
        /// </summary>
        /// <param name="datagridview"></param>
        /// <param name="行号"></param>
        /// <param name="color_"></param>
        public void 设置_选中区的文本颜色(int 行号, System.Drawing.Color color_)
        {
            datagridview.Rows[行号].DefaultCellStyle.SelectionForeColor = color_;
        }

        /// <summary>
        /// 重载:不设置行号时为选中区的文本颜色
        /// </summary>
        /// <param name="datagridview"></param>
        /// <param name="color_"></param>
        public void 设置_选中区的文本颜色(System.Drawing.Color color_)
        {
            datagridview.DefaultCellStyle.SelectionForeColor = color_;
        }

        public int 获取_当前选中的行号()
        {
            int index = -1;
            try
            {
                index = datagridview.CurrentRow.Index;
            }
            catch (Exception)
            {

                index = -1;
            }
            return index;
        }

        public int 获取_当前选中的列号()
        {
            return datagridview.CurrentCell.ColumnIndex;
        }

        /// <summary>
        /// 返回行索引
        /// </summary>
        /// <param name="datagridview"></param>
        /// <param name="列号"></param>
        /// <param name="查询数据"></param>
        /// <returns></returns>
        public int[] 查询_数据(dynamic 列号, string 查询数据)
        {
            int 总行数 = datagridview.RowCount;
            List<int> lst行号 = new List<int>();
            for (int i = 0; i < 总行数; i++)
            {
                string value = datagridview[列号, i].Value.ToString();
                if (value == 查询数据)
                {
                    lst行号.Add(i);
                }
            }

            return lst行号.ToArray();

        }

        public void 设置_列标题(dynamic 列号, string 列标题)
        {
            datagridview.Columns[列号].HeaderText = 列标题;
        }

        public string 获取_列标题(dynamic 列号)
        {
            return datagridview.Columns[列号].HeaderText;
        }


        public void 列排序_禁用(dynamic 列号)
        {
            datagridview.Columns[列号].SortMode = DataGridViewColumnSortMode.NotSortable;//禁用列排序
        }

        public void 列排序_自动(dynamic 列号)
        {
            datagridview.Columns[列号].SortMode = DataGridViewColumnSortMode.Automatic;
        }
        public void 列排序_编程方式(dynamic 列号)
        {
            datagridview.Columns[列号].SortMode = DataGridViewColumnSortMode.Programmatic;
        }

        /// <summary>
        /// 可以使用键盘上的上下键
        /// <para>使用时,必须将最下面的一行空白行删除</para>
        /// </summary>
        /// <param name="datagridview"></param>
        public bool 上移行(int 移动行数, out string msgErr)
        {
            bool rt = true;
            msgErr = string.Empty;
            try
            {
                int index = 获取_当前选中的行号();


                // 选择的行号   
                int 当前行号 = datagridview.CurrentRow.Index;
                if (当前行号 >= 1)
                {
                    // 拷贝选中的行   
                    DataGridViewRow newRow = datagridview.Rows[当前行号];
                    // 删除选中的行   
                    datagridview.Rows.Remove(datagridview.Rows[当前行号]);

                    // 将拷贝的行，插入到选中的上一行位置   
                    datagridview.Rows.Insert(当前行号 - 移动行数, newRow);
                    datagridview.ClearSelection();
                    // 选中最初选中的行 
                    // 选中指定行( 0, 当前行号);
                    选中指定行(0, index);
                    光标移到指定行(0, index);
                }


            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;

            }
            return rt;
        }

        /// <summary>
        /// 可以使用键盘上的上下键
        /// <para>使用时,必须将最下面的一行空白行删除</para>
        /// </summary>
        /// <param name="datagridview"></param>
        public bool 下移行(int 移动行数, out string msgErr)
        {
            bool rt = true;
            msgErr = string.Empty;
            try
            {
                int index = 获取_当前选中的行号();

                // 选择的行号   
                int 当前行号 = datagridview.CurrentRow.Index;
                if (当前行号 < datagridview.Rows.Count - 1)
                {

                    // 拷贝选中的行   
                    DataGridViewRow newRow = datagridview.Rows[当前行号];
                    // 删除选中的行   
                    datagridview.Rows.Remove(datagridview.Rows[当前行号]);
                    // 将拷贝的行，插入到选中的下一行位置   
                    datagridview.Rows.Insert(当前行号 + 移动行数, newRow);
                    datagridview.ClearSelection();
                    // 选中最初选中的行   
                    // 选中指定行( 0, 当前行号);

                    选中指定行(0, index);
                    光标移到指定行(0, index);
                }

            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;

            }

            return rt;
        }

        /// <summary>
        /// 可以使用键盘上的上下键
        /// <para>使用时,必须将最下面的一行空白行删除</para>
        /// </summary>
        /// <param name="datagridview"></param>
        public bool 上移指定行(int 移动行数, int 当前行号, out string msgErr)
        {
            bool rt = true;
            msgErr = string.Empty;
            try
            {
                int index = 获取_当前选中的行号();
                // 选择的行号   
                // int 当前行号 = datagridview.CurrentRow.Index;
                if (当前行号 >= 1)
                {
                    // 拷贝选中的行   
                    DataGridViewRow newRow = datagridview.Rows[当前行号];
                    // 删除选中的行   
                    datagridview.Rows.Remove(datagridview.Rows[当前行号]);

                    // 将拷贝的行，插入到选中的上一行位置   
                    datagridview.Rows.Insert(当前行号 - 移动行数, newRow);
                    datagridview.ClearSelection();
                    // 选中最初选中的行 
                    选中指定行(0, index);
                    光标移到指定行(0, index);
                }


            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }
            return rt;
        }


        /// <summary>
        /// 在插入行后调用此方法
        /// </summary>
        public void 在指定位置插入数据()
        {
            int count = datagridview.Rows.Count;

            if (count == 0)
            {
                return;
            }
            int index = 获取_当前选中的行号();

            if (index < 0)
            {
                return;
            }

            int 上移行数 = count - 2 - index;
            上移指定行(上移行数, count, out string msgErr);
            index = index + 1;

            选中指定行(1, index);
            光标移到指定行(1, index);
        }


        /// <summary>
        /// 可以使用键盘上的上下键
        /// <para>使用时,必须将最下面的一行空白行删除</para>
        /// </summary>
        /// <param name="datagridview"></param>
        public bool 下移指定行(int 移动行数, int 当前行号, out string msgErr)
        {
            bool rt = true;
            msgErr = string.Empty;
            try
            {
                int index = 获取_当前选中的行号();
                // 选择的行号   
                // int 当前行号 = datagridview.CurrentRow.Index;
                if (当前行号 < datagridview.Rows.Count - 1)
                {

                    // 拷贝选中的行   
                    DataGridViewRow newRow = datagridview.Rows[当前行号];
                    // 删除选中的行   
                    datagridview.Rows.Remove(datagridview.Rows[当前行号]);
                    // 将拷贝的行，插入到选中的下一行位置   
                    datagridview.Rows.Insert(当前行号 + 移动行数, newRow);
                    datagridview.ClearSelection();
                    // 选中最初选中的行   


                    选中指定行(0, index);
                    光标移到指定行(0, index);
                }

            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;

            }

            return rt;
        }


        /// <summary>
        ///  重载:不指定列号为所有列对齐
        ///  <para>对齐方式: DataGridViewContentAlignment.MiddleCenter;</para>
        /// </summary>
        /// <param name="datagridview"></param>
        /// <param name="列号"></param>
        /// <param name="对齐方式"></param>
        public void 设置_列对齐(dynamic 列号, DataGridViewContentAlignment 对齐方式)
        {
            datagridview.Columns[列号].DefaultCellStyle.Alignment = 对齐方式;

        }


        /// <summary>
        ///  重载:不指定列号为所有列对齐
        ///  <para>对齐方式: DataGridViewContentAlignment.MiddleCenter;</para>
        /// </summary>
        /// <param name="datagridview"></param>
        /// <param name="对齐方式"></param>
        public void 设置_列对齐(DataGridViewContentAlignment 对齐方式)
        {
            datagridview.DefaultCellStyle.Alignment = 对齐方式;

        }



        /// <summary>
        /// 对齐方式: DataGridViewContentAlignment.MiddleCenter;
        /// </summary>
        /// <param name="datagridview"></param>
        /// <param name="列号"></param>
        /// <param name="对齐方式"></param>
        public void 设置_列标题对齐(dynamic 列号, DataGridViewContentAlignment 对齐方式)
        {

            datagridview.ColumnHeadersDefaultCellStyle.Alignment = 对齐方式;

        }


        public void 设置_列标题显示(bool 是否显示)
        {

            datagridview.ColumnHeadersVisible = 是否显示;

        }

        public void 设置_列显示时间格式(dynamic 列号, string 日期格式)
        {
            datagridview.Columns[列号].DefaultCellStyle.Format = 日期格式;
        }

        public void 禁止选中焦点( )
        {
            datagridview.SelectionMode = DataGridViewSelectionMode.FullRowSelect;//设置选中方式
            datagridview.ClearSelection();//删除选中
        }

        public void 暂停界面刷新()
        {
            datagridview.SuspendLayout(); // 暂停界面刷新     
        }
        public void 恢复界面刷新()
        {
            datagridview.ResumeLayout(); // 恢复界面刷新 
        }



    }
}
