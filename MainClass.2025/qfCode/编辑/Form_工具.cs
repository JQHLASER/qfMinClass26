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
    public partial class Form_工具 : Sunny .UI .UIForm 
    {
        public Form_工具()
        {
            InitializeComponent();
            this.panel_控件.BackColor = Color.Transparent;

            this.Load += (s, e) =>
            {
                this.Padding = new System.Windows.Forms.Padding(5,40,5,5);  
            };
        }
    }
}
