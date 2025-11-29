using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace qfNet
{
    public class 显示
    {

        public virtual void 加工状态(Sunny.UI.UIPanel panel, Color cor, string msg)
        {
            panel.FillColor = cor;
            panel.Text = msg;
        }


    }
}
