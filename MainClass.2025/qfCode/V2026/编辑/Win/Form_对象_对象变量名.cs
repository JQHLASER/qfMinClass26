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
    public partial class Form_对象_对象变量名 : Sunny.UI.UIForm
    {

        internal string _对象名称 = "";
        BindingList<string> _lstBinding_对象名;

        public Form_对象_对象变量名()
        {
            InitializeComponent();
            this._lstBinding_对象名 = new BindingList<string>(Form_主窗体.forms._编辑._变量对象名);
            this.ui_Combobox2_变量对象名称._ComboBox.DataSource = this._lstBinding_对象名;

            this.ui_Combobox2_变量对象名称._ComboBox.SelectedIndexChanged += (s, e) =>
            {
                int index = this.ui_Combobox2_变量对象名称._ComboBox.SelectedIndex;
                if (index >=0)
                {
                    this._对象名称 = this._lstBinding_对象名[index];
                    this.DialogResult = DialogResult.OK;
                }
               
            };

        }
    }
}
