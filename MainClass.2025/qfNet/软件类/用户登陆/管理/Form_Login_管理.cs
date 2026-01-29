using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
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

        /// <summary>
        /// 显示到表用
        /// </summary>
        public class _UserInfoShow_
        {
            public string UserName { set; get; } = "";
            public string UserType { set; get; } = "";
        }


        // protected override CreateParams CreateParams { get { CreateParams cp = base.CreateParams; cp.ExStyle |= 0x02000000; return cp; } }//双缓冲显示窗体所有子控件
        internal Login登陆 _Login_sys;
        Form_Login_管理 forms;

        internal List<_loginInfo_> _lstInfo = new List<_loginInfo_>();
        /// <summary>
        /// 显示用的
        /// </summary>
        internal BindingList<_UserInfoShow_> _lstBindingInfo = new BindingList<_UserInfoShow_>();


        DataGridview_ _datagrid;

        internal Form_Login_管理(Login登陆 Login_sys_)
        {
            InitializeComponent();
            this._Login_sys = Login_sys_;
            forms = this;
            _datagrid = new DataGridview_(this.dataGridView1).设置行高(35);
            this._lstInfo = this._Login_sys._Config.loginInfo_Beff.Select(i => i.Clone()).ToList();
            Show所有用户信息();
            this.dataGridView1.DataSource = this._lstBindingInfo;


            this.toolStripButton_添加.Click += (s, e) => 添加();
            this.toolStripButton_修改.Click += (s, e) => 修改();
            this.toolStripButton_删除.Click += (s, e) => 删除();
            this.toolStripButton_保存.Click += (s, e) => 保存();

            this.dataGridView1.DoubleClick += (s, e) => 修改();
            this.FormClosing += (s, e) => FormClosing_();
            this.Shown += (s, e) =>
            {
                _datagrid.格式化()
                         .列标题显示or隐藏(false) 
                         .设置字体_整体(new Font("微软雅黑", 9f))
                         .设置列宽(0, 300)
                         .设置列宽(1, 300)
                         .设置行高(35)
                         .列为只读()
                         .使能修改列宽(true);
            };
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

        internal int 获取选中行数()
        {
            new DataGridview_(this.dataGridView1).获取当前选中的行号(out int index);
            return index;
        }



        #region 事件





        void 添加()
        {
            using (Form_Login_修改or添加 forms = new Form_Login_修改or添加(this, _操作方式_.添加))
            {
                if (forms.ShowDialog() == DialogResult.OK)
                {
                    this._lstInfo.Add(forms._当前选中用户信息.Clone());
                    this._lstBindingInfo.Add(new _UserInfoShow_
                    {
                        UserName = forms._当前选中用户信息.UserName,
                        UserType = 语言_权限(forms._当前选中用户信息.UserType),
                    });


                }
            }
        }

        void 修改()
        {
            int index = 获取选中行数();

            if (index == -1)
            {
                MessageBox.Show(Language_.Get语言("未选中对象"), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _loginInfo_ loginInfo = this._lstInfo[index];

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
                if (forms.ShowDialog() == DialogResult.OK)
                {
                    this._lstInfo[index] = forms._当前选中用户信息;
                    this._lstBindingInfo[index] = new _UserInfoShow_
                    {
                        UserName = forms._当前选中用户信息.UserName,
                        UserType = 语言_权限(forms._当前选中用户信息.UserType),
                    };
                }
            }
        }

        void 删除()
        {
            int index = 获取选中行数();

            if (index == -1)
            {
                MessageBox.Show(Language_.Get语言("未选中对象"), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _loginInfo_ loginInfo = this._lstInfo[index];
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

            this._lstInfo.RemoveAt(index);
            this._lstBindingInfo.RemoveAt(index);
            MessageBox.Show(Language_.Get语言("成功"));
        }

        void 保存()
        {
            this._Login_sys._Config.loginInfo_Beff = this._lstInfo.ToArray();
            this._Login_sys.读写本地用户信息(0);
            this._lstInfo = this._Login_sys._Config.loginInfo_Beff.Select(i => i.Clone()).ToList();
            Show所有用户信息();
            MessageBox.Show(Language_.Get语言("成功"));
        }

        #endregion


        #region 方法

        internal string 语言_权限(_LoginUserType_ type)
        {
            return this._Login_sys.语言_用户权限(type);
        }
        void Show所有用户信息()
        {
            this._lstBindingInfo.Clear();
            foreach (var s in _lstInfo)
            {
                this._lstBindingInfo.Add(new _UserInfoShow_
                {
                    UserName = s.UserName,
                    UserType = 语言_权限(s.UserType),
                });
            }
        }

        #endregion




    }
}
