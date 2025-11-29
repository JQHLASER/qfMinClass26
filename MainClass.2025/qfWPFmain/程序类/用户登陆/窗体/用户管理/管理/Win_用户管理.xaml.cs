using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace qfWPFmain
{
    /// <summary>
    /// Win_用户编辑.xaml 的交互逻辑
    /// </summary>
    public partial class Win_用户管理 : Window
    {
        viewModel_用户管理 _DataContext;
        public Win_用户管理(_loginInfo_[] loginInfo)
        {
            InitializeComponent();
            this._标题栏.Inistiall(this, false , true, false, Language_.Get语言("用户管理"));

            #region ListView



            new ListView_(this._listView)
            {


            };

            ListView_ListViewItemStyle  style = new ListView_ListViewItemStyle( this._listView )
            {

            };
             
            new ListView_GridView(this._listView)
            {

            }
             .Add_Column(new ListView_GridView._ColumnInfo_(Language_.Get语言("用户"), 200, new Binding("用户")))
             //.Add_Column(new ListView_GridView._ColumnInfo_(Language_.Get语言("密码"), 200, new Binding("密码")))
             .Add_Column(new ListView_GridView._ColumnInfo_(Language_.Get语言("权限"), 200, new Binding("权限")))
             ;
            #endregion

            this._DataContext = new viewModel_用户管理( this );
            this.DataContext = this._DataContext;
             
        }



    }
}
