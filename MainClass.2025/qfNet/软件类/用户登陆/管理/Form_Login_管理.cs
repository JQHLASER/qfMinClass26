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
    internal partial class Form_Login_管理 : Sunny.UI.UIForm
    {
        public enum _操作方式_
        {
            添加,
            修改,
        }

        protected override CreateParams CreateParams { get { CreateParams cp = base.CreateParams; cp.ExStyle |= 0x02000000; return cp; } }//双缓冲显示窗体所有子控件
        internal Login登陆 _Login_sys;
        Form_Login_管理 forms;
        internal List<_loginInfo_> lstInfo = new List<_loginInfo_>();



        internal Form_Login_管理(Login登陆 Login_sys_)
        {
            InitializeComponent();
            this._Login_sys = Login_sys_;
            forms = this;
            this.lstInfo = this._Login_sys._Config.loginInfo_Beff.ToList();
            显示全部信息();

            this.toolStripButton_添加.Click += (s, e) => 添加();
            this.toolStripButton_修改.Click += (s, e) => 修改();
            this.toolStripButton_删除.Click += (s, e) => 删除();
            this.toolStripButton_保存.Click += (s, e) => 保存();

            this.listView_userInfo.DoubleClick += (s, e) => 修改();
            this.FormClosing += (s, e) => FormClosing_();
        }

        private void Form_Login_管理_Load(object sender, EventArgs e)
        {
            this.Text = Language_.Get语言("用户管理");
            this.toolStripButton_添加.Text = Language_.Get语言("添加");
            this.toolStripButton_修改.Text = Language_.Get语言("修改");
            this.toolStripButton_删除.Text = Language_.Get语言("删除");
            this.toolStripButton_保存.Text = Language_.Get语言("保存");

        }

        private void FormClosing_()
        {
            this._Login_sys = null;
        }


        #region 事件

        void 添加()
        {
            using (Form_Login_修改or添加 forms = new Form_Login_修改or添加(this, _操作方式_.添加))
            {
                forms.ShowDialog();
            }
        }

        void 修改()
        {
            int index = -1;
            try
            {
                index = this.listView_userInfo.SelectedIndices[0];
            }
            catch (Exception)
            {
            }
            if (index == -1)
            {
                MessageBox.Show(Language_.Get语言("未选中对象"), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _loginInfo_ loginInfo = this.lstInfo[index];

            //权限低
            if (this._Login_sys._Config.loginInfo.UserType < loginInfo.UserType)
            {
                MessageBox.Show(Language_.Get语言("权限过低"), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //权限低
            else if (this._Login_sys._Config.loginInfo.UserName != loginInfo.UserName &&
                     this._Login_sys._Config.loginInfo.UserType <= loginInfo.UserType)
            {
                MessageBox.Show(Language_.Get语言("权限过低"), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (Form_Login_修改or添加 forms = new Form_Login_修改or添加(this, _操作方式_.修改))
            {
                forms.ShowDialog();
            }
        }

        void 删除()
        {
            int index = -1;
            try
            {
                index = this.listView_userInfo.SelectedIndices[0];
            }
            catch (Exception)
            {
            }
            if (index == -1)
            {
                MessageBox.Show(Language_.Get语言("未选中对象"), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _loginInfo_ loginInfo = this.lstInfo[index];
            //权限低
            if (this._Login_sys._Config.loginInfo.UserType <= loginInfo.UserType)
            {
                MessageBox.Show(Language_.Get语言("权限过低"), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (MessageBox.Show(Language_.Get语言("是否确认删除"), "", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }

            this.lstInfo.RemoveAt(index);
            this.listView_userInfo.Items.RemoveAt(index);
            显示全部信息();
            MessageBox.Show(Language_.Get语言("成功"));
        }

        void 保存()
        {
            this._Login_sys._Config.loginInfo_Beff = this.lstInfo.ToArray();
            this._Login_sys.读写本地用户信息(0);
            this.lstInfo = this._Login_sys._Config.loginInfo_Beff.ToList();
            显示全部信息();
            MessageBox.Show(Language_.Get语言("成功"));
        }

        #endregion


        #region 方法

        void 显示全部信息()
        {
            this.listView_userInfo.Items.Clear();
            foreach (var s in lstInfo)
            {
                ListViewItem item = new ListViewItem(s.UserName);
                item.SubItems.Add(Language_.Get语言($"{s.UserType}"));
                this.listView_userInfo.Items.Add(item);
            }
        }


        #endregion




    }
}
