using Sunny.UI.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace qfNet
{
    public partial class Form_文件_弹窗 : Sunny.UI.UIForm
    {
        //双缓冲显示窗体所有子控件
        //   protected override CreateParams CreateParams { get { CreateParams cp = base.CreateParams; cp.ExStyle |= 0x02000000; return cp; } }

        _文件弹窗类型_ _类型;
        string _后缀;

        BindingList<_FileName_> _文件目录;
        /// <summary>
        /// 选中文件名称
        /// </summary>
        public string _selectedFileName = "";
        qfNet.DataGridview_ _datagridSys;
        Func<string, (bool s, string m)> _Event_删除文件;

        public class _FileName_
        {
            public string Name { set; get; }
        }



        /// <summary>
        /// <para>label_ : 用来效互的变量</para>
        /// <para>File : 文件夹路径 </para>
        /// </summary> 
        public Form_文件_弹窗(string[] 文件目录, string 文件类型, string 后缀, _文件弹窗类型_ 类型_ = _文件弹窗类型_.打开, Func<string, (bool s, string m)> Event_删除文件 = null)
        {
            InitializeComponent();
            this._类型 = 类型_;
            this._后缀 = 后缀;
            this._Event_删除文件 = Event_删除文件;

            this.uiDataGridView1.RectColor = Color.Silver;

            this._文件目录 = new BindingList<_FileName_>(文件目录.Select(s => new _FileName_ { Name = s }).ToList());
            this.uiDataGridView1.DataSource = this._文件目录;
            _datagridSys = new DataGridview_(this.uiDataGridView1).格式化()
             .显示or隐藏标题(false)
             .设置行高(30)
             .列为只读 ();


            this.uiLabel_后缀.Text = $"{文件类型}|*{this._后缀}";

            this.Text = (this._类型 == _文件弹窗类型_.打开) ? "Open"
                : (this._类型 == _文件弹窗类型_.保存) ? "Save"
                : "";
            new qfNet.winForm窗体().Set设置_Padding(this, 10);

            this.uiDataGridView1.Click += (s, e) => On_ItemClick();
            this.uiDataGridView1.DoubleClick += (s, e) => On_Yes();
            this.Resize += (s, e) =>
            {
                设置宽度();
            };
            this.Shown += (s, e) =>
            {
                设置宽度();
                this._datagridSys.选中行_取消当前选中选中();
            };

            this.uiButton_No.Click += (s, e) => On_No();
            this.uiButton_Yes.Click += (s, e) => On_Yes();
            this.删除ToolStripMenuItem.Click += (s, e) => On_删除();
            this.删除ToolStripMenuItem.Visible = this._Event_删除文件 is null ? false : true;


        }

        private void Form_文件_弹窗_Load(object sender, EventArgs e)
        {

        }

        //单击
        void On_ItemClick()
        {
            this._datagridSys.获取当前选中的行号(out int index);
            if (index < 0)
            {
                return;
            }
            this.uiTextBox_FileName.Text = this._文件目录[index].Name;
        }


        void On_No()
        {
            this.Close();
        }

        //选中
        void On_Yes()
        {
            string str = this.uiTextBox_FileName.Text.Trim();
            if (string.IsNullOrEmpty(str))
            {
                MessageBox.Show(Language_.Get语言("未选中文件"), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            switch (this._类型)
            {
                case _文件弹窗类型_.打开:

                    #region 打开

                    if (!this._文件目录.Any(x => x.Name == str))
                    {
                        MessageBox.Show(Language_.Get语言("文件不存在"), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    this._selectedFileName = str;
                    this.DialogResult = DialogResult.OK;

                    #endregion

                    break;
                case _文件弹窗类型_.保存:

                    #region 保存

                    if (this._文件目录.Any(x => x.Name == str)
                        && MessageBox.Show(Language_.Get语言("文件已存在,是否替换?"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                    {
                        return;
                    }

                    this._selectedFileName = str;
                    this.DialogResult = DialogResult.OK;

                    #endregion

                    break;
            }


        }

        void On_删除()
        {
            this._datagridSys.获取当前选中的行号(out int index);
            if (index < 0)
            {
                return;
            }
            else if (_Event_删除文件 != null && MessageBox.Show($"{Language_.Get语言("是否确认删除")}?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            { 
                string Name = this._文件目录[index].Name;
                var rt = this._Event_删除文件.Invoke(Name);
                if (rt.s)
                {
                    this._文件目录.RemoveAt(index);
                    this.uiTextBox_FileName.Clear();
                    MessageBox.Show(Language_.Get语言("删除成功"));
                }
                else
                {
                    MessageBox.Show(rt.m, "", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                }
            }

        }

        void 设置宽度()
        {
            this._datagridSys.设置列宽(0, this.uiDataGridView1.Width - 30);
        }




    }
}
