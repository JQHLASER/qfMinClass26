using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace qfNet
{
    public class MultilineMarkEzd : qfWork.MultilineMarkEzd
    {
        public override void 初始化(bool 使能线程 = true)
        {
            this._标题栏标题_初始化状态 = 标题栏状态_初始化状态();
            base.Event_初始化状态 += On_初始化状态;
            base.初始化(使能线程);
            _Isinistiall = true;
        }
        bool _Isinistiall = false;

        public override void 释放()
        {
            if (!_Isinistiall)
            {
                return;
            }
            base.释放();
            base.Event_初始化状态 -= On_初始化状态;
        }




        public void UserColor_图像(Control 控件, int CardIndex, string File_默认文件夹, out ui_bitmap_jcz多头 userControl)
        {
            userControl = new ui_bitmap_jcz多头(this, CardIndex, File_默认文件夹);
            控件.Controls.Clear();
            控件.Controls.Add(userControl);
        }


        public void Win_设置卡ID()
        {
            using (Form_jcz多头_卡ID设置 forms = new Form_jcz多头_卡ID设置(this))
            {
                forms.ShowDialog();
            }
        }

        public void Win_调试(int CardIndex)
        {
            using (Form_jcz单头_调试 forms = new Form_jcz单头_调试(null, this, CardIndex))
            {
                forms.ShowDialog();
            }
        }

        public void Win_设置激光参数(int CardIndex)
        {
            using (Form_jcz单头_设置  forms = new Form_jcz单头_设置(null, this, CardIndex))
            {
                forms.ShowDialog();
            }
        }


        /// <summary>
        /// OK为成功, None为不加载
        /// </summary>
        /// <param name="CardIndex"></param>
        /// <param name="File_默认文件夹"></param>
        /// <param name="msgErr"></param>
        /// <returns></returns>
        public DialogResult Win_打开(int CardIndex, string File_默认文件夹, out string msgErr)
        {
            msgErr = string.Empty;
            DialogResult dlt = DialogResult.None;
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "ezd|*.ezd";
            open.InitialDirectory = File_默认文件夹;
            dlt = open.ShowDialog();
            if (dlt == DialogResult.OK)
            {
                string ezdName = open.FileName;
                qfWork._Err_jczMarkEzd2_ nerr = (qfWork._Err_jczMarkEzd2_)加载ezd(CardIndex, ezdName, true);
                bool rt = nerr == qfWork._Err_jczMarkEzd2_.成功 ? true : false;
                msgErr = qfWork.JczLmc_Multiline.ErrMsg(nerr);
                dlt = rt ? dlt : DialogResult.No;

                //On_加载Ezd(CardIndex, ezdName, nerr);

            }
            return dlt;
        }

        private _cfg_标题栏状态_[] 标题栏状态_初始化状态()
        {
            string Name = "jcz2multiline激光初始化状态";
            string 名称 = Language_.Get语言("打标卡");
            qfNet._cfg_标题栏状态_[] info = new qfNet._cfg_标题栏状态_[]
           {
              new  qfNet ._cfg_标题栏状态_(Name,$"{名称}{Language_ .Get语言("已初始化")}"  ,(int)qfmain ._初始化状态_  .已初始化  ),
              new  qfNet ._cfg_标题栏状态_(Name,$"{名称}{Language_ .Get语言("初始化中")}"  ,(int)qfmain ._初始化状态_.初始化中 ),
              new  qfNet ._cfg_标题栏状态_(Name  ,$"{名称}{Language_ .Get语言("未初始化")}" ,(int)qfmain ._初始化状态_.未初始化  ),

           };
            return info;
        }

        _cfg_标题栏状态_[] _标题栏标题_初始化状态 = new _cfg_标题栏状态_[0];

        public event Action<_cfg_标题栏状态_[], qfmain  ._初始化状态_> Event_标题栏_初始化状态;
        private void On_初始化状态(qfmain ._初始化状态_ state)
        {
            Event_标题栏_初始化状态?.Invoke(this._标题栏标题_初始化状态, state);
        }






    }
}
