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
    public partial class Form_防重查询 : Sunny.UI.UIForm
    {
        public Form_防重查询()
        {
            InitializeComponent();
            this.Padding = new Padding(5, 40, 5, 5); 


            DateTime now = DateTime.Now;
            this.uiDatePicker_Start.Value = now;
            this.uiDatePicker_End.Value = now;
            this.uiTextBox_内容.Clear();

            this.Shown += (s, e) =>
            {
                this.uiTextBox_内容.ImeMode = ImeMode.Disable;
                this.uiTextBox_内容.Focus();
            };
            this.uiButton_关闭.Click += (s, e) =>
            {
                this.Close();
            };

        }
    }
}
