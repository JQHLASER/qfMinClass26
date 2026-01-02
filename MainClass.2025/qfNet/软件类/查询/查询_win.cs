using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfNet
{
    public class 查询_win
    {





        public void 窗体_查询()
        {
            using (Form_查询 forms = new Form_查询())
            {
                forms.Event_查询 += () =>
                {

                };
                forms.ShowDialog();
            }
        }









    }
}
