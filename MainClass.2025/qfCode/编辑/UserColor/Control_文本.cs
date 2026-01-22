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
        _元素_.文本 _cfg;
        public Control_文本(type_编辑._编辑类型_ type, _元素_.文本 cfg)
        {
            InitializeComponent();
            this._type = type;
            this._cfg = new _元素_.文本().Clone();

           
            this.uiRadioButton_文本.ValueChanged  += (s, e) => On_选中();
            this.uiRadioButton_换行.ValueChanged += (s, e) => On_选中();
            this.uiRadioButton_空格.ValueChanged += (s, e) => On_选中();


            show();
        }

        #region 对外方法

        /// <summary>
        /// 赋值
        /// </summary>
        public void GetCfg()
        {
            this._cfg.内容 = this.uiTextBox1.Text;

        }

        #endregion


        #region 本地方法

        void show()
        {
            switch (this._type)
            {
                case type_编辑._编辑类型_.添加:
                    #region 添加 

                    this.uiTextBox1.Clear();
                    this.uiRadioButton_文本.Checked = true; 

                    #endregion
                    break;
                case type_编辑._编辑类型_.修改:

                    #region 修改

                    switch (this._cfg.类型)
                    {
                        case _文本_._em_文本_.换行:
                            this.uiRadioButton_换行.Checked = true;
                            break;
                        case _文本_._em_文本_.空格:
                            this.uiRadioButton_空格.Checked = true;
                            break;
                        default:
                            this.uiRadioButton_文本.Checked = true; ;
                            break;
                    }

                    #endregion

                    break;
            }

        }


        void On_选中()
        {
            if (this.uiRadioButton_换行.Checked)
            {
                this._cfg.类型 = _文本_._em_文本_.换行;
                this.uiTextBox1.Clear();
                this.uiTextBox1.Enabled = false;
            }
            else if (this.uiRadioButton_空格.Checked)
            {
                this._cfg.类型 = _文本_._em_文本_.空格;
                this.uiTextBox1.Clear();
                this.uiTextBox1.Enabled = false;
            }
            else
            {
                this._cfg.类型 = _文本_._em_文本_.文本;
                this.uiTextBox1.Text = this._cfg.内容;
                this.uiTextBox1.Enabled = true;
            }


        }

        #endregion


    }
}
