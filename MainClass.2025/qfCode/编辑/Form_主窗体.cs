using qfSqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
        internal _文件_属性_ _文件信息 = new _文件_属性_();
        internal int _编辑对象索引 = -1;

        public Form_主窗体(编辑_ 编辑)
        {
            InitializeComponent();
            forms = this;
            this._编辑 = 编辑;
            this.WindowState = FormWindowState.Maximized;
            this.Padding = new System.Windows.Forms.Padding(5, 35, 5, 5);

            this.uiListBox_元素列表.Items.Clear();
            this.uiListBox_对象列表.Items.Clear();


            #region 按钮....对象

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
                new 上下移动().上移一行(this._文件信息.对象);
                new 上下移动().上移一行(this.uiListBox_对象列表);
            };
            this.ui_Button_对象_下移.Event_Click += () =>
            {
                new 上下移动().下移一行(this._文件信息.对象);
                new 上下移动().下移一行(this.uiListBox_对象列表);
            };

            this.ui_Button_对象_保存.Event_Click += () => On_保存();
            this.ui_Button_对象_预览.Event_Click += () => On_预览();

            #endregion


            #region 按钮....元素

            this.uiListBox_元素列表.ItemDoubleClick += (s, e) =>
            {
                if (Err_未选中要操作的对象(this.uiListBox_元素列表, out int index))
                {
                    this.On_元素_添加修改(type_编辑._编辑类型_.修改);
                }
            };
            this.ui_Button_元素_添加.Event_Click += () => this.On_元素_添加修改(type_编辑._编辑类型_.添加);
            this.ui_Button_元素_修改.Event_Click += () => this.On_元素_添加修改(type_编辑._编辑类型_.修改);
            this.ui_Button_元素_删除.Event_Click += () => this.On_元素_删除();

            this.ui_Button_元素_上移.Event_Click += () =>
            {
                new 上下移动().上移一行(this._文件信息.对象[this._编辑对象索引].元素);
                new 上下移动().上移一行(this.uiListBox_元素列表);
            };
            this.ui_Button_元素_下移.Event_Click += () =>
            {
                new 上下移动().下移一行(this._文件信息.对象[this._编辑对象索引].元素);
                new 上下移动().下移一行(this.uiListBox_元素列表);
            };



            #endregion


        }


        #region 对象

        void On_对象_添加修改(type_编辑._编辑类型_ 类型)
        {
            switch (类型)
            {
                case type_编辑._编辑类型_.添加:
                    #region 添加

                    using (Form_对象 forms = new Form_对象(类型, ""))
                    {
                        if (forms.ShowDialog() == DialogResult.OK)
                        {
                            int index0 = this.uiListBox_对象列表.SelectedIndex;
                            _对象_ objc = new _对象_
                            {
                                对象名 = forms._对象名称,
                            };
                            if (index0 < 0)
                            {
                                this._文件信息.对象.Add(objc);
                                this.uiListBox_对象列表.Items.Add(forms._对象名称);
                            }
                            else
                            {
                                new 上下移动().在指定处插入(this._文件信息.对象, objc, index0 + 1);
                                new 上下移动().在指定处插入(this.uiListBox_对象列表, forms._对象名称, index0 + 1);
                            }
                        }
                    }
                    #endregion
                    break;
                case type_编辑._编辑类型_.修改:
                    #region 修改
                    if (Err_未选中要操作的对象(this.uiListBox_对象列表, out int index))
                    {
                        string txt = this._文件信息.对象[index].对象名;
                        using (Form_对象 forms = new Form_对象(类型, txt))
                        {
                            if (forms.ShowDialog() == DialogResult.OK)
                            {
                                this._文件信息.对象[index].对象名 = forms._对象名称;
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
                this._文件信息.对象.RemoveAt(index);
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
                            int index0 = this.uiListBox_元素列表.SelectedIndex;
                            if (index0 < 0)
                            {
                                this._文件信息.对象[this._编辑对象索引].元素.Add(forms._json元素信息);
                                this.uiListBox_元素列表.Items.Add(forms._json元素信息);
                            }
                            else
                            {
                                new 上下移动().在指定处插入(this._文件信息.对象[this._编辑对象索引].元素, forms._json元素信息, index0 + 1);
                                new 上下移动().在指定处插入(this.uiListBox_元素列表, forms._json元素信息, index0 + 1);
                            }
                        }
                    }
                    #endregion
                    break;
                case type_编辑._编辑类型_.修改:
                    #region 修改
                    if (Err_未选中要操作的对象(this.uiListBox_元素列表, out int index))
                    {
                        string txt = this._文件信息.对象[this._编辑对象索引].元素[index];
                        using (Form_工具箱_元素 forms = new Form_工具箱_元素(类型, txt))
                        {
                            if (forms.ShowDialog() == DialogResult.OK)
                            {
                                this._文件信息.对象[this._编辑对象索引].元素[index] = forms._json元素信息;
                                this.uiListBox_元素列表.Items[index] = forms._json元素信息;
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
                && MessageBox.Show(Language_.Get语言("确认删除?"), "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this._文件信息.对象[this._编辑对象索引].元素.RemoveAt(index);
                this.uiListBox_对象列表.Items.RemoveAt(index);
            }
        }



        #endregion

        #region 保存 / 预览


        void On_保存()
        {

        }

        void On_预览()
        {

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
            _对象_ objc = this._文件信息.对象[this._编辑对象索引];


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
            this.uiListBox_元素列表.Items.Clear();
            if (!Err_未选中要编辑的对象(out string 对象名, false))
            {
                return;
            }
            foreach (var item in this._文件信息.对象[this._编辑对象索引].元素)
            {
                this.uiListBox_元素列表.Items.Add(item);
            }
        }

        void 清空显示的元素信息(int 对象索引)
        {
            this._编辑对象索引 = 对象索引;
            this.uiTitlePanel_元素列表.Text = string.Empty;
            this.uiListBox_元素列表.Items.Clear();
        }

        #endregion

        #region Err

        bool Err_未选中要操作的对象(Sunny.UI.UIListBox listBox, out int index)
        {
            index = listBox.SelectedIndex;
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

            编辑对象 = this._文件信息.对象[this._编辑对象索引].对象名;
            return true;
        }

        internal bool Err_对象名重复(string 对象名)
        {
            foreach (var s in this._文件信息.对象)
            {
                if (对象名 == s.对象名)
                {
                    return false;
                }
            }
            return true;
        }




        #endregion
    }
}
