using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;

namespace qfCode
{
    public partial class Form_对象 : Sunny.UI.UIForm
    {
        type_编辑._编辑类型_ _编辑类型 = type_编辑._编辑类型_.添加;
        public string _对象名称 = "";
        public _对象_属性 _cfg;



        /// <summary>
        /// 对象名称 : 修改时传入要修改的对象
        /// </summary> 
        public Form_对象(type_编辑._编辑类型_ 编辑类型, string 对象名称, _对象_属性 cfg)
        {
            InitializeComponent();
            this._编辑类型 = 编辑类型;
            this._cfg = cfg.Clone();

            this.panel_位数校验.Visible = Form_主窗体.forms._编辑._功能.对象属性.校验位数;
            this.uiCheckBox_关键字.Visible = Form_主窗体.forms._编辑._功能.对象属性.校验关键字;
            this.uiCheckBox_模板变量.Visible = Form_主窗体.forms._编辑._功能.对象属性.校验模板;
            this.uiCheckBox_读码.Visible = Form_主窗体.forms._编辑._功能.对象属性.读码;
            this.uiCheckBox_防重.Visible = Form_主窗体.forms._编辑._功能.对象属性.防重;

            if (!Form_主窗体.forms._编辑._功能.对象属性.校验位数
                && !Form_主窗体.forms._编辑._功能.对象属性.校验关键字
                && !Form_主窗体.forms._编辑._功能.对象属性.校验模板
                && !Form_主窗体.forms._编辑._功能.对象属性.读码
                && !Form_主窗体.forms._编辑._功能.对象属性.防重)
            {
                this.uiGroupBox1.Visible = false;
            }
             

            switch (this._编辑类型)
            {
                case type_编辑._编辑类型_.添加:
                    this.Text = Language_.Get语言("添加");
                    this._对象名称 = "";
                    break;
                case type_编辑._编辑类型_.修改:
                    this.Text = Language_.Get语言("修改");
                    this._对象名称 = 对象名称;
                    this.uiTextBox_对象.Text = this._对象名称;
                    break;
            }

            this.uiTextBox_位数.IntValue = (int)this._cfg.校验位数;
            this.uiCheckBox_关键字.Checked = this._cfg.校验关键字;
            this.uiCheckBox_模板变量.Checked = this._cfg.校验模板;
            this.uiCheckBox_读码.Checked = this._cfg.读码;
            this.uiCheckBox_防重.Checked = this._cfg.防重;


            this.Shown += (s, e) =>
            {
                this.uiTextBox_对象.Focus();
            };
            this.uiButton_关闭.Click += (s, e) =>
            {
                this.Close();
            };
            this.uiButton_确定.Click += (s, e) =>
            {

                #region 确定

                string txt = this.uiTextBox_对象.Text.Trim();
                if (string.IsNullOrEmpty(txt))
                {
                    MessageBox.Show(Language_.Get语言("不能为空"), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (txt != this._对象名称 && !Form_主窗体.forms.Err_对象名重复(txt))
                {
                    MessageBox.Show(Language_.Get语言("检测到已重复"), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                this._对象名称 = txt;
                this._cfg.校验位数 = (uint)this.uiTextBox_位数.IntValue;
                this._cfg.校验关键字 = this.uiCheckBox_关键字.Checked;
                this._cfg.校验模板 = this.uiCheckBox_模板变量.Checked;
                this._cfg.读码 = this.uiCheckBox_读码.Checked;
                this._cfg.防重 = this.uiCheckBox_防重.Checked;

                this.DialogResult = DialogResult.OK;

                #endregion

            };

        }
    }
}
