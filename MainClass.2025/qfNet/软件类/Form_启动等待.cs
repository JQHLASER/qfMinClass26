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
    internal  partial class Form_启动等待 : Sunny .UI .UIForm  
    {

        string Title = "";
        internal Form_启动等待(string Title_)
        {
            InitializeComponent();
            this.Title = Title_;
            this.label1 .Text = Title_;
        }

      
    }
}
