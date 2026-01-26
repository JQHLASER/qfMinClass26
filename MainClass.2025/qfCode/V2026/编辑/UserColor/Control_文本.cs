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
    public partial class Control_文本 : Sunny.UI.UIPanel
    {
        type_编辑._编辑类型_ _type;
        public _元素_.文本 _cfg;
        public Control_文本(type_编辑._编辑类型_ type, _元素_.文本 cfg)
        {
            InitializeComponent();
            this._type = type;
            this._cfg = cfg.Clone();
            
            this.uiRadioButton_文本.ValueChanged += (s, e) => On_选中();
            this.uiRadioButton_换行.ValueChanged += (s, e) => On_选中();
            this.uiRadioButton_空格.ValueChanged += (s, e) => On_选中();
             

            #region 文本

            switch (this._cfg.types)
            {
                case _文本_._em_文本_.换行:
                    this.uiRadioButton_换行.Checked = true;
                    break;
                case _文本_._em_文本_.空格:
                    this.uiRadioButton_空格.Checked = true;
                    break;
                default:
                    this.uiRadioButton_文本.Checked = true;
                    break;
            }

            #endregion

        }



        /// <summary>
        /// 赋值
        /// </summary>
        public void GetCfg()
        {
            this._cfg.内容 = this.uiTextBox1.Text; 
        }

      


        #region 本地方法
         

        void On_选中()
        {
            if (this.uiRadioButton_换行.Checked)
            {
                this._cfg.types = _文本_._em_文本_.换行;
                this.uiTextBox1.Clear();
                this.uiTextBox1.Enabled = false;
            }
            else if (this.uiRadioButton_空格.Checked)
            {
                this._cfg.types  = _文本_._em_文本_.空格;
                this.uiTextBox1.Clear();
                this.uiTextBox1.Enabled = false;
            }
            else
            {
                this._cfg.types  = _文本_._em_文本_.文本; 
                this.uiTextBox1.Enabled = true;
                this.uiTextBox1.Text = this._cfg.内容;
               
            }


        }

        #endregion


    }
}
