using System;
using System.Windows.Forms;

namespace qfNet
{
    public partial class Form_软件注册 : Sunny.UI.UIForm
    {
        protected override CreateParams CreateParams { get { CreateParams cp = base.CreateParams; cp.ExStyle |= 0x02000000; return cp; } }//双缓冲显示窗体所有子控件

        qfNet.软件注册 软件注册_sys;
        private readonly viewModel_软件注册 _DataContext;
        public Form_软件注册(qfNet.软件注册 软件注册_sys_)
        {
            _DataContext = new viewModel_软件注册(软件注册_sys_);

            InitializeComponent();
            this.DataContext = this._DataContext;

            this.DataBindings.Add("Text", this._DataContext, nameof(this._DataContext.Title), false);
            this.uiLabel_机器码.DataBindings.Add("Text", this._DataContext, nameof(this._DataContext.机器码_language), false);
            this.uiLabel_注册码.DataBindings.Add("Text", this._DataContext, nameof(this._DataContext.注册码_language), false);
            this.uiCheckBox_试用.DataBindings.Add("Text", this._DataContext, nameof(this._DataContext.试用_language), false);

            this.uiTextBox_机器码.DataBindings.Add("Text", this._DataContext, nameof(this._DataContext.机器码), false);
            this.uiTextBox_注册码.DataBindings.Add("Text", this._DataContext, nameof(this._DataContext.注册码), false, DataSourceUpdateMode.OnPropertyChanged);
            this.uiCheckBox_试用.DataBindings.Add("Checked", this._DataContext, nameof(this._DataContext.Is试用), false, DataSourceUpdateMode.OnPropertyChanged);
            this.uiCheckBox_试用.DataBindings.Add("Visible", this._DataContext, nameof(this._DataContext.CheckShow), false);

            this.label_信息.DataBindings.Add("Text", this._DataContext, nameof(this._DataContext.信息), false);
            this.pictureBox_二维码.DataBindings.Add("Image", this._DataContext, nameof(this._DataContext.image机器码), false);



            软件注册_sys = 软件注册_sys_;
            this._DataContext.Title = $"{Language_.Get语言("软件授权")}--{软件注册_sys.版本}";
            this.FormClosing += (s, e) => Event_FormClosing();
            this.uiButton_注册.Click += (s, e) => Event_注册();
            this.uiCheckBox_试用.Click += (s, e) => On_试用();
            this.KeyDown += (s, e) => On_KeyDown(e);
            this.pictureBox_二维码.DoubleClick += (s, e) => 设置Tcp();
        }


        void 设置Tcp()
        {
            this._DataContext.TcpClien_设置窗体();
        }

        void On_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode==Keys.F12 )
            {
                设置Tcp();
            }
        }


        void Event_注册()
        {
            if (this._DataContext.注册(this.软件注册_sys, out string msgErr))
            {
                MessageBox.Show(msgErr);
            }
            else
            {
                MessageBox.Show(msgErr, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        void Event_FormClosing()
        {
            this._DataContext.释放();
            this.软件注册_sys = null;
        }

        void On_试用()
        {
            this._DataContext.试用();
        }



        private void Form_软件注册_Load(object sender, EventArgs e)
        {




        }
    }
}
