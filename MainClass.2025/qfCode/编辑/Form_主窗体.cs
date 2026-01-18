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
        编辑_ _编辑;

        public Form_主窗体(编辑_ 编辑)
        {
            InitializeComponent();
            this._编辑 = 编辑;
            this.WindowState = FormWindowState.Maximized;

            
           

        }

       
         
    }
}
