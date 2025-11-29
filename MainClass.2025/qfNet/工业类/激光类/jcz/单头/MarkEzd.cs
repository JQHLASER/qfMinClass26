using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace qfNet
{
    public class MarkEzd : qfWork.MarkEzd
    {

        public override void 初始化(bool 使能线程=true)
        {
            this._标题栏标题_初始化状态 = 标题栏状态_初始化状态();
            this._标题栏标题_加工状态 = 标题栏状态_加工状态();

            base.Event_初始化状态 += On_初始化状态;
            base.Event_加工状态 += On_加工状态;
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
            base.Event_加工状态 -= On_加工状态;
        }


        /// <summary>
        /// 0:获取,1:清除
        /// </summary>
        /// <param name="state"></param>
        public void 刷新图像(ushort state)
        {
            获取_图像((qfWork._激光_获取图像_)state);
        }

        public void 窗体_设置()
        {
            using (Form_jcz单头_设置 forms = new Form_jcz单头_设置(this))
            {
                forms.ShowDialog();
            }
        }

        public void UserColor_图像(Control 控件)
        {
            控件.Controls.Clear();
            控件.Controls.Add(new ui_bitmap_jcz单头(this));
        }





        /// <summary>
        /// 最大窗口显示
        /// </summary>
        /// <param name="d"></param>
        public void 窗体_查看图像()
        {
            if (this._初始化状态 != qfWork._初始化状态_.已初始化 ||
                this._激光加工状态 != qfWork._激光加工状态_.闲置)
            {
                return;
            }

        }

        public void 窗体_调试()
        {
            if (!this.Err_未初始化(out string msgErr) ||
                !this.Err_红光指示中(out msgErr) || !this.Err_出激光标刻中(out msgErr) ||
                 !this.Err_加载激光模板中(out msgErr))
            {
                MessageBox.Show(msgErr, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            using (Form_jcz单头_调试 forms = new Form_jcz单头_调试(this))
            {
                forms.ShowDialog();
            }

        }




        private _cfg_标题栏状态_[] 标题栏状态_加工状态()
        {
            string Name = "jcz2激光加工状态";
            string 名称 = Language_.Get语言("打标卡");
            qfNet._cfg_标题栏状态_[] info = new qfNet._cfg_标题栏状态_[]
           {
              new  qfNet ._cfg_标题栏状态_(Name,$"{名称}{Language_ .Get语言("闲置")}"  ,(int)qfWork._激光加工状态_ .闲置 ),
              new  qfNet ._cfg_标题栏状态_(Name,$"{名称}{Language_ .Get语言("出激光标刻中")}"  ,(int)qfWork._激光加工状态_.出激光标刻中),
              new  qfNet ._cfg_标题栏状态_(Name  ,$"{名称}{Language_ .Get语言("红指示光中")}" ,(int)qfWork._激光加工状态_.红指示光中 ),
              new  qfNet ._cfg_标题栏状态_(Name  ,$"{名称}{Language_ .Get语言("加载激光模板中")}" ,(int)qfWork._激光加工状态_.加载激光模板中  ),
           };
            return info;
        }
        private _cfg_标题栏状态_[] 标题栏状态_初始化状态()
        {
            string Name = "jcz2激光初始化状态";
            string 名称 = Language_.Get语言("打标卡");
            qfNet._cfg_标题栏状态_[] info = new qfNet._cfg_标题栏状态_[]
           {
              new  qfNet ._cfg_标题栏状态_(Name,$"{名称}{Language_ .Get语言("已初始化")}"  ,(int)qfWork._初始化状态_  .已初始化  ),
              new  qfNet ._cfg_标题栏状态_(Name,$"{名称}{Language_ .Get语言("初始化中")}"  ,(int)qfWork._初始化状态_.初始化中 ),
              new  qfNet ._cfg_标题栏状态_(Name  ,$"{名称}{Language_ .Get语言("未初始化")}" ,(int)qfWork._初始化状态_.未初始化  ),

           };
            return info;
        }

        _cfg_标题栏状态_[] _标题栏标题_初始化状态 = new _cfg_标题栏状态_[0];
        _cfg_标题栏状态_[] _标题栏标题_加工状态 = new _cfg_标题栏状态_[0];


        #region 事件响应

        private void On_初始化状态(qfWork._初始化状态_ state)
        {
            Event_标题栏_初始化状态?.Invoke(this._标题栏标题_初始化状态, state);
        }

        private void On_加工状态(qfWork._激光加工状态_ state)
        {
            Event_标题栏_加工状态?.Invoke(this._标题栏标题_加工状态, state);
        }


        #endregion

        #region 事件

        public event Action<_cfg_标题栏状态_[], qfWork._初始化状态_> Event_标题栏_初始化状态;
        public event Action<_cfg_标题栏状态_[], qfWork._激光加工状态_> Event_标题栏_加工状态;


        #endregion


    }
}
