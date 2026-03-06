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
    public partial class Form_配置 : Sunny.UI.UIForm
    {
        internal _配方文件_属性_ _cfg;
        BindingList<string> _lstBind_班次 = new BindingList<string>();
        public Form_配置(_配方文件_属性_ cfg)
        {
            InitializeComponent();
            this._cfg = cfg.Clone();

            _lstBind_班次 = new BindingList<string>(new 编辑交互_统一接口(Form_主窗体.forms._编辑)._Iworker.Get目录_配置文件_班次());
            this.ui_Combobox2_班次._ComboBox.DataSource = this._lstBind_班次;
            this.ui_Combobox2_班次._ComboBox.SelectedIndex = this.ui_Combobox2_班次._ComboBox.Items.IndexOf(this._cfg.班次文件);
            bool rt = DateTime.TryParse(this._cfg.更新时间, out DateTime times);
            this.uiTimePicker_日期更新时间.Value = rt ? times : DateTime.Parse("00:00:00");

            this.uiButton_No.Click += (s, e) =>
            {
                this.Close();
            };
            this.uiButton_Yes.Click += (s, e) =>
            {
                this._cfg.班次文件 = this._lstBind_班次[this.ui_Combobox2_班次._ComboBox.SelectedIndex];
                this._cfg.更新时间 = this.uiTimePicker_日期更新时间.Value.ToString("HH:mm:ss");
                this.DialogResult = DialogResult.OK;
            };
        }
    }
}
