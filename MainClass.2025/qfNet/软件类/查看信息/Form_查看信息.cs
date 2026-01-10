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
    public partial class Form_查看信息 : Sunny.UI.UIForm
    {
      //  protected override CreateParams CreateParams { get { CreateParams cp = base.CreateParams; cp.ExStyle |= 0x02000000; return cp; } }//双缓冲显示窗体所有子控件
        private readonly viewModel_查看信息 _DataContext = new viewModel_查看信息();
        public Form_查看信息(string Title_, string Value_, Color ForeColor)
        {
            InitializeComponent();
            this.DataContext = this._DataContext;
            this.DataBindings.Add("Text", this._DataContext, nameof(this._DataContext.Title), false);
            this.uiRichTextBox1  .DataBindings.Add("Text", this._DataContext, nameof(this._DataContext.ShowValue), false);
            this.uiRichTextBox1  .DataBindings.Add("ForeColor", this._DataContext, nameof(this._DataContext.ForeColor), false);

      
            this._DataContext.Title = Title_;
            this._DataContext.ShowValue = Value_;
            this._DataContext.ForeColor = ForeColor;

            
        }

        private void Form_log查看_Load(object sender, EventArgs e)
        {
            new winForm窗体().Set设置_Padding(this, 10);  
            this.uiRichTextBox1  .Dock = DockStyle.Fill;
        }
    }
}
