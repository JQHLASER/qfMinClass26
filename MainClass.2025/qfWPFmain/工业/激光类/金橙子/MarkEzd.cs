using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace qfWPFmain
{
    public class MarkEzd : qf_Laser.MarkEzd_Ezd2
    {

        // qfWork.MarkEzd

        /// <summary>
        /// 显示激光模板中的图像
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="img"></param>
        /// <param name="msgErr"></param>
        /// <returns></returns>
        internal bool 显示图像(int width, int height, out ImageSource img, out string msgErr)
        {
            img = new BitmapImage();
            msgErr = string.Empty;
            bool rt = true;

            try
            {
                var rtMark = 获取图形(width, height);
                rt = rtMark.s;
                msgErr = rtMark.m;
                new bitmapImage_().ImageToBitmapImage(rtMark.v, out BitmapImage img1, out msgErr);
                img = img1;
            }
            catch (Exception ex)
            {
                img = new BitmapImage();
                msgErr = ex.Message;
                On_Log(false, $"jczBitmap,Err,{msgErr}");
            }

            return rt;
        }

        /// <summary>
        /// 0:获取,1:清除
        /// </summary>
        /// <param name="state"></param>
        public void 刷新图像(ushort state)
        {
            刷新图形((qf_Laser._激光_获取图像_)state);
        }

        public void 窗体_设置(Window d)
        {
            d.Dispatcher.Invoke(() =>
            {
                new Win_设置_jczEzcad2(this) { Owner = Window.GetWindow(d) }.ShowDialog();
            });
        }

        /// <summary>
        /// 最大窗口显示
        /// </summary>
        /// <param name="d"></param>
        public void 窗体_显示图像(Window d)
        {
            if (this._初始化状态  != qf_Laser ._初始化状态_.已初始化 ||
                this._激光加工状态 != qf_Laser._激光加工状态_.闲置)
            {
                return;
            }
            d.Dispatcher.Invoke(() =>
            {
                new Win_jcz2_双击查看图像(this) { Owner = Window.GetWindow(d) }.ShowDialog();
            });
        }

        public void 窗体_调试(Window d)
        {
            if (!this.Err_未初始化(out string msgErr) ||
                !this.Err_红光指示中(out msgErr) || !this.Err_出激光标刻中(out msgErr) ||
                 !this.Err_加载激光模板中(out msgErr))
            {
                MessageBox.Show(msgErr, "", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            d.Dispatcher.Invoke(() =>
            {
                new Win_jcz2_调试(this) { Owner = Window.GetWindow(d) }.ShowDialog();
            });
        }



        public void 标题栏状态_初始化(ui_window_Title ui标题栏)
        {
            new Win_标题栏状态().初始化状态_qfWork(ui标题栏, "jcz2初始化", Language_.Get语言("打标卡"), this._初始化状态);
        }


        public void 标题栏状态_加工状态(ui_window_Title ui标题栏)
        {
            string Name = "jcz2激光加工状态";
            string 名称 = Language_.Get语言("打标卡");
            qfWPFmain._windowInfo_[] info = new _windowInfo_[]
           {
              new qfWPFmain._windowInfo_(Name,(int)qfWork._激光加工状态_ .闲置 ,$"{名称}{Language_ .Get语言("闲置")}"  ),
              new qfWPFmain._windowInfo_(Name,(int)qfWork._激光加工状态_.出激光标刻中,$"{名称}{Language_ .Get语言("出激光标刻中")}"  ),
              new qfWPFmain._windowInfo_(Name,(int)qfWork._激光加工状态_.红指示光中  ,$"{名称}{Language_ .Get语言("红指示光中")}"  ),
                   new qfWPFmain._windowInfo_(Name,(int)qfWork._激光加工状态_.加载激光模板中   ,$"{名称}{Language_ .Get语言("加载激光模板中")}"  ),
           };
            ui标题栏.Add(info, (int)this._激光加工状态);
        }


        public event Action<bool, string> Event_Log;
        void On_Log(bool s, string m)
        {
            Event_Log?.Invoke(s, m);
        }
    }
}
