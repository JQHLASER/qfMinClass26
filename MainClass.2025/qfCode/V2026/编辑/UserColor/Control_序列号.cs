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
    public partial class Control_序列号 : Sunny.UI.UIPanel
    {
        type_编辑._编辑类型_ _type;
        public _元素_.序列号 _cfg;
        public Control_序列号(type_编辑._编辑类型_ type, _元素_.序列号 cfg)
        {
            InitializeComponent();
            this._type = type;
            this._cfg = cfg.Clone();

            this.uiRadioButton_十六进制.Visible = Form_主窗体.forms._编辑._功能.序列号.类型_HEX;
            this.uiGroupBox_加工.Visible = Form_主窗体.forms._编辑._功能.序列号.加工;
            this.uiRadioButton_复位_班次.Visible = Form_主窗体.forms._编辑._功能.工具箱.班次;

            #region  序列号类型

            switch (this._cfg.types)
            {
                case _序列号_._em_类型_.十进制:
                    this.uiRadioButton_十进制.Checked = true; break;
                case _序列号_._em_类型_.十六进制hex:
                    this.uiRadioButton_十六进制.Checked = true; break;

            }

            #endregion

            #region 复位方式

            switch (this._cfg.resets)
            {
                case _序列号_._em_复位_.按最大:
                    this.uiRadioButton_复位_最大.Checked = true; break;
                case _序列号_._em_复位_.按年:
                    this.uiRadioButton_复位_年.Checked = true; break;
                case _序列号_._em_复位_.按月:
                    this.uiRadioButton_复位_月.Checked = true; break;
                case _序列号_._em_复位_.按日:
                    this.uiRadioButton_复位_日.Checked = true; break;
                case _序列号_._em_复位_.按周:
                    this.uiRadioButton_复位_周.Checked = true; break;
                case _序列号_._em_复位_.按班次:
                    this.uiRadioButton_复位_班次.Checked = true; break;
            }


            #endregion

            #region 加工

            this.uiTextBox_加工_每个数量.IntValue = this._cfg.加工.数量;
            this.uiTextBox_加工_计数.IntValue = this._cfg.加工.计数;

            #endregion

            #region 序列号

            this.uiTextBox_开始序号.Text = this._cfg.开始序号.Trim();
            this.uiTextBox_当前序号.Text = this._cfg.当前序号.Trim();
            this.uiTextBox_最大序号.Text = this._cfg.最大序号.Trim();
            this.uiTextBox_递增.IntValue = this._cfg.递增量;

            #endregion
        }

        #region 对外方法

        /// <summary>
        /// 赋值
        /// </summary>
        public void GetCfg()
        {
            #region  序列号类型

            this._cfg.types = this.uiRadioButton_十进制.Checked ? _序列号_._em_类型_.十进制 :
                    this.uiRadioButton_十六进制.Checked ? _序列号_._em_类型_.十六进制hex :
                    _序列号_._em_类型_.十进制;

            #endregion

            #region 复位方式

            this._cfg.resets = this.uiRadioButton_复位_最大.Checked ? _序列号_._em_复位_.按最大 :
                              this.uiRadioButton_复位_年.Checked ? _序列号_._em_复位_.按年 :
                              this.uiRadioButton_复位_月.Checked ? _序列号_._em_复位_.按月 :
                              this.uiRadioButton_复位_日.Checked ? _序列号_._em_复位_.按日 :
                              this.uiRadioButton_复位_周.Checked ? _序列号_._em_复位_.按周 :
                              this.uiRadioButton_复位_班次.Checked ? _序列号_._em_复位_.按班次 :
                              _序列号_._em_复位_.按最大;
            #endregion

            #region 加工

            this._cfg.加工.数量 = this.uiTextBox_加工_每个数量.IntValue;
            this._cfg.加工.计数 = this.uiTextBox_加工_计数.IntValue;

            #endregion

            #region 序列号

            this._cfg.开始序号 = this.uiTextBox_开始序号.Text.Trim();
            this._cfg.当前序号 = this.uiTextBox_当前序号.Text.Trim();
            this._cfg.最大序号 = this.uiTextBox_最大序号.Text.Trim();
            this._cfg.递增量 = this.uiTextBox_递增.IntValue;

            #endregion
        }

        #endregion






    }
}
