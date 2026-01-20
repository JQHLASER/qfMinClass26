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
    public partial class Form_对象 : Sunny.UI.UIForm
    {
        type_编辑._编辑类型_ _编辑类型 = type_编辑._编辑类型_.添加;
        public string _对象名称 = "";

        /// <summary>
        /// 对象名称 : 修改时传入要修改的对象
        /// </summary> 
        public Form_对象(type_编辑._编辑类型_ 编辑类型, string 对象名称)
        {
            InitializeComponent();
            this._编辑类型 = 编辑类型;

            switch (this._编辑类型)
            {
                case type_编辑._编辑类型_.添加:
                    this.Text = Language_.Get语言("添加");
                    this._对象名称 = ""; 
                    break;
                case type_编辑._编辑类型_.修改:
                    this.Text = Language_.Get语言("修改");
                    this._对象名称 = 对象名称;
                    break;
            }


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

                string txt = this.uiTextBox_对象.Text.Trim ();
                if (string.IsNullOrEmpty(txt))
                {
                    MessageBox.Show(Language_.Get语言("不能为空"), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (txt != this._对象名称 && Form_主窗体.forms.lst对象列表.Contains(txt))
                {
                    MessageBox.Show(Language_.Get语言("检测到已重复"), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                this._对象名称 = txt;
                this.DialogResult = DialogResult.OK;

                #endregion

            };

        }
    }
}
