using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mainclassqf 
{
    public  class pictureBox
    {

        public void 变成圆(System.Windows.Forms.PictureBox picturebox)
        {
            GraphicsPath gp = new GraphicsPath();

            gp.AddEllipse(picturebox.ClientRectangle);

            Region region = new Region(gp);

            picturebox.Region = region;

            gp.Dispose();

            region.Dispose();
        }


     




    }
}
