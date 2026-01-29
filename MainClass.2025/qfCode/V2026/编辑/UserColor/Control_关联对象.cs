using Sunny;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace qfCode
{
    public partial class Control_关联对象 : Sunny.UI.UIPanel
    {
        type_编辑._编辑类型_ _type;
        public _元素_.关联对象 _cfg;
        public Control_关联对象(type_编辑._编辑类型_ type, _元素_.关联对象 cfg)
        {
            InitializeComponent();
            this._type = type;
            this._cfg = cfg.Clone();

            this.uiRadioButton_全部.ValueChanged += (s, e) => On_选中类型();
            this.uiRadioButton_位.ValueChanged += (s, e) => On_选中类型();
            this.uiRadioButton_字符分割.ValueChanged += (s, e) => On_选中类型();
            this.uiRadioButton_首尾字符分割.ValueChanged += (s, e) => On_选中类型();

            this.uiComboBox_对象.SelectedIndexChanged += (s, e) =>
            {
                string 对象名 = this.uiComboBox_对象.SelectedText;
                DateTime dates = DateTime.Now;
                var rt = new 编辑交互_统一接口(Form_主窗体.forms._编辑)._Iworker.计算编码_对象(Form_主窗体.forms._配方信息, dates, 对象名);
                if (!rt.s)
                {
                    MessageBox.Show(rt.m, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                this.textBox_测试数据.Text = rt.v;

            };


            #region 可关联对象

            List<string> lstObject = new List<string>();
            var 当前对象 = Form_主窗体.forms._配方信息.对象[Form_主窗体.forms._编辑对象索引];
            foreach (var s in Form_主窗体.forms._配方信息.对象)
            {
                if (s.对象名 == 当前对象.对象名)
                    break;
                else
                {
                    lstObject.Add(s.对象名);
                }
            }
            this.uiComboBox_对象.DataSource = lstObject;

            #endregion

            #region 参数

            this.uiComboBox_对象.SelectedIndex = this.uiComboBox_对象.Items.IndexOf(this._cfg.对象);
            switch (this._cfg.types)
            {
                case _关联对象_._em_类型_.全部:
                    this.uiRadioButton_全部.Checked = true;

                    break;
                case _关联对象_._em_类型_.按位:
                    this.uiRadioButton_位.Checked = true;
                    var rt0 = new Json序列化().转成Json<_关联对象_._按位_>(this._cfg.param);
                    this._cfg_按位 = rt0.cfg;
                    break;
                case _关联对象_._em_类型_.按字符:
                    this.uiRadioButton_字符分割.Checked = true;
                    var rt1 = new Json序列化().转成Json<_关联对象_._按字符_>(this._cfg.param);
                    this._cfg_按字符 = rt1.cfg;
                    break;
                case _关联对象_._em_类型_.按首尾:
                    this.uiRadioButton_首尾字符分割.Checked = true;
                    var rt2 = new Json序列化().转成Json<_关联对象_._按首尾_>(this._cfg.param);
                    this._cfg_按首尾 = rt2.cfg;
                    break;

            }

            #endregion

        }


        /// <summary>
        /// 赋值
        /// </summary>
        public bool GetCfg()
        {


            #region 参数...param

            string jsonStr = "{}";
            if (this.uiRadioButton_位.Checked)
            {
                if (this.con_按位 != null)
                {
                    con_按位.GetCfg();
                }
                jsonStr = new Json序列化().转成String<_关联对象_._按位_>(this._cfg_按位);
            }
            else if (this.uiRadioButton_字符分割.Checked)
            {
                if (this.con_按字符 != null)
                {
                    con_按字符.GetCfg();
                }
                jsonStr = new Json序列化().转成String<_关联对象_._按字符_>(this._cfg_按字符);
            }
            else if (this.uiRadioButton_首尾字符分割.Checked)
            {
                if (this.con_按首尾字符 != null)
                {
                    con_按首尾字符.GetCfg();
                }
                jsonStr = new Json序列化().转成String<_关联对象_._按首尾_>(this._cfg_按首尾);
            }
            else if (this.uiRadioButton_全部.Checked)
            {
                jsonStr = "{}";
            }

            #endregion


            this._cfg.param = jsonStr;
            this._cfg.types = this.uiRadioButton_全部.Checked ? _关联对象_._em_类型_.全部 :
                              this.uiRadioButton_位.Checked ? _关联对象_._em_类型_.按位 :
                              this.uiRadioButton_字符分割.Checked ? _关联对象_._em_类型_.按字符 :
                              this.uiRadioButton_首尾字符分割.Checked ? _关联对象_._em_类型_.按首尾 :
                              _关联对象_._em_类型_.全部;
            this._cfg.对象 = this.uiComboBox_对象.SelectedText;



            if (this._cfg.types == _关联对象_._em_类型_.按字符 && string.IsNullOrEmpty(this._cfg_按字符.分割符))
            {
                MessageBox.Show(Language_.Get语言("请输入分割符"), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (this._cfg.types == _关联对象_._em_类型_.按首尾 && string.IsNullOrEmpty(this._cfg_按首尾.首))
            {

                MessageBox.Show(Language_.Get语言("请输入首分割符"), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (this._cfg.types == _关联对象_._em_类型_.按首尾 && string.IsNullOrEmpty(this._cfg_按首尾.尾))
            {

                MessageBox.Show(Language_.Get语言("请输入尾分割符"), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }


            return true;
        }



        #region 本地方法

        Control_关联对象_按位 con_按位;
        Control_关联对象_按字符 con_按字符;
        Control_关联对象_按首尾字符 con_按首尾字符;

        _关联对象_._按位_ _cfg_按位 = new _关联对象_._按位_();
        _关联对象_._按字符_ _cfg_按字符 = new _关联对象_._按字符_();
        _关联对象_._按首尾_ _cfg_按首尾 = new _关联对象_._按首尾_();

        void On_选中类型()
        {
            if (this.uiRadioButton_全部.Checked)
            {
                this.panel_参数.Controls.Clear();
            }
            else if (this.uiRadioButton_位.Checked)
            {
                this.panel_参数.Controls.Clear();
                con_按位 = null;
                con_按位 = new Control_关联对象_按位(this._type, this._cfg_按位);
                this.panel_参数.Controls.Add(con_按位);

            }
            else if (this.uiRadioButton_字符分割.Checked)
            {
                this.panel_参数.Controls.Clear();
                con_按字符 = null;
                con_按字符 = new Control_关联对象_按字符(this._type, this._cfg_按字符);
                this.panel_参数.Controls.Add(con_按字符);
            }
            else if (this.uiRadioButton_首尾字符分割.Checked)
            {
                this.panel_参数.Controls.Clear();
                con_按首尾字符 = null;
                con_按首尾字符 = new Control_关联对象_按首尾字符(this._type, this._cfg_按首尾);
                this.panel_参数.Controls.Add(con_按首尾字符);
            }


        }


        #endregion


    }
}
