using SqlSugar.Extensions;
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
        internal string _json元素信息 = "";


        public Form_工具箱_元素(type_编辑._编辑类型_ 编辑类型, string 元素信息)
        {
            InitializeComponent();
            this.panel_控件.BackColor = Color.Transparent;
            this.Padding = new Padding(5, 40, 5, 5);

            formMain = Form_主窗体.forms;
            this._编辑类型 = 编辑类型;
            this._json元素信息 = 元素信息;


            工具箱_初始化();


            this.uiButton_No.Click += (s, e) =>
            {
                this.Close();
            };
            this.uiButton_Yes.Click += (s, e) =>
            {
                #region Yes

                bool isOk = false;
                if (this.uiradioButton_文本.Checked && this.con_文本 != null)
                {
                    isOk = this.con_文本.GetCfg();
                    if (isOk)
                    {
                        this._json元素信息 = new Json序列化().转成String(this.con_文本._cfg);
                    }

                }
                else if (this.uiradioButton_序列号.Checked && this.con_序列号 != null)
                {
                    isOk = this.con_序列号.GetCfg();
                    if (isOk)
                    {
                        this._json元素信息 = new Json序列化().转成String(this.con_序列号._cfg);
                    }
                }
                else if (this.uiradioButton_日期.Checked && this.con_日期 != null)
                {
                    isOk = this.con_日期.GetCfg(); if (isOk)
                    {
                        this._json元素信息 = new Json序列化().转成String(this.con_日期._cfg);
                    }
                }
                else if (this.uiradioButton_时间.Checked && this.con_时间 != null)
                {
                    isOk = this.con_时间.GetCfg();
                    if (isOk)
                    {
                        this._json元素信息 = new Json序列化().转成String(this.con_时间._cfg);
                    }
                }
                else if (this.uiradioButton_关联对象.Checked && this.con_关联对象 != null)
                {
                    if (Form_主窗体.forms._编辑对象索引 == 0)
                    {
                        isOk = false;
                        MessageBox.Show(Language_.Get语言("首对象时无法添加"), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        isOk = this.con_关联对象.GetCfg();
                        if (isOk)
                        {
                            this._json元素信息 = new Json序列化().转成String(this.con_关联对象._cfg);
                        }
                    }
                }
                else if (this.uiradioButton_班次.Checked)
                {
                    this._json元素信息 = new Json序列化().转成String(this._cfg_班次);
                    isOk = true;
                }

                if (isOk)
                {
                    this.DialogResult = DialogResult.OK;
                }


                #endregion
            };


            #region 工具选中事件

            #region 选中...文本


            this.uiradioButton_文本.ValueChanged += (s, e) =>
            {
                if (e)
                {
                    this.con_文本 = null;
                    this.con_文本 = new Control_文本(this._编辑类型, this._cfg_文本);
                    this.panel_控件.Controls.Clear();
                    this.panel_控件.Controls.Add(this.con_文本);
                }
            };

            #endregion

            #region 选中...序列号
            this.uiradioButton_序列号.ValueChanged += (s, e) =>
            {
                if (e)
                {
                    this.con_序列号 = null;
                    this.con_序列号 = new Control_序列号(this._编辑类型, this._cfg_序列号);
                    this.panel_控件.Controls.Clear();
                    this.panel_控件.Controls.Add(this.con_序列号);
                }
            };
            #endregion

            #region 选中...日期
            this.uiradioButton_日期.ValueChanged += (s, e) =>
            {
                if (e)
                {
                    this.con_日期 = null;
                    this.con_日期 = new Control_日期(this._编辑类型, this._cfg_日期);
                    this.panel_控件.Controls.Clear();
                    this.panel_控件.Controls.Add(this.con_日期);
                }
            };
            #endregion

            #region 选中...时间
            this.uiradioButton_时间.ValueChanged += (s, e) =>
            {
                if (e)
                {
                    this.con_时间 = null;
                    this.con_时间 = new Control_时间(this._编辑类型, this._cfg_时间);
                    this.panel_控件.Controls.Clear();
                    this.panel_控件.Controls.Add(this.con_时间);
                }
            };
            #endregion

            #region 选中...关联对象
            this.uiradioButton_关联对象.ValueChanged += (s, e) =>
            {
                if (e)
                {
                    this.con_关联对象 = null;
                    this.con_关联对象 = new Control_关联对象(this._编辑类型, this._cfg_关联对象);
                    this.panel_控件.Controls.Clear();
                    this.panel_控件.Controls.Add(this.con_关联对象);
                }
            };
            #endregion

            #region 选中...班次
            this.uiradioButton_班次.ValueChanged += (s, e) =>
            {
                if (e)
                {
                    this.panel_控件.Controls.Clear();
                }
            };

            #endregion


            #endregion


            #region 进入时....添加 / 修改


            switch (this._编辑类型)
            {
                case type_编辑._编辑类型_.添加:

                    #region 添加 

                    this.Text = Language_.Get语言("添加");
                    this.uiradioButton_文本.Checked = true;

                    #endregion

                    break;
                case type_编辑._编辑类型_.修改:

                    #region 修改

                    this.Text = Language_.Get语言("修改");
                    (bool s, string m) rt = (false, "");

                    (bool s, string m, _元素_.工具 v) rtAll = new Json序列化().转成Json<_元素_.工具>(this._json元素信息);
                    rt.s = rtAll.s;
                    rt.m = rtAll.m;
                    if (rtAll.s)
                    {
                        switch (rtAll.v.Tool)
                        {
                            case _em_工具箱_.文本:

                                #region 文本

                                var rt文本 = new Json序列化().转成Json<_元素_.文本>(this._json元素信息);
                                rt.s = rt文本.s;
                                rt.m = rt文本.m;
                                if (rt.s)
                                {
                                    this._cfg_文本 = rt文本.cfg.Clone();
                                    this.uiradioButton_文本.Checked = true;
                                }

                                #endregion

                                break;
                            case _em_工具箱_.序列号:

                                #region 序列号

                                var rt序列号 = new Json序列化().转成Json<_元素_.序列号>(this._json元素信息);
                                rt.s = rt序列号.s;
                                rt.m = rt序列号.m;
                                if (rt.s)
                                {
                                    this._cfg_序列号 = rt序列号.cfg.Clone();
                                    this.uiradioButton_序列号.Checked = true;
                                }

                                #endregion

                                break;
                            case _em_工具箱_.日期:

                                #region 日期

                                var rt日期 = new Json序列化().转成Json<_元素_.日期>(this._json元素信息);
                                rt.s = rt日期.s;
                                rt.m = rt日期.m;
                                if (rt.s)
                                {
                                    this._cfg_日期 = rt日期.cfg.Clone();
                                    this.uiradioButton_日期.Checked = true;
                                }

                                #endregion

                                break;
                            case _em_工具箱_.时间:

                                #region 时间

                                var rt时间 = new Json序列化().转成Json<_元素_.时间>(this._json元素信息);
                                rt.s = rt时间.s;
                                rt.m = rt时间.m;
                                if (rt.s)
                                {
                                    this._cfg_时间 = rt时间.cfg.Clone();
                                    this.uiradioButton_时间.Checked = true;
                                }

                                #endregion

                                break;
                            case _em_工具箱_.关联对象:

                                #region 关联对象

                                var rt关联对象 = new Json序列化().转成Json<_元素_.关联对象>(this._json元素信息);
                                rt.s = rt关联对象.s;
                                rt.m = rt关联对象.m;
                                if (rt.s)
                                {
                                    this._cfg_关联对象 = rt关联对象.cfg.Clone();
                                    this.uiradioButton_关联对象.Checked = true;
                                }

                                #endregion

                                break;
                            case _em_工具箱_.班次:

                                #region 关联对象

                                var rt班次 = new Json序列化().转成Json<_元素_.班次>(this._json元素信息);
                                rt.s = rt班次.s;
                                rt.m = rt班次.m;
                                if (rt.s)
                                {
                                    this._cfg_班次 = rt班次.cfg;
                                    this.uiradioButton_班次.Checked = true;
                                }

                                #endregion

                                break;
                        }
                    }
                    if (!rt.s)
                    {
                        MessageBox.Show(rt.m, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    #endregion

                    break;
            }


            #endregion

        }



        #region 工具箱及参数

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

            string name = Form_主窗体.forms._配方信息.对象[Form_主窗体.forms._编辑对象索引].对象名;
            bool IsShow序列号 = Form_主窗体.forms._编辑._功能.定制_二维码_明码强制防呆 
                && (name.Contains("明码") || name.Contains("codeM") || name.Contains("barcodeM")
                || name.Contains("二维码") || name.Contains("barcode"))
                ? false
                : true;
            if (IsShow序列号)
                显示工具(true, this.uiradioButton_序列号);

            显示工具(true, this.uiradioButton_文本);
        }

        #endregion



    }
}
