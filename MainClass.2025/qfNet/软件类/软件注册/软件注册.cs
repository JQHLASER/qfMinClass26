 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 

namespace qfNet
{
    public class 软件注册 : qfmain.软件注册
    {
        public 软件注册(qfmain._软件授权_注册类型_ 注册类型_) : base(注册类型_)
        {

        }

        public void 窗体_软件授权()
        {
            using (Form_软件注册 form = new Form_软件注册(this))
            {
                form.ShowDialog();
            }
             
        }

        public void 标题栏状态(窗体_标题栏状态 ui标题栏)
        {
            int a = -1;
            if (this._err == qfmain._软件授权_Err_.开始注册)
            {
                a = -7;
            }
            else if (this._err == qfmain._软件授权_Err_.已完全注册 ||
                     this._err == qfmain._软件授权_Err_.已日期注册)
            {
                a = 0;
            }
            else if (this._err == qfmain._软件授权_Err_.未检测到加密狗)
            {
                a = (int)this._err;
            }
            else if (this._err == qfmain._软件授权_Err_.未检测到匹配的加密狗)
            {
                a = (int)this._err;
            }
            else
            {
                a = -1;
            }
            _cfg_标题栏状态_[] info = new _cfg_标题栏状态_[]
           {
                new _cfg_标题栏状态_("软件",Language_ .Get语言("软件已授权"),0),
                new _cfg_标题栏状态_("软件",Language_ .Get语言("软件未授权"),-1),
                new _cfg_标题栏状态_("软件",Language_ .Get语言("软件未授权"),-7),
                new _cfg_标题栏状态_("软件",Language_ .Get语言("未检测到加密狗"),-3),
                new _cfg_标题栏状态_("软件",Language_ .Get语言("未检测到匹配的加密狗"),-4),
           };
            ui标题栏.Add(info, a);
        }





    }
}
