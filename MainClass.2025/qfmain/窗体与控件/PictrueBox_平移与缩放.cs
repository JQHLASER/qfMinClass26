using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mainclassqf 
{
    public  class PictrueBox_平移与缩放
    {
        #region 平移缩放



        static System.Windows.Forms.PictureBox pic;
        static Control con;

        /// <summary>
        /// 参数: (int)status
        /// </summary>
        public Action<int> Action_缩放事件;


        class Info鼠标_
        {
            internal int X { set; get; } = 0;
            internal int Y { set; get; } = 0;

            internal bool ismove { set; get; } = false;
        }


        Info鼠标_ info鼠标 = new Info鼠标_();

        ContextMenuStrip 右键 = new ContextMenuStrip();








        public void Zoom()
        {
            pic.Width = con.Width;
            pic.Height = con.Height;

            int x = con.Width / 2 - pic.Width / 2;
            int y = con.Height / 2 - pic.Height / 2;



            if (x > 0)
            {
                x = 0;
            }

            if (y > 0)
            {
                y = 0;
            }


            pic.Top = y;
            pic.Left = x;

        }

        /// <summary>
        ///  Event缩放事件方法 :int status >=0鼠标滚轮正,>0鼠标滚轮反 在鼠标滚轮事件时,传出事件正在执行中的状态
        /// </summary>
        /// <param name="pic_"></param>
        /// <param name="con_">一般为panel</param>
        public void 初始化(System.Windows.Forms.PictureBox pic_, Control con_)
        {
            右键.Items.Add("Zoom");
            pic = pic_;
            con = con_;

            pic.SizeMode = PictureBoxSizeMode.Zoom;

            pic.MouseWheel += picturebox_MouseWheel;
            pic.MouseMove += picturebox_MouseMove;
            pic.MouseDown += picturebox_MouseDown;
            pic.MouseUp += pic_MouseUp;

            con.MouseMove += pane_mousemove;
            con.MouseDown += pane_mousemove;
            con.MouseUp += pane_mouseup;
            con.MouseWheel += panel_MouseWheel;

            Zoom();

            右键.Items[0].Click += Zoom_Click;
            con.ContextMenuStrip = 右键;


        }


        void Zoom_Click(object sender, EventArgs e)
        {
            Zoom();
        }


        /// <summary>
        /// pictruebox_MouseWhell事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picturebox_MouseWheel(object sender, MouseEventArgs e)
        {
            if (Action_缩放事件 != null)
            {
                Action_缩放事件(e.Delta);

            }

            int x = e.Location.X;
            int y = e.Location.Y;
            int ow = pic.Width;
            int oh = pic.Height;
            int vx, vy; //因缩放产生的位移矢量



            if (e.Delta > 0)
            {
                if (pic.Width >= con.Width * 20)
                {
                    return;
                }

                pic.Width = (int)Math.Ceiling((double)pic.Width * 1.1);//因为Widthh和Height都是int类型，所以要强制转换一下-_-||
                pic.Height = (int)Math.Ceiling((double)pic.Height * 1.1);

            }
            else if (e.Delta < 0)
            {
                if (pic.Width <= con.Width / 10)
                {
                    return;
                }
                pic.Width = (int)(pic.Width * 0.9);
                pic.Height = (int)(pic.Height * 0.9);
            }
            // 到中心();

            vx = (int)((double)x * (ow - pic.Width) / ow);
            vy = (int)((double)y * (oh - pic.Height) / oh);
            pic.Location = new Point(pic.Location.X + vx, pic.Location.Y + vy);







        }

        /// <summary>
        ///pictureBox1_MouseMove事件
        /// </summary>
        /// <param name="e"></param>
        private void picturebox_MouseMove(object sender, MouseEventArgs e)
        {

            pic.Focus(); //鼠标在picturebox上时才有焦点，此时可以缩放
            if (info鼠标.ismove)
            {
                int x, y;   //新的picturebox1.location(x,y)
                int movex, movey; //x方向，y方向移动大小。
                movex = Cursor.Position.X - info鼠标.X;
                movey = Cursor.Position.Y - info鼠标.Y;
                x = pic.Location.X + movex;
                y = pic.Location.Y + movey;
                pic.Location = new Point(x, y);
                info鼠标.X = Cursor.Position.X;
                info鼠标.Y = Cursor.Position.Y;
            }


        }

        /// <summary>
        /// pictruebox_MouseDown事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picturebox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                info鼠标.X = Cursor.Position.X; //标记鼠标在控件里的坐标
                info鼠标.Y = Cursor.Position.Y;
                info鼠标.ismove = true;

                pic.Focus();


            }




        }


        /// <summary>
        /// pictruebox_MouseUp事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pic_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                info鼠标.ismove = false;
            } //转换鼠标被按下的状态
        }

        private void pane_mousedown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                info鼠标.X = Cursor.Position.X; //记录鼠标左键按下时位置
                info鼠标.Y = Cursor.Position.Y;
                info鼠标.ismove = true;
            }
        }

        private void pane_mouseup(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                info鼠标.ismove = false;
            }
        }

        private void pane_mousemove(object sender, MouseEventArgs e)
        {
            con.Focus(); //鼠标不在picturebox上时焦点给别的控件，此时无法缩放   
            if (info鼠标.ismove)
            {
                int x, y;   //新的picturebox1.location(x,y)
                int movex, movey; //x方向，y方向移动大小。
                movex = Cursor.Position.X - info鼠标.X;
                movey = Cursor.Position.Y - info鼠标.Y;
                x = pic.Location.X + movex;
                y = pic.Location.Y + movey;
                pic.Location = new Point(x, y);
                info鼠标.X = Cursor.Position.X;
                info鼠标.Y = Cursor.Position.Y;
            }
        }


        /// <summary>
        /// pictruebox_MouseWhell事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel_MouseWheel(object sender, MouseEventArgs e)
        {
            if (Action_缩放事件 != null)
            {
                Action_缩放事件(e.Delta);

            }

            int x = e.Location.X;
            int y = e.Location.Y;
            int ow = pic.Width;
            int oh = pic.Height;
            int vx, vy; //因缩放产生的位移矢量



            if (e.Delta > 0)
            {
                if (pic.Width >= con.Width * 20)
                {
                    return;
                }

                pic.Width = (int)Math.Ceiling((double)pic.Width * 1.1);//因为Widthh和Height都是int类型，所以要强制转换一下-_-||
                pic.Height = (int)Math.Ceiling((double)pic.Height * 1.1);

            }
            else if (e.Delta < 0)
            {
                if (pic.Width <= con.Width / 10)
                {
                    return;
                }
                pic.Width = (int)(pic.Width * 0.9);
                pic.Height = (int)(pic.Height * 0.9);
            }
            // 到中心();

            vx = (int)((double)x * (ow - pic.Width) / ow);
            vy = (int)((double)y * (oh - pic.Height) / oh);
            pic.Location = new Point(pic.Location.X + vx, pic.Location.Y + vy);







        }

        #endregion


    }
}
