using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;

namespace qfWPFmain
{
    /// <summary>
    /// 列标题样式,添加列在 ListView_GridView 类中
    /// </summary>
    public class ListView_
    {
        ListView _ListView;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_listView_"></param>
        /// <param name="ListViewItemStyle">可以绑定每行的文本颜色</param>
        public ListView_(ListView _listView_)
        {
            this._ListView = _listView_;
            _listView_.BorderBrush = this._BorderBrush;
            _listView_.BorderThickness = this._BorderThickness;
            _listView_.FontFamily = this._FontFamily;
            _listView_.FontSize = this._FontSize;
            _listView_.Foreground = this._Foreground;
            ScrollViewer.SetHorizontalScrollBarVisibility(_listView_, this._HorizontalScrollBarVisibility);
            ScrollViewer.SetVerticalScrollBarVisibility(_listView_, this._HorizontalScrollBarVisibility);
            ListVieW附加属性.Setui_光标跳转到最后一行(_listView_, this._Is光标移到最后一行);
            _listView_.SelectedIndex = this._SelectedIndex;
            _listView_.Background = this._Background;

        

        }

        /// <summary>
        /// 网格线宽度
        /// </summary>
        public Thickness _BorderThickness = new Thickness(0.5);

        /// <summary>
        /// 字体大小
        /// </summary>
        public double _FontSize = 14;

        /// <summary>
        /// 字体
        /// </summary>
        public FontFamily _FontFamily = new FontFamily("微软雅黑");

        public Brush _Foreground = Brushes.Black;

        public Brush _BorderBrush = Brushes.Silver;

        /// <summary>
        /// 水平滚动条
        /// </summary>
        public ScrollBarVisibility _HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;

        /// <summary>
        /// 垂直滚动条
        /// </summary>
        public ScrollBarVisibility _SetVerticalScrollBarVisibility = ScrollBarVisibility.Auto;

        /// <summary>
        /// 附加属性,光标移到最后一行
        /// </summary>
        public bool _Is光标移到最后一行 { set; get; } = false;

        /// <summary>
        /// 选中索引值
        /// </summary>
        public int _SelectedIndex = -1;

        public Brush _Background = Brushes.White;



    }


    /// <summary>
    /// listview属性,ListViewItem,可以绑定每行的文本颜色
    /// </summary>
    public class ListView_ListViewItemStyle
    {
        /// <summary>
        /// 对象属性
        /// </summary>
        public Style itemStyle = new Style(typeof(ListViewItem));

        public ListView_ListViewItemStyle(ListView _listView_)
        {
            // 创建数据触发器，根据条件改变颜色

            itemStyle.Setters.Add(new Setter(TextBlock.ForegroundProperty, this._Foreground));
            itemStyle.Setters.Add(new Setter(TextBlock.HeightProperty, this._Height));//行高




            _listView_.ItemContainerStyle = itemStyle;
        }

        public Brush _Foreground = Brushes.Black;
        /// <summary>
        /// 行高
        /// </summary>
        public double _Height = 30;

    }



    /// <summary>
    /// 列标题样式,添加列在此类中
    /// </summary>
    public class ListView_GridView
    {
        //列标题样式
        Style columnHeaderStyle = new Style(typeof(GridViewColumnHeader));
        /// <summary>
        /// listView的view
        /// </summary>
        public GridView gridview = new GridView();

        public ListView_GridView(ListView listview_)
        {
            columnHeaderStyle.Setters.Add(new Setter(GridViewColumnHeader.VisibilityProperty, this._Is列标题显示));
            columnHeaderStyle.Setters.Add(new Setter(GridViewColumnHeader.HorizontalContentAlignmentProperty, this._HorizontalContentAlignment));//水平对齐
            columnHeaderStyle.Setters.Add(new Setter(GridViewColumnHeader.HorizontalAlignmentProperty, HorizontalAlignment.Stretch));

            columnHeaderStyle.Setters.Add(new Setter(GridViewColumnHeader.VerticalContentAlignmentProperty, this._VerticalContentAlignment));//垂直对齐
            columnHeaderStyle.Setters.Add(new Setter(GridViewColumnHeader.VerticalAlignmentProperty, VerticalAlignment.Stretch));

            columnHeaderStyle.Setters.Add(new Setter(GridViewColumnHeader.ForegroundProperty, this._ForegroundProperty));
            columnHeaderStyle.Setters.Add(new Setter(GridViewColumnHeader.HeightProperty , this._Height));

            columnHeaderStyle.Setters.Add(new Setter(GridViewColumnHeader.IsEnabledProperty , this._IsEnable));


            
            gridview.ColumnHeaderContainerStyle = columnHeaderStyle;
            listview_.View = gridview;
        }
        /// <summary>
        /// 列标题禁止拖动
        /// </summary>
        public bool _IsEnable = false;
        public double _Height = 30;

        public Visibility _Is列标题显示 = Visibility.Visible;
        //水平对齐方式
        public HorizontalAlignment _HorizontalContentAlignment = HorizontalAlignment.Left;
        /// <summary>
        /// 垂直对齐
        /// </summary>
        public VerticalAlignment _VerticalContentAlignment = VerticalAlignment.Center;
        public Brush _ForegroundProperty = Brushes.Gray;



        /// <summary>
        /// 添加列
        /// </summary>     
        public ListView_GridView Add_Column(_ColumnInfo_ info)
        {
            GridViewColumn Column = new GridViewColumn()
            {
                Header = info.Header,
                Width = info.Width,
                DisplayMemberBinding = info.DisplayMemberBinding,
                
            };
            gridview.Columns.Add(Column);

            return this;
        }



        /// <summary>
        /// 列信息
        /// </summary>
        public class _ColumnInfo_
        {
            public string Header { set; get; } = string.Empty;
            public double Width { set; get; } = 100;

            /// <summary>
            /// new Binding("绑字字段")
            /// </summary>
            public BindingBase DisplayMemberBinding { set; get; }

            /// <summary>
            /// 列信息
            /// </summary>
            public _ColumnInfo_()
            {

            }

            /// <summary>
            /// 列信息
            /// <para>DisplayMemberBinding_= new Binding("绑字字段")</para>
            /// </summary>
            /// <param name="Header_"></param>
            /// <param name="Width"></param>
            /// <param name="DisplayMemberBinding_"> new Binding("绑字字段")</param>
            public _ColumnInfo_(string Header_, double Width, BindingBase DisplayMemberBinding_)
            {
                this.Header = Header_;
                this.Width = Width;
                this.DisplayMemberBinding = DisplayMemberBinding_;
            }
        }



    }



}
