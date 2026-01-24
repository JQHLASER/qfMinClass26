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
    public partial class Control_时间 : Sunny.UI.UIPanel
    {
        type_编辑._编辑类型_ _type;
        public _元素_.时间 _cfg;
        public Control_时间(type_编辑._编辑类型_ type, _元素_.时间 cfg)
        {
            InitializeComponent();
            this._type = type;
            this._cfg = cfg.Clone();

            this.panel_配置文件.Visible = Form_主窗体.forms._编辑._功能.日期时间.配置编码;
            this.uiComboBox_配置文件.DataSource = new 文件目录_本地(Form_主窗体.forms._编辑).Get配置文件_日期时间();

            #region 语言

            this.uiRadioButton_时24.Text = Language_.Get语言("时") + " H24";
            this.uiRadioButton_时12.Text = Language_.Get语言("时") + " H12";
            this.uiRadioButton_分.Text = Language_.Get语言("分");
            this.uiRadioButton_秒.Text = Language_.Get语言("秒");
            this.uiRadioButton_毫秒.Text = Language_.Get语言("毫秒");

            #endregion

            show();
        }



        #region 对外方法

        /// <summary>
        /// 赋值
        /// </summary>
        public void GetCfg()
        {
            this._cfg.types = this.uiRadioButton_时24.Checked ? _日期时间_._em_时间_.时24 :
                              this.uiRadioButton_时12.Checked ? _日期时间_._em_时间_.时12 :
                              this.uiRadioButton_分.Checked ? _日期时间_._em_时间_.分 :
                              this.uiRadioButton_秒.Checked ? _日期时间_._em_时间_.秒 :
                              this.uiRadioButton_毫秒.Checked ? _日期时间_._em_时间_.毫秒 :
                              _日期时间_._em_时间_.时24;

            if (Form_主窗体.forms._编辑._功能.日期时间.配置编码)
            {
                this._cfg.配置 = this.uiComboBox_配置文件.Text;
            }
        }

        #endregion


        #region 本地方法

        void show()
        {
            switch (this._type)
            {
                case type_编辑._编辑类型_.添加:
                    #region 添加 
                    this.uiRadioButton_时24.Checked = true;
                    this.uiComboBox_配置文件.SelectedIndex = this.uiComboBox_配置文件.Items.Count > 0 ? 0 : -1;
                    #endregion
                    break;
                case type_编辑._编辑类型_.修改:

                    #region 修改

                    switch (this._cfg.types)
                    {
                        case _日期时间_._em_时间_.时24:
                            this.uiRadioButton_时24.Checked = true; break;
                        case _日期时间_._em_时间_.时12:
                            this.uiRadioButton_时12.Checked = true; break;
                        case _日期时间_._em_时间_.分:
                            this.uiRadioButton_分.Checked = true; break;
                        case _日期时间_._em_时间_.秒:
                            this.uiRadioButton_秒.Checked = true; break;
                        case _日期时间_._em_时间_.毫秒:
                            this.uiRadioButton_毫秒.Checked = true; break;
                    }


                    int index = this.uiComboBox_配置文件.Items.IndexOf(this._cfg.配置);
                    this.uiComboBox_配置文件.SelectedIndex = index;


                    #endregion

                    break;
            }

        }




        #endregion


    }
}
