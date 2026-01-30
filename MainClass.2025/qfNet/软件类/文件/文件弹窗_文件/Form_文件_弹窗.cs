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
        string _File;
        /// <summary>
        /// 选中文件名称
        /// </summary>
        public string _selectedFileName = "";



        /// <summary>
        /// <para>label_ : 用来效互的变量</para>
        /// <para>File : 文件夹路径 </para>
        /// </summary> 
        public Form_文件_弹窗( String File, string 文件类型, string 后缀, _文件弹窗类型_ 类型_ = _文件弹窗类型_.打开)
        {
            InitializeComponent();         
            this._类型 = 类型_;
            this._后缀 = 后缀;
            this._File = File;

            this.uiLabel_后缀.Text = $"{文件类型}|*{this._后缀}";
      
            this.Text = (this._类型 == _文件弹窗类型_.打开) ? "Open"
                : (this._类型 == _文件弹窗类型_.保存) ? "Save"
                : "";
            new qfNet.winForm窗体().Set设置_Padding(this, 5);

            this.uiListBox1.ItemClick += (s, e) => On_ItemClick();
            this.uiListBox1.ItemDoubleClick += (s, e) => On_Yes();
            this.uiButton_No.Click += (s, e) => On_No();
            this.uiButton_Yes.Click += (s, e) => On_Yes();
            this.删除ToolStripMenuItem.Click += (s, e) => On_删除();


            new qfmain.文件_文件夹().文件夹_获取所有文件_无后缀(File, out List<string> lstName, $"*{this._后缀}");
            this.uiListBox1.Items.Clear();
            foreach (var s in lstName)
            {
                this.uiListBox1.Items.Add(s);
            }

        }

        private void Form_文件_弹窗_Load(object sender, EventArgs e)
        {

        }

        //单击
        void On_ItemClick()
        {
            int index = this.uiListBox1.SelectedIndex;
            if (index < 0)
            {
                return;
            }
            this.uiTextBox_FileName.Text = this.uiListBox1.Items[index].ToString();
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
                return;
            }
            switch (this._类型)
            {
                case _文件弹窗类型_.打开:

                    #region 打开

                    if (this.uiListBox1.Items.IndexOf(str) < 0)
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

                    if (this.uiListBox1.Items.IndexOf(str) > -1
                        && MessageBox.Show(Language_.Get语言("文件已存在,是否替换?"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                    {
                        return;
                    }

                   this. _selectedFileName = str;               
                    this.DialogResult = DialogResult.OK;

                    #endregion

                    break;
            }


        }

        void On_删除()
        {
            int index = this.uiListBox1.SelectedIndex;
            if (index < 0)
            {
                return;
            }
            else if (MessageBox.Show($"{Language_.Get语言("是否确认删除")}?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string Name = this.uiListBox1.Items[index].ToString();
                string path = $"{this._File}\\{Name}{this._后缀}";
                bool rt = new qfmain.文件_文件夹().文件_删除文件(path, out string msgErr);
                if (rt)
                {
                    MessageBox.Show(Language_.Get语言("删除成功"));
                }
                else
                {
                    MessageBox.Show(msgErr, "", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                }
            }

        }
    }
}
