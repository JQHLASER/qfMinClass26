using Newtonsoft.Json;
using qfSqlSugar;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace qfCode
{
    public partial class Form_主窗体 : Sunny.UI.UIForm
    {
        internal 编辑_ _编辑;
        internal static Form_主窗体 forms;
        /// <summary>
        /// 配方文件
        /// </summary>
        internal _配方文件_属性_ _配方信息 = new _配方文件_属性_();
        internal int _编辑对象索引 = -1;
        internal string _配方名称 = "";
        internal List<_对象_内容_> _lst对象内容 = new List<_对象_内容_>();

        /// <summary>
        /// 当前编辑中的对象元素
        /// </summary>
        internal BindingList<_元素_Str_> _lstBind元素 = new BindingList<_元素_Str_>();

        internal 视图_ _视图;

        public Form_主窗体(string 配方名称, 编辑_ 编辑)
        {
            InitializeComponent();
            forms = this;
            this._编辑 = 编辑;
            this._配方名称 = 配方名称;

            this._视图 = new 视图_(this._编辑);
            this._视图.读写参数(1);

            视图设置();



            this.WindowState = FormWindowState.Maximized;
            this.Padding = new System.Windows.Forms.Padding(5, 35, 5, 5);

            this.dataGridView_元素.DataSource = this._lstBind元素;
            Datagridview格式();
            this.uiListBox_对象列表.Items.Clear();

            this.视图ToolStripMenuItem.Click += (s, e) =>
            {
                using (Form_视图 forms=new Form_视图 ())
                {
                    forms.ShowDialog();
                }
            };

            #region  对象

            this.uiListBox_对象列表.ItemClick += (s, e) =>
            {
                if (Err_未选中要操作的对象(this.uiListBox_对象列表, out int index))
                {
                    this._编辑对象索引 = index;
                    显示编辑对象信息();
                    显示元素信息();
                }
            };
            this.uiListBox_对象列表.ItemDoubleClick += (s, e) =>
            {
                if (Err_未选中要操作的对象(this.uiListBox_对象列表, out int index))
                {
                    On_对象_添加修改(type_编辑._编辑类型_.修改);
                }
            };
            this.ui_Button_对象_添加.Event_Click += () => this.On_对象_添加修改(type_编辑._编辑类型_.添加);
            this.ui_Button_对象_修改.Event_Click += () => this.On_对象_添加修改(type_编辑._编辑类型_.修改);
            this.ui_Button_对象_删除.Event_Click += () => this.On_对象_删除();

            this.ui_Button_对象_上移.Event_Click += () =>
            {
                new 对象_元素操作().上移一行(this._配方信息.对象);
                new 对象_元素操作().上移一行(this.uiListBox_对象列表);
            };
            this.ui_Button_对象_下移.Event_Click += () =>
            {
                new 对象_元素操作().下移一行(this._配方信息.对象);
                new 对象_元素操作().下移一行(this.uiListBox_对象列表);
            };

            this.ui_Button_对象_保存.Event_Click += () => On_保存();
            this.ui_Button_对象_预览.Event_Click += () =>
            {
                #region 预览

                if (Err_未选中要操作的对象(this.uiListBox_对象列表, out int index))
                {
                    string objectName = this._配方信息.对象[index].对象名;
                    DateTime now = DateTime.Now;
                    var rt = new 编辑交互_统一接口(this._编辑)._Iworker.计算编码_对象(this._配方信息, now, objectName);
                    if (!rt.s)
                    {
                        MessageBox.Show(rt.m, "Err", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    MessageBox.Show(rt.v);
                }

                #endregion
            };

            #endregion


            #region  元素

            this.dataGridView_元素.DoubleClick += (s, e) =>
            {
                if (Err_未选中要操作的元素(this.dataGridView_元素, out int index))
                {
                    this.On_元素_添加修改(type_编辑._编辑类型_.修改);
                }
            };
            this.ui_Button_元素_添加.Event_Click += () => this.On_元素_添加修改(type_编辑._编辑类型_.添加);
            this.ui_Button_元素_修改.Event_Click += () => this.On_元素_添加修改(type_编辑._编辑类型_.修改);
            this.ui_Button_元素_删除.Event_Click += () => this.On_元素_删除();

            this.ui_Button_元素_上移.Event_Click += () =>
            {
                if (Err_未选中要操作的元素(this.dataGridView_元素, out int index, false))
                {
                    new 对象_元素操作().上移一行(this._配方信息.对象[this._编辑对象索引].元素);
                    new 对象_元素操作().上移一行<_元素_Str_>(this._lstBind元素, index, dataGridView_元素);
                }
            };
            this.ui_Button_元素_下移.Event_Click += () =>
            {
                if (Err_未选中要操作的元素(this.dataGridView_元素, out int index, false))
                {
                    new 对象_元素操作().下移一行(this._配方信息.对象[this._编辑对象索引].元素);
                    new 对象_元素操作().下移一行<_元素_Str_>(this._lstBind元素, index, dataGridView_元素);
                }
            };



            #endregion


            //如果配方名称不为空时,则加载配方信息
            if (!string.IsNullOrEmpty(this._配方名称))
            {
                打开(this._配方名称);
            }
        }


        #region 对象

        void On_对象_添加修改(type_编辑._编辑类型_ 类型)
        {
            switch (类型)
            {
                case type_编辑._编辑类型_.添加:
                    #region 添加

                    using (Form_对象 forms = new Form_对象(类型, "", new _对象_属性()))
                    {
                        if (forms.ShowDialog() == DialogResult.OK)
                        {
                            int index0 = this.uiListBox_对象列表.SelectedIndex;
                            _对象_ objc = new _对象_
                            {
                                对象名 = forms._对象名称,
                                属性 = forms._cfg.Clone(),
                            };
                            if (index0 < 0)
                            {
                                this._配方信息.对象.Add(objc);
                                this.uiListBox_对象列表.Items.Add(forms._对象名称);
                            }
                            else
                            {
                                new 对象_元素操作().在指定处插入(this._配方信息.对象, objc, index0 + 1);
                                new 对象_元素操作().在指定处插入(this.uiListBox_对象列表, forms._对象名称, index0 + 1);
                            }
                        }
                    }
                    #endregion
                    break;
                case type_编辑._编辑类型_.修改:
                    #region 修改
                    if (Err_未选中要操作的对象(this.uiListBox_对象列表, out int index))
                    {
                        string txt = this._配方信息.对象[index].对象名;
                        _对象_属性 cfgObject = this._配方信息.对象[index].属性.Clone();
                        using (Form_对象 forms = new Form_对象(类型, txt, cfgObject))
                        {
                            if (forms.ShowDialog() == DialogResult.OK)
                            {
                                this._配方信息.对象[index].对象名 = forms._对象名称;
                                this._配方信息.对象[index].属性 = forms._cfg.Clone();
                                this.uiListBox_对象列表.Items[index] = forms._对象名称;
                            }
                        }
                    }
                    #endregion
                    break;
            }



        }
        void On_对象_删除()
        {
            if (Err_未选中要操作的对象(this.uiListBox_对象列表, out int index)
                && MessageBox.Show(Language_.Get语言("确认删除?"), "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this._配方信息.对象.RemoveAt(index);
                this.uiListBox_对象列表.Items.RemoveAt(index);
                if (index == this._编辑对象索引)
                {
                    清空显示的元素信息(-1);

                }
            }
        }



        #endregion

        #region 元素

        void On_元素_添加修改(type_编辑._编辑类型_ 类型)
        {
            if (!Err_未选中要编辑的对象(out string 对象名))
            {
                return;
            }
            switch (类型)
            {
                case type_编辑._编辑类型_.添加:
                    #region 添加

                    using (Form_工具箱_元素 forms = new Form_工具箱_元素(类型, ""))
                    {
                        if (forms.ShowDialog() == DialogResult.OK)
                        {
                            new qfNet.DataGridview_(this.dataGridView_元素).获取当前选中的行号(out int index0);
                            if (index0 < 0)
                            {
                                this._配方信息.对象[this._编辑对象索引].元素.Add(forms._json元素信息);
                                var rt = 计算_元素(forms._json元素信息);
                                this._lstBind元素.Add(rt.cfg);
                            }
                            else
                            {
                                new 对象_元素操作().在指定处插入(this._配方信息.对象[this._编辑对象索引].元素, forms._json元素信息, index0 + 1);

                                var rt = 计算_元素(forms._json元素信息);
                                new 对象_元素操作().在指定处插入<_元素_Str_>(this._lstBind元素, rt.cfg, index0 + 1, dataGridView_元素);

                            }

                        }
                    }
                    #endregion
                    break;
                case type_编辑._编辑类型_.修改:
                    #region 修改
                    if (Err_未选中要操作的元素(this.dataGridView_元素, out int index))
                    {
                        string txt = this._配方信息.对象[this._编辑对象索引].元素[index];
                        using (Form_工具箱_元素 forms = new Form_工具箱_元素(类型, txt))
                        {
                            if (forms.ShowDialog() == DialogResult.OK)
                            {
                                this._配方信息.对象[this._编辑对象索引].元素[index] = forms._json元素信息;
                                var rt = 计算_元素(forms._json元素信息);
                                this._lstBind元素[index] = rt.cfg;
                            }
                        }
                    }
                    #endregion
                    break;
            }



        }
        void On_元素_删除()
        {
            if (Err_未选中要操作的对象(this.uiListBox_对象列表, out int index)
                && Err_未选中要操作的元素(this.dataGridView_元素, out index)
                && MessageBox.Show(Language_.Get语言("确认删除?"), "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this._配方信息.对象[this._编辑对象索引].元素.RemoveAt(index);
                this.uiListBox_对象列表.Items.RemoveAt(index);
            }
        }

        (bool s, string m, _元素_Str_ cfg) 计算_元素(string json元素)
        {
            if (Err_未选中要编辑的对象(out string 编辑对象))
            {
                DateTime now = DateTime.Now;

                var rtJS = 计算_所有对象内容();
                if (rtJS.s)
                {
                    var rt = new 编辑交互_统一接口(this._编辑)._Iworker.计算元素(this._配方信息, this._lst对象内容, now, this._配方信息.对象[this._编辑对象索引], json元素);
                    return rt;
                }
                else
                {
                    return (rtJS.s, rtJS.m, default);
                }
            }
            return (false, "", default);
        }

        void Datagridview格式()
        {
            var grid = new qfNet.DataGridview_(this.dataGridView_元素).格式化();
            grid.设置行高(30);
            grid.显示or隐藏标题(false);
            grid.使能修改列宽(true);
            grid.设置列宽(0, 200);
            grid.设置列宽(1, 500);
            grid.设置字体_整体(new Font("微软雅黑", 9f));

        }


        #endregion

        #region 保存/另存为


        void On_保存()
        {
            this._配方名称 = "abc";
            if (string.IsNullOrEmpty(this._配方名称))
            {

                return;
            }

            var rt = 保存(this._配方名称);
            if (!rt.s)
            {
                MessageBox.Show(rt.m, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                MessageBox.Show(Language_.Get语言("保存成功"));
                return;
            }

        }

        void On_另存为()
        {

            var rt = 保存(this._配方名称);
            if (!rt.s)
            {
                MessageBox.Show(rt.m, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                MessageBox.Show(Language_.Get语言("保存成功"));
                return;
            }

        }



        #endregion

        #region 编辑对象显示信息

        void 显示编辑对象信息()
        {
            this.uiTitlePanel_元素列表.Text = string.Empty;
            if (!Err_未选中要编辑的对象(out string 对象名, false))
            {
                return;
            }
            int index = this._编辑对象索引;
            _对象_ objc = this._配方信息.对象[this._编辑对象索引];


            StringBuilder sb = new StringBuilder();
            sb.Append($"{Language_.Get语言("对象")}: {objc.对象名}");

            if (this._编辑._功能.对象属性.防重 && objc.属性.防重)
            {
                sb.Append($"<{Language_.Get语言("防重")}>");
            }
            if (this._编辑._功能.对象属性.读码 && objc.属性.读码)
            {
                sb.Append($"<{Language_.Get语言("读码")}>");
            }
            if (this._编辑._功能.对象属性.校验模板 && objc.属性.校验模板)
            {
                sb.Append($"<{Language_.Get语言("校验模板")}>");
            }
            if (this._编辑._功能.对象属性.校验关键字 && objc.属性.校验关键字)
            {
                sb.Append($"<{Language_.Get语言("校验关键字")}>");
            }
            if (this._编辑._功能.对象属性.校验位数 && objc.属性.校验位数 > 0)
            {
                sb.Append($"<{Language_.Get语言("校验位数")} {objc.属性.校验位数}>");
            }
            this.uiTitlePanel_元素列表.Text = sb.ToString();


        }

        void 显示元素信息()
        {
            this._lstBind元素.Clear();
            if (!Err_未选中要编辑的对象(out string 对象名, false))
            {
                return;
            }
            foreach (var item in this._配方信息.对象[this._编辑对象索引].元素)
            {
                var rt = 计算_元素(item);
                this._lstBind元素.Add(rt.cfg);
            }
        }

        void 清空显示的元素信息(int 对象索引)
        {
            this._编辑对象索引 = 对象索引;
            this.uiTitlePanel_元素列表.Text = string.Empty;
            this._lstBind元素.Clear();
        }

        #endregion

        #region Err

        bool Err_未选中要操作的元素(DataGridView view, out int index, bool is弹窗 = true)
        {
            new qfNet.DataGridview_(view).获取当前选中的行号(out index);
            if (index < 0)
            {
                if (is弹窗)
                {
                    MessageBox.Show(Language_.Get语言("未选中要操作的元素"), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return false;
            }
            return true;
        }

        bool Err_未选中要操作的对象(UIListBox listbox, out int index)
        {
            index = listbox.SelectedIndex;
            if (index < 0)
            {
                MessageBox.Show(Language_.Get语言("未选中要操作的对象"), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }


        bool Err_未选中要编辑的对象(out string 编辑对象, bool 是否弹窗 = true)
        {
            编辑对象 = string.Empty;
            if (this._编辑对象索引 < 0)
            {
                if (是否弹窗)
                {
                    MessageBox.Show(Language_.Get语言("未选中要编辑的对象"), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return false;
            }

            编辑对象 = this._配方信息.对象[this._编辑对象索引].对象名;
            return true;
        }

        internal bool Err_对象名重复(string 对象名)
        {
            foreach (var s in this._配方信息.对象)
            {
                if (对象名 == s.对象名)
                {
                    return false;
                }
            }
            return true;
        }




        #endregion

        #region 方法

        internal void 视图设置()
        {
            this.tableLayoutPanel_下边栏.Height = this._视图._cfg.下边栏;
            this.tableLayoutPanel_配方.ColumnStyles[1].SizeType = SizeType.Absolute;
            this.tableLayoutPanel_配方.ColumnStyles[0].Width = this._视图._cfg.左边栏;
        }

        (bool s, string m, List<_对象_内容_> lst) 计算_所有对象内容()
        {
            var rt = new 编辑交互_统一接口(this._编辑)._Iworker.计算编码(this._配方名称, this._配方信息, DateTime.Now, _em_计算类型_.测试, false);
            this._lst对象内容 = rt.lstObject;
            return rt;
        }

        (bool s, string m) 保存(string 配方名称)
        {
            DateTime now = DateTime.Now;
            this._配方信息.Datetimes = now.ToString("yyyy-MM-dd HH:mm:ss");
            this._配方信息.备注 = this.textBox_备注.Text;
            return new 编辑交互_统一接口(this._编辑)._Iworker.配方_保存(this._配方信息, 配方名称, now);
        }

        (bool s, string m, _配方文件_属性_ cfg) 打开(string 配方名称)
        {
            var rt = new 编辑交互_统一接口(this._编辑)._Iworker.配方_打开(配方名称);

            this._配方信息 = rt.cfg.Clone();
            显示所有对象名();
            this.Text = this._配方名称;
            显示配方信息(this._配方信息);
            return rt;
        }

        void 显示配方信息(_配方文件_属性_ cfg)
        {
            this.textBox_备注.Text = cfg.备注;
            StringBuilder sb = new StringBuilder();
            if (this._编辑._功能.工具箱.班次)
            {
                sb.AppendLine($"{Language_.Get语言("班次配置")}: {cfg.班次文件}");
            }

            if (this._编辑._功能.日期时间.更新日期)
            {
                sb.AppendLine($"{Language_.Get语言("更新日期")}: {cfg.更新时间}");
            }

            this.textBox_信息.Text = sb.ToString();
        }



        void 显示所有对象名()
        {
            this.uiListBox_对象列表.Items.Clear();
            foreach (var s in this._配方信息.对象)
            {
                this.uiListBox_对象列表.Items.Add(s.对象名);
            }



        }




        #endregion


    }
}
