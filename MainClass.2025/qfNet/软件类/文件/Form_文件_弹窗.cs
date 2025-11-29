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
        _文件弹窗类型_ _类型;
        Label _label;
        string _后缀;


        /// <summary>
        /// <para>label_ : 用来效互的变量</para>
        /// <para>File : 文件夹路径 </para>
        /// </summary> 
        public Form_文件_弹窗(Label label_, String File, string 文件类型, string 后缀, _文件弹窗类型_ 类型_ = _文件弹窗类型_.打开)
        {
            InitializeComponent();
            this._label = label_;
            this._类型 = 类型_;
            this._后缀 = 后缀;

            this.uiLabel_后缀.Text = $"{文件类型}|*.{this._后缀}";

            this.Text = (this._类型 == _文件弹窗类型_.打开) ? "Open"
                : (this._类型 == _文件弹窗类型_.保存) ? "Save"
                : "";
            new qfNet.winForm窗体().Set设置_Padding(this, 5);

            this.uiListBox1.ItemClick += (s, e) => On_ItemClick();
            this.uiListBox1.ItemDoubleClick += (s, e) => On_Yes();
            this.uiButton_No.Click += (s, e) => On_No();
            this.uiButton_Yes.Click += (s, e) => On_Yes();

            new qfmain.文件_文件夹().文件_获取文件名_指定后缀(File, this._后缀, out string[] nameBeff, out string msgErr);

            this.uiListBox1.Items.Clear();
            foreach (var s in nameBeff)
            {
                new qfmain.文件_文件夹().文件_获取文件名_含后缀(s, out string name, out string msgErr1);
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
                    this._label.Text = str;
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

                    this._label.Text = str;
                    this.DialogResult = DialogResult.OK;

                    #endregion

                    break;
            }


        }
    }
}
