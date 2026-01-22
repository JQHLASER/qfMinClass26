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
using System.Windows.Forms.VisualStyles;

namespace qfCode
{
    public partial class Form_工具箱_元素 : Sunny.UI.UIForm
    {
        /// <summary>
        /// Form_主窗体
        /// </summary>
        Form_主窗体 formMain;
        type_编辑._编辑类型_ _编辑类型 = type_编辑._编辑类型_.添加;
        internal string _元素信息 = "";

         
        public Form_工具箱_元素(type_编辑._编辑类型_ 编辑类型, string 元素信息)
        {
            InitializeComponent();
            this.panel_控件.BackColor = Color.Transparent;
            this.Padding = new Padding(5, 40, 5, 5);

            formMain = Form_主窗体.forms;
            this._编辑类型 = 编辑类型;
            this._元素信息 = 元素信息;


            工具箱_初始化();


            this.Shown += (s, e) =>
            {

                switch (this._编辑类型)
                {
                    case type_编辑._编辑类型_.添加:
                        this.uiradioButton_文本.Checked = true;

                        break;
                    case type_编辑._编辑类型_.修改:


                        break;
                }

            };

            this.uiButton_No.Click += (s, e) =>
            {
                this.Close();
            };
            this.uiButton_Yes.Click += (s, e) =>
            {
                #region Yes



                #endregion 
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
                this.uiPanel_工具箱.Controls.Add(con);
            }
        }


        #endregion

        #region 工具箱

        void 初始化工具()
        {
            uiradioButton_文本 = 生成工具(Language_.Get语言("文本"));
            uiradioButton_序列号 = 生成工具(Language_.Get语言("序列号"));
            uiradioButton_日期 = 生成工具(Language_.Get语言("日期"));
            uiradioButton_时间 = 生成工具(Language_.Get语言("时间"));
            uiradioButton_关联对象 = 生成工具(Language_.Get语言("关联对象"));
            uiradioButton_班次 = 生成工具(Language_.Get语言("班次"));

        }

        void 工具箱_初始化()
        {
            初始化工具();
            this.uiPanel_工具箱.Controls.Clear();

            //显示序列号为倒序，这样文本工具才会显示在最上面

            显示工具(this.formMain._编辑._功能.工具箱.班次, this.uiradioButton_班次);
            显示工具(this.formMain._编辑._功能.工具箱.关联对象, this.uiradioButton_关联对象);
            显示工具(true, this.uiradioButton_时间);
            显示工具(true, this.uiradioButton_日期);
            显示工具(true, this.uiradioButton_序列号);
            显示工具(true, this.uiradioButton_文本);

            工具_事件_被选中();
        }

        #endregion

        #region 事件...工具箱

        void 工具_事件_被选中()
        {
            this.uiradioButton_文本.ValueChanged += (s, e) =>
            {
                if (e)
                {
                    if (this.con_文本 is null)
                    {
                        this.con_文本 = new Control_文本(this._编辑类型, _cfg_文本);
                    }

                    this.panel_控件.Controls.Clear();
                    this.panel_控件.Controls.Add(this.con_文本);
                }
            };
            this.uiradioButton_序列号.ValueChanged += (s, e) =>
            {
                if (e)
                {
                    if (this.con_序列号 is null)
                    {
                        this.con_序列号 = new Control_序列号(this._编辑类型, _cfg_序列号);
                    }
                    this.panel_控件.Controls.Clear();
                    this.panel_控件.Controls.Add(this.con_序列号);
                }
            };
            this.uiradioButton_日期.ValueChanged += (s, e) =>
            {
                if (e)
                {
                    if (this.con_日期 is null)
                    {
                        this.con_日期 = new Control_日期(this._编辑类型, _cfg_日期);
                    }
                    this.panel_控件.Controls.Clear();
                    this.panel_控件.Controls.Add(this.con_日期);
                }
            };
            this.uiradioButton_时间.ValueChanged += (s, e) =>
            {
                if (e)
                {
                    if (this.con_时间 is null)
                    { 
                        this.con_时间 = new Control_时间(this._编辑类型, _cfg_时间);
                    }
                    this.panel_控件.Controls.Clear();
                    this.panel_控件.Controls.Add(this.con_时间);
                }
            };
            this.uiradioButton_关联对象.ValueChanged += (s, e) =>
            {
                if (e)
                {
                    if (this.con_关联对象 is null)
                    {
                        this.con_关联对象 = new Control_关联对象(this._编辑类型, _cfg_关联对象);
                    }
                    this.panel_控件.Controls.Clear();
                    this.panel_控件.Controls.Add(this.con_关联对象);
                }
            };
            this.uiradioButton_班次.ValueChanged += (s, e) =>
            {
                if (e)
                {
                    this.panel_控件.Controls.Clear();
                }
            };



        }


        #endregion




    }
}
