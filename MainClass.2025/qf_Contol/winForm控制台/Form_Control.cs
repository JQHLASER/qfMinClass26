using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace qf_Contol
{
    public partial class Form_Control : Sunny.UI.UIForm
    {
        CMD_ _cmd;
        public Form_Control(CMD_ cmd,string Title)
        {
            InitializeComponent();
            this.Padding = new System.Windows.Forms.Padding(10, 45, 10, 10);
            this.listBox1.Items.Clear();
            this._cmd = cmd;

            this.Text =Title;

            this.Shown += (s, e) =>
            {
                this.uiTextBox1.Focus();
            };

            this.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    string str = this.uiTextBox1.Text.Trim();
                    this.uiTextBox1.Clear();
                    if (string.IsNullOrEmpty(str))
                    {
                        return;
                    }
                    this._cmd.On_WriteLine(str);
                }
            };

            this.Load += (s, e) => this._cmd.On_Load();
            this.FormClosing += (s, e) => this._cmd.On_FormClosing();

            this.listBox1.DoubleClick += (s, e) =>
            {
                int index=this.listBox1.SelectedIndex;
                if (index < 0)
                {
                    return;
                }
                string v=this.listBox1 .Items[index].ToString();
                new qfNet.软件类().Win_显示信息(v, "");
                this.uiTextBox1.Focus();
            };


        }
    }
}
