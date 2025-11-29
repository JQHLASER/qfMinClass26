using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Styling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfAvaloina
{
    public class DataGrid_
    {

        //    public DataGrid _DataGrid;
        //    public DataGrid_(DataGrid Udatagrid_)
        //    {
        //        this._DataGrid = Udatagrid_;
        //        //  Udatagrid_.ItemsSource = this._ItemsSource;//数据源
        //        Udatagrid_.AutoGenerateColumns = this._AutoGenerateColumns;//是否禁用自动生成列         
        //        Udatagrid_.CanUserSortColumns = this._CanUserSortColumns;//允许列排序
        //        Udatagrid_.GridLinesVisibility = this._GridLinesVisibility;//显示所有网格线
        //        Udatagrid_.VerticalGridLinesBrush = this._VerticalGridLinesBrush;//Y方向网格线颜色
        //        Udatagrid_.HorizontalGridLinesBrush = this._HorizontalGridLinesBrush;//X方向网格线颜色
        //        Udatagrid_.IsReadOnly = this._IsReadOnly;//是否只读
        //        Udatagrid_.FontFamily = this._FontFamily;//字体
        //        Udatagrid_.FontSize = this._FontSize;//字体大小
        //                                             // Udatagrid_.VerticalScrollBarVisibility = this._VerticalScrollBarVisibility;//< !--垂直滚动条自动显示-- >
        //                                             //  Udatagrid_.HorizontalScrollBarVisibility = this._HorizontalScrollBarVisibility;//< !--水平滚动条自动显示-- >
        //        Udatagrid_.Background = this._Background;


        //        Style dataGridStyle = new Style(typeof(DataGrid));
        //        dataGridStyle.Setters.Add(new Setter(ScrollViewer.VerticalScrollBarVisibilityProperty, this._VerticalScrollBarVisibility));//垂直滚动条自动显示
        //        dataGridStyle.Setters.Add(new Setter(ScrollViewer.HorizontalScrollBarVisibilityProperty, this._HorizontalScrollBarVisibility));//水平滚动条自动显示
        //        Udatagrid_.Style = dataGridStyle;



        //    }




        //    public Brush _Background = Brushes.White;



        //    /// <summary>
        //    /// 垂直滚动条自动显示
        //    /// </summary>
        //    public ScrollBarVisibility _VerticalScrollBarVisibility = ScrollBarVisibility.Auto;

        //    /// <summary>
        //    /// 水平滚动条自动显示
        //    /// </summary>
        //    public ScrollBarVisibility _HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;

        //    /// <summary>
        //    /// 网格线宽度
        //    /// </summary>
        //    public Thickness _BorderThickness = new Thickness(0.5);

        //    /// <summary>
        //    /// 字体大小
        //    /// </summary>
        //    public double _FontSize = 14;

        //    /// <summary>
        //    /// 字体
        //    /// </summary>
        //    public FontFamily _FontFamily = new FontFamily("微软雅黑");

        //    /// <summary>
        //    /// 数据源
        //    /// </summary>
        //    public System.Collections.IEnumerable _ItemsSource;
        //    /// <summary>
        //    /// 是否禁用自动生成列
        //    /// </summary>
        //    public bool _AutoGenerateColumns = false;

        //    /// <summary>
        //    /// 允许用户添加新行
        //    /// </summary>
        //    public bool _CanUserAddRows = false;

        //    /// <summary>
        //    /// 允许用户删除行
        //    /// </summary>
        //    public bool _CanUserDeleteRows = false;

        //    /// <summary>
        //    /// 允许列排序
        //    /// </summary>
        //    public bool _CanUserSortColumns = false;

        //    /// <summary>
        //    /// 显示所有网格线
        //    /// </summary>
        //    public DataGridGridLinesVisibility _GridLinesVisibility = DataGridGridLinesVisibility.All;

        //    /// <summary>
        //    /// Y方向网格线颜色
        //    /// </summary>
        //    public Brush _VerticalGridLinesBrush = Brushes.Silver;

        //    /// <summary>
        //    /// X方向网格线颜色
        //    /// </summary>
        //    public Brush _HorizontalGridLinesBrush = Brushes.Silver;

        //    /// <summary>
        //    /// 是否只读
        //    /// </summary>
        //    public bool _IsReadOnly = true;



        //    /// <summary>
        //    ///  设置列标题样式,指定列
        //    /// </summary>
        //    /// <param name="ColumnIndex">列索引</param>
        //    /// <param name="HeaderStyle_"></param>
        //    /// <returns></returns>
        //    public virtual dataGrid_ ColumnHeader(Style HeaderStyle, int ColumnIndex = -1)
        //    {
        //        if (ColumnIndex < 0)
        //        {
        //            // 应用到整个DataGrid
        //            this._DataGrid.ColumnHeaderStyle = HeaderStyle;//全部列
        //        }
        //        else
        //        {
        //            this._DataGrid.Columns[ColumnIndex].HeaderStyle = HeaderStyle;//指定列
        //        }
        //        return this;
        //    }

        //    /// <summary>
        //    /// 全部列
        //    /// </summary>
        //    /// <param name="isEnable"></param>
        //    /// <returns></returns>
        //    public virtual dataGrid_ Is调整列宽(bool isEnable)
        //    {
        //        foreach (var s in this._DataGrid.Columns)
        //        {
        //            s.CanUserResize = isEnable;
        //        }

        //        return this;
        //    }

        //    /// <summary>
        //    /// 指定列
        //    /// </summary>
        //    /// <param name="ColumnIndex">列索引</param>
        //    /// <param name="isEnable"></param>
        //    /// <returns></returns>
        //    public virtual DataGridColumn Is调整列宽(int ColumnIndex, bool isEnable)
        //    {
        //        var targetColumn = this._DataGrid.Columns[ColumnIndex];
        //        targetColumn.CanUserResize = isEnable;
        //        return targetColumn;
        //    }

        //    /// <summary>
        //    /// 指定列
        //    /// </summary>
        //    /// <param name="ColumnName">列名称</param>
        //    /// <param name="isEnable"></param>
        //    /// <returns></returns>
        //    public virtual DataGridColumn Is调整列宽(string ColumnName, bool isEnable)
        //    {
        //        var targetColumn = this._DataGrid.Columns.FirstOrDefault(c => c.Header.ToString() == ColumnName);
        //        if (targetColumn != null)
        //        {
        //            targetColumn.CanUserResize = false;
        //        }
        //        return targetColumn;
        //    }


        //    /// <summary>
        //    ///  添加列,Text
        //    /// </summary>
        //    /// <param name="header_"></param>
        //    /// <param name="bindingPath"></param>
        //    /// <param name="width_"></param>
        //    /// <param name="HeaderStyle_">列标题属性</param>
        //    /// <param name="CanuseResize_">使能调整列宽</param>
        //    public virtual dataGrid_ AddColumn_Text(string header_, string bindingPath, int width_, Style HeaderStyle_ = null, bool CanuseResize_ = true)
        //    {
        //        DataGridTextColumn column = new DataGridTextColumn()
        //        {
        //            Header = header_,
        //            Binding = new Binding(bindingPath),
        //            Width = width_,
        //            CanUserResize = CanuseResize_,
        //        };
        //        this._DataGrid.Columns.Add(column);

        //        if (HeaderStyle_ is not null)
        //        {
        //            column.HeaderStyle = HeaderStyle_;
        //        }

        //        return this;
        //    }

        //    /// <summary>
        //    ///  添加列,CheckBox
        //    /// </summary>
        //    /// <param name="header_"></param>
        //    /// <param name="bindingPath"></param>
        //    /// <param name="width_"></param>
        //    /// <param name="HeaderStyle_">列标题属性</param>
        //    ///  <param name="CanuseResize_">使能调整列宽</param>
        //    public dataGrid_ AddColumn_CheckBox(string header_, string bindingPath, int width_, Style HeaderStyle_ = null, bool CanuseResize_ = true)
        //    {
        //        DataGridCheckBoxColumn column = new DataGridCheckBoxColumn()
        //        {
        //            Header = header_,
        //            Binding = new System.Windows.Data.Binding(bindingPath),
        //            Width = width_,
        //            CanUserResize = CanuseResize_,
        //        };
        //        this._DataGrid.Columns.Add(column);

        //        if (HeaderStyle_ is not null)
        //        {
        //            column.HeaderStyle = HeaderStyle_;
        //        }

        //        return this;
        //    }

        //    /// <summary>
        //    /// 添加列,Combobox
        //    /// </summary>
        //    /// <typeparam name="T"></typeparam>
        //    /// <param name="header_"></param>
        //    /// <param name="SelectedItembindingPath">设置绑定，对应数据对象的属性</param>
        //    /// <param name="t"></param>
        //    /// <param name="width_"></param>
        //    /// <param name="HeaderStyle_">列标题属性</param>
        //    ///  <param name="CanuseResize_">使能调整列宽</param>
        //    public virtual dataGrid_ AddColumn_Combobox<T>(string header_, string SelectedItembindingPath, List<T> t, int width_, Style HeaderStyle_ = null, bool CanuseResize_ = true)
        //    {
        //        DataGridComboBoxColumn column = new DataGridComboBoxColumn()
        //        {
        //            Header = header_,
        //            SelectedItemBinding = new Binding(SelectedItembindingPath),
        //            Width = width_,
        //            ItemsSource = t,
        //            CanUserResize = CanuseResize_,
        //        };
        //        this._DataGrid.Columns.Add(column);

        //        if (HeaderStyle_ is not null)
        //        {
        //            column.HeaderStyle = HeaderStyle_;
        //        }
        //        return this;
        //    }




        //}

        ///// <summary>
        ///// DataGrid列标题样式
        ///// </summary>  
        //public class dataGrid_Style_
        //{
        //    public Style HeaderStyle = new Style(typeof(DataGridColumnHeader));

        //    /// <summary>
        //    ///  提供一个返回Style的方法
        //    /// </summary>
        //    /// <returns></returns>
        //    public Style GetHeaderStyle()
        //    {
        //        return HeaderStyle;
        //    }

        //    public dataGrid_Style_()
        //    {

        //        // 水平对刘方式
        //        HeaderStyle.Setters.Add(new Setter(
        //            DataGridColumnHeader.VerticalContentAlignmentProperty,
        //           _VerticalContentAlignment
        //        ));

        //        // 水平对刘方式
        //        HeaderStyle.Setters.Add(new Setter(
        //            DataGridColumnHeader.HorizontalContentAlignmentProperty,
        //           _HorizontalContentAlignmen
        //        ));

        //        // 设置文本颜色（前景色）
        //        HeaderStyle.Setters.Add(new Setter(
        //            DataGridColumnHeader.ForegroundProperty,
        //           this._Foreground // 这里可以替换为任意颜色
        //        ));

        //        // 设置字体
        //        HeaderStyle.Setters.Add(new Setter(
        //            DataGridColumnHeader.FontSizeProperty,
        //           this._FontSize
        //        ));

        //        // 设置背景颜色
        //        HeaderStyle.Setters.Add(new Setter(
        //            DataGridColumnHeader.BackgroundProperty,
        //           _Background  // 可以替换为任意颜色
        //        ));

        //        // 设置字体
        //        HeaderStyle.Setters.Add(new Setter(
        //             DataGridColumnHeader.FontFamilyProperty,
        //            this._FontFamily
        //         ));

        //        // 设置高度
        //        HeaderStyle.Setters.Add(new Setter(
        //            DataGridColumnHeader.HeightProperty,
        //           this._Height
        //        ));

        //        // 设置标题边框颜色（网格线颜色）
        //        HeaderStyle.Setters.Add(new Setter(
        //            DataGridColumnHeader.BorderBrushProperty,
        //          this._VerticalGridLinesBrush   // 网格线颜色
        //        ));

        //        // 设置边框厚度（控制网格线粗细）
        //        HeaderStyle.Setters.Add(new Setter(
        //            DataGridColumnHeader.BorderThicknessProperty,
        //          this._BorderThickness // 上下左右边框厚度
        //        ));


        //        HeaderStyle.Setters.Add(new Setter(
        //            DataGridColumnHeader.MarginProperty,
        //          this._Margin // 外边距
        //        ));
        //        HeaderStyle.Setters.Add(new Setter(
        //          DataGridColumnHeader.PaddingProperty,
        //        this._Padding // 外边距
        //      ));
        //    }
        //    /// <summary>
        //    /// 内边距
        //    /// </summary>
        //    public Thickness _Padding = new Thickness(5, 0, 5, 0);
        //    /// <summary>
        //    /// 外边距
        //    /// </summary>
        //    public Thickness _Margin = new Thickness(0);//
        //    /// <summary>
        //    /// 网格线颜色
        //    /// </summary>
        //    public Brush _VerticalGridLinesBrush = Brushes.Silver;

        //    /// <summary>
        //    /// 网格线宽度
        //    /// </summary>
        //    public Thickness _BorderThickness = new Thickness(0.5);

        //    /// <summary>
        //    /// 字体大小
        //    /// </summary>
        //    public double _FontSize = 14;

        //    /// <summary>
        //    /// 字体
        //    /// </summary>
        //    public FontFamily _FontFamily = new FontFamily("微软雅黑");

        //    /// <summary>
        //    /// 字体颜色
        //    /// </summary>
        //    public Brush _Foreground = Brushes.Gray;

        //    /// <summary>
        //    /// 水平对齐方式
        //    /// </summary>
        //    public HorizontalAlignment _HorizontalContentAlignmen = HorizontalAlignment.Left;
        //    /// <summary>
        //    /// 垂直对齐方式
        //    /// </summary>
        //    public VerticalAlignment _VerticalContentAlignment = VerticalAlignment.Center;

        //    /// <summary>
        //    /// 列背景色
        //    /// </summary>
        //    public Brush _Background = Brushes.WhiteSmoke;

        //    /// <summary>
        //    /// 高度
        //    /// </summary>
        //    public double _Height = 30;

        //}

    }
}
