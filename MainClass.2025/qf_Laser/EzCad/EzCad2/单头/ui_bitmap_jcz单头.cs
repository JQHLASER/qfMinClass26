using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace qf_Laser
{
    public partial class ui_bitmap_jcz单头 : UserControl
    {
        //双缓冲显示窗体所有子控件
        //protected override CreateParams CreateParams { get { CreateParams cp = base.CreateParams; cp.ExStyle |= 0x02000000; return cp; } }
        MarkEzd_Ezd2 _markEzd;
         
        public ui_bitmap_jcz单头(MarkEzd_Ezd2 markEzd_)
        {
            InitializeComponent();
            this._markEzd = markEzd_;
            this._markEzd.Event_获取图像 +=(s)=> On_获取图像(s);
            this.DoubleClick += (s, e) => On_双击查看图像();

            this.Dock = DockStyle.Fill;


        }
         

        #region 事件响应

        async void On_获取图像( _激光_获取图像_ state)
        {
            if (this._markEzd._初始化状态 !=_初始化状态_.已初始化)
            {
                return;
            }
            var rt = await GetBitmap(state);
        }


        async Task<bool> GetBitmap(_激光_获取图像_ state)
        {
            Task t0 = Task.Run(() =>
                 {
                     Bitmap bp = null;
                     if (state ==_激光_获取图像_.获取)
                     {
                         bp = this._markEzd.获取_图形((int)this.Width, (int)this.Height);
                     }

                     this.Invoke((Action)(() =>
                     {
                         if (this.BackgroundImage != null)
                         {
                             this.BackgroundImage = null;
                         }
                         this.BackgroundImage = bp;
                     }));
                 });
            await t0;
            return true;
        }


        void On_双击查看图像()
        {

            if (this._markEzd._初始化状态 ==_初始化状态_.已初始化)
            {
                using (Form_jcz单头_双击查看图像 forms = new Form_jcz单头_双击查看图像(this._markEzd))
                {
                    forms.ShowDialog();
                }
            }
        }



        #endregion





    }
}
