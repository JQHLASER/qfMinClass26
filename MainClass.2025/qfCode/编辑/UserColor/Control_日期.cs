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
    public partial class Control_日期 : Sunny.UI.UIPanel
    {
        type_编辑._编辑类型_ _type;
        public _元素_.日期 _cfg;
        public Control_日期(type_编辑._编辑类型_ type, _元素_.日期 cfg)
        {
            InitializeComponent();
            this._type = type;
            this._cfg = cfg.Clone();

            this.uiComboBox_配置文件.DataSource = new 编辑交互_本地(Form_主窗体.forms._编辑).Get配置文件_日期时间();
            #region 偏移

            string[] py = Enum.GetNames(typeof(_日期时间_._em_偏移类型_));
            List<string> lstPy = new List<string>();
            foreach (var s in py)
            {
                lstPy.Add(Language_.Get语言(s));
            }
            this.uiComboBox_偏移_类型.DataSource = lstPy;

            #endregion

            this.uiGroupBox_偏移.Visible = Form_主窗体.forms._编辑._功能.日期时间.偏移计算;
            this.panel_配置文件.Visible = Form_主窗体.forms._编辑._功能.日期时间.配置编码;


            语言(); 

            #region 初始值


            this.uiRadioButton_年4位.Checked = true;
            this.uiComboBox_偏移_类型.SelectedIndex = 0;
            this.uiTextBox_偏移值.IntValue = 1;
            this.uiComboBox_配置文件.SelectedIndex = this.uiComboBox_配置文件.Items.Count > 0 ? 0 : -1;

            #endregion

            #region 偏移值

            if (Form_主窗体.forms._编辑._功能.日期时间.偏移计算)
            {
                this.uiComboBox_偏移_类型.SelectedIndex = (int)this._cfg.偏移类型;
                this.uiTextBox_偏移值.IntValue = this._cfg.偏移值;
            }

            #endregion

            #region 配置文件

            if (Form_主窗体.forms._编辑._功能.日期时间.配置编码)
            {
                int index = this.uiComboBox_配置文件.Items.IndexOf(this._cfg.配置);
                this.uiComboBox_配置文件.SelectedIndex = index;
            }

            #endregion


            #region 日期

            switch (this._cfg.types)
            {
                case _日期时间_._em_日期_.年4位:
                    this.uiRadioButton_年4位.Checked = true; break;
                case _日期时间_._em_日期_.年2位:
                    this.uiRadioButton_年2位.Checked = true; break;
                case _日期时间_._em_日期_.月:
                    this.uiRadioButton_月.Checked = true; break;
                case _日期时间_._em_日期_.日:
                    this.uiRadioButton_日.Checked = true; break;
                case _日期时间_._em_日期_.天:
                    this.uiRadioButton_天.Checked = true; break;
                case _日期时间_._em_日期_.周:
                    this.uiRadioButton_周.Checked = true; break;
                case _日期时间_._em_日期_.星期:
                    this.uiRadioButton_星期.Checked = true; break;
            }
             
            #endregion



        }


        void 语言()
        {
            DateTime now = DateTime.Now;
            this.uiRadioButton_年4位.Text = $"{Language_.Get语言("年")} {now.ToString("yyyy")}";
            this.uiRadioButton_年2位.Text = $"{Language_.Get语言("年")} {now.ToString("yy")}";
            this.uiRadioButton_月.Text = $"{Language_.Get语言("月")} {now.ToString("MM")}";
            this.uiRadioButton_日.Text = $"{Language_.Get语言("日")} {now.ToString("dd")}";
            this.uiRadioButton_周.Text = $"{Language_.Get语言("周")} {new qfmain.日期时间_().Get_weeks(now).ToString("00")}";
            this.uiRadioButton_星期.Text = $"{Language_.Get语言("星期")} {new qfmain.日期时间_().Get_星期(now)}";
            this.uiRadioButton_天.Text = $"{Language_.Get语言("天")} {new qfmain.日期时间_().Get_days(now).ToString("000")}";
        }
         

        /// <summary>
        /// 赋值
        /// </summary>
        public void GetCfg()
        {
            this._cfg.types =
                this.uiRadioButton_年4位.Checked ? _日期时间_._em_日期_.年4位 :
                this.uiRadioButton_年2位.Checked ? _日期时间_._em_日期_.年2位 :
                this.uiRadioButton_月.Checked ? _日期时间_._em_日期_.月 :
                this.uiRadioButton_日.Checked ? _日期时间_._em_日期_.日 :
                this.uiRadioButton_天.Checked ? _日期时间_._em_日期_.天 :
                this.uiRadioButton_周.Checked ? _日期时间_._em_日期_.周 :
                this.uiRadioButton_星期.Checked ? _日期时间_._em_日期_.星期 :
                _日期时间_._em_日期_.年4位;

            if (Form_主窗体.forms._编辑._功能.日期时间.偏移计算)
            {
                this._cfg.偏移类型 = (_日期时间_._em_偏移类型_)this.uiComboBox_偏移_类型.SelectedIndex;
                this._cfg.偏移值 = this.uiTextBox_偏移值.IntValue;
            }

            if (Form_主窗体.forms._编辑._功能.日期时间.配置编码)
            {
                this._cfg.配置 = this.uiComboBox_配置文件.Text;
            }

        }
 


 

    }
}
