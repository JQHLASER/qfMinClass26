using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace qfCode
{
    public partial class Form_视图 : Sunny.UI.UIForm
    {
        public Form_视图()
        {
            InitializeComponent();

            this.uiTextBox_左边栏.IntValue = Form_主窗体.forms._视图._cfg.左边栏;
            this.uiTextBox_下边栏.IntValue = Form_主窗体.forms._视图._cfg.下边栏;

            this.uiButton_No.Click += (s, e) =>
            {
                this.Close();
            };

            this.uiButton_Yes.Click += (s, e) =>
            {
                Form_主窗体.forms._视图._cfg.左边栏=  this.uiTextBox_左边栏.IntValue ;
                Form_主窗体.forms._视图._cfg.下边栏= this.uiTextBox_下边栏.IntValue ;
                Form_主窗体.forms._视图.读写参数(0);
                Form_主窗体.forms.视图设置();
            };


        }
    }
}
