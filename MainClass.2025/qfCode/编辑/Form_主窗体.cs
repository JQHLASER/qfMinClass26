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
    public partial class Form_主窗体 : Sunny.UI.UIForm
    {
        internal 编辑_ _编辑;
        internal static Form_主窗体 forms;
        internal _文件_属性_ _文件信息 = new _文件_属性_();
        internal List<string> lst对象列表 = new List<string>();

        /// <summary>
        /// 当前正在编辑的
        /// </summary>
        internal List<string> lst元素列表 = new List<string>();


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

            this.uiListBox_对象列表.ItemDoubleClick += (s, e) =>
            {
                if (Err_未选中要操作的对象(this.uiListBox_对象列表, out int index))
                {
                   // this.On_对象_添加修改(type_编辑._编辑类型_.修改);
                }
            };
            this.ui_Button_对象_添加.Event_Click += () => this.On_对象_添加修改(type_编辑._编辑类型_.添加);
            this.ui_Button_对象_修改.Event_Click += () => this.On_对象_添加修改(type_编辑._编辑类型_.修改);
            this.ui_Button_对象_删除.Event_Click += () => this.On_对象_删除();

            this.ui_Button_对象_上移.Event_Click += () =>
            {
                new 上下移动().上移一行(this.lst对象列表);
                new 上下移动().上移一行(this.uiListBox_对象列表);
            };
            this.ui_Button_对象_下移.Event_Click += () =>
            {
                new 上下移动().下移一行(this.lst对象列表);
                new 上下移动().下移一行(this.uiListBox_对象列表);
            };


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
                new 上下移动().上移一行(this.lst元素列表);
                new 上下移动().上移一行(this.uiListBox_元素列表);
            };
            this.ui_Button_元素_下移.Event_Click += () =>
            {
                new 上下移动().下移一行(this.lst元素列表);
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
                            if (index0 < 0)
                            {
                                this.lst对象列表.Add(forms._对象名称);
                                this.uiListBox_对象列表.Items.Add(forms._对象名称);
                            }
                            else
                            {
                                new 上下移动().在指定处插入(this.lst对象列表, forms._对象名称, index0 + 1);
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
                        string txt = this.lst对象列表[index];
                        using (Form_对象 forms = new Form_对象(类型, txt))
                        {
                            if (forms.ShowDialog() == DialogResult.OK)
                            {
                                this.lst对象列表[index] = forms._对象名称;
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
                this.lst对象列表.RemoveAt(index);
                this.uiListBox_对象列表.Items.RemoveAt(index);
            }
        }






        #endregion

        #region 元素

        void On_元素_添加修改(type_编辑._编辑类型_ 类型)
        {
            switch (类型)
            {
                case type_编辑._编辑类型_.添加:
                    #region 添加

                    using (Form_工具箱_元素 forms = new Form_工具箱_元素(类型, ""))
                    {
                        if (forms.ShowDialog() == DialogResult.OK)
                        {
                            int index0 = this.uiListBox_对象列表.SelectedIndex;
                            if (index0 < 0)
                            {
                                this.lst元素列表.Add(forms._元素信息);
                                this.uiListBox_元素列表.Items.Add(forms._元素信息);
                            }
                            else
                            {
                                new 上下移动().在指定处插入(this.lst元素列表, forms._元素信息, index0 + 1);
                                new 上下移动().在指定处插入(this.uiListBox_元素列表, forms._元素信息, index0 + 1);
                            }

                        }
                    }
                    #endregion
                    break;
                case type_编辑._编辑类型_.修改:
                    #region 修改
                    //if (Err_未选中要操作的对象(this.uiListBox_对象列表, out int index))
                    //{
                    //    string txt = this.lst对象列表[index];
                    //    using (Form_对象 forms = new Form_对象(类型, txt))
                    //    {
                    //        if (forms.ShowDialog() == DialogResult.OK)
                    //        {
                    //            this.lst对象列表[index] = forms._对象名称;
                    //            this.uiListBox_对象列表.Items[index] = forms._对象名称;
                    //        }
                    //    }
                    //}
                    #endregion
                    break;
            }



        }
        void On_元素_删除()
        {
            if (Err_未选中要操作的对象(this.uiListBox_对象列表, out int index)
                && MessageBox.Show(Language_.Get语言("确认删除?"), "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.lst对象列表.RemoveAt(index);
                this.uiListBox_对象列表.Items.RemoveAt(index);
            }
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


        #endregion
    }
}
