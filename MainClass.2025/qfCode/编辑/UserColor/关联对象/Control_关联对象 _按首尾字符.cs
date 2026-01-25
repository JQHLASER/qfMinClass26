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
    public partial class Control_关联对象_按首尾字符 : Sunny.UI.UIPanel 
    {
        type_编辑._编辑类型_ _type;
        _关联对象_._按首尾_ _cfg;
        public Control_关联对象_按首尾字符(type_编辑._编辑类型_ type, _关联对象_._按首尾_ cfg)
        {
            InitializeComponent();
            this._type = type;
            this._cfg = cfg.Clone();


            this.uiTextBox_分割符首.Text = this._cfg.首;
            this.uiTextBox_分割符尾.Text = this._cfg.尾;
            this.uiTextBox_索引.IntValue = (int)this._cfg.索引;



        }



        /// <summary>
        /// 赋值
        /// </summary>
        public void GetCfg()
        {
            this._cfg.首 = this.uiTextBox_分割符首.Text;
            this._cfg.尾 = this.uiTextBox_分割符尾.Text;
            this._cfg.索引 = (uint)this.uiTextBox_索引.IntValue;
        }



    }
}
