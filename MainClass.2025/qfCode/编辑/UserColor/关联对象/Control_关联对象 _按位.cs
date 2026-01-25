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
    public partial class Control_关联对象_按位 : Sunny.UI.UIPanel 
    {
        type_编辑._编辑类型_ _type;
        _关联对象_._按位_ _cfg;
        public Control_关联对象_按位(type_编辑._编辑类型_ type, _关联对象_._按位_ cfg)
        {
            InitializeComponent();
            this._type = type;
            this._cfg = cfg.Clone();

            this.uiTextBox_起始位.IntValue = (int)this._cfg.开始位;
            this.uiTextBox_数量.IntValue = (int)this._cfg.数量;

        }

        #region 对外方法

        /// <summary>
        /// 赋值
        /// </summary>
        public void GetCfg()
        {
            this._cfg.开始位 = (uint)this.uiTextBox_起始位.IntValue;
            this._cfg.数量 = (uint)this.uiTextBox_数量.IntValue;
        }

        #endregion





    }
}
