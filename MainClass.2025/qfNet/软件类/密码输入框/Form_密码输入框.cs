using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace qfNet
{
    public partial class Form_密码输入框 : Sunny.UI.UIForm
    {
        string _正确密码 = "8888";
        bool _是否可以关闭 = true;

        /// <summary>
        /// 正确时返回 DialogResult = DialogResult.Yes
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="正确密码_"></param>
        public Form_密码输入框(string Title, string 正确密码_, bool 是否可以关闭_ = true)
        {
            InitializeComponent();
            this._正确密码 = 正确密码_;
            this.Text = Title;
            this._是否可以关闭 = 是否可以关闭_;
            if (!this._是否可以关闭)
            {
                this.uiButton_No.Visible = false;
                this.uiButton_Yes.Top = 229;
                this.uiButton_Yes.Left = 219;
            }


            this.uiTextBox_psd.Clear();

            this.uiButton_No.Click += (s, e) => On_No();
            this.uiButton_Yes.Click += (s, e) => On_Yes();
            this.Shown += (s, e) => On_Shown();
            this.KeyDown += (s, e) => On_KeyDown(e);
        }

        private void Form_密码输入框_Load(object sender, EventArgs e)
        {

        }
        void On_No()
        {
            this.Close ();
        }
        void On_Shown()
        {
            this.uiTextBox_psd.Focus();
        }
        void On_Yes()
        {
            string pasd = this.uiTextBox_psd.Text.Trim();
            if (string.IsNullOrEmpty(pasd))
            {
                MessageBox.Show(Language_.Get语言("密码不能为空"), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (pasd != this._正确密码)
            {
                MessageBox.Show(Language_.Get语言("密码错误"), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.DialogResult = DialogResult.Yes;

        }

        void On_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                On_Yes();
            }
        }


    }
}
