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
    public partial class Form_语言 : Sunny.UI.UIForm
    {
        public Form_语言()
        {
            InitializeComponent();
            this.uiButton_No.Click += (s, e) => No();
            this.uiButton_Yes.Click += (s, e) => Yes();
            this.FormClosing += (s, e) => FormClosing_();
        }

        private void Form_语言_Load(object sender, EventArgs e)
        {
            qfmain.Language_.Get语言目录(out string[] FileName, out string msgErr);
            this.uiComboBox_语言.DataSource = FileName;
            this.uiComboBox_语言.SelectedIndex = this.uiComboBox_语言.Items.IndexOf(qfmain.Language_.Config.LangeuageCfg.LangeuageName);
        }

         private void FormClosing_()
        {
            
        }

        void Yes()
        {
            qfmain.Language_.Config.LangeuageCfg.LangeuageName = this.uiComboBox_语言.SelectedText;
            qfmain.Language_.读写参数(0);
            MessageBox.Show(Language_.Get语言("此改动在下次启动后生效"));
        }
        void No()
        {
            this.Close();
        }

    }
}
