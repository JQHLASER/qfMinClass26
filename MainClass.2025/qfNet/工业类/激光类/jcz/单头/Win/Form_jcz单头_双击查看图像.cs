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

namespace qfNet
{
    public partial class Form_jcz单头_双击查看图像 : Sunny.UI.UIForm
    {
        //双缓冲显示窗体所有子控件
        protected override CreateParams CreateParams { get { CreateParams cp = base.CreateParams; cp.ExStyle |= 0x02000000; return cp; } }
        qfNet.MarkEzd _markEzd = null;
        qfNet.MultilineMarkEzd _multilineMarkEzd = null;
        int _CardIndex = -1;

        public Form_jcz单头_双击查看图像(qfNet.MarkEzd markEzd_, qfNet.MultilineMarkEzd multilineMarkEzd_ = null, int CardIndex = -1)
        {
            InitializeComponent();
            this.Padding = new System.Windows.Forms.Padding(10, 45, 10, 10);
            this.panel1.Dock = DockStyle.Fill;
            this._markEzd = markEzd_;
            this._multilineMarkEzd = multilineMarkEzd_;
            this._CardIndex = CardIndex;

            this.FormClosing += (s, e) => FormClosing_();

            if (this._markEzd != null)
            {
                this._markEzd.Event_获取图像 += On_获取图像;
            }
            else if (this._multilineMarkEzd != null)
            {
                this._multilineMarkEzd.Event_刷新图像 += On_获取图像_multiline;
                this.Text = $"Laser {this._CardIndex + 1}";
            }


        }

        private void Form_jcz单头_双击查看图像_Load(object sender, EventArgs e)
        {
            if (this._markEzd != null)
            {
                this._markEzd.获取_图像(qfWork._激光_获取图像_.获取);
            }
            else if (this._multilineMarkEzd != null)
            {
                this._multilineMarkEzd.刷新图形(this._CardIndex, qfWork._激光_获取图像_.获取);
            }
        }

        private void FormClosing_()
        {
            if (this._markEzd != null)
            {
                this._markEzd.Event_获取图像 -= On_获取图像;
            }
            else if (this._multilineMarkEzd != null)
            {
                this._multilineMarkEzd.Event_刷新图像 -= On_获取图像_multiline;

            }
            this._markEzd = null;
            this._multilineMarkEzd = null;
        }

        #region 事件响应


        async void On_获取图像(qfWork._激光_获取图像_ state)
        {
            var t0 = await getBitmap(state);
        }

        async Task<bool> getBitmap(qfWork._激光_获取图像_ state)
        {
            Bitmap bp = null;
            Task t0 = Task.Run(() =>
            {
                {
                    if (state == qfWork._激光_获取图像_.获取)
                    {
                        bp = this._markEzd.获取_图形((int)this.Width, (int)this.Height);
                    }

                    this.Invoke((Action)(() =>
                    {
                        if (this.panel1.BackgroundImage != null)
                        {
                            this.panel1.BackgroundImage = null;
                        }
                        this.panel1.BackgroundImage = bp;
                    }));
                }
            });
            await t0;
            return true;
        }

        async void On_获取图像_multiline(int Cardindex, qfWork._激光_获取图像_ state)
        {
            var t0 = await getBitmap_multiline(Cardindex, state);
        }
        async Task<bool> getBitmap_multiline(int cardindex, qfWork._激光_获取图像_ state)
        {
            if (this._CardIndex != cardindex)
            {
                return true;
            }
            int w = this.panel1.Width;
            int h = this.panel1.Height;


            Task t0 = Task.Run(() =>
               {

                   Bitmap bp = null;
                   if (state == qfWork._激光_获取图像_.获取)
                   {
                       try
                       {
                           bp = this._multilineMarkEzd.获取图像(this._CardIndex, w, h);
                       }
                       catch (Exception ex)
                       {
                           //Add(false, ex.Message);
                       }

                   }
                   this.Invoke((Action)(() =>
                {
                    if (this.panel1.BackgroundImage != null)
                    {
                        this.panel1.BackgroundImage.Dispose();
                        this.panel1.BackgroundImage = null;
                    }
                    this.panel1.BackgroundImage = bp;

                }));
               });

            await t0;
            return true;
        }






        #endregion


    }
}
