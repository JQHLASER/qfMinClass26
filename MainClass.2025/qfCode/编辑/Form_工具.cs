using Sunny.UI;
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
    public partial class Form_工具 : Sunny.UI.UIForm
    {
        /// <summary>
        /// Form_主窗体
        /// </summary>
        Form_主窗体 formMain;
        type_编辑._编辑类型_ _编辑类型 = type_编辑._编辑类型_.添加;


        public Form_工具(type_编辑._编辑类型_ 编辑类型)
        {
            InitializeComponent();
            this.panel_控件.BackColor = Color.Transparent;
            formMain = Form_主窗体.forms;
            this._编辑类型 = 编辑类型;

            工具箱_初始化();


            this.Load += (s, e) =>
            {
                this.Padding = new System.Windows.Forms.Padding(5, 40, 5, 5);
                 
                switch (this._编辑类型)
                {
                    case type_编辑._编辑类型_.添加:
                        this.uiradioButton_文本.Checked = true;

                        break;
                    case type_编辑._编辑类型_.修改:


                        break;
                }

            };




        }

        #region 工具箱

        UIRadioButton uiradioButton_文本;
        UIRadioButton uiradioButton_序列号;
        UIRadioButton uiradioButton_日期;
        UIRadioButton uiradioButton_时间;
        UIRadioButton uiradioButton_关联对象;
        UIRadioButton uiradioButton_班次;

        Control_文本 con_文本;
        Control_序列号 con_序列号;
        Control_日期 con_日期;
        Control_时间 con_时间;
        Control_关联对象 con_关联对象;

        _元素_.文本 _cfg_文本 = new _元素_.文本();
        _元素_.序列号 _cfg_序列号 = new _元素_.序列号();
        _元素_.日期 _cfg_日期 = new _元素_.日期();
        _元素_.时间 _cfg_时间 = new _元素_.时间();
        _元素_.关联对象 _cfg_关联对象 = new _元素_.关联对象();
        _元素_.班次 _cfg_班次 = new _元素_.班次();



        #endregion





        #region 本地方法

         UIRadioButton 生成工具(string 标题)
        {
            return new UIRadioButton
            {
                ImageSize = 18,
                Text = 标题,
                Dock = DockStyle.Top,
                Height = 30,
                Tag = 标题,
            };
        }
        void 显示工具(bool IsShow, UIRadioButton con)
        {
            if (IsShow)
            {
                this.uiTitlePanel_工具箱.Controls.Add(con);
            }
        }


        #endregion

        #region 工具箱

        void  初始化工具()
        {

            uiradioButton_文本 = 生成工具(Language_.Get语言("文本"));
            uiradioButton_序列号 = 生成工具(Language_.Get语言("序列号"));
            uiradioButton_日期 = 生成工具(Language_.Get语言("日期"));
            uiradioButton_时间 = 生成工具(Language_.Get语言("时间"));
            uiradioButton_关联对象 = 生成工具(Language_.Get语言("关联对象"));
            uiradioButton_班次 = 生成工具(Language_.Get语言("班次"));

            工具_被选中();
        }

        void 工具箱_初始化()
        {
            初始化工具();
            this.uiTitlePanel_工具箱.Controls.Clear();
            显示工具(true, this.uiradioButton_文本);
            显示工具(true, this.uiradioButton_序列号);
            显示工具(true, this.uiradioButton_日期);
            显示工具(true, this.uiradioButton_时间);
            显示工具(this.formMain._编辑._功能.工具箱.关联对象, this.uiradioButton_关联对象);
            显示工具(this.formMain._编辑._功能.工具箱.班次, this.uiradioButton_班次);




        }

        #endregion

        #region 事件...工具箱

        void 工具_被选中()
        {
            this.uiradioButton_文本.Click += (s, e) =>
            {
                this.panel_控件.Controls.Clear();
                this.panel_控件.Controls.Add(new Control_文本(this._编辑类型, _cfg_文本));
            };
            this.uiradioButton_序列号 .Click += (s, e) =>
            {
                this.panel_控件.Controls.Clear();
                this.panel_控件.Controls.Add(new Control_序列号(this._编辑类型, _cfg_序列号));
            };
            this.uiradioButton_日期 .Click += (s, e) =>
            {
                this.panel_控件.Controls.Clear();
                this.panel_控件.Controls.Add(new Control_日期(this._编辑类型, _cfg_日期 ));
            };
            this.uiradioButton_时间.Click += (s, e) =>
            {
                this.panel_控件.Controls.Clear();
                this.panel_控件.Controls.Add(new Control_时间(this._编辑类型, _cfg_时间));
            };
            this.uiradioButton_关联对象.Click += (s, e) =>
            {
                this.panel_控件.Controls.Clear();
                this.panel_控件.Controls.Add(new Control_关联对象(this._编辑类型, _cfg_关联对象));
            };
            this.uiradioButton_班次.Click += (s, e) =>
            {
                this.panel_控件.Controls.Clear(); 
            };



        }


        #endregion




    }
}
