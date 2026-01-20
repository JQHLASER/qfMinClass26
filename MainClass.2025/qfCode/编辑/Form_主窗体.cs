using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace qfCode
{
    public partial class Form_主窗体 : Sunny.UI.UIForm
    {
        internal 编辑_ _编辑;
        internal static Form_主窗体 forms;
        internal _文件_属性_ _文件信息 = new _文件_属性_();



        public Form_主窗体(编辑_ 编辑)
        {
            InitializeComponent();
            forms = this;
            this._编辑 = 编辑;
            this.WindowState = FormWindowState.Maximized;




        }



    }
}
