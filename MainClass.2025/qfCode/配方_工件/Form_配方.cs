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
    public partial class Form_配方 : Sunny .UI .UIForm 
    {
        internal string _配方文件名 = "";
        internal event Action Event_进入时;
   
        /// <summary>
        /// con :  控件
        /// </summary> 
        public Form_配方(Control con)
        {
            InitializeComponent();

            this.panel_设计区.Controls.Clear();
            this.panel_设计区.Controls.Add(con);


            this.Padding = new Padding(10, 45, 10, 10);
            Event_进入时?.Invoke();
           



        }
    }
}
