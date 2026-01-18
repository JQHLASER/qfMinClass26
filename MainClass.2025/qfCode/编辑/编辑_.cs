using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfCode
{
    public class 编辑_
    {
        

        public void Win_主窗体(bool Is父窗体 = false)
        {
            using (Form_主窗体 forms = new Form_主窗体(this))
            {
                if (!Is父窗体)
                {
                    forms.ShowInTaskbar = false;
                    forms.MinimizeBox = false;
                    forms.MaximizeBox = false;
                    forms.ShowDialog();
                }
            }
        }




    }
}
